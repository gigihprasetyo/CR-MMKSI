<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmReportSPKSalesEventNasional.aspx.vb" Inherits="FrmReportSPKSalesEventNasional" SmartNavigation="False" MaintainScrollPositionOnPostback="true" %>

<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>FrmReportSPKSalesEventNasional</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <link href="./WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script language="javascript" src="../WebResources/FormFunctions.js"></script>
    <script type="text/javascript" src="../WebResources/jquery.min.js"></script>
    <style type="text/css">
        .center {
            background-color: white;
            position: absolute;
            left: 40%;
            top: 40%;
            transform: translate(-50%, -50%);
            -ms-transform: translate(-50%, -50%); /* for IE 9 */
            -webkit-transform: translate(-50%, -50%); /* for Safari */
            /* optional size in px or %: */
            width: 180px;
            height: 100px;
            border-style: solid;
            border-color: #92a8d1;
        }
    </style>
    <script language="javascript">

        function getRowIndex(el) {
            while ((el = el.parentNode) && el.nodeName.toLowerCase() !== 'tr');

            if (el)
                return el.rowIndex;
        }

        function ShowPPLeasingSelectionEdit(obj, _paymentMethodID) {
            if (_paymentMethodID == '2') {
                var hdnIndexSelectedGrid = document.getElementById("hdnIndexSelectedGrid");
                var idx = obj.parentNode.parentNode.rowIndex;
                //var idx = getRowIndex(obj);
                hdnIndexSelectedGrid.value = idx

                showPopUp('../General/../PopUp/PopUpLeasingCompany.aspx', '', 500, 760, LeasingSelectionEdit);
            }
        }

        function LeasingSelectionEdit(selectedLeasing) {
            var hdnIndexSelectedGrid = document.getElementById("hdnIndexSelectedGrid");
            var idx = hdnIndexSelectedGrid.value;
            var dtGrid = document.getElementById("dgReportSPKNationalEvent");

            var leasingName = dtGrid.rows[idx].getElementsByTagName("INPUT")[7];
            var leasingID = dtGrid.rows[idx].getElementsByTagName("INPUT")[8];

            var leasingParams = selectedLeasing.split(';');
            if (leasingID != undefined) {
                leasingID.value = leasingParams[0];
            }
            if (leasingName != undefined) {
                leasingName.value = leasingParams[1];
            }
        }

        function ShowPopUpNationalEvent() {
            showPopUp('../PopUp/PopUpEventNational.aspx', '', 430, 800, regNumberSelected);
        }

        function regNumberSelected(selectedRegNumber) {
            var txtRegNumber = document.getElementById("txtRegNumber");
            txtRegNumber.value = selectedRegNumber.split(";")[0];
            __doPostBack("txtRegNumber", "");
        }

        //function blurRegNumberText() {
        //    __doPostBack("txtRegNumber", "");
        //}

    </script>

    <script type="text/javascript">
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

                $('#dvLoading').hide();
            }

            hideAll();

            $('.waitLoad').change(function () {

                hideAll();

                switch ($(this).attr("id")) {
                    case "txtRegNumber":
                        $('#dvLoading').show();
                        break;
                }
            }); // end of function for clicking 
        });
    </script>

</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <table id="Table2" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage" colspan="2">EVENT NASIONAL - REPORT SPK STATUS</td>
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
                            <td class="titleField" style="width: 146px">Kode Event</td>
                            <td style="width: 2px">:</td>
                            <td class="titleField" valign="top">
                                <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$')" ID="txtRegNumber" AutoPostBack="true"
                                    onblur="omitSomeCharacter('txtRegNumber','<>?*%^():|\@#$;+=`~{}');" runat="server" class="waitLoad"
                                    ToolTip="Cari Kode Event" Width="128px"></asp:TextBox>
                                <asp:Label ID="lblPopUpRegNumberEvent" runat="server" Width="16px">
                                        <img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0">
                                </asp:Label>&nbsp;
                                <input id="hdnIndexSelectedGrid" type="hidden" value="" runat="server">
                                <input id="hdnNationalEventID" type="hidden" value="" runat="server">
                                &nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnDownload" runat="server" Text="Download"></asp:Button>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 146px"></td>
                            <td style="width: 2px"></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 146px">Nama Event</td>
                            <td style="width: 2px">:</td>
                            <td class="titleField" align="left">
                                <asp:Label ID="lblEventName" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 146px">Periode</td>
                            <td style="width: 2px">:</td>
                            <td>
                                <asp:Label ID="lblPeriodStartEvent" runat="server"></asp:Label>&nbsp;s.d.&nbsp;
                                <asp:Label ID="lblPeriodEndEvent" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 146px">Lokasi</td>
                            <td style="width: 2px">:</td>
                            <td>
                                <asp:Label ID="lblCityVenue" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 146px">Jumlah Dealer</td>
                            <td style="width: 2px">:</td>
                            <td>
                                <asp:Label ID="lblCountDealer" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 146px">Jumlah Salesforce</td>
                            <td style="width: 2px">:</td>
                            <td>
                                <asp:Label ID="lblCountSales" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 146px">SPK by Prom. Dept.</td>
                            <td style="width: 2px">:</td>
                            <td>
                                <asp:Label ID="txtSPKbyPromDept" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 146px">SPK by Dealer</td>
                            <td style="width: 2px">:</td>
                            <td>
                                <asp:Label ID="lblSPKbyDealer" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="width: 50%" valign="top">
                    <table id="Table3" cellspacing="1" cellpadding="2" width="100%" border="0">
                        <tr valign="top">
                            <td class="titleField" style="width: 146px"></td>
                            <td style="width: 2px"></td>
                            <td class="titleField" style="display:none">
                                <asp:DropDownList ID="ddlProspekSPK" runat="server" Width="140px" AutoPostBack="true"></asp:DropDownList>&nbsp;
                                <asp:Button ID="btnSearch" runat="server" Text="Cari" Width="60px"></asp:Button>&nbsp
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 146px"></td>
                            <td style="width: 2px"></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 146px">Target Prospek</td>
                            <td style="width: 2px">:</td>
                            <td>
                                <asp:Label ID="lblTargetProspek" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 146px">Target SPK</td>
                            <td style="width: 2px">:</td>
                            <td>
                                <asp:Label ID="lblTargetSPK" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 146px">Realisasi SPK</td>
                            <td style="width: 2px">:</td>
                            <td>
                                <asp:Label ID="lblRealisasiSPK" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 146px">SPK sudah DO</td>
                            <td style="width: 2px">:</td>
                            <td>
                                <asp:Label ID="lblSPKsudahDO" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 146px">SPK Outstanding</td>
                            <td style="width: 2px">:</td>
                            <td>
                                <asp:Label ID="lblSPKOutstanding" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 146px">Dealer Champion</td>
                            <td style="width: 2px">:</td>
                            <td>
                                <asp:Label ID="lblDealerChampion" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 146px">Dealer with lowest DO</td>
                            <td style="width: 2px">:</td>
                            <td>
                                <asp:Label ID="lblDealerwithlowestDO" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>

        <div id="dvLoading" runat="server" class="center" align="center">
            Loading. Please wait...<br />
            <br />
            <img src="../images/loader.gif" width="50px" height="50px" />
        </div>

        <div style="overflow: auto; width: 2450px">
            <hr />
        </div>
        <div id="div1" style="overflow: auto; height: 360px; width: 2450px">
            <asp:DataGrid ID="dgReportSPKNationalEvent" runat="server" Width="100%" BorderWidth="0px" BackColor="#CDCDCD" BorderColor="Gainsboro"
                CellPadding="3" CellSpacing="1" AutoGenerateColumns="False" ShowFooter="false" PageSize="25" AllowSorting="True" AllowCustomPaging="True" 
                AllowPaging="True" >
                <AlternatingItemStyle ForeColor="Black" BackColor="#FDF1F2"></AlternatingItemStyle>
                <ItemStyle ForeColor="Black" BackColor="#FFFFFF"></ItemStyle>
                <HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C" HorizontalAlign="Center"></HeaderStyle>
                <FooterStyle ForeColor="#4A3C8C" BackColor="#dedede"></FooterStyle>
                <Columns>
                    <asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
                        <HeaderStyle Width="3%" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
                    </asp:BoundColumn>
                    <asp:TemplateColumn HeaderText="No.">
                        <HeaderStyle Width="12px" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        <ItemTemplate>
                        </ItemTemplate>
                        <EditItemTemplate>
                        </EditItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Event">
                        <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle Width="10%" HorizontalAlign="Left"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblEventName" runat="server"></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:Label ID="lblEEventName" runat="server"></asp:Label>
                        </EditItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="No SPK">
                        <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle Width="4%" HorizontalAlign="Center"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblSPKNumber" runat="server">
                            </asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:Label ID="lblESPKNumber" runat="server"></asp:Label>
                        </EditItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Tanggal SPK">
                        <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle Width="3%" HorizontalAlign="Center"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblDealerSPKDate" runat="server">
                            </asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <cc1:IntiCalendar ID="icDealerSPKDate" runat="server" 
                                TextBoxWidth="60" Value='<%#DataBinder.Eval(Container, "DataItem.DealerSPKDate")%>'></cc1:IntiCalendar>
                        </EditItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Nama Konsumen">
                        <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle Width="10%" HorizontalAlign="Left"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblCustomerName" runat="server">
                            </asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtECustomerName" runat="server" Width="180px" MaxLength="100" TabIndex="6"
                                Text='<%#DataBinder.Eval(Container, "DataItem.CustomerName")%>' />
                        </EditItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Nama Dealer">
                        <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle Width="8%" HorizontalAlign="Left"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblDealerName" runat="server">
                            </asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:Label ID="lblEDealerName" runat="server"></asp:Label>
                        </EditItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Kode Dealer">
                        <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle Width="3%" HorizontalAlign="Center"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblDealerCode" runat="server">
                            </asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:Label ID="lblEDealerCode" runat="server"></asp:Label>
                        </EditItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Nama Sales">
                        <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle Width="8%" HorizontalAlign="Left"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblSalesmanName" runat="server">
                            </asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:Label ID="lblESalesmanName" runat="server"></asp:Label>
                        </EditItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Kode Sales">
                        <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle Width="4%" HorizontalAlign="Center"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblSalesmanCode" runat="server">
                            </asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:Label ID="lblESalesmanCode" runat="server"></asp:Label>
                        </EditItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Shift">
                        <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle Width="2%" HorizontalAlign="center"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblShift" runat="server">
                            </asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEShift" Style="text-align: center" runat="server"
                                Text='<%#DataBinder.Eval(Container, "DataItem.Shift")%>' onkeypress="return NumericOnlyWith(event,'');"
                                onkeyup="pic(this,this.value,'9999999999','N')" Width="30px" />
                        </EditItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Kode Kendaraan">
                        <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle Width="6%" HorizontalAlign="Left"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblVehicleTypeCategory" runat="server">
                            </asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:Label ID="lblEVehicleTypeCategory" runat="server"></asp:Label>
                        </EditItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Nama Kendaraan">
                        <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle Width="10%" HorizontalAlign="Left"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblVehicleTypeName" runat="server">
                            </asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:Label ID="lblEVehicleTypeName" runat="server"></asp:Label>
                        </EditItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Warna">
                        <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle Width="7%" HorizontalAlign="Left"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblVechileColorName" runat="server">
                            </asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:Label ID="lblEVechileColorName" runat="server"></asp:Label>
                        </EditItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Tahun Produksi">
                        <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle Width="3%" HorizontalAlign="Center"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblAssyYear" runat="server">
                            </asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEAssyYear" Style="text-align: center" runat="server" MaxLength="4"
                                Text='<%#DataBinder.Eval(Container, "DataItem.AssyYear")%>' onkeypress="return NumericOnlyWith(event,'');" Width="35px" />
                        </EditItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Tanggal Faktur">
                        <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle Width="4%" HorizontalAlign="Center"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblFakturDate" runat="server">
                            </asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:Label ID="lblEFakturDate" runat="server"></asp:Label>
                        </EditItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Nomor Faktur">
                        <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle Width="7%" HorizontalAlign="Center"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblFakturNumber" runat="server">
                            </asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:Label ID="lblEFakturNumber" runat="server"></asp:Label>
                        </EditItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Qty">
                        <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle Width="2%" HorizontalAlign="Center"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblQuantity" runat="server">
                            </asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEQuantity" Style="text-align: right" runat="server"
                                Text='<%#DataBinder.Eval(Container, "DataItem.Quantity")%>' onkeypress="return NumericOnlyWith(event,'');"
                                onkeyup="pic(this,this.value,'9999999999','N')" Width="30px" />
                        </EditItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Tanda Jadi">
                        <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle Width="5%" HorizontalAlign="Right"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblDownPayment" runat="server">
                            </asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEDownPayment" Style="text-align: right" TabIndex="4" Text='<%# Format(DataBinder.Eval(Container, "DataItem.DownPayment"), "#,##0")%>'
                                runat="server" onkeypress="return NumericOnlyWith(event,'');" onkeyup="pic(this,this.value,'9999999999','N')" Width="70px" />
                        </EditItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Tipe Pembiayaan">
                        <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle Width="10%" HorizontalAlign="Left"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblPaymentMethod" runat="server">
                            </asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlEPaymentMethod" TabIndex="1" runat="server" AutoPostBack="true" 
                                OnSelectedIndexChanged="ddlEPaymentMethod_SelectedIndexChanged">
                            </asp:DropDownList>
                        </EditItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Lembaga Pembiayaan">
                        <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle Width="15%" HorizontalAlign="Left"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblLeasingName" runat="server">
                            </asp:Label>
                            <asp:Label ID="lblLeasingID" runat="server" style="display:none"></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtELeasingName" class="txtELeasingName" Text='<%# DataBinder.Eval(Container, "DataItem.LeasingName")%>'
                                runat="server" Width="110px"></asp:TextBox>
                            &nbsp;<asp:Label ID="lblELeasingName" runat="server">
								<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:Label>
                            <asp:TextBox id="hfELeasingID" class="hfLeasingGrid" style="display:none" runat="server" Width="100" ></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Remarks">
                        <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle Width="15%" HorizontalAlign="Left"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblRemarks" runat="server">
                            </asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtERemarks" runat="server" Width="100px" TabIndex="6"  TextMode="MultiLine" Rows="3"
                                Text='<%#DataBinder.Eval(Container, "DataItem.Remarks")%>' />
                        </EditItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Di Input Oleh">
                        <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle Width="10%" HorizontalAlign="Left"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblInputedBy" runat="server">
                            </asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:Label ID="lblEInputedBy" runat="server">
                            </asp:Label>
                        </EditItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderStyle Width="8%" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle HorizontalAlign="center"></ItemStyle>
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnEdit" CausesValidation="False" CommandName="edit" Text="Ubah" runat="server">
                                        <img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
                            <asp:LinkButton ID="lbtnDelete" CausesValidation="False" OnClientClick="return confirm('Anda yakin mau hapus data ini ?');" 
                                CommandName="delete" Text="Hapus" runat="server">
                                        <img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:LinkButton ID="lbtnSave" CommandName="save" Text="Simpan" runat="server"  OnClientClick="return confirm('Anda yakin mau simpan data ini ?');" >
                                        <img src="../images/simpan.gif" border="0" alt="Simpan"></asp:LinkButton>
                            <asp:LinkButton ID="lbtnCancel" runat="server" CausesValidation="True" CommandName="cancel" Text="Batal">
										<img src="../images/batal.gif" border="0" alt="Batal"></asp:LinkButton>
                        </EditItemTemplate>
                    </asp:TemplateColumn>
                </Columns>
                <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
            </asp:DataGrid>
        </div>
        <div style="overflow: auto; width: 2450px">
            <hr />
        </div>
    </form>
</body>
</html>
