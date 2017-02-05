using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VendingNet.Models;

namespace VendingNet.Controllers
{
    public class HomeController : Controller
    {
        //Кошелек VM для сдачи
        static VMWallet vwWallet;
        //Кошелек пользователя
        static Wallet userWallet;
        //Ассортимент VM
        static ProductCatalog productCatalog;


        // GET: Home
        public ActionResult Index()
        {
            //Начальная инициализация
            vwWallet = new VMWallet(new Dictionary<FaceValueTypes, int>{
                {FaceValueTypes.One,100},
                {FaceValueTypes.Two,100},
                {FaceValueTypes.Five,100},
                {FaceValueTypes.Ten,100}
            });

            userWallet = new Wallet(new Dictionary<FaceValueTypes, int>
            {
                {FaceValueTypes.One,10},
                {FaceValueTypes.Two,30},
                {FaceValueTypes.Five,20},
                {FaceValueTypes.Ten,15}
            });

            productCatalog = new ProductCatalog(new Dictionary<ProductTypes, int>
                {
                    {ProductTypes.Tea,10},
                    {ProductTypes.Coffee,20},
                    {ProductTypes.MilkCoffee,20},
                    {ProductTypes.Juice,15}
                });

            return View();
        }

        /// <summary>
        /// Отсортированный кошелек VM
        /// </summary>
        /// <returns></returns>
        public ActionResult GetVMWallet()
        {
            return View(vwWallet.GetSorted());
        }

        /// <summary>
        /// Отсортированный кошелек пользователя
        /// </summary>
        /// <returns></returns>
        public ActionResult GetUserWallet()
        {
            return View(userWallet.GetSorted());
        }

        /// <summary>
        /// Список продуктов машины
        /// </summary>
        /// <returns></returns>
        public ActionResult GetProductCatatalog()
        {
            return View(productCatalog.GetSorted());
        }

        /// <summary>
        /// поле "Внесенная сумма"
        /// </summary>
        /// <returns></returns>
        public JsonResult GetMoneyCache()
        {
            return Json(vwWallet.MoneyCache, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Пользователь вносит деньги
        /// </summary>
        /// <param name="s_type"></param>
        /// <returns></returns>
        public JsonResult EnterMoney(string s_type)
        {
            bool res = false;
            try
            {
                FaceValueTypes type = (FaceValueTypes)Enum.Parse(typeof(FaceValueTypes), s_type);
                Coin coin = new Coin(type);
                userWallet.Remove(type);
                vwWallet.Add(coin);
                res = true;
            }
            catch { }
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Пользователь нажимает кнопку купить
        /// </summary>
        /// <param name="p_type"></param>
        /// <returns></returns>
        public ContentResult Buy(string p_type)
        {
            string res = "";
            try
            {
                ProductTypes type = (ProductTypes)Enum.Parse(typeof(ProductTypes), p_type);
                Product product = new Product(type);
                if (vwWallet.Buy(product.Info.Price))
                {
                    productCatalog.Remove(type);
                    res = "<div class=\"alert alert-success\" role=\"alert\">Спасибо!</div>";
                }
                else
                {
                    res = "<div class=\"alert alert-danger\" role=\"alert\">Недостаточно средств</div>";
                }
            }
            catch { }
            return Content(res);
        }

        /// <summary>
        /// Пользователь запрашивает сдачу 
        /// </summary>
        /// <returns></returns>
        public JsonResult GetChange()
        {
            List<Coin> coins = vwWallet.ReturnCache();
            userWallet.Add(coins);
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}