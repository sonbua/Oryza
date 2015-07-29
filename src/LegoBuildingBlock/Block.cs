using System;

namespace LegoBuildingBlock
{
    public class Block<TInput, TOutput> : IBlock<TInput, TOutput>
    {
        public Block(Func<TInput, TOutput> handle)
        {
            if (handle == null)
            {
                throw new ArgumentNullException("handle");
            }

            Handle = handle;
        }

        public Func<TInput, TOutput> Handle { get; private set; }
    }
}