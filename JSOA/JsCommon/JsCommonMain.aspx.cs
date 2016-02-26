using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class JsCommon_JsCommonMain : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }
    ///public bool Game_DownList_Bind()
    ///{
        ///DB db = new DB();
        ///string sql_game = "SELECT * FROM GAME ";
        ///SqlConnection con = new SqlConnection();
        ///con = db.GetCon();
        ///con.Open();
        ///SqlCommand cmd = new SqlCommand(sql_game, con);
        ///SqlDataReader sqlRead_Game = cmd.ExecuteReader();
        ///try
        ///{
           /// Game_DownList.DataSource = sqlRead_Game;
            ///Game_DownList.DataTextField = "gamName";
            ///Game_DownList.DataValueField = "gameID";
            ///Game_DownList.DataBind();
            ///Game_DownList.Items.Add(new ListItem("请选择", "-1"));
            ///Game_DownList.SelectedValue = "-1";
        ///}
        ///catch(Exception ex)
        ///{
            ///return false;
        ///}
        ///finally
        ///{
            ///cmd.Dispose();
            ///con.Close();
        ///}
        ///return true;
    ///}

    ///protected void Game_DownList_SelectedIndexChanged(object sender, EventArgs e)
    ///{
        ///if(Game_DownList.SelectedValue!="-1")
        ///{
            ///Response.Redirect("~/Game/2048custom/2048.aspx");
        ///}
        //else
        //{ 
            //Response.Redirect("~/Game/2048custom/2048.aspx");
        //}
    ///}
}