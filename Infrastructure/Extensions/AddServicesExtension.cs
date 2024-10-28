using Infrastructure.Services.AddressServices;
using Infrastructure.Services.AppointmentServices;
using Infrastructure.Services.BreakServices;
using Infrastructure.Services.CategoryServices;
using Infrastructure.Services.CityServices;
using Infrastructure.Services.ClosedDayServices;
using Infrastructure.Services.CompanyServices;
using Infrastructure.Services.CountryServices;
using Infrastructure.Services.PaymentServices;
using Infrastructure.Services.ReviewServices;
using Infrastructure.Services.ServiceServices;
using Infrastructure.Services.SpecialBreakServices;
using Infrastructure.Services.UserServices;
using Infrastructure.Services.WorkingHoursServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions;

public static class AddServicesExtension
{
    public static void AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<DataContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("Default"))
        );
        services.AddScoped<IAppointmentService, AppointmentService>();
        services.AddScoped<IAddressService, AddressService>();
        services.AddScoped<IBreakService, BreakService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<ICityService, CityService>();
        services.AddScoped<IClosedDayService, ClosedDayService>();
        services.AddScoped<ICompanyService, CompanyService>();
        services.AddScoped<ICountryService, CountryService>();
        services.AddScoped<IPaymentService, PaymentService>();
        services.AddScoped<IReviewService, ReviewService>();
        services.AddScoped<IServiceService, ServiceService>();
        services.AddScoped<ISpecialBreakService, SpecialBreakService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IWorkingHoursService, WorkingHoursService>();
    }
}