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
    //以下为自定义属性，用于内容页访问母版页的控件的属性
    public ImageButton btnMain
    {
        get
        {
            return Main_ImgBtn;
        }
        set
        {
            Main_ImgBtn = value;
        }
    }
    public ImageButton btnSign
    {
        get
        {
            return SignIn_ImgBtn;
        }
        set
        {
            SignIn_ImgBtn = value;
        }
    }
    public ImageButton btnFile
    {
        get
        {
            return FileDown_ImgBtn;
        }
        set
        {
            FileDown_ImgBtn = value;
        }
    }
    public ImageButton btnActivity
    {
        get
        {
            return Activity_ImgBtn;
        }
        set
        {
            Activity_ImgBtn = value;
        }
    }
    public ImageButton btnBBS
    {
        get
        {
            return BBS_ImgBtn;
        }
        set
        {
            BBS_ImgBtn = value;
        }
    }
    protected void Main_ImgBtn_Click(object sender, ImageClickEventArgs e)
    {
        //Main_ImgBtn.ImageUrl = "~/Image/Sign/SignOutTo.PNG";
        Response.Redirect("~/JsCommon/JsCommonMain.aspx");
    }
    protected void SignIn_ImgBtn_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/JsCommon/SignInAndOut.aspx");
    }
    protected void Activity_ImgBtn_Click(object sender, ImageClickEventArgs e)
    {
        //Response.Redirect("~/JsCommon/Diary.aspx");
    }
    protected void FileDown_ImgBtn_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/JsCommon/FileProgram.aspx?fileGroup=程序组&filePath=/File/Program/");
    }
    protected void BBS_ImgBtn_Click(object sender, ImageClickEventArgs e)
    {
        //Response.Redirect("~/JsCommon/BBS.aspx");
    }
    protected void btnLogout_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/JsCommon/Login.aspx");
    }
}
