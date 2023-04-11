using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using layering.Models;
using layering.Services;
using static System.Collections.Specialized.BitVector32;

namespace layering.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerServ<Customer> CustomerControl;
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(CustomerController));
        public CustomerController(ICustomerServ<Customer> context)
        {
            CustomerControl = context;
        }

        // GET: api/Customer
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            _log4net.Info("Getcustomers method is called");
            return  CustomerControl.getallcus();
        }

        // GET: api/Customer/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            _log4net.Info("The customer "+id+" is called");
            var customer = CustomerControl.getcusbyid(id);

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);    
        }

        [HttpPost]
        [Route("Loginc")]
        public async Task<ActionResult<Customer>> Loginc(Customer c)
        {
            try
            {
                _log4net.Info("Loginc method is called");
                var s1 = CustomerControl.getallcus();
                if(c.Cname== null) { return BadRequest("Customer Name is required"); }
                if (c.Pass == null) { return BadRequest("Password is required"); }
                var res = (from i in s1
                           where (i.Cname == c.Cname) && (i.Pass == c.Pass)
                           select i).SingleOrDefault();
                if (res != null)
                {
                    return Ok(res);

                }
                else
                {
                    return BadRequest("Invalid Username or Password");
                }
            }
            catch(Exception ex)
            {
                return BadRequest("Error while updating");
            }
        }
        [HttpPost]
        [Route("create")]
        public async Task<ActionResult<Customer>> create(Customer c)
        {
            try
            {
                _log4net.Info("create method is called");
                var s1 = CustomerControl.getallcus();
                if (c.Cname == null) { return BadRequest("Customer Name is required"); }
                if (c.Pass == null) { return BadRequest("Password is required"); }
                if (c.Cpass == null) { return BadRequest("Confirm Password is required"); }
                var r = (from i in s1
                         where (i.Cname == c.Cname)
                         select i).SingleOrDefault();
                if(c.Pass==c.Cpass)
                {
                    if (r == null)
                    {
                        CustomerControl.addcus(c);
                        return Ok(r);
                    }
                    else
                    {
                        return BadRequest("The Username already exists");
                    }
                }
                else
                {
                    return BadRequest("The Passwords do not match");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Error while updating");
            }
        }
        // POST: api/Customer
        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer(int id,Customer customer)
        {
            try
            {
                _log4net.Info("Pstcustomer method is called");
                CustomerControl.postcus(id,customer);
            }
            catch (DbUpdateException)
            {
                if (CustomerExists(customer.Cid))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCustomer", new { id = customer.Cid }, customer);
        }

        private bool CustomerExists(int id)
        {
            _log4net.Info("CustomerExists method is called");
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
