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
    }
}