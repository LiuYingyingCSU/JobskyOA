using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //判断用户登录的IP地址，决定签到是否可用
        string ipAddress = HttpContext.Current.Request.UserHostAddress;
        if (ipAddress.Substring(0, 3) != "::1")
        {
            //SignIn_ImgBtn.Enabled = false;
        }
        //Response.Write(ipAddress);
    }
    protected void btnLogout_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/JsCommon/Login.aspx");

    }
}
