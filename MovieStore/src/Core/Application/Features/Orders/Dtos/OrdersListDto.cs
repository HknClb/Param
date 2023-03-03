﻿namespace Application.Features.Orders.Dtos
{
    public class OrdersListDto
    {
        public string MovieName { get; set; } = null!;
        public int PublishedYear { get; set; }
        public decimal Price { get; set; }
        public DateTime OrderedDate { get; set; }
    }
}
