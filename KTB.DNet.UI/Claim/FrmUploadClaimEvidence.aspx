<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmUploadClaimEvidence.aspx.vb" Inherits="FrmClaimEvidence" smartNavigation="False"%>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>

	<HEAD>
	
		<title>FrmClaimEvidence</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="1" cellPadding="2" width="100%" border="0">
				<TR>
					<TD class="titlePage" colSpan="4">
						<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="titlePage">CLAIM&nbsp;- Upload Bukti Claim</td>
							</tr>
							<tr>
								<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
							</tr>
							<tr>
								<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
							</tr>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD class="titleField" width="20%"><asp:label id="Label1" runat="server">Kode</asp:label>Dealer</TD>
					<td width="1%">:</td>
					<TD width="530" style="WIDTH: 530px"><asp:label id="lblDealerCode" runat="server"></asp:label></TD>
					<td width="15%"></td>
				</TR>
				<TR>
					<TD class="titleField">Nama Dealer</TD>
					<td>:</td>
					<TD width="530" style="WIDTH: 530px"><asp:label id="lblDealerName" runat="server"></asp:label></TD>
					<td width="15%"></td>
				</TR>
				<TR>
					<TD class="titleField">Nomor Claim</TD>
					<td>:</td>
					<TD width="530" style="WIDTH: 530px"><asp:textbox onkeypress="return HtmlCharUniv(event)" id="txtClaimNo" onblur="HtmlCharBlur(txtName)"
							runat="server" MaxLength="50" Width="224px"></asp:textbox><asp:label id="lblSearchClaim" runat="server">
							<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:label></TD>
					<td width="15%"></td>
				</TR>
				<TR>
					<TD class="titleField">Lampiran Bukti</TD>
					<td>:</td>
					<TD width="530" style="WIDTH: 530px">&nbsp;<INPUT onkeypress="return false;" id="fileUpload" type="file" size="59" name="fileUpload"
							runat="server">
					</TD>
					<td width="15%"></td>
				</TR>
				<TR>
					<TD></TD>
					<td></td>
					<td width="530" style="WIDTH: 530px">
						<asp:textbox onkeypress="return HtmlCharUniv(event)" id="Textbox1" onblur="HtmlCharBlur(txtName)"
							runat="server" Width="480px" MaxLength="50" Rows="10" TextMode="MultiLine"></asp:textbox></td>
					<td width="15%"></td>
				</TR>
				<tr>
					<td></td>
					<td></td>
					<td width="530" align="left" style="WIDTH: 530px">
						<asp:button id="btnUpload" runat="server" Text="Upload"></asp:button><asp:button id="btnSimpan" runat="server" Text="Simpan"></asp:button>
						<asp:button id="btnBatal" runat="server" Width="56px" Text="Batal" CausesValidation="False"></asp:button>
					</td>
					<td width="15%"></td>
				</tr>
				<tr>
					<td></td>
					<td></td>
					<td width="530" align="left" style="WIDTH: 530px">
						<asp:datagrid id="dtgEvidence" runat="server" Width="100%" PageSize="25" BorderColor="#E0E0E0"
							CellPadding="3" BackColor="Gainsboro" AutoGenerateColumns="False" AllowCustomPaging="True"
							AllowPaging="True" AllowSorting="True">
							<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
							<AlternatingItemStyle ForeColor="Black" BackColor="#FDF1F2"></AlternatingItemStyle>
							<ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
							<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
							<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
							<Columns>
								<asp:BoundColumn Visible="False" DataField="id" HeaderText="id">
									<HeaderStyle Width="5%" CssClass="titleTableParts"></HeaderStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="No">
									<HeaderStyle Width="3%" CssClass="titleTableParts"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id="lblNo" runat="server"></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="ImageFileName" SortExpression="ImageFileName" HeaderText="File Name">
									<HeaderStyle Width="30%" CssClass="titleTableParts"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="CreatedBy" SortExpression="CreatedBy" HeaderText="Diupload oleh">
									<HeaderStyle Width="30%" CssClass="titleTableParts"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="CreatedTime" SortExpression="CreatedTime" HeaderText="Tgl Upload">
									<HeaderStyle Width="30%" CssClass="titleTableParts"></HeaderStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn>
									<HeaderStyle Width="15%" CssClass="titleTableParts"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center" Wrap=False></ItemStyle>
									<ItemTemplate>
										<asp:LinkButton id="lbtnView" runat="server" Text="Lihat" Width="20px" CausesValidation="False"
											CommandName="View">
											<img src="../images/detail.gif" border="0" alt="Lihat"></asp:LinkButton>
										<asp:LinkButton id="lbtnDelete" runat="server" Text="Hapus" Width="16px" CausesValidation="False"
											CommandName="Delete">
											<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
						</asp:datagrid>
					</td>
					
					<td width="15%"></td>
					
					
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
