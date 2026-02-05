using System;
using System.Text.Json.Serialization;

namespace CreativeMinds.Integrations.HeySender.Dtos {

	public record EmailAddress {
		[JsonPropertyName("address")]
		public String Address {  get; set; }
		[JsonPropertyName("name")]
		public String? Name { get; set; }
	}
}
