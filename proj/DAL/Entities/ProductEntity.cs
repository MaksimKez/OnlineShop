using System.ComponentModel;

namespace DAL.Entities;

public class ProductEntity
{
    public int Id { get; set; }
    public double PricePerUnit { get; set; }
    public ProductType ProductType { get; set; }
    public string Description { get; set; }
    public string PhotoUrl { get; set; }
    public int StockQuantity { get; set; }
}

public enum ProductType
{
    [Description("Food")]
    Food = 0,
    [Description("Electronics")]
    Electronics = 1,
    [Description("Clothes")]
    Clothes = 2
}