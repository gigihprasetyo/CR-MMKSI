<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>

<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmEntryInvoiceRevisionPayment.aspx.vb" Inherits="FrmEntryInvoiceRevisionPayment" SmartNavigation="False" %>

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
        function NoRangkaChanged(ddl) {
            if (ddl.id.length == 40) {
                var sPre = ddl.id.substring(0, ddl.id.length - 12);
                var sLast = ddl.id.substring(ddl.id.length - 1);

                var btn = document.getElementById(sPre + 'btnNoRangka' + sLast);
                btn.click();
            }
        }
        function DealerCodeChanged(ddl) {
            if (ddl.id.length == 42) {
                var sPre = ddl.id.substring(0, ddl.id.length - 14);
                var sLast = ddl.id.substring(ddl.id.length - 1);

                var btn = document.getElementById(sPre + 'btnDealerCode' + sLast);
                btn.click();
            }
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
                <td class="titlePage">REVISI FAKTUR - INPUT PEMBAYARAN</td>
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
                    <asp:Literal ID="ltrDealerCode" runat="server"></asp:Literal></td>
                <td class="titleField">Jenis Pembayaran</td>
                <td>:</td>
                <td>
                    <asp:DropDownList ID="ddlPaymentType" runat="server" Width="140px" AutoPostBack="true"></asp:DropDownList></td>
            </tr>
            <tr>
                <td class="titleField" style="height: 32px">Nomor Registrasi</td>
                <td style="height: 32px">:</td>
                <td style="height: 32px">
                    <asp:Label ID="lblRegNumber" runat="server" Width="280px">[Auto Generated]</asp:Label></td>
                <td runat="server" id="tdBankName1" class="titleField" style="height: 32px">Nama Bank</td>
                <td runat="server" id="tdBankName2" style="height: 32px">:</td>
                <td runat="server" id="tdBankName3" style="height: 32px">
                    <asp:DropDownList ID="ddlBankName" runat="server" Width="230px"></asp:DropDownList>
                </td>
            </tr>
            <tr valign="top">
                <td class="titleField">Tipe Revisi</td>
                <td>:</td>
                <td>
                    <asp:DropDownList ID="ddlRevisionType" runat="server" Width="200px" AutoPostBack="true"></asp:DropDownList>
                </td>
                <td runat="server" id="tdGiroNo1" class="titleField">Nomor Gyro</td>
                <td runat="server" id="tdGiroNo2">:</td>
                <td runat="server" id="tdGiroNo3">
                    <asp:TextBox ID="txtGiroNo" onkeypress="return alphaNumericExcept(event,'<>?*%$')" onblur="omitSomeCharacter('txtGiroNo','<>?*%$;')"
                        runat="server" Width="150px" size="22" MaxLength="10"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="titleField">Kategori</td>
                <td>:</td>
                <td>
                    <asp:DropDownList ID="ddlKategori" runat="server" Width="80px" AutoPostBack="true"></asp:DropDownList></td>
                <td runat="server" id="tdTglGiro1" class="titleField">Tgl Gyro</td>
                <td runat="server" id="tdTglGiro2">:</td>
                <td runat="server" id="tdTglGiro3">
                    <cc1:IntiCalendar ID="icTglGiro" runat="server" TextBoxWidth="60"></cc1:IntiCalendar>
                </td>
            </tr>

            <tr>
                <td class="titleField">Total Amount</td>
                <td>:</td>
                <td>
                    <asp:Label ID="lblTotalAmount" runat="server" Width="280px"></asp:Label></td>
                <td class="titleField">Total Rangka</td>
                <td>:</td>
                <td><asp:Label ID="lblTotalRangka" runat="server" Width="280px"></asp:Label></td>
            </tr>
        </table>
        <div id="div1" style="overflow: auto; height: 250px">
            <asp:DataGrid ID="dtgEntryInvRevPayment" runat="server" BackColor="#CDCDCD" Width="100%" DataKeyField="ID"
                ShowFooter="True" AutoGenerateColumns="False" AllowSorting="True" CellSpacing="1" CellPadding="3" BorderColor="Gainsboro" BorderWidth="0px">
                <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                <AlternatingItemStyle ForeColor="Black" BackColor="White"></AlternatingItemStyle>
                <ItemStyle ForeColor="Black" BackColor="#F1F6FB"></ItemStyle>
                <HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
                <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
                <Columns>
                    <asp:TemplateColumn HeaderText="Kode Dealer">
                        <HeaderStyle Width="20%" CssClass="titleTableSales"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblDealerCode" runat="server"></asp:Label>
                        </ItemTemplate>
                        <FooterStyle VerticalAlign="Middle" HorizontalAlign="Center"></FooterStyle>
                        <FooterTemplate>
                            <asp:DropDownList ID="ddlDealerCodeF" OnChange="DealerCodeChanged(this);" runat="server" Width="220px"></asp:DropDownList>
                            <asp:Button runat="server" ID="btnDealerCodeF" Width="1px" CommandName="DealerCodeChangedF" Style="visibility: hidden;"></asp:Button>
                            <%--<asp:Label ID="lblDealerCodeF" runat="server"></asp:Label>--%>
                        </FooterTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Nomor Rangka">
                        <HeaderStyle Width="20%" CssClass="titleTableSales"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        <ItemTemplate>
                            <asp:HiddenField ID="hdnID" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.ID")%>' />
                            <asp:HiddenField ID="hdnRevisionFakturID" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.RevisionFaktur.ID")%>' />
                            <asp:Label ID="lblNoRangka" runat="server"></asp:Label>
                        </ItemTemplate>
                        <FooterStyle VerticalAlign="Middle" HorizontalAlign="Center"></FooterStyle>
                        <FooterTemplate>
                            <asp:DropDownList ID="ddlNoRangkaF" OnChange="NoRangkaChanged(this);" runat="server" Width="220px"></asp:DropDownList>
                            <asp:Button runat="server" ID="btnNoRangkaF" Width="1px" CommandName="NoRangkaChangedF" Style="visibility: hidden;"></asp:Button>
                        </FooterTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Kategori">
                        <HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblCategoryCode" runat="server"></asp:Label>
                        </ItemTemplate>
                        <FooterStyle VerticalAlign="Top" HorizontalAlign="Center"></FooterStyle>
                        <FooterTemplate>
                            <asp:Label ID="lblCategoryCodeF" runat="server"></asp:Label>
                        </FooterTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Nama Customer">
                        <HeaderStyle Width="20%" CssClass="titleTableSales"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblCustomerName" runat="server"></asp:Label>
                        </ItemTemplate>
                        <FooterStyle VerticalAlign="Top" HorizontalAlign="Center"></FooterStyle>
                        <FooterTemplate>
                            <asp:Label ID="lblCustomerNameF" runat="server"></asp:Label>
                        </FooterTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="No Pengajuan Revisi">
                        <HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblRegNumber" runat="server"></asp:Label>
                        </ItemTemplate>
                        <FooterStyle VerticalAlign="Top" HorizontalAlign="Center"></FooterStyle>
                        <FooterTemplate>
                            <asp:Label ID="lblRegNumberF" runat="server"></asp:Label>
                        </FooterTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="No Debit Charge">
                        <HeaderStyle HorizontalAlign="Right" Width="10%" CssClass="titleTableSales"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblDebitChargeNo" runat="server"></asp:Label>
                        </ItemTemplate>
                        <FooterStyle VerticalAlign="Top" HorizontalAlign="Center"></FooterStyle>
                        <FooterTemplate>
                            <asp:Label ID="lblDebitChargeNoF" runat="server"></asp:Label>
                        </FooterTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Amount">
                        <HeaderStyle HorizontalAlign="Right" Width="10%" CssClass="titleTableSales"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblAmount" runat="server" EnableViewState="True" />
                        </ItemTemplate>
                        <FooterStyle VerticalAlign="Top" HorizontalAlign="Center"></FooterStyle>
                        <FooterTemplate>
                            <asp:Label ID="lblAmountF" runat="server"></asp:Label>
                        </FooterTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="TR Number">
                        <HeaderStyle HorizontalAlign="Right" Width="10%" CssClass="titleTableSales"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblTRNumber" runat="server"></asp:Label>
                        </ItemTemplate>
                        <FooterStyle VerticalAlign="Top" HorizontalAlign="Center"></FooterStyle>
                        <FooterTemplate>
                            <asp:Label ID="lblTRNumberF" runat="server"></asp:Label>
                        </FooterTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnActive" CausesValidation="False" runat="server" Text="Aktif" CommandName="Active"
                                ToolTip="Status : Cancel , Aktifkan ?">
												<img border="0" src="../images/aktif.gif" alt="Status : NonAktif , Aktifkan ?" style="cursor:hand"></asp:LinkButton>
                            <asp:LinkButton ID="lbtnInactive" CausesValidation="False" runat="server" Text="Non-aktif" CommandName="Inactive"
                                ToolTip="Status : Aktif , Non Aktifkan ?">
												<img border="0" src="../images/in-aktif.gif" alt="Status : Aktif , Non-Aktifkan ?" style="cursor:hand"></asp:LinkButton>
                            <asp:LinkButton ID="lbtnDelete" runat="server" CommandName="Delete">
									<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
                        </ItemTemplate>
                        <FooterStyle VerticalAlign="Top" HorizontalAlign="Center"></FooterStyle>
                        <FooterTemplate>
                            <asp:LinkButton ID="lbtnAdd" runat="server" CommandName="Add">
									<img src="../images/add.gif" border="0" alt="Tambah"></asp:LinkButton>
                        </FooterTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Alasan Batal" Visible="false">
                        <HeaderStyle HorizontalAlign="Right" Width="10%" CssClass="titleTableSales"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        <ItemTemplate>
                            <asp:TextBox ID="txtCancelReason" runat="server" Width="200px" Rows="2" TextMode="MultiLine" onkeyDown="checkTextAreaMaxLength(this,event,'50');" ></asp:TextBox>&nbsp;
                            <asp:LinkButton ID="lbtnSaveCancelReason" runat="server" CommandName="SaveCancelReason">
									<img src="../images/simpan.gif" border="0" alt="Update Alasan Batal"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateColumn>

                    <asp:TemplateColumn HeaderText="Upload Surat Batal" Visible="false">
                        <HeaderStyle HorizontalAlign="Right" Width="10%" CssClass="titleTableSales"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        <ItemTemplate>
                            <asp:TextBox ID="txtUploadBatal" runat="server" Width="200px" Rows="2" Visible="false"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                </Columns>
            </asp:DataGrid>
            <br />
            <asp:PlaceHolder runat="server" ID="ErrorMessage" Visible="false">
                <p>Pesan Error :</p>
                <br />
                <p class="text-danger">
                    <asp:Literal runat="server" ID="FailureText" />
                </p>
            </asp:PlaceHolder>

        </div>
        <br>
        <br />


        <asp:Button ID="btnSave" OnClientClick="return confirm('Simpan Data ?')" Text="Simpan" runat="server"></asp:Button>&nbsp;&nbsp;
        <asp:Button ID="btnBack" runat="server" Text="Kembali" Visible="false"></asp:Button>&nbsp;&nbsp;
        <asp:Button ID="btnTransferCancel" OnClientClick="return confirm('Transfer Cancel ?')" runat="server" Width="90px" Text="Transfer Cancel"></asp:Button>&nbsp;&nbsp;
    </form>
</body>
</html>
