<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmEntryPDISpecimen.aspx.vb" Inherits=".FrmEntryPDISpecimen" %>

<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<html>
<head>
    <title>PDI Master Specimen & Signature</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" type="text/javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" type="text/javascript" src="../WebResources/InputValidation.js"></script>
    <script language="javascript" type="text/javascript" src="../WebResources/jquery-1.8.3.min.js"></script>
    <script language="javascript" type="text/javascript">
        function ShowPPDealerSelection() {
            showPopUp('../PopUp/PopUpDealerSelectionOne.aspx', '', 500, 760, DealerSelection);
        }

        function DealerSelection(selectedDealer) {
            var data = selectedDealer.split(";");
            var txtbox = document.getElementById("txtDealerCode");
            txtbox.value = data[0];
        }

        function ClosePreview() {
            $("#PreviewImage").hide( "slow" );
        }
        function ShowPreview() {
            $("#PreviewImage").show( "slow" );
        }

    </script>
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage">PRE DELIVERY INSPECTION - PDI Master Specimen & Signature</td>
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
                <td align="left" valign="top">
                    <table cellspacing="3" cellpadding="3" width="773" border="0" style="width: 773px; height: 64px">
                        <tr>
                            <td class="titleField">Kode Dealer</td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="txtDealerCode" onkeypress="return alphaNumericExcept(event,'<>?*%$')" onblur="omitSomeCharacter('txtDealerCode','<>?*%$')"
                                    runat="server" Width="120"></asp:TextBox>
                                <asp:Label ID="lblSearchDealer" runat="server" onclick="ShowPPDealerSelection();">
									<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField">Berlaku Mulai</td>
                            <td>:</td>
                            <td>
                                <cc1:IntiCalendar ID="ccTglBerlaku" runat="server" Value=""></cc1:IntiCalendar>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField">Upload Template</td>
                            <td>:</td>
                            <td>
                                <asp:FileUpload ID="fuUpload" runat="server" Width="300px" />
                                <asp:Button ID="btnUpload" runat="server" Text="Upload" Width="60px" OnClick="btnUpload_Click" />
                                <input type="button" value="Preview" onclick="ShowPreview();" />
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField">Nama</td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="txtNama" runat="server" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField">Posisi</td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="txtPos" runat="server" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField">Blok</td>
                            <td>:</td>
                            <td>
                                <asp:DropDownList ID="ddlBlok" runat="server">
                                    <asp:ListItem Text="Silahkan Pilih" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="Service 1" Value="Service 1"></asp:ListItem>
                                    <asp:ListItem Text="Service 2" Value="Service 2"></asp:ListItem>
                                    <asp:ListItem Text="Sales 1" Value="Sales 1"></asp:ListItem>
                                    <asp:ListItem Text="Sales 2" Value="Sales 2"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField">Status</td>
                            <td>:</td>
                            <td>
                                <asp:DropDownList ID="ddlStatus" runat="server">
                                    <asp:ListItem Text="Silahkan Pilih" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="Non Aktif" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Aktif" Value="1"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2"></td>
                            <td>
                                <asp:Button ID="btnSimpan" runat="server" Text="Simpan" Width="60px" OnClick="btnSimpan_Click" />
                                <asp:Button ID="btnBatal" runat="server" Text="Batal" Width="60px" OnClick="btnBatal_Click"/>
                                <asp:Button ID="btnCari" runat="server" Text="Cari" Width="60px" OnClick="btnCari_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="3">&nbsp;</td>
            </tr>
            <tr>
                <td>
                    <div>
                        <asp:DataGrid ID="dgPDISpecimen" runat="server" Width="100%" AllowSorting="True" AllowCustomPaging="True" AutoGenerateColumns="False"
                            BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="0px" BackColor="#CDCDCD" CellPadding="3"
                            CellSpacing="1" GridLines="Vertical" AllowPaging="True" PageSize="10" 
                            OnItemCommand="dgPDISpecimen_ItemCommand"
                            OnPageIndexChanged="dgPDISpecimen_PageIndexChanged"
                            OnSortCommand="dgPDISpecimen_SortCommand"
                            OnItemDataBound="dgPDISpecimen_ItemDataBound">
                            <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                            <AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
                            <ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
                            <HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
                            <Columns>
                                <asp:TemplateColumn HeaderText="No">
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <HeaderStyle CssClass="titleTableService" Width="2%"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblNo" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="Dealer.DealerCode" HeaderText="Dealer">
                                    <HeaderStyle Width="7%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblKodeDealer" runat="server" ToolTip='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerCode")%>' Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerCode")%>'>
                                    </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="Dealer.DealerName" HeaderText="Nama Dealer">
                                    <HeaderStyle Width="13%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblNamaDealer" runat="server" ToolTip='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerName")%>' Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerName")%>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="Name" HeaderText="Nama">
                                    <HeaderStyle Width="13%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblNama" runat="server" ToolTip='<%# DataBinder.Eval(Container, "DataItem.Name")%>' Text='<%# DataBinder.Eval(Container, "DataItem.Name")%>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="Position" HeaderText="Posisi">
                                    <HeaderStyle Width="13%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblPosition" runat="server" ToolTip='<%# DataBinder.Eval(Container, "DataItem.Position")%>' Text='<%# DataBinder.Eval(Container, "DataItem.Position")%>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="Blok" HeaderText="Blok">
                                    <HeaderStyle Width="13%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblBlok" runat="server" ToolTip='<%# DataBinder.Eval(Container, "DataItem.Blok")%>' Text='<%# DataBinder.Eval(Container, "DataItem.Blok")%>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="ValidFrom" HeaderText="Berlaku Mulai">
                                    <HeaderStyle Width="13%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblDateFrom" runat="server" ToolTip='<%# DataBinder.Eval(Container, "DataItem.ValidFrom")%>' Text='<%# FormatDateTime(DataBinder.Eval(Container, "DataItem.ValidFrom"), Microsoft.VisualBasic.DateFormat.ShortDate) %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="Status" HeaderText="Status">
                                    <HeaderStyle Width="13%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblStatus" runat="server" ToolTip='<%# DataBinder.Eval(Container, "DataItem.StrStatus")%>' Text='<%# DataBinder.Eval(Container, "DataItem.StrStatus")%>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Template">
                                    <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hdFilename" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.FileName") %>' />
                                        <asp:LinkButton ID="lbDownload" runat="server" CommandName="download">
                                                 <img src="../images/download.gif" border="0" alt="Download File">
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                            <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
                        </asp:DataGrid>
                    </div>
                </td>
            </tr>
        </table>
        <div id="PreviewImage" style="position:absolute; top:30px; right:50px; display:none">
            <p><asp:Image ID="imgPreview" runat="server" Width="250px" Height="150px" /></p>
            <p><input type="button" value="Tutup" onclick="ClosePreview();" /></p>
        </div>
    </form>
</body>
</html>
