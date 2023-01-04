<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="FirmaKaydet.aspx.cs" Inherits="StokOtomasyon.FirmaKaydet" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        <div class="row">
            <div class="col-lg-12 col-md-11">
                <div class="card">
                    <div class="header">
                        <h4 class="title">Firma Bilgileri</h4>
                    </div>
                    <asp:HiddenField ID="hdn_FirmaId" runat="server" />
                    <div class="content">
                        <div class="row">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Şirket Adı</label>
                                    <asp:TextBox ID="txt_SirketAdi" CssClass="form-control border-input" onblur="harfleriBuyut(this);" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Telefon</label>
                                    <asp:TextBox ID="txt_Telefon" CssClass="form-control border-input mask" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label for="exampleInputEmail1">Email adresi</label>
                                    <asp:TextBox ID="txt_Mail" CssClass="form-control border-input" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Vergi Dairesi</label>
                                    <asp:TextBox ID="txt_Vergi" onblur="harfleriBuyut(this);" CssClass="form-control border-input" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-8">
                                <div class="form-group">
                                    <label>Adres</label>
                                    <asp:TextBox ID="txt_Adres" onblur="harfleriBuyut(this);" CssClass="form-control border-input" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="text-center">
                            <asp:Button ID="btn_Kayit" CssClass="btn btn-info btn-fill btn-wd" OnClick="btn_Kayit_Click" runat="server" Text="Kaydet" />
                        </div>
                        <div class="clearfix"></div>
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

          $(document).ready(function () {
            $('.example').DataTable();

        });
    </script>
</asp:Content>
