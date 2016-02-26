using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.Security;
using System.Data;

public partial class JsCommon_Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Literal failText = (Literal)Login1.FindControl("FailureText");
        failText.Visible = false;
    }
    //登录按钮
    protected void btnLogin_Click1(object sender, ImageClickEventArgs e)
    {
        DB db = new DB();
        string jobskyerID = this.Login1.UserName.Trim();
        string passWord = this.Login1.Password.Trim();
        SqlDataReader dr = db.reDr("select * from jobskyer where jobskyerID='" + jobskyerID + "'");//and password='" + passWord + "'");
        dr.Read();
        if (dr.HasRows)                                             //通过dr中是否读出数据，判断用户是否通过身份验证
        {
            if (passWord == dr.GetValue(1).ToString())
            {
                Session["jobskyerID"] = dr.GetValue(0);                //将用户ID存入Session["jobskyerID"]
                Session["jobRole"] = dr.GetValue(11);                  //将用户权限存入Session["jobRole"]中
                Session["jobName"] = dr.GetValue(2);
                FormsAuthentication.RedirectFromLoginPage(Session["jobName"].ToString(), false);
                string jobName = dr.GetValue(2).ToString();
                if (Session["jobRole"].ToString() == "0")
                {
                    Response.Redirect("~/JsCommon/JsCommonMain.aspx?Name=" + jobName + ""); //跳转至一般用户界面，并传递参数，登录人员名称
                }
                else if (Session["jobRole"].ToString() == "1")
                {
                    Response.Redirect("~/JsManagerMain.aspx?Name=" + jobName + "");  //跳转至管理人员界面
                }
                else
                {
                    Response.Redirect("~/JsSuperMain.aspx?Name=" + jobName + "");  //跳转至超级用户界面
                }
            }
            else
            {
                Login1.FindControl("FailureText").Visible = true;
                Login1.FailureText = "密码错误";
            }
        }
        else
        {
            //Response.Write("<script>alert('不存在此用户');location.href='Login.aspx';</script>");
            Login1.FindControl("FailureText").Visible = true;
            Login1.FailureText = "用户名错误";
        }
        dr.Close();
    }
}