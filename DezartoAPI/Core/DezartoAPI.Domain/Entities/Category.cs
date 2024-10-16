﻿using DezartoAPI.Domain.Entities.Common;

namespace DezartoAPI.Domain.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string? ParentCategoryId { get; set; }
    }
}
