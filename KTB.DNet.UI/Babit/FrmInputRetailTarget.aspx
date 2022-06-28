<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmInputRetailTarget.aspx.vb" Inherits=".FrmInputRetailTarget" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>FrmInputRetailTarget</title>
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
            txtKodeDealer.value = selectedRefNumber.split(";")[0];

            //if (navigator.appName == "Microsoft Internet Explorer") {
            //    hdnTemporaryOutlet.blur();
            //}
            //else {
            //    hdnTemporaryOutlet.onchange();
            //}
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage">MASTER -&nbsp; INPUT RETAIL TARGET</td>
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
                                <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" ID="txtKodeDealer" runat="server" MaxLength="10"></asp:TextBox>
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
                                <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" ID="txtKodeTempOut" runat="server" MaxLength="10"></asp:TextBox>
                                <asp:HiddenField ID="hdnTempOut" runat="server" />
                                <asp:label ID="lblPopUpTO" runat="server" Width="16px" Visible="false">
                                        <img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0">
                                </asp:label>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" style="height: 18px">Kategory / Model</td>
                            <td style="height: 18px">:</td>
                            <td style="height: 18px">
                                <asp:DropDownList ID="ddlCategory" runat="server" AutoPostBack="true" Style="margin-right: 15px"></asp:DropDownList>
                                <asp:DropDownList ID="ddlModel" runat="server"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" style="height: 18px">Periode</td>
                            <td>:</td>
                            <td>
                                <asp:CheckBox id="cbDate" runat="server" Width="2px"></asp:CheckBox>
                                <asp:DropDownList ID="ddlPeriodeMonth" runat="server" Style="margin-right: 15px"></asp:DropDownList>
                                <asp:DropDownList ID="ddlPeriodeYear" runat="server"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField">Retail Target</td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="txtRetailTarget" Style="text-align: right" runat="server" MaxLength="10" onkeypress="return NumericOnlyWith(event,'');"
                                    onkeyup="pic(this,this.value,'9999999999','N')" Width="100px" Text="0" /></td>
                </td>
            </tr>
            <tr id="trUpload" runat="server">
                <td class="titleField">Upload Data</td>
                <td>:</td>
                <td>
                    <input id="FileUploadIklan" type="file" runat="server" tabindex="0" style="width: 350px" />
                    <%--<asp:Button ID="btnDownloadExcel" runat="server" Text=" Download Template Excel " Style="margin-left: 20px"></asp:Button>--%>
                    <asp:LinkButton ID="lbtnDownloadExcel" runat="server" Text="Download Template Excel" Style="margin-left: 20px" />
                    <br />
                    <asp:GridView ID="GridView1" runat="server"></asp:GridView>
                </td>
            </tr>
            <tr>
                <td></td>
                <td></td>
                <td>
                    <asp:Button ID="btnSearch" runat="server" Text=" Cari " Width="60px" Style="margin-right: 20px"></asp:Button>
                    <asp:Button ID="btnSimpan" runat="server" Text=" Simpan " Style="margin-right: 20px"></asp:Button>
                    <asp:Button ID="btnCancel" runat="server" Width="60px" Text=" Batal " Style="margin-right: 20px"></asp:Button>
                    <asp:Button ID="btnUpload" runat="server" Text=" Upload " Width="70px"></asp:Button>
                </td>
            </tr>
        </table>
        <div id="div1" style="overflow: auto; height: 300px; margin-top: 10px">
            <asp:Label CssClass="titleField" ID="lblInfoUpload" runat="server" Visible="false"></asp:Label><br />
            <asp:DataGrid ID="dgListBabit" runat="server" Width="100%" CellSpacing="1" ForeColor="Gray" GridLines="Horizontal"
                CellPadding="3" BackColor="#CDCDCD" BorderWidth="0px" BorderColor="#CDCDCD" AutoGenerateColumns="False" AllowSorting="True" AllowCustomPaging="True"
                PageSize="25" AllowPaging="True" BorderStyle="None" DataKeyField="ID" ShowFooter="false">
                <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                <AlternatingItemStyle ForeColor="Black" BackColor="#F6F6F6"></AlternatingItemStyle>
                <ItemStyle ForeColor="Black" BackColor="White" HorizontalAlign="Center"></ItemStyle>
                <HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#4A3C8C" HorizontalAlign="Center"></HeaderStyle>
                <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
                <Columns>
                    <asp:TemplateColumn HeaderText="ID" Visible="false">
                        <HeaderStyle ForeColor="White" Width="2%" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblID" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
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
                        <HeaderStyle ForeColor="White" Width="30%" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblDealerName" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Temporary Outlet">
                        <HeaderStyle ForeColor="White" Width="7%" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblTempOut" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Model">
                        <HeaderStyle ForeColor="White" Width="10%" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblModel" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Retail Target">
                        <HeaderStyle ForeColor="White" Width="8%" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblTargetRetail" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Periode">
                        <HeaderStyle ForeColor="White" Width="10%" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblPeriode" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Status Upload" Visible="false">
                        <HeaderStyle Width="10%" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblStatusUpload" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <HeaderStyle ForeColor="White" Width="7%" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <%--<asp:LinkButton ID="lnkbtnDetail" runat="server" CommandName="Detail" CausesValidation="False">
												            <img src="../images/detail.gif" border="0" alt="Detail"></asp:LinkButton>--%>
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
