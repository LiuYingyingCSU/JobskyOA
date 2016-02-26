using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.UI.HtmlControls;

public partial class JsCommon_JsCommonMain : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            this.Master.btnMain.ImageUrl = "~/Image/Sign/SignOutTo.PNG";
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
                    lblCount.Text = dr1.GetValue(2).ToString();
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
      
}