using layering.Models;
using layering.Repository;

namespace layering.Services
{
    public class StockistServ:IStockistServ<Stockist>
    {
        private readonly IStockistRepo<Stockist>repobj;
        
        public StockistServ(IStockistRepo<Stockist> robj)
        {
            repobj = robj;
        }

        public List<Stockist> GetAll()
        {
            return repobj.GetAll();
        }
    }
}
