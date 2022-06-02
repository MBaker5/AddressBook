using Microsoft.EntityFrameworkCore;
using AddressBook.Models;
using AddressBook.Data;
using Serilog.AspNetCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Host.UseSerilog((ctx, lc) => lc.WriteTo.Console().WriteTo.Seq("https://localhost:7234")); 


builder.Services.AddControllers();
builder.Services.AddDbContext<AdressDbContext>(options => options.UseInMemoryDatabase("AddressBook"));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMvc(opt => opt.SuppressAsyncSuffixInActionNames = false);
builder.Services.AddCors(opt => opt.AddPolicy(name: "mySpecialPolicy", policy => { policy.WithOrigins("https://localhost:7234"); }));
var app = builder.Build();


await CreateSeedDb(app);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("mySpecialPolicy");
app.UseAuthorization();

app.MapControllers();

app.Run();

async Task CreateSeedDb(IHost host)
{
    await using var scope = host.Services.CreateAsyncScope();
    var services = scope.ServiceProvider;
    try 
    {
        var context = services.GetRequiredService<AdressDbContext>();
        await Seeder.Seed(context);
    }
    catch(Exception e)
    {
        Log.Error("Ups! Creating Seed go wrong!");
    }
}

