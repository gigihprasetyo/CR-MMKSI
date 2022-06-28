<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>

<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmCustomerRequest.aspx.vb" Inherits="FrmCustomerRequest" SmartNavigation="False" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>FrmCustomerRequest</title>
    <meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link rel="stylesheet" type="text/css" href="../WebResources/stylesheet.css">
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
            showPopUp('../PopUp/PopUpCustomerList.aspx?Tyjiuy678=code', '', 500, 760, CustomerSelection);
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
            //__doPostBack('__Page', 'search');
        }

        //END
    </script>
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <table id="Table1" border="0" cellspacing="0" cellpadding="0" width="100%">
            <tr>
                <td class="titlePage">KONSUMEN&nbsp;- Pendaftaran
                </td>
            </tr>
            <tr>
                <td height="1" background="../images/bg_hor.gif">
                    <img border="0" src="../images/bg_hor.gif" height="1"></td>
            </tr>
            <tr>
                <td height="10">
                    <img border="0" src="../images/dot.gif" height="1"></td>
            </tr>
            <tr>
                <td align="left">
                    <table id="Table2" border="0" cellspacing="1" cellpadding="2" width="100%">
                        <tr>
                            <td class="titleField" colspan="7">Data Pembuat Pelanggan</td>
                        </tr>
                        <tr>
                            <td class="titleField" width="28%">Kode Dealer</td>
                            <td width="1%">:</td>
                            <td width="28%">
                                <asp:Label ID="lblDealer" runat="server"></asp:Label><asp:Label ID="lblSearchDealer" runat="server" Visible="False">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup">
                                </asp:Label></td>
                            <td width="2%"></td>
                            <td class="titleField" width="18%"></td>
                            <td width="1%"></td>
                            <td width="28%"></td>
                        </tr>
                        <tr valign="top">
                            <td class="titleField" width="28%">Nomor Pengajuan</td>
                            <td width="1%">:</td>
                            <td width="28%">
                                <asp:Label ID="lblNoPengajuan" runat="server"></asp:Label></td>
                            <td width="2%"></td>
                            <td class="titleField" valign="top" width="18%">Ref No Pengajuan</td>
                            <td width="1%">:</td>
                            <td valign="top" width="28%">
                                <asp:Label ID="lblRefNoPengajuan" runat="server" Visible="False" Width="56px"></asp:Label><asp:TextBox onblur="omitSomeCharacter('txtRefNoPengajuan','<>?*%$;');" ID="txtRefNoPengajuan"
                                    onkeypress="return alphaNumericExcept(event,'?%*$[]{}<>&amp;@~!');" runat="server" Width="80px" MaxLength="50"></asp:TextBox><asp:Label ID="lblRefNomorPengajuan" onclick="ShowNoPengajuanList();" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup">
                                    </asp:Label><asp:LinkButton ID="lnkReloadReff" runat="server" Width="8px" CausesValidation="False">
										<img src="../images/reload.gif" style="cursor:hand" border="0" alt="Reload"></asp:LinkButton></td>
                        </tr>
                        <tr>
                            <td class="titleField" width="28%">Diajukan Oleh</td>
                            <td width="1%">:</td>
                            <td width="28%">
                                <asp:Label ID="lblDiajukanOleh" runat="server" Width="128px"></asp:Label></td>
                            <td width="2%"></td>
                            <td class="titleField" width="18%">Tipe Pengajuan</td>
                            <td width="1%">:</td>
                            <td width="28%">
                                <asp:Label ID="lblTipePengajuan" runat="server" Visible="False"></asp:Label><asp:DropDownList ID="ddlTipePengajuan" runat="server" AutoPostBack="True"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td class="titleField" width="28%">Tanggal Pengajuan</td>
                            <td width="1%">:</td>
                            <td width="28%">
                                <asp:Label ID="lblTglPengajuan" runat="server"></asp:Label></td>
                            <td width="2%"></td>
                            <td class="titleField" width="18%">Diproses Oleh</td>
                            <td width="1%">:</td>
                            <td width="28%">
                                <asp:Label ID="lblDiprosesOleh" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="titleField" width="28%">Status Pengajuan</td>
                            <td width="1%">:</td>
                            <td style="width: 109px" width="28%">
                                <asp:Label ID="lblStatusPengajuan" runat="server"></asp:Label></td>
                            <td width="2%"></td>
                            <td class="titleField" width="18%">Tanggal Proses</td>
                            <td width="1%">:</td>
                            <td width="28%">
                                <asp:Label ID="lblTglProses" runat="server"></asp:Label></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <table id="titleUmum" border="0" cellspacing="0" cellpadding="0" width="100%">
                        <tr>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="titlePanel"><b>INFORMASI PELANGGAN : UMUM</b></td>
                        </tr>
                        <tr>
                            <td height="1" background="../images/bg_hor.gif">
                                <img border="0" src="../images/bg_hor.gif" height="1"></td>
                        </tr>
                    </table>
                    <table border="0" cellspacing="1" cellpadding="2" width="100%">
                        <tbody>
                            <tr>
                                <td class="titleField" width="28%">Kategori</td>
                                <td width="1%">:</td>
                                <td width="28%">
                                    <asp:DropDownList ID="ddlTipe" runat="server" AutoPostBack="True"></asp:DropDownList><asp:Label ID="lblTipe" runat="server" Visible="False"></asp:Label></td>
                                <td width="2%"></td>
                                <td width="18%"></td>
                                <td width="1%"></td>
                                <td width="28%"></td>
                            </tr>
                            <tr>
                                <td class="titleField" width="28%">Tipe</td>
                                <td width="1%">:</td>
                                <td width="28%">
                                    <asp:DropDownList ID="ddlTipePerusahaan" runat="server" Visible="False" AutoPostBack="True"></asp:DropDownList><asp:Label ID="lblTipePerusahaan" runat="server" Visible="False"></asp:Label>
                                    <asp:DropDownList ID="ddlTipePerorangan" AutoPostBack="true" runat="server" Visible="False"></asp:DropDownList><asp:Label ID="lblTipePerorangan" runat="server" Visible="False"></asp:Label></td>
                                <td width="2%"></td>
                                <td width="18%"></td>
                                <td width="1%"></td>
                                <td width="28%"></td>
                            </tr>
                            <tr>
                                <td class="titleField" width="28%">Tipe Identitas</td>
                                <td width="1%">:</td>
                                <td width="28%">
                                    <asp:DropDownList ID="ddlIdentity" runat="server" Visible="true" Width="60px">
                                        <asp:ListItem Text="KTP" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="SIM" Value="1"></asp:ListItem>                        
                                    </asp:DropDownList>
                                    <asp:Label ID="lblIdentity" runat="server" Visible="False"></asp:Label>
                                    <asp:DropDownList ID="ddlIdentityAsing" runat="server" Visible="true" Width="60px">
                                        <asp:ListItem Text="KITAS" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="KITAP" Value="3"></asp:ListItem>                                    
                                    </asp:DropDownList>
                                    <asp:Label ID="lblIdentityAsing" runat="server" Visible="False"></asp:Label>
                                </td>                                        
                                <td width="2%"></td>
                                <td width="18%"><asp:TextBox ID="TxtFlag" runat="server" Visible="False" Width="42px">domestik</asp:TextBox></td>
                                <td width="1%"></td>
                                <td width="28%">
                                    <asp:TextBox ID="TglLahir" runat="server" Width="42px" Visible="False"></asp:TextBox>
                                    <asp:TextBox ID="TglLahirCW" runat="server" Width="42px" Visible="False"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="titleField" width="20%">Kode Pelanggan</td>
                                <td width="1%">:</td>
                                <td width="28%">
                                    <asp:Label ID="lblKodePelanggan" runat="server"></asp:Label></td>
                                <td style="width: 1px" width="1"></td>
                                <td class="titleField">Ref Kode Pelanggan</td>
                                <td width="1%">:</td>
                                <td width="28%" nowrap>
                                    <asp:Label ID="lblRefKodePlgn" runat="server" Visible="False"></asp:Label><asp:TextBox onblur="omitSomeCharacter('txtRefKodePelanggan','<>?*%$;');" ID="txtRefKodePelanggan"
                                        onkeypress="return alphaNumericExcept(event,'?%*$[]+={}<>&amp;@~!');" runat="server" Width="88px" MaxLength="10"></asp:TextBox><asp:Label ID="lbtnRefKode" onclick="ShowCustomerList();" runat="server">
											<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup">
                                        </asp:Label><asp:LinkButton ID="lnkReloadPlg" runat="server" CausesValidation="False">
											<img src="../images/reload.gif" style="cursor:hand" border="0" alt="Reload"></asp:LinkButton></td>
                            </tr>
                            <tr>
                                <td class="titleField" width="20%">Nama 1</td>
                                <td width="1%">:</td>
                                <td colspan="4">
                                    <asp:Label ID="lblNama1" runat="server" Visible="False"></asp:Label>
                                    <asp:TextBox onblur="omitSomeCharacterExcludeSingleQuote('txtNama','<>?*%$;');" ID="txtNama"
                                        onkeypress="return alphaNumericExceptExcludeSingleQuote(event,'<>?*%$;+=`~{}');" onkeyup="autofocus(this,'txtNama2');" runat="server" Width="200px" MaxLength="40" onchange="this.value=this.value.toUpperCase();"></asp:TextBox>
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
                                <td colspan="4">
                                    <asp:Label ID="lblNama2" runat="server" Visible="False"></asp:Label>
                                    <asp:TextBox onblur="omitSomeCharacterExcludeSingleQuote('txtNama2','<>?*%$;');" ID="txtNama2"
                                        onkeypress="return alphaNumericExceptExcludeSingleQuote(event,'<>?*%$;+=`~{}');" onkeyup="autofocus(this,'txtGedung');" runat="server" Width="200px" MaxLength="35" onchange="this.value=this.value.toUpperCase();"
                                        name="txtNama2"></asp:TextBox>
                                    <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator1"
                                        ControlToValidate="txtNama2"
                                        ValidationExpression="^[\s\S]{0,35}$"
                                        ErrorMessage="Panjang maksimal 35 karakter"></asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="titleField" width="20%">Gedung</td>
                                <td width="1%">:</td>
                                <td width="28%">
                                    <asp:Label ID="lblGedung" runat="server" Visible="False"></asp:Label>
                                    <asp:TextBox onblur="omitSomeCharacterExcludeSingleQuote('txtNama','<>?*%$;');" ID="txtGedung"
                                        onkeypress="return alphaNumericExceptExcludeSingleQuote(event,'<>?*%$;+=`~{}');" onkeyup="autofocus(this,'txtAlamat');" runat="server" Width="160px" MaxLength="40" onchange="this.value=this.value.toUpperCase();"></asp:TextBox>&nbsp;
                                    <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator2"
                                        ControlToValidate="txtGedung"
                                        ValidationExpression="^[\s\S]{0,40}$"
                                        ErrorMessage="Panjang maksimal 40 karakter"></asp:RegularExpressionValidator>
                                </td>
                                <td width="25"></td>
                                <td class="titleField" width="20%"></td>
                                <td width="1%"></td>
                                <td width="28%"></td>
                            </tr>
                            <tr>
                                <td class="titleField" width="20%">Alamat</td>
                                <td width="1%">:</td>
                                <td width="28%">
                                    <asp:Label ID="lblAlamat" runat="server" Visible="False"></asp:Label>
                                    <asp:TextBox onblur="omitSomeCharacterExcludeSingleQuote('txtNama','<>?*%$;');" ID="txtAlamat"
                                        onkeypress="return alphaNumericExceptExcludeSingleQuote(event,'<>?*%$;+=`~{}');" onkeyup="autofocus(this,'txtKelurahan');" runat="server" Width="160px" MaxLength="60" onchange="this.value=this.value.toUpperCase();"></asp:TextBox>&nbsp;
						            <asp:RequiredFieldValidator ID="Requiredfieldvalidator2" runat="server" ControlToValidate="txtAlamat" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator3"
                                        ControlToValidate="txtAlamat"
                                        ValidationExpression="^[\s\S]{0,60}$"
                                        ErrorMessage="Panjang maksimal 60 karakter"></asp:RegularExpressionValidator>
                                </td>
                                <td width="1"></td>
                                <td class="titleField" width="20%"></td>
                                <td width="1%"></td>
                                <td width="28%"></td>
                            </tr>
                            <tr>
                                <td class="titleField" width="20%">Kelurahan</td>
                                <td width="1%">:</td>
                                <td>
                                    <asp:Label ID="lblKelurahan" runat="server" Visible="False"></asp:Label>
                                    <asp:TextBox onblur="omitSomeCharacterExcludeSingleQuote('txtNama','<>?*%$;');" ID="txtKelurahan"
                                        onkeypress="return alphaNumericExceptExcludeSingleQuote(event,'<>?*%$;+=`~{}');" onkeyup="autofocus(this,'txtKecamatan');" runat="server" Width="160px" MaxLength="40"
                                        onchange="this.value=this.value.toUpperCase();"></asp:TextBox>&nbsp;
                                    <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator4"
                                        ControlToValidate="txtKelurahan"
                                        ValidationExpression="^[\s\S]{0,40}$"
                                        ErrorMessage="Panjang maksimal 40 karakter"></asp:RegularExpressionValidator>
                                </td>
                                <td style="width: 1px" width="1"></td>
                                <td class="titleField"></td>
                                <td width="1%"></td>
                                <td width="28%"></td>
                            </tr>
                            <tr>
                                <td class="titleField" width="20%">Kecamatan</td>
                                <td width="1%">:</td>
                                <td>
                                    <asp:Label ID="lblKecamatan" runat="server" Visible="False"></asp:Label>
                                    <asp:TextBox onblur="omitSomeCharacterExcludeSingleQuote('txtNama','<>?*%$;');" ID="txtKecamatan"
                                        onkeypress="return alphaNumericExceptExcludeSingleQuote(event,'<>?*%$;+=`~{}');" onkeyup="autofocus(this,'txtKodePos');" runat="server" Width="160px" MaxLength="35"
                                        onchange="this.value=this.value.toUpperCase();"></asp:TextBox>&nbsp;
                                    <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator5"
                                        ControlToValidate="txtKecamatan"
                                        ValidationExpression="^[\s\S]{0,35}$"
                                        ErrorMessage="Panjang maksimal 35 karakter"></asp:RegularExpressionValidator>
                                </td>
                                <td style="width: 1px" width="1"></td>
                                <td class="titleField">Kode Pos</td>
                                <td width="1%">:</td>
                                <td width="28%">
                                    <asp:Label ID="lblKodePos" runat="server" Visible="False"></asp:Label><asp:TextBox onblur="numericOnlyBlur(txtKodePos)" ID="txtKodePos" onkeypress="return numericOnlyUniv(event)"
                                        runat="server" Width="68px" MaxLength="5"></asp:TextBox>&nbsp;</td>
                            </tr>
                            <tr>
                                <td style="height: 18px" class="titleField" width="20%">Propinsi</td>
                                <td style="height: 18px" width="1%">:</td>
                                <td>
                                    <asp:Label ID="lblPropinsi" runat="server" Visible="False"></asp:Label><asp:DropDownList ID="ddlPropinsi" runat="server" AutoPostBack="True"></asp:DropDownList></td>
                                <td style="width: 1px; height: 18px" width="1"></td>
                                <td class="titleField">
                                    <asp:Label ID="lblCetakTitle" runat="server" ToolTip="Print Propinsi di Faktur">Cetak</asp:Label></td>
                                <td style="height: 18px" width="1%">:</td>
                                <td style="height: 18px" width="28%">
                                    <asp:Label ID="lblCetak" runat="server" Visible="False"></asp:Label><asp:DropDownList ID="ddlCetak" runat="server"></asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td class="titleField" width="20%">Kota/Kabupaten</td>
                                <td width="1%">:</td>
                                <td style="width: 293px" width="293">
                                    <asp:Label ID="lblKota" runat="server" Visible="False"></asp:Label><asp:DropDownList ID="ddlPreArea" runat="server">
                                        <asp:ListItem Value="blank" Selected="True">Silahkan Pilih</asp:ListItem>
                                        <asp:ListItem Value="KAB">KAB</asp:ListItem>
                                        <asp:ListItem Value="KODYA">KODYA</asp:ListItem>
                                        <asp:ListItem Value="KABUPATEN">KABUPATEN</asp:ListItem>
                                        <asp:ListItem Value="KOTA MADYA">KOTAMADYA</asp:ListItem>
                                        <asp:ListItem Value="KOTA">KOTA</asp:ListItem>
                                    </asp:DropDownList><asp:DropDownList ID="ddlKota" runat="server"></asp:DropDownList></td>
                                <td style="width: 1px" width="1"></td>
                                <td class="titleField"></td>
                                <td width="1%"></td>
                                <td width="28%"></td>
                            </tr>
                            <tr>
                                <td class="titleField" width="20%">Kode Negara</td>
                                <td width="1%">:</td>
                                <td style="width: 293px" width="293">
                                    <asp:TextBox onkeypress="return HtmlCharUniv(event)" ID="txtCountryCode" TabIndex="14" runat="server"
									MaxLength="50" Width="70px"></asp:TextBox>
								    <asp:RequiredFieldValidator ID="Requiredfieldvalidator4" runat="server" ControlToValidate="txtCountryCode" ErrorMessage="*"></asp:RequiredFieldValidator>
								  
								    <asp:Label ID="lblSearchCountryName" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup">
								    </asp:Label>
                                    <asp:TextBox onkeypress="return HtmlCharUniv(event)" ID="txtNoHp" TabIndex="14" runat="server"
									MaxLength="50" Width="150px"></asp:TextBox>
								    <asp:RequiredFieldValidator ID="Requiredfieldvalidator1" runat="server" ControlToValidate="txtNoHp" ErrorMessage="*"></asp:RequiredFieldValidator>
							    	<asp:LinkButton ID="LinkButton1" Style="display: none" runat="server">Dont remove</asp:LinkButton>
                                <td style="width: 1px" width="1"></td>
                                <td class="titleField">&nbsp;</td>
                                <td width="1%"></td>
                                <td width="28%"></td>
                            </tr>
                            <tr>
                                <td class="titleField" width="20%"></td>
                                <td width="1%"></td>
                                <td>
                                    <asp:Label ID="lblTelepon" runat="server" Visible="False"></asp:Label><asp:TextBox ID="txtTelepon" onkeypress="return numericOnlyUniv(event)" runat="server" Visible="False"
                                        Width="160px" MaxLength="30"></asp:TextBox>&nbsp;</td>
                                <td style="width: 1px" width="1"></td>
                                <td class="titleField">Email</td>
                                <td width="1%"></td>
                                <td width="28%">
                                    <asp:Label ID="lblEmail" runat="server" Visible="False"></asp:Label><asp:TextBox onblur="HtmlCharBlur(txtCode)" ID="txtEmail" onkeypress="return alphaNumericExcept(event,'?+=%*$[]{}<>&amp;~!');"
                                        runat="server" Visible="True" MaxLength="50"></asp:TextBox>&nbsp;</td>
                            </tr>
                            <tr>
                                <td class="titleField" width="20%">Lampiran</td>
                                <td width="1%">:</td>
                                <td colspan="4">
                                    <input style="width: 197px; height: 20px" id="fileUpload" onkeypress="return false;" size="13"
                                        type="file" name="fileUpload" runat="server">&nbsp;&nbsp;&nbsp;&nbsp;
						<asp:LinkButton ID="lbtnDownload" runat="server" Visible="False" CommandName="download">
							<img src="../images/download.gif" border="0" alt="Download">
                        </asp:LinkButton><asp:LinkButton ID="lnkbtnDeleteAttachment" Visible="False" runat="server" ToolTip="Hapus Attachment">
							<img src="../images/trash.gif" style="cursor:hand" border="0" alt="Delete Attachment">
                        </asp:LinkButton></td>
                            </tr>
                    </table>

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
                                                        <td background="../images/bg_hor.gif" height="1">
                                                            <img height="1" src="../images/bg_hor.gif" border="0"></td>
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
                                                        <td background="../images/bg_hor.gif" height="1">
                                                            <img height="1" src="../images/bg_hor.gif" border="0"></td>
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
                                                        <td background="../images/bg_hor.gif" height="1">
                                                            <img height="1" src="../images/bg_hor.gif" border="0"></td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                            <asp:Panel ID="PnlLainnya" runat="server">
                                                <table id="Table5" cellspacing="0" cellpadding="0" width="100%" border="0">
                                                    <tr>
                                                        <td class="titlePanel"><b>Lainnya&nbsp;:</b></td>
                                                    </tr>
                                                    <tr>
                                                        <td background="../images/bg_hor.gif" height="1">
                                                            <img height="1" src="../images/bg_hor.gif" border="0"></td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                            <asp:Panel ID="pnlTambahan" runat="server">
                                                <table id="titlePanel3" cellspacing="0" cellpadding="0" width="100%" border="0">
                                                    <tr>
                                                        <td class="titlePanel"><b>TAMBAHAN :</b></td>
                                                    </tr>
                                                    <tr>
                                                        <td background="../images/bg_hor.gif" height="1">
                                                            <img height="1" src="../images/bg_hor.gif" border="0"></td>
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
                <td>
                    <asp:Button ID="Button1" runat="server" Text="Simpan" Visible="True"></asp:Button>
                    <asp:Button ID="btnBack" runat="server" CausesValidation="False" Text="Kembali"></asp:Button>
                    <asp:Button ID="btnValidasi" runat="server" CausesValidation="False" Text="Validasi" Visible="True"></asp:Button>
                    <asp:Button ID="btnBatalValidasi" runat="server" Visible="False" CausesValidation="False" Text="Batal Validasi"></asp:Button>
                    <asp:Button ID="btnProses" runat="server" Visible="False" Width="50px" Text="Proses"></asp:Button>
                    <asp:Button ID="btnBatalProses" runat="server" Visible="False" Width="70px" Text="Batal Proses"></asp:Button>
                    <asp:Button ID="btnBlock" runat="server" Visible="False" Width="50px" Text="Block"></asp:Button>
                    <asp:Button ID="btnBatalBlock" runat="server" Visible="False" Width="70px" Text="Batal Block"></asp:Button>
                    <asp:Button ID="btnSelesai" runat="server" Visible="False" Width="50px" Text="Selesai"></asp:Button>
                    <asp:Button ID="btnBaru" runat="server" Width="56px" CausesValidation="False" Text="Buat Baru" Visible="false"></asp:Button></td>
            </tr>
            </TBODY>
        </table>
        <input id="hdnMCPConfirmation" type="hidden" runat="server" name="hdnMCPConfirmation">
        <input id="hdnVerifyMCP" type="hidden" runat="server" name="hdnVerifyMCP">
    </form>
</body>
</html>
