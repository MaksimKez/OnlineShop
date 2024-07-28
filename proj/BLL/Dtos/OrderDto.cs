namespace BLL.Dtos;

public class OrderDto
{
    public int Id { get; set; }
    public DateTime PaymentDateTime { get; set; }
    public DateTime DeliveryDateTime { get; set; }
    public int UserId { get; set; }
}