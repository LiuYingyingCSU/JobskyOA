using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class JsInfo_DutyTable : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            DB db = new DB();
            string str = "SELECT dutyInTime,dutyOutTime,myDutyTime.jobskyerID,jobName FROM myDutyTime,JOBSKYER WHERE myDutyTime.jobskyerID=JOBSKYER.jobskyerID";
            SqlConnection con = new SqlConnection();
            con = db.GetCon();
            SqlCommand com = new SqlCommand(str, con);
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DateTime sign_in_time = new DateTime();
            DateTime sign_out_time = new DateTime();
            string f1, f2, s1, s2, th1, th2, fh1, fh2, n1, n2;
            f1 = "8:30:00"; f2 = "10:30:00";        ///第一班
            s1 = "10:30:00"; s2 = "13:00:00";       ///第二班
            th1 = "13:00:00"; th2 = "15:30:00";     ///第三班
            fh1 = "15:30:00"; fh2 = "18:30:00";     ///第四班
            n1 = "18:30:00"; n2 = "20:30:00";       ///晚班
            int ds_Row_count = ds.Tables[0].Rows.Count;
            if (ds != null && ds.Tables.Count >= 0 && ds_Row_count >= 0)
            {
                for (int i = 0; i < ds_Row_count; i++)
                {
                    sign_in_time = Convert.ToDateTime(ds.Tables[0].Rows[i]["dutyInTime"].ToString());
                    sign_out_time = Convert.ToDateTime(ds.Tables[0].Rows[i]["dutyOutTime"].ToString());
                    switch (sign_in_time.DayOfWeek)
                    {
                        case DayOfWeek.Monday:
                            {
                                ///周一第一班
                                if (Convert.ToDateTime(sign_in_time.ToLongTimeString()) <= Convert.ToDateTime(f1) && Convert.ToDateTime(sign_out_time.ToLongTimeString()) >= Convert.ToDateTime(f2))
                                {
                                    Duty_1_1.Text += ds.Tables[0].Rows[i]["jobName"].ToString() + "<br/>";
                                }
                                ///周一第二班
                                if (Convert.ToDateTime(sign_in_time.ToLongTimeString()) <= Convert.ToDateTime(s1) && Convert.ToDateTime(sign_out_time.ToLongTimeString()) >= Convert.ToDateTime(s2))
                                {
                                    Duty_2_1.Text += ds.Tables[0].Rows[i]["jobName"].ToString() + "<br/>";
                                }
                                ///周一第三班
                                if (Convert.ToDateTime(sign_in_time.ToLongTimeString()) <= Convert.ToDateTime(th1) && Convert.ToDateTime(sign_out_time.ToLongTimeString()) >= Convert.ToDateTime(th2))
                                {
                                    Duty_3_1.Text += ds.Tables[0].Rows[i]["jobName"].ToString() + "<br/>";
                                }
                                ///周一第四班
                                if (Convert.ToDateTime(sign_in_time.ToLongTimeString()) <= Convert.ToDateTime(fh1) && Convert.ToDateTime(sign_out_time.ToLongTimeString()) >= Convert.ToDateTime(fh2))
                                {
                                    Duty_4_1.Text += ds.Tables[0].Rows[i]["jobName"].ToString() + "<br/>";
                                }
                                ///周一晚班
                                if (Convert.ToDateTime(sign_in_time.ToLongTimeString()) <= Convert.ToDateTime(n1) && Convert.ToDateTime(sign_out_time.ToLongTimeString()) >= Convert.ToDateTime(n2))
                                {
                                    Duty_5_1.Text += ds.Tables[0].Rows[i]["jobName"].ToString() + "<br/>";
                                }
                                break;
                            }
                        case DayOfWeek.Tuesday:
                            {
                                ///周二第一班
                                if (Convert.ToDateTime(sign_in_time.ToLongTimeString()) <= Convert.ToDateTime(f1) && Convert.ToDateTime(sign_out_time.ToLongTimeString()) >= Convert.ToDateTime(f2))
                                {
                                    Duty_1_2.Text += ds.Tables[0].Rows[i]["jobName"].ToString() + "<br/>";
                                }
                                ///周二第二班
                                if (Convert.ToDateTime(sign_in_time.ToLongTimeString()) <= Convert.ToDateTime(s1) && Convert.ToDateTime(sign_out_time.ToLongTimeString()) >= Convert.ToDateTime(s2))
                                {
                                    Duty_2_2.Text += ds.Tables[0].Rows[i]["jobName"].ToString() + "<br/>";
                                }
                                ///周二第三班
                                if (Convert.ToDateTime(sign_in_time.ToLongTimeString()) <= Convert.ToDateTime(th1) && Convert.ToDateTime(sign_out_time.ToLongTimeString()) >= Convert.ToDateTime(th2))
                                {
                                    Duty_3_2.Text += ds.Tables[0].Rows[i]["jobName"].ToString() + "<br/>";
                                }
                                ///周二第四班
                                if (Convert.ToDateTime(sign_in_time.ToLongTimeString()) <= Convert.ToDateTime(fh1) && Convert.ToDateTime(sign_out_time.ToLongTimeString()) >= Convert.ToDateTime(fh2))
                                {
                                    Duty_4_2.Text += ds.Tables[0].Rows[i]["jobName"].ToString() + "<br/>";
                                }
                                ///周二晚班
                                if (Convert.ToDateTime(sign_in_time.ToLongTimeString()) <= Convert.ToDateTime(n1) && Convert.ToDateTime(sign_out_time.ToLongTimeString()) >= Convert.ToDateTime(n2))
                                {
                                    Duty_5_2.Text += ds.Tables[0].Rows[i]["jobName"].ToString() + "<br/>";
                                }
                                break;
                            }
                        case DayOfWeek.Wednesday:
                            {
                                ///周三第一班
                                if (Convert.ToDateTime(sign_in_time.ToLongTimeString()) <= Convert.ToDateTime(f1) && Convert.ToDateTime(sign_out_time.ToLongTimeString()) >= Convert.ToDateTime(f2))
                                {
                                    Duty_1_3.Text += ds.Tables[0].Rows[i]["jobName"].ToString() + "<br/>";
                                }
                                ///周三第二班
                                if (Convert.ToDateTime(sign_in_time.ToLongTimeString()) <= Convert.ToDateTime(s1) && Convert.ToDateTime(sign_out_time.ToLongTimeString()) >= Convert.ToDateTime(s2))
                                {
                                    Duty_2_3.Text += ds.Tables[0].Rows[i]["jobName"].ToString() + "<br/>";
                                }
                                ///周三第三班
                                if (Convert.ToDateTime(sign_in_time.ToLongTimeString()) <= Convert.ToDateTime(th1) && Convert.ToDateTime(sign_out_time.ToLongTimeString()) >= Convert.ToDateTime(th2))
                                {
                                    Duty_3_3.Text += ds.Tables[0].Rows[i]["jobName"].ToString() + "<br/>";
                                }
                                ///周三第四班
                                if (Convert.ToDateTime(sign_in_time.ToLongTimeString()) <= Convert.ToDateTime(fh1) && Convert.ToDateTime(sign_out_time.ToLongTimeString()) >= Convert.ToDateTime(fh2))
                                {
                                    Duty_4_3.Text += ds.Tables[0].Rows[i]["jobName"].ToString() + "<br/>";
                                }
                                ///周三晚班
                                if (Convert.ToDateTime(sign_in_time.ToLongTimeString()) <= Convert.ToDateTime(n1) && Convert.ToDateTime(sign_out_time.ToLongTimeString()) >= Convert.ToDateTime(n2))
                                {
                                    Duty_5_3.Text += ds.Tables[0].Rows[i]["jobName"].ToString() + "<br/>";
                                }
                                break;
                            }
                        case DayOfWeek.Thursday:
                            {
                                ///周四第一班
                                if (Convert.ToDateTime(sign_in_time.ToLongTimeString()) <= Convert.ToDateTime(f1) && Convert.ToDateTime(sign_out_time.ToLongTimeString()) >= Convert.ToDateTime(f2))
                                {
                                    Duty_1_4.Text += ds.Tables[0].Rows[i]["jobName"].ToString() + "<br/>";
                                }
                                ///周四第二班
                                if (Convert.ToDateTime(sign_in_time.ToLongTimeString()) <= Convert.ToDateTime(s1) && Convert.ToDateTime(sign_out_time.ToLongTimeString()) >= Convert.ToDateTime(s2))
                                {
                                    Duty_2_4.Text += ds.Tables[0].Rows[i]["jobName"].ToString() + "<br/>";
                                }
                                ///周四第三班
                                if (Convert.ToDateTime(sign_in_time.ToLongTimeString()) <= Convert.ToDateTime(th1) && Convert.ToDateTime(sign_out_time.ToLongTimeString()) >= Convert.ToDateTime(th2))
                                {
                                    Duty_3_4.Text += ds.Tables[0].Rows[i]["jobName"].ToString() + "<br/>";
                                }
                                ///周四第四班
                                if (Convert.ToDateTime(sign_in_time.ToLongTimeString()) <= Convert.ToDateTime(fh1) && Convert.ToDateTime(sign_out_time.ToLongTimeString()) >= Convert.ToDateTime(fh2))
                                {
                                    Duty_4_4.Text += ds.Tables[0].Rows[i]["jobName"].ToString() + "<br/>";
                                }
                                ///周四晚班
                                if (Convert.ToDateTime(sign_in_time.ToLongTimeString()) <= Convert.ToDateTime(n1) && Convert.ToDateTime(sign_out_time.ToLongTimeString()) >= Convert.ToDateTime(n2))
                                {
                                    Duty_5_4.Text += ds.Tables[0].Rows[i]["jobName"].ToString() + "<br/>";
                                }
                                break;
                            }
                        case DayOfWeek.Friday:
                            {
                                ///周五第一班
                                if (Convert.ToDateTime(sign_in_time.ToLongTimeString()) <= Convert.ToDateTime(f1) && Convert.ToDateTime(sign_out_time.ToLongTimeString()) >= Convert.ToDateTime(f2))
                                {
                                    Duty_1_5.Text += ds.Tables[0].Rows[i]["jobName"].ToString() + "<br/>";
                                }
                                ///周五第二班
                                if (Convert.ToDateTime(sign_in_time.ToLongTimeString()) <= Convert.ToDateTime(s1) && Convert.ToDateTime(sign_out_time.ToLongTimeString()) >= Convert.ToDateTime(s2))
                                {
                                    Duty_2_5.Text += ds.Tables[0].Rows[i]["jobName"].ToString() + "<br/>";
                                }
                                ///周五第三班
                                if (Convert.ToDateTime(sign_in_time.ToLongTimeString()) <= Convert.ToDateTime(th1) && Convert.ToDateTime(sign_out_time.ToLongTimeString()) >= Convert.ToDateTime(th2))
                                {
                                    Duty_3_5.Text += ds.Tables[0].Rows[i]["jobName"].ToString() + "<br/>";
                                }
                                ///周五第四班
                                if (Convert.ToDateTime(sign_in_time.ToLongTimeString()) <= Convert.ToDateTime(fh1) && Convert.ToDateTime(sign_out_time.ToLongTimeString()) >= Convert.ToDateTime(fh2))
                                {
                                    Duty_4_5.Text += ds.Tables[0].Rows[i]["jobName"].ToString() + "<br/>";
                                }
                                ///周五晚班
                                if (Convert.ToDateTime(sign_in_time.ToLongTimeString()) <= Convert.ToDateTime(n1) && Convert.ToDateTime(sign_out_time.ToLongTimeString()) >= Convert.ToDateTime(n2))
                                {
                                    Duty_5_5.Text += ds.Tables[0].Rows[i]["jobName"].ToString() + "<br/>";
                                }
                                break;
                            }
                        case DayOfWeek.Saturday:
                            {
                                ///周六第一班
                                if (Convert.ToDateTime(sign_in_time.ToLongTimeString()) <= Convert.ToDateTime(f1) && Convert.ToDateTime(sign_out_time.ToLongTimeString()) >= Convert.ToDateTime(f2))
                                {
                                    Duty_1_6.Text += ds.Tables[0].Rows[i]["jobName"].ToString() + "<br/>";
                                }
                                ///周六第二班
                                if (Convert.ToDateTime(sign_in_time.ToLongTimeString()) <= Convert.ToDateTime(s1) && Convert.ToDateTime(sign_out_time.ToLongTimeString()) >= Convert.ToDateTime(s2))
                                {
                                    Duty_2_6.Text += ds.Tables[0].Rows[i]["jobName"].ToString() + "<br/>";
                                }
                                ///周六第三班
                                if (Convert.ToDateTime(sign_in_time.ToLongTimeString()) <= Convert.ToDateTime(th1) && Convert.ToDateTime(sign_out_time.ToLongTimeString()) >= Convert.ToDateTime(th2))
                                {
                                    Duty_3_6.Text += ds.Tables[0].Rows[i]["jobName"].ToString() + "<br/>";
                                }
                                ///周六第四班
                                if (Convert.ToDateTime(sign_in_time.ToLongTimeString()) <= Convert.ToDateTime(fh1) && Convert.ToDateTime(sign_out_time.ToLongTimeString()) >= Convert.ToDateTime(fh2))
                                {
                                    Duty_4_6.Text += ds.Tables[0].Rows[i]["jobName"].ToString() + "<br/>";
                                }
                                ///周六晚班
                                if (Convert.ToDateTime(sign_in_time.ToLongTimeString()) <= Convert.ToDateTime(n1) && Convert.ToDateTime(sign_out_time.ToLongTimeString()) >= Convert.ToDateTime(n2))
                                {
                                    Duty_5_6.Text += ds.Tables[0].Rows[i]["jobName"].ToString() + "<br/>";
                                }
                                break;
                            }
                        case DayOfWeek.Sunday:
                            {
                                ///周日第一班
                                if (Convert.ToDateTime(sign_in_time.ToLongTimeString()) <= Convert.ToDateTime(f1) && Convert.ToDateTime(sign_out_time.ToLongTimeString()) >= Convert.ToDateTime(f2))
                                {
                                    Duty_1_7.Text += ds.Tables[0].Rows[i]["jobName"].ToString() + "<br/>";
                                }
                                ///周日第二班
                                if (Convert.ToDateTime(sign_in_time.ToLongTimeString()) <= Convert.ToDateTime(s1) && Convert.ToDateTime(sign_out_time.ToLongTimeString()) >= Convert.ToDateTime(s2))
                                {
                                    Duty_2_7.Text += ds.Tables[0].Rows[i]["jobName"].ToString() + "<br/>";
                                }
                                ///周日第三班
                                if (Convert.ToDateTime(sign_in_time.ToLongTimeString()) <= Convert.ToDateTime(th1) && Convert.ToDateTime(sign_out_time.ToLongTimeString()) >= Convert.ToDateTime(th2))
                                {
                                    Duty_3_7.Text += ds.Tables[0].Rows[i]["jobName"].ToString() + "<br/>";
                                }
                                ///周日第四班
                                if (Convert.ToDateTime(sign_in_time.ToLongTimeString()) <= Convert.ToDateTime(fh1) && Convert.ToDateTime(sign_out_time.ToLongTimeString()) >= Convert.ToDateTime(fh2))
                                {
                                    Duty_4_7.Text += ds.Tables[0].Rows[i]["jobName"].ToString() + "<br/>";
                                }
                                ///周日晚班
                                if (Convert.ToDateTime(sign_in_time.ToLongTimeString()) <= Convert.ToDateTime(n1) && Convert.ToDateTime(sign_out_time.ToLongTimeString()) >= Convert.ToDateTime(n2))
                                {
                                    Duty_5_7.Text += ds.Tables[0].Rows[i]["jobName"].ToString() + "<br/>";
                                }
                                break;
                            }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }
}