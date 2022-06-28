<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmWSCStatusList.aspx.vb" Inherits="FrmWSCStatusList" smartNavigation="False"%>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>ListContract</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<LINK href="../WebResources/ImagePopup.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/ImagePopup.js"></script>
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript">
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
					x = parseInt(x) + parseInt(elm.offsetLeft) - 34;
					elm = elm.offsetParent;
				}
				return x;
			}

			function getElementTop(elm) 
			{
				var y = 0;
				y = elm.offsetTop;
				elm = elm.offsetParent;
				while(elm != null)
				{
					y = parseInt(y) + parseInt(elm.offsetTop) - 24;
					elm = elm.offsetParent;
				}
				return y;
			}
		</script>
	</HEAD>
	<body onfocus="return checkModal()" onclick="checkModal()" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">WSC - Daftar Status WSC</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD>
						<TABLE id="Table1" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR>
								<TD class="titleField" width="24%"><asp:label id="lblDealer" runat="server">Kode Dealer</asp:label></TD>
								<TD width="1%"><asp:label id="lblColon1" runat="server">:</asp:label></TD>
								<TD width="35%"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$')" id="txtKodeDealer" onblur="omitSomeCharacter('txtKodeDealer','<>?*%$')"
										runat="server"></asp:textbox><asp:label id="lblSearchDealer" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:label></TD>
								<TD class="titleField" width="15%"><asp:label id="lblClaimNo" runat="server">Nomor Klaim</asp:label></TD>
								<TD width="1%"><asp:label id="lblColon4" runat="server">:</asp:label></TD>
								<TD width="24%"><asp:textbox onkeypress="return alphaNumericPlusUniv(event)" id="txtClaimNo" onblur="alphaNumericPlusBlur(txtClaimNo)"
										runat="server" size="22" MaxLength="6"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="titleField"><asp:checkbox id="chkKirim" runat="server" Checked="True" Text="Periode Kirim"></asp:checkbox></TD>
								<TD><asp:label id="lblColon2" runat="server">:</asp:label></TD>
								<TD noWrap>
									<table cellSpacing="0" cellPadding="0" border="0">
										<tr>
											<td><cc1:inticalendar id="icStartKirim" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
											<td>&nbsp;s/d&nbsp;</td>
											<td><cc1:inticalendar id="icEndKirim" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
										</tr>
									</table>
								</TD>
								<TD class="titleField"><asp:label id="lblVehicleType" runat="server">Tipe Kendaraan</asp:label></TD>
								<TD><asp:label id="lblColon5" runat="server">:</asp:label></TD>
								<TD><asp:dropdownlist id="ddlVehicleType" runat="server" Width="140"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD class="titleField"><asp:checkbox id="chkProses" runat="server" Text="Periode Proses"></asp:checkbox></TD>
								<TD><asp:label id="lblColon3" runat="server">:</asp:label></TD>
								<TD noWrap>
									<table cellSpacing="0" cellPadding="0" border="0">
										<tr>
											<td><cc1:inticalendar id="icStartProses" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
											<td>&nbsp;s/d&nbsp;</td>
											<td><cc1:inticalendar id="icEndProses" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
										</tr>
									</table>
								</TD>
								<TD class="titleField"><asp:label id="lblStatus2" runat="server">Status</asp:label></TD>
								<TD><asp:label id="lblColon6" runat="server">:</asp:label></TD>
								<TD><asp:dropdownlist id="ddlStatus" runat="server" Width="140"></asp:dropdownlist></TD>
							</TR>
							<tr>
								<td></td>
								<td></td>
								<td></td>
								<TD class="titleField"><asp:label id="Label1" runat="server">Tipe Bukti WSC</asp:label></TD>
								<TD><asp:label id="Label3" runat="server">:</asp:label></TD>
								<TD><asp:dropdownlist id="ddlEvidenceType" runat="server" Width="140"></asp:dropdownlist></TD>
							</tr>
							<tr>
								<td></td>
								<td></td>
								<td style="WIDTH: 226px"><asp:button id="btnSearch" runat="server" Text=" Cari " Width="60px"></asp:button></td>
								<TD><span class="titleField">
										<asp:label id="lblCategory" runat="server" Visible="False"> Kategori</asp:label></span>
								</TD>
								<TD>
									<asp:label id="lblCategory2" runat="server" Visible="False">:</asp:label>
								</TD>
								<TD><asp:dropdownlist id="ddlCategory" runat="server" Width="140px" Visible="False"></asp:dropdownlist></TD>
							</tr>
							<tr>
								<td colSpan="6" align="right">
									<div id="divPath" runat="server">
										<asp:TextBox id="lblPath" Visible="true" Runat="server" Width="0px"></asp:TextBox>
									</div>
								</td>
							</tr>
							<TR>
								<TD vAlign="top" colSpan="6">
									<div id="div1" style="HEIGHT: 360px; OVERFLOW: auto"><asp:datagrid id="dgStatusList" runat="server" Width="100%" CellPadding="3" BorderWidth="0px"
											CellSpacing="1" BorderColor="Gainsboro" BackColor="#CDCDCD" AutoGenerateColumns="False" AllowCustomPaging="True" AllowPaging="True" AllowSorting="True"
											PageSize="50" ShowFooter="True">
											<AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
											<ItemStyle BackColor="White"></ItemStyle>
											<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Blue"></HeaderStyle>
											<Columns>
												<asp:BoundColumn Visible="False" DataField="ID" ReadOnly="True" HeaderText="ID">
													<HeaderStyle Width="1%" CssClass="titleTableService"></HeaderStyle>
												</asp:BoundColumn>
												<asp:TemplateColumn HeaderText="No">
													<HeaderStyle Width="1%" CssClass="titleTableService"></HeaderStyle>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="Status" HeaderText="Status">
													<HeaderStyle Width="3%" CssClass="titleTableService"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="lblStatus" runat="server"></asp:Label>
													</ItemTemplate>
													<FooterStyle Wrap="False" HorizontalAlign="Left" VerticalAlign="Top"></FooterStyle>
													<FooterTemplate>
														<B>Baru:</B>
														<asp:Label id="lblBaru" runat="server"></asp:Label><BR>
														<B>Proses:</B>
														<asp:Label id="lblProses" runat="server"></asp:Label><BR>
														<B>Disetujui:</B>
														<asp:Label id="lblApprove" runat="server"></asp:Label><BR>
														<B>Ditolak:</B>
														<asp:Label id="lblDisapprv" runat="server"></asp:Label>
													</FooterTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="Reason.Description" HeaderText="Alasan">
													<HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left"></ItemStyle>
													<ItemTemplate>
														<asp:Label runat="server" ToolTip='<%# DataBinder.Eval(Container, "DataItem.Reason.Description") %>' Text='<%# DataBinder.Eval(Container, "DataItem.Reason.ReasonCode") %>' ID="Label4">
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="ClaimStatus" SortExpression="ClaimStatus" ReadOnly="True" HeaderText="A/D">
													<HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="NotificationNumber" SortExpression="NotificationNumber" ReadOnly="True"
													HeaderText="Notifikasi">
													<HeaderStyle Width="8%" CssClass="titleTableService"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="ClaimType" SortExpression="ClaimType" ReadOnly="True" HeaderText="Jenis WSC">
													<HeaderStyle Width="8%" CssClass="titleTableService"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
												</asp:BoundColumn>
												<asp:TemplateColumn SortExpression="Dealer.DealerCode" HeaderText="Dealer">
													<HeaderStyle Width="8%" CssClass="titleTableService"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="lblDealerCode" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerCode") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="ClaimNumber" HeaderText="Nomor WSC">
													<HeaderStyle Width="8%" CssClass="titleTableService"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<asp:LinkButton id=lnkClaimNumber runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ClaimNumber") %>' CommandName="lnkClaimNumber">
														</asp:LinkButton>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="ChassisMaster.ChassisNumber" HeaderText="Nomor Rangka">
													<HeaderStyle Width="12%" CssClass="titleTableService"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left"></ItemStyle>
													<ItemTemplate>
														<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ChassisMaster.ChassisNumber") %>' ID="Label2">
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="ChassisMaster.Category.CategoryCode" HeaderText="Kategori">
													<HeaderStyle Width="12%" CssClass="titleTableService"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left"></ItemStyle>
													<ItemTemplate>
														<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ChassisMaster.Category.CategoryCode") %>' ID="Label5">
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="CreateDateText" SortExpression="CreatedTime" ReadOnly="True" HeaderText="Tgl Kirim">
													<HeaderStyle Width="8%" CssClass="titleTableService"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="DecideDateText" SortExpression="DecideDate" ReadOnly="True" HeaderText="Tgl Proses">
													<HeaderStyle Width="12%" CssClass="titleTableService"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
												</asp:BoundColumn>
												<asp:TemplateColumn SortExpression="PartAmount" HeaderText="Penggantian Parts">
													<HeaderStyle Width="8%" CssClass="titleTableService"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="lblPartAmnt" runat="server"></asp:Label>
													</ItemTemplate>
													<FooterStyle Wrap="False" HorizontalAlign="Left" VerticalAlign="Top"></FooterStyle>
													<FooterTemplate>
														<B>Part (APP):</B><BR>
														<asp:Label id="lblAPPPartAmnt" runat="server"></asp:Label><BR>
														<B>Part (DAPP):</B><BR>
														<asp:Label id="lblDAPPPartAmnt" runat="server"></asp:Label>
													</FooterTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="LaborAmount" HeaderText="Ongkos Kerja">
													<HeaderStyle Width="8%" CssClass="titleTableService"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="lblLaborAmnt" runat="server"></asp:Label>
													</ItemTemplate>
													<FooterStyle Wrap="False" HorizontalAlign="Left" VerticalAlign="Top"></FooterStyle>
													<FooterTemplate>
														<B>Ongkos (APP):</B><BR>
														<asp:Label id="lblAPPLaborAmnt" runat="server"></asp:Label><BR>
														<B>Ongkos (DAPP):</B><BR>
														<asp:Label id="lblDAPPLaborAmnt" runat="server"></asp:Label>
													</FooterTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Email">
													<HeaderStyle Width="4%" CssClass="titleTableService"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<asp:LinkButton id="lnkEmail" runat="server" CommandName="lnkEmail">
															<img src="../images/icon_mail.gif" border="0" style="cursor:hand" alt="Send email">
														</asp:LinkButton>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Kwitansi WSC">
													<HeaderStyle Width="4%" CssClass="titleTableService"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<div id="imgbox">
															<iframe id="fraImageTest" width="200px" height="200px" frameborder="0"></iframe>
														</div>
														<asp:LinkButton id="lnkKwitansi" runat="server" CommandName="lnkKwitansi">
															<!--<img id='imgKwitansi' style="width:20px; height:20px;" alt="" onmouseout="Out();" src='\\172.17.31.26\d$\KTB.DNET.Phase4\KTB.DNet.UI\DataFile\WSC\100001\114100\SS2006316103711859.JPG' onmouseover="Large(this)" />--></asp:LinkButton>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Surat WSC">
													<HeaderStyle Width="4%" CssClass="titleTableService"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<div id="imgbox">
															<iframe id="fraImageTest" width="200px" height="200px" frameborder="0"></iframe>
														</div>
														<asp:LinkButton id="lnkSurat" runat="server" CommandName="lnkSurat"></asp:LinkButton>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Teknikal WSC">
													<HeaderStyle Width="4%" CssClass="titleTableService"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<div id="imgbox">
															<iframe id="fraImageTest" width="200px" height="200px" frameborder="0"></iframe>
														</div>
														<asp:LinkButton id="lnkTeknikal" runat="server" CommandName="lnkTeknikal">
															<!--<img id='imgKwitansi' style="width:20px; height:20px;" alt="" onmouseout="Out();" src='\\172.17.31.26\d$\KTB.DNET.Phase4\KTB.DNet.UI\DataFile\WSC\100001\114100\SS2006316103711859.JPG' onmouseover="Large(this)" />--></asp:LinkButton>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Delete">
													<HeaderStyle Width="4%" CssClass="titleTableService"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<asp:LinkButton id="lbnHapus" runat="server" CommandName="Hapus"></asp:LinkButton>
													</ItemTemplate>
												</asp:TemplateColumn>
											</Columns>
											<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
										</asp:datagrid></div>
								</TD>
								<TD vAlign="top"></TD>
							</TR>
						</TABLE>
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
		<!--

-->
	</body>
</HTML>
