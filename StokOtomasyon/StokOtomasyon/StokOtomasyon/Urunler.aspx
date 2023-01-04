<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Urunler.aspx.cs" Inherits="StokOtomasyon.Urunler" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        <asp:HiddenField ID="hdnAdet" runat="server" />
        <asp:HiddenField ID="hdnKullainiciId" runat="server" />
        <div class="row">
            <div class="col-lg-12 col-md-11">
                <div class="card">
                    <div class="header">
                        <h4 class="title">Ürünler</h4>
                    </div>
                    <div class="content">
                        <div class="row">
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Ürün Grubu</label>
                                    <asp:TextBox ID="txt_Grubu" CssClass="form-control border-input" onblur="harfleriBuyut(this);" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label for="exampleInputEmail1">Markası</label>
                                    <asp:TextBox ID="txt_Marka" CssClass="form-control border-input" onblur="harfleriBuyut(this);" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Ürün Adı</label>
                                    <asp:TextBox ID="txt_Urun" CssClass="form-control border-input" onblur="harfleriBuyut(this);" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-4 hidden">
                                <div class="form-group">
                                    <label for="exampleInputEmail1">Satış Fiyatı</label>
                                    <asp:TextBox ID="txt_UrunFiyati" AutoPostBack="true" OnTextChanged="txt_UrunFiyati_TextChanged" CssClass="form-control border-input money" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>KDV</label>
                                    <asp:Label Text="" ID="oran" CssClass="hidden" runat="server" />
                                    <label class="radio-inline">
                                        <asp:RadioButton Text="%1" GroupName="kdv" CssClass="form-control radio-inline" ID="kdv_1" runat="server" />
                                    </label>
                                    <label class="radio-inline">
                                        <asp:RadioButton GroupName="kdv" Text="%8" ID="kdv_8" CssClass="form-control radio-inline" runat="server" /></label>
                                    <label class="radio-inline">
                                        <asp:RadioButton Text="%18" GroupName="kdv" CssClass="form-control radio-inline" ID="kdv_18" runat="server" />
                                    </label>
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
                            <h4 class="title">Ürün Listesi</h4>
                        </div>
                        <asp:HiddenField ID="hdn_UrunId" runat="server" />
                        <div class="content table-responsive table-full-width">
                            <asp:Repeater ID="rptTbl" OnItemCommand="rptTbl_ItemCommand" runat="server">
                                <HeaderTemplate>
                                    <table class="table table-striped example" style="width: 100%">
                                        <thead>
                                            <th>Grubu</th>
                                            <th>Markası</th>
                                            <th>Ürün</th>
                                            <th>KDV</th>
                                            <th>Güncelle</th>
                                            <th>Sil</th>
                                        </thead>
                                        <tbody>
                                </HeaderTemplate>

                                <ItemTemplate>
                                    <tr>
                                        <td><%#Eval("UrunGrubu")%></td>
                                        <td><%#Eval("UrunMarka")%></td>
                                        <td><%#Eval("UrunAdi")%></td>
                                        <td><%#Eval("UrunKdv")%></td>
                                        <td>
                                            <asp:LinkButton Text="Görüntüle" CommandArgument='<%#Eval("UrunId")%>' CssClass="btn btn-primary" CommandName="edit" runat="server" /></td>
                                        <td>
                                            <asp:LinkButton Text="Sil" CommandArgument='<%#Eval("UrunId")%>' CssClass="btn btn-danger" CommandName="delete" runat="server" />
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
    </script>


</asp:Content>
