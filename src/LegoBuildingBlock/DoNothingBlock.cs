using System;

namespace LegoBuildingBlock
{
    public class DoNothingBlock<TInput> : IBlock<TInput, Nothing>
    {
        public TInput Input { get; set; }

        public Func<TInput, Nothing> Work
        {
            get { return input => new Nothing(); }
        }
    }
}