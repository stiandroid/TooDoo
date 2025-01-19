using TooDoo.Services;

namespace TooDoo.AppConfig;

public static class ServiceRegistration
{
    public static void RegisterAppServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        builder.Services.AddScoped<ITodoService, TodoService>();
    }
}
