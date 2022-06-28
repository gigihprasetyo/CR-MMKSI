<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmUploadPemesanan.aspx.vb" Inherits="FrmUploadPemesanan" smartNavigation="False"%>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmVehicleModel</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<LINK rel="stylesheet" type="text/css" href="../WebResources/stylesheet.css">
        <style type="text/css">
            #DownloadSampleButton {
                background:none;
                border:none;
                color:#585E80;
                margin:0;
                padding:0;
                cursor: pointer;
            }
        </style>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" border="0" cellSpacing="0" cellPadding="0" width="100%">
				<tr>
					<td style="HEIGHT: 17px" class="titlePage">ORDER DEALER - Upload Pemesanan</td>
				</tr>
				<tr>
					<td height="1" background="../images/bg_hor.gif"><IMG border="0" src="../images/bg_hor.gif" height="1"></td>
				</tr>
				<tr>
					<td height="10"><IMG border="0" src="../images/dot.gif" height="1"></td>
				</tr>
				<TR>
					<TD vAlign="top" align="left">
						<TABLE id="Table4" border="0" cellSpacing="1" cellPadding="2">
							<TR>
								<TD class="titleField" width="30%">Tipe Order</TD>
								<TD width="1%">:</TD>
								<TD width="59%"><asp:dropdownlist id="ddlOrderType" Width="180px" Runat="server"  AutoPostBack="True" OnSelectedIndexChanged="ddlOrderType_SelectedIndexChanged"></asp:dropdownlist></TD>
							</TR>
                            <%--<tr>
                                <td class="titleField">Cara Pembayaran</td>
                                <td width="1%">:</td>
                                <td>
                                    <asp:DropDownList ID="ddlTermOfPayment" runat="server" Width="160px" Visible="True"></asp:DropDownList>
                                </td>
                            </tr>--%>
							<TR>
								<TD class="titleField" width="30%">Upload File</TD>
								<TD width="1%">:</TD>
								<TD width="59%">
									<INPUT id="DataFile" onkeypress="return false;" size="29" type="file" name="File1" runat="server"
										style="HEIGHT: 20px">
									<asp:button id="btnUpload" runat="server" Width="70px" Text="Upload" style="HEIGHT: 20px"></asp:button>
								</TD>
							</TR>
							<TR>
								<TD class="titleField"></TD>
								<td></td>
								<TD>
									<asp:button id="DownloadSampleButton" runat="server" Height="19px" Text="Download Template" OnClick="DownloadSampleButton_Click"></asp:button>
								</TD>
							</TR>
							<TR>
								<TD class="titleField"></TD>
								<td></td>
                                <td>
									<asp:Label id="lblErrorMsg" runat="server" Text="" ForeColor="Red"></asp:Label>

                                </td>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD vAlign="top">
						<div style="HEIGHT: 320px; OVERFLOW: auto" id="div1">
							<asp:datagrid id="dgSparePart" runat="server" Width="100%" AllowSorting="True" CellSpacing="1"
								CellPadding="3" BorderWidth="0px" BackColor="Gainsboro" BorderColor="Gainsboro" AutoGenerateColumns="False"
								AllowCustomPaging="True" AllowPaging="True" PageSize="25">
								<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
								<AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
								<ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
								<Columns>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle Width="4%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblNo" runat="server" Text="No"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Dealer.DealerCode" HeaderText="Kode Dealer">
										<HeaderStyle Width="8%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblDealerCode" runat="server" Text=""></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="DeliveryDate" HeaderText="Tanggal Kirim">
										<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblDeliveryDate" runat="server" Text=""></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="PickingTicket" HeaderText="Picking Ticket">
										<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblTicket" runat="server" Text=""></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="TermOfPayment" HeaderText="Cara Pembayaran">
										<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblTermOfPayment" runat="server" Text=""></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Pesan">
										<HeaderStyle Width="15%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblMessage" runat="server" Text="" ForeColor="Red"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Detail Spare Part">
										<HeaderStyle Width="45%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
										<ItemTemplate>
											<!-- Order Details DataGrid goes here -->
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></div>
					</TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 11px" align="left">
						<TABLE id="Table5" border="0" cellPadding="1" width="100%">
							<TR>
								<TD height="40" width="50%"><asp:button id="btnSave" runat="server" Width="60px" Text="Simpan" Enabled="False"></asp:button>&nbsp;
									<asp:label id="lblDataErr" runat="server" Width="88px" ForeColor="Red"></asp:label></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
		</form>
		<script language="javascript">
			if (window.parent==window)
			{
				if (!navigator.appName=="Microsoft Internet Explorer")
				{
				  self.opener = null;
				  self.close();
				}
				else
				{
				   this.name = "origWin";
				   origWin= window.open(window.location, "origWin");
				   window.opener = top;
                   window.close();
				}
			}	
		</script>
	</body>
</HTML>
