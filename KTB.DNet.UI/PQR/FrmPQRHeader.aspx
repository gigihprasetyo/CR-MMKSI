<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmPQRHeader.aspx.vb" Inherits="FrmPQRHeader" SmartNavigation="False" %>

<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head runat="server">
    <title>FrmPQRHeader</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">

    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script language="javascript" src="../WebResources/FormFunctions.js"></script>
    <script language="javascript">
        function KonfirmasiSimpan(obj) {
            var btnSave = document.getElementById(obj.id);
            if (!confirm('Simpan Data PQR ?')) {
                btnSave.disabled = false;
                return false;
            }
            else {
                btnSave.disabled = true;
                document.getElementById('btnSimpan2').click();
                return true;
            }
        }


        function ShowPPDealerBranchSelection() {
            var lblDealer = document.getElementById("lblDealerVal");
            var dealerText = lblDealer.textContent || lblDealer.innerText;
            var dealerCode = dealerText.split("-")[0].replace(/\s/g, '');
            showPopUp('../Service/../PopUp/PopUpDealerBranchSelectionSingle.aspx?m=d&deal=' + dealerCode, '', 500, 760, TemporaryOutlet);
        }

        function TemporaryOutlet(selectedDealer) {
            if (selectedDealer.indexOf(";") > 0) {
                var txtDealerSelection = document.getElementById("txtDealerBranchCode");
                var txtBranchName = document.getElementById("txtBranchName");
                txtDealerSelection.value = selectedDealer.split(";")[0];
                txtBranchName.value = selectedDealer.split(";")[2];
            }
            else {
                var txtDealerSelection = document.getElementById("txtDealerBranchCode");
                txtDealerSelection.value = selectedDealer;
            }
        }

        function ShowPPDealerSelection() {
            showPopUp('../SparePart/../PopUp/PopUpDealerSelectionOne.aspx?x=Territory', '', 500, 760, DealerSelection);
        }

        function DealerSelection(selectedDealer) {
            var tempParam = selectedDealer.split(';');
            var txtDealerSelection = document.getElementById("txtKodeDealer");
            var txtDealerName = document.getElementById("lblDealerVal");
            txtDealerSelection.value = tempParam[0];
            txtDealerName.innerHTML = tempParam[1];
        }
        function GetCurrentInputIndex(GridName) {
            var dtgDamageCode = document.getElementById(GridName);
            var currentRow;
            var index = 0;
            var inputs;
            var indexInput;

            for (index = 0; index < dtgDamageCode.rows.length; index++) {
                inputs = dtgDamageCode.rows[index].getElementsByTagName("INPUT");

                if (inputs != null && inputs.length > 0) {
                    for (indexInput = 0; indexInput < inputs.length; indexInput++) {
                        if (inputs[indexInput].type != "hidden")
                            return index;
                    }
                }
            }
            return -1;
        }

        function GetSelectedDamageCode(selectedCode) {
            var indek = GetCurrentInputIndex("dgKerusakan");
            var dtgDamageCode = document.getElementById("dgKerusakan");
            var tempParam = selectedCode.split(';');
            var KodeDamage = dtgDamageCode.rows[indek].getElementsByTagName("INPUT")[0];
            var DescDamage = dtgDamageCode.rows[indek].getElementsByTagName("SPAN")[1];

            KodeDamage.value = tempParam[0];
            DescDamage.innerHTML = tempParam[1];

        }

        function GetSelectedPartsCode(selectedCode) {
            var indek = GetCurrentInputIndex("dgParts");
            var dtgPartsCode = document.getElementById("dgParts");
            var tempParam = selectedCode.split(';');
            var KodeParts = dtgPartsCode.rows[indek].getElementsByTagName("INPUT")[0];
            var DescParts = dtgPartsCode.rows[indek].getElementsByTagName("SPAN")[1];

            KodeParts.value = tempParam[0];
            DescParts.innerHTML = tempParam[1];

        }

        function ShowPopUp() {
        }

        function BackButton() {
            //var ret = (parseInt(document.getElementById("hid_History").value) + 1)* (-1)
            //document.getElementById("btnBack").disabled=true
            //history.go(ret)
            document.location.href = "../SparePart/FrmPQRList.aspx";
        }
        function focusSave() {
            document.getElementById("btnSimpan").focus();
        }

        function setLastPos(lPosID) {
            var hiddenField = document.getElementById("hfLastPostId");
            hiddenField.value = lPosID;
        }
    </script>
</head>
<body>

    <form id="Form1" method="post" runat="server">
        <input id="hfLastPostId" style="width: 1px; height: 1px" type="hidden" size="1">
        <table id="TableHeader" cellspacing="0" cellpadding="0" width="100%" border="0" runat="server">
            <tr>
                <td class="titlePage">PRODUCT QUALITY REPORT "CONFIDENTIAL"</td>
            </tr>
            <tr>
                <td background="../images/bg_hor.gif" height="1">
                    <img height="1" src="../images/bg_hor.gif" border="0"></td>
            </tr>
            <tr>
                <td height="10">
                    <img height="1" src="../images/dot.gif" border="0"></td>
            </tr>
        </table>
        <table id="TableForm" cellspacing="1" cellpadding="4" width="764" border="0" runat="server">
            <tr valign="top">
                <td width="50%">
                    <table cellspacing="1" cellpadding="2" width="360" border="0">
                        <tr>
                            <td class="titleField" style="width: 126px">
                                <asp:Label ID="lblDealer" runat="server">Dealer : </asp:Label></td>
                            <td>:</td>
                            <td width="34%">
                                <asp:Label ID="lblDealerVal" runat="server" Font-Size="8"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 126px">Kode Cabang</td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="txtDealerBranchCode" Width="150px" runat="server"></asp:TextBox>
                                <asp:Label ID="lblPopUpDealerBranch" onclick="ShowPPDealerBranchSelection()" runat="server" Width="10"> 
										<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0">
                                </asp:Label>

                                <asp:Label ID="lblDealerBranch" runat="server" Visible="False"></asp:Label>

                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 126px">Nama Cabang</td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="txtBranchName" Width="150px" runat="server" BackColor="LightGray"></asp:TextBox>

                            </td>

                        </tr>
                        <tr>
                            <td class="titleField" style="width: 126px">PQR Type</td>
                            <td>:</td>
                            <td>
                                <asp:DropDownList runat="server" ID="ddlPqrType" Width="100%" AutoPostBack="true"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 126px">Dealer Invoice</td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="txtDealerInvoice" Width="150px" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 126px">No PQR</td>
                            <td>:</td>
                            <td>
                                <asp:Label ID="lblPQRNoVal" runat="server">Value Of PQR Number</asp:Label></td>
                        </tr>
                        <%--<tr>
                            <td class="titleField" style="width: 126px">
                                <asp:Label ID="lblRefPQRNo" runat="server">No PQR Ref</asp:Label></td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="txtRefPQRNo" runat="server" Width="160px"></asp:TextBox><asp:Label ID="lblRefPQRNoVal" runat="server" Font-Size="8" Width="196px"></asp:Label></td>
                        </tr>--%>
                        <tr>
                            <td class="titleField" style="width: 126px">
                                <asp:Label ID="lblTglPembuatan" runat="server">Tgl Pembuatan</asp:Label></td>
                            <td>:</td>
                            <td>
                                <asp:Label ID="lblTglPembuatanVal" runat="server">Value Of Tgl Pembuatan</asp:Label></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 126px">
                                <asp:Label ID="lblWONumber" runat="server">Nomor WO</asp:Label></td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="txtWONumber"
                                    runat="server" Width="109px"></asp:TextBox><asp:Label ID="lblWONumberVal" runat="server"></asp:Label>
                                <asp:LinkButton ID="lnkBtnCheckWONumber" runat="server" CausesValidation="False" ToolTip="Validate WO Number">
										<img style="cursor:hand" alt="Check WO Number" src="../images/tanya.gif" border="0"></asp:LinkButton>
                            </td>

                        </tr>
                        <tr>
                            <td class="titleField" style="width: 126px">
                                <asp:Label ID="lblNoChasis" runat="server">No Rangka</asp:Label></td>
                            <td>:</td>
                            <td>
                                <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" ID="txtNoChasis" onblur="omitSomeCharacter('txtNoChasis','<>?*%$;')"
                                    runat="server" Width="160px"></asp:TextBox><asp:RequiredFieldValidator ID="rfvChassisNumber" runat="server" ErrorMessage="No Rangka harus diisi" ControlToValidate="txtNoChasis">*</asp:RequiredFieldValidator>
                                <asp:LinkButton ID="lnkbtnCheckChassis" runat="server" CausesValidation="False" ToolTip="Validate Chassis">
										<img style="cursor:hand" alt="Check Chassis" src="../images/tanya.gif" border="0">
                                </asp:LinkButton><asp:LinkButton ID="lnkbtnPopUpInfoKendaraan" runat="server" CausesValidation="False" ToolTip="View Info Kendaraan">
										<img style="cursor:hand" alt="View Info Kendaraan" src="../images/popup.gif" border="0">
                                </asp:LinkButton><br />
                                <asp:Label ID="lblNoChasisVal" runat="server" Font-Size="8" Style="display: none"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 126px">
                                <asp:Label ID="lblNoMesin" runat="server">No Mesin</asp:Label></td>
                            <td style="height: 16px">
                                <asp:Label ID="lblNoMesinColon" runat="server">:</asp:Label></td>

                            <td style="height: 16px">
                                <asp:Label ID="lblNoMesinVal" runat="server" Width="213">Value Of No Mesin</asp:Label></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 126px">Model</td>
                            <td>:</td>
                            <td>
                                <asp:Label ID="lbModel" runat="server" Width="213">Value Of Model</asp:Label></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 126px">
                                <asp:Label ID="lblTypeColor" runat="server">Tipe / Warna</asp:Label></td>
                            <td>:</td>
                            <td>
                                <asp:Label ID="lblTypeColorVal" runat="server" Width="213">Value Of Type / Color</asp:Label></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 126px">
                                <asp:Label ID="lblNama" runat="server">Nama Pemilik</asp:Label></td>
                            <td>:</td>
                            <td>
                                <asp:Label ID="lblNamaVal" runat="server" Width="213">Value Of Nama</asp:Label></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 126px">
                                <asp:Label ID="lblThnProduksi" runat="server">Tahun Produksi</asp:Label></td>
                            <td>:</td>
                            <td>
                                <asp:Label ID="lblThnProduksiVal" runat="server" Width="213">Value Of Tahun Produksi/Perakitan</asp:Label></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 126px">
                                <asp:Label ID="lblTglDelivery" runat="server">Tanggal Delivery</asp:Label></td>
                            <td>:</td>
                            <td>
                                <asp:Label ID="lblTglDeliveryVal" runat="server" Width="213">Tanggal Delivery</asp:Label></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 126px">
                                <asp:Label ID="lblTglFaktur" runat="server">Tanggal Buka Faktur</asp:Label></td>
                            <td>:</td>
                            <td>
                                <asp:Label ID="lblTglFakturVal" runat="server" Width="213">Tanggal Buka Faktur</asp:Label></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 126px">
                                <asp:Label ID="lblTglPemasangan" runat="server">Tanggal Pemasangan</asp:Label></td>
                            <td>:</td>
                            <td>
                                <cc1:IntiCalendar ID="icTglPemasangan" runat="server" TextBoxWidth="70"></cc1:IntiCalendar><asp:Label ID="lblTglPemasanganVal" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 126px">
                                <asp:Label ID="lblOdoPemasangan" runat="server">Odometer Pemasangan</asp:Label></td>
                            <td>:</td>
                            <td>
                                <asp:TextBox onkeypress="return NumericOnlyWith(event,'');" ID="txtOdoPemasangan" onkeyup="pic(this,this.value,'9999999999','N')"
                                    runat="server" Width="109"></asp:TextBox><asp:RequiredFieldValidator ID="rfvOdoPemasangan" runat="server" ErrorMessage="Odometer harus diisi" ControlToValidate="txtOdoPemasangan">*</asp:RequiredFieldValidator><asp:Label ID="lblOdoPemasanganVal" runat="server"></asp:Label>&nbsp;<span style="font-size: 8pt">Km</span>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 126px">
                                <asp:Label ID="lblTglKerusakan" runat="server">Tanggal Kerusakan</asp:Label></td>
                            <td>:</td>
                            <td>
                                <cc1:IntiCalendar ID="icTglKerusakan" runat="server" TextBoxWidth="70"></cc1:IntiCalendar><asp:Label ID="lblTglKerusakanVal" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 126px">
                                <asp:Label ID="lblOdometer" runat="server">Odometer</asp:Label></td>
                            <td>:</td>
                            <td>
                                <asp:TextBox onkeypress="return NumericOnlyWith(event,'');" ID="txtOdometer" onkeyup="pic(this,this.value,'9999999999','N')"
                                    runat="server" Width="109"></asp:TextBox><asp:RequiredFieldValidator ID="rfvOdometer" runat="server" ErrorMessage="Odometer harus diisi" ControlToValidate="txtOdometer">*</asp:RequiredFieldValidator><asp:Label ID="lblOdometerVal" runat="server"></asp:Label>&nbsp;<span style="font-size: 8pt">Km</span>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 126px">
                                <asp:Label ID="lblKecepatan" runat="server">Kecepatan</asp:Label></td>
                            <td>:</td>
                            <td>
                                <asp:TextBox onkeypress="return NumericOnlyWith(event,'');" ID="txtKecepatan" onkeyup="pic(this,this.value,'9999999999','N')"
                                    runat="server" Width="109px"></asp:TextBox><asp:RequiredFieldValidator ID="rfvKecepatan" runat="server" ErrorMessage="Kecepatan harus diisi" ControlToValidate="txtKecepatan">*</asp:RequiredFieldValidator><asp:Label ID="lblKecepatanVal" runat="server"></asp:Label>&nbsp;<span style="font-size: 8pt">Km 
										/ Jam</span>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 126px">
                                <asp:Label ID="lblSubject" runat="server">Subject</asp:Label></td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="txtSubject" TabIndex="0" runat="server" Width="209px"></asp:TextBox><asp:RequiredFieldValidator ID="rfvSubject" runat="server" ErrorMessage="Subject harus diisi" ControlToValidate="txtSubject">*</asp:RequiredFieldValidator><asp:Label ID="lblSubjectVal" runat="server"></asp:Label></td>
                        </tr>
                        <tr valign="top">
                            <td class="titleField" style="width: 126px">
                                <asp:Label ID="lblGejala" runat="server">Gejala</asp:Label></td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="txtGejala" TabIndex="0" runat="server" Width="209" TextMode="MultiLine" Height="60px"></asp:TextBox><asp:RequiredFieldValidator ID="rfvGejala" runat="server" ErrorMessage="Gejala harus diisi" ControlToValidate="txtGejala">*</asp:RequiredFieldValidator></td>
                        </tr>
                        <tr valign="top">
                            <td class="titleField" style="width: 126px">
                                <asp:Label ID="lblPenyebab" runat="server">Penyebab</asp:Label></td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="txtPenyebab" TabIndex="0" runat="server" Width="208px" TextMode="MultiLine"
                                    Height="60px"></asp:TextBox><asp:RequiredFieldValidator ID="rfvPenyebab" runat="server" ErrorMessage="Penyebab harus diisi" ControlToValidate="txtPenyebab">*</asp:RequiredFieldValidator></td>
                        </tr>
                        <tr valign="top">
                            <td class="titleField" style="width: 126px">
                                <asp:Label ID="lblHasil" runat="server">Perbaikan</asp:Label></td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="txtHasil" TabIndex="0" runat="server" Width="208px" TextMode="MultiLine" Height="60px"></asp:TextBox></td>
                        </tr>
                        <tr valign="top">
                            <td class="titleField" style="width: 126px">
                                <asp:Label ID="lblCatatan" runat="server">Catatan</asp:Label></td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="txtCatatan" TabIndex="0" runat="server" Width="209" TextMode="MultiLine" Height="60px"></asp:TextBox></td>
                        </tr>
                    </table>
                    <br>
                    <font class="titleField"></font>
                    <br>
                    <div id="div2" style="overflow: auto; height: 100px">
                        <asp:DataGrid ID="dgQRS" TabIndex="99" runat="server" Width="100%" BorderWidth="1px" BorderColor="#CDCDCD"
                            CellPadding="3" AutoGenerateColumns="False" ShowFooter="True" BackColor="White" Visible="false">
                            <AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
                            <ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
                            <HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#000084"></HeaderStyle>
                            <FooterStyle ForeColor="Black" BackColor="#EFEFEF"></FooterStyle>
                            <Columns>
                                <asp:TemplateColumn HeaderText="No">
                                    <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="No Rangka">
                                    <HeaderStyle Width="20%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblNoRangka" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ChassisMaster.ChassisNumber") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Wrap="False"></FooterStyle>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtNoRangkaFooter" TabIndex="0" runat="server" Width="60px"></asp:TextBox>
                                        <asp:Label ID="lblPopUpNoRangkaFooter" runat="server" Visible="False">
												<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:Label>
                                    </FooterTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtNoRangkaEdit" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ChassisMaster.ChassisNumber") %>' Width="60">
                                        </asp:TextBox>
                                        <asp:Label ID="lblPopUpNoRangkaEdit" TabIndex="0" runat="server" Height="10px" Visible="False">
												<img style="cursor:hand" alt="Klik Disini untuk memilih Kode Kerusakan" src="../images/popup.gif"
													border="0"></asp:Label>
                                    </EditItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Tgl Kerusakan">
                                    <HeaderStyle Width="20%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblTglKerusakanQRS" runat="server" Text='<%# format(DataBinder.Eval(Container, "DataItem.TglKerusakan"),"dd/MM/yyyy") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Wrap="False"></FooterStyle>
                                    <FooterTemplate>
                                        <cc1:IntiCalendar ID="icTglKerusakanFooter" runat="server" TextBoxWidth="60"></cc1:IntiCalendar>
                                    </FooterTemplate>
                                    <EditItemTemplate>
                                        <cc1:IntiCalendar ID="icTglKerusakanEdit" runat="server" TextBoxWidth="60" Value='<%# DataBinder.Eval(Container, "DataItem.TglKerusakan") %>'>
                                        </cc1:IntiCalendar>

                                    </EditItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Odometer">
                                    <HeaderStyle Width="20%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblOdometerQRS" runat="server" Text='<%# format(DataBinder.Eval(Container, "DataItem.Odometer"),"#,##0") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Wrap="False" HorizontalAlign="Right"></FooterStyle>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtOdometerFooter" runat="server" Width="40px" onkeypress="return NumericOnlyWith(event,'');"
                                            onkeyup="pic(this,this.value,'9999999999','N')" TabIndex="0"></asp:TextBox>
                                    </FooterTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtOdometerEdit" runat="server" Width="40px" onkeypress="return NumericOnlyWith(event,'');" onkeyup="pic(this,this.value,'9999999999','N')" Text='<%# DataBinder.Eval(Container, "DataItem.Odometer") %>' TabIndex="0">
                                        </asp:TextBox>
                                        </asp:Label>
                                    </EditItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Catatan">
                                    <HeaderStyle Width="25%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblCatatanQRS" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Note") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Wrap="False"></FooterStyle>
                                    <FooterTemplate>
                                        <asp:TextBox MaxLength="100" ID="txtCatatanFooter" runat="server" Width="60px" TabIndex="0"></asp:TextBox>
                                    </FooterTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox MaxLength="100" ID="txtCatatanEdit" runat="server" Width="60px" Text='<%# DataBinder.Eval(Container, "DataItem.Note") %>' TabIndex="0">
                                        </asp:TextBox>
                                        </asp:Label>
                                    </EditItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn>
                                    <HeaderStyle Width="50px" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="Linkbutton1" runat="server" CommandName="Edit" CausesValidation="False" TabIndex="0">
												<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
                                        <asp:LinkButton ID="Linkbutton2" runat="server" CommandName="Delete" CausesValidation="False" TabIndex="0">
												<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Center"></FooterStyle>
                                    <FooterTemplate>
                                        <asp:LinkButton ID="Linkbutton3" runat="server" CommandName="Add" CausesValidation="False" TabIndex="0">
												<img src="../images/add.gif" border="0" alt="Tambah"></asp:LinkButton>
                                    </FooterTemplate>
                                    <EditItemTemplate>
                                        <asp:LinkButton ID="Linkbutton4" CommandName="Save" Text="Simpan" runat="server" CausesValidation="False"
                                            TabIndex="0">
												<img src="../images/simpan.gif" border="0" alt="Simpan"></asp:LinkButton>
                                        <asp:LinkButton ID="Linkbutton5" CommandName="Cancel" Text="Batal" runat="server" CausesValidation="False"
                                            TabIndex="0">
												<img src="../images/batal.gif" border="0" alt="Batal"></asp:LinkButton>
                                    </EditItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                            <PagerStyle HorizontalAlign="Center" ForeColor="Black" BackColor="White" Mode="NumericPages"></PagerStyle>
                        </asp:DataGrid>
                    </div>
                </td>
                <td valign="top" width="50%">
                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td>
                                <asp:Panel ID="Panel1" TabIndex="99" runat="server" Height="100%" BorderWidth="0" BorderStyle="Solid"></asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Panel ID="Panel2" TabIndex="99" runat="server" Height="100%" BorderWidth="0" BorderStyle="Solid"></asp:Panel>
                            </td>
                        </tr>
                    </table>
                    <br>
                    <font class="titleField">
                        <asp:Label runat="server" ID="lblPosKerusakan" Text="Posisi Kerusakan"></asp:Label></font>
                    <br>
                    <div id="div1" style="overflow: auto; height: 100px" runat="server">
                        <asp:DataGrid ID="dgKerusakan" TabIndex="99" runat="server" Width="100%" BorderWidth="1px" BorderColor="#CDCDCD"
                            CellPadding="3" AutoGenerateColumns="False" ShowFooter="True" BackColor="White">
                            <AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
                            <ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
                            <HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#000084"></HeaderStyle>
                            <FooterStyle ForeColor="Black" BackColor="#EFEFEF"></FooterStyle>
                            <Columns>
                                <asp:TemplateColumn HeaderText="No">
                                    <HeaderStyle Width="25px" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Kode ">
                                    <HeaderStyle Width="108px" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblKodeDamage" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DeskripsiKodePosisi.KodePosition") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Wrap="False"></FooterStyle>
                                    <FooterTemplate>
                                        <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" ID="txtKodeDamageFooter"
                                            runat="server" Width="84px" TabIndex="0"></asp:TextBox>
                                        <asp:Label ID="lblSearchDamageFooter" runat="server" TabIndex="0">
												<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:Label>
                                    </FooterTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" ID="txtKodeDamageEdit" runat="server" onblur="omitSomeCharacter('txtKodeDamageEdit','<>?*%$;')" Text='<%# DataBinder.Eval(Container, "DataItem.DeskripsiKodePosisi.KodePosition") %>' Width="70">
                                        </asp:TextBox>
                                        <asp:Label ID="lblSearchDamageEdit" TabIndex="0" runat="server" Height="10px">
												<img style="cursor:hand" alt="Klik Disini untuk memilih Kode Kerusakan" src="../images/popup.gif"
													border="0"></asp:Label>
                                    </EditItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Deskripsi">
                                    <HeaderStyle Width="300px" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblDescDamage" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DeskripsiKodePosisi.Description") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Wrap="False"></FooterStyle>
                                    <FooterTemplate>
                                        <asp:Label ID="lblDescDamageFooter" runat="server"></asp:Label>
                                    </FooterTemplate>
                                    <EditItemTemplate>
                                        <asp:Label ID="lblDescDamageEdit" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DeskripsiKodePosisi.Description") %>'>
                                        </asp:Label>
                                    </EditItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn>
                                    <HeaderStyle Width="50px" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkbtnEditDamage" runat="server" CommandName="Edit" CausesValidation="False">
												<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
                                        <asp:LinkButton ID="lnkbtnDeleteDamage" runat="server" CommandName="Delete" CausesValidation="False">
												<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Center"></FooterStyle>
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lnkbtnAddDamage" runat="server" CommandName="Add" CausesValidation="False" TabIndex="0">
												<img src="../images/add.gif" border="0" alt="Tambah"></asp:LinkButton>
                                    </FooterTemplate>
                                    <EditItemTemplate>
                                        <asp:LinkButton ID="lnkbtnSaveDamage" CommandName="Save" Text="Simpan" runat="server" CausesValidation="False">
												<img src="../images/simpan.gif" border="0" alt="Simpan"></asp:LinkButton>
                                        <asp:LinkButton ID="lnkbtnCancelDamage" CommandName="Cancel" Text="Batal" runat="server" CausesValidation="False">
												<img src="../images/batal.gif" border="0" alt="Batal"></asp:LinkButton>
                                    </EditItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                            <PagerStyle HorizontalAlign="Center" ForeColor="Black" BackColor="White" Mode="NumericPages"></PagerStyle>
                        </asp:DataGrid>
                    </div>
                    <br>
                    <font class="titleField">Posisi Parts</font>
                    <br>
                    <div id="div2" style="overflow: auto; height: 100px">
                        <asp:DataGrid ID="dgParts" TabIndex="99" runat="server" Width="100%" BorderWidth="1px" BorderColor="#CDCDCD"
                            CellPadding="3" AutoGenerateColumns="False" ShowFooter="True" BackColor="White">
                            <AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
                            <ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
                            <HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#000084"></HeaderStyle>
                            <FooterStyle ForeColor="Black" BackColor="#EFEFEF"></FooterStyle>
                            <Columns>
                                <asp:TemplateColumn HeaderText="No">
                                    <HeaderStyle Width="25px" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Kode ">
                                    <HeaderStyle Width="108px" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblKodeParts" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SparePartMaster.PartNumber") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Wrap="False"></FooterStyle>
                                    <FooterTemplate>
                                        <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" ID="txtKodePartsFooter"
                                            runat="server" Width="84px" TabIndex="0"></asp:TextBox>
                                        <asp:Label ID="lblSearchPartsFooter" runat="server" TabIndex="0">
												<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:Label>
                                    </FooterTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" ID="txtKodePartsEdit" runat="server" onblur="omitSomeCharacter('txtKodePartsEdit','<>?*%$;')" Text='<%# DataBinder.Eval(Container, "DataItem.SparePartMaster.PartNumber") %>' Width="70">
                                        </asp:TextBox>
                                        <asp:Label ID="lblSearchPartsEdit" TabIndex="0" runat="server" Height="10px">
												<img style="cursor:hand" alt="Klik Disini untuk memilih Kode Kerusakan" src="../images/popup.gif"
													border="0"></asp:Label>
                                    </EditItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Deskripsi">
                                    <HeaderStyle Width="300px" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblDescParts" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SparePartMaster.PartName") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Wrap="False"></FooterStyle>
                                    <FooterTemplate>
                                        <asp:Label ID="lblDescPartsFooter" runat="server"></asp:Label>
                                    </FooterTemplate>
                                    <EditItemTemplate>
                                        <asp:Label ID="lblDescPartsEdit" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SparePartMaster.PartName") %>'>
                                        </asp:Label>
                                    </EditItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn>
                                    <HeaderStyle Width="50px" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkbtnEditParts" runat="server" CommandName="Edit" CausesValidation="False">
												<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
                                        <asp:LinkButton ID="lnkbtnDeleteParts" runat="server" CommandName="Delete" CausesValidation="False">
												<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Center"></FooterStyle>
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lnkbtnAddParts" runat="server" CommandName="Add" CausesValidation="False" TabIndex="0">
												<img src="../images/add.gif" border="0" alt="Tambah"></asp:LinkButton>
                                    </FooterTemplate>
                                    <EditItemTemplate>
                                        <asp:LinkButton ID="lnkbtnSaveParts" CommandName="Save" Text="Simpan" runat="server" CausesValidation="False">
												<img src="../images/simpan.gif" border="0" alt="Simpan"></asp:LinkButton>
                                        <asp:LinkButton ID="lnkbtnCancelParts" CommandName="Cancel" Text="Batal" runat="server" CausesValidation="False">
												<img src="../images/batal.gif" border="0" alt="Batal"></asp:LinkButton>
                                    </EditItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                            <PagerStyle HorizontalAlign="Center" ForeColor="Black" BackColor="White" Mode="NumericPages"></PagerStyle>
                        </asp:DataGrid>
                    </div>
                    <br>
                    <table cellspacing="0" cellpadding="0" border="0" runat="server" id="tblKodeKerusakan"
                        width="100%">
                        <tr>
                            <td colspan="4"><strong>Kode Kerusakan</strong></td>
                        </tr>
                        <tr>
                            <td style="height: 2px" width="10%" align="center"><strong>A</strong></td>
                            <td width="5%" style="height: 2px">:</td>
                            <td style="height: 2px" width="85%">
                                <asp:DropDownList runat="server" ID="ddlKodeWSCA" Width="100%"></asp:DropDownList>
                            </td>
                            <td style="height: 2px"></td>
                        </tr>
                        <tr>
                            <td width="10%" align="center"><strong>B</strong></td>
                            <td width="5%">:</td>
                            <td width="85%">
                                <asp:DropDownList runat="server" ID="ddlKodeWSCB" Width="100%"></asp:DropDownList>
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td width="10%" align="center"><strong>C</strong></td>
                            <td width="5%">:</td>
                            <td width="85%">
                                <asp:DropDownList runat="server" ID="ddlKodeWSCC" Width="100%"></asp:DropDownList>
                            </td>
                            <td></td>
                        </tr>
                    </table>
                    <br>
                    <strong>Lampiran</strong>&nbsp;
						<br>
                    <div id="div2" style="overflow: auto; height: 100px">
                        <asp:DataGrid ID="dgFileAttachmentTop" TabIndex="99" runat="server" Width="100%" BorderWidth="1px"
                            BorderColor="#CDCDCD" CellPadding="3" AutoGenerateColumns="False" ShowFooter="True" BackColor="White">
                            <AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
                            <ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
                            <HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#000084"></HeaderStyle>
                            <FooterStyle ForeColor="Black" BackColor="#EFEFEF"></FooterStyle>
                            <Columns>
                                <asp:TemplateColumn HeaderText="No">
                                    <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Literal ID="ltrFileAttachmentTopNo" runat="server"></asp:Literal>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="File">
                                    <HeaderStyle Width="90%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <%--<asp:LinkButton ID="lnkbtnFileAttachmentTop" runat="server" CommandName="Download" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.Attachment") %>'>
												<%# DataBinder.Eval(Container, "DataItem.FileName") %>
                                        </asp:LinkButton>--%>
                                        <asp:Label ID="lbllFileAttachmentTop" runat="server" ><%# DataBinder.Eval(Container, "DataItem.FileName") %></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Wrap="False"></FooterStyle>
                                    <FooterTemplate>
                                        <input type="file" id="iFileAttachmentTop" runat="server" tabindex="0" accept=".docx,.doc,.xls,.xlsx,.Jpg,.jpeg,.pdf,.MP3,.MP4,.PNG,.PPT">
                                    </FooterTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn>
                                    <HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkbtnFileAttachmentTopDelete" runat="server" CommandName="Delete" CausesValidation="False">
												<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Center"></FooterStyle>
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lnkbtnFileAttachmentTopAdd" runat="server" CommandName="Add" CausesValidation="False"
                                            TabIndex="0">
												<img src="../images/add.gif" border="0" alt="Tambah"></asp:LinkButton>
                                    </FooterTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                            <PagerStyle HorizontalAlign="Center" ForeColor="Black" BackColor="White" Mode="NumericPages"></PagerStyle>
                        </asp:DataGrid>
                    </div>
                    <asp:Button ID="btnDownloadAll" runat="server" Text="Download Lampiran"></asp:Button>
                    <br>
                    <table style="margin: 5% 0px" align="center" border="0">
                        <tr>
                            <td class="titleField">
                                <asp:Label ID="lblStatus" runat="server" Font-Size="8">Status</asp:Label></td>
                            <td>:</td>
                            <td>
                                <asp:Label ID="lblStatusVal" runat="server" Font-Size="8"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="titleField">
                                <asp:Label ID="lblAppliedBy" runat="server" Font-Size="8"> Dealer</asp:Label></td>
                            <td>:</td>
                            <td>
                                <asp:Label ID="lblAppliedByVal" runat="server" Font-Size="8"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="titleField">
                                <asp:Label ID="lblTglJam" runat="server" Font-Size="8">Tanggal/Jam</asp:Label></td>
                            <td>:</td>
                            <td>
                                <asp:Label ID="lblTglJamVal" runat="server" Font-Size="8"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="titleField">
                                <asp:Label ID="lblProcessBy" runat="server" Font-Size="8">MKS</asp:Label></td>
                            <td>:</td>
                            <td>
                                <asp:Label ID="lblProcessByVal" runat="server" Font-Size="8"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="titleField">
                                <asp:Label ID="lblTglJamProcess" runat="server" Font-Size="8"> Tanggal/Jam</asp:Label></td>
                            <td>:</td>
                            <td>
                                <asp:Label ID="lblTglJamProcessVal" runat="server" Font-Size="8"></asp:Label></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr valign="top">
                <td style="width: 398px"><font class="titleField">Tambahan Info :</font>
                    <asp:Literal ID="ltrStatusAdditionalInfo" runat="server"></asp:Literal><asp:Label ID="lblLastPostedInfo" runat="server">
                        <img src="../images/icon_mail.gif" border="0" runat="server" id="img">
                    </asp:Label><asp:LinkButton ID="lnkbtnAdditionalInfoPopUp" runat="server" CausesValidation="False">
							<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup">
                    </asp:LinkButton><br>
                    <font class="titleField">Penjelasan MMKSI&nbsp;:</font>
                    <br>
                    <asp:TextBox ID="txtSolution" TabIndex="0" runat="server" Width="360px" TextMode="MultiLine"
                        Height="130px"></asp:TextBox><br>
                </td>
                <td>
                    <asp:Label class="titleField" ID="lblBobot" runat="server">Bobot : </asp:Label><asp:DropDownList ID="ddlBobot" TabIndex="1" runat="server"></asp:DropDownList><br>
                    <font class="titleField">Lampiran</font>
                    <div id="div3" style="overflow: auto; height: 100px">
                        <asp:DataGrid ID="dgFileAttachmentBottom" TabIndex="99" runat="server" Width="100%" BorderWidth="1px"
                            BorderColor="#CDCDCD" CellPadding="3" AutoGenerateColumns="False" ShowFooter="True" BackColor="White">
                            <AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
                            <ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
                            <HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#000084"></HeaderStyle>
                            <FooterStyle ForeColor="Black" BackColor="#EFEFEF"></FooterStyle>
                            <Columns>
                                <asp:TemplateColumn HeaderText="No">
                                    <HeaderStyle Width="25px" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Literal ID="ltrFileAttachmentBottomNo" runat="server"></asp:Literal>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="File">
                                    <HeaderStyle Width="400px" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkbtnFileAttachmentBottom" CommandName="Download" runat="server" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.Attachment") %>'>
												<%# DataBinder.Eval(Container, "DataItem.FileName") %>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <FooterStyle Wrap="False"></FooterStyle>
                                    <FooterTemplate>
                                        <input type="file" id="iFileAttachmentBottom" runat="server" name="iFileAttachmentBottom">
                                    </FooterTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn>
                                    <HeaderStyle Width="25px" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkbtnFileAttachmentBottomDelete" runat="server" CommandName="Delete" CausesValidation="False">
												<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Center"></FooterStyle>
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lnkbtnFileAttachmentBottomAdd" runat="server" CommandName="Add" CausesValidation="False">
												<img src="../images/add.gif" border="0" alt="Tambah"></asp:LinkButton>
                                    </FooterTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                            <PagerStyle HorizontalAlign="Center" ForeColor="Black" BackColor="White" Mode="NumericPages"></PagerStyle>
                        </asp:DataGrid>
                    </div>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2">
                    <asp:Button ID="btnSimpan2" Style="display: none" TabIndex="0" runat="server" Text="Simpan"></asp:Button>
                    <asp:Button ID="btnSimpan" TabIndex="0" runat="server" Text="Simpan" OnClientClick="return KonfirmasiSimpan(this);"></asp:Button>&nbsp;&nbsp;
						<asp:Button ID="btnCancelStatusChange" runat="server"></asp:Button>&nbsp;&nbsp;
						<asp:Button ID="btnStatusChange" runat="server"></asp:Button>&nbsp;&nbsp;
						<asp:Button ID="btnBatal" runat="server" CausesValidation="False" Text="Kembali"></asp:Button>&nbsp;&nbsp;
						<asp:Button ID="btnCetak" runat="server" CausesValidation="False" Text="Cetak"></asp:Button><asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False"></asp:ValidationSummary>
                </td>
            </tr>
        </table>
        <%--<input id="hdnConfirmWSC" type="hidden" runat="server" name="hdnConfirmWSC"/>--%>
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
    </form>

</body>
</html>
