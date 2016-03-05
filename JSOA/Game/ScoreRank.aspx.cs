using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class Game_ScoreRank01 : System.Web.UI.Page
{
    ///留坑待填，目前游戏就这一个，暂时就这样吧==
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            ScoreRankList_Bind();
            ScoreRank_Bind();
        }
    }

    protected void ScoreRankList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if(ScoreRankList.SelectedValue=="-1")
        {
            ScoreRank.Visible = false;
        }
        else
        {
            ScoreRank.Visible = true;
        }
    }
    public bool ScoreRankList_Bind()///下拉列表绑定数据
    {
        DB db = new DB();
        string sql_game = "SELECT * FROM GAME ";
        SqlConnection con = new SqlConnection();
        con = db.GetCon();
        con.Open();
        SqlCommand cmd = new SqlCommand(sql_game, con);
        SqlDataReader sqlRead_Game = cmd.ExecuteReader();
        try
        {
            ScoreRankList.DataSource = sqlRead_Game;
            ScoreRankList.DataTextField = "gamName";
            ScoreRankList.DataValueField = "gameID";
            ScoreRankList.DataBind();
            ScoreRankList.Items.Add(new ListItem("请选择","-1"));
            ScoreRankList.SelectedValue = "-1";

        }
        catch (Exception ex)
        {
            return false;
        }
        finally
        {
            cmd.Dispose();
            con.Close();
        }
        return true;
    }
    public bool ScoreRank_Bind()///分数排行绑定数据
    {
        DB db = new DB();
        string scoreRankstr = "SELECT GAME_RECORD.jobskyerID,gamScore,jobName FROM GAME_RECORD,JOBSKYER WHERE GAME_RECORD.jobskyerID=JOBSKYER.jobskyerID ORDER BY gamScore desc";
        SqlConnection con = new SqlConnection();
        con = db.GetCon();
        con.Open();
        SqlCommand com = new SqlCommand(scoreRankstr, con);
        SqlDataReader dr = com.ExecuteReader();
        try
        {
            ScoreRank.DataSource = dr;
            ScoreRank.DataBind();
        }  
        catch(Exception ex)
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
}