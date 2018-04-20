// Copyright (C) Bjarne Holen 2018. BSD compatible license.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Http;

namespace WebAppX
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
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Look for index.htm[l]? or default.htm[l]?
            app.UseDefaultFiles();
            // Use static files inside wwwroot
            // app.UseStaticFiles();

            // There has to be a simpler way to set encoding?
            app.UseStaticFiles(new StaticFileOptions
            {
                OnPrepareResponse = ctx =>
                {
                    var contentType = ctx.Context.Response.GetTypedHeaders().ContentType;
                    if( contentType.ToString().StartsWith("text/") ){
                        contentType.Encoding = System.Text.Encoding.UTF8;
                        ctx.Context.Response.Headers["Content-Type"] = contentType.ToString();
                    }
                }
            });

            // Directory listing
            app.UseDirectoryBrowser();

            // Example explicit Route binding and handler
            var routeBuilder = new RouteBuilder(app);

            routeBuilder.MapGet("hello/{name}", context =>
            {
                var name = context.GetRouteValue("name");
                // This is the route handler when HTTP GET "hello/<anything>"  matches
                // To match HTTP GET "hello/<anything>/<anything>,
                // use routeBuilder.MapGet("hello/{*name}"
                return context.Response.WriteAsync($"Hi, {name}");
            });

            var routes = routeBuilder.Build();
            app.UseRouter(routes);

            // Extracts routes from the Attribute stuff inside the Controllers I think..
            app.UseMvc();
        }
    }
}
