using System.Threading.Tasks;

namespace Loonfactory.Translate;

/// <summary>
/// Used to determine if a handler wants to participate in request processing.
/// </summary>
public interface ITranslateRequestHandler : ITranslateHandler
{
    /// <summary>
    /// Gets a value that determines if the request should stop being processed.
    /// <para>
    /// This feature is supported by the Translate middleware
    /// which does not invoke any subsequent <see cref="ITranslateHandler"/> or middleware configured in the request pipeline
    /// if the handler returns <see langword="true" />.
    /// </para>
    /// </summary>
    /// <returns><see langword="true" /> if request processing should stop.</returns>
    Task<bool> HandleRequestAsync();
}
