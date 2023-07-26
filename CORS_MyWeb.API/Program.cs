using Microsoft.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

//.. tüm domainlerden gelen tüm istekleri kabul et

builder.Services.AddCors(opt =>
{
    #region 1 - tüm domainlerden gelen istekleri kabul et
    //opt.AddDefaultPolicy(x =>
    //{
    // Tüm istekleri kabul et
    //    x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    //}); 
    #endregion

    #region 2 - Belli domaninlerden gelen istekleri kabul et

    opt.AddPolicy("AllowedSites", x =>
    {
        x.WithOrigins("https://localhost:7278", "https://www.mysite.com").AllowAnyHeader().AllowAnyMethod();
    });

    // belli headera göre
    opt.AddPolicy("AllowedSites2", x =>
    {
        x.WithOrigins("https://www.mysite2.com").WithHeaders(HeaderNames.ContentType, "x-custom-header").AllowAnyMethod();
    });

    // subdomainleri kabul et :  hhttps://api.example.com, https://mobile.example.com etc
    opt.AddPolicy("AllowedSites3", x =>
    {
        x.WithOrigins("https://*.example.com").SetIsOriginAllowedToAllowWildcardSubdomains().AllowAnyHeader().AllowAnyMethod();
    });



    #endregion
});

//..

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//..

#region 1 -  tüm domainlerden gelen istekleri kabul et
//app.UseCors(); 
#endregion

#region  2 - Belli domaninlerden gelen istekleri kabul et

app.UseCors("AllowedSites");
app.UseCors("AllowedSites2");
app.UseCors("AllowedSites3");

#endregion
//..

app.UseAuthorization();

app.MapControllers();

app.Run();
