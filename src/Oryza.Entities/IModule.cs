namespace Oryza.Entities
{
    /// <summary>
    /// Defines a runnable module that is plugged into this crawler system.
    /// </summary>
    public interface IModule
    {
        ModuleStatuses Status { get; set; }
    }
}