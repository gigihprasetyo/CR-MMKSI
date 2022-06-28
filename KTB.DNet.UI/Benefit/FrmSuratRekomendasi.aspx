<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmSuratRekomendasi.aspx.vb" Inherits="FrmSuratRekomendasi" SmartNavigation="False" %>

<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>FrmAplikasiHeader</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">


    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <link media="print" href="../WebResources/printstyle.css" type="text/css" rel="stylesheet">
    <base target="_self">
    <script language="javascript">
        function printx() {
            //document.getElementById('divPrint').style.display = 'none';
            var Posisi = document.getElementById('txtPosisi');
            var txtKepalaCabang = document.getElementById('txtKepalaCabang');
            var txtKantorPembiayaan = document.getElementById('txtKantorPembiayaan');
            if (Posisi.value == "") {
                alert("Data harap di isi");
                Posisi.focus();
                return;
            }

            if (txtKantorPembiayaan.value == "") {
                alert("Data harap di isi");
                txtKantorPembiayaan.focus();
                return;
            }

            if (txtKepalaCabang.value == "") {
                alert("Data harap di isi");
                txtKepalaCabang.focus();
                return;
            }

            window.print();
        }

        function jPosisi() {
            var Posisi = document.getElementById('txtPosisi');
            var lblJabatanTTD = document.getElementById('lblJabatanTTD');

            lblJabatanTTD.innerHTML = Posisi.value.toUpperCase();;
        }

        function jNama() {
            var txtKepalaCabang = document.getElementById('txtKepalaCabang');
            var lblNamaTTD = document.getElementById('lblNamaTTD');

            lblNamaTTD.innerHTML = txtKepalaCabang.value.toUpperCase();;
        }

        function jPembiayaan() {
            var txtKantorPembiayaan = document.getElementById('txtKantorPembiayaan');
            var lblKantorPembiayaan = document.getElementById('lblKantorPembiayaan');

            lblKantorPembiayaan.innerHTML = txtKantorPembiayaan.value;
        }
    </script>
    <style type="text/css">
        td, input, select, textarea, caption, label {
            font-family: Sans-Serif, Arial;
            font-size: 16px;
            color: #000000;
            margin: 0px;
            height: auto;
        }


        <!--

        @media print {
            td, input, select, textarea, caption, label {
                font-family: Sans-Serif, Arial;
                font-size: 16px;
                color: #000000;
                margin: 0px;
                height: auto;
            }
        }

        @media screen {
            ...
        }

        -->
        .auto-style1 {
            width: 29%;
        }
    </style>

</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">

        <table id="Table2" cellspacing="5" cellpadding="0" width="100%" border="0">
            <tr class="hideTrOnPrint">
                <td class="auto-style1">
                    <table>
                        <tr>
                            <td><asp:Label runat="server" Text ="Nama"></asp:Label></td>      
                            <td><asp:Label runat="server" Text =":"></asp:Label></td>
                            <td><input id="txtKepalaCabang" style="align: center" type="text" runat="server" placeholder="Nama" onchange="jNama();" /></td>      
                        </tr>

                        <tr>
                            <td><asp:Label runat="server" Text ="Posisi"></asp:Label></td>      
                            <td><asp:Label runat="server" Text =":"></asp:Label></td>
                            <td><input id="txtPosisi" style="align: center" type="text" runat="server" placeholder="Posisi" onchange="jPosisi();" value="Kepala Cabang" /></td>      
                        </tr>
                        <tr>
                            <td><asp:Label runat="server" Text ="Kantor Cabang Pembiayaan"></asp:Label></td>      
                            <td><asp:Label runat="server" Text =":"></asp:Label></td>
                            <td><input id="txtKantorPembiayaan" style="align: center" type="text" runat="server" placeholder="Posisi" onchange="jPembiayaan();" value="Kantor Cabang" /></td>      
                        </tr>

                         <%-- Farid Additional --%>
                    
                        <tr>
                            <td colspan="3" valign="middle" align="center" >
                                <asp:Button class="hideButtonOnPrint" ID="btnSimpan" runat="server" Text="Simpan" Width="60px" Visible="false"></asp:Button>&nbsp;
                                <input class="hideButtonOnPrint" type="button" value="Print" onclick="printx()" runat="server" />
                            </td>
                        </tr>

                        <%-- Farid Additional --%>
                    </table>
                </td>
            </tr>
            

                   
                    
                </td>
                <td></td>
            </tr>
            <tr>
                <td class="titlePage" style="font-weight: bold;" colspan="2" align="center">SURAT REKOMENDASI</td>
            </tr>
            <tr>
                <td class="titlePage" style="font-weight: bold;" colspan="2" align="center">
                    <asp:Label ID="lblNoRegRecom" runat="server" Text="" Style="font-weight: bold;"></asp:Label></td>
            </tr>

            <tr>
                <td background="../images/bg_hor.gif" colspan="2" height="1">
                    <img height="1" src="../images/bg_hor.gif" border="0"></td>
            </tr>
            <tr>
                <td style="height: 6px" colspan="2" height="6">
                    <img height="1" src="../images/dot.gif" border="0"></td>
            </tr>

            <tr>
                <td colspan="2">


                    <table style="width: 793px; max-height: 1123px;" cellspacing="0" cellpadding="0">
                        <tr valign="top" style="height: 150px;">
                            <td style="width: 30%">
                                <img alt="Embedded Image" src="data:image/png;base64,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" />
                            </td>
                            <td align="right">
                                
                            </td>
                        </tr>
                        <tr valign="top" style="height: 40px;">
                            <td>&nbsp;
                              
                            </td>
                            <td align="right">
                                <asp:Label ID="LblKota1" runat="server" Text=""></asp:Label>
                                ,
                                     <asp:Label ID="lblTanggal" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>

                        <tr valign="top" style="height: 100px;">
                            <td>
                                <p>Kepada Yth</p>

                                <p>
                                    <asp:Label ID="lblLeasing" runat="server" Text=""></asp:Label>
                                </p>
                                <div id="DCabang" runat="server" visible="false">
                                    <p>
                                        Cabang
                                    <asp:Label ID="lblKota2" runat="server" Text=""></asp:Label>
                                    </p>
                                </div>
                            </td>
                            <td></td>
                        </tr>

                        <tr valign="top" style="height: 30px;">
                            <td colspan="2">
                                <p>Dengan Hormat,</p>

                                <p>
                                    Dengan Ini kami
                                    <asp:Label ID="lblNamaDealer" runat="server" Text=""></asp:Label>
                                    &nbsp;
                                    selaku Dealer resmi Mitsubishi 
                                    merekomendasikan pembelian kendaraan Mitsubishi 
                                    <asp:Label ID="lblModel" runat="server" Text="" Visible="false"></asp:Label>
                                    &nbsp;untuk mendapatkan paket
                                    leasing dengan data sebagai berikut :
                                </p>
                                <p>
                                    <table style="width: 70%; margin-left: 20px;">
                                        <%--<tr>
                                            <td>Nama Pembeli</td>
                                            <td>:</td>
                                            <td>
                                                <asp:Label ID="lblPembeli" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Nama di Faktur</td>
                                            <td>:</td>
                                            <td>
                                                <asp:Label ID="lblNamaFaktur" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>--%>
                                        <tr>
                                            <td>Tipe Kendaraan</td>
                                            <td>:</td>
                                            <td>
                                                <asp:Label ID="lblTipe" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>No Chassis</td>
                                            <td>:</td>
                                            <td>
                                                <asp:Label ID="lblNoChassis" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>No Engine</td>
                                            <td>:</td>
                                            <td>
                                                <asp:Label ID="lblNoEngine" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Kantor Cabang Pembiayaan</td>
                                            <td>:</td>
                                            <td>
                                                <asp:Label ID="lblKantorPembiayaan" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </p>
                                <p>
                                    Data kendaraan di atas adalah benar alokasi dari PT.MMKSI untuk Dealer kami dan sesuai
                                    dengan ketentuan untuk kendaraan yang dapat diikutsertakan dalam program paket leasing ini.
                                </p>
                                <p>
                                    Dengan demikian rekomendasi ini kami berikan untuk diproses lebih lanjut, atas perhatian dan
                                    kerjasamanya kami ucapkan terimakasih.
                                </p>
                            </td>
                        </tr>

                        <tr valign="top" style="">
                            <td align="center">
                                <br />
                                <br />
                                Hormat Kami
                                    <br />
                                <br />
                                <br />
                                <br />
                                <br />

                                <label id="lblNamaTTD"></label>
                                <br />
                                <label id="lblJabatanTTD">KEPALA CABANG</label>

                            </td>
                            <td></td>

                        </tr>

                    </table>


                </td>
            </tr>






        </table>
    </form>
</body>
</html>
