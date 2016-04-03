using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
//文件操作所以要引入名称空间IO和Text
using System.IO;
using System.Text;
using System.Data.OleDb;
//using JXSoft.TicketManage.Model; 
//using JXSoft.TicketManage.BLL;
using System.Text.RegularExpressions; 


    public partial class JsManager_DutyRecordExcel : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }
        protected void BindData()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("Name");
            for (int i = 0; i < 10; i++)
            {
                dt.Rows.Add(i.ToString(), i.ToString());
            }
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            Pager(this.DutyRecordGridView, this.DutyRecordAspNetPager, ds);
        }
        protected void Pager(GridView dl,Wuqi.Webdiyer.AspNetPager anp,System.Data.DataSet dst)
        {
            PagedDataSource pds = new PagedDataSource();
            pds.DataSource = dst.Tables[0].DefaultView;
            pds.AllowPaging = true;
            anp.RecordCount = dst.Tables[0].DefaultView.Count;
            pds.CurrentPageIndex = anp.CurrentPageIndex - 1;
            pds.PageSize = anp.PageSize;
            dl.DataSource = pds;
            dl.DataBind();
        }
        protected void DutyRecordAspNetPager_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
        {
            DutyRecordAspNetPager.CurrentPageIndex = e.NewPageIndex;
            BindData();
        }
        public void bind()
        {
            DB db = new DB();
            SqlConnection con = new SqlConnection();
            con = db.GetCon();
            string str = "select jobskyer.jobskyerID,jobskyer.jobName,myDutyTime.dutyInTime,myDutyTime.dutyOutTime,myDutyTime.flag0,myDutyTime.flag1 from jobskyer,myDutyTime where jobskyer.jobskyerID=myDutyTime.jobskyerID";
            SqlCommand com = new SqlCommand(str, con);
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataSet ds = new DataSet();
            da.Fill(ds);
            int k = ds.Tables[0].Rows.Count;
            for (int i = 0; i < k; i++)
            {
                string tojobskyerID = ds.Tables[0].Rows[i]["toJobskyerID"].ToString();
                string select = "SELECT jobName FROM JOBSKYER WHERE jobskyerID='" + tojobskyerID + "'";
                DB db1 = new DB();
                SqlConnection con1 = new SqlConnection();
                con1 = db1.GetCon();
                string str1 = "select jobName from jobskyer where jobskyerID='" + tojobskyerID + "'";
                SqlCommand com1 = new SqlCommand(str1, con1);
                SqlDataAdapter da1 = new SqlDataAdapter(com1);
                DataSet ds1 = new DataSet();
                da1.Fill(ds1);
                string jobname = ds1.Tables[0].Rows[0]["jobName"].ToString();
                ds.Tables[0].Rows[i]["jobName"] = jobname;
                if (i == k)
                {
                    com1.Dispose();
                    con1.Close();
                }
            }
            try
            {
                DutyRecordGridView.DataSource = ds;
                DutyRecordGridView.DataKeyNames = new string[] { "jobskyerID" };
                DutyRecordGridView.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            finally
            {
                com.Dispose();
                con.Close();
            }

        }
        //如果没有下面方法会报错类型“GridView”的控件“GridView1”必须放在具有 runat=server 的窗体标记内
        public override void VerifyRenderingInServerForm(Control control)
        {

        }
        protected void DutyRecordGridView_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void ToExcel_Click(object sender, EventArgs e)
        {
            Export("application/ms-excel", "值班记录表.xls");
        }
        private void Export(string FileType, string FileName)
        {
            try
            {
                Response.Charset = "GB2312";
                Response.ContentEncoding = System.Text.Encoding.UTF7;
                Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(FileName, Encoding.UTF8).ToString());
                Response.ContentType = FileType;
                this.EnableViewState = false;
                FileStream fs = new FileStream("~/", FileMode.Open);
                StreamWriter tw = new StreamWriter(fs);
                HtmlTextWriter hw = new HtmlTextWriter(tw);
                DutyRecordGridView.RenderControl(hw);
                Response.Write(tw.ToString());
                Response.End();
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
        private DataSet CreateDataSource()
        {
            string strCon;
            strCon = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Server.MapPath("excel.xls") + ";Extended Properties=Excel 8.0;";
            OleDbConnection olecon = new OleDbConnection(strCon);//using System.Data.OleDb;
            OleDbDataAdapter myda = new OleDbDataAdapter("SELECT * FROM[Sheet1$]", strCon);
            DataSet myds = new DataSet();
            return myds;
        }
        protected void FromExcel_Click(object sender, EventArgs e)
        {
            DutyRecordGridView.DataSource = CreateDataSource();
            DutyRecordGridView.DataBind();
        }

        protected void DutyRecordGridView_RowDeleted(object sender, GridViewDeletedEventArgs e)
        {
            DB db = new DB();
            SqlConnection con = new SqlConnection();
            con = db.GetCon();
            string str = "delete from myDutyTime where jobskyerID='" + DutyRecordGridView.DataKeyNames.ToString() + "'";//DutyRecordGridView.DataKeys[e.RowIndex].Value.ToString()
            SqlCommand com = new SqlCommand(str, con);
            try
            {
                con.Open();
                com.ExecuteNonQuery();
                con.Close();
                bind();
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
        protected void toDutyRecord_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/JsInfo/DutyTable.aspx");
        }
        protected void toManagerDutyTime_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/JsManager/ManagerDutyTime.aspx");
        }
}

