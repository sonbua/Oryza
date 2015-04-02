using System;
using System.Collections.Generic;

namespace Oryza.ServiceInterfaces
{
    public interface IConfiguration
    {
        Uri OryzaCaptureAddress { get; }

        string PriceTableXPath { get; }

        string PublishDateXPath { get; }

        string PublishDateFormat { get; }

        string PriceUnitXPath { get; }

        ISet<string> DefaultPriceUnits { get; }

        string CategoriesXPath { get; }

        string CategoryNameXPath { get; }

        string CategoryEntriesXPath { get; }

        string CategoryEntryNameXPath { get; }

        string CategoryEntryPriceRangeXPath { get; }

        string PriceUnavailableText { get; }

        string PriceRangeDelimiter { get; }

        IDictionary<char, string> SpecialCharToWordMap { get; }
    }
}