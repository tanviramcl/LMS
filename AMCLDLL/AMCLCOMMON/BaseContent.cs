using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

/// <summary>
/// Summary description for BaseContent
/// </summary>
public class BaseContent
{
    public static bool IsSessionExpired()
    {
        if (HttpContext.Current.Session["BCContent"] == null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public static bool IsAdminSessionExpired()
    {
        if (HttpContext.Current.Session["BCContentAdmin"] == null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static DateTime GetCompanyDate()
    {

        BaseClass bcContent = new BaseClass();

        bcContent = GetBaseContent();

        if (bcContent != null)
        {
            return bcContent.AppRunDate;
        }

        return System.DateTime.Now;

    }

    public static BaseClass GetBaseContent()
    {
        BaseClass bcContent = new BaseClass();

        if (HttpContext.Current.Session["BCContent"] != null)
        {
            bcContent = (BaseClass)HttpContext.Current.Session["BCContent"];
        }

        return bcContent;

    }
    public static int GetAppUserID()
    {

        BaseClass bcContent = new BaseClass();

        bcContent = GetBaseContent();

        if (bcContent != null)
        {
            return bcContent.AppUserID;
        }

        return 0;
    }

    public static bool IsPermitted(string UserType, string MenuPermitedUserGroup)
    {
        string[] ArryUserGroup = MenuPermitedUserGroup.Split(',');
        if (ArryUserGroup.Length > 0)
        {
            for (int Looper = 0; Looper < ArryUserGroup.Length; Looper++)
            {
                if (string.Compare(UserType, ArryUserGroup[Looper].ToString(), true) == 0)
                {
                    return true;
                }
            }
        }
        return false;
    }

}
