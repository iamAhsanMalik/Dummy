using Microsoft.AspNetCore.Mvc.Authorization;
using STM.AIU.Application;
using STM.AIU.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews(options => options.Filters.Add(new AuthorizeFilter(new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build()))).AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);

// To Enable Kendo Grid Json Serialization => .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null)
builder.Services.AddKendo();

// Add the STM.AIU.Application and STM.AIU.Infrastructure to the container. Remember that STM.AIU.Infrastructure is for services registration only. 
builder.Services.AddApplicationServices().AddInfrastructureServices(builder.Configuration);

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

app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
