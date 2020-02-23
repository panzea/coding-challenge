﻿using System.Collections.Generic;

namespace ConstructionLine.CodingChallenge.Domain
{
    public class SearchOptions
    {
        public List<Size> Sizes { get; set; } = new List<Size>();

        public List<Color> Colors { get; set; } = new List<Color>();
    }
}