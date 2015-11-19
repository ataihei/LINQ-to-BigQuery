using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Bigquery.v2;
using Google.Apis.Http;
using Google.Apis.Services;
using Google.Apis.Util.Store;

namespace BigQuery.Linq
{
    public static class ContextHelper
    {
        public static BigQueryContext GetContext(IConfigurableHttpClientInitializer credential, string projectId)
        {
            var bigquery = new BigqueryService(new BaseClientService.Initializer
            {
                ApplicationName = "LINQ to BigQuery",
                HttpClientInitializer = credential
            });

            var context = new BigQueryContext(bigquery, projectId);
            // Timeout or other options
            context.TimeoutMs = (long)TimeSpan.FromMinutes(3).TotalMilliseconds;
            return context;
        }

        public static IConfigurableHttpClientInitializer CreateCredential(string json, string user, string projectId)
        {
            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(json)))
            {
                // Open Browser, Accept Auth
                return GoogleWebAuthorizationBroker.AuthorizeAsync(ms,
                    new[] { BigqueryService.Scope.Bigquery },
                    user,
                    CancellationToken.None, new FileDataStore(@"LINQ-to-BigQuery-for-" + projectId)) // localcache
                    .GetAwaiter().GetResult();
            }
        }

        public static IConfigurableHttpClientInitializer CreateCredential(byte[] p12BinaryData, string password, string user, string projectId)
        {
            return new ServiceAccountCredential(
                new ServiceAccountCredential.Initializer(user)
                {
                    Scopes = new [] { BigqueryService.Scope.Bigquery }
                }.FromCertificate(new X509Certificate2(p12BinaryData, password, X509KeyStorageFlags.Exportable)));
        }
    }
}