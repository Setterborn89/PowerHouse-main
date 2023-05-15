using Microsoft.AspNetCore.Components;

namespace PowerHouse.Client.Authorization;

public class DeleteAccount : IDeleteAccount
{
    private readonly IConfiguration _conf;
    private readonly NavigationManager _nav;

    public DeleteAccount(IConfiguration conf, NavigationManager nav)
    {
        _conf = conf;
        _nav = nav;
    }

    public string GetLink() => string.Format(
        _conf.GetRequiredSection("AzureAdB2C").GetValue<string>("RemoteDeleteAccountPath")!,
        _conf.GetRequiredSection("AzureAdB2C").GetValue<string>("DeleteAccountPolicyId")!,
        _conf.GetRequiredSection("AzureAdB2C").GetValue<string>("ClientId")!,
        _nav.BaseUri);
}