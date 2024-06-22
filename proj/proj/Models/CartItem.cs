using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace proj.Models;

public class CartItem
{
    public int CartId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}