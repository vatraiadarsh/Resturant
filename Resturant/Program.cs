using Contracts;
using Entities;
using Entities.DataTransferObjects;
using Entities.ErrorModel;
using Entities.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<RepositoryContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());
});
    
builder.Services.AddTransient<IValidator<CategoryDto>, CategoryValidator>();


var app = builder.Build();

app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseExceptionHandler(appError =>
{
    appError.Run(async context =>
    {
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Response.ContentType = "application/json";
        var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
        if (contextFeature != null)
        {
            // await context.Response.WriteAsync(new ErrorDetails()
            // {
            //     StatusCode = context.Response.StatusCode,
            //     Message = "Internal Server Error."
            // }.ToString()); 
            
            // if environment is development
            await context.Response.WriteAsync(new ErrorDetails()
            {
                StatusCode = context.Response.StatusCode,
                Message = contextFeature.Error.Message
            }.ToString());

        }

    });

});

app.MapControllers();

app.Run();
