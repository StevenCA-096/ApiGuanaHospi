using AutoMapper;
using DataAccess;
using DataAccess.DTO;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.SqlServer.Server;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDataAccess(builder.Configuration);



var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                      });
});


//builder.Services.AddControllers().AddNewtonsoftJson(options =>
//    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
//);

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(options =>
{

    //con este muestra las referencias circulares en el json luego de hacer post
    //options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;

    //con este ignoramos las referencias circulares y no se muestran en el json
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;


});

var configuration = new MapperConfiguration(cfg =>
{
    cfg.AddProfile<UsuarioMapper>();
});
var mapper = configuration.CreateMapper();

builder.Services.AddSingleton(mapper);

//JWT Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("the secret key that is going to be used")),
        ValidateIssuer = false,
        ValidIssuer = "test",
        ValidateAudience = false,
        ValidAudience = "test",
        RequireExpirationTime = false,
        ValidateLifetime = false,
        ClockSkew = TimeSpan.FromDays(1),
    };
});

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(MyAllowSpecificOrigins);
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
