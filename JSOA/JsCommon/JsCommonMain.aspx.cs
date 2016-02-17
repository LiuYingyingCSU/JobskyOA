using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class JsCommon_JsCommonMain : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
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
                //积分排名如何实现
                //SqlDataReader dr2 = db.reDr("SELECT jobName,jobCount from JOBSKYER order by jobCount DESC");
                dr1.Read();
                //dr2.Read();
               //while(dr2.HasRows)
                //{使用dr2,进入浏览器时，内存迅速增加，异常提示没有足够的内存空间
                    //int i=0;
                    //int j=0;
                    //Response.Write("<script>alert('"+dr2.GetValue(0)+"')</script>");
                    //if()
                //}
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
        Response.Write(imgUrl);
        ImgbtnProfile.ImageUrl = imgUrl;
        dr2.Close();
    }
   
}