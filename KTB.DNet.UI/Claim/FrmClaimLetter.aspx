<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmClaimLetter.aspx.vb" Inherits="FrmClaimLetter" smartNavigation="False"%>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmClaimLetter</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript">
		function checkVal()
		{
		var txtCCListVal = document.getElementById("txtCCList").value; 
		if (txtCCListVal == null || txtCCListVal.length == 0) 
		{
			alert("Isi Daftar CC terlebih Dahulu!");
        return false;
		} 
		else 
		{
        return true;
	    }
		}	
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="1" cellPadding="2" width="100%" border="0">
				<TR>
					<TD class="titlePage" colSpan="3">
						<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="titlePage">CLAIM - Paramater Surat</td>
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
				<tr>
					<td colSpan="3"><b>DAFTAR CC :</b></td>
				</tr>
				<TR>
					<TD class="titleField" width="20%"><asp:label id="Label1" runat="server">Daftar</asp:label></TD>
					<td width="1%">:</td>
					<TD width="79%"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" id="txtCCList" onblur="omitSomeCharacter('txtCCList','<>?*%$;')"
							runat="server" Width="320px" MaxLength="50"></asp:textbox><asp:button id="btnSimpanCCofLetter" runat="server" Text="Simpan"></asp:button><asp:button id="btnBatalCCofLetter" runat="server" Width="56px" Text="Batal" CausesValidation="False"></asp:button></TD>
				</TR>
				<TR>
					<TD colSpan="3">
						<DIV id="div1" style="OVERFLOW: auto; HEIGHT: 300px" DESIGNTIMEDRAGDROP="245"><asp:datagrid id="dtgClaimCCofLetter" runat="server" Width="100%" AllowSorting="True" AllowPaging="True"
								AllowCustomPaging="True" AutoGenerateColumns="False" BackColor="Gainsboro" CellPadding="3" BorderColor="#E0E0E0">
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
										<ItemStyle HorizontalAlign=Center></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblNo" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="CCList" SortExpression="CCList" HeaderText="Daftar CC">
										<HeaderStyle Width="40%" CssClass="titleTableParts"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn>
										<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="lbtnViewCCofLetter" runat="server" Width="20px" Text="Lihat" CausesValidation="False"
												CommandName="View">
												<img src="../images/detail.gif" border="0" alt="Lihat"></asp:LinkButton>
											<asp:LinkButton id="lbtnEditCCofLetter" runat="server" Width="20px" Text="Ubah" CausesValidation="False"
												CommandName="Edit">
												<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
											<asp:LinkButton id="lbtnDeleteCCofLetter" runat="server" Width="16px" Text="Hapus" CausesValidation="False"
												CommandName="Delete">
												<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></DIV>
					</TD>
				</TR>
				<tr>
					<td colSpan="3"><b>PENANDA TANGAN :</b></td>
				</tr>
				<TR>
					<TD class="titleField" width="20%"><asp:label id="Label3" runat="server">Nama</asp:label></TD>
					<td width="1%">:</td>
					<TD width="79%"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" id="txtName" onblur="omitSomeCharacter('txtName','<>?*%$;')"
							runat="server" Width="312px" MaxLength="50"></asp:textbox></TD>
				</TR>
				<TR>
					<TD class="titleField" width="20%"><asp:label id="Label4" runat="server">Jabatan</asp:label></TD>
					<td width="1%">:</td>
					<TD width="79%"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" id="txtPosition" onblur="omitSomeCharacter('txtPosition','<>?*%$;')"
							runat="server" Width="312px" MaxLength="50"></asp:textbox></TD>
				</TR>
				<TR>
					<TD class="titleField" width="20%"></TD>
					<td width="1%"></td>
					<TD width="79%"><asp:button id="btnSimpanSignofLetter" runat="server" Text="Simpan"></asp:button><asp:button id="btnBatalSignofLetter" runat="server" Width="56px" Text="Batal" CausesValidation="False"></asp:button></TD>
				</TR>
				<TR>
					<TD colSpan="3">
						<DIV id="div2" style="OVERFLOW: auto;  HEIGHT: 198px" DESIGNTIMEDRAGDROP="245"><asp:datagrid id="dtgClaimSignOfLetter" runat="server" Width="100%" AllowSorting="False" AllowPaging="True"
								AllowCustomPaging="True" AutoGenerateColumns="False" BackColor="Gainsboro" CellPadding="3" BorderColor="#E0E0E0" PageSize="5">
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
										<HeaderStyle Width="5%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign=Center></ItemStyle>
										<ItemTemplate>
											<asp:Label id="Label2" runat="server"></asp:Label>
										</ItemTemplate>
										<EditItemTemplate>
											<asp:TextBox id="Textbox2" runat="server"></asp:TextBox>
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="Name" SortExpression="Name" HeaderText="Nama">
										<HeaderStyle Width="20%" CssClass="titleTableParts"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Position" SortExpression="Position" HeaderText="Jabatan">
										<HeaderStyle Width="20%" CssClass="titleTableParts"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn>
										<HeaderStyle Width="30%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="lbtnViewSignofLetter" runat="server" Text="Lihat" Width="20px" CausesValidation="False"
												CommandName="View">
												<img src="../images/detail.gif" border="0" alt="Lihat"></asp:LinkButton>
											<asp:LinkButton id="lbtnEditSignofLetter" runat="server" Text="Ubah" Width="20px" CausesValidation="False"
												CommandName="Edit">
												<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
											<asp:LinkButton id="lbtnDeleteSignofLetter" runat="server" Text="Hapus" Width="16px" CausesValidation="False"
												CommandName="Delete">
												<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></DIV>
					</TD>
				</TR>
				<!-- Gak jadi Dipake...
				<tr>
					<TD colspan="3">Header dan Footer</TD>
				</tr>
				<tr>
					<TD class="titleField" width="20%">
						<asp:label id="Label5" runat="server">Claim Header Letter</asp:label></TD>
					<td width="1%">:</td>
					<TD width="79%"><asp:textbox onkeypress="return HtmlCharUniv(event)" id="txtheader" onblur="HtmlCharBlur(txtName)"
							runat="server" MaxLength="50" Width="408px"></asp:textbox>
					</TD>
				</tr>
				<tr>
					<TD class="titleField" width="20%">
						<asp:label id="Label6" runat="server">Claim Footer Letter</asp:label></TD>
					<td width="1%">:</td>
					<TD width="79%"><asp:textbox onkeypress="return HtmlCharUniv(event)" id="txtfooter" onblur="HtmlCharBlur(txtName)"
							runat="server" MaxLength="50" Width="408px"></asp:textbox>
					</TD>
				</tr>
				<tr>
					<TD class="titleField" width="20%">
					<td colspan="2">
						<asp:button id="Button1" runat="server" Text="Simpan"></asp:button>
					</td>
				</tr>
				--></TABLE>
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
