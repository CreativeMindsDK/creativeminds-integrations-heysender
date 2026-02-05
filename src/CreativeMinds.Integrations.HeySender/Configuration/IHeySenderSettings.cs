using System;

namespace CreativeMinds.Integrations.HeySender.Configuration {

	public interface IHeySenderSettings {
		String Host { get; }
		String ApiToken { get; }
	}
}
