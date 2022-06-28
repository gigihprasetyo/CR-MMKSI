<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmInputBabitEventReport.aspx.vb" Inherits="FrmInputBabitEventReport" SmartNavigation="False"  MaintainScrollPositionOnPostback="true" %>

<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>FrmPengajuanBabit</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script language="javascript">
        function ShowPPDealerSelectionGab(DealerGroupID, DealerCode) {
            var obj = document.getElementById("lblSearchDealer")
            var disabled = obj.getAttribute("disabled")
            if (disabled == false || disabled == null) {
                showPopUp('../General/../PopUp/PopUpDealerSelection.aspx?Group=' + DealerGroupID + '&Dealer=' + DealerCode, '', 500, 760, DealerSelectionGab);
            }
        }
        function DealerSelectionGab(selectedDealer) {
            var txtDealerSelection = document.getElementById("txtCollaborateDealer");
            var data = selectedDealer.split(";");
            var selectedDealer2 = '';
            for (i = 0; i < data.length; i++) {
                var n = txtDealerSelection.value.indexOf(data[i]);
                if (n == -1) {
                    if (selectedDealer2 == '') {
                        selectedDealer2 = data[i];
                    }
                    else {
                        selectedDealer2 += ';' + data[i];
                    }
                }
            }

            if (selectedDealer2 != '') {
                if (txtDealerSelection.value == '') {
                    txtDealerSelection.value = selectedDealer2;
                }
                else {
                    txtDealerSelection.value += ';' + selectedDealer2;
                }
            }
        }

        function btnGetInfoEventProposalClick() {
            var btnGetInfoEventProposal = document.getElementById("btnGetInfoEventProposal");
            btnGetInfoEventProposal.click();
        }
        function ShowPPDealerSelection() {
            showPopUp('../PopUp/PopUpDealerSelectionOne.aspx', '', 500, 760, DealerSelection);
        }

        function DealerSelection(selectedDealer) {
            var data = selectedDealer.split(";");
            var txtDealerCodeSelection = document.getElementById("txtDealerCode");
            txtDealerCodeSelection.value = data[0];
            var lblDealerCodeName = document.getElementById("lblDealerCodeName");
            lblDealerCodeName.innerHTML = data[0] + ' / ' + data[1];
            var btnGetInfoDealer = document.getElementById("btnGetInfoDealer");
            btnGetInfoDealer.click();

            //if (navigator.appName == "Microsoft Internet Explorer") {
            //    txtDealerCodeSelection.focus();
            //    txtDealerCodeSelection.blur();
            //}
            //else {
            //    txtDealerCodeSelection.onchange();
            //}
        }

        function ShowPPDealerBranchSelection() {
            var txtDealerSelection = document.getElementById("lblDealerCodeName");
            var data = txtDealerSelection.innerHTML.split(" / ");
            showPopUp('../Babit/../PopUp/PopUpDealerBranchSelectionSingle.aspx?m=d&deal=' + data[0], '', 500, 760, TemporaryOutlet);
        }

        function TemporaryOutlet(selectedDealerBranch) {
            var data = selectedDealerBranch.split(";");
            var txtTOCodeSelection = document.getElementById("txtTOCode");
            var lblTOName = document.getElementById("lblTOName");
            txtTOCodeSelection.value = data[0]
            var lblTOCodeName = document.getElementById("lblTOCodeName");
            lblTOCodeName.innerHTML = data[0] + ' / ' + data[1];
            lblTOName.innerHTML = data[1];

            var hdntxtTOCode = document.getElementById("hdntxtTOCode");
            var hdnlblTOName = document.getElementById("hdnlblTOName");
            hdntxtTOCode.value = data[0];
            hdnlblTOName.value = data[1];

            var btnGetInfoDealerBranch = document.getElementById("btnGetInfoDealerBranch");
            btnGetInfoDealerBranch.click();

            //if (navigator.appName == "Microsoft Internet Explorer") {
            //    txtTOCodeSelection.focus();
            //    txtTOCodeSelection.blur();
            //}
            //else {
            //    txtTOCodeSelection.onchange();
            //}
        }

        function ShowPPEventProposalSelection() {
            showPopUp('../PopUp/PopUpEventProposalSelectionOne.aspx', '', 500, 760, EventProposalSelection);
        }

        function EventProposalSelection(selectedEvent) {
            var data = selectedEvent.split(";");
            var txtEventRegNumber = document.getElementById("txtEventRegNumber");
            var hdnEventDealerHeaderID = document.getElementById("hdnEventDealerHeaderID");
            var hdnBabitEventProposalHeaderID = document.getElementById("hdnBabitEventProposalHeaderID");
            hdnBabitEventProposalHeaderID.value = data[0];
            hdnEventDealerHeaderID.value = data[1];
            txtEventRegNumber.value = data[2];
            btnGetInfoEventProposalClick()
            //if (navigator.appName == "Microsoft Internet Explorer") {
            //    txtEventRegNumber.focus();
            //    txtEventRegNumber.blur();
            //}
            //else {
            //    txtEventRegNumber.onchange();
            //}
        }

        function trim(str) {
            return str.replace(/^\s+|\s+$/g, "");
        }

    </script>

</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <input id="hdnBabitEventReportHeaderID" type="hidden" value="0" runat="server">

        <table id="Table2" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage">EVENT - INPUT LAPORAN EVENT</td>
            </tr>
            <tr>
                <td background="../images/bg_hor.gif" height="1" colspan="2">
                    <img height="1" src="../images/bg_hor.gif" border="0"></td>
            </tr>
            <tr>
                <td height="10" colspan="2">
                    <img height="1" src="../images/dot.gif" border="0"></td>
            </tr>
            <tr valign="top">
                <td style="width: 50%">
                    <table id="Table1" cellspacing="1" cellpadding="2" width="100%" border="0">
                        <tr>
                            <td class="titleField" style="width: 146px">Kode Dealer</td>
                            <td style="width: 2px">:</td>
                            <td>
                                <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$')" ID="txtDealerCode" onblur="omitSomeCharacter('txtDealerCode','<>?*%$')"
                                    Style="display: none" runat="server" ToolTip="Dealer Search 1" Width="128px" ReadOnly="true">
                                </asp:TextBox>
                                <asp:Label ID="lblPopUpDealer" runat="server" Width="16px">
                                        <img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0">
                                </asp:Label>&nbsp;<asp:Label ID="lblDealerCodeName" runat="server"></asp:Label>
                                <asp:Button ID="btnGetInfoDealer" runat="server" Text="..." Style="display: none"></asp:Button>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 146px">Kode Temporary Outlet</td>
                            <td style="width: 2px">:</td>
                            <td class="titleField">
                                <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$')" ID="txtTOCode" ReadOnly="true"
                                    onblur="omitSomeCharacter('txtTOCode','<>?*%$')" runat="server" ToolTip="TO Search 1" Width="128px" AutoPostBack="true"></asp:TextBox>
                                <asp:Label ID="lblPopUpTO" runat="server" Width="16px">
                                        <img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0">
                                </asp:Label>&nbsp;
                                   <asp:Label ID="lblTOCodeName" Style="display: none" runat="server"></asp:Label>
                                <input id="hdntxtTOCode" type="hidden" value="" runat="server">
                                <asp:Button ID="btnGetInfoDealerBranch" runat="server" Text="..." Style="display: none"></asp:Button>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 146px">Nama Temporary Outlet</td>
                            <td style="width: 2px">:</td>
                            <td class="titleField">
                                <asp:Label ID="lblTOName" runat="server"></asp:Label>
                                <input id="hdnlblTOName" type="hidden" value="" runat="server">
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 146px">Nomor Reg Proposal</td>
                            <td style="width: 2px">:</td>
                            <td class="titleField">
                                <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$')" ID="txtEventRegNumber" 
                                    onblur="omitSomeCharacter('txtEventRegNumber','<>?*%$');if (this.value != ''){btnGetInfoEventProposalClick();}"
                                    runat="server" ToolTip="Event Proposal Search 1" Width="128px" ReadOnly="true">
                                </asp:TextBox>
                                <asp:Label ID="lblPopUpEventProposal" runat="server" Width="16px">
                                        <img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0">
                                </asp:Label>
                                <input id="hdnEventDealerHeaderID" type="hidden" value="" runat="server">
                                <input id="hdnBabitEventProposalHeaderID" type="hidden" value="" runat="server">
                                <asp:Button ID="btnGetInfoEventProposal" runat="server" Style="display: none"></asp:Button>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 146px">Nama Proposal</td>
                            <td style="width: 2px">:</td>
                            <td class="titleField">
                                <asp:Label ID="lblEventProposalName" runat="server"></asp:Label>
                                <input id="hdnEventProposalName" type="hidden" value="" runat="server">
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 146px">Nama Laporan Event</td>
                            <td style="width: 2px">:</td>
                            <td>
                                <asp:TextBox ID="txtEventReportName" runat="server" Width="250px"/></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 146px">Tanggal Pelaksanaan</td>
                            <td style="width: 2px">:</td>
                            <td class="titleField">
                                <table cellspacing="0" cellpadding="0" border="0">
                                    <tr>
                                        <td>
                                            <cc1:IntiCalendar ID="icPeriodStart" runat="server" TextBoxWidth="70"></cc1:IntiCalendar>
                                            <asp:Label ID="lblPeriodStart" runat="server"></asp:Label></td>
                                        <td>&nbsp;s.d&nbsp;</td>
                                        <td>
                                            <cc1:IntiCalendar ID="icPeriodEnd" runat="server" TextBoxWidth="70"></cc1:IntiCalendar>
                                            <asp:Label ID="lblPeriodEnd" runat="server"></asp:Label></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 146px">Kode Dealer Gabungan</td>
                            <td style="width: 2px">:</td>
                            <td class="titleField">
                                <asp:textbox id="txtCollaborateDealer" runat="server" Width="200px" MaxLength="5000"  
										        TextMode="MultiLine"></asp:textbox>
								<asp:label id="lblSearchDealer" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup">
									</asp:label>
                            </td>
                        </tr>
                     </table>
                </td>
                <td style="width: 50%" valign="top">
                    <table id="Table3" cellspacing="1" cellpadding="2" width="100%" border="0">
                        <tr>
                            <td class="titleField" style="width: 146px">Nama Lokasi</td>
                            <td style="width: 2px">:</td>
                            <td class="titleField">
                                <asp:TextBox ID="txtLocationName" runat="server" Width="178px"/></td>
                        </tr>
                        <tr id="trProvinsi">
                            <td class="titleField">
                                <asp:Label ID="lblProvinsi" runat="server">Provinsi</asp:Label>
                            </td>
                            <td>:</td>
                            <td>
                                <asp:DropDownList ID="ddlProvinsi" runat="server" Width="140px" AutoPostBack="true"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField">
                                <asp:Label ID="lblKota" runat="server">Kota/Kab</asp:Label>
                            </td>
                            <td>:</td>
                            <td>
                                <asp:DropDownList ID="ddlKota" runat="server" Width="140px"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 146px">Jumlah Undangan</td>
                            <td style="width: 2px">:</td>
                            <td valign="middle">
                                <asp:TextBox ID="txtInvitationQty" Style="text-align: right" runat="server" 
                                    onkeypress="return NumericOnlyWith(event,'');" onkeyup="pic(this,this.value,'9999999999','N')" Width="60px" />
                                &nbsp;<label>Undangan</label>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 146px">Jumlah Kehadiran</td>
                            <td style="width: 2px">:</td>
                            <td>
                                <asp:TextBox ID="txtAttendeeQty" Style="text-align: right" runat="server" 
                                    onkeypress="return NumericOnlyWith(event,'');" onkeyup="pic(this,this.value,'9999999999','N')" Width="60px" />
                                &nbsp;<label>Hadir</label>
                            </td>
                        </tr>
                        <tr runat="server" id="TR_Jml_Subsidi">
                            <td class="titleField" style="width: 146px">Jumlah Subsidi</td>
                            <td style="width: 2px">:</td>
                            <td class="titleField">
                                <asp:TextBox ID="txtSubsidyAmount" Style="text-align: right" runat="server" onkeypress="return NumericOnlyWith(event,'');" onkeyup="pic(this,this.value,'9999999999','N')" Width="90px" />
                            </td>
                        </tr>
                        <tr runat="server" id="TR_ApprovedBudget">
                            <td class="titleField" style="width: 146px">Subsidi Akhir</td>
                            <td style="width: 2px">:</td>
                            <td class="titleField">
                                <asp:TextBox ID="txtApprovedBudget" Enabled="false" Style="text-align: right" runat="server" onkeypress="return NumericOnlyWith(event,'');" onkeyup="pic(this,this.value,'9999999999','N')" Width="90px" />
                            </td>
                        </tr>
                        <tr runat="server" id="TR_CatatanMKS" valign="top">
                            <td class="titleField" style="width: 146px">Catatan dari MMKSI</td>
                            <td style="width: 2px">:</td>
                            <td class="titleField">
                                <asp:TextBox ID="txtNotesMMKSI" TabIndex="0" runat="server" Width="360px" TextMode="MultiLine" Enabled="false" Height="90px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>

        <div>
            <hr />
        </div>
        <div class="titleField" style="padding-right: 2px; padding-left: 2px; padding-bottom: 2px; width: 75%; padding-top: 2px; text-align: Left">
            <asp:Label ID="Label2" runat="server" Text="Biaya" Font-Size="15px" Font-Bold="True">Rincian Kegiatan</asp:Label>
        </div>
        <div style="width: 75%; overflow: scroll">
            <asp:DataGrid ID="dgBabitEventReport" runat="server" Width="100%" BorderWidth="0px" BackColor="#CDCDCD" BorderColor="Gainsboro" CellPadding="3" CellSpacing="1" AutoGenerateColumns="False" ShowFooter="True" PageSize="1000000">
                <AlternatingItemStyle ForeColor="Black" BackColor="#FDF1F2"></AlternatingItemStyle>
                <ItemStyle ForeColor="Black" BackColor="#FFFFFF"></ItemStyle>
                <HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C" HorizontalAlign="Center"></HeaderStyle>
                <FooterStyle ForeColor="#4A3C8C" BackColor="#dedede"></FooterStyle>
                <Columns>
                    <asp:TemplateColumn HeaderText="No.">
                        <HeaderStyle Width="10px" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        <ItemTemplate>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Kategori">
                        <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle Width="15%" HorizontalAlign="Left"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label
                                ID="lblCategoryBabitEvent" runat="server" Text='' Font-Bold="true">
                            </asp:Label>
                        </ItemTemplate>
                        <FooterStyle Wrap="False" HorizontalAlign="Left"></FooterStyle>
                        <FooterTemplate>
                            <asp:DropDownList ID="ddlFCategoryBabitEvent" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlFCategoryBabitEvent_SelectedIndexChanged">
                            </asp:DropDownList>
                        </FooterTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlECategoryBabitEvent" runat="server" AutoPostBack="false">
                            </asp:DropDownList>
                        </EditItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Jenis">
                        <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblJenisBabitEvent" runat="server" Text=''>
                            </asp:Label>
                        </ItemTemplate>
                        <FooterStyle Wrap="False" HorizontalAlign="Left"></FooterStyle>
                        <FooterTemplate>
                            <asp:DropDownList ID="ddlFJenisBabitEvent" runat="server" AutoPostBack="false">
                            </asp:DropDownList>
                        </FooterTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlEJenisBabitEvent" runat="server" AutoPostBack="false">
                            </asp:DropDownList>
                        </EditItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Deskripsi">
                        <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblDescription" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Description")%>'>
                            </asp:Label>
                        </ItemTemplate>
                        <FooterStyle HorizontalAlign="Center"></FooterStyle>
                        <FooterTemplate>
                            <asp:TextBox ID="txtFDesc" runat="server" Width="460px" />
                        </FooterTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEDesc" runat="server" Width="460px" Text='<%#DataBinder.Eval(Container, "DataItem.Description")%>' />
                        </EditItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Qty">
                        <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblQty" runat="server">
                            </asp:Label>
                        </ItemTemplate>
                        <FooterStyle Wrap="False" HorizontalAlign="Right"></FooterStyle>
                        <FooterTemplate>
                            <asp:TextBox ID="txtFQty" Style="text-align: right" runat="server" onkeypress="return NumericOnlyWith(event,'');" 
                                onkeyup="pic(this,this.value,'9999999999','N')" Width="40px" />
                        </FooterTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEQty" Style="text-align: right" Text='<%#DataBinder.Eval(Container, "DataItem.Qty")%>' runat="server"
                                onkeypress="return NumericOnlyWith(event,'');" onkeyup="pic(this,this.value,'9999999999','N')" Width="60px" />
                        </EditItemTemplate>
                    </asp:TemplateColumn>

                    <asp:TemplateColumn>
                        <HeaderStyle CssClass="titleTablePromo" Width="40px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="center"></ItemStyle>
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnEdit" CausesValidation="False" CommandName="edit" Text="Ubah" runat="server">
                                        <img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
                            <asp:LinkButton ID="lbtnDelete" CausesValidation="False" CommandName="delete" Text="Hapus" runat="server">
                                        <img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
                        </ItemTemplate>
                        <FooterStyle HorizontalAlign="Center"></FooterStyle>
                        <FooterTemplate>
                            <asp:LinkButton ID="lbtnAdd" CommandName="add" Text="Tambah" runat="server">
                                        <img src="../images/add.gif" border="0" alt="Tambah"></asp:LinkButton>
                        </FooterTemplate>
                        <EditItemTemplate>
                            <asp:LinkButton ID="lbtnSave" CommandName="save" Text="Simpan" runat="server">
                                        <img src="../images/simpan.gif" border="0" alt="Simpan"></asp:LinkButton>
                            <asp:LinkButton id="lbtnCancel" Runat="server" CausesValidation="True" CommandName="cancel" text="Batal">
										<img src="../images/batal.gif" border="0" alt="Batal"></asp:LinkButton>
                        </EditItemTemplate>
                    </asp:TemplateColumn>
                </Columns>
            </asp:DataGrid>
        </div>
        <div><hr /></div>
        
        <div class="titleField" style="padding-right: 2px; padding-left: 2px; padding-bottom: 2px; width: 75%; padding-top: 2px; text-align: Left">
            <asp:Label ID="Label4" runat="server" Font-Size="15px" Text="Open Sales (SPK saat acara)" Font-Bold="True"></asp:Label>
        </div>
        <div style="width: 75%; overflow: scroll">
            <asp:DataGrid ID="dgBabitEventSPK" runat="server" Width="70%" BorderWidth="0px" BackColor="#CDCDCD" BorderColor="Gainsboro" 
                CellPadding="3" CellSpacing="1" AutoGenerateColumns="False" ShowFooter="True" PageSize="1000000">
                <AlternatingItemStyle ForeColor="Black" BackColor="#FDF1F2"></AlternatingItemStyle>
                <ItemStyle ForeColor="Black" BackColor="#FFFFFF"></ItemStyle>
                <HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C" HorizontalAlign="Center"></HeaderStyle>
                <FooterStyle ForeColor="#4A3C8C" BackColor="#dedede"></FooterStyle>
                <Columns>
                    <asp:TemplateColumn HeaderText="No.">
                        <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle Width="10%" HorizontalAlign="Center"></ItemStyle>
                        <ItemTemplate>
                            <%# container.itemindex+1 %>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Jenis Kendaraan" SortExpression="VechileTypeName">
                        <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle Wrap="true" HorizontalAlign="Left"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblVechileTypeName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.VechileTypeName")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Unit" SortExpression="QtyUnit">
                        <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle Width="10%" HorizontalAlign="Center"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblQtyUnit" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.QtyUnit")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                </Columns>
            </asp:DataGrid>
        </div>
        <div><hr /></div>

        <div class="titleField" style="padding-right: 2px; padding-left: 2px; padding-bottom: 2px; width: 75%; padding-top: 2px; text-align: Left">
            <asp:Label ID="Label3" runat="server" Font-Size="15px" Text="Aktivitas" Font-Bold="True"></asp:Label>
        </div>
        <div style="width: 75%; overflow: scroll">
            <asp:DataGrid ID="dgBabitEventReportActivity" runat="server" Width="70%" BorderWidth="0px" BackColor="#CDCDCD" BorderColor="Gainsboro" CellPadding="3" CellSpacing="1" AutoGenerateColumns="False" ShowFooter="True" PageSize="1000000">
                <AlternatingItemStyle ForeColor="Black" BackColor="#FDF1F2"></AlternatingItemStyle>
                <ItemStyle ForeColor="Black" BackColor="#FFFFFF"></ItemStyle>
                <HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C" HorizontalAlign="Center"></HeaderStyle>
                <FooterStyle ForeColor="#4A3C8C" BackColor="#dedede"></FooterStyle>
                <Columns>
                    <asp:TemplateColumn HeaderText="No.">
                        <HeaderStyle Width="10px" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        <ItemTemplate>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Jenis Aktivitas">
                        <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle Width="25%" HorizontalAlign="Left"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblActivityType" runat="server" Text='' Font-Bold="true">
                            </asp:Label>
                        </ItemTemplate>
                        <FooterStyle Wrap="False" HorizontalAlign="Left"></FooterStyle>
                        <FooterTemplate>
                            <asp:DropDownList ID="ddlFActivityType" runat="server">
                            </asp:DropDownList>
                        </FooterTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlEActivityType" runat="server">
                            </asp:DropDownList>
                        </EditItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Deskripsi">
                        <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle Wrap="true" HorizontalAlign="Left"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblDescription" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Description")%>'>
                            </asp:Label>
                        </ItemTemplate>
                        <FooterStyle HorizontalAlign="Center"></FooterStyle>
                        <FooterTemplate>
                            <asp:TextBox ID="txtFDescription" runat="server" Width="350px" />
                        </FooterTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEDescription" runat="server" Width="350px" Text='<%#DataBinder.Eval(Container, "DataItem.Description")%>' />
                        </EditItemTemplate>
                    </asp:TemplateColumn>

                    <asp:TemplateColumn>
                        <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle HorizontalAlign="center"></ItemStyle>
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnEdit" CausesValidation="False" CommandName="edit" Text="Ubah" runat="server">
                                        <img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
                            <asp:LinkButton ID="lbtnDelete" CausesValidation="False" CommandName="delete" Text="Hapus" runat="server">
                                        <img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
                        </ItemTemplate>
                        <FooterStyle HorizontalAlign="Center"></FooterStyle>
                        <FooterTemplate>
                            <asp:LinkButton ID="lbtnAdd" CommandName="add" Text="Tambah" runat="server">
                                        <img src="../images/add.gif" border="0" alt="Tambah"></asp:LinkButton>
                        </FooterTemplate>
                        <EditItemTemplate>
                            <asp:LinkButton ID="lbtnSave" CommandName="save" Text="Simpan" runat="server">
                                        <img src="../images/simpan.gif" border="0" alt="Simpan"></asp:LinkButton>
                            <asp:LinkButton id="lbtnCancel" Runat="server" CausesValidation="True" CommandName="cancel" text="Batal">
										<img src="../images/batal.gif" border="0" alt="Batal"></asp:LinkButton>
                        </EditItemTemplate>
                    </asp:TemplateColumn>
                </Columns>
            </asp:DataGrid>
        </div>
        <div><hr /></div>
        <div class="titleField" style="padding-right: 2px; padding-left: 2px; padding-bottom: 2px; width: 75%; padding-top: 2px; text-align: Left">
            <asp:Label ID="Label1" runat="server" Font-Size="15px" Text="Lampiran" Font-Bold="True"></asp:Label>
        </div>
        <div style="width: 75%">
            <asp:DataGrid ID="dgUploadFile" runat="server" Width="100%" BorderWidth="0px" BackColor="#CDCDCD" BorderColor="Gainsboro" CellPadding="3" CellSpacing="1"
                AutoGenerateColumns="False" ShowFooter="True" PageSize="1000000">
                <AlternatingItemStyle ForeColor="Black" BackColor="#F5F1EE"></AlternatingItemStyle>
                <ItemStyle ForeColor="Black" BackColor="#FFFFFF"></ItemStyle>
                <HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
                <FooterStyle ForeColor="#4A3C8C" BackColor="#dedede"></FooterStyle>
                <Columns>
                    <asp:TemplateColumn HeaderText="No.">
                        <HeaderStyle Width="10px" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle Width="10px" HorizontalAlign="Center"></ItemStyle>
                        <ItemTemplate>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Kategori">
                        <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblCategoryEvent" runat="server" Font-Bold="true" Text='<%# DataBinder.Eval(Container, "DataItem.EventDealerRequiredDocument.DocumentName")%>'>
                            </asp:Label>
                        </ItemTemplate>
                        <FooterStyle Wrap="False" HorizontalAlign="Left"></FooterStyle>
                        <FooterTemplate>
                            <asp:DropDownList ID="ddlFCategoryEvent" runat="server">
                            </asp:DropDownList>
                        </FooterTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Nama File">
                        <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkbtnFileName" runat="server" CommandName="Download" CommandArgument='<%#DataBinder.Eval(Container, "DataItem.Path")%>'>
                                <asp:Label ID="lblFileName" runat="server" alt="Download"></asp:Label>
                            </asp:LinkButton>
                        </ItemTemplate>
                        <FooterStyle HorizontalAlign="Left"></FooterStyle>
                        <FooterTemplate>
                            <input id="UploadFile" onkeydown="return false;" style="width: 267px; height: 20px" type="file" size="25" name="File1" runat="server">
                            <asp:Label ID="lblFileUpload" runat="server"></asp:Label>
                        </FooterTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Keterangan">
                        <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblKeterangan" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FileDescription")%>'>
                            </asp:Label>
                        </ItemTemplate>
                        <FooterStyle HorizontalAlign="Center"></FooterStyle>
                        <FooterTemplate>
                            <asp:TextBox ID="txtKeterangan" runat="server" Width="350px" />
                        </FooterTemplate>
                    </asp:TemplateColumn>

                    <asp:TemplateColumn>
                        <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle HorizontalAlign="center"></ItemStyle>
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnDelete" OnClientClick="return confirm('Anda yakin mau hapus?');" CausesValidation="False" CommandName="Delete" Text="Hapus" runat="server">
                                    <img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
                        </ItemTemplate>
                        <FooterStyle HorizontalAlign="Center"></FooterStyle>
                        <FooterTemplate>
                            <asp:LinkButton ID="lbtnAdd" CommandName="add" Text="Tambah" runat="server">
                                    <img src="../images/add.gif" border="0" alt="Tambah"></asp:LinkButton>
                        </FooterTemplate>
                    </asp:TemplateColumn>
                </Columns>
            </asp:DataGrid>
        </div>
        <div>
            <hr />
        </div>
        <br />
        <label runat="server" id="lblNotes"><b>Lain-lain (Komentar, Saran, Kritik,dll) :</b></label><br />
        <asp:TextBox ID="txtNotes" TabIndex="0" runat="server" Width="360px" TextMode="MultiLine" Height="130px"></asp:TextBox>
        <div>
            <br />
        </div>

        <asp:Button ID="btnBaru" runat="server" Text="Baru" Style="display: none"></asp:Button>&nbsp;
        <asp:Button ID="btnSave" OnClientClick="return confirm('Anda yakin mau simpan?');" runat="server" Text="Simpan"></asp:Button>&nbsp;
        <asp:Button ID="btnBack" runat="server" Text="Kembali" Visible="false"></asp:Button>&nbsp;
        <asp:Button ID="btnCetak" runat="server" Text="Cetak" OnClientClick="window.print()" Style="width:60px"></asp:Button>
    </form>
</body>
</html>
