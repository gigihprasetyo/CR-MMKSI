<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmTrTrainee.aspx.vb" Inherits="FrmTrTrainee" smartNavigation="False"%>
<%@ Register TagPrefix="cc1" Namespace="Intimedia.WebCC" Assembly="Intimedia.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
  <HEAD>
		<title>FrmTrTrainee</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script>
		
		function ShowPPDealerSelection()
		{
			//alert('bisa');
			showPopUp('../PopUp/PopUpSelectingDealer.aspx','',600,600,DealerSelection);
		}
		function DealerSelection(selectedDealer)
		{
			var txtDealerCodeSelection = document.getElementById("txtDealerCode");
			txtDealerCodeSelection.value = selectedDealer;
		}
		
		function ShowJobPosSelection()
		{
			//alert('bisa');
			showPopUp('../PopUp/PopUpJobPosition.aspx','',600,600,JobPosSelection);
		}
		function JobPosSelection(selectedJobPos)
		{
			var txtJobPos = document.getElementById("txtJobPosition");
			selectedJobPos = selectedJobPos + ';';
			
			var arrValue = selectedJobPos.split(';');
			txtJobPos.value = arrValue[0];
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
		<FORM id="Form1" method="post" encType="multipart/form-data" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD class="titlePage">Training - Siswa</TD>
				</TR>
				<TR>
					<TD background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></TD>
				</TR>
				<TR>
					<TD height="10"><IMG height="1" src="../images/dot.gif" border="0"></TD>
				</TR>
				<TR>
					<TD vAlign="top">
						<TABLE id="Table2" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR>
								<TD class="titleField" style="WIDTH: 138px" width="138" height="24">Nama Siswa</TD>
								<TD width="1" height="24">:</TD>
								<TD style="WIDTH: 428px" noWrap width="428"><asp:textbox onkeypress="return HtmlCharUniv(event)" id="txtName" runat="server" Width="400px"
										MaxLength="50"></asp:textbox><asp:requiredfieldvalidator id="Requiredfieldvalidator6" runat="server" Display="Dynamic" ControlToValidate="txtName"
										ErrorMessage="*"></asp:requiredfieldvalidator></TD>
								<TD vAlign="top" align="right" width="200" height="246" rowSpan="8">
									<div id="divPhoto" style="OVERFLOW: auto; WIDTH: 210px; HEIGHT: 208px" align="right"><asp:image id="photoView" runat="server" Height="200px" ImageUrl="../WebResources/GetPhoto.aspx"></asp:image></div>
								</TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 138px">Kode Dealer</TD>
								<TD>:</TD>
								<TD style="WIDTH: 428px" width="428">
									<P><asp:textbox onkeypress="return HtmlCharUniv(event)" id="txtDealerCode" runat="server" Width="128px"
											MaxLength="10" ToolTip="Dealer Search 1"></asp:textbox><asp:label id="lblPopUpDealer" runat="server" width="16px">
											<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0"></asp:label><asp:requiredfieldvalidator id="Requiredfieldvalidator5" runat="server" Display="Dynamic" ControlToValidate="txtDealerCode"
											ErrorMessage="*"></asp:requiredfieldvalidator></P>
								</TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 138px">Status</TD>
								<TD>:</TD>
								<TD style="WIDTH: 428px" width="428"><asp:dropdownlist id="ddlStatus" runat="server" Width="130px"></asp:dropdownlist><asp:requiredfieldvalidator id="Requiredfieldvalidator4" runat="server" Display="Dynamic" ControlToValidate="ddlStatus"
										ErrorMessage="*"></asp:requiredfieldvalidator></TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 138px; HEIGHT: 16px">Ukuran Baju</TD>
								<TD style="HEIGHT: 16px">:</TD>
								<TD style="WIDTH: 428px; HEIGHT: 16px" width="428"><asp:dropdownlist id="ddlShirtSize" runat="server"></asp:dropdownlist><asp:requiredfieldvalidator id="Requiredfieldvalidator3" runat="server" Display="Dynamic" ControlToValidate="ddlShirtSize"
										ErrorMessage="*"></asp:requiredfieldvalidator></TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 138px; HEIGHT: 18px">Mulai Bekerja</TD>
								<TD style="HEIGHT: 18px">:</TD>
								<TD style="WIDTH: 428px; HEIGHT: 18px"><cc1:inticalendar id="ICStartWork" runat="server"></cc1:inticalendar></TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 138px">Posisi Pekerjaan</TD>
								<TD>:</TD>
								<TD style="WIDTH: 428px" width="428"><asp:textbox onkeypress="return HtmlCharUniv(event)" id="txtJobPosition" runat="server" Width="200px"
										MaxLength="50"></asp:textbox>
									<asp:label id="lblSearchJobPos" runat="server" width="16px">
										<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0"></asp:label><asp:requiredfieldvalidator id="Requiredfieldvalidator1" runat="server" Display="Dynamic" ControlToValidate="txtJobPosition"
										ErrorMessage="*"></asp:requiredfieldvalidator></TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 138px">Level Pendidikan</TD>
								<TD>:</TD>
								<TD width="428"><asp:textbox onkeypress="return HtmlCharUniv(event)" id="txtEducationLevel" runat="server" Width="200px"
										MaxLength="50"></asp:textbox><asp:requiredfieldvalidator id="Requiredfieldvalidator2" runat="server" Display="Dynamic" ControlToValidate="txtEducationLevel"
										ErrorMessage="*"></asp:requiredfieldvalidator></TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 138px; HEIGHT: 23px">Foto (Maks. 20KB)</TD>
								<TD style="HEIGHT: 23px">:</TD>
								<TD style="WIDTH: 428px; HEIGHT: 23px" width="428"><INPUT id="photoSrc" onkeydown="return false" style="WIDTH: 322px; HEIGHT: 20px" type="file"
										size="35" name="photoSrc" runat="server">
									<asp:checkbox id="cbDeletePhoto" onclick="changeDeletePhoto(this.checked);" runat="server" Text="Hapus Foto"></asp:checkbox></TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 138px; HEIGHT: 24px"></TD>
								<TD style="HEIGHT: 24px"></TD>
								<TD style="WIDTH: 428px; HEIGHT: 24px" width="428"><asp:button id="btnSimpan" runat="server" width="60px" Text="Simpan"></asp:button><asp:button id="btnBatal" runat="server" width="60px" Text="Batal" CausesValidation="False"></asp:button></TD>
								<td align="right"></td>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD>
						<TABLE width="100%" border="0">
							<TR>
								<TD class="titleField" width="12%">Kode Dealer</TD>
								<TD width="1%">:</TD>
								<TD width="137" style="WIDTH: 137px"><asp:textbox id="txtSearchDealerCode" Runat="server" Width="120px"></asp:textbox></TD>
								<TD class="titleField" width="12%">No Reg</TD>
								<TD width="1%">:</TD>
								<TD width="12%"><asp:textbox id="txtSearchNoReg" Runat="server" Width="110px"></asp:textbox></TD>
								<TD class="titleField" width="12%">Nama Siswa</TD>
								<TD width="1%">:</TD>
								<TD width="12%"><asp:textbox id="txtSearchTraineeName" Runat="server" Width="117px"></asp:textbox></TD>
								<TD class="titleField" width="12%">Status
								</TD>
								<TD width="1%">:</TD>
								<TD width="12%"><asp:dropdownlist id="ddlSearchStatus" Width="130px" Runat="server"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD class="titleField" width="12%">Kode&nbsp;Region </TD>
								<TD width="1%">:</TD>
								<TD width="137" style="WIDTH: 137px">
									<asp:dropdownlist id="ddlRegion" runat="server" Width="120px"></asp:dropdownlist></TD>
								<TD class="titleField" width="12%"></TD>
								<TD width="1%"></TD>
								<TD width="12%"></TD>
								<TD class="titleField" width="12%"></TD>
								<TD width="1%"></TD>
								<TD width="12%"></TD>
								<TD class="titleField" width="12%"></TD>
								<TD width="1%"></TD>
								<TD width="12%"><asp:button id="btnSearch" Width="61px" Text="Cari" CausesValidation="False" Runat="server"></asp:button></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD vAlign="top">
						<DIV id="div1" style="OVERFLOW: auto; HEIGHT: 180px"><asp:datagrid id="dtgTrainee" runat="server" Width="100%" Font-Names="MS Reference Sans Serif"
								CellSpacing="1" ForeColor="GhostWhite" PageSize="50" AllowSorting="True" AllowPaging="True" AllowCustomPaging="True" BorderColor="#CDCDCD"
								BorderStyle="None" BorderWidth="0px" BackColor="Gainsboro" CellPadding="3" GridLines="Horizontal" AutoGenerateColumns="False">
								<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
								<AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
								<ItemStyle BackColor="White"></ItemStyle>
								<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
								<Columns>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
										<ItemTemplate>
											<asp:Label Runat="server" ID="lblNo"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="ID" SortExpression="ID" HeaderText="No Reg">
										<HeaderStyle Width="6%" CssClass="titleTableService"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Name" SortExpression="Name" HeaderText="Nama Siswa">
										<HeaderStyle Width="16%" CssClass="titleTableService"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn SortExpression="Dealer.ID" HeaderText="Kode Dealer">
										<HeaderStyle Width="6px" CssClass="titleTableService"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblKodeDealer" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="StartWorkingDate" SortExpression="StartWorkingDate" HeaderText="Mulai Bekerja" 
 DataFormatString="{0:dd/MM/yyyy}">
										<HeaderStyle Width="7%" CssClass="titleTableService"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="JobPosition" SortExpression="JobPosition" HeaderText="Posisi Pekerjaan">
										<HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="EducationLevel" SortExpression="EducationLevel" HeaderText="Level Pendidikan">
										<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle Width="50px"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="ShirtSize" SortExpression="ShirtSize" HeaderText="Ukuran Baju">
										<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle Width="50px"></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn SortExpression="Status" HeaderText="Status">
										<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblStatus" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn>
										<HeaderStyle Width="14%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="btnLihat" runat="server" Text="Lihat" CausesValidation="False" CommandName="View">
												<img src="../images/detail.gif" border="0" alt="Lihat"></asp:LinkButton>
											<asp:LinkButton id="btnUbah" runat="server" Text="Ubah" CausesValidation="False" CommandName="Edit">
												<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
											<asp:LinkButton id="btnAktif" runat="server" Text="Aktif" CausesValidation="False" CommandName="Active">
												<img src="../images/aktif.gif" border="0" alt="Aktif"></asp:LinkButton>
											<asp:LinkButton id="btnHapus" runat="server" Text="Hapus" CausesValidation="False" CommandName="Delete">
												<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></DIV>
					</TD>
				</TR>
			</TABLE>
		</FORM>
		<script language="javascript">
			if (window.parent==window)
			{
				if (!navigator.appName=="Microsoft Internet Explorer")
				{
				  self.opener = null;
				  self.close();
				}
				else
				{
				   this.name = "origWin";
				   origWin= window.open(window.location, "origWin");
				   window.opener = top;
                   window.close();
				}
			}	
		</script>
	</body>
</HTML>
