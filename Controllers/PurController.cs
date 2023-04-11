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
using Newtonsoft.Json;
using System.Text;

namespace layering.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurController : ControllerBase
    {
        private readonly IPurServ<Pur> PurControl;
        private readonly IProductServ<Product> prodService;
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(PurController));

        public PurController(IPurServ<Pur> context, IProductServ<Product> _prodService)
        {
            
            PurControl = context;
            prodService = _prodService;
        }

        // GET: api/Pur
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pur>>> GetPurs()
        {
            _log4net.Info("GetPur method is called");

            return  PurControl.getallpurs();
        }

        // GET: api/Pur/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Pur>> GetPur(int id)
        {
            _log4net.Info("Purchase "+id+" is called");
            var pur = PurControl.getpur(id);

            if (pur == null)
            {
                return NotFound();
            }

            return pur;
        }
        [HttpGet("getcuspur")]
        public async Task<IActionResult> getcusid(int cid)
        {
            _log4net.Info("customer "+cid+" method is called");
            return Ok(PurControl.getallpurs().Where(x=>x.Cid==cid));
        }



        // POST: api/Pur
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Pur>> PostPur(Pur pur)
        {
            
            try
            {
                _log4net.Info("PostPur method is called");
                PurControl.postpur(pur);
                
            }
            catch (DbUpdateException)
            {
                if (PurExists(pur.Puid))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(pur);
        }

        //DELETE: api/Pur/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePur(int id)
        {
            _log4net.Info("DeletePur method is called");
            var pur = PurControl.getpur(id);
            if (pur == null)
            {
                return NotFound();
            }

            PurControl.deletepur(id);
            

            return NoContent();
        }


        [HttpPost("/addPurchase/{id}/{Cid}")]
        public async Task<ActionResult<Pur>> placeorder(int id, int Cid, Product p)
        {
            _log4net.Info("Placeorder method is called");
            Product s = prodService.getproduct(id);
            Random r = new Random();
            Pur pr = new();
            pr.Puid = r.Next();
            pr.Pname = s.Pname;
            pr.Pid = s.Pid;
            pr.Flag = true;
            if (s.Pquantity == 0)
            {
                return BadRequest("Product not available");
            }
            if (p.Pquantity <= s.Pquantity && p.Pquantity > 0)
            {
                s.Pquantity = s.Pquantity - p.Pquantity;
                pr.Purquantity = p.Pquantity;
                pr.TotalPrice = pr.Purquantity * (int)s.Pcost;
                pr.Cid = Cid;

                PurControl.postpur(pr);

                prodService.UpdateProduct(s.Pid,s);
            }
            else
            {
                return BadRequest("Enter purchase quantity that is lesser than the product quantity");
            }

            return pr;
        }
        private bool PurExists(int id)
        {
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
