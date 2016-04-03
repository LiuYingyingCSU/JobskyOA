using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.UI.HtmlControls;
using System.Data;

public partial class JsCommon_JsCommonMain : System.Web.UI.Page
{
    public string imageUrl = "../Image/JsCommonMain/Business.jpg";
    protected void Page_Load(object sender, EventArgs e)
    {
        DB db = new DB();
        printAphorism();
        if(!IsPostBack)
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
        //判断用户是否已登录
         
        string jobName = HttpContext.Current.User.Identity.Name;
        if(HttpContext.Current.User.Identity.Name==""||HttpContext.Current.User.Identity.Name==null)
        {
            //用户未登录
            Response.Redirect("~/JsCommon/Login.aspx");
        }
        else
        {
            //用户已登录
            try
            {
                SqlDataReader dr1 = db.reDr("select jobAcademy,jobPosition from jobskyer where jobName='" + jobName + "'");
                dr1.Read();
                lblName.Text = HttpContext.Current.User.Identity.Name;
                if (dr1.HasRows)
                {
                    lblAcademy.Text = dr1.GetValue(0).ToString();
                    lblPosition.Text = dr1.GetValue(1).ToString();
                    //lblCount.Text = dr1.GetValue(2).ToString();
                }
                else
                {
                    Response.Write("找不到此用户！");
                }
                dr1.Dispose();
                dr1.Close();
                //dr2.Close();
                
            }
            catch
            {
                Response.Write("<script>alert('连接出错')</script>");
            }
            finally
            {

            }
        }

        //string imgUrl;
        ////imgUrl=           默认头像
        ////jobskyerID = Session["jobskyerID"].ToString();
        //SqlDataReader dr2 = db.reDr("SELECT jobImage FROM jobskyer WHERE jobName='" +jobName + "'");
        //dr2.Read();
        //imgUrl = dr2.GetValue(0).ToString().Trim();
        //dr2.Dispose();
        //dr2.Close();
    }

    public string GetJobName(object jobskyerID)             //把jobskyerID转化为jobName输出
    {
        DB db = new DB();
        SqlDataReader dr = db.reDr("SELECT jobName FROM jobskyer WHERE jobskyerID='" + jobskyerID + "'");
        dr.Read();
        return dr.GetValue(0).ToString();
    }
    //protected void rptBind()        //repeater数据绑定
    //{
    //    DB db = new DB();
    //    //int n = 4 * (Convert.ToInt32(lbPage.Text) - 1);
    //    SqlConnection myCon = db.GetCon();
    //    myCon.Open();
    //    string sqlstr = "SELECT TOP 3 jobskyerID,notTime,notTitle,notContent FROM Notice order by noticeID desc";
    //    SqlCommand mycom = new SqlCommand(sqlstr, myCon);
    //    //mycom.Parameters.Add("n", n);
    //    SqlDataAdapter da = new SqlDataAdapter(mycom);
    //    DataSet ds = new DataSet();
    //    da.Fill(ds);
    //    ds.Dispose();
    //    da.Dispose();
    //    myCon.Close();
    //    if (ds != null)
    //    {
    //        this.Repeater1.DataSource = ds;
    //        this.Repeater1.DataBind();
    //    }
    //}

    //protected void JobAphorismRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
    //{

    //}
    public void printAphorism()
    {
        try
        {
            DB db = new DB();
            string jobName = HttpContext.Current.User.Identity.Name;
            SqlDataReader dr = db.reDr("SELECT jobName,aphorism FROM jobskyer,jobAphorism WHERE jobName='" + jobName + "' AND jobskyer.aphorismID=jobAphorism.aphorismID");
            dr.Read();
            string str = dr.GetValue(0).ToString();
            SqlConnection con = new SqlConnection();
            con = db.GetCon();
            con.Open();
            SqlCommand com = new SqlCommand(str, con);
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataSet ds = new DataSet();//using System.Data;
            da.Fill(ds);
            ds.Dispose();
            da.Dispose();
            if (ds != null)
            {
                AphorismRepeater.DataSource = ds;
                AphorismRepeater.DataBind();
            }
            con.Close();
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }
    protected void ImgbtnAphorism_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("JsCommonMainWrite.aspx");
    }
    protected void JobAphorismRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
    {

    }
}