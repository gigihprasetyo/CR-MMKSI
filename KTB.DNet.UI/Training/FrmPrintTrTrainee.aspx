<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmPrintTrTrainee.aspx.vb" Inherits="FrmPrintTrTrainee" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmDetailTrTrainee</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<LINK href="../WebResources/printstyle.css" type="text/css" rel="stylesheet" media="print">
		<script language="javascript">
			function popUpClassInformation(kode)
			{		
				var url = '../PopUp/PopUpClassInformation.aspx?kode='+kode;
				showPopUp(url,'',320,440,null);
			}
			
			function changeDeletePhoto(checked)
			{
				var varPhotoSrc = document.getElementById("photoSrc");
				if (checked)			
					varPhotoSrc.style.visibility = "hidden";
				else
					varPhotoSrc.style.visibility = "visible";
			}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" encType="multipart/form-data" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD class="titlePage">Training After Sales - Siswa</TD>
				</TR>
				<TR>
					<TD background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></TD>
				</TR>
				<TR>
					<TD height="10"><IMG height="1" src="../images/dot.gif" border="0"></TD>
				</TR>
				<TR>
					<TD vAlign="top">
						<TABLE id="Table2" cellSpacing="1" cellPadding="1" width="100%" border="0">
							<TR>
								<TD class="titleField" style="HEIGHT: 25px" width="143" height="25">Nama Siswa</TD>
								<TD style="HEIGHT: 25px" width="1" height="25">:</TD>
								<TD style="HEIGHT: 25px" noWrap><asp:label id="lblTraineeName" runat="server"></asp:label></TD>
								<TD style="HEIGHT: 196px" vAlign="top" align="right" height="196" rowSpan="8">
									<div id="divPhoto" style="OVERFLOW: auto; WIDTH: 200px; HEIGHT: 200px" align="right"><asp:image id="photoView" runat="server" ImageUrl="../WebResources/GetPhoto.aspx"></asp:image></div>
								</TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 6px">Dealer</TD>
								<TD style="HEIGHT: 6px">:</TD>
								<TD style="HEIGHT: 6px" width="420">
									<P><asp:label id="lblDealerName" runat="server"></asp:label></P>
								</TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 19px">Kota</TD>
								<TD style="HEIGHT: 19px">:</TD>
								<TD style="HEIGHT: 19px" width="420"><asp:label id="lblCity" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 6px">Mulai Bekerja</TD>
								<TD style="HEIGHT: 6px">:</TD>
								<TD style="HEIGHT: 6px"><asp:label id="lblStartDate" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 16px">Posisi Pekerjaan</TD>
								<TD style="HEIGHT: 16px">:</TD>
								<TD width="420" style="HEIGHT: 16px">
									<asp:Label id="lblJobPosition" runat="server"></asp:Label></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 10px">Level Pendidikan</TD>
								<TD style="HEIGHT: 10px">:</TD>
								<TD width="420" style="HEIGHT: 10px">
									<asp:Label id="lblEducationLevel" runat="server"></asp:Label></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 8px">
									Ukuran Baju</TD>
								<TD style="HEIGHT: 8px">:</TD>
								<TD style="HEIGHT: 8px" width="420">
									<asp:Label id="lblShirtSize" runat="server"></asp:Label>
								</TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 5px">
									Status</TD>
								<TD style="HEIGHT: 5px">:</TD>
								<TD style="HEIGHT: 5px" width="420"><asp:label id="lblStatus" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 2px"></TD>
								<TD style="HEIGHT: 2px"></TD>
								<TD style="HEIGHT: 2px" width="420"></TD>
								<td rowspan="2"></td>
							</TR>
							<TR>
								<TD class="titleField" align="right"><asp:button class="hideButtonOnPrint" id="btnKembali" runat="server" Text="Kembali" width="60px"
										CausesValidation="False"></asp:button></TD>
								<TD></TD>
								<TD width="420">
									<P><INPUT class="hideButtonOnPrint" type="button" value="Cetak" onclick="window.print()" id="btnCetak"
											name="btnCetak" runat="server"></P>
								</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD style="WIDTH: 826px" vAlign="top">
						<asp:datagrid id="dtgCourseClass" runat="server" Font-Names="MS Reference Sans Serif" ForeColor="GhostWhite"
							PageSize="25" BorderColor="#666666" BorderStyle="None" BorderWidth="1px" BackColor="#CDCDCD"
							CellPadding="3" AutoGenerateColumns="False" Width="100%">
							<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
							<AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
							<ItemStyle BackColor="White"></ItemStyle>
							<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
							<Columns>
								<asp:TemplateColumn HeaderText="No">
									<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="Black" Width="12%" CssClass="titleTableService"
										BackColor="#CDCDCD"></HeaderStyle>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="TrClass.TrCourse.CourseCode" HeaderText="Kode Kategori">
									<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="Black" Width="12%" CssClass="titleTableService"
										BackColor="#CDCDCD"></HeaderStyle>
									<ItemTemplate>
										<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TrClass.TrCourse.CourseCode") %>' ID="Label1">
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="TrClass.ClassCode" HeaderText="Kode Kelas">
									<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="Black" Width="13%" CssClass="titleTableService"
										BackColor="#CDCDCD"></HeaderStyle>
									<ItemTemplate>
										<asp:HyperLink id="hlClass" runat="server"></asp:HyperLink>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="TrClass.ClassName" HeaderText="Nama Kelas">
									<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="Black" Width="25%" CssClass="titleTableService"
										BackColor="#CDCDCD"></HeaderStyle>
									<ItemTemplate>
										<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TrClass.ClassName") %>' ID="Label2">
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="TrClass.StartDate" HeaderText="Mulai">
									<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="Black" Width="10%" CssClass="titleTableService"
										BackColor="#CDCDCD"></HeaderStyle>
									<ItemTemplate>
										<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TrClass.StartDate", "{0:dd/MM/yyyy}") %>' ID="Label3">
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="TrClass.FinishDate" HeaderText="Selesai">
									<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="Black" Width="10%" CssClass="titleTableService"
										BackColor="#CDCDCD"></HeaderStyle>
									<ItemTemplate>
										<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TrClass.FinishDate", "{0:dd/MM/yyyy}") %>' ID="Label4">
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="Rank" SortExpression="Rank" ReadOnly="True" HeaderText="Ranking">
									<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="Black" Width="10%" CssClass="titleTableService"
										BackColor="#CDCDCD"></HeaderStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn SortExpression="TrClass.Location" HeaderText="Lokasi">
									<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="Black" Width="20%" CssClass="titleTableService"
										BackColor="#CDCDCD"></HeaderStyle>
									<ItemTemplate>
										<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TrClass.Location") %>' ID="Label5">
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
						</asp:datagrid>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
