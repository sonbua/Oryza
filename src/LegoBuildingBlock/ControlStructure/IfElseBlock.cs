using System;

namespace LegoBuildingBlock.ControlStructure
{
    public class IfElseBlock<TInput, TOutput> : IBlock<TInput, TOutput>
    {
        public IfElseBlock(Func<bool> testFunc, IBlock<TInput, TOutput> trueBlock, IBlock<TInput, TOutput> falseBlock)
        {
            if (testFunc())
            {
                Handle = input => trueBlock.Handle(input);
            }
            else
            {
                Handle = input => falseBlock.Handle(input);
            }
        }

        public Func<TInput, TOutput> Handle { get; private set; }
    }
}