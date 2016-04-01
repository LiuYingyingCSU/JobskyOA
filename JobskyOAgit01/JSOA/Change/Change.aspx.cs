using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class Change_Change : System.Web.UI.Page
{
    public string imgUrl = "";
    DB db = new DB();
    public static int isUpload = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            getImage();
            if (Session["jobskyerID"] == null)
            {
                //Response.Write("<script>alert('登录过期，请尝试重新登录！')</script>");
                Response.Redirect("~/JsCommon/Login.aspx");
            }
        }
    }
    private void fileUpload()
    {
        //DateTime nowTime = DateTime.Now;
        string strFileName = imageUp.FileName;
        //string filePath = Server.MapPath("../File/Program/") + strFileName;
        string imgPath = Server.MapPath("../Change/Picture/") + strFileName;
        SqlConnection sqlCon = db.GetCon();
        sqlCon.Open();
        string sqlImg = "UPDATE jobskyer SET jobImage=@jobImage WHERE jobskyerID=@jobskyerID";
        SqlCommand sqlCom = new SqlCommand(sqlImg, sqlCon);
        sqlCom.Parameters.Add("@jobImage", SqlDbType.NText, 0).Value = strFileName;
        sqlCom.Parameters.Add("@jobskyerID", SqlDbType.NChar, 0).Value=Session["jobskyerID"];
        isUpload = sqlCom.ExecuteNonQuery();
        sqlCon.Close();
        //string sqlFU = "INSERT INTO files (fileName,jobskyerID,fileGroup,fileUpTime,filePath) values('" + strFileName + "','" + Session["jobskyerID"] + "','" + fileGroup.ToString() + "','" + nowTime.ToString() + "','" + strFileName + "')";
        //isUpload = db.SqlEX(sqlFU);
        imageUp.SaveAs(imgPath);
    }
    public string getImage()
    {
        SqlDataReader dr0 = db.reDr("select jobImage from jobskyer where jobskyerID='" + Session["jobskyerID"] + "'");
        dr0.Read();
        if (dr0.HasRows)
        {
            imgUrl = "../Change/Picture/"+dr0["jobImage"].ToString();
        }
        else
        {
            imgUrl = "../Change/Picture/Business.jpg";
        }
        dr0.Close();
        return imgUrl;
    }
    public void submitNewPwd()
    {
        SqlDataReader dr2 = db.reDr("select password from jobskyer where jobskyerID='" + Session["jobskyerID"] + "'");
        dr2.Read();
        string pwd = this.tbPwd.Text.ToString();
        string newPwd = this.tbNewPwd.Text.ToString();
        string cfmPwd = this.tbConfirmPwd.Text.ToString();
        if (dr2.HasRows)
        {
            if (pwd.ToString() != dr2["password"].ToString())
            {
                lblError.Visible = true;
                lblError.Text = "原密码输入错误";
            }
            else if (pwd.ToString() == dr2["password"].ToString() && newPwd.ToString() == cfmPwd.ToString())
            {
                SqlConnection myCon = db.GetCon();
                myCon.Open();
                string sqlpwd = "UPDATE jobskyer SET password=@password WHERE jobskyerID=@jobskyerID";
                SqlCommand myCom = new SqlCommand(sqlpwd,myCon);
                myCom.Parameters.Add("@password",SqlDbType.NChar,0).Value =newPwd;
                myCom.Parameters.Add("@jobskyerID", SqlDbType.NChar, 0).Value=Session["jobskyerID"];
                int isSubmit;
                isSubmit = myCom.ExecuteNonQuery();
                if (isSubmit == 1)
                {
                    lblError.Visible = true;
                    lblError.Text = "密码修改成功";
                }
                else
                {
                    lblError.Visible = true;
                    lblError.Text = "密码修改失败"+isSubmit;
                }
            }
            else
            {
                lblError.Visible = true;
                lblError.Text = "请确认两次输入密码相同";
            }
        }
    }
    protected void btnImgUp_Click(object sender, EventArgs e)
    {
        try
        {
            if (imageUp.HasFile)
            {
                string imgFormat = (this.imageUp.FileName.Substring(this.imageUp.FileName.LastIndexOf("."))).ToLower();
                if (imgFormat == ".jpg" || imgFormat == ".png" || imgFormat == ".gif")
                {
                    fileUpload();
                    if (isUpload == 0)
                    {
                        lblFail.Visible = true;
                        lblFail.Text = "头像更换失败";
                    }
                    else if (isUpload == 1)
                    {
                        lblFail.Visible = true;
                        lblFail.Text = "头像更换成功";
                    }
                }
                else
                {
                    lblFail.Visible = true;
                    lblFail.Text = "请选择正确的图片文件格式，正确格式：.jpg  .png  .gif";

                }
                
            }
            else
            {
                lblFail.Visible = true;
                lblFail.Text = "请选择图片！";
            }
            getImage();
            
        }
        catch (Exception ep)
        {
            lblFail.Text = ep.ToString();
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        submitNewPwd();
        getImage();
    }
}