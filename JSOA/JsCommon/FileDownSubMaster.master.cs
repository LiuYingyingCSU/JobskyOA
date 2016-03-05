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
        
        if(!IsPostBack)
        {
            this.Master.btnFile.ImageUrl = "~/Image/Button/file-01.PNG";
        }
        if(Session["jobskyerID"]==null)
        {
            Response.Redirect("~/JsCommon/Login.aspx");
        }
        
    }
    public ImageButton btnPro
    {
        get
        {
            return ImgbtnProgram;
        }
        set
        {
            ImgbtnProgram = value;
        }
    }
    public ImageButton btnArt
    {
        get
        {
            return ImgbtnArt;
        }
        set
        {
            ImgbtnArt = value;
        }
    }
    public ImageButton btnNet
    {
        get
        {
            return ImgbtnNet;
        }
        set
        {
            ImgbtnNet = value;
        }
    }
    public ImageButton btnOff
    {
        get
        {
            return ImgbtnOffice;
        }
        set
        {
            ImgbtnOffice = value;
        }
    }
    protected void ImgbtnProgram_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/JsCommon/FileProgram.aspx?fileGroup=程序组&filePath=/File/Program/");
    }
    protected void ImgbtnArt_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/JsCommon/FileProgram.aspx?fileGroup=美工组&filePath=/File/Art/");
    }
    protected void ImgbtnNet_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/JsCommon/FileProgram.aspx?fileGroup=网络组&filePath=/File/Net/");
    }
    protected void ImgbtnOffice_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/JsCommon/FileProgram.aspx?fileGroup=办公室&filePath=/File/Office/");
    }
}
