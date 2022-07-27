using FirstAttempt.Core.Repositories;
using FirstAttempt.Core.Services;
using FirstAttempt.Core.UnitOfWorks;
using FirstAttempt.Repository;
using FirstAttempt.Repository.Repositories;
using FirstAttempt.Repository.UnitOfWork;
using FirstAttempt.Service.Mapping;
using FirstAttempt.Service.Services;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
 
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//Scoplar
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped(typeof(IService<>), typeof(Service<>));

//AutoMapper
//Linq ile mapping yapmak en hýzlýsýymýþ gerekirse araþtýrýrsýn
builder.Services.AddAutoMapper(typeof(MapProfile));


builder.Services.AddDbContext<AppDbContext>(x =>
{
    x.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"), option =>
    {
        //option.MigrationsAssembly("FirstAttempt.Repository")
        //Yukarýdaki gibi yapmama nedenimiz tip güvenli yapmak istememiz(Dinamik)
        option.MigrationsAssembly(Assembly.GetAssembly(typeof(AppDbContext)).GetName().Name);
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
