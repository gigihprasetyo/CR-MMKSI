<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>

<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmBabitEventReportList.aspx.vb" Inherits=".FrmBabitEventReportList" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>FrmBabitList</title>
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
            var hdnDealer = document.getElementById("hdnDealer");
            var dealerCode = hdnDealer.value;
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
</head>
<body>
    <form id="form1" runat="server">
        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage">EVENT -&nbsp; DAFTAR LAPORAN EVENT</td>
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
                    <table id="Table3" cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td align="left" valign="top" style="width: 50%">
                                <table id="Table2" cellspacing="1" cellpadding="2" width="100%" border="0">
                                    <tr id="trCategory" runat="server">
                                        <td class="titleField">Kategori Dealer</td>
                                        <td>:</td>
                                        <td>
                                            <asp:DropDownList ID="ddlCategory" runat="server"></asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titleField" width="15%" valign="top">
                                            <asp:Label ID="lblDealerSearch" runat="server">Kode Dealer</asp:Label></td>
                                        <td width="1%">:</td>
                                        <td width="34%">
                                            <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" ID="txtKodeDealer" runat="server" MaxLength="10"></asp:TextBox>
                                            <asp:HiddenField ID="hdnDealer" runat="server" />
                                            <asp:label ID="lblPopUpDealer" runat="server" Width="16px">
                                            <img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0">
                                            </asp:label>
                                            <asp:Label ID="lblKodeDealer" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titleField" valign="top">Kode Temporary Outlet</td>
                                        <td>:</td>
                                        <td>
                                            <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" ID="txtKodeTempOut" runat="server" MaxLength="10"></asp:TextBox>
                                            <asp:HiddenField ID="hdnTempOut" runat="server" />
                                            <asp:label ID="lblPopUpTO" runat="server" Width="16px">
                                            <img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0">
                                            </asp:label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titleField" style="height: 18px">Tgl Pelaksanaan Event</td>
                                        <td>:</td>
                                        <td>
                                            <table cellspacing="0" cellpadding="0" border="0" width="80%">
                                                <tr valign="top">
                                                    <td>
                                                        <asp:CheckBox ID="cbDate" runat="server" Width="2px"></asp:CheckBox>
                                                    </td>
                                                    <td>
                                                        <cc1:IntiCalendar ID="icPeriodeStart" runat="server" TextBoxWidth="70"></cc1:IntiCalendar></td>
                                                    <td valign="bottom">&nbsp;&nbsp;s.d&nbsp;&nbsp;</td>
                                                    <td>
                                                        <cc1:IntiCalendar ID="icPeriodeEnd" runat="server" TextBoxWidth="70"></cc1:IntiCalendar></td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titleField" style="height: 18px">Nama Laporan Event</td>
                                        <td style="height: 18px">:</td>
                                        <td style="height: 18px">
                                            <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$')" ID="txtEventReportName"
                                                runat="server" MaxLength="50" Width="160px"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td class="titleField" style="height: 18px">Nomor Reg Proposal</td>
                                        <td style="height: 18px">:</td>
                                        <td style="height: 18px">
                                            <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$')" ID="txtEventRegNumberProposal"
                                                runat="server" MaxLength="50" Width="160px"></asp:TextBox></td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 50%" valign="top">
                                <table id="Table4" cellspacing="1" cellpadding="2" width="70%" border="0">
                                    <tr valign="top">
                                        <td class="titleField">Status</td>
                                        <td>
                                            <asp:Label ID="lbltitik2" runat="server">:</asp:Label></td>
                                        <td>
                                            <asp:ListBox ID="lsStatus" runat="server" Width="160px" Rows="9"></asp:ListBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <hr />
                    <asp:Button ID="btnSearch" runat="server" Width="60px" Text=" Cari "></asp:Button>
                    <asp:Button ID="btnDownloadExcel" runat="server" Text=" Download Excel " Style="margin-left: 15px"></asp:Button>
                <td></td>
                <td></td>
            </tr>
        </table>
        <br />
        <div id="div1" style="overflow: auto; height: 300px">
            <asp:DataGrid ID="dgEventReportList" runat="server" Width="100%" CellSpacing="1" ForeColor="Gray" GridLines="Horizontal"
                CellPadding="3" BackColor="#CDCDCD" BorderWidth="0px" BorderColor="#CDCDCD" AutoGenerateColumns="False" AllowSorting="True" AllowCustomPaging="True"
                PageSize="25" AllowPaging="True" BorderStyle="None" DataKeyField="ID" ShowFooter="false">
                <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                <AlternatingItemStyle ForeColor="Black" BackColor="#F6F6F6"></AlternatingItemStyle>
                <ItemStyle ForeColor="Black" BackColor="White" HorizontalAlign="Center"></ItemStyle>
                <HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#4A3C8C"></HeaderStyle>
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
                    <asp:TemplateColumn>
                        <HeaderStyle Width="3%" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        <HeaderTemplate>
                            <input id="chkAllItems" type="checkbox" onclick="CheckAll('chkItemChecked',
									document.forms[0].chkAllItems.checked)" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="chkItemChecked" runat="server"></asp:CheckBox>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Status" SortExpression="Status">
                        <HeaderStyle ForeColor="White" Width="7%" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblStatus" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Dealer" SortExpression="Dealer.ID">
                        <HeaderStyle ForeColor="White" Width="5%" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblDealer" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Temporary Outlet" SortExpression="DealerBranch.ID">
                        <HeaderStyle ForeColor="White" Width="7%" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblTempOut" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Nomor Reg Proposal" SortExpression="BabitEventProposalHeader.EventRegNumber">
                        <HeaderStyle ForeColor="White" Width="7%" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblEventRegNumber" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Nama Proposal Event" SortExpression="BabitEventProposalHeader.EventProposalName">
                        <HeaderStyle ForeColor="White" Width="10%" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblEventProposalName" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Nama Laporan Event" SortExpression="EventReportName">
                        <HeaderStyle ForeColor="White" Width="10%" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblEventReportName" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Tgl Periode Mulai" SortExpression="PeriodStart">
                        <HeaderStyle ForeColor="White" Width="7%" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblPeriodStart" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Tgl Periode Selesai" SortExpression="PeriodEnd">
                        <HeaderStyle ForeColor="White" Width="7%" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblPeriodEnd" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>

                    <asp:TemplateColumn HeaderText="Jumlah Subsidi" SortExpression="BabitEventProposalHeader.EventDealerHeader.SubsidyTarget">
                        <HeaderStyle Width="7%" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblSubsidyTarget" runat="server">
                            </asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Biaya Diajukan">
                        <HeaderStyle Width="7%" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblPrice" runat="server">
                            </asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Subsidi Akhir">
                        <HeaderStyle Width="7%" CssClass="titleTablePromo"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblSubsidyAkhir" runat="server">
                            </asp:Label>
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
                            <asp:LinkButton ID="lnkbtnDelete" OnClientClick="return confirm('Anda yakin mau hapus?');" CausesValidation="False"
                                CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>' CommandName="Delete" Text="Hapus" runat="server">
                                        <img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
                                                        <asp:LinkButton ID="lnkbtnDownload" runat="server" CommandName="Download" CausesValidation="False">
												            <img src="../images/download.gif" border="0" alt="Download"></asp:LinkButton>

                            <asp:LinkButton ID="lnkbtnKuitansi" runat="server" Width="20px" CausesValidation="False" CommandName="ViewKuitansi" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.ID")%>'>
								<img width="16" alt="Input Kuitansi" src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABQAAAAUCAYAAACNiR0NAAAABHNCSVQICAgIfAhkiAAAAAlwSFlzAAALEwAACxMBAJqcGAAAAZNJREFUOI2N08+LTXEYx/HX5TBXTcSgcJVRkkwJC7KaJrKTP2H+g8vCUk2zsKIkOytbNbOxpESTBQtsSM1iMkIYdaepkfll8f1+dRzX+Z5nc56+z+e8n895zvdpqY8bOIYjGV0b+3A9o/MOm3IiTGIcb3LiNaw3ABaxcbvICFPDPTiBVTzFRkW3jquYywFTHMJlrOA5flXqLUxgqinwJTq4iDul81ncxOboUh2w8Pf8XuBbRbMQn1uj+1rgIJZifh+n+2i6wk1o42cOuAOLMb+NoT6a5PBP8zrgTvyI+RiG+2hm8RrbU/M64F58jXnRR7sUnRPcL+SA+/Ep5g+xq1JfLuVD+J4DDgsDP4oH/9F08US4h2tl4DacFe5TilF8xkFcKZ13MCPMT6x/TMUEvCesU6/04hdh3UZKZwdi4x4uYB4n8aoKPIXj/t3RalwT5nkGhyNwDI+TIC3/RgMYbBH2uCd8+u7o9FFV+LYBDG7hA94L12Yel8qCQvhTKzifgQ1EN+eUfkI1Wrjb0N0qpvGsTvQbYqhQ2cHMsT0AAAAASUVORK5CYII=" border="0">
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateColumn>

                </Columns>
                <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
            </asp:DataGrid>
        </div>
        <br />
        <div style="vertical-align: bottom">
            <asp:Label ID="lblChangeStatus" runat="server" Font-Italic="True" Font-Bold="true" Visible="False">Mengubah Status :</asp:Label>&nbsp;
		    <asp:DropDownList ID="ddlStatus" runat="server" Visible="False"></asp:DropDownList>&nbsp;
		    <asp:Button ID="btnProses" runat="server" Width="64px" Text="Proses" Visible="False"></asp:Button>&nbsp;
            <asp:Button ID="btnTransfer" runat="server" Text="Transfer"></asp:Button>&nbsp;
            <asp:Button ID="btnTransferUlang" runat="server" Text="Transfer Ulang"></asp:Button>
        </div>
    </form>
</body>
</html>
