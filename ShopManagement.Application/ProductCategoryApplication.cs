using _0_Framework.Application;
using ShopManagement.Application.Contracts.ProductCategory;
using ShopManagement.Domain.ProductCategoryAgg;

namespace ShopManagement.Application;

public class ProductCategoryApplication:IProductCategoryApplication
{
    #region Constructure

    private readonly IProductCategoryRepository _productCategoryRepository;

    public ProductCategoryApplication(IProductCategoryRepository productCategoryRepository)
    {
        _productCategoryRepository = productCategoryRepository;
    }

    #endregion

    public OperationResult Create(CreateProductCategory command)
    {
        OperationResult operation = new ();
        if (_productCategoryRepository.Exists(x=>x.Name == command.Name))
            return operation.Failed("امکان ثبت رکورد تکراری وجود ندارد .");
        
        var productCategory = new ProductCategory(command.Name, command.Description,
            command.Picture, command.PictureAlt, command.PictureTitle,
            command.Keywords, command.MetaDescription, command.Slug.Slugify());
        
        _productCategoryRepository.Create(productCategory);
        _productCategoryRepository.SaveChanges();
        return operation.Succedded();
    }

    public OperationResult Edit(EditProductCategory command)
    {
        OperationResult operation = new ();
        var productCategory = _productCategoryRepository.Get(command.Id);
        if (productCategory == null) 
            return operation.Failed("رکورد یافت نشد");
        if (_productCategoryRepository.Exists(x=>x.Name == command.Name && x.Id != command.Id))
            return operation.Failed("امکان ثبت رکورد تکراری وجود ندارد .");
        productCategory.Edit(command.Name, command.Description,
            command.Picture, command.PictureAlt, command.PictureTitle,
            command.Keywords, command.MetaDescription, command.Slug.Slugify());
      _productCategoryRepository.SaveChanges();
      return operation.Succedded();
    }

    public EditProductCategory GetDetails(long id)
        => _productCategoryRepository.GetDetails(id);

    public List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel)
        => _productCategoryRepository.Search(searchModel);
}