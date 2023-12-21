using MediatR;

namespace CinemaCat.Application.Handlers;

public abstract class ApplicationHandlerBase<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : ApplicationResponse, new()
{
    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
    {
        try
        {
            return await HandleInternalAsync(request, cancellationToken);
        }
        catch (Exception e)
        {
            return new TResponse()
            {
                Exception = e,
                Error = e.Message
            };
        }
    }

    protected abstract Task<TResponse> HandleInternalAsync(TRequest request, CancellationToken cancellationToken);
}