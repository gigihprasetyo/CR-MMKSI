<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PopUpPengembalianPPHDetail.aspx.vb" Inherits="PopUpPengembalianPPHDetail" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Pengembalian PPh Detail</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/InputValidationxx.js"></script>
		<script language="javascript">
		function CalculatePPn(txtAmount, txtPPn)
		{
			txtPPn.value = 0.1 * txtAmount.value;
			var lblTotal = document.getElementById("lblTotal");
			lblTotal.value = lblTotal.value + txtAmount.value;
						
		}
		
	
		function GetIndex(CtlID)
        {
            if (!navigator.appName=="Microsoft Internet Explorer")
			{
				var row = CtlID.parentElement.parentElement;
				indexRow = row.rowIndex;
				return row.rowIndex;
			}
			else
			{
				var row = CtlID.parentNode.parentNode;
				indexRow = row.rowIndex;
				return row.rowIndex;
			}
        }

		function DebitMemoChanged(ddl)
		{
			var indek = GetIndex(ddl);
			var val = ddl.options[ddl.selectedIndex].value;
			var text = ddl.options[ddl.selectedIndex].text;
			
			var dtgPesananKendaraan = document.getElementById("dgPengembalianPPH");
			var lblNoJV =  document.getElementById("dgPengembalianPPH__ctl"+(indek+1)+"_lblNoJVF");
			if (!lblNoJV){
			var lblNoJV =  document.getElementById("dgPengembalianPPH__ctl"+(indek+1)+"_lblNoJVE");
			}
			lblNoJV.innerHTML = '';
			lblNoJV.style.visibility='visible';
			
			var lblAmount =  document.getElementById("dgPengembalianPPH__ctl"+(indek+1)+"_lblAmountF");
			if (!lblAmount){
			var lblAmount =  document.getElementById("dgPengembalianPPH__ctl"+(indek+1)+"_lblAmountE");
			}
			lblAmount.innerHTML = '';
			lblAmount.style.visibility='visible';
			
		}
		</script>
		<base target="_self">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">Status Pengembalian PPh Parkir Detail</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td style="HEIGHT: 6px" height="6"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
			</table>
			<TABLE cellSpacing="1" cellPadding="2" width="100%" border="0">
				<tr>
					<td class="titleField" style="WIDTH: 171px">Kode Dealer</td>
					<td style="WIDTH: 10px">:</td>
					<td><asp:literal id="ltrDealerCode" runat="server"></asp:literal></td>
				</tr>
				<tr>
					<td class="titleField" style="WIDTH: 171px">Nama Dealer</td>
					<td style="WIDTH: 10px">:</td>
					<td><asp:literal id="ltrDealerName" runat="server"></asp:literal></td>
				</tr>
				<TR>
					<TD class="titleField">Nomor Bukti Potong PPH</TD>
					<td style="WIDTH: 10px">:</td>
					<td>
						<asp:TextBox ID="txtNoPPH" Runat="server"></asp:TextBox>
						<asp:label id="lnlNoPPH" Runat="server"></asp:label>
					</td>
				</TR>
				<tr>
					<td class="titleField" style="WIDTH: 171px">Tanggal Bukti Potong PPH</td>
					<td style="WIDTH: 10px">:</td>
					<td><asp:label id="lblTglPPH" Runat="server"></asp:label></td>
				</tr>
				<tr>
					<td class="titleField" style="WIDTH: 171px">Total Biaya Parkir</td>
					<td style="WIDTH: 10px">:</td>
					<td align="right" width="80"><asp:label id="lblTotal" runat="server" Font-Bold="True" Text="0"></asp:label></td>
				</tr>
				<tr>
					<td class="titleField" style="WIDTH: 171px">Total Biaya PPh</td>
					<td style="WIDTH: 10px">:</td>
					<td align="right" width="80"><asp:label id="lblTotalPPH" runat="server" Font-Bold="True" Text="0"></asp:label></td>
				</tr>
			</TABLE>
			<div id="div1" style="OVERFLOW: auto; HEIGHT: 265px"><asp:datagrid id="dgPengembalianPPH" runat="server" BackColor="#CDCDCD" Width="100%" ShowFooter="False"
					AutoGenerateColumns="False" AllowSorting="False" CellSpacing="1" CellPadding="3" BorderColor="Gainsboro" BorderWidth="0px">
					<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
					<AlternatingItemStyle ForeColor="Black" BackColor="White"></AlternatingItemStyle>
					<ItemStyle ForeColor="Black" BackColor="#F1F6FB"></ItemStyle>
					<HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
					<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
					<Columns>
						<asp:TemplateColumn HeaderText="No">
							<HeaderStyle Width="2%" CssClass="titleTableSales"></HeaderStyle>
							<ItemStyle HorizontalAlign="Center"></ItemStyle>
							<ItemTemplate>
								<asp:Label id="lblNo" runat="server"></asp:Label>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="No. Debit Memo">
							<HeaderStyle Width="25%" CssClass="titleTableSales"></HeaderStyle>
							<ItemStyle HorizontalAlign="center"></ItemStyle>
							<FooterStyle HorizontalAlign="center"></FooterStyle>
							<ItemTemplate>
								<asp:Label id="lblDebitMemo" runat="server" NAME="lblDebitMemo" Text='<%# DataBinder.Eval(Container.DataItem, "ParkingFee.DebitMemoNumber" )  %>'>
								</asp:Label>
							</ItemTemplate>
							<EditItemTemplate>
								<asp:DropDownList Runat="server" ID="ddlDebitMemoE" Width="150px" onchange="DebitMemoChanged(this);"></asp:DropDownList>
							</EditItemTemplate>
							<FooterTemplate>
								<asp:DropDownList Runat="server" ID="ddlDebitMemoF" Width="150px" onchange="DebitMemoChanged(this);"></asp:DropDownList>
							</FooterTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Dealer">
							<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
							<ItemStyle HorizontalAlign="center"></ItemStyle>
							<ItemTemplate>
								<asp:Label id="lblDealer" runat="server" ></asp:Label>
							</ItemTemplate>
							<EditItemTemplate>
								<asp:Label id="lblDealerE" Runat="server"></asp:Label>
							</EditItemTemplate>
							<FooterTemplate>
								<asp:Label id="lblDealerF" Runat="server"></asp:Label>
							</FooterTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Periode">
							<HeaderStyle Width="15%" CssClass="titleTableSales"></HeaderStyle>
							<ItemStyle HorizontalAlign="center"></ItemStyle>
							<ItemTemplate>
								<asp:Label id="lblPeriod" runat="server" ></asp:Label>
							</ItemTemplate>
							<EditItemTemplate>
								<asp:Label id="lblPeriodE" Runat="server"></asp:Label>
							</EditItemTemplate>
							<FooterTemplate>
								<asp:Label id="lblPeriodF" Runat="server"></asp:Label>
							</FooterTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Tahun">
							<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
							<ItemStyle HorizontalAlign="center"></ItemStyle>
							<ItemTemplate>
								<asp:Label id="lblYear" runat="server" NAME="lblYear" Text='<%# DataBinder.Eval(Container.DataItem, "ParkingFee.Year" )  %>'>
								</asp:Label>
							</ItemTemplate>
							<EditItemTemplate>
								<asp:Label id="lblYearE" Runat="server"></asp:Label>
							</EditItemTemplate>
							<FooterTemplate>
								<asp:Label id="lblYearF" Runat="server"></asp:Label>
							</FooterTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="No. JV">
							<HeaderStyle Width="20%" CssClass="titleTableSales"></HeaderStyle>
							<ItemStyle HorizontalAlign="center"></ItemStyle>
							<ItemTemplate>
								<asp:Label id="lblNoJV" runat="server" NAME="lblNoJV" Text='<%# DataBinder.Eval(Container.DataItem, "ParkingFee.AssignmentNumber" )  %>'>
								</asp:Label>
							</ItemTemplate>
							<EditItemTemplate>
								<asp:Label id="lblNoJVE" Runat="server"></asp:Label>
							</EditItemTemplate>
							<FooterTemplate>
								<asp:Label id="lblNoJVF" Runat="server"></asp:Label>
							</FooterTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Biaya Parkir">
							<HeaderStyle Width="26%" CssClass="titleTableSales"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<ItemTemplate>
								<asp:Label id="lblAmount" runat="server" NAME="lblAmount" Text='<%# DataBinder.Eval(Container.DataItem, "ParkingFee.Amount","{0:#,###}") %>'>
								</asp:Label>
							</ItemTemplate>
							<EditItemTemplate>
								<asp:Label id="lblAmountE" Runat="server"></asp:Label>
							</EditItemTemplate>
							<FooterTemplate>
								<asp:Label id="lblAmountF" Runat="server"></asp:Label>
							</FooterTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn>
							<HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
							<ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="40px"></ItemStyle>
							<ItemTemplate>
								<asp:LinkButton id="lbtnDelete" runat="server" CommandName="Delete">
									<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn>
							<HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
							<ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="40px"></ItemStyle>
							<FooterStyle HorizontalAlign="Center"></FooterStyle>
							<FooterTemplate>
								<asp:LinkButton id="lbtnAdd" runat="server" CommandName="Add">
									<img src="../images/add.gif" border="0" alt="Tambah"></asp:LinkButton>
							</FooterTemplate>
						</asp:TemplateColumn>
					</Columns>
				</asp:datagrid></div>
			<TABLE cellSpacing="0" cellPadding="0" border="0" width="100%">
				<tr>
					<td align="center" colSpan="4">
						<asp:button ID="btnSimpan" Runat="server" Text="Simpan"></asp:button>
						<INPUT id="btnCancel" style="WIDTH: 60px" onclick="window.close()" type="button" value="Tutup"
							name="btnCancel">
					</td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
