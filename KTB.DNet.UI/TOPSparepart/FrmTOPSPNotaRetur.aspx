<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmTOPSPNotaRetur.aspx.vb" Inherits=".FrmTOPSPNotaRetur" %>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>TOP Sparepart - Nota Retur</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<LINK media="print" href="../WebResources/printstyle.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript" src="../WebResources/FormFunctions.js"></script>
		<script language="javascript">
		    function ShowPPDealerSelection() {
		        showPopUp('../SparePart/../PopUp/PopUpDealerSelection.aspx', '', 500, 760, DealerSelection);
		    }

		    function DealerSelection(selectedDealer) {
		        var txtDealer = document.getElementById("txtDealerCode");
		        txtDealer.value = selectedDealer;
		    }
   		
		</script>
	</HEAD>
    <body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<div style="PADDING-BOTTOM:5px;PADDING-LEFT:5px;PADDING-RIGHT:5px;PADDING-TOP:5px" class="titlePage">
				<b>Kwitansi Bulanan dan Nota Retur Spare Part</b>
			</div>
			<TABLE id="Table11" cellSpacing="1" cellPadding="2" border="0">
				<tr>
					<td style="WIDTH: 190px">Kode Dealer</td>
					<TD style="WIDTH: 5px">:</TD>
					<td style="WIDTH: 500px">
						<asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$')" id="txtDealerCode" onblur="omitSomeCharacter('txtDealerCode','<>?*%$')"
							runat="server" Width="160px"></asp:textbox><asp:label id="lblSearchDealer" runat="server">
							<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:label>
					</td>
				</tr>				
                <tr>
                    <td style="width: 190px">Periode</td>
                    <td style="width: 5px">:</td>
                    <td style="width: 500px">
                        <asp:DropDownList ID="ddlPeriode" runat="server" Width="120px"></asp:DropDownList>
                        <asp:DropDownList ID="ddlPeriodeYear" runat="server" Width="120px"></asp:DropDownList>                       
                    </td>
                </tr>
				<TR>
					<TD style="WIDTH: 190px">Tipe Dokumen</TD>
					<TD style="WIDTH: 5px">:</TD>
					<TD style="WIDTH: 500px">
						<asp:dropdownlist id="ddlTipeDokumen" runat="server" Width="120px"></asp:dropdownlist>
					</TD>
				</TR>
                <tr>
					<td style="WIDTH: 190px">No Kwitansi</td>
					<TD style="WIDTH: 5px">:</TD>
					<td style="WIDTH: 500px">
						<asp:textbox id="txtNoKwitansi" runat="server" Width="160px"></asp:textbox>
					</td>
				</tr>
				<TR>
					<TD style="WIDTH: 190px"></TD>
					<TD style="WIDTH: 5px"></TD>
					<TD style="WIDTH: 500px">
						<P><asp:button id="btnSearch" runat="server" width="60px" Text="Cari"></asp:button></P>
					</TD>
				</TR>
			</TABLE>
			<div id="div1" style="HEIGHT: 300px; OVERFLOW: auto">
				<asp:datagrid id="dtgNotaRetur" runat="server" Width="100%" PageSize="25" GridLines="None" CellPadding="3"
					BackColor="#CDCDCD" AllowCustomPaging="True" AllowPaging="True" BorderWidth="0px" BorderStyle="None"
					BorderColor="#CDCDCD" AutoGenerateColumns="False" CellSpacing="1" AllowSorting="True">
					<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
					<AlternatingItemStyle ForeColor="Black" BackColor="White"></AlternatingItemStyle>
					<ItemStyle ForeColor="Black" BackColor="#FDF1F2"></ItemStyle>
					<HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#CC3333"></HeaderStyle>
					<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
					<Columns>
						<asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
							<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
						</asp:BoundColumn>
						<asp:TemplateColumn HeaderText="No">
							<HeaderStyle Width="3%" CssClass="titleTableParts"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
                            <ItemTemplate>
									<asp:Label id="lblNo" runat="server"></asp:Label>
								</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn SortExpression="Dealer.DealerCode" HeaderText="Kode Dealer">
							<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
							<ItemTemplate>
								<asp:Label id="Label1" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerCode") %>' runat="server">
								</asp:Label>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn SortExpression="Dealer.DealerName" HeaderText="Nama Dealer">
							<HeaderStyle Width="25%" CssClass="titleTableParts"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
							<ItemTemplate>
								<asp:Label id="Label2" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerName") %>' runat="server">
								</asp:Label>
							</ItemTemplate>
						</asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Tipe Dokumen">
							<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
							<ItemTemplate>
								<asp:Label id="lblTipeDokumen" runat="server">
								</asp:Label>
							</ItemTemplate>
						</asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Periode">
							<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
							<ItemTemplate>
								<asp:Label id="lblPeriode" runat="server"> </asp:Label>
							</ItemTemplate>
						</asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="No Kwitansi/Nota Retur">
							<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
							<ItemTemplate>
								<asp:Label id="lblNoKwitansi" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NoDoc")%>'>
								</asp:Label>
							</ItemTemplate>
						</asp:TemplateColumn>
                        <asp:TemplateColumn Visible="False" HeaderText="FileName">
                            <ItemTemplate>
                                <asp:Label ID="lblFullName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FileNamePath") %>'>
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn>
							<HeaderStyle Width="4%" CssClass="titleTableParts"></HeaderStyle>
							<ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
							<ItemTemplate>								
								<asp:LinkButton id="lbtnSimpan" CausesValidation="False" CommandName="Simpan" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>' 
                                    text="Simpan" Runat="server" ToolTip="Simpan">
									<img src="../images/download.gif" border="0" alt="Simpan"></asp:LinkButton>
							</ItemTemplate>
						</asp:TemplateColumn>
					</Columns>
					<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
				</asp:datagrid>
			</div>			
		</form>
	</body>
</html>
