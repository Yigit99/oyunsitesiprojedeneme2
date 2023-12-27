using oyunsitesiprojedeneme2.Domain.Models;

namespace ooyunsitesiprojedeneme2.domain.Collections.Interfaces
{
   
    public interface IRepository
    {
        void Add(UsersDataModel entity);
        void usersRemove(UsersDataModel entity);
        bool Login(UsersDataModel entity);

    }
    
  
    public interface IProductManager
    {
        void Add(ProductsDataModel entity);
        void RemoveProduct(string entity);
        bool productControl(ProductsDataModel entity);
        List<BucketDataModel> GetBuckets(int pageNumber, List<int> cartId);
        List<ProductsDataModel> GetProductsFromDatabase(string pageSize);
    }
    
}
