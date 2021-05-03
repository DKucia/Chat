using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Chat.Api.Data;
using Chat.Api.Hubs;
using Chat.Api.Services;
using Chat.Api.Services.Auth;
using Chat.Api.Services.Avatar;
using Chat.Api.Settings;
using Chat.Api.Validators;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace Chat.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped(typeof(IMongoProvider<>), typeof(MongoProvider<>));
            services.AddScoped<IEncrypter, Encrypter>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IJwtHandler, JwtHandler>();
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<IAvatarService, AvatarService>();
            services.AddScoped<IConversationService, ConversationService>();
            services.Configure<JwtSettings>(Configuration.GetSection("Jwt"));
            services.AddAuthentication(options=> {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x=> {
                x.RequireHttpsMetadata = false;
                var key = Configuration.GetSection("Jwt")["secret"];
                x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                {
                   
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };

                x.Events = new JwtBearerEvents()
                {
                    OnMessageReceived = (context) =>
                    {
                        var accessToken = context.Request.Query["access_token"];

                        // If the request is for our hub...
                        var path = context.HttpContext.Request.Path;
                        if (!string.IsNullOrEmpty(accessToken) &&
                            (path.StartsWithSegments("/chathub")))
                        {
                            context.Token = accessToken;
                        }
                        return Task.CompletedTask;
                    }
                };
            });

            services.AddCors(options =>
            {
                options.AddPolicy("default",
                                  builder =>
                                  {
                                      builder.WithOrigins("*")
                                                          .AllowAnyHeader()
                                                          .AllowAnyMethod();
                                  });
            });
            services.AddSignalR();
            services.AddControllers().AddFluentValidation(c=>c.RegisterValidatorsFromAssemblyContaining<RegisterDtoValidator>())
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.InvalidModelStateResponseFactory = c =>
                    {
                        var errors = string.Join('\n', c.ModelState.Values.Where(v => v.Errors.Count > 0)
                          .SelectMany(v => v.Errors)
                          .Select(v => v.ErrorMessage));

                        return new BadRequestObjectResult(new
                        {
                            Error = errors
                        });
                    };
                });
            MongoClassMapRegister.RegisterMapping();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseExceptionHandler(
               options =>
               {
                   options.Run(
                       async context =>
                       {
                           context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                           var ex = context.Features.Get<IExceptionHandlerFeature>();
                           if (ex != null)
                           {
                               await context.Response.WriteAsJsonAsync(new { Error=ex.Error.Message}).ConfigureAwait(false);
                           }
                       });
               }
           );


            app.UseAuthentication();
            app.UseRouting();
            app.UseCors("default");
            
            app.UseAuthorization();
            app.UseStaticFiles();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<ChatHub>("/chathub");
            });
        }
    }
}
