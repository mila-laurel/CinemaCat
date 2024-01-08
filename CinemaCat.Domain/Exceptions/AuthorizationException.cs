namespace CinemaCat.Domain.Exceptions;

[Serializable]
public class AuthorizationException : CinemaCatException
{
	public AuthorizationException() { }
	public AuthorizationException(string message) : base(message) { }
	public AuthorizationException(string message, Exception inner) : base(message, inner) { }
}
