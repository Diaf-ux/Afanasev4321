using NLog;
using NLog.Web;
using Microsoft.EntityFrameworkCore;
using Afanasev4321.Models;
using FluentValidation.AspNetCore;
using FluentValidation;
using Afanasev4321.Middlewares; // ���������� ������������ ���� � Middleware

var builder = WebApplication.CreateBuilder(args);
var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();

try
{
    // Add services to the container.
    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    // ��������� �������� ���� ������ � ��������� �����
    builder.Services.AddDbContext<UniversityDbContext>(options =>
        options.UseNpgsql(builder.Configuration.GetConnectionString("UniversityDatabase")));

    // ��������� AutoMapper
    builder.Services.AddAutoMapper(typeof(Program));

    // ��������� FluentValidation
    builder.Services.AddValidatorsFromAssemblyContaining<TeacherDTOValidator>();
    builder.Services.AddFluentValidationAutoValidation()
                    .AddFluentValidationClientsideAdapters();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    // ���������� Middleware ��� ��������� ������
    app.UseMiddleware<ExceptionHandlingMiddleware>();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    logger.Error(ex, "Stopped program because of exception");
    throw; // ��� �������, ����� ����� ������� ���������� � �����
}
finally
{
    LogManager.Shutdown();
}
