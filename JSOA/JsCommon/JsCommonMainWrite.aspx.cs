using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class JsCommon_JsCommonMainWrite : System.Web.UI.Page
{
    public string imageUrl = "../Image/JsCommonMain/Business.jpg";
    protected void Page_Load(object sender, EventArgs e)
    {
        DB db = new DB();
        if (!IsPostBack)
        {
            SqlDataReader dr0 = db.reDr("select jobImage from jobskyer where jobskyerID='" + Session["jobskyerID"] + "'");
            dr0.Read();
            if (dr0.HasRows)
            {
                imageUrl = "../Change/Picture/" + dr0["jobImage"].ToString();
            }
            else
            {
                imageUrl = "../Image/JsCommonMain/Business.jpg";
            }
            dr0.Close();
        }
    }
    protected void AddAphorismText_TextChanged(object sender, EventArgs e)
    {

    }
    protected void SubmitAnnounce_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            DateTime now = DateTime.Now;
            DB db = new DB();
            SqlConnection con = db.GetCon();
            SqlCommand com = new SqlCommand();//using System.Data.SqlClient;
            con.Open();
            com.Connection = con;
            com.CommandType = CommandType.StoredProcedure;//using System.Data;
            com.CommandText = "SubmitAphorism";
            com.Parameters.Add("@aphorismID",SqlDbType.Int);
            com.Parameters.Add("@aphorism",SqlDbType.NVarChar);
            com.Parameters["@AddAphorismText"].Value = AddAphorismText.Text;
            com.ExecuteNonQuery();
            con.Close();
            Response.Redirect("JsCommonMain.aspx");
        }
        catch(Exception ex)
        {
            Response.Write(ex.Message);
        }
    }
    
}