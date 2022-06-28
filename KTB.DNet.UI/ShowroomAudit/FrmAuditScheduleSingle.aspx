<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmAuditScheduleSingle.aspx.vb" Inherits="FrmAuditScheduleSingle" smartNavigation="False" %>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmAuditSchedule</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/FormFunctions.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript">
			function updateAuditorInfo(cboAuditor, arrAuditorInfo, spanJadwanAuditStartDate, 
								spanJadwalAuditSeparator, spanJadwalAuditEndDate, spanAuditorName){
				
				var auditorID = cboAuditor.options[cboAuditor.selectedIndex].value;
				var objAuditorInfo = arrAuditorInfo[auditorID];				
				
				spanJadwanAuditStartDate.innerText = objAuditorInfo.JadwalAuditStartDate;
				spanJadwalAuditSeparator.innerText = " s/d ";
				spanJadwalAuditEndDate.innerText = objAuditorInfo.JadwalAuditEndDate;
				spanAuditorName.innerText = objAuditorInfo.AuditorName;				
			}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage" style="HEIGHT: 19px">SHOWROOM -&nbsp;Daftar Jadwal </td>
				</tr>
				<tr>
					<td style="HEIGHT: 2px" background="../images/bg_hor.gif" height="2"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="1"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<tr>
					<td style="HEIGHT: 93px">
						<table id="Table2" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR>
								<td class="titleField" width="20%">Periode</td>
								<TD width="1%">:</TD>
								<TD width="69%" colSpan="4"><asp:label id="lblPeriode" Runat="server"></asp:label></TD>
							</TR>
							<TR>
								<td class="titleField" width="20%">Kode Dealer</td>
								<TD width="1%">:</TD>
								<TD width="69%" colSpan="4"><asp:label id="lblDealerCode" Runat="server"></asp:label></TD>
							</TR>
							<TR>
								<td class="titleField" style="HEIGHT: 19px" width="20%">Jadwal Audit</td>
								<TD style="HEIGHT: 19px" width="1%">:</TD>
								<TD style="HEIGHT: 19px" width="69%" colSpan="4"><asp:label id="lblAuditScheduleStartDate" Runat="server"></asp:label><asp:label id="lblAuditScheduleSeparator" Runat="server">s/d</asp:label><asp:label id="lblAuditScheduleEndDate" Runat="server"></asp:label></TD>
							</TR>
							<TR>
								<td class="titleField" style="HEIGHT: 19px" width="20%">Auditor</td>
								<TD style="HEIGHT: 19px" width="1%">:</TD>
								<TD style="HEIGHT: 19px" width="29%"><asp:dropdownlist id="ddlAuditor" Runat="server"></asp:dropdownlist></TD>
								<td class="titleField" width="20%">Nama</td>
								<TD width="1%">:</TD>
								<TD width="29%"><asp:label id="lblAuditorName" Runat="server">Johan Hasan</asp:label></TD>
							</TR>
							<TR>
								<td class="titleField" style="HEIGHT: 18px" width="20%"></td>
								<TD style="HEIGHT: 18px" width="1%"></TD>
								<TD style="HEIGHT: 18px" width="69%" colSpan="4"></TD>
							</TR>
							<TR>
								<td class="titleField" width="20%"></td>
								<TD width="1%"></TD>
								<TD width="69%" colSpan="4"></TD>
							</TR>
							<TR>
								<td vAlign="top" width="100%" colSpan="6">
									<div id="div1" style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 250px"><asp:datagrid id="dtgPhotoList" runat="server" Width="100%" BorderStyle="None" CellPadding="3"
											BorderWidth="1px" BorderColor="#CDCDCD" AutoGenerateColumns="False" BackColor="#E0E0E0">
											<SelectedItemStyle ForeColor="#CCFF99" BackColor="#009999"></SelectedItemStyle>
											<AlternatingItemStyle BackColor="#F5F1EE"></AlternatingItemStyle>
											<ItemStyle BackColor="White"></ItemStyle>
											<HeaderStyle VerticalAlign="Top"></HeaderStyle>
											<FooterStyle ForeColor="#003399" BackColor="White"></FooterStyle>
											<Columns>
												<asp:TemplateColumn HeaderText="NO">
													<HeaderStyle Width="2%" CssClass="titleTableService"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="lblNo" runat="server" NAME="lblNo" text= '<%# container.itemindex+1 %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Photo">
													<HeaderStyle Width="12%" CssClass="titleTableService"></HeaderStyle>
													<ItemStyle Wrap="False" HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
													<ItemTemplate>
														<asp:Label id=lblDesc runat="server" text='<%#DataBinder.Eval(Container,"DataItem.Description")%>'>
														</asp:Label><BR>
														<asp:TextBox id="txtItemDesc" runat="server" Width="400px" Rows="3" TextMode="MultiLine" Enabled="False"></asp:TextBox>
													</ItemTemplate>
													<EditItemTemplate>
														<asp:Label id="Label1" runat="server" text='<%#DataBinder.Eval(Container,"DataItem.Description")%>'>
														</asp:Label><BR>
														<asp:TextBox id="txtEditItemDesc" runat="server" Width="400px" Rows="3" TextMode="MultiLine"></asp:TextBox>
													</EditItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn>
													<HeaderStyle Width="12%" CssClass="titleTableService"></HeaderStyle>
													<ItemStyle Wrap="False" HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
													<ItemTemplate>
														<asp:Image ID="imgItemImage" Runat="server"></asp:Image><br>
													</ItemTemplate>
													<EditItemTemplate>
														<asp:Image ID="imgEditItemImage" Runat="server"></asp:Image><br>
													</EditItemTemplate>
												</asp:TemplateColumn>
												<asp:EditCommandColumn ButtonType="LinkButton" UpdateText="&lt;img src=&quot;../images/simpan.gif&quot; border=&quot;0&quot; alt=&quot;Simpan&quot;&gt;"
													CancelText="&lt;img src=&quot;../images/batal.gif&quot; border=&quot;0&quot; &quot; alt=&quot;Batal&quot;&gt;"
													EditText="&lt;img src=&quot;../images/edit.gif&quot; border=&quot;0&quot; alt=&quot;Ubah&quot;&gt;">
													<HeaderStyle Width="4%" CssClass="titleTableService"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
												</asp:EditCommandColumn>
											</Columns>
										</asp:datagrid></div>
								</td>
							</TR>
							<tr>
								<td colSpan="6">&nbsp;</td>
							</tr>
							<tr>
								<td><asp:button id="btnSimpan" runat="server" Width="64px" Text="Simpan"></asp:button><asp:button id="btnBack" runat="server" Text="Kembali" Visible="True"></asp:button></td>
								<td></td>
								<td></td>
							</tr>
						</table>
					</td>
				</tr>
			</TABLE>
			<asp:PlaceHolder ID="phScript" Runat="server"></asp:PlaceHolder>
		</form>
	</body>
</HTML>
