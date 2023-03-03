namespace Application.Features.Orders.Dtos
{
    public class PlaceOrderDto
    {
        public PlaceOrderDto()
        {
        }

        public PlaceOrderDto(string userId, string movieId) : this()
        {
            UserId = userId;
            MovieId = movieId;
        }

        public string UserId { get; set; } = null!;
        public string MovieId { get; set; } = null!;
    }
}
