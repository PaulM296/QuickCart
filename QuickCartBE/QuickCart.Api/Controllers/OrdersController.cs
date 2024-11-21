﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuickCart.Api.DTOs;
using QuickCart.Api.Extensions;
using QuickCart.Domain.Entities;
using QuickCart.Domain.Entities.OrderAggregates;
using QuickCart.Domain.Interfaces;
using QuickCart.Domain.Specifications;

namespace QuickCart.Api.Controllers
{
    [Authorize]
    public class OrdersController(ICartService cartService, IUnitOfWork unitOfWork) : BaseApiController
    {
        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder(CreateOrderDto createOrderDto)
        {
            var email = User.GetEmail();
            var cart = await cartService.GetCartAsync(createOrderDto.CartId);

            if(cart == null)
                return BadRequest("Cart not found!");

            if(cart.PaymentIntentId == null)
                return BadRequest("No payment intent for this order!");

            var items = new List<OrderItem>();

            foreach(var item in cart.Items)
            {
                var productItem = await unitOfWork.Repository<Product>().GetByIdAsync(item.ProductId);

                if (productItem == null)
                    return BadRequest("Problem with the order");

                var itemOrdered = new ProductItemOrdered
                {
                    ProductId = item.ProductId,
                    ProductName = item.ProductName,
                    PictureUrl = item.PictureUrl,
                };

                var orderItem = new OrderItem
                {
                    ItemOrdered = itemOrdered,
                    Price = productItem.Price,
                    Quantity = item.Quantity
                };

                items.Add(orderItem);
            }

            var deliveryMethod = await unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(createOrderDto.DeliveryMethodId);

            if (deliveryMethod == null)
                return BadRequest("No delivery method selected!");

            var order = new Order
            {
                OrderItems = items,
                DeliveryMethod = deliveryMethod,
                ShippingAddress = createOrderDto.ShippingAddress,
                Subtotal = items.Sum(x => x.Price * x.Quantity),
                PaymentSummary = createOrderDto.PaymentSummary,
                PaymentIntentId = cart.PaymentIntentId,
                BuyerEmail = email
            };

            unitOfWork.Repository<Order>().Add(order);

            if(await unitOfWork.Complete())
            {
                return order;
            }

            return BadRequest("Problem creating order!");
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Order>>> GetOrdersForUser()
        {
            var spec = new OrderSpecification(User.GetEmail());

            var orders = await unitOfWork.Repository<Order>().ListAsync(spec);

            return Ok(orders);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Order>> GetOrderById(int id)
        {
            var spec = new OrderSpecification(User.GetEmail(), id);

            var order = await unitOfWork.Repository<Order>().GetEntityWithSpec(spec);

            if (order == null)
                return NotFound();

            return order;
        }
    }
}
