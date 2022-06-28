<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmInputEventParticipantList.aspx.vb" Inherits="FrmInputEventParticipantList" SmartNavigation="False" %>

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
            //showPopUp('../General/../PopUp/PopUpDealerSelection.aspx', '', 500, 760, DealerSelection);
            showPopUp('../General/../Benefit/PopUpDealerSelectionBenefit.aspx', '', 500, 760, DealerSelection);
        }

        function DealerSelection(selectedDealer) {
            var txtDealerSelection = document.getElementById("txtKodeDealer");
            txtDealerSelection.value = selectedDealer;
            var txtbenefitReg = document.getElementById("txtbenefitReg");
            //txtbenefitReg.value = "";
        }

        function ShowPPBenefitSelection() {
            var selectbox = getElement("input", "txtKodeDealer")
            //showPopUp('../General/../Benefit/PopUpBenefit.aspx?dealer=' + selectbox.value, '', 500, 760, BenefitSelection);
            showPopUp('../General/../Benefit/PopUpBenefit.aspx?dealer=' + selectbox.value + "&event=1", '', 500, 760, BenefitSelection);
        }

        function BenefitSelection(selectedBenefit) {
            var txtbenefitReg = document.getElementById("txtbenefitReg");
            txtbenefitReg.value = selectedBenefit;
        }

        function ShowPopUpEvent() {

            showPopUp('../PopUp/PopUpEventNational.aspx?m=d', '', 430, 800, EventSelection);
        }

        function EventSelection(selectedRefNumber) {
            var txtNoRegEvent = document.getElementById("txtNoRegEvent");
            txtNoRegEvent.value = selectedRefNumber.split(";")[0];
            __doPostBack("txtNoRegEvent", "");
        }

        function ShowPopUpSPK() {
            var txtNoRegEvent = document.getElementById("txtNoRegEvent");
            showPopUp('../PopUp/PopUpSPK.aspx?m=d' + '&EventCode=' + txtNoRegEvent.value, '', 430, 800, SpkSelection);
        }

        function GetCurrentInputIndex() {
            var dgSPDetail = document.getElementById("dgTable");
            var currentRow;
            var index = 0;
            var inputs;
            var indexInput;

            for (index = 0; index < dgSPDetail.rows.length; index++) {
                inputs = dgSPDetail.rows[index].getElementsByTagName("INPUT");

                if (inputs != null && inputs.length > 0) {
                    for (indexInput = 0; indexInput < inputs.length; indexInput++) {
                        if (inputs[indexInput].type != "hidden")
                            return index;
                    }
                }
            }
            return -1;
        }

        function SpkSelection(selectedType) {
            var indek = GetCurrentInputIndex();
            var dgSPDetail = document.getElementById("dgTable");
            var SPKNumber = dgSPDetail.rows[indek].getElementsByTagName("INPUT")[0];
            SPKNumber.value = selectedType.split(";")[0];
            var txtNamaGrid = dgSPDetail.rows[indek].getElementsByTagName("INPUT")[1];
            var txtKtpGrid = dgSPDetail.rows[indek].getElementsByTagName("INPUT")[2];
            var txtAlamatGrid = dgSPDetail.rows[indek].getElementsByTagName("INPUT")[3];
            txtNamaGrid.value = selectedType.split(";")[1];
            txtKtpGrid.value = selectedType.split(";")[2];
            txtAlamatGrid.value = selectedType.split(";")[3];
        }

        //function SpkSelection(selectedRefNumber) {
        //    var txtKeteranganFooter = document.getElementById("txtKeteranganFooter");
        //    txtKeteranganFooter.value = selectedRefNumber.split(";")[0];
        //    __doPostBack("txtKeteranganFooter", "");
        //}

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


        }

    </script>
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <table id="Table2" cellspacing="5" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage" style="height: 17px" colspan="2">SALES CAMPAIGN - Input Peserta Event</td>
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
                <td class="titleField" width="20%">Kode Dealer&nbsp;</td>
                <td>
                    <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" ID="txtKodeDealer" onblur="omitSomeCharacter('txtKeyWords','<>?*%$;')"
                        runat="server" Width="242px"></asp:TextBox>
                    &nbsp;<asp:Label ID="lblPopUpDealer" runat="server" Width="16px">
							<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0"></asp:Label>
                    <asp:Label ID="lblDelerSession" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="titleField" width="20%">Benefit Reg&nbsp;</td>
                <td>
                    <asp:TextBox ID="txtbenefitReg" runat="server" Width="242px"></asp:TextBox>
                    &nbsp;<asp:Label ID="lblPopUpBenefit" runat="server" Width="16px">
							<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="titleField" width="20%">Nama Event&nbsp;</td>
                <td>
                    <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" ID="txtEventName" onblur="omitSomeCharacter('txtKeyWords','<>?*%$;')"
                        runat="server" Width="242px"></asp:TextBox>
                    &nbsp;<asp:Label ID="lblPopUpEvent" runat="server" Width="16px">
							<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="titleField" width="20%">No Reg Event&nbsp;</td>
                <td>
                    <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" ID="txtNoRegEvent" onblur="omitSomeCharacter('txtKeyWords','<>?*%$;')"
                        Enabled="true" runat="server" Width="242px" OnTextChanged="txtNoRegEvent_TextChanged" AutoPostBack="true"></asp:TextBox>

                </td>
            </tr>
            <tr>
                <td class="titleField" width="20%">Tanggal Event&nbsp;</td>
                <td>
                    <table cellspacing="0" cellpadding="0" border="0">
                        <tr valign="top">
                            <td>
                                <cc1:IntiCalendar ID="icTanggalEvent" runat="server" TextBoxWidth="70" Enabled="false"></cc1:IntiCalendar>
                            </td>
                            <td valign="bottom">&nbsp;&nbsp;s.d&nbsp;&nbsp;</td>
                            <td>
                                <cc1:IntiCalendar ID="icTanggalEventEnd" runat="server" TextBoxWidth="70" Enabled="false"></cc1:IntiCalendar>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="titleField" width="20%">Upload File &nbsp;</td>
                <td>
                    <asp:FileUpload ID="fileUploadParticipant" runat="server" />
                    <asp:Button ID="btnUpload" runat="server" Text="Upload" />
                </td>
            </tr>
            <tr>
                <td class="titleField" width="20%"></td>
                <td>
                    <asp:LinkButton ID="LinkDownload" runat="server">Template Participant</asp:LinkButton>
                </td>
            </tr>
            <tr style="visibility: hidden">
                <td class="titleField" width="20%">Status&nbsp;</td>
                <td>
                    <asp:DropDownList ID="ddlStatus" runat="server"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="titleField" width="20%"></td>
                <td>
                    <asp:Button ID="btnSimpan" runat="server" Text="Simpan" Width="60px"></asp:Button>&nbsp;
						  <asp:Button ID="btnBatal" runat="server" Text="Batal" Width="60px"></asp:Button>&nbsp;
                         &nbsp; &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnDelete" runat="server" Text="Hapus" Width="60px" CausesValidation="False"></asp:Button>

                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <table id="Table1" cellspacing="1" cellpadding="2" width="100%" border="0">
                        <tr>
                            <td valign="top" colspan="6">
                                <div id="div1" style="overflow: auto; height: 440px">
                                    <asp:DataGrid ID="dgTable" runat="server" Width="100%" AutoGenerateColumns="False" BackColor="#CDCDCD" BorderColor="Gainsboro" CellSpacing="1" BorderWidth="0px"
                                        CellPadding="3" DataKeyField="ID">
                                        <AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
                                        <ItemStyle BackColor="White"></ItemStyle>
                                        <HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Blue"></HeaderStyle>
                                        <Columns>
                                            <asp:TemplateColumn HeaderText="" Visible="false">
                                                <HeaderStyle Width="2%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="cbAllGrid" runat="server" />
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:CheckBox ID="cbAllGridGrid" runat="server" />
                                                </FooterTemplate>
                                                <EditItemTemplate>
                                                    <asp:CheckBox ID="cbAllGridGridEdit" runat="server" />
                                                </EditItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="No">
                                                <HeaderStyle Width="2%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNoGrid" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Nomor SPK Dnet">
                                                <HeaderStyle Width="25%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblKeterangan" runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtKeteranganFooter" runat="server" Width="85%"></asp:TextBox>
                                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFooterTipe" ErrorMessage="*" ControlToValidate="txtKeteranganFooter" ValidationGroup="footer"></asp:RequiredFieldValidator>
													<asp:Label id="lblFooterPopUpSPK" runat="server"><img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:Label>
                                                </FooterTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtKeteranganEdit" runat="server" Width="85%"></asp:TextBox>
                                                    <asp:RequiredFieldValidator runat="server" ID="RequiredEditTipe" ErrorMessage="*" ControlToValidate="txtKeteranganEdit"></asp:RequiredFieldValidator>
													<asp:Label id="lblEditPopUpSPK" runat="server"><img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:Label>
                                                </EditItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Nama Peserta">
                                                <HeaderStyle Width="20%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNamaGrid" runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtNamaGrid" runat="server" Width="100%"></asp:TextBox>
                                                </FooterTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtNamaGridEdit" runat="server" Width="100%"></asp:TextBox>
                                                </EditItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="No KTP">
                                                <HeaderStyle Width="15%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblKtpGrid" runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtKtpGrid" runat="server" Width="100%"></asp:TextBox>
                                                </FooterTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtKtpGridEdit" runat="server" Width="100%"></asp:TextBox>
                                                </EditItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Alamat">
                                                <HeaderStyle Width="45%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAlamatGrid" runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtAlamatGrid" runat="server" Width="100%"></asp:TextBox>
                                                </FooterTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtAlamatGridEdit" runat="server" Width="100%"></asp:TextBox>
                                                </EditItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn>
                                                <HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                <ItemTemplate>

                                                    <asp:LinkButton ID="lnkbtnEdit" runat="server" Width="20px" Text="Ubah" CausesValidation="False" CommandName="Edit" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
															<img alt="Ubah" src="../images/edit.gif" border="0"></asp:LinkButton>
                                                    <asp:LinkButton ID="lnkbtnDelete" runat="server" Width="20px" Text="Hapus" CausesValidation="False" CommandName="Delete" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.ID")%>'>
															<img alt="Delete" onclick="return confirm('Yakin ingin menghapus data ini?');" src="../images/trash.gif"
																border="0"></asp:LinkButton>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:LinkButton ID="Linkbutton1" runat="server" CausesValidation="true" CommandName="Add">
															<img src="../images/add.gif" border="0" alt="Tambah">
                                                    </asp:LinkButton>
                                                </FooterTemplate>
                                                <EditItemTemplate>
                                                    <asp:LinkButton ID="lbtnSave" TabIndex="40" runat="server" CausesValidation="True" CommandName="Update"
                                                        Text="Simpan">
															<img src="../images/simpan.gif" border="0" alt="Simpan"></asp:LinkButton>
                                                    <asp:LinkButton ID="lbtnCancel" TabIndex="50" runat="server" CausesValidation="True" CommandName="Cancel"
                                                        Text="Batal">
															<img src="../images/batal.gif" border="0" alt="Batal"></asp:LinkButton>
                                                </EditItemTemplate>
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
        </table>
    </form>
</body>
</html>
