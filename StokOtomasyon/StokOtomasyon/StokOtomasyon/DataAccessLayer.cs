using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;


namespace StokOtomasyon
{
    public static class DataAccessLayer
    {
        
        public static SqlConnection baglantiAyarla()
        {

            SqlConnection sql = new SqlConnection();
          
            sql.ConnectionString = ConfigurationManager.ConnectionStrings["sql"].ConnectionString;
            if (sql.State == ConnectionState.Closed)
            {
                sql.Open();

            }
            else
            {
                sql.Close();
            }
            return sql;

            
        }
    }
}