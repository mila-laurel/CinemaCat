namespace CinemaCat.Domain.Exceptions;

[Serializable]
public class ConfigurationException : CinemaCatException
{
    public ConfigurationException() { }
    public ConfigurationException(string message) : base(message) { }
    public ConfigurationException(string message, Exception inner) : base(message, inner) { }
}
