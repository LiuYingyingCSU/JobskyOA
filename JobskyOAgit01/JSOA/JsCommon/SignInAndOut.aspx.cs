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
        //内容页页不含<html>等标签，通过HtmlLink类引用外部样式
        AddLinkedStyle("/CSS/SignIn.css");
        if (Session["jobskyerID"] == null)
        {
            Response.Write("<script>alert('登录过期，请尝试重新登录！')</script>");
            Response.Redirect("~/JsCommon/Login.aspx");
        }
        
        if(!IsPostBack)
        {
            //判断Button显示与否
            ImgbtnSignIn.Enabled = false;
            ImgbtnSignIn.Visible = false;
            DLSignInTo.Enabled = false;
            DLSignInTo.Visible = false;
            //ImgbtnSignOut.Enabled = false;
            lblMessage.Visible = true;
            lblMessage.Text = "当前不在签到时间内";
            ShowButton();
            rptBind();  
            DlistBind();
        }
        state();
    }
    protected void AddLinkedStyle(string url)
    {
        HtmlLink hLink = new HtmlLink();
        hLink.Attributes.Add("type", "text/css");
        hLink.Attributes.Add("rel", "stylesheet");
        hLink.Attributes.Add("href", url);
        Page.Header.Controls.Add(hLink);
    }
    public void DlistBind()    //下拉框数据绑定
    {
        DB db = new DB();
        SqlConnection myCon = db.GetCon();
        SqlCommand myCom = new SqlCommand();
        myCon.Open();
        myCom = new SqlCommand("select count(*) FROM myDutyTime WHERE jobskyerID='" + Session["jobskyerID"] + "'", myCon); //获得该组文件的总个数
        this.lbCount.Text = (Convert.ToInt32(myCom.ExecuteScalar())/4).ToString();  //算出总页数为DropdownList赋值
        int[] num = new int[Convert.ToInt32(lbCount.Text)];
        //Response.Write(lbCount.Text.ToString());
        //Response.Write(lbPage.Text.ToString());
        myCom.Dispose();
        myCon.Close();
        for (int i = 1; i <= Convert.ToInt32(lbCount.Text); i++)
        {
            num[i - 1] = i;
        }
        DropDownList1.DataSource = num;
        DropDownList1.DataBind();
    }
    protected void rptBind()        //repeater数据绑定
    {
        DB db = new DB();
        int n = 4 * (Convert.ToInt32(lbPage.Text) - 1);
        SqlConnection myCon = db.GetCon();
        myCon.Open();
        string sqlstr = "SELECT TOP 4 jobskyerID,dutyInTime,dutyOutTime,toJobskyerID,flag0,flag1 FROM myDutyTime WHERE jobskyerID='" + Session["jobskyerID"] + "' and jobskyerID not in (select top (@n) jobskyerID from FILES where jobskyerID='" + Session["jobskyerID"] + "')";
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
    public string GetJobName(object jobskyerID)             //把jobskyerID转化为jobName输出
    {
        DB db = new DB();
        SqlDataReader dr = db.reDr("SELECT jobName FROM jobskyer WHERE jobskyerID='" + jobskyerID + "'");
        dr.Read();
        return dr.GetValue(0).ToString();
    }
    public string GetState0(object flag0)
    {
        string state;
        if (flag0.ToString() == "0")
        {
            state = "准时";
        }
        else
        {
            state = "迟到";
        }
        return state;
    }
    public string GetState1(object flag1)
    {
        string state;
        if (flag1.ToString().Trim() == "0")
        {
            state = "准时";
        }
        else if (flag1.ToString().Trim() == "1")
        {
            state = "早退";
        }
        else
        {
            state = "未签退";
        }
        return state;
    }
    private void ShowButton()
    {
        int flag = 0;//用flag标记用户的当前状态，0：未值班期间，1：值班期间
        DB db = new DB();
        DateTime nowTime = DateTime.Now;
        string sqlstr = "SELECT * FROM dutyTimeTable;SELECT * FROM myDutyTime;SELECT jobskyerID,jobName FROM jobskyer";
        DataSet ds = db.reDt(sqlstr).DataSet;
        DateTime dutyInTime=new DateTime();
        DateTime dutyOutTime=new DateTime();
        //先判断签退和代班签退，签退优先于代班签退
        //判断表中是否非空
        if(ds.Tables[0].Rows.Count>0&&ds.Tables[1].Rows.Count>0&&ds.Tables[2].Rows.Count>0)
        {
            //Response.Write("1");
            for(int i=0;i<ds.Tables[1].Rows.Count;i++)
            {
                //当天有值班人为当前用户的未签退记录
                if(ds.Tables[1].Rows[i]["jobskyerID"].ToString().Trim()==Session["jobskyerID"].ToString().Trim()&&ds.Tables[1].Rows[i]["flag1"].ToString()=="2"&&Convert.ToDateTime(ds.Tables[1].Rows[i]["dutyInTime"]).ToShortDateString()==nowTime.ToShortDateString())
                {
                    //查找被值班人的理论签退时间
                    //Response.Write("...");
                    for (int j = 0; j < ds.Tables[0].Rows.Count;j++ )
                    {
                        //找到
                        if (ds.Tables[0].Rows[j]["jobskyerID"].ToString().Trim() == ds.Tables[1].Rows[i]["toJobskyerID"].ToString().Trim() && Convert.ToDateTime(ds.Tables[0].Rows[j]["dutyOutTime"]).DayOfWeek == nowTime.DayOfWeek)
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
                                if (ds.Tables[1].Rows[i]["flag2"].ToString() == "2" && ds.Tables[2].Rows[k]["jobskyerID"].ToString().Trim() == ds.Tables[1].Rows[i]["toJobskyerID"].ToString().Trim())
                                {
                                    //获得被代班人的名字传到下面签退里
                                    //判断是否在代班签退允许时间范围内，显示代班签到签退按钮
                                    Session["toJobName"] = ds.Tables[2].Rows[k]["jobName"].ToString().Trim();
                                    //Response.Write(Session["toJobName"]);
                                    Session["toJobskyerID"] = ds.Tables[1].Rows[i]["toJobskyerID"].ToString();
                                    //ImgbtnSignOut.ImageUrl="~/Image/Sign/SignOutTo.PNG";
                                    ImgbtnSignIn.Visible = true;
                                    ImgbtnSignIn.Enabled = true;
                                    lblMessage.Visible = true;
                                    //Response.Write(ds.Tables[1].Rows[i]["toJobskyerID"].ToString());
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
                        else if(dutyOutTime.DayOfWeek==nowTime.DayOfWeek&&differOfTime(nowTime,dutyInTime)>-30&&differOfTime(dutyOutTime,nowTime)>30)
                        {
                            
                            for (int k = 0; k < ds.Tables[2].Rows.Count; k++)
                            {
                                if (ds.Tables[2].Rows[k]["jobskyerID"].ToString().Trim() == ds.Tables[1].Rows[i]["toJobskyerID"].ToString().Trim())
                                {
                                    //Session["toJobName"] = ds.Tables[2].Rows[k]["jobName"].ToString().Trim();

                                    if (Session["jobskyerID"].ToString().Trim() == ds.Tables[1].Rows[i]["toJobskyerID"].ToString().Trim())
                                    {
                                        
                                        //flag = 1;
                                        lblMessage.Visible = true;
                                        lblMessage.Text =  Session["jobName"].ToString()+ "正在值班,未到签退时间";
                                        DLSignInTo.Visible = false;
                                        ImgbtnSignIn.Enabled = false;
                                        ImgbtnSignIn.Visible = false;
                                    }
                                    else
                                    {
                                        //flag = 1;
                                        lblMessage.Visible = true;
                                        ImgbtnSignIn.Enabled = false;
                                        ImgbtnSignIn.Visible = false;
                                        DLSignInTo.Visible = false;
                                        //DLSignInTo.Enabled = false;
                                        //DLSignInTo.Visible = false;
                                        //DLSignInTo.SelectedValue = Session["toJobName"].ToString();
                                        Session["toJobName"] = ds.Tables[2].Rows[k]["jobName"].ToString();
                                        lblMessage.Text = Session["jobName"].ToString() + "正在给" + Session["toJobName"] + "值班，未到签退时间";
                                       }
                                }
                            }
                        }
                        else if(differOfTime(nowTime,dutyOutTime)>60)
                        {
                            //Response.Write("3");
                            //Response.Write(dutyOutTime);
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
            //Response.Write("3");
            for(int i=0;i<ds.Tables[0].Rows.Count;i++)
            {
                if(ds.Tables[0].Rows[i]["jobskyerID"].ToString().Trim()==Session["jobskyerID"].ToString())
                {
                    if(ds.Tables[1].Rows.Count>0)
                    {
                        for (int j = 0; j < ds.Tables[1].Rows.Count; j++)
                        {
                            //Response.Write(ds.Tables[1].Rows[j]["flag2"].ToString() == "2");
                            //检查当天是否已值班（被代班）
                            if (Convert.ToDateTime(ds.Tables[1].Rows[j]["dutyInTime"].ToString()).DayOfWeek == nowTime.DayOfWeek && ds.Tables[1].Rows[j]["jobskyerID"].ToString() != Session["jobskyerID"].ToString() && ds.Tables[1].Rows[j]["toJobskyerID"].ToString() == Session["jobskyerID"].ToString() && nowTime.DayOfWeek.ToString() == Convert.ToDateTime(ds.Tables[0].Rows[i]["dutyInTime"]).DayOfWeek.ToString() && ds.Tables[1].Rows[j]["flag2"].ToString() == "2")
                            {
                                SqlDataReader dr = db.reDr("SELECT jobName FROM jobskyer WHERE jobskyerID='" + ds.Tables[1].Rows[j]["jobskyerID"] + "'"); //and dutyInTime='"+nowTime.ToString()+"'");
                                dr.Read();
                                lblMessage.Visible = true;
                                lblMessage.Text = "正在被" + dr.GetValue(0) + "代班";
                                flag = 0;
                                ImgbtnSignIn.Enabled = false;
                                ImgbtnSignIn.Visible = false;
                                break;
                            }
                            else if (j == ds.Tables[1].Rows.Count - 1 && ds.Tables[1].Rows[j]["toJobskyerID"] != Session["jobskyerID"])
                            {
                                dutyInTime = Convert.ToDateTime(ds.Tables[0].Rows[i]["dutyInTime"].ToString());
                                //判断当前时间是否在签到时间内
                                if (dutyInTime.DayOfWeek == nowTime.DayOfWeek && differOfTime(Convert.ToDateTime(dutyInTime), nowTime) <= 30 && differOfTime(Convert.ToDateTime(dutyInTime), nowTime) >= -60)
                                {
                                    flag = 1;
                                    lblMessage.Visible = false;
                                    ImgbtnSignIn.Enabled = true;
                                    ImgbtnSignIn.Visible = true;
                                    ImgbtnSignIn.ImageUrl = "~/Image/Sign/SignIn.PNG";//不必要
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        dutyInTime = Convert.ToDateTime(ds.Tables[0].Rows[i]["dutyInTime"].ToString());
                        //判断当前时间是否在签到时间内
                        if (dutyInTime.DayOfWeek == nowTime.DayOfWeek && differOfTime(Convert.ToDateTime(dutyInTime), nowTime) <= 30 && differOfTime(Convert.ToDateTime(dutyInTime), nowTime) >= -60)
                        {
                            flag = 1;
                            lblMessage.Visible = false;
                            ImgbtnSignIn.Enabled = true;
                            ImgbtnSignIn.Visible = true;
                            ImgbtnSignIn.ImageUrl = "~/Image/Sign/SignIn.PNG";//不必要
                            break;
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
                    if (ds.Tables[1].Rows.Count==0)
                    {
                        DLSignInTo.Visible = true;
                        //lblMessage.Visible = true;
                            DLSignInTo.Enabled = true;
                            ImgbtnSignIn.Enabled = true;
                            ImgbtnSignIn.Visible = true;
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
                    else
                    {
                        for (int j = 0; j < ds.Tables[1].Rows.Count; j++)
                        {
                            //如果在值班当天记录里找到此班次，则不加入下拉框(判断被代班人员ID,而不是值班人员ID)
                            if (ds.Tables[1].Rows[j]["toJobskyerID"].ToString() == ds.Tables[0].Rows[i]["jobskyerID"].ToString().Trim() && Convert.ToDateTime(ds.Tables[1].Rows[j]["dutyInTime"].ToString().Trim()).DayOfWeek == nowTime.DayOfWeek)
                            {
                                break;
                            }
                            //如果没有找到该班次值班记录，判断当前时间是否在签到范围内，若是，放进下拉列表，可见
                            //当查找到最后一条记录或者下一条记录非当天，则加入下拉列表中
                            else if (j + 1 == ds.Tables[1].Rows.Count)//|| Convert.ToDateTime(ds.Tables[0].Rows[j]["dutyInTime"].ToString()).DayOfWeek!=nowTime.DayOfWeek)
                            {
                                DLSignInTo.Visible = true ;
                                DLSignInTo.Enabled = true;
                                lblMessage.Visible = false;
                                ImgbtnSignIn.Enabled = true;
                                ImgbtnSignIn.Visible = true;
                                //ImgbtnSignOut.ImageUrl = "~/Image/Sign/SignOutTo.PNG";
                                ImgbtnSignIn.ImageUrl = "~/Image/Sign/SignInTo.PNG";
                                Session["toJobskyerID"] = ds.Tables[0].Rows[i]["jobskyerID"];
                                for (int k = 0; k < ds.Tables[2].Rows.Count; k++)
                                {
                                    if (Session["toJobskyerID"].ToString() == ds.Tables[2].Rows[k]["jobskyerID"].ToString())
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
    }
    private int differOfTime(DateTime dateT1,DateTime dateT2)
    {
        //计算两时间差
        DateTime t1 = new DateTime();
        DateTime t2 = new DateTime();
        t1 = Convert.ToDateTime(dateT1.ToLongTimeString());
        t2 = Convert.ToDateTime(dateT2.ToLongTimeString());
        TimeSpan ts1 = new TimeSpan(t1.Ticks);
        TimeSpan ts2 = new TimeSpan(t2.Ticks);
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

                string dutyInTimeSql = "SELECT dutyInTime FROM dutyTimeTable WHERE jobskyerID='" + Session["jobskyerID"] + "'";//and dutyInTime='"+nowTime.ToString()+"'";
                SqlDataReader dutyInTimeDr = db.reDr(dutyInTimeSql);
                dutyInTimeDr.Read();
                if(dutyInTimeDr.GetValue(0).ToString()!=null)
                {          
                    int flag0=0;
                    DateTime dutyInTime = Convert.ToDateTime(dutyInTimeDr.GetValue(0).ToString());
                    if (dutyInTime.DayOfWeek == nowTime.DayOfWeek && differOfTime(Convert.ToDateTime(dutyInTime), nowTime) >= 0 && dutyInTime.DayOfWeek == nowTime.DayOfWeek && differOfTime(Convert.ToDateTime(dutyInTime), nowTime)<=30)
                    {
                        flag0=0;
                       
                    }
                    else if (differOfTime(Convert.ToDateTime(dutyInTime), nowTime) < 0 && dutyInTime.DayOfWeek == nowTime.DayOfWeek && differOfTime(Convert.ToDateTime(dutyInTime), nowTime)>=-60)
                    {
                        flag0=1;
                    }
                    string recordInsert = "INSERT INTO myDutyTime(jobskyerID,dutyInTime,flag0,toJobskyerID) values('" + Session["jobskyerID"] + "','" + nowTime.ToString() + "','" + flag0 + "','" + Session["jobskyerID"] + "')";
                    Session["dutyInTime"] = nowTime.ToString();
                    if(db.SqlEX(recordInsert)==1)
                    {
                        Response.Write("<script>alert('签到成功')</script>");
                        lblMessage.Visible = true;
                        //lblMessage.Text = Session["jobName"] + "正在值班...";
                        //flag = 1;
                    }
                    else
                    {
                        lblMessage.Visible = true;
                        lblMessage.Text="签到失败！";
                    }
                }
                dutyInTimeDr.Close();
                
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
                Session["toJobName"] = DLSignInTo.SelectedValue.ToString();
                SqlDataReader toJobId = db.reDr("SELECT JobskyerID FROM jobskyer WHERE jobName='" + Session["toJobName"] + "'");
                toJobId.Read();
                if (toJobId.HasRows)
                {
                    Session["toJobskyerID"] = toJobId.GetValue(0).ToString();
                }
                string dutyInTimeSql = "SELECT dutyInTime FROM dutyTimeTable WHERE jobskyerID='" + Session["toJobskyerID"] + "'";//and dutyInTime='"+nowTime.ToString()+"'";
                SqlDataReader dutyInTimeDr = db.reDr(dutyInTimeSql);
                dutyInTimeDr.Read();
                if (dutyInTimeDr.GetValue(0).ToString() != null)
                {
                    int flag0 = 0;
                    DateTime dutyInTime = Convert.ToDateTime(dutyInTimeDr.GetValue(0).ToString());
                    if (dutyInTime.DayOfWeek == nowTime.DayOfWeek && differOfTime(Convert.ToDateTime(dutyInTime), nowTime) >= 0 && dutyInTime.DayOfWeek == nowTime.DayOfWeek && differOfTime(Convert.ToDateTime(dutyInTime), nowTime) <= 30)
                    {
                        //准时签到
                        flag0 = 0;

                    }
                    else if (differOfTime(Convert.ToDateTime(dutyInTime), nowTime) < 0 && dutyInTime.DayOfWeek == nowTime.DayOfWeek && differOfTime(Convert.ToDateTime(dutyInTime), nowTime) >= -60)
                    {
                        //迟到
                        flag0 = 1;
                    }
                    string recordInsert = "INSERT INTO myDutyTime(jobskyerID,dutyInTime,flag0,toJobskyerID) values('" + Session["jobskyerID"] + "','" + nowTime.ToString() + "','" + flag0 + "','" + Session["toJobskyerID"] + "')";
                    Session["dutyInTime"] = nowTime.ToString();
                    if (db.SqlEX(recordInsert) == 1)
                    {
                        //签到成功                        
                        DLSignInTo.Visible = false;
                        DLSignInTo.Enabled = false;
                        ImgbtnSignIn.Visible = false;
                        ImgbtnSignIn.Enabled = false;
                        lblMessage.Visible = true;
                        lblMessage.Text = Session["jobName"] + "给" + DLSignInTo.SelectedValue.ToString() + "代班签到成功";
                        Response.Write("<script>alert('" + Session["jobName"] + "给" + DLSignInTo.SelectedValue.ToString() + "代班签到成功')</script>");
                        //flag = 1;
                    }
                    else
                    {
                        lblMessage.Visible = true;
                        lblMessage.Text = "签到失败！";
                    }
                }
                toJobId.Close();
                dutyInTimeDr.Close();
                
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
                string dutyOutTimeSql = "SELECT dutyOutTime FROM dutyTimeTable WHERE jobskyerID='" + Session["jobskyerID"] + "'";//and dutyInTime='"+nowTime.ToString()+"'";
                SqlDataReader dutyOutTimeDr = db.reDr(dutyOutTimeSql);
                dutyOutTimeDr.Read();
                if (dutyOutTimeDr.GetValue(0).ToString() != null)
                {
                    //flag2  2:未签退 0：按时签退 1：早退
                    int flag1 = 2;
                    DateTime dutyOutTime = Convert.ToDateTime(dutyOutTimeDr.GetValue(0).ToString());
                    if (dutyOutTime.DayOfWeek == nowTime.DayOfWeek && differOfTime(Convert.ToDateTime(dutyOutTime), nowTime) <= 0 && dutyOutTime.DayOfWeek == nowTime.DayOfWeek && differOfTime(Convert.ToDateTime(dutyOutTime), nowTime) >= -60)
                    {
                        flag1 = 0;
                    }
                    else if (differOfTime(Convert.ToDateTime(dutyOutTime), nowTime) < 30 && dutyOutTime.DayOfWeek == nowTime.DayOfWeek && differOfTime(Convert.ToDateTime(dutyOutTime), nowTime) > 0)
                    {
                        flag1 = 1;
                    }
                    string recordUpdate1 = "UPDATE myDutyTime set flag1='" + flag1 + "' WHERE jobskyerID='" + Session["jobskyerID"] + "'and dutyRecordID='" + Session["dutyRecordID"] + "'";
                    string recordUpdate2 = "UPDATE myDutyTime set dutyOutTime='" + nowTime.ToString() + "' WHERE jobskyerID='" + Session["jobskyerID"] + "'and dutyRecordID='" + Session["dutyRecordID"] + "'";
                    if (db.SqlEX(recordUpdate2) == 1 && db.SqlEX(recordUpdate1) == 1)
                    {
                        lblMessage.Visible = true;
                        //Response.Write(dutyRecordID);
                        lblMessage.Text = Session["jobName"] + "签退成功";
                        ImgbtnSignIn.Enabled = false;
                        ImgbtnSignIn.Visible = false;
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
                dutyOutTimeDr.Close();
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
                string dutyOutTimeSql = "SELECT dutyOutTime FROM dutyTimeTable WHERE jobskyerID='" + Session["toJobskyerID"] + "'";//and dutyInTime='"+nowTime.ToString()+"'";
                SqlDataReader dutyOutTimeDr = db.reDr(dutyOutTimeSql);
                dutyOutTimeDr.Read();
                //string dutyOutTimeDr = db.reDr(dutyOutTimeSql).ToString();
                //Response.Write("2");
                //Response.Write(Session["toJobskyerID"]);
                //Response.Write(dutyOutTimeDr.GetValue(0).ToString());
                if (dutyOutTimeDr.HasRows)
                {
                    int flag1 = 2;
                    //DateTime dutyOutTime = Convert.ToDateTime(dutyOutTimeDr);
                    DateTime dutyOutTime = Convert.ToDateTime(dutyOutTimeDr.GetValue(0).ToString());
                    if (dutyOutTime.DayOfWeek == nowTime.DayOfWeek && differOfTime(Convert.ToDateTime(dutyOutTime), nowTime) <= 0 && dutyOutTime.DayOfWeek == nowTime.DayOfWeek && differOfTime(Convert.ToDateTime(dutyOutTime), nowTime) >= -60)
                    {
                        flag1 = 0;

                    }
                    else if (differOfTime(Convert.ToDateTime(dutyOutTime), nowTime) <= 30 && dutyOutTime.DayOfWeek == nowTime.DayOfWeek && differOfTime(nowTime,Convert.ToDateTime(dutyOutTime)) < 60)
                    {
                        flag1 = 1;
                    }
                    //Response.Write(Convert.ToDateTime(dutyOutTime).ToString());
                    string recordUpdate1 = "UPDATE myDutyTime set flag2='" + flag1 + "' WHERE jobskyerID='" + Session["jobskyerID"] + "'and dutyRecordID='" + Session["dutyRecordID"] + "'";
                    string recordUpdate2 = "UPDATE myDutyTime set dutyOutTime='" + nowTime.ToString() + "' WHERE jobskyerID='" + Session["jobskyerID"] + "'and dutyRecordID='" + Session["dutyRecordID"] + "'";

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
                dutyOutTimeDr.Close();

            }
            catch
            {
                //Response.Write(ee);
                //Response.Write(dutyOutTimeDr.GetValue(0).ToString());
                Response.Write("<script>alert('登录过期，请重新登录')</script>");
                Response.Redirect("~/JsCommon/Login.aspx");
            }
            finally
            {

            }
        }
        ShowButton();
        state();
        rptBind();
    }
    public void state()        //判断首页、上页、下页、尾页的可用状态
    {
        if (lbCount.Text.ToString()=="1"||lbCount.Text.ToString()=="0")  //仅一页或无信息
        {
            lbtnGo.Enabled = false;
            DropDownList1.SelectedValue = "1";
            lbCount.Text = "1";
            lbtnFirst.Enabled = false;
            lbtnUp.Enabled = false;
            lbtnDown.Enabled = false;
            lbtnLast.Enabled = false;
        }
        else
        {
            if (lbPage.Text.ToString() == "1")            //当前页为1，上一页不可用
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
    //每一次点击选页按钮，都要进行一次数据绑定以及按钮状态判断
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