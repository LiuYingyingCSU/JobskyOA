using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class JsCommon_FileDownSubMaster : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(Session["jobskyerID"]==null)
        {
            Response.Redirect("~/JsCommon/Login.aspx");
        }
    }
    protected void ImgbtnProgram_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/JsCommon/FileProgram.aspx");
    }
    protected void ImgbtnArt_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/JsCommon/FileArt.aspx");
    }
    protected void ImgbtnNet_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/JsCommon/FileNet.aspx");
    }
    protected void ImgbtnOffice_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/JsCommon/FileOffice.aspx");
    }
}
