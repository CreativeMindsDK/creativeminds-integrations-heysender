using System;
using System.Text.Json.Serialization;

namespace CreativeMinds.Integrations.HeySender.Dtos {

	public record Message {
		[JsonPropertyName("html")]
		public String Html { get; set; }
		[JsonPropertyName("plaintext")]
		public String PlainText { get; set; }
		[JsonPropertyName("subject")]
		public String Subject { get; set; }
		[JsonPropertyName("from")]
		public String From {
			get {
				return String.IsNullOrWhiteSpace(this.FromName) == true ? this.FromAddress : $"{this.FromName} <{this.FromAddress}>";
			}
		}
		[JsonIgnore]
		public String FromAddress { set; private get; }
		[JsonIgnore]
		public String FromName { set; private get; }
		[JsonPropertyName("reply")]
		public String ReplyAddress { get; set; }
		[JsonPropertyName("tags")]
		public String[] Tags { get; set; }
		[JsonPropertyName("recipients")]
		public EmailAddress[] Recipients { get; set; }
		[JsonPropertyName("attachments")]
		public Attachment[] Attachments { get; set; }
	}
}
