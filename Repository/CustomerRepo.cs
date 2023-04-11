using layering.Models;
using layering.Repository;

namespace layering.Repository
{
    public class CustomerRepo:ICustomerRepo<Customer>
    {
        private readonly stockContext db;
        public CustomerRepo(stockContext _db)
        {
            db = _db;
        }

        public void addcus(Customer c)
        {
            db.Customers.Add(c);
            db.SaveChanges();
        }

        public List<Customer> getallcus()
        {
            return db.Customers.ToList();
        }

        public Customer getcusbyid(int id)
        {
            Customer a = db.Customers.Find(id);
            return a;
        }

        public void postcus(int id, Customer c)
        {
            db.Customers.Update(c);
            db.SaveChanges();
        }

        
    }
}
