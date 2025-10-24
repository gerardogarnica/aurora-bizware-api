namespace Aurora.Common.Application.Behaviors;

internal static class LoggingBehavior
{
    private const string ProccessingMessage = "Processing request: {Name} {@Request}";
    private const string SuccessMessage = "Request processed successfully: {Name} {@Response}";
    private const string ErrorMessage = "Request processed with errors: {Name} {@Response}";

    internal sealed class CommandHandler<TCommand, TResponse>(
        ICommandHandler<TCommand, TResponse> innerHandler,
        ILogger<CommandHandler<TCommand, TResponse>> logger) : ICommandHandler<TCommand, TResponse>
        where TCommand : ICommand<TResponse>
    {
        public async Task<Result<TResponse>> Handle(
            TCommand command,
            CancellationToken cancellationToken)
        {
            logger.LogInformation(ProccessingMessage, typeof(TCommand).Name, command);

            Result<TResponse> result = await innerHandler.Handle(command, cancellationToken);

            if (result.IsSuccessful)
            {
                logger.LogInformation(SuccessMessage, typeof(TResponse).Name, result);
            }
            else
            {
                logger.LogError(ErrorMessage, typeof(TResponse).Name, result);
            }

            return result;
        }
    }

    internal sealed class CommandBaseHandler<TCommand>(
        ICommandHandler<TCommand> innerHandler,
        ILogger<CommandBaseHandler<TCommand>> logger) : ICommandHandler<TCommand>
        where TCommand : ICommand
    {
        public async Task<Result> Handle(
            TCommand command,
            CancellationToken cancellationToken)
        {
            logger.LogInformation(ProccessingMessage, typeof(TCommand).Name, command);

            Result result = await innerHandler.Handle(command, cancellationToken);

            if (result.IsSuccessful)
            {
                logger.LogInformation(SuccessMessage, typeof(TCommand).Name, result);
            }
            else
            {
                logger.LogError(ErrorMessage, typeof(TCommand).Name, result);
            }

            return result;
        }
    }

    internal sealed class QueryHandler<TQuery, TResponse>(
        IQueryHandler<TQuery, TResponse> innerHandler,
        ILogger<QueryHandler<TQuery, TResponse>> logger) : IQueryHandler<TQuery, TResponse>
        where TQuery : IQuery<TResponse>
    {
        public async Task<Result<TResponse>> Handle(
            TQuery query,
            CancellationToken cancellationToken)
        {
            logger.LogInformation(ProccessingMessage, typeof(TQuery).Name, query);

            Result<TResponse> result = await innerHandler.Handle(query, cancellationToken);

            if (result.IsSuccessful)
            {
                logger.LogInformation(SuccessMessage, typeof(TResponse).Name, result);
            }
            else
            {
                logger.LogError(ErrorMessage, typeof(TResponse).Name, result);
            }

            return result;
        }
    }
}