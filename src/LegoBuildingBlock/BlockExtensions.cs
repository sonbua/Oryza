using System.Threading.Tasks;

namespace LegoBuildingBlock
{
    public static class BlockExtensions
    {
        public static IBlock<TInput, TOutput> ContinuesWith<TInput, TMiddle, TOutput>(this IBlock<TInput, TMiddle> firstBlock, IBlock<TMiddle, TOutput> nextBlock)
        {
            return new SequentialBlock<TInput, TMiddle, TOutput>(firstBlock, nextBlock);
        }

        public static IBlock<TInput, Task<TOutput>> AsCpuBoundTaskBlock<TInput, TOutput>(this IBlock<TInput, TOutput> block)
        {
            return new CpuBoundTaskBlock<TInput, TOutput>(block);
        }
    }
}