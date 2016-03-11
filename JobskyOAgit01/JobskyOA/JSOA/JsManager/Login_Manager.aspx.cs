using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class JSOA_JsManager_Login_Manager : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //if(this.login.)
        lblFailText.Visible = false;
        lblUser.Visible = false;
        lblPwd.Visible = false;
        if (!this.IsPostBack)
        {
            Response.Write("正在提交，请稍候！");
        }
        if (this.IsPostBack)
        {
            Response.Write("请点击提交！");
        }
    }
    /// <summary>
    /// 登录身份切换
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="img"></param>
    protected void ClickButton_Manager(object sender, ImageClickEventArgs img)
    {
        if (this.FindControl("ButtonManager") != null && this.FindControl("ButtonEmployee") != null)
        {
            ImageButton ReplaceButton_Manager = this.FindControl("ButtonManager");
            ReplaceButton_Manager.ImageUrl = "~/Image/login/Login_Manager.jpg";
            ImageButton ReplaceButton_Manager = this.FindControl("ButtonEmployee");
            ReplaceButton_Manager.ImageUrl = "~/Image/login/Login_Jobskyer.jpg";
        }
    }

    protected void ClickButton_Employee(object sender, ImageClickEventArgs img)
    {
        if (this.FindControl("ButtonManager") != null && this.FindControl("ButtonEmployee") != null)
        {
            ImageButton ReplaceButton_Manager = this.FindControl("ButtonManager");
            ReplaceButton_Manager.ImageUrl = "~/Image/login/Login_Manager.jpg";
            ImageButton ReplaceButton_Manager = this.FindControl("ButtonEmployee");
            ReplaceButton_Manager.ImageUrl = "~/Image/login/Login_Jobskyer.jpg";
        }
    }
    /// <summary>
    /// 管理员登录验证
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Login_Authenticate_Manager(object sender, ImageClickEventArgs e)
    {
        lblFailText.Visible = false;
        if (String.IsNullOrEmpty(UserName.Text))//是空或者含有空字符
        {

            lblUser.Visible = true;
        }
        else if (String.IsNullOrEmpty(Password.Text))
        {
            lblPwd.Visible = true;
        }
        else
        {
            HttpCookie ID = new HttpCookie("jobskyerID");
            HttpCookie Name = new HttpCookie("jobskyerName");
            DB db = new DB();
            string jobskyerID = this.UserName.Text.Trim();
            string passWord = this.Password.Text.Trim();
            try
            {
                DataTable dt = db.reDt("select jobskyerID,password,jobRole,jobName from JOBSKYER");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["jobskyerID"].ToString == jobskyerID && dt.Rows[i]["password"].ToString == passWord)
                    {
                        Response.Cookies["jobsyerID"] = dt.Rows[i]["jobskyerID"];
                        Response.Cookies["jobsyerID"].Expires = DateTime.MinValue;
                        Response.Cookies["jobskyerName"] = dt.Rows[i]["jobskyerName"];
                        Response.Cookies["jobskyerName"].Expires = DateTime.MinValue;
                        Response.Cookies["jobRole"] = dt.Rows[i]["jobRole"];//用cookie把用户数据保存在客户端
                        Response.Cookies["jobRole"].Expires = DateTime.MinValue;
                        FormsAuthentication.RedirectFromLoginPage(Cookie["jobskyerID"], false);
                        string jobName = dt.Rows[i]["jobName"].ToString();
                        if (Response.Cookies["jobRole"] != 1)
                        {
                            Response.Write("您没有管理员权限，抱歉！");
                            Response.Redirect("~/JsCommon/login.aspx");
                        }
                        else if (Response.Cookies["jobRole"] == 1)
                        {
                            Response.Redirect("~/JsManager/Main_Manager.aspx?Name=" + jobName + "");//查询字符串，跳到相应管理员首页
                        }
                        else if (i == dt.Rows.Count - 1)//查询完毕
                        {
                            lblFailText.Visible = true;
                            lblFailText.Text = "用户名或密码错误";
                        }
                     }
            
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
            }
        }
    }
}