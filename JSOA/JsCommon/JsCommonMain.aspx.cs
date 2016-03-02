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
    protected void Page_Load(object sender, EventArgs e)
    {
        rptBind();
        if(!IsPostBack)
        {
            this.Master.btnMain.ImageUrl = "~/Image/Button/Main.PNG";
        }
        //判断用户是否已登录
         DB db = new DB();
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
                SqlDataReader dr1 = db.reDr("select jobAcademy,jobPosition,jobCount from jobskyer where jobName='" + jobName + "'");
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

        string imgUrl;
        //imgUrl=           默认头像
        //jobskyerID = Session["jobskyerID"].ToString();
        SqlDataReader dr2 = db.reDr("SELECT imageAddress FROM JOBSKYER WHERE jobName='" +jobName + "'");
        dr2.Read();
        imgUrl = dr2.GetValue(0).ToString().Trim();
        ImgbtnProfile.ImageUrl = imgUrl;
        dr2.Close();
    }
    public string GetJobName(object jobskyerID)             //把jobskyerID转化为jobName输出
    {
        DB db = new DB();
        SqlDataReader dr = db.reDr("SELECT jobName FROM JOBSKYER WHERE jobskyerID='" + jobskyerID + "'");
        dr.Read();
        return dr.GetValue(0).ToString();
    }
    protected void rptBind()        //repeater数据绑定
    {
        DB db = new DB();
        //int n = 4 * (Convert.ToInt32(lbPage.Text) - 1);
        SqlConnection myCon = db.GetCon();
        myCon.Open();
        string sqlstr = "SELECT TOP 3 jobskyerID,notTime,notTitle,notContent FROM NOTICE order by noticeID desc";
        SqlCommand mycom = new SqlCommand(sqlstr, myCon);
        //mycom.Parameters.Add("n", n);
        SqlDataAdapter da = new SqlDataAdapter(mycom);
        DataSet ds = new DataSet();
        da.Fill(ds);
        ds.Dispose();
        da.Dispose();
        myCon.Close();
        if (ds != null)
        {
            this.Repeater1.DataSource = ds;
            this.Repeater1.DataBind();
        }
    }
}