<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmMSPClaimList.aspx.vb" Inherits=".FrmMSPClaimList"  smartNavigation="False" %>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
    <head>
		<title>Daftar MSP Claim</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
        <script type="text/javascript">
            function ShowPPDealerSelection() {
                showPopUp('../General/../PopUp/PopUpDealerSelection.aspx', '', 500, 760, DealerSelection);
            }

            function DealerSelection(selectedDealer) {
                var txtDealerSelection = document.getElementById("txtKodeDealer");
                txtDealerSelection.value = selectedDealer;
            }

            function CheckAll(aspCheckBoxID, checkVal) {
                re = new RegExp(':' + aspCheckBoxID + '$')
                for (i = 0; i < document.forms[0].elements.length; i++) {
                    elm = document.forms[0].elements[i]
                    if (elm.type == 'checkbox') {
                        if (re.test(elm.name)) {
                            elm.checked = checkVal
                        }
                    }
                }
            }

		</script>
	</head>
<body MS_POSITIONING="GridLayout">
    <form id="Form1" method="post" runat="server">
    <table id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
        <tr>
			<td class="titlePage">MSP - Daftar Claim MSP</td>
		</tr>
		<tr>
			<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
		</tr>
		<tr>
			<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
		</tr>
        <tr>
			<td vAlign="top" align="left">
                <table id="Table2" cellSpacing="1" cellPadding="2" width="100%" border="0">
					<tr>
						<td class="titleField" style="HEIGHT: 14px" width="20%">
                            <asp:label id="lblDealer" runat="server">Dealer</asp:label>
						</td>
						<td style="HEIGHT: 14px" width="1%">:</td>
						<td style="HEIGHT: 14px" width="29%">
							<asp:label ID="lblDealerName" runat="server" Visible="false"></asp:label>
                            <asp:textbox id="txtKodeDealer" runat="server" Visible="false"></asp:textbox>
							<asp:label id="lblSearchDealer" runat="server" onclick="ShowPPDealerSelection();" Visible ="false">
								<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup">
							</asp:label>
						</td>
						<td width="17%"  class="titleField">
                            <asp:Label ID="lblMSPType" runat="server">Tipe MSP</asp:Label>
						</td>
						<td width="1%">:</td>
						<td width="32%">
                            <asp:DropDownList runat="server" ID="ddlMSPType"></asp:DropDownList>
						</td>
					</tr>
                    <tr>
						<td class="titleField" style="HEIGHT: 14px" width="20%">
                            <asp:label id="lblMSPNo" runat="server">No Claim</asp:label>
						</td>
						<td style="HEIGHT: 14px" width="1%">:</td>
						<td style="HEIGHT: 14px" width="29%">
							<asp:textbox id="txtClaimNo" runat="server"></asp:textbox>
						</td>
						<td width="17%"  class="titleField">
                            <asp:checkbox id="chkClaimDate" Runat="server"></asp:checkbox>
                            <asp:Label ID="Label2" runat="server">Tgl Claim</asp:Label>
						</td>
						<td width="1%">:</td>
						<td width="32%">
                            <table cellpadding="0" cellspacing="0">
								<tr>
									<td>
										<cc1:inticalendar id="DateFrom" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
									<td>
										&nbsp;s.d&nbsp;</td>
									<td>
										<cc1:inticalendar id="DateTo" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
									<td>
									</td>
								</tr>
							</table>
						</td>
					</tr>
                     <tr  class="titleField">
						<td class="titleField" style="HEIGHT: 14px" width="20%">
                            <asp:label id="lblChassisNumber" runat="server">No Rangka</asp:label>
						</td>
						<td style="HEIGHT: 14px" width="1%">:</td>
						<td style="HEIGHT: 14px" width="29%">
							<asp:textbox id="txtChassisNumber" runat="server"></asp:textbox>
						</td>
						<td width="17%">
                            <asp:Label ID="lblStatus" runat="server">Status</asp:Label>
						</td>
						<td width="1%">:</td>
						<td width="32%">
                            <asp:ListBox ID="lboxStatus" runat="server" Width="140px" Rows="3" SelectionMode="Multiple"></asp:ListBox>
						</td>
					</tr>
                     <tr>
						<td class="titleField" style="HEIGHT: 14px" width="20%">
                            <asp:label id="Label1" runat="server">No MSP</asp:label>
						</td>
						<td style="HEIGHT: 14px" width="1%">:</td>
						<td style="HEIGHT: 14px" width="29%">
							<asp:textbox id="txtMSPNo" runat="server"></asp:textbox>
						</td>
						<td width="17%"  class="titleField">
                            <asp:Label ID="Label3" runat="server">Package KM</asp:Label>
						</td>
						<td width="1%">:</td>
						<td width="32%">
                            <asp:textbox id="txtPackegeKM" runat="server" onblur="numericOnlyBlur(txtPackegeKM)" onkeypress="return numericOnlyUniv(event)"></asp:textbox>
						</td>
					</tr>
                    <tr>
                        <td class="titleField" style="HEIGHT: 14px" width="20%">
                            <asp:label id="Label4" runat="server">Kategori</asp:label>
						&nbsp;Kendaraan</td>
						<td style="HEIGHT: 14px" width="1%">:</td>
						<td style="HEIGHT: 14px" width="39%">
						 
                            <asp:dropdownlist runat="server" ID="ddlCategoryV" width="161px" AutoPostBack="True"></asp:dropdownlist>
									<asp:dropdownlist runat="server" ID="ddlVechileModel" width="161px" AutoPostBack="True"></asp:dropdownlist>

						</td>
						<td width="7%"  class="titleField">
                            Tipe Kendaraan</td>
						<td width="1%">&nbsp;</td>
						<td width="32%">
                           
									<asp:dropdownlist runat="server" ID="ddlVechileType" width="161px" AutoPostBack="True"></asp:dropdownlist>
                           
						</td>
                    </tr>
                      <tr>
						<td class="titleField" width="14%"></td>
						<td width="1%"></td>
						<td style="WIDTH: 262px" width="35%">
							  
						</td>
					</tr>

                    <tr>
						<td class="titleField" width="14%"></td>
						<td width="1%"></td>
						<td style="WIDTH: 262px" width="35%">
							<asp:button id="btnSearch" runat="server" Text="Cari" width="70px"></asp:button>
						</td>
					</tr>
                </table>
            </td>
        </tr>
        <tr>
            <td vAlign="top">
				<div id="div1" style="OVERFLOW: auto">
                    <asp:datagrid id="dtgMSPClaimList" runat="server" Width="100%" CellSpacing="1" AllowSorting="True" PageSize="15" AllowCustomPaging="true" AllowPaging="True" GridLines="Vertical" CellPadding="3" BackColor="#CDCDCD" BorderWidth="0px" BorderStyle="None" BorderColor="#E7E7FF" AutoGenerateColumns="False" ShowFooter="false">
						<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
						<AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
						<ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
						<HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
						<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
						<Columns>
                            
                            <asp:TemplateColumn HeaderText="No">
								<HeaderStyle Width="3%" CssClass="titleTableService"></HeaderStyle>
								<ItemStyle HorizontalAlign="Center"></ItemStyle>
								<ItemTemplate>
									<asp:Label id="lblNo" runat="server"></asp:Label>
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn SortExpression="Status" HeaderText="Status">
								<HeaderStyle CssClass="titleTableService"></HeaderStyle>
								<ItemStyle HorizontalAlign="Center"></ItemStyle>
								<ItemTemplate>
									<asp:Label id="lblStatus" runat="server"></asp:Label>
								</ItemTemplate>
							</asp:TemplateColumn>
                            <asp:TemplateColumn SortExpression="ClaimNumber" HeaderText="No Claim">
								<HeaderStyle CssClass="titleTableService"></HeaderStyle>
								<ItemStyle HorizontalAlign="Center"></ItemStyle>
								<ItemTemplate>
									<asp:Label id="lblClaimNo" runat="server"></asp:Label>
								</ItemTemplate>
							</asp:TemplateColumn>
                            <asp:TemplateColumn SortExpression="Dealer.DealerCode" HeaderText="Kode Dealer">
								<HeaderStyle CssClass="titleTableService"></HeaderStyle>
								<ItemStyle HorizontalAlign="Center"></ItemStyle>
								<ItemTemplate>
									<asp:Label id="lblDealerCode" runat="server"></asp:Label>
								</ItemTemplate>
							</asp:TemplateColumn>
                            <asp:TemplateColumn SortExpression="Dealer.DealerName" HeaderText="Nama Dealer">
								<HeaderStyle CssClass="titleTableService"></HeaderStyle>
								<ItemStyle HorizontalAlign="Center"></ItemStyle>
								<ItemTemplate>
									<asp:Label id="lblDealerName" runat="server"></asp:Label>
								</ItemTemplate>
							</asp:TemplateColumn>

                             <asp:TemplateColumn SortExpression="MSPRegistrationHistory.MSPRegistration.ChassisMaster.ChassisNumber" HeaderText="No Rangka">
								<HeaderStyle CssClass="titleTableService"></HeaderStyle>
								<ItemStyle HorizontalAlign="Center"></ItemStyle>
								<ItemTemplate>
									<asp:Label id="lblChassisNumber" runat="server"></asp:Label>
								</ItemTemplate>
							</asp:TemplateColumn>

                                <asp:TemplateColumn SortExpression="MSPRegistrationHistory.MSPRegistration.ChassisMaster.VechileColor.VechileType.Description" HeaderText="Nama Kendaraan">
								<HeaderStyle CssClass="titleTableService"></HeaderStyle>
								<ItemStyle HorizontalAlign="Center"></ItemStyle>
								<ItemTemplate>
									<asp:Label id="lblVehicleDescription" runat="server"></asp:Label>
								</ItemTemplate>
							</asp:TemplateColumn>

                           <asp:TemplateColumn SortExpression="MSPRegistrationHistory.MSPRegistration.MSPCode" HeaderText="No MSP">
								<HeaderStyle CssClass="titleTableService"></HeaderStyle>
								<ItemStyle HorizontalAlign="Center"></ItemStyle>
								<ItemTemplate>
									<asp:Label id="lblMSPCode" runat="server"></asp:Label>
								</ItemTemplate>
							</asp:TemplateColumn>
                           <asp:TemplateColumn SortExpression="MSPRegistrationHistory.MSPMaster.MSPType.Description" HeaderText="Tipe MSP">
								<HeaderStyle CssClass="titleTableService"></HeaderStyle>
								<ItemStyle HorizontalAlign="Center"></ItemStyle>
								<ItemTemplate>
									<asp:Label id="lblMSPType" runat="server"></asp:Label>
								</ItemTemplate>
							</asp:TemplateColumn>

                            <asp:TemplateColumn SortExpression="StandKM" HeaderText="Aktual KM">
								<HeaderStyle CssClass="titleTableService"></HeaderStyle>
								<ItemStyle HorizontalAlign="Center"></ItemStyle>
								<ItemTemplate>
									<asp:Label id="lblActualKM" runat="server"></asp:Label>
								</ItemTemplate>
							</asp:TemplateColumn>
                            <asp:TemplateColumn SortExpression="MSPRegistrationHistory.MSPMaster.MSPKm" HeaderText="Package KM">
								<HeaderStyle CssClass="titleTableService"></HeaderStyle>
								<ItemStyle HorizontalAlign="Center"></ItemStyle>
								<ItemTemplate>
									<asp:Label id="lblPackageKM" runat="server"></asp:Label>
								</ItemTemplate>
							</asp:TemplateColumn>

                             <asp:TemplateColumn SortExpression="MSPRegistrationHistory.MSPRegistration.ChassisMaster.EndCustomer.OpenFakturDate" HeaderText="Tgl Penjualan">
								<HeaderStyle CssClass="titleTableService"></HeaderStyle>
								<ItemStyle HorizontalAlign="Center"></ItemStyle>
								<ItemTemplate>
									<asp:Label id="lblPKTDate" runat="server"></asp:Label>
								</ItemTemplate>
							</asp:TemplateColumn>

                            <asp:TemplateColumn SortExpression="ClaimDate" HeaderText="Tgl Claim">
								<HeaderStyle CssClass="titleTableService"></HeaderStyle>
								<ItemStyle HorizontalAlign="Center"></ItemStyle>
								<ItemTemplate>
									<asp:Label id="lblClaimDate" runat="server"></asp:Label>
								</ItemTemplate>
							</asp:TemplateColumn>

                            <asp:TemplateColumn HeaderText="Total Claim + PPN (10%)">
								<HeaderStyle CssClass="titleTableService"></HeaderStyle>
								<ItemStyle HorizontalAlign="Center"></ItemStyle>
								<ItemTemplate>
									<asp:Label id="lblClaimDocumentID" runat="server" visible="false"></asp:Label>
									<asp:Label id="lblTotalClaimPPn" runat="server"></asp:Label>
								</ItemTemplate>
							</asp:TemplateColumn>

                            <asp:TemplateColumn HeaderText="No Kwitansi">
								<HeaderStyle CssClass="titleTableService"></HeaderStyle>
								<ItemStyle HorizontalAlign="Center"></ItemStyle>
								<ItemTemplate>
									<asp:label id="lblNoKwitansi" runat="server"></asp:label>
								</ItemTemplate>
							</asp:TemplateColumn>

                             <%--<asp:TemplateColumn HeaderText="No Letter">
								<HeaderStyle CssClass="titleTableService"></HeaderStyle>
								<ItemStyle HorizontalAlign="Center"></ItemStyle>
								<ItemTemplate>
									<asp:label id="lblNoLetter" runat="server"></asp:label>
								</ItemTemplate>
							</asp:TemplateColumn>--%>

                           <asp:TemplateColumn HeaderText="Download Kuitansi">
								<HeaderStyle CssClass="titleTableService"></HeaderStyle>
								<ItemStyle HorizontalAlign="Center"></ItemStyle>
								<ItemTemplate>
									 <asp:LinkButton visible="false" id="lbtnDownloadKuitansi" runat="server" CommandName="DownloadKuitansi">
										<img src="../images/download.gif" border="0" alt="Download Kuitansi">
                                    </asp:LinkButton>
								</ItemTemplate>
							</asp:TemplateColumn>

                            <%--<asp:TemplateColumn HeaderText="Download Letter">
								<HeaderStyle CssClass="titleTableService"></HeaderStyle>
								<ItemStyle HorizontalAlign="Center"></ItemStyle>
								<ItemTemplate>
									 <asp:LinkButton Visible="false" id="lbtnDownloadLetter" runat="server" CommandName="DownloadLetter">
										<img src="../images/download.gif" border="0" alt="Download Letter">
                                    </asp:LinkButton>
								</ItemTemplate>
							</asp:TemplateColumn>--%>
                           
                        </Columns>
                        <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
                    </asp:datagrid>
                </div>
            </td>
        </tr>
        
    </table>
    </form>
</body>
</html>

