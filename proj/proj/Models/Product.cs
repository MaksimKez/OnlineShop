namespace proj.Models;

public class Product
{
    public int Id { get; set; }
    public string ProductType { get; set; }
    public string Description { get; set; }
    public string PhotoUrl { get; set; }
    public double Price { get; set; }
    public int StockQuantity { get; set; }
}