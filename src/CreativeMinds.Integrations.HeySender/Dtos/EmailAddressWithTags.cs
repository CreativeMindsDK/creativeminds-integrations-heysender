using System;
using System.Text.Json.Serialization;

namespace CreativeMinds.Integrations.HeySender.Dtos {

	public record EmailAddressWithTags : EmailAddress {
		[JsonPropertyName("tagvalues")]
		public String[] Tags { get; set; }
	}
}
