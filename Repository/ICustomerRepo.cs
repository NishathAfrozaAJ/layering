using layering.Models;

namespace layering.Repository
{
    public interface ICustomerRepo<Customer>
    {
        List<Customer> getallcus();
        Customer getcusbyid(int id);
        void postcus(int id,Customer c);

        void addcus(Customer c);
    }
}
