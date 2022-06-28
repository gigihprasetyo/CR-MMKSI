<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmPoListEstimationEquip.aspx.vb" Inherits="FrmPoListEstimationEquip" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmPoListEstimationEquip</title>
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
        
        function PopUpSPPOPrintBill(intPoId)
        {
            showPopUp('../PopUp/PopUpSPPOPrintBill.aspx?id='+ intPoId,'',330,510, PrintBillSelection);
        }

        function PrintBillSelection(val)
        {
            
        }
        
        function cekPaymentStatus()
        {
            var ddlStatus = document.getElementById("ddlStatus");
            var index = ddlStatus.selectedIndex;
            var val = ddlStatus.options[index].value;
            if (val != "2") 
            {
			    var hdnRejectDesc = document.getElementById("hdnRejectDesc");
			    hdnRejectDesc.value = "";
                return;
            }
            
            showPopUp('../PopUp/PopUpSPPORejectDescription.aspx','',200,360, DescSelection);
        }
        
        function DescSelection(desc)
        {
			var hdnRejectDesc = document.getElementById("hdnRejectDesc");
			hdnRejectDesc.value = desc;
        }
        
        function ShowDocFlow(ponumber)
        {
            showPopUp('../PopUp/PopUpDocFlowEqpPo.aspx?ponumber=' + ponumber,'',300,560,DealerSelection);
        }
        
        function ShowLastUpdatedHistory(urlParams)
        {
            showPopUp('../PopUp/PopupDataHistory.aspx?'+urlParams,'',500,760,DealerSelection);
        }
        function ShowLastUpdatedHistory20130722(DocNumber)
        {
            showPopUp('../PopUp/PopUpChangeStatusHistorySV.aspx?DocType=3&DocNumber=' + DocNumber,'',500,760,DealerSelection);
        }
    
		function ShowPPDealerSelection()
		{
			showPopUp('../SparePart/../PopUp/PopUpDealerSelection.aspx','',500,760,DealerSelection);
		}

		function DealerSelection(selectedDealer)
		{
			var txtDealer = document.getElementById("txtDealerCode");
			txtDealer.value = selectedDealer;				
		}
		
		function PONOSelection(PONOSelection)
		{
		    
			var txtPONumber= document.getElementById("txtPONumber");
			txtPONumber.value = PONOSelection;				
		}

		function POSelection(POSelection)
		{
		    
			var txtNoPO= document.getElementById("txtEstimationNumber");
			txtNoPO.value = POSelection;				
		}
		
		function CheckAll(aspCheckBoxID, checkVal) 
		{
			re = new RegExp(':' + aspCheckBoxID + '$')  
			for(i = 0; i < document.forms[0].elements.length; i++) {
				elm = document.forms[0].elements[i]
				if (elm.type == 'checkbox') {
					if (re.test(elm.name)) {
					elm.checked = checkVal
					}
				}
			}
		}
		
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<div class="titlePage">INDENT PART EQUIPMENT - Daftar Status Pengajuan Indent Part 
				Equipment
			</div>
			<TABLE id="Table1" cellSpacing="1" cellPadding="1" border="0">
				<TR>
					<TD style="WIDTH: 150px">Kode Dealer</TD>
					<TD style="WIDTH: 3px">:</TD>
					<TD style="WIDTH: 300px"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$')" id="txtDealerCode" onblur="omitSomeCharacter('txtDealerCode','<>?*%$')"
							runat="server" Width="160px"></asp:textbox><asp:label id="lblSearchDealer" runat="server">
							<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:label></TD>
					<TD style="WIDTH: 300px"><STRONG>Keterangan :</STRONG></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 152px">Nomor Pengajuan</TD>
					<TD style="WIDTH: 3px">:</TD>
					<TD style="WIDTH: 300px"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$')" id="txtPONumber" onblur="omitSomeCharacter('txtNoPO','<>?*%$')"
							runat="server" Width="160px"></asp:textbox><asp:label id="lblPopUpPoNo" runat="server">
							<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:label></TD>
					</TD>
					<TD style="WIDTH: 300px"><IMG alt="Merah" src="../images/red.gif" border="0">&nbsp;: 
						MMKSI belum mengalokasikan pesanan
					</TD>
				</TR>
				<TR>
					<TD style="WIDTH: 150px">Nomor Estimasi</TD>
					<TD style="WIDTH: 3px">:</TD>
					<TD style="WIDTH: 300px"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$')" id="txtEstimationNumber"
							onblur="omitSomeCharacter('txtNoPO','<>?*%$')" runat="server" Width="160px"></asp:textbox><asp:label id="lblPopUpEstNo" runat="server">
							<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:label></TD>
					<TD style="WIDTH: 300px"><IMG alt="Kuning" src="../images/yellow.gif" border="0">&nbsp;: 
						MMKSI baru memenuhi sebagian alokasi pesanan
					</TD>
				</TR>
				<TR>
					<TD style="WIDTH: 150px" vAlign="top">Status</TD>
					<TD style="WIDTH: 3px" vAlign="top">:</TD>
					<TD style="WIDTH: 300px"><asp:listbox id="lstStatus" runat="server" Width="160px" Rows="6" SelectionMode="Multiple"></asp:listbox></TD>
					<TD style="WIDTH: 300px" vAlign="top"><IMG alt="Hijau" src="../images/green.gif" border="0">&nbsp;: 
						MMKSI sudah mengalokasikan semua pesanan
					</TD>
				</TR>
				<TR>
					<TD style="WIDTH: 150px">Tanggal Pengajuan</TD>
					<TD style="WIDTH: 3px">:</TD>
					<TD style="WIDTH: 300px" vAlign="middle" colSpan="2">
						<table cellSpacing="0" cellPadding="0" border="0">
							<tr>
								<td><cc1:inticalendar id="icPODateFrom" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
								<td>&nbsp;s/d&nbsp;
								</td>
								<td><cc1:inticalendar id="icPODateUntil" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
							</tr>
						</table>
					</TD>
				</TR>
				<TR>
					<TD style="WIDTH: 150px">Tipe Barang</TD>
					<TD style="WIDTH: 3px">:</TD>
					<TD style="WIDTH: 300px">Equipment</TD>
					<TD style="WIDTH: 300px"></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 150px">Tipe Pembayaran</TD>
					<TD style="WIDTH: 3px">:</TD>
					<TD style="WIDTH: 300px"><asp:dropdownlist id="ddlPaymentType" runat="server" Width="120px"></asp:dropdownlist></TD>
					<TD style="WIDTH: 300px"></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 150px; HEIGHT: 23px"></TD>
					<TD style="WIDTH: 3px; HEIGHT: 23px"></TD>
					<TD style="WIDTH: 300px; HEIGHT: 23px"><asp:button id="btnFind" runat="server" Text="Cari" Width="96px"></asp:button></TD>
					<TD style="WIDTH: 300px; HEIGHT: 23px"></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 150px"></TD>
					<TD style="WIDTH: 3px"></TD>
					<TD style="WIDTH: 300px"></TD>
					<TD style="WIDTH: 300px"></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 150px"><asp:label id="lblTotalAmount" runat="server" Font-Bold="True">Total Amount: Rp. </asp:label></TD>
					<TD style="WIDTH: 3px"></TD>
					<TD style="WIDTH: 300px"><asp:label id="lblTotalOrder" runat="server" Font-Bold="True">Total Order: 0</asp:label></TD>
					<TD style="WIDTH: 300px"><asp:label id="lblGrandTotal" runat="server" Font-Bold="True">Grand Total Item: 0</asp:label></TD>
				</TR>
			</TABLE>
			<div id="div1" style="HEIGHT: 500px; OVERFLOW: auto">
				<asp:datagrid id="dtgEquipPO" runat="server" Width="100%" AllowSorting="True" CellSpacing="1"
					AutoGenerateColumns="False" BorderColor="#CDCDCD" BorderStyle="None" BorderWidth="0px" AllowPaging="True"
					AllowCustomPaging="True" BackColor="#CDCDCD" CellPadding="3" GridLines="None" PageSize="25">
					<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
					<AlternatingItemStyle ForeColor="Black" BackColor="White"></AlternatingItemStyle>
					<ItemStyle ForeColor="Black" BackColor="#FDF1F2"></ItemStyle>
					<HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#CC3333"></HeaderStyle>
					<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
					<Columns>
						<asp:TemplateColumn>
							<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
							<ItemStyle HorizontalAlign="Center"></ItemStyle>
							<HeaderTemplate>
								<input id="chkAllItems" type="checkbox" onclick="CheckAll('chkItemChecked',
											document.forms[0].chkAllItems.checked)" />
							</HeaderTemplate>
							<ItemTemplate>
								<asp:CheckBox id="chkItemChecked" runat="server"></asp:CheckBox>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="No.">
							<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
						</asp:TemplateColumn>
						<asp:TemplateColumn>
							<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
							<ItemStyle HorizontalAlign="Center"></ItemStyle>
							<ItemTemplate>
								<asp:Image id="imgIndikator" runat="server"></asp:Image>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID"></asp:BoundColumn>
						<asp:TemplateColumn SortExpression="StatusDesc" HeaderText="Status">
							<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
							<ItemTemplate>
								<asp:Label Runat="server" ID="lblStatus"></asp:Label>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:BoundColumn DataField="DealerCode" SortExpression="DealerCode" HeaderText="Kode Dealer">
							<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="DealerName" SortExpression="DealerName" HeaderText="Nama Dealer">
							<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="RequestNo" SortExpression="RequestNo" HeaderText="Nomor Pengajuan">
							<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="EstimationNumber" SortExpression="EstimationNumber" HeaderText="Nomor Estimasi">
							<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="CreatedTime" SortExpression="CreatedTime" HeaderText="Tanggal Pengajuan"
							DataFormatString="{0:dd MMM yyyy}">
							<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
							<ItemStyle HorizontalAlign="Center"></ItemStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="PaymentTypeDesc" SortExpression="PaymentTypeDesc" HeaderText="Tipe Pembayaran">
							<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
						</asp:BoundColumn>
						<asp:TemplateColumn HeaderText="Deadline Kwintansi">
							<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
							<ItemTemplate>
								<asp:Label Runat="server" ID="lblDeadlineKwitansi"></asp:Label>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Total Order">
							<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
							<ItemTemplate>
								<asp:Label Runat="server" ID="lblTotalTagihan"></asp:Label>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:BoundColumn DataField="TotalItem" SortExpression="TotalItem" HeaderText="Total Qty">
							<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="SisaQty" SortExpression="SisaQty" HeaderText="Sisa Qty">
							<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
						</asp:BoundColumn>
						<asp:TemplateColumn>
							<HeaderStyle Width="15%" CssClass="titleTableParts"></HeaderStyle>
							<ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
							<ItemTemplate>
								<asp:Label id="lblLastUpdated" runat="server" height="10px" ToolTip="History Estimasi">
									<img style="cursor:hand" alt="History Estimasi" src="../images/popup.gif" border="0"></asp:Label>
								<asp:Label id="lblDocFlow" runat="server" height="10px" ToolTip="Klik Disini untuk melihat document flow">
									<img style="cursor:hand" alt="Klik Disini untuk melihat document flow" src="../images/alur_flow.gif"
										border="0"></asp:Label>
								<asp:Label id="lblPrint" text="Klik Disini Untuk Print Kwitansi" Runat="server" Visible="False" 
                                    tooltip="Klik Disini Untuk Print Kwitansi">
									<img style="cursor:hand" src="../images/print.gif" border="0" alt="Detail"></asp:Label>
								<asp:LinkButton id="lbtnEdit" CausesValidation="False" CommandName="edit" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>' 
                                    text="Edit" Runat="server" ToolTip="Edit">
									<img src="../images/edit.gif" border="0" alt="Edit"></asp:LinkButton>
								<asp:LinkButton id="lbtnDetail" CausesValidation="False" CommandName="detail" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>' 
                                    text="Lihat" Runat="server" ToolTip="Detail">
									<img src="../images/detail.gif" border="0" alt="Detail"></asp:LinkButton>
								<asp:LinkButton id="lbtnPrint" CausesValidation="False" CommandName="Print" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>' 
                                    text="Print" Runat="server" ToolTip="Print">
									<img src="../images/print.gif" border="0" alt="Print"></asp:LinkButton>
                                 <asp:LinkButton id="lbtnDelete" CausesValidation="False" CommandName="delete" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>' 
                                    text="Delete" Runat="server" ToolTip="Delete">
									<img src="../images/trash.gif" border="0" alt="Detail" onclick="return confirm('Yakin ingin menghapus data ini?');"></asp:LinkButton>
							</ItemTemplate>
						</asp:TemplateColumn>
					</Columns>
					<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
				</asp:datagrid></div>
			<div>Mengubah Status :
				<asp:dropdownlist id="ddlStatus" onchange="javascript:cekPaymentStatus();" Runat="server"></asp:dropdownlist><asp:button id="btnProcess" Text="Process" Runat="server"></asp:button><asp:button id="btnDownload" Text="Download" Runat="server"></asp:button><input id="hdnRejectDesc" type="hidden" name="hdnRejectDesc" runat="server">
			</div>
		</form>
	</body>
</HTML>
