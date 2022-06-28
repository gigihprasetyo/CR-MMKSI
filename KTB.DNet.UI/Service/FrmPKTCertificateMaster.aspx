<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmPKTCertificateMaster.aspx.vb" Inherits=".FrmPKTCertificateMaster" %>

<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<html>
<head>
    <title>PKT Sertifikat Master</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" type="text/javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" type="text/javascript" src="../WebResources/InputValidation.js"></script>
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage">Umum - Template Sertifikat PKT</td>
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
                            <td class="titleField">Model</td>
                            <td>:</td>
                            <td>
                                <asp:DropDownList ID="ddlKategori" runat="server" OnSelectedIndexChanged="ddlKategori_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                <asp:DropDownList ID="ddlModel" runat="server"></asp:DropDownList>
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
                                <asp:Button ID="btnPreview" runat="server" Text="Preview" Width="60px" OnClick="btnPreview_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2"></td>
                            <td>
                                <b><label class="copyRight">Template yang diupload harus berformat .docx</label></b>
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
                                <asp:Button ID="btnDownloadTemplate" runat="server" Text="Download Sample Template" Width="170px" OnClick="btnDownloadTemplate_Click" />
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
                        <asp:DataGrid ID="dgPKTCertMaster" runat="server" Width="100%" AllowSorting="True" AllowCustomPaging="True" AutoGenerateColumns="False"
                            BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="0px" BackColor="#CDCDCD" CellPadding="3"
                            CellSpacing="1" GridLines="Vertical" AllowPaging="True" PageSize="10" 
                            OnItemCommand="dgPKTCertMaster_ItemCommand"
                            OnPageIndexChanged="dgPKTCertMaster_PageIndexChanged"
                            OnSortCommand="dgPKTCertMaster_SortCommand"
                            OnItemDataBound="dgPKTCertMaster_ItemDataBound">
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
                                <asp:TemplateColumn HeaderText="Model">
                                    <HeaderStyle Width="7%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblVechileModel" runat="server" ToolTip='<%# DataBinder.Eval(Container, "DataItem.VechileModel.IndDescriptionVehicleModelCode")%>' Text='<%# DataBinder.Eval(Container, "DataItem.VechileModel.IndDescriptionVehicleModelCode")%>'>
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
    </form>
</body>
</html>
