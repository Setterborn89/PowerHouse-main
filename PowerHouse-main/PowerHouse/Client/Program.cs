using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor;
using MudBlazor.Services;
using PowerHouse.Client;
using PowerHouse.Client.Authorization;
using PowerHouse.Client.Services;
using PowerHouse.Client.Temp;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddMudServices(config =>
{
    config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomLeft;

    config.SnackbarConfiguration.NewestOnTop = true;
    config.SnackbarConfiguration.ShowCloseIcon = true;
    config.SnackbarConfiguration.VisibleStateDuration = 3000;
    config.SnackbarConfiguration.HideTransitionDuration = 500;
    config.SnackbarConfiguration.ShowTransitionDuration = 500;
    config.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
});

builder.Services.AddHttpClient("PowerHouse.ServerAPI", client =>
client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
    .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();
builder.Services.AddHttpClient<PublicClient>("PowerHouse.PublicServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("PowerHouse.ServerAPI"));

builder.Services.AddAuthorizationCore();
builder.Services.AddBlazoredSessionStorage();

builder.Services
    .AddMsalAuthentication<RemoteAuthenticationState, CustomUserAccount>(options =>
    {
        builder.Configuration.Bind("AzureAdB2C", options.ProviderOptions.Authentication);
        options.ProviderOptions.DefaultAccessTokenScopes.Add("https://newtonpowerhouse.onmicrosoft.com/293b950f-27ad-414a-a75a-737317bd2b51/API.Access");
        options.UserOptions.RoleClaim = "appRole";
        options.AuthenticationPaths.RemoteProfilePath = string.Format(
            builder.Configuration.GetRequiredSection("AzureAdB2C").GetValue<string>("RemoteProfilePath")!,
            builder.Configuration.GetRequiredSection("AzureAdB2C").GetValue<string>("EditProfilePolicyId")!,
            builder.Configuration.GetRequiredSection("AzureAdB2C").GetValue<string>("ClientId")!,
            builder.HostEnvironment.BaseAddress);

    })
    .AddAccountClaimsPrincipalFactory<RemoteAuthenticationState, CustomUserAccount, CustomAccountFactory>();

builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
builder.Services.AddScoped<IDeleteAccount, DeleteAccount>();
builder.Services.AddScoped<IAppState, AppState>();

await builder.Build().RunAsync();
