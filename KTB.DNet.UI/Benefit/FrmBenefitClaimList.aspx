<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmBenefitClaimList.aspx.vb" Inherits="FrmBenefitClaimList" SmartNavigation="False" %>

<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>FrmAplikasiHeader</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
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
        function ShowPPDealerSelection() {
            //showPopUp('../SparePart/../PopUp/PopUpDealerSelectionOne.aspx', '', 500, 760, DealerSelection);

            showPopUp('../General/../Benefit/PopUpDealerSelectionOneBenefit.aspx', '', 500, 760, DealerSelection);
        }
        function DealerSelection(selectedDealer) {
            var tempParam = selectedDealer.split(';');
            var txtDealer = document.getElementById("txtKodeName");
            txtDealer.value = tempParam[0];
        }

        var tempPilihanClaim = ''

        function showhideLeasing() {
            var selectPilihanClaim = getElement('select', 'ddlPilihanClaim');
            var selectLeasing = getElement('select', 'ddlLeasing');

            var result = selectPilihanClaim.value.split(';');

            //  if (selectPilihanClaim.options[selectPilihanClaim.selectedIndex].text.toLowerCase().indexOf('leas') > -1)
            if (result[1] == "1")
                selectLeasing.style.display = '';
            else
                selectLeasing.style.display = 'none';

            if (tempPilihanClaim != "" && tempPilihanClaim != selectPilihanClaim.value) {
                document.getElementById('txtIdDetailMaster').value = '';
                document.getElementById('txtIdDetailMasterShow').value = '';
                tempPilihanClaim = selectPilihanClaim.value;
            }
        }


        function generateCheckBoxClick() {
            var inputs = document.getElementsByTagName("input");
            var stringlist = ""
            for (var i = 0; i < inputs.length; i++) {

                if (inputs[i].id.indexOf('cbAllGrid') > -1) {
                    if (inputs[i].type == 'checkbox') {

                        inputs[i].onclick = function () { setValueCheckBox(); };
                    }
                }
            }
        }

        function setValueCheckBox() {
            var table = document.getElementById('dgTable');
            var stringlist = '';
            for (i = 1; i < table.rows.length - 1; i++) {
                if (table.rows[i].cells[1].getElementsByTagName("input")[0] != undefined) {
                    if (table.rows[i].cells[1].getElementsByTagName("input")[0].checked == true)
                        stringlist = stringlist + ";" + i
                }


            }
            var arrayCheck = getElement('input', 'arrayCheck')

            arrayCheck.value = stringlist

        }

        function CheckAllDetail(aspCheckBoxID) {
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

            var table = document.getElementById('dgGridDetil');
            var exitsno = '';
            for (i = 1; i < table.rows.length - 1; i++) {

                //stringlist = stringlist + ";" + table.rows[i].cells[0].getElementsByTagName("input")[0].checked;
                if (table.rows[i].cells[1].getElementsByTagName("input")[0].checked == true)
                    stringlist = stringlist + ";" + i

            }

            var arrayCheck = getElement('input', 'arrCheckDetail')
            if (selectbox.checked == true) {
                arrayCheck.value = stringlist
            } else arrayCheck.value = ""
        }

        function CheckAll(aspCheckBoxID) {
            var selectbox = getElement('input', 'chkAllItems')
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

            var table = document.getElementById('dgTable');
            var exitsno = '';
            for (i = 1; i < table.rows.length - 1; i++) {

                //stringlist = stringlist + ";" + table.rows[i].cells[0].getElementsByTagName("input")[0].checked;
                if (table.rows[i].cells[1].getElementsByTagName("input")[0].checked == true)
                    stringlist = stringlist + ";" + i

            }

            var arrayCheck = getElement('input', 'arrayCheck')
            if (selectbox.checked == true) {
                arrayCheck.value = stringlist
            } else arrayCheck.value = ""
        }

        setTimeout(function () {
            generateCheckBoxClick();

            generateOnChange();

            showhideLeasing();
        }, 2000);

        function generateOnChange() {


            getElement('input', 'txtKodeName').onkeyup = function () {

                document.getElementById('txtIdDetailMaster').value = ''
                document.getElementById('txtIdDetailMasterShow').value = ''
            }

            getElement('select', 'ddlLeasing').onchange = function () {

                document.getElementById('txtIdDetailMaster').value = ''
                document.getElementById('txtIdDetailMasterShow').value = ''
            }
        }

        function GetSelectedValue() {
            var table;
            
            var value1 = ""; var valueShow1 = "";
            var bcheck = false;
            table = document.getElementById('dgGridDetil');
            var val = '';

            var value = ""; var valueshow = ""
            for (i = 1; i < table.rows.length; i++) {

                var radioBtn = table.rows[i].cells[0].getElementsByTagName('INPUT')[0];
                if (radioBtn != null && radioBtn.checked) {
                    if (navigator.appName == "Microsoft Internet Explorer") {
                        value1 = table.rows[i].cells[1].innerText;
                        valueshow1 = table.rows[i].cells[4].innerText;

                        bcheck = true;
                    }
                    else {
                        value1 = replace(table.rows[i].cells[1].getElementsByTagName("span")[0].innerHTML, ' ', '');
                        valueshow1 = replace(table.rows[i].cells[4].getElementsByTagName("span")[0].innerHTML, ' ', '');

                        bcheck = true;
                    }
                    if (bcheck) {
                        value = value + value1 + ',';
                        if (valueshow.indexOf(valueshow1 + ',') == -1) {
                            valueshow = valueshow + valueshow1 + ',';
                        }
                    }
                    //break;
                }
            }
            valueshow = valueshow.slice(0, -1);
            value = value.slice(0, -1);

            if (bcheck) {

                tempPilihanClaim = getElement('select', 'ddlPilihanClaim').value

                document.getElementById('txtIdDetailMaster').value = value

                document.getElementById('txtIdDetailMasterShow').value = valueshow


                closepanel2()
            }
            else {
                alert("Silahkan pilih ");
            }
        }

        function closepanel2() {
            document.getElementById('PanelJV').style.display = 'none'
            document.getElementById('Panel2').style.display = 'none'
            document.getElementById('Panel1').style.display = ''
        }

        function showhideLeasingJV() {
            var selectPilihanClaim = getElement('select', 'ddlTipeAccountGrid');
            var selectLeasing = getElement('select', 'ddlVendorGrid');
            var selectAmount = getElement('input', 'txtAmountGrid');

            var result = selectPilihanClaim.value;

            //  if (selectPilihanClaim.options[selectPilihanClaim.selectedIndex].text.toLowerCase().indexOf('leas') > -1)
            if (result == "V")
                selectLeasing.style.display = '';
            else
                selectLeasing.style.display = 'none';

            if (result == "O")
                selectAmount.disabled = true;
            else
                selectAmount.disabled = false;
        }

       
    </script>
    <style>
        .hiddencol {
            display: none;
        }
    </style>
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <asp:Panel ID="Panel1" runat="server">
            <table id="Table2" cellspacing="5" cellpadding="0" width="100%" border="0">
                <tr>
                    <td class="titlePage" style="height: 17px" colspan="2">SALES CAMPAIGN - Daftar Claim</td>
                </tr>
                <tr>
                    <td background="../images/bg_hor.gif" colspan="2" height="1">
                        <img height="1" src="../images/bg_hor.gif" border="0"></td>
                </tr>
                <tr>
                    <td style="height: 6px" colspan="2" height="6">
                        <img height="1" src="../images/dot.gif" border="0"></td>
                </tr>
                <tr>
                    <td>
                        <table style="width: 100%">
                            <tr>
                                <td style="width: 50%; vertical-align:top">
                                    <table style="width: 100%">
                                        <tr>
                                            <td class="titleField" width="30%">Dealer&nbsp;</td>
                                            <td>&nbsp; 
                                            </td>
                                            <td>
                                                <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" ID="txtKodeName" onblur="omitSomeCharacter('txtKodeName','<>?*%$;')"
                                                    runat="server" Width="242px"></asp:TextBox>
                                                &nbsp;<asp:Label ID="lblPopUpDealer" runat="server" Width="16px">
							        <img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0"></asp:Label>
                                                <asp:Label ID="lblDelerSession" runat="server" Text="Label"></asp:Label>
                                            </td>
                                            <td></td>

                                        </tr>

                                        <tr>
                                            <td class="titleField" width="30%">Pilihan Claim&nbsp;</td>
                                            <td>&nbsp; 
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlPilihanClaim" runat="server"></asp:DropDownList>

                                                <asp:DropDownList ID="ddlLeasing" runat="server"></asp:DropDownList>
                                            </td>
                                            <td>
                                                <!-- <asp:CheckBox ID="CheckBox2" runat="server" Text="" /> -->
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="titleField" width="30%">No Surat&nbsp;</td>
                                            <td>&nbsp; 
                                            </td>
                                            <td>
                                                <asp:HiddenField ID="txtIdDetailMaster" runat="server" />
                                                <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" ID="txtIdDetailMasterShow" onblur="omitSomeCharacter('txtIdDetailMasterShow','<>?*%$;')"
                                                    runat="server" Width="242px"></asp:TextBox>
                                                <asp:Button ID="btnRefClaim" runat="server" Text="Ref Claim" Width="60px" UseSubmitBehavior="False"></asp:Button>

                                            </td>
                                            <td>
                                                <%--<asp:TextBox runat="server" ID="txtShowIdDetail"></asp:TextBox>--%>
                                            </td>

                                        </tr>

                                        <tr>
                                            <td class="titleField" width="30%">Tanggal Claim&nbsp;</td>
                                            <td>
                                                <asp:CheckBox ID="cbDateClaim" runat="server" />
                                            </td>
                                            <td colspan="2">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <cc1:IntiCalendar ID="icDateClaim" runat="server" TextBoxWidth="60"></cc1:IntiCalendar>
                                                        </td>
                                                        <td>s/d</td>
                                                        <td><cc1:IntiCalendar ID="icDateClaimTo" runat="server" TextBoxWidth="60"></cc1:IntiCalendar></td>
                                                    </tr>
                                                </table>
                                                
                                                
                                            </td>

                                        </tr>

                                         <tr>
                                            <td class="titleField" width="30%">Tanggal Rencana Pembayaran&nbsp;</td>
                                            <td>
                                                <asp:CheckBox ID="cbDateBayar" runat="server" />
                                            </td>
                                            <td colspan="2">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <cc1:IntiCalendar ID="icDateBayarFrom" runat="server" TextBoxWidth="60"></cc1:IntiCalendar>
                                                        </td>
                                                        <td>s/d</td>
                                                        <td><cc1:IntiCalendar ID="icDateBayarTo" runat="server" TextBoxWidth="60"></cc1:IntiCalendar></td>
                                                    </tr>
                                                </table>
                                                
                                                
                                            </td>

                                        </tr>
                                        
                                        <tr>
                                            <td class="titleField" width="30%">No Claim Reg&nbsp;</td>
                                            <td>&nbsp; 
                                            </td>
                                            <td colspan="4">
                                                <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" ID="txtNoClaim" onblur="omitSomeCharacter('txtNoClaim','<>?*%$;')"
                                                    runat="server" Width="242px"></asp:TextBox>
                                            </td>

                                        </tr>

                                        <tr>
                                            <td class="titleField" width="30%">No Rangka&nbsp;</td>
                                            <td>&nbsp; 
                                            </td>
                                            <td colspan="4">
                                                <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" ID="txtNoRangka" onblur="omitSomeCharacter('txtNoRangka','<>?*%$;')"
                                                    runat="server" Width="242px"></asp:TextBox>
                                            </td>

                                        </tr>

                                         <tr>
                                            <td class="titleField" width="30%">Status Transfer&nbsp;</td>
                                            <td>&nbsp; 
                                            </td>
                                            <td colspan="4">
                                               <asp:DropDownList ID="ddlStatusTransfer" runat="server">
                                                    </asp:DropDownList>
                                            </td>

                                        </tr>

                                          <tr>
                                            <td class="titleField" width="30%">Status Upload&nbsp;</td>
                                            <td>&nbsp; 
                                            </td>
                                            <td colspan="4">
                                               <asp:DropDownList ID="ddlStatusUpload" runat="server">
                                                    </asp:DropDownList>
                                            </td>

                                        </tr>

                                        <tr>
                                            <td class="titleField" width="30%">Kategori Kendaraan&nbsp;</td>
                                            <td>&nbsp; 
                                            </td>
                                            <td colspan="4">
                                               <asp:DropDownList ID="ddlKategori" runat="server">
                                                    </asp:DropDownList>
                                            </td>

                                        </tr>

                                         <tr>
                                            <td class="titleField" width="30%">Status&nbsp;</td>
                                            <td>&nbsp; 
                                            </td>
                                            <td colspan="4">
                                               <asp:DropDownList ID="ddlStatus" runat="server">
                                                    </asp:DropDownList>
                                            </td>

                                        </tr>

                                    </table>
                                </td>
                                <td style="width: 50%; vertical-align:top">
                                    <asp:Panel ID="pnlbtnsimpan" runat="server">
                                        <table>
                                           

                                            <tr>
                                                <td class="titleField" width="50%">Tanggal Pembayaran&nbsp;</td>
                                                <td>
                                                    <cc1:IntiCalendar ID="icDateBayar" runat="server" TextBoxWidth="60"></cc1:IntiCalendar>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="titleField" width="50%">Accrued Month&nbsp;</td>
                                                <td>
                                                    
                                                    <asp:DropDownList ID="ddlAccuered" runat="server">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" align="center">
                                                    <asp:Button ID="btnSimpan" runat="server" Text="Simpan" Width="60px"></asp:Button>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                        </td>
                </tr>
                <tr>
                    <td style="align-items:left; text-align:left">
                        <asp:Button ID="btnCari" runat="server" Text="Cari" Width="60px" CausesValidation="False"></asp:Button>
                        <asp:Button ID="btnTambah" runat="server" Text="Tambah" Width="60px" Visible="false"></asp:Button>
                        
                        &nbsp;
                             <asp:Button ID="btnDownload" runat="server" Text="Download" Width="60px" CausesValidation="False"></asp:Button>
                        <asp:HiddenField ID="arrayCheck" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <table id="Table1" cellspacing="1" cellpadding="2" width="100%" border="0">
                            <tr>
                                <td valign="top" colspan="6">
                                    <div id="div1" >
                                        <asp:DataGrid ID="dgTable" runat="server" Width="100%" AllowPaging="True" AllowSorting="True"
                                            PageSize="25" AllowCustomPaging="True" AutoGenerateColumns="False" BackColor="#CDCDCD" BorderColor="Gainsboro" CellSpacing="1" BorderWidth="0px"
                                            CellPadding="3" DataKeyField="ID">
                                            <AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
                                            <ItemStyle BackColor="White"></ItemStyle>
                                            <HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Blue"></HeaderStyle>
                                            <Columns>
                                                <asp:TemplateColumn HeaderText="No" >
                                                    <HeaderStyle Width="2%" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblNo" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="">
                                                    <HeaderStyle Width="2%" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    <HeaderTemplate>
                                                        <input id="chkAllItems" onclick="CheckAll('cbAllGrid')"
                                                            type="checkbox">
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="cbAllGrid" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Kode Dealer" SortExpression="Dealer.DealerCode">
                                                    <HeaderStyle Width="15%" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblKodeDealer" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="No Claim Reg" SortExpression="ClaimRegNo">
                                                    <HeaderStyle Width="15%" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblNoClaimReg" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Keterangan MMKSI" SortExpression="MMKSINotes">
                                                    <HeaderStyle Width="15%" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMMKSINotes" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Pilihan Claim" SortExpression="BenefitType.Name">
                                                    <HeaderStyle Width="15%" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPilihanClaim" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Leasing" SortExpression="">
                                                    <HeaderStyle Width="15%" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblLeasing" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Tgl Claim" SortExpression="ClaimDate">
                                                    <HeaderStyle Width="15%" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTglClaim" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                 <asp:TemplateColumn HeaderText="Tgl Rencana Pembayaran" SortExpression="">
                                                    <HeaderStyle Width="8%" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTglBayar" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                 <asp:TemplateColumn HeaderText="Tgl Aktual Pembayaran" SortExpression="">
                                                    <HeaderStyle Width="8%" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTglAktualBayar" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                              
                                                <asp:TemplateColumn HeaderText="No. Surat" SortExpression="">
                                                    <HeaderStyle Width="15%" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSalesRef" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>

                                                <asp:TemplateColumn HeaderText="Status Claim" SortExpression="">
                                                    <HeaderStyle Width="15%" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblStatus" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>

                                                <asp:TemplateColumn HeaderText="Nomor JV" SortExpression="JVNumber">
                                                    <HeaderStyle Width="15%" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblJv" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>

                                                <asp:TemplateColumn HeaderText="Total Nilai Kuitansi" SortExpression="">
                                                    <HeaderStyle Width="15%" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTotalNilai" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>

                                                <asp:TemplateColumn HeaderText="Total Nilai Claim Nett" SortExpression="">
                                                    <HeaderStyle Width="15%" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTotalNilaiClaim" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>

                                                  <asp:TemplateColumn HeaderText="Total PPh" SortExpression="">
                                                    <HeaderStyle Width="15%" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPph" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>

                                                <asp:TemplateColumn HeaderText="Total PPn" SortExpression="">
                                                    <HeaderStyle Width="15%" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblVat" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>

                                              

                                                <asp:TemplateColumn HeaderText="Nomor Kuitansi" SortExpression="" Visible="false">
                                                    <HeaderStyle Width="15%" CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblKuitansi" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>

                                                <asp:TemplateColumn>
                                                    <HeaderStyle CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkbtnView" runat="server" Width="20px" Text="Detail" CausesValidation="False" CommandName="View" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
															<img alt="Detail" src="../images/Detail.gif" border="0"></asp:LinkButton>
                                                        <asp:LinkButton ID="lnkbtnEdit" runat="server" Width="20px" Text="Ubah" CausesValidation="False" CommandName="Edit" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
															<img alt="Ubah" src="../images/edit.gif" border="0"></asp:LinkButton>
                                                        <asp:LinkButton ID="lnkbtnDelete" runat="server" Width="20px" Text="Hapus" CausesValidation="False" CommandName="Delete" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.ID")%>'>
															<img alt="Hapus"  src="../images/trash.gif"
																border="0"></asp:LinkButton>
                                                        <asp:LinkButton ID="lnkbtnPrint" runat="server" Width="20px" Text="Detail" CausesValidation="False" CommandName="Print" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.ID")%>'>
															<img width="16" alt="Receipt" src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABQAAAAUCAYAAACNiR0NAAAABHNCSVQICAgIfAhkiAAAAAlwSFlzAAALEwAACxMBAJqcGAAAAZNJREFUOI2N08+LTXEYx/HX5TBXTcSgcJVRkkwJC7KaJrKTP2H+g8vCUk2zsKIkOytbNbOxpESTBQtsSM1iMkIYdaepkfll8f1+dRzX+Z5nc56+z+e8n895zvdpqY8bOIYjGV0b+3A9o/MOm3IiTGIcb3LiNaw3ABaxcbvICFPDPTiBVTzFRkW3jquYywFTHMJlrOA5flXqLUxgqinwJTq4iDul81ncxOboUh2w8Pf8XuBbRbMQn1uj+1rgIJZifh+n+2i6wk1o42cOuAOLMb+NoT6a5PBP8zrgTvyI+RiG+2hm8RrbU/M64F58jXnRR7sUnRPcL+SA+/Ep5g+xq1JfLuVD+J4DDgsDP4oH/9F08US4h2tl4DacFe5TilF8xkFcKZ13MCPMT6x/TMUEvCesU6/04hdh3UZKZwdi4x4uYB4n8aoKPIXj/t3RalwT5nkGhyNwDI+TIC3/RgMYbBH2uCd8+u7o9FFV+LYBDG7hA94L12Yel8qCQvhTKzifgQ1EN+eUfkI1Wrjb0N0qpvGsTvQbYqhQ2cHMsT0AAAAASUVORK5CYII=" border="0">

                                                        </asp:LinkButton>


                                                        <asp:LinkButton ID="lnkJV" Visible="false" runat="server" Width="20px" Text="Detail" CausesValidation="False" CommandName="addJv" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.ID")%>'>
															<img width="16" alt="JV" src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABAAAAAQCAYAAAAf8/9hAAACO0lEQVR42mNkgALH4Pj/TEzMDEzMQAyimZiQ2Kj81dNaGWH6GM+cOWMFpH1K2ydXzuqoYWBhYWFgZWXFSYdl12AYMPP27du2s9bs0AQJkOoKsAFr165Ni4yMZFi+fDnDHxk9BmJBd3YkI9iA/fv3q929d89BTVWV4Tm7JEOamwlBr5RNWwE3IB1oWDwfv4DlxUtXGE69+M7w/cd/BnM9ZYa///4z/P/3F0z/+w/EIPrfPzj71oMXCC8YGhoyvHv3jsHFxYVhz549DH5RCQyvvv5k+PPnH8Pfv/8YfgPxn79/wew/QCzKw86wcs95iAGXLl1SY2fncHj37i3DvUdPGRj+/GQoqm9luPTgOYOXvhKG35cdusKgKiPGsGr3GUQ0ComIVF6/84jB3tqM4fydZwyyUuIMRy7fZvC30ccwYMmOEwwGagoMq3adZABHRWVl5X+YF2DANTiaYcfx8wyB7raYLti4j8FMV4Nhzc6jEAMKCwsTTExM5yN7IbOigWHt3mMM/4EBBkQQGqQYyrcx1mdYu+MQAzxF3Xvw4P/VW/cZbC3NGKqmr2PIjfFhuPvqEzzQwAH4B8IGicmI8DCs3X4AYQAoMIE4Dca38QlluP3iI8SAP3+hhvyDGviXQU6Uj2Hdtr2oBhw6eS7t8YN7YC8kFFQz3Hz+nuHPb1A6+AeOzj9A+i/UMHlxfob1W3eiGACODRMTk6qk+qn/cxJCIYnmHyTR/P3/H8GHJqRFqzcgDEAGMRXd/4nNDwCe61SQWGtvIQAAAABJRU5ErkJggg==" border="0">

                                                        </asp:LinkButton>

                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                            </Columns>
                                            <PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
                                        </asp:DataGrid>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Panel ID="pnlProses" runat="server">
                            <asp:DropDownList ID="ddlStatus2" runat="server">
                            </asp:DropDownList>&nbsp;&nbsp;   


                      

                            <asp:Button ID="btnProses" runat="server" Text="Proses" Width="60px"></asp:Button>
                            &nbsp;&nbsp;   
                        <asp:Button ID="btnSAP" runat="server" Text="Transfer ke SAP" Width="100px" Visible="false"></asp:Button>
                        </asp:Panel>

                    </td>
                </tr>

            </table>
        </asp:Panel>


        <asp:Panel ID="Panel2" runat="server">
            <div id="areahidden">
                <div class="titlePage">
                    Daftar Referensi
                </div>

                <div style="background-image: url(../images/bg_hor.gif)">
                    <img border="0" src="../images/bg_hor.gif">
                </div>

                <div>
                    <table style="margin-top:5px;margin-bottom:5px">
                        <tr>
                            <td>No. Reg Benefit</td>
                            <td>:</td>
                            <td>
                                <asp:TextBox runat="server" ID="txtNoRegBenefit" Width="250px" />
                            </td>
                        </tr>
                        <tr>
                            <td>No. Surat</td>
                            <td>:</td>
                            <td>
                                <asp:TextBox runat="server" ID="txtNoSurat" Width="250px" />
                            </td>
                            <td style="width:10px"></td>
                            <td>
                                <asp:Button Text="Cari" ID="btnFilterCari" Width="100px" runat="server" />
                                <asp:HiddenField ID="arrCheckDetail" runat="server" />
                            </td>
                        </tr>
                    </table>
                </div>

                <div>
                    <img height="1" border="0" src="../images/dot.gif">
                </div>

                <div>
                    <asp:DataGrid ID="dgGridDetil" runat="server" Width="100%" AllowPaging="True" AllowSorting="True"
                        PageSize="200" AllowCustomPaging="True" AutoGenerateColumns="False" BackColor="#CDCDCD" BorderColor="Gainsboro" CellSpacing="1" BorderWidth="0px"
                        CellPadding="3" DataKeyField="ID">
                        <AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
                        <ItemStyle BackColor="White"></ItemStyle>
                        <HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Blue"></HeaderStyle>
                        <Columns>
                            <asp:TemplateColumn>
                                <HeaderStyle Width="1%"  CssClass="titleTableSales"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>

                                <HeaderTemplate>
                                    <input type="checkbox" id="chkbxAll" onclick="CheckAllDetail('cbxDetail')" />
                                </HeaderTemplate>

                                <ItemTemplate>
                                    &nbsp;
                                </ItemTemplate>
                            </asp:TemplateColumn>
                             <asp:TemplateColumn  HeaderText="ID" >
													    <HeaderStyle  Width="0px" CssClass="titleTableSales hiddencol" ></HeaderStyle>
													    <ItemStyle HorizontalAlign="Center" CssClass=" hiddencol" ></ItemStyle>
													    <ItemTemplate>
														     <asp:Label ID="lblIDoGridDetil" Runat="server"></asp:Label>
													    </ItemTemplate>
												    </asp:TemplateColumn>	
												    <asp:TemplateColumn  HeaderText="No">
													    <HeaderStyle  Width="2%" CssClass="titleTableSales" ></HeaderStyle>
													    <ItemStyle HorizontalAlign="Center"></ItemStyle>
													    <ItemTemplate>
														     <asp:Label ID="lblNoGridDetil" Runat="server"></asp:Label>
													    </ItemTemplate>
												    </asp:TemplateColumn>	
                                                    <asp:TemplateColumn HeaderText="No Reg Benefit">
													    <HeaderStyle Width="15%" CssClass="titleTableSales"></HeaderStyle>
													    <ItemTemplate>
                                                            <asp:Label ID="lblNoRegBenefitGridDetil" Runat="server"></asp:Label>
													    </ItemTemplate>
												    </asp:TemplateColumn>											
												<asp:TemplateColumn HeaderText="No Surat">
													    <HeaderStyle Width="15%" CssClass="titleTableSales"></HeaderStyle>
													    <ItemTemplate>
                                                            <asp:Label ID="lblnnosuratGridDetil" Runat="server"></asp:Label>
													    </ItemTemplate>
												    </asp:TemplateColumn>
                                                     <asp:TemplateColumn HeaderText="Formula">
													    <HeaderStyle Width="15%" CssClass="titleTableSales "></HeaderStyle>
                                                         <ItemStyle HorizontalAlign="Center" CssClass=" " ></ItemStyle>
													    <ItemTemplate>
                                                            <asp:Label ID="lblformula" Runat="server"></asp:Label>
													    </ItemTemplate>
												    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="Deskripsi">
													    <HeaderStyle Width="15%" CssClass="titleTableSales"></HeaderStyle>
													    <ItemTemplate>
                                                            <asp:Label ID="lbldeskripsiGridDetil" Runat="server"></asp:Label>
													    </ItemTemplate>
												    </asp:TemplateColumn>
                                                    


                        </Columns>
                        <PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
                    </asp:DataGrid>


                </div>

                <input style="width: 60px" onclick="GetSelectedValue()" type="button"
                    value="Pilih" name="btnChoose" />

                &nbsp;<input style="width: 60px" onclick="closepanel2()" type="button" value="Tutup" />
            </div>

        </asp:Panel>


        <asp:Panel ID="PanelJV" runat="server" style="display:none">

               <div  class="titlePage" >
                       Journal Voucher 
                    </div>

                     <div style="background-image:url(../images/bg_hor.gif)" >
                     <img  border="0" src="../images/bg_hor.gif">
                    </div>
                    <div  >
                     <img height="1" border="0" src="../images/dot.gif">
                    </div>
                    <div>
                         <asp:Label ID="lblJvKet" runat="server" Text="Label"></asp:Label>
                       
           

                        <br />
                         <asp:Button ID="btnAddJv" runat="server" Text="Simpan"  />
                 

                    &nbsp;<INPUT  style="WIDTH: 60px" onclick="closepanel2()" type="button" value="Tutup"
							    />
                        <br />
                       
            <asp:DataGrid ID="dgJV" runat="server" Width="100%"  AllowSorting="True"
                         AutoGenerateColumns="False" BackColor="#CDCDCD" BorderColor="Gainsboro" CellSpacing="1" BorderWidth="0px"
                        CellPadding="3" DataKeyField="ID">
                        <AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
                        <ItemStyle BackColor="White"></ItemStyle>
                        <HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Blue"></HeaderStyle>
                        <Columns>
                          
                             <asp:TemplateColumn  HeaderText="Account" >
													    <HeaderStyle  Width="0px" CssClass="titleTableSales" ></HeaderStyle>
													  
													    <ItemTemplate>
														     <asp:Label ID="lblAccount" Runat="server"></asp:Label>
													    </ItemTemplate>

                                  <FooterTemplate>
														<asp:DropDownList ID="ddlTipeAccountGrid" runat="server" class="ddlModelGrid">                                        
                                                            
                                                        </asp:DropDownList>  
													</FooterTemplate>

                                                    <EditItemTemplate>
                                                        <asp:Label ID="lblAccountEditGrid" Runat="server"></asp:Label>
                                                      </EditItemTemplate>

												    </asp:TemplateColumn>	
												    <asp:TemplateColumn  HeaderText="Vendor Code">
													    <HeaderStyle  Width="2%" CssClass="titleTableSales" ></HeaderStyle>
													    <ItemStyle HorizontalAlign="Center"></ItemStyle>
													    <ItemTemplate>
														     <asp:Label ID="lblVendor" Runat="server"></asp:Label>
													    </ItemTemplate>

                                                         <FooterTemplate>
														

                                                              <asp:DropDownList ID="ddlVendorGrid" runat="server">
                                                                        </asp:DropDownList>

													</FooterTemplate>
                                                         <EditItemTemplate>
                                                      <asp:Label ID="lblVendorEditGrid" Runat="server"></asp:Label>

                                                               <asp:DropDownList ID="ddlVendorEditGrid" runat="server">
                                                                        </asp:DropDownList>

                                                      </EditItemTemplate>
												    </asp:TemplateColumn>	
                                                    <asp:TemplateColumn HeaderText="Amount">
													    <HeaderStyle Width="15%" CssClass="titleTableSales"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Right" />
													    <ItemTemplate>
                                                            <asp:Label ID="lblAmount" Runat="server"></asp:Label>
													    </ItemTemplate>

                                                         <FooterTemplate>
														<asp:TextBox id="txtAmountGrid" runat="server" Width="100%" CssClass="textRight" onkeypress="return numericOnlyUniv(event)"
															onkeyup="pic(this,this.value,'9999999999','N')" MaxLength="11"></asp:TextBox>
														
													</FooterTemplate>
                                                         <EditItemTemplate>
                                                        <asp:Label ID="lblAmountEditGrid" Runat="server"></asp:Label>
                                                      </EditItemTemplate>
												    </asp:TemplateColumn>											
												<asp:TemplateColumn HeaderText="Month">
													    <HeaderStyle Width="15%" CssClass="titleTableSales"></HeaderStyle>
													    <ItemTemplate>
                                                            <asp:Label ID="lblMonth" Runat="server"></asp:Label>
													    </ItemTemplate>

                                                    <FooterTemplate>
														  <asp:DropDownList ID="ddlAccueredGrid" runat="server">
                                                    </asp:DropDownList>
													</FooterTemplate>
                                                     <EditItemTemplate>
                                                        <asp:DropDownList ID="ddlAccueredEditGrid" runat="server">
                                                                        </asp:DropDownList>
                                                      </EditItemTemplate>
												    </asp:TemplateColumn>
                                                     <asp:TemplateColumn HeaderText="Tanggal Pembayaran">
													    <HeaderStyle Width="15%" CssClass="titleTableSales "></HeaderStyle>
                                                         <ItemStyle HorizontalAlign="Center" CssClass=" " ></ItemStyle>
													    <ItemTemplate>
                                                            <asp:Label ID="lblPembayaran" Runat="server"></asp:Label>
													    </ItemTemplate>

                                                          <FooterTemplate>
                                                       
														<cc1:inticalendar id="icPembayaranGrid" runat="server" TextBoxWidth="60"></cc1:inticalendar>
                                                        														
													</FooterTemplate>
                                                         <EditItemTemplate>
                                                       <cc1:inticalendar id="icPembayaranEditGrid" runat="server" TextBoxWidth="60"></cc1:inticalendar>
                                                      </EditItemTemplate>
												    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="Business Area">
													    <HeaderStyle Width="15%" CssClass="titleTableSales"></HeaderStyle>
													    <ItemTemplate>
                                                            <asp:Label ID="lblBusinessArea" Runat="server"></asp:Label>
													    </ItemTemplate>
                                                         <FooterTemplate>
														<asp:textbox onkeypress="return alphaNumericExcept(event,'?*%$;')" id="txtBusinessAreaGrid" onblur="omitSomeCharacter('txtBusinessAreaGrid','?*%$;')"
							                                    runat="server" ></asp:textbox>
													</FooterTemplate>
                                                           <EditItemTemplate>
                                                       <asp:textbox onkeypress="return alphaNumericExcept(event,'?*%$;')" id="txtBusinessAreaEditGrid" onblur="omitSomeCharacter('txtBusinessAreaEditGrid','?*%$;')"
							                                    runat="server" ></asp:textbox>
                                                      </EditItemTemplate>
												    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="Cost Center">
													    <HeaderStyle Width="15%" CssClass="titleTableSales"></HeaderStyle>
                                                      
													    <ItemTemplate>
                                                            <asp:Label ID="lblCostCenter" Runat="server"></asp:Label>
													    </ItemTemplate>
                                                         <FooterTemplate>
														
												 <asp:textbox onkeypress="return alphaNumericExcept(event,'?*%$;')" id="txtCostCenterGrid" onblur="omitSomeCharacter('txtCostCenterGrid','?*%$;')"
							                                    runat="server" ></asp:textbox>		
													</FooterTemplate>
                                                        <EditItemTemplate>
                                                    
                                                          <asp:textbox onkeypress="return alphaNumericExcept(event,'?*%$;')" id="txtCostCenterEditGrid" onblur="omitSomeCharacter('txtCostCenterEditGrid','?*%$;')"
							                                    runat="server" ></asp:textbox>

                                                      </EditItemTemplate>
												    </asp:TemplateColumn>

                              <asp:TemplateColumn>
                                                    <HeaderStyle CssClass="titleTableSales"></HeaderStyle>
                                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                    <ItemTemplate>
                                                        
                                                        <asp:LinkButton ID="lnkbtnEdit" runat="server" Width="20px" Text="Ubah" CausesValidation="False" CommandName="Edit" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
															<img alt="Ubah" src="../images/edit.gif" border="0"></asp:LinkButton>
                                                        <asp:LinkButton ID="lnkbtnDelete" runat="server" Width="20px" Text="Hapus" CausesValidation="False" CommandName="Delete" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.ID")%>'>
															<img alt="Hapus"  src="../images/trash.gif"
																border="0"></asp:LinkButton>
                                                       
                                                        <asp:LinkButton id="lnkbtnAdd" runat="server" CausesValidation="true" CommandName="Add">
															<img src="../images/add.gif" border="0" alt="Tambah">
														</asp:LinkButton>



                                                    </ItemTemplate>

                                  <EditItemTemplate>
                                      <asp:LinkButton id="lbtnSaveEdit" tabIndex="40" Runat="server" CausesValidation="True" CommandName="Save"
															text="Simpan">
															<img src="../images/simpan.gif" border="0" alt="Simpan"></asp:LinkButton>
                                  </EditItemTemplate>

                                   <FooterTemplate>
														<asp:LinkButton id="lbtnSave" tabIndex="40" Runat="server" CausesValidation="True" CommandName="AddSave"
															text="Simpan">
															<img src="../images/simpan.gif" border="0" alt="Simpan"></asp:LinkButton>
													</FooterTemplate>

                                                </asp:TemplateColumn>
                        </Columns>
                       
                    </asp:DataGrid>


                        <asp:Panel ID="PanelJvInput" runat="server" Visible="false">
                            <table>
                                <tr>
                                    <td>Account</td>
                                    <td> 
                                        <asp:DropDownList ID="ddlTipeAccount" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Vendor</td>
                                    <td> 
                                        <asp:TextBox ID="txtVendorJV" runat="server"  onkeypress="return alphaNumericExcept(event,'<>?*%$;')"  onblur="omitSomeCharacter('txtVendorJV','<>?*%$;')"></asp:TextBox>
                                    </td>
                                </tr>
                                 <tr>
                                    <td>Amount</td>
                                    <td> 
                                        <asp:TextBox ID="txtAmountJV" runat="server" onkeypress="return numericOnlyUniv(event)" onkeyup="pic(this,this.value,'9999999999','N')" MaxLength="11"></asp:TextBox>
                                    </td>
                                </tr>
                                 <tr>
                                    <td>Accured Amount</td>
                                    <td>                                        
                                       <asp:TextBox ID="txtAccuredAmount" runat="server" onkeypress="return numericOnlyUniv(event)" onkeyup="pic(this,this.value,'9999999999','N')" MaxLength="11"></asp:TextBox>
                                    </td>
                                </tr>
                                 <tr>
                                    <td>Tanggal Pembayaran</td>
                                    <td> 
                                        <cc1:IntiCalendar ID="icPaymentJV" runat="server" TextBoxWidth="60"></cc1:IntiCalendar>
                                      
                                    </td>
                                </tr>
                                 <tr>
                                    <td>Business Area</td>
                                    <td> 
                                        <asp:TextBox ID="txtBusinessAreaJV" runat="server"  onkeypress="return alphaNumericExcept(event,'<>?*%$;')"  onblur="omitSomeCharacter('txtBusinessAreaJV','<>?*%$;')"></asp:TextBox>
                                    </td>
                                </tr>
                                 <tr>
                                    <td>Cost Center</td>
                                    <td> 
                                        <asp:TextBox ID="txtCostCenterJV" runat="server" onkeypress="return numericOnlyUniv(event)" onkeyup="pic(this,this.value,'9999999999','N')" MaxLength="11"></asp:TextBox>
                                    </td>
                                </tr>

                                <tr>
                                    <td> <asp:Button ID="btnAddJvInput" runat="server" Text="Tambah" /></td>
                                    <td> 
                                      
                                    </td>
                                </tr>

                            </table>
                        </asp:Panel>






                    </div>
                  


           


        </asp:Panel>
    </form>
</body>
</html>
