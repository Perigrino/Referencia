using RefsGuy.Application.Interfaces;
using RefsGuy.Application.Repository;
using RefsGuy.Application.Services;

namespace RefsGuy.API;

public static class ApplicationCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection service)
    {
        service.AddScoped<IUserRepository, UserRepository>();
        service.AddScoped<IReferralCodeRepository, ReferralCodeRepository>();
        service.AddScoped<IReferralCodeService, ReferralCodeService>();

        return service;
    }

}