using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;

public partial class JsCommon_FileDownload : System.Web.UI.Page
{ 
    protected void Page_Load(object sender, EventArgs e)
    {
        string fileGroup;
        if (Request.QueryString["fileGroup"] == null)
        {
            fileGroup = "美工组";
           // path = Server.MapPath("/File/Program");
        }
        else
        {
            fileGroup = Request.QueryString["fileGroup"];
            //path = Request.QueryString["filePath"];
            //Response.Write(path);
            switch(fileGroup)
            {
                case "程序组":
                    this.Master.btnPro.ImageUrl = "~/Image/Button/Program-01.PNG"; 
                    break;
                case "美工组":
                    this.Master.btnArt.ImageUrl = "~/Image/Button/Art-01.PNG";
                    break;
                case "网络组":
                    this.Master.btnNet.ImageUrl = "~/Image/Button/Net-01.PNG";
                    break;
                case "办公室":
                    this.Master.btnOff.ImageUrl = "~/Image/Button/Office-01.PNG";
                    break;
            }
        }
        //this.Master.btnPro.ImageUrl = "~/Image/Button/Program-01.PNG";
        if (!IsPostBack)
        {
            if (Session["jobskyerID"] == null)
            {
                Response.Write("<script>alert('登录过期，请尝试重新登录！')</script>");
                Response.Redirect("~/JsCommon/Login.aspx");
            }
        }
    }
}