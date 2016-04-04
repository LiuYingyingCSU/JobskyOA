using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;

public partial class JsManager_Announce : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
        Print();
        
    }
    protected void Print()
    {
        //string a = "...";
        string str = "select noticeID,jobskyerID,jobName,notTitle,notContent,notTime from Notice,jobskyer where jobskyer.jobskyerID=Notice.jobskyerID orderby noticeID desc";
        DB db = new DB();
        SqlConnection con = new SqlConnection();
        con = db.GetCon();
        con.Open();
        SqlCommand com = new SqlCommand(str, con);
        SqlDataAdapter da = new SqlDataAdapter(com);
        DataSet ds = new DataSet();//using System.Data;
        da.Fill(ds);
        AnnounceRepeater.DataSource = ds;
        AnnounceRepeater.DataBind();
        con.Close();
    }
    protected void ImgbtnAnnounce_Click(object sender, ImageClickEventArgs e)
    {
        //跳转到编辑公告
        Response.Redirect("AddAnnounce.aspx");
    }
    protected void AnnounceRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
    {

    }
}