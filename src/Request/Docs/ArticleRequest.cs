using System.Collections.Specialized;

namespace HelpScoutNet.Request.Docs
{
    public class ArticleRequest : PageRequest
    {
        public ArticleSortOrder? Order { get; set; }

        public ArticleSortType? Sort { get; set; }

        public ArticleStatus? Status { get; set; }

        public override NameValueCollection ToNameValueCollection()
        {
            base.ToNameValueCollection();
            if (Status.HasValue)
                Nv.Add("status", ((ArticleStatus)Status).ToString().FirstCharacterToLower());
            if (Order.HasValue)
                Nv.Add("order", ((ArticleSortOrder)Order).ToString().FirstCharacterToLower());
            if (Sort.HasValue)
                Nv.Add("sort", ((ArticleSortType)Sort).ToString().FirstCharacterToLower());
            return Nv;
        }

        public enum ArticleSortOrder
        {
            Asc,
            Desc
        }

        public enum ArticleSortType
        {
            Number,
            Visibility,
            Order,
            Name,
            CreatedAt,
            UpdatedAt
        }

        public enum ArticleStatus
        {
            All,
            Published,
            Notpublished
        }
    }
}
