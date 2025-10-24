namespace Aurora.Common.Application.Validations;

public static class RuleBuilderOptionsExtensions
{
    public static IRuleBuilderOptions<T, TProperty> WithBaseError<T, TProperty>(
        this IRuleBuilderOptions<T, TProperty> rule,
        Error error) => rule.WithErrorCode(error.Code).WithMessage(error.Message);
}