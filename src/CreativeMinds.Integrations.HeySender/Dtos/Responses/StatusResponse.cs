using System;
using System.Text.Json.Serialization;

namespace CreativeMinds.Integrations.HeySender.Dtos.Responses {

	public record StatusResponse {
		[JsonPropertyName("ids")]
		public Int64[] Ids { get; set; }
		[JsonPropertyName("usage")]
		public UsageResponse Usage { get; set; }
	}
}
