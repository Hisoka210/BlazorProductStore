using ProdctManagementSystem.Components;
// Add this using statement at the top
using BlazorProductStore.BLL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
// Add Authentication and Authorization services
builder.Services.AddAuthentication("Cookies")
    .AddCookie()
    .AddGoogle(options =>
    {
        options.ClientId = builder.Configuration["Authentication:Google:ClientId"];
        options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
    });
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddScoped(provider => new ProductService(connectionString)); // Existing
// Add the new UserService registration
builder.Services.AddScoped(provider => new UserService(connectionString));
builder.Services.AddAuthorization();
builder.Services.AddCascadingAuthenticationState(); // Makes auth state available to components

// ... before app.Run() ...
var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

// Inside the builder configuration section
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddScoped(provider => new ProductService(connectionString));

builder.Services.AddHttpClient("WebAPI", client =>
{
    client.BaseAddress = new Uri("https://localhost:5050"); // Find port in API's launchSettings.json
});

builder.Services.AddScoped<CategoryApiService>();

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
