using GitHub.API.Cached_Services;
using GitHubService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMemoryCache();
builder.Services.AddScoped<IGitHubService, GitHubService.GitHubService>();
builder.Services.Decorate<IGitHubService, CachedGitHubService>();

builder.Services.Configure<GitHubIntegretionOptions>(builder.Configuration.GetSection(nameof(GitHubIntegretionOptions)));

//var userTocken = builder.Configuration["GitHubUserTocken"];

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
