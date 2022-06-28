<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmDaftarInvoiceRevisionPayment.aspx.vb" Inherits=".FrmDaftarInvoiceRevisionPayment" %>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<html>
<head>
    <title>REVISI FAKTUR - DAFTAR PEMBAYARAN</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
	<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
	<meta content="JavaScript" name="vs_defaultClientScript">
	<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	<link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">    
	<script language="javascript" src="../WebResources/PreventNewWindow.js" type="text/javascript"></script>
	<script language="javascript" src="../WebResources/InputValidation.js" type="text/javascript"></script>
	<script language="javascript" src="../WebResources/FormFunctions.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">

        function ShowPPCreditAccountSelection(dealerGroupId) {
            if (dealerGroupId == 'All')
            {
                showPopUp('../General/../PopUp/PopUpCreditAccountSelection.aspx', '', 500, 760, CreditAccountSelection);
            }
            else {
                showPopUp('../General/../PopUp/PopUpCreditAccountSelection.aspx?Group=' + dealerGroupId, '', 500, 760, CreditAccountSelection);
            }
        }
        function CreditAccountSelection(selectedCreditAccount) {
            var temp = selectedCreditAccount.split(';');
            var txtCreditAccountSelection = document.getElementById("txtCreditAccount");
            txtCreditAccountSelection.value = temp[0];
        }

        function ShowPPDealerSelection() {
            var txtCreditAccountSelection = document.getElementById("txtCreditAccount");
            var creditAccount = txtCreditAccountSelection.value
            showPopUp('../General/../PopUp/PopUpDealerSelection.aspx?creditAccount=' + creditAccount, '', 500, 760, DealerSelection);
        }
        function DealerSelection(selectedDealer) {
            //var temp = selectedDealer.split(';');
            var txtDealerSelection = document.getElementById("txtKodeDealer");
            //txtDealerSelection.value = temp[0];
            txtDealerSelection.value = selectedDealer;
        }

        function isSuccesUpload(getSuccesUpload) {
            if (getSuccesUpload = '1')
            {
                var btn = document.getElementById('btnSuccessUpload');
                btn.click();
            }
        }

    </script>
</head>
<body ms_positioning="GridLayout" onload="javascript:window.history.forward(1);">
    <form id="form1" method="post" runat="server">
        <table id="table1" cellSpacing="1" cellPadding="1" width="100%">
            <tr>
				<td>
					<table id="table2" cellspacing="0" cellpadding="0" width="100%" border="0">
						<tr>
							<td class="titlePage" style="height: 17px">REVISI FAKTUR - DAFTAR PEMBAYARAN</td>
						</tr>
						<tr>
							<td background="../images/bg_hor.gif" height="1"><img height="1" alt="" src="../images/bg_hor.gif" border="0"></td>
						</tr>
						<tr>
							<td style="height: 6px" height="6"><img height="1" alt="" src="../images/dot.gif" border="0"></td>
						</tr>
					</table>
				</td>
			</tr>
            <tr>
				<td>
					<table id="Table3" cellspacing="1" cellpadding="2" width="100%">
						<tr valign="top">
							<td class="titleField" style="height: 11px" width="10%">
								<asp:label id="Label6" runat="server">Credit Account</asp:label><br /><br /><br />
                                <asp:label id="Label10" runat="server">Kode Dealer</asp:label>
							</td>
							<td width="1%">
								<asp:label id="Label9" runat="server">:</asp:label><br /><br /><br />
                                <asp:label id="Label13" runat="server">:</asp:label>
							</td>
							<td width="24%">
								<asp:TextBox ID="txtCreditAccount" onkeypress="return alphaNumericExcept(event,'<>?*%$')" onblur="omitSomeCharacter('txtCreditAccount','<>?*%$;')" runat="server"></asp:TextBox>
                                <asp:Label ID="lblSearchCreditAccount" runat="server">
									<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup">
                                </asp:Label><br /><br />
                                <asp:TextBox ID="txtKodeDealer" onkeypress="return alphaNumericExcept(event,'<>?*%$')" onblur="omitSomeCharacter('txtKodeDealer','<>?*%$;')" runat="server"></asp:TextBox>
                                <asp:Label ID="lblSearchDealer" runat="server">
									<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup">
                                </asp:Label>
							</td>
							
							<td class="titleField" style="height: 11px" width="10%">
								<asp:Label ID="Label11" runat="server">Status Pembayaran</asp:Label></td>
							<td style="height: 11px" width="1%">
								<asp:Label ID="Label12" runat="server">:</asp:Label></td>
							<td style="height: 11px">
                                <asp:ListBox ID="lboxPaymentRevisionStatus" runat="server" Width="150px" Rows="5"></asp:ListBox>
							</td>
						</tr>						
                        <tr>
							<td class="titleField" style="height: 11px" width="10%">
								<asp:label id="Label2" runat="server">No Reg Pembayaran</asp:label></td>
							<td width="1%">
								<asp:label id="Label3" runat="server">:</asp:label></td>
							<td width="24%">
								<asp:TextBox ID="txtRegNumberPayment" runat="server" Width="150px" onkeypress="return alphaNumericExcept(event,'<>?*%$;{},+=');"
									ToolTip="No Registrasi Pembayaran"></asp:TextBox>
							</td>

							<td class="titleField" style="height: 11px" width="10%">
								<asp:label id="Label1" runat="server">No Reg Revisi Faktur</asp:label></td>
							<td width="1%">
								<asp:label id="Label14" runat="server">:</asp:label></td>
							<td width="24%">
								<asp:TextBox ID="txtRegNumberRevision" runat="server" Width="150px" onkeypress="return alphaNumericExcept(event,'<>?*%$;{},+=');"
									ToolTip="No Registrasi Revisi Faktur"></asp:TextBox>
							</td>
						</tr>
						<tr>
							<td class="titleField" style="height: 11px" width="10%">
								<asp:Label ID="Label55" runat="server">Jenis Pembayaran</asp:Label></td>
							<td width="1%">
								<asp:label id="Label5" runat="server">:</asp:label></td>
							<td width="24%">
                                <asp:dropdownlist id="ddlPaymentType" runat="server" Width="140px" AutoPostBack="true"></asp:dropdownlist></td>
                            <td class="titleField" style="height: 11px" width="10%">
                                <asp:Label ID="Label15" runat="server">Nomor Rangka</asp:Label></td>
                            <td>
								<asp:Label ID="Label8" runat="server">:</asp:Label></td>
							<td width="34%" nowrap>
								<asp:TextBox ID="txtChassisNo" onkeypress="return alphaNumericExcept(event,'<>?*%$')" onblur="omitSomeCharacter('txtChassisNo','<>?*%$;')"
									runat="server" Width="150px" size="22" MaxLength="20" ToolTip="Nomor Rangka Revisi Faktur"></asp:TextBox>
							</td>
						</tr>
						<tr>
							<td class="titleField">
								<asp:Label ID="Label16" runat="server">Bukti Pembayaran</asp:Label>
							</td>
							<td width="1%" style="height: 11px">
								<asp:Label ID="Label4" runat="server">:</asp:Label></td>
							 <td width="24%">
								<asp:dropdownlist id="ddlPaymentProof" runat="server" Width="140px" AutoPostBack="true"></asp:dropdownlist></td>
							<td class="titleField">Kategori</td>
							<td>:</td>
							<td><asp:DropDownList ID="ddlKategori" runat="server" Width="90px" ></asp:DropDownList></td>
						</tr>
						<tr>                           
							<td class="titleField"></td>
							<td></td>
							<td style="text-align: right;">
								<asp:Button ID="btnSearch" runat="server" Width="60px" Text="Cari"></asp:Button></td>
							<td class="titleField"></td>
							<td></td>
							<td></td>
						</tr>
                        <tr>
                            <td class="titleField">
                                <asp:Label ID="lblJumRecord" runat="server"></asp:Label></td>
                            <td></td>
                            <td></td>
                            <td class="titleField"></td>
                            <td></td>
                            <td></td>
                        </tr>

                        <tr>
                            <td valign="top" colspan="6">
                                <div id="divInvoiceRevisionPaymentList" style="overflow: auto; height: 480px">
                                    <asp:DataGrid ID="dgInvoiceRevisionPaymentList" runat="server" Width="100%" AllowSorting="True" CellPadding="3"
                                        BorderWidth="0px" CellSpacing="1" BorderColor="Gainsboro" BackColor="#CDCDCD" AutoGenerateColumns="False" PageSize="15" AllowPaging="True"
                                        AllowCustomPaging="True" DataKeyField="ID">
                                        <AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
                                        <ItemStyle BackColor="White"></ItemStyle>
                                        <HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Blue"></HeaderStyle>
                                        <Columns>
                                            <asp:BoundColumn Visible="False" DataField="ID" ReadOnly="True" HeaderText="ID">
                                                <HeaderStyle CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            </asp:BoundColumn>
                                            <asp:TemplateColumn HeaderText="Check">
                                                <HeaderStyle Width="2%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <HeaderTemplate>
                                                    <input id="chkAllItems" onclick="CheckAllDataGridCheckBoxes('chkItemChecked', document.all.chkAllItems.checked)"
                                                        type="checkbox">
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkItemChecked" runat="server"></asp:CheckBox>
                                                    <asp:HiddenField ID="hdnID" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.ID")%>' />
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="No">
                                                <HeaderStyle Width="2%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNo" runat="server" Text=""></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn SortExpression="Status" HeaderText="Status Bayar">
                                                <HeaderStyle Width="2%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hdnPaymentStatusID" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.Status")%>' />
                                                    <asp:Label ID="lblPaymentStatus" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn SortExpression="Dealer.DealerCode" HeaderText="Kode Dealer">
                                                <HeaderStyle Width="2%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDealerCode" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerCode")%>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn SortExpression="RegNumber" HeaderText="No Reg. Pembayaran">
                                                <HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label  ID="lblPaymentRegNumber" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.RegNumber")%>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn SortExpression="PaymentType" HeaderText="Jenis Pembayaran">
                                                <HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label  ID="lblPaymentType" runat="server" >
                                                    </asp:Label>
                                                    <asp:HiddenField ID="hdnPaymentType" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.PaymentType")%>' />
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn SortExpression="CreatedTime" HeaderText="Tgl Validasi Pembayaran">
                                                <HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPaymentCreatedTime" runat="server" Text='<%# Format(DataBinder.Eval(Container, "DataItem.CreatedTime"),"dd/MM/yyyy") %>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Nama Bank" Visible="false">
                                                <HeaderStyle Width="20%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblBankName">
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="No Giro" Visible="false">
                                                <HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblGyroNo">
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn SortExpression="GyroDate" HeaderText="Tgl Rencana Transfer">
                                                <HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblGyroDate" Text='<%# Format(DataBinder.Eval(Container, "DataItem.GyroDate"),"dd/MM/yyyy") %>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn SortExpression="ActualPaymentDate" HeaderText="Tgl Actual">
                                                <HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblActualDate" Text='<%# Format(DataBinder.Eval(Container, "DataItem.ActualPaymentDate"), "dd/MM/yyyy")%>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn SortExpression="TotalAmount" HeaderText="Jumlah Tagihan">
                                                <HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblTotalAmount">
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Amount Actual">
                                                <HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblAmountActual">
                                                    </asp:Label>
                                                    <asp:HiddenField ID="hdnEvidencePath" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.EvidencePath")%>' />
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Selisih Tagihan">
                                                <HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblSelisihTagihan">
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Amount Kliring" Visible="false">
                                                <HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblAmountKliring">
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Nomor TR" Visible="false">
                                                <HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblJVNo">
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Bukti Bayar">
                                                <HeaderStyle Width="2%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label id="lnkUploadBukti" runat="server">
															<img src="../images/edit.gif" style="cursor:hand" border="0" alt="Upload Bukti"></asp:Label>
                                                    <asp:LinkButton ID="lnkDownloadBukti" runat="server" CommandName="lnkDownloadBukti" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.EvidencePath")%>'>
															<img src="../images/download.gif" border="0" style="cursor:hand" alt="Download Bukti">
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Details">
                                                <HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkEdit" runat="server" CommandName="EditInvoiceRevisionPayment" 
                                                            ToolTip="Edit Detail">
															<img src="../images/edit.gif" border="0" style="cursor:hand"></asp:LinkButton>
                                                    <asp:LinkButton ID="lnkDetail" runat="server" CommandName="lnkDetail"
                                                        ToolTip="View Detail">
															<img src="../images/detail.gif" border="0" style="cursor:hand" alt="Lihat detil">
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>                  
                                        </Columns>
                                        <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
                                    </asp:DataGrid>
                                </div>
                            </td>
                        </tr>
					</table>
                    
                    <asp:Button ID="btnValidasi" OnClientClick="return confirm('Validasi Data ?')"  runat="server" Width="60px" Text="Validasi"></asp:Button>&nbsp;&nbsp;
                    <asp:Button ID="btnKonfirmasi" OnClientClick="return confirm('Konfirmasi Data ?')" runat="server" Width="60px" Text="Konfirmasi"></asp:Button>&nbsp;&nbsp;
                    <asp:Button ID="btnTransfer" OnClientClick="return confirm('Transfer Data ?')" runat="server" Width="60px" Text="Transfer"></asp:Button>&nbsp;&nbsp;
                    <asp:Button ID="btnTransferUlang" OnClientClick="return confirm('Transfer Ulang Data ?')" runat="server" Width="90px" Text="Transfer Ulang"></asp:Button>&nbsp;&nbsp;
                    <asp:Button ID="btnSuccessUpload" runat="server" Width="1px" Text="" style="display:none"></asp:Button>
				</td>
			</tr>
             <tr>
                <td style="height: 8px"></td>
            </tr>
        </table>
    </form>
</body>
</html>
