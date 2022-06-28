<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmCustomerDealerList.aspx.vb" Inherits="FrmCustomerDealerList" smartNavigation="False" %>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmCustomerRequestStatusList</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript" src="../WebResources/FormFunctions.js"></script>
		<script language="javascript">
		function ShowPPDealerSelection()
		{
			showPopUp('../SparePart/../PopUp/PopUpDealerSelectionOne.aspx','',500,760,DealerSelection);
		}
		function DealerSelection(selectedDealer)
		{
			var tempParam = selectedDealer.split(';');
			var txtDealer = document.getElementById("txtDealer");
			txtDealer.value = tempParam[0];				
		}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">KONSUMEN - Daftar Konsumen - Dealer</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD align="left">
					</TD>
				</TR>
				<TR>
					<TD vAlign="top">
						<div id="div1" style="OVERFLOW: auto; HEIGHT: 310px"><asp:datagrid id="dtgCustomerRequest" runat="server" Width="100%" AllowSorting="True" CellSpacing="1"
								AutoGenerateColumns="False" BorderColor="#CDCDCD" BorderStyle="None" BorderWidth="0px" AllowPaging="True" AllowCustomPaging="True" BackColor="#CDCDCD"
								CellPadding="3" GridLines="None" PageSize="25">
								<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
								<AlternatingItemStyle ForeColor="Black" BackColor="White"></AlternatingItemStyle>
								<ItemStyle ForeColor="Black" VerticalAlign="Top" BackColor="#FDF1F2"></ItemStyle>
								<HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#CC3333"></HeaderStyle>
								<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
								<Columns>
									<asp:TemplateColumn Visible="False" HeaderText="ID">
										<HeaderStyle Width="3%" CssClass="titleTableMrk"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id=lblID runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
											</asp:Label>
										</ItemTemplate>
										<EditItemTemplate>
											<asp:TextBox id=TextBox1 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
											</asp:TextBox>
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Kode Dealer">
										<HeaderStyle Width="30%" CssClass="titleTableMrk"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblDealerCode" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Nama Dealer">
										<HeaderStyle Width="30%" CssClass="titleTableMrk"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblDealerName" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Status">
										<HeaderStyle Width="10%" CssClass="titleTableMrk"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblStatus" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.RowStatus") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn>
										<HeaderStyle Width="1%" CssClass="titleTableMrk"></HeaderStyle>
										<ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id=lbtnActivate runat="server" Text="Aktifkan" CommandName="AKTIVEKAN" CausesValidation="False" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
												<img src="../images/aktif.gif" border="0" alt="Aktifkan"></asp:LinkButton>
											<asp:LinkButton id=lbtnNotActivate runat="server" Text="Non Aktifkan" CommandName="NONAKTIVEKAN" CausesValidation="False" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
												<img src="../images/in-aktif.gif" border="0" alt="Non Aktifkan"></asp:LinkButton>
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
					<TD>
						<asp:Button id="Button1" runat="server" Text="Kembali"></asp:Button></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
