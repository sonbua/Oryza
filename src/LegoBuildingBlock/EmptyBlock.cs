using System;

namespace LegoBuildingBlock
{
    public class EmptyBlock<TInput> : IBlock<TInput, Nothing>
    {
        public Func<TInput, Nothing> Handle
        {
            get { return input => new Nothing(); }
        }
    }
}