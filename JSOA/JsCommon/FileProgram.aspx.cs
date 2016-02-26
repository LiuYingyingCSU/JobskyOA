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