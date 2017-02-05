using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VendingNet.Models
{
    public class VMWallet : Wallet
    {
        public VMWallet(Dictionary<FaceValueTypes, int> _coins)
            : base(_coins)
        {
            _money_cache = 0;
        }

        private int _money_cache;
        /// <summary>
        /// Внесенная сумма
        /// </summary>
        public int MoneyCache
        {
            get { return _money_cache; }
        }

        public void Add(Coin coin)
        {
            _money_cache += coin.Info.Price;
            Add(new List<IUnit<FaceValueTypes>> { coin });
        }

        public bool Buy(int price)
        {
            if (_money_cache >= price)
            {
                _money_cache -= price;
                return true;
            }
            else
            {
                return false;
            }
        }


        public List<Coin> ReturnCache()
        {
            List<Coin> for_return = new List<Coin>();
            //Получаем список всех доступных монет
            Dictionary<Coin, int> avail_coins = new Dictionary<Coin, int>();
            foreach (var item in GetSorted())
            {
                avail_coins.Add(new Coin(item.Key), item.Value);
            }
            //Массив всех доступных номиналов
            int[] values = avail_coins.Where(p => p.Value > 0).OrderByDescending(p => p.Key.Info.Price).Select(p => p.Key.Info.Price).ToArray();
            int i = 0;
            while (_money_cache > 0 && i < values.Length)
            {
                //Сколько нам нужно монет данного номинала
                int need_coins = (int)Math.Floor((double)_money_cache / values[i]);
                //Сколько у нас их есть
                int have_coins = avail_coins.Where(p => p.Key.Info.Price == values[i]).Select(p => p.Value).First();
                //Если у нас есть такое кол-во данных монет то возвращаем его, иначе отдаем что есть
                if (need_coins > have_coins)
                {
                    need_coins = have_coins;
                }
                List<Coin> ret_coins = _GetCoins(values[i], need_coins);
                for_return.AddRange(ret_coins);
                _money_cache -= VMWallet.GetSum(ret_coins);
                i++;
            }
            return for_return;
        }
        /// <summary>
        /// Отдает определенное кол-во монет, определенного номинала
        /// </summary>
        /// <param name="val">номинал</param>
        /// <param name="count">кол-во</param>
        /// <returns></returns>
        private List<Coin> _GetCoins(int val, int count)
        {
            List<Coin> coins = _units.Where(p => p.Info.Price == val).Select(p => (Coin)p).Take(count).ToList();
            foreach (Coin coin in coins)
            {
                Remove(coin.Type);
            }
            return coins;
        }

        public static int GetSum(List<Coin> coins)
        {
            int sum = 0;
            foreach (var coin in coins)
            {
                sum += coin.Info.Price;
            }
            return sum;
        }
    }
}