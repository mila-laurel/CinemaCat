using MediatR;

namespace CinemaCat.Application.Handlers;

public abstract class ApplicationHandlerBase<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : ApplicationResponse, new()
{
    public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
    {
        try
        {
            return HandleInternal(request, cancellationToken);
        }
        catch (Exception e)
        {
            return Task.FromResult(new TResponse()
            {
                Exception = e,
                Error = e.Message
            });
        }
    }

    protected abstract Task<TResponse> HandleInternal(TRequest request, CancellationToken cancellationToken);
}