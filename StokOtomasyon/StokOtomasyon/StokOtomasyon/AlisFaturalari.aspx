<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="AlisFaturalari.aspx.cs" Inherits="StokOtomasyon.AlisFaturalari1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="content">
        <div class="container-fluid">
            <div class="row">
                <div class="col-md-12">
                    <div class="card">
                        <div class="header">
                            <h4 class="title">Fatura Listesi</h4>
                        </div>
                        <div class="content table-responsive table-full-width">
                            <table class="table table-striped example" style="width: 100%">
                                <thead>
                                    <th>Fatura Numarası</th>
                                    <th>Firma</th>
                                    <th>Tarih</th>
                                    <th>Tutar</th>
                                    <th>Görüntüle</th>

                                </thead>
                                <tbody>
                                    <asp:HiddenField ID="hdn_faturaId" runat="server" />
                                    <asp:Repeater ID="rptTbl" OnItemCommand="rptTbl_ItemCommand" runat="server">
                                        <ItemTemplate>
                                            <tr>
                                                <td><%#Eval("FaturaNumara")%></td>
                                                <td><%#Eval("SirketAdi")%></td>
                                                <td><%#Eval("FaturaTarih","{0:d}")%></td>
                                                <td><%#Eval("ToplamFiyat","{0:c2}")%></td>
                                                <td>
                                                    <asp:LinkButton Text="Görüntüle" CommandArgument='<%#Eval("FaturaId")%>' CssClass="btn btn-primary" CommandName="edit" runat="server" /></td>

                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tbody>
                                <asp:HiddenField ID="hdn_KullaniciId" runat="server" />

                            </table>
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
                    null,
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

