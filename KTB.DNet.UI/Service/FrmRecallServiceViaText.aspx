<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>

<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmRecallServiceViaText.aspx.vb" Inherits=".FrmRecallServiceViaText" SmartNavigation="False" %>


<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Recall Service</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <script type="text/javascript" language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script type="text/javascript" language="javascript" src="../WebResources/InputValidation.js"></script>
    <link rel="stylesheet" type="text/css" href="../WebResources/stylesheet.css">
    <script type="text/javascript" language="javascript">
        function RebindRecallCategory(txt) {
            var indek = GetIndex(txt);
            //console.log('ahhh');

            //var noRangka = dgRecallUpload.rows[indek].getElementsByTagName("INPUT")[0];
            var dgRecallUpload = document.getElementById("dgRecallUpload");
            var btnRangka = document.getElementById("dgRecallUpload__ctl" + (indek + 1) + "_btnBindRegCategori");
            //console.log(btnRangka);
            if (btnRangka) btnRangka.click();
        }

        function GetIndex(CtlID) {
            if (!navigator.appName == "Microsoft Internet Explorer") {
                var row = CtlID.parentElement.parentElement;
                indexRow = row.rowIndex;
                return row.rowIndex;
            }
            else {
                var row = CtlID.parentNode.parentNode;
                indexRow = row.rowIndex;
                return row.rowIndex;
            }
        }

        function RebindRegNo(txt) {
            var indek = GetIndex(txt);
            //    console.log('ahhh');

            //var noRangka = dgRecallUpload.rows[indek].getElementsByTagName("INPUT")[0];
            var dgRecallUpload = document.getElementById("dgRecallUpload");
            var btnRangka = document.getElementById("dgRecallUpload__ctl" + (indek + 1) + "_btnRebindRegNo");
            //  console.log(btnRangka);
            if (btnRangka) btnRangka.click();
        }

    </script>
</head>
<body ms_positioning="GridLayout">
    <form id="form1" runat="server" ms_positioning="GridLayout">
        <div>
            <table id="Table1" border="0" cellspacing="0" cellpadding="0" width="100%">
                <tr>
                    <td class="titlePage">Field Fix Campaign - Upload Pencapaian Field Fix Campaign</td>
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
                        <table id="Table2" border="0" cellspacing="2" cellpadding="1" width="100%">
                            <tr>
                                <td class="titleField">Kode Dealer</td>
                                <td width="1%">:</td>
                                <td>
                                    <asp:Label ID="lblDealerCOde" runat="server"></asp:Label></td>
                                <td></td>
                                <td></td>
                                <td width="25%"></td>
                            </tr>

                            <tr>
                                <td class="titleField">Nama Dealer</td>
                                <td width="1%">:</td>
                                <td>
                                    <asp:Label ID="lblDealerName" runat="server"></asp:Label></td>
                                <td></td>
                                <td></td>
                                <td width="25%"></td>
                            </tr>

                            <tr>
                                <td class="titleField" width="20%">Lokasi File</td>
                                <td width="1%">:</td>
                                <td width="79%" colspan="4">
                                    <input id="DataFile" onkeypress="return false;" size="29" type="file" name="File1" runat="server">
                                    <asp:Button ID="btnUpload" runat="server" Text="Upload" Width="60px"></asp:Button>
                                    <asp:Button ID="btnDonloadTmp" runat="server" Text="Download Template" Width="120px"></asp:Button>
                                </td>
                            </tr>
                            
                            <tr>
                                <td class="titleField">&nbsp;</td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td width="25%"></td>
                            </tr>                            
                        </table>
                    </td>
                </tr>
                <tr>
                    <td valign="top">
                        <div style="overflow: auto; height: 330px" id="div1">
                            <asp:DataGrid ID="dgRecallUpload" runat="server" Width="100%" AutoGenerateColumns="False" BorderColor="#E0E0E0"
                                BorderStyle="None" BorderWidth="0px" BackColor="#CDCDCD" CellPadding="3" GridLines="Horizontal"
                                Visible="False" CellSpacing="1" AllowSorting="false">
                                <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                                <AlternatingItemStyle BackColor="#F5F1EE"></AlternatingItemStyle>
                                <ItemStyle ForeColor="#4A3C8C" BackColor="White"></ItemStyle>
                                <HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
                                <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
                                <Columns>
                                    <asp:TemplateColumn HeaderText="No">
                                        <HeaderStyle Width="4%" CssClass="titleTableService"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblNo"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>

                                    <asp:TemplateColumn SortExpression="ChassisNo" HeaderText="Chassis Number">
                                        <HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                        <ItemTemplate>
                                            <%--<asp:Label runat="server" ID="txtNoRangka"></asp:Label>--%>
                                            <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ChassisNumber")%>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>

                                    <asp:TemplateColumn SortExpression="MileAge" HeaderText="Mile Age">
                                        <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                        <ItemTemplate>
                                            <%--<asp:Label runat="server" ID="txtMileAge"></asp:Label>--%>
                                            <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.MileAge")%>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>

                                    <asp:TemplateColumn SortExpression="ServiceDate" HeaderText="Service Date (ddmmyyyy)">
                                        <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                        <ItemTemplate>
                                            <%--<asp:Label runat="server" ID="txtServiceDate"></asp:Label>--%>
                                            <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ServiceDate","{0:dd/MM/yyyy}") %>' CssClass="textRight">
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>

                                    <asp:TemplateColumn SortExpression="CreatedTime" HeaderText="Input Date (ddmmyyyy)">
                                        <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                        <ItemTemplate>
                                            <%--<asp:Label runat="server" ID="txtCrTime"></asp:Label>--%>
                                            <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CreatedTime", "{0:dd/MM/yyyy}")%>' CssClass="textRight">
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>

                                    <asp:TemplateColumn SortExpression="ServiceDealerID" HeaderText="Service Dealer Code">
                                        <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                        <ItemTemplate>
                                            <%--<asp:Label runat="server" ID="txtDealerCode"></asp:Label>--%>
                                            <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ServiceDealerID")%>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>

                                    <asp:TemplateColumn SortExpression="RecallRegNo" HeaderText="Recall Category No">
                                        <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                        <ItemTemplate>
                                            <%--<asp:Label runat="server" ID="txtRecalRegNo"></asp:Label>--%>
                                            <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.RecallRegNo")%>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>

                                    <asp:TemplateColumn SortExpression="WorkOrderNumber" HeaderText="Work Order No">
                                        <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                        <ItemTemplate>
                                            <%--<asp:Label runat="server" ID="txtWONo"></asp:Label>--%>
                                            <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.WorkOrderNumber")%>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>

                                    <asp:BoundColumn DataField="ErrorMessage" SortExpression="ErrorMessage" ReadOnly="True" HeaderText="Status">
                                        <HeaderStyle Width="20%" CssClass="titleTableParts"></HeaderStyle>
                                    </asp:BoundColumn>


                                </Columns>
                            </asp:DataGrid>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td style="height: 40px" align="left"></td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnStore" runat="server" Text="Simpan" Width="60px" Enabled="False"></asp:Button>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>                
            </table>
        </div>
    </form>


    <script type="text/C#" language="javascript">
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
