using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace StokOtomasyon
{
    public partial class KullaniciBilgileri : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Mitra.oturum();
        

            if (!IsPostBack)
            {
               
                hdn_KullaniciId.Value = Request.QueryString["Id"].ToString();

                SqlCommand cmdDoldur = new SqlCommand("select * from Tbl_Kullanici where KullaniciId=@Id ", StokOtomasyon.DataAccessLayer.baglantiAyarla());
                cmdDoldur.Parameters.AddWithValue("@Id", hdn_KullaniciId.Value);
                cmdDoldur.ExecuteNonQuery();
                SqlDataReader dr = cmdDoldur.ExecuteReader();

                if (dr.Read())
                {
                    txt_KullaniciAdi.Text = dr["KullaniciAdi"].ToString();
                    //txt_Sifre.Text = dr["KullaniciSifre"].ToString();
                    txt_Sifre.Attributes.Add("Value", dr["KullaniciSifre"].ToString());
                    txt_Adi.Text = dr["Ad"].ToString();
                    txt_Soyad.Text = dr["Soyad"].ToString();
                    txt_Telefon.Text = dr["Telefon"].ToString();
                    txt_Mail.Text = dr["Mail"].ToString();
                    txt_Adres.Text = dr["Adres"].ToString();
                    switch (dr["AdminDurum"])
                    {
                        case true:
                            rdb_Yetki.Checked = true;
                            break;
                        case false:
                            rdb_Yetki.Checked = false;
                            break;
                    }
                }


            }
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
            cmd.Parameters.AddWithValue("@SifreDurum",true);
            if (rdb_Yetki.Checked)
            {
                cmd.Parameters.AddWithValue("@AdminDurum", true);
            }
            else
            {
                cmd.Parameters.AddWithValue("@AdminDurum", false);
            }
            DataAccessLayer.baglantiAyarla();
            cmd.ExecuteScalar();
        }

       
    }
}