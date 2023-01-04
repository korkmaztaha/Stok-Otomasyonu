using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace StokOtomasyon
{
    public partial class _default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }
        protected void btn_Giris_Click(object sender, EventArgs e)
        {
            SqlCommand cmdHidden = new SqlCommand("Select KullaniciId from Tbl_Kullanici where KullaniciAdi=@KullaniciAdi ", StokOtomasyon.DataAccessLayer.baglantiAyarla());
            cmdHidden.Parameters.AddWithValue("@KullaniciAdi", txt_KullniciAdi.Text);
            cmdHidden.ExecuteNonQuery();
            SqlDataReader dr1 = cmdHidden.ExecuteReader();
            if (dr1.Read())
            {
                hdn_KullaniciId.Value = dr1["KullaniciId"].ToString();
            }

            SqlCommand sql = new SqlCommand("select * from Tbl_Kullanici where KullaniciAdi=@KullaniciAdi and KullaniciSifre=@KullaniciSifre", StokOtomasyon.DataAccessLayer.baglantiAyarla());
            sql.Parameters.AddWithValue("@KullaniciAdi", txt_KullniciAdi.Text);
            sql.Parameters.AddWithValue("@KullaniciSifre", txt_Sifre.Text);
            SqlDataReader dr = sql.ExecuteReader();
           
            if (dr.Read())
            {
                switch (dr["SifreDurum"])
                {
                    case true:
                        Session.Add("kullanici", txt_KullniciAdi.Text);
                        Response.Redirect("main.aspx?Id=" + hdn_KullaniciId.Value);
                        break;
                    case false:
                        Session.Add("kullanici", txt_KullniciAdi.Text);
                        Response.Redirect("KullaniciBilgileri.aspx?Id=" + hdn_KullaniciId.Value);
                        break;
                    default:
                        
                        break;
                }
            }
            else
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Uyarı", "<script>alert('Kullanıcı Adı veya Şifre hatalı');</script>");
            }
            
            DataAccessLayer.baglantiAyarla();
        }
        protected void Sifre_CheckedChanged(object sender, EventArgs e)
        {


        }
    }
}