<%@ Page Language="vb" AutoEventWireup="false" Codebehind="EntrySPPurchaseOrder.aspx.vb" Inherits="EntrySPPurchaseOrder" smartNavigation="True" %>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>EntrySPPurchaseOrder</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript">
		
			function GetCurrentInputIndex()
			{
				var dgSPOrderDetail = document.getElementById("dgSPOrderDetail");
				var currentRow;
				var index = 0;
				var inputs;
				var indexInput;
				
				for (index = 0; index < dgSPOrderDetail.rows.length; index++)
				{
					inputs = dgSPOrderDetail.rows[index].getElementsByTagName("INPUT");
					
					if (inputs != null && inputs.length > 0)
					{
						for (indexInput = 0; indexInput < inputs.length; indexInput++)
						{	
							if (inputs[indexInput].type != "hidden")
								return index;
						}
					}
				}
				
				return -1;
			}
			
			function SparePart(selectedCode)
			{
				var tempParam = selectedCode.split(';');	
				var indek = GetCurrentInputIndex();
				var dgSPOrderDetail = document.getElementById("dgSPOrderDetail");
				var partCode = dgSPOrderDetail.rows[indek].getElementsByTagName("INPUT")[0];
				partCode.innerText = tempParam[0];				
				var partName = dgSPOrderDetail.rows[indek].getElementsByTagName("SPAN")[1];
				partName.innerText = tempParam[1];				
				var partPrice = dgSPOrderDetail.rows[indek].getElementsByTagName("SPAN")[2];
				partPrice.innerText = tempParam[2];				
				var partAmount = dgSPOrderDetail.rows[indek].getElementsByTagName("SPAN")[3];
				partAmount.innerText="0";
				var partQTY = dgSPOrderDetail.rows[indek].getElementsByTagName("INPUT")[1];
				partQTY.innerText="0"
				partQTY.focus();
			}
			
			function CalculateAmount()
			{	var indek = GetCurrentInputIndex();
				var dgSPOrderDetail = document.getElementById("dgSPOrderDetail");
				var partQTY = dgSPOrderDetail.rows[indek].getElementsByTagName("INPUT")[1].value;
				var partPrice = dgSPOrderDetail.rows[indek].getElementsByTagName("SPAN")[2].innerText;
				var amount=parseInt(partQTY)*parseFloat(partPrice);
				var partAmount = dgSPOrderDetail.rows[indek].getElementsByTagName("SPAN")[3];
				partAmount.innerText=amount;
				
			}
			
			
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table2" style="Z-INDEX: 101; LEFT: 8px; WIDTH: 736px; POSITION: absolute; TOP: 64px"
				cellSpacing="1" cellPadding="1" width="300" border="1">
				<TR>
					<TD><asp:label id="Label8" runat="server" Font-Bold="True">ENTRY SP PURCHASE ORDER</asp:label></TD>
				</TR>
				<TR>
					<TD>
						<TABLE id="Table1" style="WIDTH: 568px; HEIGHT: 80px" cellSpacing="1" cellPadding="1" width="568"
							border="1">
							<TR>
								<TD style="WIDTH: 140px"><asp:label id="Label1" runat="server" Height="24px" Width="137px">Dealer Code / Name</asp:label></TD>
								<TD style="WIDTH: 19px"><asp:label id="Label5" runat="server" Height="24px" Width="8px">:</asp:label></TD>
								<TD><asp:label id="lblDealerCode_Name" runat="server" Height="24px" Width="344px"></asp:label></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 140px"><asp:label id="Label3" runat="server" Height="24px" Width="137px">Order Type</asp:label></TD>
								<TD style="WIDTH: 19px"><asp:label id="Label6" runat="server" Height="24px" Width="8px">:</asp:label></TD>
								<TD><asp:dropdownlist id="cmbOrderType" runat="server"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 140px"><asp:label id="Label4" runat="server" Height="24" Width="137">Dealer PO No / Date</asp:label></TD>
								<TD style="WIDTH: 19px"><asp:label id="Label7" runat="server" Height="24px" Width="8px">:</asp:label></TD>
								<TD>
									<TABLE id="Table3" style="WIDTH: 200px; HEIGHT: 32px" cellSpacing="1" cellPadding="1" width="200"
										border="1">
										<TR>
											<TD style="WIDTH: 128px"><asp:textbox id="txtPONo" runat="server" Width="112px" ReadOnly="True"></asp:textbox></TD>
											<TD><asp:label id="Label2" runat="server">/</asp:label></TD>
											<TD><asp:textbox id="txtDate" runat="server" Width="75px" ReadOnly="True"></asp:textbox></TD>
										</TR>
									</TABLE>
								</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD><asp:datagrid id="dgSPOrderDetail" runat="server" Height="148px" OnItemCommand="dgSPOrderDetail_ItemCommand"
							ShowFooter="True" AutoGenerateColumns="False">
							<HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#CC99FF"></HeaderStyle>
							<Columns>
								<asp:BoundColumn DataField="ID" ReadOnly="True" HeaderText="No">
									<HeaderStyle Width="3%"></HeaderStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Part Number">
									<HeaderStyle Width="100px"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id=Label9 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SparePartMaster.PartNumber") %>'>
										</asp:Label>
									</ItemTemplate>
									<FooterTemplate>
										<asp:TextBox id=TextBox2 runat="server" Width="124px" Text='<%# DataBinder.Eval(Container, "DataItem.SparePartMaster.PartNumber") %>' MaxLength="18">
										</asp:TextBox>
										<asp:Button id="btnFootPopUpPONo" runat="server" Text="..." CommandName="POFootNo"></asp:Button>
									</FooterTemplate>
									<EditItemTemplate>
										<asp:TextBox id=TextBox1 runat="server" Width="124px" Text='<%# DataBinder.Eval(Container, "DataItem.SparePartMaster.PartNumber") %>' MaxLength="18">
										</asp:TextBox>
										<asp:Button id="btnPopUpPONo" runat="server" Text="..." CommandName="PONo"></asp:Button>
									</EditItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Part Name">
									<ItemTemplate>
										<asp:Label id=Label10 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SparePartMaster.PartName") %>'>
										</asp:Label>
									</ItemTemplate>
									<FooterTemplate>
										<asp:TextBox id=txtFootPartName runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SparePartMaster.PartName") %>' MaxLength="30">
										</asp:TextBox>
									</FooterTemplate>
									<EditItemTemplate>
										<asp:TextBox id=txtPartName runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SparePartMaster.PartName") %>' MaxLength="30">
										</asp:TextBox>
									</EditItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Qty">
									<ItemTemplate>
										<asp:Label id=Label11 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SparePartPODetail.Quantity") %>'>
										</asp:Label>
									</ItemTemplate>
									<FooterTemplate>
										<asp:TextBox onkeypress="numericOnly()" id="TextBox3" runat="server" Width="36px" MaxLength="5"
											onchange="CalculateAmount()"></asp:TextBox>
									</FooterTemplate>
									<EditItemTemplate>
										<asp:TextBox id=txtQty runat="server" Width="36px" Text='<%# DataBinder.Eval(Container, "DataItem.SparePartPODetail.Quantity") %>' MaxLength="5">
										</asp:TextBox>
									</EditItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="SparePartPODetail.RetailPrice" HeaderText="Retail Price"></asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Amount">
									<ItemTemplate>
										<asp:Label id="lblAmount" runat="server" Width="144px"></asp:Label>
									</ItemTemplate>
									<FooterTemplate>
										<asp:Label id="lblFootAmount" runat="server" Width="144px"></asp:Label>
									</FooterTemplate>
									<EditItemTemplate>
										<asp:Label id="lblEditAmount" runat="server" Width="144px"></asp:Label>
									</EditItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn>
									<ItemTemplate>
										<asp:LinkButton id="btnEdit" runat="server">Ubah</asp:LinkButton>&nbsp;
										<asp:LinkButton id="btnDelete" runat="server">Hapus</asp:LinkButton>
									</ItemTemplate>
									<FooterTemplate>
										<asp:LinkButton id="btnAdd" runat="server">Tambah</asp:LinkButton>
									</FooterTemplate>
									<EditItemTemplate>
										<asp:LinkButton id="btnSaveRow" runat="server">Simpan</asp:LinkButton>
									</EditItemTemplate>
								</asp:TemplateColumn>
							</Columns>
						</asp:datagrid></TD>
				</TR>
				<TR>
					<TD>
						<TABLE id="Table4" style="WIDTH: 736px" cellSpacing="1" cellPadding="1" width="300" border="1">
							<TR>
								<TD><asp:label id="Label12" runat="server">Total Unit</asp:label></TD>
								<TD><asp:label id="Label13" runat="server">:</asp:label></TD>
								<TD><asp:label id="lblTotUnit" runat="server"></asp:label></TD>
								<TD><asp:label id="Label14" runat="server">Total Harga</asp:label></TD>
								<TD><asp:label id="Label15" runat="server">:</asp:label></TD>
								<TD><asp:label id="lblTotHarga" runat="server"></asp:label></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD><asp:button id="btnCancel" runat="server" Text="Batal"></asp:button>&nbsp;
						<asp:button id="btnSave" runat="server" Text="Simpan"></asp:button>&nbsp;
						<asp:button id="btnPrint" runat="server" Text="Cetak"></asp:button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
						<asp:button id="btnSubmit" runat="server" Text="Submit"></asp:button></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
