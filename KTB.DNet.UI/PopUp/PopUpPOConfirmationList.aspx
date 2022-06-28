<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PopUpPOConfirmationList.aspx.vb" Inherits=".PopUpPOConfirmationList" %>
<%@ Register TagPrefix="cc1" Namespace="FilterCompositeControl" Assembly="FilterCompositeControl" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>PopUpPO</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<base target="_self">
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript">

		    function GetSelectedPO() {
		        var table;
		        var poNumbers = '';
		        var poNumber = '';
		        var bcheck = false;
		        table = document.getElementById("dtgPO");

		        for (i = 1; i < table.rows.length; i++) {
		            var CheckBox = table.rows[i].cells[0].getElementsByTagName("INPUT")[0];
		            if (CheckBox != null && CheckBox.checked) {
		                if (navigator.appName == "Microsoft Internet Explorer") {
		                    if (poNumbers == '') {
		                        poNumbers = replace(table.rows[i].cells[1].innerText, ' ', '');
		                    }
		                    else {
		                        poNumbers = poNumbers + ';' + replace(table.rows[i].cells[1].innerText, ' ', '');
		                    }
		                    window.returnValue = poNumbers;
		                    bcheck = true;
		                }
		                else if (navigator.appName == "Netscape") {
		                    if (poNumbers == '') {
		                        poNumbers = replace(table.rows[i].cells[1].innerText, ' ', '');
		                    }
		                    else {
		                        poNumbers = poNumbers + ';' + replace(table.rows[i].cells[1].innerText, ' ', '');
		                    }
		                    opener.dialogWin.returnFunc(poNumbers);
		                    bcheck = true;
		                }
		                else {
		                    poNumber = table.rows[i].cells[1].getElementsByTagName("SPAN")[0].innerHTML;
		                    if (poNumber == '') {
		                        poNumbers = poNumber;
		                    }
		                    else {
		                        poNumbers = poNumbers + ';' + poNumber;
		                    }
		                    opener.dialogWin.returnFunc(poNumbers);
		                    bcheck = true;
		                }
		            }
		        }
		        if (bcheck) {
		            window.close();
		        }
		        else {
		            alert("Silahkan Pilih Nomor Barang terlebih dahulu");
		        }
		    }

		    function CheckAll(aspCheckBoxID, checkVal) {
		        re = new RegExp(':' + aspCheckBoxID + '$')
		        for (i = 0; i < document.forms[0].elements.length; i++) {
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
	<body bottomMargin="10" topMargin="10">
		<form id="Form2" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="10" cellPadding="0" width="100%" border="0">
				<tr>
					<td>
						<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="titlePage" height="20">&nbsp;&nbsp;SPAREPART - Daftar PO</td>
							</tr>
							<tr>
								<td background="../images/bg_hor_parts.gif" height="1"><IMG height="1" src="../images/bg_hor_parts.gif" border="0"></td>
							</tr>
							<tr>
								<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
							</tr>
							<TR>
								<td><cc1:compositefilter id="cfPO" runat="server" DataGridSouce="dtgPO"></cc1:compositefilter></td>
							</TR>
							<TR>
								<TD>
									<div id="div1" style="HEIGHT: 310px; OVERFLOW: auto"><asp:datagrid id="dtgPO" runat="server" PageSize="20" AllowSorting="True" AutoGenerateColumns="False"
											BorderColor="Gainsboro" BackColor="Gainsboro" CellPadding="3" CellSpacing="1" BorderWidth="0px" Width="100%" AllowCustomPaging="True" AllowPaging="True">
											<AlternatingItemStyle ForeColor="Black" BackColor="White"></AlternatingItemStyle>
											<ItemStyle ForeColor="Black" BackColor="#F1F6FB"></ItemStyle>
											<Columns>
												<asp:TemplateColumn>
													<HeaderStyle ForeColor="White" Width="1%" CssClass="titleTableParts"></HeaderStyle>
													<HeaderTemplate>
														<input id="chkAllItems" type="checkbox" onclick="CheckAll('chkItemChecked',
														document.forms[0].chkAllItems.checked)" />
													</HeaderTemplate>
													<ItemTemplate>
														<asp:CheckBox id="chkItemChecked" runat="server"></asp:CheckBox>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="PoNumber" HeaderText="Nomor PO">
													<HeaderStyle ForeColor="White" Width="30%" CssClass="titleTableParts"></HeaderStyle>
													<ItemTemplate>
														<asp:Label id="lblPoNumber" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PoNumber")%>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="PoDate" HeaderText="Tanggal PO">
													<HeaderStyle ForeColor="White" Width="40%" CssClass="titleTableParts"></HeaderStyle>
													<ItemTemplate>
														<asp:Label id="lblPoDate" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PoDate")%>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
											</Columns>
											<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
										</asp:datagrid></div>
								</TD>
							</TR>
							<TR>
								<TD align="center"><br>
									<INPUT id="btnChoose" style="WIDTH: 60px" onclick="GetSelectedPO()" type="button" value="Pilih"
										name="btnChoose">&nbsp;<INPUT id="btnCancel" style="WIDTH: 60px" onclick="window.close()" type="button" value="Tutup"
										name="btnCancel"></TD>
							</TR>
						</TABLE>
						<INPUT id="Hidden1" type="hidden" name="Hidden1" runat="server" style="Z-INDEX: 0">
					</td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
