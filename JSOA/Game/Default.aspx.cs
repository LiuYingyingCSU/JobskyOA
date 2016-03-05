using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class Game_Default : System.Web.UI.Page
{
    //改页面专门处理json传来的数据，并把分数记录到数据库
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Clear();//清空输出缓冲区，使其不要输出html
        Dictionary<String, String> list = new Dictionary<string, string>();
        JavaScriptSerializer serializer = new JavaScriptSerializer();

        if (Request["username"] == null || Request["score"] == null || Request["gameID"] == null)
        {
            list["result"] = "请重新登录！";
        }
        else
        {
            string username = Request["username"];
            string score = Request["score"];
            string gameID = Request["gameID"];

            if (SelectScore(username, gameID))
            {
                if (SelectMyScore(username, gameID) < Int32.Parse(score))
                {
                    if (UpdateScore(username, score, gameID)) { list["result"] = "SUCCESS"; }
                    else { list["result"] = "分数更新失败！"; }
                }
                else { list["result"] = "亲，还需要努力一把才能超越自己！"; }
            }
            else
            {
                if (InsertScore(username, score, gameID)){ list["result"] = "SUCCESS"; }
                else{ list["result"] = "分数记录失败！"; }
            }
        }

        StringBuilder result = new StringBuilder();
        serializer.Serialize((object)list, result);
        Response.Write(result);                //string.Format('"result":"{0}"', Request["username"])
        Response.End();
    }
    public bool SelectScore(string username,string gameID){
        SqlConnection conn = new SqlConnection();
        string sqlstr = "select count(*) from GAME_RECORD where jobskyerID ='" + username + "' and gameID='" + gameID + "' ";
        DB db = new DB();
        conn = db.GetCon();
        conn.Open();
        SqlCommand cmd = new SqlCommand(sqlstr, conn);
        try
        {
            if (Int32.Parse( cmd.ExecuteScalar().ToString()) == 0)
            {
                return false;
            }
        }
        catch (Exception ex)
        {
            return false;
        }
        finally
        {
            cmd.Dispose();
            conn.Close();
        }
        return true;//本来就有记录，就返回true
    }

    public int SelectMyScore(string username, string gameID)
    {
        int myscore = 0;
        SqlConnection conn = new SqlConnection();
        string sqlstr = "select gamScore from GAME_RECORD where jobskyerID ='" + username + "' and gameID='" + gameID + "' ";
        DB db = new DB();
        conn = db.GetCon();
        conn.Open();
        SqlCommand cmd = new SqlCommand(sqlstr, conn);
        try
        {
            myscore=Int32.Parse(cmd.ExecuteScalar().ToString());
            return myscore;//获取个人纪录并返回
        }
        catch (Exception ex)
        {
            return 0;
        }
        finally
        {
            cmd.Dispose();
            conn.Close();
        }
        //return myscore;
    }

    public bool InsertScore(string username, string score, string gameID)
    {
        SqlConnection conn = new SqlConnection();
        string sqlstr = "insert into  GAME_RECORD (gameID,jobskyerID,gamScore) values('" + gameID + "','" + username + "','" + score + "')";
        DB db = new DB();
        conn = db.GetCon();
        conn.Open();
        SqlCommand cmd = new SqlCommand(sqlstr, conn);
        try
        {
            if (cmd.ExecuteNonQuery() == 0)
            {
                return false;
            }
        }
        catch (Exception ex)
        {
            return false;
        }
        finally
        {
            cmd.Dispose();
            conn.Close();
        }
        return true;
    }
    public bool UpdateScore(string username, string score, string gameID)
    {
        SqlConnection conn = new SqlConnection();
        string sqlstr = "update GAME_RECORD set gamScore = '" + score + "' where jobskyerID = '" + username + "' and gameID = '" + gameID + "'";
        DB db = new DB();
        conn = db.GetCon();
        conn.Open();
        SqlCommand cmd = new SqlCommand(sqlstr, conn);
        try
        {
            if (cmd.ExecuteNonQuery() == 0)
            {
                return false;
            }
        }
        catch (Exception ex)
        {
            return false;
        }
        finally
        {
            cmd.Dispose();
            conn.Close();
        }
        return true;
    }

    public static DataRow GetScoreById(string sqlstr)
    {
        //  SqlConnection conn = sqlconnection.GetConnection();
        SqlConnection conn = new SqlConnection();
        DB db = new DB();
        conn = db.GetCon();
        conn.Open();
        SqlCommand cmd = new SqlCommand(sqlstr, conn);
        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        sda.Fill(dt);
        cmd.Dispose();
        conn.Close();
        return dt.Rows[0];
    }
}