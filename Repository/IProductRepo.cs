namespace layering.Repository
{
    public interface IProductRepo<Product>
    {
        List<Product> getallproducts();
        Product getproduct(int id);
        void AddProduct(Product p);
        Task<Product> UpdateProduct(int id, Product p);
        void DeleteProduct(int id);
    }
}
