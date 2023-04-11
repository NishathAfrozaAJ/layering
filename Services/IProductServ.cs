namespace layering.Services
{
    public interface IProductServ<Product>
    {
        List<Product> getallproducts();
        List<Product> getproduct0();
        Product getproduct(int id);
        void AddProduct(Product p);
        Task<Product> UpdateProduct(int id, Product p);
        void DeleteProduct(int id);


    }
}
