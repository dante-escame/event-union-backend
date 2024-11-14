using System.Diagnostics.CodeAnalysis;
using CSharpFunctionalExtensions;
using EventUnion.Domain.Common.Errors;
using FluentValidation;

namespace EventUnion.CommonResources;

// ReSharper disable UnusedMember.Global
[ExcludeFromCodeCoverage]
public static class CustomValidators
{
    private const string PropertyNamePlaceholder = "{PropertyName}";
    
    public static IRuleBuilderOptions<T, TProperty> WithError<T, TProperty>(this IRuleBuilderOptions<T, TProperty> rule, 
        Error error)
    {
        return rule.WithMessage(error.Serialize());
    }
    
    public static IRuleBuilderOptions<T, TElement> MustBeValueObject<T, TElement, TValueObject>(
        this IRuleBuilder<T, TElement> ruleBuilder,
        Func<TElement, Result<TValueObject, Error>> factoryMethod)
        where TValueObject : ValueObject
    {
        return (IRuleBuilderOptions<T, TElement>)ruleBuilder.Custom((value, context) =>
        {
            Result<TValueObject, Error> result = factoryMethod(value);

            if (result.IsFailure)
            {
                context.AddFailure(result.Error.Serialize());
            }
        });
    }
    
    public static IRuleBuilderOptions<T, TElement> MustBeValidForValueObject<T, TElement>(
        this IRuleBuilder<T, TElement> ruleBuilder,
        Func<TElement, UnitResult<Error>> factoryMethod)
    {
        return (IRuleBuilderOptions<T, TElement>)ruleBuilder.Custom((value, context) =>
        {
            UnitResult<Error> result = factoryMethod(value);

            if (result.IsFailure)
            {
                context.AddFailure(result.Error.Serialize());
            }
        });
    }
    
    public static IRuleBuilderOptions<T, TProperty> NotEmptyWithError<T, TProperty>(
        this IRuleBuilder<T, TProperty> ruleBuilder)
    {
        return ruleBuilder.NotEmpty()
            .WithError(CommonError.ValueIsEmpty(PropertyNamePlaceholder));
    }

    public static IRuleBuilderOptions<T, TProperty> NotNullWithError<T, TProperty>(
        this IRuleBuilder<T, TProperty> ruleBuilder)
    {
        return ruleBuilder.NotNull()
            .WithError(CommonError.ValueIsEmpty(PropertyNamePlaceholder));
    }
    
    public static IRuleBuilderOptions<T, TProperty> MustBeNullWithErrorWhen<T, TProperty>(
        this IRuleBuilder<T, TProperty> ruleBuilder, string when)
    {
        return ruleBuilder.Null()
            .WithError(CommonError.ValueIsNotNull(PropertyNamePlaceholder, when));
    }
    
    public static IRuleBuilderOptions<T, string> MinimumLengthWithError<T>(
        this IRuleBuilder<T, string> ruleBuilder, int minLength)
    {
        return ruleBuilder.MinimumLength(minLength)
            .WithMessage(CommonError.ValueIsTooShort(PropertyNamePlaceholder, minLength).Serialize());
    }
    
    public static IRuleBuilderOptions<T, decimal> GreaterThanOrEqualToWithError<T>(
        this IRuleBuilder<T, decimal> ruleBuilder, decimal minValue)
    {
        return ruleBuilder.GreaterThanOrEqualTo(minValue)
            .WithMessage(CommonError.ValueIsBelow(PropertyNamePlaceholder, "{ComparisonValue}").Serialize());
    }
    
    public static IRuleBuilderOptions<T, int> GreaterThanOrEqualToWithError<T>(
        this IRuleBuilder<T, int> ruleBuilder, int minValue)
    {
        return ruleBuilder.GreaterThanOrEqualTo(minValue)
            .WithMessage(CommonError.ValueIsBelow(PropertyNamePlaceholder, "{ComparisonValue}").Serialize());
    }
    
    public static void UniqueBy<T, TElement, TKey>(this IRuleBuilder<T, IEnumerable<TElement>> ruleBuilder,
        Func<TElement, TKey> keyAcessor, string listName,
        string uniqueKeyName)
    {
        ruleBuilder.Custom((list, context) =>
        {
            var keys = list.Select(keyAcessor).ToList();

            keys = keys.Where(x => x is not null).ToList();

            if (keys.Distinct().Count() != keys.Count())
            {
                context.AddFailure(CommonError.NotUnique(listName, uniqueKeyName).Serialize());
            }
        });
    }
}