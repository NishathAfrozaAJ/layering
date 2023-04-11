using layering.Models;
using layering.Repository;

namespace layering.Services
{
    public class ProductServ:IProductServ<Product>
    {
        private readonly IProductRepo<Product> repobj;
        public ProductServ(IProductRepo<Product> robj)
        {
            repobj = robj;
        }

        public void AddProduct(Product p)
        {
            repobj.AddProduct(p);
        }

        public void DeleteProduct(int id)
        {
            repobj.DeleteProduct(id);
        }

        public List<Product> getallproducts()
        {
            return repobj.getallproducts();
        }

        public Product getproduct(int id)
        {
            return repobj.getproduct(id);
        }
        public List<Product> getproduct0()
        {
            var p = repobj.getallproducts();
            var a = from i in p
                    where i.Pquantity > 0
                    select i;
            return a.ToList();
        }

        public Task<Product> UpdateProduct(int id, Product p)
        {
            try
            {
                return repobj.UpdateProduct(id, p);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
