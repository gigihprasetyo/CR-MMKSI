<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmSAPPeriod.aspx.vb" Inherits="FrmSAPPeriod" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmSAPPeriod</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		
		<script language="javascript" >
			function timeBlur(ParamTxtTime)
		{
			var curTime = document.getElementById(ParamTxtTime);
			if (!ValidateTimeFormat(curTime)) 
			{
				alert('Format waktu yang Anda masukkan salah');
				curTime.value='00:00';
				curTime.focus();
				curTime.select();
			}
		}
		</script>

	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">SAP&nbsp;-&nbsp;Setting Period SAP</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD align="left">
						<TABLE id="Table2" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR>
								<TD class="titleField" width="24%">No SAP</TD>
								<TD width="1%">:</TD>
								<TD width="75%"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" id="txtSAPNo" onblur="omitSomeCharacter('txtSAPNo','<>?*%$;')"
										runat="server" MaxLength="15" Width="144px"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="titleField" width="24%">Validity Period</TD>
								<TD width="1%">:</TD>
								<TD width="300">
									<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="240" border="0">
										<TR>
											<TD width="50%"><cc1:inticalendar id="icTglCreate" runat="server" TextBoxWidth="70"></cc1:inticalendar></TD>
											<TD width="2%">&nbsp;to</TD>
											<TD width="50%"><cc1:inticalendar id="icTglCreate2" runat="server" TextBoxWidth="70"></cc1:inticalendar></TD>
										</TR>
									</TABLE>
								</TD>
							</TR>
							<TR>
								<TD class="titleField" width="24%">Batas tanggal akhir</TD>
								<TD width="1%">:</TD>
								<TD width="300"><cc1:inticalendar id="icEndConfirmedDate" runat="server" TextBoxWidth="70"></cc1:inticalendar></TD>
							</TR>
							<TR>
								<TD class="titleField" width="24%">Batas waktu akhir</TD>
								<TD width="1%">:</TD>
								<TD width="300"><asp:textbox id="txtEndConfirmHour" runat="server" Width="56px" onkeypress="NumOnlyBlurWithOnGridTxt(this,':')"
										onblur="timeBlur(this.id);"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="titleField" width="24%"></TD>
								<TD width="1%"></TD>
								<td width="75%"><asp:button id="btnSave" runat="server" width="60px" Text="Simpan"></asp:button><asp:button id="btnNew" runat="server" width="60px" Text="Batal"></asp:button></td>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD vAlign="top">
						<div id="div1" style="OVERFLOW: auto; HEIGHT: 320px"><asp:datagrid id="dtgSAPPeriod" runat="server" Width="100%" GridLines="None" CellPadding="3" BackColor="#CDCDCD"
								AllowCustomPaging="True" AllowPaging="True" BorderWidth="0px" BorderStyle="None" BorderColor="#CDCDCD" AutoGenerateColumns="False" CellSpacing="1"
								AllowSorting="True" PageSize="25">
								<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
								<AlternatingItemStyle ForeColor="Black" BackColor="White"></AlternatingItemStyle>
								<ItemStyle ForeColor="Black" BackColor="#FDF1F2"></ItemStyle>
								<HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#CC3333"></HeaderStyle>
								<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
										<HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle Width="3%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label Runat="server" ID="lblNo"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="SAPNumber" SortExpression="SAPNumber" HeaderText="NO SAP">
										<HeaderStyle Width="20%" CssClass="titleTableGeneral"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="StartDate" SortExpression="StartDate" HeaderText="Mulai Berlaku" DataFormatString="{0:dd/MM/yyyy}">
										<HeaderStyle Width="25%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="EndDate" SortExpression="EndDate" HeaderText="Tgl Berakhir" DataFormatString="{0:dd/MM/yyyy}">
										<HeaderStyle Width="25%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn>
										<HeaderStyle Width="15%" CssClass="titleTableGeneral"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="lbtnEdit" runat="server" Text="Ubah" CommandName="Edit" CausesValidation="False">
												<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
											<asp:LinkButton id="lbtnDelete" runat="server" Text="Hapus" CommandName="Delete" CausesValidation="False"
												Visible="False">
												<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
											<asp:LinkButton id="lbtnSynch" runat="server" Text="Synchronize Salesman" CommandName="Synchronize"
												CausesValidation="False">
												<img src="../images/reload.gif" border="0" alt="Synchronize"></asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></div>
					</TD>
				</TR>
				<TR>
					<TD height="40"></TD>
				</TR>
				<TR>
					<TD></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
