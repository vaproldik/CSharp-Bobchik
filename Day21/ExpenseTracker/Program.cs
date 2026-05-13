using Microsoft.EntityFrameworkCore;
using ExpenseTracker.Data;
using ExpenseTracker.Services;

var builder = WebApplication.CreateBuilder(args);

// ── Регистрация MVC ───────────────────────────────────────────
builder.Services.AddControllersWithViews();

// ── Регистрация DbContext с подключением к SQL Server ─────────
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

// ── Регистрация сервиса через DI (внедрение зависимостей) ─────
builder.Services.AddScoped<IExpenseService, ExpenseService>();

// ── Поддержка TempData (используется для уведомлений) ─────────
builder.Services.AddSession();

var app = builder.Build();

// ── Настройка конвейера обработки запросов ────────────────────
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// ── Маршрут фильтрации по категории ──────────────────────────
app.MapControllerRoute(
    name: "expenseFilter",
    pattern: "Expenses/Filter/{category}",
    defaults: new { controller = "Expenses", action = "Filter" }
);

// ── Стандартный маршрут ───────────────────────────────────────
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Expenses}/{action=Index}/{id?}"
);

app.Run();