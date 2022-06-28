<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmFleetRequestList.aspx.vb" Inherits="FrmFleetRequestList" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Daftar Extended Free Service</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<base target="_self">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
        <script language="javascript" src="../WebResources/FormFunctions.js"></script>
		<script language="javascript">
		
		function ShowPPDealerSelectionOne()
		{
			showPopUp('../SparePart/../PopUp/PopUpDealerSelectionOne.aspx?x=Territory','',500,760,DealerSelectionOne);
		}
		function DealerSelectionOne(selectedDealer)
		{
			selectedDealer = selectedDealer + ";";
			var tempParam = selectedDealer.split(';');
			var txtDealerCode = document.getElementById("txtDealerCode");
			var lblDealerName = document.getElementById("lblDealerName");
			var lblDealerTerm = document.getElementById("lblDealerTerm");
			txtDealerCode.value = tempParam[0];
			lblDealerName.innerHTML = tempParam[1];
			lblDealerTerm.innerHTML = tempParam[3];
			
		}
		function ShowPPDealerSelection()
		{
			showPopUp('../General/../PopUp/PopUpDealerSelection2.aspx','',500,760,DealerSelection);
		}
		
		function DealerSelection(selectedDealer)
		{
		    //selectedDealer = selectedDealer + ";";
		    var tempParam = selectedDealer.split(';');
		    var txtDealerCode = document.getElementById("txtDealerCode");
		    var lblDealerName = document.getElementById("lblDealerName");
		    var lblDealerTerm = document.getElementById("lblDealerTerm");

		    var tempParam2 = tempParam[0].split('|');
		    txtDealerCode.value = tempParam2[0];
		    lblDealerName.innerHTML = tempParam2[1];
		    lblDealerTerm.innerHTML = tempParam2[2];

		    for (i = 1; i < tempParam.length; i++)
		    {
		        txtDealerCode.value = txtDealerCode.value + ';' + replace(tempParam[i], ' ', '');
		    }

		    //var txtDealerSelection = document.getElementById("txtDealerCode");
			//txtDealerSelection.value = selectedDealer;
			
			//var btnGetDealer = document.getElementById("btnGetDealer");
			//btnGetDealer.form.submit();
		}

		</script>
	    <style type="text/css">
            .auto-style1 {
                width: 39%;
            }
            .auto-style2 {
                width: 7%;
            }
            </style>
	</HEAD>
	<body onfocus="return checkModal()" onclick="checkModal()" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD class="titlePage" colSpan="3">Daftar Extended Free Service</TD>
				</TR>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD>
						<TABLE id="Table2" cellSpacing="1" cellPadding="2" width="100%" border="0">
							<TR>
								<TD class="titleField" width="24%">Kode Dealer</TD>
								<TD width="1%">:</TD>
								<TD class="auto-style1"><asp:label id="lblDealerCode" runat="server" Width="140px">Label</asp:label>
									<asp:textbox id="txtDealerCode" onkeypress="return alphaNumericExcept(event,'<>?*%$')" onblur="omitSomeCharacter('txtDealerCode','<>?*%$')" runat="server" Width="144px"></asp:textbox>
									<asp:label id="lblSearchDealer" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:label>
									<asp:button id="btnGetDealer" runat="server" Width="60px" Text="GetDealer"></asp:button></TD>
                                <td class="titleField"><asp:label id="lblNoMesin" Runat="server">Profil Bisnis</asp:label></td>
								<td>:</td>
								<td style="HEIGHT: 16px"><asp:dropdownlist id="ddlProfilBisnis" tabIndex="1" Runat="server"></asp:dropdownlist></td>
							</TR>
							<TR>
								<TD class="titleField">Nama Dealer</TD>
								<TD>:</TD>
								<TD class="auto-style1"><asp:label id="lblDealerName" runat="server">Label</asp:label>&nbsp;/
									<asp:label id="lblDealerTerm" runat="server">Label</asp:label></TD>
								<td class="titleField"><asp:label Runat="server">Tanggal Pengajuan</asp:label></td>
								<td>:</td>
								<td class="auto-style1">
                                    <table>
                                        <tr>
                                            <td><asp:CheckBox ID="chkTglPengajuan" runat="server" /></td>
                                            <td><cc1:inticalendar id="icTglPengajuan" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
                                            <td>s.d</td>
                                            <td><cc1:inticalendar id="icTglPengajuanTo" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
                                        </tr>
                                    </table>
								</td>
							</TR>
                            <TR>
								<TD class="titleField"><asp:label id="lblDealer" Runat="server">No Surat MFTBC</asp:label></TD>
								<TD>:</TD>
								<TD class="auto-style1">
									<asp:dropdownlist id="ddlFleetNumber" tabIndex="1" Runat="server"></asp:dropdownlist>
								</TD>
                                <TD class="titleField"><asp:label id="Label1" Runat="server">Mulai Pengadaan</asp:label></TD>
								<td>:</td>
								<TD width="34%">
                                    <table>
                                        <tr>
                                            <td><asp:CheckBox ID="chkMulaiPengadaan" runat="server" /></td>
                                            <td><cc1:inticalendar id="icTglMulaiPengadaan" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
                                            <td>s.d</td>
                                            <td><cc1:inticalendar id="icTglMulaiPengadaanTo" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
                                        </tr>
                                    </table>
								</TD>
							</TR>
                            <TR>
								<td class="titleField">No Extended Free Service</td>
								<td>:</td>
								<td class="auto-style1">
                                    <asp:TextBox ID="txtNoRegRequest" runat="server"></asp:TextBox></td>
                                <td class="titleField" nowrap="nowrap">Selesai Pengadaan</td>
								<td>:</td>
								<td>
                                    <table>
                                        <tr>
                                            <td><asp:CheckBox ID="chkSelesaiPengadaan" runat="server" /></td>
                                            <td><cc1:inticalendar id="icTglSelesaiPengadaan" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
                                            <td>s.d</td>
                                            <td><cc1:inticalendar id="icTglSelesaiPengadaanTo" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
                                        </tr>
                                    </table>
								</td>
							</TR>
                            <TR>
                                <td class="titleField"><asp:label id="lblNoChasis" Runat="server">Status Konsumen</asp:label></td>
								<td>:</td>
								<td><asp:dropdownlist id="ddlStatusKonsumen" tabIndex="1" Runat="server"></asp:dropdownlist></td>
                                <td class="titleField" rowspan="3" valign="top"><asp:label id="Label4" Runat="server">Status</asp:label></td>
								<td rowspan="3" valign="top">:</td>
								<td rowspan="3" valign="top">
                                    <asp:dropdownlist id="ddlStatus" tabIndex="1" Runat="server"></asp:dropdownlist>
                                </td>
							</TR>
                            <TR>
								<td class="titleField"><asp:label id="lblNamaKonsumen" Runat="server">Nama Konsumen</asp:label></td>
								<td>:</td>
								<td class="auto-style1"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" id="txtNamaKonsumen" onblur="omitSomeCharacter('txtNoChasis','<>?*%$;')"
										Runat="server" Width="160px"></asp:textbox></td>
							</TR>
                            <TR>
								<td class="titleField">&nbsp;</td>
								<td>&nbsp;</td>
								<td class="auto-style1">&nbsp;</td>
							</TR>
							<TR>
								<TD class="titleField" colspan="5"></TD>
								<TD class="auto-style2"><table border="0" cellpadding="2" cellspacing="0">
										<tr>
											<td><asp:button id="btnFind" runat="server" Width="60px" Text="Cari"></asp:button>
                                            </td>
										</tr>
									</table>
								</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD colSpan="3"><div id="div1" style="OVERFLOW: auto;"><asp:datagrid id="dtgFleetRequest" runat="server" Width="100%" BorderColor="Gainsboro" BackColor="Gainsboro"
								CellSpacing="1" CellPadding="3" OnItemDataBound="dtgFleetRequest_ItemDataBound" BorderWidth="0px" AutoGenerateColumns="False" PageSize="10" AllowPaging="True"
								AllowCustomPaging="True" AllowSorting="True">
								<AlternatingItemStyle ForeColor="Black" BackColor="White"></AlternatingItemStyle>
								<ItemStyle ForeColor="Black" BackColor="#F1F6FB"></ItemStyle>
								<HeaderStyle Font-Bold="True" Height="20px" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="id" HeaderText="ID">
										<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<FooterStyle HorizontalAlign="Right"></FooterStyle>
									</asp:BoundColumn>
                                    <asp:TemplateColumn >
										<HeaderStyle HorizontalAlign="Center" Width="3%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" Wrap="False"></ItemStyle>
							            <HeaderTemplate>
								            <input id="chkAllItemsTop" type="checkbox" onclick="CheckAllDataGridCheckBoxes('chkFleetRequest',
														            document.forms[0].chkAllItemsTop.checked)" />
							            </HeaderTemplate>
										<ItemTemplate>
                                            <asp:CheckBox ID="chkFleetRequest" runat="server" value="test"/>
										</ItemTemplate>
                                        <FooterTemplate>
                                            <input id="chkAllItemsBottom" type="checkbox" onclick="CheckAllDataGridCheckBoxes('chkFleetRequest',
														            document.forms[0].chkAllItemsBottom.checked)" />
                                        </FooterTemplate>
										<FooterStyle HorizontalAlign="Center"></FooterStyle>
									</asp:TemplateColumn>
									<asp:BoundColumn HeaderText="No">
										<HeaderStyle Width="3%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Kode Dealer" SortExpression="FleetMasterDealer.Dealer.DealerCode">
										<HeaderStyle Width="5%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        <ItemTemplate>
											<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FleetMasterDealer.Dealer.DealerCode")%>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="No Surat MFTBC" SortExpression="FleetMasterDealer.FleetMaster.NoFleet">
										<HeaderStyle Width="15%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        <ItemTemplate>
											<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FleetMasterDealer.FleetMaster.NoFleet")%>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="NamaKonsumen" HeaderText="Nama Konsumen" SortExpression="NamaKonsumen">
										<HeaderStyle Width="15%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="NoRegRequest" HeaderText="No Extended Free Service" SortExpression="NoRegRequest">
										<HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="TanggalPengajuan" HeaderText="Tanggal" SortExpression="TanggalPengajuan" DataFormatString="{0:dd/MM/yyyy}">
										<HeaderStyle Width="5%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Status Konsumen" SortExpression="StatusKonsumen">
										<HeaderStyle HorizontalAlign="Center" Width="10%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" Wrap="False"></ItemStyle>
                                        <ItemTemplate>
											<asp:Label runat="server" id="lblStatusKonsumen"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Profile Bisnis" SortExpression="ProfilBisnis">
										<HeaderStyle HorizontalAlign="Center" Width="15%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" Wrap="False"></ItemStyle>
                                        <ItemTemplate>
											<asp:Label runat="server" Text='<%# GetProfilBisnis(DataBinder.Eval(Container, "DataItem.ProfilBisnis"))%>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="MulaiPengadaan" HeaderText="Mulai Pengadaan" SortExpression="MulaiPengadaan" DataFormatString="{0:dd/MM/yyyy}">
										<HeaderStyle HorizontalAlign="Center" Width="5%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" Wrap="False"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="SelesaiPengadaan" HeaderText="Selesai Pengadaan" SortExpression="SelesaiPengadaan" DataFormatString="{0:dd/MM/yyyy}">
										<HeaderStyle HorizontalAlign="Center" Width="5%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" Wrap="False"></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Status" SortExpression="Status">
										<HeaderStyle HorizontalAlign="Center" Width="3%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" Wrap="False"></ItemStyle>
                                        <ItemTemplate>
											<asp:Label runat="server" id="lblStatus"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Batal Konfirmasi Note" SortExpression="BatalKonfirmasiNote">
										<HeaderStyle HorizontalAlign="Center" Width="3%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" Wrap="False"></ItemStyle>
                                        <ItemTemplate>
											<asp:TextBox ID="txtBatalKonfirmasi" runat="server"></asp:TextBox>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Action" >
										<HeaderStyle HorizontalAlign="Center" Width="3%" CssClass="titleTableParts"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" Wrap="False"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="lnkSave" runat="server" CausesValidation="False" CommandName="View" CommandArgument='<%#Eval("ID")%>'>
												<img src="../images/detail.gif" border="0" alt="View Details"></asp:LinkButton>
                                            <asp:LinkButton id="linkDelete" runat="server" CausesValidation="False" CommandName="Delete" CommandArgument='<%#Eval("ID")%>'>
												<img src="../images/trash.gif" border="0" alt="Delete Fleet Request" onclick="return confirm('Apakah anda akan menghapus Pengajuan Extended Free Service ini ?')"></asp:LinkButton>
                                            <asp:LinkButton id="linkEdit" runat="server" CausesValidation="False" CommandName="Edit" CommandArgument='<%#Eval("ID")%>'>
												<img src="../images/edit.gif" border="0" alt="Edit Pengajuan Extended Free Service"></asp:LinkButton>
										</ItemTemplate>
										<FooterStyle HorizontalAlign="Center"></FooterStyle>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></div>
					</TD>
				</TR>
                <TR>
					<TD colSpan="3">
                        <table>
                            <tr>
                                <td>Mengubah Status</td>
                                <td><asp:DropDownList ID="ddlRubahStatus" runat="server" AutoPostBack="True"></asp:DropDownList></td>
                                <td><asp:Button ID="btnProcess" runat="server" Text="Proses"/></td>
                            </tr>
                        </table>
                         
					</TD>
				</TR>
			</TABLE>
		<script language="javascript" type="text/javascript">
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
		</form>
		</body>
</HTML>
