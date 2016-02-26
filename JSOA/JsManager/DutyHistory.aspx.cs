using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using Jobskyconnection;
using Jobskywhole;
using System.Data;
using System.Configuration;
public partial class admin_History : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            PrintAll();
        }
    }
    protected void Search_Click(object sender, EventArgs e)
    {
        string mystr = ConfigurationManager.ConnectionStrings["myconnstring"].ToString();
        SqlConnection cn = new SqlConnection();
        cn.ConnectionString = mystr;
        cn.Open();
        try
        {
            if (SearchPeople.Text.ToString().Trim()=="")
            {
                Response.Write("<script>alert('查找失败，学号为空');location='DutyHistory.aspx'</script>");
            }
            else
            {
                string sqlstr1 = "SELECT SIGN_RECORD.JOBSKYER_ID,SIGN_RECORD_ID,DATENAME(weekday,MY_SIGN_IN_TIME)+CONVERT(varchar(12) ,MY_SIGN_IN_TIME, 108 ) AS MY_SIGN_IN_TIME_WEEK_NOW,DATENAME(weekday,MY_SIGN_OUT_TIME)+CONVERT(varchar(12) ,MY_SIGN_OUT_TIME, 108 ) AS MY_SIGN_OUT_TIME_WEEK_NOW,NAME AS SIGNNAME From SIGN_RECORD INNER JOIN JOBSKYER ON SIGN_RECORD.JOBSKYER_ID=JOBSKYER.JOBSKYER_ID WHERE SIGN_RECORD.JOBSKYER_ID=@SearchID";
                string sqlstr2 = "SELECT SIGN_RECORD.JOBSKYER_ID,SIGN_RECORD_ID,NAME AS REPLACENAME FROM SIGN_RECORD INNER JOIN JOBSKYER ON SIGN_RECORD.BE_SUBSTITUTE_ID=JOBSKYER.JOBSKYER_ID WHERE SIGN_RECORD.JOBSKYER_ID=@SearchID";
            SqlCommand cmm1 = new SqlCommand(sqlstr1,cn);          
            cmm1.Parameters.Add(new SqlParameter("@SearchID", SearchPeople.Text));
            SqlDataAdapter da1 = new SqlDataAdapter(cmm1);
            DataSet ds1 = new DataSet();
            da1.Fill(ds1);        
            Repeater1.DataSource = ds1;
            Repeater1.DataBind();
            cn.Close();
            cn.Open();
            SqlCommand cmm2 = new SqlCommand(sqlstr2, cn);
            cmm2.Parameters.Add(new SqlParameter("@SearchID", SearchPeople.Text));
            SqlDataAdapter da2 = new SqlDataAdapter(cmm2);
            DataSet ds2 = new DataSet();
            da2.Fill(ds2);
            Repeater2.DataSource = ds2;
            Repeater2.DataBind();
            cn.Close();
            }
        }
        catch
        {
            Response.Write("<script>alert('查找时出现错误！');location='DutyHistory.aspx'</script>");
        }
    }
    protected void PrintAll()
    {
        string sqlstr1 = "SELECT SIGN_RECORD.JOBSKYER_ID,SIGN_RECORD_ID,DATENAME(weekday,MY_SIGN_IN_TIME)+CONVERT(varchar(12) ,MY_SIGN_IN_TIME, 108 ) AS MY_SIGN_IN_TIME_WEEK_NOW,DATENAME(weekday,MY_SIGN_OUT_TIME)+CONVERT(varchar(12) ,MY_SIGN_OUT_TIME, 108 ) AS MY_SIGN_OUT_TIME_WEEK_NOW,NAME AS SIGNNAME From SIGN_RECORD INNER JOIN JOBSKYER ON SIGN_RECORD.JOBSKYER_ID=JOBSKYER.JOBSKYER_ID";
        string sqlstr2 = "SELECT SIGN_RECORD.JOBSKYER_ID,SIGN_RECORD_ID,NAME AS REPLACENAME FROM SIGN_RECORD INNER JOIN JOBSKYER ON SIGN_RECORD.BE_SUBSTITUTE_ID=JOBSKYER.JOBSKYER_ID";
        string mystr = ConfigurationManager.ConnectionStrings["myconnstring"].ToString();
        SqlConnection cn = new SqlConnection();
        cn.ConnectionString = mystr;
        cn.Open();
        SqlCommand cmm1 = new SqlCommand(sqlstr1,cn);     
        SqlDataAdapter da1 = new SqlDataAdapter(cmm1);
        DataSet ds1 = new DataSet();
        da1.Fill(ds1);
        Repeater1.DataSource = ds1;
        Repeater1.DataBind();
        cn.Close();
        cn.Open();
        SqlCommand cmm2 = new SqlCommand(sqlstr2, cn);
        SqlDataAdapter da2 = new SqlDataAdapter(cmm2);
        DataSet ds2 = new DataSet();
        da2.Fill(ds2);
        Repeater2.DataSource = ds2;
        Repeater2.DataBind();
        cn.Close();
    }
    protected void SearchAll_Click(object sender, EventArgs e)
    {
        PrintAll();
    }
    protected void Repeater2_ItemCommand1(object source, RepeaterCommandEventArgs e)
    {
        string id = e.CommandArgument.ToString();
        if (e.CommandName == "delete")
        {
            string sqlstr = "DELETE FROM SIGN_RECORD WHERE SIGN_RECORD_ID='" + id + "'";
            string mystr = ConfigurationManager.ConnectionStrings["myconnstring"].ToString();
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = mystr;
            cn.Open();
            SqlCommand cm = new SqlCommand(sqlstr, cn);
            try
            {
                cm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            Response.Write("<script>alert('删除成功.');location='DutyHistory.aspx'</script>");
            cn.Close();
        }
    }
    protected void AimDelete_btn_Click(object sender, EventArgs e)
    {
        if (AimDelete.Text.ToString().Trim() != "")
        {
            string JOBSKYER_ID = AimDelete.Text;
            string sqlstr = "DELETE FROM SIGN_RECORD WHERE JOBSKYER_ID='" + JOBSKYER_ID + "'";
            string mystr = ConfigurationManager.ConnectionStrings["myconnstring"].ToString();
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = mystr;
            cn.Open();
            SqlCommand cm = new SqlCommand(sqlstr, cn);
            try
            {
                cm.ExecuteNonQuery();               
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            Response.Write("<script>alert('删除成功.');location='DutyHistory.aspx'</script>");
            cn.Close();
        }
        if (AimDelete.Text.ToString()=="")
        {
            Response.Write("<script>alert('删除失败,学号为空.');location='DutyHistory.aspx'</script>");
        }
    }
}