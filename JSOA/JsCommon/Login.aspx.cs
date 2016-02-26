using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;


public partial class JsCommon_Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    //登录按钮事件
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        if (txtJobskyID.Text != "" && txtPwd.Text != "")
        {
            DB db = new DB();
            string jobskyerID = this.txtJobskyID.Text.Trim();
            string passWord = this.txtPwd.Text.Trim();
            SqlDataReader dr = db.reDr("select * from jobskyer where jobskyerID='" + jobskyerID + "'and password='" + passWord + "'");
            dr.Read();
            if (dr.HasRows)                                             //通过dr中是否包含判断用户是否通过身份验证
            {
                Session["jobskyerID"] = dr.GetValue(0);                //将用户ID存入Session["jobskyerID"]中
                Session["jobRole"] = dr.GetValue(11);                  //将用户权限存入Session["jobRole"]中
                Session["jobName"] = dr.GetValue(2);                   //将用户名字存入Session["jobName"]中
                string jobName = dr.GetValue(2).ToString();
                if (Session["jobRole"].ToString() == "0")
                {
                    Response.Redirect("~/JsCommon/JsCommonMain.aspx?Name=" + jobName + ""); //跳转至一般用户界面，并传递参数，登录人员名称
                }
                else if (Session["jobRole"].ToString() == "1")
                {
                    Response.Redirect("~/JsManagerMain.aspx?Name=" + jobName + "");  //跳转至管理人员界面
                }
                else
                {
                    Response.Redirect("~/JsSuperMain.aspx?Name=" + jobName + "");  //跳转至超级用户界面
                }
            }
            else
            {
                Response.Write("<script>alert('学号或密码错误');location.href='Login.aspx';</script>");
            }
            dr.Close();
        }
        else
        {
            Response.Write("<script>alert('学号和密码不能为空');location.href = 'Login.aspx';</script>");
            return;
        }
        
    }
}