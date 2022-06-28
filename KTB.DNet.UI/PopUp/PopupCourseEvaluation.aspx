<%@ Register TagPrefix="cc1" Namespace="FilterCompositeControl" Assembly="FilterCompositeControl" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PopupCourseEvaluation.aspx.vb" Inherits="PopupCourseEvaluation" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
  
		<title>Pencarian Jenis Evaluasi</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<base target="_self">
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript">
		
		function GetSelectedPart()
		{
			var table;
			var bcheck =false;
			table = document.getElementById("dtgCourseEval");
			for (i = 1; i < table.rows.length; i++)
			{
				var radioButton = table.rows[i].cells[0].getElementsByTagName("INPUT")[0];
				
				if (radioButton != null && radioButton.checked)
				{
					//var Code = table.rows[i].cells[1].innerText;
					//var Name = table.rows[i].cells[3].innerText;
					//if(navigator.appName == "Microsoft Internet Explorer")
					//{ window.returnValue = Code+";"+Name; }
					//else { opener.dialogWin.returnFunc(Code+";"+Name); }
					//break;
					
					if(navigator.appName == "Microsoft Internet Explorer")
					  {
						var Code = table.rows[i].cells[1].innerText;
						var Name = table.rows[i].cells[3].innerText;
						window.returnValue = Code+";"+Name;
						bcheck=true;
					  }
					else
					  {
						var Code = table.rows[i].cells[1].getElementsByTagName("span")[0].innerHTML;
						var Name = table.rows[i].cells[3].getElementsByTagName("span")[0].innerHTML;
						opener.dialogWin.returnFunc(Code+";"+Name);
						bcheck=true;
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
				alert("Silahkan pilih kategori");	
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
								<td class="titlePage" style="HEIGHT: 18px" colSpan="7">TRAINING -&nbsp;Pencarian 
									Jenis Evaluasi</td>
							</tr>
							<tr>
								<td background="../images/bg_hor_parts.gif" height="1"><IMG height="1" src="../images/bg_hor_parts.gif" border="0"></td>
							</tr>
							<tr>
								<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
							</tr>
							<TR>
								<td><cc1:compositefilter id="cfCourseEval" runat="server" DataGridSouce="dtgCourseEval"></cc1:compositefilter></td>
							</TR>
							<TR>
								<TD><div id="div1" style="OVERFLOW: auto; HEIGHT: 370px"><asp:datagrid id="dtgCourseEval" runat="server" AllowPaging="True" AllowCustomPaging="True" Width="100%"
											AutoGenerateColumns="False" BorderColor="#CDCDCD" BackColor="Gainsboro" CellPadding="3" CellSpacing="1" BorderWidth="0px" AllowSorting="True"
											Font-Names="MS Reference Sans Serif" ForeColor="GhostWhite" BorderStyle="None" GridLines="Horizontal">
											<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
											<AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
											<ItemStyle BackColor="White"></ItemStyle>
											<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
											<Columns>
												<asp:TemplateColumn>
													<HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="EvaluationCode" HeaderText="Kode">
													<HeaderStyle Width="30%" CssClass="titleTableService"></HeaderStyle>
													<ItemTemplate>
														<asp:Label id="lblEvaluationCode" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.EvaluationCode") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="Type" HeaderText="Jenis">
													<HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
													<ItemTemplate>
														<asp:Label id="lblJenis" runat="server" Text='<%# IIf(CType(DataBinder.Eval(Container, "DataItem.Type"),string) = 0, "Nilai Angka", IIf(CType(DataBinder.Eval(Container, "DataItem.Type"),string) = 1, "Nilai Sikap", "Nilai Prestasi")) %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="Name" HeaderText="Nama Evaluasi">
													<HeaderStyle Width="40%" CssClass="titleTableService"></HeaderStyle>
													<ItemTemplate>
														<asp:Label id=lblEvaluationName runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Name") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
											</Columns>
											<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
										</asp:datagrid></div>
								</TD>
							</TR>
							<TR>
								<TD align="center"><INPUT id="btnChoose" style="WIDTH: 60px" onclick="GetSelectedPart()" type="button" value="Pilih"
										name="btnChoose">&nbsp;<INPUT id="btnCancel" style="WIDTH: 60px" onclick="window.close()" type="button" value="Tutup"
										name="btnCancel"></TD>
							</TR>
						</TABLE>
					</td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
