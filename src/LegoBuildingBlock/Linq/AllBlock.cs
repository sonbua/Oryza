using System;
using System.Collections.Generic;
using System.Linq;

namespace LegoBuildingBlock.Linq
{
    public class AllBlock<TInput> : IBlock<IEnumerable<TInput>, bool>
    {
        public AllBlock(Func<TInput, bool> predicate)
        {
            Handle = inputs => inputs.All(predicate);
        }

        public AllBlock(IBlock<TInput, bool> predicateBlock)
        {
            Handle = inputs => inputs.All(predicateBlock.Handle);
        }

        public Func<IEnumerable<TInput>, bool> Handle { get; private set; }
    }
}