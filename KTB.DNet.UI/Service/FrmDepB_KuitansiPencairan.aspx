<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmDepB_KuitansiPencairan.aspx.vb" Inherits=".FrmDepB_KuitansiPencairan" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>Kuitansi Pencairan Deposit B</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link rel="stylesheet" type="text/css" href="../WebResources/stylesheet.css">
    <link rel="stylesheet" type="text/css" href="../WebResources/printstyle.css" media="print">
    <script language="javascript" type="text/javascript" src="../WebResources/InputValidation.js"></script>
    <script language="javascript">
        function ShowPPDealerSelection() {
            showPopUp('../PopUp/PopUpDealerSelectionOne.aspx', '', 500, 760, DealerSelection);
        }

        function DealerSelection(selectedDealer) {
            var tempParam = selectedDealer.split(';');
            var txtDealerSelection = document.getElementById("txtKodeDealer");
            var lblDealerName = document.getElementById("lblNamaDealer");
            txtDealerSelection.value = tempParam[0];
            lblDealerName.innerHTML = tempParam[1] + " - " + tempParam[3];
            //return false;
            var btnImgDealer = document.getElementById("btnImgDealer");
            btnImgDealer.click();
        }

        function PrintDocument() {
            // var divGrid = document.getElementById("divGrid");
            //divGrid.style.overflow='visible';
            window.print();
            //if(navigator.appName == "Microsoft Internet Explorer")
            //{
            //	divGrid.style.overflow='auto';
            //}				
        }
    </script>
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <table id="Table1" border="0" cellspacing="0" cellpadding="0" width="100%">
            <tr>
                <td class="titlePage">Service - Deposit B- Pembuatan Kuitansi Pencairan</td>
            </tr>
            <tr>
                <td height="1" background="../images/bg_hor.gif">
                    <img border="0" src="../images/bg_hor.gif" height="1"></td>
            </tr>
            <tr>
                <td style="height: 6px" height="6">
                    <img border="0" src="../images/dot.gif" height="1"></td>
            </tr>
            <tr>
                <td>
                    <table id="Table2" border="0" cellspacing="1" cellpadding="2" width="100%">
                        <tr>
                            <td style="width: 22%; height: 16px" class="titleField">
                                <asp:Label ID="lblCode" runat="server">Kode Dealer</asp:Label></td>
                            <td style="width: 1%; height: 16px">:</td>
                            <td style="height: 16px">
                                <asp:TextBox ID="txtKodeDealer" runat="server" onblur="omitSomeCharacter('txtKodeDealer','<>?*%$')"></asp:TextBox>
                                <img id="imgDealerS" src="../images/popup.gif" alt="" style="cursor: hand" onclick="ShowPPDealerSelection();" >
                                <asp:ImageButton Style="display: none" ID="imgDealer" runat="server" ImageUrl="../images/popup.gif"></asp:ImageButton>
                                <asp:Button ID="btnImgDealer" runat="server" Text="" Style="display: none"></asp:Button>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 22%" class="titleField">
                                <asp:Label ID="Label1" runat="server">Nama Dealer</asp:Label></td>
                            <td style="width: 1%">:</td>
                            <td>
                                <asp:Label ID="lblNamaDealer" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="width: 22%; height: 11px" class="titleField">Tipe Pengajuan</td>
                            <td style="width: 1%; height: 11px">:</td>
                            <td style="height: 11px" class="titleField">
                                <asp:DropDownList ID="ddlTipePengajuan" runat="server" AutoPostBack="True" Width="136px"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td style="width: 22%; height: 3px" class="titleField">
                                <asp:Label ID="Label2" runat="server" Width="160px"> Nomor Reg. Pengajuan</asp:Label></td>
                            <td style="width: 1%; height: 3px">:</td>
                            <td style="height: 3px" class="titleField">
                                <asp:DropDownList ID="ddlNoRegPengajuan" runat="server" AutoPostBack="True" Width="136px"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 22%; height: 3px" class="titleField">Produk</td>
                            <td style="width: 1%; height: 3px">:</td>
                            <td style="height: 3px">
                                <asp:Label ID="lblProduk" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="width: 22%; height: 3px" class="titleField">Nomor Ref. Surat Pengajuan</td>
                            <td style="width: 1%; height: 3px">:</td>
                            <td style="height: 3px">
                                <asp:Label ID="lblNoRefSuratPengajuan" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="width: 22%; height: 3px" class="titleField">Nomor Rekening</td>
                            <td style="width: 1%; height: 3px">:</td>
                            <td style="height: 3px">
                                <asp:Label ID="lblNoRekening" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="width: 22%" class="titleField">
                                <asp:Label ID="Label3" runat="server" Width="112px">Tanggal Pengajuan</asp:Label></td>
                            <td style="width: 1%">:</td>
                            <td>
                                <asp:Label ID="lblTglPengajuan" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="width: 22%" class="titleField">
                                <asp:Label ID="Label5" runat="server" Width="88px">Nomor Kuitansi</asp:Label></td>
                            <td style="width: 1%">:</td>
                            <td>
                                <asp:TextBox ID="txtNomorKuitansi" runat="server" MaxLength="40"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td style="width: 22%" class="titleField">
                                <asp:Label ID="Label7" runat="server" Width="96px">Tanggal Kwitansi</asp:Label></td>
                            <td style="width: 1%">:</td>
                            <td>
                                <asp:Label ID="lblTanggalKwitansi" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="width: 22%" class="titleField">
                                <asp:Label ID="Label9" runat="server" Width="96px">Telah Terima dari</asp:Label></td>
                            <td style="width: 1%">:</td>
                            <td>
                                <asp:Label ID="lblTelahTerimaDari" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="width: 22%" class="titleField">
                                <asp:Label ID="Label6" runat="server" Width="96px">Uang Sejumlah</asp:Label></td>
                            <td style="width: 1%">:</td>
                            <td>
                                <asp:Label ID="lblUangSejumlah" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="width: 22%" class="titleField">
                                <asp:Label ID="Label12" runat="server" Width="112px">Untuk Pembayaran</asp:Label></td>
                            <td style="width: 1%">:</td>
                            <td>
                                <asp:Label ID="lblUangPembayaran" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td colspan="3"></td>
                        </tr>

                        <tr>
                            <td class="titleField" colspan="3">
                                <br>
                                <br>
                                <br>
                                <asp:Label ID="lblFooter" runat="server"></asp:Label><br>
                                <br>
                                <br>
                                <br>
                                <br>
                                <br>
                                <br>
                                <br>
                                <asp:TextBox ID="txtSign" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="titleField" colspan="3">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
									<asp:Label ID="Label4" runat="server" Width="80px">( Nama Jelas )</asp:Label></td>
                        </tr>
                        <tr>
                            <td class="titleField" colspan="3">
                                <asp:TextBox ID="txtJabatan" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" colspan="3">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
									<asp:Label ID="Label8" runat="server" Width="80px">( Jabatan )</asp:Label></td>
                        </tr>
                        <tr>
                            <td class="titleField" colspan="3">*) Nama yang bertanda tangan harus sesuai dengan 
									nama yang berhak atau diberi kuasa menandatangani kuitansi.</td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Button ID="btnSimpan" class="hideButtonOnPrint" runat="server" Width="72px" Text="Simpan"></asp:Button></td>
                                        <td>
                                            <asp:Button ID="btnCetak" class="hideButtonOnPrint" runat="server" Width="72px" Text="Cetak" Visible="False"></asp:Button>
                                        </td>
                                        <td>
                                            <asp:Button ID="btnNew" class="hideButtonOnPrint" runat="server" Width="72px" Text="Baru"></asp:Button></td>
                                        <td>
                                            <asp:Button ID="btnKembali" class="hideButtonOnPrint" runat="server" Width="72px" Text="Kembali"
                                                Visible="False"></asp:Button></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
