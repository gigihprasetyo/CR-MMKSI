<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmDisplayAnnualDiscount.aspx.vb" Inherits="FrmDisplayAnnualDiscount" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmDisplayAnnualDiscount</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language=javascript src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript">
		function ShowPPDealerSelection()
		{
			showPopUp('../General/../PopUp/PopUpDealerSelection.aspx','',500,760,DealerSelection);
		}
		
		function DealerSelection(selectedDealer)
		{
			var txtDealerSelection = document.getElementById("txtKodeDealer");
			txtDealerSelection.value = selectedDealer;			
		}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="1" cellPadding="1" width="100%" border="0">
				<tr>
					<td colSpan="3">
						<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="titlePage">ANNUAL DISCOUNT - Daftar&nbsp;File Annual Discount</td>
							</tr>
							<tr>
								<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
							</tr>
							<tr>
								<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
							</tr>
						</TABLE>
					</td>
				</tr>
				<TR>
					<TD class="titleField" width="25%" colSpan="3">
						<div id="div1" style="OVERFLOW: auto; HEIGHT: 400px"><asp:datagrid id="dgDaftarAnnual" runat="server" Width="100%" AutoGenerateColumns="False" BorderColor="#CDCDCD"
								CellSpacing="1" BorderWidth="0px" CellPadding="3" BackColor="#CDCDCD" AllowCustomPaging="True" PageSize="25" AllowPaging="True" AllowSorting="True">
								<AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
								<ItemStyle BackColor="White"></ItemStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID"></asp:BoundColumn>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle ForeColor="White" Width="3%" CssClass="titleTableParts"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblNo" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="ProgramName" HeaderText="Nama Dokumen">
										<HeaderStyle ForeColor="White" Width="30%" CssClass="titleTableParts"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id=lblProgramName runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ProgramName") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="FileName" SortExpression="FileName" HeaderText="Nama File">
										<HeaderStyle ForeColor="White" Width="30%" CssClass="titleTableParts"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Remark" SortExpression="Remark" HeaderText="Keterangan">
										<HeaderStyle ForeColor="White" Width="30%" CssClass="titleTableParts"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="Tipe" SortExpression="Tipe" HeaderText="Tipe">
										<HeaderStyle ForeColor="White" Width="7%" CssClass="titleTableParts"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn>
										<HeaderStyle Width="5%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="LinkButton1" runat="server" CommandName="Download" CausesValidation="False">
												<img src="../images/download.gif" border="0" alt="Download"></asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></div>
					</TD>
				</TR>
			</TABLE>
			<TABLE id="Table2" cellSpacing="1" cellPadding="1" width="100%" border="0">
				<TR>
					<TD style="WIDTH: 90px">&nbsp;</TD>
					<TD style="WIDTH: 79px"></TD>
					<TD></TD>
				</TR>
				<TR>
					<TD class="titlePage" colSpan="3">
						<TABLE id="Table3" cellSpacing="1" cellPadding="1" width="400" bgColor="#cdcdcd" border="0">
							<TR id="opClient" runat="server">
								<TD bgColor="white">
									<TABLE id="Table4" cellSpacing="1" cellPadding="1" width="100%" border="0">
										<TR>
											<TD class="titleTableParts" colSpan="5" height="20">ANNUAL DISCOUNT - 
												Daftar&nbsp;Item&nbsp;Annual Discount</TD>
										</TR>
										<TR>
											<TD><STRONG>Periode</STRONG></TD>
											<TD><asp:dropdownlist id="ddlValidateFrom" runat="server" AutoPostBack="True"></asp:dropdownlist></TD>
											<TD>s/d</TD>
											<td><asp:textbox id="txtValidateTo" runat="server" ReadOnly="True"></asp:textbox><asp:requiredfieldvalidator id="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="txtValidateTo"></asp:requiredfieldvalidator></td>
											<TD><asp:button id="btnRetrive" runat="server" Width="64px" Text="Cari"></asp:button></TD>
										</TR>
									</TABLE>
								</TD>
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
