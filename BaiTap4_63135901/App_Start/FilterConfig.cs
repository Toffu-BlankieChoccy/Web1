﻿using System.Web;
using System.Web.Mvc;

namespace BaiTap4_63135901
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}