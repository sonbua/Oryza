using System;
using System.Threading.Tasks;

namespace LegoBuildingBlock
{
    internal class CpuBoundTaskBlock<TInput, TOutput> : IBlock<TInput, Task<TOutput>>
    {
        public CpuBoundTaskBlock(IBlock<TInput, TOutput> block)
        {
            Handle = async input => { return await Task.Run(() => block.Handle(input)); };
        }

        public Func<TInput, Task<TOutput>> Handle { get; private set; }
    }
}