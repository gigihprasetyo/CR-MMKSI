<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmInputBabitEvent.aspx.vb" Inherits="FrmInputBabitEvent" SmartNavigation="False" MaintainScrollPositionOnPostback="true" %>

<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<html>
<head runat="server">
    <title>FrmPengajuanBabit</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script language="javascript" src="../WebResources/FormFunctions.js"></script>
    <script type="text/javascript" src="../WebResources/jquery.min.js"></script>
    <script type="text/javascript">
        function DisableButton() {
            document.form1.btnSave.disabled = true
        }

        function HideDiv() {
            var p = document.getElementsByTagName("Loading");
            p.style.display = 'none';
        }

        function GetIEVersion() {
            var sAgent = window.navigator.userAgent;
            var Idx = sAgent.indexOf("MSIE");

            if (Idx > 0) {
                return parseInt(sAgent.substring(Idx + 5, sAgent.indexOf(".", Idx)));
            }
            else if (!!navigator.userAgent.match(/Trident\/7\./))
                return 11;

            else
                return 0; //It is not IE
        }

        $(document).ready(function () {

            function hideAll() {

                $('#Loading').hide();
            }

            hideAll();

            $('.waitLoad').click(function () {

                hideAll();

                switch ($(this).attr("id")) {
                    case "lnkReload":
                        $('#Loading').show();
                        break;
                }
            }); // end of function for clicking 


        });


    </script>

    <script language="javascript">
        function ShowPPDealerSelectionAlokasi(obj, dealerID) {
            var hdnIndexSelectedGrid = document.getElementById("hdnIndexSelectedGrid");
            var idx = obj.parentNode.parentNode.rowIndex;
            hdnIndexSelectedGrid.value = idx
            showPopUp('../General/../PopUp/PopUpBabitDealerAllocationSelectionOne.aspx?dealerID=' + dealerID, '', 500, 760, DealerSelectionAlokasi);
        }
        function DealerSelectionAlokasi(selectedDealer) {
            var hdnIndexSelectedGrid = document.getElementById("hdnIndexSelectedGrid");
            var idx = hdnIndexSelectedGrid.value;
            var dtGrid = document.getElementById("dgAlloc");

            var txtDealerSelection = dtGrid.rows[idx].getElementsByTagName("INPUT")[0];
            if (txtDealerSelection != undefined) {
                txtDealerSelection.value = selectedDealer.split(';')[0];
                __doPostBack(txtDealerSelection.id, "");
            }
        }

        function ShowPPDealerSelectionGab(DealerGroupID, DealerCode) {
            var obj = document.getElementById("lblSearchDealer")
            var disabled = obj.getAttribute("disabled")
            if (disabled === false || disabled == null) {
                showPopUp('../General/../PopUp/PopUpDealerSelection.aspx?Group=' + DealerGroupID + '&Dealer=' + DealerCode, '', 500, 760, DealerSelectionGab);
            }
        }

        function DealerSelectionGab(selectedDealer) {
            var txtDealerSelection = document.getElementById("txtBabitDealerGroup");
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

            if (navigator.appName == "Microsoft Internet Explorer") {
                txtDealerCodeSelection.focus();
                txtDealerCodeSelection.blur();
            }
            else {
                txtDealerCodeSelection.onchange();
            }
        }

        function ShowPPDealerBranchSelection() {
            var txtDealerSelection = document.getElementById("lblDealerCodeName");
            var data
            if (txtDealerSelection == null) {
                txtDealerSelection = document.getElementById("txtDealerCode");
                data = txtDealerSelection.value;
            } else {
                data = txtDealerSelection.innerHTML.split(" / ")[0];
            }
            showPopUp('../Babit/../PopUp/PopUpDealerBranchSelectionSingle.aspx?m=d&deal=' + data, '', 500, 760, TemporaryOutlet);
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

        function toCommas(value) {
            return value.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ".");
        }
        function calculatePrice(txtPrice) {
            var txtPrices = txtPrice.value.replace(".", "").replace(".", "").replace(".", "").replace(".", "");
            var index = txtPrice.parentNode.parentNode.rowIndex;
            var dtg = document.getElementById("dgBabitEvent");
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
            var dtg = document.getElementById("dgBabitEvent");
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
    <form id="Form1" runat="server">
        <input id="hdnBabitHeaderID" type="hidden" value="0" runat="server">
        <input id="hdnIndexSelectedGrid" type="hidden" value="" runat="server">

        <table id="Table2" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage">BABIT - INPUT BABIT EVENT</td>
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
                        <tr>
                            <td class="titleField" style="width: 146px">Nomor Registrasi</td>
                            <td style="width: 2px">:</td>
                            <td>
                                <asp:Label ID="lblBabitRegNumber" runat="server">[Auto Generated]</asp:Label></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 146px">Kode Dealer</td>
                            <td style="width: 2px">:</td>
                            <td>
                                <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$')" ID="txtDealerCode" onblur="omitSomeCharacter('txtDealerCode','<>?*%$')" runat="server" ToolTip="Dealer Search 1" Width="128px"></asp:TextBox>
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
                            <td class="titleField" style="width: 146px">Area</td>
                            <td style="width: 2px">:</td>
                            <td class="titleField">
                                <asp:Label ID="lblArea2CodeDesc" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr valign="top">
                            <td class="titleField" style="width: 146px">MarBox</td>
                            <td style="width: 2px">
                                <asp:Label ID="Label8" runat="server">:</asp:Label></td>
                            <td class="titleField">
                                <asp:DropDownList ID="ddlMarBox" runat="server" Width="150px" AutoPostBack="true"></asp:DropDownList>&nbsp;
                                <asp:LinkButton ID="lnkReload" runat="server" Width="16px" class="waitLoad">
                                        <img style="cursor:hand" alt="Reload MarBox" src="../images/reload.gif" border="0">
                                </asp:LinkButton>
                                <div id="Loading">
                                    <img src="../images/loader.gif" width="100px" height="100px" />
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 146px">Periode Marbox</td>
                            <td style="width: 2px">
                                <asp:Label ID="Label9" runat="server">:</asp:Label></td>
                            <td class="titleField">
                                <asp:Label ID="lblPeriodeMarbox" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 146px">Lokasi Marbox</td>
                            <td style="width: 2px">
                                <asp:Label ID="Label11" runat="server">:</asp:Label></td>
                            <td class="titleField">
                                <asp:Label ID="lblLokasiMarbox" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 146px">Nomor Surat</td>
                            <td style="width: 2px">:</td>
                            <td class="titleField">
                                <asp:TextBox ID="txtNoSurat" MaxLength="30" runat="server" Width="150px"></asp:TextBox></td>
                        </tr>
                        <tr valign="top">
                            <td class="titleField" style="width: 146px">Periode</td>
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
                            <td class="titleField" style="width: 146px">Kategori Kegiatan</td>
                            <td style="width: 2px">:</td>
                            <td>
                                <asp:DropDownList ID="ddlBabitMasterEventTypeID" AutoPostBack="true" Width="100px" runat="server" /></td>
                        </tr>
                        <tr style="display: none">
                            <td class="titleField" style="width: 146px">Tipe Alokasi Babit</td>
                            <td style="width: 2px">:</td>
                            <td>
                                <asp:DropDownList ID="ddlAlocBabitType" Width="100px" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 146px">Kode Dealer Gabungan</td>
                            <td style="width: 1px">:</td>
                            <td class="titleField">
                                <asp:TextBox ID="txtBabitDealerGroup" runat="server" Width="200px" MaxLength="5000"
                                    TextMode="MultiLine"></asp:TextBox>
                                <asp:Label ID="lblSearchDealer" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup">
                                </asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="width: 50%" valign="top">
                    <table id="Table11" cellspacing="1" cellpadding="2" width="100%" border="0">
                        <tr>
                            <td class="titleField" style="width: 146px">Lokasi Event</td>
                            <td style="width: 2px">:</td>
                            <td class="titleField">
                                <asp:TextBox ID="txtLocation" MaxLength="200" runat="server" Width="50%"></asp:TextBox></td>
                        </tr>
                        <tr>
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
                            <td class="titleField" style="width: 146px">Target SPK</td>
                            <td style="width: 2px">:</td>
                            <td valign="top">
                                <asp:TextBox ID="txtProspectTarget" Style="text-align: right" runat="server"
                                    onkeypress="return NumericOnlyWith(event,'');" onkeyup="pic(this,this.value,'9999999999','N')" Width="60px" />
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 146px">Jumlah Undangan</td>
                            <td style="width: 2px">:</td>
                            <td class="titleField">
                                <asp:TextBox ID="txtInvitationQty" Style="text-align: right" runat="server" onkeypress="return NumericOnlyWith(event,'');" onkeyup="pic(this,this.value,'9999999999','N')" Width="60px" />
                            </td>
                        </tr>
                        <tr runat="server" id="TR_Alokasi_Babit2" style="display: none">
                            <td class="titleField" style="width: 146px">Alokasi Babit yang digunakan</td>
                            <td style="width: 2px">:</td>
                            <td class="titleField">
                                <asp:DropDownList ID="ddlAllocationBabit" runat="server" Width="150px"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="titleField" style="width: 146px">Tipe Alokasi</td>
                            <td style="width: 2px">:</td>
                            <td class="titleField">
                                <asp:DropDownList ID="ddlAllocationType" runat="server" Width="150px"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr runat="server" id="TR_Jml_Subsidi" style="display: none">
                            <td class="titleField" style="width: 146px">Jumlah Subsidi</td>
                            <td style="width: 2px">:</td>
                            <td class="titleField">
                                <asp:TextBox ID="txtSubsidyAmount" Style="text-align: right" runat="server" onkeypress="return NumericOnlyWith(event,'');" onkeyup="pic(this,this.value,'9999999999','N')" Width="90px" />
                            </td>
                        </tr>
                        <tr runat="server" id="TR_CatatanMKS" valign="top">
                            <td class="titleField" style="width: 146px">Catatan dari MMKSI</td>
                            <td style="width: 2px">:</td>
                            <td class="titleField">
                                <asp:TextBox ID="txtNotes" TabIndex="0" runat="server" Width="360px" TextMode="MultiLine" Enabled="false" Height="90px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <div>
            <hr />
        </div>
        <div id="div_Alokasi_Babit" runat="server">
            <div class="titleField" style="padding-right: 2px; padding-left: 2px; padding-bottom: 2px; width: 75%; padding-top: 2px; text-align: Left">
                <asp:Label ID="Label4" runat="server" Text="Alokasi Babit yang digunakan :" Font-Size="15px" Font-Bold="True"></asp:Label>
            </div>
            <div style="width: 100%">
                <asp:DataGrid ID="dgAlloc" runat="server" Width="70%" BorderWidth="0px" BackColor="#CDCDCD" BorderColor="Gainsboro"
                    CellPadding="3" CellSpacing="1" AutoGenerateColumns="False" ShowFooter="True">
                    <ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
                    <HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" CssClass="titleTablePromo"></HeaderStyle>
                    <FooterStyle ForeColor="Black" BackColor="#EFEFEF"></FooterStyle>
                    <Columns>
                        <asp:TemplateColumn HeaderText="No">
                            <HeaderStyle Width="2%" CssClass="titleTablePromo"></HeaderStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblNo" runat="server" Text='<%# container.itemindex+1 %>'>
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Dealer Alokasi">
                            <HeaderStyle ForeColor="White" CssClass="titleTablePromo" HorizontalAlign="Center" Width="250px"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Left" />
                            <ItemTemplate>
                                <asp:Label ID="lblDealerCodeName" runat="server" Text=""></asp:Label>
                                <asp:Label ID="lblDealerID" runat="server" Text="" style="display:none"></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtDealerCodeAlokasi" runat="server" Width="75px" AutoPostBack="true" 
                                    OnTextChanged = "txtDealerCodeAlokasi_TextChanged"></asp:TextBox>
                                <asp:Label ID="lblSearchDealerGrid" runat="server">
									    <img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup">
                                </asp:Label>                                            
                            </FooterTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtEDealerCodeAlokasi" name="txtEDealerCodeAlokasi" Text="" AutoPostBack="true" 
                                    OnTextChanged = "txtDealerCodeAlokasi_TextChanged"
                                    runat="server" Width="75px"></asp:TextBox>
                                &nbsp;<asp:Label ID="lblESearchDealerGrid" runat="server">
								    <img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:Label>
                            </EditItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Alokasi BABIT">
                            <HeaderStyle ForeColor="White" CssClass="titleTablePromo" HorizontalAlign="Center" Width="150px"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label ID="lblAllocationBabit" runat="server" Text=""></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:DropDownList ID="ddlAllocationBabit" runat="server" AutoPostBack="true" 
                                    OnSelectedIndexChanged="ddlAllocationBabit_SelectedIndexChanged">
                                </asp:DropDownList>
                            </FooterTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList ID="ddlEAllocationBabit" runat="server" AutoPostBack="true" 
                                    OnSelectedIndexChanged="ddlAllocationBabit_SelectedIndexChanged">
                                </asp:DropDownList>
                            </EditItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Maksimal Subsidi">
                            <HeaderStyle ForeColor="White" CssClass="titleTablePromo"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Right" />
                            <ItemTemplate>
                                <asp:Label ID="lblJmlMaxSubsidy" runat="server" Text="0"></asp:Label>
                            </ItemTemplate>
                            <FooterStyle HorizontalAlign="Right"/>
                            <FooterTemplate>
                                <asp:Label ID="lblFJmlMaxSubsidy" runat="server" Text="0"></asp:Label>
                            </FooterTemplate>
                            <EditItemTemplate>
                                <asp:Label ID="lblEJmlMaxSubsidy" runat="server" Text="0"></asp:Label>
                            </EditItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Jumlah Subsidi" FooterStyle-HorizontalAlign="Center">
                            <HeaderStyle ForeColor="White" CssClass="titleTablePromo" HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Right" />
                            <ItemTemplate>
                                <asp:Label ID="lblJmlSubsidy" runat="server" Text="0"></asp:Label>
                            </ItemTemplate>
                            <FooterStyle HorizontalAlign="right"/>
                            <FooterTemplate>
                                <asp:TextBox ID="txtJmlSubsidy" Style="text-align: right"
                                    onkeypress="return NumericOnlyWith(event,'');" onkeyup="pic(this,this.value,'9999999999','N')" 
                                    runat="server" Width="90px" TabIndex="1" Text="0"></asp:TextBox>
                            </FooterTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtEJmlSubsidy" Style="text-align: right"
                                    onkeypress="return NumericOnlyWith(event,'');" onkeyup="pic(this,this.value,'9999999999','N')" 
                                    runat="server" Width="90px" TabIndex="1" Text='0' />
                            </EditItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="ID" Visible="false">
                            <HeaderStyle Width="2%" CssClass="titleTablePromo"></HeaderStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblID" runat="server" Text='<%#DataBinder.Eval(Container, "DataItem.ID")%>' />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:Label ID="lblEID" runat="server" Text='<%#DataBinder.Eval(Container, "DataItem.ID")%>' />
                            </EditItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center">
                            <HeaderStyle ForeColor="White" CssClass="titleTablePromo"></HeaderStyle>
                            <ItemStyle Width="10%" />
                            <FooterStyle Width="10%" />
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnEdit" CausesValidation="False" CommandName="Edit" Text="Ubah" runat="server">
                                            <img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
                                <asp:LinkButton ID="lnkbtnDelete" runat="server" CommandName="Delete" CausesValidation="False">
										    <img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:LinkButton ID="lnkbtnAdd" runat="server" CommandName="Add" CausesValidation="False" TabIndex="0">
										    <img src="../images/add.gif" border="0" alt="Tambah"></asp:LinkButton>
                            </FooterTemplate>
                            <EditItemTemplate>
                                <asp:LinkButton ID="lbtnSave" CommandName="Save" Text="Simpan" runat="server" OnClientClick="return confirm('Anda yakin mau simpan data ini ?');" >
                                            <img src="../images/simpan.gif" border="0" alt="Simpan"></asp:LinkButton>
                                <asp:LinkButton ID="lbtnCancel" runat="server" CausesValidation="True" CommandName="Cancel" Text="Batal">
										    <img src="../images/batal.gif" border="0" alt="Batal"></asp:LinkButton>
                            </EditItemTemplate>
                        </asp:TemplateColumn>
                    </Columns>
                </asp:DataGrid>
            </div>
        </div>
        <div>
            <hr />
        </div>
        <div class="titleField" style="padding-right: 2px; padding-left: 2px; padding-bottom: 2px; width: 75%; padding-top: 2px; text-align: Left">
            <asp:Label ID="Label1" runat="server" Text="Upload Dokumen Pendukung" Font-Size="15px" Font-Bold="True"></asp:Label>
        </div>
        <div style="width: 70%">
            <asp:DataGrid ID="dgUploadFile" runat="server" Width="100%" BorderWidth="0px" BackColor="#CDCDCD" BorderColor="Gainsboro" CellPadding="3" CellSpacing="1"
                AutoGenerateColumns="False" ShowFooter="True" PageSize="1000000">
                <AlternatingItemStyle ForeColor="Black" BackColor="#F5F1EE"></AlternatingItemStyle>
                <ItemStyle ForeColor="Black" BackColor="#FFFFFF"></ItemStyle>
                <HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
                <FooterStyle ForeColor="#4A3C8C" BackColor="#dedede"></FooterStyle>
                <Columns>
                    <asp:TemplateColumn HeaderText="No.">
                        <HeaderStyle Width="5%" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        <ItemTemplate>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Nama File">
                        <HeaderStyle CssClass="titleTablePromo" Width="20%"></HeaderStyle>
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkbtnFileName" runat="server" CommandName="Download" CommandArgument='<%#DataBinder.Eval(Container, "DataItem.Path")%>'>
                                <asp:Label ID="lblFileName" runat="server" alt="Download"></asp:Label>
                            </asp:LinkButton>
                        </ItemTemplate>
                        <FooterStyle HorizontalAlign="Center"></FooterStyle>
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
                            <asp:TextBox ID="txtKeterangan" runat="server" Width="450px" TabIndex="12" />
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
                            <asp:LinkButton ID="lbtnAdd" TabIndex="40" CommandName="add" Text="Tambah" runat="server">
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
            <asp:Label ID="Label3" runat="server" Text="Kategori Display dan Target Penjualan" Font-Size="15px" Font-Bold="True"></asp:Label>
        </div>
        <div style="width: 70%">
            <asp:DataGrid ID="dgDisplayAndTarget" runat="server" Width="100%" BorderWidth="0px" BackColor="#CDCDCD" BorderColor="Gainsboro"
                CellPadding="3" CellSpacing="1" AutoGenerateColumns="False" ShowFooter="True" PageSize="1000000">
                <AlternatingItemStyle ForeColor="Black" BackColor="#F5F1EE"></AlternatingItemStyle>
                <ItemStyle ForeColor="Black" BackColor="#FFFFFF"></ItemStyle>
                <HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
                <FooterStyle ForeColor="#4A3C8C" BackColor="#dedede"></FooterStyle>
                <Columns>
                    <asp:TemplateColumn HeaderText="No.">
                        <HeaderStyle Width="5%" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        <ItemTemplate>
                            <%# container.itemindex+1 %>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Kategori">
                        <HeaderStyle Width="20%" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblKategoriKendaraan" runat="server" Text=''>
                            </asp:Label>
                        </ItemTemplate>
                        <FooterStyle Wrap="False" HorizontalAlign="Center"></FooterStyle>
                        <FooterTemplate>
                            <asp:DropDownList ID="ddlFKategoriKendaraan" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlFKategoriKendaraan_SelectedIndexChanged"></asp:DropDownList>
                        </FooterTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlEKategoriKendaraan" runat="server" AutoPostBack="true">
                            </asp:DropDownList>
                        </EditItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Model">
                        <HeaderStyle Width="25%" CssClass="titleTablePromo"></HeaderStyle>
                        <%--<ItemStyle HorizontalAlign="Right"></ItemStyle>--%>
                        <ItemTemplate>
                            <asp:Label ID="lblModelKendaraan" runat="server">
                            </asp:Label>
                        </ItemTemplate>
                        <FooterStyle Wrap="False" HorizontalAlign="Center"></FooterStyle>
                        <FooterTemplate>
                            <asp:DropDownList ID="ddlFModelKendaraan" runat="server" AutoPostBack="false"></asp:DropDownList>
                        </FooterTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlEModelKendaraan" runat="server" AutoPostBack="false"></asp:DropDownList>
                        </EditItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Qty Display">
                        <HeaderStyle Width="10%" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblQtyDisplay" runat="server">
                            </asp:Label>
                        </ItemTemplate>
                        <FooterStyle Wrap="False" HorizontalAlign="Center"></FooterStyle>
                        <FooterTemplate>
                            <asp:TextBox ID="txtFQtyDisplay" Style="text-align: right" runat="server" onkeypress="return NumericOnlyWith(event,'');" onkeyup="pic(this,this.value,'9999999999','N')" Width="60px" />
                        </FooterTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEQtyDisplay" Style="text-align: right" runat="server" Text='<%#DataBinder.Eval(Container, "DataItem.Qty")%>' onkeypress="return NumericOnlyWith(event,'');" onkeyup="pic(this,this.value,'9999999999','N')" Width="60px" />
                        </EditItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Target Penjualan">
                        <HeaderStyle Width="20%" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblTargetPenjualan" runat="server">
                            </asp:Label>
                        </ItemTemplate>
                        <FooterStyle Wrap="False" HorizontalAlign="Center"></FooterStyle>
                        <FooterTemplate>
                            <asp:TextBox ID="txtFTargetPenjualan" Style="text-align: right" runat="server" onkeypress="return NumericOnlyWith(event,'');" onkeyup="pic(this,this.value,'9999999999','N')" Width="100px" />
                        </FooterTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtETargetPenjualan" Style="text-align: right" runat="server" Text='<%#DataBinder.Eval(Container, "DataItem.SalesTarget")%>' onkeypress="return NumericOnlyWith(event,'');" onkeyup="pic(this,this.value,'9999999999','N')" Width="100px" />
                        </EditItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Test Drive">
                        <HeaderStyle Width="10%" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle HorizontalAlign="center"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblTestDrive" runat="server">
                            </asp:Label>
                        </ItemTemplate>
                        <FooterStyle Wrap="False" HorizontalAlign="Center"></FooterStyle>
                        <FooterTemplate>
                            <asp:CheckBox ID="CBFTestDrive" runat="server" />
                        </FooterTemplate>
                        <EditItemTemplate>
                            <asp:CheckBox ID="CBETestDrive" runat="server" />
                        </EditItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderStyle Width="10%" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle HorizontalAlign="center"></ItemStyle>
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnEdit" CausesValidation="False" CommandName="edit" Text="Ubah" runat="server">
                                        <img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
                            <asp:LinkButton ID="lbtnDelete" OnClientClick="return confirm('Anda yakin mau hapus?');" CausesValidation="False" CommandName="delete" Text="Hapus" runat="server">
                                        <img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
                        </ItemTemplate>
                        <FooterStyle HorizontalAlign="Center"></FooterStyle>
                        <FooterTemplate>
                            <asp:LinkButton ID="lbtnAdd" TabIndex="7" CommandName="add" Text="Tambah" runat="server">
                                        <img src="../images/add.gif" border="0" alt="Tambah"></asp:LinkButton>
                        </FooterTemplate>
                        <EditItemTemplate>
                            <asp:LinkButton ID="lbtnSave" TabIndex="40" CommandName="save" Text="Simpan" runat="server">
                                        <img src="../images/simpan.gif" border="0" alt="Simpan"></asp:LinkButton>
                            <asp:LinkButton ID="lbtnCancel" TabIndex="50" CommandName="cancel" Text="Batal" runat="server">
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
            <asp:Label ID="Label2" runat="server" Text="Biaya" Font-Bold="True" Font-Size="15px"></asp:Label>
        </div>
        <div>
            <asp:DataGrid ID="dgBabitEvent" runat="server" Width="100%" BorderWidth="0px" BackColor="#CDCDCD" BorderColor="Gainsboro" CellPadding="3" CellSpacing="1" AutoGenerateColumns="False" ShowFooter="True" PageSize="1000000">
                <AlternatingItemStyle ForeColor="Black" BackColor="#F5F1EE"></AlternatingItemStyle>
                <ItemStyle ForeColor="Black" BackColor="#FFFFFF"></ItemStyle>
                <HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
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
                        <ItemTemplate>
                            <asp:Label
                                ID="lblCategoryBabitEvent" runat="server" Text='' Font-Bold="true">
                            </asp:Label>
                        </ItemTemplate>
                        <FooterStyle Wrap="False" HorizontalAlign="Center"></FooterStyle>
                        <FooterTemplate>
                            <asp:DropDownList ID="ddlFCategoryBabitEvent" TabIndex="0" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlFCategoryBabitEvent_SelectedIndexChanged">
                            </asp:DropDownList>
                        </FooterTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlECategoryBabitEvent" TabIndex="0" runat="server" AutoPostBack="false">
                            </asp:DropDownList>
                        </EditItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Jenis">
                        <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblJenisBabitEvent" runat="server" Text=''>
                            </asp:Label>
                        </ItemTemplate>
                        <FooterStyle Wrap="False" HorizontalAlign="Center"></FooterStyle>
                        <FooterTemplate>
                            <asp:DropDownList ID="ddlFJenisBabitEvent" TabIndex="1" runat="server" AutoPostBack="false">
                            </asp:DropDownList>
                        </FooterTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlEJenisBabitEvent" TabIndex="1" runat="server" AutoPostBack="false">
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
                            <asp:TextBox ID="txtFItem" runat="server" TabIndex="2" Width="250px" />
                        </FooterTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEItem" runat="server" TabIndex="2" Width="250px" Text='<%#DataBinder.Eval(Container, "DataItem.Item")%>' />
                        </EditItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Qty">
                        <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblQty" runat="server">
                            </asp:Label>
                        </ItemTemplate>
                        <FooterStyle Wrap="False" Width="20px" HorizontalAlign="Center"></FooterStyle>
                        <FooterTemplate>
                            <asp:TextBox ID="txtFQty" Style="text-align: right" TabIndex="3" runat="server" onblur="calculateQty(this)" onkeypress="return NumericOnlyWith(event,'');" onkeyup="pic(this,this.value,'9999999999','N')" Width="60px" />
                        </FooterTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEQty" Style="text-align: right" TabIndex="3" Text='<%#DataBinder.Eval(Container, "DataItem.Qty")%>' runat="server"
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
                            <asp:TextBox ID="txtFPrice" Style="text-align: right" TabIndex="4" onblur="calculatePrice(this)" runat="server" onkeypress="return NumericOnlyWith(event,'');"
                                onkeyup="pic(this,this.value,'9999999999','N')" Width="100px" />
                        </FooterTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEPrice" Style="text-align: right" TabIndex="4" Text='<%# Format(DataBinder.Eval(Container, "DataItem.Price"), "#,##0")%>'
                                runat="server" onblur="calculatePrice(this)" onkeypress="return NumericOnlyWith(event,'');" onkeyup="pic(this,this.value,'9999999999','N')" Width="100px" />
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
                            <asp:TextBox ID="txtFTotalPrice" Style="text-align: right" TabIndex="5" runat="server" ReadOnly="true"
                                Text='<%# Format(DataBinder.Eval(Container, "DataItem.Price") * DataBinder.Eval(Container, "DataItem.Qty"), "#,##0")%>'
                                onkeypress="return NumericOnlyWith(event,'');" onkeyup="pic(this,this.value,'9999999999','N')" Width="100px" />
                        </FooterTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtETotalPrice" Style="text-align: right" TabIndex="5" ReadOnly="true"
                                Text='<%# Format(DataBinder.Eval(Container, "DataItem.Price") * DataBinder.Eval(Container, "DataItem.Qty"), "#,##0")%>'
                                runat="server" onkeypress="return NumericOnlyWith(event,'');" onkeyup="pic(this,this.value,'9999999999','N')" Width="100px" />
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
                            <asp:TextBox ID="txtFDesc" runat="server" Width="280px" TabIndex="6" />
                        </FooterTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEDesc" runat="server" Width="280px" TabIndex="6" Text='<%#DataBinder.Eval(Container, "DataItem.Description")%>' />
                        </EditItemTemplate>
                    </asp:TemplateColumn>

                    <asp:TemplateColumn>
                        <HeaderStyle Width="200px" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle HorizontalAlign="center"></ItemStyle>
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnEdit" CausesValidation="False" CommandName="edit" Text="Ubah" runat="server">
                                        <img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
                            <asp:LinkButton ID="lbtnDelete" OnClientClick="return confirm('Anda yakin mau hapus?');" CausesValidation="False" CommandName="delete" Text="Hapus" runat="server">
                                        <img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
                        </ItemTemplate>
                        <FooterStyle HorizontalAlign="Center"></FooterStyle>
                        <FooterTemplate>
                            <asp:LinkButton ID="lbtnAdd" TabIndex="7" CommandName="add" Text="Tambah" runat="server">
                                        <img src="../images/add.gif" border="0" alt="Tambah"></asp:LinkButton>
                        </FooterTemplate>
                        <EditItemTemplate>
                            <asp:LinkButton ID="lbtnSave" TabIndex="40" CommandName="save" Text="Simpan" runat="server">
                                        <img src="../images/simpan.gif" border="0" alt="Simpan"></asp:LinkButton>
                            <asp:LinkButton ID="lbtnCancel" TabIndex="50" CommandName="cancel" Text="Batal" runat="server">
                                        <img src="../images/batal.gif" border="0" alt="Batal"></asp:LinkButton>
                        </EditItemTemplate>
                    </asp:TemplateColumn>
                </Columns>
            </asp:DataGrid>
        </div>
        <div>
            <hr />
        </div>
        <br />

        <asp:Button ID="btnBaru" runat="server" Text="Baru" TabIndex="150" class="hideButtonOnPrint" Style="display: none"></asp:Button>
        <asp:Button ID="btnSave" OnClientClick="return confirm('Anda yakin mau simpan?');" runat="server" class="hideButtonOnPrint" Text="Simpan"></asp:Button>
        <asp:Button ID="btnBack" runat="server" Text="Kembali" class="hideButtonOnPrint" Visible="false"></asp:Button>
        <input class="hideButtonOnPrint" id="btnCetak" style="width: 48px; height: 21px" onclick="window.print()" type="button" value="Cetak" name="btnCetak">
    </form>
</body>
</html>
