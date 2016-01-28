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
            Response.Write("数据库连接成功！");
        }
        else
        {
            Response.Write("数据库连接失败！");
        }
        scon.Close();
    }
}