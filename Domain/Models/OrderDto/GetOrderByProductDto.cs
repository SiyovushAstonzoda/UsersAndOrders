namespace Domain.Models.OrderDto;

public class GetOrderByProductDto
{
    public string Product { get; set; }
    public int UserId { get; set; }
    public string UserName { get; set; }
    public int SumQuantity { get; set; }
}
