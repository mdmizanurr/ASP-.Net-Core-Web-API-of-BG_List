using Microsoft.EntityFrameworkCore;
using MyBGList.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ApplicationDb")));


builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(cfg =>
    {
        cfg.WithOrigins(builder.Configuration["AllowedOrigins"]);
        cfg.AllowAnyHeader();
        cfg.AllowAnyMethod();
    });

    options.AddPolicy(name: "AnyOrigin",
        cfg =>
        {
            cfg.AllowAnyOrigin();
            cfg.AllowAnyHeader();
            cfg.AllowAnyMethod();
        });
});


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    //app.UseDeveloperExceptionPage();
}

if (app.Configuration.GetValue<bool>("UseDeveloperExceptionPage"))
    app.UseDeveloperExceptionPage();

else
{
    app.UseExceptionHandler("/error");
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors();
app.UseAuthorization();

// app.MapGet("/error", () => Results.Problem());

////app.MapControllers();

app.UseEndpoints(endpoint =>
{
    endpoint.MapControllers();
});


//app.MapGet("/cod/test",
//[EnableCors("AnyOrigin")]
//[ResponseCache(NoStore = true)] () =>
//Results.Text("<script>" +
//"window.alert('Your client supports JavaScript!" +
//"\\r\\n\\r\\n" +
//$"Server time (UTC): {DateTime.UtcNow.ToString("o")}" +
//"\\r\\n" +
//"Client time (UTC): ' + new Date().toISOString());" +
//"</script>" +
//"<noscript>Your client does not support JavaScript</noscript>", "text/html")
//);


app.Run();
