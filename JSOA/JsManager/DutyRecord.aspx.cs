using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class JsManager_DutyRecord : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
            printAll();
            name_dropListBind();
       
       
    }
    
    public bool name_dropListBind()
    {
        try
        {
            DB db = new DB();
            string str = "SELECT dutyTimeTable.jobskyerID,jobName FROM dutyTimeTable,jobskyer WHERE dutyTimeTable.jobskyerID=jobskyer.jobskyerID";
            SqlConnection con = new SqlConnection();
            con = db.GetCon();
            con.Open();
            SqlCommand com = new SqlCommand(str, con);
            SqlDataReader dr = com.ExecuteReader();      
            name_dropList.DataSource = dr;
            name_dropList.DataTextField = "jobName";
            name_dropList.DataBind();
            name_dropList.Items.Add(new ListItem("请选择", "-1"));
            name_dropList.SelectedValue = "-1";
            com.Dispose();
            con.Close();
        }
        catch (Exception ex)
        {
            return false;
        }
        
        return true;
    }
    protected void Delete_Click(object sender, EventArgs e)
    {
        string jobskyerID = name_dropList.SelectedValue;
        string str = "DELETE FROM myDutyTime WHERE jobskyerID='" + jobskyerID + "'";
        DB db = new DB();
        SqlConnection con = new SqlConnection();
        con = db.GetCon();
        con.Open();
        SqlCommand com = new SqlCommand(str, con);
        try
        {
            com.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Response.Write("<script>alert('删除失败！');location='DutyRecord.aspx'</script>");
        }
        finally
        {
            com.Dispose();
            con.Close();
        }
    }
    protected void Search_Click(object sender, EventArgs e)
    {
        string jobskyerID = name_dropList.SelectedValue;
        ///根据值重新绑定表单
        DB db1 = new DB();
        SqlConnection con1 = new SqlConnection();
        con1 = db1.GetCon();
        con1.Open();
        string str1 = "SELECT myDutyTime.jobskyerID,dutyRecordID,DATENAME(WEEKDAY,dutyInTime)+CONVERT(varchar(12),dutyInTime,108) AS MyDutyInTime,DATENAME(WEEKDAY,dutyOutTime)+CONVERT(varchar(12),dutyOutTime,108) AS MyDutyOutTime,jobName FROM myDutyTime,jobskyer WHERE myDutyTime.jobskyerID=JOBSKYER.jobskyerID AND myDutyTime.jobskyerID='" + jobskyerID + "'";
        SqlCommand com1 = new SqlCommand(str1, con1);
        SqlDataReader da1 = com1.ExecuteReader();
        try
        {
            DutyRecord_repeater.DataSource = da1;
            DutyRecord_repeater.DataBind();
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
        finally
        {
            com1.Dispose();
            con1.Close();
        }

        DB db = new DB();
        SqlConnection con = new SqlConnection();
        con = db.GetCon();
        con.Open();
        string str = "SELECT dutyRecordID,flag0,flag1 FROM myDutyTime WHERE myDutyTime.jobskyerID='" + jobskyerID + "'";
        SqlCommand com = new SqlCommand(str, con);
        SqlDataAdapter da = new SqlDataAdapter(com);
        DataSet ds = new DataSet();
        da.Fill(ds);
        int k = ds.Tables[0].Rows.Count;
        for (int i = 0; i < k; ++i)
        {
            switch (ds.Tables[0].Rows[i]["flag1"].ToString())
            {
                case "0":
                    {
                        ds.Tables[0].Rows[i]["flag1"] = "准时签退";
                        break;
                    }
                case "1":
                    {
                        ds.Tables[0].Rows[i]["flag1"] = "早退";
                        break;
                    }
                case "2":
                    {
                        ds.Tables[0].Rows[i]["flag1"] = "未签退";
                        break;
                    }
            }
            switch (ds.Tables[0].Rows[i]["flag0"].ToString())
            {
                case "0":
                    {
                        ds.Tables[0].Rows[i]["flag0"] = "准时签到";
                        break;
                    }
                case "1":
                    {
                        ds.Tables[0].Rows[i]["flag0"] = "迟到";
                        break;
                    }
                case "2":
                    {
                        ds.Tables[0].Rows[i]["flag0"] = "未签到";
                        ds.Tables[0].Rows[i]["flag1"] = "未签退";
                        break;
                    }
            }

        }
        try
        {
            Duty_state.DataSource = ds;
            Duty_state.DataBind();
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

        DB db2 = new DB();
        SqlConnection con2 = new SqlConnection();
        con2 = db2.GetCon();
        string str2 = "SELECT dutyRecordID,toJobskyerID,flag0 AS toName,jobskyerID FROM myDutyTime WHERE jobskyerID='" + jobskyerID + "'";
        SqlCommand com2 = new SqlCommand(str2, con2);
        SqlDataAdapter da2 = new SqlDataAdapter(com2);
        DataSet ds2 = new DataSet();
        da2.Fill(ds2);
        int m = ds2.Tables[0].Rows.Count;
        for (int i = 0; i < m; ++i)
        {
            string tojobskyerID = ds.Tables[0].Rows[i]["toJobskyerID"].ToString();
            string select = "SELECT jobName FROM JOBSKYER WHERE jobskyerID='" + tojobskyerID + "'";
            DB db3 = new DB();
            SqlConnection conn = new SqlConnection();
            conn = db3.GetCon();
            conn.Open();
            SqlCommand comm = new SqlCommand(select, conn);
            SqlDataAdapter da3 = new SqlDataAdapter(comm);
            DataSet ds3 = new DataSet();
            da3.Fill(ds3);
            string jobname = ds3.Tables[0].Rows[0]["jobName"].ToString();
            ds3.Tables[0].Rows[i]["jobName"] = jobname;
            if (i == m)
            {
                comm.Dispose();
                conn.Close();
            }
        }
        try
        {
            Replace_Name.DataSource = ds2;
            Replace_Name.DataBind();
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);

        }
        finally
        {
            com2.Dispose();
            con2.Close();
        }
    }
    public bool printAll()
    {
        try
        {
            repeater_name_Bind();
            state_Bind();
            replace_Bind();
        }
        catch (Exception ex)
        {
            Response.Write("错误！");
            return false;
        }
        return true;
    }
    public bool repeater_name_Bind()
    {
        DB db = new DB();
        SqlConnection con = new SqlConnection();
        con = db.GetCon();
        con.Open();
        string str = "SELECT MY_DUTY_TIME.jobskyerID,dutyRecordID,DATENAME(WEEKDAY,dutyInTime)+CONVERT(varchar(12),dutyInTime,108) AS MyDutyInTime,DATENAME(WEEKDAY,dutyOutTime)+CONVERT(varchar(12),dutyOutTime,108) AS MyDutyOutTime,jobName FROM MY_DUTY_TIME,JOBSKYER WHERE MY_DUTY_TIME.jobskyerID=JOBSKYER.jobskyerID";
        SqlCommand com = new SqlCommand(str, con);
        SqlDataReader da = com.ExecuteReader();
        try
        {
            DutyRecord_repeater.DataSource = da;
            DutyRecord_repeater.DataBind();
        }
        catch (Exception ex)
        {
            //Response.Write("名字绑定错误！");
            return false;
        }
        finally
        {
            com.Dispose();
            con.Close();
        }
        return true;
    }
    public bool state_Bind()
    {
        DB db = new DB();
        SqlConnection con = new SqlConnection();
        con = db.GetCon();
        con.Open();
        string str = "SELECT dutyRecordID,flag0,flag1 FROM MY_DUTY_TIME";
        SqlCommand com = new SqlCommand(str, con);
        SqlDataAdapter da = new SqlDataAdapter(com);
        DataSet ds = new DataSet();
        da.Fill(ds);
        int k = ds.Tables[0].Rows.Count;
        for (int i = 0; i < k; ++i)
        {
            switch (ds.Tables[0].Rows[i]["flag1"].ToString())
            {
                case "0":
                    {
                        ds.Tables[0].Rows[i]["flag1"] = "准时签退";
                        break;
                    }
                case "1":
                    {
                        ds.Tables[0].Rows[i]["flag1"] = "早退";
                        break;
                    }
                case "2":
                    {
                        ds.Tables[0].Rows[i]["flag1"] = "未签退";
                        break;
                    }
            }
            switch (ds.Tables[0].Rows[i]["flag0"].ToString())
            {
                case "0":
                    {
                        ds.Tables[0].Rows[i]["flag0"] = "准时签到";
                        break;
                    }
                case "1":
                    {
                        ds.Tables[0].Rows[i]["flag0"] = "迟到";
                        break;
                    }
                case "2":
                    {
                        ds.Tables[0].Rows[i]["flag0"] = "未签到";
                        ds.Tables[0].Rows[i]["flag1"] = "未签退";
                        break;
                    }
            }

        }
        try
        {
            Duty_state.DataSource = ds;
            Duty_state.DataBind();
        }
        catch (Exception ex)
        {
            //Response.Write("状态绑定错误！");
            return false;
        }
        finally
        {
            com.Dispose();
            con.Close();
        }
        return true;
    }
    public bool replace_Bind()
    {
        DB db = new DB();
        SqlConnection con = new SqlConnection();
        con = db.GetCon();
        string str = "SELECT dutyRecordID,toJobskyerID,flag0 AS toName,jobskyerID FROM myDutyTime";
        SqlCommand com = new SqlCommand(str, con);
        SqlDataAdapter da = new SqlDataAdapter(com);
        DataSet ds = new DataSet();
        da.Fill(ds);
        int k = ds.Tables[0].Rows.Count;
        for (int i = 0; i < k; ++i)
        {
            string tojobskyerID = ds.Tables[0].Rows[i]["toJobskyerID"].ToString();
            string select = "SELECT jobName FROM JOBSKYER WHERE jobskyerID='" + tojobskyerID + "'";
            DB db1 = new DB();
            SqlConnection conn = new SqlConnection();
            conn = db1.GetCon();
            conn.Open();
            SqlCommand comm = new SqlCommand(select, conn);
            SqlDataAdapter da1 = new SqlDataAdapter(comm);
            DataSet ds1 = new DataSet();
            da1.Fill(ds1);
            string jobname = ds1.Tables[0].Rows[0]["jobName"].ToString();
            ds.Tables[0].Rows[i]["jobName"] = jobname;
            if (i == k)
            {
                comm.Dispose();
                conn.Close();
            }
        }
        try
        {
            Replace_Name.DataSource = ds;
            Replace_Name.DataBind();
        }
        catch (Exception ex)
        {
            //Response.Write("代班绑定错误！");
            return false;
        }
        finally
        {
            com.Dispose();
            con.Close();
        }
        return true;
    }
    protected void Replace_Name_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        string id = e.CommandArgument.ToString();
        if (e.CommandName == "delete")
        {
            string str = "DELETE FROM MY_DUTY_TIME WHERE dutyRecordID='" + id + "'";
            DB db = new DB();
            SqlConnection con = new SqlConnection();
            con = db.GetCon();
            SqlCommand com = new SqlCommand(str, con);
            try
            {
                com.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            Response.Write("<script>alert('删除成功.');location='DutyRecord.aspx'</script>");
            con.Close();
        }
    }
    protected void toDutyRecord_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/JsInfo/DutyTable.aspx");
    }
   
    protected void Duty_state_ItemCommand(object source, RepeaterCommandEventArgs e)
    {

    }
    protected void toManagerDutyTime_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/JsManager/ManagerDutyTime.aspx");
    }
    protected void DutyRecord_repeater_ItemCommand(object source, RepeaterCommandEventArgs e)
    {

    }
    protected void name_dropList_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}