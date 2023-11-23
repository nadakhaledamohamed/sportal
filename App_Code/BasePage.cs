using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for BasePage
/// </summary>
public class BasePage : System.Web.UI.Page
{
    public BasePage()
    {


    }
    //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    protected void Page_Error(object sender, EventArgs e)
    {
        //EMail.SendError();
    }
    //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    protected override void InitializeCulture()
    {
        //Account.CheckLanguage();
        //System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("ar-EG");
        //System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("ar-EG");
    }
    //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    protected void Page_PreInit(object sender, EventArgs e)
    {
        //Account.CheckLanguage();
        //Page.Theme = Account.GetTheme();
        //System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("ar-EG");
        //System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("ar-EG");
    }
    //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    protected void PleaseSelectDropDownList_DataBound(object sender, EventArgs e)
    {
        ((DropDownList)sender).Items.Insert(0, new ListItem("Please Select", "0"));
        //Account.CheckLanguage();
        //if (new CookiesManager().GetValue("lang") == "en")
        //{
        //}
        //else
        //{
        //    ((DropDownList)sender).Items.Insert(0, new ListItem("مطلوب الاختيار", "0"));
        //}

    }
    protected void GridView_PreRender(object sender, EventArgs e)
    {
        if (((GridView)sender).HeaderRow != null)
        {
            ((GridView)sender).HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        if (((GridView)sender).FooterRow != null)
        {
            ((GridView)sender).FooterRow.TableSection = TableRowSection.TableFooter;
        }
    }
    public static string FormatDate(string DDMMYYYY)
    {
        string[] str = DDMMYYYY.Split('/');
        return str[1] + "/" + str[0] + "/" + str[2];
    }

}