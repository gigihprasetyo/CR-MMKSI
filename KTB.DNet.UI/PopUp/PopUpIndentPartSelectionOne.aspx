<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>

<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PopUpIndentPartSelectionOne.aspx.vb" Inherits=".PopUpIndentPartSelectionOne" smartNavigation="False" %>


<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
	<title>PopUpIndentPart</title>
	<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
	<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
	<meta content="JavaScript" name="vs_defaultClientScript">
	<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	<link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
	<script language="javascript" src="../WebResources/InputValidation.js"></script>
	<base target="_self">

	<script language="javascript">

		function ShowPPDealerSelection() {
			showPopUp('../PopUp/PopUpDealerSelectionOne.aspx', '', 500, 760, DealerSelection);
		}
		function DealerSelection(selectedDealer) {
			var tempParam = selectedDealer.split(';');
			var txtDealer = document.getElementById("txtDealerCode");

			txtDealer.value = tempParam[0];
		}

		function GetSelectedPONOMany() {
		    var table;
		    var bcheck = false;
		    table = document.getElementById("dtgIndentPart");

		    var Indent = '';
		    for (i = 1; i < table.rows.length; i++) {
		        var rdoButton = table.rows[i].cells[0].getElementsByTagName("INPUT")[0];
		        if (rdoButton != null && rdoButton.checked) {
		            if (navigator.appName == "Microsoft Internet Explorer") {
		                if (Indent == '') {
		                    Indent = replace(table.rows[i].cells[2].innerText, ' ', '');
		                }
		                else {
		                    Indent = Indent + ';' + replace(table.rows[i].cells[2].innerText, ' ', '');
		                }
		                window.returnValue = Indent;
		                bcheck = true;
		            }
		            else if (navigator.appName == "Netscape") {
		                if (Indent == '') {
		                    Indent = replace(table.rows[i].cells[2].innerText, ' ', '');
		                }
		                else {
		                    Indent = Indent + ';' + replace(table.rows[i].cells[2].innerText, ' ', '');
		                }
		                bcheck = true;
		            }
		            else {
		                if (Indent == '') {
		                    Indent = replace(table.rows[i].cells[2].getElementsByTagName("span")[0].innerHTML, ' ', '');
		                }
		                else {
		                    Indent = Indent + ';' + replace(table.rows[i].cells[2].getElementsByTagName("span")[0].innerHTML, ' ', '');
		                }

		                bcheck = true;
		            }
		        }
		    }
		    if (navigator.appName != "Microsoft Internet Explorer") {
		        opener.dialogWin.returnFunc(Indent);
		    }

		    if (bcheck) {
		        window.close();
		    }
		    else {
		        alert("Silahkan Pilih Data terlebih dahulu");
		    }
		}
	</script>
</head>
<body ms_positioning="GridLayout">
	<form id="form1" method="post" runat="server">
		<table width="100%">
			<tr>
				<td class="titleField" style="height: 26px" valign="top" width="15%">Kode Dealer</td>
				<td width="1%">:</td>
				<td>
					<asp:TextBox ID="txtDealerCode" runat="server" Width="152px" onkeypress="return alphaNumericExcept(event,'<>?*%$')"
						onblur="omitSomeCharacter('txtDealerCode','<>?*%$;')"></asp:TextBox>
					<asp:Label ID="lblSearchDealer" runat="server">
						<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup">
					</asp:Label>
				</td>
			</tr>
			<tr>
				<td class="titleField" style="height: 26px" valign="top" width="15%">Tanggal Pengajuan</td>
				<td style="height: 26px" valign="top" width="1%">:</td>
				<td style="height: 26px" width="50%">
					<table id="Table2" cellspacing="0" cellpadding="0" border="0">
						<tr>
							<td>
								<cc1:IntiCalendar ID="icPODateFrom" runat="server"></cc1:IntiCalendar></td>
							<td>&nbsp;s/d&nbsp;</td>
							<td>
								<cc1:IntiCalendar ID="icPODateUntil" runat="server"></cc1:IntiCalendar>
							</td>
						</tr>
					</table>
				</td>
			</tr>
			<tr>
				<td class="titleField" style="height: 26px" valign="top" width="15%">Tipe Barang</td>
				<td width="1%">:</td>
				<td width="50%">
					<asp:DropDownList ID="ddlMaterialType" runat="server" Width="160px"></asp:DropDownList>
				</td>
			</tr>
			<tr>
				<td width="30%"></td>
				<td width="1%"></td>
				<td>
					<asp:Button ID="btnSearch" runat="server" Width="60px" Text="Cari"></asp:Button>
				</td>
			</tr>
			<tr>
				<td colspan="3">
					<div id="div1" style="overflow: auto; height: 310px">
						<asp:DataGrid ID="dtgIndentPart" runat="server" Width="100%" AllowSorting="True" CellSpacing="1"
							AutoGenerateColumns="False" BorderColor="#CDCDCD" BorderStyle="None" BorderWidth="0px" AllowPaging="True" AllowCustomPaging="True" BackColor="#CDCDCD"
							CellPadding="3" GridLines="None" PageSize="15">
							<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
							<AlternatingItemStyle ForeColor="Black" BackColor="White"></AlternatingItemStyle>
							<ItemStyle ForeColor="Black" BackColor="#FDF1F2"></ItemStyle>
							<HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#CC3333"></HeaderStyle>
							<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
							<Columns>
								<asp:TemplateColumn>
									<HeaderStyle Width="5%" CssClass="titleTableParts"></HeaderStyle>
									<%--<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<HeaderTemplate>
										<input id="chkAllItems" type="checkbox" onclick="CheckAll('chkItemChecked',
														document.forms[0].chkAllItems.checked)" />
									</HeaderTemplate>
									<ItemTemplate>
										<asp:CheckBox ID="chkItemChecked" runat="server"></asp:CheckBox>
									</ItemTemplate>--%>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="No.">
									<HeaderStyle Width="5%" CssClass="titleTableParts"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
									<ItemTemplate>
										<asp:Label ID="lblNo" runat="server"></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="RequestNo" HeaderText="Nomor Pengajuan">
									<HeaderStyle Width="15%" CssClass="titleTableParts"></HeaderStyle>
									<ItemTemplate>
										<asp:Label ID="lblNoPO" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.RequestNo") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="RequestDate" SortExpression="RequestDate" HeaderText="Tanggal Pengajuan"
									DataFormatString="{0:dd/MM/yyyy}">
									<HeaderStyle Width="15%" CssClass="titleTableParts"></HeaderStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn SortExpression="Dealer.DealerCode" HeaderText="Kode Dealer">
									<HeaderStyle Width="15%" CssClass="titleTableParts"></HeaderStyle>
									<ItemTemplate>
										<asp:Label ID="lblDealerCode" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerCode") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="Dealer.DealerName" HeaderText="Nama Dealer">
									<HeaderStyle Width="45%" CssClass="titleTableParts"></HeaderStyle>
									<ItemTemplate>
										<asp:Label ID="lblDealerName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerName") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
						</asp:DataGrid>
					</div>
				</td>
			</tr>
			<tr>
				<td colspan="3" align="center">&nbsp;<input id="btnChoose" style="width: 60px" disabled onclick="GetSelectedPONOMany()" type="button"
					value="Pilih" name="btnChoose" runat="server">
					<input id="btnCancel" style="width: 60px" onclick="window.close();" type="button" value="Tutup"
						name="btnCancel">
				</td>
			</tr>
		</table>
	</form>
</body>
</html>
