using Authentication.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//session
builder.Services.AddSession(); // habilita serviço de sessão
builder.Services.AddHttpContextAccessor();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles(); // necessário para servir arquivos wwwroot

app.UseRouting();

app.UseSession();              // ativa a sessão na pipeline

// Middleware para redirecionar se não estiver logado
app.Use(async (context, next) =>
{
    var path = context.Request.Path;

    if (!path.StartsWithSegments("/User/Register") &&
        !path.StartsWithSegments("/User/Login"))
    {
        var isAuthenticated = context.Session.GetString("user") != null;

        if (!isAuthenticated)
        {
            context.Response.Redirect("/User/Register");
            return;
        }
    }

    await next();
});



app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
