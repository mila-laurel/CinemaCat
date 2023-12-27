namespace CinemaCat.Domain.Exceptions;

[Serializable]
public abstract class CinemaCatException : Exception
{
	public CinemaCatException() { }
	public CinemaCatException(string message) : base(message) { }
	public CinemaCatException(string message, Exception inner) : base(message, inner) { }
}
