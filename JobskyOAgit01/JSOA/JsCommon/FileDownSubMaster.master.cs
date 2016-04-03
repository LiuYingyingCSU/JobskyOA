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
            
        }
        if(Session["jobskyerID"]==null)
        {
            Response.Redirect("~/JsCommon/Login.aspx");
        }
        
    }
    
    
}
