using HelpScoutNet.Model.Docs;
using System.Collections.Specialized;

namespace HelpScoutNet.Request.Docs
{
    public class CollectionRequest : PageRequest
    {
        public string SiteId { get; set; }

        public CollectionVisibility? Visibility { get; set; }

        public CollectionSortOrder? Order { get; set; }

        public CollectionSortType? Sort { get; set; }

        public override NameValueCollection ToNameValueCollection()
        {
            base.ToNameValueCollection();
            if (!string.IsNullOrEmpty(SiteId))
                Nv.Add("siteId", SiteId);
            if (Visibility.HasValue)
                Nv.Add("visibility", ((CollectionVisibility)Visibility).ToString().FirstCharacterToLower());
            if (Order.HasValue)
                Nv.Add("order", ((CollectionSortOrder)Order).ToString().FirstCharacterToLower());
            if (Sort.HasValue)
                Nv.Add("sort", ((CollectionSortType)Sort).ToString().FirstCharacterToLower());
            return Nv;
        }
    }

    public enum CollectionSortOrder
    {
        Asc,
        Desc
    }

    public enum CollectionSortType
    {
        Number,
        Visibility,
        Order,
        Name,
        CreatedAt,
        UpdatedAt
    }
    public enum CollectionVisibility
    {
        Public,
        Private,
        All
    }
}
