<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmTrMRTC.aspx.vb" Inherits=".FrmTrMRTC" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>FrmCourse</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script type="text/javascript" src="../WebResources/jquery-1.8.3.min.js"></script>
    <script language="javascript">

        function popUpListInstruktur(mrtcId) {
            var url = '../PopUp/PopUpListInstruktur.aspx?mrtcId=' + mrtcId;
            showPopUp(url, '', 320, 440, null);
        }

        function ShowPopUpHeadSelection() {

            var dealerCode = document.getElementById("txtDealerCode").value;

            if (dealerCode == '') {
                alert("Harap pilih kode dealer MRTC terlebih dahulu")
            }

            else {
                showPopUp('../PopUp/PopUpMRTCHead.aspx?dealerCode=' + dealerCode, '', 500, 760, headSelection);
            }

           
        }

        function headSelection(selectedHead) {

            var txtHead = document.getElementById("txtHead");
            txtHead.value = selectedHead;
        }
        function ShowPopUpInstrukturSelection() {
            var dealerCode = document.getElementById("txtDealerCode").value;

            if (dealerCode == '')
            {
                alert("Harap pilih owner MRTC terlebih dahulu")
            }
            else
            {
                showPopUp('../PopUp/PopUpMRTCInstruktur.aspx?dealerCode=' + dealerCode, '', 500, 760, instrukturSelection);
            }

        }

        function instrukturSelection(selectedInstruktur) {

            var txtInstruktur = document.getElementById("txtInstruktur");
            txtInstruktur.value = selectedInstruktur;
        }

        function ShowPopupDealer() {
            showPopUp('../PopUp/PopUpDealerSelectionOne.aspx', '', 500, 760, dealerSelection);
        }


        function dealerSelection(selectedDealer) {
            var data = selectedDealer.split(";");
            var txtDealerCode = document.getElementById("txtDealerCode");
            txtDealerCode.value = data[0];

            var clickButton = document.getElementById("btnTriggerDealer");
            clickButton.click();

        }

        function ShowPopupDealerMultiple() {

            var ddlMainArea = document.getElementById("ddlMainArea");
            var mainAreaID = ddlMainArea.options[ddlMainArea.selectedIndex].value;

            if (mainAreaID == '0') {
                alert("Harap pilih main area terlebih dahulu")
            }
            else {
                showPopUp('../PopUp/PopUpDealerSelectionMRTC.aspx?mainAreaId=' + mainAreaID, '', 500, 760, dealerSelectionMultiple);
            }

          
        }


        function dealerSelectionMultiple(selectedDealer) {
            var txtListDealer = document.getElementById("txtListDealer");
            txtListDealer.value = selectedDealer;

        }


    </script>
    <style type="text/css">
        .hidden {
            display: none;
        }
    </style>

</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage">
                    <asp:Label ID="lblTitle" Text="Training - MRTC" runat="server"></asp:Label>
                </td>
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
                <td valign="top">
                    <asp:Panel ID="pnlInput" runat="server">
                        <table id="Table2" cellspacing="1" cellpadding="1" width="100%" border="0">
                            <tr>
                                <td class="titleField" width="24%">Kode MRTC</td>
                                <td width="1%">:</td>
                                <td width="75%">
                                    <asp:TextBox ID="txtMRTCCode" runat="server" onkeypress="return HtmlCharUniv(event)" MaxLength="20"
                                        Width="120px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvMRTCCode" EnableClientScript="false" runat="server" ErrorMessage="* Kode MRTC harus diisi" ControlToValidate="txtMRTCCode"></asp:RequiredFieldValidator></td>
                            </tr>
                            <tr>
                                <td class="titleField" style="height: 16px">Nama MRTC</td>
                                <td style="height: 16px">:</td>
                                <td style="height: 16px">
                                    <p>
                                        <asp:TextBox ID="txtName" onkeypress="return HtmlCharUniv(event)" runat="server" MaxLength="50"
                                            Width="120px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvNama" EnableClientScript="false" runat="server" ErrorMessage="* Nama MRTC harus diisi" ControlToValidate="txtName"></asp:RequiredFieldValidator>
                                    </p>
                                </td>
                            </tr>

                            <tr>
                                <td class="titleField" style="height: 16px"></td>
                                <td style="height: 16px">:</td>
                                <td style="height: 16px">
                                    <p>
                                        <asp:RadioButton ID="rbYes" AutoPostBack="true" CausesValidation="false" GroupName="isMainDealer" runat="server" Text="YA - MMKSI TC" />
                                        <asp:RadioButton ID="rbNo" AutoPostBack="true" CausesValidation="false" GroupName="isMainDealer" runat="server" Text="TIDAK - MRTC" />
                                    </p>
                                </td>
                            </tr>


                            <tr>
                                <td class="titleField" width="24%">Kode Dealer MRTC</td>
                                <td width="1%">:</td>
                                <td width="75%">
                                    <table cellpadding="0">
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtDealerCode" runat="server" onkeypress="return HtmlCharUniv(event)" MaxLength="20" AutoPostBack="true" CausesValidation="false"
                                                    Width="120px"></asp:TextBox>
                                            </td>
                                            <%-- <td>
                                            <div id="divTextDealer" runat="server" visible="true">
                                                <asp:Label ID="lblDealerName" runat="server" Enabled="false" Text=""></asp:Label>
                                            </div>
                                        </td>--%>
                                            <td>
                                                <asp:Label ID="lblPopUpDealer" runat="server" Width="16px" onclick="ShowPopupDealer();">
										<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0"></asp:Label>
                                                <asp:CustomValidator CssClass="style-warning" ID="cvDealer" runat="server" ControlToValidate="" OnServerValidate="cvDealer_ServerValidate" ErrorMessage="*"></asp:CustomValidator>
                                        </tr>
                                    </table>
                            </tr>
                            <tr>
                                <td class="titleField" width="24%">Main Area</td>
                                <td width="1%">:</td>
                                <td width="75%">
                                    <asp:DropDownList ID="ddlMainArea" runat="server" AutoPostBack="True" CausesValidation="false"></asp:DropDownList>
                                    <asp:CustomValidator CssClass="style-warning" ID="cvMainArea" runat="server" ControlToValidate="" OnServerValidate="cvMainArea_ServerValidate" ErrorMessage="*Main Area Harus Dipilih"></asp:CustomValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="titleField" width="24%">Area</td>
                                <td width="1%">:</td>
                                <td width="75%">
                                    <asp:DropDownList ID="ddlArea1" runat="server"></asp:DropDownList>
                                    <asp:CustomValidator CssClass="style-warning" ID="cvArea1" runat="server" ControlToValidate="" OnServerValidate="cvArea1_ServerValidate" ErrorMessage="*Area harus 1 Dipilih"></asp:CustomValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="titleField" width="24%">Propinsi</td>
                                <td width="1%">:</td>
                                <td width="75%">
                                    <asp:DropDownList ID="ddlPropinsi" runat="server" AutoPostBack="True"></asp:DropDownList>
                                    <asp:CustomValidator CssClass="style-warning" ID="cvPropinsi" runat="server" ControlToValidate="" OnServerValidate="cvPropinsi_ServerValidate" ErrorMessage="*Propinsi Harus Dipilih"></asp:CustomValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="titleField" width="24%">Kota</td>
                                <td width="1%">:</td>
                                <td width="75%">
                                    <asp:DropDownList ID="ddlKota" runat="server"></asp:DropDownList>
                                    <asp:CustomValidator CssClass="style-warning" ID="cvKota" runat="server" ControlToValidate="" OnServerValidate="cvKota_ServerValidate" ErrorMessage="*Kota Harus Dipilih"></asp:CustomValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="titleField" width="24%">Alamat</td>
                                <td width="1%">:</td>
                                <td width="75%">
                                    <asp:TextBox ID="txtAlamat" TabIndex="8"
                                        runat="server" MaxLength="200" Width="232px" TextMode="MultiLine"></asp:TextBox><asp:Label ID="lblAlamat" runat="server"></asp:Label>
                                    <asp:RequiredFieldValidator EnableClientScript="false" CssClass="style-warning" ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtAlamat" Text="*Alamat Harus diisi"></asp:RequiredFieldValidator>
                                </td>
                            </tr>

                            <tr>
                                <td class="titleField" width="24%">Head</td>
                                <td width="1%">:</td>
                                <td width="75%">
                                    <table cellpadding="0">
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtHead" runat="server" onkeypress="return HtmlCharUniv(event)" MaxLength="20"
                                                    Width="120px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblPopUpHead" runat="server" Width="16px" onclick="ShowPopUpHeadSelection();">
										<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0"></asp:Label>
                                                <asp:CustomValidator CssClass="style-warning" ID="cvHead" runat="server" ControlToValidate="" OnServerValidate="cvHead_ServerValidate" ErrorMessage="*"></asp:CustomValidator>
                                        </tr>
                                    </table>
                            </tr>
                            <tr>
                                <td class="titleField" width="24%">Instruktur</td>
                                <td width="1%">:</td>
                                <td width="75%">
                                    <asp:TextBox ID="txtInstruktur" runat="server" onkeypress="return HtmlCharUniv(event)" 
                                        Width="200px"></asp:TextBox>
                                    <asp:Label ID="lblPopUpInstruktur" runat="server" Width="16px" onclick="ShowPopUpInstrukturSelection();">
										<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0"></asp:Label>
                                    <asp:CustomValidator CssClass="style-warning" ID="cvInstruktur" runat="server" ControlToValidate="" OnServerValidate="cvInstruktur_ServerValidate" ErrorMessage="*"></asp:CustomValidator>
                                </td>
                            </tr>
                            <tr id="trListDealer" runat="server">
                                <td class="titleField" width="24%">List Dealer</td>
                                <td width="1%">:</td>
                                <td width="75%">
                                    <asp:TextBox ID="txtListDealer" runat="server" onkeypress="return HtmlCharUniv(event)"
                                        Width="200px"></asp:TextBox>
                                    <asp:Label ID="lblPopUpListDealer" runat="server" Width="16px" onclick="ShowPopupDealerMultiple();">
										<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0"></asp:Label>
                                    <asp:CustomValidator CssClass="style-warning" ID="cvListDealer" runat="server" ControlToValidate="" OnServerValidate="cvListDealer_ServerValidate" ErrorMessage="*"></asp:CustomValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="titleField" width="24%">Harga per Hari</td>
                                <td width="1%">:</td>
                                <td width="75%">
                                    <asp:TextBox ID="txtPricePerDay" runat="server" onkeypress="onlyNumbers();"
                                        Width="120px"></asp:TextBox>
                                    <asp:CustomValidator CssClass="style-warning" ID="cvPricePerDay" runat="server" ControlToValidate="" OnServerValidate="cvPricePerDay_ServerValidate" ErrorMessage="*"></asp:CustomValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="titleField" width="24%">Status</td>
                                <td width="1%">:</td>
                                <td width="75%">
                                    <asp:DropDownList ID="ddlStatus" runat="server">
                                        <asp:ListItem  Text="Silakan Pilih" Value="-1"></asp:ListItem>
                                        <asp:ListItem Selected="True" Text="AKTIF" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="TIDAK AKTIF" Value="0"></asp:ListItem>
                                    </asp:DropDownList>
                                      <asp:CustomValidator CssClass="style-warning" ID="cvStatus" runat="server" ControlToValidate="" OnServerValidate="cvStatus_ServerValidate" ErrorMessage="*"></asp:CustomValidator>
                                </td>
                            </tr>

                            <%--   <tr>
                            <td class="titleField"></td>
                            <td></td>
                            <td>
                                <asp:DataGrid ID="dtgInstruktur" runat="server" AutoGenerateColumns="False" GridLines="Horizontal"
                                    CellPadding="3" BackColor="Gainsboro" BorderWidth="0px" BorderStyle="None" BorderColor="#CDCDCD" AllowPaging="false" AllowSorting="false" PageSize="25"
                                    Width="300px" ForeColor="GhostWhite" CellSpacing="1" Font-Names="MS Reference Sans Serif" AllowCustomPaging="false">
                                    <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                                    <AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
                                    <ItemStyle BackColor="White"></ItemStyle>
                                    <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
                                    <Columns>
                                        <asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
                                            <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                        </asp:BoundColumn>
                                        <asp:TemplateColumn HeaderText="No">
                                            <HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblNo" runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Nama Trainee">
                                            <HeaderStyle Width="75%" CssClass="titleTableService"></HeaderStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblTraineeInstruktur" runat="server">			
                                                </asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                      
                                        <asp:TemplateColumn>
                                            <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnHapus" runat="server" Text="Hapus" CausesValidation="False" CommandName="Delete" OnClientClick="return confirm('Anda yakin ingin menghapus data ini?')">
												<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                    </Columns>

                                </asp:DataGrid>
                            </td>
                        </tr>--%>
                            <tr>
                                <td class="titleField"></td>
                                <td></td>
                                <td>
                                    <p>
                                        <asp:Button ID="btnSimpan" runat="server" Width="60px" Text="Simpan"></asp:Button>&nbsp;
										<asp:Button ID="btnBatal" runat="server" Width="60px" Text="Batal" CausesValidation="False"></asp:Button>
                                        <asp:Button ID="btnCari" runat="server" Width="60px" Text="Cari" CausesValidation="False"></asp:Button>
                                    </p>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>

                </td>
            </tr>
            <tr>
                <td valign="top">
                    <div id="div1" style="overflow: auto; height: 350px">
                        <asp:DataGrid ID="dtgMRTC" runat="server" AutoGenerateColumns="False" GridLines="Horizontal"
                            CellPadding="3" BackColor="Gainsboro" BorderWidth="0px" BorderStyle="None" BorderColor="#CDCDCD" AllowPaging="True" AllowSorting="True" PageSize="25"
                            Width="100%" ForeColor="GhostWhite" CellSpacing="1" Font-Names="MS Reference Sans Serif" AllowCustomPaging="True">
                            <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                            <AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
                            <ItemStyle BackColor="White"></ItemStyle>
                            <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
                            <Columns>
                                <asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
                                    <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                </asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="No">
                                    <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblNo" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="Code" HeaderText="Kode MRTC">
                                    <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                       <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblMRTCCode" runat="server">			
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Nama MRTC" SortExpression="Name">
                                    <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                       <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblMRTCName" runat="server">			
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="MMKSI" SortExpression="Name">
                                    <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                                     <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblIsMainDealer" runat="server">			
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="Dealer.DealerCode" HeaderText="Kode Dealer MRTC">
                                    <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                       <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblDealerCode" runat="server">			
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="MainArea.AreaCode" HeaderText="Main Area">
                                    <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                       <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblMainAreaCode" runat="server">			
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="Status" HeaderText="Status">
                                    <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                                       <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblStatus" runat="server">			
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Head">
                                    <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                       <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblLeader" runat="server">			
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Instruktur">
                                    <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                 <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                       <asp:HyperLink ID="lnkInstruktur" Text="View Instruktur" runat="server"></asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="List Dealer">
                                    <HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
                                       <ItemStyle Wrap="true" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblListDealer" runat="server">			
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Harga per Hari">
                                    <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                               <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblPricePerDay" runat="server">			
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn>
                                    <HeaderStyle Width="70px" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>

                                        <asp:LinkButton ID="btnLihat" runat="server" Text="Lihat" CausesValidation="False" CommandName="View">
												<img src="../images/detail.gif" border="0" alt="Lihat"></asp:LinkButton>
                                        <asp:LinkButton ID="btnUbah" runat="server" Text="Ubah" CausesValidation="False" CommandName="Edit">
												<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
                                        <asp:LinkButton ID="btnHapus" runat="server" Text="Hapus" CausesValidation="False" CommandName="Delete" OnClientClick="return confirm('Anda yakin ingin menghapus data ini?')">
												<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                            <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
                        </asp:DataGrid>
                    </div>
                </td>
            </tr>
            <tr>
                <td height="40"></td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnTriggerDealer" runat="server" CssClass="hidden" CausesValidation="false" /></td>
            </tr>
        </table>
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

        function onlyNumbers(event) {
            var charCode = (event.which) ? event.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;

            return true;
        }

        $(document).ready(function () {

            $('#txtPricePerDay').blur(function () {
                var x = $('#txtPricePerDay').val();
                $('#txtPricePerDay').val(addThousandDelimeter(x));
            });
        });

    </script>

</body>
</html>

