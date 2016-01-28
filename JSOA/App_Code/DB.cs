using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Security.Cryptography;

/// <summary>
/// 公共类用于执行各种数据库操作及公共方法
/// </summary>
public class DB
{
	public DB()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}
    /// <summary>
    /// 连接数据库
    /// </summary>
    /// <returns>返回SqlConnection对象</returns>
    public SqlConnection GetCon()
    {
        SqlConnection scon = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"].ToString());
        return scon;
    }
    /// <summary>
    /// 执行SQL语句
    /// </summary>
    /// <param name="cmdstr"></param>
    /// <returns>返回值为int型；成功返回1；失败返回0</returns>
    public int SqlEX(string cmdstr)
    {
        SqlConnection scon = GetCon();
        scon.Open();
        SqlCommand scmd = new SqlCommand(cmdstr,scon);
        try
        {
            scmd.ExecuteNonQuery();        //执行SQL语句，并返回受影响的行数
            return 1;                      //成功返回1
        }
        catch(Exception e)
        {
            return 0;                      //失败返回0
        }
        finally
        {
            scon.Dispose();                //释放连接对象资源
        }
    }
    /// <summary>
    /// 执行SQL查询语句
    /// </summary>
    /// <param name="cmdstr"></param>
    /// <returns>返回DataTable数据表</returns>
    public DataTable reDt(string cmdstr)
    {
        SqlConnection scon = GetCon();
        SqlDataAdapter da = new SqlDataAdapter(cmdstr, scon);
        DataSet ds = new DataSet();
        da.Fill(ds);
        return (ds.Tables[0]);
    }
    /// <summary>
    /// 执行SQL查询语句
    /// </summary>
    /// <param name="str">查询语句</param>
    /// <returns>返回SqlDataReader对象dr</returns>
    public SqlDataReader reDr(string str)
    {
        SqlConnection scon = GetCon();      //连接数据库
        scon.Open();
        SqlCommand scom = new SqlCommand(str, scon);
        SqlDataReader dr = scom.ExecuteReader(CommandBehavior.CloseConnection);
        return dr;                          //返回SqlDataReader对象dr
    }
    /// <summary>
    /// MD5加密
    /// </summary>
    /// <param name="strPwd">被加密的字符串</param>
    /// <returns>返回加密后的字符串</returns>
    //public string GetMD5(string strPwd)
    //{
    //    MD5 md5 = new MD5CryptoServiceProvider();
    //    byte[] data = System.Text.Encoding.Default.GetBytes(strPwd);    //将字符编码为一个字节系列
    //    byte[] md5data = md5.ComputeHash(data);                         //计算data字节数组的哈希值
    //    md5.Clear();
    //    string str = "";
    //    for (int i=0;i<md5data.Length-1;i++)
    //    {
    //        str += md5data[i].ToString("x").PadLeft(2, '0');
    //    }
    //    return str;
    //}
}