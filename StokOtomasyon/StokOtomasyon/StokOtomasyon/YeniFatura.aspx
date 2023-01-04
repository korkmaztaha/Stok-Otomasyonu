<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="YeniFatura.aspx.cs" Inherits="StokOtomasyon.YeniFatura" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--<link rel="stylesheet" href="/resources/demos/style.css">--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container-fluid">
        <div class="alert alert-danger" id="ErrorMessage" runat="server" visible="false">

            <span><b>Hata - </b>
                <asp:Label ID="lblErrorMessage" runat="server"></asp:Label>
                <br />  
                <asp:Label ID="lbltext" runat="server"></asp:Label>
                <br />
                <asp:Label ID="lblMiktar" runat="server"></asp:Label>
            </span>
        </div>

        <div class="row">
            <div class="col-lg-12 col-md-11">
                <div class="card">
                    <div class="header">
                        <h4 class="title">Yeni Fatura Girşi</h4>
                        <asp:HiddenField ID="hdnFirmaId" runat="server" />
                        <asp:HiddenField ID="hdn_KullaniciId" runat="server" />
                        <asp:HiddenField ID="hdn_FaturaTipi" runat="server" />
                        <asp:HiddenField ID="hdn_FaturaId" runat="server" />
                        <asp:HiddenField ID="hdn_HareketId" runat="server" />
                        <asp:HiddenField ID="hdn_SatisId" runat="server" />
                    </div>
                    <div class="content">
                        <div class="row">
                            <div class="col-md-5">
                                <label>Şirket</label>
                                <asp:DropDownList ID="ddl_Sirketİsmi" AutoPostBack="true" OnSelectedIndexChanged="ddl_Sirketİsmi_SelectedIndexChanged" DataTextField="FirmaAdi" DataValueField="FirmaId" CssClass="form-control border-input select2" runat="server">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-5">
                                <div class="form-group">
                                    <label for="exampleInputEmail1">Vergi Dairesi</label>
                                    <asp:Label ID="txt_Vergi" CssClass="form-control border-input" runat="server"></asp:Label>
                                </div>
                            </div>
                            <asp:HiddenField ID="hdn_dbAdet" runat="server" />
                            <asp:HiddenField ID="hdn_yDbAdet" runat="server" />
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Tarih</label>
                                    <asp:TextBox ID="txt_Tarih" CssClass="form-control border-input datepicker" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-10">
                                <div class="form-group">
                                    <label>Adres</label>
                                    <asp:Label ID="txt_Adres" CssClass="form-control border-input" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Fatura Numarası</label>
                                    <asp:TextBox ID="txt_Fatura" onblur="harfleriBuyut(this);" CssClass="form-control border-input" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="text-center">
                            <asp:Button ID="btn_Kayit" CssClass="btn btn-info btn-fill btn-wd" OnClick="btn_Kayit_Click" runat="server" Text="Kaydet" />
                            <asp:Button Text="Yazdır" CssClass="btn btn-info btn-fill btn-primary" OnClick="btn_Yazdir_Click" ID="btn_Yazdir" runat="server" />
                            <asp:Button Text="Sil" CssClass="btn btn-info btn-fill btn-danger" OnClick="btnSil_Click" ID="btnSil" runat="server" />
                        </div>
                        <div class="clearfix"></div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-12 col-md-11">
                <div class="card">
                    <div class="header" id="header" runat="server">
                        <h4 class="title">Ürün Ekle</h4>
                    </div>
                    <div class="content">
                        <div class="form-group" id="form" runat="server">
                            <div class="row">
                                <div class="col-md-2">
                                    <div class="form-group ">
                                        <label>Ürün Adı</label>
                                        <asp:DropDownList ID="ddlUrun" DataTextField="UrunAdi" DataValueField="UrunId" CssClass="form-control border-input select2" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Birim Fiyatı</label>
                                        <asp:TextBox ID="txt_BirimFiyatı" CssClass="form-control border-input currency" runat="server"></asp:TextBox>
                                    </div>
                                </div>                    
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Adet</label>
                                        <asp:TextBox ID="txt_Adet" CssClass="form-control border-input" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>İskonto</label>
                                        <asp:TextBox ID="txt_Iskonto" CssClass="form-control border-input" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                           
                                <div class="col-md-2 hidden">
                                    <div class="form-group">
                                        <label>KDV</label>
                                        <asp:Label Text="" ID="oran" CssClass="hidden" runat="server" />
                                        <asp:RadioButton Text="%8" GroupName="kdv" CssClass="form-control border-input" ID="kdv_8" runat="server" />
                                        <asp:RadioButton Text="%18" GroupName="kdv"  CssClass="form-control border-input" ID="kdv_18" runat="server" />
                                        <asp:RadioButton Text="%1" GroupName="kdv" CssClass="form-control border-input" ID="kdv_1" runat="server" />
                                    </div>
                                </div>
                                <div class="col-md-2 hidden">
                                    <div class="form-group ">
                                        <label>Toplam Fiyat</label>
                                        <asp:Label ID="txt_Toplam" CssClass="form-control border-input " Style="width: 100%" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <br />
                                <div class="col-md-2">
                                    <div class="btn-group">
                                        <asp:Button Text="Ekle" CssClass="btn btn-info btn-fill btn-wd myModal" data-toggle="modal" data-target="#myModal" OnClick="btn_UrunEkle_Click" ID="btn_UrunEkle" runat="server" />
                                    </div>
                                </div>
                                <br />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="row">
                                    <div class="col-md-12 mt-2">
                                        <table id="example" class="table table-striped table-bordered" style="width: 100%">
                                            <thead>
                                                <tr>
                                                    <th class="auto-style1">Ürün</th>
                                                    <th class="auto-style1">Birim Fiyatı</th>
                                                    <th class="auto-style1">Adet</th>
                                                    <th class="auto-style1">KDV</th>
                                                    <th class="auto-style1">Ara Toplam</th>
                                                    <th class="auto-style1">İskonto Oranı %</th>
                                                    <th class="auto-style1">İskonto Miktarı</th>
                                                    <th class="auto-style1">Genel Toplam</th>
                                                    <div>
                                                        <th class="auto-style1" id="sil" runat="server">Sil</th>
                                                    </div>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="rptTbl" runat="server" OnItemCommand="rptTbl_ItemCommand">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td><%#Eval("Urun")%></td>
                                                            <td><%#Eval("BirimFiyatı","{0:c2}")%></td>
                                                            <td><%#Eval("Adet")%></td>
                                                            <td><%#Eval("KDV")%></td>
                                                            <td><%#Eval("UrunToplamı","{0:c2}")%></td>
                                                            <td><%#Eval("IskontoOrani")%></td>
                                                            <td><%#Eval("IskontoMiktari","{0:c2}")%></td>
                                                            <td><%#Eval("IskontoluFiyat","{0:c2}")%></td>
                                                            <td>
                                                                <asp:LinkButton Text="sil" ID="linkSil" CommandArgument='<%#Eval("SatisId")%>' CssClass="btn btn-danger" CommandName="delete" runat="server"></asp:LinkButton>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tbody>
                                        </table>
                                        <div class="col-md-12">
                                            <div class="row">
                                                <div class="col-md-2">
                                                    <div class="form-group">
                                                        <label>Ürün Toplamı</label>
                                                        <asp:Label ID="lbl_Urun" CssClass="form-control currency" runat="server" />
                                                    </div>
                                                </div>
                                                  <div class="col-md-2">
                                                    <div class="form-group">
                                                        <label>İskonto Miktarı</label>
                                                        <asp:Label ID="lbl_Iskonto" CssClass="form-control currency" runat="server" />
                                                    </div>
                                                </div>
                                                <div class="col-md-2">
                                                    <div class="form-group">
                                                        <label>Ara Toplam</label>
                                                        <asp:Label ID="lbl_AraToplam" CssClass="form-control currency" runat="server" />
                                                    </div>
                                                </div>
                                                <div class="col-md-1">
                                                    <div class="form-group">
                                                        <label>%1 KDV</label>
                                                        <asp:Label CssClass="form-control currency" ID="lbl_f1" runat="server" />
                                                        <asp:Label ID="tahahahah" runat="server" />
                                                    </div>
                                                </div>
                                                <div class="col-md-1">
                                                    <div class="form-group">
                                                        <label>%8 KDV</label>
                                                        <asp:Label CssClass="form-control currency" ID="lbl_f8" runat="server" />
                                                    </div>
                                                </div>
                                                <div class="col-md-1">
                                                    <div class="form-group">
                                                        <label>%18 KDV</label>
                                                        <asp:Label CssClass="form-control currency" ID="lbl_f18" runat="server" />
                                                    </div>
                                                </div>
                                                <div class="col-md-2">
                                                    <div class="form-group">
                                                        <label>Genel Toplam</label>
                                                        <asp:Label CssClass="form-control currency" ID="lbl_toplam" runat="server" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hdn_Urun" runat="server" />
    <asp:HiddenField ID="hdn_Adet" runat="server" />
    <asp:HiddenField ID="hdn_Birim" runat="server" />
    <asp:HiddenField ID="hdn_Kdv" runat="server" />
    <asp:HiddenField ID="hdn_UrunToplami" runat="server" />
    <asp:HiddenField ID="hdn_QueryStringID" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Footer" runat="server">
    <script src="autoNumeric-1.9.46/autoNumeric-min.js"></script>
    <script>
        $(function () {
            $(".datepicker").datepicker({
                dateFormat: "dd.mm.yy",
                monthNames: ["Ocak", "Şubat", "Mart", "Nisan", "Mayıs", "Haziran", "Temmuz", "Ağustos", "Eylül", "Ekim", "Kasım", "Aralık"],
                dayNamesMin: ["Pa", "Pt", "Sl", "Ça", "Pe", "Cu", "Ct"],
                firstDay: 1,
            });
        });
        //$(function () {
        //    $('.currency').maskMoney();
        //})
        //$(document).ready(function () { $(".select2").select2(); }   );
        $(document).ready(function () {
            $(".select2").select2();
            $('.currency').autoNumeric('init', {
                aSep: '.',
                aDec: ',',
            });
            $('.currency').autoNumeric('update');
            <%--data: '{ UrunId: ' + $("#<%ddlUrun.ClientID%>").select("val"); +'}',--%>
            $("#<%=ddlUrun.ClientID%>").on("change", function () {
                $.ajax({
                    type: "POST",
                    url: "YeniFatura.aspx/urunBilgileri",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false, 
                    data: JSON.stringify({
                        "UrunId": $("#<%=ddlUrun.ClientID%>").select2("val")
                    }),
                    success: function (msg) {
                        var t = $.parseJSON(msg.d);
                        $.each(t.Table, function (i, item) {
                            var birimFiyati = item.UrunSatis.toString();
                           <%-- $("#<%=txt_BirimFiyatı.ClientID%>").val(birimFiyati.replace('.', ','));--%>
                            switch (item.UrunKdv) {
                                case 1:
                                    $("#<%=kdv_1.ClientID%>").prop("checked", "true");
                                case 8:
                                    $("#<%=kdv_8.ClientID%>").prop("checked", "true");
                                    break;
                                case 18:
                                    $("#<%=kdv_18.ClientID%>").prop("checked", "true");
                                    break;
                                default:
                            }
                            $("#<%=oran.ClientID%>").text(item.UrunKdv);
                            $("#<%=txt_BirimFiyatı.ClientID%>").trigger("focus");
                            window.setTimeout(function () { $("#<%=txt_Adet.ClientID%>").trigger("focus"); }, 200)
                            
                            $('.currency').autoNumeric('update');
                        });
                    },
                    error: function () {
                        Lobibox.notify('error', {
                            msg: 'Ürün bilgileri bulunamdı',
                        });
                    }
                });
                
                 $.ajax({
                    type: "POST",
                    url: "YeniFatura.aspx/fiyat",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false, 
                    data: JSON.stringify({
                        "UrunId": $("#<%=ddlUrun.ClientID%>").select2("val"),
                        "htipi": $("#<%=hdn_FaturaTipi.ClientID%>").val()
                    }),
                    success: function (msg) {
                        var t = $.parseJSON(msg.d);
                        $.each(t.Table, function (i, item) {
                            $("#<%=txt_BirimFiyatı.ClientID%>").val(item.fiyat);
                                                    
                        });
                    },
                    error: function () {
                        Lobibox.notify('error', {
                            msg: 'Ürün bilgileri bulunamdı',
                        });
                    }
                });
            });
        });

    </script>
</asp:Content>

