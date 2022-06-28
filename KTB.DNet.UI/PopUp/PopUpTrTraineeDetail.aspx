<%@ Page Language="vb"  AutoEventWireup="false" Codebehind="PopUpTrTraineeDetail.aspx.vb" Inherits="PopUpTrTraineeDetail" smartNavigation="False"%>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>PopUpTrTraineeDetail</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<base target="_self">
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript">

		    function FuncUpdated(trtraineeid) {
		        var bcheck = false;
		        if (navigator.appName == "Microsoft Internet Explorer") {
		            window.returnValue = trtraineeid;
		            bcheck = true;
		            window.close();

		        }
		        else {
		            opener.dialogWin.returnFunc(trtraineeid);
		            bcheck = true;
		            window.close();
		        }
		        return false;
		    }
    </script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<FORM id="Form1" method="post" runat="server">
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
											MaxLength="10" ToolTip="Dealer Search"></asp:textbox><asp:label Visible="false" id="lblPopUpDealer" runat="server" width="16px">
											<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0"></asp:label><asp:requiredfieldvalidator id="Requiredfieldvalidator5" runat="server" Display="Dynamic" ControlToValidate="txtDealerCode"
											ErrorMessage="*"></asp:requiredfieldvalidator></P>
								</TD>
							</TR>
                            <tr>
                                <td class="titleField">Kode Cabang Dealer</td>
                                <td>:</td>
                                <td>
                                    <P><asp:textbox onkeypress="return HtmlCharUniv(event)" id="txtDealerBranchCode" runat="server" Width="128px"
											MaxLength="10" ToolTip="Dealer Branch Search"></asp:textbox><asp:label id="lblPopupDealerBranch" Visible="false" runat="server" width="16px">
											<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0"></asp:label></P>
                                </td>
                            </tr>
							<TR>
								<TD class="titleField" style="WIDTH: 138px; HEIGHT: 18px">Tanggal Lahir</TD>
								<TD style="HEIGHT: 18px">:</TD>
								<TD style="WIDTH: 600px; HEIGHT: 18px" valign="middle">
									<table  cellSpacing="0" cellPadding="0" border="0">
										<tr>
											<td><cc1:inticalendar id="icBirthDate" runat="server"></cc1:inticalendar></td>
											<td>&nbsp; Format dd/mm/yyyy </td>
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
                            <tr>
                                <td class="titleField">Email</td>
                                <td>:</td>
                                <td><asp:textbox onkeypress="return HtmlCharUniv(event)" id="txtEmail" runat="server" Width="200px"
										MaxLength="50"></asp:textbox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtEmail" ErrorMessage="*" Enabled="false"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmail" ErrorMessage="Format email salah" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>                                    
                                </td>
                            </tr>
                            <tr>
                                <td class="titleField">No KTP</td>
                                <td>:</td>
                                <td><asp:textbox onkeypress="return HtmlCharUniv(event)" id="txtKTP" runat="server" Width="200px"
										MaxLength="50"></asp:textbox>                                    
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtKTP" ErrorMessage="*" Enabled ="false"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtKTP" ErrorMessage="Format No KTP salah" ValidationExpression="[0-9]{16}"></asp:RegularExpressionValidator>
                                </td>
                            </tr>
							<TR>
								<TD class="titleField" style="WIDTH: 138px; HEIGHT: 18px">Mulai Bekerja</TD>
								<TD style="HEIGHT: 18px">:</TD>
								<TD style="WIDTH: 550px; HEIGHT: 18px" valign="middle">
									<table  cellSpacing="0" cellPadding="0" border="0">
										<tr>
											<td><cc1:inticalendar id="ICStartWork" runat="server"></cc1:inticalendar></td>
											<td>&nbsp; Format dd/mm/yyyy </td>
										</tr>
									</table>
								</TD>
							</TR>
							<TR>
								<TD class="titleField" style="WIDTH: 138px">Posisi Pekerjaan</TD>
								<TD>:</TD>
								<TD style="WIDTH: 428px" width="428">
                                    <asp:textbox onkeypress="return HtmlCharUniv(event)" id="txtJobPosition" runat="server" Width="200px"
										MaxLength="50" AutoPostBack="False"></asp:textbox>
									<asp:label id="lblSearchJobPos" Visible="false" runat="server" width="16px">
										<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0"></asp:label>
                                    <asp:requiredfieldvalidator id="Requiredfieldvalidator1" runat="server" ControlToValidate="txtJobPosition"
										ErrorMessage="*"></asp:requiredfieldvalidator>
								</TD>
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
								<TD style="WIDTH: 428px; HEIGHT: 24px" width="428"><asp:button id="btnSimpan" runat="server" width="60px" Text="Simpan"></asp:button>
                                    <INPUT id="btnCancel" style="WIDTH: 60px" onclick="window.close()" type="button" value="Tutup"
										name="btnCancel"/></TD>
								<td align="right"></td>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<tr>
					<td><hr>
					</td>
				</tr>
				
			</TABLE>
		</FORM>
		
	</body>
</HTML>
