using System.Text;
using Data;
using Data.Model.Options;
using Data.Repository;
using Data.Repository.Interface;
using Logic.Service;
using Logic.Service.Interface;
using MaxOHara;
using MaxOHara.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Quartz;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors();
builder.Services.AddControllers();

/*builder.Services.AddQuartz(q =>
{
    q.UseMicrosoftDependencyInjectionJobFactory();
    var jobKey = new JobKey("MaxOHara");
    q.AddJob<CheckPaymentStatus>(opts => opts.WithIdentity(jobKey));
    q.AddTrigger(opts => opts
        .ForJob(jobKey)
        .WithSimpleSchedule(x => x
            .WithInterval(TimeSpan.FromSeconds(1200)) //10minutes
            .RepeatForever())
    );
    jobKey = new JobKey("MaxOHara2");
    q.AddJob<ReservesIiko>(opts => opts.WithIdentity(jobKey));
    q.AddTrigger(opts => opts
        .ForJob(jobKey)//1day
    );
    jobKey = new JobKey("MaxOHara3");
    q.AddJob<CheckReserveDateTime>(opts => opts.WithIdentity(jobKey));
    q.AddTrigger(opts => opts
        .ForJob(jobKey)
        .WithSimpleSchedule(x => x
            .WithInterval(TimeSpan.FromSeconds(3600)) //1 hour
            .RepeatForever())
    );
});
builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);*/

var appConfig = builder.Configuration;
builder.Services.Configure<JwtTokenOptions>(builder.Configuration.GetSection(JwtTokenOptions.JwtToken));
builder.Services.Configure<FilesOptions>(builder.Configuration.GetSection(FilesOptions.Files));
//builder.Services.Configure<BookingOptions>(builder.Configuration.GetSection(BookingOptions.Booking));
//builder.Services.Configure<ForIIKO>(builder.Configuration.GetSection(ForIIKO.DataShopForIiko));
//builder.Services.Configure<AdminForEmail>(builder.Configuration.GetSection(AdminForEmail.Admin));


builder.Services.AddDbContext<DbContext, MaxOHaraContext>(option =>
{
    option.UseLazyLoadingProxies();
    AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    option.UseNpgsql(appConfig["ConnectionStrings"]);
});

builder.Services.AddScoped<IScopeInfo, ScopeInfo>();
//builder.Services.AddScoped<IFeatureRepository, FeatureRepository>();
builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
builder.Services.AddScoped(typeof(ILendingRepository<>), typeof(LendingRepository<>));
builder.Services.AddScoped<IBannerLendingRepository, BannerLendingRepository>();
builder.Services.AddScoped<IAboutLendingRepository, AboutLendingRepository>();
builder.Services.AddScoped<IAtmosphereLendingRepository, AtmosphereLendingRepository>();
builder.Services.AddScoped<ISliderLendingRepository, SliderLendingRepository>();

builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IMenuRepository, MenuRepository>();
builder.Services.AddTransient<IFilesRepository, FilesRepository>();
builder.Services.AddTransient<INewsRepository, NewsRepository>();
builder.Services.AddTransient<IGalleryRepository, GalleryRepository>();
//builder.Services.AddTransient<IClientRepository, ClientRepository>();
//builder.Services.AddTransient<ITablesRepository, TablesRepository>();
//builder.Services.AddTransient<IReservesRepository, ReservesRepository>();

builder.Services.AddScoped(typeof(IBaseService<>), typeof(BaseService<>));
builder.Services.AddScoped(typeof(ILendingService<>), typeof(LendingService<>));
builder.Services.AddScoped<BannerLendingService>();
builder.Services.AddScoped<AboutLendingService>();
builder.Services.AddScoped<AtmosphereLendingService>();

builder.Services.AddTransient<IAuthService, AuthService>();
builder.Services.AddTransient<IJwtService, JwtService>();
builder.Services.AddTransient<UserService>();
builder.Services.AddTransient<INewsService, NewsService>();
builder.Services.AddTransient<IGalleryService, GalleryService>();
builder.Services.AddTransient<IMenuService, MenuService>();
builder.Services.AddTransient<IFilesService, FilesService>();
builder.Services.AddTransient<ISliderService, SliderLendingService>();
//builder.Services.AddTransient<IClientService, ClientService>();
//builder.Services.AddTransient<ITablesService, TablesService>();
//builder.Services.AddTransient<IIikoService, IikoService>();
//builder.Services.AddTransient<IReservesService, ReservesService>();
//builder.Services.AddTransient<ISendEmailService, SendEmailService>();

builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var jwt = new JwtTokenOptions();
        appConfig.GetSection(JwtTokenOptions.JwtToken).Bind(jwt);
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = jwt.Issuer,
            ValidateAudience = true,
            ValidAudience = jwt.Audience,
            ValidateLifetime = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key)),
            ValidateIssuerSigningKey = true,
        };
    });



var app = builder.Build();

/*using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<MaxOHaraContext>();
    db.Database.Migrate();
}*/

app.UseCors(x=>x.AllowAnyMethod().AllowAnyHeader().SetIsOriginAllowed(_=>true).AllowCredentials());
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<AuthMiddleware>();
app.UseMiddleware<ErrorHandlingMiddleware>();
//app.UseMiddleware<CheckBookingMiddleware>();

Directory.CreateDirectory("StaticFiles");
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), @"StaticFiles")),
    RequestPath = new PathString("/StaticFiles"),
});

app.MapControllerRoute(name: "default", pattern: "/");

app.Run();