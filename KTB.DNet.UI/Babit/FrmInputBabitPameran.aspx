<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>

<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmInputBabitPameran.aspx.vb" Inherits=".FrmInputBabitPameran" MaintainScrollPositionOnPostback="true" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<html>
<head runat="server">
    <title>InputBabitPameran</title>
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

            if (Idx > 0)
            {
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
    <script type="text/javascript">
        function ShowPopUpTO() {
            var lblDealer = document.getElementById("lblKodeDealer");
            var dealerCode = lblDealer.innerText.split("/")[0].replace(/\s/g, '');
            //showPopUp('../PopUp/PopUpDealerBranchSelectionOne.aspx', '', 430, 800, TemporaryOutlet);
            showPopUp('../PopUp/PopUpDealerBranchSelectionSingle.aspx?m=d&deal=' + dealerCode, '', 430, 800, TemporaryOutlet);
        }

        function ShowPopUpLocation() {
            showPopUp('../PopUp/PopUpBabitMasterLocation.aspx', '', 430, 600, Location);
        }

        function TemporaryOutlet(selectedRefNumber) {
            //var txtTemporaryOutlet = document.getElementById("txtTemporaryOutlet");
            var hdnTemporaryOutlet = document.getElementById("hdnTemporaryOutlet");
            var txtTemporaryOutlet = document.getElementById("txtTemporaryOutlet");
            hdnTemporaryOutlet.value = selectedRefNumber;
            txtTemporaryOutlet.value = selectedRefNumber.split(";")[0];

            //if (navigator.appName == "Microsoft Internet Explorer") {
            //    hdnTemporaryOutlet.blur();
            //}
            //else {
            //    hdnTemporaryOutlet.onchange();
            //}
        }

        function Location(selectedRefNumber) {
            var HFLocation = document.getElementById("HFLocation");
            HFLocation.value = selectedRefNumber;
            __doPostBack("HFLocation", "");

            if (navigator.appName == "Microsoft Internet Explorer") {
                HFLocation.blur();
            }
            else {
                HFLocation.onchange();
            }
        }

        function toCommas(value) {
            return value.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ".");
        }

        function calculatePrice(txtPrice) {
            var txtPrices = txtPrice.value.replace(".", "").replace(".", "").replace(".", "").replace(".", "");
            var index = txtPrice.parentNode.parentNode.rowIndex;
            var dtg = document.getElementById("dgBabitPameran");
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
            var dtg = document.getElementById("dgBabitPameran");
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

        function BindCBMaterialPromosiEvent() {
            var checkList = document.getElementById("<%=CBMaterialPromosi.ClientID%>");
            var checkboxes = checkList.getElementsByTagName("INPUT");
            for (var i = 0; i < checkboxes.length; i++) {
                checkboxes[i].onclick = function () {
                    var message = "";
                    for (var i = 0; i < checkboxes.length; i++) {
                        var label = checkboxes[i].parentNode.getElementsByTagName("LABEL")[0];
                        if (label.innerHTML.toUpperCase() == 'LAINNYA') {
                            if (checkboxes[i].checked) {
                                document.getElementById("<%= txtOtherMaterialPromosi.ClientID%>").disabled = false
                                //alert('tes')
                            }
                            else {
                                document.getElementById("<%=txtOtherMaterialPromosi.ClientID%>").disabled = true
                                document.getElementById("<%=txtOtherMaterialPromosi.ClientID%>").value = ''
                            }
                        }
                    }
                };
            }
        }

        function BindCBProfilPengunjungEvent() {
            var checkListPP = document.getElementById("<%=CBProfilPengunjung.ClientID%>");
            var checkboxesPP = checkListPP.getElementsByTagName("INPUT");
            for (var i = 0; i < checkboxesPP.length; i++) {
                checkboxesPP[i].onclick = function () {
                    var message = "";
                    for (var i = 0; i < checkboxesPP.length; i++) {
                        var label = checkboxesPP[i].parentNode.getElementsByTagName("LABEL")[0];
                        if (label.innerHTML.toUpperCase() == 'LAINNYA') {
                            if (checkboxesPP[i].checked) {
                                document.getElementById("<%= txtOtherProfilPengunjung.ClientID%>").disabled = false
                                //alert('tes')
                            }
                            else {
                                document.getElementById("<%=txtOtherProfilPengunjung.ClientID%>").disabled = true
                                document.getElementById("<%=txtOtherProfilPengunjung.ClientID%>").value = ''
                            }
                        }
                    }
                };
            }
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <input id="hdnIndexSelectedGrid" type="hidden" value="" runat="server">
        <input id="hdnBabitHeaderID" type="hidden" value="0" runat="server">
        <table id="Table123" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage">BABIT -&nbsp; INPUT BABIT PAMERAN </td>
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
                <td>
                    <table id="Table1" cellspacing="1" cellpadding="2" width="100%" border="0">
                        <tr>
                            <td class="titleField" width="24%">
                                <asp:Label ID="Label1" runat="server">Nomor Registrasi</asp:Label>
                            </td>
                            <td width="1%">:</td>
                            <td width="25%">
                                <asp:Label ID="lblNomorRegistrasi" runat="server" Text="[Auto Generated]"></asp:Label>
                            </td>
                            <td style="width: 149px" width="149"></td>
                            <td width="1%"></td>
                            <td width="29%"></td>
                        </tr>
                        <tr>
                            <td class="titleField">
                                <asp:Label ID="Label5" runat="server">Kode Dealer</asp:Label>
                            </td>
                            <td>:</td>
                            <td>
                                <asp:Label ID="lblKodeDealer" runat="server"></asp:Label>
                                <asp:Label ID="lblNamaDealer" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField">
                                <asp:Label ID="Label4" runat="server">Kode Temporary Outlet</asp:Label>
                            </td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="txtTemporaryOutlet" runat="server" Width="128px" ReadOnly="true"></asp:TextBox>
                                <asp:HiddenField ID="hdnTemporaryOutlet" runat="server" />
                                <asp:label ID="lblPopUpTO" runat="server" Width="16px">
                                        <img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0">
                                </asp:label>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField">
                                <asp:Label ID="Label17" runat="server">Nama Temporary Outlet</asp:Label>
                            </td>
                            <td>:</td>
                            <td>
                                <asp:Label ID="lblNamaCabang" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField">
                                <asp:Label ID="Label7" runat="server">Area</asp:Label>
                            </td>
                            <td>:</td>
                            <td>
                                <asp:Label ID="lblArea" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr valign="top">
                            <td class="titleField">MarBox</td>
                            <td>
                                <asp:Label ID="Label8" runat="server">:</asp:Label></td>
                            <td>
                                <asp:DropDownList ID="ddlMarBox" runat="server" Width="150px" AutoPostBack="true"></asp:DropDownList>
                                <asp:LinkButton ID="lnkReload" runat="server" Width="16px" class="waitLoad">
                                        <img style="cursor:hand" alt="Reload MarBox" src="../images/reload.gif" border="0">
                                </asp:LinkButton>
                                <div id="Loading">
                                    <img src="../images/loader.gif" width="100px" height="100px" />
                                </div>

                            </td>
                        </tr>
                        <tr>
                            <td class="titleField">Periode Marbox</td>
                            <td>
                                <asp:Label ID="Label16" runat="server">:</asp:Label></td>
                            <td>
                                <asp:Label ID="lblPeriodeMarbox" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="titleField">Lokasi Marbox</td>
                            <td>
                                <asp:Label ID="Label18" runat="server">:</asp:Label></td>
                            <td>
                                <asp:Label ID="lblLokasiMarbox" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="titleField">
                                <asp:Label ID="Label6" runat="server">Nomor Surat</asp:Label>
                            </td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="txtNomorSurat" runat="server" MaxLength="30" Width="150px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr valign="top">
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

                        <%--<tr>
                                <td class="titleField">
                                    <asp:Label ID="label24" runat="server">Tipe Alokasi BABIT</asp:Label></td>
                                <td>:</td>
                                <td>
                                    <asp:DropDownList ID="ddlAllocationType" runat="server" Width="140px"></asp:DropDownList>
                                </td>
                            </tr>--%>
                        <tr>
                            <td colspan="6">
                                <asp:Label ID="Label3" runat="server" Text="INFORMASI PAMERAN" ForeColor="Red" Font-Bold="true" Font-Size="X-Small"></asp:Label>
                            </td>
                        </tr>
                        <tr valign="top">
                            <td class="titleField">
                                <asp:Label ID="Label2" runat="server">Lokasi</asp:Label>
                            </td>
                            <td>:</td>
                            <td>
                                <asp:DropDownList ID="ddlLocationType" runat="server" AutoPostBack="true"></asp:DropDownList>
                            <%--</td>
                            <td>--%>
                                <asp:TextBox ID="txtLocation" runat="server" Width="128px" Visible="false" ReadOnly="true"></asp:TextBox>
                                <asp:label ID="lblPULocation" runat="server" Width="16px" Visible="false">
                                        <img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0">
                                </asp:label>
                                <asp:TextBox ID="txtOtherLocation" runat="server" Visible="false"></asp:TextBox>
                                <asp:HiddenField ID="HFLocation" runat="server" />
                            </td>
                        </tr>
                        <tr id="trProvinsi" visible="false">
                            <td class="titleField">
                                <asp:Label ID="lblProvinsi" runat="server" Visible="false">Provinsi</asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblProvinsiTitik2" runat="server" Visible="false">:</asp:Label></td>
                            <td>
                                <asp:DropDownList ID="ddlProvinsi" runat="server" Width="140px" AutoPostBack="true" Visible="false"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField">
                                <asp:Label ID="lblKota" runat="server" Visible="false">Kota/Kab</asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblKotaTitik2" runat="server" Visible="false">:</asp:Label></td>
                            <td>
                                <asp:DropDownList ID="ddlKota" runat="server" Width="140px" Visible="false"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField">
                                <asp:Label ID="lblFactoring" runat="server">Tanggal Pameran</asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblFactoringColon" runat="server">:</asp:Label>
                            </td>
                            <td>
                                <table border="0" cellpadding="0">
                                    <tr>
                                        <td>
                                            <cc1:IntiCalendar ID="ICPameranStart" runat="server" TextBoxWidth="70" CanPostBack="True"></cc1:IntiCalendar>
                                        </td>
                                        <td>&nbsp;s.d&nbsp;</td>
                                        <td>
                                            <cc1:IntiCalendar ID="ICPameranEnd" runat="server" TextBoxWidth="70" CanPostBack="True"></cc1:IntiCalendar>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField">
                                <asp:Label ID="Label9" runat="server">Periode Pameran</asp:Label>
                            </td>
                            <td>:</td>
                            <td>
                                <asp:Label ID="lblPeriodePameran" runat="server" Text=""></asp:Label>
                            </td>
                            <td></td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="titleField">
                                <asp:Label ID="Label10" runat="server">Luas Pameran</asp:Label>
                            </td>
                            <td>:</td>
                            <td>
                                <table border="0" cellpadding="0">
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtLuasPameran" Style="text-align: right" runat="server"
                                                onkeypress="return NumericOnlyWith(event,'');" onkeyup="pic(this,this.value,'9999999999','N')" Width="60px"></asp:TextBox>
                                        </td>
                                        <td>&nbsp;m<sup>2</sup></td>
                                        <td></td>
                                    </tr>
                                </table>
                            </td>
                            <td></td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="titleField">
                                <asp:Label ID="Label11" runat="server">Target SPK</asp:Label>
                            </td>
                            <td>:</td>
                            <td>
                                <table border="0" cellpadding="0">
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtTargetProspek" Style="text-align: right" runat="server"
                                                onkeypress="return NumericOnlyWith(event,'');" onkeyup="pic(this,this.value,'9999999999','N')" Width="60px"></asp:TextBox>
                                        </td>
                                        <td></td>
                                        <td></td>
                                    </tr>
                                </table>
                            </td>
                            <td></td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr id="TR_Alokasi_Babit" runat="server">
                            <td class="titleField">
                                <asp:Label ID="Label19" runat="server">Alokasi Babit yang digunakan :</asp:Label>
                            </td>
                            <td></td>
                            <td>
                                <table border="0" cellpadding="0" style="display:none">
                                    <tr>
                                        <td>
                                            <asp:DropDownList ID="ddlAllocationBabit" runat="server" Width="150px"></asp:DropDownList>
                                        </td>
                                        <td></td>
                                        <td></td>
                                    </tr>
                                </table>
                            </td>
                            <td></td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr id="TR_Alokasi_Babit2" runat="server">
                            <td colspan="6">
                                <asp:DataGrid ID="dgAlloc" runat="server" Width="60%" BorderWidth="0px" BackColor="#CDCDCD" BorderColor="Gainsboro"
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
                                                <asp:TextBox ID="txtDealerCode" runat="server" Width="75px" AutoPostBack="true" 
                                                    OnTextChanged = "txtDealerCode_TextChanged"></asp:TextBox>
                                                <asp:Label ID="lblSearchDealerGrid" runat="server">
										                <img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup">
                                                </asp:Label>                                            
                                            </FooterTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtEDealerCode" name="txtEDealerCode" Text="" AutoPostBack="true" 
                                                    OnTextChanged = "txtDealerCode_TextChanged"
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

                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="titleField">
                                <asp:Label ID="Label20" runat="server">Tipe Alokasi</asp:Label>
                            </td>
                            <td>:</td>
                            <td>
                                <table border="0" cellpadding="0">
                                    <tr>
                                        <td>
                                            <asp:DropDownList ID="ddlAllocationType" runat="server" Width="150px"></asp:DropDownList>
                                        </td>
                                        <td></td>
                                        <td></td>
                                    </tr>
                                </table>
                            </td>
                            <td></td>
                            <td></td>
                            <td></td>
                        </tr>

                        <tr id="TR_Jml_Subsidi" runat="server" style="display:none">
                            <td class="titleField">
                                <asp:Label ID="Label21" runat="server">Jumlah Subsidi</asp:Label>
                            </td>
                            <td>:</td>
                            <td>
                                <table border="0" cellpadding="0">
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtSubsidyAmount" Style="text-align: right" runat="server" onkeypress="return NumericOnlyWith(event,'');" onkeyup="pic(this,this.value,'9999999999','N')" Width="100px" />
                                        </td>
                                        <td></td>
                                        <td></td>
                                    </tr>
                                </table>
                            </td>
                            <td></td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr id="TR_CatatanMKS" runat="server" valign="top">
                            <td class="titleField">
                                <asp:Label ID="Label12" runat="server">Catatan dari MMKSI</asp:Label>
                            </td>
                            <td>:</td>
                            <td>
                                <table border="0" cellpadding="0">
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtNotes" TabIndex="0" runat="server" Width="360px" TextMode="MultiLine" Height="100px"></asp:TextBox>
                                        </td>
                                        <td></td>
                                        <td></td>
                                    </tr>
                                </table>
                            </td>
                            <td></td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <asp:Label ID="Label22" runat="server" Text="Kategori Display dan Target Penjualan :" ForeColor="Red" Font-Bold="true" Font-Size="X-Small"></asp:Label>
                                <asp:DataGrid ID="dgDisplayAndTarget" runat="server" Width="100%" BorderWidth="0px" BackColor="#CDCDCD" BorderColor="Gainsboro"
                                    CellPadding="3" CellSpacing="1" AutoGenerateColumns="False" ShowFooter="True">
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
                                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
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
                                                <asp:LinkButton ID="lbtnDelete" OnClientClick="return confirm('Anda yakin mau hapus?');"
                                                    CausesValidation="False" CommandName="delete" Text="Hapus" runat="server">
                                                    <img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Center"></FooterStyle>
                                            <FooterTemplate>
                                                <asp:LinkButton ID="lbtnAdd" TabIndex="40" CommandName="add" Text="Tambah" runat="server">
                                                        <img src="../images/add.gif" border="0" alt="Tambah">
                                                </asp:LinkButton>
                                            </FooterTemplate>
                                            <EditItemTemplate>
                                                <asp:LinkButton ID="lbtnSave" TabIndex="40" CommandName="save" Text="Simpan" runat="server">
                                                        <img src="../images/simpan.gif" border="0" alt="Simpan">
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="lbtnCancel" TabIndex="50" CommandName="cancel" Text="Batal" runat="server">
                                                        <img src="../images/batal.gif" border="0" alt="Batal">
                                                </asp:LinkButton>
                                            </EditItemTemplate>
                                        </asp:TemplateColumn>
                                    </Columns>
                                </asp:DataGrid>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <asp:Label ID="lblSubTitle2" runat="server" Text="MATERIAL PROMOSI" ForeColor="Red" Font-Bold="true" Font-Size="X-Small"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:CheckBoxList ID="CBMaterialPromosi" runat="server" AutoPostBack="false"></asp:CheckBoxList>
                                <asp:TextBox ID="txtOtherMaterialPromosi" runat="server" Enabled="false" MaxLength="100"></asp:TextBox>
                            </td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <asp:Label ID="lblSubtitle3" runat="server" Text="GAMBAR LOKASI" ForeColor="Red" Font-Bold="true" Font-Size="X-Small"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label14" runat="server" Text="Profil Pengunjung"></asp:Label>
                            </td>
                            <td></td>
                            <td>
                                <asp:Label ID="Label15" runat="server" Text="Lokasi Pameran"></asp:Label>
                            </td>
                            <td></td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td style="vertical-align: top;">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:CheckBoxList ID="CBProfilPengunjung" runat="server" AutoPostBack="false"></asp:CheckBoxList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtOtherProfilPengunjung" runat="server" Enabled="false" MaxLength="100"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td></td>
                            <td style="vertical-align: top;">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:CheckBoxList ID="CBLokasiPameran" runat="server"></asp:CheckBoxList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                    </tr>
                                </table>
                            </td>
                            <td></td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <asp:Label ID="lblSubtitle4" runat="server" Text="DOKUMENT PENDUKUNG (CONTOH: FOTO LOKASI, DOKUMEN, DLL)" ForeColor="Red" Font-Bold="true" Font-Size="X-Small"></asp:Label>
                                <asp:DataGrid ID="dgUploadFile" runat="server" Width="100%" BorderWidth="0px" BackColor="#CDCDCD" BorderColor="Gainsboro"
                                    CellPadding="3" CellSpacing="1" AutoGenerateColumns="False" ShowFooter="True">
                                    <AlternatingItemStyle ForeColor="Black" BackColor="#F5F1EE"></AlternatingItemStyle>
                                    <ItemStyle ForeColor="Black" BackColor="#FFFFFF"></ItemStyle>
                                    <HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
                                    <FooterStyle ForeColor="#4A3C8C" BackColor="#dedede"></FooterStyle>
                                    <Columns>
                                        <asp:TemplateColumn HeaderText="No.">
                                            <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            <ItemTemplate>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Nama File">
                                            <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkbtnFileName" runat="server" CommandName="Download" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.Path")%>'>
                                                    <asp:Label ID="lblFileName" runat="server" alt="Download"></asp:Label>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Center"></FooterStyle>
                                            <FooterTemplate>
                                                <input id="UploadFile" onkeydown="return false;" style="width: 267px; height: 20px" type="file"
                                                    size="25" name="File1" runat="server"><asp:Label ID="lblFileUpload" runat="server"></asp:Label>
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
                                                <asp:TextBox ID="txtKeterangan" runat="server" Width="150px" TabIndex="6" />
                                            </FooterTemplate>
                                        </asp:TemplateColumn>

                                        <asp:TemplateColumn>
                                            <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="center"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtnDelete" CausesValidation="False" CommandName="Delete" Text="Hapus" runat="server">
                                                        <img src="../images/trash.gif" border="0" alt="Hapus">
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Center"></FooterStyle>
                                            <FooterTemplate>
                                                <asp:LinkButton ID="lbtnAdd" TabIndex="40" CommandName="add" Text="Tambah" runat="server">
                                                        <img src="../images/add.gif" border="0" alt="Tambah">
                                                </asp:LinkButton>
                                            </FooterTemplate>
                                        </asp:TemplateColumn>
                                    </Columns>
                                </asp:DataGrid>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <asp:Label ID="Label13" runat="server" Text="BIAYA" ForeColor="Red" Font-Bold="true" Font-Size="X-Small"></asp:Label>
                                <asp:DataGrid ID="dgBabitPameran" runat="server" Width="100%" BorderWidth="0px" BackColor="#CDCDCD" BorderColor="Gainsboro"
                                    CellPadding="3" CellSpacing="1" AutoGenerateColumns="False" ShowFooter="True">
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
                                        <asp:TemplateColumn HeaderText="Kategori">
                                            <HeaderStyle Width="15%" CssClass="titleTablePromo"></HeaderStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblCategoryBabitEvent" runat="server" Text='' Font-Bold="true">
                                                </asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Wrap="False" HorizontalAlign="Center"></FooterStyle>
                                            <FooterTemplate>
                                                <asp:DropDownList ID="ddlFCategoryBabitEvent" TabIndex="0" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlFCategoryBabitEvent_SelectedIndexChanged"></asp:DropDownList>
                                            </FooterTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlECategoryBabitEvent" TabIndex="0" runat="server" AutoPostBack="false">
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Jenis">
                                            <HeaderStyle Width="15%" CssClass="titleTablePromo"></HeaderStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblJenisBabitEvent" runat="server" Text=''>
                                                </asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Wrap="False" HorizontalAlign="Center"></FooterStyle>
                                            <FooterTemplate>
                                                <asp:DropDownList ID="ddlFJenisBabitEvent" TabIndex="1" runat="server" AutoPostBack="false"></asp:DropDownList>
                                            </FooterTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlEJenisBabitEvent" TabIndex="1" runat="server" AutoPostBack="false">
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Item">
                                            <HeaderStyle Width="10%" CssClass="titleTablePromo"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblItem" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Item")%>'>
                                                </asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Wrap="False" HorizontalAlign="Center"></FooterStyle>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtFItem" runat="server" TabIndex="2" Width="150px" />
                                            </FooterTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtEItem" runat="server" TabIndex="2" Width="150px" Text='<%# DataBinder.Eval(Container, "DataItem.Item")%>' />
                                            </EditItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Qty">
                                            <HeaderStyle Width="5%" CssClass="titleTablePromo"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblQty" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Qty")%>'>
                                                </asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Wrap="False" HorizontalAlign="Center"></FooterStyle>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtFQty" Style="text-align: right" TabIndex="3" runat="server" 
                                                    onblur="calculateQty(this)" onkeypress="return NumericOnlyWith(event,'');" 
                                                    onkeyup="pic(this,this.value,'9999999999','N')" Width="40px" />
                                            </FooterTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtEQty" Style="text-align: right" TabIndex="3" 
                                                    Text='<%# DataBinder.Eval(Container, "DataItem.Qty")%>' runat="server" onkeypress="return NumericOnlyWith(event,'');" 
                                                    onkeyup="pic(this,this.value,'9999999999','N')" Width="40px" />
                                            </EditItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Biaya Satuan">
                                            <HeaderStyle Width="8%" CssClass="titleTablePromo"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblPrice" runat="server" Text='<%# Format(DataBinder.Eval(Container, "DataItem.Price"), "#,##0")%>'>
                                                </asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Wrap="False" HorizontalAlign="Center"></FooterStyle>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtFPrice" Style="text-align: right" TabIndex="4" onblur="calculatePrice(this)" runat="server" onkeypress="return NumericOnlyWith(event,'');" onkeyup="pic(this,this.value,'9999999999','N')" Width="100px" />
                                            </FooterTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtEPrice" Style="text-align: right" TabIndex="4" Text='<%# Format(DataBinder.Eval(Container, "DataItem.Price"), "#,##0")%>' runat="server" onkeypress="return NumericOnlyWith(event,'');" onkeyup="pic(this,this.value,'9999999999','N')" Width="100px" />
                                            </EditItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Sub Total Biaya">
                                            <HeaderStyle Width="8%" CssClass="titleTablePromo"></HeaderStyle>
                                            <ItemStyle
                                                HorizontalAlign="Right"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblTotalPrice" runat="server" Text='<%# Format(DataBinder.Eval(Container, "DataItem.TotalPrice"), "#,##0")%>'>
                                                </asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Wrap="False"
                                                HorizontalAlign="Center"></FooterStyle>
                                            <FooterTemplate>
                                                <asp:TextBox Enabled="false" ID="txtFTotalPrice" Style="text-align: right" TabIndex="5" runat="server" ReadOnly="true"
                                                    Text='<%# Format(DataBinder.Eval(Container, "DataItem.Price") * DataBinder.Eval(Container, "DataItem.Qty"), "#,##0")%>'
                                                    onkeypress="return NumericOnlyWith(event,'');" onkeyup="pic(this,this.value,'9999999999','N')" Width="100px" />
                                            </FooterTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox Enabled="false" ID="txtETotalPrice" Style="text-align: right" TabIndex="5" ReadOnly="true"
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
                                                <asp:TextBox ID="txtFDesc" runat="server" Width="200px" TabIndex="6" />
                                            </FooterTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtEDesc" runat="server" Width="200px" TabIndex="6" Text='<%# DataBinder.Eval(Container, "DataItem.Description")%>' />
                                            </EditItemTemplate>
                                        </asp:TemplateColumn>

                                        <asp:TemplateColumn>
                                            <HeaderStyle Width="6%" CssClass="titleTablePromo"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="center"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtnEdit" CausesValidation="False" CommandName="edit" Text="Ubah" runat="server">
                                                        <img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
                                                <asp:LinkButton ID="lbtnDelete" OnClientClick="return confirm('Anda yakin mau hapus?');"
                                                    CausesValidation="False" CommandName="delete" Text="Hapus" runat="server">
                                                        <img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Center"></FooterStyle>
                                            <FooterTemplate>
                                                <asp:LinkButton ID="lbtnAdd" TabIndex="40" CommandName="add" Text="Tambah" runat="server">
                                                        <img src="../images/add.gif" border="0" alt="Tambah">
                                                </asp:LinkButton>
                                            </FooterTemplate>
                                            <EditItemTemplate>
                                                <asp:LinkButton ID="lbtnSave" TabIndex="40" CommandName="save" Text="Simpan" runat="server">
                                                        <img src="../images/simpan.gif" border="0" alt="Simpan">
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="lbtnCancel" TabIndex="50" CommandName="cancel" Text="Batal" runat="server">
                                                        <img src="../images/batal.gif" border="0" alt="Batal">
                                                </asp:LinkButton>
                                            </EditItemTemplate>
                                        </asp:TemplateColumn>
                                    </Columns>
                                </asp:DataGrid>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <hr />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <asp:Button ID="btnSave" OnClientClick="return confirm('Anda yakin mau simpan?');" runat="server" Text="Simpan" Style="margin-right: 10px" class="hideButtonOnPrint" />
                                <asp:Button ID="btnBack" runat="server" Text="Kembali" Visible="false" class="hideButtonOnPrint" Style="margin-right: 10px" />
                                <input class="hideButtonOnPrint" id="btnCetak" style="width: 48px; height: 21px" onclick="window.print()" type="button" value="Cetak" name="btnCetak">
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>

        </table>
        </div>
    </form>

    <script type="text/javascript">
        if (window.parent == window) {
            if (!navigator.appName == "Microsoft Internet Explorer") {
                self.opener = null;
                self.close();
            }
            else {
                this.name = "origWin";
                origWin = window.open(window.location, "origWin");
                window.opener = top;
                window.close();
            }
        }
    </script>
</body>
</html>
