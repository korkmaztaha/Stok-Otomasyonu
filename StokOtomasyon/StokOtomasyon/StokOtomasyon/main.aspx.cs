using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using OfficeOpenXml;
using System.IO;

namespace StokOtomasyon
{
    public partial class main : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            Mitra.oturum();

            SqlCommand sql = new SqlCommand();
            sql.Connection = StokOtomasyon.DataAccessLayer.baglantiAyarla();
            sql.CommandType = CommandType.StoredProcedure;
            sql.CommandText = "sp_UrunListele";
            SqlDataAdapter da1 = new SqlDataAdapter();
            DataTable dt1 = new DataTable();
            da1.SelectCommand = sql;
            da1.Fill(dt1);
            rptTbl.DataSource = dt1;
            rptTbl.DataBind();
            DataAccessLayer.baglantiAyarla();


        }
        

        protected void rptTbl_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
        }

        protected void btn_Excel_Click(object sender, EventArgs e)
        {
            string GuidKey = Guid.NewGuid().ToString();

            SqlCommand cmd = new SqlCommand();
            urunfiltrele(cmd);

            DataTable t1 = new DataTable();
            using (SqlDataAdapter a = new SqlDataAdapter(cmd))
            {
                a.Fill(t1);
            }

            //string filePath = @"C:\Users\korkm\source\repos\StokOtomasyon\StokOtomasyon\Excel\" + GuidKey + ".xlsx";
            string filePath = HttpContext.Current.Server.MapPath("~/Excel") + "\\" + GuidKey + ".xlsx";
            var file = new FileInfo(filePath);

            using (ExcelPackage pck = new ExcelPackage(file))
            {
                ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Rapor");
                ws.Cells["A1"].LoadFromDataTable(t1, true);
                pck.Save();
                Response.Redirect("Excel/" + GuidKey + ".xlsx");
            }
            StokOtomasyon.DataAccessLayer.baglantiAyarla();
        }

        public void urunfiltrele(SqlCommand cmd)
        {
            cmd.Connection = StokOtomasyon.DataAccessLayer.baglantiAyarla();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "sp_UrunFiltrele";
            cmd.Parameters.AddWithValue("@UrunAdi", Mitra.IsDbNull(txt_UrunAdi.Text, "-x"));
            cmd.Parameters.AddWithValue("@UrunGrubu", Mitra.IsDbNull(txt_Grubu.Text, "-x"));
            cmd.Parameters.AddWithValue("@UrunMarka", Mitra.IsDbNull(txt_Marka.Text, "-x"));
            cmd.Parameters.AddWithValue("@UrunKdv", Mitra.IsDbNull(oran.Text, -999));
            cmd.Parameters.AddWithValue("@UrunSatisFiyatBaslangic", Mitra.IsDbNull(txt_baslangicFiyat.Text, -999));
            cmd.Parameters.AddWithValue("@UrunSatisFiyatBitis", Mitra.IsDbNull(txt_bitisFiyat.Text, -999));
            cmd.Parameters.AddWithValue("@UrunStoktakiMiktarBaslangic", Mitra.IsDbNull(txt_baAdet.Text, -999));
            cmd.Parameters.AddWithValue("@UrunStoktakiMiktarBitis", Mitra.IsDbNull(txt_biAdet.Text, -999));
        }

        protected void btn_Ara_Click(object sender, EventArgs e)
        {
            if (kdv_8.Checked)
            {
                oran.Text = "8";
            }
            else if (kdv_18.Checked)
            {
                oran.Text = "18";
            }
            else if (kdv_1.Checked)
            {
                oran.Text = "1";
            }

            SqlCommand cmd = new SqlCommand();
            urunfiltrele(cmd);

            

            SqlDataAdapter da1 = new SqlDataAdapter();
            DataTable dt1 = new DataTable();
            da1.SelectCommand = cmd;
            da1.Fill(dt1);
            rptTbl.DataSource = dt1;
            rptTbl.DataBind();
            DataAccessLayer.baglantiAyarla();
           
        }
    }
}