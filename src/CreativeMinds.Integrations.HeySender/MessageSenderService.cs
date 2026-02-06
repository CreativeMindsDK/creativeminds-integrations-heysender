using CreativeMinds.Integrations.HeySender.Configuration;
using CreativeMinds.Integrations.HeySender.Dtos;
using CreativeMinds.Integrations.HeySender.Dtos.Responses;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace CreativeMinds.Integrations.HeySender {

	public class MessageSenderService {
		private readonly IHttpClientFactory httpClientFactory;
		private readonly IHeySenderSettings settings;
		private static String[] acceptMimes = { "application/json", "text/javascript" };
		private readonly ILogger<MessageSenderService> logger;

		public MessageSenderService(IHttpClientFactory httpClientFactory, IHeySenderSettings settings, ILogger<MessageSenderService> logger) {
			this.httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
			this.settings = settings ?? throw new ArgumentNullException(nameof(settings));
			this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		public async Task<Boolean> SendAsync(Message message, CancellationToken cancellationToken) {

			using var client = this.httpClientFactory.CreateClient();

			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Token", this.settings.ApiToken);
			client.DefaultRequestHeaders.Add("Accept", acceptMimes);

			var messageContent = JsonContent.Create(message, options: new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull });
			//var messageString = JsonSerializer.Serialize(message, options: new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull });

			using var response = await client.PostAsync($"{this.settings.Host}/rest/email", messageContent, cancellationToken);

			var responseBody = await response.Content.ReadAsStringAsync();

			try {
				response.EnsureSuccessStatusCode();

				var result = JsonSerializer.Deserialize<StatusResponse>(responseBody);

				// TODO:

				return true;
			}
			catch (HttpRequestException e) {
				this.logger.LogError(e, $"Failed to send message to recipients! Response was: {responseBody}");

				return false;
			}
		}
	}
}
