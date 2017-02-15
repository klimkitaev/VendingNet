using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VendingNet.Models;
using System.Collections.Generic;

namespace VendingNet.Test
{
    [TestClass]
    public class WalletTest
    {
        /// <summary>
        /// Тест корректности добавления монет в монетоприемник вендиновой машины
        /// </summary>
        [TestMethod]
        public void AddCoinsTest()
        {
            //arrange
            VMWallet vwWallet = new VMWallet(new Dictionary<FaceValueTypes, int>{
                {FaceValueTypes.One,100},
                {FaceValueTypes.Two,100},
                {FaceValueTypes.Five,100},
                {FaceValueTypes.Ten,100}
            });

            List<Coin> AddingCoins = new List<Coin>{
            new Coin(FaceValueTypes.Ten),
            new Coin(FaceValueTypes.Ten),
            new Coin(FaceValueTypes.Two),
            new Coin(FaceValueTypes.One)
            };

            //act
            foreach (var coin in AddingCoins)
            {
                vwWallet.Add(coin);
            }

            //assert
            Assert.AreEqual(23, vwWallet.MoneyCache);
        }

        /// <summary>
        /// Тест корректности выдачи сдачи
        /// </summary>
        [TestMethod]
        public void ReturnCacheTest()
        {
            //arrange
            VMWallet vwWallet = new VMWallet(new Dictionary<FaceValueTypes, int>{
                {FaceValueTypes.One,100},
                {FaceValueTypes.Two,100},
                {FaceValueTypes.Five,100},
                {FaceValueTypes.Ten,100}
            });

            List<Coin> AddingCoins = new List<Coin>{
            new Coin(FaceValueTypes.Ten),
            new Coin(FaceValueTypes.Ten),
            new Coin(FaceValueTypes.Two),
            new Coin(FaceValueTypes.One)
            };

            //act
            foreach (var coin in AddingCoins)
            {
                vwWallet.Add(coin);
            }
            List<Coin> returned_coins = vwWallet.ReturnCache();

            //assert
            Assert.AreEqual(AddingCoins.Count, returned_coins.Count);
        }

        /// <summary>
        /// Тест корректности последовательности выдачи сдачи
        /// </summary>
        [TestMethod]
        public void ReturnCacheOrderTest()
        {
            //arrange
            VMWallet vwWallet = new VMWallet(new Dictionary<FaceValueTypes, int>{
                {FaceValueTypes.One,100},
                {FaceValueTypes.Two,100},
                {FaceValueTypes.Five,100},
                {FaceValueTypes.Ten,100}
            });

            List<Coin> AddingCoins = new List<Coin>{
            new Coin(FaceValueTypes.Ten),
            new Coin(FaceValueTypes.Ten),
            new Coin(FaceValueTypes.Two),
            new Coin(FaceValueTypes.One)
            };

            //act
            foreach (var coin in AddingCoins)
            {
                vwWallet.Add(coin);
            }
            List<Coin> returned_coins = vwWallet.ReturnCache();

            //assert
            Assert.AreEqual(AddingCoins.Count, returned_coins.Count);

            var e1 = AddingCoins.GetEnumerator();
            var e2 = returned_coins.GetEnumerator();

            while (e1.MoveNext() && e2.MoveNext())
            {
                Assert.AreEqual(e1.Current, e2.Current);
            }
        }
    }
}
