<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>

<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmSPKDetailCustomer.aspx.vb" Inherits=".FrmSPKDetailCustomer" SmartNavigation="False" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>FrmCustomerRequest</title>

    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />

    <meta http-equiv="X-UA-Compatible" content="IE=edge">



    <link href="../WebResources/ocr/bootstrap.min.css" rel="stylesheet" />
    <link href="../WebResources/css/Modal.css" rel="stylesheet" />

    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">

    <%--<script type="text/javascript" src="../WebResources/ocr/jquery-1.10.2.js"></script>--%>
    <script type="text/javascript" src="../WebResources/ocr/jquery-1.12.4.min.js"></script>

    <%--<script type="text/javascript" src="../WebResources/ocr/jquery.ui.min.js"></script>--%>
    <!-- HTML5 shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!--[if lt IE 7]>
      <script src="../WebResources/ocr/html5shiv.min.js"></script>
      <script src="../WebResources/ocr/respond.min.js"></script>
    <![endif]-->

    <!--[if lt IE 8]>
      <script src="../WebResources/ocr/html5shiv.min.js"></script>
      <script src="../WebResources/ocr/respond.min.js"></script>
    <![endif]-->

    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>

    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script language="javascript">

        function numericOnlyBlur(controlName) {
            var key = controlName.value;
            var newValue = "";
            for (i = 0; i < key.length; i++) {
                if ((key.charCodeAt(i) >= 48 && key.charCodeAt(i) <= 57) || key.charCodeAt(i) == 8) {
                    newValue = newValue + key.charAt(i);
                }
            }
            controlName.value = newValue;
        }

        function ShowPPDealerSelection() {
            showPopUp('../PopUp/PopUpDealerSelectionOne.aspx?x=Territory', '', 500, 760, DealerSelection);
        }
        function DealerSelection(selectedDealer) {
            var tempParam = selectedDealer.split(';');
            var txtDealer = document.getElementById("txtDealer");
            txtDealer.value = tempParam[0];
        }
        function autofocus(field, next) {
            if (field.value.length == field.maxLength) {
                field.form.elements[next].focus();
            }
        }

        function ShowCustomerList() {
            var ddl = document.getElementById("ddlTipePerusahaan");
            if (ddl) {
                var val = ddl.options[ddl.selectedIndex].value;
                var text = ddl.options[ddl.selectedIndex].text + '.';
                showPopUp('../PopUp/PopUpCustomerList.aspx?Tyjiuy678=code&tipe=' + text, '', 500, 760, CustomerSelection);
            }
            else {
                showPopUp('../PopUp/PopUpCustomerList.aspx?Tyjiuy678=code', '', 500, 760, CustomerSelection);
            }
        }
        function ShowNoPengajuanList() {
            showPopUp('../PopUp/PopUpCustomerList.aspx?Tyjiuy678=number', '', 500, 760, PengajuanSelection);
        }
        function PengajuanSelection(selectedCustomer) {
            var data = selectedCustomer.split(";")
            var txtRefNoPengajuan = document.getElementById("txtRefNoPengajuan");
            var txtNama = document.getElementById("txtNama");
            var txtNama2 = document.getElementById("txtNama2");
            txtRefNoPengajuan.value = data[0];
            txtNama.value = data[1];
            txtNama2.value = data[2];
        }
        function CustomerSelection(selectedCustomer) {
            var txtRefKodePelanggan = document.getElementById("txtRefKodePelanggan");
            txtRefKodePelanggan.value = selectedCustomer;
        }
        function UpdatePanelHeight() {
            var ddlTipe = document.getElementById("ddlTipe");
            var pnlName = 'pnlPerorangan';
            var sSelected = ddlTipe.options[ddlTipe.selectedIndex].value;

            if (sSelected == 0) pnlName = 'pnlPerorangan';
            else if (sSelected == 1) pnlName = 'pnlPerusahaan';
            else if (sSelected == 2) pnlName = 'PnlBUMN';
            else if (sSelected == 3) pnlName = 'PnlLainnya';
            HidePanelRow(pnlName);

        }
        function HidePanelRow(pnlName) {
            return false;
            var pnlPerorangan = document.getElementById(pnlName);//"pnlPerorangan");
            var txtNomerID = document.getElementById("txtNomerID");
            var titlePanel1 = document.getElementById("titlePanel1");
            var i = 0; var j = 0; var tr; var spns; var IsDisplay = 1;

            for (i = 0; i < pnlPerorangan.childNodes[1].rows.length; i++) {
                tr = pnlPerorangan.childNodes[1].rows[i];
                spns = tr.getElementsByTagName("span");
                for (j = 0; j < spns.length; j++) {
                    IsDisplay = 1;
                    if (spns[j].innerHTML == 'NO KTP' || spns[j].innerHTML == 'NO NPWP' || spns[j].innerHTML == 'NO SIUP' || spns[j].innerHTML == 'NO TDP') IsDisplay = 0;
                    if (IsDisplay == 0) pnlPerorangan.childNodes[1].rows[i].style.display = 'none';
                }
            }
        }

        function ShowLKPPSelection() {
            var lblVCId = document.getElementById('lblVCId');
            showPopUp('../PopUp/PopUpLKPPTersedia.aspx?IsGroupDealer=1&VC=' + lblVCId.innerHTML, '', 500, 800, LKPPSelection);
        }

        function LKPPSelection(selectedLKPP) {
            var temp = selectedLKPP.split(";")
            //console.log(temp);
            //console.log(selectedLKPP);
            var txtLKPPNumber = document.getElementById('txtLKPPNumber');
            var lblinstitutionName2 = document.getElementById('lblinstitutionName2');
            txtLKPPNumber.value = temp[0];
            lblinstitutionName2.innerHTML = temp[1];
        }
    </script>
    <style type="text/css">
        .modal-dialog-centered {
            height: calc(100% - (.5rem * 2));
        }

        @media (min-width:576px) {
            .modal-dialog-centered {
                height: calc(100% - (1.75rem * 2));
            }
        }
    </style>
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage">MARKETING&nbsp;- Data Konsumen Faktur</td>
            </tr>
            <tr>
                <td style="border-bottom: dotted thin; height: 1px">
                    <%--<img height="1" src="../images/bg_hor.gif" border="0">--%>
                    <%--  <hr />--%>
                </td>
            </tr>
            <tr>
                <td height="10">
                    <img height="1" src="../images/dot.gif" border="0"></td>
            </tr>
            <tr>
                <td align="left">
                    <table id="Table2" cellspacing="1" cellpadding="2" width="100%" border="0">
                        <tr>
                            <td class="titleField" width="20%">Dealer</td>
                            <td width="1%">:</td>
                            <td width="30%">
                                <asp:Label ID="lblDealer" runat="server"></asp:Label>
                            </td>
                            <td width="2%"></td>
                            <td class="titleField" width="18%">Reg SPK</td>
                            <td width="1%">:</td>
                            <td class="titleField" width="28%">
                                <asp:Label ID="lblRegSPK" runat="server"></asp:Label></td>
                        </tr>

                        <tr>
                            <td class="titleField" width="20%">Kendaraan</td>
                            <td width="1%">:</td>
                            <td width="30%">
                                <asp:Label ID="lblVehicleColor" runat="server"></asp:Label>
                                <asp:Label ID="lblVCId" runat="server"></asp:Label>
                            </td>
                            <td width="2%"></td>
                            <td class="titleField" width="18%">Qty</td>
                            <td width="1%">:</td>
                            <td class="titleField" width="28%">
                                <asp:TextBox MaxLength="3" onkeypress="return alphaNumericPlusUniv(event)" ID="txtQty" onblur="alphaNumericPlusBlur(txtQty)" runat="server"></asp:TextBox></td>
                        </tr>

                        <tr>
                            <td class="titleField" width="20%">Kategori</td>
                            <td width="1%">:</td>
                            <td width="30%">
                                <asp:DropDownList ID="ddlTipe" runat="server" Width="200px" AutoPostBack="True"></asp:DropDownList><asp:Label ID="lblTipe" runat="server" Visible="False"></asp:Label></td>
                            <td width="2%"></td>
                            <td class="titleField" width="18%">
                                <asp:Button ID="BtnCopySPKCustomer" runat="server" Text="Copy Data SPK Customer" CausesValidation="false" />
                                <%--<asp:LinkButton ID="LnkCopySPKCustomer" runat="server" Text="Copy Data SPK Customer" CausesValidation="false"></asp:LinkButton>--%>
                            </td>
                            <td width="1%"></td>
                            <td width="28%"></td>
                        </tr>


                        <tr>
                            <td class="titleField" width="20%">Tipe</td>
                            <td width="1%">:</td>
                            <td width="30%">
                                <asp:DropDownList ID="ddlTipePerusahaan" AutoPostBack="true" runat="server" Visible="False"></asp:DropDownList><asp:Label ID="lblTipePerusahaan" runat="server" Visible="False"></asp:Label>
                                <asp:DropDownList ID="ddlTipePerorangan" AutoPostBack="true" runat="server" Visible="False"></asp:DropDownList><asp:Label ID="lblTipePerorangan" runat="server" Visible="False"></asp:Label>
                            </td>
                            <td width="2%"></td>
                            <td class="titleField" width="18%">
                                <asp:HiddenField ID="hdnUpload" runat="server" Value="" />
                                <asp:HiddenField ID="hdnGetApi" runat="server" Value="" />
                                <asp:HiddenField ID="hdnEnabled" runat="server" Value="" />
                                <asp:HiddenField ID="hdnNameEnabled" runat="server" Value="" />
                            </td>
                            <td width="1%"></td>
                            <td width="28%">
                                <asp:TextBox ID="TxtFlag" runat="server" Visible="False" Width="42px">domestik</asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField"width="20%">Nomor Pengadaan</td>
                            <td width="1%">:</td>
                            <td colspan="5">
                                <asp:TextBox ID="txtLKPPNumber" runat="server" Width="150px" MaxLength="60"></asp:TextBox>
                                <asp:Label ID="lblsearchLKPP" runat="server" Width="16px">
										<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0">
                                </asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>

            <tr runat="server" id="trUpload" valign="top" class="ShowControl">
                <td valign="top">
                    <table id="titleUmum" cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="titlePanel"><b>Upload Identitas</b>
                            </td>
                        </tr>
                        <tr>
                            <td style="border-bottom: dotted thin; height: 1px">
                                <%--<img height="1" src="../images/bg_hor.gif" border="0">--%>
                                <%--  <hr />--%>
                            </td>
                        </tr>
                    </table>

                    <table cellspacing="1" cellpadding="2" width="100%" border="0">
                        <tr align="left" valign="top">
                            <td class="titleField" width="28%">Tipe Identitas</td>
                            <td width="1%">:</td>
                            <td width="30%">
                                <asp:DropDownList ID="ddlIdentity" runat="server" Visible="true" Width="60px">
                                    <asp:ListItem Text="KTP" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="SIM" Value="1"></asp:ListItem>

                                </asp:DropDownList>
                                <asp:DropDownList ID="ddlIdentityAsing" runat="server" Visible="true" Width="60px">
                                    <asp:ListItem Text="KITAS" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="KITAP" Value="3"></asp:ListItem>                                    
                                </asp:DropDownList>
                            </td>

                            <td class="titleField" width="20%" colspan="2">
                                <asp:TextBox TextMode="MultiLine" runat="server" ID="txtJSon" Style="display: none;">

                                </asp:TextBox>
                                <asp:TextBox ID="TglLahir" runat="server" Width="42px" Visible="False"></asp:TextBox>
                                <asp:TextBox ID="TglLahirCW" runat="server" Width="42px" Visible="False"></asp:TextBox>
                            </td>
                            <td width="1%"></td>
                            <td width="28%" rowspan="2">
                                <asp:Image ID="photoView" onclick="imageOnClick()" runat="server" Width="300px" Height="150px" CssClass="ShowControl" ImageUrl="../DataFile/PPT/NotFound.png"></asp:Image>

                            </td>
                        </tr>

                        <tr align="left" valign="top">
                            <td class="titleField" width="28%">Pilih Lokasi File<br />
                                <span style="font-weight: lighter; font-style: italic;">(Max 1 Mb dengan extension&nbsp; *.jpeg, *.jpg&nbsp; , *.png)</span></td>
                            <td width="1%">:</td>
                            <td width="30%">
                                <input id="fileUpload" onkeydown="return false;" style="width: 240px" type="file" name="File1"
                                    runat="server">
                                <br />
                                <asp:Label ID="lblAttachment" runat="server"></asp:Label>
                            </td>

                            <td class="titleField" width="20%" colspan="2">

                                <input type="button" value="Upload" title="Upload Berkas Identitas" onclick="UploadFile()" runat="server" id="btnUplouadJS" />
                                <asp:Button ID="btnUpload" runat="server" Text="Upload File" CausesValidation="false" Style="display: none;" />
                                <asp:Button ID="btnGetApi" runat="server" Text="GetData" CausesValidation="false" Style="display: none;" />

                            </td>
                            <td width="1%"></td>
                            <%--  <td width="28%"></td>--%>
                        </tr>
                    </table>

                </td>
            </tr>
            <tr>
                <td valign="top">
                    <table id="titleUmum" cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="titlePanel"><b>INFORMASI PELANGGAN </b>
                            </td>
                        </tr>
                        <tr>
                            <td style="border-bottom: dotted thin; height: 1px">
                                <%--<img height="1" src="../images/bg_hor.gif" border="0">--%>
                                <%--  <hr />--%>
                            </td>
                        </tr>
                    </table>
                    <table cellspacing="1" cellpadding="2" width="100%" border="0" id="tblInformasi">
                        <tbody>

                            <tr>
                                <td class="titleField" width="20%">Kode Pelanggan</td>
                                <td width="1%">:</td>
                                <td style="width: 151px" width="151">
                                    <asp:Label ID="lblKodePelanggan" runat="server"></asp:Label></td>
                                <td style="width: 30px" width="30"></td>
                                <td class="titleField">
                                    <asp:Label ID="Label1" runat="server" Visible="false">Ref Kode Pelanggan</asp:Label></td>
                                <td width="1%" visible="true">:</td>
                                <td nowrap width="28%" visible="False">
                                    <asp:Label ID="lblRefKodePlgn" runat="server" Visible="False"></asp:Label>
                                    <asp:TextBox onkeypress="return alphaNumericExcept(event,'?%*$[]+={}<>&amp;@~!');" ID="txtRefKodePelanggan"
                                        onblur="omitSomeCharacter('txtRefKodePelanggan','<>?*%$;');" runat="server" Width="88px"
                                        MaxLength="10" Visible="False"></asp:TextBox>
                                    <asp:Label ID="lbtnRefKode" onclick="ShowCustomerList();" runat="server" Visible="False">
											<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup">
                                    </asp:Label>
                                    <asp:LinkButton ID="lnkReloadPlg" runat="server" CausesValidation="False" Visible="False">
											<img src="../images/reload.gif" style="cursor:hand" border="0" alt="Reload">
                                    </asp:LinkButton>
                                </td>
                            </tr>
                            <tr>
                                <td class="titleField" width="20%">Nama 1</td>
                                <td width="1%">:</td>
                                <td colspan="5">
                                    <asp:Label ID="lblNama1" runat="server" Visible="False"></asp:Label>
                                    <asp:TextBox ID="txtNama" runat="server" Width="200px" MaxLength="40" onkeypress="return alphaNumericExceptExcludeSingleQuote(event,'<>?*%$;+=`~{}');"
                                        onblur="omitSomeCharacterExcludeSingleQuote('txtNama','<>?*%$;');" onkeyup="autofocus(this,'txtNama2');"
                                        onchange="this.value=this.value.toUpperCase();"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="Requiredfieldvalidator5" runat="server" ControlToValidate="txtNama" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator runat="server" ID="valInput"
                                        ControlToValidate="txtNama"
                                        ValidationExpression="^[\s\S]{0,40}$"
                                        ErrorMessage="Panjang maksimal 40 karakter"></asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="titleField" width="20%">Nama&nbsp;2</td>
                                <td width="1%">:</td>
                                <td colspan="5">
                                    <asp:Label ID="lblNama2" runat="server" Visible="False"></asp:Label>
                                    <asp:TextBox ID="txtNama2" runat="server" Width="200px" MaxLength="35" onkeypress="return alphaNumericExceptExcludeSingleQuote(event,'<>?*%$;+=`~{}');"
                                        onblur="omitSomeCharacterExcludeSingleQuote('txtNama2','<>?*%$;');" onkeyup="autofocus(this,'txtGedung');"
                                        onchange="this.value=this.value.toUpperCase();"></asp:TextBox>
                                    <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator1"
                                        ControlToValidate="txtNama2"
                                        ValidationExpression="^[\s\S]{0,35}$"
                                        ErrorMessage="Panjang maksimal 35 karakter"></asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="titleField" width="20%">Gedung</td>
                                <td width="1%">:</td>
                                <td colspan="5">
                                    <asp:Label ID="lblGedung" runat="server" Visible="False"></asp:Label>
                                    <asp:TextBox ID="txtGedung" runat="server" Width="200px" MaxLength="40" onkeypress="return alphaNumericExceptExcludeSingleQuote(event,'<>?*%$;+=`~{}');"
                                        onblur="omitSomeCharacterExcludeSingleQuote('txtGedung','<>?*%$;');" onkeyup="autofocus(this,'txtAlamat');"
                                        onchange="this.value=this.value.toUpperCase();"></asp:TextBox>
                                    <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator2"
                                        ControlToValidate="txtGedung"
                                        ValidationExpression="^[\s\S]{0,40}$"
                                        ErrorMessage="Panjang maksimal 40 karakter"></asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="titleField" width="20%">Alamat</td>
                                <td width="1%">:</td>
                                <td colspan="5">
                                    <asp:Label ID="lblAlamat" runat="server" Visible="False"></asp:Label>
                                    <asp:TextBox ID="txtAlamat" runat="server" Width="200px" MaxLength="60" onkeypress="return alphaNumericExceptExcludeSingleQuote(event,'<>?*%$;+=`~{}');"
                                        onblur="omitSomeCharacterExcludeSingleQuote('txtNama','<>?*%$;');" onkeyup="autofocus(this,'txtKelurahan');"
                                        onchange="this.value=this.value.toUpperCase();"></asp:TextBox>&nbsp;
										<asp:RequiredFieldValidator ID="Requiredfieldvalidator2" runat="server" ControlToValidate="txtAlamat" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator3"
                                        ControlToValidate="txtAlamat"
                                        ValidationExpression="^[\s\S]{0,60}$"
                                        ErrorMessage="Panjang maksimal 60 karakter"></asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="titleField" width="20%">Kelurahan</td>
                                <td width="1%">:</td>
                                <td colspan="5">
                                    <asp:Label ID="lblKelurahan" runat="server" Visible="False"></asp:Label>
                                    <asp:TextBox onkeypress="return alphaNumericExceptExcludeSingleQuote(event,'<>?*%$;+=`~{}');"
                                        ID="txtKelurahan" onblur="omitSomeCharacterExcludeSingleQuote('txtNama','<>?*%$;');" onkeyup="autofocus(this,'txtKecamatan');" runat="server" Width="200px" MaxLength="40" onchange="this.value=this.value.toUpperCase();"></asp:TextBox>&nbsp;
                                    <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator4"
                                        ControlToValidate="txtKelurahan"
                                        ValidationExpression="^[\s\S]{0,40}$"
                                        ErrorMessage="Panjang maksimal 40 karakter"></asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="titleField" width="20%">Kecamatan</td>
                                <td width="1%">:</td>
                                <td width="30%">
                                    <asp:Label ID="lblKecamatan" runat="server" Visible="False"></asp:Label>
                                    <asp:TextBox onkeypress="return alphaNumericExceptExcludeSingleQuote(event,'<>?*%$;+=`~{}');"
                                        ID="txtKecamatan" onblur="omitSomeCharacterExcludeSingleQuote('txtNama','<>?*%$;');" onkeyup="autofocus(this,'txtKodePos');" runat="server" Width="200px" MaxLength="35" onchange="this.value=this.value.toUpperCase();"></asp:TextBox>&nbsp;
                                    <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator5"
                                        ControlToValidate="txtKecamatan"
                                        ValidationExpression="^[\s\S]{0,35}$"
                                        ErrorMessage="Panjang maksimal 35 karakter"></asp:RegularExpressionValidator>
                                </td>
                                <td style="width: 30px" width="30"></td>
                                <td class="titleField">Kode Pos</td>
                                <td width="1%">:</td>
                                <td width="28%">
                                    <asp:Label ID="lblKodePos" runat="server" Visible="False"></asp:Label>
                                    <asp:TextBox ID="txtKodePos" runat="server" Width="68px" MaxLength="5" onkeypress="return numericOnlyUniv(event)"
                                        onblur="numericOnlyBlur(txtKodePos)"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="Requiredfieldvalidator3" runat="server" ControlToValidate="txtKodePos" ErrorMessage="kode pos 5 angka"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="titleField" style="height: 18px" width="20%">Propinsi</td>
                                <td style="height: 18px" width="1%">:</td>
                                <td width="30%">
                                    <asp:Label ID="lblPropinsi" runat="server" Visible="False"></asp:Label>
                                    <asp:DropDownList ID="ddlPropinsi" runat="server" Width="200px" AutoPostBack="True"></asp:DropDownList>
                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="*" ControlToValidate="ddlPropinsi"
                                        ValueToCompare="0" Operator="NotEqual">*</asp:CompareValidator>
                                </td>
                                <td style="width: 30px; height: 18px" width="30"></td>
                                <td class="titleField">
                                    <asp:Label ID="lblCetakTitle" runat="server" ToolTip="Print Propinsi di Faktur">Cetak</asp:Label></td>
                                <td style="height: 18px" width="1%">:</td>
                                <td style="height: 18px" width="28%">
                                    <asp:Label ID="lblCetak" runat="server" Visible="False"></asp:Label><asp:DropDownList ID="ddlCetak" runat="server"></asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td class="titleField" width="20%">Kota/Kabupaten</td>
                                <td width="1%">:</td>
                                <td colspan="5">
                                    <asp:Label ID="lblKota" runat="server" Visible="False"></asp:Label><asp:DropDownList ID="ddlPreArea" runat="server" Width="100px">
                                        <asp:ListItem Value="blank" Selected="True">Silahkan Pilih</asp:ListItem>
                                        <asp:ListItem Value="KAB">KAB</asp:ListItem>
                                        <asp:ListItem Value="KODYA">KODYA</asp:ListItem>
                                        <asp:ListItem Value="KABUPATEN">KABUPATEN</asp:ListItem>
                                        <asp:ListItem Value="KOTA MADYA">KOTAMADYA</asp:ListItem>
                                        <asp:ListItem Value="KOTA">KOTA</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:CompareValidator ID="Comparevalidator2" runat="server" ErrorMessage="*" ControlToValidate="ddlPreArea"
                                        ValueToCompare="0" Operator="NotEqual">*</asp:CompareValidator>
                                    <asp:DropDownList ID="ddlKota" runat="server" Width="220px"></asp:DropDownList>
                                    <asp:CompareValidator ID="Comparevalidator3" runat="server" ErrorMessage="*" ControlToValidate="ddlKota"
                                        ValueToCompare="0" Operator="NotEqual">*</asp:CompareValidator>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <table cellspacing="1" cellpadding="2" width="100%" border="0" id="tblInformasi2">
                        <tbody>
                            <tr>
                                <td class="titleField" width="20%">Nomor kontak</td>
                                <td width="1%"></td>
                                <td style="width: 151px" width="151"></td>
                                <td style="width: 30px" width="30"></td>
                                <td class="titleField" width="20%"></td>
                                <td width="1%"></td>
                                <td width="28%"></td>
                            </tr>
                            <tr>
                                <td class="titleField" style="text-align: right" width="20%">Kode Negara</td>
                                <td width="1%">:</td>
                                <td colspan="5">
                                    <asp:Label ID="lblKodeNegara" runat="server" Visible="False"></asp:Label>
                                    <asp:TextBox onkeypress="return HtmlCharUniv(event)" ID="txtCountryCode" TabIndex="14" runat="server"
									MaxLength="50" Width="70px"></asp:TextBox>
								    <asp:RequiredFieldValidator ID="Requiredfieldvalidator4" runat="server" ControlToValidate="txtCountryCode" ErrorMessage="*"></asp:RequiredFieldValidator>
								  
								    <asp:Label ID="lblSearchCountryName" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup">
								    </asp:Label>
							    	<asp:LinkButton ID="LinkButton1" Style="display: none" runat="server">Dont remove</asp:LinkButton>
                                </td>
                            </tr>
                            <tr>
                                <td class="titleField" style="text-align: right" width="20%">Office</td>
                                <td width="1%">:</td>
                                <td colspan="5">
                                    <asp:Label ID="lblOfficeNo" runat="server" Visible="False"></asp:Label>
                                    <asp:TextBox ID="txtOffice" runat="server" Width="200px" MaxLength="60" onkeypress="return numericOnlyUniv(event)"
                                        onblur="numericOnlyBlur(txtOffice)"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="titleField" style="text-align: right" width="20%">Home</td>
                                <td width="1%">:</td>
                                <td colspan="5">
                                    <asp:Label ID="lblHomeNo" runat="server" Visible="False"></asp:Label>
                                    <asp:TextBox ID="txtHome" runat="server" Width="200px" MaxLength="60" onkeypress="return numericOnlyUniv(event)"
                                        onblur="numericOnlyBlur(txtHome)"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="titleField" style="text-align: right" width="20%">Handphone</td>
                                <td width="1%">:</td>
                                <td colspan="5">
                                    <asp:Label ID="lblHpNo" runat="server" Visible="False"></asp:Label>
                                    <asp:TextBox ID="txtHp" runat="server" Width="200px" MaxLength="60" onkeypress="return numericOnlyUniv(event)"
                                        onblur="numericOnlyBlur(txtHp)"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="Requiredfieldvalidator1" runat="server" ControlToValidate="txtHp" ErrorMessage="*"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="titleField" style="text-align: right" width="20%">Email</td>
                                <td width="1%">:</td>
                                <td colspan="5">
                                    <asp:Label ID="lblEmail" runat="server" Visible="False"></asp:Label>
                                    <asp:TextBox ID="txtEmail" runat="server" Width="200px" MaxLength="60" onkeypress="return alphaNumericExceptExcludeSingleQuote(event,'<>?*%$;+=`~{}');"
                                        onblur="omitSomeCharacterExcludeSingleQuote('txtEmail','<>?*%$;');" onchange="this.value=this.value.toUpperCase();"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" Font-Bold="True" ControlToValidate="txtEmail" ErrorMessage="Email harus diisi">*</asp:RequiredFieldValidator>&nbsp;
									<asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" Font-Bold="True" ControlToValidate="txtEmail" ErrorMessage="Email tidak sesuai ! " ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Panel ID="Panel1" runat="server">
                        <table id="Table3" cellspacing="0" cellpadding="0" width="600" border="0">
                            <tr>
                                <td>
                                    <br>
                                    <asp:Panel ID="pnlPerorangan" runat="server">
                                        <table id="titlePanel1" cellspacing="0" cellpadding="0" width="100%" border="0">
                                            <tr>
                                                <td class="titlePanel"><b>PERORANGAN :</b></td>
                                            </tr>
                                            <tr>
                                                <td style="border-bottom: dotted thin; height: 1px">
                                                    <%--<img height="1" src="../images/bg_hor.gif" border="0">--%>
                                                    <%--  <hr />--%>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                    <br>
                                    <asp:Panel ID="pnlPerusahaan" runat="server">
                                        <table id="titlePanel2" cellspacing="0" cellpadding="0" width="100%" border="0">
                                            <tr>
                                                <td class="titlePanel"><b>PERUSAHAAN :</b></td>
                                            </tr>
                                            <tr>
                                                <td style="border-bottom: dotted thin; height: 1px">
                                                    <%--<img height="1" src="../images/bg_hor.gif" border="0">--%>
                                                    <%--  <hr />--%>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                    <br>
                                    <asp:Panel ID="PnlBUMN" runat="server">
                                        <table id="Table4" cellspacing="0" cellpadding="0" width="100%" border="0">
                                            <tr>
                                                <td class="titlePanel"><b>BUMN&nbsp;:</b></td>
                                            </tr>
                                            <tr>
                                                <td style="border-bottom: dotted thin; height: 1px">
                                                    <%--<img height="1" src="../images/bg_hor.gif" border="0">--%>
                                                    <%--  <hr />--%>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                    <asp:Panel ID="PnlLainnya" runat="server">
                                        <table id="Table5" cellspacing="0" cellpadding="0" width="100%" border="0">
                                            <tr>
                                                <td class="titlePanel"><b>Lainnya&nbsp;:</b></td>
                                            </tr>
                                            <tr>
                                                <td style="border-bottom: dotted thin; height: 1px">
                                                    <%--<img height="1" src="../images/bg_hor.gif" border="0">--%>
                                                    <%--  <hr />--%>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                    <asp:Panel ID="pnlTambahan" runat="server">
                                        <table id="titlePanel3" cellspacing="0" cellpadding="0" width="100%" border="0">
                                            <tr>
                                                <td class="titlePanel"><b>TAMBAHAN :</b></td>
                                            </tr>
                                            <tr>
                                                <td style="border-bottom: dotted thin; height: 1px">
                                                    <%--<img height="1" src="../images/bg_hor.gif" border="0">--%>
                                                    <%--  <hr />--%>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td style="border-bottom: dotted thin; height: 1px">
                    <%--<img height="1" src="../images/bg_hor.gif" border="0">--%>
                    <%--  <hr />--%>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnSave" runat="server" CausesValidation="True" Text="Simpan"></asp:Button>
                    <asp:Button ID="btnBack" runat="server" CausesValidation="False" Text="Kembali"></asp:Button>
                </td>
            </tr>
        </table>
        </TD></TR></TBODY></TABLE>

              <!-- Modal -->
        <div id="pleaseWaitDialog" class="modal" style="display: none" data-backdrop="static">
            <div class="center  ">
                <img alt="" src="../Images/ajax-loader.gif" class="img center-block" style="position: fixed; top: 50%; left: 50%; margin-top: -50px; margin-left: -100px;" />

            </div>
        </div>
        <!--end: Modal -->

        <input id="hdnMCPConfirmation" type="hidden" runat="server" name="hdnMCPConfirmation">
        <input id="hdnVerifyMCP" type="hidden" runat="server" name="hdnVerifyMCP">



        <input id="hdnLKPPConfirmation" type="hidden" runat="server" name="hdnLKPPConfirmation">
        <input id="hdnVerifyLKPP" type="hidden" runat="server" name="hdnVerifyLKPP">
        <script>UpdatePanelHeight();</script>
    </form>

    <script src="../WebResources/ocr/bootstrap.min.js"></script>


    <script>
        $(document).ready(function () {


            try {
                var isIE = window.ActiveXObject || "ActiveXObject" in window;
                if (isIE) {
                    $('.modal').removeClass('fade');
                }
            } catch (e) {

            }
            //try {
            //    alert(navigator.userAgent);
            //} catch (e) {

            //}
            //try {

            //    alert($.browser.version);
            //    if ($.browser.version > 10) {
            //        $('.modal').removeClass('fade');
            //    }


            //} catch (e) {
            //    alert(e.message);
            //}



            try {
                showLoadingPage();
            } catch (e) {

            }

            try {
                //if ($.browser.version > 10) {
                //    $('.modal').removeClass('fade');
                //}
            } catch (e) {

            }

            try {

                var hdnUpload = document.getElementById('hdnUpload');
                var hdnGetApi = document.getElementById('hdnGetApi');
                if (hdnUpload.value == "1" && hdnGetApi.value == "0") {
                    hdnGetApi.value = "1";
                    setTimeout(GetData, 5000)
                }
            } catch (e) {

            }
        });

        function shoModal() {

            $('#pleaseWaitDialog').modal();

        }

        function hidModal() {
            $('#pleaseWaitDialog').modal('hide');
        }
    </script>

    <script language="javascript">

        function addEventListeners() {

            try {
                document.getElementById('photoView').addEventListener('click', imageOnClick);
            } catch (e) {

            }

        }


        function GetData() {
            var btnGetApi = document.getElementById('btnGetApi');
            if (btnGetApi) btnGetApi.click();
        }

        function imageOnClick(evt) {
            var photoView = document.getElementById('photoView');
            var imageSource = photoView.getAttribute('src');
            var lblAttachment = document.getElementById('lblAttachment').innerHTML;


            if (lblAttachment == null || lblAttachment == '' || lblAttachment == undefined) {

                return false;
            }


            showPopUp('../PopUp/PopUpImage.aspx?url=' + lblAttachment, '', 500, 760, DealerSelection);
            return false;
        }

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

        function UploadFile() {
            var btnUpload = document.getElementById('btnUpload');
            var hdnUpload = document.getElementById('hdnUpload');
            hdnUpload.value = "1";

            showLoadingPage();

            try {

                if (btnUpload) btnUpload.click();
            } catch (e) {
                alert(e.message);
            }
            return true;

        }

        function showLoadingPage() {

            try {
                var hdnUpload = document.getElementById('hdnUpload');
                var hdnEnabled = document.getElementById('hdnEnabled');
                var hdnNameEnabled = document.getElementById('hdnNameEnabled');

                if (hdnUpload.value == "1") {

                    //$("#tblInformasi").find("input,button,textarea,select").attr("disabled", "disabled");
                    // $("#pnlPerorangan").find("input,button,textarea,select").attr("disabled", "disabled");
                    try {
                        //$("#btnUpload").attr("disabled", "disabled");
                        $('#btnSave').attr("disabled", "disabled");
                    } catch (ef) {

                    }

                    $('#pleaseWaitDialog').modal();
                } else {
                    $('#pleaseWaitDialog').modal('hide');
                    //  $("#tblInformasi").find("input,button,textarea,select").removeAttr("disabled");
                    //  $("#pnlPerorangan").find("input,button,textarea,select").removeAttr("disabled");
                    //$("#btnUpload").removeAttr("disabled");
                    $('#btnSave').removeAttr("disabled");

                    if (hdnNameEnabled.value == "0") {
                        try {
                            $("#txtNama").attr("disabled", "disabled");
                            $("#txtHp").attr("disabled", "disabled");
                        } catch (e) {
                            alert(e.message);
                        }

                    }

                    if (hdnEnabled.value == "" || hdnEnabled.value == "1") {
                        try {
                            $('#btnSave').removeAttr("disabled");

                        } catch (e) {

                        }

                    } else {
                        $('#btnSave').attr("disabled", "disabled");
                    }
                }
            } catch (e) {

            }


            return true;
        }

        try {
            addEventListeners();
        } catch (e) {

        }

        //CR SPK
        function ShowPopUpSPKMasterCountryCode() {

            //var txtSapNo = document.getElementById("txtSapNo");
            showPopUp('../PopUp/PopUpSPKMasterCountry.aspx', '', 460, 760, MasterCountrySelection);
            //showPopUp('../PopUp/PopUpSPKMasterCountry.aspx?FilterIndicator=Unit&IsGroupDealer=0', '', 500, 760, SAPRegisterSelection);
        }

        function MasterCountrySelection(SelectedSalesman) {

            //var indek = GetCurrentInputIndex();
            //var dgSAPCustomer = document.getElementById("dgSAPCustomer");
            var tempParam = SelectedSalesman.split(';');
            var txtCountryCode = document.getElementById("txtCountryCode");
            //var txtCountryName = document.getElementById("txtCountryName");

            txtCountryCode.value = tempParam[0];
            //txtCountryName.value = tempParam[1];
            __doPostBack('__Page', 'search');
        }

        //END


    </script>
</body>
</html>
