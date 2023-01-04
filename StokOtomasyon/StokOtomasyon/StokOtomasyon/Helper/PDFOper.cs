using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp;
using System.Data.SqlClient;
using System.Xml;
using System.Web;

public static class PdfOper
{
    static XFont fntMainHeader = new XFont("Verdana", 14, XFontStyle.Bold, new XPdfFontOptions(PdfFontEncoding.Unicode));
    static XFont fntMainBold = new XFont("Times New Roman", 12, XFontStyle.Regular,new XPdfFontOptions(PdfFontEncoding.Unicode));
    static XFont fntMainBold6px = new XFont("Verdana", 6, XFontStyle.Bold);
    static XFont fntMain = new XFont("Verdana", 8, XFontStyle.Regular);
    static XFont fntDetail = new XFont("Verdana", 9, XFontStyle.Bold);
    static XFont fntDetail6px = new XFont("Verdana", 9, XFontStyle.Regular); 

    static PdfDocument document;
    static PdfPage page;
    static XGraphics gfx;
    static DataSet faturafiyatlar;



    #region "PrintFunctions"
    public static void XPrintCentered(PdfSharp.Drawing.XGraphics value, string Text, PdfSharp.Drawing.XFont Font, float X, float Y, float Width, PdfSharp.Drawing.XBrush Brush = null)
    {
        PdfSharp.Drawing.XBrush Brs = default(PdfSharp.Drawing.XBrush);
        if (Brush == null)
        {
            Brs = PdfSharp.Drawing.XBrushes.Black;
        }
        else
        {
            Brs = Brush;
        }
        PdfSharp.Drawing.XSize Sz = value.MeasureString(Text, Font);
        value.DrawString(Text, Font, Brs, X + (Width - Sz.Width) / 2, Y);
    }
    public static string FindIdealSize(PdfSharp.Drawing.XGraphics value, string Text, PdfSharp.Drawing.XFont Font, float x, float x2)
    {
        string yenitext = "";
        for (int i = 0; i <= Text.Length - 1; i++)
        {
            yenitext = Text.Substring(0, i);
            PdfSharp.Drawing.XSize Sz = value.MeasureString(yenitext, Font);
            if (x + Sz.Width >= x2)
            {
                yenitext = Text.Substring(0, i - 1);
                break; // TODO: might not be correct. Was : Exit For
            }
        }
        return yenitext;
    }
    public static int KacSatir(PdfSharp.Drawing.XGraphics value, string Text, PdfSharp.Drawing.XFont Font, float x, float x2)
    {
        int SatirAdet = 1;
        string yenitext = "";
        for (int i = 0; i <= Text.Length - 1; i++)
        {
            yenitext = Text.Substring(0, i);
            PdfSharp.Drawing.XSize Sz = value.MeasureString(yenitext, Font);
            if (x + Sz.Width >= x2)
            {
                if (Text.LastIndexOf(" ", i) > 0)
                {
                    yenitext = Text.Substring(0, Text.LastIndexOf(" ", i));
                }
                else if (Text.LastIndexOf("-", i) > 0)
                {
                    yenitext = Text.Substring(0, Text.LastIndexOf("-", i));
                }
                else if (Text.LastIndexOf("+", i) > 0)
                {
                    yenitext = Text.Substring(0, Text.LastIndexOf("+", i));
                }
                else if (Text.LastIndexOf(",", i) > 0)
                {
                    yenitext = Text.Substring(0, Text.LastIndexOf(".", i));
                }
                else if (Text.LastIndexOf("*", i) > 0)
                {
                    yenitext = Text.Substring(0, Text.LastIndexOf("*", i));
                }
                else if (Text.LastIndexOf("/", i) > 0)
                {
                    yenitext = Text.Substring(0, Text.LastIndexOf("/", i));
                }
                else if (Text.LastIndexOf("\\", i) > 0)
                {
                    yenitext = Text.Substring(0, Text.LastIndexOf("\\", i));
                }
                else
                {
                    yenitext = Text.Substring(0, i);
                }
                Text = Text.Substring(yenitext.Length + 1);
                SatirAdet = SatirAdet + KacSatir(value, Text, Font, x, x2);
                break; // TODO: might not be correct. Was : Exit For
            }
        }
        return SatirAdet;
    }
    public static string TakeNeededPart(PdfSharp.Drawing.XGraphics value, string Text, PdfSharp.Drawing.XFont Font, float x, float x2, int hangiParca)
    {
        string yenitext = "";
        int bolumno = 1;
        for (int i = 0; i <= Text.Length - 1; i++)
        {
            yenitext = Text.Substring(0, i);
            PdfSharp.Drawing.XSize Sz = value.MeasureString(yenitext, Font);
            if (x + Sz.Width >= x2)
            {
                if (Text.LastIndexOf(" ", i) > 0)
                {
                    yenitext = Text.Substring(0, Text.LastIndexOf(" ", i));
                }
                else if (Text.LastIndexOf("-", i) > 0)
                {
                    yenitext = Text.Substring(0, Text.LastIndexOf("-", i));
                }
                else if (Text.LastIndexOf("+", i) > 0)
                {
                    yenitext = Text.Substring(0, Text.LastIndexOf("+", i));
                }
                else if (Text.LastIndexOf(",", i) > 0)
                {
                    yenitext = Text.Substring(0, Text.LastIndexOf(".", i));
                }
                else if (Text.LastIndexOf("*", i) > 0)
                {
                    yenitext = Text.Substring(0, Text.LastIndexOf("*", i));
                }
                else if (Text.LastIndexOf("/", i) > 0)
                {
                    yenitext = Text.Substring(0, Text.LastIndexOf("/", i));
                }
                else if (Text.LastIndexOf("\\", i) > 0)
                {
                    yenitext = Text.Substring(0, Text.LastIndexOf("\\", i));
                }
                else
                {
                    yenitext = Text.Substring(0, i);
                }
                Text = Text.Substring(yenitext.Length + 1);
                if (bolumno >= hangiParca)
                {
                    return yenitext;
                }
                else
                {
                    return TakeNeededPart(value, Text, Font, x, x2, hangiParca - 1);
                }
            }
        }
        return yenitext;
    }
    public static int XPrint2a(PdfSharp.Drawing.XGraphics value, string Text, PdfSharp.Drawing.XFont Font, float X, float X2, float Y, XTextAlignment Alignment = XTextAlignment.Left, PdfSharp.Drawing.XBrush Brush = null)
    {
        //Chr(13), "\n")
        int kacsatira = 1;
        PdfSharp.Drawing.XBrush Brs = default(PdfSharp.Drawing.XBrush);
        if (Brush == null)
        {
            Brs = PdfSharp.Drawing.XBrushes.Black;
        }
        else
        {
            Brs = Brush;
        }
        string[] sStr = null;
        int topSize = 0;
        sStr = Text.Split((char)13);
        for (int l = 0; l <= sStr.Length - 1; l++)
        {
            PdfSharp.Drawing.XSize Sz = value.MeasureString(sStr[l], Font);
            switch (Alignment)
            {
                case XTextAlignment.Left:
                    if (X + Sz.Width > X2)
                    {
                        kacsatira = KacSatir(value, sStr[l], Font, X, X2);
                        for (int i = 1; i <= kacsatira; i++)
                        {
                            value.DrawString(TakeNeededPart(value, sStr[l], Font, X, X2, i), Font, Brs, X, Y + (i - 1) * 10 + topSize * 10);
                        }
                    }
                    else
                    {
                        value.DrawString(sStr[l], Font, Brs, X, Y + 10 * topSize);
                    }
                    break;
                case XTextAlignment.Right:
                    value.DrawString(sStr[l], Font, Brs, X - Sz.Width, Y + 10 * topSize);
                    kacsatira = 1;
                    break;
            }
            //(Y - 10) + i * 10
            topSize = topSize + kacsatira;
        }
        return topSize;
    }
    public static void XPrint2(PdfSharp.Drawing.XGraphics value, string Text, PdfSharp.Drawing.XFont Font, float X, float X2, float Y, XTextAlignment Alignment = XTextAlignment.Left, PdfSharp.Drawing.XBrush Brush = null)
    {
        PdfSharp.Drawing.XBrush Brs = default(PdfSharp.Drawing.XBrush);
        if (Brush == null)
        {
            Brs = PdfSharp.Drawing.XBrushes.Black;
        }
        else
        {
            Brs = Brush;
        }
        PdfSharp.Drawing.XSize Sz = value.MeasureString(Text, Font);
        switch (Alignment)
        {
            case XTextAlignment.Left:
                if (X + Sz.Width > X2)
                {
                    value.DrawString(FindIdealSize(value, Text, Font, X, X2), Font, Brs, X, Y);
                }
                else
                {
                    value.DrawString(Text, Font, Brs, X, Y);
                }
                break;
            case XTextAlignment.Right:
                value.DrawString(Text, Font, Brs, X - Sz.Width, Y);
                break;
        }
    }
    public static void XPrint(PdfSharp.Drawing.XGraphics value, string Text, PdfSharp.Drawing.XFont Font, float X, float Y, XTextAlignment Alignment = XTextAlignment.Left, PdfSharp.Drawing.XBrush Brush = null)
    {
        PdfSharp.Drawing.XBrush Brs = default(PdfSharp.Drawing.XBrush);
        if (Brush == null)
        {
            Brs = PdfSharp.Drawing.XBrushes.Black;
        }
        else
        {
            Brs = Brush;
        }
        switch (Alignment)
        {

            case XTextAlignment.Left:
                value.DrawString(Text, Font, Brs, X, Y);
                break;
            case XTextAlignment.Right:
                PdfSharp.Drawing.XSize Sz = value.MeasureString(Text, Font);
                value.DrawString(Text, Font, Brs, X - Sz.Width, Y);
                break;
        }
    }
    public enum XTextAlignment
    {
        Left,
        Right
    }
    #endregion
    #region "helperfnc"
    public static string YesNo(object ValA)
    {
        string functionReturnValue = null;
        try
        {
            if ((ValA == null))
            {
                functionReturnValue = "";
            }
            else
            {
                if (object.ReferenceEquals(ValA, DBNull.Value))
                {
                    functionReturnValue = "";
                }
                else if (string.IsNullOrEmpty(ValA.ToString()) || ValA.ToString() == "undefined" || ValA.ToString() == "null")
                {
                    functionReturnValue = "";
                }
                else
                {
                    if (Convert.ToUInt32(ValA) == 1)
                    {
                        functionReturnValue = "Yes";
                    }
                    else
                    {
                        functionReturnValue = "No";
                    }
                }

            }
        }
        catch (Exception ex)
        {
            return "";
        }
        return functionReturnValue;
    }
    #endregion

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

    public static void FaturaBaslik(DataTable baslik, ref XGraphics gfy)
    {


        if (baslik.Rows.Count > 0)
        {
            DataRow drr = baslik.Rows[0];
            XPrint2a(gfy, drr["SirketAdi"].ToString(), fntMainBold, 20, 250, 130, XTextAlignment.Left);
            XPrint2a(gfy, drr["FaturaAdresi"].ToString(), fntMainBold, 20, 250, 145, XTextAlignment.Left);
            XPrint(gfy, drr["VergiDaire"].ToString(), fntMainBold, 150, 235, XTextAlignment.Left);
            XPrint(gfy, SQL2CSDate2String(drr["FaturaTarih"], "dd/MM/yyyy"), fntMainBold, 480, 170, XTextAlignment.Left);
            XPrint(gfy, DateTime.Now.ToShortTimeString(), fntMainBold, 480, 190, XTextAlignment.Left);
        }
    }

    public static void faturaUrunler(DataTable urunler, DataTable baslik, DataSet faturafiyatlar)
    {
        document = new PdfDocument();
        page = document.AddPage();
        page.Size = PageSize.A4;
        page.Orientation = PageOrientation.Portrait;
        gfx = XGraphics.FromPdfPage(page);

        int satir = 1, sayfaTopmlamSatir = 0; double UrunSayfaToplam = 0;
        int d = 270;
        bool byaz = true, nyaz = true;
        bool nakliyekunyaz = false;
        foreach (DataRow dr in urunler.Rows)
        {
            if (byaz)
            {
                FaturaBaslik(baslik, ref gfx);
                d = 270;
                byaz = false;
                if (nakliyekunyaz)
                {
                    XPrint2a(gfx, "Nakli Yekün", fntMainBold, 85, 300, d, XTextAlignment.Left);
                    XPrint(gfx, Convert.ToDouble(UrunSayfaToplam).ToString("c2"), fntMainBold, 500, d, XTextAlignment.Left);
                    nakliyekunyaz = false;
                    d = d + 15;
                }
            }
            satir = XPrint2a(gfx, dr["Urun"].ToString(), fntMainBold, 75, 300, d, XTextAlignment.Left);
            XPrint(gfx, dr["Adet"].ToString(), fntMainBold, 335, d, XTextAlignment.Left);
            XPrint(gfx, Convert.ToDouble(dr["BirimFiyatı"]).ToString("c2"), fntMainBold, 390, d, XTextAlignment.Left);
            if (dr["IskontoOrani"].ToString()!="0")
            {
                XPrint(gfx, dr["IskontoOrani"].ToString(), fntMainBold, 440, d, XTextAlignment.Left);
            }
           
            XPrint(gfx, dr["KDV"].ToString(), fntMainBold, 470, d, XTextAlignment.Left);
            XPrint(gfx, Convert.ToDouble(dr["UrunToplamı"]).ToString("c2"), fntMainBold, 500, d, XTextAlignment.Left);
            d = d + 15;
            UrunSayfaToplam += Convert.ToDouble(dr["UrunToplamı"]);
            sayfaTopmlamSatir = sayfaTopmlamSatir + satir;
            if (sayfaTopmlamSatir == 21)
            {
                XPrint(gfx, "Nakli Yekün:", fntMainBold, 500, 625, XTextAlignment.Right);
                XPrint(gfx, Convert.ToDouble(UrunSayfaToplam).ToString("c2"), fntMainBold, 505, 625, XTextAlignment.Left);
                page = document.AddPage();
                page.Size = PageSize.A4;
                page.Orientation = PageOrientation.Portrait;
                gfx = XGraphics.FromPdfPage(page);
                nakliyekunyaz = true; byaz = true;
                sayfaTopmlamSatir = 0;
            }
        }

        fiyatlar(faturafiyatlar);
    }

    public static void fiyatlar(DataSet faturafiyatlar)
    {
        int dikey = 625;

        //lbl_Iskonto.Text = ds.Tables[4].Rows[0]["IskontoMiktari"].ToString();
        XPrint(gfx, Convert.ToDouble(faturafiyatlar.Tables[0].Rows[0]["UrunToplam"]).ToString("c2"), fntMainBold, 505, dikey, XTextAlignment.Left);
        XPrint(gfx, Convert.ToDouble(faturafiyatlar.Tables[4].Rows[0]["IskontoMiktari"]).ToString("c2"), fntMainBold, 505, dikey + 15, XTextAlignment.Left);
        XPrint(gfx, Convert.ToDouble(faturafiyatlar.Tables[3].Rows[0]["IskontoluToplam"]).ToString("c2"), fntMainBold, 505, dikey+30, XTextAlignment.Left);
        

        foreach (DataRow dr in faturafiyatlar.Tables[1].Rows)
        {
            switch (dr["KDV"].ToString())
            {
                case "1":
                    XPrint(gfx, Convert.ToDouble(dr["KdvToplam"]).ToString("c2"), fntMainBold, 505, dikey + 45, XTextAlignment.Left);
                    break;
                case "8":
                    XPrint(gfx, Convert.ToDouble(dr["KdvToplam"]).ToString("c2"), fntMainBold, 505, dikey + 60, XTextAlignment.Left);
                    break;
                case "18":
                    XPrint(gfx, Convert.ToDouble(dr["KdvToplam"]).ToString("c2"), fntMainBold, 505, dikey + 75, XTextAlignment.Left);
                    break;
                default:
                    break;
            }
        }
        foreach (DataRow dr in faturafiyatlar.Tables[2].Rows)
        {
            XPrint(gfx, Convert.ToDouble(dr[0]).ToString("c2"), fntMainBold, 505, dikey +90, XTextAlignment.Left);
            XPrint(gfx, "YALNIZ:", fntMainBold, 60, dikey + 90, XTextAlignment.Left);
            XPrint(gfx, yaziyaCevir(Convert.ToDecimal(dr[0])).ToUpper(), fntMainBold, 105, dikey + 90, XTextAlignment.Left);
           
        }

        XPrint(gfx, "Toplam:", fntMainBold, 500, dikey, XTextAlignment.Right);
        XPrint(gfx, "İskonto Toplamı:", fntMainBold, 500, dikey+15, XTextAlignment.Right);
        XPrint(gfx, "Ara Toplam:", fntMainBold, 500, dikey+30, XTextAlignment.Right);
        XPrint(gfx, "%1 KDV:", fntMainBold, 500, dikey + 45, XTextAlignment.Right);
        XPrint(gfx, "%8 KDV:", fntMainBold, 500, dikey + 60, XTextAlignment.Right);
        XPrint(gfx, "%18 KDV:", fntMainBold, 500, dikey + 75, XTextAlignment.Right);
        XPrint(gfx, "Genel Toplam:", fntMainBold, 500, dikey +90, XTextAlignment.Right);

      

    }

    public static void pdfyaz(string faturaId)
    {
        SqlCommand sqlCommand = new SqlCommand("select * from Tbl_FaturaDetay where FaturaId=@id", StokOtomasyon.DataAccessLayer.baglantiAyarla());
        sqlCommand.Parameters.AddWithValue("@id", faturaId);
        SqlDataAdapter daa2 = new SqlDataAdapter();
        daa2.SelectCommand = sqlCommand;
        DataTable urunler = new DataTable();
        daa2.Fill(urunler);

        SqlCommand cmd = new SqlCommand();
        cmd.Connection = StokOtomasyon.DataAccessLayer.baglantiAyarla();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "sp_FaturaListele";
        cmd.Parameters.AddWithValue("@faturaId", faturaId);
        SqlDataAdapter daa = new SqlDataAdapter();
        daa.SelectCommand = cmd;
        DataTable baslik = new DataTable();
        daa.Fill(baslik);

        SqlCommand faturatoplam = new SqlCommand();
        faturatoplam.Connection = StokOtomasyon.DataAccessLayer.baglantiAyarla();
        faturatoplam.CommandType = CommandType.StoredProcedure;
        faturatoplam.CommandText = "sp_FaturaToplami";
        faturatoplam.Parameters.AddWithValue("@faturaId", faturaId);
        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = faturatoplam;
        faturafiyatlar = new DataSet();
        da.Fill(faturafiyatlar);

        faturaUrunler(urunler, baslik, faturafiyatlar);

        string GuidKey = Guid.NewGuid().ToString();
        string filePath = HttpContext.Current.Server.MapPath("~/Fatura") + "\\" + GuidKey + ".pdf";
        document.Save(filePath);
        Process.Start(filePath);
    }


    public static string yaziyaCevir(decimal tutar)
    {
        string sTutar = tutar.ToString("F2").Replace('.', ','); // Replace('.',',') ondalık ayracının . olma durumu için            
        string lira = sTutar.Substring(0, sTutar.IndexOf(',')); //tutarın tam kısmı
        string kurus = sTutar.Substring(sTutar.IndexOf(',') + 1, 2);
        string yazi = "";

        string[] birler = { "", "BİR", "İKİ", "Üç", "DÖRT", "BEŞ", "ALTI", "YEDİ", "SEKİZ", "DOKUZ" };
        string[] onlar = { "", "ON", "YİRMİ", "OTUZ", "KIRK", "ELLİ", "ALTMIŞ", "YETMİŞ", "SEKSEN", "DOKSAN" };
        string[] binler = { "KATRİLYON", "TRİLYON", "MİLYAR", "MİLYON", "BİN", "" }; //KATRİLYON'un önüne ekleme yapılarak artırabilir.

        int grupSayisi = 6; //sayıdaki 3'lü grup sayısı. katrilyon içi 6. (1.234,00 daki grup sayısı 2'dir.)
                            //KATRİLYON'un başına ekleyeceğiniz her değer için grup sayısını artırınız.

        lira = lira.PadLeft(grupSayisi * 3, '0'); //sayının soluna '0' eklenerek sayı 'grup sayısı x 3' basakmaklı yapılıyor.            

        string grupDegeri;

        for (int i = 0; i < grupSayisi * 3; i += 3) //sayı 3'erli gruplar halinde ele alınıyor.
        {
            grupDegeri = "";

            if (lira.Substring(i, 1) != "0")
                grupDegeri += birler[Convert.ToInt32(lira.Substring(i, 1))] + "YÜZ"; //yüzler                

            if (grupDegeri == "BİRYÜZ") //biryüz düzeltiliyor.
                grupDegeri = "YÜZ";

            grupDegeri += onlar[Convert.ToInt32(lira.Substring(i + 1, 1))]; //onlar

            grupDegeri += birler[Convert.ToInt32(lira.Substring(i + 2, 1))]; //birler                

            if (grupDegeri != "") //binler
                grupDegeri += binler[i / 3];

            if (grupDegeri == "BİRBİN") //birbin düzeltiliyor.
                grupDegeri = "BİN";

            yazi += grupDegeri;
        }

        if (yazi != "")
            yazi += " TL ";

        int yaziUzunlugu = yazi.Length;

        if (kurus.Substring(0, 1) != "0") //kuruş onlar
            yazi += onlar[Convert.ToInt32(kurus.Substring(0, 1))];

        if (kurus.Substring(1, 1) != "0") //kuruş birler
            yazi += birler[Convert.ToInt32(kurus.Substring(1, 1))];

        if (yazi.Length > yaziUzunlugu)
            yazi += " Kr.";
        else
            yazi += "SIFIR Kr.";

       
        return yazi;

    }


}
