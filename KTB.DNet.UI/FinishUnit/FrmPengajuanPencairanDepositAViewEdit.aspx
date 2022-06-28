<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmPengajuanPencairanDepositAViewEdit.aspx.vb" Inherits="FrmPengajuanPencairanDepositAViewEdit"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmPengajuanPencairanDepositAViewEdit</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript">
			function CalculatePPn(txtAmount, txtPPn)
			{
				txtPPn.value = 0.1 * txtAmount.value;
				var lblTotal = document.getElementById("lblTotal");
				lblTotal.value = lblTotal.value + txtAmount.value;
							
			}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">Sales - DepositA - Pengajuan Pencairan Deposit A (Detail)</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td style="HEIGHT: 6px" height="6"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
			</table>
			<div id="div1" style="OVERFLOW: auto; HEIGHT: 265px">
				<asp:datagrid id="dgEntryPencairanDepositA" runat="server" BackColor="#CDCDCD" Width="100%" ShowFooter="False"
					AutoGenerateColumns="False" AllowSorting="False" CellSpacing="1" CellPadding="3" BorderColor="Gainsboro"
					BorderWidth="0px">
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
								<asp:Label id="lblNo" runat="server" text="1"></asp:Label>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Jumlah Total">
							<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<ItemTemplate>
								<asp:Label id="lblHeaderAmount" Runat="server">
								</asp:Label>
							</ItemTemplate>
							<EditItemTemplate>
								<asp:TextBox id="txtHeaderAmountEdit" runat="server" BackColor="White" Width="95px" MaxLength="18" Enabled="False" >
								</asp:TextBox>
							</EditItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Jumlah Pencairan">
							<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<ItemTemplate>
								<asp:Label id=lblJumlahPencairan Text='<%# DataBinder.Eval(Container.DataItem, "DealerAmount","{0:#,###}") %>' Runat="server">
								</asp:Label>
							</ItemTemplate>
							<EditItemTemplate>
								<asp:TextBox id=txtJumlahPencairanEdit runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "DealerAmount","{0:#,###}" ) %>' BackColor="White" Width="95px" MaxLength="18">
								</asp:TextBox>
							</EditItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="PPn (10%)">
							<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<ItemTemplate>
								<asp:Label id="lblPPn" runat="server">
								</asp:Label>
							</ItemTemplate>
							<EditItemTemplate>
								<asp:TextBox id="txtPPnEdit" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PPn","{0:#,###.00}" ) %>' BackColor="White" Width="95px" MaxLength="18" Enabled="False">
								</asp:TextBox>
							</EditItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Penjelasan">
							<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
							<ItemStyle HorizontalAlign="Left"></ItemStyle>
							<ItemTemplate>
								<asp:Label id="lblPenjelasan" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Description") %>'>
								</asp:Label>
							</ItemTemplate>
							<EditItemTemplate>
								<asp:TextBox ID="txtPenjelasanEntryEdit" Width="200px" Runat="server" CssClass="textLeft" Text='<%# DataBinder.Eval(Container.DataItem, "Penjelasan") %>'/>
							</EditItemTemplate>
						</asp:TemplateColumn>
						<asp:EditCommandColumn ButtonType="LinkButton" UpdateText="&lt;img src=&quot;../images/simpan.gif&quot; border=&quot;0&quot; alt=&quot;Simpan&quot;&gt;"
							CancelText="&lt;img src=&quot;../images/batal.gif&quot; border=&quot;0&quot; &quot; alt=&quot;Batal&quot;&gt;"
							EditText="&lt;img src=&quot;../images/edit.gif&quot; border=&quot;0&quot; alt=&quot;Ubah&quot;&gt;">
							<HeaderStyle Width="2%" CssClass="titleTableSales"></HeaderStyle>
							<ItemStyle HorizontalAlign="Center"></ItemStyle>
						</asp:EditCommandColumn>
					</Columns>
				</asp:datagrid>
			</div>
			<TABLE cellSpacing="1" cellPadding="2" width="100%" border="0">
				<tr>
					<td class="titleField" width="5%">Total</td>
					<td width="2%">:</td>
					<td width="20%" align="right"><asp:Label id="lblTotal" runat="server" Font-Bold="True" Text="0"></asp:Label></td>
					<td colspan="3"></td>
				</tr>
			</TABLE>
			<br>
			<asp:button id="btnCancel" runat="server" Text="Kembali"></asp:button>
		</form>
	</body>
</HTML>
