<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>

<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmTOPSPPenaltyList.aspx.vb" Inherits=".FrmTOPSPPenaltyList" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>FrmTOPSPPenaltyList</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script language="javascript" src="../WebResources/FormFunctions.js"></script>
    <style type="text/css">
        .center {
          background-color: white;
          position: absolute;
          left: 25%;
          top: 30%;
          transform: translate(-50%, -50%);
          -ms-transform: translate(-50%, -50%); /* for IE 9 */
          -webkit-transform: translate(-50%, -50%); /* for Safari */

          /* optional size in px or %: */
          width: 600px;
          height: 250px;
          border-style: solid;
          border-color: #92a8d1;
        }
    </style>

    <script language="javascript">
        function KTBNote(selectedCode) {

        }

        function ShowPopUpDealer() {
            showPopUp('../PopUp/PopUpDealerSelectionOne.aspx', '', 430, 800, DealerSSS);
        }

        function DealerSSS(selectedRefNumber) {
            var hdnTemporaryOutlet = document.getElementById("hdnDealer");
            hdnTemporaryOutlet.value = selectedRefNumber;

            if (navigator.appName == "Microsoft Internet Explorer") {
                hdnTemporaryOutlet.blur();
            }
            else {
                hdnTemporaryOutlet.onchange();
            }
        }

        function CheckAll(aspCheckBoxID, checkVal) {
            re = new RegExp(':' + aspCheckBoxID + '$')
            for (i = 0; i < document.forms[0].elements.length; i++) {
                elm = document.forms[0].elements[i]
                if (elm.type == 'checkbox') {
                    if (re.test(elm.name)) {
                        elm.checked = checkVal
                    }
                }
            }
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Panel ID="pnlDaftar" runat="server">
            <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
                <tr>
                    <td class="titlePage">TOP Sparepart -&nbsp; Daftar Penalty</td>
                </tr>
                <tr>
                    <td background="../images/bg_hor.gif" height="1">
                        <img height="1" src="../images/bg_hor.gif" border="0"></td>
                </tr>
                <tr>
                    <td height="10">
                        <img height="1" src="../images/dot.gif" border="0"></td>
                </tr>
                <tr>
                    <td valign="top">
                        <table id="Table3" cellspacing="0" cellpadding="0" width="100%" border="0">
                            <tr>
                                <td align="left" valign="top" style="width: 50%">
                                    <table id="Table2" cellspacing="1" cellpadding="2" width="100%" border="0">
                                        <tr>
                                            <td class="titleField" width="15%" valign="top">
                                                <asp:Label ID="lblDealerSearch" runat="server">Kode Dealer</asp:Label></td>
                                            <td width="1%">:</td>
                                            <td width="34%">
                                                <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" ID="txtKodeDealer" runat="server" MaxLength="10"></asp:TextBox>
                                                <asp:LinkButton ID="lnkBtnPopUpDealer" runat="server" Width="16px">
                                                <img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0">
                                                </asp:LinkButton>
                                                <asp:Label ID="lblKodeDealer" runat="server"></asp:Label>
                                                <asp:HiddenField ID="hdnDealer" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="titleField" style="height: 18px">Credit Account</td>
                                            <td style="height: 18px">:</td>
                                            <td style="height: 18px">
                                                <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$')" ID="txtCreditAccount"
                                                    runat="server" MaxLength="50" Width="160px"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td class="titleField" style="height: 18px">Nomor Reg Payment</td>
                                            <td style="height: 18px">:</td>
                                            <td style="height: 18px">
                                                <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$')" ID="txtRegNumber"
                                                    runat="server" MaxLength="50" Width="160px"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td class="titleField" style="height: 18px">Tanggal Debit Memo</td>
                                            <td>:</td>
                                            <td>
                                                <table cellspacing="0" cellpadding="0" border="0" width="70%">
                                                    <tr valign="top">
                                                        <td>
                                                            <asp:CheckBox ID="cbDate" runat="server" Width="2px"></asp:CheckBox>
                                                        </td>
                                                        <td valign="top">
                                                            <cc1:IntiCalendar ID="icDebitMemoDateStart" runat="server" TextBoxWidth="70"></cc1:IntiCalendar></td>
                                                        <td valign="center">&nbsp;&nbsp;s.d&nbsp;&nbsp;</td>
                                                        <td valign="top">
                                                            <cc1:IntiCalendar ID="icDebitMemoDateEnd" runat="server" TextBoxWidth="70"></cc1:IntiCalendar></td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td class="titleField" style="height: 18px">No Debit Memo</td>
                                            <td style="height: 18px">:</td>
                                            <td style="height: 18px">
                                                <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$')" ID="txtNoDebitMemo"
                                                    runat="server" MaxLength="50" Width="160px"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td class="titleField" style="height: 18px">No Billing</td>
                                            <td style="height: 18px">:</td>
                                            <td style="height: 18px">
                                                <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$')" ID="txtNoBilling"
                                                    runat="server" MaxLength="50" Width="160px"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td class="titleField" style="height: 18px">Nilai Penalty</td>
                                            <td style="height: 18px">:</td>
                                            <td style="height: 18px">
                                                <asp:TextBox  onkeypress="return NumericOnlyWith(event,'');" ID="txtNilaiPenalty"
                                                    runat="server" MaxLength="50" Width="160px"></asp:TextBox></td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="width: 50%" valign="top">
                                    <table id="Table4" cellspacing="1" cellpadding="2" width="70%" border="0">
                                        <tr>
                                            <td class="titleField" style="height: 18px">No Accounting Doc</td>
                                            <td style="height: 18px">:</td>
                                            <td style="height: 18px">
                                                <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$')" ID="txtNoAccountingDoc"
                                                    runat="server" MaxLength="50" Width="160px"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td class="titleField" style="height: 18px">No Clearing Doc</td>
                                            <td style="height: 18px">:</td>
                                            <td style="height: 18px">
                                                <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$')" ID="txtNoClearingDoc"
                                                    runat="server" MaxLength="50" Width="160px"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td class="titleField" style="height: 18px">Nomor JV</td>
                                            <td style="height: 18px">:</td>
                                            <td style="height: 18px">
                                                <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$')" ID="txtNoJV"
                                                    runat="server" MaxLength="50" Width="160px"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td class="titleField" style="height: 18px">Nomor Reg Pengembalian</td>
                                            <td style="height: 18px">:</td>
                                            <td style="height: 18px">
                                                <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$')" ID="txtNoRegPengembalian"
                                                    runat="server" MaxLength="50" Width="160px"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td class="titleField" style="height: 18px">Status Penalty</td>
                                            <td style="height: 18px">:</td>
                                            <td style="height: 18px">
                                                <asp:DropDownList runat="server" ID="ddlStatusPenalty"></asp:DropDownList></td>
                                        </tr>
                                        <tr>
                                            <td class="titleField" style="height: 18px">Status Pengembalian PPh</td>
                                            <td style="height: 18px">:</td>
                                            <td style="height: 18px">
                                                <asp:DropDownList runat="server" ID="ddlStatusPengembalian"></asp:DropDownList></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <hr />
                        <asp:Button ID="btnSearch" runat="server" Width="60px" Text=" Cari "></asp:Button>
                        <asp:Button ID="btnDownloadExcel" runat="server" Text=" Download " Style="margin-left: 15px"></asp:Button>
                    <td></td>
                    <td></td>
                </tr>
            </table>

            <br />
            <div id="div1" style="overflow:scroll; height: auto;width: 1125px">
                <asp:DataGrid ID="dgTOPSPPenaltyDetailList" runat="server" Width="1700px" CellSpacing="1" ForeColor="Gray" GridLines="Horizontal"
                    CellPadding="3" BackColor="#CDCDCD" BorderWidth="0px" BorderColor="#CDCDCD" AutoGenerateColumns="False" 
                    AllowSorting="True" AllowCustomPaging="True" PageSize="25" AllowPaging="True" BorderStyle="None" DataKeyField="ID" ShowFooter="false">
                    <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                    <AlternatingItemStyle ForeColor="Black" BackColor="#F6F6F6"></AlternatingItemStyle>
                    <ItemStyle ForeColor="Black" BackColor="White" HorizontalAlign="Center"></ItemStyle>
                    <HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#4A3C8C"></HeaderStyle>
                    <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
                    <Columns>
                        <asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
                            <HeaderStyle CssClass="titleTableSales"></HeaderStyle>
                        </asp:BoundColumn>
                        <asp:TemplateColumn HeaderText="No">
                            <HeaderStyle ForeColor="White" Width="2%" CssClass="titleTableSales"></HeaderStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblNo" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn>
                            <HeaderStyle Width="3%" CssClass="titleTableSales"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            <HeaderTemplate>
                                <input id="chkAllItems" type="checkbox" onclick="CheckAll('chkItemChecked',
									    document.forms[0].chkAllItems.checked)" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkItemChecked" runat="server"></asp:CheckBox>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Kode Dealer" SortExpression="TOPSPPenalty.Dealer.ID">
                            <HeaderStyle ForeColor="White" Width="5%" CssClass="titleTableSales"></HeaderStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblDealerCode" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Nomor Reg Payment" SortExpression="TOPSPPenalty.TOPSPTransferPayment.RegNumber">
                            <HeaderStyle ForeColor="White" Width="7%" CssClass="titleTableSales"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            <ItemTemplate>      
                                <asp:LinkButton id="lbtnRegNumber" runat="server"></asp:LinkButton>                         
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="No Billing" SortExpression="BillingNumber">
                            <HeaderStyle ForeColor="White" Width="7%" CssClass="titleTableSales"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblBillingNumber" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Tanggal Jatuh Tempo" SortExpression="TOPSPTransferPayment.DueDate">
                            <HeaderStyle ForeColor="White" Width="6%" CssClass="titleTableSales"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblDueDate" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Tanggal Aktual Transfer" SortExpression="ActualTransferDate">
                            <HeaderStyle ForeColor="White" Width="6%" CssClass="titleTableSales"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblTransferActualDate" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Keterlambatan (Hari)" SortExpression="PenaltyDays">
                            <HeaderStyle ForeColor="White" Width="7%" CssClass="titleTableSales"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblKeterlambatanHari" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Nilai Penalty" SortExpression="AmountPenalty">
                            <HeaderStyle ForeColor="White" Width="7%" CssClass="titleTableSales"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblAmountPenalty" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Tanggal Debit Memo" SortExpression="TOPSPPenalty.DebitMemoDate">
                            <HeaderStyle ForeColor="White" Width="6%" CssClass="titleTableSales"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblDebitMemoDate" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="No Debit Memo" SortExpression="TOPSPPenalty.DebitMemoNumber">
                            <HeaderStyle ForeColor="White" Width="7%" CssClass="titleTableSales"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblDebitMemoNumber" runat="server"></asp:Label>&nbsp;&nbsp;
                                <asp:LinkButton ID="lnkbtnDownloadDebitMemo" runat="server" CommandName="DownloadDebitMemo" CausesValidation="False">
								    <img src="../images/download.gif" border="0" alt="Download"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="No Accounting Doc" SortExpression="TOPSPPenalty.AccountingNumber">
                            <HeaderStyle Width="7%" CssClass="titleTableSales"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblAccountingNumber" runat="server">
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="No Clearing Doc" SortExpression="TOPSPPenalty.ClearingNumber">
                            <HeaderStyle Width="7%" CssClass="titleTableSales"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblClearingNumber" runat="server">
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Tgl Pembayaran Penalty" SortExpression="TOPSPPenalty.PaymentDate">
                            <HeaderStyle Width="7%" CssClass="titleTableSales"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblPaymentDate" runat="server">
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Status Penalty" SortExpression="TOPSPPenalty.StatusPenalty">
                            <HeaderStyle Width="7%" CssClass="titleTableSales"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblStatusPenalty" runat="server">
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Keterangan" SortExpression="TOPSPPenalty.Message">
                            <HeaderStyle Width="25%" CssClass="titleTableSales"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblMessage" runat="server">
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Upload Bukti Potong">
                            <HeaderStyle ForeColor="White" Width="7%" CssClass="titleTableSales"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkbtnUploadBuktiPotong" runat="server" Width="20px" CausesValidation="False" 
                                    CommandName="UploadBuktiPotong" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.ID")%>'>
								    <img width="16" alt="Upload Bukti Potong" src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABQAAAAUCAYAAACNiR0NAAAABHNCSVQICAgIfAhkiAAAAAlwSFlzAAALEwAACxMBAJqcGAAAAZNJREFUOI2N08+LTXEYx/HX5TBXTcSgcJVRkkwJC7KaJrKTP2H+g8vCUk2zsKIkOytbNbOxpESTBQtsSM1iMkIYdaepkfll8f1+dRzX+Z5nc56+z+e8n895zvdpqY8bOIYjGV0b+3A9o/MOm3IiTGIcb3LiNaw3ABaxcbvICFPDPTiBVTzFRkW3jquYywFTHMJlrOA5flXqLUxgqinwJTq4iDul81ncxOboUh2w8Pf8XuBbRbMQn1uj+1rgIJZifh+n+2i6wk1o42cOuAOLMb+NoT6a5PBP8zrgTvyI+RiG+2hm8RrbU/M64F58jXnRR7sUnRPcL+SA+/Ep5g+xq1JfLuVD+J4DDgsDP4oH/9F08US4h2tl4DacFe5TilF8xkFcKZ13MCPMT6x/TMUEvCesU6/04hdh3UZKZwdi4x4uYB4n8aoKPIXj/t3RalwT5nkGhyNwDI+TIC3/RgMYbBH2uCd8+u7o9FFV+LYBDG7hA94L12Yel8qCQvhTKzifgQ1EN+eUfkI1Wrjb0N0qpvGsTvQbYqhQ2cHMsT0AAAAASUVORK5CYII=" border="0">
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Nilai PPh" SortExpression="PPh">
                            <HeaderStyle ForeColor="White" Width="7%" CssClass="titleTableSales"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblPPh" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="No Reg Pengembalian PPh" SortExpression="TOPSPPenalty.NoRegPengembalian">
                            <HeaderStyle Width="7%" CssClass="titleTableSales"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblNoRegPengembalian" runat="server">
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="No Bukti Potong" SortExpression="TOPSPPenalty.NoBuktiPotong">
                            <HeaderStyle Width="7%" CssClass="titleTableSales"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblNoBuktiPotong" runat="server">
                                </asp:Label>&nbsp;&nbsp;
                                <asp:LinkButton ID="lnkbtnDownloadBuktiPotong" runat="server" CommandName="DownloadBuktiPotong" CausesValidation="False">
								    <img src="../images/download.gif" border="0" alt="Download"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="No JV" SortExpression="TOPSPPenalty.JVNumber">
                            <HeaderStyle Width="7%" CssClass="titleTableSales"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblJVNumber" runat="server">
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Status Pengembalian PPh" SortExpression="TOPSPPenalty.StatusPengembalian">
                            <HeaderStyle Width="7%" CssClass="titleTableSales"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblStatusPengembalian" runat="server">
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                    </Columns>
                    <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
                </asp:DataGrid>
            </div>
            <br />
            <div style="vertical-align: bottom">
                <asp:Label ID="lblChangeStatus0" runat="server" Font-Italic="True" Font-Bold="true" Visible="False">Pengembalian PPh</asp:Label>&nbsp;
            </div>
            <div style="vertical-align: bottom;left:200px">
                <asp:Label ID="lblChangeStatus" runat="server" Font-Italic="false" Font-Bold="false" Font-Size="Small" Visible="False">&nbsp;&nbsp;
                    &nbsp;&nbsp;Mengubah Status :</asp:Label>&nbsp;
		        <asp:DropDownList ID="ddlStatus" runat="server" Visible="False"></asp:DropDownList>&nbsp;
		        <asp:Button ID="btnProses" runat="server" Width="64px" Text="Proses" Visible="False"></asp:Button>&nbsp;
                <asp:Button ID="btnTransfer" runat="server" Text="Send to SAP"></asp:Button>&nbsp;
            </div>
        </asp:Panel>

            <div ID="pnlUpload" runat="server" Visible="false" class="center">
                <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
                <tr>
                    <td class="titlePage">Upload -&nbsp; Bukti Potong</td>
                </tr>
                <tr>
                    <td>
                        <hr style="color:#92a8d1" /></td>
                </tr>
                <tr>
                    <td height="10">
                        <img height="1" src="../images/dot.gif" border="0"></td>
                </tr>
                <tr>
                    <td valign="top">
                        <table id="Table3" cellspacing="0" cellpadding="0" width="100%" border="0">
                            <tr>
                                <td align="left" valign="top" style="width: 50%">
                                    <table id="Table2" cellspacing="1" cellpadding="2" width="100%" border="0">
                                        <tr>
                                            <td class="titleField" width="25%" valign="top">
                                                <asp:Label ID="Label1" runat="server">Nomor Debit Memo</asp:Label></td>
                                            <td width="1%">:</td>
                                            <td width="34%">
                                                <asp:Label ID="lblDebitMemoNumber" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="titleField" style="height: 18px">Nomor Reg Pengembalian PPh</td>
                                            <td style="height: 18px">:</td>
                                            <td style="height: 18px">
                                                <asp:Label ID="lblNoRegPengembalianUpload" runat="server">[Auto Generate]</asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td class="titleField" style="height: 18px">Amount PPh</td>
                                            <td style="height: 18px">:</td>
                                            <td style="height: 18px">
                                                <asp:Label ID="lblAmountPPh" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td class="titleField" style="height: 18px">Nomor Bukti Potong</td>
                                            <td style="height: 18px">:</td>
                                            <td style="height: 18px">
                                                <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$')" ID="txtNoBuktiPotong"
                                                    runat="server" MaxLength="50" Width="160px"></asp:TextBox>
                                                <asp:Label ID="lblNoBuktiPotong" runat="server" Visible="false"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td class="titleField" style="height: 18px">Tanggal Bukti Potong</td>
                                            <td style="height: 18px">:</td>
                                            <td style="height: 18px">
                                                <asp:Label ID="lblBuktiPotongDate" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td class="titleField" style="height: 18px">Upload Bukti Potong</td>
                                            <td style="height: 18px">:</td>
                                            <td style="height: 18px">
                                                <input id="FileUpload" onkeydown="return false;" style="width: 267px; height: 20px" type="file" size="25" name="FileUpload" runat="server"><br /><asp:Label ID="Label2" runat="server" ForeColor="Red"><i>Maksimal file size 1MB</i></asp:Label>&nbsp;
                                                <asp:Label ID="lblFileUpload" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                                <table width="100%">
                                                    <tr>
                                                        <td>
                                                            <asp:DataGrid ID="dgRincianBilling" runat="server" Width="100%" CellSpacing="1" ForeColor="Gray" GridLines="Horizontal"
                                                                CellPadding="3" BackColor="#CDCDCD" BorderWidth="0px" BorderColor="#CDCDCD" AutoGenerateColumns="False" 
                                                                AllowSorting="True" AllowCustomPaging="True" PageSize="25" AllowPaging="True" BorderStyle="None" DataKeyField="ID" ShowFooter="false">
                                                                <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                                                                <AlternatingItemStyle ForeColor="Black" BackColor="#F6F6F6"></AlternatingItemStyle>
                                                                <ItemStyle ForeColor="Black" BackColor="White" HorizontalAlign="Center"></ItemStyle>
                                                                <HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#4A3C8C"></HeaderStyle>
                                                                <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
                                                                <Columns>
                                                                    <asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
                                                                        <HeaderStyle CssClass="titleTableSales"></HeaderStyle>
                                                                    </asp:BoundColumn>
                                                                    <asp:TemplateColumn HeaderText="No">
                                                                        <HeaderStyle ForeColor="White" Width="2%" CssClass="titleTableSales"></HeaderStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblNo" runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateColumn>
                                                                    <asp:TemplateColumn HeaderText="Billing" SortExpression="TOPSPPenaltyDetail.SparePartBilling.BillingNumber">
                                                                        <HeaderStyle ForeColor="White" Width="7%" CssClass="titleTableSales"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                        <ItemTemplate>
								                                            <asp:Label ID="lblBillingNumber" runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateColumn>
                                                                    <asp:TemplateColumn HeaderText="Nilai Penalty" SortExpression="AmountPenalty">
                                                                        <HeaderStyle ForeColor="White" Width="7%" CssClass="titleTableSales"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblAmountPenalty" runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateColumn>
                                                                    <asp:TemplateColumn HeaderText="Nilai PPh" SortExpression="PPh">
                                                                        <HeaderStyle ForeColor="White" Width="7%" CssClass="titleTableSales"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblPPh" runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateColumn>
                                                                </Columns>
                                                                <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
                                                            </asp:DataGrid>
                                                        </td>
                                                    </tr>
                                                </table>                                                
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="titleField" style="height: 18px"></td>
                                            <td></td>
                                            <td>                                                
                                                <asp:Button ID="btnSimpan" OnClientClick="return confirm('Anda yakin mau simpan?');" runat="server" Width="60px" Text=" Simpan "></asp:Button>
                                                <asp:Button ID="btnKembali" runat="server" Text=" Kembali " Style="margin-left: 15px"></asp:Button>
                                                <asp:HiddenField ID="hdnTOPSPPenaltyID" runat="server" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>

    </form>
</body>
</html>
