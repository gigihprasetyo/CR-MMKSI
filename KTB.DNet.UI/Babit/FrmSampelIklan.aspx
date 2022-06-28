<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmSampelIklan.aspx.vb" Inherits="FrmSampelIklan" smartNavigation="False"%>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmSampelIklan</title>
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
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage" style="HEIGHT: 19px">BABIT - Upload Contoh Iklan</td>
				</tr>
				<tr>
					<td style="HEIGHT: 1px" background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD align="left">
						<TABLE id="Table2" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR>
								<TD class="titleField" width="24%">No Iklan</TD>
								<TD width="1%">:</TD>
								<td width="75%"><asp:textbox id="txtNoIklan" onkeypress="return alphaNumericExcept(event,'<>?*%$;')" onblur="omitSomeCharacter('txtNoIklan','<>?*%$;')"
										runat="server" Width="152px"></asp:textbox></td>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 27px" width="24%">Upload Iklan</TD>
								<TD style="HEIGHT: 27px" width="1%">:</TD>
								<td style="HEIGHT: 27px" width="75%"><INPUT onkeypress="return false;" id="FileUploadIklan" type="file" size="59" name="fileUpload"
										runat="server"></td>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 27px" width="24%">Upload Cara Penggunaan</TD>
								<TD style="HEIGHT: 27px" width="1%">:</TD>
								<TD style="HEIGHT: 27px" width="75%"><INPUT onkeypress="return false;" id="fileUploadPenggunaan" type="file" size="59" name="fileUpload"
										runat="server"></TD>
							</TR>
							<TR valign="top">
								<TD class="titleField" style="HEIGHT: 16px" width="24%">Keterangan</TD>
								<TD style="HEIGHT: 16px" width="1%">:</TD>
								<TD style="HEIGHT: 16px" width="75%"><asp:textbox id="txtKeterangan" runat="server" TextMode="MultiLine" Width="400px" Height="56px"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 27px" width="24%"></TD>
								<TD style="HEIGHT: 27px" width="1%"></TD>
								<td style="HEIGHT: 27px" width="75%">
									<asp:button id="btnSimpan" runat="server" width="60px" Text="Simpan"></asp:button>
									<asp:button id="btnBatal" runat="server" width="60px" Text="Batal" CausesValidation="False"></asp:button>
									<asp:button id="btnCari" runat="server" width="60px" Text="Cari" CausesValidation="False"></asp:button>
								</td>
							</TR>
						</TABLE>
						<asp:datagrid id="dtgSampelIklan" runat="server" Width="100%" AllowSorting="True" AllowPaging="True"
							AllowCustomPaging="True" AutoGenerateColumns="False" BackColor="Gainsboro" CellPadding="3"
							BorderColor="#E0E0E0" PageSize="10">
							<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
							<AlternatingItemStyle ForeColor="Black" BackColor="#FDF1F2"></AlternatingItemStyle>
							<ItemStyle ForeColor="Black" BackColor="White" VerticalAlign="Top"></ItemStyle>
							<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
							<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
							<Columns>
								<asp:TemplateColumn HeaderText="NO">
									<HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
									<ItemTemplate>
										<asp:Label ID="lblNo" Runat="server"></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="NoIklan" HeaderText="No Iklan">
									<HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
									<ItemTemplate>
										<asp:Label ID="lblNoIklan" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NoIklan") %>'>
										</asp:Label><br>
										<asp:LinkButton ID="lbtnDirection" Runat="server" CommandName="LihatPenggunaan">Cara Penggunaan</asp:LinkButton>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="Note" HeaderText="Keterangan">
									<HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
									<ItemTemplate>
										<asp:Label ID="lblNote" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Note") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Gambar">
									<HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
									<ItemTemplate>
										<asp:Image ID="imgIklan" Runat="server" Height="100px" Width="100px"></asp:Image>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn>
									<HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:LinkButton id="lbtnEdit" runat="server" Text="Ubah" CommandName="Edit" CausesValidation="False">
											<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
										<asp:LinkButton id="lbtnDelete" runat="server" Text="Hapus" Width="16px" CausesValidation="False"
											CommandName="Delete">
											<img src="../images/trash.gif" border="0" alt="Hapus" onclick="return confirm('Yakin ingin menghapus data ini?');"></asp:LinkButton>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn Visible="False">
									<ItemTemplate>
										<asp:Label ID="lblID" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
