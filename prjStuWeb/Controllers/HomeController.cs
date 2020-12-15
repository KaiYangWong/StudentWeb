using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data; //要加入的引用1
using System.Data.SqlClient; //要加入的引用2

namespace prjStuWeb.Controllers
{
    public class HomeController : Controller
    {
        //指定連接字串
        string constr = @"Data Source=(LocalDB)\MSSQLLocalDB;" +
                         "AttachDbFilename=|DataDirectory|dbStudent.mdf;" +
                         "Integrated Security=True";
       
        public ActionResult Index()
        {
            DataTable dt = querySql("SELECT * FROM tStudent");
            return View(dt);
        }

        //新增
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(string fStuId,string fName,string fEmail,int fScore)
        {
            string sql = "INSERT INTO tStudent(fStuId,fName,fEmail,fScore)VALUES('"
                         + fStuId.Replace("'", "''") + "',N'"
                         + fName.Replace("'", "''") + "','"
                         + fEmail.Replace("'", "''") + "',"
                         + fScore + ")";
            executeSql(sql);
            return RedirectToAction("Index");
        }

        //刪除
        public ActionResult Delete(string id)
        {
            string sql = "DELETE FROM tStudent WHERE fStuId="
                         + id.Replace("'", "''") + "'";
            executeSql(sql);
            DataTable dt = querySql("SELECT * FROM tStudent");
            return RedirectToAction("Index");
        }


        //傳入SQL字串來編輯資料表
        private void executeSql(string sql)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = constr;
            con.Open();
            SqlCommand cmd = new SqlCommand(sql,con);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        //傳入SQL字串並傳回DataTable物件
        private DataTable querySql(string sql)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = constr;
            SqlDataAdapter adp = new SqlDataAdapter(sql,con);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            return ds.Tables[0];
        }
    }
}