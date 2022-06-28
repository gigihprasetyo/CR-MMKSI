<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmCeilingDetail2.aspx.vb" Inherits="FrmCeilingDetail2" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
  <HEAD>
		<title>FrmCeilingDetail</title>
<meta name=GENERATOR content="Microsoft Visual Studio .NET 7.1">
<meta name=CODE_LANGUAGE content="Visual Basic .NET 7.1">
<meta name=vs_defaultClientScript content=JavaScript>
<meta name=vs_targetSchema content=http://schemas.microsoft.com/intellisense/ie5><LINK rel=stylesheet type=text/css href="../WebResources/stylesheet.css" ><LINK rel=stylesheet type=text/css href="./WebResources/stylesheet.css" >
<style>.hide { DISPLAY: none }
	</style>

<script language=javascript src="../WebResources/FormFunctions.js"></script>

<script language=javascript>
				function ShowPPDealerSelection()
				{
					showPopUp('../PopUp/PopUpDealerSelection.aspx','',500,760,DealerSelection);
				}
				function HideThirdRow()
				{
					var spn = document.getElementById("dtlDetail__ctl3_Label3");
					if(spn){
						var row = spn.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode;
						row.parentNode.removeChild(row);
					}
				}			
				function DealerSelection(selectedDealer)
				{
					var tempParam= selectedDealer;
					var txtDealerSelection = document.getElementById("txtKodeDealer");
					txtDealerSelection.value = tempParam;
				}
				function getNextSibling(startBrother){
 					endBrother=startBrother.nextSibling;
 					while(endBrother.nodeType!=1){
   						endBrother = endBrother.nextSibling;
 					} 				
 					return endBrother;
				}
				var isshown = false;
				function toggleDetail(elm){								
					
					if (elm.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.rows[elm.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.rowIndex + 1].style.display =="none")
					{
						isshown = false;
					}
					if (elm.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.rows[elm.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.rowIndex + 1].style.display =="")
					{
						isshown = true;
					}
					if(!isshown){										
						elm.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.rows[elm.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.rowIndex + 1].style.display = "block";
						elm.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.rows[elm.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.rowIndex + 1].style.display = "";					
						isshown = true;
					}
					else{							
						elm.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.rows[elm.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.rowIndex + 1].style.display = "none";										
						elm.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.rows[elm.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.rowIndex + 1].style.display = "";
						elm.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.rows[elm.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.rowIndex + 1].style.display = "none";
						isshown = false;
					}
					
					if(elm.childNodes[2].tagName == 'IMG'){
						if(elm.childNodes[2].style.display == 'none'){
							elm.childNodes[2].style.display = 'block';										
						}
						else{
							elm.childNodes[2].style.display = 'none';					
							
						}
					}
					else{
						if(elm.childNodes[3].style.display == 'none'){
							elm.childNodes[3].style.display = 'block';										
						}
						else{
							elm.childNodes[3].style.display = 'none';					
							
						}				
					}
					if(elm.childNodes[0].tagName == 'IMG'){
						if(elm.childNodes[0].style.display == 'none'){
							elm.childNodes[0].style.display = 'block';
						}
						else{					
							elm.childNodes[0].style.display = 'none';
						}
					}
					else{
						if(elm.childNodes[1].style.display == 'none'){
							elm.childNodes[1].style.display = 'block';
						}
						else{					
							elm.childNodes[1].style.display = 'none';
						}				
					}
					
				}
				function toggleDepositDetail(elm){
					var tr = elm.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode;
					var rows = tr.parentNode.rows;
					
					if(elm.childNodes[1].style.display == 'none'){
						elm.childNodes[1].style.display = 'block';										
					}
					else{
						elm.childNodes[1].style.display = 'none';					
						
					}
					
					if(elm.childNodes[0].style.display == 'none'){
						elm.childNodes[0].style.display = 'block';
					}
					else{					
						elm.childNodes[0].style.display = 'none';
					}
					var suffix = (getNextSibling(elm.parentNode).innerHTML); // innerHTML ,handle mozilla req
					for(var i =0; i < rows.length; i++){
						if(rows[i].id == "tr" + suffix){
							if(rows[i].style.display == "none"){
								rows[i].style.display = ""; // handle mozilla req
							}
							else{
								rows[i].style.display = "none";
							}
						}
					}
				}
				
				function FormatDTGProposed()
				{
					var dtg = document.getElementById("dtlDetail__ctl4_dtgDetail2");
					var n=dtg.rows.length;
					var i=0;
					
					
					for(i=0;i<dtg.rows[0].cells.length;i++)
					{
						dtg.rows[n-2].cells[i].style.fontWeight="bold";
						dtg.rows[n-1].cells[i].style.fontWeight="bold";
					}
					
					dtg.rows[n-2].style.backgroundColor="#F5F1EE";
					dtg.rows[n-2].cells[0].colSpan="3";
					dtg.rows[n-2].deleteCell(1);
					dtg.rows[n-2].deleteCell(1);
					
					dtg.rows[n-1].style.backgroundColor="#F5F1EE";
					dtg.rows[n-1].cells[0].colSpan="3";
					dtg.rows[n-1].deleteCell(1);
					dtg.rows[n-1].deleteCell(1);
					dtg.rows[n-1].cells[0].width=dtg.rows[0].cells[0].width + dtg.rows[0].cells[1].width+dtg.rows[0].cells[2].width;
				}
				function FormatDTGCair()
				{
					var dtg = document.getElementById("dtlDetail__ctl2_dtgDetail2");
					var n=dtg.rows.length;
					var i=0;
					
					if(!dtg) return;
					
					for(i=0;i<dtg.rows[0].cells.length;i++)
					{
						dtg.rows[n-1].cells[i].style.fontWeight="bold";
					}
					
					dtg.rows[n-1].style.backgroundColor="#F5F1EE";
					dtg.rows[n-1].cells[0].colSpan="3";
					dtg.rows[n-1].deleteCell(1);
					dtg.rows[n-1].deleteCell(1);
					dtg.rows[n-1].cells[0].width=dtg.rows[0].cells[0].width + dtg.rows[0].cells[1].width+dtg.rows[0].cells[2].width;
				}
			</script>
</HEAD>
<body MS_POSITIONING="GridLayout">
<form id=Form1 method=post runat="server">
<TABLE style="WIDTH: 100%; HEIGHT: 313px; id: 'Table2'" border=0 cellSpacing=0 
cellPadding=0 width="100%">
  <TR>
    <TD style="HEIGHT: 18px" class=titlePage>CREDIT 
      CONTROL&nbsp;- Detail Ceiling Position Credit Account&nbsp;&nbsp;<asp:label id=lblCreditAccount runat="server"></asp:label>&nbsp;
<asp:label style="Z-INDEX: 0" id=lblProductCategory runat="server"></asp:label></TD></TR>
  <TR>
    <TD height=1 background=../images/bg_hor.gif><IMG border=0 src="../images/bg_hor.gif" height=1 ></TD></TR>
  <TR>
    <TD height=10><IMG border=0 src="../images/dot.gif" height=1 ></TD></TR><asp:panel id=pnlSearch 
   Runat="server">
  <TR>
    <TD>
      <TABLE id=Table1 border=0 cellSpacing=1 cellPadding=2 width="100%">
        <TR>
          <TD style="WIDTH: 146px; HEIGHT: 8px" class=titleField>Ceiling</TD>
          <TD style="WIDTH: 2px; HEIGHT: 8px">:</TD>
          <TD style="HEIGHT: 8px" class=titleField>
<asp:Label id=lblCeiling runat="server" Width="264px"></asp:Label></TD></TR>
        <TR>
          <TD style="WIDTH: 146px; HEIGHT: 8px" class=titleField>Outstanding 
            SAP</TD>
          <TD style="WIDTH: 2px; HEIGHT: 8px">:</TD>
          <TD style="HEIGHT: 8px" class=titleField>
<asp:Label style="Z-INDEX: 0" id=lblOutstandingSAP runat="server" Width="264px"></asp:Label></TD></TR>
        <TR>
          <TD style="WIDTH: 146px" class=titleField>Permintaan Kirim</TD>
          <TD style="WIDTH: 2px">:</TD>
          <TD class=titleField>
<asp:Label id=lblReqDeliveryDate runat="server" Width="264px"></asp:Label></TD></TR>
        <TR>
          <TD style="WIDTH: 146px" class=titleField>Tgl Laporan</TD>
          <TD style="WIDTH: 2px">:</TD>
          <TD class=titleField>
<asp:Label id=lblReportDate runat="server" Width="264px"></asp:Label></TD></TR>
        <TR>
          <TD style="WIDTH: 146px" class=titleField></TD>
          <TD style="WIDTH: 2px"></TD>
          <TD class=titleField></TD></TR></TABLE></TD></TR>
  <TR>
    <TD><!-- o2nA -->
      <DIV style="WIDTH: 100%; HEIGHT: 340px; OVERFLOW: auto" id=divHidden 
      DESIGNTIMEDRAGDROP="203">
<asp:DataList id=dtlDetail Runat="server" Width="760px" ShowFooter="False" CellSpacing="1" CellPadding="1" BorderColor="white" BackColor="white" BorderWidth="0px">
<HeaderTemplate>
	No </TD>
	<TD class=titleTableSales>
		<asp:HyperLink id="hypNoPO" Runat="server" ForeColor="white">Nomor PO</asp:HyperLink></TD>
	<TD class=titleTableSales>Kode Dealer</TD class=titleTableSales>
	<TD class=titleTableSales>Nomor SO</TD class=titleTableSales>
	<TD class=titleTableSales>Nomor Giro</TD>
	<TD class=titleTableSales>Tgl Kirim</TD>
	<TD class=titleTableSales>Jatuh Tempo</TD>
	<TD class=titleTableSales>Tanggal Efektif</TD>
	<TD class=titleTableSales>Jumlah</TD>
</HeaderTemplate>

<AlternatingItemStyle ForeColor="Black"  BackColor="#CDCDCD">
</AlternatingItemStyle>

<FooterTemplate>
	</TD>
	<TD align="center"><STRONG>Balance For New TOP:</STRONG> </TD>
	<TD align="right"></TD>
	<TD align="right">
		<asp:Label id=lblTotalSaldoAwal runat="server" Font-Bold="True"></asp:Label></TD>
	<TD align="right">
		<asp:Label id=lblTotalDebet runat="server" Font-Bold="True"></asp:Label></TD>
	<TD align="right">
		<asp:Label id=lblTotalKredit runat="server" Font-Bold="True"></asp:Label></TD>
	<TD align="right">
		<asp:Label id=lblTotalSaldoAkhir runat="server" Font-Bold="True"></asp:Label></TD>
	<TD align="center">
</FooterTemplate>

<ItemStyle ForeColor="Black" BackColor="#CDCDCD">
</ItemStyle>

<ItemTemplate>
<TABLE cellSpacing=0>
<TR>
<TD>
<asp:Label id=Label3 onclick=javascript:toggleDetail(this) Runat="server" Font-Bold="True">
						<img src="../images/plus.gif">
						<img style="display:none" src="../images/minus.gif">
					</asp:Label></TD>
<TD></TD></TR></TABLE></TD>
<TD style="COLOR: black; BACKGROUND-COLOR: #CDCDCD" align="left" colspan="4">
	<asp:Label id="lblTitle" runat="server" Font-Bold="True" ></asp:Label></TD>
<TD style="COLOR: black; BACKGROUND-COLOR: #CDCDCD" align="left">
<asp:Label id=lblTglKirim runat="server" Font-Bold="True">SubTotal</asp:Label></TD><TD style="COLOR: black; BACKGROUND-COLOR: #CDCDCD" align="right">
<asp:Label id=lblJatuhTempo runat="server"></asp:Label></TD><TD style="COLOR: black; BACKGROUND-COLOR: #CDCDCD" align="right">
<asp:Label id=lblTglEfektif runat="server"></asp:Label></TD><TD style="COLOR: black; BACKGROUND-COLOR: #CDCDCD" align="right">
<asp:Label id=lblJumlah runat="server" Font-Bold="True" Text=""></asp:Label></TD></TR>
<TR style="DISPLAY: none"><TD></TD><TD colspan="7">
<asp:DataGrid id="dtgDetail" Runat="server" Width="100%" BorderWidth="0px" BackColor="#CDCDCD" BorderColor="Gainsboro" CellPadding="3" CellSpacing="1" AutoGenerateColumns="False">

<AlternatingItemStyle ForeColor="Black" BackColor="#CDCDCD">
</AlternatingItemStyle>

<ItemStyle ForeColor="Black" BackColor="#F1F6FB">
</ItemStyle>

<HeaderStyle CssClass="titleTableSales">
</HeaderStyle>

<Columns>
<asp:TemplateColumn HeaderText="Nomor PO">
	<HeaderStyle ForeColor="white"></HeaderStyle>
	<ItemStyle Width="100px"></ItemStyle>
	<ItemTemplate>
		<asp:Label ID="lblPONumber" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PONumber") %>' ></asp:Label>
	</ItemTemplate>
</asp:TemplateColumn>
<asp:TemplateColumn HeaderText="Kode Dealer">
	<HeaderStyle ForeColor="white"></HeaderStyle>
	<ItemStyle Width="100px"></ItemStyle>
	<ItemTemplate>
		<asp:Label ID="lblDealerCode" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DealerCode") %>' ></asp:Label>
	</ItemTemplate>
</asp:TemplateColumn>
<asp:TemplateColumn HeaderText="Nomor SO">
	<HeaderStyle ForeColor="white"></HeaderStyle>
	<ItemStyle Width="100px"></ItemStyle>
	<ItemTemplate>
		<asp:Label ID="lblSONumber" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SONumber") %>' ></asp:Label>
	</ItemTemplate>
</asp:TemplateColumn>
<asp:TemplateColumn HeaderText="Nomor Giro">
	<HeaderStyle ForeColor="white"></HeaderStyle>
	<ItemStyle Width="100px"></ItemStyle>
	<ItemTemplate>
		<asp:Label ID="lblNoGiro" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.GiroNumber") %>' ></asp:Label>
	</ItemTemplate>
</asp:TemplateColumn>
<asp:TemplateColumn HeaderText="Status Giro" Visible="False">
	<HeaderStyle ForeColor="white"></HeaderStyle>
	<ItemStyle Width="100px"></ItemStyle>
	<ItemTemplate>
		<asp:Label ID="lblGyroStatus" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.GiroStatus") %>' ></asp:Label>
	</ItemTemplate>
</asp:TemplateColumn>
<asp:TemplateColumn HeaderText="Tgl Kirim">
	<HeaderStyle ForeColor="white"></HeaderStyle>
	<ItemStyle Width="100px"></ItemStyle>
	<ItemTemplate>
		<asp:Label ID="lblTglKirim" Runat="server" Text='<%# format(DataBinder.Eval(Container, "DataItem.DeliveryDate"), "dd/MM/yyyy") %>' ></asp:Label>
	</ItemTemplate>
</asp:TemplateColumn>
<asp:TemplateColumn HeaderText="Jatuh Tempo">
	<HeaderStyle ForeColor="white"></HeaderStyle>
	<ItemStyle Width="100px"></ItemStyle>
	<ItemTemplate>
		<asp:Label ID="lblJatuhTempo" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.BaselineDates") %>' ></asp:Label>
	</ItemTemplate>
</asp:TemplateColumn>
<asp:TemplateColumn HeaderText="Tgl Efektif">
	<HeaderStyle ForeColor="white"></HeaderStyle>
	<ItemStyle Width="100px"></ItemStyle>
	<ItemTemplate>
		<asp:Label ID="lblTglEfektif" Runat="server" Text='<%# format(DataBinder.Eval(Container, "DataItem.EfectiveDate"), "dd/MM/yyyy") %>' ></asp:Label>
	</ItemTemplate>
</asp:TemplateColumn>
<asp:TemplateColumn HeaderText="Jumlah">
	<HeaderStyle ForeColor="white"></HeaderStyle>
	<ItemStyle Width="100px"></ItemStyle>
	<ItemTemplate>
		<asp:Label ID="lblJumlah" Runat="server" Width="100%" Text='<%# FormatNumber(DataBinder.Eval(Container, "DataItem.Amount"), 2, TriState.UseDefault, TriState.UseDefault, TriState.True) %>' style="text-align:right;" ></asp:Label>
	</ItemTemplate>
</asp:TemplateColumn>
</Columns>
</asp:DataGrid>
</ItemTemplate>

<HeaderStyle CssClass="titleTableSales">
</HeaderStyle>

<AlternatingItemTemplate>
<TABLE cellSpacing=0 bgcolor="#CDCDCD">
<TR>
<TD >
<asp:Label id=Label2 onclick=toggleDetail(this) Runat="server" Font-Bold="True">
						<img src="../images/plus.gif">
						<img style="display:none" src="../images/minus.gif">
					</asp:Label></TD>
<TD></TD></TR></TABLE></TD>
<TD style="COLOR: black; BACKGROUND-COLOR: #CDCDCD" align="left" colspan="4">
	<asp:Label id="lblTitle2" runat="server" Font-Bold="True"></asp:Label></TD>
<TD style="COLOR: black; BACKGROUND-COLOR: #CDCDCD" align="left">
<asp:Label id="Label1" runat="server" Font-Bold="True">SubTotal</asp:Label></TD><TD style="COLOR: black; BACKGROUND-COLOR: #CDCDCD" align="right">
<asp:Label id="Label4" runat="server"></asp:Label></TD><TD style="COLOR: black; BACKGROUND-COLOR: #CDCDCD" align="right">
<asp:Label id="Label5" runat="server"></asp:Label></TD><TD style="COLOR: black; BACKGROUND-COLOR: #CDCDCD" align="right">
<asp:Label id="lblJumlah2" runat="server" Font-Bold="True" Text='<%# DataBinder.Eval(Container, "DataItem.ProposedPO") %>'></asp:Label></TD>
</TR><TR style="DISPLAY: none"><TD></TD><TD colspan="7">
<asp:DataGrid id="dtgDetail2" Runat="server" Width="100%" BorderWidth="0px" BackColor="#CDCDCD" BorderColor="Gainsboro" CellPadding="3" CellSpacing="1" AutoGenerateColumns="False">
<AlternatingItemStyle ForeColor="Black" BackColor="White">
</AlternatingItemStyle>

<ItemStyle ForeColor="Black" BackColor="White">
</ItemStyle>

<HeaderStyle CssClass="titleTableSales">
</HeaderStyle>

<Columns>
<asp:TemplateColumn HeaderText="No. Reg. PO">
	<HeaderStyle ForeColor="white"></HeaderStyle>
	<ItemStyle Width="100px"></ItemStyle>
	<ItemTemplate>
		<asp:Label ID="lblPONumber2" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PONumber") %>' ></asp:Label>
	</ItemTemplate>
</asp:TemplateColumn>
<asp:TemplateColumn HeaderText="Nomor SO">
	<HeaderStyle ForeColor="white"></HeaderStyle>
	<ItemStyle Width="100px"></ItemStyle>
	<ItemTemplate>
		<asp:Label ID="lblNoSO2" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SONumber") %>' ></asp:Label>
	</ItemTemplate>
</asp:TemplateColumn>
<asp:TemplateColumn HeaderText="Status PO">
	<HeaderStyle ForeColor="white"></HeaderStyle>
	<ItemStyle Width="100px"></ItemStyle>
	<ItemTemplate>
		<asp:Label ID="lblPOStatus" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.POStatus") %>' ></asp:Label>
	</ItemTemplate>
</asp:TemplateColumn>
<asp:TemplateColumn HeaderText="Nomor Giro">
	<HeaderStyle ForeColor="white"></HeaderStyle>
	<ItemStyle Width="100px"></ItemStyle>
	<ItemTemplate>
		<asp:Label ID="Label6" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.GiroNumber") %>' ></asp:Label>
	</ItemTemplate>
</asp:TemplateColumn>
<asp:TemplateColumn HeaderText="Status Giro">
	<HeaderStyle ForeColor="white"></HeaderStyle>
	<ItemStyle Width="100px"></ItemStyle>
	<ItemTemplate>
		<asp:Label ID="Label7" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.GiroStatus") %>' ></asp:Label>
	</ItemTemplate>
</asp:TemplateColumn>
<asp:TemplateColumn HeaderText="Date1">
	<HeaderStyle ForeColor="white"></HeaderStyle>
	<ItemStyle Width="100px"></ItemStyle>
	<ItemTemplate>
		<asp:Label ID="lblAmountDate1" Width="100%" Runat="server" Text='<%# FormatNumber(DataBinder.Eval(Container, "DataItem.AmountDate1"), 2, TriState.UseDefault, TriState.UseDefault, TriState.True) %>' style="text-align:right;" ></asp:Label>
	</ItemTemplate>
</asp:TemplateColumn>
<asp:TemplateColumn HeaderText="Date2">
	<HeaderStyle ForeColor="white"></HeaderStyle>
	<ItemStyle Width="100px"></ItemStyle>
	<ItemTemplate>
		<asp:Label ID="lblAmountDate2" Width="100%" Runat="server" Text='<%# FormatNumber(DataBinder.Eval(Container, "DataItem.AmountDate2"), 2, TriState.UseDefault, TriState.UseDefault, TriState.True) %>' style="text-align:right;" ></asp:Label>
	</ItemTemplate>
</asp:TemplateColumn>
<asp:TemplateColumn HeaderText="Date3">
	<HeaderStyle ForeColor="white"></HeaderStyle>
	<ItemStyle Width="100px"></ItemStyle>
	<ItemTemplate>
		<asp:Label ID="lblAmountDate3" Width="100%" Runat="server" Text='<%# FormatNumber(DataBinder.Eval(Container, "DataItem.AmountDate3"), 2, TriState.UseDefault, TriState.UseDefault, TriState.True) %>' style="text-align:right;" ></asp:Label>
	</ItemTemplate>
</asp:TemplateColumn>
<asp:TemplateColumn HeaderText="Date4">
	<HeaderStyle ForeColor="white"></HeaderStyle>
	<ItemStyle Width="100px"></ItemStyle>
	<ItemTemplate>
		<asp:Label ID="lblAmountDate4" Width="100%" Runat="server" Text='<%# FormatNumber(DataBinder.Eval(Container, "DataItem.AmountDate4"), 2, TriState.UseDefault, TriState.UseDefault, TriState.True) %>' style="text-align:right;" ></asp:Label>
	</ItemTemplate>
</asp:TemplateColumn>
<asp:TemplateColumn HeaderText="Date5">
	<HeaderStyle ForeColor="white"></HeaderStyle>
	<ItemStyle Width="100px"></ItemStyle>
	<ItemTemplate>
		<asp:Label ID="lblAmountDate5" Width="100%" Runat="server" Text='<%# FormatNumber(DataBinder.Eval(Container, "DataItem.AmountDate5"), 2, TriState.UseDefault, TriState.UseDefault, TriState.True) %>' style="text-align:right;" ></asp:Label>
	</ItemTemplate>
</asp:TemplateColumn>
</Columns>
</asp:DataGrid>
</AlternatingItemTemplate>

<HeaderStyle CssClass="titleTableSales">
</HeaderStyle>
</asp:DataList></DIV><!-- o2nB --></TD></TR>
  <TR>
    <TD>
<asp:Button id=btnBack runat="server" Text="Kembali"></asp:Button></TD></TR></asp:panel><asp:panel 
  id=pnlDetails Runat="server" Visible="False">
  <TR>
    <TD></TD></TR>
  <TR>
    <TD style="HEIGHT: 35px">
  <TR>
    <TD style="HEIGHT: 45px"></TD></TR>
  <TR>
    <TD><asp:button id=BtnDownloadDtl runat="server" Text="Download"></asp:button><BR 
      ></TD></TR>
  <TR>
    <TD></TD></TR>
  <TR></TR></TABLE></TR><TR><TD>
<TABLE border=0 cellSpacing=1 cellPadding=3 width="100%"><asp:repeater id=dtlDetails Runat="server">
<HeaderTemplate><tr>
					<td Class="titleTableSales" width="13%" >
						Tanggal Transaksi
					</td>						
					<td Class="titleTableSales" width="15%">
						Keterangan
					</td>
					<td Class="titleTableSales" width="10%">
						No Dokumen
					</td>		
					<td Class="titleTableSales" width="15%">
						Reference
					</td>		
					<td Class="titleTableSales" width="25%">
						Text
					</td>
					<td Class="titleTableSales" width="11%">
						Debet (Rp)
					</td>		
					<td Class="titleTableSales" width="11%">
						Kredit (Rp)																						
				</td></tr>				
</HeaderTemplate>
<ItemTemplate>
					<td style="color:Black; background-color:#F1F6FB">
						<asp:Label id="lblTransactionDate" runat="server" ></asp:Label>
					</td>						
					<td style="color:Black; background-color:#F1F6FB">
						<asp:Label id="Label9" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Tipe") %>'></asp:Label>
					</td>
					<td align="center" style="color:Black; background-color:#F1F6FB">
						<asp:Label id="Label10" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DocumentNumber") %>'></asp:Label>						
					</td>		
					<td align="center" style="color:Black; background-color:#F1F6FB">
						<asp:Label id="Label11" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Reff") %>'></asp:Label>						
					</td>		
					<td style="color:Black; background-color:#F1F6FB">
						<asp:Label id="Label12" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Description") %>'></asp:Label>						
					</td>
					<td align="right" style="color:Black; background-color:#F1F6FB">
						<asp:Label id="Label13" runat="server" ></asp:Label>
					</td>		
					<td align="right" style="color:Black; background-color:#F1F6FB">
						<asp:Label id="Label14" runat="server" ></asp:Label>
				</td>
</ItemTemplate>
<AlternatingItemTemplate>
					<td>
						<asp:Label id="Label15" runat="server" ></asp:Label>
					</td>						
					<td>
						<asp:Label id="Label16" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Tipe") %>'></asp:Label>
					</td>
					<td align="center">
						<asp:Label id="Label17" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DocumentNumber") %>'></asp:Label>						
					</td>		
					<td align="center">
						<asp:Label id="Label18" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Reff") %>'></asp:Label>						
					</td>		
					<td >
						<asp:Label id="Label19" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Description") %>'></asp:Label>						
					</td>
					<td align="right">
						<asp:Label id="Label20" runat="server" ></asp:Label>
					</td>		
					<td align="right">
						<asp:Label id="Label21" runat="server" ></asp:Label>
				</td>
</AlternatingItemTemplate>

</asp:repeater></TABLE></TD></TR><TR><TD></TD></TR><TR><TD>
</TD></TR></asp:panel></TABLE>
<!-- DataGrid --></form>
<script>
	HideThirdRow();
	FormatDTGCair();
</script>

	</body>
</HTML>
