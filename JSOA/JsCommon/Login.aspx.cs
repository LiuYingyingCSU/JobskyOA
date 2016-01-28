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
    //登录按钮
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        DB db = new DB();
        string jobskyerID = this.txtJobskyID.Text.Trim();
        string passWord = this.txtPwd.Text.Trim();      //对密码进行加密处理
        SqlDataReader dr = db.reDr("select * from jobskyer where jobskyerID='"+jobskyerID+"'and password='"+passWord+"'");
        dr.Read();
        if(dr.HasRows)                                             //通过dr中是否包含判断用户是否通过身份验证
        {
            Session["jobskyerID"] = dr.GetValue(0);                //将用户ID存入Session["UserID"]
            Session["jobRole"] = dr.GetValue(4);                   //将用户权限存入Session["jobRole"]中
            Response.Write("登录成功");
            //Response.Redirect("~/jobskymain.aspx");                //登录成功,进入主页面
        }
        else
        {
            //Response.Write("<script>alter('登录失败！请返回查找原因');location='Login.aspx'</script>");
            Response.Write("登录失败！请返回重新登录");//此处需改
            //Response.Redirect("~/Login.aspx");
        }
        dr.Close();
    }
}