<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmAuditAssesmentResultList.aspx.vb" Inherits="FrmAuditAssesmentResultList"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmParameterAuditList</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript">

			function KTBNote()
			{
				return false;
			}

			function ShowAuditParameter()
			{
				var txtDealerCode= document.getElementById("txtDealerCode");
				var txtUser= document.getElementById("txtUser");
				if (txtUser.value == "KTB" || txtUser.value == "MKS")
				{
					if (txtDealerCode.value != '')
					{
						showPopUp('../PopUp/PopUpAuditParameter.aspx?DealerCode='+ txtDealerCode.value,'',460,760,AuditSelection);
					}
					else
					{
						showPopUp('../PopUp/PopUpAuditParameter.aspx','',460,760,AuditSelection);
					}
				}
				else
				{ // case from dealer
					showPopUp('../PopUp/PopUpAuditParameter.aspx','',460,760,AuditSelection);
				}
				
			}
			function AuditSelection(selectedCode)
			{
				var tempParam = selectedCode.split(';');
				var txtEventID = document.getElementById("txtAuditCode");				
									
				if(navigator.appName == "Microsoft Internet Explorer")
				{
				txtEventID.innerText = tempParam[0];				
				
				}
				else
				{
				txtEventID.value = tempParam[0];				
				}		
			}		
			function CheckAll(aspCheckBoxID, checkVal) 
			{
				re = new RegExp(':' + aspCheckBoxID + '$')  
				for(i = 0; i < document.forms[0].elements.length; i++) {
					elm = document.forms[0].elements[i]
					if (elm.type == 'checkbox') {
						if (re.test(elm.name)) {
						elm.checked = checkVal
						}
					}
				}
			}
			function ShowPPDealerSelection()
			{
				showPopUp('../PopUp/PopUpDealerSelectionOne.aspx','',500,760,DealerSelection);
			}
			function DealerSelection(selectedDealer)
			{				
				var txtDealerSelection = document.getElementById("txtDealerCode");
				txtDealerSelection.value = selectedDealer.split(';')[0];				
			}			
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage" style="HEIGHT: 19px">SHOWROOM AUDIT - Lihat Hasil Audit</td>
				</tr>
				<tr>
					<td style="HEIGHT: 1px" background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="10" src="../images/blank.gif" border="0"></td>
				</tr>
			</TABLE>
			<TABLE id="Table1" cellSpacing="1" cellPadding="2" width="100%" border="0">
				<TR>
					<TD class="titleField" style="HEIGHT: 18px">Kode Dealer</TD>
					<TD class="titleField" style="HEIGHT: 18px">:</TD>
					<TD style="HEIGHT: 18px"><asp:textbox id="txtDealerCode" Runat="server"></asp:textbox><asp:label onclick="ShowPPDealerSelection()" id="lblSearchDealer" runat="server">
							<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:label>
						<asp:Label id="lblDealerCode" runat="server">Label</asp:Label><INPUT id="txtUser" type="hidden" name="txtUser" runat="server"></TD>
				</TR>
				<TR>
					<TD class="titleField" style="HEIGHT: 18px">Kode Audit</TD>
					<TD class="titleField" style="HEIGHT: 18px">:</TD>
					<TD style="HEIGHT: 18px"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" id="txtAuditCode" onblur="omitSomeCharacter('txtAuditCode','<>?*%$;')"
							Runat="server"></asp:textbox><asp:label onclick="ShowAuditParameter()" id="lblSearchAudit" runat="server">
							<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:label></TD>
				</TR>
				<tr>
					<td></td>
					<td></td>
					<td><asp:button id="btnCari" runat="server" Width="56px" Text="Cari"></asp:button></td>
				</tr>
			</TABLE>
			<TABLE id="Table2" cellSpacing="1" cellPadding="2" width="100%" border="0">
				<tr>
					<td>
						<asp:datagrid id="dtgFiles" runat="server" Width="100%" AllowSorting="false" AllowPaging="false"
							PageSize="25" AllowCustomPaging="false" BackColor="#E0E0E0" AutoGenerateColumns="False" BorderColor="#CDCDCD"
							BorderWidth="1px" CellPadding="3" BorderStyle="None">
							<SelectedItemStyle ForeColor="#CCFF99" BackColor="#009999"></SelectedItemStyle>
							<AlternatingItemStyle BackColor="#F5F1EE"></AlternatingItemStyle>
							<ItemStyle BackColor="White"></ItemStyle>
							<HeaderStyle ForeColor="White" VerticalAlign="Top"></HeaderStyle>
							<FooterStyle ForeColor="#003399" BackColor="White"></FooterStyle>
							<Columns>
								<asp:TemplateColumn SortExpression="Code" HeaderText="Kode Dealer">
									<HeaderStyle Width="15%" CssClass="titleTableRsd"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id="lblDealerCode" runat="server" text='<%# DataBinder.Eval(Container,"DataItem.Dealer.DealerCode") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Nama Dealer">
									<HeaderStyle Width="15%" CssClass="titleTableRsd"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id="lblDealerName" runat="server" text='<%# DataBinder.Eval(Container,"DataItem.Dealer.DealerName") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="AssessmentItem" HeaderText="Download File Hasil Audit">
									<HeaderStyle Width="15%" CssClass="titleTableRsd"></HeaderStyle>
									<ItemTemplate>
										<asp:LinkButton id="lbtnJuklakFile" runat="server" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.AssessmentFile") %>' CommandName="DownloadJukLak">
										</asp:LinkButton>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Left" ForeColor="#003399" BackColor="#99CCCC" Mode="NumericPages"></PagerStyle>
						</asp:datagrid>
					</td>
				</tr>
				<TR>
					<TD class="titleField">Foto Awal</TD>
				</TR>
				<tr>
					<td>
						<asp:datagrid id="dtgFotoAwal" runat="server" Width="100%" AllowSorting="false" AllowPaging="false"
							PageSize="25" AllowCustomPaging="false" BackColor="#E0E0E0" AutoGenerateColumns="False" BorderColor="#CDCDCD"
							BorderWidth="1px" CellPadding="3" BorderStyle="None">
							<SelectedItemStyle ForeColor="#CCFF99" BackColor="#009999"></SelectedItemStyle>
							<AlternatingItemStyle BackColor="#F5F1EE"></AlternatingItemStyle>
							<ItemStyle BackColor="White"></ItemStyle>
							<HeaderStyle ForeColor="White" VerticalAlign="Top"></HeaderStyle>
							<FooterStyle ForeColor="#003399" BackColor="White"></FooterStyle>
							<Columns>
								<asp:TemplateColumn SortExpression="ItemDesc" HeaderText="Nama Foto">
									<HeaderStyle Width="15%" CssClass="titleTableRsd"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id="lblItemDesc" runat="server" text='<%# DataBinder.Eval(Container,"DataItem.AuditParameterPhoto.Description") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Foto">
									<HeaderStyle Width="15%" CssClass="titleTableRsd"></HeaderStyle>
									<ItemTemplate>
										<asp:Image ID="imgItemImage" Runat="server" style="cursor:hand"></asp:Image>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Upload By">
									<HeaderStyle Width="15%" CssClass="titleTableRsd"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id="lblUploadBy" runat="server" text='<%# DataBinder.Eval(Container,"DataItem.CreatedBy") + " on " + CDate(DataBinder.Eval(Container,"DataItem.CreatedTime")).ToString("dd/MM/yyyy") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Left" ForeColor="#003399" BackColor="#99CCCC" Mode="NumericPages"></PagerStyle>
						</asp:datagrid>
					</td>
				</tr>
				<TR>
					<TD class="titleField">Foto Perbaikan</TD>
				</TR>
				<tr>
					<td>
						<asp:datagrid id="dtgFotoPerbaikan" runat="server" Width="100%" AllowSorting="false" AllowPaging="false"
							PageSize="25" AllowCustomPaging="false" BackColor="#E0E0E0" AutoGenerateColumns="False" BorderColor="#CDCDCD"
							BorderWidth="1px" CellPadding="3" BorderStyle="None">
							<SelectedItemStyle ForeColor="#CCFF99" BackColor="#009999"></SelectedItemStyle>
							<AlternatingItemStyle BackColor="#F5F1EE"></AlternatingItemStyle>
							<ItemStyle BackColor="White"></ItemStyle>
							<HeaderStyle ForeColor="White" VerticalAlign="Top"></HeaderStyle>
							<FooterStyle ForeColor="#003399" BackColor="White"></FooterStyle>
							<Columns>
								<asp:TemplateColumn SortExpression="ItemDesc" HeaderText="Nama Foto">
									<HeaderStyle Width="15%" CssClass="titleTableRsd"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id="Label2" runat="server" text='<%# DataBinder.Eval(Container,"DataItem.AuditParameterPhoto.Description") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Foto">
									<HeaderStyle Width="15%" CssClass="titleTableRsd"></HeaderStyle>
									<ItemTemplate>
										<asp:Image ID="imgItemImage" Runat="server" style="cursor:hand"></asp:Image>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Update By">
									<HeaderStyle Width="15%" CssClass="titleTableRsd"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id="Label3" runat="server" text='<%# DataBinder.Eval(Container,"DataItem.LastUpdateBy") + " on " + Cdate(DataBinder.Eval(Container,"DataItem.LastUpdateTime")).ToString("dd/MM/yyyy") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Left" ForeColor="#003399" BackColor="#99CCCC" Mode="NumericPages"></PagerStyle>
						</asp:datagrid>
					</td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
