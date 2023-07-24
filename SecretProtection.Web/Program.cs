using System.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//..

var connectionString = builder.Configuration["ConnectionStrings:SqlCon"]; // = builder.Configuration.GetSection("ConnectionStrings:SqlCon");

var sqlBuilder = new SqlConnectionStringBuilder(connectionString);
sqlBuilder.Password = builder.Configuration["Passwords:SqlPass"];
string conStr = sqlBuilder.ConnectionString;


//..

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
