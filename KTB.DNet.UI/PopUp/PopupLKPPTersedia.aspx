<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PopupLKPPTersedia.aspx.vb" Inherits=".PopupLKPPTersedia" smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<html  >
<head runat="server">
   <HEAD>
		<title>PopUp SPK</title>
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

		    function GetSelectedLKPP() {
		        var table;
		        var bcheck = false;
		        table = document.getElementById("dtgLKPP");
		        var LKPPnumber = '';

		        for (i = 1; i < table.rows.length; i++) {
		            var radioBtn = table.rows[i].cells[0].getElementsByTagName("INPUT")[0];
		            if (radioBtn != null && radioBtn.checked) {
		                if (navigator.appName == "Microsoft Internet Explorer") {
		                    LKPPnumber = trim(table.rows[i].cells[1].innerText) + ";" + trim(table.rows[i].cells[2].innerText);
		                    window.returnValue = LKPPnumber;
		                    bcheck = true;
		                    break;
		                }
		                else {
		                    if (LKPPnumber == '') {
		                        LKPPnumber = replace(table.rows[i].cells[1].innerHTML, '', '') + ';' + replace(table.rows[i].cells[2].getElementsByTagName("span")[0].innerHTML, '', '');
		                    }
		                    window.close();
		                    opener.dialogWin.returnFunc(LKPPnumber);
		                    bcheck = true;
		                    break;
		                }
		            }
		        }

		        if (bcheck) {
		            window.close();
		        }
		        else {
		            alert("Silahkan pilih LKPP");
		        }
		    }

		    function ClosePopUp() {
		        window.close();
		    }
		</script>
	</HEAD>
</head>
<body MS_POSITIONING="GridLayout">
 
		<form id="Form1" method="post" runat="server">
			<TABLE border="0" width="100%" cellpadding="0" cellspacing="0">
				<tr>
					<td class="titlePage" style="HEIGHT: 21px">LKPP - Daftar LKPP dengan kuota masih tersedia</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<TR>
					<TD align="left">
						<TABLE id="Table2" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR>
								<TD class="titleField" width="91" style="WIDTH: 140px">Nomor Pengadaan</TD>
								<TD width="1%">:</TD>
								<TD width="75%"><asp:textbox id="txtLKPPNumber" onkeypress="return alphaNumericExcept(event,'<>?*%$;')" onblur="omitSomeCharacter('txtLKPPNumber','<>?*%$;')"
										runat="server" Width="230px"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="titleField" width="91" style="WIDTH: 140px">Nama Institusi</TD>
								<TD width="1%">:</TD>
								<TD width="75%"><asp:textbox id="txtIntitutionName" runat="server" Width="228px"></asp:textbox></TD>
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
							<asp:datagrid id="dtgLKPP" runat="server" Width="100%" CellPadding="3" BackColor="#CDCDCD"
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
									<asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False">
										<HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="ReferenceNumber" SortExpression="ReferenceNumber" HeaderText="Nomor Pengadaan">
										<HeaderStyle Width="30%" CssClass="titleTableSales"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn SortExpression="GovInstName" HeaderText="Nama Instansi">
										<HeaderStyle CssClass="titleTableSales"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="Label1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.GovInstName")%>' >
											</asp:Label>
										</ItemTemplate> 
									</asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Tanggal Dibuat" SortExpression="CreatedTime" >
										<HeaderStyle width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        <ItemTemplate>
										    <asp:Label runat="server" ID="lblCreatedTime"></asp:Label>
									    </ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Dibuat Oleh" SortExpression="CreatedBy">
										<HeaderStyle width="8%" CssClass="titleTableSales"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        <ItemTemplate>
										    <asp:Label runat="server" ID="lblCreatedBy"></asp:Label>
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
						<INPUT id="btnChoose" style="WIDTH: 60px" onclick="GetSelectedLKPP()" type="button"
							value="Pilih" name="btnChoose" runat="server">&nbsp; 
                        <INPUT id="btnCancel" style="WIDTH: 60px" onclick="window.close()" type="button" value="Tutup"
							name="btnCancel">
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</html>
