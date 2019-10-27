using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cookbook.DTO
{
    public class IngredientDTO
    {
        public string Name { get; set; }
        public int Amount { get; set; }
        public Enums.MeasuringUnitType MeasuringUnit { get; set; }
    }
}
