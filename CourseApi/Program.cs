using CourseApi.DataLayer.CommonModels;
using CourseApi.DataLayer.DataContext;
using CourseApi.DataLayer.DataContext.Entities;
using CourseApi.DataLayer.Initializer;
using CourseApi.Service.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;
using CourseApi.DataLayer.UoW;
using FluentValidation;
using System.Globalization;
using CourseApi.ExceptionHandler;
using MassTransit;
using CourseApi.Messaging.Consumer;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers()
	.AddJsonOptions(options =>
	{
		options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
	});

builder.Services.Configure<TokenOption>(builder.Configuration.GetSection("TokenOption"));

builder.Services.AddDbContext<CourseDbContext>(options =>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("CourseDbConnection"));
});

builder.Services.AddIdentity<AppUser, AppRole>(opt =>
{
	opt.Password.RequireDigit = true;
	opt.Password.RequireLowercase = true;
	opt.Password.RequireUppercase = true;
	opt.Password.RequireNonAlphanumeric = true;
	opt.Password.RequiredLength = 8;
	opt.User.RequireUniqueEmail = true;
	opt.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+"; // add handler in UI?
}).AddEntityFrameworkStores<CourseDbContext>().AddDefaultTokenProviders();

builder.Services.AddAuthentication(options =>
{
	options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
	.AddJwtBearer(opt =>
	{
		var tokenOptions = builder.Configuration.GetSection("TokenOption").Get<TokenOption>()!;
		opt.TokenValidationParameters = new TokenValidationParameters()
		{
			ValidIssuer = tokenOptions.Issuer,
			ValidAudience = tokenOptions.Audience[0],
			IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOptions.SecurityKey)),
			ClockSkew = TimeSpan.Zero
		};
	});

if (builder.Environment.IsDevelopment())
{
	builder.Services.AddCors(options =>
	{
		options.AddPolicy("Localhost",
			policy =>
			{
				policy.SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost")
					  .AllowAnyMethod()
					  .AllowAnyHeader();
			});
	});
}

builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();

builder.Services.AddServices();

builder.Services.AddValidators();

builder.Services.AddLogging(builder => builder.AddConsole());

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddProblemDetails();

builder.Services.AddMassTransit(x =>
{
	x.AddEntityFrameworkOutbox<CourseDbContext>(o =>
	{
		o.QueryDelay = TimeSpan.FromSeconds(10);

		o.UseSqlServer();
		o.UseBusOutbox();
	});

	x.UsingRabbitMq((context, cfg) =>
	{
		cfg.Host("localhost", "/", h =>
		{
			h.Username("guest");
			h.Password("guest"); 
		});
		cfg.ConfigureEndpoints(context);
	});

	x.AddConsumersFromNamespaceContaining<PaymentCreatedConsumer>();

	x.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter(
		"course", false));
});

builder.Services.AddStackExchangeRedisCache(redisOptions =>
{
	redisOptions.Configuration = builder.Configuration.GetConnectionString("Redis");
});

ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("en");

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

app.UseHttpsRedirection();

if (app.Environment.IsDevelopment())
{
	app.UseCors("Localhost");
}

app.UseExceptionHandler();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

try
{
	DbInitializer.InitDb(app);
}
catch (Exception ex)
{
	Console.WriteLine(ex.Message);
}
app.Run();
