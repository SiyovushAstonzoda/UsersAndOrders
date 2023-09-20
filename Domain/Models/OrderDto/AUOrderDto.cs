namespace Domain.Models.OrderDto;

public class AUOrderDto
{
    public int Id { get; set; }
    public string Product { get; set; }
    public int Quantity { get; set; }
    public int UserId { get; set; }
}
