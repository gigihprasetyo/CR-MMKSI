<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmCcCSPerformanceMaster.aspx.vb" Inherits=".FrmCcCSPerformanceMaster" %>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <head>
        <title>Master Assessment Form</title>
        <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
        <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
        <meta content="JavaScript" name="vs_defaultClientScript">
        <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
        <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
        <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
        <script language="javascript" src="../WebResources/InputValidation.js"></script>
    </head>
</head>
<body ms_positioning="GridLayout">
    <form id="FrmDealAssessmentMaster" runat="server">
        <table id="table1" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage">CS Performance - Form Master</td>
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

                        <tr valign="top">
                            <td class="titleField" style="width: 88px" width="88">Form Reff Code</td>
                            <td width="1%">:</td>
                            <td style="width: 330px" width="262">
                                <asp:DropDownList runat="server" ID="ddlRefFormCode" AutoPostBack="true" Width="200px"></asp:DropDownList>
                            </td>
                        </tr>

                        <tr>
                            <td class="titleField" style="width: 88px" width="88">Form Code</td>
                            <td width="1%">:</td>
                            <td style="width: 262px" width="262">
                                <asp:Label ID="lblCode" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr valign="top">
                            <td class="titleField" style="width: 88px" width="88">Description</td>
                            <td width="1%">:</td>
                            <td style="width: 330px" width="262">
                                <asp:TextBox runat="server" ID="txtDescription" Width="200px" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"
                                    ControlToValidate="txtDescription" EnableClientScript="false"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr valign="top">
                            <td class="titleField" style="width: 88px" width="88">Dari Periode</td>
                            <td width="1%">:</td>
                            <td style="width: 330px" width="262">
                                <asp:DropDownList SortExpression="PeriodIDFrom" ID="ddlPeriodeFrom" runat="server">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" EnableClientScript="false" runat="server" ErrorMessage="*" ControlToValidate="ddlPeriodeFrom"></asp:RequiredFieldValidator>
                            </td>
                        </tr>

                        <tr valign="top">
                            <td class="titleField" style="width: 88px" width="88">Sampai Periode</td>
                            <td width="1%">:</td>
                            <td style="width: 330px" width="262">
                                <asp:DropDownList SortExpression="PeriodIDTo" ID="ddlPeriodeTo" runat="server">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" EnableClientScript="false" runat="server" ErrorMessage="*" ControlToValidate="ddlPeriodeTo"></asp:RequiredFieldValidator>
                            </td>
                        </tr>



                        <%--  <tr valign="top">
                            <td class="titleField" style="width: 88px" width="88">Status</td>
                            <td width="1%">:</td>
                            <td style="width: 330px" width="262">
                                <asp:DropDownList runat="server" ID="ddlstatus" Width="200px">
                                    <asp:ListItem Text="Pilih" Value="Pilih" />
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4"
                                    EnableClientScript="false" runat="server" ErrorMessage="*"
                                    ControlToValidate="ddlStatus"></asp:RequiredFieldValidator>
                            </td>
                        </tr>--%>

                        <tr>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td></td>
                            <td>&nbsp;
                                <asp:Button Text="Simpan" ID="btnSimpan" runat="server" Width="75px" />
                                &nbsp;
                                <asp:Button Text="Batal" ID="btnBaru" runat="server" Width="75px" />
                                &nbsp;
                                <asp:Button Text="Cari" ID="btnCari" runat="server" Width="75px" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td></td>
            </tr>
            <tr>
                <td valign="top">
                    <div id="div1" style="overflow: auto; height: 320px">
                        <asp:DataGrid ID="dtgCSPMaster" runat="server" Width="100%" PageSize="20" CellPadding="3"
                            BackColor="#CDCDCD" AllowCustomPaging="True" AllowPaging="True" BorderWidth="0px" BorderColor="#CDCDCD" AutoGenerateColumns="False" CellSpacing="1"
                            AllowSorting="True" Font-Names="Microsoft Sans Serif">
                            <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                            <AlternatingItemStyle BackColor="White"></AlternatingItemStyle>
                            <ItemStyle ForeColor="Black" BackColor="#FDF1F2"></ItemStyle>
                            <HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="#F7F7F7" BorderColor="#E0E0E0"
                                BackColor="#CC3333"></HeaderStyle>
                            <Columns>
                                <asp:BoundColumn Visible="false" DataField="ID" SortExpression="ID" HeaderText="ID"></asp:BoundColumn>

                                <asp:TemplateColumn HeaderText="No">
                                    <HeaderStyle Width="2%" CssClass="titleTableGeneral"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblNo" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>

                                <asp:BoundColumn DataField="Code" SortExpression="Code" HeaderText="Form Kode">
                                    <HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
                                </asp:BoundColumn>

                                <asp:BoundColumn DataField="Description" SortExpression="Description" HeaderText="Description">
                                    <HeaderStyle Width="20%" CssClass="titleTableGeneral"></HeaderStyle>
                                </asp:BoundColumn>

                                <asp:TemplateColumn HeaderText="Dari Period">
                                    <HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblPeriodFrom" runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateColumn>

                                <asp:TemplateColumn HeaderText="Sampai Period">
                                    <HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblPeriodTo" runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateColumn>

                                <asp:TemplateColumn HeaderText="Status">
                                    <HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblStatus" runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateColumn>

                                <asp:TemplateColumn>
                                    <HeaderStyle Width="15%" CssClass="titleTableGeneral"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnCluster" runat="server" Width="20px" Text="Lihat" CausesValidation="False" CommandName="cluster">
												<img src="../images/icon_general.gif" border="0" alt="Peserta"></asp:LinkButton>&nbsp;
                                        <asp:LinkButton ID="lbtnAssessmentCategory" runat="server" Width="20px" Text="Assessment Category" CausesValidation="False" CommandName="GoToParameter">
												<img src="../images/set.gif" border="0" alt="CS Performance Parameter"></asp:LinkButton>&nbsp;
                                            <asp:LinkButton ID="lbtnView" runat="server" Width="20px" Text="Lihat" CausesValidation="False" CommandName="View">
												<img src="../images/detail.gif" border="0" alt="Lihat"></asp:LinkButton>&nbsp;
											<asp:LinkButton ID="lbtnEdit" runat="server" Text="Ubah" CommandName="Edit" CausesValidation="False">
												<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>&nbsp;
                                        <%--                                            <asp:LinkButton id="lbtnDelete" runat="server" Width="20px" Text="Hapus" CausesValidation="False" CommandName="Delete">
											    <img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>&nbsp;--%>
                                        <%--  <asp:LinkButton ID="linkButonActive" runat="server" Width="16px" CausesValidation="False" CommandName="Activate">
												<img src="../images/aktif.gif" border="0" alt="Klik untuk Aktifkan"></asp:LinkButton>
                                        <asp:LinkButton ID="LinkButtonNonActive" runat="server" Width="16px" CausesValidation="False" CommandName="Deactivate">
												<img src="../images/in-aktif.gif" border="0" alt="Klik untuk Non-Aktifkan"></asp:LinkButton>--%>
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
