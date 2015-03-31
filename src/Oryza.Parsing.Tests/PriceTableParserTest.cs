﻿using System;
using System.IO;
using Oryza.ServiceInterfaces;
using Oryza.TestBase;
using Xunit;

namespace Oryza.Parsing.Tests
{
    public class PriceTableParserTest
    {
        private readonly IServiceProvider _serviceProvider;

        public PriceTableParserTest()
        {
            _serviceProvider = new TestDoublesContainerBuilder().Build();
        }

        [Fact]
        public void Parse_InputHtml_ReturnsHtmlPriceTable()
        {
            // arrange
            var parser = _serviceProvider.GetService<IPriceTableParser>();
            var html = File.ReadAllText("oryza_web_page.txt");

            // act
            var priceTable = parser.Parse(html);

            // assert
            Assert.Contains("view-rice-price", priceTable);
        }
    }
}