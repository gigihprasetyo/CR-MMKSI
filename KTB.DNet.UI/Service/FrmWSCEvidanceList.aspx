<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmWSCEvidanceList.aspx.vb" Inherits="FrmWSCEvidanceList" smartNavigation="False"%>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>WSC - Daftar Bukti WSC</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<LINK href="../WebResources/ImagePopup.css" type="text/css" rel="stylesheet">
		<!--<script language="javascript" src="../WebResources/ImagePopup.js"></script>-->
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript">
		function InitMousePosition() 
		{
			if (window.Event) {
			document.captureEvents(Event.MOUSEMOVE);
			}
			document.onmousemove = SetXYPosition;
		}
		function SetXYPosition(e) {
			var x = (window.Event) ? e.pageX : event.clientX;
			var y = (window.Event) ? e.pageY : event.clientY;
			var txtYPosition = document.getElementById("txtYPosition");
			//txtYPosition.value=y;
				//document.ee.sd.value = x+":"+y;
		}
		function ShowPPDealerSelection()
		{
			showPopUp('../General/../PopUp/PopUpDealerSelection.aspx','',500,760,DealerSelection);
		}
		function DealerSelection(selectedDealer)
		{
			var txtDealerSelection = document.getElementById("txtKodeDealer");
			txtDealerSelection.value = selectedDealer;			
		}
		function SetPath(obj)
		{
			document.getElementById("lblPath").innerText = obj.lowsrc;
		}
			<!--        Script by hscripts.com          -->
		

		function ShowEvidenceImage(obj)
		{ 
			var fraImageTest = document.getElementById("fraImageTest");
			fraImageTest.src="../WebResources/GetImageGlobal.aspx?file=" + obj.lowsrc + "&hg=200&wd=200&type=ImageFile";
			var divImageTest = document.getElementById("imgBox");
			if(navigator.appName != "Microsoft Internet Explorer")
			{
				divImageTest=obj.parentNode.parentNode.childNodes[1];
			}
			divImageTest.style.visibility="visible";					
			divImageTest.innerHTML='';    
			divImageTest.appendChild(fraImageTest);    
			divImageTest.style.left=(getElementLeft(obj)) +'px';
			divImageTest.style.top=(getElementTop(obj)) + 'px';
			document.getElementById("lblPath").innerText = obj.lowsrc;
		}
		
		function HideEvidenceImage(obj)
		{
			var divImageTest = document.getElementById("imgBox");
			
			if(navigator.appName != "Microsoft Internet Explorer")
			{
				divImageTest=obj.parentNode.parentNode.childNodes[1];
			}
			divImageTest.style.visibility="hidden";
		}
		
		function getElementLeft(elm) 
		{
			var x = 0;
			x = elm.offsetLeft;
			elm = elm.offsetParent;
			while(elm != null)
			{
				x = parseInt(x) + parseInt(elm.offsetLeft) - 44;
				elm = elm.offsetParent;
			}
			return x;
		}

		function getElementTop(elm) 
		{
			if(navigator.appName != "Microsoft Internet Explorer")
			{
				var txtYPosition = document.getElementById("txtYPosition");
				var y = txtYPosition.value;
			}
			else
			{
				var y = 0;
				y = elm.offsetTop;
				elm = elm.offsetParent;
				while(elm != null)
				{
					y = parseInt(y) + parseInt(elm.offsetTop) - 24;
					elm = elm.offsetParent;
				}
			}				
			
			return y;
		}
		</script>
	</HEAD>
	<body onfocus="return checkModal()" onclick="checkModal()" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">WSC - Daftar Bukti WSC
						<BR>
					</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<tr>
					<TD>
						<TABLE id="Table2" cellSpacing="1" cellPadding="1" width="100%" border="0">
							<TR>
								<TD class="titleField" width="15%"><asp:label id="lblKodeDealer" runat="server">Kode Dealer</asp:label></TD>
								<TD width="1%">:</TD>
								<TD width="34%"><asp:textbox id="txtKodeDealer" onkeypress="return alphaNumericExcept(event,'<>?*%$;')" onblur="omitSomeCharacter('txtKodeDealer','<>?*%$;')"
										runat="server" Width="140px"></asp:textbox><asp:label id="lblSearchDealer" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:label></TD>
								<TD class="titleField" width="15%"><asp:label id="lblNomorWSC" runat="server">Nomor WSC</asp:label></TD>
								<TD width="1%">:</TD>
								<TD width="34%"><asp:textbox onkeypress="return alphaNumericPlusUniv(event)" id="txtnomorwsc" onblur="alphaNumericPlusBlur(txtnomorwsc)"
										runat="server" Width="90px" MaxLength="6"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="titleField" width="15%"><asp:label id="lblStatus" runat="server">Status</asp:label></TD>
								<TD width="1%">:</TD>
								<TD width="34%"><asp:dropdownlist id="ddlstatus" runat="server"></asp:dropdownlist></TD>
								<TD class="titleField" width="15%"><asp:label id="lblKategori" runat="server">Kategori</asp:label></TD>
								<TD width="1%">:</TD>
								<TD width="34%"><asp:dropdownlist id="ddlkategori" runat="server" Width="90px"></asp:dropdownlist></TD>
							</TR>
							<TR valign="top">
								<TD class="titleField" width="15%"><asp:label id="lblTanggalKirim" runat="server">Tanggal Kirim</asp:label></TD>
								<TD width="1%">:</TD>
								<TD width="34%"><table border="0" cellpadding="0" cellspacing="0" width="100%">
										<tr>
											<td>
												<cc1:IntiCalendar id="ICStart" runat="server" TextBoxWidth="70"></cc1:IntiCalendar></td>
											<td>s/d</td>
											<td>
												<cc1:IntiCalendar id="ICEnd" runat="server" TextBoxWidth="70"></cc1:IntiCalendar></td>
										</tr>
									</table>
								</TD>
								<!--<td width="1%"><asp:dropdownlist id="ddlTglKirim" runat="server" Width="32px" Visible="False"></asp:dropdownlist></td>-->
								<TD class="titleField" width="15%"><asp:label id="Label1" runat="server">Tipe Bukti</asp:label></TD>
								<TD width="1%">:</TD>
								<TD width="34%"><asp:dropdownlist id="ddlEvidenceType" runat="server" Width="140"></asp:dropdownlist></TD>
							</TR>
							<tr align="center">
								<TD colspan="6"><asp:button id="btnCari" runat="server" Width="60px" Text="Cari"></asp:button><asp:button id="btnDownload" runat="server" Width="66px" Text="Download" Enabled="False"></asp:button></TD>
							</tr>
							<TR>
								<TD colSpan="6" height="10"><asp:textbox id="txtDownload" runat="server"></asp:textbox></TD>
							</TR>
							<tr>
								<td colSpan="6" align="right">
									<div id="divPath" runat="server">
										<asp:TextBox id="lblPath" Visible="true" Runat="server" Width="0px"></asp:TextBox>
									</div>
								</td>
							</tr>
						</TABLE>
					</TD>
				</tr>
				<TR>
					<TD vAlign="top">
						<div id="div1" style="HEIGHT: 350px; OVERFLOW: auto">
							<asp:datagrid id="dtgEvidence" runat="server" Width="100%" AllowSorting="True" AllowCustomPaging="True"
								AllowPaging="True" PageSize="50" BackColor="#CDCDCD" AutoGenerateColumns="False" BorderColor="#CDCDCD"
								CellSpacing="1" BorderWidth="0px" CellPadding="3">
								<AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
								<ItemStyle BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Blue"></HeaderStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="id" HeaderText="id"></asp:BoundColumn>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle Width="3%" CssClass="titleTableService"></HeaderStyle>
									</asp:TemplateColumn>
									<asp:BoundColumn SortExpression="Status" HeaderText="Status">
										<HeaderStyle Width="7%" CssClass="titleTableService"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn SortExpression="WSCHeader.Dealer.DealerCode" HeaderText="Dealer">
										<HeaderStyle Width="20%" CssClass="titleTableService"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" SortExpression="WSCHeader.Dealer.DealerName" HeaderText="Nama Dealer">
										<HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn SortExpression="WSCHeader.ClaimNumber" HeaderText="Nomor WSC">
										<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn SortExpression="WSCHeader.ChassisMaster.ChassisNumber" HeaderText="Nomor Rangka">
										<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn SortExpression="WSCHeader.ChassisMaster.Category.CategoryCode" HeaderText="Kategori">
										<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn SortExpression="CreatedTime" HeaderText="Tanggal Kirim">
										<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="CreatedBy" SortExpression="CreatedBy" HeaderText="Pengirim">
										<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Kwitansi WSC">
										<HeaderStyle Width="4%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<div id="imgbox">
												<iframe id="fraImageTest" width="200px" height="200px" frameborder="0"></iframe>
											</div>
											<asp:LinkButton id="lnkKwitansi" runat="server" CommandName="lnkKwitansi"></asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Surat WSC">
										<HeaderStyle Width="4%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<div id="imgbox">
												<iframe id="fraImageTest" width="200px" height="200px" frameborder="0"></iframe>
											</div>
											<asp:LinkButton id="lnkSurat" runat="server" CommandName="lnkSurat">
												<!--<img id='imgKwitansi' border="0" alt="" onmouseout="Out();" src="../images/download.gif" lowsrc="../images/icon_mail.gif" onmouseover="Large(this)" /><br>--></asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Teknikal WSC">
										<HeaderStyle Width="4%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<div id="imgbox">
												<iframe id="fraImageTest" width="200px" height="200px" frameborder="0"></iframe>
											</div>
											<asp:LinkButton id="lnkTeknikal" runat="server" CommandName="lnkTeknikal"></asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Repair/Wo">
										<HeaderStyle Width="4%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<div id="imgbox">
												<iframe id="fraImageTest" width="200px" height="200px" frameborder="0"></iframe>
											</div>
											<asp:LinkButton id="lnkRepairWo" runat="server" CommandName="lnkRepairWo"></asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>

                                    
                                    <asp:TemplateColumn HeaderText="Part Bekas">
										<HeaderStyle Width="4%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<div id="imgbox">
												<iframe id="fraImageTest" width="200px" height="200px" frameborder="0"></iframe>
											</div>
											<asp:LinkButton id="lnkPartBekas" runat="server" CommandName="lnkPartBekas"></asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Photo">
										<HeaderStyle Width="4%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<div id="imgbox">
												<iframe id="fraImageTest" width="200px" height="200px" frameborder="0"></iframe>
											</div>
											<asp:LinkButton id="lnkPhoto" runat="server" CommandName="lnkPhoto"></asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>

									<asp:BoundColumn Visible="false" DataField="PathFile">
										<HeaderStyle Width="0%" CssClass="titleTableService"></HeaderStyle>
									</asp:BoundColumn>
                                    <asp:TemplateColumn HeaderText="Delete">
										<HeaderStyle Width="3%" CssClass="titleTableService"></HeaderStyle>
										<ItemTemplate>
											<asp:LinkButton id="lbnHapus" runat="server" CommandName="Hapus"></asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Detil">
										<HeaderStyle Width="0%" CssClass="titleTableService"></HeaderStyle>
										<ItemTemplate>
											<asp:label id="lblEvidence" runat="server">
												<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:label>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></div>
					</TD>
				</TR>
			</TABLE>
		</form>
		<script language="javascript">
			document.getElementById("txtDownload").style.visibility="hidden";			
			if (document.getElementById("txtDownload").value != "")
			{	var downloadURL = document.getElementById("txtDownload").value
				document.getElementById("txtDownload").value = ""
				window.open('../Downloadlocal.aspx?file='+downloadURL, '_top')
				
			}
		</script>
		<script language="javascript">
			InitMousePosition();
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
