using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
public partial class JsManager_Manager_Master : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //管理员什么时候都可以登录
    }
    protected void btnLogout_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/JsCommon/Login.aspx");
    }
}
