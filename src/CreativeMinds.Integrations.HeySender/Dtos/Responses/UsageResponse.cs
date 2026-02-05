using System;
using System.Text.Json.Serialization;

namespace CreativeMinds.Integrations.HeySender.Dtos.Responses {

	public record UsageResponse {
		[JsonPropertyName("amount")]
		public Int32 Amount { get; set; }
		[JsonPropertyName("currency")]
		public String Currency { get; set; }
		[JsonPropertyName("total_cost")]
		public Double Cost { get; set; }
	}
}
