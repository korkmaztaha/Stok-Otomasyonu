<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="KullaniciBilgileri.aspx.cs" Inherits="StokOtomasyon.KullaniciBilgileri" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="content">
        <div class="container-fluid">
            <div class="row">
                <div class="col-lg-12 col-md-11">
                    <div class="card">
                        <div class="header">
                            <h4 class="title">Kullanıcı Bilgileri</h4>
                        </div>
                        <div class="content">
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Kullanıcı  Adı</label>
                                        <asp:TextBox ID="txt_KullaniciAdi" CssClass="form-control border-input" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Şifre</label>
                                        <asp:TextBox ID="txt_Sifre"  TextMode="Password" CssClass="form-control border-input" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label for="exampleInputEmail1">Email adres</label>
                                        <asp:TextBox ID="txt_Mail" CssClass="form-control border-input" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Adı</label>
                                        <asp:TextBox ID="txt_Adi" onblur="harfleriBuyut(this);" CssClass="form-control border-input" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Soyadı</label>
                                        <asp:TextBox ID="txt_Soyad" onblur="harfleriBuyut(this);" CssClass="form-control border-input" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-8">
                                    <div class="form-group">
                                        <label>Adres</label>
                                        <asp:TextBox ID="txt_Adres" CssClass="form-control border-input" onblur="harfleriBuyut(this);" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Telefon</label>
                                        <asp:TextBox ID="txt_Telefon" CssClass="form-control border-input mask" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="text-center">
                                <asp:Button ID="btn_Guncelle" CssClass="btn btn-info btn-fill btn-wd" OnClick="btn_Guncelle_Click" runat="server" Text="Kaydet" />
                            </div>
                             <asp:RadioButton Text="Yeni Kullanıcı Açma Yetkisi" GroupName="kdv" CssClass="form-control hidden" ID="rdb_Yetki" runat="server" />
                            <div class="clearfix"></div>
                        </div>
                        <asp:HiddenField ID="hdn_KullaniciId" runat="server" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Footer" runat="server">
    <script>
        $(":input").inputmask();

        $(".mask").inputmask({ "mask": "(999) 999-9999" });

        harfleriBuyut = function (obj) {
            var deger = obj.value;
            var yeniDeger = '';
            deger = deger.split(' ');
            for (var i = 0; i < deger.length; i++) {
                yeniDeger += deger[i].substring(0, 1).toUpperCase() + deger[i].substring(1, deger[i].length) + ' ';
                obj.value = yeniDeger;
            }
        }
    </script>


</asp:Content>
