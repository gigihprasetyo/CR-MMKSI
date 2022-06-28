<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmBabitAllocationDetail.aspx.vb" Inherits="FrmBabitAllocationDetail" SmartNavigation="False" MaintainScrollPositionOnPostback="true" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<html>
<head>
    <title>FrmBabitAllocationDetail</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link media="print" href="../WebResources/printstyle.css" type="text/css" rel="stylesheet">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <link href="./WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <link href="../WebResources/ImagePopup.css" type="text/css" rel="stylesheet">
    <script language="javascript" type="text/javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" type="text/javascript" src="../WebResources/InputValidation.js"></script>
    <script language="javascript">
        function ShowPopUpTO() {
            var txtKodeDealer = document.getElementById("txtKodeDealer");
            var dealerCode;
            if (txtKodeDealer == null) {
                txtKodeDealer = document.getElementById("lblKodeDealer");
                dealerCode = txtKodeDealer.innerText.split("/")[0].replace(/\s/g, '');
            } else {
                dealerCode = txtKodeDealer.value;
            }
            showPopUp('../PopUp/PopUpDealerBranchSelectionSingle.aspx?m=d&deal=' + dealerCode, '', 430, 800, TemporaryOutlet);
        }

        function TemporaryOutlet(selectedRefNumber) {
            //var txtTemporaryOutlet = document.getElementById("txtTemporaryOutlet");
            //var hdnTemporaryOutlet = document.getElementById("hdnTempOut");
            var txtKodeTempOut = document.getElementById("txtKodeTempOut");
            //hdnTemporaryOutlet.value = selectedRefNumber;
            txtKodeTempOut.value = selectedRefNumber.split(";")[0];

            //if (navigator.appName == "Microsoft Internet Explorer") {
            //    hdnTemporaryOutlet.blur();
            //}
            //else {
            //    hdnTemporaryOutlet.onchange();
            //}
        }

        function ShowPopUpDealer() {
            showPopUp('../babit/../PopUp/PopUpDealerSelectionOne.aspx?m=d', '', 430, 800, DealerSSS);
        }

        function DealerSSS(selectedRefNumber) {
            //var hdnTemporaryOutlet = document.getElementById("hdnTempOut");
            var txtKodeDealer = document.getElementById("txtKodeDealer");
            //hdnTemporaryOutlet.value = selectedRefNumber;
            txtKodeDealer.value = selectedRefNumber.split(";")[0];
            //if (navigator.appName == "Microsoft Internet Explorer") {
            //    hdnTemporaryOutlet.blur();
            //}
            //else {
            //    hdnTemporaryOutlet.onchange();
            //}
        }

        function calculatePrice(txtAddPrice) {
            var txtAddPriceVal = txtAddPrice.value.replace(".", "").replace(".", "").replace(".", "").replace(".", "");
            var index = txtAddPrice.parentNode.parentNode.rowIndex;
            var dtg = document.getElementById("dgListBabitMasterRetailTarget");
            var lblAlokasiBabitVal = dtg.rows[index].getElementsByTagName("SPAN")[8].innerHTML;
            var lblTotalAlokasiBabit = dtg.rows[index].getElementsByTagName("INPUT")[2];
            lblAlokasiBabitVal = lblAlokasiBabitVal.replace(".", "").replace(".", "").replace(".", "");
            lblTotalAlokasiBabit.value = toCommas(parseInt(lblAlokasiBabitVal) + parseInt(txtAddPriceVal))
        }

        function toCommas(value) {
            return value.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ".");
        }
    </script>
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <table id="Table23" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage">BABIT -&nbsp; BABIT ALOKASI DETAIL</td>
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
                <td align="left" style="width: 70%">
                    <table id="Table2" cellspacing="1" cellpadding="2" width="100%" border="0">
                        <tr>
                            <td class="titleField" width="15%">Kode Dealer</td>
                            <td width="1%">:</td>
                            <td width="50%">
                                <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" ID="txtKodeDealer" runat="server" MaxLength="10"></asp:TextBox>
                                <asp:HiddenField ID="hdnDealer" runat="server" />
                                <asp:label ID="lblPopUpDealer" runat="server" Width="16px">
                                        <img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0">
                                </asp:label>
                                <asp:Label ID="lblKodeDealer" runat="server" Visible="false"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField">Kode Temporary Outlet</td>
                            <td>:</td>
                            <td>
                                <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" ID="txtKodeTempOut" runat="server" MaxLength="10"></asp:TextBox>
                                <%--<asp:HiddenField ID="hdnTempOut" runat="server" />--%>
                                <asp:label ID="lblPopUpTO" runat="server" Width="16px">
                                        <img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0">
                                </asp:label>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" style="height: 18px">Kategory/ Model</td>
                            <td style="height: 18px">:</td>
                            <td style="height: 18px">
                                <asp:DropDownList ID="ddlCategory" runat="server" Style="margin-right: 15px" AutoPostBack="true"></asp:DropDownList>
                                <asp:DropDownList ID="ddlModel" runat="server" Width="150px"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" style="height: 18px">Periode</td>
                            <td>:</td>
                            <td>
                                <asp:DropDownList ID="ddlPeriodeMonth" runat="server" Style="margin-right: 15px"></asp:DropDownList>
                                <asp:DropDownList ID="ddlPeriodeYear" runat="server" Style="margin-right: 15px"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" style="height: 18px">Tipe Babit Alokasi</td>
                            <td>:</td>
                            <td>
                                <asp:DropDownList ID="ddlBabitAllocationType" runat="server"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td></td>
                            <td>
                                <asp:Button ID="btnSearch" runat="server" Width="60px" Text=" Cari "></asp:Button>
                                <asp:Button ID="btnDownload" runat="server" Width="100px" Text=" Download Excel " Style="margin-left: 10px"></asp:Button>
                                <asp:Button ID="btnCalculate" runat="server" Width="100px" Text=" Kalkulasi " Style="margin-left: 10px"></asp:Button>
                            </td>
                        </tr>
                    </table>
                </td>
                <td align="left" style="width: 30%" valign="top">
                    <table id="Table3" cellspacing="1" cellpadding="2" width="100%" border="0">
                        <tr valign="top" runat="server" id="trUpload" visible="false">
                            <td class="titleField">Upload Data</td>
                            <td>:</td>
                            <td>
                                <input id="FileUploadIklan" type="file" runat="server" tabindex="0" style="width: 150px" />
                                <span style="width: 20px;"></span>
                                <asp:LinkButton ID="lbtnDownloadExcel" runat="server" Text="Download Template Excel" Style="margin-left: 10px" /><br />
                                <asp:Button ID="btnSimpan" runat="server" Text=" Upload "></asp:Button>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <hr />
        <div id="div1" style="overflow: auto; height: 300px; margin-top: 10px">
            <asp:DataGrid ID="dgListBabitMasterRetailTarget" runat="server" Width="100%" AllowPaging="True" AllowSorting="True"
                PageSize="25" AllowCustomPaging="True" AutoGenerateColumns="False" BackColor="#CDCDCD" BorderColor="Gainsboro" CellSpacing="1" BorderWidth="0px"
                CellPadding="3" DataKeyField="ID">
                <AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
                <ItemStyle BackColor="White"></ItemStyle>
                <HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Blue"></HeaderStyle>
                <Columns>
                    <asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
                        <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                    </asp:BoundColumn>
                    <asp:TemplateColumn HeaderText="No">
                        <HeaderStyle ForeColor="White" Width="2%" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblNo" runat="server"></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:Label ID="lblENo" runat="server"></asp:Label>
                            <asp:HiddenField ID="hdnID" runat="server" />
                        </EditItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Kode Dealer" SortExpression="Dealer.DealerCode">
                        <HeaderStyle ForeColor="White" Width="5%" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblDealerCode" runat="server"></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:Label ID="lblEDealerCode" runat="server"></asp:Label>
                        </EditItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Nama Dealer" SortExpression="Dealer.DealerName">
                        <HeaderStyle ForeColor="White" Width="10%" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblDealerName" runat="server"></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:Label ID="lblEDealerName" runat="server"></asp:Label>
                        </EditItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Temporary Outlet" SortExpression="DealerBranch.DealerBranchCode">
                        <HeaderStyle ForeColor="White" Width="7%" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblTempOut" runat="server"></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:Label ID="lblETempOut" runat="server"></asp:Label>
                        </EditItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Retail Target" SortExpression="RetailTarget">
                        <HeaderStyle ForeColor="White" Width="5%" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblTargetRetail" runat="server"></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:Label ID="lblETargetRetail" runat="server"></asp:Label>
                        </EditItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Kategori" SortExpression="Category.CategoryCode">
                        <HeaderStyle ForeColor="White" Width="6%" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblCategory" runat="server"></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:Label ID="lblECategory" runat="server"></asp:Label>
                        </EditItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Model">
                        <HeaderStyle ForeColor="White" Width="10%" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblModel" runat="server"></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:Label ID="lblEModel" runat="server"></asp:Label>
                        </EditItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Periode">
                        <HeaderStyle ForeColor="White" Width="8%" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblPeriode" runat="server"></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:Label ID="lblEPeriode" runat="server"></asp:Label>
                        </EditItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Alokasi BABIT" SortExpression="AllocationBabit">
                        <HeaderStyle ForeColor="White" Width="8%" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblBabitAllocation" runat="server"></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:Label ID="lblEBabitAllocation" runat="server"></asp:Label>
                        </EditItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Alokasi Tambahan">
                        <HeaderStyle ForeColor="White" Width="8%" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblBabitAdditionalAllocation" runat="server"></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEBabitAdditionalAllocation" Style="text-align: right"
                                runat="server" onblur="calculatePrice(this)" onkeypress="return NumericOnlyWith(event,'');"
                                onkeyup="pic(this,this.value,'9999999999','N')" Width="80px" />
                        </EditItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Total Alokasi">
                        <HeaderStyle ForeColor="White" Width="8%" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblBabitTotalAllocation" runat="server"></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEBabitTotalAllocation" Style="text-align: right" disabled="disabled"
                                runat="server" onkeypress="return NumericOnlyWith(event,'');"
                                onkeyup="pic(this,this.value,'9999999999','N')" Width="80px" />
                        </EditItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Sisa">
                        <HeaderStyle ForeColor="White" Width="8%" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblRemains" runat="server"></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:Label ID="lblERemains" runat="server"></asp:Label>
                        </EditItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Terpakai">
                        <HeaderStyle ForeColor="White" Width="8%" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblOngoing" runat="server"></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:Label ID="lblEOngoing" runat="server"></asp:Label>
                        </EditItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderStyle Width="200px" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle HorizontalAlign="center"></ItemStyle>
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnEdit" CausesValidation="False" CommandName="edit" Text="Ubah" runat="server">
                                        <img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
                            <%--<asp:LinkButton ID="lbtnDelete" CausesValidation="False" CommandName="delete" Text="Hapus" runat="server">
                                        <img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>--%>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:LinkButton ID="lbtnSave" CommandName="save" Text="Simpan" runat="server">
                                        <img src="../images/simpan.gif" border="0" alt="Simpan"></asp:LinkButton>
                            <asp:LinkButton ID="lbtnCancel" runat="server" CausesValidation="True" CommandName="cancel" Text="Batal">
										<img src="../images/batal.gif" border="0" alt="Batal"></asp:LinkButton>
                        </EditItemTemplate>
                    </asp:TemplateColumn>

                </Columns>
                <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
            </asp:DataGrid>
        </div>
    </form>
</body>
</html>
