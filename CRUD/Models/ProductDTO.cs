﻿namespace CRUD.Models
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string? Bla_Name { get; set; }
        public string? Brand { get; set; }
        public string? Category { get; set; }
        public decimal? Price { get; set; }
        public string? Description { get; set; }
        public IFormFile? FileImage { get; set; }
    }
}
