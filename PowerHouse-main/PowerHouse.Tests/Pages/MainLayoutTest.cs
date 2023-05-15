using Bunit;
using Bunit.TestDoubles;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor;
using MudBlazor.Services;
using PowerHouse.Client.Services;
using PowerHouse.Client.Shared;
using PowerHouse.Shared.DTO;
using RichardSzalay.MockHttp;

namespace PowerHouse.tests.Pages;

[UsesVerify]
public class MainLayoutTest
{
    [Fact]
    public Task MainLayout_VerifyHtml_Unauthorized()
    {
        // Arrange
        var ctx = new TestContext();
        ctx.Services.AddMudServices();
        ctx.Services.AddSingleton<IAppState, AppState>();
        ctx.JSInterop.Mode = JSRuntimeMode.Loose;
        var settings = new VerifySettings();
        settings.ScrubInlineGuids();

        var authContext = ctx.AddTestAuthorization();
        authContext.SetAuthorizing();

        // Act
        var cut = ctx.RenderComponent<MainLayout>();

        // Assert
        return Verify(cut.Markup, settings);
    }

    [Fact]
    public Task MainLayout_VerifyHtml_Authorized()
    {
        // Arrange
        var ctx = new TestContext();
        ctx.Services.AddMudServices();
        ctx.Services.AddSingleton<IAppState, AppState>();
        ctx.JSInterop.Mode = JSRuntimeMode.Loose;
        var settings = new VerifySettings();
        settings.ScrubInlineGuids();
        var mock = ctx.Services.AddMockHttpClient();
        mock.When("api/Conversation").RespondJson(new List<ConversationDTO>());

        var authContext = ctx.AddTestAuthorization();
        authContext.SetAuthorizing();
        authContext.SetAuthorized("TEST USER");

        // Act
        var cut = ctx.RenderComponent<MainLayout>();

        // Assert
        return Verify(cut.Markup, settings);
    }

    [Fact]
    public Task MainLayout_VerifyHtml_Authorized_Hover_Menu()
    {
        // Arrange
        var ctx = new TestContext();
        ctx.Services.AddMudServices();
        ctx.Services.AddSingleton<IAppState, AppState>();
        ctx.JSInterop.Mode = JSRuntimeMode.Loose;
        var settings = new VerifySettings();
        settings.ScrubInlineGuids();
        settings.ScrubLinesContaining("data-ticks");
        var mock = ctx.Services.AddMockHttpClient();
        mock.When("api/Conversation").RespondJson(new List<ConversationDTO>());

        var authContext = ctx.AddTestAuthorization();
        authContext.SetAuthorizing();
        authContext.SetAuthorized("TEST USER");
        var @onmouseenter = new MouseEventArgs();

        // Act
        var cut = ctx.RenderComponent<MainLayout>();
        cut.Find(".mud-menu").TriggerEvent("onmouseenter", @onmouseenter);

        // Assert
        return Verify(cut.Markup, settings);
    }

    [Fact]
    public Task MainLayout_MudIconButton_Test()
    {
        // Arrange
        var ctx = new TestContext();
        ctx.Services.AddMudServices();
        ctx.Services.AddSingleton<IAppState, AppState>();
        ctx.JSInterop.Mode = JSRuntimeMode.Loose;
        var settings = new VerifySettings();
        settings.ScrubInlineGuids();
        settings.ScrubLinesContaining("data-ticks");
        var mock = ctx.Services.AddMockHttpClient();
        mock.When("api/Conversation").RespondJson(new List<ConversationDTO>());

        var authContext = ctx.AddTestAuthorization();
        authContext.SetAuthorizing();
        authContext.SetAuthorized("TEST USER");

        // Act
        var cut = ctx.RenderComponent<MainLayout>();
        var buttonComponent = cut.FindComponents<MudIconButton>();
        buttonComponent[0].Find("button").Click();

        // Assert
        return Verify(cut.Markup, settings);
    }

    [Fact]
    public Task MainLayout_MudIconButtonIndex1_Test()
    {
        // Arrange
        var ctx = new TestContext();
        ctx.Services.AddMudServices();
        ctx.Services.AddSingleton<IAppState, AppState>();
        ctx.JSInterop.Mode = JSRuntimeMode.Loose;
        var settings = new VerifySettings();
        settings.ScrubInlineGuids();
        settings.ScrubLinesContaining("data-ticks");
        var mock = ctx.Services.AddMockHttpClient();
        mock.When("api/Conversation").RespondJson(new List<ConversationDTO>());

        var authContext = ctx.AddTestAuthorization();
        authContext.SetAuthorizing();
        authContext.SetAuthorized("TEST USER");

        // Act
        var cut = ctx.RenderComponent<MainLayout>();
        var buttonComponent = cut.FindComponents<MudIconButton>();
        buttonComponent[1].Find("button").Click();

        // Assert
        return Verify(cut.Markup, settings);
    }
}