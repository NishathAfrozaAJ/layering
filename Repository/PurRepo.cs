using layering.Models;

namespace layering.Repository
{
    public class PurRepo : IPurRepocs<Pur>
    {
        private readonly stockContext db;
        public PurRepo(stockContext _db)
        {
            db = _db;
        }

        public void deletepur(int id)
        {
            Pur p=db.Purs.Find(id);
            db.Purs.Remove(p);
            db.SaveChanges();

        }

        public Pur get(int id)
        {
           Pur p=db.Purs.Find(id);
            return (p);

        }

        public List<Pur> getallpurs()
        {
            return db.Purs.ToList();

        }

        public void postpur( Pur pur)
        {
            db.Purs.Add(pur);
            db.SaveChanges();
        }
    }
}
