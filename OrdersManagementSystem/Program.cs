using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using OrdersManagementSystem.Data;
using OrdersManagementSystem.Entities.Entities;
using OrdersManagementSystem.Services;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Text;


var builder = WebApplication.CreateBuilder(args);


var key1 = Encoding.UTF8.GetBytes("Qweras778992**__))(()(iiiiiii8)(5d")
	.Take(32)
	.ToArray();

var key = Convert.ToBase64String(key1);
builder.Services.AddAuthentication(x =>
{
	x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
	x.RequireHttpsMetadata = false;
	x.SaveToken = true;
	x.TokenValidationParameters = new TokenValidationParameters
	{
		ValidateIssuerSigningKey = true,
		IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
		ValidateIssuer = false,
		ValidateAudience = false,

	};
});
builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowSpecificOrigin",
		builder =>
		{
			builder.WithOrigins("http://localhost:4200") // Allow requests from this origin
				   .AllowAnyMethod()                  // Allow all HTTP methods
				   .AllowAnyHeader();                 // Allow all HTTP headers
		});
});

        // Other service configurations...
   
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<ItemService>();
builder.Services.AddScoped<OrderService>();
builder.Services.AddScoped<JwtAuthenticationService>(provider =>
	new JwtAuthenticationService(key, provider.GetService<UserService>()));
builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(AuthMapperProfile).Assembly);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
	options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
	{
		Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
		In = ParameterLocation.Header,
		Name = "Authorization",
		Type = SecuritySchemeType.ApiKey 
	});

	options.OperationFilter<SecurityRequirementsOperationFilter>();
}); builder.Services.AddDbContext<DatabaseContext>(options =>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("default"));
	options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

});
builder.Services.AddIdentity<User, IdentityRole>()
	.AddDefaultTokenProviders().AddRoles<IdentityRole>()
	.AddEntityFrameworkStores<DatabaseContext>();

//builder.Services.AddIdentity<User, IdentityRole>()F
//	.AddEntityFrameworkStores<DatabaseContext>()
//	.AddDefaultTokenProviders();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowSpecificOrigin");


app.MapControllers();

app.Run();
