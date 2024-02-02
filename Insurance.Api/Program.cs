using Application.Shared.Middlewares;
using Insurance.Api;

var builder = WebApplication.CreateBuilder(args);

//IOC Container 
new DependencyResolver(builder.Configuration).Resolve(builder.Services);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<ErrorHandlerMiddleware>();

app.MapControllers();

app.Run();
