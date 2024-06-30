namespace DAL.Entities;

public class OrderEntity
{
    public int Id { get; set; }
    public DateTime PaymentDateTime { get; set; }
    public DateTime DeliveryDateTime { get; set; }
    public int UserId { get; set; }
    public UserEntity User { get; set; }
}