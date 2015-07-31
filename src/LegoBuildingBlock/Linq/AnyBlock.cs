using System;
using System.Collections.Generic;
using System.Linq;

namespace LegoBuildingBlock.Linq
{
    public class AnyBlock<TInput> : IBlock<IEnumerable<TInput>, bool>
    {
        public AnyBlock(Func<TInput, bool> predicate)
        {
            Handle = inputs => inputs.Any(predicate);
        }

        public AnyBlock(IBlock<TInput, bool> predicateBlock)
        {
            Handle = inputs => inputs.Any(predicateBlock.Handle);
        }

        public Func<IEnumerable<TInput>, bool> Handle { get; private set; }
    }
}