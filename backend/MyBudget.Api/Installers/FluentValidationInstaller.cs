using FluentValidation;
using FluentValidation.Internal;
using MyBudget.Application.Budgets.CreateBudget;
using MyBudget.Infrastructure.Abstractions.Installer;
using System.Globalization;
using System.Linq.Expressions;
using System.Reflection;

namespace MyBudget.Api.Installers;

public sealed class FluentValidationInstaller : IInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration, IHostEnvironment hostingEnvironment)
    {
        services.AddValidatorsFromAssemblyContaining<CreateBudgetCommandValidator>();
        ValidatorOptions.Global.PropertyNameResolver = CamelCasePropertyNameResolver.ResolvePropertyName;
    }
}


public class CamelCasePropertyNameResolver
{

    public static string ResolvePropertyName(Type type, MemberInfo memberInfo, LambdaExpression expression)
    {
        return ToCamelCase(DefaultPropertyNameResolver(type, memberInfo, expression));
    }

    private static string DefaultPropertyNameResolver(Type type, MemberInfo memberInfo, LambdaExpression expression)
    {
        if (expression != null)
        {
            var chain = PropertyChain.FromExpression(expression);
            if (chain.Count > 0) return chain.ToString();
        }

        if (memberInfo != null)
        {
            return memberInfo.Name;
        }

        return null!;
    }

    private static string ToCamelCase(string s)
    {
        if (string.IsNullOrEmpty(s) || !char.IsUpper(s[0]))
        {
            return s;
        }

        var chars = s.ToCharArray();

        for (var i = 0; i < chars.Length; i++)
        {
            if (i == 1 && !char.IsUpper(chars[i]))
            {
                break;
            }

            var hasNext = (i + 1 < chars.Length);
            if (i > 0 && hasNext && !char.IsUpper(chars[i + 1]))
            {
                break;
            }

            chars[i] = char.ToLower(chars[i], CultureInfo.InvariantCulture);
        }

        return new string(chars);
    }
}