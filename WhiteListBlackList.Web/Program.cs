using WhiteListBlackList.Web.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//..

builder.Services.Configure<IPList>(builder.Configuration.GetSection("IPList"));


//..

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


//..  


//IP izinli mi diye baktýðýmýz için tüm diðer  mw lerden önce çalýþmasý gayet mantýklý
app.UseMiddleware<IPSafeMiddleware>();

//..

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
