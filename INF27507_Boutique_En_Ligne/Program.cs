using INF27507_Boutique_En_Ligne.Settings;
using INF27507_Boutique_En_Ligne.Services;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Stripe;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMvc();
builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSingleton<IDatabaseAdapter>(ServicesFactory.getInstance().GetDatabaseService());
builder.Services.AddSingleton<IAuthentificationAdapter>(ServicesFactory.getInstance().GetAuthService());

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages()
    .AddRazorRuntimeCompilation();

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.Name = "BoutiqueEnLigne.Session";
    options.Cookie.IsEssential = true;
});

builder.Services.Configure<StripeSettings>(options =>
{
    options.PublicKey = builder.Configuration["Stripe:PublicKey"];
    options.SecretKey = builder.Configuration["Stripe:SecretKey"];
});

StripeConfiguration.ApiKey = builder.Configuration["Stripe:SecretKey"];

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
