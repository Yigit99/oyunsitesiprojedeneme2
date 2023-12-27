using ooyunsitesiprojedeneme2.domain.Collections.Interfaces;
using ooyunsitesiprojedeneme2.Domain;
using oyunsitesiprojedeneme2.Domain.Collections;
using oyunsitesiprojedeneme2.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oyunprojesideneme2.Services
{
  
        public class ProductManager 
    { 
        
            private IProductManager _manager;
            public ProductManager(IProductManager product)
            {
                _manager = product;
            }
            public List<BucketDataModel> bucetGetProductFromDatabase(int pageNumber, List<int> cartIds) {


            List<BucketDataModel> products = new List<BucketDataModel>();

            ProductsCollection productManager = new ProductsCollection();
            products=productManager.GetBuckets(pageNumber, cartIds);
            return products;
        }

            public void productAdd(ProductsDataModel data)
            {
            ProductsCollection productCollecCon = new ProductsCollection();
            productCollecCon.Add(data);
            }

            public void productRemove(string removeName)
            {
            ProductsCollection productCollecCon = new ProductsCollection();
            productCollecCon.RemoveProduct(removeName);
            }

            public bool productControl(ProductsDataModel data) { 
            ProductsCollection productsCollecCon = new ProductsCollection();
            return productsCollecCon.productControl(data);
        }
            
            public List<ProductsDataModel> GetProducts(string pageNumber) { 
        
            ProductsCollection productsCollection = new ProductsCollection();
            return productsCollection.GetProductsFromDatabase(pageNumber);

        }
    }
}
