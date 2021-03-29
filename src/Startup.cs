using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.SpaServices.Extensions;
using System.IO;

namespace hughesofficeapp
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSpaStaticFiles();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.Map("/apps/sample", spaApp => {
                spaApp.UseSpa(spa => {
                    spa.Options.SourcePath = "/apps/sample";
                    spa.Options.DefaultPageStaticFileOptions = new StaticFileOptions
                    {
                        FileProvider = new PhysicalFileProvider(Path.Combine(env.WebRootPath, "apps/sample")),
                    };
                    spa.Options.DefaultPage = "/index.html";
                });
            });

            var binPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            app.Map("/apps/emailrecovery", spaApp => {
                spaApp.UseStaticFiles(new StaticFileOptions
                {
                    FileProvider = new PhysicalFileProvider(Path.Combine(binPath, "apps/emailrecovery"))
                });

                spaApp.UseSpa(spa => {
                    spa.Options.SourcePath = "/apps/emailrecovery";
                    spa.Options.DefaultPageStaticFileOptions = new StaticFileOptions
                    {
                        FileProvider = new PhysicalFileProvider(Path.Combine(binPath, "apps/emailrecovery")),
                    };
                    spa.Options.DefaultPage = "/index.html";
                });
            });
        }
    }
}
