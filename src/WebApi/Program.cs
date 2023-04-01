using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Saiketsu.Service.Party.Application;
using Saiketsu.Service.Party.Application.Common;
using Saiketsu.Service.Party.Domain.Options;
using Saiketsu.Service.Party.Infrastructure;
using Saiketsu.Service.Party.Infrastructure.Persistence;
using Serilog;
using Serilog.Events;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateBootstrapLogger();

static void AddMiddleware(WebApplication app)
{
    app.UseSerilogRequestLogging();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.MapControllers();
}

static void AddServices(WebApplicationBuilder builder)
{
    builder.Services.AddRouting(options => options.LowercaseUrls = true);
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();

    builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(IApplicationMarker).Assembly));
    builder.Services.AddValidatorsFromAssemblyContaining<IApplicationMarker>();

    builder.Services.AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Version = "v1",
            Title = "Party API",
            Description = ".NET Web API for managing Saiketsu parties."
        });

        options.EnableAnnotations();
        options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Saiketsu.Service.Party.Application.xml"));
        options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Saiketsu.Service.Party.Domain.xml"));
    });

    builder.Services.AddDbContext<ApplicationDbContext>(options =>
    {
        options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"),
                builder => { builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName); })
            .UseSnakeCaseNamingConvention();
    });

    builder.Services.Configure<RabbitMQOptions>(builder.Configuration.GetSection(RabbitMQOptions.Position));

    builder.Services.AddSingleton<IEventBus, RabbitEventBus>();
    builder.Services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
}

static void InjectSerilog(WebApplicationBuilder builder)
{
    builder.Host.UseSerilog((context, services, configuration) => configuration
        .ReadFrom.Configuration(context.Configuration)
        .ReadFrom.Services(services)
        .Enrich.FromLogContext()
        .WriteTo.Console());
}

try
{
    Log.Information("Starting web application");

    var builder = WebApplication.CreateBuilder(args);

    InjectSerilog(builder);
    AddServices(builder);

    var app = builder.Build();

    AddMiddleware(app);

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}