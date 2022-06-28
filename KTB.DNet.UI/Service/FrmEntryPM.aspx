<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmEntryPM.aspx.vb" Inherits="FrmEntryPM" SmartNavigation="False" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>Free Service</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">

        function firstFocus() {
            document.all.txtChassisMaster.focus();
        }

        function enter(controlAfter) {

            var charPressed = event.keyCode;
            if (charPressed == 13) {
                controlAfter.focus();
                return false;
            }
        }

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
        function GetCurrentSpanIndex() {
            var dgSPDetail = document.getElementById("dgEntryPM");
            var currentRow;
            var index = 0;
            var inputs;
            var indexInput;


            for (index = 0; index < dgSPDetail.rows.length; index++) {
                inputs = dgSPDetail.rows[index].getElementsByTagName("SPAN");

                if (inputs != null && inputs.length > 0) {
                    for (indexInput = 0; indexInput < inputs.length; indexInput++) {
                        if (inputs[indexInput].type != "hidden")
                            return index;
                    }
                }
            }
            return -1;
        }

        function ShowReplacementPart() {
            showPopUp('../General/../PopUp/PopReplecementPart.aspx?ID=' + 0, '', 500, 500, ReplacementPartSelection);

        }
        function ReplacementPartSelection(selectedPart) {
            var txtPenggatianPart = document.getElementById("txtPenggatianPart");
            txtPenggatianPart.value = selectedPart;
        }
        function ShowReplacementPart2() {

            var index = GetCurrentSpanIndex();
            inputs = dgEntryPM.rows[index].getElementsByTagName("SPAN")[1];
            alert(inputs.innerHTML);
            showPopUp('../General/../PopUp/PopReplecementPartSelection.aspx?ID=' + inputs.innerHTML, '', 500, 500, ReplacementPartSelection);
        }

        function ShowPPDealerBranchSelection() {
            var lblDealer = document.getElementById("lblDealerCode");
            var dealerCode = lblDealer.innerText.split("/")[0].replace(/\s/g, '');
            showPopUp('../General/../PopUp/PopUpDealerBranchSelectionSingle.aspx?m=d&deal=' + dealerCode, '', 500, 760, TemporaryOutlet);
        }

        function TemporaryOutlet(selectedDealer) {
            if (selectedDealer.indexOf(";") > 0) {
                var txtDealerSelection = document.getElementById("txtDealerBranchCode");
                var txtBranchName = document.getElementById("txtBranchName");
                txtDealerSelection.value = selectedDealer.split(";")[0];
                txtBranchName.value = selectedDealer.split(";")[1];
            }
            else {
                var txtDealerSelection = document.getElementById("txtDealerBranchCode");
                txtDealerSelection.value = selectedDealer;
            }
        }
    </script>
</head>
<body onload="firstFocus()" ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage">PERIODICAL MAINTENANCE -&nbsp; Data PM</td>
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
                <td valign="top" align="left">
                    <table id="Table2" cellspacing="1" cellpadding="2" width="100%" border="0">
                        <tr>
                            <td class="titleField" width="24%" colspan="2">
                                <asp:Label ID="lblDealer" runat="server">Kode Dealer :</asp:Label></td>
                            <td width="75%" colspan="5">
                                <asp:Label ID="lblDealerCode" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="titleField" colspan="2">Nama Dealer :</td>
                            <td colspan="6">
                                <asp:Label ID="lblDealerName" runat="server"></asp:Label></td>
                        </tr>
                        
                        <tr>
                            <td class="titleField" colspan="2">Kode Cabang :</td>
                            <td colspan="6">
                                <asp:TextBox ID="txtDealerBranchCode" runat="server" Width="150px" onkeypress="return alphaNumericExcept(event,'<>?*%$;{},+=');"
                                    ToolTip="Kode Cabang" AutoPostBack="true"></asp:TextBox>
                                <asp:Label ID="lblPopUpDealerBranch" runat="server" Width="10"> 
									<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0">
                                </asp:Label>
                                <asp:Label ID="lblDealerBranch" runat="server" Visible="False"></asp:Label>
                                </td>
                        </tr>
                        <tr>
                            <td class="titleField" width="24%" colspan="2">
                                <STRONG><label id="trDealerBranch"  runat="server">Nama Cabang</label></STRONG></td>
                            <td width="75%" colspan="6">
                                <asp:textbox id="txtBranchName" Width="150px" Runat="server" disabled=""></asp:textbox>
                        </tr>
                        <tr>
                            <td colspan="8" height="10">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="No Rangka (tidak boleh kosong)"
                                    Display="None" ControlToValidate="txtChassisMaster"></asp:RequiredFieldValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Silahkan isi jarak tempuh (tidak boleh kosong)"
                                    Display="None" ControlToValidate="txtKM"></asp:RequiredFieldValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Silahkan isi tgl servis dengan format 'ddmmyyyy'"
                                    Display="None" ControlToValidate="txtTglServis"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Silahkan isi jarak tempuh dengan angka"
                                    Display="None" ControlToValidate="txtKM" ValidationExpression="\d{1,6}"></asp:RegularExpressionValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Silahkan isi tgl PM Servis dengan format  'ddMMyyyy' misal 01122005"
                                    Display="None" ControlToValidate="txtTglServis" ValidationExpression="^((((0?[1-9]|[12]\d|3[01])[\.\-\/](0?[13578]|1[02])[\.\-\/]((1[6-9]|[2-9]\d)?\d{2}))|((0?[1-9]|[12]\d|30)[\.\-\/](0?[13456789]|1[012])[\.\-\/]((1[6-9]|[2-9]\d)?\d{2}))|((0?[1-9]|1\d|2[0-8])[\.\-\/]0?2[\.\-\/]((1[6-9]|[2-9]\d)?\d{2}))|(29[\.\-\/]0?2[\.\-\/]((1[6-9]|[2-9]\d)?(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00)|00)))|(((0[1-9]|[12]\d|3[01])(0[13578]|1[02])((1[6-9]|[2-9]\d)?\d{2}))|((0[1-9]|[12]\d|30)(0[13456789]|1[012])((1[6-9]|[2-9]\d)?\d{2}))|((0[1-9]|1\d|2[0-8])02((1[6-9]|[2-9]\d)?\d{2}))|(2902((1[6-9]|[2-9]\d)?(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00)|00))))$"></asp:RegularExpressionValidator>
                                <asp:RequiredFieldValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="Silahkan Pilih Item"
                                        Display="None" ControlToValidate="txtPenggatianPart"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 2px" width="24%" colspan="2"></td>
                            <td style="height: 2px" width="20%" colspan="6">
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowSummary="False" ShowMessageBox="True"
                                    DisplayMode="List"></asp:ValidationSummary>
                            </td>
                        </tr>
                        <tr>

                            <%--<td class="titleField">Kode Cabang</td>--%>
                            <td class="titleField" style="width: 10%">&nbsp;Kind</td>
                            <td class="titleField" style="width: 10%">&nbsp;No. Rangka</td>
                            <td class="titleField" style="width: 10%">No. Mesin</td>                            
                            <td class="titleField" style="width: 10%">
                                <asp:Label ID="Label1" runat="server" Width="150px">Jarak Tempuh (KM)</asp:Label>
                            </td>
                            <td class="titleField" style="width: 10%">
                                <asp:Label ID="LblTglServis" runat="server" Width="100px">Tgl. PM Service</asp:Label>
                            </td>
                            <td class="titleField" style="width: 10%">
                                <asp:Label ID="lblVisitType" runat="server" Width="100px">Tipe Visit</asp:Label></td>
                            <td class="titleField" style="width: 10%">WO Number</td>
                            <td style="width: 30%"></td>
                            <td class="titleField" style="width: 10%">
                                <asp:Label runat="server" ID="lblPMKind" Visible="false" Text="Jenis PM"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <%--<td>
                                <asp:TextBox ID="txtDealerBranchCode" runat="server" Width="80%" onkeypress="return alphaNumericExcept(event,'<>?*%$;{},+=');"
                                    ToolTip="Kode Cabang" AutoPostBack="true"></asp:TextBox>
                                <asp:Label ID="lblPopUpDealerBranch" runat="server" Width="10"> 
									<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0">
                                </asp:Label>
                                <asp:Label ID="lblDealerBranch" runat="server" Visible="False"></asp:Label>
                            </td>--%>
                            <td>
                                <asp:DropDownList ID="ddlPMKind" runat="server" Width="100%">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:TextBox ID="txtChassisMaster" runat="server" Width="100%" onkeypress="return alphaNumericExcept(event,'<>?*%$;{},+=');"
                                    onblur="omitSomeCharacter('txtChassisMaster','<>?*%$;');" ToolTip="Silakan isi nomor rangka (tanpa spasi). Contoh: SA00FFF4K000008 dimana SA00FFF4K000008 adalah nomor rangka." AutoPostBack="true"
                                    MaxLength="20"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtEngineNo" runat="server" Width="100%" onkeypress="return alphaNumericExcept(event,'<>?*%$;{},+=');"
                                    onblur="omitSomeCharacter('txtEngineNo','<>?*%$;');" ToolTip="Silakan isi nomor mesin (tanpa spasi). Contoh: 4G15-R66169 dimana 4D56C-R52049 adalah nomor mesin." AutoPostBack="true"
                                    MaxLength="20"></asp:TextBox>
                            </td>                            
                            
                            <td>
                                <asp:TextBox ID="txtKM" runat="server" Width="100%" ToolTip="Harus dimasukan dengan angka" MaxLength="6"
                                    Height="18px" onkeypress="return NumericOnlyWith(event,'');"></asp:TextBox></td>
                            <td>
                                <asp:TextBox ID="txtTglServis" runat="server" Placeholder="ddMMyyyy" Width="100%" ToolTip="Format tgl Servis adalah  'ddMMyyyy' misal 31122005 berarti 31-12-2005"
                                    MaxLength="8" Height="17px" onkeypress="return NumericOnlyWith(event,'');"></asp:TextBox></td>

                            <td>
                                <asp:DropDownList ID="ddlVisitType" runat="server" Width="100%">
                                    <asp:ListItem Value="WI">Walk In</asp:ListItem>
                                    <asp:ListItem Value="BO">Booking</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:TextBox ID="txtWONo" runat="server" Width="100%" ToolTip="Work Order Number (not Mandatory)" MaxLength="50"
                                    Height="18px"></asp:TextBox></td>
                            <td>
                                <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" ID="txtPenggatianPart" onblur="omitSomeCharacter('txtPenggatianPart','<>?*%$;')"
                                    runat="server" Width="100px" ToolTip="Format tgl Servis adalah  'ddMMyyyy' misal 31122005 berarti 31-12-2005"
                                    Height="17px" Visible="False"></asp:TextBox><asp:Label ID="lblPenggatianPart" runat="server" Visible="False">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup">
                                    </asp:Label><input id="btnSimpan" style="width: 50px" type="button" value="Simpan" name="btnSimpan"
                                        runat="server" causesvalidation="true">
                                <input id="btnBatal" style="width: 50px" type="button" value="Batal" name="btnBatal" runat="server"
                                    causesvalidation="False"></td>
                            <%--<td>
                                <asp:DropDownList runat="server" ID="ddlPMKind" Visible="false" Width="100%"></asp:DropDownList>
                            </td>--%>

                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td colspan="6"></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <div id="div1" style="overflow: auto; height: 320px">
                        <asp:DataGrid ID="dgEntryPM" runat="server" Width="100%" PageSize="25" AllowSorting="True" CellSpacing="1"
                            AutoGenerateColumns="False" BorderColor="#CDCDCD" BorderStyle="None" BorderWidth="0px" BackColor="#CDCDCD" CellPadding="3" GridLines="Vertical"
                            AllowPaging="True" AllowCustomPaging="True">
                            <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                            <AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
                            <ItemStyle BackColor="White"></ItemStyle>
                            <HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
                            <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
                            <Columns>
                                <asp:BoundColumn Visible="False" DataField="ID" SortExpression="ID" ReadOnly="True" HeaderText="ID">
                                    <HeaderStyle Width="3%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="No">
                                    <HeaderStyle Width="3%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblNo" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn Visible="False" HeaderText="ID">
                                    <HeaderStyle Width="3%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="Label3" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn>
                                    <HeaderStyle Width="3%" CssClass="titleTableService" HorizontalAlign="Center"></HeaderStyle>
                                    <HeaderTemplate>
                                        <input id="chkAllItems" onclick="CheckAll('cbSelect', document.forms[0].chkAllItems.checked)"
                                            type="checkbox">
                                    </HeaderTemplate>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="cbSelect" runat="server"></asp:CheckBox>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="" HeaderText="Kode Cabang">
                                    <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblDealerBranch" runat="server" Width="173px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Left"></FooterStyle>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="ChassisMaster.ChassisNumber" HeaderText="No. Rangka ">
                                    <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblChassisNo" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ChassisMaster.ChassisNumber") %>' Width="173px">
                                        </asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Left"></FooterStyle>
                                </asp:TemplateColumn>                                
                                <asp:TemplateColumn SortExpression="" HeaderText="Jenis PM">
                                    <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblPMKind" runat="server" Width="173px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Left"></FooterStyle>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="StandKM" HeaderText="Jarak Tempuh KM">
                                    <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblDatKm" runat="server" Text='<%# Format(DataBinder.Eval(Container, "DataItem.StandKM"),"#,##0") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="ServiceDate" HeaderText="Tgl. PM Service">
                                    <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="Label2" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ServiceDate", "{0:dd/MM/yyyy}") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="VisitType" HeaderText="Tipe Visit">
                                    <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblVisitTypeGrid" runat="server" Width="173px" Text='<%# DataBinder.Eval(Container, "DataItem.VisitType") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="" HeaderText="WO Number">
                                    <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblWONo" runat="server" Width="173px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Left"></FooterStyle>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Keterangan">
                                    <HeaderStyle CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="left"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblMSPDescription" runat="server" Width="173px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>

                                <asp:TemplateColumn>
                                    <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" Wrap="False"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lbtnPart" runat="server" Style="display: none">
												<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Add or Remove Part">
                                        </asp:Label>
                                        <asp:LinkButton ID="lbtnEdit" runat="server" Width="20px" Text="Ubah" CausesValidation="False" CommandName="Edit">
												<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
                                        <asp:LinkButton ID="lbtnDelete" runat="server" Text="Hapus" CausesValidation="False" CommandName="Delete">
												<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Center"></FooterStyle>
                                </asp:TemplateColumn>
                            </Columns>
                            <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
                        </asp:DataGrid>
                    </div>
                    <br>
                    <asp:Button ID="btnRelease" runat="server" Width="60px" CausesValidation="False" Text="Rilis"></asp:Button></td>
            </tr>
            <tr>
                <td style="height: 11px" align="left">&nbsp;&nbsp;</td>
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
