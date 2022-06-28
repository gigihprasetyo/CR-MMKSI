<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>

<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmTrPaymentInput.aspx.vb" Inherits="FrmTrPaymentInput" SmartNavigation="False" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>REVISI FAKTUR - INPUT PEMBAYARAN</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script language="javascript" src="../WebResources/FormFunctions.js"></script>
    <script language="javascript">
        function DN_NumberChanged(ddl) {
            if (ddl.id.length == 40) {
                var sPre = ddl.id.substring(0, ddl.id.length - 12);
                var btn = document.getElementById(sPre + 'btnDN_NumberF');
                btn.click();
            }
        }

        function ShowInformation() {
            //alert('bisa');
            showPopUp('../PopUp/PopUpPaymentInformation.aspx', '', 400, 600, null);
        }

        function checkTextAreaMaxLength(textBox, e, length) {
            var mLen = textBox["MaxLength"];
            if (null == mLen)
                mLen = length;

            var maxLength = parseInt(mLen);
            if (!checkSpecialKeys(e)) {
                if (textBox.value.length > maxLength - 1) {
                    if (window.event)//IE
                        e.returnValue = false;
                    else//Firefox
                        e.preventDefault();
                }
            }
        }
        function checkSpecialKeys(e) {
            if (e.keyCode != 8 && e.keyCode != 46 && e.keyCode != 37 && e.keyCode != 38 && e.keyCode != 39 && e.keyCode != 40)
                return false;
            else
                return true;
        }

    </script>
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage">
                    <asp:Label ID="lblPageTitle" runat="server"></asp:Label>
                    <asp:HiddenField ID="hdnCategory" runat="server" />
                    <asp:HiddenField ID="hdnPaymentID" runat="server" />

                </td>
            </tr>
            <tr>
                <td background="../images/bg_hor.gif" height="1">
                    <img height="1" src="../images/bg_hor.gif" border="0"></td>
            </tr>
            <tr>
                <td height="10">
                    <img height="1" src="../images/dot.gif" border="0"></td>
            </tr>
        </table>
        <table cellspacing="1" cellpadding="2" width="100%" border="0">
            <tr>
                <td class="titleField">Credit Account</td>
                <td>:</td>
                <td>
                    <asp:Literal ID="ltrDealerCode" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td class="titleField" style="height: 32px">Nomor Registrasi</td>
                <td style="height: 32px">:</td>
                <td style="height: 32px">
                    <asp:Label ID="lblRegNumber" runat="server" Width="280px">[Auto Generated]</asp:Label>
                </td>
            </tr>
            <tr valign="top">
                <td class="titleField">Jenis Pembayaran</td>
                <td>:</td>
                <td>
                    <asp:Label ID="Label1" runat="server" Width="280px">Virtual Account</asp:Label>
                </td>
                <td class="titleField">Tanggal Transfer</td>
                <td>:</td>
                <td>
                    <cc1:IntiCalendar ID="icTglTransfer" runat="server" TextBoxWidth="60"></cc1:IntiCalendar>
                </td>
            </tr>
            <tr id="trUpload" runat="server">
                <td class="titleField" width="200px" height="22">Bukti Transfer</td>
                <td style="height: 22px" width="20px">:</td>
                <td style="height: 22px" nowrap="nowrap" id="tdUpload" runat="server">
                    <input onkeypress="return false;" id="photoSrc" tabindex="19" type="file" size="29" name="File1"
                        runat="server">
                    &nbsp;
                                            <asp:Button ID="btnUpload" runat="server" Text="Upload" CausesValidation="false"></asp:Button>
                    <asp:HiddenField ID="hdnFilePath" runat="server" />
                    &nbsp;
                </td>
            </tr>
            <tr id="trDownload" runat="server">
                <td class="titleField" width="200px" height="22">Bukti Transfer</td>
                <td style="height: 22px" width="20px">:</td>
                <td style="height: 22px" nowrap="nowrap">
                    <asp:LinkButton ID="lbtnDownload" runat="server" Text="Download" CausesValidation="false"></asp:LinkButton>
                    &nbsp;
                                              <asp:LinkButton ID="lbnDelete" runat="server" Text="Hapus" CausesValidation="false"></asp:LinkButton>

                </td>
            </tr>
            <tr>
                <td class="titleField" style="height: 32px">Total Amount</td>
                <td>:</td>
                <td>
                    <asp:Label ID="lblTotalAmount" runat="server" Text="0" Width="280px" Height="16px"></asp:Label>
                </td>
                <td class="titleField">Total Debit Note</td>
                <td>:</td>
                <td>
                    <asp:Label ID="lblTotalDN" runat="server" Text="0" Width="280px"></asp:Label></td>
            </tr>
            <tr>
                <td class="titleField" style="height: 32px"> &nbsp;</td>
                <td></td>
                <td>
                    &nbsp;
                </td>
                <td class="titleField"><asp:HyperLink runat="server" style="cursor:hand" alt="Klik Popup" ID="hylInfo">Prosedur Pembayaran Transfer</asp:HyperLink></td>
            </tr>
        </table>
        <div id="div1" style="overflow: auto; height: 250px">
            <asp:DataGrid ID="dtgEntryInvRevPayment" runat="server" Width="100%" AutoGenerateColumns="False"
                GridLines="Horizontal" CellPadding="3" BackColor="Gainsboro" BorderWidth="0px" BorderStyle="None" BorderColor="#CDCDCD"
                ShowFooter="true" AllowSorting="true" PageSize="25" ForeColor="GhostWhite" CellSpacing="1" Font-Names="MS Reference Sans Serif">
                <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                <AlternatingItemStyle ForeColor="Black" BackColor="White"></AlternatingItemStyle>
                <ItemStyle ForeColor="Black" BackColor="#F1F6FB"></ItemStyle>
                <HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
                <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
                <Columns>
                    <asp:TemplateColumn HeaderText="Nomor Debit Note">
                        <HeaderStyle Width="20%" CssClass="titleTableService"></HeaderStyle>
                        <ItemTemplate>
                            <asp:HiddenField ID="hdnID" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.ID")%>' />
                            <asp:Label ID="lblDN_Number" runat="server"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Wrap="False"></ItemStyle>
                        <FooterTemplate>
                            <asp:DropDownList ID="ddlDN_Number" OnChange="DN_NumberChanged(this);" runat="server" Width="90%"></asp:DropDownList>
                            <asp:Button runat="server" ID="btnDN_NumberF" Width="0px" CommandName="retrievedata" Style="visibility: hidden;"></asp:Button>
                        </FooterTemplate>
                        <FooterStyle HorizontalAlign="Center" VerticalAlign="Top" Wrap="False"></FooterStyle>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Kode Dealer">
                        <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Wrap="False"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblDealerCode" runat="server"></asp:Label>
                        </ItemTemplate>
                        <FooterStyle VerticalAlign="Top" Height="5px" HorizontalAlign="Center"></FooterStyle>
                        <FooterTemplate>
                            <asp:Label ID="lblDealerCodeF" runat="server"></asp:Label>
                        </FooterTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Nama Dealer">
                        <HeaderStyle Width="20%" CssClass="titleTableService"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Wrap="False"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblDealerName" runat="server"></asp:Label>
                        </ItemTemplate>
                        <FooterStyle VerticalAlign="Top" HorizontalAlign="Center"></FooterStyle>
                        <FooterTemplate>
                            <asp:Label ID="lblDealerNameF" runat="server"></asp:Label>
                        </FooterTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="No Tagihan">
                        <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Wrap="False"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblRegNumber" runat="server"></asp:Label>
                        </ItemTemplate>
                        <FooterStyle VerticalAlign="Top" HorizontalAlign="Center"></FooterStyle>
                        <FooterTemplate>
                            <asp:Label ID="lblRegNumberF" runat="server"></asp:Label>
                        </FooterTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Amount">
                        <HeaderStyle HorizontalAlign="Right" Width="10%" CssClass="titleTableService"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" Wrap="False"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblAmount" runat="server" EnableViewState="True" />
                        </ItemTemplate>
                        <FooterStyle VerticalAlign="Top" HorizontalAlign="Center"></FooterStyle>
                        <FooterTemplate>
                            <asp:Label ID="lblAmountF" runat="server"></asp:Label>
                        </FooterTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Wrap="False"></ItemStyle>
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnActive" Visible="false" CausesValidation="False" runat="server" Text="Aktif" CommandName="Active"
                                ToolTip="Status : Cancel , Aktifkan ?">
												<img border="0" src="../images/aktif.gif" alt="Status : NonAktif , Aktifkan ?" style="cursor:hand"></asp:LinkButton>
                            <asp:LinkButton ID="lbtnInactive" Visible="false" CausesValidation="False" runat="server" Text="Non-aktif" CommandName="Inactive"
                                ToolTip="Status : Aktif , Non Aktifkan ?">
												<img border="0" src="../images/in-aktif.gif" alt="Status : Aktif , Non-Aktifkan ?" style="cursor:hand"></asp:LinkButton>
                            <asp:LinkButton ID="lbtnDelete" runat="server" CommandName="Delete">
									<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
                        </ItemTemplate>
                        <FooterStyle VerticalAlign="Top" HorizontalAlign="Center"></FooterStyle>
                        <FooterTemplate>
                            <asp:LinkButton ID="lbtnAdd" runat="server" CommandName="tambah">
									<img src="../images/add.gif" border="0" alt="Tambah"></asp:LinkButton>
                        </FooterTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Alasan Batal" Visible="false">
                        <HeaderStyle HorizontalAlign="Right" Width="10%" CssClass="titleTableService"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Wrap="False"></ItemStyle>
                        <ItemTemplate>
                            <asp:TextBox ID="txtCancelReason" runat="server" Width="200px" Rows="2" TextMode="MultiLine" onkeyDown="checkTextAreaMaxLength(this,event,'50');"></asp:TextBox>&nbsp;
                            <asp:LinkButton ID="lbtnSaveCancelReason" runat="server" CommandName="SaveCancelReason">
									<img src="../images/simpan.gif" border="0" alt="Update Alasan Batal"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateColumn>

                    <asp:TemplateColumn HeaderText="Upload Surat Batal" Visible="false">
                        <HeaderStyle HorizontalAlign="Right" Width="10%" CssClass="titleTableService"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Wrap="False"></ItemStyle>
                        <ItemTemplate>
                            <asp:TextBox ID="txtUploadBatal" runat="server" Width="200px" Rows="2" Visible="false"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                </Columns>
            </asp:DataGrid>
            <br />
            
        </div>
        <br>
        <br />


        <asp:Button ID="btnSave" OnClientClick="return confirm('Simpan Data ?')" Text="Simpan" runat="server"></asp:Button>&nbsp;&nbsp;
        <asp:Button ID="btnBack" runat="server" Text="Kembali" Visible="false"></asp:Button>&nbsp;&nbsp;
        <asp:Button ID="btnTransferCancel" OnClientClick="return confirm('Transfer Cancel ?')" Visible="false" runat="server" Width="90px" Text="Transfer Cancel"></asp:Button>&nbsp;&nbsp;
    </form>
</body>
</html>
