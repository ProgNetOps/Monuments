using Microsoft.AspNetCore.StaticFiles;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using Monuments.API.DbContexts;
using Monuments.API.Profiles;
using Monuments.API.Services;
using Serilog;
using System.Reflection;
using System.Text;

namespace Monuments.API.Extensions;

internal static class StartupHelperExtensions
{
    //ADD SERVICES TO CONTAINER
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        //Configure Serilog
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console()
            .WriteTo.File("logs/monumentsApi.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();

        //builder.Logging.ClearProviders();
        //builder.Logging.AddConsole();

        //Use Serilog in place of the in-built logger
        builder.Host.UseSerilog();

        // Add services to the container.

        builder.Services.AddControllers(options =>
        {
            options.ReturnHttpNotAcceptable = true; //Return a message for unsupported formats instead of giving json output
        }).AddNewtonsoftJson() //Replacing the default input/output formatters
        .AddXmlDataContractSerializerFormatters(); //XML Support

        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddCustomSwagger();
       builder.Services.AddOpenApi();
        builder.Services.AddSingleton<FileExtensionContentTypeProvider>();//Detect MIME types of file

        //COMPILER DIRECTIVE
#if DEBUG
        builder.Services.AddTransient<IMailService, LocalMailService>();
#else
builder.Services.AddTransient<IMailService,CloudMailService>();
#endif


        builder.Services.AddSingleton<CitiesDataStore>();
        builder.Services.AddDbContext<MonumentsDbContext>(options =>
        {
            options.UseSqlite(builder.Configuration["ConnectionStrings:MonumentDbConnectionString"]);
        });
        builder.Services.AddScoped<IMonumentRepository, MonumentRepository>();

        //Register Automapper
        builder.Services.AddAutoMapper(cfg =>
        {
            cfg.AddProfile<CityProfile>(); //
            cfg.AddProfile<MonumentProfile>(); //

        });

        //Configure token validation
        builder.Services.AddAuthentication("Bearer")
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Authentication:Issuer"],
                    ValidAudience = builder.Configuration["Authentication:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.ASCII.GetBytes(builder.Configuration["Authentication:SecretForKey"]))
                };
            });

        //Add Authorization Policies
        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("MustBeFromBauchi", policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.RequireClaim("city", ["Bauchi"]);
            });

        });

        //Add support for versioning
        builder.Services.AddApiVersioning(setupAction =>
        {
            //Assume a default value if consumer doesn't specify version
            setupAction.AssumeDefaultVersionWhenUnspecified = true;

            //Set default version to 1.0
            setupAction.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
            setupAction.ReportApiVersions = true;
        });

        return builder.Build();
    }


    //CONFIGURE THE REQUEST/RESPONSE PIPELINE
    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();//To serve files in wwwroot
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints => {
            endpoints.MapControllers();
        });

        return app;
    }

}
