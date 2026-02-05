using Microsoft.Extensions.Options;
using System;

namespace CreativeMinds.Integrations.HeySender.Configuration {

	public class HeySenderSettingsBridge : IHeySenderSettings {
		private readonly IOptionsSnapshot<HeySenderSettingsReader> optionsConfig;

		public HeySenderSettingsBridge(IOptionsSnapshot<HeySenderSettingsReader> optionsConfig) {
			this.optionsConfig = optionsConfig ?? throw new ArgumentNullException(nameof(optionsConfig));
		}

		public String Host => this.optionsConfig.Value.Host;
		public String ApiToken => this.optionsConfig.Value.ApiToken;
	}
}
