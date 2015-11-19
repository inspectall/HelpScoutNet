using System;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using HelpScoutNet.Model;
using HelpScoutNet.Request;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using HelpScoutNet.Model.Docs;
using HelpScoutNet.Request.Docs;


namespace HelpScoutNet
{
    public sealed class HelpScoutDocsClient
    {
        private readonly string _apiKey;
        private const string BaseUrl = "https://docsapi.helpscout.net/v1/";

        private JsonSerializerSettings _serializerSettings
        {
            get
            {
                var serializer = new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    NullValueHandling = NullValueHandling.Ignore,
                    DefaultValueHandling = DefaultValueHandling.Ignore,
                    DateFormatHandling = DateFormatHandling.IsoDateFormat,
                };
                serializer.Converters.Add(new StringEnumConverter { CamelCaseText = true });

                return serializer;
            }
        }

        public HelpScoutDocsClient(string apiKey)
        {
            _apiKey = apiKey;
        }

        #region Sites
        public Paged<Site> ListSites(PageRequest requestArg = null)
        {
            PagedSites ps = Get<PagedSites>("sites", requestArg);
            Paged<Site> pagedSite = ps.Sites;
            return pagedSite;
        }

        public Site GetSite(string siteId)
        {
            SingleSite ss = Get<SingleSite>(string.Format("sites/{0}", siteId), null);
            return ss.Site;
        }
        #endregion

        #region Collections

        public Paged<Collection> ListCollections(string siteId = null, CollectionRequest requestArg = null)
        {
            if (siteId != null)
            {
                if (requestArg == null)
                    requestArg = new CollectionRequest();
                requestArg.SiteId = siteId;
            }
            PagedCollections pc = Get<PagedCollections>("collections", requestArg);
            return pc.Collections;
        }

        public Collection GetCollection(string collectionId, FieldRequest requestArg = null)
        {
            SingleCollection ss = Get<SingleCollection>(string.Format("collections/{0}", collectionId), requestArg);
            return ss.Collection;
        }

        public Collection GetCollection(int collectionNumber)
        {
            SingleCollection ss = Get<SingleCollection>(string.Format("collections/{0}", collectionNumber), null);
            return ss.Collection;
        }
        #endregion

        #region Categories
        public Paged<Category> ListCategories(string collectionId, CategoryRequest requestArg = null)
        {
            PagedCategories pc = Get<PagedCategories>(string.Format("collections/{0}/categories", collectionId), requestArg);
            return pc.Categories;
        }

        public Category GetCategory(string categoryId)
        {
            SingleCategory sc = Get<SingleCategory>(string.Format("categories/{0}", categoryId), null);
            return sc.Category;
        }

        public Category GetCategory(int categoryNumber)
        {
            SingleCategory sc = Get<SingleCategory>(string.Format("categories/{0}", categoryNumber), null);
            return sc.Category;
        }
        #endregion

        #region Articles
        public Paged<ArticleRef> ListArticlesForCollection(string collectionId, ArticleRequest requestArg = null)
        {
            PagedArticles pa = Get<PagedArticles>(string.Format("collections/{0}/articles", collectionId), requestArg);
            return pa.Articles;
        }

        public Paged<ArticleRef> ListArticlesForCategory(string categoryId, ArticleRequest requestArg = null)
        {
            PagedArticles pa = Get<PagedArticles>(string.Format("categories/{0}/articles", categoryId), requestArg);
            return pa.Articles;
        }

        public Article GetArticle(string articleId)
        {
            SingleArticle sa = Get<SingleArticle>(string.Format("articles/{0}", articleId), null);
            return sa.Article;
        }

        public Article GetArticle(int articleNumber)
        {
            SingleArticle sa = Get<SingleArticle>(string.Format("articles/{0}", articleNumber), null);
            return sa.Article;
        }
        #endregion

        private T Get<T>(string endpoint, IRequest request) where T : class
        {
            var client = InitHttpClient();

            string debug = BaseUrl + endpoint + ToQueryString(request);
            Console.WriteLine("URL:" + debug);
            HttpResponseMessage response = client.GetAsync(BaseUrl + endpoint + ToQueryString(request)).Result;
            string body = response.Content.ReadAsStringAsync().Result;
            Console.WriteLine("Body:" + body);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Serializer Settings:" + _serializerSettings);
                T result = JsonConvert.DeserializeObject<T>(body, _serializerSettings);
                return result;
            }

            var error = JsonConvert.DeserializeObject<HelpScoutError>(body);
            throw new HelpScoutApiException(error, body);
        }

        private string PostAttachment(string endpoint, CreateAttachmentRequest request)
        {
            var client = InitHttpClient();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var jsonPayload = JsonConvert.SerializeObject(request, _serializerSettings);

            HttpResponseMessage response = client.PostAsync(BaseUrl + endpoint, new StringContent(jsonPayload, Encoding.UTF8, "application/json")).Result;
            string body = response.Content.ReadAsStringAsync().Result;

            if (response.IsSuccessStatusCode)
            {
                dynamic file = JsonConvert.DeserializeObject(body);

                return file.item.hash;
            }

            var error = JsonConvert.DeserializeObject<HelpScoutError>(body);
            throw new HelpScoutApiException(error, body);
        }

        private T Post<T>(string endpoint, T payload, IPostOrPutRequest request)
        {
            var client = InitHttpClient();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var jsonPayload = JsonConvert.SerializeObject(payload, _serializerSettings);

            HttpResponseMessage response = client.PostAsync(BaseUrl + endpoint + ToQueryString(request), new StringContent(jsonPayload, Encoding.UTF8, "application/json")).Result;
            string body = response.Content.ReadAsStringAsync().Result;

            if (response.IsSuccessStatusCode)
            {
                if (request.Reload)
                {
                    T result = JsonConvert.DeserializeObject<SingleItem<T>>(body).Item;
                    return result;
                }
                else
                {
                    return payload;
                }
            }

            var error = JsonConvert.DeserializeObject<HelpScoutError>(body);
            throw new HelpScoutApiException(error, body);
        }

        private T Put<T>(string endpoint, T payload, IPostOrPutRequest request)
        {
            var client = InitHttpClient();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var jsonPayload = JsonConvert.SerializeObject(payload, _serializerSettings);

            HttpResponseMessage response = client.PutAsync(BaseUrl + endpoint + ToQueryString(request), new StringContent(jsonPayload, Encoding.UTF8, "application/json")).Result;
            string body = response.Content.ReadAsStringAsync().Result;

            if (response.IsSuccessStatusCode)
            {
                if (request.Reload)
                {
                    T result = JsonConvert.DeserializeObject<SingleItem<T>>(body).Item;
                    return result;
                }
                else
                {
                    return payload;
                }
            }

            var error = JsonConvert.DeserializeObject<HelpScoutError>(body);
            throw new HelpScoutApiException(error, body);
        }

        private void Delete(string endpoint)
        {
            var client = InitHttpClient();

            var response = client.DeleteAsync(BaseUrl + endpoint).Result;

            if (!response.IsSuccessStatusCode)
            {
                string body = response.Content.ReadAsStringAsync().Result;
                var error = JsonConvert.DeserializeObject<HelpScoutError>(body);
                throw new HelpScoutApiException(error, body);
            }
        }

        private HttpClient InitHttpClient()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(string.Format("{0}:{1}", _apiKey, "X"))));
            return client;
        }

        private static string ToQueryString(IRequest request)
        {
            NameValueCollection nvc = null;
            if (request != null)
            {
                nvc = request.ToNameValueCollection();
            }

            if (nvc == null)
                return string.Empty;

            var array = (from key in nvc.AllKeys
                         from value in nvc.GetValues(key)
                         select string.Format("{0}={1}", Uri.EscapeDataString(key), Uri.EscapeDataString(value)))
                .ToArray();
            return "?" + string.Join("&", array);
        }
    }
}
