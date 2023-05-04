using Microsoft.AspNetCore.Mvc;
using MVCPractice.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// builder.Services.AddMvc();

// builder.Services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
// builder.Services.Configure<MvcOptions>(options => options.EnableEndpointRouting = false);

// builder.Services.AddSingleton<IStudentRepository, TestStudentRepository>();

var app = builder.Build();

// app.UseMvc();

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

app.MapControllerRoute(
        name: "student",
        pattern: "{controller}/{action}/{id:int?}"
    );

app.Run();
