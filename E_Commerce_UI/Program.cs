using Blazored.LocalStorage;
using E_Commerce_UI;
using E_Commerce_UI.Service;
using E_Commerce_UI.Service.IService;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.Configuration.GetValue<string>("BaseAPIUrl")) });

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IAuthenticationSercice, AuthenticationService>();
builder.Services.AddScoped<IDiscountService, DiscountService>();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, CustomStateProvider>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddBlazoredLocalStorage();
await builder.Build().RunAsync();
