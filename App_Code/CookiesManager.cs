using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for CookiesManager
/// </summary>
public class CookiesManager
{
    public CookiesManager()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    //----------------------------------------------------------------------------------------------------------------------------------------------
    public void Add(string Key, string Value)
    {
        //if (HttpContext.Current.Request.Cookies[Key] != null)
        //{
        //    Remove(Key);
        //    //HttpContext.Current.Response.Cookies[Key].Value = Value;
        //}

        HttpCookie cookie = new HttpCookie(Key);
        //cookie.Domain = "http://demo.kidneyonline.org";
        cookie.Expires = DateTime.Now.AddDays(8);
        cookie.Value = Value;
        HttpContext.Current.Response.Cookies.Add(cookie);
        //HttpContext.Current.Response.Cookies[Key].Value = Value;
        //HttpContext.Current.Response.Cookies[Key].Expires = DateTime.Now.AddDays(8);
    }
    //----------------------------------------------------------------------------------------------------------------------------------------------
    public void Remove(string Key)
    {
        if (HttpContext.Current.Request.Cookies[Key] != null)
        {
            //HttpCookie cookie = new HttpCookie(Key);
            //cookie.Expires = DateTime.Now;
            HttpContext.Current.Response.Cookies[Key].Expires = DateTime.Now.AddDays(-8);
        }
    }
    //----------------------------------------------------------------------------------------------------------------------------------------------
    public void RemoveAll()
    {
        foreach (string Key in HttpContext.Current.Response.Cookies.AllKeys)
        {
            HttpContext.Current.Response.Cookies[Key].Expires = DateTime.Now.AddDays(-8);
        }
    }
    //----------------------------------------------------------------------------------------------------------------------------------------------
    public bool CheckKey(string Key)
    {
        bool status = false;
        //HttpCookie cookie = new HttpCookie(Key);
        //cookie.Domain = "http://demo.kidneyonline.org";
        //cookie = HttpContext.Current.Request.Cookies[Key];
        if (HttpContext.Current.Request.Cookies[Key] != null)
        {
            status = true;
        }
        return status;
    }
    //----------------------------------------------------------------------------------------------------------------------------------------------
    public string GetValue(string Key)
    {
        string strData = null;
        //HttpCookie cookie = new HttpCookie(Key);
        //cookie.Domain = "http://demo.kidneyonline.org";
        //cookie = HttpContext.Current.Request.Cookies[Key];
        if (HttpContext.Current.Request.Cookies[Key] != null)
        {
            strData = HttpContext.Current.Request.Cookies[Key].Value;
        }
        return strData;
    }

}