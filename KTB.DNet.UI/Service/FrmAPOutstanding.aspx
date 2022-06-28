<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmAPOutstanding.aspx.vb" Inherits="FrmAPOutstanding" %>

<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>Umum - AP Outstanding</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script language="javascript" src="../WebResources/FormFunctions.js"></script>
    <script type="text/javascript">
        function ShowPPDealerSelection() {
            showPopUp('../General/../PopUp/PopUpDealerSelection.aspx', '', 500, 760, DealerSelection);
        }

        function DealerSelection(selectedDealer) {
            var txtDealerSelection = document.getElementById("txtKodeDealer");
            txtDealerSelection.value = selectedDealer;
        }
    </script>
</head>

<body MS_POSITIONING="GridLayout">
    <form id="Form1" method="post" runat="server">
    <table id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
        <tr>
			<td class="titlePage">Umum - Dokumen AP Outstanding</td>
		</tr>
		<tr>
			<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
		</tr>
		<tr>
			<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
		</tr>
        <tr>
			<td vAlign="top" align="left">
                <table id="Table2" cellSpacing="1" cellPadding="2" width="100%" border="0">
                    <tr>
                        <td class="titleField" style="height: 16px" width="24%">
                            <asp:Label ID="Label1" runat="server">Dealer</asp:Label></td>
                        <td style="height: 16px" width="1%">:</td>
                        <td style="height: 16px" width="75%">
                            <asp:TextBox ID="txtKodeDealer" runat="server" onkeypress="return alphaNumericExcept(event,'<>?*%$')"
                                onblur="omitSomeCharacter('txtKodeDealer','<>?*%$')"></asp:TextBox>
                            <asp:label id="lblSearchDealer" runat="server" onclick="ShowPPDealerSelection();">
								<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup">
							</asp:label>
                    </tr>
                    <tr>
                        <td class="titleField">Area</td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="txtArea" runat="server" Width="174px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="titleField">Jenis Dokumen</td>
                        <td>:</td>
                        <td>
                            <asp:DropDownList ID="ddlJenisDokumen" runat="server" Width="240px"></asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td class="titleField">No Accounting</td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="txtNoAccounting" runat="server" Width="174px"></asp:TextBox>
                        </td>
                    </tr> 
                    <tr>
                        <td class="titleField">Billing No</td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="txtNoReff" runat="server" Width="174px"></asp:TextBox>
                        </td>
                    </tr>                   
                    <tr>
                        <td class="titleField">
                            <asp:Label ID="lblDueDate" runat="server"> Due Date</asp:Label></td>
                        <td>:</td>
                        <td>
                            <table cellspacing="0" cellpadding="0">
                                <tr>
                                    <td>
                                        <cc1:IntiCalendar ID="DueDateFrom" runat="server" TextBoxWidth="70"></cc1:IntiCalendar>
                                    <td>&nbsp;s/d&nbsp;</td>
                                    <td>
                                        <cc1:IntiCalendar ID="DueDateTo" runat="server" TextBoxWidth="70"></cc1:IntiCalendar>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="titleField">Clearing No</td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="txtClearingNo" runat="server" Width="174px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="titleField"></td>
                        <td></td>
                        <td>
                            <asp:Button ID="btnCari" runat="server" Width="64px" Text="Cari" Style="z-index: 0"></asp:Button></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td vAlign="top">
				<div id="div1" style="OVERFLOW: auto">
                    <asp:datagrid id="dtgAPoutstanding" runat="server" Width="100%" CellSpacing="1" OnItemDataBound="dtgAPoutstanding_ItemDataBound"
                        AllowCustomPaging="true" AllowSorting="True" PageSize="50" AllowPaging="True" GridLines="Vertical" 
                        CellPadding="3" BackColor="#CDCDCD" BorderWidth="0px" BorderStyle="None" BorderColor="#E7E7FF" 
                        AutoGenerateColumns="False" ShowFooter="false">
						<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
						<AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
						<ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
						<HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
						<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
						<Columns>                            
                            <asp:TemplateColumn HeaderText="No">
								<HeaderStyle Width="3%" CssClass="titleTableService"></HeaderStyle>
								<ItemStyle HorizontalAlign="Center"></ItemStyle>
								<ItemTemplate>
									<asp:Label id="lblMonthlyDocumentID" runat="server" Visible="false"></asp:Label>
									<asp:Label id="lblNo" runat="server"></asp:Label>
								</ItemTemplate>
							</asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Jenis Dokumen">
								<HeaderStyle CssClass="titleTableService"></HeaderStyle>
								<ItemStyle HorizontalAlign="Left"></ItemStyle>
								<ItemTemplate>
									<asp:Label id="lblJenisDokument" runat="server"></asp:Label>
								</ItemTemplate>
							</asp:TemplateColumn>	                                                                                                               						
                            <asp:TemplateColumn SortExpression="MonthlyDocument.Dealer.DealerCode" HeaderText="Dealer Code">
								<HeaderStyle CssClass="titleTableService"></HeaderStyle>
								<ItemStyle HorizontalAlign="Center"></ItemStyle>
								<ItemTemplate>
									<asp:Label id="lblDealerCode" runat="server"></asp:Label>
								</ItemTemplate>
							</asp:TemplateColumn>
                           <asp:TemplateColumn SortExpression="MonthlyDocument.Dealer.Area1.MainArea.Description" HeaderText="Region">
								<HeaderStyle CssClass="titleTableService"></HeaderStyle>
								<ItemStyle HorizontalAlign="Center"></ItemStyle>
								<ItemTemplate>
									<asp:Label id="lblRegion" runat="server"></asp:Label>
								</ItemTemplate>
							</asp:TemplateColumn>
                            <asp:TemplateColumn SortExpression="MonthlyDocument.Dealer.Area1.Description" HeaderText="Area">
								<HeaderStyle CssClass="titleTableService"></HeaderStyle>
								<ItemStyle HorizontalAlign="Center"></ItemStyle>
								<ItemTemplate>
									<asp:Label id="lblArea" runat="server"></asp:Label>
								</ItemTemplate>
							</asp:TemplateColumn>
                            <asp:TemplateColumn SortExpression="MonthlyDocument.Dealer.DealerName" HeaderText="Dealer Name">
								<HeaderStyle CssClass="titleTableService"></HeaderStyle>
								<ItemStyle HorizontalAlign="Center"></ItemStyle>
								<ItemTemplate>
									<asp:Label id="lblDealerName" runat="server"></asp:Label>
								</ItemTemplate>
							</asp:TemplateColumn>
                            <asp:TemplateColumn SortExpression="" HeaderText="Parked Name">
								<HeaderStyle CssClass="titleTableService"></HeaderStyle>
								<ItemStyle HorizontalAlign="Center"></ItemStyle>
								<ItemTemplate>
									<asp:Label id="lblParkedName" runat="server"></asp:Label>
								</ItemTemplate>
							</asp:TemplateColumn>
                            <asp:TemplateColumn SortExpression="" HeaderText="Doc No">
								<HeaderStyle CssClass="titleTableService"></HeaderStyle>
								<ItemStyle HorizontalAlign="Center"></ItemStyle>
								<ItemTemplate>
									<asp:Label id="lblDocNo" runat="server"></asp:Label>
								</ItemTemplate>
							</asp:TemplateColumn>
                            <asp:TemplateColumn SortExpression="" HeaderText="Billing No">
								<HeaderStyle CssClass="titleTableService"></HeaderStyle>
								<ItemStyle HorizontalAlign="Center"></ItemStyle>
								<ItemTemplate>
									<asp:Label id="lblReffNo" runat="server"></asp:Label>
								</ItemTemplate>
							</asp:TemplateColumn>
                            <asp:TemplateColumn SortExpression="" HeaderText="Assignment">
								<HeaderStyle CssClass="titleTableService"></HeaderStyle>
								<ItemStyle HorizontalAlign="Center"></ItemStyle>
								<ItemTemplate>
									<asp:Label id="lblAssignment" runat="server"></asp:Label>
								</ItemTemplate>
							</asp:TemplateColumn>
                            <asp:TemplateColumn  HeaderText="Posting Date">
								<HeaderStyle CssClass="titleTableService"></HeaderStyle>
								<ItemStyle HorizontalAlign="Center"></ItemStyle>
								<ItemTemplate>
									<asp:Label id="lblPostingDate" runat="server" DataFormatString="{0:dd/MM/yyyy}"></asp:Label>
								</ItemTemplate>
							</asp:TemplateColumn>
                            <asp:TemplateColumn  HeaderText="Year">
								<HeaderStyle CssClass="titleTableService"></HeaderStyle>
								<ItemStyle HorizontalAlign="Center"></ItemStyle>
								<ItemTemplate>
									<asp:Label id="lblYear" runat="server"></asp:Label>
								</ItemTemplate>
							</asp:TemplateColumn>
                            <asp:TemplateColumn SortExpression="" HeaderText="Amount">
								<HeaderStyle CssClass="titleTableService"></HeaderStyle>
								<ItemStyle HorizontalAlign="Center"></ItemStyle>
								<ItemTemplate>
									<asp:Label id="lblAmount" runat="server" DataFormatString="{0:#,###}"></asp:Label>
								</ItemTemplate>
							</asp:TemplateColumn>
                            <asp:TemplateColumn SortExpression="" HeaderText="Curr">
								<HeaderStyle CssClass="titleTableService"></HeaderStyle>
								<ItemStyle HorizontalAlign="Center"></ItemStyle>
								<ItemTemplate>
									<asp:Label id="lblCurrency" runat="server"></asp:Label>
								</ItemTemplate>
							</asp:TemplateColumn>
                            <asp:TemplateColumn SortExpression="" HeaderText="Text">
								<HeaderStyle CssClass="titleTableService"></HeaderStyle>
								<ItemStyle HorizontalAlign="Center"></ItemStyle>
								<ItemTemplate>
									<asp:Label id="lblText" runat="server"></asp:Label>
								</ItemTemplate>
							</asp:TemplateColumn>
                            <asp:TemplateColumn SortExpression="" HeaderText="Document">
								<HeaderStyle CssClass="titleTableService"></HeaderStyle>
								<ItemStyle HorizontalAlign="Center"></ItemStyle>
								<ItemTemplate>
									<asp:Label id="lblDocument" runat="server"></asp:Label>
								</ItemTemplate>
							</asp:TemplateColumn>
                            <asp:TemplateColumn SortExpression="" HeaderText="Clearing">
								<HeaderStyle CssClass="titleTableService"></HeaderStyle>
								<ItemStyle HorizontalAlign="Center"></ItemStyle>
								<ItemTemplate>
									<asp:Label id="lblClearing" runat="server"></asp:Label>
								</ItemTemplate>
							</asp:TemplateColumn>
                            <asp:TemplateColumn SortExpression="" HeaderText="Reason">
								<HeaderStyle CssClass="titleTableService"></HeaderStyle>
								<ItemStyle HorizontalAlign="Center"></ItemStyle>
								<ItemTemplate>
									<asp:Label id="lblReason" runat="server"></asp:Label>
								</ItemTemplate>
							</asp:TemplateColumn>
                            <asp:TemplateColumn SortExpression="" HeaderText="Estimasi Rencana Transfer" >
								<HeaderStyle CssClass="titleTableService"></HeaderStyle>
								<ItemStyle HorizontalAlign="Center"></ItemStyle>
								<ItemTemplate>
									<asp:Label id="lblEstimasiRT" runat="server" ></asp:Label>
								</ItemTemplate>
							</asp:TemplateColumn>
                            <asp:TemplateColumn SortExpression="" HeaderText="Actual Transfer" >
								<HeaderStyle CssClass="titleTableService"></HeaderStyle>
								<ItemStyle HorizontalAlign="Center"></ItemStyle>
								<ItemTemplate>
									<asp:Label id="lblActualTransfer" runat="server" ></asp:Label>
								</ItemTemplate>
							</asp:TemplateColumn>
                        </Columns>
                        <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
                    </asp:datagrid>
                </div>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
