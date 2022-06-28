<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmDetailTrTrainee.aspx.vb" Inherits="FrmDetailTrTrainee" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmDetailTrTrainee</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript">
		function ShowPPDealerBranchSelection() {
		    var dealerCode = document.getElementById("lblDealerCode").value;
		    showPopUp('../Service/../PopUp/PopUpDealerBranchSelectionSingle.aspx?m=d&deal=' + dealerCode, '', 500, 760, TemporaryOutlet);
		}

		function TemporaryOutlet(selectedDealer) {
		    if (selectedDealer.indexOf(";") > 0) {
		        var txtDealerSelection = document.getElementById("txtDealerBranchCode");
		        txtDealerSelection.value = selectedDealer.split(";")[0];
		    }
		    else {
		        var txtDealerSelection = document.getElementById("txtDealerBranchCode");
		        txtDealerSelection.value = selectedDealer;
		    }
		}

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
			
		function ShowJobPosSelection()
		{
			//alert('bisa');
			showPopUp('../PopUp/PopUpJobPosition.aspx?ServiceOnly=1','',600,600,JobPosSelection);
		}
		function JobPosSelection(selectedJobPos)
		{
			var txtPosisi = document.getElementById("txtJobPosition");
			selectedJobPos = selectedJobPos + ';';
			
			var arrValue = selectedJobPos.split(';');
			txtPosisi.value = arrValue[0];
		}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" encType="multipart/form-data" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD class="titlePage">TRAINING - Siswa</TD>
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
								<TD class="titleField" style="HEIGHT: 24px" width="24%" height="24">Nama Siswa</TD>
								<TD style="HEIGHT: 24px" width="1%" height="24">:</TD>
								<TD style="HEIGHT: 24px" noWrap width="420"><asp:label id="lblTraineeName" runat="server"></asp:label></TD>
								<TD style="HEIGHT: 219px" vAlign="top" align="right" width="200" height="219" rowSpan="8">
									<div id="divPhoto" style="OVERFLOW: auto; WIDTH: 200px; HEIGHT: 200px" align="right"><asp:image id="photoView" runat="server" ImageUrl="../WebResources/GetPhoto.aspx"></asp:image></div>
								</TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 23px">Dealer</TD>
								<TD style="HEIGHT: 23px">:</TD>
								<TD style="HEIGHT: 23px" width="420">
									<P><asp:label id="lblDealerName" runat="server"></asp:label>
                                        <asp:HiddenField ID="lblDealerCode" runat="server" />
                                    </P>
								</TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 23px">Kota</TD>
								<TD style="HEIGHT: 23px">:</TD>
								<TD style="HEIGHT: 23px" width="420"><asp:label id="lblCity" runat="server"></asp:label></TD>
							</TR>
                            <TR>
								<TD class="titleField" style="HEIGHT: 23px">Kode Cabang Dealer</TD>
								<TD style="HEIGHT: 23px">:</TD>
								<TD style="HEIGHT: 23px">
                                    <asp:TextBox ID="txtDealerBranchCode" Width="150px" runat="server"></asp:TextBox>
                                    <asp:Label ID="lblPopUpDealerBranch" runat="server" Width="10"> 
										<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0">
                                    </asp:Label>
								</TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 23px">Mulai Bekerja</TD>
								<TD style="HEIGHT: 23px">:</TD>
								<TD style="HEIGHT: 23px"><asp:label id="lblStartDate" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 138px; HEIGHT: 18px">Tanggal Lahir</TD>
								<TD style="HEIGHT: 18px">:</TD>
								<TD style="WIDTH: 428px; HEIGHT: 18px" valign="middle">
									<table cellpadding="0" cellspacing="0" border="0">
										<tr>
											<td><cc1:inticalendar id="icBirthDate" runat="server"></cc1:inticalendar></td>
											<td>&nbsp; Format dd/mm/yyyy</td>
										</tr>
									</table>
								</TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 138px">Jenis Kelamin</TD>
								<TD>:</TD>
								<TD style="WIDTH: 428px" width="428"><asp:dropdownlist id="ddlGender" tabIndex="7" runat="server"></asp:dropdownlist><asp:requiredfieldvalidator id="Requiredfieldvalidator7" runat="server" ControlToValidate="ddlGender" ErrorMessage="Jenis Kelamin Harus dipilih">*</asp:requiredfieldvalidator></TD>
							</TR>
                            <TR>
								<TD class="titleField">Email</TD>
								<TD>:</TD>
								<TD width="420">
                                    <asp:textbox onkeypress="return HtmlCharUniv(event)" id="txtEmail" runat="server" Width="200px"
										MaxLength="50"></asp:textbox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtEmail" ErrorMessage="Email harus diisi" Enabled="false">*</asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmail" ErrorMessage="Format email salah" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" Enabled="true"></asp:RegularExpressionValidator>
								</TD>
							</TR>
                            <TR>
								<TD class="titleField">No KTP</TD>
								<TD>:</TD>
								<TD width="420">
                                    <asp:textbox onkeypress="return HtmlCharUniv(event)" id="txtKTP" runat="server" Width="200px"
										MaxLength="50"></asp:textbox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtKTP" ErrorMessage="No. KTP harus diisi" Enabled="false">*</asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtKTP" ErrorMessage="Format No KTP salah" ValidationExpression="[0-9]{16}" Enabled="true"></asp:RegularExpressionValidator>
								</TD>
							</TR>
							<TR>
								<TD class="titleField">Posisi Pekerjaan</TD>
								<TD>:</TD>
								<TD width="420"><asp:textbox onkeypress="return HtmlCharUniv(event)" id="txtJobPosition" runat="server" Width="340px"
										MaxLength="50"></asp:textbox><asp:requiredfieldvalidator id="Requiredfieldvalidator1" runat="server" Display="Dynamic" ControlToValidate="txtJobPosition"
										ErrorMessage="Posisi Pekerjaan harus diisi">*</asp:requiredfieldvalidator>
									<asp:label id="lblSearchJobPos" runat="server" width="16px">
										<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField">Level Pendidikan</TD>
								<TD>:</TD>
								<TD width="420"><asp:textbox onkeypress="return HtmlCharUniv(event)" id="txtEducationLevel" runat="server" Width="340px"
										MaxLength="50"></asp:textbox><asp:requiredfieldvalidator id="Requiredfieldvalidator2" runat="server" Display="Dynamic" ControlToValidate="txtEducationLevel"
										ErrorMessage="Level Pendidikan harus diisi">*</asp:requiredfieldvalidator></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 24px">Foto (Maks. 20KB)</TD>
								<TD style="HEIGHT: 24px">:</TD>
								<TD style="HEIGHT: 24px" width="420"><INPUT id="photoSrc" onkeydown="return false" style="WIDTH: 340px; HEIGHT: 20px" type="file"
										size="47" name="photoSrc" runat="server">
									<asp:CheckBox id="cbDeletePhoto" runat="server" Text="Hapus Foto" onclick="changeDeletePhoto(this.checked);"></asp:CheckBox>
								</TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 16px">Ukuran Baju</TD>
								<TD style="HEIGHT: 16px">:</TD>
								<TD style="HEIGHT: 16px" width="420"><asp:dropdownlist id="ddlShirtSize" runat="server"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 24px">Status</TD>
								<TD style="HEIGHT: 24px">:</TD>
								<TD style="HEIGHT: 24px" width="420"><asp:dropdownlist id="ddlStatus" runat="server" Width="130px"></asp:dropdownlist><asp:label id="lblStatus" runat="server"></asp:label></TD>
								<td rowspan="2"><asp:ValidationSummary id="messageValidationSummary" runat="server" ShowMessageBox="True" ShowSummary="False"></asp:ValidationSummary></td>
							</TR>
							<TR>
								<TD class="titleField" align="right"></TD>
								<TD></TD>
								<TD width="420">
									<P><asp:button class="hideButtonOnPrint" id="btnSimpan" runat="server" Text="Simpan" width="60px"></asp:button><asp:button class="hideButtonOnPrint" Text="Proses Cetak" id="btnCetak" runat="server"></asp:button><asp:button class="hideButtonOnPrint" id="btnRegister" runat="server" Text="Daftar" CausesValidation="False"
											ToolTip="Mendaftar Kelas"></asp:button></P>
								</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD style="WIDTH: 826px" vAlign="top">
						<div id="div1" style="OVERFLOW: auto; HEIGHT: 150px"><asp:datagrid id="dtgCourseClass" runat="server" Width="100%" GridLines="Horizontal" CellPadding="3"
								BackColor="Gainsboro" BorderWidth="0px" BorderStyle="None" BorderColor="#CDCDCD" PageSize="25" ForeColor="GhostWhite" CellSpacing="1" Font-Names="MS Reference Sans Serif"
								AllowSorting="True" AllowPaging="True" AutoGenerateColumns="False">
								<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
								<AlternatingItemStyle ForeColor="White" BackColor="#F6F6F6"></AlternatingItemStyle>
								<ItemStyle BackColor="White"></ItemStyle>
								<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
								<Columns>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle Width="12%" CssClass="titleTableService"></HeaderStyle>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="TrClass.TrCourse.CourseCode" HeaderText="Kode Kategori">
										<HeaderStyle Width="12%" CssClass="titleTableService"></HeaderStyle>
										<ItemTemplate>
											<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TrClass.TrCourse.CourseCode") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="TrClass.ClassCode" HeaderText="Kode Kelas">
										<HeaderStyle Width="13%" CssClass="titleTableService"></HeaderStyle>
										<ItemTemplate>
											<asp:HyperLink id="hlClass" runat="server"></asp:HyperLink>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="TrClass.ClassName" HeaderText="Nama Kelas">
										<HeaderStyle Width="25%" CssClass="titleTableService"></HeaderStyle>
										<ItemTemplate>
											<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TrClass.ClassName") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="TrClass.StartDate" HeaderText="Mulai">
										<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
										<ItemTemplate>
											<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TrClass.StartDate", "{0:dd/MM/yyyy}") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="TrClass.FinishDate" HeaderText="Selesai">
										<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
										<ItemTemplate>
											<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TrClass.FinishDate", "{0:dd/MM/yyyy}") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Rank" HeaderText="Rangking">
										<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
										<ItemTemplate>
											<asp:HyperLink runat="server" ID="hlRank"></asp:HyperLink>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="TrClass.Location" HeaderText="Lokasi">
										<HeaderStyle Width="20%" CssClass="titleTableService"></HeaderStyle>
										<ItemTemplate>
											<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TrClass.Location") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></div>
						<br>
						<asp:button class="hideButtonOnPrint" id="btnKembali" runat="server" Text="Kembali" width="60px"
							CausesValidation="False"></asp:button>
					</TD>
				</TR>
			</TABLE>
		</form>
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
