using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using SodaMachine.Domain.Models;
using SodaMachine.Domain.Models.Enums;

namespace SodaMachine.Domain.DBInit
{
    public class MainDBInit 
    {
        private readonly ApplicationContext _context;

        public MainDBInit(ApplicationContext context)
        {
            _context = context;
        }

        public void Init()
        {
            CreateDemoSoda();
            CreateDemoCoins();
            CreateDemoCoinsInMachine();
        }

        private void CreateDemoSoda()
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                // Allow to insert Ids to the table
                _context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT Soda ON");
                // Create countries
                if (!_context.Soda.Any())
                {
                    var data = new List<Soda>()
                    {
                        new Soda
                        {
                            Id = 1,
                            Name = "Sprite",
                            Img = "https://i5.walmartimages.com/asr/428a8d5c-7bc6-459f-b731-c2b3b2bfe166_1.95826ed6d7106e86e7db6849427e98bc.jpeg?odnHeight=450&odnWidth=450&odnBg=FFFFFF",
                            Price = 16,
                            Amount = 2
                        },
                        new Soda
                        {
                            Id = 2,
                            Name = "Coke",
                            Img = "https://products1.imgix.drizly.com/ci-diet-coke-9baf6ff373130a99.jpeg?auto=format%2Ccompress&dpr=2&fm=jpeg&h=240&q=20",
                            Price = 30,
                            Amount = 2
                        },
                        new Soda
                        {
                            Id = 3,
                            Name = "Cola",
                            Img = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTzAACP4pSqzTXUG8FnkzQihW8VIodwOwmWQzrJdTcBA2yj9OeS",
                            Price = 22,
                            Amount = 5
                        },
                        new Soda
                        {
                            Id = 4,
                            Name = "Fanta",
                            Img = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcShPwFZvfSN1mbe7eFZnda2R7lHyHipY2POd1sUeoLBiGZcZrDO",
                            Price = 12,
                            Amount = 5
                        },
                        new Soda
                        {
                            Id = 5,
                            Name = "Paper",
                            Img = "https://www.myamericanmarket.com/977-large_default/dr-pepper-soda.jpg",
                            Price = 47,
                            Amount = 12
                        },
                    };

                    _context.Soda.AddRange(data);
                }

                _context.SaveChanges();
                _context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT Soda OFF");
                transaction.Commit();
            }
        }

        private void CreateDemoCoins()
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                // Allow to insert Ids to the table
                _context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT Coin ON");
                // Create countries
                if (!_context.Coin.Any())
                {
                    var data = new List<Coin>()
                    {
                        new Coin
                        {
                            Id = 1,
                            Name = "1 Рубль",
                            Value = 1,
                            IsAvalible = true,
                            CoinType = CoinType.One
                        },
                        new Coin
                        {
                            Id = 2,
                            Name = "2 Рубля",
                            Value = 2,
                            IsAvalible = false,
                            CoinType = CoinType.Two
                        },
                        new Coin
                        {
                            Id = 3,
                            Name = "5 Рублей",
                            Value = 5,
                            IsAvalible = true,
                            CoinType = CoinType.Five
                        },
                        new Coin
                        {
                            Id = 4,
                            Name = "10 Рублей",
                            Value = 10,
                            IsAvalible = true,
                            CoinType = CoinType.Ten
                        },
                    };

                    _context.Coin.AddRange(data);
                }

                _context.SaveChanges();
                _context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT Coin OFF");
                transaction.Commit();
            }
        }

        private void CreateDemoCoinsInMachine()
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                // Allow to insert Ids to the table
                _context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT CoinsInMachine ON");
                // Create countries
                if (!_context.CoinsInMachine.Any())
                {
                    var data = new List<CoinsInMachine>()
                    {
                        new CoinsInMachine
                        {
                            Id = 1,
                            Count = 17,
                            CoinId = 1
                        },
                        new CoinsInMachine
                        {
                            Id = 2,
                            Count = 11,
                            CoinId = 2
                        },
                        new CoinsInMachine
                        {
                            Id = 3,
                            Count = 43,
                            CoinId = 3
                        },
                        new CoinsInMachine
                        {
                            Id = 4,
                            Count = 32,
                            CoinId = 4
                        }
                    };

                    _context.CoinsInMachine.AddRange(data);
                }

                _context.SaveChanges();
                _context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT CoinsInMachine OFF");
                transaction.Commit();
            }
        }
    }
}
