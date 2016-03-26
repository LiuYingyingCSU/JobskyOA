using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class JsManager_TimeTable_Manager : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ///姓名下拉列表绑定
        ChooseNameBind();
        ///每人的值班时间表数据绑定
        TimeList_Bind();
    }

    protected void confirm_Click(object sender, EventArgs e)///首先判断该人员是否已经安排过值班
    {
        DB db = new DB();
        string str = "SELECT jobskyerID FROM DUTY_TIME";
        string jobskyerID = chooseName.SelectedValue;
        SqlConnection con = new SqlConnection();
        con = db.GetCon();
        SqlCommand com = new SqlCommand(str, con);
        SqlDataAdapter da = new SqlDataAdapter(com);
        DataSet ds = new DataSet();
        da.Fill(ds);
        ///SqlDataReader dr = com.ExecuteReader();
        int k = ds.Tables[0].Rows.Count;
        for (int i = 0; i < k; ++i)
        {
            if (jobskyerID == ds.Tables[0].Rows[i]["jobskyerID"].ToString())
            {
                Update_DutyTime();
            }
            if (i == k)
            {
                Insert_DutyTime();
            }
        }
    }
    public bool Insert_DutyTime()///如果选择了没有安排过值班的人，就插入值班安排数据
    {
        string dutyInTime = chooseWeekday.SelectedValue + chooseSignInTime.SelectedValue;
        string dutyOutTime = chooseWeekday.SelectedValue + chooseSignOutTime.SelectedValue;
        string InsertStr = "INSERT INTO DUTY_TIME(dutyInTime,dutyOutTime,jobskyerID)VALUES(@dutyInTime,@dutyOutTime,@jobskyerID)";
        DB db = new DB();
        SqlConnection con = new SqlConnection();
        con = db.GetCon();
        con.Open();
        SqlCommand com = new SqlCommand(InsertStr, con);
        com.Parameters.Add("@dutyInTime", SqlDbType.DateTime, 0).Value = Convert.ToDateTime(dutyInTime);
        com.Parameters.Add("@dutyOutTime", SqlDbType.DateTime, 0).Value = Convert.ToDateTime(dutyOutTime);
        com.Parameters.Add("@jobskyerID", SqlDbType.NChar, 0).Value = chooseName.SelectedValue;
        try
        {
            com.ExecuteNonQuery();
            Sign_Result.Text = "安排值班成功！";
        }
        catch (Exception ex)
        {
            Sign_Result.Text = "安排值班失败！";
            return false;
        }
        finally
        {
            com.Dispose();
            con.Close();
        }
        return true;
    }
    public bool Update_DutyTime()///如果选择了安排过值班的人，就更新值班安排数据
    {
        DB db = new DB();
        string dutyInTime = chooseWeekday.SelectedValue + chooseSignInTime.SelectedValue;
        string dutyOutTime = chooseWeekday.SelectedValue + chooseSignOutTime.SelectedValue;
        string str = "UPDATE DUTY_TIME SET dutyInTime=@dutyInTime,dutyOutTime=@dutyOutTime WHERE jobskyerID=@jobskyerID";
        SqlConnection con = new SqlConnection();
        con = db.GetCon();
        SqlCommand com = new SqlCommand(str, con);
        com.Parameters.Add("@dutyInTime", SqlDbType.DateTime, 0).Value = dutyInTime;
        com.Parameters.Add("@dutyOutTime", SqlDbType.DateTime, 0).Value = dutyOutTime;
        com.Parameters.Add("@jobskyerID", SqlDbType.DateTime, 0).Value = chooseName.SelectedValue;
        try
        {
            com.ExecuteNonQuery();
            Sign_Result.Text = "更新值班成功！";
        }
        catch (Exception ex)
        {
            Sign_Result.Text = "更新值班失败！";
            return false;
        }
        finally
        {
            com.Dispose();
            con.Close();
        }
        return true;
    }

    public bool ChooseNameBind()
    {
        DB db = new DB();
        string chooseName_str = "SELECT jobskyerID,jobName FROM JOBSKYER";
        SqlConnection con = db.GetCon();
        con.Open();
        SqlCommand com = new SqlCommand(chooseName_str, con);
        SqlDataReader chooseName_read = com.ExecuteReader();
        try
        {
            chooseName.DataSource = chooseName_read;
            chooseName.DataTextField = "jobName";
            chooseName.DataValueField = "jobskyerID";
            chooseName.DataBind();
            chooseName.Items.Add(new ListItem("请选择", "-1"));
            chooseName.SelectedValue = "-1";
        }
        catch (Exception ex)
        {
            return false;
        }
        finally
        {
            com.Dispose();
            con.Close();
        }
        return true;
    }

    public bool TimeList_Bind()
    {
        DB db = new DB();
        string str = "SELECT jobName,dutyInTime,dutyOutTime FROM DUTY_TIME,JOBSKYER WHERE DUTY_TIME.jobskyerID=JOBSKYER.jobskyerID";
        SqlConnection con = new SqlConnection();
        con = db.GetCon();
        con.Open();
        SqlCommand com = new SqlCommand(str, con);
        SqlDataAdapter da = new SqlDataAdapter(com);
        DataSet ds = new DataSet();
        da.Fill(ds);
        try
        {
            TimeList_Manager.DataSource = ds;
            TimeList_Manager.DataBind();
        }
        catch (Exception ex)
        {
            return false;
        }
        finally
        {
            com.Dispose();
            con.Close();
        }
        return true;
    }

    protected void TimeList_Manager_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "delect")
        {
            string jobskyerID = Convert.ToString(e.CommandArgument);
            string str = "DELETE FROM DUTY_TIME WHERE jobskyerID='" + jobskyerID + "'";
            DB db = new DB();
            SqlConnection con = new SqlConnection();
            SqlCommand com = new SqlCommand(str, con);
            con.Open();
            try
            {
                com.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('删除失败！');location.href='DutyTable_Manager.aspx';</script>");
            }
            finally
            {
                com.Dispose();
                con.Close();
            }
            DB db1 = new DB();
            string str1 = "SELECT jobName,dutyInTime,dutyOutTime FROM DUTY_TIME,JOBSKYER WHERE DUTY_TIME.jobskyerID=JOBSKYER.jobskyerID";
            SqlConnection con1 = new SqlConnection();
            con1 = db.GetCon();
            con1.Open();
            SqlCommand com1 = new SqlCommand(str1, con1);
            SqlDataAdapter da = new SqlDataAdapter(com1);
            DataSet ds = new DataSet();
            da.Fill(ds);
            try
            {
                TimeList_Manager.DataSource = ds;
                TimeList_Manager.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write("< script > alert('数据更新失败！'); location.href = 'DutyTable_Manager.aspx';</ script >");
            }
            finally
            {
                com.Dispose();
                con.Close();
                Response.Redirect(Request.Url.ToString());
            }

        }

    }

    protected void ToDutyTable_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/JsInfo/DutyTable.aspx");
    }
}