<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PopUpPODestinationSelection.aspx.vb" Inherits=".PopUpPODestinationSelection" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>PopUp PO Destination</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<base target="_self">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript">

		    function trim(str) {
		        if (!str || typeof str != 'string')
		            return null;

		        return str.replace(/^[\s]+/, '').replace(/[\s]+$/, '').replace(/[\s]{2,}/, ' ');
		    }

		    function GetSelectedPODestination() {
		        var table;
		        var bcheck = false;
		        table = document.getElementById("dtgPODestination");
		        var PODestinationCode = '';

		        for (i = 1; i < table.rows.length; i++) {
		            var radioBtn = table.rows[i].cells[0].getElementsByTagName("INPUT")[0];
		            if (radioBtn != null && radioBtn.checked) {
		                if (navigator.appName == "Microsoft Internet Explorer") {
		                    PODestinationCode = trim(table.rows[i].cells[1].innerText) + ";" + trim(table.rows[i].cells[2].innerText) + ";" + trim(table.rows[i].cells[3].innerText);
		                    window.returnValue = PODestinationCode;
		                    bcheck = true;
		                    break;
		                }
		                else if (navigator.appName == "Netscape") {
		                    PODestinationCode = trim(table.rows[i].cells[1].innerText) + ";" + trim(table.rows[i].cells[2].innerText) + ";" + trim(table.rows[i].cells[3].innerText);
		                    opener.dialogWin.returnFunc(PODestinationCode);
		                    bcheck = true;
		                    break;
		                }
		                else {
		                    if (PODestinationCode == '') {
		                        PODestinationCode = replace(table.rows[i].cells[1].innerHTML, ' ', '') + ';' + replace(table.rows[i].cells[2].innerHTML, ' ', '') + ';' + replace(table.rows[i].cells[3].getElementsByTagName("span")[0].innerHTML, ' ', '');
		                    }
		                    window.close();
		                    opener.dialogWin.returnFunc(PODestinationCode);
		                    bcheck = true;
		                    break;
		                }
		            }
		        }

		        if (bcheck) {
		            window.close();
		        }
		        else {
		            alert("Silahkan pilih PO Destination");
		        }
		    }


		    function getQueryVariable(variable) {
		        var query = window.location.search.substring(1);
		        var vars = query.split("&");
		        for (var i = 0; i < vars.length; i++) {
		            var pair = vars[i].split("=");
		            if (pair[0] == variable) {
		                return pair[1];
		            }
		        }
		        return "nothing";
		    }

		    function ClosePopUp() {
		        window.close();
		    }
		</script>
        <style type="text/css">
          .hiddencol
          {
            display: none;
          }
        </style>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE border="0" width="100%" cellpadding="0" cellspacing="0">
				<tr>
					<td class="titlePage" style="HEIGHT: 21px">PO - Destination Selection</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<TR>
					<TD align="left">
						<TABLE id="Table2" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR>
								<TD class="titleField" width="91" style="WIDTH: 140px">PO Destination Code</TD>
								<TD width="1%">:</TD>
								<TD width="75%"><asp:textbox id="txtPODestinationCode" onkeypress="return alphaNumericExcept(event,'<>?*%$;')" onblur="omitSomeCharacter('txtPODestinationCode','<>?*%$;')"
										runat="server" Width="230px"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="titleField" width="91" style="WIDTH: 140px">PO Destination Name</TD>
								<TD width="1%">:</TD>
								<TD width="75%"><asp:textbox id="txtNama" runat="server" Width="228px"></asp:textbox></TD>
							<TR>
								<TD class="titleField" width="91" style="WIDTH: 91px"></TD>
								<TD width="1%"></TD>
								<td width="75%"><asp:button id="btnSearch" runat="server" Width="64px" Text="Cari"></asp:button></td>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<tr>
					<TD colSpan="7">
						<div id="div1" style="OVERFLOW: auto; HEIGHT: 310px">
							<asp:datagrid id="dtgPODestination" runat="server" Width="100%" CellPadding="3" BackColor="#CDCDCD"
								BorderWidth="0px" BorderColor="Gainsboro" 
								AllowSorting="True" AllowCustomPaging="True" AllowPaging="True" PageSize="25"
								AutoGenerateColumns="False" CellSpacing="1">
								<AlternatingItemStyle ForeColor="Black" BackColor="White"></AlternatingItemStyle>
								<ItemStyle ForeColor="Black" BackColor="#F1F6FB"></ItemStyle>
								<HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#4A3C8C"></HeaderStyle>
								<Columns>
									<asp:TemplateColumn>
										<HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<HeaderTemplate>
											&nbsp;Pilih
										</HeaderTemplate>
										<ItemTemplate>
											<input type="radio" id="x" name="y" />
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="ID" HeaderText="ID">
										<HeaderStyle Width="5%" CssClass="hiddencol"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left" CssClass="hiddencol"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Code" SortExpression="Code" HeaderText="Code">
										<HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
									</asp:BoundColumn>
                                    <asp:TemplateColumn SortExpression="VWI_Dealer.DealerCode" HeaderText="Kode Dealer Destinasi">
										<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblDealerDestination" runat="server" 
                                                Text='<%# DataBinder.Eval(Container, "DataItem.DealerDestinationCode.DealerCode")%>' >
											</asp:Label>
										</ItemTemplate> 
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Nama" HeaderText="Nama">
										<HeaderStyle Width="25%" CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="Label1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Nama")%>' >
											</asp:Label>
                                            <asp:HiddenField ID="hidPODestinationId" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.ID")%>' />
										</ItemTemplate> 
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="Alamat" SortExpression="Alamat" HeaderText="Alamat">
										<HeaderStyle Width="35%" CssClass="titleTableSales"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn SortExpression="City.CityName" HeaderText="City">
										<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="Label2" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.City.CityName")%>' >
											</asp:Label>
										</ItemTemplate> 
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="City.Province.ProvinceName" HeaderText="Provinsi">
										<HeaderStyle Width="20%" CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="Label3" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.City.Province.ProvinceName")%>' >
											</asp:Label>
										</ItemTemplate> 
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="LeadTime" HeaderText="LeadTime">
										<HeaderStyle Width="20%" CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="Label3" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.LeadTime")%>' >
											</asp:Label>
										</ItemTemplate> 
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle VerticalAlign="Middle" HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
							</asp:datagrid>
						</div>
					</TD>
				<tr>
				<TR>
					<TD align="center" colSpan="7">
						<INPUT id="btnChoose" style="WIDTH: 60px" onclick="GetSelectedPODestination()" type="button"
							value="Pilih" name="btnChoose" runat="server">&nbsp; 
                        <INPUT id="btnCancel" style="WIDTH: 60px" onclick="window.close()" type="button" value="Tutup"
							name="btnCancel">
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
