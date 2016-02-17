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
        if (ipAddress.Substring(0, 3) != "::2")
        {
            SignIn_ImgBtn.Enabled = false;
            //ImageButton MasSignBtn = (ImageButton)this.Master.FindControl("SignIn_ImgBtn");
            //MasSignBtn.Enabled = false;
            //Response.Write("<script>alert('" + ipAddress + "')</script>");
        }
        //Response.Write(HttpContext.Current.User.Identity.Name);
    }
    protected void Main_ImgBtn_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/JsCommon/JsCommonMain.aspx");
    }
    protected void SignIn_ImgBtn_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/JsCommon/SignInAndOut.aspx");
    }
    protected void Activity_ImgBtn_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/JsCommon/Diary.aspx");
    }
    protected void FileDown_ImgBtn_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/JsCommon/FileProgram.aspx");
    }
    protected void BBS_ImgBtn_Click(object sender, ImageClickEventArgs e)
    {
        //Response.Redirect("~/JsCommon/BBS.aspx");
    }
}
