using layering.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace layering.Repository
{
    public class ProductRepo:IProductRepo<Product>
    {
        private readonly stockContext db;
        public ProductRepo (stockContext _db)
        {
            db = _db;
        }

        public void AddProduct(Product p)
        {
            db.Products.Add(p);
            db.SaveChanges();

        }

        public void DeleteProduct(int id)
        {
            Product p = db.Products.Find(id);
            db.Products.Remove(p); 
            db.SaveChanges();
        }

        public List<Product> getallproducts()
        {
            return db.Products.ToList();
        }

        public Product getproduct(int id)
        {
            Product p = db.Products.Find(id);
            return p;
        }

        public async Task<Product> UpdateProduct(int id, Product p)
        {
            try
            {
                Product temp = db.Products.Find(id);
               
                          
                    temp.Pname = p.Pname;
                    //temp.Purs = p.Purs;
                    temp.Pquantity = p.Pquantity;
                    //temp.Purchase = p.Purchase;
                    temp.Pcost = p.Pcost;
                    db.Products.Update(temp);
                    db.SaveChanges();
                    return p;
                
                
            }
            catch(DbUpdateException e)
            {
                throw new Exception(e.Message);
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
/*
 {
  "pid": 1,
  "pname": "Pediasure",
  "pcost": 900,
  "pquantity": 92
}
 */
