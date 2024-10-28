﻿namespace QuickCart.Domain.Entities
{
    public class ShoppingCart
    {
        public required string Id { get; set; }
        public List<CartItem> Items { get; set; } = [];
    }
}