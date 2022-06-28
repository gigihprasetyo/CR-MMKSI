<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmEntryPDI.aspx.vb" Inherits="FrmEntryPDI" SmartNavigation="False" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>Upload PDI</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script type="text/javascript">

        function ShowPPDealerBranchSelection() {
            var lblDealer = document.getElementById("lblDealerCode");
            var dealerCode = lblDealer.innerText.split("/")[0].replace(/\s/g, '');
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
    </script>
    <style type="text/css">
        .auto-style1 {
            font-family: Sans-Serif, Arial;
            font-size: 11px;
            color: #000000;
            margin: 0px;
            font-weight: bold;
            text-align: left;
            height: 18px;
        }

        .auto-style2 {
            height: 18px;
        }

        .auto-style5 {
            font-family: Sans-Serif, Arial;
            font-size: 11px;
            color: #000000;
            margin: 0px;
            font-weight: bold;
            text-align: left;
            width: 181px;
        }

        .auto-style6 {
            width: 181px;
        }

        </style>
</head>
<body onload="firstFocus()" ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage">&nbsp;PRE DELIVERY INSPECTION&nbsp;-&nbsp;Data PDI</td>
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
                    <table id="Table2" width="100%" border="0">
                        <tr>
                            <td class="titleField" width="20%">
                                <asp:Label ID="lblDealer" runat="server">Kode Dealer </asp:Label>:</td>
                            <td width="20%">
                                <asp:Label ID="lblDealerCode" runat="server" Width="183px"></asp:Label></td>
                            <td width="20%"></td>
                            <td width="40%" class="titleField">Keterangan Jenis PDI</td>
                        </tr>
                        <tr>
                            <td class="auto-style1">Nama Dealer:</td>
                            <td class="auto-style2">
                                <asp:Label ID="lblDealerName" runat="server" Width="392px"></asp:Label></td>
                            <td></td>
                            <td width="40%">A : Kendaraan telah memiliki customer</td>
                        </tr>
                        <tr>
                            <td class="auto-style1">
                                <asp:Label ID="lbl2KodeCabang" runat="server">Kode Cabang</asp:Label>
                            &nbsp;:</td>
                            <td colspan="3" class="auto-style2">
                                <span id="spanPopUpDB" runat="server" visible="true">
                                    <asp:TextBox ID="txtDealerBranchCode" Width="150px" runat="server"></asp:TextBox>
                                    <asp:Label ID="lblPopUpDealerBranch" runat="server" Width="10"> 
										<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0">
                                    </asp:Label>
                                </span>

                                <asp:Label ID="lblDealerBranch" runat="server" Visible="False"></asp:Label>

                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style1">Nama Cabang :</td>
                            <td class="auto-style2" colspan="3">
                                <asp:textbox id="txtBranchName" Width="150px" Runat="server" disabled=""></asp:textbox></td>
                        </tr>
                        <tr>
                            <td colspan="4" height="10">
                                <p>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtChassisMaster"
                                        Display="None" ErrorMessage="Silahkan isi No Rangka (tidak boleh kosong)"></asp:RequiredFieldValidator>
                                </p>
                                <p>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtTglPDI" Display="None"
                                        ErrorMessage="Silahkan isi tgl PDI dengan format  'ddMMyyyy' misal 01122005" Height="15px" ValidationExpression="\d{2}\d{2}\d{4}"></asp:RegularExpressionValidator><asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlPDIKind" Display="None"
                                            ErrorMessage="Silahkan Isi Jenis PDI (Tidak boleh kosong)"></asp:RequiredFieldValidator><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtTglPDI" Display="None"
                                                ErrorMessage="Silahkan isi tgl PDI dengan format 'ddmmyyyy'"></asp:RequiredFieldValidator>
                                </p>
                                <p>
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" Width="136px" DisplayMode="List" ShowMessageBox="True"
                                        ShowSummary="False"></asp:ValidationSummary>
                                </p>
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td class="auto-style5">No. Rangka</td>
                            <td class="auto-style5">No. Mesin</td>                            
                            <td class="auto-style5">
                                <asp:Label ID="lblKM" title="A = Mengirim PDI/ Kendaraan Sudah Terjual &#13;&#10;B = Tidak Mengirim PDI/ Kendaraan Belum Terjual &#13;&#10;C = Tidak Mengirim PDI/ Kendaraan Sudah Terjual &#13;&#10;D = Mengirim PDI/ Kendaraan Belum Terjual"
                                    runat="server" CssClass="help"> Jenis PDI</asp:Label></td>
                            <td class="auto-style5">
                                <asp:Label ID="LblTglServis" runat="server">Tanggal PDI</asp:Label></td>
                            <td class="auto-style5">WO Number</td>
                            <td width="25%">
                                <asp:Button ID="btnSave" runat="server" Width="60px" Text="Simpan" Visible="False"></asp:Button><asp:Button ID="btnCancel" runat="server" Width="60px" Text="Batal" Visible="False" CausesValidation="False"></asp:Button></td>
                        </tr>
                        <tr>
                            <td class="auto-style6">
                                <asp:TextBox ID="txtChassisMaster" onkeypress="return alphaNumericExcept(event,'<>?*%$;')" onblur="omitSomeCharacter('txtChassisMaster','<>?*%$;')"
                                    runat="server" Width="140px" MaxLength="20"></asp:TextBox></td>
                            <td class="auto-style6">
                                <asp:TextBox ID="txtEngineNo" onkeypress="return alphaNumericExcept(event,'<>?*%$;')" onblur="omitSomeCharacter('txtEngineNo','<>?*%$;')"
                                    runat="server" Width="140px" MaxLength="20"></asp:TextBox></td>                            
                            <td class="auto-style6">
                                <asp:DropDownList ID="ddlPDIKind" runat="server" Width="140px"></asp:DropDownList></td>
                            <td class="auto-style6">
                                <asp:TextBox ID="txtTglPDI" runat="server" Width="140px" MaxLength="8"></asp:TextBox></td>
                            <td class="auto-style6">
                                <asp:TextBox ID="txtWONumber" onkeypress="return alphaNumericExcept(event,'<>?*%$;')" onblur="omitSomeCharacter('txtWONumber','<>?*%$;')"
                                    runat="server" Width="140px" MaxLength="20"></asp:TextBox></td>
                            <td>
                                <input id="btnSimpan" style="width: 60px" type="button" value="Simpan" name="btnSimpan"
                                    runat="server" causesvalidation="true">
                                <input id="btnBatal" style="width: 60px" type="button" value="Batal" name="btnBatal" runat="server"
                                    causesvalidation="False">
                            </td>
                        </tr>
                    </table>
                    <p></p>
                    <div id="div1" style="height: 350px; overflow: auto">
                        <asp:DataGrid ID="dgPDIEntry" runat="server" Width="100%" AllowSorting="True" CellSpacing="1"
                            AutoGenerateColumns="False" BorderColor="#CDCDCD" BorderStyle="Solid" BorderWidth="1px" BackColor="#CDCDCD" CellPadding="3" GridLines="Vertical"
                            PageSize="1000">
                            <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                            <AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
                            <ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
                            <HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
                            <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
                            <Columns>
                                <asp:BoundColumn Visible="False" DataField="ID" ReadOnly="True" HeaderText="ID">
                                    <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="No">
                                    <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblNo" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn>
                                    <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <HeaderTemplate>
                                        <input id="chkAllItems" onclick="CheckAll('cbSelect', document.forms[0].chkAllItems.checked)"
                                            type="checkbox">
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="cbSelect" runat="server"></asp:CheckBox>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn Visible="False" HeaderText="Dealer Code">
                                    <HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Width="53px" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerCode") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="DealerBranch.DealerBranchCode" HeaderText="Cabang">
                                    <HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblDealerBranch" runat="server" Width="53px" ToolTip='<%# DataBinder.Eval(Container, "DataItem.DealerBranch.Name")%>' Text='<%# DataBinder.Eval(Container, "DataItem.DealerBranch.DealerBranchCode")%>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="ChassisMaster.ChassisNumber" HeaderText="No. Rangka ">
                                    <HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblChassisNo" runat="server" Width="157px" Text='<%# DataBinder.Eval(Container, "DataItem.ChassisMaster.ChassisNumber") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Left"></FooterStyle>
                                </asp:TemplateColumn>                                
                                <asp:TemplateColumn SortExpression="ChassisMaster.Category.ProductCategory.Code" HeaderText="Kategori"
                                    Visible="False">
                                    <HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="Label2" runat="server" Width="157px" Text='<%# DataBinder.Eval(Container, "DataItem.ChassisMaster.Category.ProductCategory.Code") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Left"></FooterStyle>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="ChassisMaster.Category.CategoryCode" HeaderText="Kategori" Visible="false">
                                    <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="Label3" runat="server" Width="157px" Text='<%# DataBinder.Eval(Container, "DataItem.ChassisMaster.Category.CategoryCode") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Left"></FooterStyle>
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="Kind" SortExpression="Kind" HeaderText="Jenis PDI">
                                    <HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="PDIDate" SortExpression="PDIDate" ReadOnly="True" HeaderText="Tanggal PDI"
                                    DataFormatString="{0:dd/MM/yyyy}">
                                    <HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:TemplateColumn SortExpression="WorkOrderNumber" HeaderText="WO Number ">
                                    <HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="WONumber" runat="server" Width="157px" Text='<%# DataBinder.Eval(Container, "DataItem.WorkOrderNumber")%>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Left"></FooterStyle>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn>
                                    <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnEdit" runat="server" Width="20px" Text="Ubah" CausesValidation="False" CommandName="Edit">
												<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
                                        <asp:LinkButton ID="lbtnDelete" runat="server" Text="Hapus" CausesValidation="False" CommandName="Delete">
												<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                            <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
                        </asp:DataGrid>
                    </div>
                    <div>
                        <asp:TextBox ID="txtDisclaimer" runat="server" TextMode="MultiLine" Width="600px" Height="50px"></asp:TextBox>
                    </div>
                    <asp:Button ID="btnRelease" runat="server" Width="60px" Text="Rilis" Visible="False" CausesValidation="false"></asp:Button><input id="btnRilis" style="width: 60px" type="button" value="Rilis" name="btnRilis" runat="server"
                        causesvalidation="False"></td>
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
