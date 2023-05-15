using AngleSharp.Dom;
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
public class MessageCardTest
{
    [Fact]
    public Task MessageCard_Verify_Test()
    {
        // Arrange
        var ctx = new TestContext();
        ctx.Services.AddMudServices();
        ctx.JSInterop.Mode = JSRuntimeMode.Loose;
        var settings = new VerifySettings();
        settings.ScrubInlineGuids();

        var customAuthor = new UserDTO
        {
            Id = Guid.NewGuid(),
            Username = "Efraim Caspersson",
            Conversations = new List<ConversationDTO>
            {
                new ConversationDTO
                {
                   Id = Guid.NewGuid(),
                   Topic = "Exploring the World of Microorganisms: The Hidden Life in Your Backyard",
                   IsPublic = true,
                   AuthorId = Guid.NewGuid(),
                }
            }
        };

        var customConvo = new ConversationDTO
        {
            Id = Guid.NewGuid(),
            Topic = "Exploring the World of Microorganisms: The Hidden Life in Your Backyard",
            IsPublic = true,
            AuthorId = Guid.NewGuid(),
            Users = new()
            {
                customAuthor
            },
        };

        var CustomMessage = new MessageDTO
        {
            Id = Guid.NewGuid(),
            Text = "Swag in Ohio",
            CreateDate = DateTime.MaxValue,
            EditDate = DateTime.MaxValue,
            ConversationId = Guid.NewGuid(),
            AuthorId = Guid.NewGuid(),
            Author = customAuthor,
            IsEdited = false,
            IsBlocked = false,
            IsDeleted = false,
        };
            
        

        var eventCalled = false;

        var mock = ctx.Services.AddMockHttpClient();
       // mock.When("api/Conversation").RespondJson(customConvo);

        var authContext = ctx.AddTestAuthorization();
        authContext.SetAuthorizing();
        authContext.SetAuthorized("TEST USER");

        // Act
        var cut = ctx.RenderComponent<MessageCard>(paramters => paramters
        .Add(p => p.DeleteItem, () => { eventCalled = true; })
        .Add(p => p.EditItem, () => { eventCalled = true; })
        .Add(p => p.Message, CustomMessage));
       
        var buttonComponent = cut.FindComponent<MudMenu>();
        buttonComponent.Find("button").Click();

        // Assert
        return Verify(cut.Markup, settings);
    } 
}

