using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.Security;
using System.Data;


public partial class JsCommon_Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblFailText.Visible = false;
        lblUser.Visible = false;
        lblPwd.Visible = false;
        if (!IsPostBack)
        {

        }
    }
    //登录按钮
    protected void btnLogin_Click1(object sender, ImageClickEventArgs e)
    {
        lblFailText.Visible = false;
        if (String.IsNullOrEmpty(UserName.Text))
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
            HttpCookie Name = new HttpCookie("jobName");
            if (RememberMe.Checked == true)
            {

                ID.Values.Add("UserID", UserName.Text.Trim());
                Name.Values.Add("UserName", Password.Text.Trim());
                Response.Cookies.Add(ID);
                Response.Cookies.Add(Name);
                ID.Expires = DateTime.Now.AddMonths(6);
                Name.Expires = DateTime.Now.AddMonths(6);
            }
            DB db = new DB();
            string jobskyerID = this.UserName.Text.Trim();
            string passWord = this.Password.Text.Trim();
            try
            {
                DataTable dt = db.reDt("select jobskyerID,password,jobRole,jobName from JOBSKYER");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["jobskyerID"].ToString() == jobskyerID && dt.Rows[i]["password"].ToString() == passWord)
                    {
                        Session["jobskyerID"] = dt.Rows[i]["jobskyerID"];                //将用户ID存入Session["jobskyerID"]
                        Session["jobRole"] = dt.Rows[i]["jobRole"];                  //将用户权限存入Session["jobRole"]中
                        Session["jobName"] = dt.Rows[i]["jobName"];
                        FormsAuthentication.RedirectFromLoginPage(Session["jobName"].ToString(), false);
                        string jobName = dt.Rows[i]["jobName"].ToString();
                        if (Session["jobRole"].ToString() == "0")
                        {
                            Response.Redirect("~/JsCommon/JsCommonMain.aspx?Name=" + jobName + ""); //跳转至一般用户界面，并传递参数，登录人员名称
                        }
                        else if (Session["jobRole"].ToString() == "1")
                        {
                            Response.Redirect("~/JsManagerMain.aspx?Name=" + jobName + "");  //跳转至管理人员界面
                        }
                        else if (Session["jobRole"].ToString() == "2")
                        {
                            Response.Redirect("~/JsSuperMain.aspx?Name=" + jobName + "");  //跳转至超级用户界面
                        }
                        //break;
                    }
                    else if (i == dt.Rows.Count - 1)
                    {
                        lblFailText.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
            }
            finally
            {

            }
        }
    }
}