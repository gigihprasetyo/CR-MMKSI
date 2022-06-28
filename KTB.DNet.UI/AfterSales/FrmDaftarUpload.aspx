<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmDaftarUpload.aspx.vb" Inherits="FrmDaftarUpload" SmartNavigation="False" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>

<html>
<head>
    <title>FrmSalesChannel</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script language="javascript" src="../WebResources/FormFunctions.js"></script>

    <script>

        function openWindowDownload() {
            var h = document.getElementById('hUploadURL');
            var url = h.value
            //showPopUp(url, '', 250, 350, "");
            window.open(url, 'uploadpage', 'menubar=1,resizable=0,width=400,height=250');
        }

        function ShowPPDealerSelection() {
            showPopUp('../PopUp/PopUpDealerSelectionArea.aspx', '', 500, 760, DealerSelection);
        }

        function DealerSelection(selectedDealer) {
            var tempParam = selectedDealer;
            var txtDealerSelection = document.getElementById("txtKodeDealer");
            txtDealerSelection.value = tempParam;
        }

    </script>
</head>
<body leftmargin="0" topmargin="0" ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage">Data Upload - Daftar Upload</td>
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
                <td>
                    <table id="Table2" cellspacing="2" cellpadding="2">
                        <tr>
                            <td class="titleField" style="width: 30%">Kode Dealer</td>
                            <td width="1%">:</td>
                            <td style="width: 69%">
                                <asp:TextBox ID="txtKodeDealer" onblur="omitSomeCharacter('txtKodeDealer','<>?*%$')" runat="server"
                                    Width="152px"></asp:TextBox><asp:Label ID="lblSearchDealer" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup">
                                    </asp:Label></td>
                        </tr>
                        <tr>
                            <td class="titleField" width="24%">Bulan Periode</td>
                            <td width="1%">:</td>
                            <td width="30%">
                                <asp:DropDownList ID="ddlMonth" runat="server" Width="100px"></asp:DropDownList></td>
                            <asp:RequiredFieldValidator ID="Requiredfieldvalidator2" runat="server" ErrorMessage="*" ControlToValidate="ddlMonth" InitialValue=" "
                                Width="16px" EnableClientScript="False" Display="Dynamic"></asp:RequiredFieldValidator>
                        </tr>
                        <tr>
                            <td class="titleField" width="24%">Tahun Periode</td>
                            <td width="1%">:</td>
                            <td width="30%">
                                <asp:DropDownList ID="ddlYear" runat="server" Width="100px"></asp:DropDownList></td>
                            <asp:RequiredFieldValidator ID="Requiredfieldvalidator3" runat="server" ErrorMessage="*" ControlToValidate="ddlYear" InitialValue=" "
                                Width="16px" EnableClientScript="False" Display="Dynamic"></asp:RequiredFieldValidator>
                        </tr>
                        <tr>
                            <td class="titleField">
                                <asp:Label ID="lblAllocation" runat="server">Tanggal Upload</asp:Label></td>
                            <td>:</td>
                            <td style="width: 328px">
                                <table>
                                    <tr>
                                        <td>
                                            <cc1:IntiCalendar ID="periodeFrom" runat="server" TextBoxWidth="70"></cc1:IntiCalendar></td>
                                        <td>&nbsp;s.d&nbsp;</td>
                                        <td>
                                            <cc1:IntiCalendar ID="periodeTo" runat="server" TextBoxWidth="70"></cc1:IntiCalendar></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>

                        <tr>
                            <td class="titleField" width="24%">Tipe Pelaporan Data</td>
                            <td width="1%">:</td>
                            <td width="70%">
                                <asp:DropDownList ID="ddlTipePelaporan" runat="server" Width="180px"></asp:DropDownList>
                            </td>
                            <asp:RequiredFieldValidator ID="Requiredfieldvalidator4" runat="server" ErrorMessage="*" ControlToValidate="ddlTipePelaporan" InitialValue=" "
                                Width="16px" EnableClientScript="False" Display="Dynamic"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="titleField" width="24%">Status</td>
                <td width="1%">:</td>
                <td width="70%">
                    <asp:DropDownList ID="ddlStatus" runat="server" Width="180px"></asp:DropDownList></td>
                <asp:RequiredFieldValidator ID="Requiredfieldvalidator5" runat="server" ErrorMessage="*" ControlToValidate="ddlStatus" InitialValue=" "
                    Width="16px" EnableClientScript="False" Display="Dynamic"></asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td class="titleField"></td>
                <td></td>
                <td>
                    <asp:Button ID="btnCari" runat="server" Text=" Cari " Width="60px" CausesValidation="False"></asp:Button></td>
            </tr>
        </table>
        </TD>
				</TR>
				<tr>
                    <td>
                        <div id="div1" style="overflow: auto; height: 360px">
                            <asp:DataGrid ID="dtgDaftarUpload" OnItemCommand="dtgDaftarUpload_ItemCommand" runat="server" Width="100%" PageSize="100" AllowCustomPaging="True"
                                AllowPaging="True" CellSpacing="1" ToolTip="Module" AutoGenerateColumns="False" CellPadding="3" GridLines="None" BackColor="#E0E0E0"
                                BorderWidth="0px" BorderStyle="None" BorderColor="#E0E0E0" AllowSorting="True">
                                <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                                <AlternatingItemStyle ForeColor="Black" BackColor="#FDF1F2"></AlternatingItemStyle>
                                <ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
                                <HeaderStyle Font-Bold="True" ForeColor="White" BorderColor="#E0E0E0" BackColor="#CC3333"></HeaderStyle>
                                <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
                                <Columns>
                                    <asp:BoundColumn Visible="False" DataField="id" HeaderText="Id">
                                        <HeaderStyle Width="3%" CssClass="titleTableSales"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
                                    </asp:BoundColumn>
                                    <asp:BoundColumn Visible="False" DataField="ModuleID" HeaderText="ModuleID">
                                        <HeaderStyle Width="3%" CssClass="titleTableSales"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
                                    </asp:BoundColumn>
                                    <asp:TemplateColumn>
                                        <HeaderStyle Width="3%" CssClass="titleTableGeneral"></HeaderStyle>
                                        <HeaderTemplate>
                                            <input id="chkAllItems" onclick="CheckAllDataGridCheckBoxes('cbCheck', document.all.chkAllItems.checked)"
                                                type="checkbox">
                                        </HeaderTemplate>
                                        <ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="cbCheck" runat="server"></asp:CheckBox>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="No">
                                        <HeaderStyle Width="3%" CssClass="titleTableGeneral"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblNo"></asp:Label>
                                            <asp:Label Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID")%>' ID="lblID" NAME="lblID" />
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn SortExpression="Dealer.DealerCode" HeaderText="Kode Dealer">
                                        <HeaderStyle Width="8%" CssClass="titleTableGeneral"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="Label4" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerCode")%>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn SortExpression="Month" HeaderText="Periode">
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="Label14" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Period")%>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn SortExpression="ModuleID" HeaderText="Tipe Pelaporan Data">
                                        <HeaderStyle Width="18%" CssClass="titleTableGeneral"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.AssistModule.Name")%>' ID="Label2" NAME="Label2">
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn SortExpression="UploadTime" HeaderText="Tanggal Upload">
                                        <HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text='<%#Format( DataBinder.Eval(Container, "DataItem.UploadTime") ,"dd/MM/yyyy")%>' ID="Label3" NAME="Label3">
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn SortExpression="UploadTime" HeaderText="Waktu Upload">
                                        <HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text='<%#Format( DataBinder.Eval(Container, "DataItem.UploadTime") ,"HH:mm")%>' ID="Label11" NAME="Label11">
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn SortExpression="ErrorRatio" HeaderText="Error Ratio">
                                        <HeaderStyle Width="8%" CssClass="titleTableGeneral"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text='<%# String.Concat( DataBinder.Eval(Container, "DataItem.ErrorRatio")," %")%>' ID="Label8" NAME="Label8">
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn SortExpression="Performance" HeaderText="Performance">
                                        <HeaderStyle Width="8%" CssClass="titleTableGeneral"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text='<%# String.Concat(DataBinder.Eval(Container, "DataItem.Performance"), " %")%>' ID="Label5" NAME="Label5">
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn SortExpression="OriginalFileName" HeaderText="Original File">
                                        <HeaderStyle Width="18%" CssClass="titleTableGeneral"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.OriginalFileName")%>' ID="Label6" NAME="Label6">
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn SortExpression="Status" HeaderText="Status">
                                        <HeaderStyle Width="20%" CssClass="titleTableGeneral"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblStatusID" Visible="False" Text='<%# DataBinder.Eval(Container, "DataItem.ValidateStatus")%>'></asp:Label>
                                            <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.StatusDescription")%>' ID="Label7" NAME="Label7">
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn>
                                        <HeaderStyle Width="4%" CssClass="titleTableGeneral"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbtnOperator" runat="server" CommandName="Detail">
															<img src="../images/detail.gif" border="0" alt="Lihat Detail"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn>
                                        <HeaderStyle Width="4%" CssClass="titleTableGeneral"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbtnHapus" runat="server" CommandName="Delete">
															<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                </Columns>
                                <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
                            </asp:DataGrid>
                        </div>
                    </td>
                </tr>
        <tr>
            <td colspan="6" valign="bottom">
                <table id="tblOperator" style="width: 192px; height: 46px" cellspacing="1" cellpadding="1"
                    width="192" border="0" runat="server">
                    <tr>
                        <td valign="top">
                            <asp:Button ID="btnValidasi" runat="server" Width="60px" Text="Validasi"></asp:Button></td>
                        <td valign="top">
                            <asp:Button ID="btnTolakValidasi" runat="server" Width="80px" Text="Tolak Validasi"></asp:Button></td>
                        <td valign="top">
                            <asp:Button ID="btnKonfirmasi" runat="server" Width="60px" Text="Konfirmasi"></asp:Button></td>
                        <td valign="top">
                            <asp:Button ID="btnTolakKonfirmasi" runat="server" Width="90px" Text="Tolak Konfirmasi"></asp:Button></td>
                    </tr>
                </table>
            </td>
            <td></td>
        </tr>
        </TABLE>

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
