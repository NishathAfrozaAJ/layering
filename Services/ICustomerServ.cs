using layering.Models;

namespace layering.Services
{
    public interface ICustomerServ<Customer>
    {
        List<Customer> getallcus();
        Customer getcusbyid(int id);
        void postcus(int id, Customer c);
        void addcus(Customer c);
    }
}
