using AutoMapper;
using steamPy.Helpers;
using steamPy.Helpers.IHelpers;
using steamPy.Services;
using SteamPy.SDK.Helper;
using System.Reflection;

namespace steamPy.Setup
{
    public static class injectSetup
    {
        public static void AddInjectSetup(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IMailHelper,SmtpMailHelper>();
            services.AddScoped<ISteamPyServices, SteamPyServices>();
            services.AddScoped<ITaskServices, TaskServices>();

            services.AddRazorPages();
            services.AddHttpClient();
            services.AddHttpContextAccessor();
            services.AddMiniProfiler();
            services.AddSession();
            services.AddMemoryCache();

            services.AddCors(a =>
            {
                a.AddDefaultPolicy(policy =>
                {
                    policy.SetIsOriginAllowed(_ => true)
                    .AllowAnyHeader().AllowAnyMethod();
                });
            });

            List<Assembly> assemblies = new List<Assembly>();
            assemblies.Add(Assembly.GetExecutingAssembly());

            services.AddAutoMapper(config =>
            {

                var profiles = assemblies.Select(x => x.GetTypes())
                    .Select(x =>
                    {
                        foreach (var type in x)
                        {
                            if (typeof(Profile).IsAssignableFrom(type))
                            {
                                return Activator.CreateInstance(type) as Profile;
                            }
                        }
                        return null;
                    }).Where(x => x != null);
                config.AddProfiles(profiles);
            });

        }


    }
}
