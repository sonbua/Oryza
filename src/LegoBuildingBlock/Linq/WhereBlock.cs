using System;
using System.Collections.Generic;
using System.Linq;

namespace LegoBuildingBlock.Linq
{
    public class WhereBlock<TInput> : IBlock<IEnumerable<TInput>, IEnumerable<TInput>>
    {
        public WhereBlock(Func<TInput, bool> predicate)
        {
            Handle = inputs => inputs.Where(predicate);
        }

        public WhereBlock(IBlock<TInput, bool> predicateBlock)
        {
            Handle = inputs => inputs.Where(predicateBlock.Handle);
        }

        public Func<IEnumerable<TInput>, IEnumerable<TInput>> Handle { get; private set; }
    }
}