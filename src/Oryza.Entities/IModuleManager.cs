using System.Collections.Generic;

namespace Oryza.Entities
{
    public interface IModuleManager
    {
        ICollection<IModule> Modules { get; set; }

        void SetStatus(IModule module, ModuleStatuses newStatus);
    }
}