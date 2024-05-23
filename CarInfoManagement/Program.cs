using CarInfoBFF.Identity;
using CarInfoBFF.Services;
using CarInfoManagement.Extensions;
using CarInfoManagement.Middleware;
using CarInfoManagement.Models.MappingModel;
using CarInfoManagement.Services;
using CarInfoManagement.Services.CarInfoDetails;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();
builder.Services.AddScoped<IBaseService, BaseService>();
builder.Services.AddScoped<ICarInfoDetailsServices, CarInforDetailsServices>();
builder.Services.AddScoped<IIdentityService, IdentityServices>();
builder.Services.AddScoped<IFileServices, FileServices>();
builder.Services.AddScoped<MappingData>();
builder.Services.AddSession();
builder.Services.AddRazorPages()
    .AddRazorRuntimeCompilation();
builder.AddSDApiUri();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseExceptionHandlerMiddleware();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=UserAuthentication}/{action=Login}/{id?}");

app.Run();
