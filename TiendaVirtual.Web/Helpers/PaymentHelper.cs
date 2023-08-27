using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace TiendaVirtual.Web.Helpers
{
    public class PaymentHelper
    {
        public static List<SelectListItem> GetMeses()
        {
            List<SelectListItem> meses=new List<SelectListItem>();
            for(int i = 1; i <= 12; i++)
            {
                var item = new SelectListItem()
                {
                    Text = i.ToString().PadLeft(2, '0'),
                    Value = i.ToString()
                };
                meses.Add(item);
            }
            return meses;
        }
        public static List<SelectListItem> GetAnios() {

            List<SelectListItem> anios = new List<SelectListItem>();
            for (int i = DateTime.Now.Year; i <= i+10; i++)
            {
                var item = new SelectListItem()
                {
                    Text = i.ToString().PadLeft(2, '0'),
                    Value = i.ToString()
                };
                anios.Add(item);
            }
            return anios;

        }
    }
}