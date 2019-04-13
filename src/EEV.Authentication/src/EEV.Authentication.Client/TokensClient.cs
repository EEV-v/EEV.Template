using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace EEV.ServiceDiscovery.Client
{
    public class TokensClient
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<TokensClient> _logger;
        private const string CONGIG_KEY = "Authentication:URI"; 
        private const string BASE_PATH = "api/v1/tokens";

        public TokensClient(
            IConfiguration configuration,
            IHttpClientFactory httpClientFactory,
            ILogger<TokensClient> logger)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Register(string model)
        {
            var uri = GetUri();
            _logger.LogInformation($"Request to {uri}");
            var response = await GetClient().PostAsync(uri, new StringContent(model));
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError($"Request to {uri}... failed");
                _logger.LogError(response.ToString());
                throw new Exception(response.StatusCode.ToString());
            }
        }

        public async Task Check(string v)
        {
            var uri = new UriBuilder(GetUri()).Query.Insert(0, v).ToString();
            _logger.LogInformation($"Request to {uri}");
            var response = await GetClient().GetAsync(uri);
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError($"Request to {uri}... failed");
                _logger.LogError(response.ToString());
                throw new Exception(response.StatusCode.ToString());
            }
        }

        private Uri GetUri()
        {
            return new Uri(new Uri(_configuration[CONGIG_KEY]), BASE_PATH);
        }

        private HttpClient GetClient()
        {
            return _httpClientFactory.CreateClient(nameof(TokensClient));
        }
    }
}
