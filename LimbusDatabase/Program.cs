using LimbusDatabase.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<LimbusDatabaseContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("LimbusDatabase"));
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Personnages}/{action=Index}/{id?}"
);
app.MapRazorPages();

app.Run();
