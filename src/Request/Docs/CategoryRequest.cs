using System.Collections.Specialized;

namespace HelpScoutNet.Request.Docs
{
    public class CategoryRequest : PageRequest
    {
        public CategorySortOrder? Order { get; set; }

        public CategorySortType? Sort { get; set; }

        public override NameValueCollection ToNameValueCollection()
        {
            base.ToNameValueCollection();
            if (Order.HasValue)
                Nv.Add("order", ((CategorySortOrder)Order).ToString().FirstCharacterToLower());
            if (Sort.HasValue)
                Nv.Add("sort", ((CategorySortType)Sort).ToString().FirstCharacterToLower());
            return Nv;
        }

        public enum CategorySortOrder
        {
            Asc,
            Desc
        }

        public enum CategorySortType
        {
            Number,
            Order,
            Name,
            ArticleCount,
            CreatedAt,
            UpdatedAt
        }
    }
}
