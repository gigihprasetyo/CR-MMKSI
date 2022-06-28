<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmEntryFreeServisBB.aspx.vb" Inherits="FrmEntryFreeServisBB" SmartNavigation="False" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>Free Service</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link rel="stylesheet" type="text/css" href="../WebResources/stylesheet.css">
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
                txtBranchName.value = selectedDealer.split(";")[1];

                //do post back to filter grid data based on selected dealerbranchcode
                __doPostBack("<%= txtDealerBranchCode.ClientID()%>", "");
                }
                else {
                    var txtDealerSelection = document.getElementById("txtDealerBranchCode");
                    txtDealerSelection.value = selectedDealer;
                }
            }

            function firstFocus() {
                document.all.txtChassisMasterBB.focus();
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


            function checkFSPeriod() {
                var d = new Date('<%= Now.toString("yyyy/M/d HH:mm:ss") %>');
		        var a = new Date("2013/1/1 00:00:00");
		        var b = new Date("2013/1/11 00:00:00");
		        var str = "";

		        str = str + "<table width='100%'>";
		        str = str + "	<tr height='100px'>";
		        str = str + "		<td></td>";
		        str = str + "	</tr>";
		        str = str + "	<tr height='200px'>";
		        str = str + "		<td align='center'>";
		        str = str + "		Sehubungan dengan adanya perubahan proses pengiriman data kupon Free Service,<br>maka untuk sementara menu ini tidak dapat diakses mulai tanggal 1 Januari 2013 s/d 10 Januari 2013	";
		        str = str + "		</td>";
		        str = str + "	</tr>";
		        str = str + "	<tr>";
		        str = str + "		<td></td>";
		        str = str + "	</tr>";
		        str = str + "</table>";

		        //alert(d);
		        if (d.valueOf() > a.valueOf()) {
		            if (d.valueOf() < b.valueOf()) {
		                //alert(str);
		                document.write(str);
		            }
		        }
		    }
    </script>
</head>
<body onload="firstFocus();checkFSPeriod();" ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <table id="Table1" border="0" cellspacing="0" cellpadding="0" width="100%">
            <tr>
                <td class="titlePage">FREE SERVICE -&nbsp; Data Free Service Special</td>
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
                <td valign="top" align="left">
                    <table id="Table2" border="0" cellspacing="0" cellpadding="0" width="100%">
                        <tr>
                            <td class="titleField">
                                <asp:Label ID="lblDealer" runat="server">Kode Dealer </asp:Label></td>
                            <td width="1%" colspan="2">:
                                <asp:Label ID="lblDealerCode" runat="server"></asp:Label></td>

                            <td width="75%" colspan="2"></td>
                        </tr>
                        <tr>
                            <td class="titleField">Nama Dealer</td>

                            <td colspan="2">:
                                <asp:Label ID="lblDealerName" runat="server"></asp:Label></td>
                            <td colspan="2"></td>
                        </tr>
                        <tr>
                            <td class="titleField">Kode Cabang</td>
                            <td colspan="2">:
                                <asp:TextBox ID="txtDealerBranchCode" runat="server" AutoPostBack="True" Width="150px"></asp:TextBox>
                                <asp:Label ID="lblPopUpDealerBranch" runat="server" Width="10"> 
										<img alt="Klik Popup" border="0" src="../images/popup.gif" style="cursor:hand"></img>
                                </asp:Label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtDealerBranchCode"
                                    Display="None" ErrorMessage="Silahkan isi Cabang Dealer (tidak boleh kosong)"></asp:RequiredFieldValidator>
                                <asp:HiddenField runat="server" ID="hdnTitle" Value="DEALER" />
                            </td>
                            <td colspan="2"></td>
                        </tr>
                        <tr>
                            <td class="titleField">Nama Cabang</td>
                            <td colspan="2">:
                                <asp:TextBox ID="txtBranchName" Width="150px" runat="server" disabled=""></asp:TextBox>
                            </td>
                            <td colspan="2"></td>
                        </tr>
                        <tr>
                            <td height="10" colspan="7">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Silahkan isi Kode Jenis Servis + No Rangka (tidak boleh kosong)"
                                    Display="None" ControlToValidate="txtChassisMasterBB"></asp:RequiredFieldValidator><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Silahkan isi jarak tempuh (tidak boleh kosong)"
                                        Display="None" ControlToValidate="txtKM"></asp:RequiredFieldValidator><asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Silahkan isi tgl servis dengan format 'ddmmyyyy'"
                                            Display="None" ControlToValidate="txtTglServis"></asp:RequiredFieldValidator><asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Silahkan isi jarak tempuh dengan angka"
                                                Display="None" ControlToValidate="txtKM" ValidationExpression="\d{1,6}"></asp:RegularExpressionValidator><asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Silahkan isi tgl Servis dengan format  'ddMMyyyy' misal 01122005"
                                                    Display="None" ControlToValidate="txtTglServis" ValidationExpression="^((((0?[1-9]|[12]\d|3[01])[\.\-\/](0?[13578]|1[02])[\.\-\/]((1[6-9]|[2-9]\d)?\d{2}))|((0?[1-9]|[12]\d|30)[\.\-\/](0?[13456789]|1[012])[\.\-\/]((1[6-9]|[2-9]\d)?\d{2}))|((0?[1-9]|1\d|2[0-8])[\.\-\/]0?2[\.\-\/]((1[6-9]|[2-9]\d)?\d{2}))|(29[\.\-\/]0?2[\.\-\/]((1[6-9]|[2-9]\d)?(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00)|00)))|(((0[1-9]|[12]\d|3[01])(0[13578]|1[02])((1[6-9]|[2-9]\d)?\d{2}))|((0[1-9]|[12]\d|30)(0[13456789]|1[012])((1[6-9]|[2-9]\d)?\d{2}))|((0[1-9]|1\d|2[0-8])02((1[6-9]|[2-9]\d)?\d{2}))|(2902((1[6-9]|[2-9]\d)?(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00)|00))))$"></asp:RegularExpressionValidator><asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ErrorMessage="Silahkan isi tgl Jual dengan 8 digit angka,"
                                                        Display="None" ControlToValidate="txtTglJual" ValidationExpression="\d{8}"></asp:RegularExpressionValidator><asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="Silahkan isi tgl Jual dengan format  'ddMMyyyy' mis 01122005"
                                                            Display="None" ControlToValidate="txtTglJual" ValidationExpression="^((((0?[1-9]|[12]\d|3[01])[\.\-\/](0?[13578]|1[02])[\.\-\/]((1[6-9]|[2-9]\d)?\d{2}))|((0?[1-9]|[12]\d|30)[\.\-\/](0?[13456789]|1[012])[\.\-\/]((1[6-9]|[2-9]\d)?\d{2}))|((0?[1-9]|1\d|2[0-8])[\.\-\/]0?2[\.\-\/]((1[6-9]|[2-9]\d)?\d{2}))|(29[\.\-\/]0?2[\.\-\/]((1[6-9]|[2-9]\d)?(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00)|00)))|(((0[1-9]|[12]\d|3[01])(0[13578]|1[02])((1[6-9]|[2-9]\d)?\d{2}))|((0[1-9]|[12]\d|30)(0[13456789]|1[012])((1[6-9]|[2-9]\d)?\d{2}))|((0[1-9]|1\d|2[0-8])02((1[6-9]|[2-9]\d)?\d{2}))|(2902((1[6-9]|[2-9]\d)?(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00)|00))))$"></asp:RegularExpressionValidator></td>
                        </tr>
                        <tr>
                            <td style="height: 2px" width="20%" colspan="2"></td>
                            <td style="height: 2px" width="1%"></td>
                            <td style="height: 2px" width="10%">
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="False"></asp:ValidationSummary>
                            </td>

                            <td style="height: 2px" width="10%"></td>
                            <td style="height: 2px" width="10%"></td>
                            <td style="height: 2px" width="10%"></td>
                            <td style="height: 2px" width="10%">
                                <asp:Button ID="btnSave" runat="server" CausesValidation="True" Text="Simpan" Visible="False" Width="60"></asp:Button><asp:Button ID="btnCancel" runat="server" CausesValidation="False" Text="Batal" Visible="False" Width="60"></asp:Button>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField">Kind + No. Rangka</td>
                            <td class="titleField"></td>
                            <td class="titleField">No. Mesin</td>
                            <td class="titleField">
                                <asp:Label ID="lblKM" runat="server">Jarak Tempuh (KM)</asp:Label></td>
                            <td class="titleField">
                                <asp:Label ID="LblTglServis" runat="server" Width="73px">Tgl. Service</asp:Label></td>
                            <td class="titleField">
                                <asp:Label ID="lblTgl" runat="server">Tgl. Penjualan</asp:Label></td>
                            <td class="titleField">
                                <asp:Label ID="lblVisitType" runat="server">Tipe Visit</asp:Label></td>
                            <td class="titleField">
                                <asp:Label ID="lblWONumber" runat="server">WO Number</asp:Label></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox onblur="omitSomeCharacter('txtTglJual','<>?*%$;')" ID="txtChassisMasterBB" onkeypress="return alphaNumericExcept(event,'<>?*%$;')"
                                    runat="server" Width="174px" ToolTip="Silakan isi kode jenis free service disambung langsung dengan nomor rangka (tanpa spasi). Contoh: 1SA00FFF4K000008 dimana 1 adalah tipe servis 1000 km dan SA00FFF4K000008 adalah nomor rangka."
                                    MaxLength="20"></asp:TextBox></td>
                            <td></td>
                            <td>
                                <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" ID="txtEngineNumber" runat="server"
                                    onblur="omitSomeCharacter('txtTglJual','<>?*%$;')" Width="124px" MaxLength="20" ToolTip="Silakan isi dengan nomor mesin (tanpa spasi). Contoh: 4G15-C97564"></asp:TextBox></td>
                            <td>
                                <asp:TextBox onblur="omitSomeCharacter('txtKM','<>?*%$;')" ID="txtKM" onkeypress="return numericOnlyUniv(event)"
                                    runat="server" Width="120px" ToolTip="Harus dimasukan dengan angka" MaxLength="6" Height="18px"></asp:TextBox></td>
                            <td>
                                <asp:TextBox onblur="omitSomeCharacter('txtTglJual','<>?*%$;')" ID="txtTglServis" onkeypress="return alphaNumericExcept(event,'<>?*%$;')"
                                    runat="server" Width="120px" ToolTip="Format tgl Servis adalah  'ddMMyyyy' misal 31122005 berarti 31-12-2005"
                                    MaxLength="8" Height="17px"></asp:TextBox></td>
                            <td>
                                <asp:TextBox onblur="omitSomeCharacter('txtTglJual','<>?*%$;')" ID="txtTglJual" onkeypress="return alphaNumericExcept(event,'<>?*%$;')"
                                    runat="server" Width="120px" ToolTip="Format tgl Servis adalah  'ddMMyyyy' misal 31122005 berarti 31-12-2005"
                                    MaxLength="8" Height="18px"></asp:TextBox></td>
                            <td>
                                <asp:DropDownList ID="ddlVisitType" runat="server" ToolTip="Silahkan pilih Tipe Visit"></asp:DropDownList></td>
                            <td>
                                <asp:TextBox ID="txtWONumber" runat="server" Width="124px" MaxLength="50" ToolTip="Maksimal 50 karakter"></asp:TextBox></td>
                            <td style="margin: 20px">
                                <input style="width: 50px" id="btnSimpan" value="Simpan" type="button" name="btnSimpan"
                                    runat="server" causesvalidation="False">
                                <input style="width: 50px" id="btnBatal" value="Batal" type="button" name="btnBatal" runat="server"
                                    causesvalidation="False"></td>
                        </tr>
                        <tr>
                            <td style="width: 169px; height: 10px" colspan="6"></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="height: 385px" valign="top">
                    <div style="height: 350px; overflow: auto" id="div1">
                        <asp:DataGrid ID="dgFreeServisBBEntry" runat="server" Width="100%" PageSize="1000" AllowSorting="True"
                            CellSpacing="1" AutoGenerateColumns="False" BorderColor="#CDCDCD" BorderStyle="None" BorderWidth="0px" BackColor="#CDCDCD" CellPadding="3"
                            GridLines="Vertical">
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
                                <asp:TemplateColumn>
                                    <HeaderStyle Width="3%" CssClass="titleTableService"></HeaderStyle>
                                    <HeaderTemplate>
                                        <input id="chkAllItems" onclick="CheckAll('cbSelect', document.forms[0].chkAllItems.checked)"
                                            type="checkbox">
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="cbSelect" runat="server"></asp:CheckBox>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn Visible="False" HeaderText="Dealer Code">
                                    <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Width="53px" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerCode") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="FSKind.KindDescription" HeaderText="Jenis Free Servis">
                                    <HeaderStyle Width="12%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblFSKind" runat="server" Width="149px" Text='<%# DataBinder.Eval(Container, "DataItem.FSKind.KindDescription") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="ChassisMasterBB.ChassisNumber" HeaderText="No. Rangka ">
                                    <HeaderStyle Width="12%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblChassisNo" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ChassisMasterBB.ChassisNumber") %>' Width="173px">
                                        </asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Left"></FooterStyle>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn Visible="False" SortExpression="ChassisMasterBB.Category.ProductCategory.Code" HeaderText="Produk">
                                    <HeaderStyle Width="12%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="Label3" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ChassisMasterBB.Category.ProductCategory.Code") %>' Width="173px">
                                        </asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Left"></FooterStyle>
                                </asp:TemplateColumn>

                                <asp:TemplateColumn Visible="false" SortExpression="ChassisMasterBB.Category.CategoryCode" HeaderText="Kategori">
                                    <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="Label4" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ChassisMasterBB.Category.CategoryCode")%>' Width="65px">
                                        </asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Left"></FooterStyle>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="MileAge" HeaderText="Jarak Tempuh KM">
                                    <HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblDatKm" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.MileAge") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="ServiceDate" HeaderText="Tgl. Free Service">
                                    <HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="Label2" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ServiceDate", "{0:dd/MM/yyyy}") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="SoldDate" HeaderText="Tgl. Penjualan">
                                    <HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="tglPenjualan" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SoldDate", "{0:dd/MM/yyyy}") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="VisitType" HeaderText="Tipe Visit">
                                    <HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblVisitType" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.VisitType")%>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="WorkOrderNumber" HeaderText="WO Number">
                                    <HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblWONumber" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.WorkOrderNumber")%>'>
                                        </asp:Label>
                                    </ItemTemplate>
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
                                    <FooterStyle HorizontalAlign="Center"></FooterStyle>
                                </asp:TemplateColumn>
                            </Columns>
                            <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
                        </asp:DataGrid>
                    </div>
                    <br>
                    <asp:Button ID="btnRelease" runat="server" Width="60px" Visible="False" Text="Rilis" CausesValidation="False"></asp:Button><input style="width: 60px" id="btnRilis" value="Rilis" type="button" name="btnRilis" runat="server"
                        causesvalidation="False"></td>
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
