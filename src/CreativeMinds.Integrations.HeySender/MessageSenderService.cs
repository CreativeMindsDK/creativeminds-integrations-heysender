using CreativeMinds.Integrations.HeySender.Configuration;
using CreativeMinds.Integrations.HeySender.Dtos;
using CreativeMinds.Integrations.HeySender.Dtos.Responses;
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

		public MessageSenderService(IHttpClientFactory httpClientFactory, IHeySenderSettings settings) {
			this.httpClientFactory = httpClientFactory;
			this.settings = settings;
		}

		public async Task<Boolean> SendAsync(Message message, CancellationToken cancellationToken) {

			using var client = this.httpClientFactory.CreateClient();

			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Token", this.settings.ApiToken);
			client.DefaultRequestHeaders.Add("Accept", new[] { "application/json", "text/javascript" });

			using var response = await client.PostAsync(
				$"{this.settings.Host}/rest/email",
				JsonContent.Create(message),
				cancellationToken
			);

			//client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
			//	"Token",
			//	this.settings.ApiToken
			//);

			//HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "/rest/email");
			//request.Headers.Add("Authorization", $"Basic {this.settings.ApiToken}");
			//request.Content = JsonContent.Create(message, new MediaTypeHeaderValue("application/json"), new JsonSerializerOptions() { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull, PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

			//var response = await client.PostAsync(, request, cancellationToken);

			var responseBody = await response.Content.ReadAsStringAsync();

			try {
				response.EnsureSuccessStatusCode(); // Throws an error on non-succes status code

				var result = JsonSerializer.Deserialize<StatusResponse>(responseBody);





				return true;
			}
			catch (HttpRequestException e) {
				// Catches error and throws the Conta-api-error to frontend instead of generic error.
				//this.logger.LogError(e, $"Something went wrong when executing API-call to Conta: {nameof(this.PostToContaAsync)} with values: URI: {this._httpClient.BaseAddress}{specificUri}");

				return false;
			}
		}
	}
}
