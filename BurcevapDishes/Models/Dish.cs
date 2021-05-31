﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BurcevapDishes.Models
{
    public class Dish
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Calorie { get; set; }

        public int Weight { get; set; }
    }
}
