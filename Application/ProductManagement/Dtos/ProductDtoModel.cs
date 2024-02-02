﻿namespace Application.ProductManagement.Dtos
{
    public class ProductDtoModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
    }
}
