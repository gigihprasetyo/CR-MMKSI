<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PopUpDealerCitySelectionArea.aspx.vb" Inherits=".PopUpDealerCitySelectionArea" smartNavigation="False"%>
<%@ Register TagPrefix="cc1" Namespace="FilterCompositeControl" Assembly="FilterCompositeControl" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>PopUpDealerCitySelectionArea</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/BasicStyle.css" type="text/css" rel="stylesheet">
		<base target="_self">
		<script language="javascript">
		    function GetSelectedDealerCity() {
		        var table;
		        var bcheck = false;
		        table = document.getElementById("dtgDealerCitySelection");
		        var DealerCity = '';
		        for (i = 1; i < table.rows.length; i++) {
		            var CheckBox = table.rows[i].cells[0].getElementsByTagName("INPUT")[0];
		            if (CheckBox != null && CheckBox.checked) {
		                if (navigator.appName == "Microsoft Internet Explorer") {
		                    if (DealerCity == '') {
		                        DealerCity = replace(table.rows[i].cells[1].innerText, ' ', '');
		                    }
		                    else {
		                        DealerCity = DealerCity + ';' + replace(table.rows[i].cells[1].innerText, ' ', '');
		                    }
		                    window.returnValue = DealerCity;
		                    bcheck = true;
		                }
		                else if (navigator.appName == "Netscape") {
		                    if (DealerCity == '') {
		                        DealerCity = replace(table.rows[i].cells[1].innerText, ' ', '');
		                    }
		                    else {
		                        DealerCity = DealerCity + ';' + replace(table.rows[i].cells[1].innerText, ' ', '');
		                    }
		                    bcheck = true;
		                }
		                else {
		                    if (DealerCity == '') {
		                        DealerCity = replace(table.rows[i].cells[1].getElementsByTagName("span")[0].innerHTML, ' ', '');
		                    }
		                    else {
		                        DealerCity = DealerCity + ';' + replace(table.rows[i].cells[1].getElementsByTagName("span")[0].innerHTML, ' ', '');
		                    }
		                    bcheck = true;
		                }
		            }
		        }
		        if (bcheck) {
		            window.close();
		            if (navigator.appName != "Microsoft Internet Explorer")
		            { opener.dialogWin.returnFunc(DealerCity); }
		        }
		        else {
		            alert("Silahkan Pilih Kota terlebih dahulu");
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
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="6" width="100%" border="0">
				<tr>
					<td>
						<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="titlePage" colSpan="7">Peserta Dealer -&nbsp;List Kota</td>
							</tr>
							<tr>
								<td background="../images/bg_hor_sales.gif" colSpan="7" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
							</tr>
							<tr>
								<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
							</tr>
						</TABLE>
						<TABLE id="Table2" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR valign="top">
								<TD class="titleField" width="10%" style="HEIGHT: 20px">Kota</TD>
								<TD width="1%" style="HEIGHT: 20px">:</TD>
								<TD width="25%" style="HEIGHT: 20px"><asp:textbox id="txtCityName" runat="server" Width="152px" ></asp:textbox></TD>
								<TD style="WIDTH: 17px; HEIGHT: 20px" width="2%"><asp:Button ID="btnSearch" Width="50px" runat="server" Text=" Cari "></asp:Button></TD>
								<TD style="HEIGHT: 15px"></TD>
								<TD style="HEIGHT: 15px"></TD>
								<TD style="HEIGHT: 15px"></TD>
							</TR>
                            <TR>
								<TD class="titleField" style="HEIGHT: 15px"></TD>
								<TD style="HEIGHT: 15px"></TD>
								<TD style="HEIGHT: 15px"></TD>
								<TD style="WIDTH: 17px; HEIGHT: 15px"></TD>
								<TD style="HEIGHT: 15px"></TD>
								<TD style="HEIGHT: 15px"></TD>
								<TD style="HEIGHT: 15px"></TD>
							</TR>
							<TR>
								<TD colSpan="7">
									<div id="div1" style="OVERFLOW: auto; HEIGHT: 260px">
                                        <asp:datagrid id="dtgDealerCitySelection" runat="server" Width="100%" AutoGenerateColumns="False" CellPadding="2" 
											AllowSorting="True">
											<AlternatingItemStyle BackColor="#FDF1F2"></AlternatingItemStyle>
											<HeaderStyle ForeColor="white" BackColor="#CC3333" Font-Bold=True HorizontalAlign="Center"></HeaderStyle>
											<Columns>
												<asp:TemplateColumn>
													<HeaderStyle Width="1%" ></HeaderStyle>
													<HeaderTemplate>
														<input id="chkAllItems" type="checkbox" onclick="CheckAll('chkItemChecked',
														document.forms[0].chkAllItems.checked)" />
													</HeaderTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
													<ItemTemplate>
														<asp:CheckBox id="chkItemChecked" runat="server"></asp:CheckBox>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="City.CityCode" HeaderText="Kode Kota">
													<HeaderStyle Width="20%" ></HeaderStyle>
													<ItemTemplate>
														<asp:Label id="lblCityCode" runat="server" Text=""></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="City.CityName" HeaderText="Nama Kota">
													<HeaderStyle Width="50%" ></HeaderStyle>
													<ItemTemplate>
														<asp:Label id="lblCityName" runat="server" Text=""></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
											</Columns>
										</asp:datagrid>
									</div>
								</TD>
							</TR>
							<TR>
								<TD align="center" colspan="7"><INPUT id="btnChoose" style="WIDTH: 60px" disabled onclick="GetSelectedDealerCity()" type="button" runat="server"
										value="Pilih" name="btnChoose" runat="server">&nbsp;<INPUT id="btnCancel" style="WIDTH: 60px" onclick="window.close()" type="button" value="Batal"
										name="btnCancel"></TD>
							</TR>
						</TABLE>
					</td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
