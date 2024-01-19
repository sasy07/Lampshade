using System.Linq.Expressions;
using ShopManagement.Application.Contracts.ProductCategory;
using ShopManagement.Domain.ProductCategoryAgg;

namespace ShopManagement.Infrastructure.EFCore.Repository;

public class ProductCategoryRepository:IProductCategoryRepository
{

    #region Constructure

    private readonly ShopContext _context;

    public ProductCategoryRepository(ShopContext shopContext)
    {
        _context = shopContext;
    }

    #endregion
    
    public void Create(ProductCategory entity)
    {
        _context.ProductCategories.Add(entity);
    }

    public ProductCategory Get(long id)
        => _context.ProductCategories.Find(id);
    

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
    public List<ProductCategory> GetAll()
        => _context.ProductCategories.ToList();

    public bool Exists(Expression<Func<ProductCategory, bool>> expression)
        => _context.ProductCategories.Any(expression);
    

    public void SaveChanges()
    {
        _context.SaveChanges();
    }
}