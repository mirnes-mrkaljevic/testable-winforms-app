using NLog;
using Desktop.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Desktop.Client.DBAccess
{
    public class ProductDataAccess : IProductDataAccess
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public string ErrorMessage { get; set; }

        public IList<Product> GetAllProducts()
        {
            try
            {
                using (DBAContext context = DBAContext.GetContext())
                {
                    return context.Products.ToList();
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error during retrieving data from database." + ex.Message);
                return new List<Product>();
            }
        }

        public Product GetProduct(int id)
        {
            try
            {
                using (DBAContext context = DBAContext.GetContext())
                {

                    return context.Products.SingleOrDefault(x => x.Id == id);

                }
            }
            catch (Exception ex)
            {
                logger.Error("Error during retrieving data from database." + ex.Message);
                ErrorMessage = "Error during retrieving data from database." + ex.Message;
                return null;
            }
        }

        public bool AddProduct(Product product)
        {
            try
            {
                using (DBAContext context = DBAContext.GetContext())
                {
                    product.LastUpdated = DateTime.Now;
                    context.Products.Add(product);
                    int affectedRows = context.SaveChanges();
                    return affectedRows == 1;
                }
            }
            catch(Exception ex)
            {
                logger.Error("Error during inserting new data in database."+ ex.Message);
                ErrorMessage = "Error during inserting new data in database." + ex.Message;
                return false;
            }
        }


        public bool DeleteProduct(int productId)
        {
            try
            {
                using (DBAContext context = DBAContext.GetContext())
                {
                    Product product = context.Products.SingleOrDefault(x => x.Id == productId);
                    if (product == null)
                    {
                        logger.Error("Product with id " + productId + " not exists.");
                        return false;
                    }
                    context.Products.Remove(product);
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error during deleting data from database." + ex.Message);
                ErrorMessage = "Error during deleting data from database." + ex.Message;
                return false;
            }
        }

        public bool EditProduct(int productId, Product product)
        {
            try
            {
                using (DBAContext context = DBAContext.GetContext())
                {
                    Product modifiedProduct = context.Products.SingleOrDefault(x => x.Id == productId);
                    if (modifiedProduct == null)
                    {
                        logger.Error("Product with id " + productId + " not exists.");
                        return false;
                    }
                    modifiedProduct.Price = product.Price;
                    modifiedProduct.Photo = product.Photo;
                    modifiedProduct.Name = product.Name;
                    modifiedProduct.LastUpdated = DateTime.Now;
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error during modifying data in database." + ex.Message);
                ErrorMessage = "Error during modifying data in database." + ex.Message;
                return false;
            }
        }
    }
}