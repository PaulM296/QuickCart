﻿using Microsoft.AspNetCore.Mvc;
using QuickCart.Api.RequestHelpers;
using QuickCart.Domain.Entities;
using QuickCart.Domain.Interfaces;
using QuickCart.Domain.Specifications;

namespace QuickCart.Api.Controllers
{
    public class ProductsController(IBaseRepository<Product> repo) : BaseApiController
    {

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts(
            [FromQuery]ProductSpecParams specParams)
        {
            var spec = new ProductSpecification(specParams);

            return await CreatePagedResult(repo, spec, specParams.PageIndex, specParams.PageSize);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await repo.GetByIdAsync(id);

            if(product == null) 
                return NotFound();

            return product;
        }

        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
            repo.Add(product);

            if (await repo.SaveAllAsync())
            {
                return CreatedAtAction("GetProduct", new { id = product.Id }, product);
            }

            return BadRequest("Problem creating product.");
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateProduct(int id, Product product)
        {
            if (product.Id != id || !ProductExists(id))
                return BadRequest("Cannot update this product.");

            repo.Update(product);

            if (await repo.SaveAllAsync())
            {
                return NoContent();
            }

            return BadRequest("Problem updating the product.");
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var product = await repo.GetByIdAsync(id);

            if(product == null)
                return NotFound();

            repo.Remove(product);

            if (await repo.SaveAllAsync())
            {
                return NoContent();
            }

            return BadRequest("Problem deleting the product.");
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<string>>> GetBrands()
        {
            var spec = new BrandListSpecification();

            return Ok(await repo.ListAsync(spec));
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<string>>> GetTypes()
        {
            var spec = new TypeListSpecification();
            
            return Ok(await repo.ListAsync(spec));
        }

        private bool ProductExists(int id)
        {
            return repo.Exists(id);
        }

    }
}