using Microsoft.EntityFrameworkCore;
using Project.Sanha.Web.Data;
using Project.Sanha.Web.Repositories;
using Project.Sanha.Web.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<SanhaDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("AFSConn")));

builder.Services.AddControllers().AddJsonOptions(opts => opts.JsonSerializerOptions.PropertyNamingPolicy = null);

builder.Services.AddScoped<IInformationService, InformationService>();
builder.Services.AddScoped<IInformationRepo, InformationRepo>();

builder.Services.AddScoped<IServiceUnitSave, ServiceUnitSave>();
builder.Services.AddScoped<ICreateTransactionRepo, CreateTransactionRepo>();

builder.Services.AddScoped<ISearchUnitService, SearchUnitService>();
builder.Services.AddScoped<ISearchUnitRepo, SearchUnitRepo>();


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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
