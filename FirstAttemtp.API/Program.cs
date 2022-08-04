using Autofac;
using Autofac.Extensions.DependencyInjection;
using FirstAttempt.Repository;
using FirstAttempt.Service.Mapping;
using FirstAttempt.Service.Validation;
using FirstAttemtp.API.Filters;
using FirstAttemtp.API.Middlewares;
using FirstAttemtp.API.Modules;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Filter bu 
builder.Services.AddControllers(options => options.Filters.Add(new ValidateFilterAttribute()))
    .AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<ProductDtoValidator>());

//Api filter�n� kapat�yoruz
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//�stersen filterlar i�in de ayr� bir module ekleyebilirsin
builder.Services.AddScoped(typeof(NotFoundFilter<>));


//AutoMapper
//Linq ile mapping yapmak en h�zl�s�ym�� gerekirse ara�t�r�rs�n
builder.Services.AddAutoMapper(typeof(MapProfile));


builder.Services.AddDbContext<AppDbContext>(x =>
{
    x.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"), option =>
    {
        //option.MigrationsAssembly("FirstAttempt.Repository")
        //Yukar�daki gibi yapmama nedenimiz tip g�venli yapmak istememiz(Dinamik)
        option.MigrationsAssembly(Assembly.GetAssembly(typeof(AppDbContext)).GetName().Name);
    });
});

//AutoFac k�sm� buas�
builder.Host.UseServiceProviderFactory
    (new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder => containerBuilder.RegisterModule(new RepoServiceModule()));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//Middleware aktif ediyorum
app.UseCustomException();

app.UseAuthorization();

app.MapControllers();

app.Run();
