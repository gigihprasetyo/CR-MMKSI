<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmBabitReportReceiptList.aspx.vb" Inherits=".FrmBabitReportReceiptList" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>FrmBabitReportReceiptList</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script language="javascript" src="../WebResources/FormFunctions.js"></script>
    <script language="javascript">
        function ShowPopUpDealer() {
            showPopUp('../PopUp/PopUpDealerSelection.aspx', '', 430, 800, DealerSSS);
        }

        function DealerSSS(selectedRefNumber) {
            var hdnTemporaryOutlet = document.getElementById("hdnDealer");
            var txtKodeDealer = document.getElementById("txtKodeDealer");
            hdnTemporaryOutlet.value = selectedRefNumber;
            txtKodeDealer.value = selectedRefNumber;
        }

        function showPopupNoAcc(evt) {
            var index = getRowIndex(evt);
            var dgListKuitansi = document.getElementById("dgListKuitansi");
            var hdnSelectedKuitansi = document.getElementById("hdnSelectedKuitansi");
            hdnSelectedKuitansi.value = index;
            var hdnDealer = dgListKuitansi.rows[index].getElementsByTagName("SPAN")[2];
            if (hdnDealer.innerText == "") {
                alert("Dealer harus di isi !");
                return;
            }
            var codePart = dgListKuitansi.rows[index].getElementsByTagName("INPUT")[1];
            showPopUp('../PopUp/PopUpNoAccEvent.aspx?DealerCode=' + hdnDealer.innerText, '', 500, 760, getSelectedNoAcc);
        }

        function getRowIndex(el) {
            while ((el = el.parentNode) && el.nodeName.toLowerCase() !== 'tr');

            if (el)
                return el.rowIndex;
        }

        function getSelectedNoAcc(selectedPart) {
            var hdnSelectedKuitansi = document.getElementById("hdnSelectedKuitansi");
            var index = hdnSelectedKuitansi.value;
            var dgListKuitansi = document.getElementById("dgListKuitansi");
            var tempParams = selectedPart.split(';');
            var codePart = dgListKuitansi.rows[index].getElementsByTagName("INPUT")[1];
            codePart.value = trim(tempParams[0]);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage">BABIT -&nbsp; DAFTAR KUITANSI BABIT</td>
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
                    <table id="Table5" cellspacing="1" cellpadding="2" width="100%" border="0">
                        <tr>
                            <asp:HiddenField ID="hdnSelectedKuitansi" runat="server" />
                            <td class="titleField" width="3%">Kode Dealer</td>
                            <td width="1%">:</td>
                            <td width="34%">
                                <asp:Label ID="lblKodeDealer" runat="server"></asp:Label>
                                <asp:TextBox ID="txtKodeDealer" runat="server" Width="200px" TextMode="MultiLine" Height="30px"></asp:TextBox>
                                <asp:HiddenField ID="hdnDealer" runat="server" />
                                <asp:label ID="lblPopUpDealer" runat="server" Width="16px">
                                    <img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0">
                                </asp:label>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" width="3%">No. Kuitansi</td>
                            <td width="1%">:</td>
                            <td width="34%">
                                <asp:TextBox ID="txtNoKuitansi" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" width="3%">No. Reg Babit</td>
                            <td width="1%">:</td>
                            <td width="34%">
                                <asp:TextBox ID="txtNoRegBabit" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" width="3%">Tipe Babit</td>
                            <td width="1%">:</td>
                            <td width="34%">
                                <asp:DropDownList ID="ddlBabitType" runat="server"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" width="3%">Status</td>
                            <td width="1%">:</td>
                            <td width="34%">
                                <asp:DropDownList ID="ddlStatus" runat="server"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <hr />
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td></td>
                            <td>
                                <asp:Button ID="btnSearch" runat="server" Width="60px" Text=" Cari "></asp:Button>&nbsp;
                                <asp:Button ID="btnDownloadExcel" runat="server" Text=" Download Excel " Style="margin-left: 20px"></asp:Button>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <div id="div1" style="overflow: auto; height: 300px">
            <asp:DataGrid ID="dgListKuitansi" runat="server" Width="100%" CellSpacing="1" ForeColor="Gray" GridLines="Horizontal"
                CellPadding="3" BackColor="#CDCDCD" BorderWidth="0px" BorderColor="#CDCDCD" AutoGenerateColumns="False" AllowSorting="True" AllowCustomPaging="True"
                PageSize="10" AllowPaging="True" BorderStyle="None" DataKeyField="ID" ShowFooter="false">
                <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                <AlternatingItemStyle ForeColor="Black" BackColor="#F6F6F6"></AlternatingItemStyle>
                <ItemStyle ForeColor="Black" BackColor="White" HorizontalAlign="Center"></ItemStyle>
                <HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#4A3C8C" HorizontalAlign="Center"></HeaderStyle>
                <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
                <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
                <Columns>
                    <asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
                        <HeaderStyle CssClass="titleTablePromo"></HeaderStyle>
                    </asp:BoundColumn>
                    <asp:TemplateColumn>
                        <HeaderStyle Width="3%" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                        <HeaderTemplate>
                            <input id="chkAllItems" onclick="CheckAllDataGridCheckBoxes('chkSelect', document.all.chkAllItems.checked)"
                                type="checkbox">
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="chkSelect" runat="server"></asp:CheckBox>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="No">
                        <HeaderStyle ForeColor="White" Width="2%" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblNo" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Status" SortExpression="Status">
                        <HeaderStyle ForeColor="White" Width="5%" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblStatus" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Kode Dealer" SortExpression="BabitReportHeader.BabitHeader.Dealer.DealerCode">
                        <HeaderStyle ForeColor="White" Width="5%" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblDealerCode" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Nama Dealer" SortExpression="BabitReportHeader.BabitHeader.Dealer.DealerName">
                        <HeaderStyle ForeColor="White" Width="20%" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblDealerName" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="No. Reg Babit" SortExpression="BabitReportHeader.BabitHeader.BabitRegNumber">
                        <HeaderStyle ForeColor="White" Width="7%" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblRegBabit" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="No. Kuitansi" SortExpression="ReceiptNo">
                        <HeaderStyle ForeColor="White" Width="7%" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblReceipt" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="No. Acc" SortExpression="MasterAccrued.AccKey">
                        <HeaderStyle ForeColor="White" Width="7%" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblNoAcc" runat="server"></asp:Label>
                            <asp:TextBox ID="txtNoAcc" runat="server" Width="80px"></asp:TextBox>
                            <asp:HiddenField ID="hdnNoAcc" runat="server" />
                            <asp:Label ID="lblPopUpNoAcc" runat="server" Width="16px" OnClick="showPopupNoAcc(this);">
                                    <img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0">
                            </asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="No. Dokumen" SortExpression="">
                        <HeaderStyle ForeColor="White" Width="7%" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblNoJV" runat="server"></asp:Label>
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
                        </ItemTemplate>
                    </asp:TemplateColumn>
                </Columns>
            </asp:DataGrid>
        </div>
        <table id="Table3" border="0" cellspacing="1" cellpadding="1" width="100%">
            <tr>
                <td>
                    <hr />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label9" runat="server" Font-Bold="True" Font-Italic="True">Mengubah Status :</asp:Label>
                    <span style="margin-right: 10px"></span>
                    <asp:DropDownList ID="ddlAction" runat="server"></asp:DropDownList>
                    <span style="margin-right: 10px"></span>
                    <asp:Button ID="btnProses" runat="server" Text="Proses"></asp:Button><span style="margin-right: 10px"></span>
                    <asp:Button ID="btnGenerateJV" runat="server" Text="Generate Pengajuan JV"></asp:Button>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
