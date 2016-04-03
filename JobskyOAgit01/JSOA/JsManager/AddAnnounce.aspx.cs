using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class JsManager_AddAnnounce : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //Session.Clear();
        }
        
    }
    protected void AddAnnounceTitle_TextChanged(object sender, EventArgs e)
    {

    }
    protected void AddContentText_TextChanged(object sender, EventArgs e)
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
            com.CommandText = "SubmitAnnounce";
            com.Parameters.Add("@jobskyerID", SqlDbType.NVarChar);
            com.Parameters.Add("@notTitle", SqlDbType.NVarChar);
            com.Parameters.Add("@notContent", SqlDbType.NVarChar);
            com.Parameters.Add("@notTime", SqlDbType.DateTime);
            com.Parameters["@jobskyerID"].Value = Session["jobskyerID"].ToString();
            com.Parameters["@notTitle"].Value = AddAnnounceTitle.Text;
            com.Parameters["@notContent"].Value = AddContentText.Text;
            com.Parameters["@notTime"].Value = now;
            com.ExecuteNonQuery();
            con.Close();
            Response.Redirect("Announce.aspx");
        }
        catch(Exception ex)
        {
            Response.Write(ex.Message);
        }
    }
    protected void ImgbtnAnnounce_Click(object sender, ImageClickEventArgs e)
    {

    }
}