using System;

namespace LegoBuildingBlock
{
    internal class SequentialBlock<TInput, TMiddle, TOutput> : IBlock<TInput, TOutput>
    {
        private readonly IBlock<TInput, TMiddle> _firstBlock;
        private readonly IBlock<TMiddle, TOutput> _nextBlock;

        public SequentialBlock(IBlock<TInput, TMiddle> firstBlock, IBlock<TMiddle, TOutput> nextBlock)
        {
            _firstBlock = firstBlock;
            _nextBlock = nextBlock;
        }

        public Func<TInput, TOutput> Handle
        {
            get
            {
                return input =>
                       {
                           var temp = _firstBlock.Handle(input);

                           return _nextBlock.Handle(temp);
                       };
            }
        }
    }
}