using System;

namespace HelpScoutNet.Model.Docs
{
    public enum CollectionVisibility
    {
        Public,
        Private
    }

    class PagedCollections
    {
        public Paged<Collection> Collections { get; set; }
    }

    class SingleCollection
    {
        public Collection Collection { get; set; }
    }

    public class Collection
    {
        public string Id { get; set; }
        public int Number { get; set; }
        public string SiteId { get; set; }
        public string Slug { get; set; }
        public CollectionVisibility Visibility { get; set; }
        public int Order { get; set; }
        public string Name { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
