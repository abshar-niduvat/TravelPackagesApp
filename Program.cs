var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddControllersWithViews();
builder.Services.AddSwaggerGen(); 

var app = builder.Build();

// Use Swagger only in the Development environment
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(); 
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

//builder.Services.AddSwaggerGen(c =>
//{
//    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
//    {
//        Title = "Travel Packages API",
//        Version = "v1"
//    });
//});

//app.UseSwaggerUI(c =>
//{
//    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Travel Packages API v1");
//    c.RoutePrefix = string.Empty; // Optional: Makes Swagger available at the root URL
//});

app.MapControllers(); 
app.Run();