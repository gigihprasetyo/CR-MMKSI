<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmQueryReport.aspx.vb" Inherits="FrmQueryReport" SmartNavigation="False" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>FrmCity</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script language="javascript" src="../WebResources/FormFunctions.js"></script>
    <script language="javascript">
        function ShowCriteriaDescription() {
            showPopUp('../PopUp/PopUpCriteriaDescription.aspx', '', 500, 760);
        }
    </script>
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td>
                    <asp:Literal ID="ltrSQl" runat="server" Visible="false"></asp:Literal></td>
            </tr>
            <tr>
                <td class="titlePage">TOOLS - Download</td>
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
                    <table id="Table2" cellspacing="1" cellpadding="1" width="100%" border="0">
                        <tr>
                            <td class="titleField" width="24%" valign="top">Laporan</td>
                            <td width="1%" valign="top">:</td>
                            <td width="75%" valign="top">
                                <asp:DropDownList ID="ddlReport" runat="server" Width="350px" AutoPostBack="True"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td colspan="3">&nbsp;</td>
                        </tr>
                        <asp:Panel ID="pnlInfo" runat="server" Visible="True">
                            <tr>
                                <td class="titleField" valign="top" width="24%">Daftar Kolom</td>
                                <td valign="top" width="1%">:</td>
                                <td valign="top" width="75%">
                                    <div id="div1" style="overflow: auto; height: 200px">
                                        <asp:DataGrid ID="dtgColumns" runat="server" Width="40%" PageSize="100" GridLines="None" CellPadding="3"
                                            BackColor="#CDCDCD" AllowCustomPaging="True" AllowPaging="True" BorderWidth="0px" BorderStyle="None"
                                            BorderColor="#CDCDCD" AutoGenerateColumns="False" CellSpacing="1" AllowSorting="True" OnItemDataBound="dtgColumns_ItemDataBound">
                                            <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9CD"></SelectedItemStyle>
                                            <AlternatingItemStyle ForeColor="Black" BackColor="White"></AlternatingItemStyle>
                                            <ItemStyle ForeColor="Black" VerticalAlign="Top" BackColor="#FDF1F2"></ItemStyle>
                                            <HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#CC3333"></HeaderStyle>
                                            <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
                                            <Columns>
                                                <asp:TemplateColumn>
                                                    <HeaderStyle Width="15%" CssClass="titleTableGeneral"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    <HeaderTemplate>
                                                        <input id="chkAllItems" onclick="CheckAllDataGridCheckBoxes('cbCheck', document.all.chkAllItems.checked)"
                                                            type="checkbox">
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="cbCheck" runat="server"></asp:CheckBox>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn Visible="True" HeaderText="Nama Kolom">
                                                    <HeaderStyle Width="85%" CssClass="titleTableGeneral"></HeaderStyle>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblColumnName" runat="server">
                                                        </asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                            </Columns>
                                            <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
                                        </asp:DataGrid>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">&nbsp;</td>
                            </tr>
                            <tr>
                                <td class="titleField" valign="top" width="24%">Kriteria
										<asp:Label ID="lbtnRefKode" onclick="ShowCriteriaDescription();" runat="server">
											<img src="../images/tanya.gif" style="cursor:hand" border="0" alt="Penjelasan Kriteria">
                                        </asp:Label></td>
                                <td valign="top" width="1%">:</td>
                                <td valign="top" width="75%">
                                    <div id="div2" style="overflow: auto; height: 120px">
                                        <asp:DataGrid ID="dtgCriterias" runat="server" Width="100%" PageSize="25" GridLines="None" CellPadding="3"
                                            BackColor="#CDCDCD" BorderWidth="0px" BorderStyle="None" BorderColor="#CDCDCD" AutoGenerateColumns="False"
                                            CellSpacing="1" ShowFooter="True" OnUpdateCommand="dtgCriterias_Update" OnCancelCommand="dtgCriterias_Cancel"
                                            OnEditCommand="dtgCriterias_Edit" OnItemCommand="dtgCriterias_ItemCommand" OnItemDataBound="dtgCriterias_ItemDataBound">
                                            <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9CD"></SelectedItemStyle>
                                            <AlternatingItemStyle ForeColor="Black" BackColor="White"></AlternatingItemStyle>
                                            <ItemStyle ForeColor="Black" VerticalAlign="Top" BackColor="#FDF1F2"></ItemStyle>
                                            <HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#CC3333"></HeaderStyle>
                                            <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
                                            <Columns>
                                                <asp:TemplateColumn Visible="True" HeaderText="And / Or">
                                                    <HeaderStyle Width="8%" CssClass="titleTableGeneral"></HeaderStyle>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAndOr" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:DropDownList runat="server" ID="ddlAndOrE" Width="100%"></asp:DropDownList>
                                                    </EditItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:DropDownList runat="server" ID="ddlAndOrF" Width="100%"></asp:DropDownList>
                                                    </FooterTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn Visible="True" HeaderText="Nama Kolom">
                                                    <HeaderStyle Width="22%" CssClass="titleTableGeneral"></HeaderStyle>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblColumn" runat="server" Text='<%#DataBinder.Eval(Container,"DataItem.Column")%>'>
                                                        </asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:DropDownList runat="server" ID="ddlColumnE" Width="100%"></asp:DropDownList>
                                                    </EditItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:DropDownList runat="server" ID="ddlColumnF" Width="100%"></asp:DropDownList>
                                                    </FooterTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn Visible="True" HeaderText="Operator">
                                                    <HeaderStyle Width="20%" CssClass="titleTableGeneral"></HeaderStyle>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOperator" runat="server" Text='<%#DataBinder.Eval(Container,"DataItem.Operator")%>'>
                                                        </asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:DropDownList runat="server" ID="ddlOperatorE" Width="100%"></asp:DropDownList>
                                                    </EditItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:DropDownList runat="server" ID="ddlOperatorF" Width="100%"></asp:DropDownList>
                                                    </FooterTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn Visible="True" HeaderText="Nilai">
                                                    <HeaderStyle Width="35%" CssClass="titleTableGeneral"></HeaderStyle>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblValue" runat="server" Text='<%#DataBinder.Eval(Container,"DataItem.Value")%>'>
                                                        </asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtValueE" runat="server" Width="100%"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <FooterStyle></FooterStyle>
                                                    <FooterTemplate>
                                                        <asp:TextBox ID="txtValueF" runat="server" Width="100%"></asp:TextBox>
                                                    </FooterTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn>
                                                    <HeaderStyle Width="5%" CssClass="titleTableGeneral"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="40px"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbtnDelete" runat="server" CommandName="Delete">
																<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn>
                                                    <HeaderStyle Width="5%" CssClass="titleTableGeneral"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="40px"></ItemStyle>
                                                    <FooterStyle HorizontalAlign="Center"></FooterStyle>
                                                    <FooterTemplate>
                                                        <asp:LinkButton ID="lbtnAdd" runat="server" CommandName="Add">
																<img src="../images/add.gif" border="0" alt="Tambah"></asp:LinkButton>
                                                    </FooterTemplate>
                                                </asp:TemplateColumn>
                                            </Columns>
                                        </asp:DataGrid>
                                    </div>
                                </td>
                            </tr>
                        </asp:Panel>
                        <tr>
                            <td colspan="3">&nbsp;</td>
                        </tr>
                        <tr>
                            <td></td>
                            <td></td>
                            <td>
                                <asp:Button ID="btnDownload" runat="server" Width="60px" Text="Download"></asp:Button></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
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
    <!-- 
<asp:EditCommandColumn ButtonType="LinkButton" UpdateText="&lt;img src=&quot;../images/simpan.gif&quot; border=&quot;0&quot; alt=&quot;Simpan&quot;&gt;"
														CancelText="&lt;img src=&quot;../images/batal.gif&quot; border=&quot;0&quot; &quot; alt=&quot;Batal&quot;&gt;"
														EditText="&lt;img src=&quot;../images/edit.gif&quot; border=&quot;0&quot; alt=&quot;Ubah&quot;&gt;">
														<HeaderStyle Width="4%" CssClass="titleTableGeneral"></HeaderStyle>
														<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
													</asp:EditCommandColumn>
-->
</body>
</html>
