using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cookbook.DTO
{
    public class AdjustedIngredientDTO
    {
        public string Name { get; set; }
        public double Amount { get; set; }
        public Enums.MeasuringUnitType MeasuringUnit { get; set; }
    }
}
