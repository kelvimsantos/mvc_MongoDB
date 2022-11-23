using mvc_MongoDB.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using mvc_MongoDB.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<mvc_MongoDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("mvc_MongoDBContext") ?? throw new InvalidOperationException("Connection string 'mvc_MongoDBContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

//pegar as configura��es connections string no app setings
ContextMongoDB.ConnectionString = builder.Configuration.GetRequiredSection("MongoConnection:ConnectionString").Value;
ContextMongoDB.DataBaseName = builder.Configuration.GetRequiredSection("MongoConnection:Database").Value;
ContextMongoDB.IsSSL = Convert.ToBoolean(builder.Configuration.GetRequiredSection("MongoConnection:IsSSL").Value);


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
