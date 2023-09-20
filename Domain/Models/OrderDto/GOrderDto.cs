namespace Domain.Models.OrderDto;

public class GOrderDto
{
    public int Id { get; set; }
    public string Product { get; set; }
    public int Quantity { get; set; }
    public int UserId { get; set; }
}
