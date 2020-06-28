using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.EntityFrameworkCore;
using ProductAPIWithEF.Business.Interfaces;
using ProductAPIWithEF.Data;
using ProductAPIWithEF.Models;
using Swashbuckle.AspNetCore.SwaggerGen;


namespace ProductAPIWithEF.Controllers
{
    [ApiController]
    [Route("v1/products")]
    public class ProductController : ControllerBase
    {
        private readonly IProductBusiness _productBusiness;

        public ProductController(IProductBusiness productBusiness)
        {
            _productBusiness = productBusiness;
        }


        [HttpGet]
        [Route("")]
        public async Task<ActionResult> Get()
        {
            try
            {
                var result = await _productBusiness.GetAll();
                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult> GetById(int id)
        {
            try
            {
                var result = await _productBusiness.GetById(id);
                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
        }

        [HttpGet]
        [Route("categories/{id:int}")]
        public async Task<ActionResult> GetByCategory(int id)
        {
            try
            {
                var result = await _productBusiness.GetByCategory(id);
                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
        }

        [HttpPost]
        [Route("")]
        public IActionResult Post([FromBody] Product model)
        {
            try
            {
                var result = _productBusiness.Create(model);
                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
        }


        [HttpPut]
        [Route("{id}")]
        public IActionResult Put([FromBody] Product model)
        {
            try
            {
                var result = _productBusiness.Update(model);
                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                _productBusiness.Delete(id);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest("Erro:" + e.Message);
            }
        }
    }
}
