using layering.Models;
using layering.Repository;

namespace layering.Services
{
    public class CustomerServ: ICustomerServ<Customer>
    {
        private readonly ICustomerRepo<Customer> repobj;
        public CustomerServ(ICustomerRepo<Customer> robj)
        {
            repobj = robj;
        }

        public void addcus(Customer c)
        {
            repobj.addcus(c);
        }

        public List<Customer> getallcus()
        {
            return repobj.getallcus();
        }

        public Customer getcusbyid(int id)
        {
            return repobj.getcusbyid(id);
        }

        public void postcus(int id, Customer c)
        {
             repobj.postcus(id, c);
        }

         
    }
}
