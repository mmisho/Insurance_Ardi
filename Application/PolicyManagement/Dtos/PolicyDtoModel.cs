using Application.ProductManagement.Dtos;
using Domain.PolicyManagement.Enums;

namespace Application.PolicyManagement.Dtos
{
    public class PolicyDtoModel
    {
        public Guid Id { get; set; }
        public decimal Premium { get; set; }
        public DateTimeOffset StartDateUtc { get; set; }
        public DateTimeOffset EndDateUtc { get; set; }
        public Status Status { get; set; }
        public ProductDtoModel? Product { get; set; }
    }
}
