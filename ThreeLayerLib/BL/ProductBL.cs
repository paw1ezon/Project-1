using Persistence;
using DAL;

namespace BL
{
    public class ProductBL
    {
        ProductDAL pDAL = new ProductDAL();
        public List<Product> GetAllProduct()
        {
            return pDAL.GetProducts();
        }
        public Product GetProductByID(int id)
        {
            return pDAL.GetProductByID(id);
        }
        public decimal GetPriceByProductName(string productName)
        {
            List<Product> products = pDAL. GetProducts();
            foreach (Product product in products)
            {
                if(product.Name == productName)
                return product.Price;
            }
            return 0;
        }

    }
}