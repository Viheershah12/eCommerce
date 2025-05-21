using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Abp.eCommerce.AiFilterBuilder
{
    public class ProductFilterBuilder
    {
        private readonly List<object> _mustFilters = new();
        private readonly List<object> _shouldFilters = new();
        private int _minimumShouldMatch = 1;

        public ProductFilterBuilder WithBrand(string? brand, bool exactMatch = true)
        {
            if (!string.IsNullOrEmpty(brand))
            {
                object matchCondition;

                if (exactMatch)
                {
                    matchCondition = new
                    {
                        value = brand
                    };
                }
                else
                {
                    matchCondition = new
                    {
                        text = brand
                    };
                }

                _mustFilters.Add(new
                {
                    key = "brand",
                    match = matchCondition
                });
            }
            return this;
        }

        public ProductFilterBuilder WithColor(string? color, bool exactMatch = true)
        {
            if (!string.IsNullOrEmpty(color))
            {
                object matchCondition;

                if (exactMatch)
                {
                    if (color.Contains(','))
                    {
                        // Multiple possible colors (e.g., "red,blue,green")
                        matchCondition = new
                        {
                            any = color.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                      .Select(c => c.Trim())
                                      .ToArray()
                        };
                    }
                    else
                    {
                        // Single color exact match
                        matchCondition = new
                        {
                            value = color
                        };
                    }
                }
                else
                {
                    // Partial text match for single color
                    matchCondition = new
                    {
                        text = color
                    };
                }

                _mustFilters.Add(new
                {
                    key = "color",
                    match = matchCondition
                });
            }
            return this;
        }

        public ProductFilterBuilder WithSize(string? size)
        {
            if (!string.IsNullOrEmpty(size))
            {
                if (TryParseSize(size, out var numericSize))
                {
                    _mustFilters.Add(new
                    {
                        key = "size_numeric",
                        range = new { gt = numericSize * 0.9, lt = numericSize * 1.1 }
                    });
                }
                else
                {
                    _mustFilters.Add(new { key = "size", match = new { text = size } });
                }
            }
            return this;
        }

        public ProductFilterBuilder WithMaterial(string? material)
        {
            if (!string.IsNullOrEmpty(material))
            {
                _mustFilters.Add(new
                {
                    key = "material",
                    match = new { text = material }
                });
            }
            return this;
        }

        public ProductFilterBuilder WithType(string? type)
        {
            if (!string.IsNullOrEmpty(type))
            {
                _mustFilters.Add(new
                {
                    key = "product_type",
                    match = new { text = type }
                });
            }
            return this;
        }

        public ProductFilterBuilder WithPriceRange(decimal? minPrice, decimal? maxPrice)
        {
            if (minPrice.HasValue || maxPrice.HasValue)
            {
                var range = new Dictionary<string, object>();
                if (minPrice.HasValue) range["gte"] = minPrice.Value;
                if (maxPrice.HasValue) range["lte"] = maxPrice.Value;

                _mustFilters.Add(new { key = "price", range = range });
            }
            return this;
        }

        public ProductFilterBuilder WithTags(params string[] tags)
        {
            if (tags != null && tags.Length > 0)
            {
                _mustFilters.Add(new
                {
                    key = "tags",
                    match = new { any = tags }
                });
            }
            return this;
        }

        public static bool TryParseSize(string? size, out double numericSize)
        {
            numericSize = 0;

            if (string.IsNullOrWhiteSpace(size))
                return false;

            // Common size patterns with units
            var patterns = new Dictionary<string, double>
            {
                { @"(\d+)\s*GB", 1 },             // 32GB → 32
                { @"(\d+)\s*TB", 1000 },           // 1TB → 1000
                { @"(\d+)\s*oz", 1 },              // 16oz → 16
                { @"(\d+)\s*ml", 1 },              // 500ml → 500
                { @"(\d+)\s*g", 1 },               // 250g → 250
                { @"(\d+)\s*kg", 1000 },           // 2kg → 2000
                { @"(\d+)\s*mm", 1 },              // 120mm → 120
                { @"(\d+)\s*cm", 10 },             // 15cm → 150
                { @"(\d+)\s*inch(es)?", 25.4 },    // 1inch → 25.4
                { @"(\d+)\s*in", 25.4 },           // 1in → 25.4
                { @"(\d+)\s*""", 25.4 },           // 10" → 254
                { @"(\d+)\s*'", 304.8 },           // 6' → 1828.8
                { @"(\d+)\s*lbs?", 453.592 },      // 5lbs → 2267.96
                { @"(\d+)\s*x\s*(\d+)", 1 },       // 2x4 → 2 (takes first number)
                { @"(\d+)\s*-\s*(\d+)", 1 },       // 10-15 → 10 (takes first number)
                { @"(\d+)\s*\.\s*(\d+)", 1 },      // 1.5 → 1.5
                { @"(\d+)", 1 }                     // 500 → 500 (plain number)
            };

            foreach (var pattern in patterns)
            {
                var match = Regex.Match(size, pattern.Key, RegexOptions.IgnoreCase);
                if (match.Success &&
                    double.TryParse(match.Groups[1].Value, NumberStyles.Any, CultureInfo.InvariantCulture, out var value))
                {
                    // Handle ranges like "10-15" by averaging
                    if (match.Groups.Count > 2 &&
                        double.TryParse(match.Groups[2].Value, NumberStyles.Any, CultureInfo.InvariantCulture, out var value2))
                    {
                        numericSize = (value + value2) / 2 * pattern.Value;
                    }
                    else
                    {
                        numericSize = value * pattern.Value;
                    }
                    return true;
                }
            }

            // Handle special size formats
            if (TryParseSpecialSizes(size, out numericSize))
            {
                return true;
            }

            return false;
        }

        private static bool TryParseSpecialSizes(string? size, out double numericSize)
        {
            numericSize = 0;
            var lowerSize = size.ToLowerInvariant();

            // Handle clothing sizes
            var clothingSizes = new Dictionary<string, double>
            {
                { "xxs", 0 }, { "xs", 2 }, { "s", 4 }, { "m", 6 },
                { "l", 8 }, { "xl", 10 }, { "xxl", 12 }, { "xxxl", 14 }
            };

            if (clothingSizes.TryGetValue(lowerSize, out numericSize))
            {
                return true;
            }

            // Handle shoe sizes
            if (Regex.IsMatch(size, @"[a-z]"))
            {
                // Convert letters to numbers (A=1, B=2, etc.)
                var letter = size[0].ToString().ToUpperInvariant()[0];
                if (letter >= 'A' && letter <= 'Z')
                {
                    numericSize = letter - 'A' + 1;
                    return true;
                }
            }

            return false;
        }

        public Dictionary<string, object> Build()
        {
            if (!_mustFilters.Any() && !_shouldFilters.Any())
                return new();

            var filter = new Dictionary<string, object>();

            if (_mustFilters.Any())
            {
                filter["must"] = _mustFilters;
            }

            if (_shouldFilters.Any())
            {
                filter["should"] = _shouldFilters;
                filter["minimum_should_match"] = _minimumShouldMatch;
            }

            return filter.Count != 0 ? filter : [];
        }
    }
}
