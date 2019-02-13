using System;
using System.Collections.Generic;
using System.Text;

namespace SodaMachine.Domain.Models.DTOModels
{
    public class SodaDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Img { get; set; }
        public decimal Price { get; set; }
    }
}
