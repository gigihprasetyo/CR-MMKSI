<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PopUpPartsForecastMaster.aspx.vb" Inherits=".PopUpPartsForecastMaster" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>PopUpPositionCode</title>
		<style>
			.HiddenColumn { DISPLAY: none; FONT-WEIGHT: bold; FONT-SIZE: 11px; BACKGROUND: #666666; MARGIN: 0px; COLOR: #ffffff; FONT-FAMILY: Sans-Serif, Arial; TEXT-ALIGN: center }
		</style>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<base target="_self">
		<script language="javascript">
		
		function getSelectedCourse()
		{
			var table;
			var bcheck = false;
			var Course = '';
			table = document.getElementById("dtgParts");
			for (i = 1; i < table.rows.length; i++)
			{
				var RadioButton = table.rows[i].cells[0].getElementsByTagName("INPUT")[0];
				if (RadioButton != null && RadioButton.checked)
				{
					if(navigator.appName == "Microsoft Internet Explorer")
					  {
					    Course = table.rows[i].cells[1].innerText + ";" + table.rows[i].cells[2].innerText + ";" + table.rows[i].cells[3].innerText;
						window.returnValue = Course;
						bcheck=true;
						break;
					}
					else if (navigator.appName == "Netscape") {
					    Course = table.rows[i].cells[1].innerText + ";" + table.rows[i].cells[2].innerText + ";" + table.rows[i].cells[3].innerText;
					    window.opener.dialogWin.returnFunc(Course);
					    bcheck = true;
					    break;
					}
					else
					  {
					    Course = table.rows[i].cells[1].getElementsByTagName("span")[0].innerHTML + ";" + table.rows[i].cells[2].getElementsByTagName("span")[0].innerHTML + ";" + table.rows[i].cells[3].getElementsByTagName("span")[0].innerHTML;
					  	window.opener.dialogWin.returnFunc(Course);
					  	bcheck=true;
						break;
					  }	
				}
			}
			if (bcheck)
			  {
				window.close();
			  }
			else
			  {
				alert("Silahkan pilih Kode Parts");	
			  }	
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
								<td class="titlePage" style="HEIGHT: 18px" colSpan="7">
									SERVICE&nbsp;-&nbsp;Kode Posisi Parts</td>
							</tr>
							<tr>
								<td style="HEIGHT: 1px" background="../images/bg_hor_sales.gif" colSpan="7" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
							</tr>
							<tr>
								<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
							</tr>
							<TR>
								<TD class="titleField" style="HEIGHT: 22px" width="20%">
									Kode&nbsp;Part</TD>
								<TD style="HEIGHT: 22px" width="1%">:</TD>
								<TD style="HEIGHT: 22px" width="25%"><asp:textbox id="txtKodeParts" runat="server" Width="152px"></asp:textbox></TD>
								<TD style="WIDTH: 17px; HEIGHT: 22px" width="2%"></TD>								
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 13px">
									Nama Part</TD>
								<TD style="HEIGHT: 13px">:</TD>
								<TD style="HEIGHT: 13px"><asp:textbox id="txtNmPart" runat="server" Width="152px"></asp:textbox></TD>
								<TD style="WIDTH: 17px; HEIGHT: 13px"></TD>
								
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
                                <TD class="titleField" style="HEIGHT: 13px"></TD>
								<TD style="HEIGHT: 13px"></TD>
								<TD class="titleField" style="HEIGHT: 22px" width="20%"><asp:button id="btnSearch" runat="server" Text=" Cari " Width="80px"></asp:button></TD>
								<TD style="HEIGHT: 22px" width="1%"></TD>
								<TD style="HEIGHT: 22px" width="33%"></TD>
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
									<div id="div1" style="OVERFLOW: auto; HEIGHT: 350px">
										<asp:datagrid id="dtgParts" runat="server" Width="100%" AutoGenerateColumns="False" GridLines="Horizontal"
											CellPadding="3" BackColor="Gainsboro" BorderWidth="0px" BorderStyle="None" BorderColor="#CDCDCD"
											AllowPaging="True" AllowCustomPaging="True" AllowSorting="True" PageSize="25" ForeColor="GhostWhite"
											CellSpacing="1" Font-Names="MS Reference Sans Serif">
											<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
											<AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
											<ItemStyle BackColor="White"></ItemStyle>
											<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
											<Columns>
												<asp:TemplateColumn>
													<HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Kode Parts">
													<HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
													<ItemTemplate>
														<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SparePartMaster.PartNumber")%>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Nama Parts">
													<HeaderStyle Width="50%" CssClass="titleTableService"></HeaderStyle>
													<ItemTemplate>
														<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SparePartMaster.PartName")%>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="No Service Bulletin">
													<HeaderStyle Width="20%" CssClass="titleTableService"></HeaderStyle>
													<ItemTemplate>
														<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NoBulletinService") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Status">
													<HeaderStyle Width="10%" CssClass="HiddenColumn"></HeaderStyle>
													<ItemStyle CssClass="HiddenColumn" HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="lblStatus" runat="server"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
											</Columns>
											<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
										</asp:datagrid></div>
								</TD>
							</TR>
							<TR>
								<TD colspan="7" align="center"><INPUT id="btnChoose" style="WIDTH: 60px" disabled onclick="getSelectedCourse()" type="button"
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