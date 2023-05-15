using AngleSharp.Dom;
using Bunit;
using Bunit.TestDoubles;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor.Services;
using PowerHouse.Client.Pages;
using PowerHouse.Client.Services;
using PowerHouse.Client.Shared;
using PowerHouse.Shared.DTO;
using RichardSzalay.MockHttp;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PowerHouse.Tests.Pages;

[UsesVerify]
public class IndexTest 
{

    [Fact]
    public Task TopicsContainer_Verify_Markup_Test()
    {
        // Arrange
        var ctx = new TestContext();
        ctx.Services.AddMudServices();
        ctx.JSInterop.Mode = JSRuntimeMode.Loose;
        var settings = new VerifySettings();
        settings.ScrubInlineGuids();

        var list = new List<ConversationDTO>()
        {
            new()
            {
                Id = new("01f424df-72cd-4dd0-a0a5-929ddbbda931"),
                Topic = "Hejejjee",
                IsPublic = true,
                IsBlocked = false,
                AuthorId= new("01f424df-72cd-4dd0-a0a5-929ddbbda931")
            }
        };

        var eventCalled = false;

        // Act
        var cut = ctx.RenderComponent<TopicsContainer>(parameters => parameters
        .Add(p => p.Conversations,list)
        .Add(p => p.NavigateClick, () => { eventCalled = true; }));

        // Assert
        return Verify(cut.Markup, settings);
    }

    [Fact]
    public void TopicsContainer_Verify_Callback_Test()
    {
        // Arrange
        var ctx = new TestContext();
        ctx.Services.AddMudServices();
        ctx.JSInterop.Mode = JSRuntimeMode.Loose;
        var settings = new VerifySettings();
        settings.ScrubInlineGuids();

        var list = new List<ConversationDTO>()
        {
            new()
            {
                Id = new("01f424df-72cd-4dd0-a0a5-929ddbbda931"),
                Topic = "Hejejjee",
                IsPublic = true,
                IsBlocked = false,
                AuthorId= new("01f424df-72cd-4dd0-a0a5-929ddbbda931")
            }
        };

        var eventCalled = false;

        // Act
        var cut = ctx.RenderComponent<TopicsContainer>(parameters => parameters
        .Add(p => p.Conversations, list)
        .Add(p => p.NavigateClick, () => { eventCalled = true; }));

        cut.Find(".topic_text_style").Click();

        // Assert
        Assert.True(eventCalled);
    }

    [Fact]
    public void TopicsContainer_Verify_Callback_Hover_Test()
    {
        // Arrange
        var ctx = new TestContext();
        ctx.Services.AddMudServices();
        ctx.JSInterop.Mode = JSRuntimeMode.Loose;
        var settings = new VerifySettings();
        settings.ScrubInlineGuids();


        var list = new List<ConversationDTO>()
        {
            new()
            {
                Id = new("01f424df-72cd-4dd0-a0a5-929ddbbda931"),
                Topic = "TjugoTeckenFörHoverSkaSynas",
                IsPublic = true,
                IsBlocked = false,
                AuthorId= new("01f424df-72cd-4dd0-a0a5-929ddbbda931")
            }
        };

        var eventCalled = false;

        // Act
        var cut = ctx.RenderComponent<TopicsContainer>(parameters => parameters
        .Add(p => p.Conversations, list)
        .Add(p => p.NavigateClick, () => { eventCalled = true; }));

        cut.Find(".topic_text_position").IsHovered();
        var tooltip = cut.Find(".tooltip_text").TextContent;

        // Assert
        Assert.Equal(tooltip, list[0].Topic);
    }
}

