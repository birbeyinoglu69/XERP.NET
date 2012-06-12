using System;
using System.Linq;
using System.ComponentModel.Composition;

namespace SimpleMvvmToolkit
{
    /// <summary>
    /// Apply this attribute to a service agent class.
    /// Specify the service agent interface type as the contract type,
    /// as well as the agent type. For example:
    /// [ServiceAgentExport(typeof(ICustomerServiceAgent), AgentType = AgentType.Mock)]
    /// </summary>
    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ServiceAgentExportAttribute : ExportAttribute
    {
        public ServiceAgentExportAttribute(Type contractType) :
            base(contractType) { }

        public AgentType AgentType { get; set; }
    }
}
