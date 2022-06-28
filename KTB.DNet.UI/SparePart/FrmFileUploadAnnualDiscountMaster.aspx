<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmFileUploadAnnualDiscountMaster.aspx.vb" Inherits="FrmFileUploadAnnualDiscountMaster" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmFileUploadAnnualDiscountMaster</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language=javascript src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">ANNUAL DISCOUNT&nbsp;-&nbsp;Pengelolaan File Annual Discount
					</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD vAlign="top">
						<TABLE id="Table2" cellSpacing="1" cellPadding="1" width="100%" border="0">
							<TR>
								<TD class="titleField" width="24%">Nama Dokumen</TD>
								<TD width="1%">:</TD>
								<td width="75%"><asp:textbox onkeypress="return HtmlCharUniv(event)" id="txtProgramName" runat="server" Width="288px"
										MaxLength="40"></asp:textbox><asp:requiredfieldvalidator id="RequiredFieldValidator1" runat="server" ControlToValidate="txtProgramName" ErrorMessage="*"></asp:requiredfieldvalidator></td>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 23px">Nama File</TD>
								<TD style="HEIGHT: 23px">:</TD>
								<td style="HEIGHT: 23px">
									<P><asp:textbox onkeypress="return HtmlCharUniv(event)" id="txtFileName" onblur="HtmlCharBlur(txtFileName)"
											runat="server" Width="288px" MaxLength="50"></asp:textbox><asp:regularexpressionvalidator id="RegularExpressionValidator1" runat="server" ControlToValidate="txtFileName"
											ErrorMessage=".doc / .pdf / .xls" ValidationExpression="^(\w[\w].*)+(.doc|.DOC|.pdf|.PDF|.xls|.XLS)$"></asp:regularexpressionvalidator><asp:requiredfieldvalidator id="RequiredFieldValidator2" runat="server" ControlToValidate="txtFileName" ErrorMessage="*"></asp:requiredfieldvalidator></P>
								</td>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 24px">Keterangan</TD>
								<TD style="HEIGHT: 24px">:</TD>
								<TD style="HEIGHT: 24px">
									<P><asp:textbox onkeypress="return HtmlCharUniv(event)" id="txtRemark" onblur="HtmlCharBlur(txtRemark)"
											runat="server" Width="288px" MaxLength="100"></asp:textbox></P>
								</TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 15px">Tipe</TD>
								<TD style="HEIGHT: 15px">:</TD>
								<TD style="HEIGHT: 15px"><asp:dropdownlist id="ddlTipe" runat="server" Width="96px" AutoPostBack="True">
										<asp:ListItem Value="Umum">Umum</asp:ListItem>
										<asp:ListItem Value="Per Dealer" Selected="True">Per Dealer</asp:ListItem>
										<asp:ListItem Value="Per Group">Per Group</asp:ListItem>
									</asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD class="titleField"></TD>
								<TD></TD>
								<td>
									<P><asp:button id="btnSimpan" runat="server" width="60px" Text="Simpan"></asp:button>&nbsp;
										<asp:button id="btnBatal" runat="server" width="60px" Text="Batal" CausesValidation="False"></asp:button></P>
								</td>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD vAlign="top">
						<div id="div1" style="OVERFLOW: auto; HEIGHT: 320px"><asp:datagrid id="dtgUploadFile" runat="server" Width="760px" AutoGenerateColumns="False" GridLines="Horizontal"
								CellPadding="3" BackColor="Gainsboro" BorderWidth="0px" BorderStyle="None" BorderColor="#CDCDCD" AllowCustomPaging="True" AllowPaging="True"
								AllowSorting="True" PageSize="25" ForeColor="GhostWhite" CellSpacing="1" Font-Names="MS Reference Sans Serif">
								<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
								<AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
								<ItemStyle BackColor="White"></ItemStyle>
								<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
										<HeaderStyle Width="3%" CssClass="titleTableParts"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle Width="3%" CssClass="titleTableParts"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblNo" Runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="ProgramName" SortExpression="ProgramName" HeaderText="Nama Dokumen">
										<HeaderStyle Width="20%" CssClass="titleTableParts"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="FileName" SortExpression="FileName" HeaderText="Nama File">
										<HeaderStyle Width="20%" CssClass="titleTableParts"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Remark" SortExpression="Remark" HeaderText="Keterangan">
										<HeaderStyle Width="30%" CssClass="titleTableParts"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn SortExpression="Tipe" HeaderText="Tipe">
										<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="CreatedTime" SortExpression="CreatedTime" HeaderText="Tanggal Dokumen"
										DataFormatString="{0:dd/MM/yyyy}">
										<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn>
										<HeaderStyle Width="7%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="btnLihat" runat="server" Text="Lihat" CausesValidation="False" CommandName="View"
												Visible="False">
												<img src="../images/detail.gif" border="0" alt="Lihat"></asp:LinkButton>
											<asp:LinkButton id="btnUbah" runat="server" Text="Ubah" CausesValidation="False" CommandName="Edit">
												<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
											<asp:LinkButton id="btnHapus" runat="server" Text="Hapus" CausesValidation="False" CommandName="Delete">
												<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></div>
					</TD>
				</TR>
				<TR>
					<TD height="40"></TD>
				</TR>
				<TR>
					<TD></TD>
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
