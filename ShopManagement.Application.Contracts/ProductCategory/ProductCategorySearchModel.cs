namespace ShopManagement.Application.Contracts.ProductCategory;

public class ProductCategorySearchModel
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Picture { get; set; }
    public string CreationDate{ get; set; }
    public long ProductCount { get; set; }
}