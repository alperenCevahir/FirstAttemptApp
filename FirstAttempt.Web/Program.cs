using Autofac;
using Autofac.Extensions.DependencyInjection;
using FirstAttempt.Repository;
using FirstAttempt.Service.Mapping;
using FirstAttempt.Service.Validation;
using FirstAttempt.Web;
using FirstAttempt.Web.Services;
using FirstAttemtp.Web.Modules;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<ProductDtoValidator>());
//AutoMapper
//Linq ile mapping yapmak en h�zl�s�ym�� gerekirse ara�t�r�rs�n
builder.Services.AddAutoMapper(typeof(MapProfile));

//Db context
builder.Services.AddDbContext<AppDbContext>(x =>
{
    x.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"), option =>
    {
        //option.MigrationsAssembly("FirstAttempt.Repository")
        //Yukar�daki gibi yapmama nedenimiz tip g�venli yapmak istememiz(Dinamik)
        option.MigrationsAssembly(Assembly.GetAssembly(typeof(AppDbContext)).GetName().Name);
    });
});

//Appsettingsteki base urli okuyoruz, api ile ba�lant�yla alakal�
builder.Services.AddHttpClient<ProductApiService>(opt =>
{
    opt.BaseAddress = new Uri(builder.Configuration["BaseUrl"]);
});
builder.Services.AddHttpClient<CategoryApiService>(opt =>
{
    opt.BaseAddress = new Uri(builder.Configuration["BaseUrl"]);
});



//NotFound Filter
builder.Services.AddScoped(typeof(NotFoundFilter<>));

//Autofac
builder.Host.UseServiceProviderFactory
    (new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder => containerBuilder.RegisterModule(new RepoServiceModule()));

var app = builder.Build();

//A�a��daki ifin i�inde olmas� daha do�ru dedi
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
