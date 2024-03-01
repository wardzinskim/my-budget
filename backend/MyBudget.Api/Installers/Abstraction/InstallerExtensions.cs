using System.Reflection;

namespace MyBudget.Api.Installers.Abstraction;

public static class InstallerExtensions
{
    public static WebApplicationBuilder Install(
        this WebApplicationBuilder builder,
        Assembly? assembly
    )
    {
        if (assembly is null) return builder;

        var installers = assembly.ExportedTypes
            .Where(t => typeof(IInstaller).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
            .Select(Activator.CreateInstance)
            .Cast<IInstaller>()
            .ToList();


        installers.ForEach(installer =>
            installer.Install(builder.Services, builder.Configuration, builder.Environment));


        return builder;
    }
}