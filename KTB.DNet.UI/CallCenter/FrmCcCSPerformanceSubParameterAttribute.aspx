<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmCcCSPerformanceSubParameterAttribute.aspx.vb" Inherits=".FrmCcCSPerformanceSubParameterAttribute" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <head>
        <title>Master Assessment Parameter</title>
        <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
        <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
        <meta content="JavaScript" name="vs_defaultClientScript">
        <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
        <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">

        <style>
            .hidden {
                display: none;
            }
        </style>

        <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
        <script language="javascript" src="../WebResources/InputValidation.js"></script>

        <script type="text/javascript">

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

            function ShowAttribute(customerCategoryID) {
                showPopUp('../PopUp/PopUpCcAttribute.aspx?CustomerCategoryID=' + customerCategoryID, '', 470, 600, AttributeSelection);
            }

            function AttributeSelection(SelectedAttribute) {
                alert(SelectedAttribute);
                var tempParam = SelectedAttribute.split(';');
                var txtAttribute = document.getElementById("txtAttribute");
                var hdnAttributeID = document.getElementById("hdnAttributeID");
                hdnAttributeID.value = tempParam[0]
                txtAttribute.value = tempParam[2];
            }

        </script>
    </head>
</head>
<body ms_positioning="GridLayout">
    <form id="FrmDealAssessmentParameterField" runat="server">
        <table id="table1" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage">CS Performance - Sub Parameter</td>
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
                            <td class="titleField">Nama Parameter</td>
                            <td width="1%">:</td>
                            <td>
                                <asp:Label ID="lblKodeParameter" runat="server"></asp:Label>
                            </td>
                        </tr>

                        <tr valign="top">
                            <td class="titleField">Kode Sub Parameter</td>
                            <td width="1%">:</td>
                            <td>
                                <asp:Label ID="lblKodeSubParameter" runat="server"></asp:Label>
                            </td>
                        </tr>

                        <tr valign="top">
                            <td class="titleField">Attribute</td>
                            <td width="1%">:</td>
                            <td>
                                <asp:Label ID="lblShowAttribute" runat="server" Width="16px">
										<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0">
                                </asp:Label>
                                <asp:HiddenField ID="hdnAttributeID" runat="server" />
                                <asp:Button ID="btnTriggerAttribute" runat="server" CssClass="hidden" />
                            </td>
                        </tr>
                        <tr>
                            <td></td><td></td>
                            <td>
                                <asp:GridView ID="gvAttribute" runat="server" AutoGenerateColumns="false" AllowSorting="false" AllowPaging="false" Width="300px">
                                    <Columns>
                                        <asp:BoundField DataField="ID" Visible="false" />
                                        <asp:BoundField DataField="Description" HeaderText="Deskripsi" />
                                    </Columns>

                                </asp:GridView>
                            </td>
                        </tr>

                        <tr valign="top">
                            <td class="titleField">Nomor Urut</td>
                            <td width="1%">:</td>
                            <td>
                                <asp:TextBox onkeypress="return numericOnlyUniv(event)" runat="server" ID="txtSequence" Width="200px">
                                </asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                                    EnableClientScript="false" runat="server" ErrorMessage="*"
                                    ControlToValidate="txtSequence"></asp:RequiredFieldValidator>
                            </td>
                        </tr>


                        <%--<tr vAlign="top">
                                <TD class="titleField" style="WIDTH: 88px" width="88">Level / Layer</TD>
								<TD width="1%">:</TD>
								<td >
                                    <asp:DropDownList runat="server" ID="ddlLevelLayer" Width="200px" AutoPostBack="true" OnSelectedIndexChanged="ddlLevelLayer_SelectedIndexChanged">
                                        <asp:ListItem Text="0" Value="0" />
                                        <asp:ListItem Text="1" Value="1" />
                                    </asp:DropDownList>
								</td>
                            </tr>

                                <TR>
								<TD class="titleField" >Kode Master</TD>
								<TD width="1%">:</TD>
								<TD >
                                    <asp:DropDownList runat="server" ID="ddlKodeMaster" Width="200px" AutoPostBack="true"  OnSelectedIndexChanged="txtFormCode_SelectedIndexChanged" >
                                        <asp:ListItem Text="Pilih" Value="Pilih" />
                                    </asp:DropDownList>
								</TD>
							</TR>--%>
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
                                <asp:Button Text="Cari" ID="btnCari" runat="server" Width="75px" CausesValidation="False" />
                                &nbsp;
                                <asp:Button Text="Baru" ID="btnBaru" runat="server" Width="75px" CausesValidation="False" />
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
                            AllowSorting="True" Font-Names="Microsoft Sans Serif">
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

                                <asp:TemplateColumn HeaderText="Nama Parameter">
                                    <HeaderStyle Width="5%" CssClass="titleTableGeneral"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblKodeParameter" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>

                                <asp:BoundColumn DataField="Code" SortExpression="Code" HeaderText="kode SubParameter">
                                    <HeaderStyle Width="5%" CssClass="titleTableGeneral"></HeaderStyle>
                                </asp:BoundColumn>

                                <asp:BoundColumn DataField="Name" SortExpression="Name" HeaderText="Nama">
                                    <HeaderStyle Width="15%" CssClass="titleTableGeneral"></HeaderStyle>
                                </asp:BoundColumn>

                                <asp:TemplateColumn HeaderText="Prosedur">
                                    <HeaderStyle Width="15%" CssClass="titleTableGeneral"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblprosedur" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>

                                <asp:BoundColumn DataField="Sequence" SortExpression="Sequence" HeaderText="No.Urut">
                                    <HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
                                </asp:BoundColumn>

                                <asp:TemplateColumn SortExpression="Type" HeaderText="Tipe">
                                    <HeaderStyle Width="5%" CssClass="titleTableGeneral"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblTipe" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>

                                <asp:BoundColumn DataField="Weight" SortExpression="Weight" HeaderText="Bobot">
                                    <HeaderStyle Width="5%" CssClass="titleTableGeneral"></HeaderStyle>
                                </asp:BoundColumn>

                                <%-- <asp:BoundColumn DataField="Level" SortExpression="Level" HeaderText="Level">
										<HeaderStyle Width="5%" CssClass="titleTableGeneral"></HeaderStyle>
									</asp:BoundColumn>--%>

                                <asp:TemplateColumn>
                                    <HeaderStyle Width="15%" CssClass="titleTableGeneral"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
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
