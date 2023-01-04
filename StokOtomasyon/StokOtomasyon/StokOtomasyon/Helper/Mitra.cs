using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Web;
using System.Web.Security;
using System.Text.RegularExpressions;
using System.Threading;
using System.Globalization;
using System.Text;
using System.Data.Sql;

public static class Mitra
{
    public static string GetCurrentUserInitial()
    {
        //System.Security.Principal.WindowsIdentity.GetCurrent().Name; 
        //Environment.UserName;
        string Initial = HttpContext.Current.User.Identity.Name; //System.Security.Principal.WindowsIdentity.GetCurrent().Name;

        if (Initial.IndexOf('\\') > -1)
        {
            Initial = Initial.Substring(Initial.IndexOf('\\') + 1);
        }
        else if (Initial.IndexOf('@') > -1)
        {
            Initial = Initial.Substring(0, Initial.IndexOf('@'));
        }

        Initial = Initial.ToUpper();

        Initial = Regex.Replace(Initial, "[ıİ]", "I");
        //Initial.ToUpper(New CultureInfo("en-US", False))
        if (Initial == "ADMINISTRATOR")
        {
            Initial = "TOZU";
        }
        if (Initial == "ADMINTOZU")
        {
            Initial = "TOZU";
        }
        if (Initial == "ENVER")
        {
            Initial = "TOZU";
        }
        if (Initial == "TOZU")
        {
            Initial = "FACA";
        }
        if (Initial == "KORKM")
        {
            Initial = "TOZU";
        }
        if (!Mitra.IsDbNull(HttpContext.Current.Session))
        {
            if (!Mitra.IsDbNull(HttpContext.Current.Session["SahteUser"]))
            {
                Initial = HttpContext.Current.Session["SahteUser"].ToString();
            }
        }
        return Initial;
    }

   
    public static string GetRealUserInitial()
    {
        //System.Security.Principal.WindowsIdentity.GetCurrent().Name; 
        //Environment.UserName;
        string Initial = HttpContext.Current.User.Identity.Name; //System.Security.Principal.WindowsIdentity.GetCurrent().Name;

        if (Initial.IndexOf('\\') > -1)
        {
            Initial = Initial.Substring(Initial.IndexOf('\\') + 1);
        }
        else if (Initial.IndexOf('@') > -1)
        {
            Initial = Initial.Substring(0, Initial.IndexOf('@'));
        }

        Initial = Initial.ToUpper();

        Initial = Regex.Replace(Initial, "[ıİ]", "I");

        return Initial;
    }

    public static void ComboStart(ref System.Web.UI.WebControls.DropDownList cmb, string defvalue = "", string defText = "")
    {
        cmb.Items.Clear();
        System.Web.UI.WebControls.ListItem itm = new System.Web.UI.WebControls.ListItem(defText, defvalue);
        cmb.Items.Add(itm);
    }
    public static void RadioStart(ref System.Web.UI.WebControls.RadioButtonList cmb, string defvalue = "", string defText = "")
    {
        cmb.Items.Clear();
        System.Web.UI.WebControls.ListItem itm = new System.Web.UI.WebControls.ListItem(defText, defvalue);
        cmb.Items.Add(itm);
    }


    public static void ListboxStart(ref System.Web.UI.WebControls.ListBox cmb, string defvalue = "", string defText = "")
    {
        cmb.Items.Clear();
        System.Web.UI.WebControls.ListItem itm = new System.Web.UI.WebControls.ListItem(defText, defvalue);
        cmb.Items.Add(itm);
    }

    public static void oturum()
    {
       
        object kullanici = HttpContext.Current.Session["kullanici"];
        if (kullanici == null)
        {
            HttpContext.Current.Response.Redirect("default.aspx");
        }
    }

    public static object IsDbNull(object ValA, object ValB)
    {
        object functionReturnValue = null;
        try
        {
            if ((ValA == null))
            {
                functionReturnValue = ValB;
            }
            else if (ValA.ToString() == "null" || ValA.ToString() == "0" || ValA.ToString() == "undefined")
            {
                functionReturnValue = ValB;
            }
            else if (IsDate(ValA))
            {
                if (object.ReferenceEquals(ValA, DBNull.Value))
                {
                    functionReturnValue = ValB;
                }
                else
                {
                    functionReturnValue = ValA;
                }
            }
            else
            {
                if (object.ReferenceEquals(ValA, DBNull.Value))
                {
                    functionReturnValue = ValB;
                }
                else if (string.IsNullOrEmpty(ValA.ToString()))
                {
                    functionReturnValue = ValB;
                }
                else
                {
                    functionReturnValue = ValA;
                }

            }
        }
        catch (Exception ex)
        {

        }
        return functionReturnValue;
    }


    public static Boolean IsDbNull(object ValA)
    {
        try
        {
            if ((ValA == null))
            {
                return true;
            }
            else if (ValA.ToString() == "0")
            { return true; }
            else if (ValA.ToString() == "null")
            { return true; }
            else if (ValA.ToString() == "undefined")
            { return true; }
            else if (ValA is DateTime)
            {
                if (object.ReferenceEquals(ValA, DBNull.Value))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                if (object.ReferenceEquals(ValA, DBNull.Value))
                {
                    return true;
                }
                else if (string.IsNullOrEmpty(ValA.ToString()))
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
        }
        catch (Exception ex)
        {
            return false;
        }
    }
    public static bool IsDate(Object obj)
    {
        string strDate = obj.ToString();
        try
        {
            DateTime dt = DateTime.Parse(strDate);
            if (dt != DateTime.MinValue && dt != DateTime.MaxValue)
                return true;
            return false;
        }
        catch
        {
            return false;
        }
    }

    public enum TDateType
    {

        ddMMyyyy = 1,
        yyyyMMdd = 2,
        ddMMMyyyy = 3
    }
    public static double tToDouble(string value, string aSep = ".", char aDec = ',')
    {
        double myResult = 0;
        if (IsDbNull(value))
        {
            return 0;
        }
        else
        {
            string[] xx = value.Replace(aSep, "").Split(aDec);
            if (xx.Length == 2)
            {
                double a = xx[1].Length;
                double b = Convert.ToDouble(Mitra.IsDbNull(xx[1], 0));
                myResult = Convert.ToDouble(Mitra.IsDbNull(xx[0], 0)) + (b / (Math.Pow(10, a)));
            }
            else
            {
                myResult = Convert.ToDouble(xx[0]);
            }
            //            Return Val(Regex.Replace(value, "[,]", ""))
        }
        return myResult;
    }

    public static Nullable<System.DateTime> TDate(string d, TDateType t = TDateType.ddMMMyyyy)
    {

        if (IsDbNull(d))
        {
            return null;
        }
        //Try
        DateTime k = default(DateTime);
        bool ol = false;

        switch (t)
        {
            //case TDateType.ddMMMyyyy
            // 'DateTime.TryParseExact(d, "dd.MMM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, TDate) 'DateTime.Parse(d) '
            // Return DateSerial(d.Substring(7, 4), d.Substring(3, 2), d.Substring(0, 2))
            case TDateType.ddMMyyyy:

                return new DateTime(Convert.ToInt32(d.Substring(6, 4)), Convert.ToInt32(d.Substring(3, 2)), Convert.ToInt32(d.Substring(0, 2)));
            case TDateType.ddMMMyyyy:
                ol = DateTime.TryParse(d, out k);
                if (!ol)
                {
                    ol = DateTime.TryParseExact(d, "dd.MMM.yyyy", System.Threading.Thread.CurrentThread.CurrentCulture, DateTimeStyles.None, out k);
                }
                if (!ol)
                {
                    ol = DateTime.TryParseExact(d, "dd.MMM.yyyy", CultureInfo.GetCultureInfo("en-GB"), DateTimeStyles.None, out k);
                }
                if (!ol)
                {
                    ol = DateTime.TryParseExact(d, "dd.MMM.yyyy", CultureInfo.GetCultureInfo("ar-MA"), DateTimeStyles.None, out k);
                }
                if (!ol)
                {
                    ol = DateTime.TryParseExact(d, "dd.MMM.yyyy", CultureInfo.GetCultureInfo("tr-TR"), DateTimeStyles.None, out k);
                }
                break;
            case TDateType.yyyyMMdd:
                return new DateTime(Convert.ToInt32(d.Substring(0, 4)), Convert.ToInt32(d.Substring(4, 2)), Convert.ToInt32(d.Substring(6, 2)));
            default:
                return DateTime.Now;
        }
        if (ol)
        {
            return k;
        }
        else
        {
            return null;
        }


        //Catch ex As Exception
        // Return Nothing
        //End Try
    }
    public static System.Boolean IsNumeric(System.Object Expression)
    {
        if (Expression == null || Expression is DateTime)
            return false;

        if (Expression is Int16 || Expression is Int32 || Expression is Int64 || Expression is Decimal || Expression is Single || Expression is Double || Expression is Boolean)
            return true;

        int test;
        return int.TryParse(Expression.ToString(), out test);
        //try
        //{
        //    if (Expression is string)
        //        Double.Parse(Expression as string);
        //    else
        //        Double.Parse(Expression.ToString());
        //    return true;
        //}
        //catch { } // just dismiss errors but return false
        //return false;
    }

    public static bool IsNumeric(string input)
    {
        int test;
        return int.TryParse(input, out test);
    }
    /// <summary>
    /// Creates mail address from initial
    /// </summary>
    /// <param name="initial">mail user</param>
    /// <returns>returns initial@novonordisk.com</returns>
    public static string MakeEmail(string initial)
    {
        if (!Mitra.IsDbNull(initial))
        {
            if (System.Configuration.ConfigurationManager.AppSettings["Demo"].ToString() == "1")
            {
                return System.Configuration.ConfigurationManager.AppSettings["OnlyMailer"].ToString();
            }
            else
            {
                if (initial.Contains(","))
                {
                    string mailers = "", virgul = "";
                    foreach (string item in initial.Split(','))
                    {
                        if (!Mitra.IsDbNull(item))
                        {
                            if (item.Contains("@"))
                            {
                                mailers += virgul + item; virgul = ",";
                            }
                            else
                            {
                                mailers += virgul + item + "@novonordisk.com"; virgul = ",";
                            }
                        }
                    }
                    return mailers;
                }
                else
                {
                    if (initial.Contains("@"))
                    {
                        return initial;
                    }
                    else
                    {
                        return initial + "@novonordisk.com";
                    }
                }

            }
        }
        else { return ""; }

    }

    public static string DosyaLinkOlustur(string DosyaAdres, string DosyaAd, string DocumentDesc, string FormID, string askdel, string FormType, string RowID, string MainTbl)
    {
        if (IsDbNull(DosyaAdres)) { return ""; }
        StringBuilder strb = new StringBuilder();
        System.IO.FileInfo f = new System.IO.FileInfo(HttpContext.Current.Server.MapPath(DosyaAdres));
        DosyaAdres = DosyaAdres.Replace("~/", "../");

        String imgStr;
        if (askdel == "")
        {
            imgStr = "<div class=\"col-lg-2 col-md-2 col-sm-6 col-xs-6\"><div class=\"panel \"><div class=\"panel-heading attachment-panel-heading\"><h3 class=\"panel-title text-center attachment-panel-heading-h3\">{3}</h3></div><div class=\"panel-body attachment-panel-body\"><p class=\"text-center attachment-panel-body-p\"><a href='{0}' title='' target='_blank' border='0' >{2}</a></p></div></div></div>";
        }
        else
        {
            imgStr = "<div class=\"col-lg-2 col-md-2 col-sm-6 col-xs-6\"><div class=\"AttachShadow\"><div class=\"panel attachment-panel-blue\"><div class=\"panel-body attachment-panel-body\"><p class=\"text-center attachment-panel-body-p\"><a href='{0}' title='' target='_blank' border='0' >{2}</a><br/>{3}</p></div><div class=\"panel-heading attachment-panel-heading\"><h3 class=\"panel-title text-center attachment-panel-heading-h3\"><i onclick=\"javascript:DelAttach({6},'{5}',{4},'{7}','{8}')\" class=\"mhc-cpointer mcc-danger fa fa-trash fa-2x\" aria-hidden=\"true\"></i></h3></div></div></div></div>";
        }

        switch (f.Extension.ToLower())
        {
            case ".7z":
                strb.AppendFormat(imgStr, DosyaAdres, DosyaAd, "<i class=\"mbbc mbbc-addC mcc-fzip fa fa-file-archive-o\" aria-hidden=\"true\"></i>", DocumentDesc, FormID, askdel, RowID, FormType, MainTbl); break;
            case ".rar":
                strb.AppendFormat(imgStr, DosyaAdres, DosyaAd, "<i class=\"mbbc mbbc-addC mcc-fzip fa fa-file-archive-o\" aria-hidden=\"true\"></i>", DocumentDesc, FormID, askdel, RowID, FormType, MainTbl); break;
            case ".zip":
                strb.AppendFormat(imgStr, DosyaAdres, DosyaAd, "<i class=\"mbbc mbbc-addC mcc-fzip fa fa-file-archive-o\" aria-hidden=\"true\"></i>", DocumentDesc, FormID, askdel, RowID, FormType, MainTbl); break;
            case ".mp3":
                strb.AppendFormat(imgStr, DosyaAdres, DosyaAd, "<i class=\"mbbc mbbc-addC mcc-faudio fa fa-file-audio-o\" aria-hidden=\"true\"></i>", DocumentDesc, FormID, askdel, RowID, FormType, MainTbl); break;
            case ".xls":
                strb.AppendFormat(imgStr, DosyaAdres, DosyaAd, "<i class=\"mbbc mbbc-addC mcc-fexcel fa fa-file-excel-o\" aria-hidden=\"true\"></i>", DocumentDesc, FormID, askdel, RowID, FormType, MainTbl); break;
            case ".xlsx":
                strb.AppendFormat(imgStr, DosyaAdres, DosyaAd, "<i class=\"mbbc mbbc-addC mcc-fexcel fa fa-file-excel-o\" aria-hidden=\"true\"></i>", DocumentDesc, FormID, askdel, RowID, FormType, MainTbl); break;
            case ".bmp":
                strb.AppendFormat(imgStr, DosyaAdres, DosyaAd, "<i class=\"mbbc mbbc-addC mcc-fimage fa fa-file-image-o\" aria-hidden=\"true\"></i>", DocumentDesc, FormID, askdel, RowID, FormType, MainTbl); break;
            case ".gif":
                strb.AppendFormat(imgStr, DosyaAdres, DosyaAd, "<i class=\"mbbc mbbc-addC mcc-fimage fa fa-file-image-o\" aria-hidden=\"true\"></i>", DocumentDesc, FormID, askdel, RowID, FormType, MainTbl); break;
            case ".jpeg":
                strb.AppendFormat(imgStr, DosyaAdres, DosyaAd, "<i class=\"mbbc mbbc-addC mcc-fimage fa fa-file-image-o\" aria-hidden=\"true\"></i>", DocumentDesc, FormID, askdel, RowID, FormType, MainTbl); break;
            case ".jpg":
                strb.AppendFormat(imgStr, DosyaAdres, DosyaAd, "<i class=\"mbbc mbbc-addC mcc-fimage fa fa-file-image-o\" aria-hidden=\"true\"></i>", DocumentDesc, FormID, askdel, RowID, FormType, MainTbl); break;
            case ".png":
                strb.AppendFormat(imgStr, DosyaAdres, DosyaAd, "<i class=\"mbbc mbbc-addC mcc-fimage fa fa-file-image-o\" aria-hidden=\"true\"></i>", DocumentDesc, FormID, askdel, RowID, FormType, MainTbl); break;
            case ".tif":
                strb.AppendFormat(imgStr, DosyaAdres, DosyaAd, "<i class=\"mbbc mbbc-addC mcc-fimage fa fa-file-image-o\" aria-hidden=\"true\"></i>", DocumentDesc, FormID, askdel, RowID, FormType, MainTbl); break;
            case ".tiff":
                strb.AppendFormat(imgStr, DosyaAdres, DosyaAd, "<i class=\"mbbc mbbc-addC mcc-fimage fa fa-file-image-o\" aria-hidden=\"true\"></i>", DocumentDesc, FormID, askdel, RowID, FormType, MainTbl); break;
            case ".pdf":
                strb.AppendFormat(imgStr, DosyaAdres, DosyaAd, "<i class=\"mbbc mbbc-addC mcc-fpdf fa fa-file-pdf-o\" aria-hidden=\"true\"></i>", DocumentDesc, FormID, askdel, RowID, FormType, MainTbl); break;
            case ".pps":
                strb.AppendFormat(imgStr, DosyaAdres, DosyaAd, "<i class=\"mbbc mbbc-addC mcc-fpowerpoint fa fa-file-powerpoint-o\" aria-hidden=\"true\"></i>", DocumentDesc, FormID, askdel, RowID, FormType, MainTbl); break;
            case ".ppt":
                strb.AppendFormat(imgStr, DosyaAdres, DosyaAd, "<i class=\"mbbc mbbc-addC mcc-fpowerpoint fa fa-file-powerpoint-o\" aria-hidden=\"true\"></i>", DocumentDesc, FormID, askdel, RowID, FormType, MainTbl); break;
            case ".pptx":
                strb.AppendFormat(imgStr, DosyaAdres, DosyaAd, "<i class=\"mbbc mbbc-addC mcc-fpowerpoint fa fa-file-powerpoint-o\" aria-hidden=\"true\"></i>", DocumentDesc, FormID, askdel, RowID, FormType, MainTbl); break;
            case ".txt":
                strb.AppendFormat(imgStr, DosyaAdres, DosyaAd, "<i class=\"mbbc mbbc-addC mcc-ftext fa fa-file-text-o\" aria-hidden=\"true\"></i>", DocumentDesc, FormID, askdel, RowID, FormType, MainTbl); break;
            case ".avi":
                strb.AppendFormat(imgStr, DosyaAdres, DosyaAd, "<i class=\"mbbc mbbc-addC mcc-fvideo fa fa-file-video-o\" aria-hidden=\"true\"></i>", DocumentDesc, FormID, askdel, RowID, FormType, MainTbl); break;
            case ".flv":
                strb.AppendFormat(imgStr, DosyaAdres, DosyaAd, "<i class=\"mbbc mbbc-addC mcc-fvideo fa fa-file-video-o\" aria-hidden=\"true\"></i>", DocumentDesc, FormID, askdel, RowID, FormType, MainTbl); break;
            case ".mov":
                strb.AppendFormat(imgStr, DosyaAdres, DosyaAd, "<i class=\"mbbc mbbc-addC mcc-fvideo fa fa-file-video-o\" aria-hidden=\"true\"></i>", DocumentDesc, FormID, askdel, RowID, FormType, MainTbl); break;
            case ".mp4":
                strb.AppendFormat(imgStr, DosyaAdres, DosyaAd, "<i class=\"mbbc mbbc-addC mcc-fvideo fa fa-file-video-o\" aria-hidden=\"true\"></i>", DocumentDesc, FormID, askdel, RowID, FormType, MainTbl); break;
            case ".swf":
                strb.AppendFormat(imgStr, DosyaAdres, DosyaAd, "<i class=\"mbbc mbbc-addC mcc-fvideo fa fa-file-video-o\" aria-hidden=\"true\"></i>", DocumentDesc, FormID, askdel, RowID, FormType, MainTbl); break;
            case ".doc":
                strb.AppendFormat(imgStr, DosyaAdres, DosyaAd, "<i class=\"mbbc mbbc-addC mcc-fword fa fa-file-word-o\" aria-hidden=\"true\"></i>", DocumentDesc, FormID, askdel, RowID, FormType, MainTbl); break;
            case ".docx":
                strb.AppendFormat(imgStr, DosyaAdres, DosyaAd, "<i class=\"mbbc mbbc-addC mcc-fword fa fa-file-word-o\" aria-hidden=\"true\"></i>", DocumentDesc, FormID, askdel, RowID, FormType, MainTbl); break;
            case ".rtf":
                strb.AppendFormat(imgStr, DosyaAdres, DosyaAd, "<i class=\"mbbc mbbc-addC mcc-fword fa fa-file-word-o\" aria-hidden=\"true\"></i>", DocumentDesc, FormID, askdel, RowID, FormType, MainTbl); break;
            default:
                strb.AppendFormat(imgStr, DosyaAdres, DosyaAd, "<i class=\"mbbc mbbc-addC mcc-ffile fa fa-file-o\" aria-hidden=\"true\"></i>", DocumentDesc, FormID, askdel, RowID, FormType, MainTbl); break;
        }
        return strb.ToString();
    }

    public static string UpperEng(string strC)
    {
        //ç, Ç, ö, Ö, ş, Ş, ı, İ, ü, Ü, ğ, Ğ
        //c, C, o, O, s, S, i, I, u, U, g, G
        strC = strC.ToUpper();
        strC = strC.Replace("i", "I");
        strC = strC.Replace("ı", "I");
        strC = strC.Replace("İ", "I");
        strC = strC.Replace("ş", "S");
        strC = strC.Replace("Ş", "S");
        strC = strC.Replace("ğ", "G");
        strC = strC.Replace("Ğ", "G");
        strC = strC.Replace("ö", "O");
        strC = strC.Replace("o", "O");
        strC = strC.Replace("ç", "C");
        strC = strC.Replace("Ç", "C");
        strC = strC.Replace("ü", "U");
        strC = strC.Replace("Ü", "U");
        return strC;
    }


    public static void giveSuccess(System.Web.UI.Control Frm, string msgHeader, string msgBody, string msgDelay = "false")
    {
        //'error|success|warning|info
        string xx = "$(document).ready(function () {Lobibox.notify(\"success\", {title: \"" + msgHeader + "\", msg: \"" + msgBody + "\", position: \"top right\", sound: false, delay: " + msgDelay + " });});";
        System.Web.UI.ScriptManager.RegisterClientScriptBlock(Frm, Frm.GetType(), Guid.NewGuid().ToString(), xx, true);
    }
    public static void giveWarning(System.Web.UI.Control Frm, string msgHeader, string msgBody, string msgDelay = "false")
    {
        string xx = "$(document).ready(function () {Lobibox.notify(\"warning\", {title: \"" + msgHeader + "\", msg: \"" + msgBody + "\", position: \"center top\", sound: false, delay: " + msgDelay + " });});";
        System.Web.UI.ScriptManager.RegisterClientScriptBlock(Frm, Frm.GetType(), Guid.NewGuid().ToString(), xx, true);
    }

    public static void giveError(System.Web.UI.Control Frm, string msgHeader, string msgBody, string myType = "xpnotify")
    {
        string xx = null;
        //if (myType == "pnotify")
        //{
        //    xx = "new PNotify({title: '" + msgHeader + "',text: '" + msgBody + "',icon: 'glyphicon glyphicon-question-sign',hide: false,confirm: {confirm: true,buttons: [{text: 'Ok',addClass: 'btn-primary',click: function (notice) {notice.remove();}},null]}, buttons: {closer: false,sticker: false}, history: {history: false}, addclass: 'stack-modal',stack: { 'dir1': 'down', 'dir2': 'right', 'modal': true }});";
        //}
        //else
        //{
        xx = "$(document).ready(function () {Lobibox.alert(\"error\", {title: \"" + msgHeader + "\", msg: \"" + msgBody + "\", sound: false, delay: false });});";
        //}


        System.Web.UI.ScriptManager.RegisterClientScriptBlock(Frm, Frm.GetType(), Guid.NewGuid().ToString(), xx, true);
    }
    //public static void giveSuccess(System.Web.UI.Control Frm, string msgHeader, string msgBody)
    //{
    //    //'error|success|warning|info
    //    //string xx = "$(document).ready(function () {swal({title: \"" + msgHeader + "\", html: \"" + msgBody + "\", type: \"success\", timer: 9500 });});";
    //    string xx = "$(document).ready(function () {swal({title: \"" + msgHeader + "\", html: \"" + msgBody + "\", type: \"success\", timer: 9500 });});";
    //    System.Web.UI.ScriptManager.RegisterClientScriptBlock(Frm, Frm.GetType(), Guid.NewGuid().ToString(), xx, true);
    //}
    //public static void giveWarning(System.Web.UI.Control Frm, string msgHeader, string msgBody)
    //{
    //    string xx = "$(document).ready(function () {swal({title: \'<span class=\"title\">" + msgHeader + "</span>\', html: \'<span class=\"text\">" + msgBody + "</span>\', type: \'warning\'});});";
    //    //string xx = "$(document).ready(function () {swal({title: '" + msgHeader + "','" + msgBody + "'," + "'warning' );});";
    //    System.Web.UI.ScriptManager.RegisterClientScriptBlock(Frm, Frm.GetType(), Guid.NewGuid().ToString(), xx, true);
    //}
    //public static void giveError(System.Web.UI.Control Frm, string msgHeader, string msgBody, string myType = "xpnotify")
    //{
    //    string xx = "$(document).ready(function () {swal({title: \"" + msgHeader + "\", html: \"" + msgBody + "\", type: \"error\"});});";
    //    //string xx = "$(document).ready(function () {swal('" + msgHeader + "','" + msgBody + "'," + "'' );});";
    //    System.Web.UI.ScriptManager.RegisterClientScriptBlock(Frm, Frm.GetType(), Guid.NewGuid().ToString(), xx, true);
    //}


}
