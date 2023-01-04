<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="main.aspx.cs" Inherits="StokOtomasyon.main" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .dataTables_wrapper .dt-buttons {
            float: right;
            margin-right: 10px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="content">
        <div class="container-fluid">
            <div class="row">
                <div class="col-lg-12 col-md-11">
                    <div class="card">
                        <div class="header">
                            <h4 class="title">Arama</h4>
                        </div>
                        <div class="content">
                            <div class="row">
                                <div class="col-md-1">
                                    <label>Adet ></label>
                                    <asp:TextBox ID="txt_baAdet" CssClass="form-control border-input" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-md-1">
                                    <div class="form-group">
                                        <label for="">< Adet</label>
                                        <asp:TextBox ID="txt_biAdet" CssClass="form-control border-input " runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Ürün Adı</label>
                                        <asp:TextBox ID="txt_UrunAdi" CssClass="form-control border-input " runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Marka</label>
                                        <asp:TextBox ID="txt_Marka" CssClass="form-control border-input" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Grubu</label>
                                        <asp:TextBox ID="txt_Grubu" CssClass="form-control border-input" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Fiyat ></label>
                                        <asp:TextBox ID="txt_baslangicFiyat" CssClass="form-control border-input currency" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>< Fiyat</label>
                                        <asp:TextBox ID="txt_bitisFiyat" CssClass="form-control border-input currency" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-5">
                                        <div class="form-group">
                                            <div class="col-md-1">
                                                <label>KDV</label>
                                                <asp:Label Text="" ID="oran" CssClass="hidden" runat="server" />
                                            </div>
                                            <div class="col-md-1">
                                                <asp:RadioButton Text="%1" GroupName="kdv" ID="kdv_1" runat="server" />
                                            </div>
                                            <div class="col-md-1">
                                                <asp:RadioButton GroupName="kdv" Text="%8" ID="kdv_8" runat="server" />
                                            </div>
                                            <div class="col-md-1">
                                                <asp:RadioButton Text="%18" GroupName="kdv" ID="kdv_18" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="text-center">
                                <asp:Button ID="btn_Ara" CssClass="btn btn-info btn-fill btn-wd" OnClick="btn_Ara_Click" runat="server" Text="Ara" />
                                <asp:Button ID="btn_Excel" CssClass="btn btn-primary btn-fill btn-wd hidden" OnClick="btn_Excel_Click" runat="server" Text="Excel'e Çıkar" />
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
                                <h4 class="title">Ürünler</h4>
                            </div>
                            <table id="example" class="table table-striped example" style="width: 100%">
                                <thead>
                                    <th>Grubu</th>
                                    <th>Markası</th>
                                    <th>Adı</th>
                                    <th>Fiyatı</th>
                                    <th>KDV</th>
                                    <th>Stok Miktarı</th>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="rptTbl" OnItemCommand="rptTbl_ItemCommand" runat="server">
                                        <ItemTemplate>
                                            <tr>
                                                <td><%#Eval("UrunGrubu")%></td>
                                                <td><%#Eval("UrunMarka")%></td>
                                                <td><%#Eval("UrunAdi")%></td>
                                                <td><%#Eval("UrunSatis","{0:c2}")%></td>
                                                <td><%#Eval("UrunKdv")%></td>
                                                <td><%#Eval("UrunStoktakiMiktar")%></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Footer" runat="server">
    <script src="assets/js/dataTables.buttons.min.js"></script>
    <script src="assets/js/buttons.bootstrap.min.js"></script>
    <script src="assets/js/jszip.min.js"></script>
    <script src="assets/js/pdfmake.min.js"></script>
    <script src="assets/js/vfs_fonts.js"></script>
    <script src="assets/js/buttons.html5.min.js"></script>
    <script src="assets/js/buttons.print.min.js"></script>
    <script src="assets/js/buttons.colVis.min.js"></script>
   <%-- <script src="https://cdn.datatables.net/buttons/1.5.2/js/dataTables.buttons.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.5.2/js/buttons.bootstrap.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jszip/3.1.3/jszip.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.36/pdfmake.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.36/vfs_fonts.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.5.2/js/buttons.html5.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.5.2/js/buttons.print.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.5.2/js/buttons.colVis.min.js"></script>--%>

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
            var table = $('#example').DataTable({
                lengthChange: false,
                dom: 'Bfrtip',
                buttons: [
                    {
                        extend: 'copyHtml5',
                        text: 'Koplaya',
                        className: 'btn btn-primary  btn-wd  '
                    },
                    {
                        extend: 'excelHtml5',
                        text: 'Excele Çıkar',
                        className: 'btn btn-primary  btn-wdd  '
                    },
                    {
                        extend: 'pdfHtml5',
                        text: 'PDF e çıkar',
                        className: 'btn btn-primary btn-wd '

                    }


                ],
                "columns": [
                    { "orderable": false },
                    null,
                    null,
                    null,
                    null,
                    null
                ],
                "language": {
                    "lengthMenu": "_MENU_ sayfa başına kayıt",
                    "zeroRecords": "Kayıt Bulunamadı",
                    "info": "Gösterilen Sayfa _PAGE_-_PAGES_",
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


            table.buttons().container()
                .appendTo('#example_wrapper .col-sm-6:eq(0)');
        });


        $(function () {
            $('.currency').maskMoney();
        })
    </script>
</asp:Content>

