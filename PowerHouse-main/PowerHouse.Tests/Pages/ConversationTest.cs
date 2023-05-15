using AngleSharp.Dom;
using AngleSharp.Html.Dom.Events;
using Bunit;
using Bunit.TestDoubles;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor;
using MudBlazor.Services;
using NuGet.Configuration;
using PowerHouse.Client.Pages;
using PowerHouse.Client.Services;
using PowerHouse.Client.Shared;
using PowerHouse.Shared.DTO;
using RichardSzalay.MockHttp;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PowerHouse.Tests.Pages;

[UsesVerify]
public class ConversationTest
{
    [Fact]
    public Task Conversation_Verify_Test()
    {
        // Arrange
        var ctx = new TestContext();
        ctx.Services.AddMudServices();
        ctx.Services.AddSingleton<IAppState, AppState>();
        ctx.JSInterop.Mode = JSRuntimeMode.Loose;
        var settings = new VerifySettings();
        settings.ScrubInlineGuids();

        var CustomConversation = new Conversation
        {
           ConversationId = Guid.NewGuid(),
           ConversationDTO = new ConversationDTO
           {
               Id = Guid.Parse("01f424df-72cd-4dd0-a0a5-929ddbbda931"),
               Topic = "Exploring the World of Microorganisms: The Hidden Life in Your Backyard",
               IsPublic = true,
               AuthorId = Guid.Parse("b1873f2f-81f0-4683-aa7f-33c3e9845df2"),
           }
        };

        var mock = ctx.Services.AddMockHttpClient();
        mock.When("api/Conversation").RespondJson(CustomConversation);

        var authContext = ctx.AddTestAuthorization();
        authContext.SetAuthorizing();
        authContext.SetAuthorized("TEST USER");

        // Act
        var cut = ctx.RenderComponent<Conversation>();

        // Assert
        return Verify(cut.Markup, settings);
    }
}