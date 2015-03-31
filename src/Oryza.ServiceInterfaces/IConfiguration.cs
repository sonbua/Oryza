﻿using System;

namespace Oryza.ServiceInterfaces
{
    public interface IConfiguration
    {
        Uri OryzaCaptureAddress { get; }

        string PriceTableXPath { get; }

        string PublishDateXPath { get; }

        string PublishDateFormat { get; }
    }
}