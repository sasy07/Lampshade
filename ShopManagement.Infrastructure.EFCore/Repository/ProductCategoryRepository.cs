using System.Linq.Expressions;
using _0_Framework.Infrastructure;
using ShopManagement.Application.Contracts.ProductCategory;
using ShopManagement.Domain.ProductCategoryAgg;

namespace ShopManagement.Infrastructure.EFCore.Repository;

public class ProductCategoryRepository:RepositoryBase<long , ProductCategory> , IProductCategoryRepository
{

    #region Constructure

    private readonly ShopContext _context;

    public ProductCategoryRepository(ShopContext shopContext):base(shopContext)
    {
        _context = shopContext;
    }

    #endregion
    
    

    public EditProductCategory GetDetails(long id)
        => _context.ProductCategories.Select(x => new EditProductCategory()
        {
            Id = id,
            Name = x.Name,
            Description = x.Description,
            Keywords = x.Keywords,
            MetaDescription = x.MetaDescription,
            Picture = x.Picture,
            PictureAlt = x.PictureAlt,
            PictureTitle = x.PictureTitle,
            Slug = x.Slug,
        }).FirstOrDefault(x => x.Id == id);

    public List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel)
    {
        var query = _context.ProductCategories
            .Select(x => new ProductCategoryViewModel()
        {
            Id = x.Id,
            Name = x.Name,
            CreationDate = x.CreationDate.ToString(),
            Picture = x.Picture,
            ProductCount = 0
        });
        if (!string.IsNullOrWhiteSpace(searchModel.Name))
            query = query.Where(x => x.Name.Contains(searchModel.Name));

        return query.OrderByDescending(x => x.Id).ToList();
    }
   
}