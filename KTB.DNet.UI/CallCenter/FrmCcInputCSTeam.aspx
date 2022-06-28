<%@ Page Language="vb" ViewStateEncryptionMode="Always" AutoEventWireup="false" CodeBehind="FrmCcInputCSTeam.aspx.vb" Inherits=".FrmInputCSTeam" SmartNavigation="False" Async="true" %>


<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>FrmAplikasiHeader</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript">
        /* Deddy H	validasi value *********************************** */
        /* ini untuk handle char yg tdk diperbolehkan, saat paste */
        function TxtBlur(objTxt) {
            omitSomeCharacter(objTxt, '<>?*%$;');
        }
        /* ini untuk handle char yg tdk diperbolehkan, saat keypress */
        function TxtKeypress() {
            return alphaNumericExcept(event, '<>?*%$;');
        }

        function imageOnClick(evt) {
            var photoView = document.getElementById('photoView');
            var imageSource = photoView.getAttribute('src');
            window.open(
                imageSource,
                '1511239697381',
                'width=700,height=500,toolbar=0,menubar=0,location=0,status=1,scrollbars=1,resizable=1,left=0,top=0'
            );
            return false;
        }

    </script>
    <style type="text/css">
        .Button1 {
            background-image: url('http://i.microsoft.com/global/en-us/homepage/PublishingImages/sprites/microsoft_gray.png');
            border: 0;
            width: 30px;
            background-color: transparent;
        }
    </style>
    <script language="javascript" type="text/javascript">

        function HandleSparePartPanel() {
            var chkSparePart = document.getElementById("SparePartIndicator");
            var pnl = document.getElementById("pnlSparePart");
            if (chkSparePart.checked) {
                pnl.style.display = "block";
            }
            else {
                pnl.style.display = "none";
            }

        }

        function imageOnClick(evt) {
            var photoView = document.getElementById('photoView');
            var imageSource = photoView.getAttribute('src');
            window.open(
                imageSource,
                '1511239697381',
                'width=700,height=500,toolbar=0,menubar=0,location=0,status=1,scrollbars=1,resizable=1,left=0,top=0'
            );
            return false;
        }

        function ShowPopUpSuperior() {
            var ddlJobPositionDesc = document.getElementById("ddlJobPositionDesc");
            var Position = ddlJobPositionDesc.value;
            var lblDealerCodes = document.getElementById("lblDealerCode");
            var oDealerSalesman = lblDealerCodes.innerHTML
            if (ddlJobPositionDesc.value == '') {
                alert('Kategori harus dipilih dulu !');
                return;
            }
            //showPopUp('../PopUp/PopUpSalesman.aspx?PositionID=' + Position,'',600,600,SuperiorSelection);
            showPopUp('../PopUp/PopUpSalesman.aspx?PositionID=' + Position + '&DealerSalesman=' + oDealerSalesman, '', 600, 600, SuperiorSelection);
        }

        function SuperiorSelection(SelectedSuperior) {

            var tempParam = SelectedSuperior.split(';');
            var txtSuperior = document.getElementById("txtSuperior");
            var txtSuperiorName = document.getElementById("txtSuperiorName");
            //alert(txtSuperior);

            if (navigator.appName == "Microsoft Internet Explorer") {
                txtSuperior.innerText = tempParam[0];
                txtSuperiorName.innerText = tempParam[1];
            }
            else {
                txtSuperior.value = tempParam[0];
                txtSuperiorName.value = tempParam[1];
            }
        }


        function ShowPopUpDealerHistory() {
            showPopUp('../PopUp/PopUpSelectingDealer.aspx', '', 600, 600, DealerSelectionHistory);
        }


        function DealerSelectionHistory(SelectedHistory) {
            var dtgHistory = document.getElementById("dtgHistory");
            var indek = GetCurrentInputIndex(dtgHistory);
            //alert(indek);
            var tempParam = SelectedHistory.split(';');
            var KodeArea = dtgHistory.rows[indek].getElementsByTagName("INPUT")[0];
            var DescArea = dtgHistory.rows[indek].getElementsByTagName("SPAN")[1];

            if (navigator.appName == "Microsoft Internet Explorer") {
                KodeArea.innerText = tempParam[0];
                DescArea.innerHTML = tempParam[1];
            }
            else {
                KodeArea.value = tempParam[0];
                DescArea.value = tempParam[1];
            }
        }

        function ShowPopUpSalesManArea() {
            //var myDate = new Date( );
            //showPopUp('../PopUp/PopUpSalesmanArea.aspx?time='+myDate.getTime( ),'',600,600,AreaSelection);
            showPopUp('../PopUp/PopUpSalesmanArea.aspx', '', 600, 600, AreaSelection);
        }


        function AreaSelection(SelectedArea) {

            var dtgArea = document.getElementById("dtgArea");
            var indek = GetCurrentInputIndex(dtgArea);
            var tempParam = SelectedArea.split(';');
            var KodeArea = dtgArea.rows[indek].getElementsByTagName("INPUT")[0];
            var DescArea = dtgArea.rows[indek].getElementsByTagName("SPAN")[1];

            if (navigator.appName == "Microsoft Internet Explorer") {
                KodeArea.innerText = tempParam[0];
                DescArea.innerHTML = tempParam[1];
            }
            else {
                KodeArea.value = tempParam[0];
                DescArea.value = tempParam[1];
            }
        }

        function GetCurrentInputIndex(dtg) {
            //var dtgArea = document.getElementById("dtgArea");
            var currentRow;
            var index = 0;
            var inputs;
            var indexInput;
            for (index = 0; index < dtg.rows.length; index++) {
                inputs = dtg.rows[index].getElementsByTagName("INPUT");

                if (inputs != null && inputs.length > 0) {
                    for (indexInput = 0; indexInput < inputs.length; indexInput++) {
                        if (inputs[indexInput].type != "hidden")
                            return index;
                    }
                }
            }
            return -1;
        }


        function SetDealer(Parameter) {
            arrParam = Parameter.split(";");
            document.Form1.txtDealerCode.value = trim(arrParam[0]);
            document.Form1.txtDealerName.value = trim(arrParam[1]);

        }

        function ShowPPDealerSelection() {
            showPopUp('../PopUp/PopUpSelectingDealer.aspx?multi=' + true, '', 600, 600, DealerSelection);
        }
        function DealerSelection(selectedDealer) {
            var txtDealerCodeSelection = document.getElementById("txtDealerCode");
            var txtDealerNameSelection = document.getElementById("txtDealerName");
            var arrValue = selectedDealer.split(';');

            txtDealerCodeSelection.value = arrValue[0];
            txtDealerNameSelection.value = arrValue[1];
        }

        function ShowJobPosSelection() {
            showPopUp('../PopUp/PopUpJobPosition.aspx?Menu=1', '', 600, 600, JobPosSelection);
        }
        function JobPosSelection(selectedJobPos) {
            var txtJobPos = document.getElementById("txtJobPosition");
            var txtJobPosDesc = document.getElementById("txtJobPositionDesc");
            var arrValue = selectedJobPos.split(';');
            txtJobPos.value = arrValue[0];
            txtJobPosDesc.value = arrValue[1];

        }
        // popUp City
        function ShowCitySelection() {
            showPopUp('../PopUp/PopUpCity.aspx', '', 600, 600, CitySelection);
        }
        function CitySelection(selectedCity) {
            var txtKota = document.getElementById("txtKota");
            var arrValue = selectedCity.split(';');
            txtKota.value = arrValue[1];
        }

        // comment
        function ShowLeadJobPosSelection() {
            showPopUp('../PopUp/PopUpJobPosition.aspx?Menu=1', '', 600, 600, LeadJobPosSelection);
        }
        function LeadJobPosSelection(selectedLeadJobPos) {
            var txtLeadJobPos = document.getElementById("txtLeadJobPosition");
            var txtLeadJobPosDesc = document.getElementById("txtLeadJobPositionDesc");
            var arrValue = selectedLeadJobPos.split(';');
            txtLeadJobPos.value = arrValue[0];
            txtLeadJobPosDesc.value = arrValue[1];
        }

        function ShowSalesmanSelection() {
            var lblSalesmanCode = document.getElementById("lblSalesmanCode");
            alert(lblSalesmanCode.innerText);
            showPopUp('../PopUp/PopUpSAPRegisterSalesman.aspx?SSCode=' + lblSalesmanCode.innerText, '', 600, 600, SalesmanSelection);
        }

        function SalesmanSelection(SelectedSalesman) {
            var tempParam = SelectedSalesman.split(';');
            var txtLeaderName = document.getElementById("txtLeaderName");
            var txtLeadJobPositionDesc = document.getElementById("txtLeadJobPositionDesc");
            var txtLeadJobPosition = document.getElementById("txtLeadJobPosition");
            var txtSalesmanCode = document.getElementById("txtSalesmanCode");

            if (navigator.appName == "Microsoft Internet Explorer") {
                txtLeaderName.innerText = tempParam[1];
                txtLeadJobPositionDesc.innerText = tempParam[3];
            }
            else {
                txtLeaderName.value = tempParam[1];
                txtLeadJobPositionDesc.value = tempParam[3];
            }
            txtSalesmanCode.value = tempParam[0]
            txtLeadJobPosition.value = tempParam[2];
        }

        function ShowSalesmanResign() {
            var txtMode = document.getElementById("txtMode");
            showPopUp('../PopUp/PopUpCsTeam.aspx?Code=Resign&IsResign=1&IsSales=99' + '&Mode=' + txtMode.value, '', 500, 760, SalesmanResignSelection);
        }

        function SalesmanResignSelection(selectedVal) {
            var tempParam = selectedVal.split(';');
            var txtRefSalesman = document.getElementById("txtRefSalesman");
            txtRefSalesman.value = tempParam[0];
        }
        function ShowPopUpPartShop() {
            //alert('test');
            showPopUp('../PopUp/PopUppartshop.aspx', '', 500, 600, PartShopSelection);
        }

        function PartShopSelection(SelectedPartShop) {
            var dgPartshop = document.getElementById("dgPartshop");
            var indek = GetCurrentInputIndex(dgPartshop);
            var tempParam = SelectedPartShop.split(';');
            var Code = dgPartshop.rows[indek].getElementsByTagName("INPUT")[0];
            var Name = dgPartshop.rows[indek].getElementsByTagName("SPAN")[1];
            var City = dgPartshop.rows[indek].getElementsByTagName("SPAN")[2];

            if (navigator.appName == "Microsoft Internet Explorer") {
                Code.innerText = tempParam[0];
                Name.innerHTML = tempParam[1];
                City.innerHTML = tempParam[2];
            }
            else {
                Code.value = tempParam[0];
                Name.value = tempParam[1];
                City.value = tempParam[2];
            }
        }
    </script>
</head>
<body>
    <form id="Form1" method="post" runat="server">
        <table id="Table2" cellspacing="0" cellpadding="0" border="0">
            <tr>
                <td class="titlePage" style="height: 17px">
                    <asp:Label ID="lblPageTitle" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td background="../images/bg_hor.gif" height="1">
                    <img height="1" src="../images/bg_hor.gif" border="0"></td>
            </tr>
            <tr>
                <td style="height: 6px" height="6">
                    <img height="1" src="../images/dot.gif" border="0"></td>
            </tr>
            <tr>
                <td style="width: 100%">
                    <table id="Table1" cellspacing="1" cellpadding="2" width="100%" border="0">
                        <tbody>
                            <tr>
                                <td class="titleField" width="180">Indicator</td>
                                <td>
                                    <asp:Label ID="Label5" runat="server">:</asp:Label></td>
                                <td width="263">
                                    <asp:DropDownList ID="ddlIndicator" runat="server"></asp:DropDownList>
                                    <asp:Label ID="lblIndicator" runat="server" Visible="False"></asp:Label>
                                    <asp:RequiredFieldValidator ID="Requiredfieldvalidator9" runat="server" ControlToValidate="ddlIndicator" EnableClientScript="false" ErrorMessage="Indicator Harus dipilih">*</asp:RequiredFieldValidator>
                                </td>
                                <td colspan="3">
                                    <asp:Label ID="lblRef" runat="server">Employee Ref :</asp:Label>
                                    <asp:TextBox ID="txtRefSalesman"
                                        runat="server"
                                        Width="100px" MaxLength="100"></asp:TextBox>
                                    <asp:Label ID="lbtnRefSalesman" onclick="ShowSalesmanResign();" runat="server">
											<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup">
                                    </asp:Label>
                                    <asp:LinkButton ID="lnkReloadSalesman" runat="server" CausesValidation="False">
											<img src="../images/reload.gif" style="cursor:hand" border="0" alt="Reload">
                                    </asp:LinkButton>
                                </td>
                            </tr>
                            <tr valign="top">
                                <td class="titleField" width="130">Kode Dealer</td>
                                <td>
                                    <asp:Label ID="lblColon3" runat="server">:</asp:Label></td>
                                <td width="263">
                                    <asp:Label ID="lblDealerCode" runat="server"></asp:Label></td>
                                <td colspan="3" rowspan="6">
                                    <table cellspacing="1" cellpadding="2" width="100%" border="0">
                                        <tr>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div id="divPhoto" style="overflow: auto; width: 300px; height: 150px" align="center">
                                                    <asp:Image ID="photoView" runat="server" Width="300px" Height="150px"></asp:Image>
                                                </div>
                                                <asp:Button ID="btnRemoveFile" Visible="False" runat="server" CausesValidation="False" Text="Remove Picture"></asp:Button>
                                                <asp:LinkButton ID="lblRemoveImage" runat="server" CausesValidation="False" Text="Hapus Photo" CommandName="deleteImage">
														<img src="../images/trash.gif" border="0" alt="Hapus Photo"></asp:LinkButton></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblUploadKTP" runat="server" Text="Upload Foto"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <input onkeypress="return false;" id="photoSrc" tabindex="19" type="file" size="29" name="File1"
                                                                runat="server">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:CustomValidator ID="cvImage" runat="server" ErrorMessage="Foto harus diupload" ControlToValidate="" OnServerValidate="cvImage_ServerValidate"></asp:CustomValidator>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>

                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="titleField" width="130">Nama Dealer</td>
                                <td>
                                    <asp:Label ID="Label1" runat="server">:</asp:Label></td>
                                <td width="263">
                                    <asp:Label ID="lblDealerName" runat="server"></asp:Label></td>
                            </tr>

                            <tr>
                                <td class="titleField" width="130">
                                    <asp:Label ID="lblKodeSalesman" runat="server"></asp:Label></td>
                                <td>:</td>
                                <td nowrap width="263">
                                    <asp:Label ID="lblSalesmanCode" runat="server"></asp:Label></td>
                            </tr>

                            <tr>
                                <td class="titleField" width="130">Nama</td>
                                <td>:</td>
                                <td nowrap width="263">
                                    <asp:TextBox onkeypress="TxtKeypress();" ID="txtName" onblur="TxtBlur('txtName');" TabIndex="4"
                                        runat="server" Width="128px" MaxLength="60"></asp:TextBox><asp:Label ID="lblName" runat="server" Visible="False"></asp:Label><asp:RequiredFieldValidator ID="valName" runat="server" ControlToValidate="txtName" EnableClientScript="false" ErrorMessage="Nama Harus diisi">*</asp:RequiredFieldValidator></td>
                            </tr>

                            <tr valign="top">
                                <td class="titleField" width="130">Tempat/Tgl Lahir</td>
                                <td>:</td>
                                <td style="width: 263px">
                                    <table cellspacing="0" cellpadding="0" border="0">
                                        <tr>
                                            <td style="width: 117px">
                                                <asp:TextBox onkeypress="TxtKeypress();" ID="txtPlaceOfBirth" onblur="TxtBlur('txtPlaceOfBirth');"
                                                    TabIndex="6" runat="server" Width="110px" MaxLength="60"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtPlaceOfBirth"
                                                        ErrorMessage="Tempat Lahir Harus diisi" EnableClientScript="false">*</asp:RequiredFieldValidator><asp:Label ID="lblPlaceOfBirth" runat="server" Visible="False"></asp:Label></td>
                                            <td style="width: 4px">/</td>
                                            <td>
                                                <cc1:IntiCalendar ID="ICDateOfBirth" TabIndex="8" runat="server"></cc1:IntiCalendar><asp:Label ID="lblDateOfBirth" runat="server" Visible="False"></asp:Label></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="titleField" width="130">Jenis Kelamin</td>
                                <td>:</td>
                                <td nowrap width="263">
                                    <asp:DropDownList ID="ddlGender" TabIndex="10" runat="server"></asp:DropDownList><asp:Label ID="lblGender" runat="server" Visible="False"></asp:Label><asp:CustomValidator ID="cvGender" runat="server" ErrorMessage="Jenis Kelamin harus dipilih" ControlToValidate="" OnServerValidate="cvGender_ServerValidate">*</asp:CustomValidator></td>
                            </tr>
                            <tr>
                                <td class="titleField" width="130">Status Perkawinan</td>
                                <td>:</td>
                                <td nowrap width="263">
                                    <asp:DropDownList ID="ddlMarriedStatus" TabIndex="11" runat="server"></asp:DropDownList><asp:Label ID="lblMarriedStatus" runat="server" Visible="False"></asp:Label><asp:CustomValidator ID="cvMarriedStatus" runat="server" ControlToValidate="" ErrorMessage="Status Perkawinan harus dipilih" OnServerValidate="cvMarriedStatus_ServerValidate">*</asp:CustomValidator></td>

                                <td colspan="3" rowspan="6">
                                    <table cellspacing="1" cellpadding="2" width="100%" border="0">
                                        <tr>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div id="divPhoto" style="overflow: auto; width: 300px; height: 150px" align="center">
                                                    <asp:Image ID="ktpView" runat="server" Width="300px" Height="150px"></asp:Image>
                                                </div>
                                                <asp:Button ID="btnRemoveKtp" Visible="False" runat="server" CausesValidation="False" Text="Remove Picture"></asp:Button>
                                                <asp:LinkButton ID="lblRemoveKtp" runat="server" CausesValidation="False" Text="Hapus Photo" CommandName="deleteImage">
														<img src="../images/trash.gif" border="0" alt="Hapus Photo"></asp:LinkButton></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label2" runat="server" Text="Upload KTP"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <input onkeypress="return false;" id="ktpSrc" tabindex="19" type="file" size="29" name="File1"
                                                                runat="server">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:CustomValidator ID="cvKtp" runat="server" ErrorMessage="KTP harus diupload" ControlToValidate="" OnServerValidate="cvKtp_ServerValidate"></asp:CustomValidator>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>

                                    </table>
                                </td>
                            </tr>

                            <tr valign="top">
                                <td class="titleField" width="130">Alamat</td>
                                <td>:</td>
                                <td nowrap width="263">
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:TextBox onkeypress="TxtKeypress();" ID="txtAlamat" onblur="TxtBlur('txtAlamat');" TabIndex="13"
                                                    runat="server" Width="232px" MaxLength="200" TextMode="MultiLine"></asp:TextBox><asp:Label ID="lblAlamat" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:CustomValidator ID="cvAlamat" runat="server" ErrorMessage="Alamat harus diisi" ControlToValidate="" OnServerValidate="cvAlamat_ServerValidate"></asp:CustomValidator>
                                            </td>
                                        </tr>
                                    </table>

                                </td>
                            </tr>
                            <tr>
                                <td class="titleField" width="130" style="height: 21px">Propinsi</td>
                                <td style="height: 21px">:</td>
                                <td nowrap width="263" style="height: 21px">
                                    <asp:DropDownList ID="ddlPropinsi" runat="server" AutoPostBack="True" TabIndex="14"></asp:DropDownList><asp:Label ID="lblPropinsi" runat="server" Visible="False"></asp:Label><asp:CustomValidator ID="cvPropinsi" runat="server" ControlToValidate="" ErrorMessage="Propinsi harus dipilih" OnServerValidate="cvPropinsi_ServerValidate">*</asp:CustomValidator></td>


                            </tr>
                            <tr>
                                <td class="titleField" width="130">Kota</td>
                                <td>:</td>
                                <td nowrap width="263">
                                    <asp:DropDownList ID="ddlKota" runat="server" TabIndex="15"></asp:DropDownList><asp:Label ID="lblKota" runat="server" Visible="False"></asp:Label><asp:CustomValidator ID="cvKota" runat="server" ControlToValidate="" ErrorMessage="Kota harus dipilih" OnServerValidate="cvKota_ServerValidate">*</asp:CustomValidator></td>
                            </tr>
                            <tr>
                                <td colspan="3" width="100%">
                                    <asp:Panel ID="Panel1" TabIndex="18" runat="server" Width="100%"></asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td class="titleField" style="height: 20px" width="130">Posisi</td>
                                <td style="height: 20px">:</td>
                                <td style="width: 263px; height: 20px" nowrap width="263">
                                    <asp:DropDownList ID="ddlJobPositionDesc" runat="server" TabIndex="16" CausesValidation="false" AutoPostBack="true"></asp:DropDownList>
                                    <asp:Label ID="lblJobPositionDesc" runat="server" Visible="False"></asp:Label>
                                    <asp:CustomValidator ID="cvJobPositionDesc" runat="server" ControlToValidate="" ErrorMessage="Posisi harus dipilih" OnServerValidate="cvJobPositionDesc_ServerValidate">*</asp:CustomValidator></td>
                            </tr>

                            <tr visible="false">

                                <td class="titleField" style="height: 20px" width="130"></td>
                                <td style="height: 20px"></td>
                                <%-- <td style="width: 263px; height: 20px" nowrap width="263">
                                    <asp:DropDownList ID="ddlSalesmanLevel" TabIndex="17" runat="server" ></asp:DropDownList><asp:Label ID="lblSalesmanLevel" runat="server" Visible="False"></asp:Label><asp:RequiredFieldValidator ID="Requiredfieldvalidator6" runat="server" ControlToValidate="ddlSalesmanLevel"
                                        ErrorMessage="Level Harus dipilih">*</asp:RequiredFieldValidator></td>--%>

                                <td>
                                    <asp:DropDownList ID="ddlSalesmanLevel" runat="server" TabIndex="16" Visible="False"></asp:DropDownList>
                                    <asp:Label ID="lblSalesmanLevel" runat="server" Visible="False"></asp:Label>
                                </td>
                                <td class="titleField" colspan="3">
                                    <asp:Panel ID="pnlSuperior" runat="server">
                                        <asp:Label ID="lblSuperior" runat="server">Atasan :</asp:Label>
                                        <table>
                                            <tr>
                                                <td width="70">
                                                    <asp:TextBox onkeypress="TxtKeypress();" ID="txtSuperior" onblur="TxtBlur('txtName');" TabIndex="4"
                                                        runat="server" MaxLength="10" Width="60px"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="ValAtasan" runat="server" ErrorMessage="Atasan Harus diisi" ControlToValidate="txtSuperior">*</asp:RequiredFieldValidator></td>
                                                <td>
                                                    <asp:TextBox onkeypress="TxtKeypress();" ID="txtSuperiorName" onblur="TxtBlur('txtName');" TabIndex="4"
                                                        runat="server" BorderColor="Silver" BorderStyle="None"></asp:TextBox></td>
                                                <td>
                                                    <asp:Label ID="Label4" onclick="ShowPopUpSuperior();" runat="server">
															<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup">
                                                    </asp:Label></td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>

                            <tr id="rowKategoriTim" runat="server" visible="false">
                                <td class="titleField" style="height: 20px" width="130">Kategori Tim</td>
                                <td style="height: 20px">:</td>
                                <td style="width: 263px; height: 20px" nowrap width="263">
                                    <asp:DropDownList ID="ddlKategoriTim" runat="server"></asp:DropDownList>
                                </td>
                            </tr>

                            <tr id="trUploadAppointmentLetter" runat="server">
                                <td class="titleField" width="130" style="height: 21px">Upload Surat Pengangkatan</td>
                                <td style="height: 21px">:</td>
                                <td nowrap width="263" style="height: 21px">
                                    <input onkeypress="return false;" id="uplAppointmentLetter" style="width: 200px; height: 20px" type="file"
                                        size="46" name="fileUpload" runat="server" accept="application/pdf">
                                    <br />
                                    <asp:Label ID="lblAppointmentLetterWarn" runat="server" Text="* File Pdf dengan ukuran maksimal 500Kb"></asp:Label></td>
                            </tr>

                              <tr id="trDownloadAppointmentLetter" runat="server">
                                <td class="titleField" width="130" style="height: 21px">Download Surat Pengangkatan</td>
                                <td style="height: 21px">:</td>
                                <td nowrap width="263" style="height: 21px">
                                     <asp:LinkButton ID="lnkAppointmentLetter" runat="server" Text="Download"></asp:LinkButton>
                                    <asp:HiddenField id="hdnAppointmentLetterPath" runat="server"/></td>
                            </tr>

                </td>
                <td class="titleField" style="width: 136px; height: 20px" width="136"></td>
                <td style="height: 20px" width="1%"></td>
                <td style="height: 20px" width="29%"></td>
            </tr>
            <tr>
                <td class="titleField" style="height: 20px" width="130">Tgl Masuk</td>
                <td style="height: 20px">:</td>
                <td style="width: 263px; height: 20px" nowrap width="263">
                    <cc1:IntiCalendar ID="ICStartWork" TabIndex="17" runat="server" CanPostBack="True"></cc1:IntiCalendar><asp:Label ID="lblStartWork" runat="server" Visible="False"></asp:Label></td>
                <td class="titleField" style="width: 136px; height: 20px" width="136">
                    <input id="txtMode" style="width: 88px; height: 20px" type="hidden" size="9" name="txtMode"
                        runat="server"></td>
                <td style="height: 20px" width="1%"></td>
                <td style="height: 20px" width="29%"></td>
            </tr>
            <tr>
                <td class="titleField" style="height: 20px" width="130">Tgl Keluar</td>
                <td style="height: 20px">:</td>
                <td style="width: 263px; height: 20px" nowrap width="263">
                    <cc1:IntiCalendar ID="ICEndWork" runat="server" Enabled="False"></cc1:IntiCalendar><asp:Label ID="lblEndWork" runat="server" Visible="False"></asp:Label></td>
                <td class="titleField" style="width: 136px; height: 20px" width="136">
                    <input id="txtJobPosition" style="width: 88px; height: 20px" type="hidden" size="9" name="txtJobPosition"
                        runat="server"></td>
                <td style="height: 20px" width="1%">&nbsp;</td>
                <td style="height: 20px" width="29%">&nbsp;</td>
            </tr>
            <%--<tr>
                <td class="titleField" style="height: 20px" width="130">Gaji</td>
                <td style=" height: 20px" >:</td>
                <td style="width: 263px; height: 20px" nowrap width="263">
                    <asp:TextBox ID="txtSalary" runat="server" Width="120px" MaxLength="60" TabIndex="19" TextMode="Number"></asp:TextBox>
                    <asp:Label ID="lblSalary" runat="server" Visible="False"></asp:Label></td>
                <td class="titleField" style="width: 136px; height: 20px" width="136"></td>
                <td style="height: 20px" width="1%"></td>
                <td style="height: 20px" width="29%"></td>
            </tr>--%>

            <tr>
                <td class="titleField" style="height: 20px" width="130">Status</td>
                <td style="height: 20px">:</td>
                <td style="width: 263px; height: 20px" nowrap width="263">
                    <asp:DropDownList ID="ddlStatus" runat="server" TabIndex="20">
                    </asp:DropDownList><asp:Label ID="lblStatus" runat="server" Visible="False"></asp:Label></td>
                <td class="titleField" style="width: 136px; height: 20px" width="136"></td>
                <td style="height: 20px" width="1%"></td>
                <td style="height: 20px" width="29%"></td>
            </tr>
            <tr>
                <td class="titleField" style="height: 20px" width="130"></td>
                <td style="height: 20px"></td>
                <td style="width: 263px; height: 20px" nowrap width="263"></td>
                <td class="titleField" style="width: 136px; height: 20px" width="136"></td>
                <td style="height: 20px" width="1%"></td>
                <td style="height: 20px" width="29%"></td>
            </tr>
            <tr>
                <td class="titleField" style="height: 20px" width="130"></td>
                <td style="height: 20px"></td>
                <td style="width: 263px; height: 20px" nowrap width="263" colspan="4">
                    <%-- <Table ID="Table3" Border="1">
                        <tr>
                            <td valign="middle" align="center" class="titleSubField"><asp:Label ID="Label2" runat="server" Text="Modul" ></asp:Label></td>
                            <td valign="middle" align="center"class="titleSubField"><asp:Label ID="Label3" runat="server" Text="Tempat / Tanggal"></asp:Label></td>
                            <td valign="middle" align="center"class="titleSubField"><asp:Label ID="Label6" runat="server" Text="Penyelenggara"></asp:Label></td>
                            <td></td>
                        </tr>
                        <tbody>
                            <tr>
                                <td><asp:TextBox ID="txtModul" runat="server" Width="150px" MaxLength="200" ></asp:TextBox></td>
                                <td><asp:TextBox ID="txtTempatTanggal" runat="server" Width="150px" MaxLength="200" ></asp:TextBox></td>
                                <td><asp:TextBox ID="txtPenyelenggara" runat="server" Width="150px" MaxLength="200" ></asp:TextBox></td>
                                <td>
                                    <asp:LinkButton ID="lbtnAddTrain" runat="server" CausesValidation="False" CommandName="addTrain">
												<img src="../images/add.gif" border="0" alt="Tambah"></asp:LinkButton>
                                </td>
                            </tr>
                        </tbody>
                    </Table>--%>
                </td>
            </tr>

            <tr>
                <td class="titleField" style="height: 20px" width="130"></td>
                <td style="height: 20px" colspan="5">
                    <div id="div1" style="overflow: auto; height: 100px">
                        <%--<asp:datagrid id="dtgTraining" runat="server" Width="100%" BorderColor="Gainsboro" AllowCustomPaging="True"
											AllowPaging="True" BackColor="#CDCDCD" AutoGenerateColumns="False" ShowFooter="True" CellPadding="3" BorderWidth="0px" AllowSorting="True"
											CellSpacing="1" PageSize="25">
											<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
											<AlternatingItemStyle ForeColor="Black" BackColor="White"></AlternatingItemStyle>
											<ItemStyle ForeColor="Black" BackColor="#F1F6FB"></ItemStyle>
											<HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
											<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
											<Columns>
												<asp:TemplateColumn HeaderText="No">
													<HeaderStyle Width="2%" CssClass="titleTableRsd"></HeaderStyle>
												</asp:TemplateColumn>
                                                <asp:TemplateColumn SortExpression="ID" HeaderText="ID" Visible="false" >
													<HeaderStyle HorizontalAlign="Right" Width="10%" CssClass="titleTableRsd"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="lblID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID")%>' >
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="TrainingModule" HeaderText="Modul">
													<HeaderStyle HorizontalAlign="Right" Width="10%" CssClass="titleTableRsd"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="lblModulTraining" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TrainingModule") %>' >
														</asp:Label>
													</ItemTemplate>
													<FooterTemplate>
														<asp:TextBox id="txtModulTrainingAdd" onkeypress="return alphaNumericExcept(event,'<>?*%$;')"
															onblur="omitCharsOnCompsTxt(this,'<>?*%$;')" runat="server"></asp:TextBox>
													</FooterTemplate>
													<EditItemTemplate>
														<asp:TextBox id="txtModulTrainingEdit" runat="server" onkeypress="return alphaNumericExcept(event,'<>?*%$;')" onblur="omitCharsOnCompsTxt(this,'<>?*%$;')" Text='<%# DataBinder.Eval(Container, "DataItem.TrainingModule") %>'>
														</asp:TextBox>
													</EditItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="TrainingPlaceAndDate" HeaderText="Tempat/Tanggal">
													<HeaderStyle HorizontalAlign="Right" Width="10%" CssClass="titleTableRsd"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="lblTempatTanggal" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TrainingPlaceAndDate") %>' >
														</asp:Label>
													</ItemTemplate>
													<FooterTemplate>
														<asp:TextBox id="txtTempatTanggalAdd" runat="server" onkeypress="return alphaNumericExcept(event,'<>?*%$;')"
															onblur="omitCharsOnCompsTxt(this,'<>?*%$;')"></asp:TextBox>
													</FooterTemplate>
													<EditItemTemplate>
														<asp:TextBox id="txtTempatTanggalEdit" runat="server" onkeypress="return alphaNumericExcept(event,'<>?*%$;')" onblur="omitCharsOnCompsTxt(this,'<>?*%$;')" Text='<%# DataBinder.Eval(Container, "DataItem.TrainingPlaceAndDate") %>' >
														</asp:TextBox>
													</EditItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="TrainingProvider" HeaderText="Penyelenggara">
													<HeaderStyle HorizontalAlign="Right" Width="10%" CssClass="titleTableRsd"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="lblPenyelenggara" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TrainingProvider") %>' >
														</asp:Label>
													</ItemTemplate>
													<FooterTemplate>
														<asp:TextBox id="txtPenyelenggaraAdd" runat="server" onkeypress="return alphaNumericExcept(event,'<>?*%$;')"
															onblur="omitCharsOnCompsTxt(this,'<>?*%$;')"></asp:TextBox>
													</FooterTemplate>
													<EditItemTemplate>
														<asp:TextBox id="txtPenyelenggaraEdit" runat="server" onkeypress="return alphaNumericExcept(event,'<>?*%$;')" onblur="omitCharsOnCompsTxt(this,'<>?*%$;')" Text='<%# DataBinder.Eval(Container, "DataItem.TrainingProvider") %>' >
														</asp:TextBox>
													</EditItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="aksi">
													<HeaderStyle HorizontalAlign="Center" Width="10%" CssClass="titleTableRsd"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<asp:LinkButton id="lbtnEditTrain" CausesValidation="False" CommandName="editTrain" text="Ubah"
															Runat="server">
															<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
														<asp:LinkButton id="lbtnDeleteTrain" CausesValidation="False" CommandName="deleteTrain" text="Hapus"
															Runat="server">
															<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
													</ItemTemplate>
													<FooterStyle HorizontalAlign="Center"></FooterStyle>
													<FooterTemplate>
														<asp:LinkButton id="Linkbutton1" runat="server" CausesValidation="False" CommandName="addTrain">
															<img src="../images/add.gif" border="0" alt="Tambah"></asp:LinkButton>
													</FooterTemplate>
													<EditItemTemplate>
														<asp:LinkButton id="lbtnSaveTrain" tabIndex="40" CommandName="saveTrain" text="Simpan" Runat="server"
															CausesValidation="False">
															<img src="../images/simpan.gif" border="0" alt="Simpan"></asp:LinkButton>
														<asp:LinkButton id="Linkbutton6" tabIndex="50" CommandName="cancelTrain" text="Batal" Runat="server"
															CausesValidation="False">
															<img src="../images/batal.gif" border="0" alt="Batal"></asp:LinkButton>
													</EditItemTemplate>
												</asp:TemplateColumn>
											</Columns>
											<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
										</asp:datagrid>--%>
                    </div>
                </td>
            </tr>
            <tr>
                <td class="titleField" style="height: 20px" width="130"></td>
                <td style="height: 20px"></td>
                <td style="width: 263px; height: 20px" nowrap width="263">
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" Width="208px"></asp:ValidationSummary>
                </td>
                <td class="titleField" style="width: 136px; height: 20px" width="136"></td>
                <td style="height: 20px" width="1%"></td>
                <td style="height: 20px" width="29%"></td>
            </tr>
            <tr>
                <td class="titleField" style="height: 20px" width="130"></td>
                <td style="height: 20px"></td>
                <td style="height: 20px" colspan="4">
                    <asp:Button ID="btnSimpan" TabIndex="27" runat="server" Width="60px" Text="Simpan"></asp:Button>
                    <asp:Button ID="btnRequestID" TabIndex="28" runat="server" Width="60px" Text="Request ID"></asp:Button>
                    <asp:Button ID="btnGenerateCode" TabIndex="29" runat="server" Text="Generate Code"></asp:Button>
                    <%--<asp:Button ID="btn" TabIndex="28" runat="server" Text="Generate Code"></asp:Button>--%>
                    <asp:Button ID="btnBatal" TabIndex="30" runat="server" Width="60px" CausesValidation="False" Text="Batal"></asp:Button>
                    <asp:Button ID="btnKembali" TabIndex="31" runat="server" Width="60px" CausesValidation="False" Text="Kembali"></asp:Button>
                </td>
            </tr>
            <tr>
                <td class="titleField" style="height: 20px" width="130"></td>
                <td style="height: 20px"></td>
                <td style="width: 263px; height: 20px" nowrap width="238"></td>
                <td class="titleField" style="width: 136px; height: 20px" width="136"></td>
                <td style="height: 20px" width="1%"></td>
                <td style="height: 20px" width="29%"></td>
            </tr>
            <tr>
                <td class="titleField" style="height: 20px" width="130"></td>
                <td style="height: 20px"></td>
                <td style="width: 263px; height: 20px" nowrap width="238"></td>
                <td class="titleField" style="width: 136px; height: 20px" width="136"></td>
                <td style="height: 20px" width="1%"></td>
                <td style="height: 20px" width="29%"></td>
            </tr>
            <tr valign="top">
                <td class="titleField" style="height: 20px" width="130"></td>
                <td class="auto-style3">&nbsp;</td>
                <td style="height: 20px"></td>
                <td nowrap colspan="4">&nbsp;</td>
            </tr>
        </table>

        <input id="hdnVal" type="hidden" name="hdnVal" runat="server">
        <input id="IsInsertSuccess" type="hidden" name="IsInsertSuccess" runat="server">
    </form>
    <script language="javascript">
        var IsInsertSuccess = document.getElementById("IsInsertSuccess");
        if (IsInsertSuccess.value == '1') {
            alert('Data berhasil disimpan');
        }
    </script>
</body>
</html>
