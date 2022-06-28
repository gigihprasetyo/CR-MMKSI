<%@ Register TagPrefix="cc1" Namespace="FilterCompositeControl" Assembly="FilterCompositeControl" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmSparePartMaster.aspx.vb" Inherits="FrmSparePartMaster" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Master Barang</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<base target="_self">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript" src="../WebResources/FormFunctions.js"></script>
		<script language="javascript">

		function showPopUp(Url, Parameters, Height, Width, CallbackFunction)
	{
		var strFeature = 'dialogHeight:' + Height + 'px;';	
		strFeature += 'dialogWidth:' + Width + 'px';
		strFeature += 'center:yes;';	
		strFeature += 'status:no;';
		strFeature += 'help:no;';
		strFeature += 'resizable:no;';
		
		if(navigator.appName == "Microsoft Internet Explorer")
		{
			var retVal = window.showModalDialog(Url, Parameters,strFeature);
			if (CallbackFunction != null && retVal != null) {
			CallbackFunction(retVal);
			}
		}
		else
		{
			openDGDialog(Url, Width, Height, CallbackFunction);
			return false;
		}	
	}

		function ShowModelSelection()
		{
			showPopUp('../General/../PopUp/PopupSPModelSelectionMultiple.aspx','',500,480,ModelSelection);
		}
		
		function ModelSelection(selectedModel)
		{
			var txtModelSelection = document.getElementById("txtModel");
			txtModelSelection.value = selectedModel;			
		}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="1" cellPadding="2" width="100%" border="0">
				<TR>
					<TD>
						<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="titlePage">MASTER BARANG - Master Barang</td>
							</tr>
							<tr>
								<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
							</tr>
							<tr>
								<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
							</tr>
						</TABLE>
						<asp:label id="Label1" runat="server" Font-Bold="True"> Model </asp:label>
                        <asp:textbox id="txtModel" onblur="omitSomeCharacter('txtModel','<>?*%$')" runat="server"
						Width="152px"></asp:textbox>
						<asp:label id="lblSearchDealer" runat="server">
							<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:label></TD>
				</TR>
				<TR>
					<TD><b><cc1:compositefilter id="cfSparePartMaster" runat="server" DataGridSouce="dtgSparePartMaster"></cc1:compositefilter></b></TD>
				</TR>
				<TR>
					<TD>
						<div id="div1" style="HEIGHT: 350px; OVERFLOW: auto"><asp:datagrid id="dtgSparePartMaster" runat="server" AllowPaging="True" AllowCustomPaging="True"
								AutoGenerateColumns="False" AllowSorting="True" Width="100%" BorderColor="#CDCDCD" BorderStyle="None" BorderWidth="0px" BackColor="#CDCDCD"
								CellPadding="3" GridLines="Vertical" PageSize="25" CellSpacing="1">
								<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#008A8C"></SelectedItemStyle>
								<AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
								<ItemStyle BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#000084"></HeaderStyle>
								<FooterStyle ForeColor="Black" BackColor="#CCCCCC"></FooterStyle>
								<Columns>
											
									<asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID"></asp:BoundColumn>
                                    <asp:TemplateColumn>
									    <HeaderStyle Width="2%" CssClass="titleTableParts" ></HeaderStyle>
										<HeaderTemplate>
										    <input id="chkAllItems" type="checkbox" onclick="CheckAllDataGridCheckBoxes('chkItemChecked', document.all.chkAllItems.checked)" />
										</HeaderTemplate>
										<ItemTemplate>
										    <asp:CheckBox id="chkItemChecked" runat="server"></asp:CheckBox>
										</ItemTemplate>
									</asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="No">
										<HeaderStyle ForeColor="White" Width="3%" CssClass="titleTableParts"></HeaderStyle>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="PartNumber" SortExpression="PartNumber" HeaderText="Nomor Barang">
										<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="PartName" SortExpression="PartName" HeaderText="Nama Barang">
										<HeaderStyle Width="25%" CssClass="titleTableParts"></HeaderStyle>
									</asp:BoundColumn>
                                    <asp:TemplateColumn SortExpression="PartCode" HeaderText="Status">
										<HeaderStyle Width="5%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
										    <asp:Label id="labelPartCode" runat="server"></asp:Label>
										</ItemTemplate>									
                                    </asp:TemplateColumn>
								    <%--<asp:BoundColumn DataField="PartCode" SortExpression="PartCode" HeaderText="Status">
										<HeaderStyle Width="5%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>--%>
                                    <asp:TemplateColumn SortExpression="ProductCategory"  HeaderText="Produk">
										<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
										<ItemTemplate>
										    <asp:Label id="lblProduct" runat="server"></asp:Label>
										</ItemTemplate>									
                                    </asp:TemplateColumn>
                                    <asp:BoundColumn DataField="ModelCode" SortExpression="ModelCode" HeaderText="Model">
										<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="RetalPrice" SortExpression="RetalPrice" HeaderText="Harga Eceran (Rp)"
										DataFormatString="{0:#,##0}">
										<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn Visible="false" SortExpression="Stock" HeaderText="Stok">
										<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
									</asp:TemplateColumn>
                                    <asp:TemplateColumn SortExpression="AltPartNumber"  HeaderText="Barang Pengganti">
										<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
										<ItemTemplate>
										    <asp:Label id="labelAlternatifPartNumber" runat="server"></asp:Label>
										</ItemTemplate>									
                                    </asp:TemplateColumn>
									<%--<asp:BoundColumn DataField="AltPartNumber" SortExpression="AltPartNumber" HeaderText="Barang Pengganti">
										<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
									</asp:BoundColumn>--%>
                                    <asp:TemplateColumn SortExpression="ActiveStatus" HeaderText="Aktif">
										<HeaderStyle Width="7%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
										    <asp:Label id="labelStatus" runat="server" Text="Aktif"></asp:Label>
										</ItemTemplate>									
                                    </asp:TemplateColumn>
									<%--<asp:BoundColumn DataField="ActiveStatus" SortExpression="ActiveStatus" HeaderText="Aktif">
										<HeaderStyle Width="7%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>--%>
									<asp:TemplateColumn HeaderText="Detail">
                                        <HeaderStyle Width="5%" CssClass="titleTableParts"></HeaderStyle>
                                        <ItemStyle HorizontalAlign ="Center" />
										<ItemTemplate>
											<asp:Label id="lblView" runat="server" Visible="false">
												<img style="cursor:hand" alt="Alternatif" src="../images/detail.gif" border="0" height="17px"
													width="17px">
											</asp:Label>
											<asp:LinkButton Runat="server" ID="lbtnActivate" CommandName="activate"  Visible="false">
												<img style="cursor:hand" alt="Set Aktif" src="../images/aktif.gif" border="0" height="17px"
													width="17px">
											</asp:LinkButton>
											<asp:LinkButton Runat="server" ID="lbtnDeactive" CommandName="deactivate"  Visible="false">
												<img style="cursor:hand" alt="Set Tidak Aktif" src="../images/in-aktif.gif" border="0"
													height="17px" width="17px">
											</asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn >
										<ItemTemplate>
											<asp:DropDownList Runat="server" ID="ddlAccessoriesType" AutoPostBack="False" ></asp:DropDownList>
											<asp:Button Runat="server" ID="btnUpdateAccType" style="display:none;" Text="Update" CommandName="UpdateAccType"></asp:Button>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" ForeColor="Black" BackColor="#cdcdcd" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></div>
					</TD>
				</TR>
				<TR>
					<TD>
						<P>*&nbsp;Harga dapat berubah sewaktu - waktu tanpa pemberitahuan terlebih 
							dahulu&nbsp;
							<br>
							* Data terupdate tiap hari setiap jam 12 siang dan 12 malam</P>
					</TD>
				</TR>
			</TABLE>
            <br />
            <asp:label id="Label2" runat="server" Font-Bold="True"> Set Status : </asp:label>
            <asp:DropDownList ID="ddlStatus" runat="server"></asp:DropDownList>
            <asp:Button ID="btnProses" runat="server" Text="Proses" />
            <asp:button id="btnDnLoad" runat="server" Width="96px" Text="Download"></asp:button>
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
