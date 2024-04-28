using System.Text;
using BookStoreApp;
using BookStoreApp.Data;
using BookStoreApp.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;


// In older version of Dot Net below code is written inside ConfigureServices() Method
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddNewtonsoftJson();

// Add configuration for BookRepo
builder.Services.AddTransient<IBookRepo, BookRepo>();
builder.Services.AddTransient<IAccountRepo, AccountRepository>();

// Add configuration for DB string connection
builder.Services.AddDbContext<BookStoreContext>(options =>{
    options.UseSqlite(builder.Configuration.GetConnectionString("BooksDBString"));
});


// Add Identity Core 
builder.Services.AddIdentity<ApplicationUser,IdentityRole>()
    .AddEntityFrameworkStores<BookStoreContext>()
    .AddDefaultTokenProviders();

// Add JWT Configuration
builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(option =>{
        option.SaveToken = true;
        option.RequireHttpsMetadata = false;
        option.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters(){
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidAudience = builder.Configuration["JWT:ValidAudience"],
            ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
        };
    });

// To enable CORS
builder.Services.AddCors(option =>
{
    option.AddDefaultPolicy(build =>
    {
        build.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

// Add AutoMapper
builder.Services.AddAutoMapper(typeof(Program));


// In older version of Dot Net below code is written inside Configure() Method
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseRouting();

// To use CORS
app.UseCors();

// Authentication and Authorization
app.UseAuthentication();
app.UseAuthorization();

//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapControllers();
//});

app.MapControllers();

// app.MapControllerRoute(
//     name: "default",
//     pattern : "{controller=Home}/{action=Index}/{id}"
// );

app.Run();

