<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frmUploadMaintainGeneralRepair.aspx.vb" Inherits="frmUploadMaintainGeneralRepair" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<html>
<head>
    <title>FrmDepositA</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <link href="./WebResources/stylesheet.css" type="text/css" rel="stylesheet">

    <style>
        .hide {
            DISPLAY: none;
        }
        .auto-style1 {
            height: 13px;
        }
    </style>
    <script language="javascript" src="../WebResources/FormFunctions.js"></script>
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>

    <script language="javascript">
        function ShowPPDealerSelection() {
            showPopUp('../PopUp/PopUpDealerSelection.aspx', '', 500, 760, DealerSelection);
        }

        function ShowPopUpUpload() {
            showPopUp('../PopUp/PopUpUploadMaintainGeneralRepair.aspx', '', 500, 760, RefreshPage);
        }

        function RefreshPage(message) {
            alert(message)
            var btnrefresh = document.getElementById("btnrefresh");
            btnrefresh.click();
        }

        function PopupDetail(id) {
            showPopUp('../PopUp/PopUpUploadMaintainGeneralRepairDetail.aspx?id=' + id, '', 500, 760, GRGaborSelection);
        }

        function GRGaborSelection(selectedDealer) {
            console.log(selectedDealer)
            var tempParam = selectedDealer;
            var txtGRGaborSelection = document.getElementById("IDGRGabor");
            txtGRGaborSelection.value = tempParam;
        }
        function getNextSibling(startBrother) {
            endBrother = startBrother.nextSibling;
            while (endBrother.nodeType != 1) {
                endBrother = endBrother.nextSibling;
            }
            return endBrother;
        }
        function GetDetailData() {
            console.log(document.getElementById("IDGRGabor"))
            window.external.GetDetailData(document.getElementById("IDGRGabor").innerHTML)
        }
        var isshown = false;
        function toggleDetail(elm) {
            //GetDetailData()

            if (elm.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.rows[elm.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.rowIndex + 1].style.display == "none") {
                isshown = false;
            }
            if (elm.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.rows[elm.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.rowIndex + 1].style.display == "") {
                isshown = true;
            }
            if (!isshown) {
                elm.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.rows[elm.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.rowIndex + 1].style.display = "block";
                elm.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.rows[elm.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.rowIndex + 1].style.display = "";
                isshown = true;
            }
            else {
                elm.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.rows[elm.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.rowIndex + 1].style.display = "none";
                elm.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.rows[elm.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.rowIndex + 1].style.display = "";
                elm.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.rows[elm.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.rowIndex + 1].style.display = "none";
                isshown = false;
            }

            if (elm.childNodes[2].tagName == 'IMG') {
                if (elm.childNodes[2].style.display == 'none') {
                    elm.childNodes[2].style.display = 'block';
                }
                else {
                    elm.childNodes[2].style.display = 'none';

                }
            }
            else {
                if (elm.childNodes[3].style.display == 'none') {
                    elm.childNodes[3].style.display = 'block';
                }
                else {
                    elm.childNodes[3].style.display = 'none';

                }
            }
            if (elm.childNodes[0].tagName == 'IMG') {
                if (elm.childNodes[0].style.display == 'none') {
                    elm.childNodes[0].style.display = 'block';
                }
                else {
                    elm.childNodes[0].style.display = 'none';
                }
            }
            else {
                if (elm.childNodes[1].style.display == 'none') {
                    elm.childNodes[1].style.display = 'block';
                }
                else {
                    elm.childNodes[1].style.display = 'none';
                }
            }
        }
        function toggleDepositDetail(elm) {
            var tr = elm.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode;
            var rows = tr.parentNode.rows;

            if (elm.childNodes[1].style.display == 'none') {
                elm.childNodes[1].style.display = 'block';
            }
            else {
                elm.childNodes[1].style.display = 'none';

            }

            if (elm.childNodes[0].style.display == 'none') {
                elm.childNodes[0].style.display = 'block';
            }
            else {
                elm.childNodes[0].style.display = 'none';
            }
            var suffix = (getNextSibling(elm.parentNode).innerHTML); // innerHTML ,handle mozilla req
            for (var i = 0; i < rows.length; i++) {
                if (rows[i].id == "tr" + suffix) {
                    if (rows[i].style.display == "none") {
                        rows[i].style.display = ""; // handle mozilla req
                    }
                    else {
                        rows[i].style.display = "none";
                    }
                }
            }
        }
    </script>
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <table style="id: 'Table2'" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage">Service - Maintain General Repair</td>
            </tr>
            <tr>
                <td background="../images/bg_hor.gif" height="1">
                    <img height="1" src="../images/bg_hor.gif" border="0"></td>
            </tr>
            <tr>
                <td class="auto-style1">
                    <img height="1" src="../images/dot.gif" border="0"></td>
            </tr>
                <tr>
                    <td>
                        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
                          
                            <tr>
                                <td style="width: 2px; clip: rect(auto, auto, auto, auto);">&nbsp;</td>
                                <td></td>
                                <td class="titleField">
                                    <asp:Button ID="btnSearch" runat="server" Text="Upload" Width="72px"></asp:Button>
                                   <%-- <asp:Button ID="BtnDownload" runat="server" Text="Batal" Width="72px"></asp:Button>
                                    <asp:Button ID="BtnDownloadHeader" runat="server" Text="Simpan" Width="100px"></asp:Button>--%>
                                    <%--<asp:Button ID="BtnDownloadAllDetail" runat="server" Text="Download Detail" Width="100px" Visible="True"></asp:Button></td>--%>
                                     <asp:LinkButton ID="LnkDownloadTemplate" runat="server" ToolTip="Download Template Excell">Download Template</asp:LinkButton>
                            </tr>
                            </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:DataGrid ID="dtlDepositA"  runat="server" Width="100%" BackColor="#E0E0E0" AutoGenerateColumns="False"
                        BorderColor="#CDCDCD" BorderStyle="None" BorderWidth="1px" CellPadding="3" ShowFooter="false" AllowSorting="True" AllowCustomPaging="True" PageSize="25"
                        AllowPaging="True">
                            <SelectedItemStyle ForeColor="#CCFF99" BackColor="#009999"></SelectedItemStyle>
                            <AlternatingItemStyle ForeColor="Black" BackColor="White"></AlternatingItemStyle>
                            <ItemStyle ForeColor="Black" BackColor="#F1F6FB"></ItemStyle>
                            <HeaderStyle CssClass="titleTableSales"></HeaderStyle>
                            <Columns>
                                <asp:TemplateColumn HeaderText="ID" Visible="false">
                                    <HeaderStyle Width="2%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="IDGRGabor" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="No" Visible="false">
                                    <HeaderStyle Width="1%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblGRKindID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.GRKindID")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="No">
                                    <HeaderStyle Width="2%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblNo" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Variant">
                                    <HeaderStyle Width="2%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblVariant" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Variants")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Jenis Service">
                                    <HeaderStyle Width="2%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lbJenisService" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Kind Code">
                                    <HeaderStyle Width="2%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lbKindCode" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Jasa Service">
                                    <HeaderStyle Width="2%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lbJasaService" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.LaborCost")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Durasi Service">
                                    <HeaderStyle Width="2%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lbDurasiService" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.LaborDuration")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Mulai Berlaku">
                                    <HeaderStyle Width="2%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lbMulaiBerlaku" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ValidFrom")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Detail">
                                    <HeaderStyle Width="2%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton1" CausesValidation="False" CommandName="detail" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID")%>' Text="Lihat" runat="server">
													<img src="../images/detail.gif" border="0" alt="Lihat"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                            <PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
                    
                        </asp:DataGrid></td>
                </tr>
            
        </table>
        <asp:Button ID="btnrefresh" runat="server" Text="Button" style="visibility:hidden"/>
    </form>
</body>
</html>
