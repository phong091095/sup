using Microsoft.EntityFrameworkCore;
using shipping.DBContext;
using shipping.Model;
using shipping.Services.Implement;
using shipping.Services.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IShipServices<DonViVanChuyen>,ShipSvc>();
builder.Services.AddScoped<IShipDTO<ChiTietDonViVanChuyenDTO>,ShipSvc>();
builder.Services.AddScoped<IPostDTO<DonViVanChuyen>, ShipSvc > ();
builder.Services.AddScoped<IPostDTO<ChiTietDVVanChuyen>, ShipDetail>();
builder.Services.AddScoped<IPutData<ChiTietDVVanChuyen>,ShipSvc > ();
builder.Services.AddScoped<IGetDTO<ProductDetail>,SanPhamSvc>();
builder.Services.AddScoped<IGetByRQ<ProductDetail>, SanPhamSvc>();
builder.Services.AddScoped<IPutByID<BienTheSanPham>, SanPhamSvc>();
builder.Services.AddScoped<IPutData<ProductDetail>, SanPhamSvc>();
builder.Services.AddScoped<IDeleTeDTO<SanPham>, SanPhamSvc>();

builder.Services.AddScoped<IGetDTO<BienTheSanPham>, BienTheSvc>();
builder.Services.AddScoped<IPostDTO<BienTheSanPham>, BienTheSvc>();
builder.Services.AddScoped<IDeleTeDTO<BienTheSanPham>, BienTheSvc>();

var connectionString = builder.Configuration.GetConnectionString("Mydb");
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
