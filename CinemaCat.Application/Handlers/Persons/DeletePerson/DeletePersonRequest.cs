using MediatR;

namespace CinemaCat.Application.Handlers.Persons.DeletePerson;

public class DeletePersonRequest : IRequest<DeletePersonResponse>
{
    public Guid Id { get; set; }
}
