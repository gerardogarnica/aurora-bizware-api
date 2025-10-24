using Aurora.Common.Domain.Results;

namespace Aurora.Common.Endpoints;

public static class ResultExtensions
{
    public static TOut Match<TOut>(
        this Result result,
        Func<TOut> onSuccess,
        Func<Result, TOut> onFail)
    {
        return result.IsSuccessful
            ? onSuccess()
            : onFail(result);
    }

    public static TOut Match<TIn, TOut>(
        this Result<TIn> result,
        Func<TIn, TOut> onSuccess,
        Func<Result<TIn>, TOut> onFail)
    {
        return result.IsSuccessful
            ? onSuccess(result.Value)
            : onFail(result);
    }
}