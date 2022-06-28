<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmCcCSPerformanceParameter.aspx.vb" Inherits=".FrmCcCSPerformanceParameter" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <head>
        <title>Master Assessment Parameter</title>
        <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
        <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
        <meta content="JavaScript" name="vs_defaultClientScript">
        <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
        <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
        <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
        <script language="javascript" src="../WebResources/InputValidation.js"></script>
        <script>

            function Confirm() {
                var confirm_value = document.createElement("INPUT");
                confirm_value.type = "hidden";
                confirm_value.name = "confirm_value";
                if (confirm("Apakah Anda Yakin Menghapus Data Ini?")) {
                    confirm_value.value = "Yes";
                } else {
                    confirm_value.value = "No";
                }
                document.forms[0].appendChild(confirm_value);
            }

        </script>
    </head>
    <style type="text/css">
        .auto-style1 {
            font-family: Sans-Serif, Arial;
            font-size: 11px;
            color: #000000;
            margin: 0px;
            font-weight: bold;
            text-align: left;
            height: 24px;
        }

        .auto-style2 {
            height: 24px;
        }
    </style>
</head>
<body ms_positioning="GridLayout">
    <form id="FrmDealAssessmentParameterField" runat="server">
        <table id="table1" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage">CS Performance - Parameter</td>
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
                            <td class="titleField">Nama Master</td>
                            <td width="1%">:</td>
                            <td>
                                <asp:Label ID="lblKodeMaster" runat="server"></asp:Label>
                            </td>
                        </tr>

                        <tr valign="top">
                            <td class="titleField">Kode Parameter</td>
                            <td width="1%">:</td>
                            <td>
                                <asp:Label ID="lblKodeParameter" runat="server"></asp:Label>
                            </td>
                        </tr>

                        <tr valign="top">
                            <td class="titleField">Nama Parameter</td>
                            <td width="1%">:</td>
                            <td>
                                <asp:TextBox runat="server" ID="txtParameterName" Width="200px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                                    EnableClientScript="false" runat="server" ErrorMessage="*"
                                    ControlToValidate="txtParameterName"></asp:RequiredFieldValidator>
                            </td>
                        </tr>

                        <tr valign="top">
                            <td class="titleField">Bobot</td>
                            <td width="1%">:</td>
                            <td>
                                <asp:TextBox onkeypress="return numericOnlyUniv(event)" runat="server" ID="txtParameterWeight" Width="90px">
                                </asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                                    EnableClientScript="false" runat="server" ErrorMessage="*"
                                    ControlToValidate="txtParameterWeight"></asp:RequiredFieldValidator>
                                <asp:Label ID="Label1" runat="server" Text="%"></asp:Label>
                            </td>
                        </tr>

                        <tr valign="top">
                            <td class="titleField">Nomor Urut</td>
                            <td width="1%">:</td>
                            <td>

                                <asp:TextBox onkeypress="return numericOnlyUniv(event)" runat="server" ID="txtSequence" Width="65px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3"
                                    EnableClientScript="false" runat="server" ErrorMessage="*"
                                    ControlToValidate="txtSequence"></asp:RequiredFieldValidator>
                            </td>
                        </tr>

                        <tr valign="top">
                            <td class="titleField">Tipe Customer</td>
                            <td width="1%">:</td>
                            <td>

                                <asp:DropDownList runat="server" ID="ddlCustomerCategory" Width="200px">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5"
                                    EnableClientScript="false" runat="server" ErrorMessage="*"
                                    ControlToValidate="ddlCustomerCategory"></asp:RequiredFieldValidator>
                            </td>
                        </tr>

                        <tr valign="top">
                            <td class="titleField">Status</td>
                            <td width="1%">:</td>
                            <td>

                                <asp:DropDownList runat="server" ID="ddlStatus" Width="200px">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6"
                                    EnableClientScript="false" runat="server" ErrorMessage="*"
                                    ControlToValidate="ddlStatus"></asp:RequiredFieldValidator>
                            </td>
                        </tr>


                        <%--  <tr valign="top">
                            <td class="auto-style1">Refrensi</td>
                            <td width="1%" class="auto-style2">:</td>
                            <td class="auto-style2">
                                <asp:DropDownList runat="server" ID="ddlRefrensi" Width="200px" >
                                </asp:DropDownList>
                                <asp:requiredfieldvalidator id="RequiredFieldValidator4" 
                                    EnableClientScript="false" runat="server" ErrorMessage="*" 
                                    ControlToValidate="ddlRefrensi"></asp:requiredfieldvalidator>
                            </td>
                        </tr>

                        <tr valign="top">
                            <td class="titleField" ></td>
                            <td width="1%"></td>
                            <td style="width: 330px" width="262">
                                <asp:DropDownList AutoPostBack="true" runat="server" ID="ddlReferral" Width="200px" >
                                    <asp:ListItem Text="Pilih" Value="0" />
                                </asp:DropDownList>
                            </td>
                        </tr>--%>

                        <tr>
                            <td class="titleField"></td>
                            <td width="1%"></td>
                            <td>
                                <asp:Label ID="lblErrMsg" runat="server" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>

                        <tr>
                            <td></td>
                            <td></td>
                            <td>
                                <asp:Button Text="Simpan" ID="btnSimpan" runat="server" Width="75px" />
                                &nbsp;
                                <asp:Button Text="Cari" ID="btnCari" runat="server" Width="75px" CausesValidation="false" />
                                &nbsp;
                                <asp:Button Text="Baru" ID="btnBaru" runat="server" Width="75px" Visible="false" CausesValidation="false" />
                                &nbsp;&nbsp;&nbsp;
                                <asp:Button Text="Kembali" ID="btnKembali" runat="server" Width="75px" />
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
                        <asp:DataGrid ID="dtgCSPParameter" runat="server" Width="100%" PageSize="50" CellPadding="3"
                            BackColor="#CDCDCD" AllowCustomPaging="True" AllowPaging="True" BorderWidth="0px" BorderColor="#CDCDCD" AutoGenerateColumns="False" CellSpacing="1"
                            AllowSorting="false" Font-Names="Microsoft Sans Serif">
                            <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                            <AlternatingItemStyle BackColor="White"></AlternatingItemStyle>
                            <ItemStyle ForeColor="Black" BackColor="#FDF1F2"></ItemStyle>
                            <HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="#F7F7F7" BorderColor="#E0E0E0"
                                BackColor="#CC3333"></HeaderStyle>
                            <Columns>
                                <asp:BoundColumn Visible="False" DataField="ID" SortExpression="ID" HeaderText="ID"></asp:BoundColumn>

                                <asp:TemplateColumn HeaderText="No">
                                    <HeaderStyle Width="2%" CssClass="titleTableGeneral"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblNo" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>

                                <asp:TemplateColumn HeaderText="Name Master">
                                    <HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblKodeMaster" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>

                                <asp:BoundColumn DataField="Code" SortExpression="Code" HeaderText="kode Parameter">
                                    <HeaderStyle Width="20%" CssClass="titleTableGeneral"></HeaderStyle>
                                </asp:BoundColumn>

                                <asp:BoundColumn DataField="Name" SortExpression="Name" HeaderText="Nama">
                                    <HeaderStyle Width="20%" CssClass="titleTableGeneral"></HeaderStyle>
                                </asp:BoundColumn>

                                <asp:BoundColumn DataField="Sequence" SortExpression="Sequence" HeaderText="No.Urut">
                                    <HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
                                </asp:BoundColumn>

                                <asp:BoundColumn DataField="Weight" SortExpression="Weight" HeaderText="Bobot">
                                    <HeaderStyle Width="5%" CssClass="titleTableGeneral"></HeaderStyle>
                                </asp:BoundColumn>

                                <asp:TemplateColumn HeaderText="Tipe Customer">
                                    <HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblGvCustomerCategory" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>

                                <asp:TemplateColumn HeaderText="Status">
                                    <HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblGvStatus" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>

                              <%--  <asp:TemplateColumn HeaderText="Referensi">
                                    <HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblReferensi" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>--%>
                                <%-- <asp:BoundColumn DataField="Level" SortExpression="Level" HeaderText="Level">
										<HeaderStyle Width="5%" CssClass="titleTableGeneral"></HeaderStyle>
									</asp:BoundColumn>--%>

                                <asp:TemplateColumn>
                                    <HeaderStyle Width="15%" CssClass="titleTableGeneral"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnSubParameter" runat="server" Width="20px" Text="Sub Parameter" CausesValidation="False" CommandName="GotoSubParameter">
												<img src="../images/set.gif" border="0" alt="Sub Parameter"></asp:LinkButton>&nbsp;
                                            <asp:LinkButton ID="lbtnView" runat="server" Width="20px" Text="Lihat" CausesValidation="False" CommandName="View">
												<img src="../images/detail.gif" border="0" alt="Lihat"></asp:LinkButton>&nbsp;
											<asp:LinkButton ID="lbtnEdit" runat="server" Text="Ubah" CommandName="Edit" CausesValidation="False">
												<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>&nbsp;
                                            <asp:LinkButton ID="lbtnDelete" runat="server" Width="20px" Text="Hapus" CausesValidation="False" CommandName="Delete" OnClientClick="Confirm();">
											    <img src="../images/trash.gif" border="0" alt="Hapus" ></asp:LinkButton>&nbsp;
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
