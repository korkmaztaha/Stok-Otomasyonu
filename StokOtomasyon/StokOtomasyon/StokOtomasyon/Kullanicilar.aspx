<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Kullanicilar.aspx.cs" Inherits="StokOtomasyon.KullaniciEkle" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <meta charset="utf-8" />
    <meta charset="utf-8" />
    <link rel="apple-touch-icon" sizes="76x76" href="assets/img/apple-icon.png" />
    <link rel="icon" type="image/png" sizes="96x96" href="assets/img/favicon.png" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />

    <title>Stok otomasyon</title>

    <meta content='width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0' name='viewport' />
    <meta name="viewport" content="width=device-width" />
    <!-- Bootstrap core CSS     -->
    <link href="assets/css/bootstrap.min.css" rel="stylesheet" />
    <!-- Animation library for notifications   -->
    <link href="assets/css/animate.min.css" rel="stylesheet" />
    <!--  Paper Dashboard core CSS    -->
    <link href="assets/css/paper-dashboard.css" rel="stylesheet" />
    <!--  CSS for Demo Purpose, don't include it in your project     -->
    <link href="assets/css/demo.css" rel="stylesheet" />
    <!--  Fonts and icons     -->
 
    <link href="assets/fonts/300.css" rel="stylesheet" />
    <link href="assets/css/themify-icons.css" rel="stylesheet" />
    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css"/>
       <link href="assets/fonts/font-awesome.min.css" rel="stylesheet" />

 <%--  <link href="assets/Plugins/bootstrap-alt-table/bootstrap-alt-table.css" rel="stylesheet" />
    <link href="assets/Plugins/datatables/css/dataTables.bootstrap.min.css" rel="stylesheet" />
    <link href="assets/Plugins/datatables/extensions/Responsive/css/responsive.bootstrap.min.css" rel="stylesheet" />
    <script src="assets/Plugins/datatables/js/jquery.dataTables.min.js"></script>
    <script src="assets/Plugins/datatables/js/dataTables.bootstrap.min.js"></script>
    <script src="assets/Plugins/datatables/extensions/Responsive/js/dataTables.responsive.min.js"></script>
    <script src="assets/Plugins/datatables/extensions/Responsive/js/responsive.bootstrap.min.js"></script>--%>
    <link href="assets/css/dataTablesTaha.css" rel="stylesheet" />
    <link href="assets/fonts/font-awesome.min.css" rel="stylesheet" />
 <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/material-design-iconic-font/2.2.0/css/material-design-iconic-font.min.css"/>
  
     <link href="assets/select2-3.5.3/select2-3.5.3/select2.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="wrapper">
            <div class="sidebar" data-background-color="white" data-active-color="danger">
                <div class="sidebar-wrapper">
                    <div class="logo">
                        <a href="http://www.creative-tim.com" class="simple-text">Stok Otomasyon</a>
                    </div>
                    <ul class="nav">
                        <li>
                            <a href="default.aspx">
                                <i class="ti-arrow-right"></i>
                                <p>Kullanıcı Girişi</p>
                            </a>
                        </li>
                        <li class="active">
                            <a href="Kullanicilar.aspx">
                                <i class="ti-user"></i>
                                <p>Kullanıcılar</p>
                            </a>
                        </li>
                    </ul>
                </div>
            </div>
            <div class="main-panel">
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
                                                    <asp:TextBox ID="txt_Sifre" TextMode="Password" CssClass="form-control border-input" onclick="myFunction()" runat="server"></asp:TextBox>
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
                                                    <asp:TextBox ID="txt_Telefon" CssClass="form-control border-input" runat="server"></asp:TextBox>
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
                                            <h4 class="title">Kullanıcı Listesi</h4>
                                        </div>
                                        <div class="content table-responsive table-full-width">
                                             <table class="table table-striped example" style="width: 100%">
                                                <thead>
                                                    <th>Kullanıcı Adı</th>
                                                    <th>Ad</th>
                                                    <th>Soyad</th>
                                                    <th>Telefon</th>
                                                    <th>Mail</th>
                                                    <th>Adres</th>
                                                </thead>
                                                <tbody>
                                                    <asp:HiddenField ID="hdn_KullaniciId" runat="server" />
                                                    <asp:Repeater ID="rpt_Kullanicilar" OnItemCommand="rpt_Kullanicilar_ItemCommand" runat="server">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td><%#Eval("KullaniciAdi")%></td>
                                                                <td><%#Eval("Ad")%></td>
                                                                <td><%#Eval("Soyad")%></td>
                                                                <td><%#Eval("Telefon")%></td>
                                                                <td><%#Eval("Mail")%></td>
                                                                <td><%#Eval("Adres")%></td>
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


                </div>
            </div>
        </div>
    </form>
</body>
  
    <script src="assets/js/3.1.1.jquery.min.js"></script>
    <script src="assets/js/jquery-1.10.2.js" type="text/javascript"></script>
    <script src="assets/jquery-ui.js"></script>
    <script src="assets/js/bootstrap.min.js" type="text/javascript"></script>
    <script src="assets/js/dataTables.min.js"></script>
    <script src="assets/js/dataTables.bootstrap.min.js"></script>
    <script src="assets/js/jquery.inputmask.bundle.js"></script>
    <script src="assets/jquery-maskmoney-cdbeeac/dist/jquery.maskMoney.min.js"></script>
    <script>
        $(":input").inputmask();

        $("#txt_Telefon").inputmask({ "mask": "(999) 999-9999" });

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
</html>
