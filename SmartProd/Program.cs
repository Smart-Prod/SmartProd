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

body {
  margin: 0;
  font-family: Arial, sans-serif;
  background-color: #d3d3d3;
}

.container {
  max-width: 1000px;
  margin: 0 auto;
  padding: 20px;
}

header {
  background-color: #fff;
  padding: 20px 0;
}

.header-content {
  display: flex;
  justify-content: space-between;
  align-items: center;
}

.logo {
  font-size: 24px;
}

.logo span {
  font-weight: bold;
  color: #555;
}

.buttons button {
  margin-left: 10px;
  padding: 8px 16px;
  border: none;
  background-color: #333;
  color: #fff;
  cursor: pointer;
}

.hero {
  background-color: #333;
  color: #fff;
  padding: 40px 0;
}

.hero-content {
  display: flex;
  justify-content: space-between;
  align-items: center;
}

.features {
  background-color: #ff944d;
  padding: 40px 0;
  color: #000;
}

.features h3 {
  text-align: center;
  margin-bottom: 30px;
}

.cards {
  display: flex;
  gap: 20px;
  flex-wrap: wrap;
  justify-content: space-around;
}

.card {
  background-color: #fff;
  padding: 20px;
  border-radius: 8px;
  width: 200px;
  text-align: center;
}

.why {
  background-color: #333;
  color: #fff;
  padding: 40px 0;
}

.why h3 {
  text-align: center;
  margin-bottom: 20px;
}

.contact {
  background-color: #ff944d;
  padding: 40px 0;
}

.contact h3 {
  text-align: center;
  margin-bottom: 20px;
}

form {
  display: flex;
  flex-direction: column;
  gap: 10px;
  max-width: 400px;
  margin: 0 auto;
}

input, textarea {
  padding: 10px;
  border: none;
  border-radius: 4px;
}

textarea {
  height: 100px;
}

.form-buttons {
  display: flex;
  justify-content: space-between;
}

.form-buttons button {
  padding: 10px 20px;
  border: none;
  background-color: #333;
  color: white;
  cursor: pointer;
}