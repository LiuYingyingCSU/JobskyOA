using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI.HtmlControls;



public partial class JsCommon_fileRepeater : System.Web.UI.UserControl
{
    DB db = new DB();
    public static int isUpload = 0;
    string fileGroup;
    string path;
    protected void Page_Load(object sender, EventArgs e)
    {
        if(Request.QueryString["fileGroup"]==null)
        {
            fileGroup = "美工组";
            path = Server.MapPath("/File/Program");
        }
        else
        {
            fileGroup = Request.QueryString["fileGroup"];
            path = Request.QueryString["filePath"];
            //Response.Write(path);
        }
        //加入css样式文件
        AddLinkedStyle("/CSS/FileArt.css");
        if(Session["jobName"]==null)
        {
            Response.Redirect("~/JsCommon/Login.aspx");
        }
        lblMessage.Visible = false;
        lblDelMess.Visible = false;
        if(!IsPostBack)
        {
            rptBind();
            DlistBind();
        }
        state();
    }
    protected void AddLinkedStyle(string url)
    {
        HtmlLink hLink = new HtmlLink();
        hLink.Attributes.Add("type", "text/css");
        hLink.Attributes.Add("rel", "stylesheet");
        hLink.Attributes.Add("href", url);
        Page.Header.Controls.Add(hLink);
    }
    public string GetPath(object fileName)
    {
        return path + fileName;
    }
    public string GetJobName(object jobskyerID)
    {
        
        SqlDataReader dr = db.reDr("SELECT jobName FROM jobskyer WHERE jobskyerID='" + jobskyerID + "'");
        dr.Read();
        return dr.GetValue(0).ToString();
    }
    private void fileUpload()
    {
        DateTime nowTime = DateTime.Now;
        string strFileName = fileUp.FileName;
        //string filePath = Server.MapPath("../File/Program/") + strFileName;
        string filePath = Server.MapPath(path) + strFileName;
        string sqlFU = "INSERT INTO files (fileName,jobskyerID,fileGroup,fileUpTime,filePath) values('" + strFileName + "','" + Session["jobskyerID"] + "','" + fileGroup.ToString() + "','" + nowTime.ToString() + "','" + strFileName + "')";
        isUpload = db.SqlEX(sqlFU);
        fileUp.SaveAs(filePath);
    }
    public void DlistBind()
    {
        SqlConnection myCon = db.GetCon();
        SqlCommand myCom = new SqlCommand();
        myCon.Open();
        myCom = new SqlCommand("select count(*) from files WHERE fileGroup='"+fileGroup+"'",myCon); //获得该组文件的总个数
        this.lbCount.Text = (Convert.ToInt32(myCom.ExecuteScalar()) / 4).ToString();  //算出总页数为DropdownList赋值
        int[] num = new int[Convert.ToInt32(lbCount.Text)];
        for(int i=1;i<=Convert.ToInt32(lbCount.Text);i++)
        {
            num[i - 1] = i;
        }
        DropDownList1.DataSource = num;
        DropDownList1.DataBind();
        myCom.Dispose();
        myCon.Dispose();
        myCon.Close();
    }
    protected void rptBind()
    {
        //DB db = new DB();
        try
        {
            int n = 4 * (Convert.ToInt32(lbPage.Text) - 1);
            SqlConnection myCon = db.GetCon();
            myCon.Open();
            string sqlstr = "SELECT TOP 4 fileUpTime,fileName,jobskyerID FROM files WHERE fileGroup='" + fileGroup + "' and fileID not in (select top (@n) fileID from FILES where fileGroup='" + fileGroup + "' order by fileID desc) order by fileID desc";
            SqlCommand mycom = new SqlCommand(sqlstr, myCon);
            mycom.Parameters.Add("n", n);
            SqlDataAdapter da = new SqlDataAdapter(mycom);
            DataSet ds = new DataSet();
            da.Fill(ds);
            ds.Dispose();
            da.Dispose();
            mycom.Dispose();
            myCon.Dispose();
            myCon.Close();
            if (ds != null)
            {
                this.Repeater1.DataSource = ds;
                this.Repeater1.DataBind();
            }
        }
        catch(Exception ee)
        {
            Response.Write(ee);
        }
        finally
        {  
            
        }
        
    }

    protected void btnFileUp_Click(object sender, EventArgs e)
    {
        try
        {
            if (fileUp.HasFile)
            {
                
                fileUpload();
                if (isUpload == 0)
                {
                    lblMessage.Visible = true;
                    lblMessage.Text = "文件上传失败";
                }
                else if (isUpload == 1)
                {
                    lblMessage.Visible = true;
                    lblMessage.Text = "文件上传成功";
                }
            }
            else
            {
                lblMessage.Visible=true;
                lblMessage.Text = "请选择文件";
            }
            rptBind();
        }
        catch(Exception ep)
        {
            lblMessage.Text = ep.ToString();
        }
        
    }
    protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "lbtnDelete")
        {
            
            int isDel = 0;
            string fileName = e.CommandArgument.ToString();
            //string filePath = Server.MapPath("../File/Program/") + fileName;
            string filePath = Server.MapPath(path) + fileName;
            
            FileInfo file = new FileInfo(filePath);//指定文件路径
            if (file.Exists)//判断文件是否存在
            {
                //Response.Write("1");
                file.Attributes = FileAttributes.Normal;//将文件属性设置为普通,比方说只读文件设置为普通
                file.Delete();//删除文件
                isDel = 1;
            }
            string deleteStr="DELETE FROM files WHERE fileName='"+fileName+"'";
            int isDeleteSql = db.SqlEX(deleteStr);
            if (isDeleteSql > 0)
            { 
               if(isDel==1)
               {
                   rptBind();
                   lblDelMess.Visible = true;
                   lblDelMess.Text = "删除成功";
               }
               else
               {
                   lblDelMess.Visible = true;
                   lblDelMess.Text = "删除失败";
                }
            }
            else
            {
                lblDelMess.Visible = true;
                lblDelMess.Text = "删除失败";
            }
        }
    }
    protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        LinkButton lbtnDelete = (LinkButton)e.Item.FindControl("lbtnDelete");
        Label lblName=(Label)e.Item.FindControl("lblName");
        if(lblName.Text.ToString().Trim()==Session["jobName"].ToString())
        {
            lbtnDelete.Visible=true;
        }
        else
        {
            lbtnDelete.Visible = false;
        }
    }
    public void state()
    {
        if (lbCount.Text.ToString() == "1" || lbCount.Text.ToString() == "0")
        {
            lbCount.Text = "1";
            lbtnGo.Enabled = false;
            lbtnFirst.Enabled = false;
            lbtnUp.Enabled = false;
            lbtnDown.Enabled = false;
            lbtnLast.Enabled = false;
        }
        else
        {
            if (lbPage.Text.ToString() == "1")
            {
                lbtnFirst.Enabled = false;
                lbtnUp.Enabled = false;
                lbtnDown.Enabled = true;
                lbtnLast.Enabled = true;
            }
            if (lbPage.Text.ToString() == lbCount.Text.ToString())
            {
                lbtnFirst.Enabled = true;
                lbtnUp.Enabled = true;
                lbtnDown.Enabled = false;
                lbtnLast.Enabled = false;
            }
            else if (Convert.ToInt32(lbPage.Text) > 1 && Convert.ToInt32(lbPage.Text) < Convert.ToInt32(lbCount.Text))
            {
                lbtnFirst.Enabled = true;
                lbtnUp.Enabled = true;
                lbtnDown.Enabled = true;
                lbtnLast.Enabled = true;
            }
        }
    }
    protected void lbtnFirst_Click(object sender, EventArgs e)
    {
        lbPage.Text = "1";
        rptBind();
        state();
    }
    protected void lbtnUp_Click(object sender, EventArgs e)
    {
        lbPage.Text = (Convert.ToInt32(lbPage.Text) - 1).ToString();
        rptBind();
        state();
    }
    protected void lbtnDown_Click(object sender, EventArgs e)
    {
        lbPage.Text = (Convert.ToInt32(lbPage.Text) + 1).ToString();
        rptBind();
        state();
    }
    protected void lbtnLast_Click(object sender, EventArgs e)
    {
        lbPage.Text = (Convert.ToInt32(lbCount.Text)).ToString();
        rptBind();
        state();
    }
    protected void lbtnGo_Click(object sender, EventArgs e)
    {
        lbPage.Text = DropDownList1.SelectedValue;
        rptBind();
        state();
    }
}
