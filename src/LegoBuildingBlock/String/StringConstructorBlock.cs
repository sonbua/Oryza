using System;

namespace LegoBuildingBlock.String
{
    public class StringConstructorBlock : IBlock<char[], string>
    {
        public Func<char[], string> Handle
        {
            get { return chars => new string(chars); }
        }
    }
}