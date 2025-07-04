﻿using CompanyWebsite.Application;

namespace CompanyWebsite.Web;

public static class DependencyInjection
{
    public static IServiceCollection AddProgramDependencies(this IServiceCollection services) => 
        services
            .AddWebDependencies()
            .AddApplication();
    
    private static IServiceCollection AddWebDependencies(this IServiceCollection services)
    { 
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        
        return services;
    }
}