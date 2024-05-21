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
/// Summary description for BaseClass
/// </summary>
public class BaseClass
{
    private int _id = 0;
    private int _appUserID = 0;
    private string _loginID;
    private string _loginUserName;
    private string _loginName;
    private DateTime _loginTime;
    private string _zoneId;
    private int _appId;
    private string _appName;
    private string _dbServerName;
    private string _dbName;
    private string _dbUserId;
    private string _dbUserPassword;
    private DateTime _appRunDate;
    private string _roles;
    private string _usertype;
    private bool _changepassword = true;
    private long _sessionid;

    public int ID
    {
        get { return _id; }
        set { _id = value; }
    }
    public int AppUserID
    {
        get { return _appUserID; }
        set { _appUserID = value; }
    }
    public string LoginID
    {
        get { return _loginID; }
        set { _loginID = value; }
    }
    public string LoginUserName
    {
        get { return _loginUserName; }
        set { _loginUserName = value; }
    }

    public string LoginName
    {
        get { return _loginName; }
        set { _loginName = value; }
    }
    public DateTime LoginTime
    {
        get { return _loginTime; }
        set { _loginTime = value; }
    }
    public string ZoneId
    {
        get { return _zoneId; }
        set { _zoneId = value; }
    }
    public int AppId
    {
        get { return _appId; }
        set { _appId = value; }
    }
    public string AppName
    {
        get { return _appName; }
        set { _appName = value; }
    }
    public string DBServerName
    {
        get { return _dbServerName; }
        set { _dbServerName = value; }
    }
    public string DbName
    {
        get { return _dbName; }
        set { _dbName = value; }
    }
    public string DbUserId
    {
        get { return _dbUserId; }
        set { _dbUserId = value; }
    }
    public string DbUserPassword
    {
        get { return _dbUserPassword; }
        set { _dbUserPassword = value; }
    }
    public DateTime AppRunDate
    {
        get { return _appRunDate; }
        set { _appRunDate = value; }
    }
    public string Roles
    {
        get { return _roles; }
        set { _roles = value; }
    }
    public string UserType
    {
        get { return _usertype; }
        set { _usertype = value; }
    }
    public bool ChangePass
    {
        get { return _changepassword; }
        set { _changepassword = value; }
    }
    public long SessionID
    {
        get { return _sessionid; }
        set { _sessionid = value; }
    }
}
