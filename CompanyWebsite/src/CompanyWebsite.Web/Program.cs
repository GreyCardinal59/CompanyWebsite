using CompanyWebsite.Web;
using CompanyWebsite.Web.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddProgramDependencies(builder.Configuration);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

app.UseExceptionMiddleware();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

// app.UseHttpsRedirection();

// app.UseAuthorization();

app.MapControllers();

app.ApplyMigrations();

app.UseStaticFiles();
app.MapFallbackToFile("index.html");

app.Run();