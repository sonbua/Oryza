using System;
using System.Collections.Generic;
using System.Linq;

namespace LegoBuildingBlock.Linq
{
    public class SelectBlock<TInput, TOutput> : IBlock<IEnumerable<TInput>, IEnumerable<TOutput>>
    {
        public SelectBlock(Func<TInput, TOutput> selector)
        {
            Handle = inputs => inputs.Select(selector);
        }

        public SelectBlock(IBlock<TInput, TOutput> selectorBlock)
        {
            Handle = inputs => inputs.Select(selectorBlock.Handle);
        }

        public Func<IEnumerable<TInput>, IEnumerable<TOutput>> Handle { get; private set; }
    }
}