using layering.Models;

namespace layering.Repository
{
    public class StockistRepo: IStockistRepo<Stockist>
    {
        private readonly stockContext db;
        public StockistRepo(stockContext _db) 
        {
            db = _db;
        }

        public List<Stockist> GetAll()
        {
            return db.Stockists.ToList();
        }
    }
}
