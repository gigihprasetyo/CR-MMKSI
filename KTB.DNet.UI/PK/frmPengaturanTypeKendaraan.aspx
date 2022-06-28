<%@ Page Language="vb" AutoEventWireup="False" CodeBehind="frmPengaturanTypeKendaraan.aspx.vb" Inherits=".frmPengaturanTypeKendaraan" SmartNavigation="False" %>

<%@ Import Namespace="KTB.DNet.Domain" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>FrmTransactionControlPK</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script src="xmlhttprequest.js" type="text/javascript"></script>
    <script language="javascript">
        function CheckAll(aspCheckBoxID, checkVal) {
            re = new RegExp(':' + aspCheckBoxID + '$')
            for (i = 0; i < document.forms[0].elements.length; i++) {
                elm = document.forms[0].elements[i]
                if (elm.type == 'checkbox') {
                    if (re.test(elm.name)) {
                        elm.checked = checkVal
                    }
                }
            }
        }

        function ShowPPMaterialNumberSelection() {
            var ddlCategory = document.getElementById("ddlKategori");
            var txtVechileType = document.getElementById("txtVechileType");
            var ddlSubCategory = document.getElementById("ddlSubCategory");
            showPopUp('../PopUp/PopUpVechileColorMultipleSelection.aspx?vechileTypeCode=' + txtVechileType.value + '&vechileSubCategoryID=' + ddlSubCategory.value + '&vechileCategory=' + ddlCategory.value, '', 450, 500, MaterialNumber)
        }

        function MaterialNumber(selectedType) {
            var txtMaterialNumber = document.getElementById("txtMaterialNumber");
            txtMaterialNumber.value = selectedType
            if (navigator.appName == "Microsoft Internet Explorer") {
                txtMaterialNumber.focus();
                txtMaterialNumber.blur();
            }
            else {
                txtMaterialNumber.onchange();
            }
        }

        function ShowPPVechileTypeSelection() {
            var ddlCategory = document.getElementById("ddlKategori");
            var ddlVechileType = document.getElementById("ddlVechileType");
            var ddlSubCategory = document.getElementById("ddlSubCategory");

            if (parseInt(ddlCategory.value) < 1 && parseInt(ddlSubCategory.value) < 1) {
                alert("Silahkan pilih kategori & sub-kategory terlebih dahulu");
                return;
            }

            showPopUp('../PK/PopUpVechileTypeMultiplePK.aspx?IsActive=A' + '&Category=' + ddlCategory.value + '&SubCategory=' + ddlSubCategory.value, '', 450, 500, VechileType)
        }

        function VechileType(selectedType) {
            var txtVechileType = document.getElementById("txtVechileType");
            txtVechileType.value = selectedType
            if (navigator.appName == "Microsoft Internet Explorer") {
                txtVechileType.focus();
                txtVechileType.blur();
            }
            else {
                txtVechileType.onchange();
            }
        }

        function ShowPPKodeModelSelection() {
            var ddlCategory = document.getElementById("ddlKategori");
            var ddlSubCategory = document.getElementById("ddlSubCategory");
            showPopUp('../General/FrmModelSelectionOnPK.aspx?cat=' + ddlCategory.value + '&subCat=' + ddlSubCategory.value, '', 450, 500, KodeTipe)
        }

        function ShowPPTipeGeneral() {
            var subCat = document.getElementById("ddlSubCategory");
            if (subCat.value == -1) {
                alert("Silahkan pilih sub-category terlebih dahulu");
                return;
            }

            showPopUp('../PopUp/PopUpTipeGeneral.aspx?subCategory=' + subCat.value, '', 450, 500, TipeGeneral);
        }

        function ShowPPModelKendaraan() {
            showPopUp('../PopUp/PopUpSalesModelKendaraan.aspx', '', 450, 500, ModelKendaraan);
        }

        function TipeGeneral(selectedType) {
            var results = selectedType.split(';');
            document.getElementById("txtTypeGeneralID").value = results[1];
            document.getElementById("txtTypeGeneral").value = results[0];
        }

        function ModelKendaraan(selectedType) {
            var results = selectedType.split(';');
            document.getElementById("txtModelKendaraanID").value = results[1];
            document.getElementById("txtModelKendaraan").value = results[0];
        }

        function KodeTipe(selectedType) {
            var txtKodeModel = document.getElementById("txtKodeModel");
            txtKodeModel.value = selectedType;

            if (navigator.appName == "Microsoft Internet Explorer") {
                txtKodeModel.blur();
            }
            else {
                txtKodeModel.onchange();
            }
        }

        function isNumber(obj, evt) {
            var txt = document.getElementById(obj).value;
            if (txt.length == 0) {
                var kc = (evt.which) ? evt.which : evt.keyCode
                if (kc == 48)
                    return false;
            }
            if (txt.length == 4)
                return false;

            var iKeyCode = (evt.which) ? evt.which : evt.keyCode
            if (iKeyCode != 46 && iKeyCode > 31 && (iKeyCode < 48 || iKeyCode > 57))
                return false;

            return true;
        }

    </script>
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <style type="text/css">
        .auto-style2 {
            font-family: Sans-Serif, Arial;
            font-size: 11px;
            color: #000000;
            margin: 0px;
            font-weight: bold;
            text-align: left;
            width: 144px;
        }

        .auto-style3 {
            width: 144px;
        }

        .hidden {
            display: none;
        }
    </style>
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage">Pesanan Kendaraan - Pengaturan Tipe Kendaraan</td>
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
                <td style="height: 80px">
                    <table id="Table2" cellspacing="1" cellpadding="2" width="100%" border="0">
                        <tr>
                            <td class="titleField">Kategori</td>
                            <td>:</td>
                            <td>
                                <asp:DropDownList ID="ddlKategori" runat="server" Width="140px" AutoPostBack="True"></asp:DropDownList>&nbsp;
                                <asp:DropDownList Style="z-index: 0" ID="ddlSubCategory" runat="server" AutoPostBack="True"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr valign="top">
                            <td class="titleField">Tipe General</td>
                            <td>:</td>
                            <td valign="top">
                                <asp:TextBox ID="txtTypeGeneralID" CssClass="hidden" runat="server" Width="200px"></asp:TextBox>
                                <asp:TextBox ID="txtTypeGeneral" runat="server" Width="200px"></asp:TextBox>
                                <asp:Label ID="Label1" runat="server" onclick="ShowPPTipeGeneral();">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup">
                                </asp:Label>
                            </td>
                        </tr>
                        <tr valign="top">
                            <td class="titleField">Tipe Kendaraan</td>
                            <td>:</td>
                            <td valign="top">
                                <%--<asp:TextBox ID="txtKodeModel" TextMode="MultiLine" runat="server" Width="200px"></asp:TextBox>
                                <asp:Label ID="lblKodeModel" runat="server">
									<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup">
                                </asp:Label>--%>
                                <asp:TextBox ID="txtVechileType" runat="server" Width="200px"></asp:TextBox>
                                <asp:Label ID="lblVechileType" runat="server" onclick="ShowPPVechileTypeSelection();">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup">
                                </asp:Label>
                            </td>
                        </tr>
                        <tr valign="top">
                            <td class="titleField">Material Number</td>
                            <td>:</td>
                            <td valign="top">
                                <asp:TextBox ID="txtMaterialNumber" TextMode="MultiLine" runat="server" Width="200px"></asp:TextBox>
                                <asp:Label ID="lblMaterialNumber" runat="server" onclick="ShowPPMaterialNumberSelection();">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup">
                                </asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style2">Tahun Perakitan</td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="txtTahunPerakitan" runat="server" Width="200px" onkeypress="return isNumber('txtTahunPerakitan', event);"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style2">Tahun Model</td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="txtTahunModel" runat="server" Width="200px" onkeypress="return isNumber('txtTahunModel', event);"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style2">Status</td>
                            <td>:</td>
                            <td>
                                <asp:DropDownList ID="ddlstatus" runat="server" Width="140px"></asp:DropDownList></td>
                        </tr>
                    </table>
                    <table id="Table2" cellspacing="1" cellpadding="2" width="100%" border="0">
                        <tr>
                            <td></td>
                        </tr>
                        <tr>
                            <td></td>
                        </tr>
                        <tr>
                            <td></td>
                        </tr>
                    </table>
                    <table id="Table2" cellspacing="1" cellpadding="2" width="100%" border="0">
                        <tr>
                            <td>
                                <asp:Button ID="btnSearch" runat="server" Width="56px" Text=" Cari " Font-Bold="True"></asp:Button>&nbsp;
                                <asp:Button ID="btnCancel" runat="server" Width="56px" Text=" Batal " Font-Bold="True"></asp:Button>&nbsp;
                                <asp:Button ID="btnActivate" runat="server" Width="80px" Text="Aktifkan" Font-Bold="True" Enabled="False"></asp:Button>&nbsp;
                                <asp:Button ID="btnNoActivate" runat="server" Width="90px" Text="Non-Aktifkan" Font-Bold="True" Enabled="False"></asp:Button>&nbsp;
                                <asp:Button ID="btnSave" runat="server" Width="56px" Text=" Simpan " Font-Bold="True"></asp:Button>&nbsp;
                                <asp:Button ID="btnDownload" runat="server" Text="Download Excel" Font-Bold="True"></asp:Button>
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="height: 80px" valign="top">
                    <table id="Table2" cellspacing="1" cellpadding="2" width="100%" border="0">
                        <tr>
                            <td class="auto-style2">Model Kendaraan</td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="txtModelKendaraanID" CssClass="hidden" runat="server" Width="200px"></asp:TextBox>
                                <asp:TextBox ID="txtModelKendaraan" runat="server" Width="200px"></asp:TextBox>
                                <asp:Label ID="Label2" runat="server" onclick="ShowPPModelKendaraan();">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup">
                                </asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style2">Deskripsi NickName Kendaraan</td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="txtDeskripsiKendaraan" runat="server" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style2"></td>
                            <td></td>
                            <td>
                                <asp:LinkButton ID="LnkDownloadTemplate" runat="server" ToolTip="Download Template Excell">Template File Upload Deskripsi Kendaraan</asp:LinkButton>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style2">Upload Deskripsi NickName Kendaraan</td>
                            <td>:</td>
                            <td>
                                <asp:FileUpload ID="fileUploadExcel" runat="server" ></asp:FileUpload>
                                <asp:Button ID="btnUpload" runat="server" Text="Upload" Width="70px" OnClientClick="return confirm('Apakah anda yakin akan melanjutkan proses upload data?');"></asp:Button>
                            </td>
                        </tr>
                        <%--<tr>
                            <td class="auto-style2">Status Data</td>
                            <td>:</td>
                            <td>
                                <asp:DropDownList ID="ddlDataUpload" runat="server" Width="140px"></asp:DropDownList></td>
                        </tr>--%>
                    </table>
                </td>
            </tr>
            <tr>
                <td></td>
            </tr>
            <tr>
                <td align="left"></td>
            </tr>
        </table>
        &nbsp;&nbsp;
        <div id="div1" style="overflow: auto; height: 300px">
            <asp:DataGrid ID="dtgVechileColorList" runat="server" Width="100%" PageSize="25" Height="1px" AutoGenerateColumns="False"
                BorderColor="#E0E0E0" BorderStyle="None" BorderWidth="0px" CellPadding="3" GridLines="Horizontal" CellSpacing="1" BackColor="#CDCDCD"
                AllowPaging="true" AllowSorting="True" AllowCustomPaging="True">
                <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                <AlternatingItemStyle BackColor="#FDF1F2"></AlternatingItemStyle>
                <ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
                <HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
                <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
                <Columns>
                    <asp:TemplateColumn HeaderText="cbHeader">
                        <HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                        <HeaderTemplate>
                            <input id="chkAllItems" onclick="CheckAll('chkItemChecked', document.forms[0].chkAllItems.checked)"
                                type="checkbox">
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="chkItemChecked" runat="server"></asp:CheckBox>
                        </ItemTemplate>
                    </asp:TemplateColumn>

                    <asp:TemplateColumn HeaderText="No">
                        <HeaderStyle Width="4%" CssClass="titleTableSales"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblNo" runat="server" BorderColor="Transparent"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>

                    <asp:BoundColumn Visible="False" DataField="ID" ReadOnly="True" HeaderText="">
                        <HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
                    </asp:BoundColumn>

                    <asp:TemplateColumn HeaderText="Tipe General" SortExpression="TipeGeneral">
                        <HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblTipeGeneral" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>

                    <asp:TemplateColumn HeaderText="Kode Kendaraan" SortExpression="VechileColor.VechileType.VechileTypeCode">
                        <HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblKodeKendaraan" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>

                    <asp:TemplateColumn HeaderText="Model" SortExpression="VechileColor.VechileType.VechileModel.IndDescription">
                        <HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblModelKendaraan" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>

                    <asp:TemplateColumn HeaderText="Sales Model" SortExpression="VechileColor.VechileType.SalesVechileModel.NewVechileModelDesc">
                        <HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblSalesModelKendaraan" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>

                    <asp:TemplateColumn HeaderText="Deskripsi Kendaraan (MMKSI)" SortExpression="VechileColor.VechileType.Description">
                        <HeaderStyle Width="30%" CssClass="titleTableSales"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblMaterialDescription" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>

                    <asp:TemplateColumn HeaderText="Deskripsi NickName Kendaraan" SortExpression="VechileColor.VechileType.DescriptionDealer">
                        <HeaderStyle Width="30%" CssClass="titleTableSales"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblMaterialDescriptionDealer" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>

                    <asp:TemplateColumn HeaderText="Kode Warna Kendaraan" SortExpression="VechileColor.MaterialNumber">
                        <HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblMaterialNumber" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>

                    <asp:TemplateColumn HeaderText="Warna Kendaraan" SortExpression="VechileColor.ColorIndName">
                        <HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblWarnaKendaraan" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>

                    <asp:BoundColumn DataField="ProductionYear" SortExpression="ProductionYear" ReadOnly="True" HeaderText="Tahun Perakitan">
                        <HeaderStyle Width="8%" CssClass="titleTableSales" HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundColumn>

                    <asp:BoundColumn DataField="ModelYear" SortExpression="ModelYear" ReadOnly="True" HeaderText="Tahun Model">
                        <HeaderStyle Width="8%" CssClass="titleTableSales" HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundColumn>

                    <asp:BoundColumn Visible="False" DataField="LastUpdateTime" SortExpression="LastUpdateTime" HeaderText="Tgl. Update"
                        DataFormatString="{0:dd/MM/yyyy}">
                        <HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
                    </asp:BoundColumn>

                    <asp:TemplateColumn HeaderText="Tgl. Update" SortExpression="LastUpdateTime">
                        <HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblTglUpdate" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>

                    <asp:TemplateColumn HeaderText="Status" SortExpression="Status">
                        <HeaderStyle Width="9%" CssClass="titleTableSales"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblStatus" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>

                    <asp:TemplateColumn HeaderText="Action">
                        <HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnEdit" runat="server" Width="20px" Text="Ubah" CausesValidation="False" CommandName="Edit">
								<img alt="Ubah" src="../images/edit.gif" border="0"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                </Columns>
                <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#CDCDCD" Mode="NumericPages"></PagerStyle>
            </asp:DataGrid>
        </div>
        <table id="Table3" border="0" cellspacing="1" cellpadding="1" width="100%">
            <tr>
                <td>
                    
                </td>
            </tr>
        </table>
    </form>

    <script language="javascript">
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

