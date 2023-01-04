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
    public partial class FirmaKaydet : System.Web.UI.Page
    {
      
        int rowID;
        protected void Page_Load(object sender, EventArgs e)
        {
            Mitra.oturum();

            if (Mitra.IsNumeric(Request.QueryString["FirmaId"]))
            {
                this.hdn_FirmaId.Value = Request.QueryString["FirmaId"];
            }
        }

        protected void btn_Kayit_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = StokOtomasyon.DataAccessLayer.baglantiAyarla();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "sp_FirmaKaydet";
            cmd.Parameters.AddWithValue("@FirmaId",hdn_FirmaId.Value);
            cmd.Parameters.AddWithValue("@FirmaAdi",txt_SirketAdi.Text);
            cmd.Parameters.AddWithValue("@FirmaVergiDaire",txt_Vergi.Text);
            cmd.Parameters.AddWithValue("@FirmaEposta",txt_Mail.Text);
            cmd.Parameters.AddWithValue("@FirmaTelefon",txt_Telefon.Text);
            cmd.Parameters.AddWithValue("@FirmaAdres",txt_Adres.Text);
            DataAccessLayer.baglantiAyarla();
   
            rowID = Convert.ToInt32(Mitra.IsDbNull(cmd.ExecuteScalar(), 0));
            this.hdn_FirmaId.Value = rowID.ToString();
        }
    }
}