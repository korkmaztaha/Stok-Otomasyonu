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
    public partial class Urunler : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            Mitra.oturum();
            urunListele();
           
        }
        public void urunListele()
        {
            SqlCommand sql = new SqlCommand("Select * from Tbl_Urunler", StokOtomasyon.DataAccessLayer.baglantiAyarla());
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
            cmd.Connection = StokOtomasyon.DataAccessLayer.baglantiAyarla();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText="sp_UrunKaydet";
            cmd.Parameters.AddWithValue("@UrunId",hdn_UrunId.Value);
            cmd.Parameters.AddWithValue("@UrunAdi",txt_Urun.Text);
            cmd.Parameters.AddWithValue("@UrunGrubu",txt_Grubu.Text);
            cmd.Parameters.AddWithValue("@UrunMarka",txt_Marka.Text);
            cmd.Parameters.AddWithValue("@UrunKdv", oran.Text);
            cmd.Parameters.AddWithValue("@UrunSatis",txt_UrunFiyati.Text);
            cmd.Parameters.AddWithValue("@UrunStoktakiMiktar",hdnAdet.Value);
            DataAccessLayer.baglantiAyarla();
            cmd.ExecuteNonQuery();
            urunListele();

        }

        protected void rptTbl_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName=="edit")
            {
                SqlCommand getir = new SqlCommand();
                getir.Connection = StokOtomasyon.DataAccessLayer.baglantiAyarla();
                getir.CommandType = CommandType.StoredProcedure;
                getir.CommandText = "sp_UrunGetir";
                getir.Parameters.AddWithValue("@UrunId", e.CommandArgument);
                getir.ExecuteNonQuery();
                SqlDataReader dr = getir.ExecuteReader();

                if (dr.Read())
                {
                    hdn_UrunId.Value = dr["UrunId"].ToString();
                    txt_Urun.Text = dr["UrunAdi"].ToString();
                    txt_Marka.Text = dr["UrunMarka"].ToString();
                    txt_Grubu.Text = dr["UrunGrubu"].ToString();
                    oran.Text = dr["UrunKdv"].ToString();
                    txt_UrunFiyati.Text = dr["UrunSatis"].ToString();
                    hdnAdet.Value = dr["UrunStoktakiMiktar"].ToString();
                }

                if (oran.Text == "8")
                {
                    kdv_8.Checked = true;
                    kdv_18.Checked = false;
                    kdv_1.Checked = false;
                }
                else if (oran.Text == "18")
                {
                    kdv_18.Checked = true;
                    kdv_8.Checked = false;
                    kdv_1.Checked = false;
                }
                else if (oran.Text == "1")
                {
                    kdv_1.Checked = true;
                    kdv_18.Checked = false;
                    kdv_8.Checked = false;
                }

               
            }
            else if (e.CommandName=="delete")
            {
                SqlCommand sil = new SqlCommand();
                sil.Connection = StokOtomasyon.DataAccessLayer.baglantiAyarla();
                sil.CommandType = CommandType.StoredProcedure;
                sil.CommandText = "sp_UrunSil";
                sil.Parameters.AddWithValue("@UrunId", e.CommandArgument);
                sil.ExecuteNonQuery();
                urunListele();
            }


        }

        protected void txt_UrunFiyati_TextChanged(object sender, EventArgs e)
        {
            
        }
    }
}