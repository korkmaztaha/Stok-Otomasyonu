


using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;


namespace StokOtomasyon
{
    public partial class Master : System.Web.UI.MasterPage
    {
        int rowID;
        DateTime dateAndTime = DateTime.Now;

      
        protected void Page_Load(object sender, EventArgs e)
        {
            hdn_KullaniciId.Value = Request.QueryString["Id"].ToString();
            SqlCommand sql = new SqlCommand("select * from Tbl_Kullanici where @id=KullaniciId", StokOtomasyon.DataAccessLayer.baglantiAyarla());
            sql.Parameters.AddWithValue("id",hdn_KullaniciId.Value);
            SqlDataReader dr = sql.ExecuteReader();
            if (dr.Read())
            {
                switch (dr["AdminDurum"])
                {
                    case true:
                        this.KullaniciEkle.Visible = true;
                        break;
                    case false:
                        this.KullaniciEkle.Visible = false;
                        break;
                }
            }
          
            
            DataAccessLayer.baglantiAyarla();

        }
        protected void lnk_Alis_Click(object sender, EventArgs e)
        {
            
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = StokOtomasyon.DataAccessLayer.baglantiAyarla();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "sp_FaturaAnaKaydet";
            cmd.Parameters.AddWithValue("@FaturaId", hdn_FaturaId.Value);
            cmd.Parameters.AddWithValue("@SirketAdi", "");
            cmd.Parameters.AddWithValue("@FaturaNumara", "");
            cmd.Parameters.AddWithValue("@FaturaTarih", dateAndTime);
            cmd.Parameters.AddWithValue("@VergiDaire", "");
            cmd.Parameters.AddWithValue("@FaturaAdresi", "");
            cmd.Parameters.AddWithValue("@FaturaTipi", "Alis");
            cmd.Parameters.AddWithValue("@HareketId", "");
            cmd.Parameters.AddWithValue("@HareketTarihi", dateAndTime);
            cmd.Parameters.AddWithValue("@HareketFaturaNumara", "");
            cmd.Parameters.AddWithValue("@HareketTuru", "Alis");
            cmd.Parameters.AddWithValue("@HareketFirmaId", "");
            cmd.Parameters.AddWithValue("@HareketKullaniciId", "");
            cmd.Parameters.AddWithValue("@ToplamFiyat","");
            cmd.Parameters.AddWithValue("@FaturaDurum","");
            cmd.Parameters.AddWithValue("@IskontoluToplam", "");
            cmd.Parameters.AddWithValue("@HareketSatisAdet", 0);
            cmd.Parameters.AddWithValue("@HareketAlisAdet", 0);


            DataAccessLayer.baglantiAyarla();

            rowID = Convert.ToInt32(Mitra.IsDbNull(cmd.ExecuteScalar(), 0));
            this.hdn_FaturaId.Value = rowID.ToString();

            SqlCommand sqlCommand = new SqlCommand("select * from Tbl_Hareket", DataAccessLayer.baglantiAyarla());
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = sqlCommand;
            DataSet ds = new DataSet();
            da.Fill(ds);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                hdn_HareketId.Value = dr["HareketId"].ToString();
            }
            DataAccessLayer.baglantiAyarla();

            Response.Redirect("YeniFatura.aspx?Id=" + hdn_KullaniciId.Value + "&FId=" + hdn_FaturaId.Value + "&Htipi=Alis" + "&hId=" + hdn_HareketId.Value);
        }

        protected void lnk_Satis_Click(object sender, EventArgs e)
        {
           
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = StokOtomasyon.DataAccessLayer.baglantiAyarla();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "sp_FaturaAnaKaydet";
            cmd.Parameters.AddWithValue("@FaturaId", hdn_FaturaId.Value);
            cmd.Parameters.AddWithValue("@SirketAdi", "");
            cmd.Parameters.AddWithValue("@FaturaNumara", "");
            cmd.Parameters.AddWithValue("@FaturaTarih", dateAndTime);         
            cmd.Parameters.AddWithValue("@VergiDaire", "");
            cmd.Parameters.AddWithValue("@FaturaAdresi", "");
            cmd.Parameters.AddWithValue("@FaturaTipi", "Satis");
            cmd.Parameters.AddWithValue("@HareketId", "");
            cmd.Parameters.AddWithValue("@HareketTarihi", dateAndTime);
            cmd.Parameters.AddWithValue("@HareketFaturaNumara", "");
            cmd.Parameters.AddWithValue("@HareketTuru", "Satis");
            cmd.Parameters.AddWithValue("@HareketFirmaId", "");
            cmd.Parameters.AddWithValue("@HareketKullaniciId", "");
            cmd.Parameters.AddWithValue("@ToplamFiyat", "");
            cmd.Parameters.AddWithValue("@FaturaDurum", "");
            cmd.Parameters.AddWithValue("@IskontoluToplam","");
            cmd.Parameters.AddWithValue("@HareketSatisAdet",0);
            cmd.Parameters.AddWithValue("@HareketAlisAdet",0);
          
            DataAccessLayer.baglantiAyarla();

            rowID = Convert.ToInt32(Mitra.IsDbNull(cmd.ExecuteScalar(), 0));
            this.hdn_FaturaId.Value = rowID.ToString();

            SqlCommand sqlCommand = new SqlCommand("select * from Tbl_Hareket", DataAccessLayer.baglantiAyarla());
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = sqlCommand;
            DataSet ds = new DataSet();
            da.Fill(ds);
           
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                hdn_HareketId.Value = dr["HareketId"].ToString();
                
            }
            DataAccessLayer.baglantiAyarla();

            Response.Redirect("YeniFatura.aspx?Id=" + hdn_KullaniciId.Value + "&FId=" + hdn_FaturaId.Value + "&Htipi=Satis" + "&hId=" + hdn_HareketId.Value);
        }

        protected void lnk_TedarikciEkle_Click(object sender, EventArgs e)
        {
           
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = StokOtomasyon.DataAccessLayer.baglantiAyarla();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "sp_FirmaKaydet";
            cmd.Parameters.AddWithValue("@FirmaId", hdn_FirmaId.Value);
            cmd.Parameters.AddWithValue("@FirmaAdi", "");
            cmd.Parameters.AddWithValue("@FirmaVergiDaire", "");
            cmd.Parameters.AddWithValue("@FirmaEposta", "");
            cmd.Parameters.AddWithValue("@FirmaTelefon", "");
            cmd.Parameters.AddWithValue("@FirmaAdres", "");
            cmd.Parameters.AddWithValue("@FirmaTipi", "Tedarikci");
            DataAccessLayer.baglantiAyarla();
            rowID = Convert.ToInt32(Mitra.IsDbNull(cmd.ExecuteScalar(), 0));
            this.hdn_FirmaId.Value = rowID.ToString();
            Response.Redirect("FirmaKaydet.aspx?FirmaId=" + hdn_FirmaId.Value+ "&Id=" + hdn_KullaniciId.Value);

        }

        protected void lnk_MusteriEkle_Click(object sender, EventArgs e)
        {
          

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = StokOtomasyon.DataAccessLayer.baglantiAyarla();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "sp_FirmaKaydet";
            cmd.Parameters.AddWithValue("@FirmaId", hdn_FirmaId.Value);
            cmd.Parameters.AddWithValue("@FirmaAdi", "");
            cmd.Parameters.AddWithValue("@FirmaVergiDaire", "");
            cmd.Parameters.AddWithValue("@FirmaEposta", "");
            cmd.Parameters.AddWithValue("@FirmaTelefon", "");
            cmd.Parameters.AddWithValue("@FirmaAdres", "");
            cmd.Parameters.AddWithValue("@FirmaTipi", "Musteri");
            DataAccessLayer.baglantiAyarla();

            rowID = Convert.ToInt32(Mitra.IsDbNull(cmd.ExecuteScalar(), 0));
            this.hdn_FirmaId.Value = rowID.ToString();

            Response.Redirect("FirmaKaydet.aspx?FirmaId=" + hdn_FirmaId.Value+"&Id=" + hdn_KullaniciId.Value);
        }

        protected void lnk_SatisListele_Click(object sender, EventArgs e)
        {
            
            Response.Redirect("SatisFaturalari.aspx?Id=" + hdn_KullaniciId.Value);
        }

        protected void lnk_AlisListele_Click(object sender, EventArgs e)
        {
           
            Response.Redirect("AlisFaturalari.aspx?Id=" + hdn_KullaniciId.Value);
        }

        protected void lnk_TedarikciListesi_Click(object sender, EventArgs e)
        {
            
            Response.Redirect("Tedarikciler.aspx?Id=" + hdn_KullaniciId.Value);
        }

        protected void lnk_MusteriListesi_Click(object sender, EventArgs e)
        {
            Response.Redirect("Musteriler.aspx?Id=" + hdn_KullaniciId.Value);
        }

        protected void lnk_Urunler_Click(object sender, EventArgs e)
        {
           
            Response.Redirect("Urunler.aspx?Id=" + hdn_KullaniciId.Value);
        }

        protected void lnk_Raporlar_Click(object sender, EventArgs e)
        {
           
            Response.Redirect("Raporlar.aspx?Id=" + hdn_KullaniciId.Value);
        }

        protected void lnk_Main_Click(object sender, EventArgs e)
        {
            
            Response.Redirect("main.aspx?Id=" + hdn_KullaniciId.Value);
        }

        protected void lnk_KullaniciBilgileri_Click1(object sender, EventArgs e)
        {
           
            Response.Redirect("KullaniciBilgileri.aspx?Id=" + hdn_KullaniciId.Value);
        }

        protected void lnkCikisYap_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("default.aspx");
        }

        protected void lnk_YeniKullanici_Click(object sender, EventArgs e)
        {
            hdn_KullaniciId.Value = Request.QueryString["Id"].ToString();
            Response.Redirect("YeniKullaniciEkle.aspx?Id=" + hdn_KullaniciId.Value);
        }
    }
}