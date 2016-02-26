using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Main_ImgBtn_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/JsCommon/JsCommonMain.aspx");
    }

    protected void Game_ImgBtn_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Game/2048custom/2048Game.aspx");
    }
}
