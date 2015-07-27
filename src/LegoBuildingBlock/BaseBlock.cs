using System;

namespace LegoBuildingBlock
{
    public abstract class BaseBlock<TInput, TOutput> : IBlock<TInput, TOutput>
    {
        protected BaseBlock()
        {
            Input = default(TInput);
        }

        protected BaseBlock(TInput input)
        {
            Input = input;
        }

        public TInput Input { get; set; }

        public abstract Func<TInput, TOutput> Work { get; }
    }
}