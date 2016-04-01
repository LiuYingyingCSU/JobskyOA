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
    //string jobRole = "3";
    public static int role = 3;
    protected void Page_Load(object sender, EventArgs e)
    {
        lblFailText.Visible = false;
        lblUser.Visible = false;
        lblPwd.Visible = false;
        this.Common.ImageUrl = "../Image/Login/Common.png";
        this.Manage.ImageUrl = "../Image/Login/Manage.png";
        if (!IsPostBack)
        {
            
        }
        //Response.Write(jobRole);
    }

    //登录按钮
    protected void btnLogin_Click(object sender, ImageClickEventArgs e)
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
            //string role = "0";
            //role = jobRole.ToString();
            //Response.Write(role);
            try
            {
                DataTable dt = db.reDt("select jobskyerID,password,jobRole,jobName from jobskyer");
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
                            if (role.ToString() == "1")
                            {
                                //this.lblFailText.Text = "请以正确身份登录";
                                Response.Redirect("~/JsCommon/Login.aspx");
                                //this.lblFailText.Visible = true;
                            }
                            else if (role.ToString() == "0")
                            {
                                Response.Redirect("~/JsCommon/JsCommonMain.aspx?Name=" + jobName + ""); //跳转至一般用户界面，并传递参数，登录人员名称
                            }
                            else
                            {
                                //Response.Redirect("~/JsCommon/JsCommonMain.aspx?Name=" + jobName + "");
                                Response.Redirect("~/JsCommon/Login.aspx");
                                //lblFailText.Text = "请选择角色身份";
                                //this.lblFailText.Visible = true;
                            }
                        }
                        else if (Session["jobRole"].ToString() == "1")
                        {
                            if (role.ToString() == "1")
                            {
                                //Response.Write("管理员");
                                Response.Redirect("~/JsManager/JsManagerMain.aspx?Name=" + jobName + "");  //跳转至管理人员界面
                            }
                            else if (role.ToString() == "0")
                            {
                                //Response.Write("普通用户");
                                Response.Redirect("~/JsCommon/JsCommonMain.aspx?Name=" + jobName + "");  
                            }
                            else
                            {
                                //lblFailText.Text = "请选择角色身份";
                                //lblFailText.Visible = true;
                                Response.Redirect("~/JsCommon/Login.aspx");
                            }
                        }
                    }
                    else if (i == dt.Rows.Count - 1)
                    {
                        lblFailText.Visible = true;
                        lblFailText.Text = "用户名或密码错误";
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
    protected void Manage_Click(object sender, ImageClickEventArgs e)
    {
        this.Common.ImageUrl = "../Image/Login/Common.png";
        this.Manage.ImageUrl = "../Image/Login/Manage-01.png";
        role = 1;
    }

    protected void Common_Click(object sender, ImageClickEventArgs e)
    {
        this.Common.ImageUrl = "../Image/Login/Common-01.png";
        this.Manage.ImageUrl = "../Image/Login/Manage.png";
        role = 0;
    }
    
}