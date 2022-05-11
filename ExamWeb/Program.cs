using Microsoft.EntityFrameworkCore;
using ExamWebEF;
using AutoMapper;
using ExamWeb.Mapper;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetSection("ConnectionStrings")["SqlServer"];

builder.Services.AddControllers();
builder.Services.AddControllers().AddNewtonsoftJson(
    opt => opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
builder.Services.AddDbContext<ExamContext>(opt => {
    opt.UseSqlServer(connectionString);
    opt.EnableSensitiveDataLogging();
}) ;
builder.Services.AddCors(options =>
{
    options.AddPolicy("any", builder =>
    {
        builder.WithMethods("GET", "POST", "HEAD", "PUT", "DELETE", "OPTIONS")
        .AllowAnyHeader().AllowAnyOrigin();
    });
});
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("any");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
