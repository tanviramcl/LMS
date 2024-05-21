using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace LoanManagementSystem.Controllers
{
    public class LoginController : Controller
    {
        CommonGateway commonGatewayObj = new CommonGateway();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Login");
        }


        [HttpPost]
        [AllowAnonymous]
        public JsonResult Login(String loginMdl)
        {
            List<Models.User> jsul = new JavaScriptSerializer().Deserialize<List<Models.User>>(loginMdl);
            string loginId = "";
            string password = "";
            string msg = "";

            foreach (Models.User user in jsul)
            {
                loginId = user.UserName;
                password = user.PassWord;
            }

            if (IsUesrCheck(loginId, password))
            {
                if (IsUesrPermitted(loginId))
                {
                    msg = "SUCCESS";

                }
                else
                {
                    msg = "You are not permitted to access this resource ";
                }
            }
            else
            {
                msg = "Invalid LoginID or Passward";
            }


            return Json(msg, JsonRequestBehavior.AllowGet);
        }


        public string Encrypt(string clearText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = System.Text.Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }

        public static string EncodePasswordToBase64(string password)
        {
            try
            {
                byte[] encData_byte = new byte[password.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(password);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Encode" + ex.Message);
            }
        }

        public bool IsUesrCheck(string loginID, string loginPassword)
        {
            DataTable dtUserInfo = new DataTable();

            string userId = loginID.ToString();
            string passWord = Encrypt(loginPassword);


            dtUserInfo = commonGatewayObj.Select("SELECT * FROM USER_TABLE WHERE USER_ID='" + loginID + "' AND PASSWORD='" + passWord + "' AND ACTIVE='Y'");
            if (dtUserInfo.Rows.Count > 0)
            {
                string UserID = dtUserInfo.Rows[0]["USER_ID"].ToString();
                string UserName = dtUserInfo.Rows[0]["NAME"].ToString();
                string UserType = dtUserInfo.Rows[0]["USER_TYPE"].ToString();
                string password = dtUserInfo.Rows[0]["PASSWORD"].ToString();

                if (UserID == userId && passWord == password)

                Session["UserID"] = UserID;
                Session["UserName"] = UserName;
                Session["UserType"] = UserType;
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsUesrPermitted(string loginID)
        {
            DataTable dtUserInfo = new DataTable();
            dtUserInfo = commonGatewayObj.Select(" SELECT * FROM MENUPERMISSIONS WHERE USER_ID='" + loginID + "'");
            if (dtUserInfo.Rows.Count > 0)
            {
                string UserID = dtUserInfo.Rows[0]["USER_ID"].ToString();

                Session["UserID"] = UserID;

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}