<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmProgramDiskonReguler.aspx.vb" Inherits=".FrmProgramDiskonReguler" %>

<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>Program Diskon Reguler</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>

    <script language="javascript" src="../WebResources/FormFunctions.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script type="text/javascript" src="../WebResources/jquery-1.7.2.min.js"></script>
    <script type="text/javascript">
        $("[src*=plus]").live("click", function () {
            $(this).closest("tr").after("<tr><td></td><td colspan = '999'>" + $(this).next().html() + "</td></tr>")
            $(this).attr("src", "../images/minus.gif");
        });
        $("[src*=minus]").live("click", function () {
            $(this).attr("src", "../images/plus.gif");
            $(this).closest("tr").next().remove();
        });
    </script>
    <script language="javascript">
        function getElement(tipeElement, IdElement) {
            var selectbox;
            var inputs = document.getElementsByTagName(tipeElement);

            for (var i = 0; i < inputs.length; i++) {
                if (inputs[i].id.indexOf(IdElement) > -1) {
                    selectbox = inputs[i]
                    break;
                }
            }
            return selectbox;
        }
        var strDealaerBranch = '../PK/../PopUp/PopUpDealerBranchMultipleSelection.aspx';

        function ShowPPDealerBranchSelection() {

            var hdnTitle = document.getElementById('hdnTitle');

            var uri = strDealaerBranch;

            if (hdnTitle.value == "MKS" || 1 == 1) {

                var txtDealerSelection = document.getElementById("txtKodeDealer");
                if (txtDealerSelection.value != '') {
                    uri = uri + "?DealerCode=" + txtDealerSelection.value;
                }

            }

            showPopUp(uri, '', 500, 760, DealerBranchSelection);
        }

        function DealerBranchSelection(selectedDealer) {

            var txtDealerSelection = document.getElementById("txtDealerBranchCode");
            txtDealerSelection.value = selectedDealer;

        }

        function ShowPPDealerSelection() {
            showPopUp('../General/../PopUp/PopUpDealerSelection.aspx', '', 500, 760, DealerSelection);
        }

        function DealerSelection(selectedDealer) {
            var txtDealerSelection = document.getElementById("txtKodeDealer");
            txtDealerSelection.value = selectedDealer;
        }

        function ViewDailyPKFlow()
        { }
        function ValidateData() {
            var ddl = document.getElementById("ddlRencanaPenebusan");
            var txt = document.getElementById("txtPKNumber");
            var lbl = document.getElementById("lblValidator");
            var rsl = true;

            lbl.style.visibility = "hidden";
            if (ddl.selectedIndex == 0) {
                if (txt.value == "") {
                    lbl.style.visibility = "visible";
                    rsl = false;
                }
            }
            //alert(rsl);
            return rsl;
        }
        setTimeout(function () {
            generateCheckBoxClick();
        }, 2000);
        function CheckAll(aspCheckBoxID) {
            var selectbox = getElement('input', 'chkbxAll')
            var inputs = document.getElementsByTagName("input");
            var stringlist = ""
            for (var i = 0; i < inputs.length; i++) {
                if (inputs[i].id.indexOf(aspCheckBoxID) > -1) {
                    if (inputs[i].type == 'checkbox') {
                        if (selectbox.checked == true) {
                            inputs[i].checked = "checked"

                        }

                        else
                            inputs[i].checked = ""
                    }
                }
            }

            var table = document.getElementById('dgMain');
            var exitsno = '';
            for (i = 1; i < table.rows.length - 1; i++) {

                if (table.rows[i].cells[1].getElementsByTagName("input")[0].checked == true)
                    stringlist = stringlist + ";" + i

            }

            var arrayCheck = getElement('input', 'arrayCheck')
            if (selectbox.checked == true) {
                arrayCheck.value = stringlist
            } else arrayCheck.value = ""
        }
        function generateCheckBoxClick() {
            var inputs = document.getElementsByTagName("input");
            var stringlist = ""
            for (var i = 0; i < inputs.length; i++) {

                if (inputs[i].id.indexOf('cbxDetail') > -1) {
                    if (inputs[i].type == 'checkbox') {

                        inputs[i].onclick = function () { setValueCheckBox(); };
                    }
                }
            }
        }

        function clearVechileTypeGeneralID(obj) {
            var hdnVechileTypeGeneralID = document.getElementById("hdnVechileTypeGeneralID");
            var hdnVechileTypeGeneralName = document.getElementById("hdnVechileTypeGeneralName");
            if (trim(obj.value) == '') {
                hdnVechileTypeGeneralID.value = '';
                hdnVechileTypeGeneralName.value = '';
            }
        }

        function ShowPPTipeGeneralSelection() {
            var ddlModel = document.getElementById("ddlModel");
            var selectedModelValue = ddlModel.options[ddlModel.selectedIndex].value;
            if (selectedModelValue == '-1')
            {
                alert('Silahkan Pilih Model dahulu');
                return;
            }
            showPopUp('../DiscountProposal/../PopUp/PopUpVechileTypeGeneral.aspx?SubCategoryVehicleID=' + selectedModelValue, '', 500, 500, TipeGeneralSelection);
        }

        function TipeGeneralSelection(selectedTipeGeneral) {
            var txtVechileTypeGeneralName = document.getElementById("txtVechileTypeGeneralName");
            var hdnVechileTypeGeneralID = document.getElementById("hdnVechileTypeGeneralID");
            var hdnVechileTypeGeneralName = document.getElementById("hdnVechileTypeGeneralName");
            var txtModelYear = document.getElementById("txtModelYear");
            var txtAssyYear = document.getElementById("txtAssyYear");
            var hdnModelYear = document.getElementById("hdnModelYear");
            var hdnAssyYear = document.getElementById("hdnAssyYear");
            
            var arrVechileTypeGeneralID = '';
            var arrVechileTypeGeneralName = '';
            var string_array = selectedTipeGeneral.split("|");
            for (i = 0; i < string_array.length; i++) {
                var data = string_array[i].split(";");
                if (arrVechileTypeGeneralID == '') {
                    arrVechileTypeGeneralID = data[0];
                }
                else {
                    arrVechileTypeGeneralID = arrVechileTypeGeneralID + ';' + data[0];
                }
                if (arrVechileTypeGeneralName == '') {
                    arrVechileTypeGeneralName = data[1];
                }
                else {
                    arrVechileTypeGeneralName = arrVechileTypeGeneralName + ';' + data[1];
                }
            }

            hdnVechileTypeGeneralID.value = arrVechileTypeGeneralID;
            txtVechileTypeGeneralName.value = arrVechileTypeGeneralName;
            hdnVechileTypeGeneralName.value = arrVechileTypeGeneralName;
            txtModelYear.value = '';
            hdnModelYear.value = '';
            txtAssyYear.value = '';
            hdnAssyYear.value = '';

            if (navigator.appName == "Microsoft Internet Explorer") {
                txtVechileTypeGeneralName.focus();
                txtVechileTypeGeneralName.blur();
            }
            else {
                txtVechileTypeGeneralName.onchange();
            }
        }

        function ShowPPModelYearSelection() {
            var hdnVechileTypeGeneralID = document.getElementById("hdnVechileTypeGeneralID");
            var vechileTypeGeneralIDValue = hdnVechileTypeGeneralID.value;
            if (vechileTypeGeneralIDValue == '') {
                alert('Silahkan Pilih Tipe dahulu');
                return;
            }
            showPopUp('../PopUp/PopUpModelYearMultipleSelection.aspx?VechileTypeGeneralID=' + vechileTypeGeneralIDValue, '', 450, 400, ModelYearSelection)
        }

        function ModelYearSelection(selectedModelYear) {
            var data = selectedModelYear;
            var txtModelYear = document.getElementById("txtModelYear");
            var hdnModelYear = document.getElementById("hdnModelYear");
            txtModelYear.value = data;
            hdnModelYear.value = data;

            var txtAssyYear = document.getElementById("txtAssyYear");
            var hdnAssyYear = document.getElementById("hdnAssyYear");
            txtAssyYear.value = '';
            hdnAssyYear.value = '';

            if (navigator.appName == "Microsoft Internet Explorer") {
                txtModelYear.focus();
                txtModelYear.blur();
            }
            else {
                txtModelYear.onchange();
            }
        }

        function ShowPPProductionYearSelection() {
            var hdnVechileTypeGeneralID = document.getElementById("hdnVechileTypeGeneralID");
            var vechileTypeGeneralIDValue = hdnVechileTypeGeneralID.value;
            if (vechileTypeGeneralIDValue == '') {
                alert('Silahkan Pilih Tipe dahulu');
                return;
            }
            var hdnModelYear = document.getElementById("hdnModelYear");
            var hdnModelYearValue = hdnModelYear.value;
            if (hdnModelYearValue == '') {
                alert('Silahkan Pilih Tahun Model dahulu');
                return;
            }
            showPopUp('../PopUp/PopUpProductionYearMultipleSelection.aspx?VechileTypeGeneralID=' + vechileTypeGeneralIDValue + '&ModelYear=' + hdnModelYearValue, '', 450, 400, ProductionYearSelection)
        }

        function ProductionYearSelection(selectedProductionYear) {
            var data = selectedProductionYear;
            var txtAssyYear = document.getElementById("txtAssyYear");
            var hdnAssyYear = document.getElementById("hdnAssyYear");
            txtAssyYear.value = data;
            hdnAssyYear.value = data;

            if (navigator.appName == "Microsoft Internet Explorer") {
                txtAssyYear.focus();
                txtAssyYear.blur();
            }
            else {
                txtAssyYear.onchange();
            }
        }

    </script>
</head>
<body>
    <form id="Form1" runat="server">
        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
            <td class="titlePage">DISCOUNT PROPOSAL - Program Diskon Reguler</td>
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
                <td style="width: 40%" valign="top">
                    <table style="width: 100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td class="titleField" width="30%">
                                <asp:Label ID="lblPeriode" runat="server">Periode</asp:Label></td>
                            <td style="padding-right:5px">
                                <asp:Label ID="Label5" runat="server">:</asp:Label></td>
                            <td>
                                <table>
                                    <tr>
                                        <td><asp:CheckBox ID="chkPeriode" Runat="server"></asp:CheckBox></td>
                                        <td>
                                            <cc1:IntiCalendar ID="icValidFrom" runat="server" TextBoxWidth="70"></cc1:IntiCalendar></td>
                                        <td>&nbsp;s.d&nbsp;</td>
                                        <td>
                                            <cc1:IntiCalendar ID="icValidTo" runat="server" TextBoxWidth="70"></cc1:IntiCalendar></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="titlefield" width="30%">Program Based</td>
                            <td style="padding-right:1px">
                                <asp:Label ID="Label3" runat="server">:</asp:Label></td>
                            <td>
                                <asp:DropDownList ID="ddlProgramBased" runat="server" Width="150px"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="titlefield" width="30%">Program</td>
                            <td style="padding-right:1px">
                                <asp:Label ID="Label2" runat="server">:</asp:Label></td>
                            <td>
                                <asp:DropDownList ID="ddlDiscountCategory" runat="server" Width="150px"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="titlefield" width="30%">Jumlah Diskon</td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="txtDiscountAmount" Style="text-align: right" runat="server"
                                    onkeypress="return NumericOnlyWith(event,'');" onkeyup="pic(this,this.value,'9999999999','N')" Width="150px" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="width: 50%" valign="top">
                    <table width="100%" cellpadding="0" cellspacing="0">
                        <tr valign="top">
                            <td class="titlefield" width="30%">Model</td>
                            <td style="padding-right:5px">:</td>
                            <td>
                                <asp:DropDownList ID="ddlModel" runat="server" Width="150px" AutoPostBack="True"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr valign="top">
                            <td class="titlefield" width="30%">Tipe</td>
                            <td style="padding-right:5px">:</td>
                            <td>
                                <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$')" ID="txtVechileTypeGeneralName" Enabled="false"
                                    onblur="omitSomeCharacter('txtVechileTypeGeneralName','<>?*%$');clearVechileTypeGeneralID(this);" TextMode="MultiLine" Rows="2"
                                    runat="server" Width="200px"></asp:TextBox>
                                <asp:Label ID="lblPopUpVechileTypeGeneral" runat="server" Width="16px"> 
                                    <img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0"> </asp:Label>&nbsp;&nbsp;
                                <asp:HiddenField ID="hdnVechileTypeGeneralName" runat="server" />
                                <asp:HiddenField ID="hdnVechileTypeGeneralID" runat="server" />
                            </td>
                        </tr>
                        <tr valign="top">
                            <td class="titlefield" width="30%">Tahun Model</td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="txtModelYear" Style="text-align: left" runat="server" MaxLength="4" Enabled="false"
                                    onkeypress="return NumericOnlyWith(event,'');" Width="200px" TextMode="MultiLine" Rows="2" />
                                <asp:Label ID="lblPopUpModelYear" runat="server" Width="16px"> 
                                    <img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0"> </asp:Label>&nbsp;&nbsp;
                                <asp:HiddenField ID="hdnModelYear" runat="server" />
                            </td>
                        </tr>
                        <tr valign="top">
                            <td class="titlefield" width="30%">Tahun Perakitan</td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="txtAssyYear" Style="text-align: left" runat="server" MaxLength="4"  Enabled="false"
                                    onkeypress="return NumericOnlyWith(event,'');" Width="200px" TextMode="MultiLine" Rows="2" />
                                <asp:Label ID="lblPopUpAssyYear" runat="server" Width="16px"> 
                                    <img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0"> </asp:Label>&nbsp;&nbsp;
                                <asp:HiddenField ID="hdnAssyYear" runat="server" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr style="text-align:center; padding-top:15px">
                <td colspan="2">
                    <hr />
                    <asp:Button ID="btnCari" runat="server" Text="Cari" Width="75px"></asp:Button>&nbsp&nbsp
                    <asp:Button ID="btnSimpan" runat="server" Text="Simpan" OnClientClick="return confirm('Anda yakin mau simpan?');"  Width="100px"></asp:Button>&nbsp&nbsp
                    <asp:Button ID="btnBatal" runat="server" Text="Batal" Width="100px"></asp:Button>
                </td>
            </tr>
        </table>
        <br />
        <asp:Panel ID="Panel2" runat="server">
            <div>
                <asp:DataGrid ID="dgMain" runat="server" Width="100%" AllowPaging="True" AllowSorting="True"
                    PageSize="25" AllowCustomPaging="True" AutoGenerateColumns="False" BackColor="#CDCDCD" BorderColor="Gainsboro" CellSpacing="1" BorderWidth="0px"
                    CellPadding="3" DataKeyField="ID">
                    <AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
                    <ItemStyle BackColor="White"></ItemStyle>
                    <HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Blue"></HeaderStyle>
                    <Columns>
                        <asp:TemplateColumn HeaderText="ID" Visible="false">
                            <HeaderStyle Width="0px" CssClass="titleTableSales hiddencol"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center" CssClass="hiddencol"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblID" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="No">
                            <HeaderStyle Width="4%" CssClass="titleTableSales"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblRowNum" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Periode">
                            <HeaderStyle Width="15%" CssClass="titleTableSales"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblPeriode" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Program Based">
                            <HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblProgramBased" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Program">
                            <HeaderStyle Width="20%" CssClass="titleTableSales"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblKategoriProgram" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Jumlah Diskon">
                            <HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblJumlahDiskon" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Model">
                            <HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblModel" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Tipe">
                            <HeaderStyle Width="20%" CssClass="titleTableSales"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblTipe" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Tahun Model">
                            <HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblModelYear" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Tahun Perakitan">
                            <HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblAssyYear" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn>
                            <HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
                            <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkbtnEdit" runat="server" Width="20px" Text="Ubah" CausesValidation="False" CommandName="Edit" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
															<img alt="Ubah" src="../images/edit.gif" border="0"></asp:LinkButton>
                                <asp:LinkButton ID="lnkbtnDelete" runat="server" OnClientClick="return confirm('Anda yakin mau hapus?');" Width="20px" Text="Hapus" CausesValidation="False" CommandName="Delete" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.ID")%>'>
															<img alt="Hapus"  src="../images/trash.gif"
																border="0"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                    </Columns>
                    <PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
                </asp:DataGrid>
            </div>
        </asp:Panel>
    </form>
    <script type="text/javascript" language="javascript">
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
