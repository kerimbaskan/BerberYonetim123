using BerberYonetim.Data;
using BerberYonetim.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Veritaban� ba�lant�s�n� yap�land�r
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BerberYonetimDb")));

// Oturum y�netimi (Session) yap�land�rmas�
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Oturum zaman a��m� s�resi
    options.Cookie.HttpOnly = true; // Cookie'nin yaln�zca HTTP �zerinden eri�ilebilir olmas�
    options.Cookie.IsEssential = true; // Cookie'nin zorunlu oldu�unu belirtir
});

// Authentication servisini ekle
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Admin/Giris"; // Yetkisiz eri�imde y�nlendirme
        options.LogoutPath = "/Admin/Cikis"; // ��k�� i�lemi
        options.AccessDeniedPath = "/Admin/Giris"; // Yetki reddinde y�nlendirme
    });

// MVC deste�ini ekle
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Hata y�netimi ve HTTPS y�nlendirmesi
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// Orta katman (middleware) yap�land�rmas�
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication(); // Authentication middleware
app.UseAuthorization(); // Authorization middleware
app.UseSession(); // Oturum deste�ini etkinle�tir

// Varsay�lan rota
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
