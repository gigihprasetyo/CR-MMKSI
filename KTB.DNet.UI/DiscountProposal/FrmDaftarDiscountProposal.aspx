<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmDaftarDiscountProposal.aspx.vb" Inherits=".FrmDaftarDiscountProposal" %>

<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>Daftar Discount Proposal</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>

    <script language="javascript" src="../WebResources/FormFunctions.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script type="text/javascript" src="../WebResources/jquery-1.7.2.min.js"></script>
    <script type="text/javascript">
        $("[src*=plus]").live("click", function () {
            $(this).closest("tr").after("<tr><td></td><td colspan = '999'>" + $(this).next().html() + "</td></tr>")
            $(this).attr("src", "../images/minus.gif");
        });
        $("[src*=minus]").live("click", function () {
            $(this).attr("src", "../images/plus.gif");
            $(this).closest("tr").next().remove();
        });
    </script>
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
        var strDealaerBranch = '../PK/../PopUp/PopUpDealerBranchMultipleSelection.aspx';

        function ShowPPDealerBranchSelection() {

            var hdnTitle = document.getElementById('hdnTitle');

            var uri = strDealaerBranch;

            if (hdnTitle.value == "MKS" || 1 == 1) {

                var txtDealerSelection = document.getElementById("txtKodeDealer");
                if (txtDealerSelection.value != '') {
                    uri = uri + "?DealerCode=" + txtDealerSelection.value;
                }

            }

            showPopUp(uri, '', 500, 760, DealerBranchSelection);
        }

        function DealerBranchSelection(selectedDealer) {

            var txtDealerSelection = document.getElementById("txtDealerBranchCode");
            txtDealerSelection.value = selectedDealer;

        }

        function ShowPPDealerSelection() {
            showPopUp('../General/../PopUp/PopUpDealerSelection.aspx', '', 500, 760, DealerSelection);
        }

        function DealerSelection(selectedDealer) {
            var txtDealerSelection = document.getElementById("txtKodeDealer");
            txtDealerSelection.value = selectedDealer;
        }

        function ViewDailyPKFlow()
        { }
        function ValidateData() {
            var ddl = document.getElementById("ddlRencanaPenebusan");
            var txt = document.getElementById("txtPKNumber");
            var lbl = document.getElementById("lblValidator");
            var rsl = true;

            lbl.style.visibility = "hidden";
            if (ddl.selectedIndex == 0) {
                if (txt.value == "") {
                    lbl.style.visibility = "visible";
                    rsl = false;
                }
            }
            //alert(rsl);
            return rsl;
        }
        setTimeout(function () {
            generateCheckBoxClick();
        }, 2000);
        function CheckAll(aspCheckBoxID) {
            var selectbox = getElement('input', 'chkbxAll')
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

            var table = document.getElementById('dgMain');
            var exitsno = '';
            for (i = 1; i < table.rows.length - 1; i++) {

                if (table.rows[i].cells[1].getElementsByTagName("input")[0].checked == true)
                    stringlist = stringlist + ";" + i

            }

            var arrayCheck = getElement('input', 'arrayCheck')
            if (selectbox.checked == true) {
                arrayCheck.value = stringlist
            } else arrayCheck.value = ""
        }
        function generateCheckBoxClick() {
            var inputs = document.getElementsByTagName("input");
            var stringlist = ""
            for (var i = 0; i < inputs.length; i++) {
                if (inputs[i].id.indexOf('cbxDetail') > -1) {
                    if (inputs[i].type == 'checkbox') {
                        inputs[i].onclick = function () { setValueCheckBox(); };
                    }
                }
            }
        }
        function setValueCheckBox() {
            var table = document.getElementById('dgMain');
            var stringlist = '';
            for (i = 1; i < table.rows.length - 1; i++) {
                if (table.rows[i].cells[1].getElementsByTagName("input")[0] != undefined) {
                    if (table.rows[i].cells[1].getElementsByTagName("input")[0].checked == true)
                        stringlist = stringlist + ";" + i
                }


            }
            var arrayCheck = getElement('input', 'arrayCheck')
            arrayCheck.value = stringlist
        }

        function isSuccesUpload(getSuccesUpload) {
            var dgMain = document.getElementById("dgMain");
            var tempParam = getSuccesUpload.split(';');
            var hdnDiscountProposalHeaderID = document.getElementById("hdnDiscountProposalHeaderID");
            hdnDiscountProposalHeaderID.value = tempParam[1]
        }
    </script>
</head>
<body onfocus="return checkModal()" onclick="checkModal()" ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <asp:Panel ID="Panel1" runat="server">
            <INPUT id="hdnDiscountProposalHeaderID" type="hidden" runat="server" NAME="hdnDiscountProposalHeaderID">
            <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
                <tr>
                <td class="titlePage">DISCOUNT PROPOSAL - Daftar Discount Proposal</td>
                </tr>
                <tr>
                    <td background="../images/bg_hor.gif" height="1" colspan="2">
                        <img height="1" src="../images/bg_hor.gif" border="0"></td>
                </tr>
                <tr>
                    <td height="10" colspan="2">
                        <img height="1" src="../images/dot.gif" border="0"></td>
                </tr>

                
                <tr>
                    <td style="width: 50%" valign="top">
                        <table style="width: 100%" cellpadding="0" cellspacing="0">
                            <tr>
                                <td style="width: 50%">
                                    <table width="100%">
                                        <tr>
                                            <td class="titlefield" width="30%">Kategori</td>
                                            <td style="padding-right:3px">
                                                <asp:Label ID="Label19" runat="server">:</asp:Label></td>
                                            <td>
                                                <asp:DropDownList ID="ddlCategory" runat="server" Width="120px" AutoPostBack="True"></asp:DropDownList>
                                                <asp:DropDownList Style="z-index: 0" ID="ddlSubCategory" runat="server" Width="118px"></asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 50%">
                                    <table width="100%">
                                        <tr valign="top">
                                            <td class="titlefield" width="30%">Status</td>
                                            <td style="padding-right:0px">
                                                <asp:Label ID="Label6" runat="server">:</asp:Label>
                                            </td>
                                            <td>
                                                <asp:ListBox ID="lboxStatus" runat="server" Width="136px" Rows="4" SelectionMode="Multiple"></asp:ListBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr valign="top">
                                <td style="width: 100%">
                                    <table width="100%">
                                        <tr>
                                            <td class="titlefield" width="30%"></td>
                                            <td style="padding-right:4px">
                                                <asp:Label runat="server"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Button ID="btnCari" runat="server" Text="Cari" Width="75px"></asp:Button>
                                                <asp:Button ID="btnDownload" runat="server" Text="Download" Width="100px"></asp:Button>
                                                <asp:HiddenField ID="arrayCheck" runat="server" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr style="padding-top: -10px">
                                <td style="width: 50%">
                                    <table width="100%">
                                        <%--<tr>
                                            <td class="titlefield" width="30%">Nama Customer</td>
                                            <td>
                                                <asp:Label ID="Label3" runat="server">:</asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtCustomerName" runat="server" onblur="omitSomeCharacter('txtCustomerName','&lt;&gt;?*%$;')" onkeypress="return alphaNumericExcept(event,'&lt;&gt;?*%$;')" Width="242px"></asp:TextBox>
                                            </td>
                                        </tr>--%>
                                    </table>
                                </td>
                            </tr>
                            <tr style="padding-top:10px;padding-bottom:10px">
                                <td style="width:50%">
                                    <table width="100%">
                                        <tr>
                                            <td width="20%">Mengubah Status :</td>
                                            <td width="20%">
                                                <asp:DropDownList ID="ddlStatus" runat="server" style="width:100%"></asp:DropDownList>
                                            </td>
                                            <td width="20%">
                                                <asp:Button ID="btnProses" runat="server" Text="Proses" Width="150px"></asp:Button>
                                            </td>
                                            <td>
                                                <asp:Button ID="btnTransfer" OnClientClick="return confirm('Anda yakin mau transfer data ke Groupware ?');" runat="server" Text="Transfer To Groupware" Width="202px"></asp:Button>
                                            </td>
                                            <%--<td>
                                                <asp:Button ID="btnDownload" runat="server" Text="Download" Width="100px"></asp:Button>
                                            </td>--%>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 50%" valign="top">
                        <table width="100%" cellpadding="0" cellspacing="0">
                            <tr>
                                <td class="titlefield" width="30%">Kode Dealer</td>
                                <td>
                                    <asp:Label ID="lbl1" runat="server">:</asp:Label>
                                </td>
                                <td colspan="3" style="padding-left:8px">
                                    <asp:TextBox ID="txtKodeDealer" runat="server" onblur="omitSomeCharacter('txtKodeDealer','&lt;&gt;?*%$;')" onkeypress="return alphaNumericExcept(event,'&lt;&gt;?*%$;')" Width="242px"></asp:TextBox>
                                    &nbsp;
                                    <asp:Label ID="lblPopUpDealer" runat="server" Width="16px"><img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0" /></asp:Label>
                                    <asp:Label ID="lblDealerSession" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="titlefield" width="30%">Tanggal Pengajuan Diskon</td>
                                <td>
                                    <asp:Label ID="Label1" runat="server">:</asp:Label>
                                </td>
                                <td colspan="3" style="padding-left:0px">
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:CheckBox ID="cbDateClaim" runat="server" /></td>
                                            <td>
                                                <cc1:IntiCalendar ID="icDateClaim" runat="server" TextBoxWidth="60"></cc1:IntiCalendar>
                                            </td>
                                            <td>s/d</td>
                                            <td>
                                                <cc1:IntiCalendar ID="icDateClaimTo" runat="server" TextBoxWidth="60"></cc1:IntiCalendar>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="titlefield" width="30%">Nomor Aplikasi Dealer</td>
                                <td>
                                    <asp:Label ID="Label4" runat="server">:</asp:Label>
                                </td>
                                <td colspan="3" style="padding-left:8px">
                                    <asp:TextBox ID="txtDPNo" runat="server" onblur="omitSomeCharacter('txtDPNo','&lt;&gt;?*%$;')" onkeypress="return alphaNumericExcept(event,'&lt;&gt;?*%$;')" Width="242px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="titlefield" width="30%">No.Reg Aplikasi</td>
                                <td>
                                    <asp:Label ID="Label5" runat="server">:</asp:Label>
                                </td>
                                <td colspan="3" style="padding-left:8px">
                                    <asp:TextBox ID="txtPropRegNo" runat="server" onblur="omitSomeCharacter('txtPropRegNo','&lt;&gt;?*%$;')" onkeypress="return alphaNumericExcept(event,'&lt;&gt;?*%$;')" Width="242px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="titlefield" width="30%">Nama Fleet Customer</td>
                                <td>
                                    <asp:Label ID="Label3" runat="server">:</asp:Label>
                                </td>
                                <td colspan="3" style="padding-left:8px">
                                    <asp:TextBox ID="txtCustomerName" runat="server" onblur="omitSomeCharacter('txtCustomerName','&lt;&gt;?*%$;')" onkeypress="return alphaNumericExcept(event,'&lt;&gt;?*%$;')" Width="242px"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="Panel2" runat="server">
            <div>
                <asp:DataGrid ID="dgMain" runat="server" Width="100%" AllowPaging="True" AllowSorting="True"
                    PageSize="25" AllowCustomPaging="True" AutoGenerateColumns="False" BackColor="#CDCDCD" BorderColor="Gainsboro" CellSpacing="1" BorderWidth="0px"
                    CellPadding="3" DataKeyField="ID">
                    <AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
                    <ItemStyle BackColor="White"></ItemStyle>
                    <HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Blue"></HeaderStyle>
                    <Columns>
                        <asp:TemplateColumn>
                            <HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            <HeaderTemplate>
                                <input type="checkbox" id="chkbxAll" onclick="CheckAll('cbxDetail')" />
                            </HeaderTemplate>

                            <ItemTemplate>
                                <asp:CheckBox ID="cbxDetail" runat="server" />
                                <img alt="" style="cursor: pointer" id="imgPlus" src="../images/plus.gif"  />
                                <asp:Panel ID="pnlDetail" runat="server" Style="display: none">
                                    <asp:DataGrid ID="dtgDetail" Width="100%" runat="server" AutoGenerateColumns="false">
                                        <Columns>
                                            <asp:TemplateColumn HeaderText="Model" SortExpression="">
                                                <HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemTemplate>
                                                        <asp:Label ID="lblModel" runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Tipe" SortExpression="">
                                                <HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblType" runat="server">
                                                    </asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Warna" SortExpression="">
                                                <HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblWarna" runat="server">
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Assy Year" SortExpression="">
                                                <HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAssyYear" runat="server">
                                                    </asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Unit" SortExpression="">
                                                <HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblUnit" runat="server">
                                                    </asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Permohonan Diskon" SortExpression="">
                                                <HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPermohonanDiskon" runat="server">
                                                    </asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Diskon Disetujui" SortExpression="">
                                                <HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDiskonDisetujui" runat="server">
                                                    </asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                            </asp:TemplateColumn>
                                        </Columns>
                                    </asp:DataGrid>
                                </asp:Panel>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="ID" Visible="false">
                            <HeaderStyle Width="0px" CssClass="titleTableSales hiddencol"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center" CssClass="hiddencol"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblIdDP" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="No">
                            <HeaderStyle Width="2%" CssClass="titleTableSales"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblNoGridRow" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Status">
                            <HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblStatusDP" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Dealer">
                            <HeaderStyle Width="8%" CssClass="titleTableSales"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblDealerDP" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Term 1">
                            <HeaderStyle Width="15%" CssClass="titleTableSales"></HeaderStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblTermDP" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Tgl. Pengajuan Dealer">
                            <HeaderStyle Width="8%" CssClass="titleTableSales"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblRequestDateDP" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="No. SPL">
                            <HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblNoSPLDP" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="No. Aplikasi Dealer">
                            <HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblNoAppDealerDP" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Kategori Customer">
                            <HeaderStyle Width="8%" CssClass="titleTableSales"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblCustCategoryDP" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Nama Customer">
                            <HeaderStyle Width="20%" CssClass="titleTableSales"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblCustName" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Total Pengajuan Dealer" Visible="false">
                            <HeaderStyle Width="15%" CssClass="titleTableSales"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblTotalRequestAmountDP" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Total Disetujui" Visible="false">
                            <HeaderStyle Width="15%" CssClass="titleTableSales"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblTotalApprovedDP" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn>
                            <HeaderStyle CssClass="titleTableSales"></HeaderStyle>
                            <ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkbtnUpload" runat="server" Width="20px" Text="Upload" CausesValidation="False" CommandName="Upload">
									<img alt="Upload" src="../images/icon_evid.gif" border="0">
                                </asp:LinkButton>
                                <asp:LinkButton ID="lnkbtnView" runat="server" Width="20px" Text="Detail" CausesValidation="False" CommandName="View" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
															<img alt="Detail" src="../images/Detail.gif" border="0"></asp:LinkButton>
                                <asp:LinkButton ID="lnkbtnEdit" runat="server" Width="20px" Text="Ubah" CausesValidation="False" CommandName="Edit" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
															<img alt="Ubah" src="../images/edit.gif" border="0"></asp:LinkButton>
                                <asp:LinkButton ID="lnkbtnDelete" runat="server" Width="20px" Text="Hapus" CausesValidation="False" CommandName="Delete" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.ID")%>'>
															<img alt="Hapus"  src="../images/trash.gif"
																border="0"></asp:LinkButton>
                                <asp:Label ID="lblHistoryStatus" runat="server">
												        <img src="../images/popup.gif" style="cursor:hand" alt="Histori Status">
                                                    </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                    </Columns>
                    <PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
                </asp:DataGrid>
            </div>
            <%--<div>
                <table>
                    <tr>
                        <td>Status</td>
                        <td>
                            <asp:DropDownList ID="ddlStatus" runat="server"></asp:DropDownList>
                        </td>
                        <td>
                            <asp:Button ID="btnProses" runat="server" Text="Proses" Width="100px"></asp:Button>
                        </td>
                        <td>
                            <asp:Button ID="btnTransfer" runat="server" Text="Transfer To Groupware" Width="200px"></asp:Button>
                        </td>
                        <td>
                            <asp:Button ID="btnDownload" runat="server" Text="Download" Width="100px"></asp:Button>
                        </td>
                    </tr>
                </table>
            </div>--%>
        </asp:Panel>
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
