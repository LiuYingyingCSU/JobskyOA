using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
public partial class JsCommon_SignInAndOut : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        AddLinkedStyle("/CSS/SignIn.css");
        if (Session["jobskyerID"] == null)
        {
            Response.Write("<script>alert('登录过期，请尝试重新登录！')</script>");
            Response.Redirect("~/JsCommon/Login.aspx");
        }
        
        if(!IsPostBack)
        {//判断Button显示与否
            ImgbtnSignIn.Enabled = false;
            DLSignInTo.Enabled = false;
            DLSignInTo.Visible = false;
            //ImgbtnSignOut.Enabled = false;
            lblMessage.Visible = false;
            ShowButton();
            rptBind();
            state();
            DlistBind();
        }
    }
    protected void AddLinkedStyle(string url)
    {
        HtmlLink hLink = new HtmlLink();
        hLink.Attributes.Add("type", "text/css");
        hLink.Attributes.Add("rel", "stylesheet");
        hLink.Attributes.Add("href", url);
        Page.Header.Controls.Add(hLink);
    }
    public void DlistBind()
    {
        DB db = new DB();
        SqlConnection myCon = db.GetCon();
        SqlCommand myCom = new SqlCommand();
        myCon.Open();
        myCom = new SqlCommand("select count(*) FROM MY_DUTY_TIME WHERE jobskyerID='" + Session["jobskyerID"] + "'", myCon); //获得该组文件的总个数
        this.lbCount.Text = (Convert.ToInt32(myCom.ExecuteScalar())/4).ToString();  //算出总页数为DropdownList赋值
        int[] num = new int[Convert.ToInt32(lbCount.Text)];
        //Response.Write(lbCount.Text.ToString());
        //Response.Write(lbPage.Text.ToString());
        for (int i = 1; i <= Convert.ToInt32(lbCount.Text); i++)
        {
            num[i - 1] = i;
        }
        DropDownList1.DataSource = num;
        DropDownList1.DataBind();
    }
    protected void rptBind()
    {
        DB db = new DB();
        int n = 4 * (Convert.ToInt32(lbPage.Text) - 1);
        SqlConnection myCon = db.GetCon();
        myCon.Open();
        string sqlstr = "SELECT TOP 4 jobskyerID,dutyInTime,dutyOutTime,toJobskyerID FROM MY_DUTY_TIME WHERE jobskyerID='" + Session["jobskyerID"] + "' and jobskyerID not in (select top (@n) jobskyerID from FILES where jobskyerID='" + Session["jobskyerID"] + "')";
        SqlCommand mycom = new SqlCommand(sqlstr, myCon);
        mycom.Parameters.Add("n", n);
        SqlDataAdapter da = new SqlDataAdapter(mycom);
        DataSet ds = new DataSet();
        da.Fill(ds);
        ds.Dispose();
        da.Dispose();
        myCon.Close();
        if (ds != null)
        {
            this.Repeater1.DataSource = ds;
            this.Repeater1.DataBind();
        }
    }
    public string GetJobName(object jobskyerID)
    {
        DB db = new DB();
        SqlDataReader dr = db.reDr("SELECT jobName FROM JOBSKYER WHERE jobskyerID='" + jobskyerID + "'");
        dr.Read();
        return dr.GetValue(0).ToString();
    }
    private void ShowButton()
    {
        int flag = 0;//用flag标记用户的当前状态，0：未值班期间，1：值班期间
        DB db = new DB();
        DateTime nowTime = DateTime.Now;
        string sqlstr = "SELECT * FROM DUTY_TIME;SELECT * FROM MY_DUTY_TIME;SELECT jobskyerID,jobName FROM JOBSKYER";
        DataSet ds = db.reDt(sqlstr).DataSet;
        DateTime dutyInTime=new DateTime();
        DateTime dutyOutTime=new DateTime();
        //先判断签退和代班签退，签退优先于代班签退
        //判断表中是否非空
        if(ds.Tables[0].Rows.Count>0&&ds.Tables[1].Rows.Count>0&&ds.Tables[2].Rows.Count>0)
        {
            for(int i=0;i<ds.Tables[1].Rows.Count;i++)
            {
                //当天有值班人为当前用户的未签退记录
                if(ds.Tables[1].Rows[i]["jobskyerID"].ToString().Trim()==Session["jobskyerID"].ToString().Trim()&&ds.Tables[1].Rows[i]["flag2"].ToString()=="2"&&Convert.ToDateTime(ds.Tables[1].Rows[i]["dutyInTime"]).ToShortDateString()==nowTime.ToShortDateString())
                {
                    //查找被值班人的理论签退时间
                    for (int j = 0; j < ds.Tables[0].Rows.Count;j++ )
                    {
                        //找到
                        if(ds.Tables[0].Rows[j]["jobskyerID"].ToString().Trim()==ds.Tables[1].Rows[i]["toJobskyerID"].ToString().Trim())
                        {
                            flag = 1;
                            Session["dutyRecordID"] = ds.Tables[1].Rows[i]["dutyRecordID"].ToString();
                            dutyOutTime = Convert.ToDateTime(ds.Tables[0].Rows[j]["dutyOutTime"].ToString());
                        }
                        else
                        {
                            continue;
                        }
                        //找到才会执行下面的语句,在允许的签退范围之内时
                        if(dutyOutTime.DayOfWeek==nowTime.DayOfWeek&&differOfTime(dutyOutTime,nowTime)<=30&&differOfTime(dutyOutTime,nowTime)>=-60)
                        {
                            //从这里开始往下
                            //找到被值班人的名字
                            for (int k = 0; k < ds.Tables[2].Rows.Count;k++ )
                            {
                                if (ds.Tables[2].Rows[k]["jobskyerID"].ToString().Trim() == ds.Tables[1].Rows[i]["toJobskyerID"].ToString().Trim())
                                {
                                    //获得被代班人的名字传到下面签退里
                                    //判断是否在代班签退允许时间范围内，显示代班签到签退按钮
                                    Session["toJobName"] = ds.Tables[2].Rows[k]["jobName"].ToString().Trim();
                                    //ImgbtnSignOut.ImageUrl="~/Image/Sign/SignOutTo.PNG";
                                    
                                    ImgbtnSignIn.Enabled = true;
                                    if (Session["jobskyerID"].ToString().Trim() == ds.Tables[1].Rows[i]["toJobskyerID"].ToString().Trim())
                                    {
                                        ImgbtnSignIn.ImageUrl = "~/Image/Sign/SignOut.PNG";
                                        lblMessage.Text = Session["jobName"] + "正在值班";
                                    }
                                    else
                                    {
                                        ImgbtnSignIn.ImageUrl = "~/Image/Sign/SignOutTo.PNG";
                                        lblMessage.Text = Session["jobName"] + "正在给"+Session["toJobName"]+"值班";
                                    }     
                                }
                            }
                            
                        }
                            //如果在值班期间，显示值班提示信息
                        else if(dutyOutTime.DayOfWeek==nowTime.DayOfWeek&&differOfTime(nowTime,dutyInTime)>60&&differOfTime(dutyOutTime,nowTime)>30)
                        {
                            for (int k = 0; k < ds.Tables[2].Rows.Count; k++)
                            {
                                if (ds.Tables[2].Rows[k]["jobskyerID"].ToString().Trim() == ds.Tables[1].Rows[i]["toJobskyerID"].ToString().Trim())
                                {
                                    Session["toJobName"] = ds.Tables[2].Rows[k]["jobName"].ToString().Trim();
                                    if (Session["jobskyerID"].ToString().Trim() == ds.Tables[1].Rows[i]["toJobskyerID"].ToString().Trim())
                                    {
                                        lblMessage.Visible = true;
                                        lblMessage.Text =  Session["jobName"].ToString()+ "正在值班,未到签退时间";
                                        
                                    }
                                    else
                                    {                     
                                        lblMessage.Visible = true;
                                        //DLSignInTo.Enabled = false;
                                        //DLSignInTo.Visible = false;
                                        //DLSignInTo.SelectedValue = Session["toJobName"].ToString();
                                        lblMessage.Text = Session["jobName"].ToString() + "正在给" + Session["toJobName"] + "值班，未到签退时间";
                                       }
                                }
                            }
                        }
                        else if(differOfTime(nowTime,dutyOutTime)>60)
                        {
                            flag = 0;
                            ImgbtnSignIn.ImageUrl = "~/Image/Sign/SignIn.PNG";
                            DLSignInTo.Visible = false;
                            DLSignInTo.Enabled = false;
                        }
                    }
                }
            }
        }
        //判断签到代签按钮是否可用
        //签到
        if(ds!=null&&ds.Tables.Count>0&&ds.Tables[0].Rows.Count>0&&flag==0)
        {
            for(int i=0;i<ds.Tables[0].Rows.Count;i++)
            {
                if(ds.Tables[0].Rows[i]["jobskyerID"].ToString().Trim()==Session["jobskyerID"].ToString())
                {
                    for (int j = 0; j < ds.Tables[1].Rows.Count;j++ )
                    {
                        //检查当天是否已值班（被代班）
                        if (Convert.ToDateTime(ds.Tables[1].Rows[j]["dutyInTime"].ToString()).DayOfWeek==nowTime.DayOfWeek&&ds.Tables[1].Rows[j]["jobskyerID"].ToString() != Session["jobskyerID"].ToString()&&ds.Tables[1].Rows[j]["toJobskyerID"].ToString() == Session["jobskyerID"].ToString()&&nowTime.DayOfWeek.ToString()== Convert.ToDateTime( ds.Tables[0].Rows[i]["dutyInTime"]).DayOfWeek.ToString())
                        {
                            SqlDataReader dr = db.reDr("SELECT jobName FROM JOBSKYER WHERE jobskyerID='" + ds.Tables[1].Rows[j]["jobskyerID"] + "'"); //and dutyInTime='"+nowTime.ToString()+"'");
                            dr.Read();
                            lblMessage.Visible = true;
                            lblMessage.Text="正在被"+dr.GetValue(0)+"代班";
                            flag = 1;
                            ImgbtnSignIn.Enabled = false;
                            break;
                        }
                        else if(j==ds.Tables[1].Rows.Count-1&&ds.Tables[1].Rows[j]["toJobskyerID"]!=Session["jobskyerID"])
                        {
                            dutyInTime = Convert.ToDateTime(ds.Tables[0].Rows[i]["dutyInTime"].ToString());
                            //判断当前时间是否在签到时间内
                            if (dutyInTime.DayOfWeek == nowTime.DayOfWeek && differOfTime(Convert.ToDateTime(dutyInTime), nowTime) <= 30 && differOfTime(Convert.ToDateTime(dutyInTime), nowTime) >= -60)
                            {
                                flag = 1;
                                ImgbtnSignIn.Enabled = true;
                                ImgbtnSignIn.ImageUrl = "~/Image/Sign/SignIn.PNG";//不必要
                                break;
                            }
                        }
                    }
                        
                }
            }
        }
        //代班签到
        if(ds.Tables[0].Rows.Count>0&&ds.Tables[2].Rows.Count>0&&flag==0)
        {
            DLSignInTo.Items.Clear();
            DLSignInTo.Items.Add(new ListItem("请选择被代班人："));
            for(int i=0;i<ds.Tables[0].Rows.Count;i++)
            {
                dutyInTime = Convert.ToDateTime(ds.Tables[0].Rows[i]["dutyInTime"].ToString().Trim());
                //不是我的值班时间
                if (ds.Tables[0].Rows[i]["jobskyerID"].ToString().Trim()!= Session["jobskyerID"].ToString().Trim() && differOfTime(dutyInTime, nowTime) <= 30 && differOfTime(dutyInTime, nowTime) >= -60 && Convert.ToDateTime(ds.Tables[0].Rows[i]["dutyInTime"].ToString().Trim()).DayOfWeek == nowTime.DayOfWeek)
                {
                    for(int j=0;j<ds.Tables[1].Rows.Count;j++)
                    {
                        //如果在值班当天记录里找到此班次，则不加入下拉框(判断被代班人员ID,而不是值班人员ID)
                        if(ds.Tables[1].Rows[j]["toJobskyerID"].ToString()==ds.Tables[0].Rows[i]["jobskyerID"].ToString().Trim()&&Convert.ToDateTime(ds.Tables[1].Rows[j]["dutyInTime"].ToString().Trim()).DayOfWeek==nowTime.DayOfWeek)
                        {
                            break;
                        }
                         //如果没有找到该班次值班记录，判断当前时间是否在签到范围内，若是，放进下拉列表，可见
                         //当查找到最后一条记录或者下一条记录非当天，则加入下拉列表中
                        else if (j+1==ds.Tables[1].Rows.Count)//|| Convert.ToDateTime(ds.Tables[0].Rows[j]["dutyInTime"].ToString()).DayOfWeek!=nowTime.DayOfWeek)
                        {
                            DLSignInTo.Visible = true;
                            DLSignInTo.Enabled = true;
                            ImgbtnSignIn.Enabled = true;
                            //ImgbtnSignOut.ImageUrl = "~/Image/Sign/SignOutTo.PNG";
                            ImgbtnSignIn.ImageUrl = "~/Image/Sign/SignInTo.PNG";
                            Session["toJobskyerID"] = ds.Tables[0].Rows[i]["jobskyerID"];
                            for (int k = 0; k < ds.Tables[2].Rows.Count; k++)
                            {
                                if(Session["toJobskyerID"].ToString()==ds.Tables[2].Rows[k]["jobskyerID"].ToString())
                                {
                                    DLSignInTo.Items.Add(new ListItem(ds.Tables[2].Rows[k]["jobName"].ToString().Trim()));
                                }
                            }
                        }
                    }
                }
            }
        }
    }
    private int differOfTime(DateTime dateT1,DateTime dateT2)
    {
        //计算两时间差
        TimeSpan ts1 = new TimeSpan(dateT1.Ticks);
        TimeSpan ts2 = new TimeSpan(dateT2.Ticks);
        int differ=Convert.ToInt32(ts1.Subtract(ts2).TotalMinutes);
        return differ;
    }
    protected void ImgbtnSignIn_Click(object sender, ImageClickEventArgs e)
    {
        DB db = new DB();
        DateTime nowTime = DateTime.Now;      
        if (ImgbtnSignIn.ImageUrl.ToString() == "~/Image/Sign/SignIn.PNG")
        {
            try
            {

                string dutyInTimeSql = "SELECT dutyInTime FROM DUTY_TIME WHERE jobskyerID='" + Session["jobskyerID"] + "'";//and dutyInTime='"+nowTime.ToString()+"'";
                SqlDataReader dutyInTimeDr = db.reDr(dutyInTimeSql);
                dutyInTimeDr.Read();
                if(dutyInTimeDr.Read())
                {          
                    int flag1=0;
                    DateTime dutyInTime = Convert.ToDateTime(dutyInTimeDr["dutyInTime"].ToString());
                    if (dutyInTime.DayOfWeek == nowTime.DayOfWeek && differOfTime(Convert.ToDateTime(dutyInTime), nowTime) >= 0 && dutyInTime.DayOfWeek == nowTime.DayOfWeek && differOfTime(Convert.ToDateTime(dutyInTime), nowTime)<=30)
                    {
                        flag1=0;
                       
                    }
                    else if (differOfTime(Convert.ToDateTime(dutyInTime), nowTime) < 0 && dutyInTime.DayOfWeek == nowTime.DayOfWeek && differOfTime(Convert.ToDateTime(dutyInTime), nowTime)>=-60)
                    {
                        flag1=1;
                    }
                    string recordInsert = "INSERT INTO MY_DUTY_TIME(jobskyerID,dutyInTime,flag1,toJobskyerID) values('" + Session["jobskyerID"] + "','" + nowTime.ToString() + "','" + flag1 + "','" + Session["jobskyerID"] + "')";
                    Session["dutyInTime"] = nowTime.ToString();
                    if(db.SqlEX(recordInsert)==1)
                    {
                        Response.Write("<script>alert('签到成功')</script>");
                        lblMessage.Visible = true;
                        lblMessage.Text = Session["jobName"] + "正在值班...";
                        //flag = 1;
                    }
                    else
                    {
                        lblMessage.Visible = true;
                        lblMessage.Text="签到失败！";
                    }
                }
                
            }
            catch
            {
                Response.Write("<script>alert('可能是登录过期，请重新登录')</script>");
                Response.Redirect("~/JsCommon/Login.aspx");
            }
            finally
            {

            }
        }
        //代班签到
        else if (ImgbtnSignIn.ImageUrl == "~/Image/Sign/SignInTo.PNG")
        {
            if(DLSignInTo.SelectedValue.ToString()=="请选择被代班人：")
            {
                return;
            }
            else
            {
                try
            {
                string dutyInTimeSql = "SELECT dutyInTime FROM DUTY_TIME WHERE jobskyerID='" + Session["toJobskyerID"] + "'";//and dutyInTime='"+nowTime.ToString()+"'";
                SqlDataReader dutyInTimeDr = db.reDr(dutyInTimeSql);
                dutyInTimeDr.Read();
                if (dutyInTimeDr.HasRows)
                {
                    int flag1 = 0;
                    DateTime dutyInTime = Convert.ToDateTime(dutyInTimeDr["dutyInTime"].ToString());
                    if (dutyInTime.DayOfWeek == nowTime.DayOfWeek && differOfTime(Convert.ToDateTime(dutyInTime), nowTime) >= 0 && dutyInTime.DayOfWeek == nowTime.DayOfWeek && differOfTime(Convert.ToDateTime(dutyInTime), nowTime) <= 30)
                    {
                        //准时签到
                        flag1 = 0;

                    }
                    else if (differOfTime(Convert.ToDateTime(dutyInTime), nowTime) < 0 && dutyInTime.DayOfWeek == nowTime.DayOfWeek && differOfTime(Convert.ToDateTime(dutyInTime), nowTime) >= -60)
                    {
                        //迟到
                        flag1 = 1;
                    }
                    string recordInsert = "INSERT INTO MY_DUTY_TIME(jobskyerID,dutyInTime,flag1,toJobskyerID) values('" + Session["jobskyerID"] + "','" + nowTime.ToString() + "','" + flag1 + "','" + Session["toJobskyerID"] + "')";
                    Session["dutyInTime"] = nowTime.ToString();
                    if (db.SqlEX(recordInsert) == 1)
                    {
                        //签到成功
                        Response.Write("<script>alert('" + Session["jobName"] + "给" + DLSignInTo.SelectedValue.ToString() + "代班签到成功')</script>");
                        //flag = 1;
                    }
                    else
                    {
                        lblMessage.Visible = true;
                        lblMessage.Text = "签到失败！";
                    }
                }

            }
            catch
            {
                Response.Write("<script>alert('可能是登录过期，请重新登录')</script>");
                Response.Redirect("~/JsCommon/Login.aspx");
            }
            finally
            {

            }
            }
            
        }
        if (ImgbtnSignIn.ImageUrl.ToString() == "~/Image/Sign/SignOut.PNG")
        {
            try
            {
                string dutyOutTimeSql = "SELECT dutyOutTime FROM DUTY_TIME WHERE jobskyerID='" + Session["jobskyerID"] + "'";//and dutyInTime='"+nowTime.ToString()+"'";
                SqlDataReader dutyOutTimeDr = db.reDr(dutyOutTimeSql);
                dutyOutTimeDr.Read();
                if (dutyOutTimeDr.HasRows)
                {
                    //flag2  2:未签退 0：按时签退 1：早退
                    int flag2 = 2;
                    DateTime dutyOutTime = Convert.ToDateTime(dutyOutTimeDr.GetValue(0).ToString());
                    if (dutyOutTime.DayOfWeek == nowTime.DayOfWeek && differOfTime(Convert.ToDateTime(dutyOutTime), nowTime) <= 0 && dutyOutTime.DayOfWeek == nowTime.DayOfWeek && differOfTime(Convert.ToDateTime(dutyOutTime), nowTime) >= -60)
                    {
                        flag2 = 0;
                    }
                    else if (differOfTime(Convert.ToDateTime(dutyOutTime), nowTime) < 30 && dutyOutTime.DayOfWeek == nowTime.DayOfWeek && differOfTime(Convert.ToDateTime(dutyOutTime), nowTime) > 0)
                    {
                        flag2 = 1;
                    }
                    string recordUpdate1 = "UPDATE MY_DUTY_TIME set flag2='" + flag2 + "' WHERE jobskyerID='" + Session["jobskyerID"] + "'and dutyRecordID='" + Session["dutyRecordID"] + "'";
                    string recordUpdate2 = "UPDATE MY_DUTY_TIME set dutyOutTime='" + nowTime.ToString() + "' WHERE jobskyerID='" + Session["jobskyerID"] + "'and dutyRecordID='" + Session["dutyRecordID"] + "'";
                    if (db.SqlEX(recordUpdate2) == 1 && db.SqlEX(recordUpdate1) == 1)
                    {
                        lblMessage.Visible = true;
                        //Response.Write(dutyRecordID);
                        lblMessage.Text = Session["jobName"] + "签退成功";
                        //ImgbtnSignOut.Enabled = false;
                        //flag = 0;
                    }
                    else
                    {
                        lblMessage.Visible = true;
                        lblMessage.Text = "签退失败！";
                    }
                }
                else
                {
                    Response.Write("未读取到数据");
                }
                ShowButton();
            }
            catch
            {
                Response.Write("<script>alert('可能是登录过期，请重新登录')</script>");
                //Response.Redirect("~/JsCommon/Login.aspx");
            }
            finally
            {

            }
        }
        else if (ImgbtnSignIn.ImageUrl == "~/Image/Sign/SignOutTo.PNG")
        {
            try
            {
                string dutyOutTimeSql = "SELECT dutyOutTime FROM DUTY_TIME WHERE jobskyerID='" + Session["toJobskyerID"] + "'";//and dutyInTime='"+nowTime.ToString()+"'";
                SqlDataReader dutyOutTimeDr = db.reDr(dutyOutTimeSql);
                dutyOutTimeDr.Read();
                Response.Write(dutyOutTimeDr.Read().ToString());
                if (dutyOutTimeDr.HasRows)
                {
                    int flag2 = 2;
                    DateTime dutyOutTime = Convert.ToDateTime(dutyOutTimeDr["dutyOutTime"].ToString());
                    if (dutyOutTime.DayOfWeek == nowTime.DayOfWeek && differOfTime(Convert.ToDateTime(dutyOutTime), nowTime) <= 0 && dutyOutTime.DayOfWeek == nowTime.DayOfWeek && differOfTime(Convert.ToDateTime(dutyOutTime), nowTime) >= -60)
                    {
                        flag2 = 0;

                    }
                    else if (differOfTime(Convert.ToDateTime(dutyOutTime), nowTime) >= 30 && dutyOutTime.DayOfWeek == nowTime.DayOfWeek && differOfTime(Convert.ToDateTime(dutyOutTime), nowTime) < 0)
                    {
                        flag2 = 1;
                    }
                    string recordUpdate1 = "UPDATE MY_DUTY_TIME set flag2='" + flag2 + "' WHERE jobskyerID='" + Session["jobskyerID"] + "'and dutyRecordID='" + Session["dutyRecordID"] + "'";
                    string recordUpdate2 = "UPDATE MY_DUTY_TIME set dutyOutTime='" + nowTime.ToString() + "' WHERE jobskyerID='" + Session["jobskyerID"] + "'and dutyRecordID='" + Session["dutyRecordID"] + "'";

                    if (db.SqlEX(recordUpdate1) == 1 && db.SqlEX(recordUpdate2) == 1)
                    {
                        lblMessage.Visible = true;
                        lblMessage.Text = Session["jobName"] + "给" + Session["toJobName"] + "签退成功！"; ;
                        //flag = 0;
                    }
                    else
                    {
                        lblMessage.Visible = true;
                        lblMessage.Text = "签退失败！";
                    }
                }
                else
                {
                    Response.Write("未读取到数据");
                }

            }
            catch
            {
                Response.Write("<script>alert('可能是登录过期，请重新登录')</script>");
                //Response.Redirect("~/JsCommon/Login.aspx");
            }
            finally
            {

            }
        }
        ShowButton();
        state();
        rptBind();
    }
    public void state()
    {
        if (lbPage.Text.ToString() == "1" && lbPage.Text.ToString() == lbCount.Text.ToString())
        {
            lbtnFirst.Enabled = false;
            lbtnUp.Enabled = false;
            lbtnDown.Enabled = false;
            lbtnLast.Enabled = false;
        }
        else
        {
            if (lbPage.Text.ToString() == "1")
            {
                lbtnFirst.Enabled = false;
                lbtnUp.Enabled = false;
                lbtnDown.Enabled = true;
                lbtnLast.Enabled = true;
            }
            if (lbPage.Text.ToString() == lbCount.Text.ToString())
            {
                lbtnFirst.Enabled = true;
                lbtnUp.Enabled = true;
                lbtnDown.Enabled = false;
                lbtnLast.Enabled = false;
            }
            else if (Convert.ToInt32(lbPage.Text) > 1 && Convert.ToInt32(lbPage.Text) < Convert.ToInt32(lbCount.Text))
            {
                lbtnFirst.Enabled = true;
                lbtnUp.Enabled = true;
                lbtnDown.Enabled = true;
                lbtnLast.Enabled = true;
            }
        }
        
        
    }
    protected void lbtnFirst_Click(object sender, EventArgs e)
    {
        lbPage.Text = "1";
        rptBind();
        state();
    }
    protected void lbtnUp_Click(object sender, EventArgs e)
    {
        lbPage.Text = (Convert.ToInt32(lbPage.Text) - 1).ToString();
        rptBind();
        state();
    }
    protected void lbtnDown_Click(object sender, EventArgs e)
    {
        lbPage.Text = (Convert.ToInt32(lbPage.Text) + 1).ToString();
        rptBind();
        state();
    }
    protected void lbtnLast_Click(object sender, EventArgs e)
    {
        lbPage.Text = (Convert.ToInt32(lbCount.Text)).ToString();
        rptBind();
        state();
    }
    protected void lbtnGo_Click(object sender, EventArgs e)
    {
        lbPage.Text = DropDownList1.SelectedValue;
        rptBind();
        state();
    }
}