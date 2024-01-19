using HomeTaskkMVC4;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
builder.Services.Register(config);

var app = builder.Build();
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=DashBoard}/{action=Index}/{id?}"
          );
app.MapControllerRoute(
    "default",
    "{controller=home}/{action=index}/{id?}"
    );
app.UseStaticFiles();

app.Run();
