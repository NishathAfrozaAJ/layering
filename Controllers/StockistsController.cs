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
    public class StockistsController : ControllerBase
    {
        private readonly IStockistServ<Stockist> StockistControl;
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(StockistsController));
        public StockistsController(IStockistServ<Stockist> context)
        {
            StockistControl = context;
        }

        // GET: api/Stockists
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Stockist>>> GetStockists()
        {
            _log4net.Info("GetStockists method is called");
            return StockistControl.GetAll();
        }
        [HttpPost]
        public async Task<ActionResult<Stockist>> login(Stockist s)
        {
            _log4net.Info("login method is called");
            var s1 = StockistControl.GetAll();
            if(s.StName== null)
            {
                return BadRequest("Admin Name required");
            }
            if(s.Password== null) { return BadRequest("Admin Name required"); }
            
            var res = (from i in s1
                       where (i.StName == s.StName) && (i.Password == s.Password)
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


    }
}
