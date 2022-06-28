<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>

<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmAllocIndentByOrder.aspx.vb" Inherits="FrmAllocIndentByOrder" SmartNavigation="False" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>FrmAllocIndentByOrder</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script language="javascript">
        function ShowPPDealerSelection() {
            showPopUp('../SparePart/../PopUp/PopUpDealerSelection.aspx', '', 500, 760, DealerSelection);
        }
        function DealerSelection(selectedDealer) {
            //var tempParam = selectedDealer.split(';');
            var txtDealer = document.getElementById("txtDealerCode");
            txtDealer.value = selectedDealer;
        }
        function ShowPPSparePartSelection() {
            var ddlMaterialType = document.getElementById("ddlMaterialType");
            if (ddlMaterialType.value == '0') {
                alert('Pilih Tipe Barang Dulu');
                return;
            }

            showPopUp('../SparePart/../PopUp/PopUpSparePartSelection.aspx?IPMaterialtype=' + ddlMaterialType.value, '', 500, 760, SparePartSelection);
        }
        function SparePartSelection(selected) {
            //var tempParam = selected.split(';');
            var txtSparePart = document.getElementById("txtSparePartName");
            txtSparePart.value = selected;// tempParam[0];				
        }

        function ShowPPIndent() {
            showPopUp('../Indent/../PopUp/PopUpIndentPart.aspx?', '', 500, 760, PONOSelection);
        }

        function PONOSelection(selectedIndent) {
            //var tempParam = selectedDealer.split(';');
            var txtNoPO = document.getElementById("txtNoPO");
            txtNoPO.value = selectedIndent;
        }

        //function CheckAll(aspCheckBoxID, checkVal) 
        //{
        //	re = new RegExp(':' + aspCheckBoxID + '$')  
        //	for(i = 0; i < document.forms[0].elements.length; i++) {
        //		elm = document.forms[0].elements[i]
        //		if (elm.type == 'checkbox') {
        //			if (re.test(elm.name)) {
        //			elm.checked = checkVal
        //			}
        //		}
        //	}
        //}

        function NumOnlyBlurWithOnGridTxtCustom(txtclientid, lblremainqtyclientid, lblorderqtyclientid, hdnremainqtyclientid) {
            var txtalocqty = document.getElementById(txtclientid);
            var lblRemainQty = document.getElementById(lblremainqtyclientid);
            var lblOrderQty = document.getElementById(lblorderqtyclientid);
            var hdnRemainQty = document.getElementById(hdnremainqtyclientid);
            var alocqty = parseFloat(txtalocqty.value);
            var orderqty = parseFloat(lblOrderQty.innerText);
            var hdnremainqty = parseFloat(hdnRemainQty.value);
            var remainqty = hdnremainqty - alocqty;
            if (remainqty < 0) {
                alert('Alokasi Qty Tidak Bisa Lebih Besar Dari Order Qty');
                txtalocqty.focus();
                txtalocqty.value = hdnremainqty;
                return;
            }
            //document.getElementById(lblremainqtyclientid).innerText = parseFloat(remainqty);
        }
    </script>
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <table id="Table01" cellspacing="1" cellpadding="2" width="100%" border="0">
            <tr>
                <td colspan="3">
                    <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td class="titlePage" colspan="6">
                                <asp:Label runat="server" ID="lblTitle" Text="INDENT PART - Alokasi Indent Part"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td background="../images/bg_hor.gif" colspan="6">
                                <img height="1" src="../images/bg_hor.gif" border="0"></td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <img height="10" src="../images/blank.gif" border="0"></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <%--<TR>
					<TD class="titleField" width="15%">Kode Dealer</TD>
					<TD width="1%">:</TD>
					<td width="50%"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$')" id="txtDealerCode" onblur="omitSomeCharacter('txtDealerCode','<>?*%$')"
							runat="server" Width="160px" TextMode="MultiLine" Height="42px"></asp:textbox><asp:label id="lblSearchDealer" runat="server">
							<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:label></td>
				</TR>--%>
            <tr>
                <td class="titleField" width="15%" valign="top">Nomor Pengajuan</td>
                <td width="1%" valign="top">:</td>
                <td width="50%" valign="top">
                    <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$')" ID="txtNoPO" onblur="omitSomeCharacter('txtNoPO','<>?*%$')"
                        runat="server" Width="160px"></asp:TextBox><asp:Label ID="lblPopUpPengajuan" runat="server">
							<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:Label><br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNoPO" ErrorMessage="No Pengajuan harus diisi"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <%--<TR>
					<TD class="titleField" style="HEIGHT: 13px" vAlign="top" width="15%">Tanggal 
						Pengajuan</TD>
					<TD style="HEIGHT: 13px" vAlign="top" width="1%">:</TD>
					<td style="HEIGHT: 13px" width="50%">
						<table id="Table2" cellSpacing="0" cellPadding="0" border="0">
							<tr>
								<td><cc1:inticalendar id="icPODateFrom" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
								<td>&nbsp;s/d&nbsp;</td>
								<td><cc1:inticalendar id="icPODateUntil" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
							</tr>
						</table>
					</td>
				</TR>
				<TR style="display:none">
					<TD class="titleField" vAlign="top" width="15%">Tipe Barang</TD>
					<TD vAlign="top" width="1%">:</TD>
					<td width="50%"><asp:dropdownlist id="ddlMaterialType" runat="server" Width="160px"></asp:dropdownlist></td>
				</TR>
				<TR style="display:none">
					<TD class="titleField" vAlign="top" width="15%">Nomor Barang</TD>
					<TD vAlign="top" width="1%">:</TD>
					<td width="50%"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$')" id="txtSparePartName" onblur="omitSomeCharacter('txtSparePartName','<>?*%$')"
							runat="server" Width="264px" Rows="2" TextMode="MultiLine"></asp:textbox><asp:label id="lblSearchProduk" runat="server">
							<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:label></td>
				</TR>--%>
            <tr>
                <td class="titleField" width="15%"></td>
                <td width="1%"></td>
                <td width="50%">
                    <asp:Button ID="btnSearch" runat="server" Width="60px" Text="Cari"></asp:Button></td>
            </tr>
            <tr>
                <td valign="top" colspan="3">
                    <div id="div1" style="height: 240px; overflow: auto">
                        <asp:DataGrid ID="dtgIndentPart" runat="server" Width="100%" PageSize="25" GridLines="None" CellPadding="3"
                            BackColor="#CDCDCD" AllowCustomPaging="True" AllowPaging="True" BorderWidth="0px" BorderStyle="None"
                            BorderColor="#CDCDCD" AutoGenerateColumns="False" CellSpacing="1" AllowSorting="True">
                            <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                            <AlternatingItemStyle ForeColor="Black" BackColor="White"></AlternatingItemStyle>
                            <ItemStyle ForeColor="Black" VerticalAlign="Top" BackColor="#FDF1F2"></ItemStyle>
                            <HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#CC3333"></HeaderStyle>
                            <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
                            <Columns>
                                <asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
                                    <HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
                                </asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="No.">
                                    <HeaderStyle Width="5%" CssClass="titleTableParts"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:TemplateColumn>
                                <%--<asp:TemplateColumn>
										<HeaderStyle CssClass="titleTableParts"></HeaderStyle>
										<HeaderTemplate>
											<input id="chkAllItems" type="checkbox" onclick="CheckAll('chkPO',
															document.forms[0].chkAllItems.checked)" />
										</HeaderTemplate>
										<ItemTemplate>
											<asp:CheckBox ID="chkPO" Runat="server"></asp:CheckBox>
										</ItemTemplate>
									</asp:TemplateColumn>--%>
                                <asp:TemplateColumn SortExpression="IndentPartHeader.Dealer.DealerCode" HeaderText="Kode Dealer">
                                    <HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" Text='<%# DataBinder.Eval(Container, "DataItem.IndentPartHeader.Dealer.DealerCode") %>' runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="SparePartMaster.PartNumber" HeaderText="Nomor Barang">
                                    <HeaderStyle Width="15%" CssClass="titleTableParts"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="Label2" Text='<%# DataBinder.Eval(Container, "DataItem.SparePartMaster.PartNumber") %>' runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="SparePartMaster.PartName" HeaderText="Nama Barang">
                                    <HeaderStyle Width="30%" CssClass="titleTableParts"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblPartName" Text='<%# DataBinder.Eval(Container, "DataItem.SparePartMaster.PartName") %>'>Label</asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="SparePartMaster.ModelCode" HeaderText="Model">
                                    <HeaderStyle Width="8%" CssClass="titleTableParts"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblModel" Text='<%# DataBinder.Eval(Container, "DataItem.SparePartMaster.ModelCode") %>'>Label</asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Remain Qty">
                                    <HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblRemainQty" CssClass="textRight" Text='<%# DataBinder.Eval(Container, "DataItem.SisaQty") %>'>
                                        </asp:Label>
                                        <input type="hidden" id="hdnRemainQty" runat="server" name="hdnRemainQty" value='<%# DataBinder.Eval(Container, "DataItem.SisaQty") %>' />
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Order Qty">
                                    <HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblOrderQty" runat="server" CssClass="textRight" Text='<%# DataBinder.Eval(Container, "DataItem.Qty") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="AllocationQty" HeaderText="Alokasi Qty">
                                    <HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblQtyAllocation" runat="server" CssClass="textRight" Text='<%# DataBinder.Eval(Container, "DataItem.AllocationQty") %>'>
                                        </asp:Label>

                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="IndentPartHeader.RequestNo" HeaderText="Nomor Pengajuan">
                                    <HeaderStyle Width="8%" CssClass="titleTableParts"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="Label3" Text='<%# DataBinder.Eval(Container, "DataItem.IndentPartHeader.RequestNo") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="IndentPartHeader.RequestDate" HeaderText="Tanggal Pengajuan">
                                    <HeaderStyle Width="8%" CssClass="titleTableParts"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="Label4" Text='<%# format(DataBinder.Eval(Container, "DataItem.IndentPartHeader.RequestDate"),"dd MMM yyyy") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>

                                <asp:TemplateColumn SortExpression="IndentPartHeader.TermOfPayment.ID" HeaderText="Cara Pembayaran" Visible="False">
                                    <HeaderStyle CssClass="titleTableParts"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="LabelTOP" runat="server" Text='<%# IIf(CType(DataBinder.Eval(Container, "DataItem.IndentPartHeader.TermOfPayment.Description"), String) = "", "", CType(DataBinder.Eval(Container, "DataItem.IndentPartHeader.TermOfPayment.Description"), String))%>'>Label</asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn Visible="False" HeaderText="Nomor PO">
                                    <HeaderStyle Width="8%" CssClass="titleTableParts"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblPO" runat="server" Text=""></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn Visible="False">
                                    <HeaderStyle Width="15%" CssClass="titleTableParts"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnEdit" runat="server" Text="Ubah" CausesValidation="False" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>' 
                                            CommandName="Edit" ToolTip="Ubah">
												<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:LinkButton ID="lbtnCancel" TabIndex="50" CommandName="Cancel" runat="server" Text="Batal" ToolTip="Batal">
												<img src="../images/batal.gif" border="0" alt="Batal"></asp:LinkButton>
                                    </EditItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                            <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
                        </asp:DataGrid>
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="3"></td>
            </tr>
            <tr>
                <td align="center" colspan="3">
                    <%--<asp:button id="Button1" runat="server" Text="Simpan"></asp:button>--%>
                    <asp:Button ID="btnGeneratePO" runat="server" Text="Generate PO"></asp:Button>
                    <asp:Button ID="btnDownload" runat="server" Text="Download"></asp:Button>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
