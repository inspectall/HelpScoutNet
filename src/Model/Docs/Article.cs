using System;
using System.Collections.Generic;

namespace HelpScoutNet.Model.Docs
{
    public class ArticleRef
    {
        public string Id { get; set; }
        public int Number { get; set; }
        public string CollectionId { get; set; }
        public string Slug { get; set; }
        public string Status { get; set; }
        public string HasDraft { get; set; }
        public string Name { get; set; }
        public string PublicUrl { get; set; }
        public string Popularity { get; set; }
        public decimal ViewCount { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? LastPublishedAt { get; set; }
    }

    public class Article
    {
        public string Id { get; set; }
        public int Number { get; set; }
        public string CollectionId { get; set; }
        public string Slug { get; set; }
        public string Status { get; set; }
        public string HasDraft { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public string Code { get; set; }
        public List<string> Categories { get; set; }
        public List<string> Related { get; set; }
        public string PublicUrl { get; set; }
        public string Popularity { get; set; }
        public List<string> Keywords { get; set; }
        public decimal ViewCount { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? LastPublishedAt { get; set; }
    }
}
