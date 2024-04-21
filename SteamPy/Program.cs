using Hangfire;
using Hangfire.Dashboard.BasicAuthorization;
using Hangfire.Storage.SQLite;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using steamPy.Attributes;
using steamPy.Db;
using steamPy.Setup;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Logging.AddLog4Net("log4netDefault.config");
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);


builder.Services.AddInjectSetup();
builder.Services.AddControllersWithViews(opt =>
{
    opt.Filters.Add(typeof(GlobalExceptionsFilter));
    opt.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
}).AddJsonOptions(option =>
{
    option.JsonSerializerOptions.WriteIndented = true;
    option.JsonSerializerOptions.PropertyNamingPolicy = null;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


GlobalJobFilters.Filters.Add(new AutomaticRetryAttribute { Attempts = 5 });
builder.Services.AddHangfire(configuration =>
    configuration.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
        .UseSimpleAssemblyNameTypeSerializer()
        .UseRecommendedSerializerSettings()
        .UseSQLiteStorage(builder.Configuration.GetConnectionString("SqliteConnection").Replace("Filename=", "").Replace("data source=", "")));

builder.Services.AddHangfireServer();

builder.Services.AddControllersWithViews().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
});
builder.Services.AddDbContext<SteamPyDbContext>(options =>
{
    var connectionStr = builder.Configuration.GetConnectionString("SqliteConnection");//�����������appsettings.json������ӵ����ݿ����Ӵ�
    options.UseSqlite(connectionStr);
});


Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
}
app.UseSwagger();
app.UseSwaggerUI();
app.UseHangfireDashboard("/hangfire", new DashboardOptions
{
    Authorization = new[] { new BasicAuthAuthorizationFilter(
        new BasicAuthAuthorizationFilterOptions
        {
            RequireSsl = false,
            SslRedirect = false,
            LoginCaseSensitive = true,
            Users = new[]
            {
                new BasicAuthAuthorizationUser
                {
                    Login = builder.Configuration["hangfire:LoginId"],
                    PasswordClear = builder.Configuration["hangfire:Password"]
                }
            }
        }) }
});

app.UseStaticFiles();
app.UseRouting();
app.UseSession();

app.UseAuthorization();
//�������ܷ���
app.UseMiniProfiler();


app.MapRazorPages();
app.MapControllers();

app.Run();
