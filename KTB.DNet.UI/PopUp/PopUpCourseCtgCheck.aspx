<%@ Register TagPrefix="cc1" Namespace="FilterCompositeControl" Assembly="FilterCompositeControl" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PopUpCourseCtgCheck.aspx.vb" Inherits="PopUpCourseCtgCheck" smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Pencarian Kategori</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<base target="_self">
		<script language="javascript">
		
			function getSelectedCourse()
			{
				var table;
				var bcheck =false;
				table = document.getElementById("dtgCourseSelection");
				var Kategori ='';
				for (i = 1; i < table.rows.length; i++)
				{
					var CheckBox = table.rows[i].cells[0].getElementsByTagName("INPUT")[0];
					if (CheckBox != null && CheckBox.checked)
					{
						if(navigator.appName == "Microsoft Internet Explorer")
						{
							if (Kategori == '')
							{
								Kategori = replace(table.rows[i].cells[1].innerText,' ','');
							}
							else
							{
								Kategori = Kategori + ';' + replace(table.rows[i].cells[1].innerText,' ','');
							}
						window.returnValue = Kategori;
						bcheck=true;
						}
						else if (navigator.appName == "Netscape") {
						    if (Kategori == '') {
						        Kategori = replace(table.rows[i].cells[1].innerText, ' ', '');
						    }
						    else {
						        Kategori = Kategori + ';' + replace(table.rows[i].cells[1].innerText, ' ', '');
						    }
						    bcheck = true;
						}
						else
						{
							if (Kategori == '')
							{
								Kategori = replace(table.rows[i].cells[1].getElementsByTagName("span")[0].innerHTML,' ','');
							}
							else
							{
								Kategori = Kategori + ';' + replace(table.rows[i].cells[1].getElementsByTagName("span")[0].innerHTML,' ','');
							}
							bcheck=true;
						}
					}
				}
				if (bcheck)
				{
						window.close();
						if(navigator.appName != "Microsoft Internet Explorer")
						{	opener.dialogWin.returnFunc(Kategori);	}
				}
				else
				{
					alert("Silahkan Pilih Kategori terlebih dahulu");	
				}
			}

			function CheckAll(aspCheckBoxID, checkVal) 
			{
				re = new RegExp(':' + aspCheckBoxID + '$')  
				for(i = 0; i < document.forms[0].elements.length; i++) {
					elm = document.forms[0].elements[i]
					if (elm.type == 'checkbox') {
						if (re.test(elm.name)) {
							elm.checked = checkVal
						}
					}
				}
			}
			
			function replace(string,text,by) 
				{
					var strLength = string.length, txtLength = text.length;
					if ((strLength == 0) || (txtLength == 0)) return string;

					var i = string.indexOf(text);
					if ((!i) && (text != string.substring(0,txtLength))) return string;
					if (i == -1) return string;

					var newstr = string.substring(0,i) + by;

					if (i+txtLength < strLength)
					newstr += replace(string.substring(i+txtLength,strLength),text,by);

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
								<td class="titlePage" style="HEIGHT: 18px" colSpan="7">TRAINING -&nbsp;Pencarian 
									Kategori Kursus</td>
							</tr>
							<tr>
								<td style="HEIGHT: 1px" background="../images/bg_hor_sales.gif" colSpan="7" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
							</tr>
							<tr>
								<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
							</tr>
                             <TR id="rowCategory" runat="server">
								<TD class="titleField" style="HEIGHT: 21px" width="20%">Pilih Kategory</TD>
								<TD style="HEIGHT: 21px" width="1%">:</TD>
								<TD style="HEIGHT: 21px" width="25%"><asp:DropDownList id="ddlJobPositionCategory" runat="server" Width="152px"></asp:DropDownList></TD>
								<TD style="WIDTH: 17px; HEIGHT: 21px" width="2%"></TD>
							
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 21px" width="20%">Kode&nbsp;Kategori&nbsp;Kursus</TD>
								<TD style="HEIGHT: 21px" width="1%">:</TD>
								<TD style="HEIGHT: 21px" width="25%"><asp:textbox id="txtKodeKategori" runat="server" Width="152px"></asp:textbox></TD>
								<TD style="WIDTH: 17px; HEIGHT: 21px" width="2%"></TD>
								<TD class="titleField" style="HEIGHT: 21px" width="20%"><asp:button id="btnSearch" runat="server" Text=" Cari " Width="80px"></asp:button></TD>
								<TD style="HEIGHT: 21px" width="1%"></TD>
								<TD style="HEIGHT: 21px" width="33%"><asp:dropdownlist id="ddlStatus" runat="server" Width="80px" Visible="False">
										<asp:ListItem Value="1" Selected="True">Aktif</asp:ListItem>
										<asp:ListItem Value="0">Tidak Aktif</asp:ListItem>
									</asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 13px">Nama Kategori</TD>
								<TD style="HEIGHT: 13px">:</TD>
								<TD style="HEIGHT: 13px"><asp:textbox id="txtNamaKategori" runat="server" Width="152px"></asp:textbox></TD>
								<TD style="WIDTH: 17px; HEIGHT: 13px"></TD>
								<TD class="titleField" style="HEIGHT: 13px"></TD>
								<TD style="HEIGHT: 13px"></TD>
								<TD style="HEIGHT: 13px"><INPUT id="txtSelectedIndex" type="hidden" runat="server" NAME="txtSelectedIndex"></TD>
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
									<div id="div1" style="OVERFLOW: auto; HEIGHT: 350px"><asp:datagrid id="dtgCourseSelection" runat="server" Width="100%" AutoGenerateColumns="False"
											GridLines="Horizontal" CellPadding="3" BackColor="Gainsboro" BorderWidth="0px" BorderStyle="None" BorderColor="#CDCDCD" AllowPaging="True"
											AllowCustomPaging="True" AllowSorting="True" PageSize="25" ForeColor="GhostWhite" CellSpacing="1" Font-Names="MS Reference Sans Serif">
											<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
											<AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
											<ItemStyle BackColor="White"></ItemStyle>
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
												<asp:TemplateColumn HeaderText="Kode Kategori" SortExpression="Code">
													<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
													<ItemTemplate>
														<asp:Label id=lblKodeKategori runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Code")%>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="Description" HeaderText="Nama Kategori" SortExpression="Description">
													<HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
												</asp:BoundColumn>
                                                <asp:TemplateColumn HeaderText="Tipe Kategori" SortExpression="IsMandatory">
													<HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="lblMandatory" runat="server"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Status">
													<HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
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
								<TD align="center" colspan="7"><INPUT id="btnChoose" style="WIDTH: 60px" disabled onclick="getSelectedCourse()" type="button"
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
