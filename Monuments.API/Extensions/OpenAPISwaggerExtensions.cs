using Microsoft.OpenApi;
using System.Reflection;

namespace Monuments.API.Extensions;

public static class OpenAPISwaggerExtensions
{
    public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(setupAction =>
        {
            var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);

            setupAction.IncludeXmlComments(xmlCommentsFullPath);

            //MAKE AUTHORIZE BUTTON DISPLAY ON PAGE
            setupAction.AddSecurityDefinition("MonumentsApiBearerAuth", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.Http,
                Name = "MonumentsApiBearerAuth",
                Scheme = "Bearer",
                Description = "Input a valid token to access this API"
            });


            setupAction.AddSecurityRequirement((document) => new OpenApiSecurityRequirement()
            {
                [new OpenApiSecuritySchemeReference("MonumentsApiBearerAuth", document)] = []
            });

            #region DEPRECATED IN .NET 10            
            //setupAction.AddSecurityRequirement(new OpenApiSecurityRequirement
            //{
            //    {
            //        new OpenApiSecurityScheme
            //        {
            //            Reference = new OpenApiReference
            //            {
            //                Type=ReferenceType.SecurityScheme,
            //                Id = "MonumentsApiBearerAuth"
            //            }, new List<string>()
            //        }
            //    }
            //});
            #endregion

            setupAction.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "National Monuments API",
                Version = "v1",
                Description = """

                <img src="/Images/nigeria.jpg" height="30px"/>

                ## Tour Nigeria

                API for exploring national monuments in Nigeria.
                Provides brief descriptions of interesting sites to visit

                ### Key Features:
                - Sites and their locations
                - Slogans of states Nigeria
                - Grouping national monuments into states

                """,
                Contact = new OpenApiContact
                {
                    Name = "Nigeria National Monument",
                    Url = new Uri("https://tournigeria.com"),
                    Email = "support@tournigeria.com",
                    
                }
            });
        });


        return services;
    }
}
