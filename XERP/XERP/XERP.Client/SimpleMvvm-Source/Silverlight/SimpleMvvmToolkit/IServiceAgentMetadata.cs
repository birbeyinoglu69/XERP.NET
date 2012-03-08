using System;
using System.Linq;

namespace SimpleMvvmToolkit
{
    /// <summary>
    /// Metadata indicating the kind of service agent used.
    /// </summary>
    public interface IServiceAgentMetadata
    {
        AgentType AgentType { get; }
    }

    /// <summary>
    /// Service agent type.
    /// </summary>
    public enum AgentType
    {
        Unspecified,
        Real,
        Mock
    }
}
