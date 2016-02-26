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
        Response.Redirect("~/JsCommon/FileProgram.aspx?fileGroup=程序组&filePath=/File/Program/");
    }
    protected void ImgbtnArt_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/JsCommon/FileArt.aspx?fileGroup=美工组&filePath=/File/Art/");
    }
    protected void ImgbtnNet_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/JsCommon/FileNet.aspx?fileGroup=网络组&filePath=/File/Net/");
    }
    protected void ImgbtnOffice_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/JsCommon/FileOffice.aspx?fileGroup=办公室&filePath=/File/Office/");
    }
}
