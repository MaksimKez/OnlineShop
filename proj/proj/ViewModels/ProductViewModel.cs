using DAL.Entities;

namespace proj.ViewModels;

public class ProductViewModel
{
    public int Id { get; set; }
    public ProductType ProductType { get; set; }
    public string Description { get; set; }
    public string PhotoUrl { get; set; }
    public double PricePerUnit { get; set; }
    public int StockQuantity { get; set; }
}