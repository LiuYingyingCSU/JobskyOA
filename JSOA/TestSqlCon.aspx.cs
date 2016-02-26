using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class TestSqlCon : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DB db = new DB();
        SqlConnection scon = new SqlConnection();
        scon = db.GetCon();
        scon.Open();
        if (scon.State == ConnectionState.Open)
        {
            Response.Write("<script>alert('数据库连接成功！');location.href='JsCommon/Login.aspx';</script>");
        }
        else
        {
            Response.Write("<script>alert('数据库连接失败！');location.href='JsCommon/Login.aspx';</script>");
        }
        scon.Close();
    }
}