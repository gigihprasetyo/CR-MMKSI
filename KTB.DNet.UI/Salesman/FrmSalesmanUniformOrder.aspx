<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmSalesmanUniformOrder.aspx.vb" Inherits="FrmSalesmanUniformOrder" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmSalesmanUniformOrder</title>
		<META http-equiv="Content-Type" content="text/html; charset=windows-1252">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript" src="../WebResources/FormFunctions.js"></script>
		<script language="javascript">

		function ShowPopUpRecap()
		{
			//var myDate = new Date( );
			var ddlUnifDist=document.getElementById("ddlUnifDist")
			showPopUp('../PopUp/PopUpUniformRecap.aspx?id='+ddlUnifDist.value,'',500,760,null);
			//showPopUp('../PopUp/PopUpUniformRecap.aspx?id='+ddlUnifDist.value+'&time='+myDate.getTime( ),'',500,760,null);
		}
		function UpdateTotal(){
			var dtg=document.getElementById("dtgUniformOrder");
			var i =0;
			var ChkBoxItem;
			var Total=0;
			var Price = document.getElementById("lblHargaDealerVal").innerText;
			var lblGrandTotal = document.getElementById("lblGrandTotal");
			Price = Price.replace(',','');
			Price = Price.replace('.','');
			for(i=0;i<dtg.rows.length-1;i++){
				ChkBoxItem = document.getElementById("dtgUniformOrder__ctl"+(i+2)+"_ChkBoxItem");
				if(ChkBoxItem.checked==true){
					Total += (1 * Price);
				}				
			}
			lblGrandTotal.innerText=FormatNumber(Total);
		}
		
		function FormatNumber(pNumber){
			pNumber+="";
			x=pNumber.split(".");
			x1=x[0];
			x2=x.length>1?"."+x[1]:"";
			var rgx=/(\d+)(\d{3})/;
			while(rgx.test(x1)){
				x1=x1.replace(rgx,"$1"+"."+"$2");
			}
			pNumber=x1+x2;
			//pNumber=pNumber.replace(".","");
			//pNumber=pNumber.replace(",",".");
			return pNumber;
		}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="1" cellPadding="2" width="100%" border="0">
				<TR>
					<TD class="titlePage" colSpan="3">
						<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="titlePage">
									SERAGAM TENAGA PENJUAL&nbsp;-
									<asp:label id="lblTitle" runat="server" Width="408px"></asp:label></td>
							</tr>
							<tr>
								<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
							</tr>
							<tr>
								<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
							</tr>
						</TABLE>
					</TD>
				</TR>
				<tr>
					<TD class="titleField" width="30%"><asp:label id="Label4" runat="server">No. Pemesanan</asp:label></TD>
					<td style="WIDTH: 8px" width="8">:</td>
					<TD width="69%"><asp:label id="lblOrderNo" runat="server" Width="104px"></asp:label></TD>
				</tr>
				<tr>
					<TD class="titleField" width="30%"><asp:label id="Label1" runat="server">Kode Pesanan</asp:label></TD>
					<td style="WIDTH: 8px" width="8">:</td>
					<TD width="69%"><asp:dropdownlist id="ddlUnifDist" runat="server" Width="168px" AutoPostBack="True"></asp:dropdownlist></TD>
				</tr>
				<TR>
					<TD class="titleField" style="HEIGHT: 21px" width="30%">Kategori</TD>
					<TD style="WIDTH: 8px; HEIGHT: 21px" width="8">:</TD>
					<TD style="HEIGHT: 21px" width="69%"><asp:dropdownlist id="ddlJobPositionDesc" runat="server"></asp:dropdownlist></TD>
				</TR>
				<TR>
					<TD class="titleField" width="30%"><asp:label id="lblHargaNormal" runat="server">Harga Normal</asp:label></TD>
					<TD style="WIDTH: 8px" width="8"><asp:label id="lblSeparator01" runat="server">:</asp:label></TD>
					<TD width="69%"><asp:label id="lblHargaNormalVal" runat="server"></asp:label></TD>
				</TR>
				<TR>
					<TD class="titleField" width="30%"><asp:label id="lblHargaDealer" runat="server">Harga Dealer</asp:label></TD>
					<TD style="WIDTH: 8px" width="8"><asp:label id="Label3" runat="server">:</asp:label></TD>
					<TD width="69%"><asp:label id="lblHargaDealerVal" runat="server"></asp:label></TD>
				</TR>
				<TR>
					<TD class="titleField" width="30%"></TD>
					<td style="WIDTH: 8px" width="8"></td>
					<TD width="69%"><asp:button id="btnSearch" runat="server" Width="56px" CausesValidation="False" Text="Cari"></asp:button></TD>
				</TR>
				<TR>
					<TD class="titleField" width="30%"></TD>
					<TD style="WIDTH: 8px" width="8"></TD>
					<TD width="69%"></TD>
				</TR>
				<TR>
					<TD class="titleField" width="30%"><asp:label id="Label2" runat="server">Grand Total</asp:label></TD>
					<td style="WIDTH: 8px" width="8">:</td>
					<TD width="69%">Rp.
						<asp:label id="lblGrandTotal" runat="server" CssClass="textRight">0</asp:label></TD>
				</TR>
				<TR>
					<TD align="right" colSpan="3">
						<div id="div1" style="OVERFLOW: auto; HEIGHT: 240px"><asp:datagrid id="dtgUniformOrder" runat="server" Width="100%" AllowSorting="True" CellSpacing="1"
								AutoGenerateColumns="False" BorderColor="#CDCDCD" BorderStyle="None" BorderWidth="0px" BackColor="#CDCDCD" CellPadding="3" GridLines="None"
								PageSize="25">
								<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
								<AlternatingItemStyle ForeColor="Black" BackColor="White"></AlternatingItemStyle>
								<ItemStyle ForeColor="Black" BackColor="#FDF1F2"></ItemStyle>
								<HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#CC3333"></HeaderStyle>
								<Columns>
									<asp:TemplateColumn Visible="False" SortExpression="SalesmanUniformAssigned.ID" HeaderText="SalesmanUniformAssigned ID">
										<HeaderStyle Width="12%" CssClass="titleTableRsd"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblOrderID" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="No.">
										<HeaderStyle Width="5%" CssClass="titleTableRsd"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
									</asp:TemplateColumn>
									<asp:TemplateColumn Visible="False">
										<HeaderStyle Width="5%" CssClass="titleTableRsd"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<ItemTemplate>
											<asp:CheckBox id="chk" runat="server" Visible=False Checked='<%# DataBinder.Eval(Container, "DataItem.IsValidate")=1 %>'>
											</asp:CheckBox>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="SalesmanHeader.SalesmanCode" HeaderText="ID">
										<HeaderStyle Width="12%" CssClass="titleTableRsd"></HeaderStyle>
										<ItemStyle Wrap="False"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblSalesmanID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SalesmanHeader.SalesmanCode") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="SalesmanHeader.Name" HeaderText="Nama Tenaga Penjual">
										<HeaderStyle Width="20%" CssClass="titleTableRsd"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblSalesmanName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SalesmanHeader.Name") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="SalesmanHeader.JobPosition.Description" HeaderText="Posisi">
										<HeaderStyle Width="20%" CssClass="titleTableRsd"></HeaderStyle>
										<ItemTemplate>
											<asp:Label Runat="server" ID="lblPosition" Text='<%# DataBinder.Eval(Container, "DataItem.SalesmanHeader.JobPosition.Description") %>'>Label</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Ukuran">
										<HeaderStyle Width="5%" CssClass="titleTableRsd"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblUkuran" Runat="server"></asp:Label>
										</ItemTemplate>
										<EditItemTemplate>
											<asp:DropDownList id="ddlUniformSize" runat="server"></asp:DropDownList>
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn>
										<HeaderStyle Width="15%" CssClass="titleTableRsd"></HeaderStyle>
										<ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="lbtnEdit" runat="server" CausesValidation="False" Text="Ubah" CommandName="edit">
												<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
											<asp:LinkButton id="lbtnDelete" CausesValidation="False" CommandName="delete" Runat="server" text="Hapus"
												Visible="False">
												<img src="../images/trash.gif" border="0" alt="Hapus" onclick="return confirm('Apakah anda akan menghapus record ini?');"></asp:LinkButton>
											<asp:LinkButton id=lbtnAdd tabIndex=40 CommandName="add" Runat="server" text="Tambah" Visible="False" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.SalesmanHeader.ID") %>'>
												<img src="../images/add.gif" border="0" alt="Tambah"></asp:LinkButton>
										</ItemTemplate>
										<EditItemTemplate>
											<asp:LinkButton id="lbtnSaveTarget" tabIndex="40" CausesValidation="False" CommandName="save" Runat="server"
												text="Simpan">
												<img src="../images/simpan.gif" border="0" alt="Simpan"></asp:LinkButton>
											<asp:LinkButton id="Linkbutton7" tabIndex="50" CausesValidation="False" CommandName="cancel" Runat="server"
												text="Batal">
												<img src="../images/batal.gif" border="0" alt="Batal"></asp:LinkButton>
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn Visible="False">
										<HeaderStyle Width="5%"></HeaderStyle>
										<ItemTemplate>
											<asp:CheckBox id="ChkBoxItem" runat="server" onclick="UpdateTotal();"></asp:CheckBox>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
							</asp:datagrid></div>
						<INPUT id="btnRecap" style="WIDTH: 96px; HEIGHT: 21px" onclick="ShowPopUpRecap();" type="button"
							value="Rekap" runat="server" NAME="btnRecap">
					</TD>
				</TR>
				<TR>
					<TD class="titleField" width="30%"></TD>
					<td style="WIDTH: 8px" width="8"></td>
					<TD width="69%"><asp:button id="btnSimpanDealer" runat="server" Text="Simpan"></asp:button><asp:button id="btnSimpanKTB" runat="server" Text="Simpan"></asp:button><asp:button id="btnValidate" tabIndex="70" runat="server" Width="48px" CausesValidation="False"
							Text="Submit"></asp:button><asp:button id="btnBack" tabIndex="70" runat="server" Width="48px" CausesValidation="False"
							Text="Kembali"></asp:button><asp:button id="btnBatal" tabIndex="70" runat="server" Width="48px" CausesValidation="False"
							Text="Batal"></asp:button></TD>
				</TR>
				<TR>
					<TD colSpan="3"></TD>
				</TR>
			</TABLE>
			<script> UpdateTotal(); </script>
		</form>
	</body>
</HTML>
