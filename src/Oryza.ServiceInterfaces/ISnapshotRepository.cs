using Oryza.Entities;

namespace Oryza.ServiceInterfaces
{
    public interface ISnapshotRepository
    {
        void Store(Snapshot snapshot);
    }
}