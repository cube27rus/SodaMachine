using System;
using System.Collections.Generic;
using System.Text;

namespace SodaMachine.Domain.Models.DTOModels
{
    public class CoinDTO
    {
        public string Name { get; set; }
        public decimal Value { get; set; }
        public bool IsAvalible { get; set; }
    }
}
