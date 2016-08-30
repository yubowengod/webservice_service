using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using System.Data.OracleClient;


namespace WebApplication1
{
    /// <summary>
    /// 一个操作数据库的类，所有对SQLServer的操作都写在这个类中，使用的时候实例化一个然后直接调用就可以
    /// </summary>
    public class DBOperation:IDisposable
    {
        public static OracleConnection OracleCon;  //用于连接数据库

        //将下面的引号之间的内容换成上面记录下的属性中的连接字符串
        private String ConServerStr = @"Data Source=WSGA1;User ID=xwpolice;Password=njust8032;Unicode=True";
        
        //默认构造函数
        public DBOperation()
        {
            if (OracleCon == null)
            {
                OracleCon = new OracleConnection();
                OracleCon.ConnectionString = ConServerStr;
                OracleCon.Open();
            }
        }
         
        //关闭/销毁函数，相当于Close()
        public void Dispose()
        {
            if (OracleCon != null)
            {
                OracleCon.Close();
                OracleCon = null;
            }
        }
        
        /// <summary>
        /// 获取所有货物的信息
        /// </summary>
        /// <returns>所有货物信息</returns>
        public List<string> selectAllCargoInfor()
        {
            List<string> list = new List<string>();

            try
            {
                string Oracle = "select * from C order by Cno desc";
                OracleCommand cmd = new OracleCommand(Oracle,OracleCon);
                OracleDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    //将结果集信息添加到返回向量中
                    list.Add(reader[0].ToString());
                    list.Add(reader[1].ToString());
                    list.Add(reader[2].ToString());

                }

                reader.Close();
                cmd.Dispose();

            }
            catch(Exception)
            {

            }
            return list;
        }

        /// <summary>
        /// 增加一条货物信息
        /// </summary>
        /// <param name="Cname">货物名称</param>
        /// <param name="Cnum">货物数量</param>
        public bool insertCargoInfo(string Cname, int Cnum)
        {
            try
            {
                string Oracle = "insert into C (Cname,Cnum) values ('" + Cname + "'," + Cnum + ")";
                OracleCommand cmd = new OracleCommand(Oracle, OracleCon);
                cmd.ExecuteNonQuery();
                cmd.Dispose();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 删除一条货物信息
        /// </summary>
        /// <param name="Cno">货物编号</param>
        public bool deleteCargoInfo(string Cno)
        {
            try
            {
                string Oracle = "delete from C where Cno=" + Cno;
                OracleCommand cmd = new OracleCommand(Oracle, OracleCon);
                cmd.ExecuteNonQuery();
                cmd.Dispose();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}