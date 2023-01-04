<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Tedarikciler.aspx.cs" Inherits="StokOtomasyon.Tedarikciler" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        <div class="row">
            <div class="col-lg-12 col-md-11">
                <div class="card">
                    <asp:HiddenField ID="hdn_FaturaId" runat="server" />
                    <div class="header">
                        <h4 class="title">Tedarikci Bilgileri</h4>
                    </div>
                    <div class="content">
                        <div class="row">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Şirket Adı</label>
                                    <asp:TextBox ID="txt_SirketAdi" onblur="harfleriBuyut(this);" CssClass="form-control border-input" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <asp:HiddenField ID="hdnKullaniciId" runat="server" />
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
    <div class="content">
        <div class="container-fluid">
            <div class="row">
                <div class="col-md-12">
                    <div class="card">
                        <div class="header">
                            <h4 class="title">Tedarikci Listesi</h4>
                        </div>
                        <asp:HiddenField ID="hdn_TedarikciId" runat="server" />
                        <div class="content table-responsive table-full-width">
                            <asp:Repeater ID="rptTbl" OnItemCommand="rptTbl_ItemCommand" runat="server">
                                <HeaderTemplate>
                                    <table class="table table-striped example" style="width: 100%">
                                        <thead>
                                            <th>Şirket Adı</th>
                                            <th>Telefon</th>
                                            <th>Email adresi</th>
                                            <th>Güncelle</th>
                                            <th>Sil</th>
                                        </thead>
                                        <tbody>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td><%#Eval("FirmaAdi")%></td>
                                        <td><%#Eval("FirmaTelefon")%></td>
                                        <td><%#Eval("FirmaEposta")%></td>
                                        <td>
                                            <asp:LinkButton Text="Görüntüle" CommandArgument='<%#Eval("FirmaId")%>' CssClass="btn btn-primary" CommandName="edit" runat="server" /></td>
                                        <td>
                                            <asp:LinkButton Text="Sil" CommandArgument='<%#Eval("FirmaId")%>' CssClass="btn btn-danger" CommandName="delete" runat="server" />
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </tbody>
                            </table>
                                </FooterTemplate>
                            </asp:Repeater>
                        </div>
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
            $('.example').DataTable({
                "columns": [
                    null,
                    null,
                    null,
                    { "orderable": false },
                    { "orderable": false },
                ],
                "language": {
                    "lengthMenu": "_MENU_ sayfa başına kayıt",
                    "zeroRecords": "Kayıt Bulunamadı",
                    "info": "Gösterilen Safa _PAGE_-_PAGES_",
                    "infoEmpty": "Kayıt Bulunamadı",
                    "infoFiltered": "(toplam kayıt sayısı _MAX_)",
                    "sSearch": "Ara:",
                    "sFirst": "İlk",
                    "sLast": "Son",
                    "sNext": "En Baş",
                    "sPrevious": "En Son",
                    "oPaginate": {
                        "sFirst": "İlk",
                        "sLast": "Son",
                        "sNext": "En Baş",
                        "sPrevious": "En Son",
                    }
                }
            });

        });
        $(function () {
            $('.currency').maskMoney();
        })
    </script>

</asp:Content>
