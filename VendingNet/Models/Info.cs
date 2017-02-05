using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VendingNet.Models
{
    /// <summary>
    /// Контейнер даных, содержащий понятное наименование продукции/монеты и стоимость/номинал
    /// </summary>
    public class Info
    {
        public string Title { get; set; }
        public int Price { get; set; }
    }
}