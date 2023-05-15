using AngleSharp.Dom;
using Bunit;
using Bunit.TestDoubles;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.DependencyInjection;
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
public class ReportDialogTest
{
    [Fact]
    public Task ReportDialog_Verify_Test()
    {
        // Arrange
        var ctx = new TestContext();
        ctx.Services.AddMudServices();
        ctx.JSInterop.Mode = JSRuntimeMode.Loose;
        var settings = new VerifySettings();
        settings.ScrubInlineGuids();

        var CustomReportDialog = new ReportDTO
        {
            Id = Guid.Parse("cd7764d6-c990-4527-9dff-c4522d5ece11"),
            Reported = DateTime.UtcNow,
            Reason = "Offended text",
            MessageId = Guid.Parse("c8092944-3926-4370-8c8c-c08dcfec841d"),           
        };

        var mock = ctx.Services.AddMockHttpClient();
        mock.When("api/Conversation").RespondJson(CustomReportDialog);

        var authContext = ctx.AddTestAuthorization();
        authContext.SetAuthorizing();
        authContext.SetAuthorized("TEST USER");

        // Act
        var cut = ctx.RenderComponent<ReportDialog>();

        // Assert
        return Verify(cut.Markup, settings);
    }
}