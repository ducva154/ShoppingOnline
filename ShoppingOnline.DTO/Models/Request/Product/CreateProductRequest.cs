﻿using ShoppingOnline.DTO.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingOnline.DTO.Models.Request.Product
{
    public class CreateProductRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public double Discount { get; set; }
        public string Brand { get; set; }
        public int StockQuantity { get; set; }
        public string Image { get; set; }
        public double Rating { get; set; }
        public IEnumerable<string> CategoryIds { get; set; }
    }
}