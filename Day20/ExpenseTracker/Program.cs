using ExpenseTracker.Services;

var builder = WebApplication.CreateBuilder(args);

// ✅ Регистрируем сервисы
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<IExpenseService, ExpenseService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// ✅ Маршрут по умолчанию
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Expenses}/{action=Index}/{id?}");

app.Run();