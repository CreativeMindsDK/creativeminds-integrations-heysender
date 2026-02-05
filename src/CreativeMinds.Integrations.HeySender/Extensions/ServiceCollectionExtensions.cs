using CreativeMinds.Integrations.HeySender.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CreativeMinds.Integrations.HeySender {

	public static class ServiceCollectionExtensions {

		/// <summary>
		/// Pmæy adds the MessageSenderService, you'll have to register IHttpClientFactory and IHeySenderSettings yourself
		/// </summary>
		/// <param name="services"></param>
		/// <returns></returns>
		public static IServiceCollection AddHeySenderSenderCore(this IServiceCollection services) {
			return services.AddSingleton<MessageSenderService, MessageSenderService>();
		}

		public static IServiceCollection AddHeySenderSender(this IServiceCollection services, IConfiguration configuration) {

			services.AddHttpClient();

			//	  <PackageReference Include="Alyio.Extensions.Http.Logging" Version="4.8.1" />
			//			services.ConfigureHttpClientDefaults(builder =>
			//{
			//				builder.AddHttpRawMessageLogging(options =>
			//				{
			//					options.CategoryName = "MyCustomCategory";
			//					options.Level = LogLevel.Information;
			//					options.IgnoreRequestContent = false;
			//					options.IgnoreResponseContent = false;
			//					options.IgnoreRequestHeaders = new[] { "User-Agent" };
			//					options.IgnoreResponseHeaders = new[] { "Date" };
			//					options.RedactRequestHeaders = new[] { "X-Api-Key" };
			//				});
			//			});

			services.Configure<HeySenderSettingsReader>(configuration.GetSection("HeySender"));
			services.AddTransient<IHeySenderSettings, HeySenderSettingsBridge>();

			return services.AddHeySenderSenderCore();
		}
	}
}
