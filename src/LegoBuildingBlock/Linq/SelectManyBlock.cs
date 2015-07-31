using System;
using System.Collections.Generic;
using System.Linq;

namespace LegoBuildingBlock.Linq
{
    public class SelectManyBlock<TInput, TOutput> : IBlock<IEnumerable<TInput>, IEnumerable<TOutput>>
    {
        public SelectManyBlock(IBlock<TInput, IEnumerable<TOutput>> innerBlock)
        {
            Handle = inputs => { return inputs.SelectMany(input => innerBlock.Handle(input)); };
        }

        public Func<IEnumerable<TInput>, IEnumerable<TOutput>> Handle { get; private set; }
    }
}