<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<%@ Register TagPrefix="FCKeditorV2" Namespace="CKEditor.NET" Assembly="CKEditor.NET" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmTransactionalAlert.aspx.vb"  smartNavigation="False" Inherits="FrmTransactionalAlert"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmActivityPlanBabit</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript">
			function ShowPPUserGroup()
			{
				showPopUp('../General/../PopUp/PopUpUserGroup.aspx?x=Territory','',500,760,UserGroupSelection);
			}
			function UserGroupSelection(val)
			{
				var txtUserGroup = document.getElementById("txtUserGroup");
				txtUserGroup.value = val;			
			}
			
			function toggleStatusList(){
				if(!document.all.divStatus) return;
				document.all.divStatus.style.display = document.all.divStatus.style.display == "block" ? "none" : "block";
				document.all.divStatus.style.position = "absolute";
				document.all.divStatus.style.zIndex = 1001;
				
				document.all.divStatus.style.left  = txtStatusHolder.parentElement.offsetLeft;
				document.all.divStatus.style.top  = txtStatusHolder.offsetTop + txtStatusHolder.clientHeight
													+ txtStatusHolder.parentElement.offsetTop + txtStatusHolder.parentElement.offsetHeight;
								
				document.all.divStatusIFrame.style.position = document.all.divStatus.style.position;
				document.all.divStatusIFrame.style.zIndex = 1000;
				document.all.divStatusIFrame.style.display = document.all.divStatus.style.display;
				document.all.divStatusIFrame.style.left = document.all.divStatus.style.left;
				document.all.divStatusIFrame.style.top = document.all.divStatus.style.top;
				document.all.divStatusIFrame.style.width = document.all.divStatus.clientWidth + 2;
				//document.all.divStatusIFrame.style.clip = 'rect(0px ' + document.all.divStatusIFrame.offsetWidth + ' ' + document.all.divStatusIFrame.offsetHeight + ' 0px)';
				//document.all.divStatus.style.clip = document.all.divStatusIFrame.style.clip;
				
				if(document.all.divStatus.style.display == "none"){
					var str = "";
					var rows;
					if (navigator.appName == "Microsoft Internet Explorer") {
					    rows = document.all.divStatus.childNodes[0].tBodies[0].rows;
					}
					else{
					    rows = document.all.divStatus.childNodes[1].tBodies[0].rows;
					}
					
					for(var i = 0; i < rows.length; i++){
						if(rows[i].cells[0].childNodes[0].checked){
							if(str.length > 0){
								str += ";";
							}
							str += rows[i].cells[0].childNodes[1].innerText;
						}
					}					
					
					txtStatusHolder.value = str;					
				}				
			}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage" style="HEIGHT: 19px">ALERT MANAGEMENT&nbsp;- Setting Alert 
						Management</td>
				</tr>
				<tr>
					<td style="HEIGHT: 1px" background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD align="left">
						<TABLE id="Table2" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR>
								<TD class="titleField" width="24%">Kategori</TD>
								<TD width="1%">:</TD>
								<TD width="75%"><asp:dropdownlist id="ddlAlertCategory" runat="server" Width="184px" AutoPostBack="True"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 16px" width="24%">Modul</TD>
								<TD style="HEIGHT: 16px" width="1%">:</TD>
								<TD style="HEIGHT: 16px" width="75%"><asp:dropdownlist id="ddlAlertModul" runat="server" Width="184px" AutoPostBack="True"></asp:dropdownlist></TD>
							</TR>
							<asp:panel id="pnlStatus" Runat="server">
								<TR>
									<TD class="titleField" width="24%">Status</TD>
									<TD width="1%">:</TD>
									<TD width="75%">
										<asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%^():|\@#$;+=`~{}\'');" id="txtStatus"
											runat="server" Width="152px" Enabled="False"></asp:textbox>
										<asp:label id="lblDealers" runat="server">
											<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup" onclick="javascript:toggleStatusList()">
										</asp:label>
										<DIV id="divStatus" style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; DISPLAY: none; OVERFLOW: auto; BORDER-LEFT: black 1px solid; WIDTH: 176px; BORDER-BOTTOM: black 1px solid; HEIGHT: 100px; BACKGROUND-COLOR: #ffffff">
											<asp:checkboxlist id="cblStatusList" style="Z-INDEX: 101" runat="server" Width="150px"></asp:checkboxlist></DIV>
										<DIV id="divStatusIFrame" style="DISPLAY: none; HEIGHT: 100px; BACKGROUND-COLOR: #ffffff"><IFRAME width="100%" height="100"></IFRAME>
										</DIV>
									</TD>
								</TR>
							</asp:panel>
							<TR>
								<TD class="titleField" style="HEIGHT: 27px" width="24%">Nama Alert</TD>
								<TD style="HEIGHT: 27px" width="1%">:</TD>
								<TD style="HEIGHT: 27px" width="75%"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%^():|\@#$;+=`~{}\'');" id="txtNamaAlert"
										runat="server" Width="288px"></asp:textbox></TD>
							</TR>
							<TR vAlign="top">
								<TD class="titleField" width="24%">Deskripsi Alert</TD>
								<TD vAlign="top" width="1%">:</TD>
								<td width="75%"><FCKEDITORV2:CKEditorControl id="txtDeskripsiAlert" runat="server" BasePath="../UserControl/fckeditor/"></FCKEDITORV2:CKEditorControl></td>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 27px" width="24%"></TD>
								<TD style="HEIGHT: 27px" width="1%"></TD>
								<TD style="HEIGHT: 27px" width="75%"><asp:textbox id="txtJumlahKarakter" style="TEXT-ALIGN: right" runat="server" Width="64px" Enabled="False"></asp:textbox>&nbsp;Characters</TD>
							</TR>
							<asp:panel id="pnlJenisAlert" Runat="server">
								<TR>
									<TD class="titleField" style="HEIGHT: 27px" width="24%">Jenis Alert</TD>
									<TD style="HEIGHT: 27px" width="1%">:</TD>
									<TD style="HEIGHT: 27px" width="75%">
										<asp:DropDownList id="ddlJenisAlert" Runat="server" onchange="CheckAlertMediaStatus();"></asp:DropDownList></TD>
								</TR>
							</asp:panel>
							<asp:panel id="pnlFontEffect" Runat="server">
								<TR>
									<TD class="titleField" style="HEIGHT: 27px" width="24%">Efek Huruf (Alert via 
										Dashboard)</TD>
									<TD style="HEIGHT: 27px" width="1%">:</TD>
									<TD style="HEIGHT: 27px" width="75%">
										<asp:dropdownlist id="ddlFontEffect" runat="server"></asp:dropdownlist></TD>
								</TR>
							</asp:panel>
							<TR>
								<TD class="titleField" style="HEIGHT: 19px" width="24%">Valid dari</TD>
								<TD style="HEIGHT: 19px" width="1%">:</TD>
								<TD style="HEIGHT: 19px" width="75%">
									<table cellSpacing="0">
										<tr>
											<td><cc1:inticalendar id="icValidFrom" runat="server"></cc1:inticalendar></td>
											<td>s/d</td>
											<td><cc1:inticalendar id="icValidTo" runat="server"></cc1:inticalendar></td>
											<td><asp:checkbox id="chkIncludeHoliday" Runat="server" Text="Include Holiday"></asp:checkbox></td>
										</tr>
									</table>
								</TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 27px" width="24%">Mulai Pukul</TD>
								<TD style="HEIGHT: 27px" width="1%">:</TD>
								<TD style="HEIGHT: 27px" width="75%">
									<TABLE id="Table3" cellSpacing="0">
										<TR>
											<TD><asp:textbox onkeypress="return alphaNumericExcept(event,'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz<>?*%^()|\@#$;+=`~{}\'');"
													id="txtStartHour" runat="server" Width="40px" MaxLength="5">10:00</asp:textbox></TD>
											<TD>s/d</TD>
											<TD><asp:textbox onkeypress="return alphaNumericExcept(event,'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz<>?*%^()|\@#$;+=`~{}\'');"
													id="txtEndHour" runat="server" Width="40px" MaxLength="5">18:00</asp:textbox></TD>
										</TR>
									</TABLE>
								</TD>
							</TR>
							<TR>
								<TD style="HEIGHT: 128px" width="24%" colSpan="3"><table cellSpacing="0">
										<tr>
											<td class="titleField">Alert Media</td>
											<td></td>
											<td class="titleField" colSpan="2">Frekuensi Alert</td>
										</tr>
										<tr>
											<td><asp:checkbox id="chkViaDashboard" onclick="CheckAlertMediaStatus();" Runat="server" Text="Via D-NET - Dashboard"></asp:checkbox></td>
											<td>/</td>
											<td><asp:textbox onkeypress="return NumericOnlyWith(event,'');" id="txtFrekuensiViaDashboard" Runat="server"
													Width="60"></asp:textbox></td>
											<td><asp:dropdownlist id="ddlFrekuensiTypeViaDashboard" Runat="server"></asp:dropdownlist>Minimal&nbsp;21 
												menit</td>
										</tr>
										<tr>
											<td><asp:checkbox id="chkViaAlert" onclick="CheckAlertMediaStatus();" Runat="server" Text="Via D-NET - Alert Notification Box"></asp:checkbox></td>
											<td>/</td>
											<td><asp:textbox onkeypress="return NumericOnlyWith(event,'');" id="txtFrekuensiViaAlert" Runat="server"
													Width="60"></asp:textbox></td>
											<td><asp:dropdownlist id="ddlFrekuensiTypeViaAlert" Runat="server"></asp:dropdownlist>Minimal&nbsp;21 
												menit</td>
										</tr>
										<tr>
											<td><asp:checkbox id="chkViaSMS" onclick="CheckAlertMediaStatus();" Runat="server" Text="SMS"></asp:checkbox></td>
											<td>/</td>
											<td><asp:textbox onkeypress="return NumericOnlyWith(event,'');" id="txtFrekuensiViaSMS" Runat="server"
													Width="60"></asp:textbox></td>
											<td><asp:dropdownlist id="ddlFrekuensiTypeViaSMS" Runat="server"></asp:dropdownlist></td>
										</tr>
										<tr>
											<td><asp:checkbox id="chkViaEmail" onclick="CheckAlertMediaStatus();" Runat="server" Text="Email"></asp:checkbox></td>
											<td>/</td>
											<td><asp:textbox onkeypress="return NumericOnlyWith(event,'');" id="txtFrekuensiViaEmail" Runat="server"
													Width="60"></asp:textbox></td>
											<td><asp:dropdownlist id="ddlFrekuensiTypeViaEmail" Runat="server"></asp:dropdownlist></td>
										</tr>
									</table>
								</TD>
							</TR>
							<TR vAlign="top">
								<TD class="titleField" width="24%">Di-upload Oleh</TD>
								<TD width="1%">:</TD>
								<TD width="75%"><asp:label id="lblDiuploadOleh" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField" width="24%">User Group</TD>
								<TD width="1%">:</TD>
								<TD width="75%"><asp:textbox id="txtUserGroup" runat="server" Width="152px"></asp:textbox><asp:label id="lblUserGroup" onclick="ShowPPUserGroup()" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup">
									</asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField" width="24%" colSpan="3">Alerts&nbsp;&amp; Sounds</TD>
							</TR>
							<TR>
								<TD class="titleField" width="24%">Alert Sound</TD>
								<TD width="1%">:</TD>
								<TD width="75%"><INPUT id="fileUploadSound" style="WIDTH: 400px; HEIGHT: 20px" type="file" size="47" runat="server">&nbsp;
									<asp:button id="btnUpload" runat="server" Text="Upload"></asp:button></TD>
							</TR>
							<TR>
								<TD class="titleField" style="HEIGHT: 27px" width="24%" colSpan="3"><asp:radiobuttonlist id="rdlAlertSounds" runat="server"></asp:radiobuttonlist></TD>
							</TR>
						</TABLE>
						<asp:button id="btnSaveAs" runat="server" Text="Save As"></asp:button>
						<asp:button id="btnSave" runat="server" Text="Save"></asp:button>
						<asp:button id="btnCancel" runat="server" Text="Kembali"></asp:button>
					</TD>
				</TR>
			</TABLE>
			<input id="hdnJumlahKarakter" type="hidden" value="0" runat="server">
			<script language="javascript">			
				function getDeskripsiAlertCharsCount(elm){
							var count = 0;
							
							for(var i = 0; i < elm.innerText.length; i++){								
								if((elm.innerText.charCodeAt(i) != 13) && (elm.innerText.charCodeAt(i) != 10)){
									count++;
								}
							}			
							return count;								
				}
				function hookKeyPressToFCKEditor(){
					if(document.all.<%=txtDeskripsiAlert.ClientID %>.parentNode.childNodes[2].contentWindow.document.all.xEditingArea.childNodes[0]){
						if(timerID != 0){
							clearInterval(timerID);
						}
						var txtJumlahKarakter = document.all.<%=txtJumlahKarakter.ClientID %>;
						txtJumlahKarakter.value = getDeskripsiAlertCharsCount(document.all.<%=txtDeskripsiAlert.ClientID %>.parentNode.childNodes[2].contentWindow.document.all.xEditingArea.childNodes[0].contentWindow.document.body);
						document.all.<%=txtDeskripsiAlert.ClientID %>.parentNode.childNodes[2].contentWindow.document.all.xEditingArea.childNodes[0].contentWindow.document.body.onkeyup = function(){
							var count = getDeskripsiAlertCharsCount(this);
							/*
							for(var i = 0; i < this.innerText.length; i++){								
								if((this.innerText.charCodeAt(i) != 13) && (this.innerText.charCodeAt(i) != 10)){
									count++;
								}
							}
							*/
							document.all.<%=hdnJumlahKarakter.ClientID%>.value = document.all.<%=txtJumlahKarakter.ClientID%>.value = count;
						}
						
					}					
				}
				var timerID = 0;
				function runOnWindowLoad(){										
					document.all.<%=txtDeskripsiAlert.ClientID %>.parentNode.childNodes[2].onload = function(){
						timerID = setInterval(hookKeyPressToFCKEditor, 100);
					};
				}
				var timerOnLoadID = 0;
				function checkDocState(){
					if(document.all.<%=txtDeskripsiAlert.ClientID %>.parentNode.childNodes[2].contentWindow.document.readyState == "complete"){
						clearInterval(timerOnLoadID);						
						hookKeyPressToFCKEditor();
						
						var txtJumlahKarakter = document.all.<%=txtJumlahKarakter.ClientID %>;
						//txtJumlahKarakter.value = document.all.<%=hdnJumlahKarakter.ClientID%>.value;						
																		
						toggleStatusList();
						toggleStatusList();
						CheckAlertMediaStatus();
					}
					
				}
				var ddlAlertCategory = document.all.<%=ddlAlertCategory.ClientID %>;
				if(ddlAlertCategory){
					if(!ddlAlertCategory.disabled){
						ddlAlertCategory.focus();
					}
				}
				timerOnLoadID = setInterval(checkDocState, 150);

				function CheckAlertMediaStatus()
				{
					if(!ddlJenisAlert) return;

					var ddlJenisAlert = document.all.<%=ddlJenisAlert.ClientID %>
					var chkViaDashboard = document.all.<%=chkViaDashboard.ClientID %>;
					var txtFrekuensiViaDashboard = document.all.<%=txtFrekuensiViaDashboard.ClientID %>;
					var ddlFrekuensiTypeViaDashboard = document.all.<%=ddlFrekuensiTypeViaDashboard.ClientID %>;
					var chkViaAlert = document.all.<%=chkViaAlert.ClientID %>
					var txtFrekuensiViaAlert = document.all.<%=txtFrekuensiViaAlert.ClientID %>;
					var ddlFrekuensiTypeViaAlert = document.all.<%=ddlFrekuensiTypeViaAlert.ClientID %>;
					var chkViaSMS = document.all.<%=chkViaSMS.ClientID %>
					var txtFrekuensiViaSMS = document.all.<%=txtFrekuensiViaSMS.ClientID %>;
					var ddlFrekuensiTypeViaSMS = document.all.<%=ddlFrekuensiTypeViaSMS.ClientID %>;
					var chkViaEmail = document.all.<%=chkViaEmail.ClientID %>
					var txtFrekuensiViaEmail = document.all.<%=txtFrekuensiViaEmail.ClientID %>;
					var ddlFrekuensiTypeViaEmail = document.all.<%=ddlFrekuensiTypeViaEmail.ClientID %>;

					if (ddlJenisAlert.selectedIndex == 0) 
					{
						txtFrekuensiViaDashboard.disabled 	= true;
						txtFrekuensiViaDashboard.value = ""
						ddlFrekuensiTypeViaDashboard.disabled 	= true;
						ddlFrekuensiTypeViaDashboard.selectedIndex =0

						txtFrekuensiViaAlert.disabled 	= true;
						txtFrekuensiViaAlert.value = ""
						ddlFrekuensiTypeViaAlert.disabled 	= true;
						ddlFrekuensiTypeViaAlert.selectedIndex =0

						txtFrekuensiViaSMS.disabled 	= true;
						txtFrekuensiViaSMS.value = ""
						ddlFrekuensiTypeViaSMS.disabled 	= true;
						ddlFrekuensiTypeViaSMS.selectedIndex =0

						txtFrekuensiViaEmail.disabled 	= true;
						txtFrekuensiViaEmail.value = ""
						ddlFrekuensiTypeViaEmail.disabled 	= true;
						ddlFrekuensiTypeViaEmail.selectedIndex =0
					}
					else
					{
						if (chkViaDashboard.checked)
						{
						txtFrekuensiViaDashboard.disabled 	= false;
						ddlFrekuensiTypeViaDashboard.disabled 	= false;
						}
						else
						{
						txtFrekuensiViaDashboard.disabled 	= true;
						txtFrekuensiViaDashboard.value = ""
						ddlFrekuensiTypeViaDashboard.disabled 	= true;
						ddlFrekuensiTypeViaDashboard.selectedIndex =0
						}
						
						if (chkViaAlert.checked)
						{
						txtFrekuensiViaAlert.disabled 	= false;
						ddlFrekuensiTypeViaAlert.disabled 	= false;
						}
						else
						{
						txtFrekuensiViaAlert.disabled 	= true;
						txtFrekuensiViaAlert.value = ""
						ddlFrekuensiTypeViaAlert.disabled 	= true;
						ddlFrekuensiTypeViaAlert.selectedIndex =0
						}

						if (chkViaSMS.checked)
						{
						txtFrekuensiViaSMS.disabled 	= false;
						ddlFrekuensiTypeViaSMS.disabled 	= false;
						}
						else
						{
						txtFrekuensiViaSMS.disabled 	= true;
						txtFrekuensiViaSMS.value = ""
						ddlFrekuensiTypeViaSMS.disabled 	= true;
						ddlFrekuensiTypeViaSMS.selectedIndex =0
						}

						if (chkViaEmail.checked)
						{
						txtFrekuensiViaEmail.disabled 	= false;
						ddlFrekuensiTypeViaEmail.disabled 	= false;
						}
						else
						{
						txtFrekuensiViaEmail.disabled 	= true;
						txtFrekuensiViaEmail.value = ""
						ddlFrekuensiTypeViaEmail.disabled 	= true;
						ddlFrekuensiTypeViaEmail.selectedIndex =0
						}
					}
				
				}
				
				


			</script>
		</form>
	</body>
</HTML>
