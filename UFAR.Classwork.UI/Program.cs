using Microsoft.EntityFrameworkCore;
using UFAR.Classwork.Data.DAO;
using UFAR.Classwork.UI.Components;
using UFAR.Classwork.UI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();


builder.Services.AddDbContext<ApplicationDBContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlOptions => sqlOptions.EnableRetryOnFailure(
            maxRetryCount: 5, // Adjust the number of retry attempts as needed
            maxRetryDelay: TimeSpan.FromSeconds(10), // Maximum delay between retries
            errorNumbersToAdd: null // Use default transient errors
        )
    )
);
builder.Services.AddScoped<AuthenticationService>();
builder.Services.AddScoped<UserSessionService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
