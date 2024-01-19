using System.Linq.Expressions;
using ShopManagement.Application.Contracts.ProductCategory;

namespace ShopManagement.Domain.ProductCategoryAgg;

public interface IProductCategoryRepository
{
    void Create(ProductCategory entity);
    ProductCategory Get(long id);
    EditProductCategory GetDetails(long id);
    List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel);
    List<ProductCategory> GetAll();
    bool Exists(Expression<Func<ProductCategory , bool>> expression);
    void SaveChanges();
}