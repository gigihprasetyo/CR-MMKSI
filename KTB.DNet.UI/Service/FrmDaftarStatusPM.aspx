<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>

<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmDaftarStatusPM.aspx.vb" Inherits="FrmDaftarStatusPM" SmartNavigation="False" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>Daftar Status Free Service</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script type="text/javascript">

        function ShowPopUp() {
        }

        function ShowPPDealerSelection() {
            showPopUp('../PopUp/PopUpDealerSelection.aspx', '', 500, 760, DealerSelection);
        }

        function DealerSelection(selectedDealer) {
            var txtDealerSelection = document.getElementById("txtKodeDealer");
            txtDealerSelection.value = selectedDealer;
        }

        function ShowPPDealerBranchSelection() {
            var txtDealerSelection = document.getElementById("txtKodeDealer");
            showPopUp('../General/../PopUp/PopUpDealerBranchMultipleSelection.aspx?DealerCode=' + txtDealerSelection.value, '', 500, 760, DealerBranchSelection);
        }

        function DealerBranchSelection(selectedDealerBranch) {
            var txtDealerBranchSelection = document.getElementById("txtKodeDealerBranch");
            txtDealerBranchSelection.value = selectedDealerBranch;
        }

    </script>
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage">PERIODICAL MAINTENANCE - Daftar Status PM</td>
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
                    <table id="Table2" cellspacing="1" cellpadding="2" width="100%" border="0">
                        <tr>
                            <td class="titleField" style="width: 110px; height: 14px" width="110">
                                <asp:Label ID="lblDealer" runat="server">Kode Dealer </asp:Label></td>
                            <td style="height: 14px" width="1%">:</td>
                            <td style="height: 14px" width="25%">
                                <asp:TextBox ID="txtKodeDealer" runat="server" onkeypress="return alphaNumericExcept(event,'<>?*%$');"
                                    onblur="omitSomeCharacter('txtKodeDealer','<>?*%$');"></asp:TextBox><asp:Label ID="lblSearchDealer" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:Label></td>
                            <td width="110"></td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 110px; height: 14px" width="110">
                                <asp:Label ID="lblDealerBranch" runat="server">Kode Cabang </asp:Label></td>
                            <td style="height: 14px" width="1%">:</td>
                            <td style="height: 14px" width="25%">
                                <asp:TextBox ID="txtKodeDealerBranch" runat="server" onkeypress="return alphaNumericExcept(event,'<>?*%$');"
                                    onblur="omitSomeCharacter('txtKodeDealerBranch','<>?*%$');"></asp:TextBox><asp:Label ID="lblSearchDealerBranch" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:Label></td>
                            <td width="110"></td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 110px">Status</td>
                            <td>:</td>
                            <td>
                                <asp:DropDownList ID="ddlStatus" runat="server"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 110px; height: 18px" valign="top">Periode 
									Rilis</td>
                            <td style="height: 18px" valign="top">:</td>
                            <td style="height: 18px" valign="top">
                                <table cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td>
                                            <cc1:inticalendar id="ICDari" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
                                        <td>&nbsp;s.d&nbsp;</td>
                                        <td>
                                            <cc1:inticalendar id="ICSampai" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
                                        <td></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td><span class="titleField">
                                <asp:Label ID="lblCategory" runat="server" Visible="False"> Kategori</asp:Label></span></td>
                            <td>
                                <asp:Label ID="lblCategory2" runat="server" Visible="False">:</asp:Label></td>
                            <td>
                                <asp:DropDownList ID="ddlCategory" runat="server" Width="140px" Visible="False"></asp:DropDownList></td>
                        </tr>

                        <tr>
                            <td><span class="titleField">
                                No Rangka</span></td>
                            <td>:</td>
                            <td>
                                   <asp:TextBox runat="server" Width="173px" ID="txtChassisNo"   ></asp:TextBox>

                            </td>
                        </tr>

                        <tr>
                            <td><span class="titleField">
                                <label>Jenis PM</label>
                            </span></td>
                            <td>
                             :
                               
                            </td>
                            <td>  <asp:DropDownList ID="ddlPMKind" runat="server" Width="140px" ></asp:DropDownList></td>
                        </tr>


                        <tr>
                            <td></td>
                            <td></td>
                            <td>
                                <asp:Button ID="btnCari" runat="server" Width="60px" Text="Cari"></asp:Button></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <div id="div1" style="height: 300px; overflow: auto">
                        <asp:DataGrid ID="dgPMStatus" runat="server" Width="100%" ShowFooter="True" AutoGenerateColumns="False"
                            BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="0px" BackColor="#CDCDCD" CellPadding="3" GridLines="Vertical" AllowPaging="True" PageSize="50"
                            AllowSorting="True" CellSpacing="1">
                            <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                            <AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
                            <ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
                            <HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
                            <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
                            <Columns>
                                <asp:TemplateColumn HeaderText="No">
                                    <HeaderStyle Width="3%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblNo" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="PMStatus" HeaderText="Status">
                                    <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblStatus" runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Wrap="False" HorizontalAlign="Left" VerticalAlign="Top"></FooterStyle>
                                    <FooterTemplate>
                                        <b>Total Dealer :</b>
                                        <asp:Label ID="lblTotalDealer" runat="server"></asp:Label><br>
                                        <b>Total Unit :</b>
                                        <asp:Label ID="lblTotalUnit" runat="server"></asp:Label>
                                    </FooterTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="Dealer.DealerCode" HeaderText="Dealer">
                                    <HeaderStyle Width="8%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblKodeDealer" runat="server" ToolTip='<%# DataBinder.Eval(Container, "DataItem.Dealer.SearchTerm1") %>' Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerCode") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="DealerBranch.DealerBranchCode" HeaderText="Cabang">
                                    <HeaderStyle Width="8%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblKodeDealerBranch" runat="server" ToolTip='<%# DataBinder.Eval(Container, "DataItem.DealerBranch.Name")%>' Text='<%# DataBinder.Eval(Container, "DataItem.DealerBranch.DealerBranchCode")%>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="ChassisMaster.ChassisNumber" HeaderText="No Rangka">
                                    <HeaderStyle Width="20%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblNoChassis" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ChassisMaster.ChassisNumber") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>                                
                                <asp:TemplateColumn SortExpression="ChassisMaster.Category.CategoryCode" HeaderText="Kategori" Visible="false">
                                    <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ChassisMaster.Category.CategoryCode") %>' ToolTip='<%# DataBinder.Eval(Container, "DataItem.ChassisMaster.Category.CategoryCode") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="StandKM" HeaderText="Jenis PM">
                                    <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblJenis" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PMKindCode") %>' ToolTip='<%# DataBinder.Eval(Container, "DataItem.PMKindDesc") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Jarak Tempuh" SortExpression="StandKM">
                                    <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="right" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblJarakTempuh" runat="server" Text='<%# Format(DataBinder.Eval(Container, "DataItem.StandKM"),"#,##0") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="ServiceDate" HeaderText="Tgl PM">
                                    <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblTglPM" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ServiceDate","{0:dd/MM/yyyy}") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="ReleaseDate" HeaderText="Tgl Rilis">
                                    <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblTglRilis" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ReleaseDate","{0:dd/MM/yyyy}") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="VisitType" HeaderText="Tipe Visit">
                                    <HeaderStyle Width="8%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblVisitType" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="WorkOrderNumber" HeaderText="WO Number">
                                    <HeaderStyle Width="8%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblWONumber" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.WorkOrderNumber")%>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="Remarks" HeaderText="Keterangan" visible="false">
                                    <HeaderStyle Width="8%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label id="lblRemarks" runat="server" ToolTip='<%# DataBinder.Eval(Container, "DataItem.Remarks") %>' Text='<%# DataBinder.Eval(Container, "DataItem.Remarks")%>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                
                                <asp:TemplateColumn Visible="False">
                                    <HeaderStyle ForeColor="White" Width="7%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Wrap="False"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblPopUpDetail" runat="server">
												<img src="../images/detail.gif" border="0" alt="Replacement Part"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                            <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
                        </asp:DataGrid>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="height: 40px" align="left">&nbsp;
						<asp:Button ID="btnDownload" runat="server" Width="70px" Text="Download" Height="24px" Enabled="False"></asp:Button></td>
            </tr>
        </table>
    </form>
</body>
</html>
