using Mapster;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Net7.WebApi.Template;
using Net7.WebApi.Template.DataAccess;
using Net7.WebApi.Template.DataAccess.Contracts;
using Net7.WebApi.Template.DataAcess;
using Net7.WebApi.Template.Helpers;
using Net7.WebApi.Template.Models;
using Net7.WebApi.Template.Models.Options;
using Net7.WebApi.Template.Services;
using Serilog;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.SetBasePath(Directory.GetCurrentDirectory());

var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
builder.Configuration.AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true);
builder.Configuration.AddJsonFile($"appsettings.json", optional: true, reloadOnChange: true);

if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddUserSecrets<Program>();
}

builder.Configuration.AddEnvironmentVariables().Build();

// ### Add services to the container.
builder.Services.Configure<ConfigurationOptions>(builder.Configuration.GetSection(ConfigurationOptions.ConfigKey));
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
//
//
builder.Services.AddTransient(typeof(UserService));
builder.Services.AddTransient(typeof(AuthService));

builder.Services.AddMapster();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString(Constants.CONNECTION_STRING_JSON_KEY), providerOptions =>
    {
        providerOptions.CommandTimeout(180);
        providerOptions.EnableRetryOnFailure();
    });
});

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

var configOptions = builder.Configuration.GetSection(ConfigurationOptions.ConfigKey).Get<ConfigurationOptions>();

if (configOptions == null)
{
    throw new Exception("Startup failed, appsettings configuration missing");
}

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.IncludeErrorDetails = true;
    o.RequireHttpsMetadata = false;
    o.SaveToken = false;

    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = configOptions.Tokens.Issuer,
        ValidAudience = configOptions.Tokens.Issuer,
        ValidateLifetime = true,
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configOptions.Tokens.Key)),
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddAuthorization(policy =>
{
    policy.AddPolicy(Constants.REGULAR_USER_POLICY, x => x.RequireClaim(ClaimTypes.Role, "Regular")
    .RequireAssertion(m => m.User.HasClaim(m => m.Type == ClaimTypes.Name) && m.User.HasClaim(m => m.Type == ClaimTypes.NameIdentifier)));

    policy.AddPolicy(Constants.ADMIN_POLICY, x => x.RequireRole(ClaimTypes.Role, "Admin")
    .RequireAssertion(m => m.User.HasClaim(m => m.Type == ClaimTypes.Name) && m.User.HasClaim(m => m.Type == ClaimTypes.NameIdentifier)));

    policy.AddPolicy(Constants.READONLY_USER_POLICY, x => x.RequireAssertion(m => m.User.HasClaim(m => m.Type == ClaimTypes.Name && m.Type == ClaimTypes.NameIdentifier)));

    policy.DefaultPolicy = policy.GetPolicy(Constants.REGULAR_USER_POLICY)!;
});

//Configure number of interations on cryptocraphic alghoritm
builder.Services.Configure<PasswordHasherOptions>(opt => opt.IterationCount = 600_000);

var dpBuilder = builder.Services.AddDataProtection()
               .SetApplicationName(configOptions.AppName)
               .PersistKeysToDbContext<ApplicationDbContext>();
if (!string.IsNullOrWhiteSpace(configOptions.DataProtection.PfxBase64))
{
    X509KeyStorageFlags keyStorageFlags = RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
        ? X509KeyStorageFlags.EphemeralKeySet | X509KeyStorageFlags.MachineKeySet
        : X509KeyStorageFlags.DefaultKeySet;

    var pfxBytes = Convert.FromBase64String(configOptions.DataProtection.PfxBase64);
    var pfx = new X509Certificate2(pfxBytes, configOptions.DataProtection.PfxPwd, keyStorageFlags);

    dpBuilder
        .ProtectKeysWithCertificate(pfx)
        .UnprotectKeysWithAnyCertificate(pfx);
}

builder.Services.AddControllers();

// ### Add fluent validation like this here ###

//builder.Services.AddControllers()
//    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<T>())
//    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<T>());

// ###

builder.Services.AddApiVersioning(opt =>
{
    opt.DefaultApiVersion = new ApiVersion(1, 0);
    opt.AssumeDefaultVersionWhenUnspecified = true;
    opt.ReportApiVersions = true;
    opt.ApiVersionReader = ApiVersionReader.Combine(new UrlSegmentApiVersionReader(),
                                                    new HeaderApiVersionReader("x-api-version"),
                                                    new MediaTypeApiVersionReader("x-api-version"));
});

builder.Services.AddVersionedApiExplorer(setup =>
{
    setup.GroupNameFormat = "'v'VVV";
    setup.SubstituteApiVersionInUrl = true;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Net7.WebApi.Template", Version = "v1" });
    option.CustomOperationIds(e => $"{e.ActionDescriptor.RouteValues["controller"]}{e.ActionDescriptor.RouteValues["action"]}");
    option.UseAllOfToExtendReferenceSchemas();
    option.ExampleFilters();
    option.SchemaFilter<EnumerationToEnumSchemaFilter>();

    option.MapType<decimal>(() => new OpenApiSchema { Type = "number", Format = "decimal" });

    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.OperationFilter<AuthResponsesOperationFilter>();

    string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    option.IncludeXmlComments(xmlPath);
});
builder.Services.AddSwaggerExamples();

//Enrich logs with correlationId
builder.Host.UseSerilog((ctx, lc) => lc
 .ReadFrom.Configuration(builder.Configuration)
 .Enrich.WithCorrelationIdHeader("correlationId")
 );

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
        {
            options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                description.GroupName.ToUpperInvariant());
        }
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

//Added in order to run tests
public partial class Program
{ }