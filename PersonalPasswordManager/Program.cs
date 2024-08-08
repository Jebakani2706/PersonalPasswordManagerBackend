using PersonalPasswordManager.DBContext;
using Microsoft.EntityFrameworkCore;
using PersonalPasswordManager.ModelView;
using PersonalPasswordManager.DIRResolver;

var builder = WebApplication.CreateBuilder(args);
IConfigurationRoot configuration = new ConfigurationBuilder()
.AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: false, reloadOnChange: true).Build();
builder.Configuration.AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json");
builder.Services.AddDbContext<PasswordManagerDBContext>(options =>
                        options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString"), builder =>
                        {
                            builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
                        }));
var appSettings = builder.Configuration.GetSection(nameof(AppSettings)).Get<AppSettings>();
builder.Services.AddSingleton(appSettings!);
builder.Services.PMDIResolver();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();
app.UseCors(x => x
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
app.MapControllers();
app.Run();
