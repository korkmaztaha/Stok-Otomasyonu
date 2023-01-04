using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace StokOtomasyon
{
    public partial class Tedarikciler : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Mitra.oturum();
            listele();
            
        }
        
        public void listele()
        {
            SqlCommand sql = new SqlCommand();
            sql.Connection = StokOtomasyon.DataAccessLayer.baglantiAyarla();
            sql.CommandType = CommandType.StoredProcedure;
            sql.CommandText = "sp_TedarikciListele";
            SqlDataAdapter da1 = new SqlDataAdapter();
            DataTable dt1 = new DataTable();
            da1.SelectCommand = sql;
            da1.Fill(dt1);
            rptTbl.DataSource = dt1;
            rptTbl.DataBind();
            DataAccessLayer.baglantiAyarla();

        }

        protected void btn_Kayit_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = StokOtomasyon.DataAccessLayer.baglantiAyarla();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "sp_FirmaKaydet";
            cmd.Parameters.AddWithValue("@FirmaId", hdn_TedarikciId.Value);
            cmd.Parameters.AddWithValue("@FirmaAdi", txt_SirketAdi.Text);
            cmd.Parameters.AddWithValue("@FirmaVergiDaire", txt_Vergi.Text);
            cmd.Parameters.AddWithValue("@FirmaEposta", txt_Mail.Text);
            cmd.Parameters.AddWithValue("@FirmaTelefon", txt_Telefon.Text);
            cmd.Parameters.AddWithValue("@FirmaAdres", txt_Adres.Text);
            cmd.Parameters.AddWithValue("@FirmaTipi", "Tedarikci");
            DataAccessLayer.baglantiAyarla();
            cmd.ExecuteNonQuery();
            listele();
        }

        protected void rptTbl_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            hdn_TedarikciId.Value = e.CommandArgument.ToString();
            if (e.CommandName == "edit")
            {
                SqlCommand getir = new SqlCommand();
                getir.Connection = StokOtomasyon.DataAccessLayer.baglantiAyarla();
                getir.CommandType = CommandType.StoredProcedure;
                getir.CommandText = "sp_MusteriGetir";
                getir.Parameters.AddWithValue("@FirmaId", hdn_TedarikciId.Value);
                getir.ExecuteNonQuery();
                SqlDataReader dr = getir.ExecuteReader();

                if (dr.Read())
                {
                    txt_SirketAdi.Text = dr["FirmaAdi"].ToString();
                    txt_Vergi.Text = dr["FirmaVergiDaire"].ToString();
                    txt_Mail.Text = dr["FirmaEposta"].ToString();
                    txt_Telefon.Text = dr["FirmaTelefon"].ToString();
                    txt_Adres.Text = dr["FirmaAdres"].ToString();
                }
            }
            else if (e.CommandName == "delete")
            {
                SqlCommand sil = new SqlCommand();
                sil.Connection = StokOtomasyon.DataAccessLayer.baglantiAyarla();
                sil.CommandType = CommandType.StoredProcedure;
                sil.CommandText = "sp_MusteriSil";
                sil.Parameters.AddWithValue("@FirmaId", hdn_TedarikciId.Value);
                sil.ExecuteNonQuery();
                listele();
            }
        }
    }
}