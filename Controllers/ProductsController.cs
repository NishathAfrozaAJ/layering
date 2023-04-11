using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using layering.Models;
using layering.Services;

namespace layering.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductServ<Product> productcontrol;
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(ProductsController));
        public ProductsController(IProductServ<Product> context)
        {

            productcontrol = context;
        }

        // GET: api/Example
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        
        {
            _log4net.Info("GetProducts method is called");
            return productcontrol.getallproducts();
        }
        [HttpGet]
        [Route("GetProduct0")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProduct0()

        {
            _log4net.Info("GetProducts method is called");
            return productcontrol.getproduct0();
        }

        // GET: api/Example/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            _log4net.Info("Product "+id+" is called");
            var product =  productcontrol.getproduct(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // PUT: api/Example/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            Product productFromDB = productcontrol.getproduct(id);

            if (productFromDB != null)
            {
                try
                {
                        var a = productcontrol.getallproducts();
                        var s1 = (from i in a
                                  where i.Pname == product.Pname && i.Pid != product.Pid
                                  select i).SingleOrDefault();
                        if (s1 == null) { productcontrol.UpdateProduct(id, product); }
                        else { return BadRequest("Product name already exists"); }
                                         
                }
                catch (DbUpdateException)
                {
                    if (!ProductExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                catch (Exception e)
                {
                    if (!ProductExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        return BadRequest(e.Message);
                    }
                }
            }
            else
            {
                return BadRequest("Product not found!");
            }
            return Ok(product);
        }

        // POST: api/Example
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Route("Create")]
        public async Task<ActionResult<Product>> create(Product p)
        {
            try
            {
                _log4net.Info("create method is called");
                var s1 = productcontrol.getallproducts();
                var r = (from i in s1
                         where (i.Pname == p.Pname) || (i.Pid == p.Pid)
                         select i).SingleOrDefault();
               
                
                    if (r == null)
                    {
                        productcontrol.AddProduct(p);
                        return Ok(r);
                    }
                    else
                    {
                        return BadRequest("The Product ID already exists");
                    }
            }
            catch (Exception ex)
            {
                return BadRequest("Error while updating");
            }
        }
        
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            
            try
            {
                _log4net.Info("Postproduct method is called");
                productcontrol.AddProduct(product);
                //await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ProductExists(product.Pid))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetProduct", new { id = product.Pid }, product);
        }

        // DELETE: api/Example/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            _log4net.Info("DeleteProduct method is called");
            var product = productcontrol.getproduct(id);
            if (product == null)
            {
                return NotFound();
            }

            productcontrol.DeleteProduct(id);
            //_context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(int id)
        {
            _log4net.Info("ProductExists method is called");
            if (id == null)
            {
                return false;
            }
            else
            {
                return true;
            }

            
        }
    }
}
