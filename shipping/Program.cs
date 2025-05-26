using Microsoft.EntityFrameworkCore;
using shipping.DBContext;
using shipping.Model;
using shipping.Model.DTO;
using shipping.Services.Implement;
using shipping.Services.Interface;
using WebAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IShip, ShipSvc>();
//

builder.Services.AddScoped<ISanPham, SanPhamSvc>();
//
builder.Services.AddScoped<IBienThe,BienTheSvc>();
//
builder.Services.AddScoped<SanPhamSvc>();
builder.Services.AddScoped<BienTheSvc>();
builder.Services.AddScoped<ShipSvc>();
builder.Services.AddScoped<StoreSvc>();
builder.Services.AddScoped<ImageSvc>();
builder.Services.AddScoped<SendMail>();
//
builder.Services.AddScoped<IStore, StoreSvc>();
//
builder.Services.AddScoped<IImage,ImageSvc>();

var connectionString = builder.Configuration.GetConnectionString("Mydbtest");
builder.Services.AddDbContext<Context>(option=>option.UseSqlServer(connectionString));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
