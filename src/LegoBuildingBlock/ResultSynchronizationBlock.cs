using System;
using System.Threading.Tasks;

namespace LegoBuildingBlock
{
    public class ResultSynchronizationBlock<TOutput> : IBlock<Task<TOutput>, TOutput>
    {
        public Func<Task<TOutput>, TOutput> Handle
        {
            get { return task => task.Result; }
        }
    }
}