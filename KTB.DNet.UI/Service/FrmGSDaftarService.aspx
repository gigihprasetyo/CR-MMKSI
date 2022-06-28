<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmGSDaftarService.aspx.vb" Inherits=".FrmGSDaftarService" %>

<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<html>
<head runat="server">
    <title>Customer Satisfaction - Service Reminder </title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script language="javascript">
        function ShowDealerSelection() {
            showPopUp('../PopUp/PopUpDealerSelection.aspx', '', 500, 500, getDealer);
        }

        function ShowDealerBranchSelection() {
            showPopUp('../PopUp/PopUpDealerBranchMultipleSelection.aspx', '', 500, 500, getDealerBranch);
        }

        function getDealerBranch(dealer) {
            var txtDealerBranchCode = document.getElementById('txtDealerBranchCode');
            txtDealerBranchCode.value = dealer
        }

        function getDealer(dealer) {
            var txtDealerCode = document.getElementById('txtDealerCode');
            txtDealerCode.value = dealer
        }
    </script>
    <style type="text/css">
        .auto-style1 {
            width: 600px;
        }
        .auto-style3 {
            width:200px;
        }
        .auto-style5 {
            width:500px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <table id="TittleTable" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage">CUSTOMER SATISFACTION -&nbsp; Service Reminder</td>
            </tr>
            <tr>
                <td background="../images/bg_hor.gif" height="1">
                    <img height="1" src="../images/bg_hor.gif" border="0"></td>
            </tr>
            <tr>
                <td height="10">
                    <img height="1" src="../images/dot.gif" border="0"></td>
            </tr>
        </table>
        <table id="Table1" cellspacing="0" cellpadding="0" border="0">
			<tr>
                <td valign="top" align="left" class="auto-style5">
                    <table id="Table_left" cellspacing="1" cellpadding="2" width="100%" border="0">
                        <tr>
                            <td class="auto-style1" colspan="2">Kode dealer&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </td>                            
                            <td colspan="5px" class="auto-style4">
                                <asp:TextBox ID="txtDealerCode" runat="server" Width="90%" onkeypress="return alphaNumericExcept(event,'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz<>?*%${},+=');"
                                    ToolTip="Kode Dealer"></asp:TextBox>
                                <asp:Label ID="lblPopUpDealer" runat="server" Width="10px"> 
								    <img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0" onclick="ShowDealerSelection();">
                                </asp:Label>
                                <asp:Label ID="lblDealerCode" runat="server" Text="Dealer"></asp:Label>
                                </td>
                        </tr>
                        <tr>
                            <td class="auto-style1" colspan="2">Kode dealer branch&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </td>                            
                            <td colspan="5px" class="auto-style4">
                                <asp:TextBox ID="txtDealerBranchCode" runat="server" Width="90%" onkeypress="return alphaNumericExcept(event,'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz<>?*%${},+=');"
                                    ToolTip="Kode Cabang"></asp:TextBox>
                                <asp:Label ID="lblPopUpDealerBranch" runat="server" Width="10px"> 
								    <img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0" onclick="ShowDealerBranchSelection();">
                                </asp:Label>
                                </td>
                        </tr>
                        <tr>
                            <td class="auto-style1" colspan="2">
                                Batas waktu reminder&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp; </td>
                            <td nowrap class="auto-style4">
                                    <table cellspacing="0" cellpadding="0" border="0">
                                    <tr>
                                        <td><asp:CheckBox ID="chReminderDate" runat="server" /></td>
                                        <td>
                                            <cc1:IntiCalendar ID="icStartReminder" runat="server" TextBoxWidth="70" CanPostBack="False" Friday="True" Monday="True" Saturday="True" ScriptOnFocusOut="" Sunday="True" TargetForm="" TargetTemporaryFocus="" TargetTextBox="" Thursday="True" Tuesday="True" Wednesday="True"></cc1:IntiCalendar></td>
                                        <td>&nbsp;s/d&nbsp;</td>
                                        <td>
                                            <cc1:IntiCalendar ID="icEndReminder" runat="server" TextBoxWidth="70" CanPostBack="False" Friday="True" Monday="True" Saturday="True" ScriptOnFocusOut="" Sunday="True" TargetForm="" TargetTemporaryFocus="" TargetTextBox="" Thursday="True" Tuesday="True" Wednesday="True"></cc1:IntiCalendar></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style1" colspan="2">Nomor Rangka&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                            <td colspan="6" class="auto-style4">
                                <asp:TextBox ID="txtChassisNo" runat="server" Width="150px" onkeypress="return alphaNumericExcept(event,'<>?*%$;{},+=');"
                                    ToolTip="Kode Cabang" AutoPostBack="true"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style1" colspan="2">Nomor Mesin&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </td>                            
                            <td colspan="6" class="auto-style4">
                                <asp:textbox id="txtEngineNo" Width="150px" Runat="server"></asp:textbox>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style1" colspan="2">&nbsp;</td>
                            <td colspan="6" class="auto-style4">
                                <asp:Button ID="btnSearch" runat="server" Text="Cari" />
                                &nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnClear" runat="server" Text="Clear" CausesValidation="False" />
                            &nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnDownload" runat="server" Text="Download" CausesValidation="False" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td class="auto-style3"></td>
                <td valign="top" align="left">
                    <table id="Table_right" cellspacing="1" cellpadding="2" width="100%" border="0">
                       <tr>
                           <td></td>
                       </tr>
                       <tr>
                           <td></td>
                       </tr>
                       <tr>
                            <td class="auto-style1" colspan="2">Nama Konsumen&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                            <td colspan="6" class="auto-style4">
                                <asp:TextBox ID="txtConsumenName" runat="server" Width="150px" onkeypress="return alphaNumericExcept(event,'<>?*%$;{},+=');"
                                    ToolTip="Kode Cabang" AutoPostBack="true"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style1" colspan="2">Status</td>                            
                            <td colspan="6" class="auto-style4" style="display:none">
                                <asp:DropDownList ID="ddlStatus" runat="server">
                                </asp:DropDownList>
                            </td>
                            <td colspan="6" class="auto-style4"><asp:ListBox ID="lboxStatus" runat="server" Rows="3" SelectionMode="Multiple"></asp:ListBox></td>
                        </tr>
                        <tr>
                            <td class="auto-style1" colspan="2">Kategori</td>
                            <td colspan="6" class="auto-style4">
                                <asp:DropDownList ID="ddlCategory" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style1" colspan="2">Jenis Service</td>
                            <td colspan="6" class="auto-style4">
                                <asp:DropDownList ID="ddlJnsService" runat="server"></asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <br />
        <table width="100%">
            <tr>
                <td style="width:70%">
                    <table>
                        <tr><td width: 50px">Total data</td>
                            <td>:
                                <asp:Label ID="lblTotalGridData" runat="server" Text="0"></asp:Label>
                            </td>
                            <td style="width: 30px"></td></tr>
                    </table>
                </td>
                <td style="width:30%">
                    <table>
                        <tr><td style="background-color: yellow; width: 50px">&nbsp;&nbsp;</td>
                            <td>Reminder harus segera di follow up</td>
                            <td style="width: 30px"></td></tr>
                        <tr><td style="background-color: LightSalmon; width: 50px">&nbsp;&nbsp;</td>
                            <td>Reminder di atas batas waktu reminder</td>
                            <td style="width: 30px"></td></tr>
                    </table>
                </td>
            </tr>
        </table>
        <br />
        <asp:DataGrid ID="dgSvcReminder" runat="server" Width="120%" PageSize="25" AllowSorting="True" CellSpacing="1"
            AutoGenerateColumns="False" BorderColor="#CDCDCD" BorderStyle="None" BorderWidth="0px" BackColor="#CDCDCD" CellPadding="3" GridLines="Vertical"
            AllowPaging="True" AllowCustomPaging="True">
            <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
            <AlternatingItemStyle ForeColor="Black" BackColor="#F1F6FB"></AlternatingItemStyle>
            <ItemStyle BackColor="White"></ItemStyle>
            <HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#000084"></HeaderStyle>
            <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
            <Columns>
                <asp:BoundColumn Visible="False" DataField="ID" SortExpression="ID" ReadOnly="True" HeaderText="ID">
                    <HeaderStyle Width="3%" CssClass="titleTableMrk"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                </asp:BoundColumn>
                <asp:TemplateColumn HeaderText="No">
                    <HeaderStyle Width="3%" CssClass="titleTableMrk"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                    <ItemTemplate>
                        <asp:Label ID="lblNo" runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="Nomor Rangka">
                    <HeaderStyle CssClass="titleTableMrk"></HeaderStyle>
                    <ItemStyle HorizontalAlign="left"></ItemStyle>
                    <ItemTemplate>
                        <asp:Label ID="lblChassisNo" runat="server" ></asp:Label>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn SortExpression="ServiceReminderDate" HeaderText="Tanggal Batas Reminder">
                    <HeaderStyle Width="7%" CssClass="titleTableMrk"></HeaderStyle>
                    <ItemStyle HorizontalAlign="left"></ItemStyle>
                    <ItemTemplate>
                        <asp:Label ID="lblReminderDate" runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="Nama Konsumen">
                    <HeaderStyle CssClass="titleTableMrk"></HeaderStyle>
                    <ItemStyle HorizontalAlign="left"></ItemStyle>
                    <ItemTemplate>
                        <asp:Label ID="lblConsumenName" runat="server" ></asp:Label>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="Phone">
                    <HeaderStyle CssClass="titleTableMrk"></HeaderStyle>
                    <ItemStyle HorizontalAlign="left"></ItemStyle>
                    <ItemTemplate>
                        <asp:Label ID="lblPhone" runat="server" ></asp:Label>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="Tipe Kendaraan">
                    <HeaderStyle CssClass="titleTableMrk"></HeaderStyle>
                    <ItemStyle HorizontalAlign="left"></ItemStyle>
                    <ItemTemplate>
                        <asp:Label ID="lblVehicleType" runat="server" ></asp:Label>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <%--<asp:TemplateColumn HeaderText="Kategori">
                    <HeaderStyle CssClass="titleTableMrk"></HeaderStyle>
                    <ItemStyle HorizontalAlign="left"></ItemStyle>
                    <ItemTemplate>
                        <asp:Label ID="lblCategory" runat="server" ></asp:Label>
                    </ItemTemplate>
                </asp:TemplateColumn>--%>
                <asp:TemplateColumn HeaderText="Kind Service(KM)">
                    <HeaderStyle CssClass="titleTableMrk"></HeaderStyle>
                    <ItemStyle HorizontalAlign="left"></ItemStyle>
                    <ItemTemplate>
                        <asp:Label ID="lblKindService" runat="server" ></asp:Label>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="Dealer Code">
                    <HeaderStyle CssClass="titleTableMrk"></HeaderStyle>
                    <ItemStyle HorizontalAlign="left"></ItemStyle>
                    <ItemTemplate>
                        <asp:Label ID="lblDealerCode" runat="server" ></asp:Label>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="Dealer Name">
                    <HeaderStyle CssClass="titleTableMrk"></HeaderStyle>
                    <ItemStyle HorizontalAlign="left"></ItemStyle>
                    <ItemTemplate>
                        <asp:Label ID="lblDealerName" runat="server" ></asp:Label>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="Dealer Branch">
                    <HeaderStyle CssClass="titleTableMrk"></HeaderStyle>
                    <ItemStyle HorizontalAlign="left"></ItemStyle>
                    <ItemTemplate>
                        <asp:Label ID="lblDealerBranch" runat="server" ></asp:Label>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn SortExpression="Status" HeaderText="Status">
                    <HeaderStyle CssClass="titleTableMrk"></HeaderStyle>
                    <ItemStyle HorizontalAlign="left"></ItemStyle>
                    <ItemTemplate>
                        <asp:Label ID="lblStatus" runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn SortExpression="LastUpdateTime" HeaderText="Last Follow Up">
                    <HeaderStyle CssClass="titleTableMrk"></HeaderStyle>
                    <ItemStyle HorizontalAlign="left"></ItemStyle>
                    <ItemTemplate>
                        <asp:Label ID="lblLastFollowUp" runat="server" ></asp:Label>
                    </ItemTemplate>
                </asp:TemplateColumn>                
                <asp:TemplateColumn SortExpression="ActualDealerService" HeaderText="Actual Dealer Service">
                    <HeaderStyle CssClass="titleTableMrk"></HeaderStyle>
                    <ItemStyle HorizontalAlign="left"></ItemStyle>
                    <ItemTemplate>
                        <asp:Label ID="lblActDealerService" runat="server" ></asp:Label>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn SortExpression="ActualServiceDate" HeaderText="Actual Service Date">
                    <HeaderStyle CssClass="titleTableMrk"></HeaderStyle>
                    <ItemStyle HorizontalAlign="left"></ItemStyle>
                    <ItemTemplate>
                        <asp:Label ID="lblActServiceDate" runat="server" ></asp:Label>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn SortExpression="ActualKM" HeaderText="Aktual KM">
                    <HeaderStyle CssClass="titleTableMrk"></HeaderStyle>
                    <ItemStyle HorizontalAlign="left"></ItemStyle>
                    <ItemTemplate>
                        <asp:Label ID="lblActualKMGrd" runat="server" ></asp:Label>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn>
                    <HeaderStyle Width="5%" CssClass="titleTableMrk"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Wrap="False"></ItemStyle>
                    <ItemTemplate>
                        <asp:Label ID="lbtnPart" runat="server" Style="display: none">
								<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Add or Remove Part">
                        </asp:Label>
                        <asp:LinkButton ID="lbtnDetail" runat="server" CausesValidation="False" CommandName="View" Text="Ubah" ToolTip="Detail">
								<img src="../images/detail.gif" border="0" alt="Detail"></asp:LinkButton>
                        <asp:LinkButton ID="lbtnEdit" runat="server" Width="20px" Text="Ubah" CausesValidation="False" CommandName="Edit">
								<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
                    </ItemTemplate>
                    <FooterStyle HorizontalAlign="Center"></FooterStyle>
                </asp:TemplateColumn>
            </Columns>
            <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
        </asp:DataGrid>
    </form>
</body>
</html>
