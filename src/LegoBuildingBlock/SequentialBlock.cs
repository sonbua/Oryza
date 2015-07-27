using System;

namespace LegoBuildingBlock
{
    public class SequentialBlock<TInput, TMiddle, TOutput> : IBlock<TInput, TOutput>
    {
        private readonly IBlock<TInput, TMiddle> _firstBlock;
        private readonly IBlock<TMiddle, TOutput> _nextBlock;

        public SequentialBlock(IBlock<TInput, TMiddle> firstBlock, IBlock<TMiddle, TOutput> nextBlock)
        {
            _firstBlock = firstBlock;
            _nextBlock = nextBlock;
        }

        public TInput Input
        {
            get { return _firstBlock.Input; }
            set { _firstBlock.Input = value; }
        }

        public Func<TInput, TOutput> Work
        {
            get
            {
                return input =>
                       {
                           _nextBlock.Input = _firstBlock.Work(input);

                           return _nextBlock.Work(_nextBlock.Input);
                       };
            }
        }
    }
}