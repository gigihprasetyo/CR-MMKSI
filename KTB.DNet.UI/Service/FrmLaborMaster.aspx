<%@ Import Namespace="KTB.DNet.Domain"%>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmLaborMaster.aspx.vb" Inherits="FrmLaborMaster" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmLaborMaster</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript">
			function ShowPosisi()
			{
				showPopUp('../PopUp/PopUpPositionCode.aspx','',500,760,PosisiSelection);
			}
			function PosisiSelection(selectedPosisi)
			{
				var txtLaborCode = document.getElementById("txtLaborCode");
				txtLaborCode.value = selectedPosisi;				
			}
			
			function ShowKerja()
			{
				showPopUp('../PopUp/PopUpWorkCode.aspx','',500,760,WorkSelection);
			}
			function WorkSelection(selectedWork)
			{
				var txtWorkCode = document.getElementById("txtWorkCode");
				txtWorkCode.value = selectedWork;				
			}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">WSC - Kode Posisi</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD>
						<TABLE id="Table1" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR>
								<TD class="titleField" width="24%"><asp:label id="Label1" runat="server">Kode Tipe Kendaraan</asp:label></TD>
								<TD width="1%"><asp:label id="Label4" runat="server">:</asp:label></TD>
								<TD width="75%"><asp:dropdownlist id="ddlVehicleCode" runat="server" Width="90px"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD class="titleField"><asp:label id="Label2" runat="server">Kode Posisi</asp:label></TD>
								<TD><asp:label id="Label5" runat="server">:</asp:label></TD>
								<TD><asp:textbox onkeypress="return HtmlCharUniv(event)" onblur="HtmlCharUniv(txtLaborCode)" id="txtLaborCode"
										runat="server" Width="90" MaxLength="10"></asp:textbox><asp:requiredfieldvalidator id="RequiredFieldValidator1" runat="server" ControlToValidate="txtLaborCode" ErrorMessage="*"></asp:requiredfieldvalidator>
									<asp:label id="lblSearchPosisi" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField"><asp:label id="Label3" runat="server">Kode Kerja</asp:label></TD>
								<TD><asp:label id="Label6" runat="server">:</asp:label></TD>
								<TD><asp:textbox onkeypress="return HtmlCharUniv(event)" onblur="HtmlCharUniv(txtWorkCode)" id="txtWorkCode"
										runat="server" Width="90px" MaxLength="6"></asp:textbox><asp:requiredfieldvalidator id="RequiredFieldValidator2" runat="server" ControlToValidate="txtWorkCode" ErrorMessage="*"></asp:requiredfieldvalidator>
									<asp:label id="lblSearchKerja" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:label></TD>
							</TR>
							<TR>
								<TD></TD>
								<TD></TD>
								<TD><asp:button id="btnSave" runat="server" Width="60" Text="Simpan"></asp:button><asp:button id="btnCancel" runat="server" Width="60" Text="Batal" CausesValidation="False"></asp:button><asp:button id="btnCari" runat="server" Width="56px" Text="Cari" CausesValidation="False"></asp:button></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD vAlign="top">
						<div id="div1" style="OVERFLOW: auto; HEIGHT: 370px"><asp:datagrid id="dgLabor" runat="server" Width="100%" PageSize="100" AllowPaging="True" AutoGenerateColumns="False"
								AllowCustomPaging="True" AllowSorting="True" BackColor="#CDCDCD" BorderColor="#CDCDCD" CellSpacing="1" BorderWidth="0px" CellPadding="3">
								<AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
								<ItemStyle BackColor="White"></ItemStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
										<HeaderStyle ForeColor="White" Width="5%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<FooterStyle HorizontalAlign="Center"></FooterStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle HorizontalAlign="Center" ForeColor="White" Width="5%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblNo" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="VechileTypeCode" HeaderText="Kode Tipe Kendaraan">
										<HeaderStyle ForeColor="White" Width="10%" CssClass="titleTableService"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblVehicleCode" runat="server" Text='<%# CType(Container.DataItem, LaborMaster).VechileType.VechileTypeCode %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Deskripsi Tipe Kendaraan">
										<HeaderStyle ForeColor="White" Width="30%" CssClass="titleTableService"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblDescription" runat="server" Text='<%# CType(Container.DataItem, LaborMaster).VechileType.Description %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="LaborCode" HeaderText="Kode Posisi">
										<HeaderStyle ForeColor="White" Width="20%" CssClass="titleTableService"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id=lblKodePosisi runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.LaborCode") %>'>
											</asp:Label>
										</ItemTemplate>
										<EditItemTemplate>
											<asp:TextBox id=TextBox1 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.LaborCode") %>'>
											</asp:TextBox>
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="WorkCode" HeaderText="Kode Kerja">
										<HeaderStyle ForeColor="White" Width="20%" CssClass="titleTableService"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id=lblKodeKerja runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.WorkCode") %>'>
											</asp:Label>
										</ItemTemplate>
										<EditItemTemplate>
											<asp:TextBox id=TextBox2 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.WorkCode") %>'>
											</asp:TextBox>
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn>
										<HeaderStyle HorizontalAlign="Center" Width="10%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="lnkView" runat="server" CausesValidation="False" CommandName="View">
												<img src="../images/detail.gif" border="0" alt="Lihat Detail"></asp:LinkButton>&nbsp;
											<asp:LinkButton id="lnkEdit" runat="server" CausesValidation="False" CommandName="Edit">
												<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>&nbsp;
											<asp:LinkButton id="lnkDelete" runat="server" CausesValidation="False" CommandName="Delete">
												<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
										</ItemTemplate>
										<FooterStyle HorizontalAlign="Center"></FooterStyle>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></div>
					</TD>
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
