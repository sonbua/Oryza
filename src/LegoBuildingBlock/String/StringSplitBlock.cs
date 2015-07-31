using System;
using System.Collections.Generic;

namespace LegoBuildingBlock.String
{
    public class StringSplitBlock : IBlock<string, IEnumerable<string>>
    {
        public StringSplitBlock(char[] separator)
        {
            Handle = s => s.Split(separator);
        }

        public Func<string, IEnumerable<string>> Handle { get; private set; }
    }
}