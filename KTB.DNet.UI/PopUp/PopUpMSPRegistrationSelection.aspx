<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PopUpMSPRegistrationSelection.aspx.vb" Inherits=".PopUpMSPRegistrationSelection" smartNavigation="False" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<html>
	<head>
        <title>MSP Registration Selection</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/BasicStyle.css" type="text/css" rel="stylesheet">
		<base target="_self">
		<script language="javascript">

		    function GetSelectedCChassis() {
		        var table;
		        var bcheck = false;
		        table = document.getElementById("dtgMSPRegistration");
		        var MSPReg = '';
		        for (i = 1; i < table.rows.length; i++) {
		            var CheckBox = table.rows[i].cells[0].getElementsByTagName("INPUT")[0];

		            if (CheckBox != null && CheckBox.checked) {

		                if (navigator.appName == "Microsoft Internet Explorer") {
		                    if (MSPReg == '') {
		                        MSPReg = replace(table.rows[i].cells[1].innerText, ' ', '');
		                    }
		                    else {
		                        MSPReg = MSPReg + ';' +  replace(table.rows[i].cells[1].innerText, ' ', '');
		                    }
		                    window.returnValue = MSPReg;
		                    bcheck = true;
		                }
		                else if (navigator.appName == "Netscape") {
		                    if (MSPReg == '') {
		                        MSPReg = replace(table.rows[i].cells[1].innerText, ' ', '');
		                    }
		                    else {
		                        MSPReg = MSPReg + ';' + replace(table.rows[i].cells[1].innerText, ' ', '');
		                    }
		                    bcheck = true;
		                }
		                else {
		                    if (MSPReg == '') {
		                        MSPReg = replace(table.rows[i].cells[1].getElementsByTagName("span")[0].innerHTML, ' ', '');
		                    }
		                    else {
		                        MSPReg = MSPReg + ';' + replace(table.rows[i].cells[1].getElementsByTagName("span")[0].innerHTML, ' ', '');
		                    }
		                    bcheck = true;
		                }
		            }
		        }

		        if (bcheck) {
		            window.close();
		            if (navigator.appName != "Microsoft Internet Explorer")
		            { opener.dialogWin.returnFunc(MSPReg); }
		        }
		        else {
		            alert("Silahkan Pilih No Rangka terlebih dahulu");
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
    </head>
    <body MS_POSITIONING="GridLayout">
        <form id="Form2" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="6" width="100%" border="0">
				<tr>
					<td>
						<table id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="titlePage" colSpan="7">MSP -&nbsp;Pencarian No Rangka</td>
							</tr>
							<tr>
								<td background="../images/bg_hor_sales.gif" colSpan="7" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
							</tr>
							<tr>
								<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
							</tr>
						</table>
						<table id="Table2" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR>
								<TD class="titleField" width="20%">No Rangka</TD>
								<TD width="1%">:</TD>
								<TD width="25%"><asp:textbox id="txtChassisNumber" onkeypress="return alphaNumericExcept(event,'?%*$[]+={}<>&amp;@~!');"
										onblur="omitSomeCharacter(this.id,'<>?*%$;');" runat="server" Width="152px"></asp:textbox></TD>
								<TD style="WIDTH: 17px" width="2%"></TD>
								<TD class="titleField" width="20%"><%--Tipe MSP--%></TD>
								<TD width="1%"></TD>
								<TD width="33%">
                                    <%--<asp:dropdownlist runat="server" ID="ddlMSPType" AutoPostBack="true"></asp:dropdownlist>--%>
								</TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 20px">Kode Dealer</TD>
								<TD style="HEIGHT: 20px">:</TD>
								<TD style="HEIGHT: 20px"><asp:textbox id="txtDealerCode" onkeypress="return alphaNumericExcept(event,'?%*$[]+={}<>&amp;@~!');"
										onblur="omitSomeCharacter(this.id,'<>?*%$;');" runat="server" Width="152px"></asp:textbox></TD>
								<TD style="WIDTH: 17px; HEIGHT: 20px"></TD>
								<TD class="titleField" style="HEIGHT: 20px"><asp:button id="btnSearch" runat="server" Text=" Cari "></asp:button></TD>
								<TD style="HEIGHT: 20px"></TD>
								<TD style="HEIGHT: 20px"></TD>
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
                                        <asp:datagrid id="dtgMSPRegistration" runat="server" Width="100%" CellSpacing="1" AllowSorting="True" PageSize="15" AllowPaging="false" GridLines="Vertical" CellPadding="3" BackColor="#FDF1F2" BorderWidth="0px" BorderStyle="None" BorderColor="#E7E7FF" AutoGenerateColumns="False" ShowFooter="false">
						                <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
						                <AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
						                <ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
						                <HeaderStyle ForeColor="white" BackColor="#CC3333" Font-Bold=True HorizontalAlign="Center"></HeaderStyle>
						                <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
											<Columns>
												<asp:TemplateColumn>
													<HeaderStyle Width="2%" ></HeaderStyle>
													<HeaderTemplate>
														<input id="chkAllItems" type="checkbox" onclick="CheckAll('chkItemChecked',
														document.forms[0].chkAllItems.checked)" />
													</HeaderTemplate>
													<ItemTemplate>
														<asp:CheckBox id="chkItemChecked" runat="server"></asp:CheckBox>
													</ItemTemplate>
												</asp:TemplateColumn>

                                                 <asp:TemplateColumn HeaderText="MSP Registration ID" Visible="false">
													<HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
													<ItemTemplate>
                                                        <asp:Label id="lblMSPRegID" runat="server" Visible="false"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>

                                                <asp:TemplateColumn HeaderText="No Rangka">
													<HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
													<ItemTemplate>
														<asp:Label id="lblChassisNumber" runat="server"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Credit Account">
								                    <HeaderStyle CssClass="titleTableService"></HeaderStyle>
								                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
								                    <ItemTemplate>
									                    <asp:Label id="lblDealerCode" runat="server"></asp:Label>
								                    </ItemTemplate>
							                    </asp:TemplateColumn>

                                        <asp:TemplateColumn HeaderText="No MSP">
								            <HeaderStyle CssClass="titleTableService"></HeaderStyle>
								            <ItemStyle HorizontalAlign="Center"></ItemStyle>
								            <ItemTemplate>
									            <asp:Label id="lblNoMSP" runat="server"></asp:Label>
								            </ItemTemplate>
                                        </asp:TemplateColumn>

                                        <asp:TemplateColumn HeaderText="Tipe Pengajuan">
								            <HeaderStyle CssClass="titleTableService"></HeaderStyle>
								            <ItemStyle HorizontalAlign="Center"></ItemStyle>
								            <ItemTemplate>
									            <asp:Label id="lblRequestType" runat="server"></asp:Label>
								            </ItemTemplate>
                                        </asp:TemplateColumn>

                                        <asp:TemplateColumn HeaderText="Kategori MSP">
								            <HeaderStyle CssClass="titleTableService"></HeaderStyle>
								            <ItemStyle HorizontalAlign="Center"></ItemStyle>
								            <ItemTemplate>
									            <asp:Label id="lblMSPCategory" runat="server"></asp:Label>
								            </ItemTemplate>
                                        </asp:TemplateColumn>

											</Columns>                                            
										</asp:datagrid></div>
								</TD>
							</TR>
							<TR>
								<TD align="center" colspan="7"><INPUT id="btnChoose" style="WIDTH: 60px" disabled onclick="GetSelectedCChassis()" type="button"
										value="Pilih" name="btnChoose" runat="server">&nbsp;<INPUT id="btnCancel" style="WIDTH: 60px" onclick="window.close()" type="button" value="Batal"
										name="btnCancel"></TD>
							</TR>
						</table>
					</td>
				</tr>
			</TABLE>
		</form>
    </body>
</html>
