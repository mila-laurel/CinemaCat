using MediatR;

namespace CinemaCat.Api.Handlers.Persons.DeletePerson;

public class DeletePersonRequest : IRequest<DeletePersonResponse>
{
    public Guid Id { get; set; }
}
