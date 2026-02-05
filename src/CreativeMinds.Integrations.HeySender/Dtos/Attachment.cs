using System;
using System.Text.Json.Serialization;

namespace CreativeMinds.Integrations.HeySender.Dtos {

	public record Attachment {
		[JsonPropertyName("filename")]
		public String Filename { get; set; }
		[JsonPropertyName("data")]
		public String Data { get; set; }
		[JsonPropertyName("mimetype")]
		public String MimeType { get; set; }
	}
}
