using System;

namespace HelpScoutNet.Model.Docs
{
    class PagedCategories
    {
        public Paged<Category> Categories { get; set; }
    }

    class SingleCategory
    {
        public Category Category { get; set; }
    }

    public class Category
    {
        public string Id { get; set; }
        public int Number { get; set; }
        public string Slug { get; set; }
        public string CollectionId { get; set; }
        public int Order { get; set; }
        public string Name { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
