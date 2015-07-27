using System;

namespace LegoBuildingBlock
{
    public interface IBlock<TInput, TOutput>
    {
        TInput Input { get; set; }

        Func<TInput, TOutput> Work { get; }
    }
}