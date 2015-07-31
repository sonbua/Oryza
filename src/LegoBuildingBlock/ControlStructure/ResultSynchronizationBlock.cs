using System;
using System.Threading.Tasks;

namespace LegoBuildingBlock.ControlStructure
{
    public class ResultSynchronizationBlock<TOutput> : IBlock<Task<TOutput>, TOutput>
    {
        public Func<Task<TOutput>, TOutput> Handle
        {
            get { return task => task.Result; }
        }
    }
}