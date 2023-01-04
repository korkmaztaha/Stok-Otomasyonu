<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="StokOtomasyon._default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
    <link href="assets/fonts/font-awesome.min.css" rel="stylesheet" />
    <link href="assets/fonts/300.css" rel="stylesheet" />
    <link href="assets/css/themify-icons.css" rel="stylesheet" />


</head>
<body>
    <form id="form1" runat="server">
        <div class="wrapper">
            <div class="sidebar" data-background-color="white" data-active-color="danger">
                <div class="sidebar-wrapper">
                    <div class="logo">
                        <a href="#" class="simple-text">Stok Otomasyon</a>
                    </div>
                    <asp:HiddenField ID="hdn_KullaniciId" runat="server" />
                    <ul class="nav">
                        <li class="active">
                            <a href="default.aspx">
                                <i class="ti-arrow-right"></i>
                                <p>Kullanıcı Girişi</p>
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
                                        <h4 class="title">Kullanıcı Girişi</h4>
                                    </div>
                                    <div class="content">
                                        <div class="col-md-4"></div>
                                        <div class="row">
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>Kullanıcı Adı</label>
                                                    <asp:TextBox ID="txt_KullniciAdi" CssClass="form-control border-input" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-4"></div>
                                        <div class="row">
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>Şifre</label>
                                                    <asp:TextBox ID="txt_Sifre" CssClass="form-control border-input"  runat="server" TextMode="Password"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="text-center">
                                        <asp:Button ID="btn_Giris" OnClick="btn_Giris_Click" Style="margin-right: 75px" CssClass="btn btn-info btn-fill btn-wd" runat="server" Text="Giriş" />
                                    </div>
                                    <br />
                                    <div class="clearfix"></div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
