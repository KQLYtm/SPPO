using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using sppo.Areas.Identity.Data;
using sppo.Data;

[assembly: HostingStartup(typeof(sppo.Areas.Identity.IdentityHostingStartup))]
namespace sppo.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<MyContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("MyContextConnection")));

                services.AddDefaultIdentity<Profile>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<MyContext>();
            });
        }
    }
}