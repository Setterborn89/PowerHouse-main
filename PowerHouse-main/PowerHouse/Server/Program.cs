using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using PowerHouse.Server.Data;
using PowerHouse.Server.Hubs;
using PowerHouse.Server.Interfaces.Repository;
using PowerHouse.Server.Interfaces.Services;
using PowerHouse.Server.Models;
using PowerHouse.Server.Repositories;
using PowerHouse.Server.Services;
using PowerHouse.Shared.DTO;
using Microsoft.Identity.Web;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAdB2C"));

builder.Services.AddControllersWithViews()
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve)
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddRazorPages();
builder.Services.AddDbContext<PowerHouseDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("PowerHouseDbContext") ?? throw new InvalidOperationException("Connection string 'PowerHouseDbContext' not found.")));
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();
builder.Services.AddResponseCompression(opts =>
{
    opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
        new[] { "application/octet-stream" });
});

#region Automapper
MapperConfiguration mapperConfig = new MapperConfiguration(cfg =>
{
    cfg.CreateMap<Message, MessageDTO>().ReverseMap();
    cfg.CreateMap<Conversation, ConversationDTO>().ReverseMap();
	cfg.CreateMap<Conversation, CreateTopicDTO>().ReverseMap();
	cfg.CreateMap<Report, ReportDTO>().ReverseMap();
    cfg.CreateMap<User, UserDTO>().ReverseMap();
	cfg.CreateMap<User, RegisterDTO>().ReverseMap();
    cfg.CreateMap<Message, PostMessageDTO>().ReverseMap();
    cfg.CreateMap<User, UserConversationDTO>().ReverseMap();


});
IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);
#endregion 

#region Services
builder.Services.AddScoped<IConversationService, ConversationService>();
builder.Services.AddScoped<IMessageService, MessageService>();
builder.Services.AddScoped<IReportService, ReportService>();
builder.Services.AddScoped<IUserService, UserService>();

#endregion

#region Repository
builder.Services.AddScoped<IConversationRepository, ConversationRepository>();
builder.Services.AddScoped<IMessageRepository, MessageRepository>();
builder.Services.AddScoped<IReportRepository, ReportRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

#endregion


var app = builder.Build();
app.UseResponseCompression();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseResponseCompression();
    app.UseWebAssemblyDebugging();
    app.MigrateDatabase();
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapHub<ConversationHub>("/messages");

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
