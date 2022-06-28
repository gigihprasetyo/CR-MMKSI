<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmBuletinRead.aspx.vb" Inherits="FrmBuletinRead" smartNavigation="False"%>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Form User Info</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/FormFunctions.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" width="100%">
				<tr>
					<td class="titlePage">BULETIN &amp; MANUAL - DAFTAR BULLETIN
					</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td></td>
				</tr>
				<tr>
					<td>
						<table cellSpacing="1" cellPadding="2" width="100%">
							<tr>
								<td class="titleField" width="30%">Kategori&nbsp;
								</td>
								<td><asp:dropdownlist id="DdlCategory" runat="server" AutoPostBack="True"></asp:dropdownlist></td>
							</tr>
							<tr>
								<td class="titleField" style="HEIGHT: 20px" width="30%">Sub Kategori&nbsp;
								</td>
								<td style="HEIGHT: 20px"><asp:dropdownlist id="DdlSubCategory" runat="server"></asp:dropdownlist></td>
							</tr>
							<tr>
								<td class="titleField" width="30%">Kata Kunci&nbsp;
								</td>
								<td><asp:textbox id="txtKeyWords" runat="server" Width="200px" onkeypress="return alphaNumericExcept(event,'<>?*%$;')"
										onblur="omitSomeCharacter('txtKeyWords','<>?*%$;')"></asp:textbox></td>
							</tr>
							<tr>
								<td class="titleField" width="30%">Deskripsi&nbsp;
								</td>
								<td><asp:textbox id="txtDescription" runat="server" Width="200px" onkeypress="return alphaNumericExcept(event,'<>?*%$;')"
										onblur="omitSomeCharacter('txtDescription','<>?*%$;')"></asp:textbox></td>
							</tr>
							<TR>
								<TD class="titleField" width="30%">Judul</TD>
								<TD>
									<asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" id="txtJudul" onblur="omitSomeCharacter('txtDescription','<>?*%$;')"
										runat="server" Width="200px"></asp:textbox></TD>
							</TR>
							<tr>
								<td class="titleField" width="30%">&nbsp;
								</td>
								<td><asp:button id="Button1" runat="server" Text="Cari" Width="56px"></asp:button></td>
							</tr>
						</table>
						&nbsp;
						<asp:datagrid id="dgBuletinList" runat="server" Width="100%" AllowSorting="True" AutoGenerateColumns="False"
							BorderColor="Gainsboro" BorderWidth="0px" BackColor="#CDCDCD" CellPadding="3" CellSpacing="1"
							AllowCustomPaging="True" AllowPaging="True">
							<AlternatingItemStyle BackColor="#FDF1F2"></AlternatingItemStyle>
							<ItemStyle BackColor="White"></ItemStyle>
							<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Blue"></HeaderStyle>
							<Columns>
								<asp:BoundColumn Visible="False" DataField="ID" ReadOnly="True" HeaderText="ID">
									<HeaderStyle Width="5%" CssClass="titleTableGeneral"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="FileName" HeaderText="FileName"></asp:BoundColumn>
								<asp:TemplateColumn HeaderText="No">
									<HeaderStyle Width="5%" CssClass="titleTableGeneral"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="Title" SortExpression="Title" HeaderText="Judul">
									<HeaderStyle Width="25%" CssClass="titleTableGeneral"></HeaderStyle>
									<ItemStyle HorizontalAlign="Left"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="Description" SortExpression="Description" HeaderText="Deskripsi">
									<HeaderStyle Width="30%" CssClass="titleTableGeneral"></HeaderStyle>
									<ItemStyle HorizontalAlign="Left"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="UploadDate" SortExpression="UploadDate" HeaderText="Tanggal Upload" DataFormatString="{0:dd/MM/yyyy}">
									<HeaderStyle Width="15%" CssClass="titleTableGeneral"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="MemberAssigned" HeaderText="Jumlah Member">
									<HeaderStyle Width="5%" CssClass="titleTableGeneral"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="MemberRead" HeaderText="Jumlah Pembaca">
									<HeaderStyle Width="5%" CssClass="titleTableGeneral"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="PercentageMemberRead" HeaderText="Persentase Pembaca">
									<HeaderStyle Width="5%" CssClass="titleTableGeneral"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Aksi">
									<HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
									<ItemStyle Wrap="False" HorizontalAlign="Left"></ItemStyle>
									<ItemTemplate>
										<asp:LinkButton id="lbtnDownload" CommandName="Download" text="Download" Runat="server" CausesValidation="False" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
											<img border="0" src="../images/download.gif" alt="Download" style="cursor:hand"></asp:LinkButton>
										<asp:Label id="lbtnIsRead" Runat="server" tooltip="Sudah didownload">
											<img border="0" src="../images/green.gif"></asp:Label>
										<asp:Label id="lbtnIsNotRead" tooltip="Belum Didownload" Runat="server">
											<img border="0" src="../images/red.gif"></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></td>
				</tr>
			</table>
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
