<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmReleaseApproval.aspx.vb" Inherits="FrmReleaseApproval" smartNavigation="False"%>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
    <HEAD>
        <title>FrmAplikasiHeader</title>
        <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
        <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
        <meta content="JavaScript" name="vs_defaultClientScript">
        <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
        <LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
        <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
        <base target="_self">
        <script language="javascript" src="../WebResources/InputValidation.js"></script>
        <script language="javascript">
			/* Deddy H	validasi value *********************************** */
			/* ini untuk handle char yg tdk diperbolehkan, saat paste */
			function TxtBlur(objTxt)
			{
				omitSomeCharacter(objTxt,'<>?*%$;');
			}
			/* ini untuk handle char yg tdk diperbolehkan, saat keypress */
			function TxtKeypress()
			{
				return alphaNumericExcept(event,'<>?*%$;')
			}

			//function js untuk handle alphanumeric, dengan menghilangkan karakter numeric
			function alphaNumericNonNumeric(event)
			{	
				if(navigator.appName == "Microsoft Internet Explorer")	
					pressedKey = event.keyCode;
				else
					pressedKey = event.which
				
				if ((pressedKey == 32) || (pressedKey >=97 && pressedKey <=122) || (pressedKey >=65 && pressedKey <=90))
				{
					return true;
				}
				else
				{	
					return false;
				}
			}
			
			function TxtBlurNonNumeric(objTxt)
			{
				omitSomeCharacter(objTxt,'<>?*%$;0123456789');
			}
			
			function DealerSelection(selectedDealer)
			{
				var txtDealerCodeSelection = document.getElementById("txtDealerCode");
				txtDealerCodeSelection.value =selectedDealer;
			}
			function BabitProposalSelection(selectedBabitProposal)
			{
				var arrValue= selectedBabitProposal.split('@');
				var txtNoPersetujuanSelection = document.getElementById("txtNoPersetujuan");
				// just pick No Persetujuan
				txtNoPersetujuanSelection.value =arrValue[1];
			}
			function SetICPersetujuan(Parameter)
			{	
				__doPostBack("PersetujuanDate","");
			}
			
			function SetICPaymentDate(Parameter)
			{	
				__doPostBack("PaymentDate","");
			}
			
        </script>
    </HEAD>
    <body MS_POSITIONING="GridLayout">
        <form id="Form1" method="post" runat="server">
            <TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
                <tr>
                    <td class="titlePage" style="HEIGHT: 17px">BABIT - Pembayaran</td>
                </tr>
                <tr>
                    <td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
                </tr>
                <tr>
                    <td style="HEIGHT: 6px" height="6"><IMG height="1" src="../images/dot.gif" border="0"></td>
                </tr>
                <TR>
                    <TD>
                        <TABLE id="Table1" cellSpacing="1" cellPadding="2" width="100%" border="0">
                            <TBODY>
                                <TR>
                                    <TD class="titleField" style="WIDTH: 151px; HEIGHT: 10px" width="151">Kode Dealer</TD>
                                    <TD style="HEIGHT: 10px" width="1%">:</TD>
                                    <TD colSpan="2">
                                        <asp:textbox onkeypress="return HtmlCharUniv(event)" id="txtDealerCode" runat="server" Width="128px"
                                            ToolTip="Dealer Search 1"></asp:textbox>
                                        <asp:label id="lblPopUpDealer" runat="server" width="16px">
                                            <img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0"></asp:label></TD>
                                    <TD style="HEIGHT: 10px" width="1%"></TD>
                                    <TD style="HEIGHT: 10px" width="29%"></TD>
                                </TR>
                                <TR>
                                    <TD class="titleField" style="WIDTH: 151px; HEIGHT: 10px" width="151">Tgl 
                                        Persetujuan</TD>
                                    <TD style="HEIGHT: 10px" width="1%"><asp:label id="Label3" runat="server">:</asp:label></TD>
                                    <TD><asp:checkbox id="chk01" runat="server" AutoPostBack="True"></asp:checkbox></TD>
                                    <TD><cc1:inticalendar id="ICPersetujuan" runat="server" TextBoxWidth="70"></cc1:inticalendar></TD>
                                    <TD style="HEIGHT: 10px" width="1%"></TD>
                                    <TD style="HEIGHT: 10px" width="29%"></TD>
                                </TR>
                                <TR>
                                    <TD class="titleField" style="WIDTH: 151px; HEIGHT: 20px" width="151">Nomor 
                                        Pembayaran</TD>
                                    <TD style="HEIGHT: 20px" width="1%"><asp:label id="Label2" runat="server">:</asp:label></TD>
                                    <TD class="titleField" width="161" colSpan="2"><asp:textbox onkeypress="return HtmlCharUniv(event)" id="txtNomorPembayaran" runat="server" MaxLength="25"
                                            Width="152px" ToolTip="Dealer Search 1"></asp:textbox></TD>
                                    <TD style="HEIGHT: 20px" width="1%"></TD>
                                    <TD style="HEIGHT: 20px" width="29%"></TD>
                                </TR>
                                <TR>
                                    <TD class="titleField" style="WIDTH: 151px; HEIGHT: 11px">Tgl Pembayaran</TD>
                                    <TD style="HEIGHT: 11px"><asp:label id="Label5" runat="server">:</asp:label></TD>
                                    <TD><asp:checkbox id="chk02" runat="server" AutoPostBack="True"></asp:checkbox></TD>
                                    <TD class="titleField"><cc1:inticalendar id="ICPaymentDate" runat="server" TextBoxWidth="70"></cc1:inticalendar></TD>
                                    <TD style="HEIGHT: 11px"></TD>
                                    <TD style="HEIGHT: 11px"></TD>
                                </TR>
                                <TR>
                                    <TD class="titleField" style="WIDTH: 151px; HEIGHT: 11px">Invoice Dealer</TD>
                                    <TD style="HEIGHT: 11px"><asp:label id="Label6" runat="server">:</asp:label></TD>
                                    <TD colSpan="2"><asp:textbox onkeypress="return HtmlCharUniv(event)" id="txtDealerInvoice" runat="server" MaxLength="10"
                                            Width="128px"></asp:textbox></TD>
                                    <TD style="HEIGHT: 11px"></TD>
                                    <TD style="HEIGHT: 11px"></TD>
                                </TR>
                                <TR>
                                    <TD class="titleField" style="WIDTH: 151px; HEIGHT: 11px">Nomor Persetujuan</TD>
                                    <TD style="HEIGHT: 11px"><asp:label id="Label7" runat="server">:</asp:label></TD>
                                    <TD colSpan="2"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$')" id="txtNoPersetujuan" onblur="omitSomeCharacter('txtNoPersetujuan','<>?*%$')"
                                            runat="server" Width="128px"></asp:textbox><asp:label id="lblPopUpBabitProposal" runat="server" width="16px">
                                            <img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0"></asp:label></TD>
                                    <TD style="HEIGHT: 11px"></TD>
                                    <TD style="HEIGHT: 11px"></TD>
                                </TR>
                                <TR>
                                    <TD class="titleField" style="WIDTH: 151px; HEIGHT: 11px">Status Pembayaran</TD>
                                    <TD style="HEIGHT: 11px"><asp:label id="Label8" runat="server">:</asp:label></TD>
                                    <TD colSpan="2"><asp:dropdownlist id="ddlReleaseStatus" runat="server" Width="128px"></asp:dropdownlist></TD>
                                    <TD style="HEIGHT: 11px"></TD>
                                    <TD style="HEIGHT: 11px"></TD>
                                </TR>
                                <TR>
                                    <TD class="titleField" style="WIDTH: 151px; HEIGHT: 11px"></TD>
                                    <TD style="HEIGHT: 11px"></TD>
                                    <TD class="titleField" colSpan="2"><asp:button id="btnSearch" runat="server" Width="60px" Text="Cari"></asp:button><asp:button id="btnBatal" runat="server" Width="60px" Text="Batal" CausesValidation="False"></asp:button></TD>
                                    <TD style="HEIGHT: 11px"></TD>
                                    <TD style="HEIGHT: 11px"></TD>
                                </TR>
                                <TR>
                                    <TD vAlign="top" colSpan="6">
                                        <div id="div1" style="OVERFLOW: auto; HEIGHT: 200px"><asp:datagrid id="dgBabitProposal" runat="server" Width="100%" AllowPaging="True" PageSize="25"
                                                AllowCustomPaging="True" AllowSorting="True" AutoGenerateColumns="False" BackColor="#CDCDCD" BorderColor="Gainsboro" CellSpacing="1" BorderWidth="0px"
                                                CellPadding="3">
                                                <AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
                                                <ItemStyle BackColor="White"></ItemStyle>
                                                <HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Blue"></HeaderStyle>
                                                <Columns>
                                                    <asp:BoundColumn Visible="False" DataField="ID" SortExpression="ID" HeaderText="ID"></asp:BoundColumn>
                                                    <asp:TemplateColumn HeaderText="Pilih">
                                                        <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkPilih" Runat="server"></asp:CheckBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="No">
                                                        <HeaderStyle Width="2%" CssClass="titleTablePromo"></HeaderStyle>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="Kode Dealer">
                                                        <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDealerCode" Runat="server"></asp:Label>
                                                            <asp:LinkButton ID="lbtnTmp" Runat="server"></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:label ID="lblEditDealerCode" Runat="server"></asp:label>
                                                        </EditItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="No Pembayaran">
                                                        <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblNomorPembayaran" Runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:label ID="lblEditNomorPembayaran" Runat="server"></asp:label>
                                                        </EditItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="Nomor Persetujuan">
                                                        <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblNoPersetujuan" Runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:label ID="lblEditNoPersetujuan" Runat="server"></asp:label>
                                                        </EditItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="Invoice Dealer">
                                                        <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDealerInvoice" Runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtEditDealerInvoice" Runat="server"></asp:TextBox>
                                                        </EditItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="Tgl Proses Invoice">
                                                        <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPaymentDate" Runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <cc1:IntiCalendar ID="icEditPaymentDate" runat="server" TextBoxWidth="70"></cc1:IntiCalendar>
                                                        </EditItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="Jml Tagihan">
                                                        <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblKTBApprovalAmount" Runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:Label ID="lblEditKTBApprovalAmount" Runat="server"></asp:Label>
                                                        </EditItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn>
                                                        <HeaderStyle Width="10%" CssClass="titleTablePromo"></HeaderStyle>
                                                        <ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
                                                        <ItemTemplate>
                                                            <asp:LinkButton id="lbtnEdit" runat="server" Width="20px" Text="Ubah" CausesValidation="False" CommandName="Edit">
                                                                <img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
                                                            <asp:LinkButton Runat="server" ID="lnkDetail" CommandName="detail" ></asp:LinkButton>
                                                            <asp:LinkButton ID="lnkReject" Runat="server" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>' CommandName="Reject">
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:LinkButton id="lbtnSave" tabIndex="40" CommandName="Save" text="Simpan" Runat="server" CausesValidation="False">
                                                                <img src="../images/simpan.gif" border="0" alt="Simpan"></asp:LinkButton>
                                                            <asp:LinkButton id="lbtnCancel" tabIndex="50" CommandName="Cancel" text="Batal" Runat="server" CausesValidation="False">
                                                                <img src="../images/batal.gif" border="0" alt="Batal"></asp:LinkButton>
                                                        </EditItemTemplate>
                                                    </asp:TemplateColumn>
                                                </Columns>
                                                <PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
                                            </asp:datagrid></div>
                                    </TD>
                                </TR>
                            </TBODY>
                        </TABLE>
                        <asp:button id="btnSimpan" runat="server" Width="60px" Text="Simpan"></asp:button><asp:button id="btnGenerateNo" runat="server" Width="160px" Text="Generate No Pembayaran"></asp:button></TD>
                </TR>
            </TABLE>
        </form>
    </body>
</HTML>
