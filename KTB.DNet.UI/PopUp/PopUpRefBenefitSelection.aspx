<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PopUpRefBenefitSelection.aspx.vb" Inherits="PopUpRefBenefitSelection" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>SALES CAMPAIGN - Referensi Benefit</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
        <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
        <link href="./WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<base target="_self">
		<script language="javascript">
		
		function getQueryVariable(variable)
		{
			var query = window.location.search.substring(1);
			var vars = query.split("&");
			for (var i=0;i<vars.length;i++)
			{	
				var pair = vars[i].split("=");
				if (pair[0] == variable)
				{
					return pair[1];
				}
			}
			return "nothing";
		}
		
		function GetSelectedBenefitClaim()
		{
		    var table;
		    var bcheck = false;
		    table = document.getElementById("dtgBenefitClaimSelection");
		    var BenefitInfo = '';
		    for (i = 1; i < table.rows.length; i++) {
		        var radioBtn = table.rows[i].cells[0].getElementsByTagName("INPUT")[0];
		        if (radioBtn != null && radioBtn.checked) {
		            if (navigator.appName == "Microsoft Internet Explorer") {
		                BenefitInfo = table.rows[i].cells[1].innerText + ";" + table.rows[i].cells[2].innerText
		                window.returnValue = BenefitInfo;
		                bcheck = true;
		            }
		            else if (navigator.appName == "Netscape") {
		                BenefitInfo = table.rows[i].cells[1].innerText + ";" + table.rows[i].cells[2].innerText
		                opener.dialogWin.returnFunc(BenefitInfo);
		                bcheck = true;
		            }
		            else {
		                BenefitInfo = replace(table.rows[i].cells[1].getElementsByTagName("span")[0].innerHTML, ' ', '') + ";" + replace(table.rows[i].cells[2].getElementsByTagName("span")[0].innerHTML, ' ', '')
		                opener.dialogWin.returnFunc(BenefitInfo);
		                bcheck = true;
		            }
		            break;
		        }
		    }

			if (bcheck)
			{
    			window.close();
			}
			else
			{
	    		alert("Silahkan pilih No. Registrasi Benefit");	
			}
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
			<TABLE id="Table1" cellSpacing="0" cellPadding="4" width="100%" border="0">
				<tr>
					<td>
						<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="titlePage" colSpan="7">SALES CAMPAIGN - Referensi Benefit</td>
							</tr>
							<tr>
								<td background="../images/bg_hor_sales.gif" colSpan="7" height="1"><IMG alt="" height="1" src="../images/bg_hor.gif" border="0"></td>
							</tr>
							<tr>
								<td height="10"><IMG height="1" alt="" src="../images/dot.gif" border="0"/></td>
							</tr>
							<TR valign="top">
								<TD class="titleField" width="20%">Nomor Surat</TD>
								<TD width="1%">:</TD>
								<TD width="25%"><asp:textbox id="txtNomorSurat" runat="server" Width="172px"></asp:textbox></TD>
								<TD style="WIDTH: 17px" width="10%"></TD>
								<TD class="titleField" width="20%">Remarks</TD>
								<TD width="1%">:</TD>
								<TD style="WIDTH: 225px" width="225"><asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" Width="225px" /></TD>
							</TR>
							<TR valign="top">
								<TD class="titleField" width="20%">Benefit Reg No</TD>
								<TD width="1%">:</TD>
								<TD width="25%"><asp:textbox id="txtBenefitRegNo" runat="server" Width="172px"></asp:textbox></TD>
								<TD style="WIDTH: 17px; HEIGHT: 13px"></TD>
								<TD class="titleField" style="HEIGHT: 13px"></TD>
								<TD style="HEIGHT: 13px">&nbsp;</TD>
								<TD valign="bottom" style="WIDTH: 225px; HEIGHT: 13px"><asp:button id="btnSearch" runat="server" Text=" Cari "></asp:button></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 15px"></TD>
								<TD style="HEIGHT: 15px"></TD>
								<TD style="HEIGHT: 15px"></TD>
								<TD style="WIDTH: 17px; HEIGHT: 15px"></TD>
								<TD style="HEIGHT: 15px"></TD>
								<TD style="HEIGHT: 15px"></TD>
								<TD style="WIDTH: 225px; HEIGHT: 15px"></TD>
							</TR>
							<TR>
								<TD  colSpan="7">
									<div id="div1" style="OVERFLOW: auto; HEIGHT: 280px">
                                        <asp:datagrid id="dtgBenefitClaimSelection" runat="server" Width="100%" AutoGenerateColumns="False" 
											AllowSorting="True">
											<AlternatingItemStyle BackColor="#FDF1F2"></AlternatingItemStyle>
											<HeaderStyle ForeColor="#F7F7F7" BackColor="#4A3C8C" Font-Bold=True HorizontalAlign="Center"></HeaderStyle>
											<Columns>
												<asp:TemplateColumn>
													<HeaderStyle Width="2%" CssClass="titleTableSales"></HeaderStyle>
												</asp:TemplateColumn>
									            <asp:BoundColumn DataField="ID" HeaderText="ID">
										            <HeaderStyle Width="5%" CssClass="hiddencol"></HeaderStyle>
										            <ItemStyle HorizontalAlign="Left" CssClass="hiddencol"></ItemStyle>
									            </asp:BoundColumn>

												<asp:BoundColumn DataField="BenefitRegNo" SortExpression="BenefitRegNo" HeaderText="Benefit Reg No">
													<HeaderStyle Width="25%" CssClass="titleTableSales"></HeaderStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="NomorSurat" SortExpression="NomorSurat" HeaderText="Nomor Surat">
													<HeaderStyle Width="25%" CssClass="titleTableSales"></HeaderStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="Remarks" SortExpression="Remarks" HeaderText="Remarks">
													<HeaderStyle Width="25%" CssClass="titleTableSales"></HeaderStyle>
												</asp:BoundColumn>
												<asp:TemplateColumn HeaderText="Status" SortExpression="Status">
													<HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center" />
													<ItemTemplate>
														<asp:Label id="lblStatus" runat="server"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
											</Columns>
										</asp:datagrid>
									</div>
								</TD>
							</TR>
							<TR>
								<TD align="center" colspan="7">
								<INPUT id="btnChoose" style="WIDTH: 60px" disabled onclick="GetSelectedBenefitClaim()" type="button"
										value="Pilih" name="btnChoose" runat="server">&nbsp;<INPUT id="btnCancel" style="WIDTH: 60px" onclick="window.close()" type="button" value="Tutup"
										name="btnCancel"></TD>
							</TR>
						</TABLE>
					</td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
