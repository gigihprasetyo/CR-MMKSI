<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmLogisticPrice.aspx.vb" Inherits="FrmLogisticPrice"  smartNavigation="False" %>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmLogisticPrice</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
        <script language="javascript" src="../WebResources/InputValidation.js"></script>
		<LINK rel="stylesheet" type="text/css" href="../WebResources/stylesheet.css">
        <script language="javascript">

            function ShowPPModelSelection() {
                showPopUp('../General/../PopUp/PopUpVechileTypeModel.aspx', '', 520, 750, ModelSelection);
            }

            function ModelSelection(selectedDealer) {
                var txtModelSelection = document.getElementById("txtModel");
                txtModelSelection.value = selectedDealer;
            }

            function ShowPPDealerSelection() {
                showPopUp('../General/../PopUp/PopUpDealerSelection.aspx', '', 500, 760, DealerSelection);
            }

            function DealerSelection(selectedDealer) {
                var txtDealerSelection = document.getElementById("txtKodeDealer");
                txtDealerSelection.value = selectedDealer;
            }

		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" border="0" cellSpacing="0" cellPadding="0" width="100%">
				<tr>
					<td class="titlePage">UMUM - Master Logistic Price</td>
				</tr>
				<tr>
					<td height="1" background="../images/bg_hor.gif"><IMG border="0" src="../images/bg_hor.gif" height="1"></td>
				</tr>
				<tr>
					<td height="10"><IMG border="0" src="../images/dot.gif" height="1"></td>
				</tr>
				<TR>
					<TD vAlign="top" align="left">
						<TABLE id="Table2" border="0" cellSpacing="1" cellPadding="2" width="100%">
                            <TR runat="server" id="trDealer">
								<TD class="titleField">Dealer</TD>
								<TD>:</TD>
								<TD>
									<asp:textbox onblur="omitSomeCharacter('txtKodeDealer','<>?*%$')" style="Z-INDEX: 0" id="txtKodeDealer" Width="150px"
										onkeypress="return alphaNumericExcept(event,'<>?*%$')" runat="server"></asp:textbox>
									<asp:label style="Z-INDEX: 0" id="lblSearchDealer" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:label></TD>
								<TD></TD>
								<TD></TD>
								<TD></TD>
							</TR>
                            <TR>
								<TD class="titleField" width="24%">Region</TD>
								<TD width="1%">:</TD>
								<td width="75%"><asp:textbox onkeypress="return HtmlCharUniv(event)" onblur="HtmlCharBlur(txtRegion)" id="txtRegion"
										runat="server" MaxLength="50" Width="150px"></asp:textbox>&nbsp;</td>
							    </TR>
							 <TR runat="server" id="trModel">
								<TD class="titleField">Model</TD>
								<TD>:</TD>
								<TD>
									<asp:textbox onblur="omitSomeCharacter('txtModel','<>?*%$')" style="Z-INDEX: 0" id="txtModel" Width="150px"
										onkeypress="return alphaNumericExcept(event,'<>?*%$')" runat="server"></asp:textbox>
									<asp:label style="Z-INDEX: 0" id="lblSearchModel" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:label></TD>
								<TD></TD>
								<TD></TD>
								<TD></TD>
							</TR>
                           <%-- <TR>
								<TD class="titleField" style="WIDTH: 114px">Status</TD>
								<TD style="WIDTH: 3px">:</TD>
								<TD style="WIDTH: 278px">
									<asp:dropdownlist id="ddlStatus" runat="server" Width="96px"></asp:dropdownlist></TD>
                                </TR>--%>
                                <TR>
								<TD class="titleField" width="24%">Mulai Berlaku</TD>
								<TD width="1%">:</TD>
								<TD width="25%"><asp:checkbox id="chkAll" runat="server" Text="Semua"></asp:checkbox></TD>
							    </TR>
                            <TR>
                            
                                <TD></TD>
								<TD></TD>
                                <TD>
                                <cc1:inticalendar id="calCalendar" runat="server"></cc1:inticalendar></TD>
                             </TR>
							<tr>
								<td></td>
								<td></td>
								<td><asp:button id="btnSearch" runat="server" Width="60px" Text=" Cari "></asp:button></td>
							</tr>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD vAlign="top">
						<div style="HEIGHT: 320px; OVERFLOW: auto" id="div1"><asp:datagrid id="dgLogisticPrice" runat="server" Width="100%" AutoGenerateColumns="False" BorderColor="#E0E0E0"
								BorderStyle="None" BorderWidth="0px" BackColor="#CDCDCD" CellPadding="3" GridLines="Horizontal" CellSpacing="1"
								AllowSorting="True" AllowPaging="True" AllowCustomPaging="True" PageSize="25">
								<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
								<AlternatingItemStyle VerticalAlign="Top" BackColor="#F5F1EE"></AlternatingItemStyle>
								<ItemStyle ForeColor="#4A3C8C" VerticalAlign="Top" BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
								<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
										<HeaderStyle Width="1%" CssClass="titleTableSales"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle Width="1%" CssClass="titleTableSales"></HeaderStyle>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="RegionCode" SortExpression="RegionCode" HeaderText="Region Code">
										<HeaderStyle Width="7%" CssClass="titleTableSales"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="RegionDescription" SortExpression="RegionDescription" ReadOnly="True" HeaderText="Region">
										<HeaderStyle Width="9%" CssClass="titleTableSales"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="SAPModel" SortExpression="SAPModel" HeaderText="Model">
										<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Status" SortExpression="Status" ReadOnly="True" HeaderText="Status" Visible="false">
										<HeaderStyle Width="6%" CssClass="titleTableSales"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
                                    <asp:BoundColumn DataField="EffectiveDate" SortExpression="EffectiveDate" ReadOnly="True" HeaderText="Mulai Berlaku" DataFormatString="{0:dd/MM/yyyy}"> 
										<HeaderStyle Width="7%" CssClass="titleTableSales"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
                                    <asp:BoundColumn DataField="LogisticPrice" SortExpression="LogisticPrice" ReadOnly="True" HeaderText="Logistic Price" DataFormatString="{0:#,##0}">
										<HeaderStyle Width="9%" CssClass="titleTableSales"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
                                    <asp:BoundColumn DataField="TotalPPn" SortExpression="TotalPPn" ReadOnly="True" HeaderText="PPn" DataFormatString="{0:#,##0}">
										<HeaderStyle Width="7%" CssClass="titleTableSales"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
                                    <asp:BoundColumn DataField="TotalLogisticPrice" SortExpression="TotalLogisticPrice" ReadOnly="True" HeaderText="Total Logistic Price" DataFormatString="{0:#,##0}">
										<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
								
								</Columns>
								<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></div>
					</TD>
				</TR>
				<TR>
					<TD height="40" align="left"><asp:button id="btnDnLoad" runat="server" Width="60px" Text="Download" Enabled="False"></asp:button></TD>
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
