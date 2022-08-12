using Autofac;
using Autofac.Extensions.DependencyInjection;
using FirstAttempt.Repository;
using FirstAttempt.Service.Mapping;
using FirstAttempt.Service.Validation;
using FirstAttempt.Web;
using FirstAttemtp.Web.Modules;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<ProductDtoValidator>());
//AutoMapper
//Linq ile mapping yapmak en hýzlýsýymýþ gerekirse araþtýrýrsýn
builder.Services.AddAutoMapper(typeof(MapProfile));

//Db context
builder.Services.AddDbContext<AppDbContext>(x =>
{
    x.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"), option =>
    {
        //option.MigrationsAssembly("FirstAttempt.Repository")
        //Yukarýdaki gibi yapmama nedenimiz tip güvenli yapmak istememiz(Dinamik)
        option.MigrationsAssembly(Assembly.GetAssembly(typeof(AppDbContext)).GetName().Name);
    });
});
//NotFound Filter
builder.Services.AddScoped(typeof(NotFoundFilter<>));

//Autofac
builder.Host.UseServiceProviderFactory
    (new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder => containerBuilder.RegisterModule(new RepoServiceModule()));

var app = builder.Build();

//Aþaðýdaki ifin içinde olmasý daha doðru dedi
app.UseExceptionHandler("/Home/Error");

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
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
