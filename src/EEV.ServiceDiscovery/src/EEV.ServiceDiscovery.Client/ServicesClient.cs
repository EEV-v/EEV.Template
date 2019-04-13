using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace EEV.ServiceDiscovery.Client
{
    public class ServicesClient
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<ServicesClient> _logger;
        private const string CONGIG_KEY = "ServiceDiscovery:URI"; 
        private const string BASE_PATH = "api/services";

        public ServicesClient(
            IConfiguration configuration,
            IHttpClientFactory httpClientFactory,
            ILogger<ServicesClient> logger)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            _logger = logger;
        }

        public async Task Register(string model)
        {
            var uri = new Uri(new Uri(_configuration[CONGIG_KEY]), BASE_PATH);
            _logger.LogInformation($"Request to {uri}");
            var response = await GetClient().PostAsync(uri, new StringContent(model));
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError($"Request to {uri}... failed");
                _logger.LogError(response.ToString());
                throw new Exception(response.StatusCode.ToString());
            }
        }

        private HttpClient GetClient()
        {
            return _httpClientFactory.CreateClient(nameof(ServicesClient));
        }
    }
}
