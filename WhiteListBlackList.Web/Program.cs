using WhiteListBlackList.Web.Filters;
using WhiteListBlackList.Web.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//..

builder.Services.Configure<IPList>(builder.Configuration.GetSection("IPList"));

// her req geldi�inde generic olarak verilen s�n�ftan (CheckWhiteList) bir nesne �re�i al 
builder.Services.AddScoped<CheckWhiteList>();

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


//IP izinli mi diye bakt���m�z i�in t�m di�er  mw lerden �nce �al��mas� gayet mant�kl�
//app.UseMiddleware<IPSafeMiddleware>();

//..

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
