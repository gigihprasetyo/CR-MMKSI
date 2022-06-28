<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>

<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmGetFreeServiceDataStatus.aspx.vb" Inherits="FrmGetFreeServiceDataStatus" SmartNavigation="False" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
		<title>Daftar Status Free Service</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script type="text/javascript">
		
			
        function showReasonDescription(reasonID, description) {
				var URL = "FrmFSReasonDescription.aspx?ID=" + reasonID + "&description=" + description;
				window.showModalDialog(URL);
				return false;
			}
			
        function ShowPPDealerSelection() {
            showPopUp('../General/../PopUp/PopUpDealerSelection.aspx', '', 500, 760, DealerSelection);
			}
			
        function DealerSelection(selectedDealer) {
				var txtDealerSelection = document.getElementById("txtKodeDealer");
				txtDealerSelection.value = selectedDealer;			
			}

			function ShowPPDealerBranchSelection() {

			    var txtDealerSelection = document.getElementById("txtKodeDealer");
			    showPopUp('../General/../PopUp/PopUpDealerBranchMultipleSelection.aspx?DealerCode=' + txtDealerSelection.value, '', 500, 760, DealerBranchSelection);
			}

			function DealerBranchSelection(selectedDealer) {
			    var txtDealerSelection = document.getElementById("txtDealerBranchCode");
			    txtDealerSelection.value = selectedDealer;
			}
			
			function ShowPartDetail(FSID) {
			    showPopUp('../General/../PopUp/PopUpFreeServicePartDetail.aspx?FSID=' + FSID, '', 400, 660);
			}

		</script>
</head>
<body ms_positioning="GridLayout">
		<form id="Form1" method="post" runat="server">
        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">FREE SERVICE - Daftar Status Free Service</td>
				</tr>
				<tr>
                <td background="../images/bg_hor.gif" height="1">
                    <img height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
                <td height="10">
                    <img height="1" src="../images/dot.gif" border="0"></td>
				</tr>
            <tr>
                <td valign="top" align="left">
                    <table id="Table2" cellspacing="1" cellpadding="2" width="100%" border="0">
                        <tr>
                            <td class="titleField" style="height: 14px" width="24%">
                                <asp:Label ID="lblDealer" runat="server">Kode Dealer </asp:Label></td>
                            <td style="height: 14px" width="1%">:</td>
                            <td style="height: 14px" width="25%">
                                <asp:TextBox ID="txtKodeDealer" onkeypress="return alphaNumericExcept(event,'<>?*%$;')" runat="server"
                                    onblur="omitSomeCharacter('txtKodeDealer','<>?*%$;')"></asp:TextBox>
                                <asp:Label ID="lblSearchDealer" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:Label></td>
								<td width="20%"></td>
								<td width="1%"></td>
								<td width="29%"></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="height: 14px" width="24%">
                                <asp:Label ID="lblDealerBranch" runat="server">Kode Cabang </asp:Label></td>
                            <td style="height: 14px" width="1%">:</td>
                            <td style="height: 14px" width="25%">
                                <asp:TextBox ID="txtDealerBranchCode" onkeypress="return alphaNumericExcept(event,'<>?*%$;')" runat="server"
                                    onblur="omitSomeCharacter('txtDealerBranchCode','<>?*%$;')"></asp:TextBox>
                                <asp:Label ID="lblSearchDealerBranch" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:Label></td>
								<td width="20%"></td>
								<td width="1%"></td>
								<td width="29%"></td>
                        </tr>
                        <tr>
                            <td class="titleField">Status</td>
                            <td>:</td>
                            <td>
                                <asp:DropDownList ID="ddlStatus" runat="server"></asp:DropDownList></td>
                            <td><strong>
                                <asp:Label runat="server" ID="lblNoRangka">Nomor Rangka</asp:Label></strong></td>
                            <td>
                                <asp:Label runat="server" ID="Label3">:</asp:Label></td>
                            <td>
                                <asp:TextBox ID="txtNomorRangka" runat="server"></asp:TextBox>
                            </td>
                            
                        </tr>
                        <tr>
                            <td class="titleField">Kode Penolakan</td>
                            <td>:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlRejectStatus" runat="server">
										<asp:ListItem Value="Semua">Semua</asp:ListItem>
										<asp:ListItem Value="APP" Selected="True">APP</asp:ListItem>
										<asp:ListItem Value="DAPP">DAPP</asp:ListItem>
                                </asp:DropDownList></td>
                            <td class="titleField">Tipe FS</td>
                            <td>:</td>
                            <td>
                                <asp:DropDownList ID="ddlFSType" runat="server" Width="104px">
                                    <asp:ListItem Value="0">Semua</asp:ListItem>
                                    <asp:ListItem Value="Z1" Selected="True">Regular</asp:ListItem>
                                    <asp:ListItem Value="Z3">Campaign</asp:ListItem>
                                    <asp:ListItem Value="2">Labor</asp:ListItem>
                                    <asp:ListItem Value="3">Maintenance</asp:ListItem>
                                    <asp:ListItem Value="4">Maintenance Silver</asp:ListItem>
                                    <asp:ListItem Value="5">Maintenance Gold</asp:ListItem>
                                    <asp:ListItem Value="6">Maintenance Diamond</asp:ListItem>
                                    <asp:ListItem Value="7">Extended 2XPM</asp:ListItem>
                                    <asp:ListItem Value="8">Extended 4XPM</asp:ListItem>
                                    <asp:ListItem Value="9">Extended 6XPM</asp:ListItem>
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="height: 18px" valign="top">Periode Rilis</td>
                            <td style="height: 18px" valign="top">:</td>
                            <td style="height: 18px" valign="top">
									<table cellpadding="0" cellspacing="0">
										<tr>
											<td>
                                            <cc1:IntiCalendar ID="ICDari" runat="server" TextBoxWidth="70"></cc1:IntiCalendar></td>
                                        <td>&nbsp;s.d&nbsp;</td>
											<td>
                                            <cc1:IntiCalendar ID="ICSampai" runat="server" TextBoxWidth="70"></cc1:IntiCalendar></td>
                                        <td></td>
                                    </tr>
                                </table>
                            </td>
                            <td><strong><asp:Label runat="server" ID="lblCategori">Kategori</asp:Label></strong></td>
							<td><asp:Label runat="server" ID="lblCategori2">:</asp:Label></td>
							<td><asp:DropDownList Style="z-index: 0" ID="ddlCategory" runat="server" Width="140px"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td><strong><asp:Label runat="server" ID="Label1">Berkas</asp:Label></strong></td>
                            <td><asp:Label runat="server" ID="Label2">:</asp:Label></td>
                            <td>
                                <asp:DropDownList Style="z-index: 0" ID="ddlEvidence" runat="server" Width="140px">
                                    <asp:ListItem Value="0" Selected="True">Semua</asp:ListItem>
                                    <asp:ListItem Value="1">Terlampir</asp:ListItem>
                                    <asp:ListItem Value="2">Tidak terlampir</asp:ListItem>
                                </asp:DropDownList>
                            </td>
										</tr>
                        <tr>
								<td></td>
								<td></td>
                            <td><asp:Button ID="btnRefresh" runat="server" Width="60px" Text="Cari"></asp:Button></td>
                        </tr>
                    </table>
                </td>
            </tr>
                <tr>
                    <td></td>
                </tr>
            <tr>
                <td valign="top">
                    <div id="div1" style="height: 280px; overflow: auto">
                        <asp:DataGrid ID="dgFreeService" runat="server" Width="100%" CellSpacing="1" AllowSorting="True"
								PageSize="50" AllowPaging="True" GridLines="Vertical" CellPadding="3" BackColor="#CDCDCD" BorderWidth="0px" BorderStyle="None" BorderColor="#E7E7FF"
								AutoGenerateColumns="False" ShowFooter="True">
								<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
								<AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
								<ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
								<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
								<Columns>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle Width="3%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
                                        <asp:Label ID="lblNo" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Status" HeaderText="Status">
										<HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
                                        <asp:Label ID="lblStatus" runat="server"></asp:Label>
										</ItemTemplate>
										<FooterStyle Wrap="False" HorizontalAlign="Left" VerticalAlign="Top"></FooterStyle>
										<FooterTemplate>
                                        <b>Baru:</b>
                                        <asp:Label ID="lblTotalNew" runat="server"></asp:Label><br>
                                        <b>Proses:</b>
                                        <asp:Label ID="lblTotalProcessed" runat="server"></asp:Label>
										</FooterTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="DealerCode" HeaderText="Dealer">
										<HeaderStyle Width="6%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
                                        <asp:Label ID="lblKodeDealer" runat="server" ToolTip='<%# DataBinder.Eval(Container, "DataItem.SearchTerm1")%>' Text='<%# DataBinder.Eval(Container, "DataItem.DealerCode") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="DealerBranchCode" HeaderText="Kode Cabang">
										<HeaderStyle Width="7%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
                                        <asp:Label ID="lblKodeDealerBranch" runat="server" ToolTip='<%# DataBinder.Eval(Container, "DataItem.DealerBranchCode")%>' Text='<%# DataBinder.Eval(Container, "DataItem.DealerBranchCode")%>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="ChassisNumber" HeaderText="No Rangka">
										<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
                                        <asp:Label ID="lblNoChassis" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ChassisNumber")%>'>
											</asp:Label>
										</ItemTemplate>
										<FooterStyle Wrap="False" HorizontalAlign="Left" VerticalAlign="Top"></FooterStyle>
										<FooterTemplate>
											<b>Disetujui:</b>
                                        <asp:Label ID="lblTotalApp" runat="server"></asp:Label>
											<br>
											<b>Tidak disetujui:</b>
                                        <asp:Label ID="lblTotalDisapp" runat="server"></asp:Label>
										</FooterTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="CategoryCode" HeaderText="Kategori" Visible="False">
										<HeaderStyle Width="6%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
                                        <asp:Label ID="lblCategory" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CategoryCode")%>'>
											</asp:Label>
										</ItemTemplate>
										<FooterStyle Wrap="False" HorizontalAlign="Left" VerticalAlign="Top"></FooterStyle>
										<FooterTemplate>
										</FooterTemplate>
									</asp:TemplateColumn>

                                    <asp:TemplateColumn SortExpression="NoRegRequest" HeaderText="No Extended Free Service" Visible="false">
										<HeaderStyle Width="6%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
                                        <asp:Label ID="lblNoRegRequest" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NoRegRequest") %>'>
											</asp:Label>
										</ItemTemplate>
										<FooterStyle Wrap="False" HorizontalAlign="Left" VerticalAlign="Top"></FooterStyle>
										<FooterTemplate>
										</FooterTemplate>
									</asp:TemplateColumn>

									<asp:TemplateColumn SortExpression="KindCode" HeaderText="Jenis FS">
										<HeaderStyle Width="6%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
                                        <asp:Label ID="lblJenis" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.KindCode")%>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
                                    									
									<asp:TemplateColumn SortExpression="VisitType" HeaderText="Tipe Visit">
										<HeaderStyle Width="6%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
                                        <asp:Label ID="lblVisitType" runat="server">
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
                                    
									<asp:BoundColumn Visible="False" DataField="ServiceDate" SortExpression="ServiceDate" HeaderText="Tgl Servis"
										DataFormatString="{0:dd/MM/yyyy}">
										<HeaderStyle Width="6%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn Visible="False" SortExpression="SoldDate" HeaderText="Tgl Penjualan">
										<HeaderStyle Width="6%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
                                        <asp:Label ID="lblTanggalJual" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn Visible="False" DataField="MileAge" SortExpression="MileAge" HeaderText="Jarak Tempuh"
										DataFormatString="{0:#.###}">
										<HeaderStyle Width="6%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="Reject" SortExpression="Reject" HeaderText="Tolak">
										<HeaderStyle Width="8%" CssClass="titleTableService"></HeaderStyle>
										<FooterStyle VerticalAlign="Top"></FooterStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="ReleaseDate" SortExpression="ReleaseDate" HeaderText="Tgl Rilis" DataFormatString="{0:dd/MM/yyyy}">
										<HeaderStyle ForeColor="White" Width="8%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
									</asp:BoundColumn>
                                    <asp:TemplateColumn SortExpression="WorkOrderNumber" HeaderText="WO Number">
										<HeaderStyle Width="6%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
                                        <asp:Label ID="lblWONumber" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.WorkOrderNumber")%>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="NotificationNumber" HeaderText="Notifikasi">
										<HeaderStyle Width="8%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
                                        <asp:Label ID="lblNotifikasi" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NotificationNumber") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Kode Penolakan" SortExpression="ReasonCode">
										<HeaderStyle Width="8%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle VerticalAlign="Top" HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
                                        <asp:Label ID="lblAlasan" runat="server" ToolTip='<%# DataBinder.Eval(Container, "DataItem.ReasonDescription")%>' Text='<%# DataBinder.Eval(Container, "DataItem.ReasonCode")%>'>
											</asp:Label>
										</ItemTemplate>
										<FooterStyle Wrap="False" HorizontalAlign="Right" VerticalAlign="Top"></FooterStyle>
										<FooterTemplate>
                                        <asp:Label ID="lblGrandTotalA" runat="server"></asp:Label><br>
                                        <asp:Label ID="lblGrandTotalD" runat="server"></asp:Label>
										</FooterTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn Visible="False" SortExpression="ReasonDescription" HeaderText="Deskripsi Alasan">
										<HeaderStyle Width="8%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
                                        <asp:Label ID="lblDeskripsi" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ReasonDescription")%>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="LabourAmount" HeaderText="Ongkos Kerja (Rp)">
										<HeaderStyle Width="7%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
                                        <asp:Label ID="lblLabourAmount" runat="server"></asp:Label>
										</ItemTemplate>
										<FooterStyle HorizontalAlign="Right" VerticalAlign="Top"></FooterStyle>
										<FooterTemplate>
                                        <asp:Label ID="lblTotalLabourAmountA" runat="server"></asp:Label><br>
                                        <asp:Label ID="lblTotalLabourAmountD" runat="server"></asp:Label>
										</FooterTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="PartAmount" HeaderText="Penggantian Parts (Rp)">
										<HeaderStyle Width="7%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
                                            <asp:LinkButton ID="lbtnPartAmount" runat="server"></asp:LinkButton>
											<%--<asp:Label id="lblPartAmount" runat="server"></asp:Label>--%>
										</ItemTemplate>
										<FooterStyle HorizontalAlign="Right" VerticalAlign="Top"></FooterStyle>
										<FooterTemplate>
                                        <asp:Label ID="lblTotalPartAmountA" runat="server"></asp:Label><br>
                                        <asp:Label ID="lblTotalPartAmountD" runat="server"></asp:Label>
										</FooterTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="PPNAmount" HeaderText="PPN (Rp)">
										<HeaderStyle Width="7%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
                                        <asp:Label ID="lblPPNAmount" runat="server"></asp:Label>
										</ItemTemplate>
										<FooterStyle HorizontalAlign="Right" VerticalAlign="Top"></FooterStyle>
										<FooterTemplate>
                                        <asp:Label ID="lblTotalPPNAmountA" runat="server"></asp:Label><br>
                                        <asp:Label ID="lblTotalPPNAmountD" runat="server"></asp:Label>
										</FooterTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="PPHAmount" HeaderText="PPh (Rp)">
										<HeaderStyle Width="7%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
                                        <asp:Label ID="lblPPHAmount" runat="server"></asp:Label>
										</ItemTemplate>
										<FooterStyle HorizontalAlign="Right" VerticalAlign="Top"></FooterStyle>
										<FooterTemplate>
                                        <asp:Label ID="lblTotalPPHAmountA" runat="server"></asp:Label><br>
                                        <asp:Label ID="lblTotalPPHAmountD" runat="server"></asp:Label>
										</FooterTemplate>
									</asp:TemplateColumn>                                    
                                    <asp:TemplateColumn SortExpression="Cashback" HeaderText="Cashback (Rp)">
										<HeaderStyle Width="7%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
                                        <asp:Label ID="lblCashback" runat="server"></asp:Label>
										</ItemTemplate>
										<FooterStyle HorizontalAlign="Right" VerticalAlign="Top"></FooterStyle>
										<FooterTemplate>
                                        <asp:Label ID="lblCashbackA" runat="server"></asp:Label><br>
                                        <asp:Label ID="lblCashbackD" runat="server"></asp:Label>
										</FooterTemplate>
									</asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Berkas">
										<HeaderStyle Width="17%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
                                            <div style="display:inline" >
                                            <asp:LinkButton id="lbtnDownload" runat="server" Text="Download" CausesValidation="False" CommandName="Download">
												<img src="../images/download.gif" border="0" alt="Download"></asp:LinkButton>
                                            <asp:Label ID="lblEvStatus" runat="server"></asp:Label>
                                                </div>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
                        </asp:DataGrid>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="height: 40px" align="left">&nbsp;
						<asp:Button ID="btnDownload" runat="server" Width="70px" Text="Download" Enabled="False" Height="24px"></asp:Button></td>
            </tr>
        </table>
		</form>
		<script language="javascript">
        if (document.parentWindow.name != "frMain") {
				  self.opener = null;
				  self.close();
				}
		</script>
	</body>
</html>
