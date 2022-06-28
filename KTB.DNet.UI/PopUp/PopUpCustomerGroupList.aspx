<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PopUpCustomerGroupList.aspx.vb" Inherits=".PopUpCustomerGroupList" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>PopUpCustomerList</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<base target="_self">
		<script language="javascript">

		    function GetSelectedCustomerGroup() {
		        var table;
		        var bcheck = false;
		        table = document.getElementById("dtgCustomerGroupSelection");
		        var CustomerGroup = '';
		        for (i = 1; i < table.rows.length; i++) {
		            var radioBtn = table.rows[i].cells[0].getElementsByTagName("INPUT")[0];
		            if (radioBtn != null && radioBtn.checked) {
		                if (navigator.appName == "Microsoft Internet Explorer") {

		                    CustomerGroup = replace(table.rows[i].cells[1].innerText, ' ', '');
		                    window.returnValue = CustomerGroup;
		                    bcheck = true;
		                }
		                else if (navigator.appName == "Netscape") {
		                    CustomerGroup = replace(table.rows[i].cells[1].innerText, ' ', '');
		                    opener.dialogWin.returnFunc(CustomerGroup);
		                    bcheck = true;
		                }
		                else {
		                    CustomerGroup = replace(table.rows[i].cells[1].getElementsByTagName("span")[0].innerHTML, ' ', '');
		                    opener.dialogWin.returnFunc(CustomerGroup);
		                    bcheck = true;
		                }
		                break;
		            }
		        }

		        if (bcheck) {
		            window.close();
		        }
		        else {
		            alert("Silahkan Pilih Grup Konsumen terlebih dahulu");
		        }
		    }


		</script>
	</HEAD>
<body MS_POSITIONING="GridLayout">
    <form id="form1" method="post" runat="server">
        <TABLE id="Table1" cellSpacing="0" cellPadding="6" width="100%" border="0">
			<tr>
                <td>
                    <TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
						<tr>
							<td class="titlePage" colSpan="7">Daftar Grup Konsumen</td>
						</tr>
                        <tr>
							<td background="../images/bg_hor_sales.gif" colSpan="7" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
						</tr>
						<tr>
							<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
						</tr>
                        <TR>
							<TD class="titleField" width="20%">Kode</TD>
							<TD width="1%">:</TD>
							<TD width="25%"><asp:textbox id="txtKode" onkeypress="return alphaNumericExcept(event,'?%*$[]+={}<>&amp;@~!');"
									onblur="omitSomeCharacter(this.id,'<>?*%$;');" runat="server" Width="152px"></asp:textbox></TD>
							<TD style="WIDTH: 17px" width="2%"></TD>
							<TD class="titleField" width="20%"></TD>
							<TD width="1%"></TD>
							<TD width="33%"></TD>
						</TR>
						<TR>
							<TD class="titleField" style="HEIGHT: 20px">Nama</TD>
							<TD style="HEIGHT: 20px">:</TD>
							<TD style="HEIGHT: 20px"><asp:textbox id="txtNama" onkeypress="return alphaNumericExcept(event,'?%*$[]+={}<>&amp;@~!');"
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
								<div id="div1" style="OVERFLOW: auto; HEIGHT: 310px">
                                    <asp:datagrid id="dtgCustomerGroupSelection" runat="server" Width="100%" AutoGenerateColumns="False"
										AllowCustomPaging="True" PageSize="25" AllowPaging="True" AllowSorting="True">
										<AlternatingItemStyle BackColor="#FDF1F2"></AlternatingItemStyle>
										<ItemStyle BackColor="White"></ItemStyle>
										<HeaderStyle ForeColor="White"></HeaderStyle>
										<Columns>
											<asp:TemplateColumn>
													<HeaderStyle Width="2%" CssClass="titleTableGeneral"></HeaderStyle>
												</asp:TemplateColumn>

                                            <asp:BoundColumn DataField="Code" SortExpression="Code" HeaderText="Kode Group">
												<HeaderStyle CssClass="titleTableGeneral"></HeaderStyle>
											</asp:BoundColumn>
											
											<asp:BoundColumn DataField="Name" SortExpression="Name" HeaderText="Nama">
												<HeaderStyle CssClass="titleTableGeneral"></HeaderStyle>
											</asp:BoundColumn>

										</Columns>
										<PagerStyle Mode="NumericPages" HorizontalAlign="Right"></PagerStyle>
									</asp:datagrid>
                                </div>
                            </TD>
                        </TR>
                        <TR>
							<TD colspan="7" align="center"><INPUT id="btnChoose" style="WIDTH: 60px" disabled onclick="GetSelectedCustomerGroup()" type="button"
									value="Pilih" name="btnChoose" runat="server">&nbsp;<INPUT id="btnCancel" style="WIDTH: 60px" onclick="window.close()" type="button" value="Tutup"
									name="btnCancel"></TD>
						</TR>
                    </TABLE>
                </td>
            </tr>
        </TABLE>
    </form>
</body>
</html>
