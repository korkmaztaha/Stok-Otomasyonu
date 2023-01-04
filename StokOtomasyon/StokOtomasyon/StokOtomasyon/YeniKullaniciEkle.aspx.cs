using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data;

namespace StokOtomasyon
{
    public partial class YeniKullaniciEkle : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Mitra.oturum();
            kullaniciListele();

        }
        public void kullaniciListele()
        {
            SqlCommand sqlCommand = new SqlCommand("Select * from Tbl_Kullanici", StokOtomasyon.DataAccessLayer.baglantiAyarla());
            SqlDataAdapter da1 = new SqlDataAdapter();
            DataTable dt1 = new DataTable();
            da1.SelectCommand = sqlCommand;
            da1.Fill(dt1);
            rpt_Kullanicilar.DataSource = dt1;
            rpt_Kullanicilar.DataBind();
            DataAccessLayer.baglantiAyarla();

        }
        protected void btn_Guncelle_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = StokOtomasyon.DataAccessLayer.baglantiAyarla();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "sp_KullaniciKaydet";
            cmd.Parameters.AddWithValue("@KullaniciId", hdn_KullaniciId.Value);
            cmd.Parameters.AddWithValue("@KullaniciAdi", txt_KullaniciAdi.Text);
            cmd.Parameters.AddWithValue("@KullaniciSifre", txt_Sifre.Text);
            cmd.Parameters.AddWithValue("@Ad", txt_Adi.Text);
            cmd.Parameters.AddWithValue("@Soyad", txt_Soyad.Text);
            cmd.Parameters.AddWithValue("@Telefon", txt_Telefon.Text);
            cmd.Parameters.AddWithValue("@Mail", txt_Mail.Text);
            cmd.Parameters.AddWithValue("@Adres", txt_Adres.Text);
            cmd.Parameters.AddWithValue("@SifreDurum",false);
            if (rdb_Yetki.Checked)
            {
                cmd.Parameters.AddWithValue("@AdminDurum",true);
            }
            else
            {
                cmd.Parameters.AddWithValue("@AdminDurum",false);
            }

            DataAccessLayer.baglantiAyarla();
            cmd.ExecuteNonQuery();
            kullaniciListele();
        }

        protected void rpt_Kullanicilar_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName=="delete")
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM Tbl_Kullanici WHERE KullaniciId=@KullaniciId",StokOtomasyon.DataAccessLayer.baglantiAyarla());
                cmd.Parameters.AddWithValue("KullaniciId",e.CommandArgument);
                cmd.ExecuteNonQuery();
                kullaniciListele();
            }
        }
    }
}