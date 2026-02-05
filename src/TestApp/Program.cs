// See https://aka.ms/new-console-template for more information
using CreativeMinds.Integrations.HeySender;
using CreativeMinds.Integrations.HeySender.Dtos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading;

CancellationTokenSource source = new CancellationTokenSource();

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

builder.Configuration
	.SetBasePath(Directory.GetCurrentDirectory())
	.AddJsonFile("appsettings.json")
	.AddJsonFile($"appsettings.{Environment.UserName}.json", true);

builder.Services.AddHeySenderSender(builder.Configuration);

builder.Logging.AddConsole();

using IHost host = builder.Build();

var sender = host.Services.GetService<MessageSenderService>();

await sender.SendAsync(new CreativeMinds.Integrations.HeySender.Dtos.Message { /*FromAddress = new CreativeMinds.Integrations.HeySender.Dtos.EmailAddress { Name = "The Robot Bunny", Address = "robot-bunny@human-account.com" }*/
	FromAddress = "robot-bunny@human-account.com",
	Html = "<b>hej</b>",
	PlainText = "hey",
	Subject = "min test",
	Recipients = new EmailAddress[] { new EmailAddress { Address = "steen@newsuntold.dk", Name = "Steen" } }
}, source.Token);

await host.RunAsync();
