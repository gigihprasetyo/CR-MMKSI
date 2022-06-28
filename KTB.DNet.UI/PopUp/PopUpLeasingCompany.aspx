<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PopUpLeasingCompany.aspx.vb" Inherits="PopUpLeasingCompany" smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Leasing Company</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<base target="_self">
		<script language="javascript">
		    function GetSelectedValue()
		    {
			    var table;
			    table = document.getElementById("dgLeasingCompany");
			    var find = false;
			    var area;
			    for (i = 1; i < table.rows.length; i++) {
			        var radioButton = table.rows[i].cells[0].getElementsByTagName("INPUT")[0];
			        if (radioButton != null && radioButton.checked) {
			            if (navigator.appName == "Microsoft Internet Explorer") {
			                area = table.rows[i].cells[1].innerText + ';' + table.rows[i].cells[3].innerText;
			                window.returnValue = area;
			            } else if (navigator.appName == "Netscape") {
			                area = replace(table.rows[i].cells[1].innerText, ' ', '') + ';' + replace(table.rows[i].cells[3].innerText, ' ', '');
			                opener.dialogWin.returnFunc(area);
			            }
			            else {
			                area = replace(table.rows[i].cells[1].getElementsByTagName("span")[0].innerHTML, ' ', '') + ';' + replace(table.rows[i].cells[3].getElementsByTagName("span")[0].innerHTML, ' ', '');
			                opener.dialogWin.returnFunc(area);
			            }
			            find = true;
			            break;
			        }
			    }
			    if (find)
			        window.close();
			    else
			        alert("Silahkan pilih  Leasing Company");
		    }

		</script>

        <style>           
            .hiddencol
              {
                display: none;
              }
        </style>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="FrmSalesmanLevel" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">
						EVENT - Data Leasing</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
			</TABLE>

			<TABLE id="Table2" cellSpacing="1" cellPadding="2" width="100%" border="0">
				<TR valign="top">
					<TD class="titleField" width="100px" style="HEIGHT: 20px">Nama Leasing</TD>
					<TD width="1%" style="HEIGHT: 20px">:</TD>
					<TD width="25%" style="HEIGHT: 20px"><asp:textbox id="txtLeasingName" runat="server" Width="252px" ></asp:textbox></TD>
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
					<TD colSpan="7" vAlign="top">
						<div id="div1" style="OVERFLOW: auto; HEIGHT: 330px">
                            <asp:datagrid id="dgLeasingCompany" runat="server" Width="100%" AllowSorting="True" CellSpacing="1"
								AutoGenerateColumns="False" BorderColor="Gainsboro" BorderWidth="0px" AllowPaging="True" AllowCustomPaging="True" PageSize="10"
                                BackColor="#CDCDCD" CellPadding="3">
								<AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
								<ItemStyle BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Gray"></HeaderStyle>
								<Columns>
									<asp:TemplateColumn>
										<HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
                                       <HeaderTemplate>
										</HeaderTemplate>
										<ItemTemplate>
                                            <INPUT type="radio" name="radio">
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn  HeaderText="ID" >
										<HeaderStyle CssClass="titleTablePromo hiddencol"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" CssClass=" hiddencol" ></ItemStyle>
										<ItemTemplate >
											<asp:Label id="lblCode" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>

                                    <asp:TemplateColumn  HeaderText="Kode Leasing">
										<HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblLeasingCode" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.LeasingCode")%>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>

                                    <asp:TemplateColumn  HeaderText="Nama Leasing">
										<HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblLeasingName" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.LeasingName") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									
								</Columns>
								<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></div>
					</TD>
				</TR>
				<TR>
					<TD colspan="7" height="40" align="center"><INPUT id="btnChoose" style="WIDTH: 60px" disabled onclick="GetSelectedValue()" type="button"
							value="Pilih" name="btnChoose" runat="server">&nbsp;<INPUT id="btnCancel" style="WIDTH: 60px" onclick="window.close()" type="button" value="Tutup"
							name="btnCancel"></TD>
				</TR>
				<TR>
					<TD></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
