using SmartProd.Models;
using Microsoft.EntityFrameworkCore;
using SmartProd.Data;
using SmartProd.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<SmartProdContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SmartProdContext") ?? throw new InvalidOperationException("Connection string 'SmartProdContext' not found.")));

builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient<ViaCepService>();

ContextMongodb.ConnectionString = builder.Configuration.GetSection("MongoConnection:ConnectionString").Value;
ContextMongodb.Database = builder.Configuration.GetSection("MongoConnection:Database").Value;
ContextMongodb.IsSSL = Convert.ToBoolean(builder.Configuration.GetSection("MongoConnection:IsSSL").Value);

// Add services to the container.
builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
    .AddMongoDbStores<ApplicationUser, ApplicationRole, Guid>(
    ContextMongodb.ConnectionString, ContextMongodb.Database);

var app = builder.Build();

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
