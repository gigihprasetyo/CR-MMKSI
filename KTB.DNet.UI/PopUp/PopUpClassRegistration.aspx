<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PopUpClassRegistration.aspx.vb" Inherits="PopUpClassRegistration" smartNavigation="False"%>
<%@ Register TagPrefix="cc1" Namespace="FilterCompositeControl" Assembly="FilterCompositeControl" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
  
		<title>Daftar Peserta Training</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<base target="_self">
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript">
		
		function GetSelectedClassRegistration()
		{
			var table;
			var bcheck =false;
			table = document.getElementById("dtgClassRegistration");
			for (i = 1; i < table.rows.length; i++)
			{
				var radioButton = table.rows[i].cells[0].getElementsByTagName("INPUT")[0];
				
				if (radioButton != null && radioButton.checked)
					{
					//var CourseID = table.rows[i].cells[1].innerText;
					//var ClassCode = table.rows[i].cells[2].innerText;
					//var RegistrationCode = table.rows[i].cells[3].innerText;
					//var TraineeName = table.rows[i].cells[4].innerText;
					//window.returnValue = CourseID+";"+RegistrationCode+";"+TraineeName;
					//break;
					
					if(navigator.appName == "Microsoft Internet Explorer")
						{
						var CourseID = table.rows[i].cells[1].innerText;
						var ClassCode = table.rows[i].cells[2].innerText;
						var RegistrationCode = table.rows[i].cells[3].innerText;
						var TraineeName = table.rows[i].cells[4].innerText;
						window.returnValue = CourseID+";"+RegistrationCode+";"+TraineeName;
						bcheck=true;
						break;
					}
					else if (navigator.appName == "Netscape") {
					    var CourseID = table.rows[i].cells[1].innerText;
					    var ClassCode = table.rows[i].cells[2].innerText;
					    var RegistrationCode = table.rows[i].cells[3].innerText;
					    var TraineeName = table.rows[i].cells[4].innerText;
					    opener.dialogWin.returnFunc(CourseID + ";" + RegistrationCode + ";" + TraineeName);
					    bcheck = true;
					    break;
					}
					else
						{
						var CourseID = table.rows[i].cells[1].getElementsByTagName("span")[0].innerHTML;
						var ClassCode = table.rows[i].cells[2].getElementsByTagName("span")[0].innerHTML;
						var RegistrationCode = table.rows[i].cells[3].innerHTML;
						var TraineeName = table.rows[i].cells[4].getElementsByTagName("span")[0].innerHTML;
						opener.dialogWin.returnFunc(CourseID+";"+RegistrationCode+";"+TraineeName);
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
				alert("Silahkan pilih siswa");	
			  }
			
		}
		</script>
</HEAD>
	<body bottomMargin="10" topMargin="10">
		<form id="Form2" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="10" cellPadding="0" width="100%" border="0">
				<tr>
					<td>
						<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="titlePage" style="HEIGHT: 18px" colSpan="7">TRAINING -&nbsp;Daftar 
									Peserta Training</td>
							</tr>
							<tr>
								<td background="../images/bg_hor_parts.gif" height="1"><IMG height="1" src="../images/bg_hor_parts.gif" border="0"></td>
							</tr>
							<tr>
								<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
							</tr>
							<TR>
								<td><cc1:compositefilter id="cfClassRegistration" runat="server" DataGridSouce="dtgClassRegistration"></cc1:compositefilter></td>
							</TR>
							<TR>
								<TD>
									<div id="div1" style="OVERFLOW: auto; HEIGHT: 370px"><asp:datagrid id="dtgClassRegistration" runat="server" AllowPaging="True" AllowCustomPaging="True"
											Width="100%" BorderWidth="0px" CellSpacing="1" CellPadding="3" BackColor="Gainsboro" BorderColor="#CDCDCD" AutoGenerateColumns="False" AllowSorting="True"
											PageSize="25" Font-Names="MS Reference Sans Serif" ForeColor="GhostWhite" BorderStyle="None" GridLines="Horizontal">
											<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
											<AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
											<ItemStyle BackColor="White"></ItemStyle>
											<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
											<Columns>
												<asp:TemplateColumn>
													<HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Kode Kategori">
													<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
													<ItemTemplate>
														<asp:Label id=lblClassID runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TrClass.TrCourse.CourseCode") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Kode Kelas">
													<HeaderStyle Width="13%" CssClass="titleTableService"></HeaderStyle>
													<ItemTemplate>
														<asp:Label id=lblClassCode runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TrClass.ClassCode") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="No.Reg">
													<HeaderStyle Width="10px" CssClass="titleTableService"></HeaderStyle>
													<ItemTemplate>
														<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TrTrainee.ID") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="TrTrainee.Name" HeaderText="Nama Siswa">
													<HeaderStyle Width="30%" CssClass="titleTableService"></HeaderStyle>
													<ItemTemplate>
														<asp:Label id=lblTraineeName runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TrTrainee.Name") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="Dealer.DealerName" HeaderText="Dealer">
													<HeaderStyle Width="32px" CssClass="titleTableService"></HeaderStyle>
													<ItemTemplate>
														<asp:Label id="lblDealer" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerName")+" / " +DataBinder.Eval(Container, "DataItem.TrTrainee.Dealer.SearchTerm1")%>' >
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
											</Columns>
											<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
										</asp:datagrid></div>
								</TD>
							</TR>
							<TR>
								<TD align="center"><INPUT id="btnChoose" style="WIDTH: 60px" onclick="GetSelectedClassRegistration()" type="button"
										value="Pilih" name="btnChoose">&nbsp;<INPUT id="btnCancel" style="WIDTH: 60px" onclick="window.close()" type="button" value="Tutup"
										name="btnCancel"></TD>
							</TR>
						</TABLE>
					</td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
