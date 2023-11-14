﻿using System.Collections.Generic;

namespace VNet.CommandLine
{
    public interface IUnknownOptions
    {
        bool AllowUnknownOptions { get; set; }
        List<string> ShortNames { get; set; }
    }
}
