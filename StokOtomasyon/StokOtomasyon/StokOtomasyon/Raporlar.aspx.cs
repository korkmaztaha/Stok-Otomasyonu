using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OfficeOpenXml;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Data.Sql;

namespace StokOtomasyon
{
    public partial class Raporlar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Mitra.oturum();
                      

            SqlDataReader dr;
            if (!IsPostBack)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = StokOtomasyon.DataAccessLayer.baglantiAyarla();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_FirmaGetir";
                dr = cmd.ExecuteReader();
                ddl_Sirketİsmi.DataSource = dr;
                ddl_Sirketİsmi.DataBind();
                ddl_Sirketİsmi.Items.Insert(0, new ListItem("Seçiniz...", "0"));
                dr.Close();
            }

            SqlCommand sql = new SqlCommand();
            sql.Connection = StokOtomasyon.DataAccessLayer.baglantiAyarla();
            sql.CommandType = CommandType.StoredProcedure;
            sql.CommandText = "sp_TumFaturalariListele";
            SqlDataAdapter da1 = new SqlDataAdapter();
            DataTable dt1 = new DataTable();
            da1.SelectCommand = sql;
            da1.Fill(dt1);
            rptTbl.DataSource = dt1;
            rptTbl.DataBind();
            DataAccessLayer.baglantiAyarla();

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

            if (rd_Alis.Checked)
            {
                fTip.Text = "Alis";
            }
            else if (rdSatis.Checked)
            {
                fTip.Text = "Satis";
            }
           
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = StokOtomasyon.DataAccessLayer.baglantiAyarla();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "sp_FaturaFilitrele";
            cmd.Parameters.AddWithValue("@Sirket",Mitra.IsDbNull(ddl_Sirketİsmi.SelectedItem.Text,"-x"));
            cmd.Parameters.AddWithValue("@Kdv",Mitra.IsDbNull(oran.Text,-999));
            cmd.Parameters.AddWithValue("@FaturaNumara",Mitra.IsDbNull(txt_FaturaNo.Text,"-x"));
            cmd.Parameters.AddWithValue("@UrunIsmi", Mitra.IsDbNull(txt_Urun.Text,"-x"));
            cmd.Parameters.AddWithValue("@BaslagicFiyati",Mitra.IsDbNull(txt_BaslangicFiyat.Text,-999));
            cmd.Parameters.AddWithValue("@BitisFiyati", Mitra.IsDbNull(txt_bitisFiyat.Text,-999));
            cmd.Parameters.AddWithValue("@FaturaTipi",Mitra.IsDbNull(fTip.Text,"-x"));
            cmd.Parameters.AddWithValue("@BitisTarihi", Mitra.TDate(this.txt_bitisTarih.Text, Mitra.TDateType.ddMMyyyy));
            cmd.Parameters.AddWithValue("@BaslangicTarihi", Mitra.TDate(this.txt_baslangicTarih.Text, Mitra.TDateType.ddMMyyyy));
         

            SqlDataAdapter da1 = new SqlDataAdapter();
            DataTable dt1 = new DataTable();
            da1.SelectCommand = cmd;
            da1.Fill(dt1);
            rptTbl.DataSource = dt1;
            rptTbl.DataBind();
            DataAccessLayer.baglantiAyarla();
        }


        protected void rptTbl_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            hdn_faturaId.Value = e.CommandArgument.ToString();
            if (e.CommandName == "edit")
            {
                Response.Redirect("YeniFatura.aspx?Id=" + hdn_KullaniciId.Value + "&FId=" + e.CommandArgument);

            }
            else if (e.CommandName == "delete")
            {
                SqlCommand sil = new SqlCommand();
                sil.Connection = StokOtomasyon.DataAccessLayer.baglantiAyarla();
                sil.CommandType = CommandType.StoredProcedure;
                sil.CommandText = "sp_FaturaSil";
                sil.Parameters.AddWithValue("@faturaId", hdn_faturaId.Value);
                sil.ExecuteNonQuery();
                Page.Response.Redirect(Page.Request.Url.ToString(), true);
            }
        }
        protected void ddl_Sirketİsmi_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btn_Excel_Click(object sender, EventArgs e)
        {
            string GuidKey = Guid.NewGuid().ToString();

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

            if (rd_Alis.Checked)
            {
                fTip.Text = "Alis";
            }
            else if (rdSatis.Checked)
            {
                fTip.Text = "Satis";
            }

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = StokOtomasyon.DataAccessLayer.baglantiAyarla();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "sp_FaturaFilitrele";
            cmd.Parameters.AddWithValue("@Sirket", Mitra.IsDbNull(ddl_Sirketİsmi.SelectedItem.Text, "-x"));
            cmd.Parameters.AddWithValue("@Kdv", Mitra.IsDbNull(oran.Text, -999));
            cmd.Parameters.AddWithValue("@FaturaNumara", Mitra.IsDbNull(txt_FaturaNo.Text, "-x"));
            cmd.Parameters.AddWithValue("@UrunIsmi", Mitra.IsDbNull(txt_Urun.Text, "-x"));
            cmd.Parameters.AddWithValue("@BaslagicFiyati", Mitra.IsDbNull(txt_BaslangicFiyat.Text, -999));
            cmd.Parameters.AddWithValue("@BitisFiyati", Mitra.IsDbNull(txt_bitisFiyat.Text, -999));
            cmd.Parameters.AddWithValue("@FaturaTipi", Mitra.IsDbNull(fTip.Text, "-x"));
            cmd.Parameters.AddWithValue("@BitisTarihi", Mitra.TDate(this.txt_bitisTarih.Text, Mitra.TDateType.ddMMyyyy));
            cmd.Parameters.AddWithValue("@BaslangicTarihi", Mitra.TDate(this.txt_baslangicTarih.Text, Mitra.TDateType.ddMMyyyy));

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
    }
}