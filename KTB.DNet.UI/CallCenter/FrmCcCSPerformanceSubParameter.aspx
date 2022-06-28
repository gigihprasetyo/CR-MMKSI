<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmCcCSPerformanceSubParameter.aspx.vb" Inherits=".FrmCcCSPerformanceSubParameter" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <head>
        <title>Master Assessment Parameter</title>
        <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
        <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
        <meta content="JavaScript" name="vs_defaultClientScript">
        <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
        <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
        <style type="text/css">
            .hidden {
                display: none;
            }

            .rightAlignment {
                text-align: right;
            }
        </style>

        <script type="text/javascript" src="../WebResources/PreventNewWindow.js"></script>
        <script type="text/javascript" src="../WebResources/InputValidation.js"></script>
        <script type="text/javascript" src="../WebResources/jquery-1.8.3.min.js"></script>
        <script type="text/javascript">
            $("[src*=plus]").live("click", function () {
                $(this).closest("tr").after("<tr><td></td><td colspan = '999'>" + $(this).next().html() + "</td></tr>")
                $(this).attr("src", "../images/minus.gif");
            });
            $("[src*=minus]").live("click", function () {
                $(this).attr("src", "../images/plus.gif");
                $(this).closest("tr").next().remove();
            });

        </script>

        <script type="text/javascript">

            function decimalOnly(e) { //example onkeypress="return decimalOnly(this,event);"
                if (window.event) // IE
                {
                    if ((e.keyCode < 48 || e.keyCode > 57) & e.keyCode != 8 && e.keyCode != 44) {
                        event.returnValue = false;
                        return false;
                    }
                }
                else { // Fire Fox
                    if ((e.which < 48 || e.which > 57) & e.which != 8 && e.which != 44) {
                        e.preventDefault();
                        return false;
                    }
                }
            }

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
                var btnTriggerAttribute = document.getElementById("btnTriggerAttribute");
                var hdnAttributeID = document.getElementById("hdnAttributeID");
                hdnAttributeID.value = SelectedAttribute;
                btnTriggerAttribute.click();
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
                            <td class="titleField">Nama Sub Parameter</td>
                            <td width="1%">:</td>
                            <td>
                                <asp:TextBox runat="server" ID="txtParameterName" Width="200px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4"
                                    EnableClientScript="false" runat="server" ErrorMessage="*"
                                    ControlToValidate="txtParameterName"></asp:RequiredFieldValidator>
                            </td>
                        </tr>

                        <tr valign="top">
                            <td class="titleField">Prosedur Sub Parameter</td>
                            <td width="1%">:</td>
                            <td>
                                <asp:TextBox ID="txtProcedure" runat="server"></asp:TextBox>
                            </td>
                        </tr>

                        <tr valign="top">
                            <td class="titleField">Nomor Urut</td>
                            <td width="1%">:</td>
                            <td>
                                <asp:TextBox onkeypress="return numericOnlyWithComa(event)" runat="server" ID="txtSequence" Width="200px">
                                </asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                                    EnableClientScript="false" runat="server" ErrorMessage="*"
                                    ControlToValidate="txtSequence"></asp:RequiredFieldValidator>
                            </td>
                        </tr>

                        <tr valign="top">
                            <td class="titleField">Tipe</td>
                            <td width="1%">:</td>
                            <td>
                                <asp:DropDownList runat="server" ID="ddlTipe" Width="200px">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3"
                                    EnableClientScript="false" runat="server" ErrorMessage="*"
                                    ControlToValidate="ddlTipe"></asp:RequiredFieldValidator>
                            </td>
                        </tr>

                        <tr valign="top">
                            <td class="titleField">Bobot</td>
                            <td width="1%">:</td>
                            <td>
                                <asp:TextBox CssClass="rightAlignment" onkeypress="return decimalOnly(event);" runat="server" ID="txtParameterWeight" Width="90px"> </asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                                    EnableClientScript="false" runat="server" ErrorMessage="*"
                                    ControlToValidate="txtParameterWeight"></asp:RequiredFieldValidator>
                                <asp:Label ID="Label1" runat="server" Text="%"></asp:Label>
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
                            <td></td>
                            <td></td>
                            <td>
                                <asp:GridView ID="gvAttribute" runat="server" AutoGenerateColumns="false" AllowSorting="false" AllowPaging="false" Width="500px">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Atribut" HeaderStyle-Width="40%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblID" runat="server" Visible="false"></asp:Label>
                                                <asp:Label ID="lblCcAttributeID" runat="server" Visible="false"></asp:Label>
                                                <asp:Label ID="lblDescription" runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Nilai Minimum" HeaderStyle-Width="10%">
                                            <ItemTemplate>
                                                <asp:TextBox CssClass="rightAlignment" ID="txtMinimumScore" runat="server" Text="0" onkeypress="return decimalOnly(event);"></asp:TextBox>
                                            </ItemTemplate>


                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Masa Berlaku" HeaderStyle-Width="40%">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlCcPeriodFrom" runat="server"></asp:DropDownList>
                                                &nbsp;s/d&nbsp;
                                                  <asp:DropDownList ID="ddlCcPeriodTo" runat="server"></asp:DropDownList>
                                            </ItemTemplate>


                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-Width="10%">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtnDelete" runat="server" Width="20px" Text="Hapus" CausesValidation="False" CommandName="Delete" OnClientClick="Confirm();">
											    <img src="../images/trash.gif" border="0" alt="Hapus" ></asp:LinkButton>&nbsp;
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>

                                </asp:GridView>
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
                                <asp:Button Text="Cari" ID="btnCari" runat="server" Width="75px"  CausesValidation="False" />
                                &nbsp;
                                <asp:Button Text="Baru" ID="btnBaru" runat="server" Width="75px" Visible="false" CausesValidation="False" />
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

                                <asp:TemplateColumn HeaderText="Atribut">
                                    <HeaderStyle Width="5%" CssClass="titleTableGeneral" HorizontalAlign="Center"></HeaderStyle>
                                    <ItemTemplate>
                                        <img alt="" style="cursor: pointer" id="imgPlus" src="../images/plus.gif" />
                                        <%--  <asp:Label ID="lblNo" runat="server">
                                        </asp:Label>--%>
                                        <asp:Panel ID="pnlDetail" runat="server" Style="display: none">
                                            <asp:DataGrid ID="dtgAttribute"
                                                Width="100%" runat="server" AutoGenerateColumns="false" GridLines="Horizontal" CellPadding="3" BackColor="Gainsboro" BorderWidth="0px" BorderStyle="None" BorderColor="#CDCDCD"
                                                AllowCustomPaging="false" AllowSorting="false" AllowPaging="false"  ForeColor="GhostWhite" CellSpacing="1" Font-Names="MS Reference Sans Serif">
                                                <Columns>

                                                    <asp:TemplateColumn HeaderText="Faktor" SortExpression="">
                                                        <HeaderStyle Width="20%" CssClass="titleTableService"></HeaderStyle>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblFaktor" runat="server">
                                                            </asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="Deskripsi" SortExpression="">
                                                        <HeaderStyle Width="30%" CssClass="titleTableService"></HeaderStyle>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDeskripsi" runat="server">
                                                            </asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="False" HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="Tipe Kendaraan" SortExpression="">
                                                        <HeaderStyle Width="20%" CssClass="titleTableService"></HeaderStyle>
                                                        <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTipeKendaraan" runat="server">
                                                            </asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                      <asp:TemplateColumn HeaderText="Nilai Minimum" SortExpression="">
                                                        <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                                        <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblMinimumScore" runat="server">
                                                            </asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>

                                                    <asp:TemplateColumn HeaderText="Masa Berlaku" SortExpression="">
                                                        <HeaderStyle Width="20%" CssClass="titleTableService"></HeaderStyle>
                                                        <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPeriodFrom" runat="server">
                                                            </asp:Label>
                                                            s/d
                                                             <asp:Label ID="lblPeriodTo" runat="server">
                                                            </asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>

                                                </Columns>
                                            </asp:DataGrid>
                                        </asp:Panel>
                                    </ItemTemplate>
                                </asp:TemplateColumn>

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
                                        <%--   <asp:LinkButton ID="lbtnGoToAttribute" runat="server" Width="20px" ToolTip="Set Attribute" CausesValidation="False" CommandName="GoToAttribute">
												<img src="../images/set.gif" border="0" alt="Assessment Field"></asp:LinkButton>&nbsp;--%>
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
