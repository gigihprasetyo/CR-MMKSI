<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmInputClaimCBUReturn.aspx.vb" Inherits=".FrmInputClaimCBUReturn" %>
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
    <script type="text/javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script type="text/javascript" src="../WebResources/InputValidation.js"></script>
    <script type="text/javascript" src="../WebResources/FormFunctions.js"></script>
    <script type="text/javascript" src="../WebResources/jquery.min.js"></script>

    <script type="text/javascript">
        function ShowPPDealerSelection() {
            showPopUp('../PopUp/PopUpDealerSelectionOne.aspx', '', 500, 760, DealerSelection);
        }

        function DealerSelection(selectedDealer) {
            var data = selectedDealer.split(";");
            var txtbox = document.getElementById("txtDealerName");
            txtbox.value = data[0] + " - " + data[1];

            __doPostBack('', '');
        }

        function ShowPPCompanySelection() {
            showPopUp('../PopUp/PopUpCompanyCBUReturn.aspx', '', 500, 760, CompanySelection);
        }

        function CompanySelection(selectedCompany) {
            var data = selectedCompany.split(";");
            var txtbox = document.getElementById("txtCompany");
            txtbox.value = data[0] + " - " + data[1];
        }

        function ShowPPChassisSelection() {
            var txtbox = document.getElementById("txtDealerName").value;
            var data = txtbox.split("-")[0].replace(" ", "");
            showPopUp('../PopUp/PopUpChassisCBUReturn.aspx?type=1&code=' + data, '', 500, 760, ChassisSelection);
        }

        function ChassisSelection(selected) {
            var data = selected.split(";");
            var txtbox = document.getElementById("txtNoChassis");
            txtbox.value = data[0];

            var btn = document.getElementById("btnGetInfoChassis");
            btn.click();
        }

        function ShowPPDOSelection() {
            var txtbox = document.getElementById("txtDealerName").value;
            var data = txtbox.split("-")[0].replace(" ", "");
            showPopUp('../PopUp/PopUpChassisCBUReturn.aspx?type=2&code=' + data, '', 500, 760, DOSelection);
        }

        function DOSelection(selected) {
            var data = selected.split(";");
            var txtbox = document.getElementById("txtNoDO");
            txtbox.value = data[1];

            var btn = document.getElementById("btnGetInfoDO");
            btn.click();
        }

        function showPPPassword() {
            if (confirm('Apakah anda yakin mau proses?'))
                showPopUp('../General/../PopUp/PopupInputPassword.aspx', '', 200, 600, GotPassword);
        }

        function ShowConfirm(msg, id) 
        {
            var btn = document.getElementById(id);
            var hdConfirm = document.getElementById("hdConfirm");
            if(confirm(msg)) {
                hdConfirm.value = "0";
                btn.click();
            }
        }

        function GotPassword(result) {
            var txtUser = document.getElementById("txtUser");
            var txtPwd = document.getElementById("txtPass");
            var btn = document.getElementById("btnSend");
            var str = result;
            var username = '', pwd = '';

            username = str.split(';')[0];
            pwd = str.split(';')[1];

            txtUser.value = username;
            txtPwd.value = pwd;
            btn.click();
        }

    </script>

</head>
<body ms_positioning="GridLayout" onfocus="LockToModal()" onclick="LockToModal()">
    <form id="Form1" runat="server">
        <table id="Table2" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage"><asp:Label ID="lblTitle" runat="server" Text="CBU Return - Pengajuan Claim"></asp:Label></td>
            </tr>
            <tr>
                <td background="../images/bg_hor.gif" height="1" colspan="2">
                    <img height="1" src="../images/bg_hor.gif" border="0"></td>
            </tr>
            <tr>
                <td height="10" colspan="2">
                    <img height="1" src="../images/dot.gif" border="0"></td>
            </tr>
        </table>
        <table id="Table1" cellspacing="1" cellpadding="2" width="100%" border="0">
            <tr>
                <td class="titleTableParts3" style="font-size:smaller" colspan="6">Informasi Claim</td>
            </tr>
            <tr>
                <td class="titleField" style="width: 146px">Tanggal Claim</td>
                <td style="width: 2px">:</td>
                <td>
                    <asp:TextBox ID="txtTglClaim" runat="server" Enabled="false"></asp:TextBox>
                </td>
                <td class="titleField" style="width: 146px">Logistic Company</td>
                <td style="width: 2px">:</td>
                <td>
                    <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$')" ID="txtCompany" onblur="omitSomeCharacter('txtCompany','<>?*%$')" runat="server" ToolTip="Logistic Company Search" Width="250px"></asp:TextBox>
                    <asp:Label ID="lblPopupCompany" onclick="ShowPPCompanySelection();" runat="server" Style="z-index: 0">
					            <img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="titleField" style="width: 146px">Pelapor Issue</td>
                <td style="width: 2px">:</td>
                <td class="titleField">
                    <asp:TextBox ID="txtPelaporIssue" runat="server"></asp:TextBox>
                </td>
                <td class="titleField" style="width: 146px">Nomor Claim</td>
                <td style="width: 2px">:</td>
                <td class="titleField">
                    <asp:Label ID="lblNoClaim" runat="server">[Auto Generate]</asp:Label>
                </td>
            </tr>
            <tr>
                <td class="titleField" style="width: 146px">Nama Dealer</td>
                <td style="width: 2px">:</td>
                <td>
                    <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$')" ID="txtDealerName" OnTextChanged="txtDealerName_TextChanged" onblur="omitSomeCharacter('txtDealerName','<>?*%$')" runat="server" ToolTip="Dealer Search" Width="250px"></asp:TextBox>
                    <asp:Label ID="lblPopupDealer" onclick="ShowPPDealerSelection();" runat="server" Style="z-index: 0">
					            <img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:Label>
                </td>
                <td class="titleField" style="width: 146px">Status Claim</td>
                <td style="width: 2px">:</td>
                <td class="titleField">
                    <asp:TextBox ID="txtStatusClaim" runat="server" Enabled="false"></asp:TextBox></td>
            </tr>
            <tr>
                <td id="tdKodeDest1" runat="server" class="titleField" style="width: 146px">Kode Destinasi Claim</td>
                <td id="tdKodeDest2" runat="server" style="width: 2px">:</td>
                <td id="tdKodeDest3" runat="server" class="titleField">
                    <asp:DropDownList ID="ddlKodeDestClaim" runat="server"></asp:DropDownList>
                </td>
                <td class="titleField" style="width: 146px">Tanggal Kejadian</td>
                <td style="width: 2px">
                    <asp:Label ID="Label9" runat="server">:</asp:Label></td>
                <td class="titleField">
                    <cc1:IntiCalendar ID="icTglKejadian" runat="server"></cc1:IntiCalendar></td>
            </tr>
            <tr>
                <td class="titleField" style="width: 146px">Dealer PIC</td>
                <td style="width: 2px">:</td>
                <td class="titleField">
                    <asp:TextBox ID="txtDealerPIC" runat="server"></asp:TextBox></td>
                <td class="titleField" style="width: 146px">Tempat Kejadian</td>
                <td style="width: 2px">:</td>
                <td class="titleField">
                    <asp:TextBox ID="txtTempatKejadian" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td colspan="3"></td>
                <td class="titleField" style="width: 146px">Status Stock DMS</td>
                <td style="width: 2px">:</td>
                <td class="titleField"><asp:Label ID="lblStatusStok" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="titleTableParts3" style="font-size:smaller" colspan="6">Informasi Kendaraan</td>
            </tr>
            <tr>
                <td class="titleField" style="width: 146px">Nomor Chassis</td>
                <td style="width: 2px">:</td>
                <td class="titleField">
                    <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$')" ID="txtNoChassis" Enabled="false" onblur="omitSomeCharacter('txtNoChassis','<>?*%$')" runat="server" ToolTip="Nomor Chassis Search" Width="128px"></asp:TextBox>
                    <asp:Label ID="lblPopupChassis" onclick="ShowPPChassisSelection();" runat="server" Visible="false" Style="z-index: 0">
					            <img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:Label>
                    <asp:ImageButton ID="btnGetInfoChassis" runat="server" ImageUrl="~/images/reload.gif" Visible="false" OnClick="btnGetInfoChassis_Click"></asp:ImageButton>
                </td>
                <td class="titleField" style="width: 146px">Dealer Alokasi</td>
                <td style="width: 2px">:</td>
                <td class="titleField">
                    <asp:TextBox ID="txtDealerAlokasi" runat="server" Enabled="false" Width="250px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="titleField" style="width: 146px">Nomor DO</td>
                <td style="width: 2px">:</td>
                <td class="titleField">
                    <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$')" ID="txtNoDO" Enabled="false" onblur="omitSomeCharacter('txtNoDO','<>?*%$')" runat="server" ToolTip="Nomor DO Search" Width="128px"></asp:TextBox>
                    <asp:Label ID="lblPopupDO" onclick="ShowPPDOSelection();" runat="server" Visible="false" Style="z-index: 0">
					            <img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:Label>
                    <asp:ImageButton ID="btnGetInfoDO" runat="server" ImageUrl="~/images/reload.gif" Visible="false" OnClick="btnGetInfoDO_Click"></asp:ImageButton>
                </td>
                <td class="titleField" style="width: 146px">Tanggal Unit Diterima</td>
                <td style="width: 2px">:</td>
                <td class="titleField">
                    <asp:TextBox ID="txtTglUnitTerima" runat="server" Enabled="false"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="titleField" style="width: 146px">Model</td>
                <td style="width: 2px">:</td>
                <td class="titleField"><asp:TextBox ID="txtModel" runat="server" Enabled="false"></asp:TextBox></td>
                <td id="tdMksOnly1" runat="server" class="titleField" style="width: 146px">Kode Destinasi</td>
                <td id="tdMksOnly2" runat="server" style="width: 2px">:</td>
                <td id="tdMksOnly3" runat="server" class="titleField"><asp:TextBox ID="txtKodeDestinasi" runat="server" Width="200" Enabled="false"></asp:TextBox></td>
            </tr>
            <tr id="trMksOnly1" runat="server">
                <td class="titleField" style="width: 146px">Respon Claim</td>
                <td style="width: 2px">:</td>
                <td class="titleField">
                    <asp:DropDownList ID="ddlRespon" runat="server" OnSelectedIndexChanged="ddlRespon_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                </td>
                <td class="titleField" style="width: 146px"><asp:Label ID="lblTitleF1" runat="server"></asp:Label></td>
                <td id="tdMksOnly4" style="width: 2px">:</td>
                <td class="titleField">
                    <%--<asp:TextBox ID="txtNoMesin" runat="server"></asp:TextBox>--%>
                    <cc1:IntiCalendar ID="ictTglActFin" runat="server"></cc1:IntiCalendar>
                    <asp:TextBox ID="txtChassisPengganti" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr id="trMksOnly2" runat="server">
                <td class="titleField" style="width: 146px"><asp:Label ID="lblTitleF2" runat="server"></asp:Label></td>
                <td style="width: 2px">:</td>
                <td class="titleField">
                    <cc1:IntiCalendar ID="ictTglTrf" runat="server"></cc1:IntiCalendar>
                    <cc1:IntiCalendar ID="ictTglEst" runat="server"></cc1:IntiCalendar>
                    <asp:TextBox ID="txtStatusRetur" runat="server" Width="200" Enabled="false"></asp:TextBox>
                </td>
                <td colspan="3"></td>
            </tr>
            <tr id="trMksOnly3" runat="server">
                <td class="titleField" style="width: 146px"><asp:Label ID="lblTitleF3" runat="server" Text="Nominal"></asp:Label></td>
                <td style="width: 2px">:</td>
                <td class="titleField">
                    <asp:TextBox ID="txtNominal" runat="server" onkeypress="return NumericOnlyWith(event,'');" onkeyup="pic(this,this.value,'9999999999','N')"></asp:TextBox>
                </td>
                <td colspan="3"></td>
            </tr>
            <tr id="trNote" runat="server">
                <td class="titleField" style="width: 146px">Catatan MMKSI</td>
                <td style="width: 2px">:</td>
                <td class="titleField" colspan="4">
                    <asp:TextBox ID="txtNote" runat="server" TextMode="MultiLine" MaxLength="225" Width="350px" Height="80px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:DataGrid ID="dgClaim" runat="server" Width="70%" BorderWidth="0px" BackColor="#CDCDCD" BorderColor="Gainsboro" CellPadding="3" CellSpacing="1"
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
                                    <asp:Label ID="lblNo" runat="server"></asp:Label>
                                    <asp:HiddenField ID="hdID" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Tipe Claim">
                                <HeaderStyle CssClass="titleTablePromo" Width="150"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblTipeClaimName" runat="server"></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddlTipeClaimEdit" runat="server" Width="150"></asp:DropDownList>
                                </EditItemTemplate>
                                <FooterStyle HorizontalAlign="Center"></FooterStyle>
                                <FooterTemplate>
                                    <asp:DropDownList ID="ddlTipeClaimNew" runat="server" Width="150" OnPreRender="ddlTipeClaimNew_PreRender"></asp:DropDownList>
                                </FooterTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Claim Point">
                                <HeaderStyle CssClass="titleTablePromo" Width="450"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblClaimPoint" runat="server"></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtClaimPointEdit" runat="server" Width="450"></asp:TextBox>
                                </EditItemTemplate>
                                <FooterStyle HorizontalAlign="Center"></FooterStyle>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtClaimPointNew" runat="server" Width="450" />
                                </FooterTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <HeaderStyle CssClass="titleTablePromo" Width="80"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" />
                                <FooterStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <div style="width:80px">
                                        <asp:LinkButton ID="lnkbtnEdit" runat="server" CommandName="edit" CausesValidation="False">
								            <img src="../images/edit.gif"  border="0" alt="Ubah"></asp:LinkButton>
                                        <asp:LinkButton ID="lnkbtnDelete" runat="server" OnClientClick="return confirm('Anda yakin mau hapus?');" CommandName="delete" CausesValidation="False">
                                            <img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
                                    </div>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <div style="width:80px">
                                        <asp:LinkButton ID="lbtnSave" CommandName="save" Text="Simpan" runat="server">
                                            <img src="../images/simpan.gif" border="0" alt="Simpan"></asp:LinkButton>
                                        <asp:LinkButton ID="lbtnCancel" CommandName="cancel" Text="Batal" runat="server">
                                            <img src="../images/batal.gif" border="0" alt="Batal"></asp:LinkButton>
                                    </div>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:LinkButton ID="lnkbtnAdd" runat="server" CommandName="add" CausesValidation="False" TabIndex="0">
								        <img src="../images/add.gif" border="0" alt="Tambah">
                                    </asp:LinkButton>
                                </FooterTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                    </asp:DataGrid>
                </td>
            </tr>
        </table>
        <div>
            <hr />
        </div>
        <div class="titleField" style="padding-right: 2px; padding-left: 2px; padding-bottom: 2px; width: 75%; padding-top: 2px; text-align: Left">
            <asp:Label ID="Label1" runat="server" Text="Attachment Claim" Font-Size="15px" Font-Bold="True"></asp:Label>
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
                            <asp:HiddenField ID="hdDocID" runat="server" />
                            <asp:Label ID="lblNo" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>                    
                    <asp:TemplateColumn HeaderText="Remark">
                        <HeaderStyle CssClass="titleTablePromo" Width="350"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblDeskripsi" runat="server" Style="word-wrap: normal; word-break: break-all;"></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtDeskripsiEdit" runat="server" Width="350"></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtDeskripsiNew" runat="server" Width="350"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="File Name">
                        <HeaderStyle CssClass="titleTablePromo" Width="240"></HeaderStyle>
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkbtnDownload" runat="server" CommandName="download" CausesValidation="False" Style="word-wrap: normal; word-break: break-all;"></asp:LinkButton>                                      
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:FileUpload ID="fuUploadEdit" runat="server" Width="240"></asp:FileUpload>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:FileUpload ID="fuUploadNew" runat="server" Width="240"></asp:FileUpload>
                        </FooterTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderStyle CssClass="titleTablePromo" Width="80"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" />
                            <FooterStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <div style="width:80px">
                                    <asp:LinkButton ID="lnkbtnEdit" runat="server" CommandName="edit" CausesValidation="False">
								        <img src="../images/edit.gif"  border="0" alt="Ubah"></asp:LinkButton>
                                    <asp:LinkButton ID="lnkbtnDelete" runat="server" OnClientClick="return confirm('Anda yakin mau hapus?');" CommandName="delete" CausesValidation="False">
                                        <img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
                                </div>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <div style="width:80px">
                                    <asp:LinkButton ID="lbtnSave" CommandName="save" Text="Simpan" runat="server">
                                        <img src="../images/simpan.gif" border="0" alt="Simpan"></asp:LinkButton>
                                    <asp:LinkButton ID="lbtnCancel" CommandName="cancel" Text="Batal" runat="server">
                                        <img src="../images/batal.gif" border="0" alt="Batal"></asp:LinkButton>
                                </div>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:LinkButton ID="lnkbtnAdd" runat="server" CommandName="add" CausesValidation="False" TabIndex="0">
								    <img src="../images/add.gif" border="0" alt="Tambah">
                                </asp:LinkButton>
                            </FooterTemplate>
                    </asp:TemplateColumn>
                </Columns>
            </asp:DataGrid>
        </div>
        <div id="divPassword" style="display: none;">
            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                <tr>
                    <td>SAP Password</td>
                    <td>:</td>
                    <td>
                        <asp:TextBox ID="txtUser" runat="server" Width="171px"></asp:TextBox>
                        <asp:TextBox ID="txtPass" runat="server" TextMode="Password" Width="171px"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <hr />
        </div>
        <br />
        <input id="hdConfirm" type="hidden" value="-1" runat="server">
        <asp:Button ID="btnBaru" runat="server" Text="Baru" OnClick="btnBaru_Click"></asp:Button>
        <asp:Button ID="btnSimpan" runat="server" Text="Simpan" OnClick="btnSimpan_Click"></asp:Button>
        <asp:Button ID="btnValidasi" runat="server" Text="Validasi" Visible="false" Enabled="false" OnClick="btnValidasi_Click"></asp:Button>
        <asp:Button ID="btnRevisi" runat="server" Width="80px" Text="Revisi" Visible="false" OnClick="btnRevisi_Click"></asp:Button>
        <asp:Button ID="btnKonfirmasi" runat="server" Width="80px" Text="Konfirmasi" Visible="false" OnClick="btnKonfirmasi_Click"></asp:Button>
        <asp:Button ID="btnSend" runat="server" Width="120px" Text="Send to SAP" Visible="false" OnClick="btnSend_Click"></asp:Button>
        <asp:Button ID="btnTolak" runat="server" Width="50px" Text="Tolak" Visible="false" OnClick="btnTolak_Click"></asp:Button>
        <asp:Button ID="btnBatal" runat="server" Width="120px" Text="Batal Validasi" Visible="false" OnClick="btnBatal_Click"></asp:Button>
        <asp:Button ID="btnSelesai" runat="server" Width="120px" Text="Selesai" Visible="false" OnClick="btnSelesai_Click"></asp:Button>
        <asp:Button ID="btnTransfer" runat="server" Width="150px" Text="Transfer Ulang ke SAP" Visible="false" OnClick="btnTransfer_Click" />
        <asp:Button ID="btnCancelProses" runat="server" Width="120px" Text="Cancel Proses" Visible="false" OnClick="btnCancelProses_Click" />
        <asp:Button ID="btnKembali" runat="server" Text="Kembali" Visible="false" OnClick="btnKembali_Click"></asp:Button>
    </form>
</body>
</html>
