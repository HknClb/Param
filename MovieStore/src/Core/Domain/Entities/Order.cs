using Domain.Entities.Common;
using Domain.Entities.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Order : Entity
    {
        public Order()
        {
        }

        public Order(string userId, Guid movieId, decimal price) : this()
        {
            UserId = userId;
            MovieId = movieId;
            Price = price;
        }

        public string UserId { get; set; } = null!;
        public Guid MovieId { get; set; }
        public decimal Price { get; set; }

        public virtual User User { get; set; } = null!;
        public virtual Movie Movie { get; set; } = null!;

        [NotMapped] public override Guid Id { get => base.Id; set => base.Id = value; }
        [NotMapped] public override DateTime? UpdatedDate { get => base.UpdatedDate; set => base.UpdatedDate = value; }
        [NotMapped] public override bool IsActive { get => base.IsActive; set => base.IsActive = value; }
    }
}