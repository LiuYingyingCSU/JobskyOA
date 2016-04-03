using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class JsInfo_AddressLIst : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Address_list_Bind_Office();
        Address_list_Bind_SoftWare();
        Address_list_Bind_Internet();
        Address_list_Bind_Art();
        Address_list_Bind_Reporter();
    }
    public bool Address_list_Bind_Office()
    {
        DB db = new DB();
        SqlConnection con = new SqlConnection();
        string Office_sqlstr = "SELECT jobName, jobPhone  FROM jobskyer WHERE jobPosition = '办公室'";
        con = db.GetCon();
        con.Open();
        SqlCommand com_Office = new SqlCommand(Office_sqlstr, con);
        SqlDataReader dr_office = com_Office.ExecuteReader();
        try
        {
            AddressList_Repeater_Office.DataSource = dr_office;
            AddressList_Repeater_Office.DataBind();
        }
        catch(Exception ex)
        {
            return false;
        }
        finally
        {
            com_Office.Dispose();
            con.Close();
        }
        return true;
    }
    public bool Address_list_Bind_SoftWare()
    {
        DB db = new DB();
        SqlConnection con = new SqlConnection();
        string SoftWare_sqlstr = "SELECT jobName, jobPhone  FROM jobskyer WHERE jobPosition = '程序组'";
        con = db.GetCon();
        con.Open();
        SqlCommand com_SoftWare = new SqlCommand(SoftWare_sqlstr, con);
        SqlDataReader dr_software = com_SoftWare.ExecuteReader();
        try
        {
            AddressList_Repeater_SoftWare.DataSource = dr_software;
            AddressList_Repeater_SoftWare.DataBind();
        }
        catch(Exception ex)
        {
            return false;
        }
        finally
        {
            com_SoftWare.Dispose();
            con.Close();
        }
        return true;
    }
    public bool Address_list_Bind_Internet()
    {
        DB db = new DB();
        SqlConnection con = new SqlConnection();
        string Internet_sqlstr = "SELECT jobName, jobPhone  FROM jobskyer WHERE jobPosition = '网络组'";
        con = db.GetCon();
        con.Open();
        SqlCommand com_Internet = new SqlCommand(Internet_sqlstr, con);
        SqlDataReader dr_Internet = com_Internet.ExecuteReader();
        try
        {
            AddressList_Repeater_Internet.DataSource = dr_Internet;
            AddressList_Repeater_Internet.DataBind();
        }
        catch(Exception ex)
        {
            return false;
        }
        finally
        {
            com_Internet.Dispose();
            con.Close();
        }
        return true;
    }
    public bool Address_list_Bind_Art()
    {
        DB db = new DB();
        SqlConnection con = new SqlConnection();
        string Art_sqlstr = "SELECT jobName, jobPhone  FROM jobskyer WHERE jobPosition = '美工组'";
        con = db.GetCon();
        con.Open();
        SqlCommand com_Art = new SqlCommand(Art_sqlstr, con);
        SqlDataReader dr_art = com_Art.ExecuteReader();
        try
        {
            AddressList_Repeater_Art.DataSource = dr_art;
            AddressList_Repeater_Art.DataBind();
        }
        catch(Exception ex)
        {
            return false;
        }
        finally
        {
            com_Art.Dispose();
            con.Close();
        }
        return true;
    }
    public bool Address_list_Bind_Reporter()
    {
        DB db = new DB();
        SqlConnection con = new SqlConnection();
        string Report_sqlstr = "SELECT jobName, jobPhone  FROM jobskyer WHERE jobPosition = '记者团'";
        con = db.GetCon();
        con.Open();
        SqlCommand com_Report = new SqlCommand(Report_sqlstr, con);
        SqlDataReader dr_report = com_Report.ExecuteReader();
        try
        {
            AddressList_Repeater_Reporter.DataSource = dr_report;
            AddressList_Repeater_Reporter.DataBind();
        }
        catch(Exception ex)
        {
            return false;
        }
        finally
        {
            com_Report.Dispose();
            con.Close();
        }
        return true;
    }
}