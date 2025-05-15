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
builder.Services.AddScoped<IShipServices<DonViVanChuyen>,ShipSvc>();
builder.Services.AddScoped<IShipDTO<ChiTietDonViVanChuyenDTO>,ShipSvc>();
builder.Services.AddScoped<IPostDVVC<DonViVanChuyenDTO>, ShipSvc > ();
builder.Services.AddScoped<IPutShip, ShipSvc>();
//
builder.Services.AddScoped<IGetByRQ<ProductDetail>, SanPhamSvc>();
builder.Services.AddScoped<IPutReview<ProductReview>, SanPhamSvc>();
builder.Services.AddScoped<IGetDTO<ProductDetail>, SanPhamSvc>();
builder.Services.AddScoped<IDeleTeDTO<SanPham>, SanPhamSvc>();
builder.Services.AddScoped<IPutSp<SanPhamDTO>, SanPhamSvc>();
//
builder.Services.AddScoped<IGetAll<BienTheSanPham>, BienTheSvc>();
builder.Services.AddScoped<IPostDTO<BienTheSPDTO>, BienTheSvc>();
builder.Services.AddScoped<IDeleTeDTO<BienTheSanPham>, BienTheSvc>();
builder.Services.AddScoped<IPutGT,BienTheSvc>();
//
builder.Services.AddScoped<SanPhamSvc>();
builder.Services.AddScoped<BienTheSvc>();
builder.Services.AddScoped<ShipSvc>();
//
builder.Services.AddScoped<ImageSvc>();
builder.Services.AddScoped<SendMail>();
//
builder.Services.AddScoped<IGetAllStore, StoreSvc>();
builder.Services.AddScoped<IPutApprove, StoreSvc>();
builder.Services.AddScoped<IRejectStore, StoreSvc>();
builder.Services.AddScoped<IGetStoreStatus, StoreSvc>();
builder.Services.AddScoped<IPutStoreStatus, StoreSvc>();
builder.Services.AddScoped<ILockStore, StoreSvc>(); 
builder.Services.AddScoped<ISendNotify, StoreSvc>();
builder.Services.AddScoped<IGetActive, StoreSvc>();


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
