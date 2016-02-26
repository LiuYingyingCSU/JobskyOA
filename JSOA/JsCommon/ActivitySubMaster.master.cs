using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class JsCommon_ActivitySubMaster : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            this.Master.btnActivity.ImageUrl = "~/Image/Sign/SignOutTo.PNG";
        }
        if(Session["jobskyerID"]==null)
        {
            Response.Redirect("~/JsCommon/Login.aspx");
        }
    }
    protected void ImgbtnDiary_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/JsCommon/Diary.aspx");
    }
    protected void ImgbtnNews_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/JsCommon/News.aspx");
    }
    protected void ImgbtnNotice_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/JsCommon/Notice.aspx");
    }
    protected void ImgbtnMessage_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/JsCommon/Message.aspx");
    }
}
