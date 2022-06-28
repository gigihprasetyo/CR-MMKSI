<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmUploadFaktureEvidance.aspx.vb" Inherits=".FrmUploadFaktureEvidance" %>

<%@ Register Assembly="KTB.DNet.WebCC" Namespace="KTB.DNet.WebCC" TagPrefix="cc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Popup Pegawai</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <base target="_self">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script language="javascript">
        function addEventListeners() {

            try {
                document.getElementById('photoView').addEventListener('click', imageOnClick);
            } catch (e) {

            }

        }

        function imageOnClick(evt) {
            var photoView = document.getElementById('photoView');
            var imageSource = photoView.getAttribute('src');
            var lblAttachment = document.getElementById('lblAttachment').innerHTML;


            if (lblAttachment == null || lblAttachment == '' || lblAttachment == undefined) {

                return false;
            }


            showPopUp('../PopUp/PopUpImage.aspx?url=' + lblAttachment, '', 500, 760, DealerSelection);
            return false;
        }

    </script>
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <table width="100%" cellspacing="1" cellpadding="2">
            <tr>
                <td class="titlePage" style="height: 8px">Upload - Bukti Faktur</td>
            </tr>
            <tr>
                <td class="titlePage" style="height: 8px"></td>
            </tr>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td class="titleField" width="20%">Nomor Upload</td>
                            <td>:</td>
                            <td>
                                <asp:Label ID="lblNomorUpload" runat="server"></asp:Label>
                                <asp:HiddenField ID="HDCriteria" runat="server" />
                                <asp:HiddenField ID="DealerCode" runat="server" />
                                <asp:HiddenField ID="SCDealer" runat="server" />
                                <asp:HiddenField ID="JDoc" runat="server" />
                                <asp:HiddenField ID="NoFaktur" runat="server" />
                                <asp:HiddenField ID="Month" runat="server" />
                                <asp:HiddenField ID="Year" runat="server" />
                                <asp:HiddenField ID="MonthTo" runat="server" />
                                <asp:HiddenField ID="YearTo" runat="server" />
                                <asp:HiddenField ID="PCategory" runat="server" />
                                <asp:HiddenField ID="Download" runat="server" />
                                <asp:HiddenField ID="BillingNo" runat="server" />
                                <asp:HiddenField ID="AccountingNo" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField">
                                <asp:Label ID="lblJenisDokumen" runat="server">Jenis Dokumen</asp:Label></td>
                            <td>:</td>
                            <td>
                                <asp:DropDownList ID="ddlJenisDokumen" runat="server" Width="240px" Enabled ="false"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td class="titleField">Nomor Faktur Pajak</td>
                            <td>:</td>
                            <td style="height: 20px; white-space: nowrap;" width="75%">
                                <table cellspacing="0" cellpadding="0" border="0">
                                    <tr id="dFaktur" runat="server">
                                        <td><asp:TextBox ID="txtNomorFaktur" runat="server" Width="25px" MaxLength="3"></asp:TextBox></td>
                                        <td id="nf1">&nbsp;.&nbsp;</td>
                                        <td><asp:TextBox ID="txtNomorFaktur2" runat="server" Width="25px" MaxLength="3"></asp:TextBox></td>
                                        <td id="nf2">&nbsp;-&nbsp;</td>
                                        <td><asp:TextBox ID="txtNomorFaktur3" runat="server" Width="20px" MaxLength="2"></asp:TextBox></td>
                                        <td id="nf3">&nbsp;.&nbsp;</td>
                                        <td><asp:TextBox ID="txtNomorFaktur4" runat="server" Width="80px" MaxLength="8"></asp:TextBox></td>                                        
                                    </tr>
                                    <tr id="dFaktur2" runat="server">
                                        <td><asp:Label runat="server" ID="lblFaktur" Visible="false"></asp:Label></td>
                                    </tr>
                                </table>                                
                            </td>                            
                        </tr>
                        
                        <tr>
                            <td class="titleField">Deskripsi Pembayaran</td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="txtDeskripsiPembayaran" runat="server" Width="300px"></asp:TextBox>
                                <asp:Label runat="server" ID="lblGroup" Style="display: none"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField">Tanggal Upload</td>
                            <td>:</td>
                            <td>
                                <cc1:IntiCalendar ID="ICTglUpload" runat="server" TextBoxWidth="70" Enabled ="false"></cc1:IntiCalendar>
                            </td>
                            <td></td>
                                <td></td>
                                <td></td>
                        </tr>
                        <tr>
                            <td class="titleField">Tanggal Rencana Transfer</td>
                            <td>:</td>
                            <td>
                                <cc1:IntiCalendar ID="ICTanggalTranferPlanning" runat="server" TextBoxWidth="70"></cc1:IntiCalendar>
                            </td>
                            <td></td>
                                <td></td>
                                <td></td>
                        </tr>
                        <tr>
                            <td class="titleField" >Rekening Transfer</td>
                            <td >:</td>
                            <td >
                                <asp:DropDownList ID="ddlRekeningTransfer" runat="server" Width="250px"></asp:DropDownList>
                            </td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr >
                            <td class="titleField" >Lampiran</td>
                            <td>:</td>
                            <td id="IdUpload" runat="server">
                                <input onkeypress="return false;" id="photoSrc" tabindex="19" type="file" size="29" name="File1" runat="server" accept=".pdf" />
                                <asp:Button ID="btnUpload" runat="server" Text="Upload" CausesValidation="false" Visible="false"></asp:Button>
                            </td>
                            <td id="IdPath" runat="server" visible="false">
                                <asp:Label ID="lblPathFile" runat="server"></asp:Label>
                                <asp:Button ID="btnDownload" runat="server" Text="Download" CausesValidation="false" ></asp:Button>
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td></td>
                            <td></td>
                            <td>
                                <asp:Label ID="lblPathImage" runat="server"></asp:Label>

                            </td>
                        </tr>
                        <tr style="display: none">
                            <td></td>
                            <td></td>
                            <td>
                                <asp:Image ID="photoView" onclick="imageOnClick()" runat="server" Width="300px" Height="150px" CssClass="ShowControl" ImageUrl="../DataFile/PPT/NotFound.png"></asp:Image>

                            </td>
                        </tr>
                        <tr class="ShowControl">
                            <td></td>
                            <td></td>
                            <td>
                                <asp:Label ID="lblError" runat="server" Font-Bold="true" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>

                        <tr>
                            <td colspan="2"></td>
                            <td width="75%">&nbsp; &nbsp;
                                <asp:Button ID="btnSave" runat="server" value="Simpan" Text="Simpan" />
                                <asp:Button ID="btnKembali" runat="server" value="Kembali" Text="Kembali" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>

        </table>
    </form>
</body>
</html>
