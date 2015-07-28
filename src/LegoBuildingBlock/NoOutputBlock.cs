using System;

namespace LegoBuildingBlock
{
    public class NoOutputBlock<TInput> : IBlock<TInput, Nothing>
    {
        public Func<TInput, Nothing> Handle
        {
            get { return input => new Nothing(); }
        }
    }
}