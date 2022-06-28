<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PopUpVechileTypeGeneral.aspx.vb" Inherits="PopUpVechileTypeGeneral"  smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
	
		<title>Tipe General</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<base target="_self">
		<script language="javascript">

		 function getSelectedTipeGeneral() {
		    var table;
		    var bcheck = false;
		    table = document.getElementById("dtgTipeGeneral");
		    var strTipeGeneral = '';
		    for (i = 1; i < table.rows.length; i++) {
		        var CheckBox = table.rows[i].cells[1].getElementsByTagName("INPUT")[0];
		        if (CheckBox != null && CheckBox.checked) {
		            if (navigator.appName == "Microsoft Internet Explorer") {
		                if (strTipeGeneral == '') {
		                    strTipeGeneral = replace(table.rows[i].cells[2].innerText, ' ', '') + ';' + replace(table.rows[i].cells[3].innerText, ' ', '');
		                }
		                else {
		                    strTipeGeneral = strTipeGeneral + '|' + replace(table.rows[i].cells[2].innerText, ' ', '') + ';' + replace(table.rows[i].cells[3].innerText, ' ', '');
		                }
		                window.returnValue = strTipeGeneral;
		                bcheck = true;
		            }
		            else if (navigator.appName == "Netscape") {
		                if (strTipeGeneral == '') {
		                    strTipeGeneral = replace(table.rows[i].cells[2].innerText, ' ', '') + ';' + replace(table.rows[i].cells[3].innerText, ' ', '');
		                }
		                else {
		                    strTipeGeneral = strTipeGeneral + '|' + replace(table.rows[i].cells[2].innerText, ' ', '') + ';' + replace(table.rows[i].cells[3].innerText, ' ', '');
		                }
		                bcheck = true;
		            }
		            else {
		                if (strTipeGeneral == '') {
		                    strTipeGeneral = replace(table.rows[i].cells[2].getElementsByTagName("span")[0].innerHTML, ' ', '') + ';' + replace(table.rows[i].cells[3].getElementsByTagName("span")[0].innerHTML, ' ', '');
		                }
		                else {
		                    strTipeGeneral = strTipeGeneral + '|' + replace(table.rows[i].cells[2].getElementsByTagName("span")[0].innerHTML, ' ', '') + ';' + replace(table.rows[i].cells[3].getElementsByTagName("span")[0].innerHTML, ' ', '');
		                }
		                bcheck = true;
		            }
		        }
		    }
		    if (bcheck) {
		        window.close();
		        if (navigator.appName != "Microsoft Internet Explorer")
		        { opener.dialogWin.returnFunc(strTipeGeneral); }
		    }
		    else {
		        alert("Silahkan Pilih Tipe General terlebih dahulu");
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

		function replace(string, text, by) {
		    var strLength = string.length, txtLength = text.length;
		    if ((strLength == 0) || (txtLength == 0)) return string;

		    var i = string.indexOf(text);
		    if ((!i) && (text != string.substring(0, txtLength))) return string;
		    if (i == -1) return string;

		    var newstr = string.substring(0, i) + by;

		    if (i + txtLength < strLength)
		        newstr += replace(string.substring(i + txtLength, strLength), text, by);

		    return newstr;
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
			<TABLE id="Table1" cellSpacing="0" cellPadding="6" width="100%" border="0">
				<tr>
					<td>
						<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="titlePage" style="HEIGHT: 18px" colSpan="7">
									PROGRAM DISKON REGULER&nbsp;- Tipe General</td>
							</tr>
							<tr>
								<td style="HEIGHT: 1px" background="../images/bg_hor_sales.gif" colSpan="7" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
							</tr>
							<tr>
								<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
							</tr>
							<TR>
								<TD class="titleField" style="HEIGHT: 22px" width="20%">Nama Tipe General</TD>
								<TD style="HEIGHT: 22px" width="1%">:</TD>
								<TD style="HEIGHT: 22px" width="25%"><asp:textbox id="txtName" runat="server" Width="152px"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 22px" width="20%"></TD>
								<TD style="HEIGHT: 22px" width="1%">:</TD>
								<TD style="HEIGHT: 22px" width="25%"><asp:button id="btnSearch" runat="server" Text=" Cari " Width="80px"></asp:button></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 15px"></TD>
								<TD style="HEIGHT: 15px"></TD>
								<TD style="HEIGHT: 15px"></TD>
							</TR>
                            <tr>
                                <td colspan="3"><hr /></td>
                            </tr>
							<TR>
								<TD colSpan="3">
										<div id="div1" style="OVERFLOW: auto; HEIGHT: 350px">
											<asp:datagrid id="dtgTipeGeneral" runat="server" AutoGenerateColumns="False" Width="100%" CellSpacing="1"
												BorderColor="#CDCDCD" BorderStyle="None" BorderWidth="0px" BackColor="#CDCDCD" CellPadding="3" PageSize="5000">
												<SelectedItemStyle Font-Bold="True" ForeColor="#663399" BackColor="#FFCC66"></SelectedItemStyle>
												<AlternatingItemStyle BackColor="#F5F1EE"></AlternatingItemStyle>
												<ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
												<HeaderStyle Font-Bold="True" ForeColor="#FFFFCC" BackColor="#990000"></HeaderStyle>
												<FooterStyle ForeColor="#330099" BackColor="#FFFFCC"></FooterStyle>
											<Columns>
                                                <asp:TemplateColumn HeaderText="No">
                                                    <HeaderStyle Width="2%" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle Width="2%" HorizontalAlign="Center"></ItemStyle>
                                                </asp:TemplateColumn>

                                                <asp:TemplateColumn>
                                                    <HeaderStyle Width="2%" CssClass="titleTableSales"></HeaderStyle>
                                                    <HeaderTemplate>
                                                        <input id="chkAllItems" type="checkbox" onclick="CheckAll('chkItemChecked',
													    document.forms[0].chkAllItems.checked)" />
                                                    </HeaderTemplate>
                                                    <ItemStyle Width="2%" HorizontalAlign="Center"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkItemChecked" runat="server"></asp:CheckBox>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="ID">
											        <HeaderStyle Width="1%" CssClass="hiddencol"></HeaderStyle>
                                                    <ItemStyle Width="1%" CssClass="hiddencol"></ItemStyle>
											        <ItemTemplate>
												        <asp:Label id="lblID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID")%>'>
												        </asp:Label>
											        </ItemTemplate>
											    </asp:TemplateColumn>
                                                <asp:TemplateColumn SortExpression="Name" HeaderText="Nama Tipe General">
											        <HeaderStyle Width="15%" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
											        <ItemTemplate>
												        <asp:Label id="lblFleetCustomerName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Name")%>'>
												        </asp:Label>
											        </ItemTemplate>
											    </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Status" SortExpression="Status">
                                                    <HeaderStyle Width="15%" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblStatus" runat="server">
                                                        </asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
											</Columns>
											<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
										</asp:datagrid>
									</div>
								</TD>
							</TR>
							<TR>
								<TD align=center colspan=7><INPUT id="btnChoose" style="WIDTH: 60px" disabled onclick="getSelectedTipeGeneral()" type="button"
										value="Pilih" name="btnChoose" runat="server">&nbsp;<INPUT id="btnCancel" style="WIDTH: 60px" onclick="window.close()" type="button" value="Tutup"
										name="btnCancel">
                                    <asp:HiddenField ID="hdnSubCategoryVehicleID" runat="server" />
								</TD>
							</TR>
						</TABLE>
					</td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
