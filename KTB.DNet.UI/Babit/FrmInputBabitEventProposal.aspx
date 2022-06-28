<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmInputBabitEventProposal.aspx.vb" Inherits="FrmInputBabitEventProposal" SmartNavigation="False" MaintainScrollPositionOnPostback="true" %>

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
    <link href="./WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script language="javascript">
        function btnFindEventClick() {
            var btnFindEvent = document.getElementById("btnFindEvent");
            btnFindEvent.click();
        }

        function ShowPPDealerSelectionGab(DealerGroupID, DealerCode) {
            var obj = document.getElementById("lblSearchDealer")
            var disabled = obj.getAttribute("disabled")
            if (disabled === false || disabled == null) {
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

        function ShowPPEventDealerSelection() {
            showPopUp('../PopUp/PopUpEventDealerSelectionOne.aspx', '', 500, 760, EventDealerSelection);
        }

        function EventDealerSelection(selectedEvent) {
            var data = selectedEvent.split(";");

            var hdnEventName = document.getElementById("hdnEventName");
            var hdnEventID = document.getElementById("hdnEventID");
            var hdnEventDealerRequiredDocumentID = document.getElementById("hdnEventDealerRequiredDocumentID");
            var txtEventDealerSelection = document.getElementById("txtEventCode");
            hdnEventID.value = data[0];
            hdnEventDealerRequiredDocumentID.value = data[1];
            txtEventDealerSelection.value = data[2];
            hdnEventName.value = data[2];
            btnFindEventClick();
            //if (navigator.appName == "Microsoft Internet Explorer") {
            //    txtEventDealerSelection.focus();
            //    txtEventDealerSelection.blur();
            //}
            //else {
            //    txtEventDealerSelection.onchange();
            //}
        }

        function toCommas(value) {
            return value.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ".");
        }
        function calculatePrice(txtPrice) {
            var txtPrices = txtPrice.value.replace(".", "").replace(".", "").replace(".", "").replace(".", "");
            var index = txtPrice.parentNode.parentNode.rowIndex;
            var dtg = document.getElementById("dgBabitEventProposal");
            var txtFQty = dtg.rows[index].getElementsByTagName("INPUT")[1];
            var txtFQty = txtFQty.value.replace(".", "").replace(".", "").replace(".", "");
            var txtFTotalPrice = dtg.rows[index].getElementsByTagName("INPUT")[3];

            if (trim(txtPrices) != "" || trim(txtPrices) != "0") {
                txtFTotalPrice.value = txtFQty * txtPrices
                txtFTotalPrice.value = toCommas(txtFTotalPrice.value)
            }
        }
        function calculateQty(txtQty) {
            var txtQtys = txtQty.value.replace(".", "").replace(".", "").replace(".", "").replace(".", "");
            var index = txtQty.parentNode.parentNode.rowIndex;
            var dtg = document.getElementById("dgBabitEventProposal");
            var txtFPrice = dtg.rows[index].getElementsByTagName("INPUT")[2];
            var txtFPrice = txtFPrice.value.replace(".", "").replace(".", "").replace(".", "").replace(".", "");
            var txtFTotalPrice = dtg.rows[index].getElementsByTagName("INPUT")[3];

            if (trim(txtQtys) != "" || trim(txtQtys) != "0") {
                txtFTotalPrice.value = txtQtys * txtFPrice
                txtFTotalPrice.value = toCommas(txtFTotalPrice.value)
            }
        }
        function trim(str) {
            return str.replace(/^\s+|\s+$/g, "");
        }

    </script>

</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <input id="hdnBabitEventProposalHeaderID" type="hidden" value="0" runat="server">

        <table id="Table2" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage" colspan="2">EVENT - INPUT PROPOSAL EVENT</td>
            </tr>
            <tr>
                <td background="../images/bg_hor.gif" height="1" colspan="2">
                    <img height="1" src="../images/bg_hor.gif" border="0"></td>
            </tr>
            <tr>
                <td height="10" colspan="2">
                    <img height="1" src="../images/dot.gif" border="0"></td>
            </tr>
            <tr>
                <td style="width: 50%" valign="top">
                    <table id="Table1" cellspacing="1" cellpadding="2" width="100%" border="0">
                        <tr valign="top">
                            <td valign="top" class="titleField" style="width: 146px">No. Proposal Event</td>
                            <td style="width: 2px">:</td>
                            <td valign="top">
                                <asp:Label ID="lblEventRegNumber" runat="server">Auto Generated</asp:Label></td>
                        </tr>
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
                            <td class="titleField" style="width: 146px">Kode Dealer Gabungan</td>
                            <td style="width: 2px">:</td>
                            <td class="titleField">
                                <asp:TextBox ID="txtCollaborateDealer" runat="server" Width="200px" MaxLength="5000"
                                    TextMode="MultiLine"></asp:TextBox>
                                <asp:Label ID="lblSearchDealer" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup">
                                </asp:Label>
                            </td>
                        </tr>
                        <tr id="trStatus" runat="server" visible="false">
                            <td class="titleField" style="width: 146px">Status Proposal</td>
                            <td style="width: 2px">:</td>
                            <td>
                                <asp:DropDownList ID="ddlEventStatus" runat="server"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 146px">Nama Event</td>
                            <td style="width: 2px">:</td>
                            <td class="titleField">
                                <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$')" ID="txtEventCode" onblur="omitSomeCharacter('txtEventCode','<>?*%$');if (this.value != ''){btnFindEventClick();}"
                                    runat="server" ToolTip="Event Search 1" Width="175px" ReadOnly="true">
                                </asp:TextBox>
                                <asp:Label ID="lblPopUpEvent" runat="server" Width="16px">
                                        <img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0">
                                </asp:Label>
                                <input id="hdnEventName" type="hidden" value="" runat="server">
                                <input id="hdnEventDealerRequiredDocumentID" type="hidden" value="" runat="server">
                                <input id="hdnEventID" type="hidden" value="" runat="server">
                                <asp:Button ID="btnFindEvent" runat="server" Style="display: none"></asp:Button>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 146px">Periode Event</td>
                            <td style="width: 2px">:</td>
                            <td>
                                <asp:Label ID="lblPeriodStartEvent" Font-Bold="true" runat="server"></asp:Label>&nbsp;s.d.&nbsp;<asp:Label ID="lblPeriodEndEvent" Font-Bold="true" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 146px">Nama Proposal Event</td>
                            <td style="width: 2px">:</td>
                            <td>
                                <asp:TextBox ID="txtProposalEventName" runat="server" Width="175px" /></td>
                        </tr>
                    </table>
                </td>
                <td style="width: 50%" valign="top">
                    <table id="Table3" cellspacing="1" cellpadding="2" width="100%" border="0">
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
                            <td class="titleField" style="width: 146px">Nama Lokasi</td>
                            <td style="width: 2px">:</td>
                            <td class="titleField">
                                <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$')" ID="txtLocationName" onblur="omitSomeCharacter('txtLocationName','<>?*%$')" runat="server" ToolTip="Nama Lokasi" Width="128px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 146px">Provinsi</td>
                            <td style="width: 2px">:</td>
                            <td class="titleField">
                                <asp:DropDownList ID="ddlProvinsi" runat="server" Width="140px" AutoPostBack="true"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 146px">Kota/Kab</td>
                            <td style="width: 2px">:</td>
                            <td class="titleField">
                                <asp:DropDownList ID="ddlKota" runat="server" Width="140px" ></asp:DropDownList>
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
            <asp:Label ID="Label3" runat="server" Font-Size="15px" Text="Rencana Aktivitas" Font-Bold="True"></asp:Label>
        </div>
        <div style="width: 75%; overflow: scroll">
            <asp:DataGrid ID="dgBabitEventProposalActivity" runat="server" Width="70%" BorderWidth="0px" BackColor="#CDCDCD" BorderColor="Gainsboro" CellPadding="3" CellSpacing="1" AutoGenerateColumns="False" ShowFooter="True" PageSize="1000000">
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
                            <asp:LinkButton ID="lbtnCancel" runat="server" CausesValidation="True" CommandName="cancel" Text="Batal">
										<img src="../images/batal.gif" border="0" alt="Batal"></asp:LinkButton>
                        </EditItemTemplate>
                    </asp:TemplateColumn>
                </Columns>
            </asp:DataGrid>
        </div>

        <div>
            <hr />
        </div>
        <div class="titleField" style="padding-right: 2px; padding-left: 2px; padding-bottom: 2px; width: 75%; padding-top: 2px; text-align: Left">
            <asp:Label ID="Label1" runat="server" Font-Size="15px" Text="Upload Dokumen Pendukung" Font-Bold="True"></asp:Label>
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
                            <asp:Label ID="lblEventDealerRequiredDocument" runat="server" Font-Bold="true" Text='<%# DataBinder.Eval(Container, "DataItem.EventDealerRequiredDocument.DocumentName")%>'>
                            </asp:Label>
                        </ItemTemplate>
                        <FooterStyle Wrap="False" HorizontalAlign="Left"></FooterStyle>
                        <FooterTemplate>
                            <asp:DropDownList ID="ddlFEventDealerRequiredDocument" runat="server">
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
        <div class="titleField" style="padding-right: 2px; padding-left: 2px; padding-bottom: 2px; width: 75%; padding-top: 2px; text-align: Left">
            <asp:Label ID="Label2" runat="server" Text="Biaya" Font-Size="15px" Font-Bold="True"></asp:Label>
        </div>
        <div>
            <asp:DataGrid ID="dgBabitEventProposal" runat="server" Width="100%" BorderWidth="0px" BackColor="#CDCDCD" BorderColor="Gainsboro" CellPadding="3" CellSpacing="1" AutoGenerateColumns="False" ShowFooter="True" PageSize="1000000">
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
                    <asp:TemplateColumn HeaderText="Item">
                        <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblItem" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Item")%>'>
                            </asp:Label>
                        </ItemTemplate>
                        <FooterStyle Wrap="False" HorizontalAlign="Center"></FooterStyle>
                        <FooterTemplate>
                            <asp:TextBox ID="txtFItem" runat="server" Width="250px" />
                        </FooterTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEItem" runat="server" Width="250px" Text='<%#DataBinder.Eval(Container, "DataItem.Item")%>' />
                        </EditItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Qty">
                        <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblQty" runat="server">
                            </asp:Label>
                        </ItemTemplate>
                        <FooterStyle Wrap="False" HorizontalAlign="Center"></FooterStyle>
                        <FooterTemplate>
                            <asp:TextBox ID="txtFQty" Style="text-align: right" runat="server" onblur="calculateQty(this)" onkeypress="return NumericOnlyWith(event,'');" onkeyup="pic(this,this.value,'9999999999','N')" Width="60px" />
                        </FooterTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEQty" Style="text-align: right" Text='<%#DataBinder.Eval(Container, "DataItem.Qty")%>' runat="server"
                                onblur="calculateQty(this)" onkeypress="return NumericOnlyWith(event,'');" onkeyup="pic(this,this.value,'9999999999','N')" Width="40px" />
                        </EditItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Biaya Satuan">
                        <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblPrice" runat="server">
                            </asp:Label>
                        </ItemTemplate>
                        <FooterStyle Wrap="False" HorizontalAlign="Center"></FooterStyle>
                        <FooterTemplate>
                            <asp:TextBox ID="txtFPrice" Style="text-align: right" onblur="calculatePrice(this)" runat="server" onkeypress="return NumericOnlyWith(event,'');"
                                onkeyup="pic(this,this.value,'9999999999','N')" Width="80px" />
                        </FooterTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEPrice" Style="text-align: right" Text='<%# Format(DataBinder.Eval(Container, "DataItem.Price"), "#,##0")%>'
                                runat="server" onblur="calculatePrice(this)" onkeypress="return NumericOnlyWith(event,'');" onkeyup="pic(this,this.value,'9999999999','N')" Width="80px" />
                        </EditItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Sub Total Biaya">
                        <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle
                            HorizontalAlign="Right"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblTotalPrice" runat="server" Text='<%# Format(DataBinder.Eval(Container, "DataItem.TotalPrice"), "#,##0")%>'>
                            </asp:Label>
                        </ItemTemplate>
                        <FooterStyle Wrap="False"
                            HorizontalAlign="Center"></FooterStyle>
                        <FooterTemplate>
                            <asp:TextBox ID="txtFTotalPrice" Style="text-align: right" runat="server" disabled="disabled"
                                Text='<%# Format(DataBinder.Eval(Container, "DataItem.Price") * DataBinder.Eval(Container, "DataItem.Qty"), "#,##0")%>'
                                onkeypress="return NumericOnlyWith(event,'');" onkeyup="pic(this,this.value,'9999999999','N')" Width="80px" />
                        </FooterTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtETotalPrice" Style="text-align: right" disabled="disabled"
                                Text='<%# Format(DataBinder.Eval(Container, "DataItem.Price") * DataBinder.Eval(Container, "DataItem.Qty"), "#,##0")%>'
                                runat="server" onkeypress="return NumericOnlyWith(event,'');" onkeyup="pic(this,this.value,'9999999999','N')" Width="80px" />
                        </EditItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Keterangan">
                        <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblDesc" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Description")%>'>
                            </asp:Label>
                        </ItemTemplate>
                        <FooterStyle HorizontalAlign="Center"></FooterStyle>
                        <FooterTemplate>
                            <asp:TextBox ID="txtFDesc" runat="server" Width="320px" />
                        </FooterTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEDesc" runat="server" Width="320px" Text='<%#DataBinder.Eval(Container, "DataItem.Description")%>' />
                        </EditItemTemplate>
                    </asp:TemplateColumn>

                    <asp:TemplateColumn>
                        <HeaderStyle Width="200px" CssClass="titleTablePromo"></HeaderStyle>
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
                            <asp:LinkButton ID="lbtnCancel" runat="server" CausesValidation="True" CommandName="cancel" Text="Batal">
										<img src="../images/batal.gif" border="0" alt="Batal"></asp:LinkButton>
                        </EditItemTemplate>
                    </asp:TemplateColumn>
                </Columns>
            </asp:DataGrid>
        </div>
        <div>
            <hr />
        </div>
        <label runat="server" id="lblNotes"><b>Catatan dari MMKSI :</b></label><br />
        <asp:TextBox ID="txtNotes" TabIndex="0" runat="server" Width="360px" TextMode="MultiLine" Enabled="false"
            Height="130px"></asp:TextBox><br />
        <br />

        <asp:Button ID="btnBaru" runat="server" Text="Baru" Style="display: none"></asp:Button>
        <asp:Button ID="btnSave" OnClientClick="return confirm('Anda yakin mau simpan?');" runat="server" Text="Simpan"></asp:Button>
        <asp:Button ID="btnBack" runat="server" Text="Kembali" Visible="false"></asp:Button>
    </form>
</body>
</html>
