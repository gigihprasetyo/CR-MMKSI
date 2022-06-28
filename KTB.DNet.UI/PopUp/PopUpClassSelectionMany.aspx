<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PopUpClassSelectionMany.aspx.vb" Inherits="PopUpClassSelectionMany"  smartNavigation="False"%>
<%@ Register TagPrefix="cc1" Namespace="FilterCompositeControl" Assembly="FilterCompositeControl" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
	
		<title>PopUpClassSelectionMany</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<base target="_self">
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript">
		
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
		
		function GetSelectedClassMany()
		{
			var table;
			table = document.getElementById("dtgClassCourse");
			var Class ='';
			for (i = 1; i < table.rows.length; i++)
			{
				var CheckBox = table.rows[i].cells[0].getElementsByTagName("INPUT")[0];
				if (CheckBox != null && CheckBox.checked)
				{
					if(navigator.appName == "Microsoft Internet Explorer")
					{
						if (Class == '')
						{
							Class = replace(table.rows[i].cells[1].innerText,' ','');
						}
						else
						{
							Class = Class + ';' + replace(table.rows[i].cells[1].innerText,' ','');
						}
					window.returnValue = Class;
					}
					else if (navigator.appName == "Netscape") {
					    if (Class == '') {
					        Class = replace(table.rows[i].cells[1].innerText, ' ', '');
					    }
					    else {
					        Class = Class + ';' + replace(table.rows[i].cells[1].innerText, ' ', '');
					    }
					    opener.dialogWin.returnFunc(Class);
					}
					else
					{
						if (Class == '')
						{
							Class = replace(table.rows[i].cells[1].getElementsByTagName("span")[0].innerHTML,' ','');
						}
						else
						{
							Class = Class + ';' + replace(table.rows[i].cells[1].getElementsByTagName("span")[0].innerHTML,' ','');
						}
					opener.dialogWin.returnFunc(Class);
					}
				}
			}			
			window.close();
		}
		
		function GetSelectedPart()
		{
			
			var table,count=0;
			table = document.getElementById("dtgClassCourse");
			for (i = 1; i < table.rows.length; i++)
			{
				var radioButton = table.rows[i].cells[0].getElementsByTagName("INPUT")[0];
				
				if (radioButton != null && radioButton.checked)
				{
					count+=1;
					if(navigator.appName == "Microsoft Internet Explorer")
					{
					var ClassCode = table.rows[i].cells[1].innerText;
					var ClassName = table.rows[i].cells[2].innerText;
					var Cap = table.rows[i].cells[5].innerText;
					window.returnValue = ClassCode+";"+ClassName+";"+Cap;
					break;
					}
					else
					{
					var ClassCode = table.rows[i].cells[1].getElementsByTagName("span")[0].innerHTML;
					
					var ClassName = table.rows[i].cells[2].getElementsByTagName("span")[0].innerHTML;
					
					var Cap = table.rows[i].cells[5].innerHTML;
					
					opener.dialogWin.returnFunc(ClassCode+";"+ClassName+";"+Cap);
					
					break;
					}
				}
			}
			
			if (count==0)
			{
				
				window.alert("Silahkan Pilih Kelas");
			}else{
			
			window.close();
			}
		}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="10" cellPadding="0" width="100%" border="0">
				<tr>
					<td>
						<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="titlePage" style="HEIGHT: 18px" colSpan="7">TRAINING -&nbsp;Pencarian 
									Kelas</td>
							</tr>
							<tr>
								<td background="../images/bg_hor_parts.gif" height="1"><IMG height="1" src="../images/bg_hor_parts.gif" border="0"></td>
							</tr>
							<tr>
								<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
							</tr>
							<TR>
								<td><cc1:compositefilter id="cfClassCourse" runat="server" DataGridSouce="dtgClassCourse"></cc1:compositefilter></td>
							</TR>
							<TR>
								<TD>
									<div id="div1" style="OVERFLOW: auto; HEIGHT: 370px"><asp:datagrid id="dtgClassCourse" runat="server" PageSize="25" AllowSorting="True" AutoGenerateColumns="False"
											BorderColor="#CDCDCD" BackColor="Gainsboro" CellPadding="3" CellSpacing="1" BorderWidth="0px" Width="100%" AllowCustomPaging="True" AllowPaging="True"
											Font-Names="MS Reference Sans Serif" ForeColor="GhostWhite" BorderStyle="None" GridLines="Horizontal">
											<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
											<AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
											<ItemStyle BackColor="White"></ItemStyle>
											<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
											<Columns>
												<asp:TemplateColumn>
													<HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<HeaderTemplate>
														<input id="chkAllItems" type="checkbox" onclick="CheckAll('chkItemChecked',
														document.forms[0].chkAllItems.checked)" />
													</HeaderTemplate>
													<ItemTemplate>
														<asp:CheckBox id="chkItemChecked" runat="server"></asp:CheckBox>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="ClassCode" HeaderText="Kode Kelas">
													<HeaderStyle Width="30%" CssClass="titleTableService"></HeaderStyle>
													<ItemTemplate>
														<asp:Label id="lblPartNumber" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ClassCode") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="ClassName" HeaderText="Nama Kelas">
													<HeaderStyle Width="40%" CssClass="titleTableService"></HeaderStyle>
													<ItemTemplate>
														<asp:Label id="lblPartName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ClassName") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn SortExpression="StartDate" DataField="StartDate" DataFormatString="{0:dd/MM/yyyy}"
													HeaderText="Tgl Mulai" HeaderStyle-CssClass="titleTableService"></asp:BoundColumn>
												<asp:BoundColumn SortExpression="FinishDate" DataField="FinishDate" DataFormatString="{0:dd/MM/yyyy}"
													HeaderText="Tgl Selesai" HeaderStyle-CssClass="titleTableService"></asp:BoundColumn>
												<asp:BoundColumn SortExpression="Capacity" DataField="Capacity" HeaderText="Kapasitas" HeaderStyle-CssClass="titleTableService"></asp:BoundColumn>
											</Columns>
											<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
										</asp:datagrid></div>
								</TD>
							</TR>
							<TR>
								<TD align="center"><INPUT id="btnChoose" style="WIDTH: 60px" onclick="GetSelectedClassMany()" type="button"
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
