<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmListEstimationEquip.aspx.vb" Inherits="FrmListEstimationEquip"%>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmListEstimationEquip</title>
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
        
        function ShowDocFlow(estimationnumber)
        {
            showPopUp('../PopUp/PopUpDocFlowEqpPo.aspx?estimationnumber=' + estimationnumber,'',300,560,DealerSelection);
        }


        function ShowLastUpdatedHistory(urlParams)
        {
            showPopUp('../PopUp/PopupDataHistory.aspx?'+urlParams,'',500,760,DealerSelection);
        }
        function ShowLastUpdatedHistory20130722(DocNumber)
        {
            showPopUp('../PopUp/PopUpChangeStatusHistorySV.aspx?DocType=3&DocNumber=' + DocNumber,'',500,760,DealerSelection);
        }
        function ShowLastUpdatedHistory2(DocNumber)
        {
            showPopUp('../PopUp/PopUpChangeStatusHistorySV.aspx?DocType=4&DocNumber=' + DocNumber,'',500,760,DealerSelection);
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
		
		function PONOSelection(POSelection)
		{
		    
			var txtNoPO= document.getElementById("txtNoPO");
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
			<div style="PADDING-BOTTOM:5px;PADDING-LEFT:5px;PADDING-RIGHT:5px;PADDING-TOP:5px" class="titlePage">
				<b>INDENT PART EQUIPMENT - Daftar Status Estimasi Indent Part Equipment</b>
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
				<TR>
					<TD style="WIDTH: 190px">Nomor Permintaan Estimasi</TD>
					<TD style="WIDTH: 5px">:</TD>
					<TD style="WIDTH: 500px">
						<asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$')" id="txtNoPO" onblur="omitSomeCharacter('txtNoPO','<>?*%$')"
							runat="server" Width="160px"></asp:textbox><asp:label id="lblPopUpPONo" runat="server">
							<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:label>
					</TD>
				</TR>
				<TR>
					<TD style="WIDTH: 190px">Tanggal Pengajuan Estimasi</TD>
					<TD style="WIDTH: 5px">:</TD>
					<TD style="WIDTH: 500px">
						<table cellpadding="0" cellspacing="0" border="0">
							<tr>
								<td>
									<cc1:inticalendar id="icPODateFrom" runat="server" TextBoxWidth="70"></cc1:inticalendar>
								</td>
								<td>
									&nbsp;s/d&nbsp;
								</td>
								<td>
									<cc1:inticalendar id="icPODateUntil" runat="server" TextBoxWidth="70"></cc1:inticalendar>
								</td>
							</tr>
						</table>
					</TD>
				</TR>
				<TR>
					<TD style="WIDTH: 190px">Status</TD>
					<TD style="WIDTH: 5px">:</TD>
					<TD style="WIDTH: 500px">
						<asp:listbox id="lstStatus" runat="server" Width="160px" SelectionMode="Multiple" Rows="3"></asp:listbox>
					</TD>
				</TR>
				<TR>
					<TD style="WIDTH: 190px"></TD>
					<TD style="WIDTH: 5px"></TD>
					<TD style="WIDTH: 500px">
						<P><asp:button id="btnSearch" runat="server" width="60px" Text="Cari"></asp:button></P>
					</TD>
				</TR>
				<TR>
					<TD style="WIDTH: 190px"></TD>
					<TD style="WIDTH: 5px"></TD>
					<TD style="WIDTH: 500px"></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 190px"><asp:label id="lblTotalAmount" runat="server" Font-Bold="True">Total Amount: Rp. </asp:label></TD>
					<TD style="WIDTH: 5px"></TD>
					<TD style="WIDTH: 500px">
						<asp:label id="lblTotalOrder" runat="server" Font-Bold="True">Total Order: 0</asp:label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
						<asp:label id="lblGrandTotal" runat="server" Font-Bold="True">Grand Total Item: 0</asp:label>
					</TD>
				</TR>
			</TABLE>
			<div id="div1" style="HEIGHT: 300px; OVERFLOW: auto">
				<asp:datagrid id="dtgIndentPart" runat="server" Width="100%" PageSize="25" GridLines="None" CellPadding="3"
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
						<asp:TemplateColumn>
							<HeaderStyle Width="5%" CssClass="titleTableParts"></HeaderStyle>
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
							<HeaderStyle Width="5%" CssClass="titleTableParts"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
						</asp:TemplateColumn>
						<asp:TemplateColumn SortExpression="Status" HeaderText="Status">
							<HeaderStyle Width="14%" CssClass="titleTableParts"></HeaderStyle>
							<ItemTemplate>
								<asp:Label Runat="server" ID="lblStatus" text='<%# Databinder.Eval(Container, "DataItem.StatusDesc")%>'>
								</asp:Label>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn SortExpression="Dealer.DealerCode" HeaderText="Kode Dealer">
							<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
							<ItemTemplate>
								<asp:Label id="Label1" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerCode") %>' runat="server">
								</asp:Label>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn SortExpression="Dealer.DealerName" HeaderText="Nama Dealer">
							<HeaderStyle Width="25%" CssClass="titleTableParts"></HeaderStyle>
							<ItemTemplate>
								<asp:Label id="Label2" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerName") %>' runat="server">
								</asp:Label>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:BoundColumn DataField="EstimationNumber" SortExpression="EstimationNumber" HeaderText="Nomor Pengajuan">
							<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
						</asp:BoundColumn>
                        <asp:TemplateColumn SortExpression="DepositBKewajibanHeaderID" HeaderText="Nomor Kewajiban">
							<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
							<ItemTemplate>
								<asp:Label id="lblNoRegKewajiban" runat="server">
								</asp:Label>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:BoundColumn DataField="CreatedTime" SortExpression="CreatedTime" HeaderText="Tanggal Pengajuan"
							DataFormatString="{0:dd MMM yyyy}">
							<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
							<ItemStyle HorizontalAlign="Center"></ItemStyle>
						</asp:BoundColumn>
						<asp:TemplateColumn HeaderText="Total Qty">
							<HeaderStyle Width="8%" CssClass="titleTableParts"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<ItemTemplate>
								<asp:Label Runat="server" ID="lblQty" text='<%# Databinder.Eval(Container, "DataItem.TotalQty")%>' CssClass="textRight">
								</asp:Label>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Jumlah Permintaan Estimasi (Rp)">
							<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<ItemTemplate>
								<asp:Label id="Label3" Text='<%# DataBinder.Eval(Container, "DataItem.TotalAmount", "{0:#,##0}") %>' runat="server">
								</asp:Label>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn>
							<HeaderStyle Width="15%" CssClass="titleTableParts"></HeaderStyle>
							<ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
							<ItemTemplate>
								<asp:Label id="lblDocFlow" runat="server" height="10px" ToolTip="Klik Disini untuk melihat document flow">
									<img style="cursor:hand" alt="Klik Disini untuk melihat document flow" src="../images/alur_flow.gif"
										border="0"></asp:Label>
								<asp:Label id="lblLastUpdated" runat="server" height="10px" ToolTip="History Estimasi">
									<img style="cursor:hand" alt="History Estimasi" src="../images/popup.gif"
										border="0"></asp:Label>
								<asp:LinkButton id="lbtnEdit" CausesValidation="False" CommandName="edit" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>' 
                                    text="Edit" Runat="server" ToolTip="Edit">
									<img src="../images/edit.gif" border="0" alt="Edit"></asp:LinkButton>
								<asp:LinkButton id="lbtnDetail" CausesValidation="False" CommandName="detail" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>' 
                                    text="Lihat" Runat="server" ToolTip="Detail">
									<img src="../images/detail.gif" border="0" alt="Detail"></asp:LinkButton>
                                <asp:LinkButton id="lbtnDelete" CausesValidation="False" CommandName="delete" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>' 
                                    text="Delete" Runat="server" ToolTip="Delete">
									<img src="../images/trash.gif" border="0" alt="Detail"></asp:LinkButton>
							</ItemTemplate>
						</asp:TemplateColumn>
					</Columns>
					<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
				</asp:datagrid>
			</div>
			<br>
			<div>
				<asp:Panel id="pnlStatus" runat="server" style="FLOAT:left">
                 Mengubah Status : 
<asp:dropdownlist id="ddlupdatestatus" runat="server" Width="152px"></asp:dropdownlist>
<asp:button id="btnProses" runat="server" Text="Proses" width="60px"></asp:button>
                </asp:Panel>
				<asp:button id="btnDownload" runat="server" Text="Download Pesanan" width="168px" Visible="True"></asp:button>
				<div style="CLEAR:both"></div>
			</div>
		</form>
	</body>
</HTML>
