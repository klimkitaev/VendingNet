using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VendingNet.Models
{
    /// <summary>
    /// Типы номиналов монеты
    /// </summary>
    public enum FaceValueTypes { One, Two, Five, Ten };


    public class Coin : IUnit<FaceValueTypes>
    {
        private FaceValueTypes _type;

        public Coin(FaceValueTypes f_type)
        {
            _type = f_type;
        }

        public static Info GetInfo(FaceValueTypes type)
        {
            switch (type)
            {
                case FaceValueTypes.One:
                    return new Info { Title = "1 руб", Price = 1 };
                case FaceValueTypes.Two:
                    return new Info { Title = "2 руб", Price = 2 };
                case FaceValueTypes.Five:
                    return new Info { Title = "5 руб", Price = 5 };
                case FaceValueTypes.Ten:
                    return new Info { Title = "10 руб", Price = 10 };
            }
            return null;
        }

        public FaceValueTypes Type
        {
            get { return _type; }
        }
        public Info Info
        {
            get { return Coin.GetInfo(_type); }
        }

        public override bool Equals(object obj)
        {
            Coin coin = obj as Coin;
            if (coin == null)
                return false;
            return this.Type == coin.Type;
        }
    }
}