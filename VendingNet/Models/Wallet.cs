using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VendingNet.Models
{
    public class Wallet : UnitCollection<FaceValueTypes>
    {
        public Wallet(Dictionary<FaceValueTypes, int> _coins)
            : base(_coins)
        {

        }

        protected override IUnit<FaceValueTypes> _UnitCreator(FaceValueTypes type)
        {
            return new Coin(type);
        }

        public void Add(List<Coin> coins)
        {
            _units.AddRange(coins);
        }
    }
}