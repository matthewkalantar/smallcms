using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using SmallCMS.API.Endpoints;
using SmallCMS.API.Middlewares;
using SmallCMS.Application;
using SmallCMS.Infrastructure;
using SmallCMS.Infrastructure.Data;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.RegisterInfrastructureServices(builder.Configuration);

builder.Services.RegisterApplicationServices();

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<SmallCmsDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        //TODO: JWT key shuold to move to env-var or secret
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

builder.Services.AddAuthorization(cfg =>
{
    var defaultAuthorizationPolicyBuilder = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme);
    defaultAuthorizationPolicyBuilder =
        defaultAuthorizationPolicyBuilder.RequireAuthenticatedUser();
    cfg.DefaultPolicy = defaultAuthorizationPolicyBuilder.Build();
});


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(builder =>
{
    // TODO: Define Cors before deployment
    builder.AddPolicy("AllowAll", options =>
    {
        options.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

builder.Services.AddHttpContextAccessor();
var app = builder.Build();

// Exception handling middleware
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapBlogPostEndpoints();
app.MapUserEndpoints();

app.UseCors("AllowAll");

app.Run();

