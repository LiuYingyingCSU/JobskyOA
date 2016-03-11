using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class JSOA_JsManager_Announce : System.Web.UI.Page
{
    //允许分页
    PagedDataSource pds = new PagedDataSource();
    DB db = new DB();
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    /// <summary>
    /// 发布公告
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Submit_Click(object sender, EventArgs e)
    {
        Response.Write(content.InnerText);
    }
    /// <summary>
    /// 附件上传
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Upload_Click(object sender, EventArgs e)
    {
        if (FileUpload1.HasFile)
        {
            FileUpload1.SaveAs(Server.MapPath("~/")+FileUpload1.FileName);
            Label1.Text = "文件上传成功！";
        }
    }
    private void DataBind()
    {
        int totalCount = 0;
        int totalPage = 1;
        int curPage;

        SqlConnection scon = db.GetCon();
        SqlCommand scom = new SqlCommand();
        scon.Open();
        SqlDataAdapter sda = new SqlDataAdapter("select * from ANNOUNCE order by annTime asce", scon);//时间的升降序是怎样？？
        DataSet ds = new DataSet();
        sda.Fill(ds, "ANNOUNCE");
        DataView dv = ds.Tables[0].DefaultView;

        totalCount = dv.Count;
        pds.DataSource = dv;
        //scon.Dispose();
        pds.AllowPaging = true;//允许分页
        pds.PageSize = 10;
        if (Request.QueryString["page"] != null)
        {
            curPage = Convert.ToInt32(Request.QueryString["page"]);
        }
        else curPage = 1;
        if (totalCount <= 10)
            totalPage = 1;
        else
        {
            if (totalCount % pds.PageSize == 0)
                totalPage = totalCount / pds.PageSize;
            else
                totalPage = totalCount / pds.PageSize + 1;
        }
        pds.CurrentPageIndex = curPage - 1;//获取当前页面的指引
        lblCurrentPage.Text = "共" + totalCount.ToString() + "条记录 当前页：" + curPage.ToString() + "/" + totalPage;
        lnkFirst.NavigateUrl = Request.CurrentExecutionFilePath + "?Page=1";//获取当前页的虚拟路径
        if(!pds.IsFirstPage)
            lnkPrev.NavigetaUrl = Request.CurrentExecutionFilePath + "?Page=" + Convert.ToString(curPage - 1);

        if (!pds.IsLastPage)
            lnkNext.NavigateUrl = Request.CurrentExecutionFilePath + "?Page=" + Convert.ToString(curPage + 1);
        lnkEnd.NavigateUrl = Request.CurrentExecutionFilePath + "?Page=" + totalPage;

        Repeater1.DataSource = pds;
        Repeater1.DataBind();
        scon.Dispose();
    }
}