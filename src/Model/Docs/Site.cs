using Newtonsoft.Json;
using System;

namespace HelpScoutNet.Model.Docs
{
    public enum SiteStatus
    {
        active,
        inactive
    }

    class PagedSites
    {
        public Paged<Site> Sites { get; set; }
    }

    class SingleSite
    {
        public Site Site { get; set; }
    }

    public class Site
    {
        public string Id { get; set; }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Include)]
        public SiteStatus Status { get; set; }
        public string SubDomain { get; set; }
        public string Cname { get; set; }
        public string HasPublicSite { get; set; }
        public string CompanyName { get; set; }
        public string Title { get; set; }
        public string LogoUrl { get; set; }
        public int? LogoWidth { get; set; }
        public int? LogoHeight { get; set; }
        public string FavIconUrl { get; set; }
        public string TouchIconUrl { get; set; }
        public string HomeUrl { get; set; }
        public string HomeLinkText { get; set; }
        public string BgColor { get; set; }
        public string Description { get; set; }
        public string HasContactForm { get; set; }
        public int MailboxId { get; set; }
        public string ContactEmail { get; set; }
        public string StyleSheetUrl { get; set; }
        public string HeaderCode { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
