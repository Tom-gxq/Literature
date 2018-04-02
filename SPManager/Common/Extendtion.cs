using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

public static class Extendtion
{
    public static string GetActive(this HtmlHelper html, string name, string param, string style)
    {
        if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(param)) return string.Empty;
        return name.ToLower() == param.ToLower() ? style : string.Empty;
    }
}