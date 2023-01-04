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
    public partial class AlisFaturalari1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            Mitra.oturum();

            hdn_KullaniciId.Value = Request.QueryString["Id"].ToString();
           
            SqlCommand liste = new SqlCommand();
            liste.Connection = StokOtomasyon.DataAccessLayer.baglantiAyarla();
            liste.CommandType = CommandType.StoredProcedure;
            liste.CommandText = "sp_AlisfaturalariListele";
            SqlDataAdapter da1 = new SqlDataAdapter();
            DataTable dt1 = new DataTable();
            da1.SelectCommand = liste;
            da1.Fill(dt1);
            rptTbl.DataSource = dt1;
            rptTbl.DataBind();
        }  


        protected void rptTbl_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            hdn_faturaId.Value = e.CommandArgument.ToString();
            if (e.CommandName == "edit")
            {
                Response.Redirect("YeniFatura.aspx?Id=" + hdn_KullaniciId.Value + "&FId=" + hdn_faturaId.Value);

            }
        }

    }

}