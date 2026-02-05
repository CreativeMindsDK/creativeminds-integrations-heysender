using System;

namespace CreativeMinds.Integrations.HeySender.Configuration {

	public record HeySenderSettingsReader : IHeySenderSettings {
		public String Host {  get; set; }
		public String ApiToken {  get; set; }
	}
}
