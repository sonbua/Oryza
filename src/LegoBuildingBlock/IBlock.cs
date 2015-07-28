using System;

namespace LegoBuildingBlock
{
    public interface IBlock<TInput, TOutput>
    {
        Func<TInput, TOutput> Handle { get; }
    }
}