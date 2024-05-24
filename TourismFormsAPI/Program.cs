using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;

using TourismFormsAPI.Interfaces;
using TourismFormsAPI.Models;
using TourismFormsAPI.Repositories;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TourismFormsAPI.Tools;
using TourismFormsAPI.Interfaces.Services;
using TourismFormsAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("isAdmin", policy => policy.Requirements.Add(new HasAdminClaim()));
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(x => {

    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration.GetSection("JwtSettings:Issuer").Value,
        ValidAudience = builder.Configuration.GetSection("JwtSettings:Audience").Value,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("JwtSettings:Key").Value!)),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
    };
});


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);

string connection = builder.Configuration.GetConnectionString("Tourism")!;
builder.Services.AddTransient<IAnswerRepository, AnswerRepository>();
builder.Services.AddTransient<IAuthRepository, AuthRepository>();
builder.Services.AddDbContext<TourismContext>(options => options.UseSqlServer(connection));
builder.Services.AddTransient<IMunicipalityRepository, MunicipalityRepository>();
builder.Services.AddTransient<IFormRepository, FormRepository>();
builder.Services.AddTransient<IFillMethodRepository, FillMethodRepository>();
builder.Services.AddTransient<IMeasureRepository, MeasureRepository>();
builder.Services.AddTransient<ICriteriaRepository, CriteriaRepository>();
builder.Services.AddTransient<ISurveyRepository, SurveyRepository>();

builder.Services.AddTransient<IEmailSenderService, EmailSenderService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(opt => { opt.AllowAnyHeader(); opt.AllowAnyOrigin(); opt.AllowAnyMethod(); });
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
