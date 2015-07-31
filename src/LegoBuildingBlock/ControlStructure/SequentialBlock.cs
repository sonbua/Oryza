using System;

namespace LegoBuildingBlock.ControlStructure
{
    internal class SequentialBlock<TInput, TMiddle, TOutput> : IBlock<TInput, TOutput>
    {
        public SequentialBlock(IBlock<TInput, TMiddle> firstBlock, IBlock<TMiddle, TOutput> nextBlock)
        {
            Handle = input =>
                     {
                         var temp = firstBlock.Handle(input);

                         return nextBlock.Handle(temp);
                     };
        }

        public Func<TInput, TOutput> Handle { get; private set; }
    }
}