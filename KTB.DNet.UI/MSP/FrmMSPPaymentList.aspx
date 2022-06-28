<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmMSPPaymentList.aspx.vb" Inherits=".FrmMSPPaymentList" smartNavigation="False" %>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
    <head>
		<title>Daftar Pembayaran MSP</title>
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
			<td class="titlePage">MSP - Daftar Pembayaran</td>
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
                            <asp:label id="lblDealer" runat="server">Credit Account</asp:label>
						</td>
						<td style="HEIGHT: 14px" width="1%">:</td>
						<td style="HEIGHT: 14px" width="29%">
							<asp:textbox id="txtCreditAccount" runat="server"></asp:textbox>
						</td>
						<td width="17%">
                            
						</td>
						<td width="1%"></td>
						<td width="32%">
                            
						</td>
					</tr>
                    <tr>
                        <td class="titleField" style="HEIGHT: 14px" width="20%">
                            <asp:label id="Label6" runat="server">Dealer</asp:label>
						</td>
						<td style="HEIGHT: 14px" width="1%">:</td>
						<td style="HEIGHT: 14px" width="29%">
							<asp:label ID="lblDealerName" runat="server" Visible="false"></asp:label>
                            <asp:textbox id="txtKodeDealer" runat="server" Visible="true"></asp:textbox>
							<asp:label id="lblSearchDealer" runat="server" onclick="ShowPPDealerSelection();" Visible ="true">
								<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup">
							</asp:label>
						</td>
                    </tr>
                    <tr>
						<td class="titleField" style="HEIGHT: 14px" width="20%">
                            <asp:label id="Label1" runat="server">No Registrasi Pembayaran</asp:label>
						</td>
						<td style="HEIGHT: 14px" width="1%">:</td>
						<td style="HEIGHT: 14px" width="29%">
							<asp:textbox id="txtPaymentRegNo" runat="server"></asp:textbox>
						</td>
						<td width="17%">
                            
						</td>
						<td width="1%"></td>
						<td width="32%">
                            
						</td>
					</tr>
                    
                     <tr>
						<td class="titleField" style="HEIGHT: 14px" width="20%">
                            <asp:checkbox id="chkRequestDate" Runat="server"></asp:checkbox>
                            <asp:Label ID="Label2" runat="server">Tgl Transfer</asp:Label>
						</td>
						<td style="HEIGHT: 14px" width="1%">:</td>
						<td style="HEIGHT: 14px" width="29%">
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
						<td width="17%">
                            
						</td>
						<td width="1%"></td>
						<td width="32%">
                            
						</td>
					</tr>
                    <tr>
						<td class="titleField" style="HEIGHT: 14px" width="20%">
                            <asp:label id="Label4" runat="server">Status</asp:label>
						</td>
						<td style="HEIGHT: 14px" width="1%">:</td>
						<td style="HEIGHT: 14px" width="29%">
							<asp:ListBox ID="lboxStatus" runat="server" Width="140px" Rows="3" SelectionMode="Multiple"></asp:ListBox>
						</td>
						<td width="17%">
                            
						</td>
						<td width="1%"></td>
						<td width="32%">
                            
						</td>
					</tr>

                    <tr>
						<td class="titleField" style="HEIGHT: 14px" width="20%">
                            <asp:label id="Label3" runat="server">No Debit Charge</asp:label>
						</td>
						<td style="HEIGHT: 14px" width="1%">:</td>
						<td style="HEIGHT: 14px" width="29%">
							<asp:textbox id="txtNoDebitCharge" runat="server"></asp:textbox>
						</td>
						<td width="17%">
                            
						</td>
						<td width="1%"></td>
						<td width="32%">
                            
						</td>
					</tr>

                    <tr>
						<td class="titleField" style="HEIGHT: 14px" width="20%">
                            <asp:label id="Label5" runat="server">No Debit Memo</asp:label>
						</td>
						<td style="HEIGHT: 14px" width="1%">:</td>
						<td style="HEIGHT: 14px" width="29%">
							<asp:textbox id="txtNoDebitMemo" runat="server"></asp:textbox>
						</td>
						<td width="17%">
                            
						</td>
						<td width="1%"></td>
						<td width="32%">
                            
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
                    <asp:datagrid id="dtgMSPPaymentList" runat="server" Width="100%" CellSpacing="1" AllowSorting="True" PageSize="25" AllowCustomPaging="True" AllowPaging="True" GridLines="Vertical" CellPadding="3" BackColor="#CDCDCD" BorderWidth="0px" BorderStyle="None" BorderColor="#E7E7FF" AutoGenerateColumns="False" ShowFooter="False">
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
									<asp:Label id="lblMSPPaymentID" runat="server" Visible="false"></asp:Label>
                                    <asp:Label id="lblMSPPaymentDtlID" runat="server" Visible="false"></asp:Label>
									<asp:Label id="lblNo" runat="server"></asp:Label>
								</ItemTemplate>
							</asp:TemplateColumn>

							<asp:TemplateColumn SortExpression="Dealer.CreditAccount" HeaderText="Credit Account">
								<HeaderStyle CssClass="titleTableService"></HeaderStyle>
								<ItemStyle HorizontalAlign="Center"></ItemStyle>
								<ItemTemplate>
									<asp:Label id="lblCreditAccount" runat="server"></asp:Label>
								</ItemTemplate>
							</asp:TemplateColumn>
                            <asp:TemplateColumn SortExpression="Dealer.DealerCode" HeaderText="Kode Dealer">
								<HeaderStyle CssClass="titleTableService"></HeaderStyle>
								<ItemStyle HorizontalAlign="Center"></ItemStyle>
								<ItemTemplate>
									<asp:Label id="lblDealerCode" runat="server"></asp:Label>
								</ItemTemplate>
							</asp:TemplateColumn>

                            <asp:TemplateColumn SortExpression="PlanTransferDate" HeaderText="Plan Tgl Transfer">
								<HeaderStyle CssClass="titleTableService"></HeaderStyle>
								<ItemStyle HorizontalAlign="Center"></ItemStyle>
								<ItemTemplate>
									<asp:Label id="lblTransferDate" runat="server"></asp:Label>
								</ItemTemplate>
							</asp:TemplateColumn>
                           <asp:TemplateColumn HeaderText="Tujuan Pembayaran">
								<HeaderStyle CssClass="titleTableService"></HeaderStyle>
								<ItemStyle HorizontalAlign="Center"></ItemStyle>
								<ItemTemplate>
									<asp:Label id="lblPaymentPurpose" runat="server"></asp:Label>
								</ItemTemplate>
							</asp:TemplateColumn>
                           <asp:TemplateColumn SortExpression="RegNumber" HeaderText="No Registrasi Pembayaran">
								<HeaderStyle CssClass="titleTableService"></HeaderStyle>
								<ItemStyle HorizontalAlign="Center"></ItemStyle>
								<ItemTemplate>
									<asp:Label id="lblPaymentRegNo" runat="server"></asp:Label>
								</ItemTemplate>
							</asp:TemplateColumn>
                            <asp:TemplateColumn SortExpression="TotalAmount" HeaderText="Total Amount">
								<HeaderStyle CssClass="titleTableService"></HeaderStyle>
								<ItemStyle HorizontalAlign="Center"></ItemStyle>
								<ItemTemplate>
									<asp:Label id="lblTotalAmount" runat="server"></asp:Label>
								</ItemTemplate>
							</asp:TemplateColumn>

                            <asp:TemplateColumn HeaderText="No Debit Charge">
                                <HeaderStyle CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblDebitChargeNo" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>

                            <asp:TemplateColumn HeaderText="No Debit Memo">
                                <HeaderStyle CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblDebitMemoNo" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>

                            <asp:TemplateColumn SortExpression="ActualTransferDate" HeaderText="Tgl Aktual Transfer">
								<HeaderStyle CssClass="titleTableService"></HeaderStyle>
								<ItemStyle HorizontalAlign="Center"></ItemStyle>
								<ItemTemplate>
									<asp:Label id="lblActualTransferDate" runat="server"></asp:Label>
								</ItemTemplate>
							</asp:TemplateColumn>
                            <asp:TemplateColumn SortExpression="TotalActualAmount" HeaderText="Total Aktual Amount">
								<HeaderStyle CssClass="titleTableService"></HeaderStyle>
								<ItemStyle HorizontalAlign="Center"></ItemStyle>
								<ItemTemplate>
									<asp:Label id="lblTotalActualAmount" runat="server"></asp:Label>
								</ItemTemplate>
							</asp:TemplateColumn>
                             <asp:TemplateColumn SortExpression="Status" HeaderText="Status">
								<HeaderStyle CssClass="titleTableService"></HeaderStyle>
								<ItemStyle HorizontalAlign="Center"></ItemStyle>
								<ItemTemplate>
									<asp:Label id="lblStatus" runat="server"></asp:Label>
								</ItemTemplate>
							</asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <HeaderStyle CssClass="titleTableService"></HeaderStyle>
                                <FooterStyle HorizontalAlign="Left"></FooterStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>  
                                    <div style="width:115px;align-content:center">
                                        
                                        <asp:LinkButton id="lbtnView" runat="server" Width="20px" Text="Lihat" CausesValidation="False" CommandName="view" visible="false">
										    <img src="../images/detail.gif" border="0" alt="Lihat">
                                        </asp:LinkButton>
									    <asp:LinkButton id="lbtnEdit" runat="server" Width="20px" Text="Ubah" CausesValidation="False" CommandName="Edit" visible="false">
										    <img src="../images/edit.gif" border="0" alt="Ubah">
									    </asp:LinkButton>
									    <asp:LinkButton id="lbtnDelete" runat="server" Width="16px" Text="Hapus" CausesValidation="False" CommandName="Delete" visible="false">
										    <img src="../images/trash.gif" border="0" alt="Hapus">
									    </asp:LinkButton>
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
                            <asp:Button ID="btnDownload" runat="server" Text="Download" Visible="false"></asp:Button>
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
