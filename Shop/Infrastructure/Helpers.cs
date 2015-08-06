using System;
using System.Web.Mvc;
using Persia;

namespace Shop.Infrastructure
{
    public static class Helpers
    {
        public static MvcHtmlString ToPersianRelativeDate(this HtmlHelper htmlHelper, DateTime now)
        {
            SolarDate s = Persia.Calendar.ConvertToPersian(now);
            return new MvcHtmlString(s.ToRelativeDateString("W"));
        }
    }
}