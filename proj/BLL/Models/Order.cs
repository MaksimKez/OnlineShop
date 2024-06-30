namespace BLL.Models;

public class Order
{
    public int Id { get; set; }
    public DateTime PaymentDateTime { get; set; }
    public DateTime DeliveryDateTime { get; set; }
    public int UserId { get; set; }
}