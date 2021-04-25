﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace DevIO.Api.Configuration
{
    public static class ApiConfig
    {
        public static IServiceCollection WebApiConfig(this IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.Configure<ApiBehaviorOptions>(options =>
            {
                //estou suprimindo pq quero persoanlizar os erros retornados ao Cliente
                options.SuppressModelStateInvalidFilter = true;
            });

            //abertura de api para utilização por aplicação de outro dominio que está em outra política via CORS
            services.AddCors(options =>
            {
                options.AddPolicy("Development", builder =>
                builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });

            services.AddCors(options =>
            {
                options.AddPolicy("Production", builder =>
                builder
                    .WithMethods("GET", "POST")
                    .WithOrigins("htpps://urlDoSistemaemProducao.com")
                    .SetIsOriginAllowedToAllowWildcardSubdomains()
                    .AllowAnyHeader());
            });

            return services;
        }


        public static IApplicationBuilder UseMvcConfiguration(this IApplicationBuilder app)
        {
            app.UseHttpsRedirection();
            /*Faz o redirecionamento para HTTPS indepente se a chamada foi HTTPS ou HTTP */
            app.UseMvc();
            return app;
        }
    }
 }
