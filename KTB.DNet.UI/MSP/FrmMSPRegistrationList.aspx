<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmMSPRegistrationList.aspx.vb" Inherits=".FrmMSPRegistrationList"  smartNavigation="False"%>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
    <head>
		<title>Daftar Registrasi Konsumen</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript" src="../WebResources/FormFunctions.js"></script>
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

            function InputPasswordPlease() {
                showPPPassword();
            }

            function showPPPassword() {
                showPopUp('../PopUp/PopupInputPassword.aspx', '', 200, 600, GotPassword);
            }
            function GotPassword(result) {
                var txtUser = document.getElementById("txtUser");
                var txtPwd = document.getElementById("txtPass");
                var btn = document.getElementById("btnTransfertoSAP");
                var str = result;
                var username = '', pwd = '';

                username = str.split(';')[0];
                pwd = str.split(';')[1];

                txtUser.value = username;
                txtPwd.value = pwd;
                btn.click();
            }

            function promptPassword() {
                var txt = document.getElementById("txtPass");
                var div = document.getElementById("divPassword");

                if (txt.value)

                    div.style.display = "inherit";
                alert("Please, Enter Your SAP Password First!")
                txt.focus();
            }
            function onLoad() {
                var div = document.getElementById("divPassword");
                div.style.display = "none";
            }

		</script>
	</head>
<body MS_POSITIONING="GridLayout">
    <form id="Form1" method="post" runat="server">
    <table id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
        <tr>
			<td class="titlePage">MSP - Daftar Registrasi Konsumen</td>
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
							<asp:textbox id="txtKodeDealer" runat="server"></asp:textbox>
							<asp:label id="lblSearchDealer" runat="server" onclick="ShowPPDealerSelection();">
								<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup">
							</asp:label>
						</td>
						<td width="17%"  class="titleField">
                            <asp:Label ID="lblRequestType" runat="server">Tipe Pengajuan</asp:Label>
						</td>
						<td width="1%">:</td>
						<td width="32%">
                            <asp:DropDownList runat="server" ID="ddlRequestType"></asp:DropDownList>
						</td>
					</tr>
                    <tr>
						<td class="titleField" style="HEIGHT: 14px" width="20%">
                            <asp:label id="lblMSPNo" runat="server">No MSP</asp:label>
						</td>
						<td style="HEIGHT: 14px" width="1%">:</td>
						<td style="HEIGHT: 14px" width="29%">
							<asp:textbox id="txtMSPNo" runat="server"></asp:textbox>
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
                            <asp:label id="lblChassisNumber" runat="server">No Rangka</asp:label>
						</td>
						<td style="HEIGHT: 14px" width="1%">:</td>
						<td style="HEIGHT: 14px" width="29%">
							<asp:textbox id="txtChassisNumber" runat="server"></asp:textbox>
						</td>
						<td width="17%"  class="titleField">
                            <asp:Label ID="lblStatus" runat="server">Status</asp:Label>
						</td>
						<td width="1%">:</td>
						<td width="32%">
                            <asp:ListBox ID="lboxStatus" runat="server" Width="140px" Rows="3" SelectionMode="Multiple"></asp:ListBox>
						</td>
					</tr>
                     <tr>
						<td class="titleField" style="HEIGHT: 14px" width="20%">
                            <asp:label id="lblCategory" runat="server">Kategori</asp:label>
						</td>
						<td style="HEIGHT: 14px" width="1%">:</td>
						<td style="HEIGHT: 14px" width="29%">
							<asp:DropDownList runat="server" ID="ddlCategory">
                                <asp:ListItem Value="blank" Selected="True">Silahkan Pilih</asp:ListItem>
							    <asp:ListItem Value="PAID">Paid</asp:ListItem>
							    <asp:ListItem Value="PROMO">Promo</asp:ListItem>
							</asp:DropDownList>
						</td>
						<td width="17%"  class="titleField">
                            <asp:checkbox id="chkRequestDate" Runat="server"></asp:checkbox>
                            <asp:Label ID="Label2" runat="server">Tgl Pengajuan</asp:Label>
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
                    <tr>
                        <td class="titleField" style="HEIGHT: 14px" width="20%">
                            <asp:label id="Label1" runat="server">Kategori</asp:label>
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
							<asp:button id="btnSearch" runat="server" Text="Cari" width="70px"></asp:button>
						</td>
					</tr>
                </table>
            </td>
        </tr>
        <tr>
            <td vAlign="top">
				<div id="div1" style="OVERFLOW: auto">
                    <asp:datagrid id="dtgMSPRegistrationList" runat="server" Width="100%" CellSpacing="1" AllowCustomPaging="true" AllowSorting="True" PageSize="50" AllowPaging="True" GridLines="Vertical" CellPadding="3" BackColor="#CDCDCD" BorderWidth="0px" BorderStyle="None" BorderColor="#E7E7FF" AutoGenerateColumns="False" ShowFooter="false">
						<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
						<AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
						<ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
						<HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
						<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
						<Columns>
                            <asp:TemplateColumn HeaderText="Check">
                                <HeaderStyle Width="2%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <HeaderTemplate>
                                    <input id="chkAllItems" onclick="CheckAll('chkSelect', document.forms[0].chkAllItems.checked)"
                                        type="checkbox">
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkSelect" runat="server"></asp:CheckBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="No">
								<HeaderStyle Width="3%" CssClass="titleTableService"></HeaderStyle>
								<ItemStyle HorizontalAlign="Center"></ItemStyle>
								<ItemTemplate>
									<asp:Label id="lblMSPRegistrationHistoryID" runat="server" Visible="false"></asp:Label>
									<asp:Label id="lblNo" runat="server"></asp:Label>
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn HeaderText="Status">
								<HeaderStyle CssClass="titleTableService"></HeaderStyle>
								<ItemStyle HorizontalAlign="Center"></ItemStyle>
								<ItemTemplate>
									<asp:Label id="lblStatus" runat="server"></asp:Label>
								</ItemTemplate>
							</asp:TemplateColumn>
                            <asp:TemplateColumn SortExpression="Dealer.DealerCode" HeaderText="Dealer">
								<HeaderStyle CssClass="titleTableService"></HeaderStyle>
								<ItemStyle HorizontalAlign="Center"></ItemStyle>
								<ItemTemplate>
									<asp:Label id="lblDealer" runat="server"></asp:Label>
								</ItemTemplate>
							</asp:TemplateColumn>
                           <asp:TemplateColumn SortExpression="MSPCode" HeaderText="No MSP">
								<HeaderStyle CssClass="titleTableService"></HeaderStyle>
								<ItemStyle HorizontalAlign="Center"></ItemStyle>
								<ItemTemplate>
									<asp:Label id="lblMSPCode" runat="server"></asp:Label>
								</ItemTemplate>
							</asp:TemplateColumn>
                           <asp:TemplateColumn SortExpression="ChassisMaster.ChassisNumber" HeaderText="No Rangka">
								<HeaderStyle CssClass="titleTableService"></HeaderStyle>
								<ItemStyle HorizontalAlign="Center"></ItemStyle>
								<ItemTemplate>
									<asp:Label id="lblChassisNumber" runat="server"></asp:Label>
								</ItemTemplate>
							</asp:TemplateColumn>

                              <asp:TemplateColumn SortExpression="ChassisMaster.VechileColor.VechileType.Description" HeaderText="Nama Kendaraan">
								<HeaderStyle CssClass="titleTableService"></HeaderStyle>
								<ItemStyle HorizontalAlign="Center"></ItemStyle>
								<ItemTemplate>
									<asp:Label id="lblVehicleDescription" runat="server"></asp:Label>
								</ItemTemplate>
							</asp:TemplateColumn>

                            <asp:TemplateColumn HeaderText="Kategory">
								<HeaderStyle CssClass="titleTableService"></HeaderStyle>
								<ItemStyle HorizontalAlign="Center"></ItemStyle>
								<ItemTemplate>
									<asp:Label id="lblCategory" runat="server"></asp:Label>
								</ItemTemplate>
							</asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Tipe Pengajuan">
								<HeaderStyle CssClass="titleTableService"></HeaderStyle>
								<ItemStyle HorizontalAlign="Center"></ItemStyle>
								<ItemTemplate>
									<asp:Label id="lblRequestType" runat="server"></asp:Label>
								</ItemTemplate>
							</asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Tipe MSP">
								<HeaderStyle CssClass="titleTableService"></HeaderStyle>
								<ItemStyle HorizontalAlign="Center"></ItemStyle>
								<ItemTemplate>
									<asp:Label id="lblMSPType" runat="server"></asp:Label>
								</ItemTemplate>
							</asp:TemplateColumn>
                             <asp:TemplateColumn HeaderText="Tgl Pengajuan">
								<HeaderStyle CssClass="titleTableService"></HeaderStyle>
								<ItemStyle HorizontalAlign="Center"></ItemStyle>
								<ItemTemplate>
									<asp:Label id="lblRequestDate" runat="server"></asp:Label>
								</ItemTemplate>
							</asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Sertifikat">
								<HeaderStyle CssClass="titleTableService"></HeaderStyle>
								<ItemStyle HorizontalAlign="Center"></ItemStyle>
								<ItemTemplate>
									<asp:label id="lblDownload" runat="server" visible="false">
										<img src="../images/download.gif" style="cursor:hand" border="0" alt="Cetak Sertifikat">
                                    </asp:label>
								</ItemTemplate>
							</asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Status Claim">
								<HeaderStyle CssClass="titleTableService"></HeaderStyle>
								<ItemStyle HorizontalAlign="Left"></ItemStyle>
								<ItemTemplate>
									<asp:Label id="lblClaimStatus" runat="server"></asp:Label>
								</ItemTemplate>
							</asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <HeaderStyle CssClass="titleTableService"></HeaderStyle>
                                <FooterStyle HorizontalAlign="Left"></FooterStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>  
                                    <div style="width:115px;align-content:center">
                                        <asp:LinkButton id="lbtnUpgrade" runat="server" Width="20px" Text="Upgrade" CausesValidation="False" CommandName="Upgrade" visible="false">
										    <img src="../images/green.gif" border="0" alt="Upgrade">
                                        </asp:LinkButton>
                                        <asp:LinkButton id="lbtnView" runat="server" Width="20px" Text="Lihat" CausesValidation="False" CommandName="view" visible="false">
										    <img src="../images/detail.gif" border="0" alt="Lihat">
                                        </asp:LinkButton>
									    <asp:LinkButton id="lbtnEdit" runat="server" Width="20px" Text="Ubah" CausesValidation="False" CommandName="Edit" visible="false">
										    <img src="../images/edit.gif" border="0" alt="Ubah">
									    </asp:LinkButton>
									    <asp:LinkButton id="lbtnDelete" runat="server" Width="16px" Text="Hapus" CausesValidation="False" CommandName="Delete" visible="false">
										    <img src="../images/trash.gif" border="0" alt="Hapus">
									    </asp:LinkButton>
                                        
                                        <asp:Label ID="lblHistoryStatus" runat="server">
												<img src="../images/popup.gif" style="cursor:hand" alt="Perubahan Status"></asp:Label>

                                        <asp:LinkButton id="lbtnHistory" runat="server" Width="16px" Text="History" CausesValidation="False" CommandName="History" visible="false">
										    <img src="../images/alur_flow.gif" border="0" alt="History">
									    </asp:LinkButton>
                                    </div>
                                </ItemTemplate>
                                            
                            </asp:TemplateColumn>
                        </Columns>
                        <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
                    </asp:datagrid>
                </div>
            </td>
        </tr>
        <tr>
			<td align="left">
                <br />
                <div id="divPassword" style="display:none;">
                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                        <tr>
                            <td>SAP Password</td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="txtUser" runat="server"   Width="171px"></asp:TextBox>
                                <asp:TextBox ID="txtPass" runat="server" TextMode="Password"  Width="171px"></asp:TextBox>
                            </td>
                        </tr>

                    </table>
                </div>
				<table id="tblOperator" cellspacing="1" cellpadding="1" border="0" runat="server">
                    <tr>
                        <td>
                            <asp:Label ID="lblChangeStatus" runat="server" Visible="false">Mengubah Status :</asp:Label></td>
                        <td>
                            <asp:DropDownList ID="ddlProses" runat="server" Visible="false">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Button ID="btnProses" runat="server" Text="Proses" Visible="false"></asp:Button></td>
                        <td>
                            <asp:Button ID="btnDownload" runat="server" Text="Download" ></asp:Button>
                        </td>
                        <td><asp:button id="btnTransfertoSAP" runat="server" Width="130px" Text="Transfer to SAP" Height="24px" Visible="false"></asp:button></td>
                        <td><%--<asp:button id="btnTransferUlangtoSAP" runat="server" Width="130px" Text="Transer Ulang to SAP" Height="24px" Visible="false"></asp:button>--%></td>
                    </tr>
                </table>
			</td>
		</tr>
    </table>
    </form>
</body>
</html>
