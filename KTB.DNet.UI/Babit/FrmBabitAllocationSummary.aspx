<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmBabitAllocationSummary.aspx.vb" Inherits=".FrmBabitAllocationSummary" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<html>
<head runat="server">
    <title>FrmBabitAllocationSummary</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script language="javascript" src="../WebResources/FormFunctions.js"></script>
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
            var hdnTemporaryOutlet = document.getElementById("hdnTempOut");
            var txtKodeTempOut = document.getElementById("txtKodeTempOut");
            hdnTemporaryOutlet.value = selectedRefNumber;
            txtKodeTempOut.value = selectedRefNumber.split(";")[0];

            //if (navigator.appName == "Microsoft Internet Explorer") {
            //    hdnTemporaryOutlet.blur();
            //}
            //else {
            //    hdnTemporaryOutlet.onchange();
            //}
        }

        function ShowPopUpDealer() {
            showPopUp('../PopUp/PopUpDealerSelectionOne.aspx', '', 430, 800, DealerSSS);
        }

        function DealerSSS(selectedRefNumber) {
            var txtKodeDealer = document.getElementById("txtKodeDealer");
            var lblKodeDealer = document.getElementById("lblKodeDealer");
            txtKodeDealer.value = selectedRefNumber.split(";")[0];
            lblKodeDealer.value = selectedRefNumber.split(";")[0];
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage">BABIT -&nbsp; BABIT Allocation Summary</td>
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
                <td align="left">
                    <table id="Table2" cellspacing="1" cellpadding="2" width="100%" border="0">
                        <tr>
                            <td class="titleField" width="10%">Kode Dealer</td>
                            <td width="1%">:</td>
                            <td width="50%">
                                <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" ID="txtKodeDealer" runat="server" MaxLength="10" Enabled="false"></asp:TextBox>
                                <asp:HiddenField ID="hdnDealer" runat="server" />
                                <asp:label ID="lblPopUpDealer" runat="server" Width="16px">
                                        <img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0">
                                </asp:label>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField">Kode Temporary Outlet</td>
                            <td>:</td>
                            <td>
                                <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" ID="txtKodeTempOut" runat="server" MaxLength="10" Enabled="false"></asp:TextBox>
                                <asp:HiddenField ID="hdnTempOut" runat="server" />
                                <asp:label ID="lblPopUpTO" runat="server" Width="16px">
                                        <img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0">
                                </asp:label>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" style="height: 18px">Kategory/Model</td>
                            <td style="height: 18px">:</td>
                            <td style="height: 18px">
                                <asp:DropDownList ID="ddlCategory" runat="server" style="margin-right: 15px"></asp:DropDownList>
                                <asp:DropDownList ID="ddlModel" runat="server"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" style="height: 18px">Periode</td>
                            <td>:</td>
                            <td>
                                <asp:DropDownList ID="ddlPeriodeMonth" runat="server" style="margin-right: 15px"></asp:DropDownList>
                                <asp:DropDownList ID="ddlPeriodeYear" runat="server"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" style="height: 18px">Tipe Babit Alokasi</td>
                            <td>:</td>
                            <td>
                                <asp:DropDownList ID="ddlBabitAllocationType" runat="server"></asp:DropDownList>
                            </td>
                        </tr>
<%--                        <tr>
                            <td class="titleField">Upload Data</td>
                            <td>:</td>
                            <td><input id="FileUploadIklan" type="file" runat="server" tabindex="0" style="width: 350px" />
                                <span style="width: 20px;"></span>
                                <asp:Button ID="btnDownloadExcel" runat="server" Text=" Download Template Excel " Style="margin-left: 20px"></asp:Button>
                            </td>
                        </tr>--%>
                        <tr>
                            <td></td>
                            <td></td>
                            <td>
                                <%--<asp:Button ID="btnSimpan" runat="server" Text=" Simpan " ></asp:Button>--%>
                                <asp:Button ID="btnSearch" runat="server" Width="60px" Text=" Cari " Style="margin-left: 20px"></asp:Button>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <div id="div1" style="overflow: auto; height: 300px; margin-top: 20px">
            <asp:DataGrid ID="dgListBabit" runat="server" Width="100%" CellSpacing="1" ForeColor="Gray" GridLines="Horizontal"
                CellPadding="3" BackColor="#CDCDCD" BorderWidth="0px" BorderColor="#CDCDCD" AutoGenerateColumns="False" AllowSorting="True" AllowCustomPaging="True"
                PageSize="10" AllowPaging="True" BorderStyle="None" DataKeyField="ID" ShowFooter="false">
                <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                <AlternatingItemStyle ForeColor="Black" BackColor="#F6F6F6"></AlternatingItemStyle>
                <ItemStyle ForeColor="Black" BackColor="White" HorizontalAlign="Center"></ItemStyle>
                <HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#4A3C8C" HorizontalAlign="Center"></HeaderStyle>
                <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
                <Columns>
                    <asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
                        <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                    </asp:BoundColumn>
                    <asp:TemplateColumn HeaderText="No">
                        <HeaderStyle ForeColor="White" Width="2%" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblNo" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Kode Dealer">
                        <HeaderStyle ForeColor="White" Width="7%" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblDealerCode" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Nama Dealer">
                        <HeaderStyle ForeColor="White" Width="10%" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblDealerName" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Temporary Outlet">
                        <HeaderStyle ForeColor="White" Width="10%" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblTempOut" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Tipe Babit">
                        <HeaderStyle ForeColor="White" Width="8%" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblBabitType" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Kategori">
                        <HeaderStyle ForeColor="White" Width="8%" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblCategory" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Periode">
                        <HeaderStyle ForeColor="White" Width="10%" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblPeriode" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Babit Alokasi">
                        <HeaderStyle ForeColor="White" Width="10%" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblBabitAllocation" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Total Penggunaan">
                        <HeaderStyle ForeColor="White" Width="10%" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblTotalUsed" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Sisa Alokasi">
                        <HeaderStyle ForeColor="White" Width="10%" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblRemains" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderStyle ForeColor="White" Width="7%" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkbtnDetail" runat="server" CommandName="Detail" CausesValidation="False">
												            <img src="../images/detail.gif" border="0" alt="Detail"></asp:LinkButton>
                            <asp:LinkButton ID="lnkbtnEdit" runat="server" CommandName="Edit" CausesValidation="False">
												            <img src="../images/edit.gif"  border="0" alt="Ubah"></asp:LinkButton>
                            <asp:LinkButton ID="lnkbtnDelete" runat="server" CommandName="Delete" CausesValidation="False">
												            <img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateColumn>

                </Columns>
                <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
            </asp:DataGrid>
        </div>
    </form>
</body>
</html>
