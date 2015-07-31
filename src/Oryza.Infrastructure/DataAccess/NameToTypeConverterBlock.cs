using System;
using System.Collections.Generic;
using System.Linq;
using LegoBuildingBlock;
using LegoBuildingBlock.Linq;
using LegoBuildingBlock.String;
using Oryza.ServiceInterfaces;

namespace Oryza.Infrastructure.DataAccess
{
    public class NameToTypeConverterBlock : IBlock<string, string>
    {
        public NameToTypeConverterBlock(IConfiguration configuration, WordCapitalizationBlock wordCapitalizationBlock, SpecialCharTranslationBlock specialCharTranslationBlock)
        {
            Handle = name => new StringSplitBlock(configuration.EntryNameSeparators.ToArray()).ContinuesWith(new SelectManyBlock<string, char>(wordCapitalizationBlock))
                                                                                              .ContinuesWith(new SelectManyBlock<char, char>(specialCharTranslationBlock))
                                                                                              .ContinuesWith(new ToArrayBlock<char>())
                                                                                              .ContinuesWith(new StringConstructorBlock())
                                                                                              .Handle(name);
        }

        public Func<string, string> Handle { get; private set; }
    }

    public class SpecialCharTranslationBlock : IBlock<char, IEnumerable<char>>
    {
        private readonly IConfiguration _configuration;

        public SpecialCharTranslationBlock(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Func<char, IEnumerable<char>> Handle
        {
            get { return InnerHandle; }
        }

        private IEnumerable<char> InnerHandle(char c)
        {
            if (!_configuration.SpecialCharToWordMap.ContainsKey(c))
            {
                yield return c;
                yield break;
            }

            foreach (var translatedChar in _configuration.SpecialCharToWordMap[c])
            {
                yield return translatedChar;
            }
        }
    }
}