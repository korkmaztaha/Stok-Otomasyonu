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
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System.Diagnostics;
using System.Web.Services;
using Newtonsoft.Json;

namespace StokOtomasyon
{
    public partial class YeniFatura : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Mitra.oturum();

            hdn_FaturaId.Value = Request.QueryString["FId"].ToString();
            
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = StokOtomasyon.DataAccessLayer.baglantiAyarla();
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.CommandText = "sp_FaturaDurumu";
            sqlCommand.Parameters.AddWithValue("@faturaId", hdn_FaturaId.Value);
            sqlCommand.Parameters.AddWithValue("@faturaDurumu", 1);
            SqlDataReader fDurumu = sqlCommand.ExecuteReader();
            if (fDurumu.Read())
            {
                gizle();

            }

            SqlDataReader dr, drA;
            if (!IsPostBack)
            {

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = StokOtomasyon.DataAccessLayer.baglantiAyarla();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_FirmaGetir";
                dr = cmd.ExecuteReader();
                ddl_Sirketİsmi.DataSource = dr;
                ddl_Sirketİsmi.DataBind();
                dr.Close();

                SqlCommand sqlA = new SqlCommand();
                sqlA.Connection = StokOtomasyon.DataAccessLayer.baglantiAyarla();
                sqlA.CommandType = CommandType.StoredProcedure;
                sqlA.CommandText = "sp_SatisFatura";
                drA = sqlA.ExecuteReader();
                ddlUrun.DataSource = drA;
                ddlUrun.DataBind();
                drA.Close();
            }


            if (!IsPostBack && Request.QueryString["Htipi"] == "Alis")
            {
                btn_Yazdir.Visible = false;
            }
            #region bozukluk Olursa Aç
            //if (!IsPostBack && Request.QueryString["Htipi"] == "Alis")
            //{
            //    SqlCommand sqlA = new SqlCommand();
            //    sqlA.Connection = StokOtomasyon.DataAccessLayer.baglantiAyarla();
            //    sqlA.CommandType = CommandType.StoredProcedure;
            //    sqlA.CommandText = "sp_Alisfatura";
            //    drA = sqlA.ExecuteReader();
            //    ddlUrun.DataSource = drA;
            //    ddlUrun.DataBind();
            //    drA.Close();
            //    btn_Yazdir.Visible = false;
            //}
            //if (!IsPostBack && Request.QueryString["Htipi"] == "Satis")
            //{
            //    SqlCommand sqlA = new SqlCommand();
            //    sqlA.Connection = StokOtomasyon.DataAccessLayer.baglantiAyarla();
            //    sqlA.CommandType = CommandType.StoredProcedure;
            //    sqlA.CommandText = "sp_SatisFatura";
            //    drA = sqlA.ExecuteReader();
            //    ddlUrun.DataSource = drA;
            //    ddlUrun.DataBind();
            //    drA.Close();
            //}
            #endregion

            if (!IsPostBack && !Mitra.IsDbNull(Request.QueryString["FId"]))
            {


                hdn_FaturaId.Value = Request.QueryString["FId"].ToString();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = StokOtomasyon.DataAccessLayer.baglantiAyarla();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_FaturaListele";
                cmd.Parameters.AddWithValue("@faturaId", hdn_FaturaId.Value);
                SqlDataReader drr = cmd.ExecuteReader();

                if (drr.Read())
                {
                    
                    ddl_Sirketİsmi.SelectedItem.Text = drr["SirketAdi"].ToString();
                    txt_Vergi.Text = drr["VergiDaire"].ToString();
                    txt_Tarih.Text = SQL2CSDate2String(drr["FaturaTarih"], "dd/MM/yyyy");
                    txt_Adres.Text = drr["FaturaAdresi"].ToString();
                    txt_Fatura.Text = drr["FaturaNumara"].ToString();
                    urunListesi();
                }
            }
        }

        private void urunListesi()
        {
            SqlCommand sqlCommand = new SqlCommand("Select * from Tbl_FaturaDetay  where FaturaId=@id", StokOtomasyon.DataAccessLayer.baglantiAyarla());
            sqlCommand.Parameters.AddWithValue("id", hdn_FaturaId.Value);
            SqlDataAdapter da1 = new SqlDataAdapter();
            DataTable dt1 = new DataTable();
            da1.SelectCommand = sqlCommand;
            da1.Fill(dt1);
            rptTbl.DataSource = dt1;
            rptTbl.DataBind();
            FaturaToplami();
        }

        protected void ddl_Sirketİsmi_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlCommand getir = new SqlCommand();
            getir.Connection = StokOtomasyon.DataAccessLayer.baglantiAyarla();
            getir.CommandType = CommandType.StoredProcedure;
            getir.CommandText = "sp_MusteriGetir";
            getir.Parameters.AddWithValue("@FirmaId", ddl_Sirketİsmi.SelectedValue);
            hdnFirmaId.Value = ddl_Sirketİsmi.SelectedValue;
            getir.ExecuteNonQuery();
            SqlDataReader dr1 = getir.ExecuteReader();

            if (dr1.Read())
            {
                txt_Vergi.Text = dr1["FirmaVergiDaire"].ToString();
                txt_Adres.Text = dr1["FirmaAdres"].ToString();

            }
        }

        protected void btn_Kayit_Click(object sender, EventArgs e)
        {
            hdn_KullaniciId.Value = Request.QueryString["Id"].ToString();
            hdn_FaturaTipi.Value = Request.QueryString["Htipi"].ToString();
            hdn_FaturaId.Value = Request.QueryString["FId"].ToString();
            hdn_HareketId.Value = Request.QueryString["hId"].ToString();

            kaydet();

        }
        public void kaydet()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = StokOtomasyon.DataAccessLayer.baglantiAyarla();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "sp_FaturaAnaKaydet";
            cmd.Parameters.AddWithValue("@FaturaId", hdn_FaturaId.Value);
            cmd.Parameters.AddWithValue("@SirketAdi", ddl_Sirketİsmi.SelectedItem.Text);
            cmd.Parameters.AddWithValue("@FaturaNumara", txt_Fatura.Text);
            if (Mitra.IsDbNull(Mitra.TDate(this.txt_Tarih.Text, Mitra.TDateType.ddMMyyyy)))
            {
                cmd.Parameters.AddWithValue("@FaturaTarih", DBNull.Value);
                cmd.Parameters.AddWithValue("@HareketTarihi", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FaturaTarih", Mitra.TDate(this.txt_Tarih.Text, Mitra.TDateType.ddMMyyyy));
                cmd.Parameters.AddWithValue("@HareketTarihi", Mitra.TDate(this.txt_Tarih.Text, Mitra.TDateType.ddMMyyyy));
            }
            cmd.Parameters.AddWithValue("@VergiDaire", txt_Vergi.Text);
            cmd.Parameters.AddWithValue("@FaturaAdresi", txt_Adres.Text);
            cmd.Parameters.AddWithValue("@FaturaTipi", hdn_FaturaTipi.Value);
            cmd.Parameters.AddWithValue("@HareketId", hdn_HareketId.Value);
            cmd.Parameters.AddWithValue("@HareketFaturaNumara", txt_Fatura.Text);
            cmd.Parameters.AddWithValue("@HareketTuru", hdn_FaturaTipi.Value);
            cmd.Parameters.AddWithValue("@HareketFirmaId", ddl_Sirketİsmi.SelectedValue);
            cmd.Parameters.AddWithValue("@HareketKullaniciId", hdn_KullaniciId.Value);
            cmd.Parameters.AddWithValue("@ToplamFiyat", Mitra.tToDouble(txt_Toplam.Text));
            cmd.Parameters.AddWithValue("@FaturaDurum", "");
            cmd.Parameters.AddWithValue("IskontoluToplam", "");
            cmd.Parameters.AddWithValue("@HareketSatisAdet", 0);
            cmd.Parameters.AddWithValue("@HareketAlisAdet", 0);
            DataAccessLayer.baglantiAyarla();
            cmd.ExecuteNonQuery();
        }

        public void faturaToplaminiGuncelle()
        {
            SqlCommand fiyat = new SqlCommand();
            fiyat.Connection = StokOtomasyon.DataAccessLayer.baglantiAyarla();
            fiyat.CommandType = CommandType.StoredProcedure;
            fiyat.CommandText = "sp_FaturaAnaToplamDetayıGuncelle";
            fiyat.Parameters.AddWithValue("@faturaId", hdn_FaturaId.Value);
            fiyat.Parameters.AddWithValue("@tutar", Mitra.tToDouble(lbl_toplam.Text));

            DataAccessLayer.baglantiAyarla();
            fiyat.ExecuteNonQuery();
        }
        public void FaturaToplami()
        {
            SqlCommand faturatoplam = new SqlCommand();
            faturatoplam.Connection = StokOtomasyon.DataAccessLayer.baglantiAyarla();
            faturatoplam.CommandType = CommandType.StoredProcedure;
            faturatoplam.CommandText = "sp_FaturaToplami";
            faturatoplam.Parameters.AddWithValue("@faturaId", hdn_FaturaId.Value);
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = faturatoplam;
            DataSet ds = new DataSet();
            da.Fill(ds);

            lbl_Urun.Text = ds.Tables[0].Rows[0]["UrunToplam"].ToString();
            lbl_toplam.Text = ds.Tables[2].Rows[0]["GenelToplam"].ToString();
            lbl_AraToplam.Text = ds.Tables[3].Rows[0]["IskontoluToplam"].ToString();
            lbl_Iskonto.Text = ds.Tables[4].Rows[0]["IskontoMiktari"].ToString();

            foreach (DataRow dr in ds.Tables[1].Rows)
            {
                switch (dr["KDV"].ToString())
                {
                    case "1":
                        lbl_f1.Text = dr["KdvToplam"].ToString();
                        break;
                    case "8":
                        lbl_f8.Text = dr["KdvToplam"].ToString();
                        break;
                    case "18":
                        lbl_f18.Text = dr["KdvToplam"].ToString();
                        break;
                    default:
                        break;
                }
            }

        }
        int rowID, mAdet;
        public void sp_UrunTablosuStokGuncelle()
        {
            SqlCommand tblUrunStokGun = new SqlCommand();
            tblUrunStokGun.Connection = StokOtomasyon.DataAccessLayer.baglantiAyarla();
            tblUrunStokGun.CommandType = CommandType.StoredProcedure;
            tblUrunStokGun.CommandText = "sp_UrunTablosuStokGuncelle";
            tblUrunStokGun.Parameters.AddWithValue("@uid", ddlUrun.SelectedValue);
            tblUrunStokGun.Parameters.AddWithValue("@miktar", mAdet.ToString());
            DataAccessLayer.baglantiAyarla();
            tblUrunStokGun.ExecuteNonQuery();
        }

        double BirimFiyat = 0, adedi = 0, kdvliFiyat, kdvTutarı, KdvsizUrunToplami, iskontluFiyat, iskontoMiktari;

        protected void btn_UrunEkle_Click(object sender, EventArgs e)
        {

            BirimFiyat = Mitra.tToDouble(txt_BirimFiyatı.Text);
            adedi = Convert.ToDouble(txt_Adet.Text);
            KdvsizUrunToplami = BirimFiyat * adedi;

            if (txt_Iskonto.Text != "")
            {
                iskontluFiyat = KdvsizUrunToplami * (1 - (Convert.ToDouble(txt_Iskonto.Text)) / 100);
                iskontoMiktari = KdvsizUrunToplami - iskontluFiyat;

                if (kdv_8.Checked)
                {
                    kdvliFiyat = iskontluFiyat * 1.08;
                    kdvTutarı = kdvliFiyat - iskontluFiyat;
                    oran.Text = "8";

                }
                else if (kdv_18.Checked)
                {
                    kdvliFiyat = iskontluFiyat * 1.18;
                    kdvTutarı = kdvliFiyat - iskontluFiyat;
                    oran.Text = "18";
                }
                else if (kdv_1.Checked)
                {
                    kdvliFiyat = iskontluFiyat * 1.01;
                    kdvTutarı = kdvliFiyat - iskontluFiyat;
                    oran.Text = "1";
                }

            }
            else
            {
                if (kdv_8.Checked)
                {
                    kdvliFiyat = KdvsizUrunToplami * 1.08;
                    kdvTutarı = kdvliFiyat - KdvsizUrunToplami;
                    oran.Text = "8";
                }
                else if (kdv_18.Checked)
                {
                    kdvliFiyat = KdvsizUrunToplami * 1.18;
                    kdvTutarı = kdvliFiyat - KdvsizUrunToplami;
                    oran.Text = "18";
                }
                else if (kdv_1.Checked)
                {
                    kdvliFiyat = KdvsizUrunToplami * 1.01;
                    kdvTutarı = kdvliFiyat - KdvsizUrunToplami;
                    oran.Text = "1";
                }
            }



            hdn_KullaniciId.Value = Request.QueryString["Id"].ToString();
            hdn_FaturaTipi.Value = Request.QueryString["Htipi"].ToString();
            hdn_FaturaId.Value = Request.QueryString["FId"].ToString();
            hdn_HareketId.Value = Request.QueryString["hId"].ToString();

            SqlCommand ekle = new SqlCommand();
            ekle.Connection = StokOtomasyon.DataAccessLayer.baglantiAyarla();
            ekle.CommandType = CommandType.StoredProcedure;
            ekle.CommandText = "sp_UrunStok";
            ekle.Parameters.AddWithValue("@uid", ddlUrun.SelectedValue);
            SqlDataReader dr1 = ekle.ExecuteReader();
            if (dr1.Read())
            {
                hdn_dbAdet.Value = dr1["adet"].ToString();
            }
            if (hdn_dbAdet.Value == "")
            {
                hdn_dbAdet.Value = 0.ToString();
            }

            if (Request.QueryString["Htipi"] == "Alis")
            {
                int dbAdet = Convert.ToInt32(hdn_dbAdet.Value);
                int hAdet = Convert.ToInt32(txt_Adet.Text);
                mAdet = hAdet + dbAdet;

                urunSatis();
                urunListesi();
                FaturaToplami();
                faturaToplaminiGuncelle();
                sp_UrunTablosuStokGuncelle();

                Response.Redirect("YeniFatura.aspx?Id=" + hdn_KullaniciId.Value + "&FId=" + hdn_FaturaId.Value + "&Htipi=Alis" + "&hId=" + hdn_HareketId.Value);

            }
            else if (Request.QueryString["Htipi"] == "Satis")
            {
                int dbAdet = Convert.ToInt32(hdn_dbAdet.Value);
                int hAdet = Convert.ToInt32(txt_Adet.Text);
                mAdet = dbAdet - hAdet;

                if (mAdet < 0)
                {
                    this.ErrorMessage.Visible = true;
                    this.lblErrorMessage.Text = "İşlemi yapmak için yeterli stok yok";

                    this.lbltext.Text = "Stokta Kalan Miktar";
                    this.lblMiktar.Text = dbAdet.ToString();
                }
                else
                {
                    urunSatis();
                    urunListesi();
                    FaturaToplami();
                    faturaToplaminiGuncelle();
                    sp_UrunTablosuStokGuncelle();
                    #region UrunFiyatGuncelleme
                    SqlCommand urunFiyat = new SqlCommand();
                    urunFiyat.Connection = StokOtomasyon.DataAccessLayer.baglantiAyarla();
                    urunFiyat.CommandType = CommandType.StoredProcedure;
                    urunFiyat.CommandText = "sp_FiyatGuncell";
                    urunFiyat.Parameters.AddWithValue("@uid", ddlUrun.SelectedValue);
                    urunFiyat.Parameters.AddWithValue("@fiyat", Mitra.tToDouble(txt_BirimFiyatı.Text));
                    DataAccessLayer.baglantiAyarla();
                    urunFiyat.ExecuteNonQuery();
                    #endregion

                    Response.Redirect("YeniFatura.aspx?Id=" + hdn_KullaniciId.Value + "&FId=" + hdn_FaturaId.Value + "&Htipi=Satis" + "&hId=" + hdn_HareketId.Value);
                }
            }

        }

        protected void btnSil_Click(object sender, EventArgs e)
        {
            SqlCommand sil = new SqlCommand();
            sil.Connection = StokOtomasyon.DataAccessLayer.baglantiAyarla();
            sil.CommandType = CommandType.StoredProcedure;
            sil.CommandText = "sp_FaturaSil";
            sil.Parameters.AddWithValue("@faturaId", hdn_FaturaId.Value);
            sil.ExecuteNonQuery();
        }

        public void urunSatis()
        {
            #region UrunEkleme

            int eksi = Convert.ToInt32(txt_Adet.Text);
            int eeksi = eksi * 1;

            SqlCommand urun = new SqlCommand();
            urun.Connection = StokOtomasyon.DataAccessLayer.baglantiAyarla();
            urun.CommandType = CommandType.StoredProcedure;
            urun.CommandText = "sp_FaturaDetay";
            urun.Parameters.AddWithValue("@SatisId", "");
            urun.Parameters.AddWithValue("@FaturaId", hdn_FaturaId.Value);
            urun.Parameters.AddWithValue("@Urun", ddlUrun.SelectedItem.Text);
            urun.Parameters.AddWithValue("@BirimFiyatı", Mitra.tToDouble(txt_BirimFiyatı.Text));
            urun.Parameters.AddWithValue("@Adet", txt_Adet.Text);
            urun.Parameters.AddWithValue("@KDV", oran.Text);
            urun.Parameters.AddWithValue("@KdvTutarı", Convert.ToDouble(kdvTutarı));
            urun.Parameters.AddWithValue("@KdvVeUrunFiyatı", Convert.ToDouble(kdvliFiyat));
            urun.Parameters.AddWithValue("@HareketId", hdn_HareketId.Value);
            urun.Parameters.AddWithValue("@HareketMevcutStok", mAdet.ToString());
            urun.Parameters.AddWithValue("@HareketUrunId", ddlUrun.SelectedValue);
            urun.Parameters.AddWithValue("@UrunToplamı", Mitra.tToDouble(Convert.ToString(KdvsizUrunToplami)));
            if (txt_Iskonto.Text != "")
            {
                urun.Parameters.AddWithValue("@IskontoluFiyat", Mitra.tToDouble(Convert.ToString(iskontluFiyat)));
                urun.Parameters.AddWithValue("@IskontoOrani", txt_Iskonto.Text);
                urun.Parameters.AddWithValue("@IskontoMiktari", Mitra.tToDouble(Convert.ToString(iskontoMiktari)));
            }
            else
            {
                urun.Parameters.AddWithValue("@IskontoluFiyat", Mitra.tToDouble(Convert.ToString(KdvsizUrunToplami)));
                urun.Parameters.AddWithValue("@IskontoOrani", string.Empty);
                urun.Parameters.AddWithValue("@IskontoMiktari", string.Empty);
            }
            if (Request.QueryString["Htipi"] == "Alis")
            {
                urun.Parameters.AddWithValue("@HareketAlisFiyatı", Mitra.tToDouble(txt_BirimFiyatı.Text));
                urun.Parameters.AddWithValue("@HareketAlisAdet", txt_Adet.Text);
                urun.Parameters.AddWithValue("@HareketSatisFiyatı", "");
                urun.Parameters.AddWithValue("@HareketSatisAdet", 0);

            }
            else if (Request.QueryString["Htipi"] == "Satis")
            {
                urun.Parameters.AddWithValue("@HareketSatisFiyatı", Mitra.tToDouble(txt_BirimFiyatı.Text));
                urun.Parameters.AddWithValue("@HareketSatisAdet", txt_Adet.Text);
                urun.Parameters.AddWithValue("@HareketAlisFiyatı", "");
                urun.Parameters.AddWithValue("@HareketAlisAdet", 0);
            }
            urun.Parameters.AddWithValue("@HareketFaturaId", hdn_FaturaId.Value);
            if (Mitra.IsDbNull(Mitra.TDate(this.txt_Tarih.Text, Mitra.TDateType.ddMMyyyy)))
            {
                urun.Parameters.AddWithValue("@HareketTarihi", DBNull.Value);
            }
            else
            {
                urun.Parameters.AddWithValue("@HareketTarihi", Mitra.TDate(this.txt_Tarih.Text, Mitra.TDateType.ddMMyyyy));
            }
            urun.Parameters.AddWithValue("@HareketFaturaNumara", txt_Fatura.Text);
            urun.Parameters.AddWithValue("@HareketTuru", hdn_FaturaTipi.Value);
            urun.Parameters.AddWithValue("@HareketFirmaId", ddl_Sirketİsmi.SelectedValue);
            urun.Parameters.AddWithValue("@HareketKullaniciId", hdn_KullaniciId.Value);
            DataAccessLayer.baglantiAyarla();
            rowID = Convert.ToInt32(Mitra.IsDbNull(urun.ExecuteScalar(), 0));
            this.hdn_HareketId.Value = rowID.ToString();
            #endregion
        }



        public static string SQL2CSDate2String(object sqldate, string DateFormat)
        {
            if (Mitra.IsDbNull(sqldate))
            {
                return "";
            }
            else
            {
                DateTime csdate;
                DateTime.TryParse(sqldate.ToString(), out csdate);
                return csdate.ToString(DateFormat);
            }
        }

        protected void rptTbl_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            hdn_HareketId.Value = Request.QueryString["hId"].ToString();
            hdn_SatisId.Value = e.CommandArgument.ToString();
            if (e.CommandName == "edit")
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = StokOtomasyon.DataAccessLayer.baglantiAyarla();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_FaturaUrunuGetir";
                cmd.Parameters.AddWithValue("@id", hdn_SatisId.Value);
                cmd.ExecuteNonQuery();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    ddlUrun.SelectedItem.Text = dr["Urun"].ToString();
                    txt_BirimFiyatı.Text = dr["BirimFiyatı"].ToString();
                    txt_Adet.Text = dr["Adet"].ToString();
                    oran.Text = dr["KDV"].ToString();
                    txt_Toplam.Text = dr["UrunToplamı"].ToString();
                }
                if (oran.Text == "8")
                {
                    kdv_8.Checked = true;
                }
                else if (oran.Text == "18")
                {
                    kdv_18.Checked = true;
                }
                else if (oran.Text == "1")
                {
                    kdv_1.Checked = true;
                }

            }
            else if (e.CommandName == "delete")
            {
                SqlCommand cmdSil = new SqlCommand();
                cmdSil.Connection = StokOtomasyon.DataAccessLayer.baglantiAyarla();
                cmdSil.CommandType = CommandType.StoredProcedure;
                cmdSil.CommandText = "sp_FaturaUrunSil";
                cmdSil.Parameters.AddWithValue("@satisId", hdn_SatisId.Value);
                cmdSil.Parameters.AddWithValue("@hareketId", hdn_HareketId.Value);
                cmdSil.ExecuteNonQuery();
                faturaToplaminiGuncelle();
                urunListesi();
                //Page.Response.Redirect(Page.Request.Url.ToString(), true);
            }
        }


        protected void btn_Yazdir_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("update  Tbl_FaturaAna set FaturaDurum=@durum where FaturaId=@id", StokOtomasyon.DataAccessLayer.baglantiAyarla());
            cmd.Parameters.AddWithValue("@durum", 1);
            cmd.Parameters.AddWithValue("id", hdn_FaturaId.Value);
            cmd.ExecuteNonQuery();
            gizle();
            kaydet();
            PdfOper.pdfyaz(hdn_FaturaId.Value);

            #region EskiFatura
            //lbl_f1.Text = "66";
            //lbl_f8.Text = "66";
            //int yU = -190, d = -162, mY = 40, bY = 100, kY = 175, tY = 250;
            //lbl_f8.Text = "66";

            //string GuidKey = Guid.NewGuid().ToString();

            //PdfDocument document = new PdfDocument();
            //document.Info.Title = "Fatura";
            //PdfPage page = document.AddPage();
            //XGraphics gfx = XGraphics.FromPdfPage(page);
            //XFont font = new XFont("Arial", 12);
            //gfx.DrawString(ddl_Sirketİsmi.SelectedItem.Text, font, XBrushes.Black, new XRect(-235, -285, page.Width, page.Height), XStringFormats.Center);
            //gfx.DrawString(txt_Adres.Text, font, XBrushes.Black, new XRect(-235, -270, page.Width, page.Height), XStringFormats.Center);
            //gfx.DrawString(txt_Vergi.Text, font, XBrushes.Black, new XRect(-95, -180, page.Width, page.Height), XStringFormats.Center);
            //gfx.DrawString(txt_Tarih.Text, font, XBrushes.Black, new XRect(230, -246, page.Width, page.Height), XStringFormats.Center);
            //gfx.DrawString(DateTime.Now.ToShortTimeString(), font, XBrushes.Black, new XRect(230, -225, page.Width, page.Height), XStringFormats.Center);
            //#region urunler
            //SqlCommand cmd = new SqlCommand("select Urun,Adet,BirimFiyatı,KDV,UrunToplamı from Tbl_FaturaDetay where FaturaId=@id", StokOtomasyon.DataAccessLayer.baglantiAyarla());
            //cmd.Parameters.AddWithValue("@id", hdn_FaturaId.Value);
            //SqlDataAdapter da = new SqlDataAdapter();
            //da.SelectCommand = cmd;
            //DataSet ds = new DataSet();
            //da.Fill(ds);
            //foreach (DataRow dr in ds.Tables[0].Rows)
            //{
            //    d = d + 15;
            //    hdn_Urun.Value = dr["Urun"].ToString();
            //    hdn_Adet.Value = dr["Adet"].ToString();
            //    hdn_Birim.Value = dr["BirimFiyatı"].ToString();
            //    hdn_Kdv.Value = dr["KDV"].ToString();
            //    hdn_UrunToplami.Value = dr["UrunToplamı"].ToString();

            //    gfx.DrawString(hdn_Urun.Value, font, XBrushes.Black, new XRect(yU, d, page.Width, page.Height), XStringFormats.Center);
            //    gfx.DrawString(hdn_Adet.Value, font, XBrushes.Black, new XRect(mY, d, page.Width, page.Height), XStringFormats.Center);
            //    gfx.DrawString(hdn_Birim.Value, font, XBrushes.Black, new XRect(bY, d, page.Width, page.Height), XStringFormats.Center);
            //    gfx.DrawString(hdn_Kdv.Value, font, XBrushes.Black, new XRect(kY, d, page.Width, page.Height), XStringFormats.Center);
            //    gfx.DrawString(hdn_UrunToplami.Value, font, XBrushes.Black, new XRect(tY, d, page.Width, page.Height), XStringFormats.Center);
            //}
            //#endregion
            //gfx.DrawString("Toplam:", font, XBrushes.Black, new XRect(200, 250, page.Width, page.Height), XStringFormats.Center);
            //gfx.DrawString(lbl_Urun.Text, font, XBrushes.Black, new XRect(235, 250, page.Width, page.Height), XStringFormats.Center);
            //gfx.DrawString("%1 KDV:", font, XBrushes.Black, new XRect(197, 265, page.Width, page.Height), XStringFormats.Center);
            //gfx.DrawString(lbl_f1.Text, font, XBrushes.Black, new XRect(232, 265, page.Width, page.Height), XStringFormats.Center);
            //gfx.DrawString("%8 KDV:", font, XBrushes.Black, new XRect(197, 280, page.Width, page.Height), XStringFormats.Center);
            //gfx.DrawString(lbl_f8.Text, font, XBrushes.Black, new XRect(232, 280, page.Width, page.Height), XStringFormats.Center);
            //gfx.DrawString("%18 KDV:", font, XBrushes.Black, new XRect(194, 295, page.Width, page.Height), XStringFormats.Center);
            //gfx.DrawString(lbl_f18.Text, font, XBrushes.Black, new XRect(235, 295, page.Width, page.Height), XStringFormats.Center);
            //gfx.DrawString("Genel Toplam:", font, XBrushes.Black, new XRect(182, 310, page.Width, page.Height), XStringFormats.Center);
            //gfx.DrawString(lbl_toplam.Text, font, XBrushes.Black, new XRect(240, 310, page.Width, page.Height), XStringFormats.Center);

            //string filePath = @"C:\Users\korkm\source\repos\StokOtomasyon\StokOtomasyon\Fatura\" + GuidKey + ".pdf";
            //document.Save(filePath);
            //Process.Start(filePath);
            #endregion
        }

        public void gizle()
        {
            this.form.Visible = false;
            this.header.Visible = false;

            ddlUrun.Enabled = false;
            ddl_Sirketİsmi.Enabled = false;
            btn_Kayit.Enabled = false;
            btn_UrunEkle.Enabled = false;
            txt_Adet.Enabled = false;
            txt_BirimFiyatı.Enabled = false;
            this.sil.Visible = false;
            foreach (RepeaterItem ri in rptTbl.Items)
            {
                LinkButton lnk = ri.FindControl("linkSil") as LinkButton;
                lnk.Visible = false;

            }
        }

        [WebMethod()]
        public static string urunBilgileri(int UrunId)
        {
            string jsonString = "{}";
            if (!Mitra.IsDbNull(UrunId))
            {
                using (SqlDataAdapter adp = new SqlDataAdapter())
                {
                    SqlCommand getir = new SqlCommand();
                    getir.Connection = StokOtomasyon.DataAccessLayer.baglantiAyarla();
                    getir.CommandType = CommandType.StoredProcedure;
                    getir.CommandText = "sp_UrunGetirId";
                    getir.Parameters.AddWithValue("@UrunId", UrunId);
                    adp.SelectCommand = getir;
                    DataSet oDs = new DataSet();
                    adp.Fill(oDs);
                    jsonString = JsonConvert.SerializeObject(oDs, Formatting.Indented);

                }
            }
            return jsonString;
        }

        [WebMethod()]
        public static string fiyat(int UrunId, string htipi)
        {
            string jsonString = "{}";
            if (!Mitra.IsDbNull(UrunId) || Mitra.IsDbNull(htipi))
            {
                using (SqlDataAdapter adp = new SqlDataAdapter())
                {
                    SqlCommand getir = new SqlCommand();
                    getir.Connection = StokOtomasyon.DataAccessLayer.baglantiAyarla();
                    getir.CommandType = CommandType.StoredProcedure;
                    getir.CommandText = "sp_FiyatGetir";
                    getir.Parameters.AddWithValue("@UrunId", UrunId);
                    getir.Parameters.AddWithValue("@htipi", htipi);
                    adp.SelectCommand = getir;
                    DataSet oDs = new DataSet();
                    adp.Fill(oDs);
                    jsonString = JsonConvert.SerializeObject(oDs, Formatting.Indented);

                }
            }
            return jsonString;
        }

    }
}