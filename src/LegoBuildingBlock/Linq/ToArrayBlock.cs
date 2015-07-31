using System;
using System.Collections.Generic;
using System.Linq;

namespace LegoBuildingBlock.Linq
{
    public class ToArrayBlock<TInput> : IBlock<IEnumerable<TInput>, TInput[]>
    {
        public ToArrayBlock()
        {
            Handle = inputs => inputs.ToArray();
        }

        public Func<IEnumerable<TInput>, TInput[]> Handle { get; private set; }
    }
}