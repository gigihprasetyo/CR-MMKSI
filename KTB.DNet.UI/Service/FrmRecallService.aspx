<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>

<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmRecallService.aspx.vb" Inherits=".FrmRecallService" SmartNavigation="False" %>


<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>REcall Service</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <script type="text/javascript" language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script type="text/javascript" language="javascript" src="../WebResources/InputValidation.js"></script>
    <link rel="stylesheet" type="text/css" href="../WebResources/stylesheet.css">
    <script type="text/javascript" language="javascript">
        function RebindRecallCategory(txt) {
            var indek = GetIndex(txt);
            //console.log('ahhh');

            //var noRangka = dgRecallMaster.rows[indek].getElementsByTagName("INPUT")[0];
            var dgRecallMaster = document.getElementById("dgRecallMaster");
            var btnRangka = document.getElementById("dgRecallMaster__ctl" + (indek + 1) + "_btnBindRegCategori");
            //console.log(btnRangka);
            if (btnRangka) btnRangka.click();
        }

        function GetIndex(CtlID) {
            if (!navigator.appName == "Microsoft Internet Explorer") {
                var row = CtlID.parentElement.parentElement;
                indexRow = row.rowIndex;
                return row.rowIndex;
            }
            else {
                var row = CtlID.parentNode.parentNode;
                indexRow = row.rowIndex;
                return row.rowIndex;
            }
        }

        function RebindRegNo(txt) {
            var indek = GetIndex(txt);
        //    console.log('ahhh');

            //var noRangka = dgRecallMaster.rows[indek].getElementsByTagName("INPUT")[0];
            var dgRecallMaster = document.getElementById("dgRecallMaster");
            var btnRangka = document.getElementById("dgRecallMaster__ctl" + (indek + 1) + "_btnRebindRegNo");
          //  console.log(btnRangka);
            if (btnRangka) btnRangka.click();
        }

    </script>
</head>
<body ms_positioning="GridLayout">
    <form id="form1" runat="server" ms_positioning="GridLayout">
        <div>
            <table id="Table1" border="0" cellspacing="0" cellpadding="0" width="100%">
                <tr>
                    <td class="titlePage">Field Fix Campaign - Input Data Campaign</td>
                </tr>
                <tr>
                    <td height="1" background="../images/bg_hor.gif">
                        <img border="0" src="../images/bg_hor.gif" height="1"></td>
                </tr>
                <tr>
                    <td height="10">
                        <img border="0" src="../images/dot.gif" height="1"></td>
                </tr>
                <tr>
                    <td valign="top" align="left">
                        <table id="Table2" border="0" cellspacing="2" cellpadding="1" width="100%">
                            <tr>
                                <td class="titleField">Kode Dealer</td>
                                <td width="1%">:</td>
                                <td>
                                    <asp:Label ID="lblDealerCOde" runat="server"></asp:Label></td>
                                <td></td>
                                <td></td>
                                <td width="25%"></td>
                            </tr>

                            <tr>
                                <td class="titleField">Nama Dealer</td>
                                <td width="1%">:</td>
                                <td>
                                    <asp:Label ID="lblDealerName" runat="server"></asp:Label></td>
                                <td></td>
                                <td></td>
                                <td width="25%"></td>
                            </tr>

                            <tr>
                                <td class="titleField" width="20%">Lokasi File</td>
                                <td width="1%">:</td>
                                <td width="79%" colspan="4">
                                    <input id="DataFile" onkeypress="return false;" size="29" type="file" name="File1" runat="server">
                                    <asp:Button ID="btnUpload" runat="server" Text="Upload" Width="60px"></asp:Button></td>
                            </tr>
                            <tr>
                                <td></td>
                                <td></td>
                                <td>
                                    <asp:LinkButton ID="LnkTemplate" runat="server">Download Template</asp:LinkButton>
                                </td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>

                            <tr>
                                <td class="titleField">&nbsp;</td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td width="25%"></td>
                            </tr>
                            <tr>
                                <td class="titleField"></td>
                                <td>:</td>
                                <td>
                                    <asp:Button Style="z-index: 0" ID="btnCancel" runat="server" Text="Batal" Width="60px"></asp:Button>
                                    <asp:Button ID="btnStore" runat="server" Text="Simpan" Width="60px"></asp:Button>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td></td>
                                <td></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td valign="top">
                        <div style="overflow: auto; height: 330px" id="div1">
                            <asp:DataGrid ID="dgRecallMaster" runat="server" Width="100%" CellPadding="3" BorderWidth="0px" CellSpacing="1"
                                BorderColor="Gainsboro" BackColor="#CDCDCD" ShowFooter="True" AutoGenerateColumns="False"
                                AllowSorting="false">
                                <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                                <AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
                                <ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
                                <HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
                                <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
                                <Columns>
                                    <asp:BoundColumn Visible="False" DataField="ID" ReadOnly="True" HeaderText="ID">
                                        <HeaderStyle Width="3%" CssClass="titleTableParts"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                    </asp:BoundColumn>
                                    <asp:TemplateColumn HeaderText="No">
                                        <HeaderStyle Width="3%" CssClass="titleTableParts"></HeaderStyle>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="No Rangka">
                                        <HeaderStyle Width="15%" CssClass="titleTableParts"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblChassisNumber" runat="server" >
                                            </asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtFChassisNumber" MaxLength="20" runat="server" size="20" onkeypress="return alphaNumericExcept(event,'<>?*%$;')" onblur="omitCharsOnCompsTxt(this,'<>?*%$;')" onChange="RebindRecallCategory(this);"></asp:TextBox>


                                            <asp:Button runat="server" ID="btnBindRegCategori" Text="" CommandName="RebindRecallCategory" Style="display: none;"></asp:Button>

                                        </FooterTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtEChassisNumber" runat="server" size="20" MaxLength="20">
                                            </asp:TextBox>

                                        </EditItemTemplate>
                                    </asp:TemplateColumn>


                                    <asp:TemplateColumn HeaderText="Jarak Tempuh (KM)">
                                        <HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
                                        <ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblMileAge" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.MileAge") %>' CssClass="textRight">
                                            </asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox onkeypress="return numericOnlyUniv(event)" ID="txtFMileAge" runat="server" size="5"
                                                CssClass="textRight" MaxLength="6"></asp:TextBox>
                                        </FooterTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox onkeypress="return numericOnlyUniv(event)" ID="txtEMileAge" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.MileAge") %>' size="5" CssClass="textRight" MaxLength="6">
                                            </asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateColumn>


                                    <asp:TemplateColumn HeaderText="Tanggal Pengerjaan (ddmmyyyy)">
                                        <HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
                                        <ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblServiceDate" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ServiceDate","{0:dd/MM/yyyy}") %>' CssClass="textRight">
                                            </asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox onkeypress="return numericOnlyUniv(event)" ID="txtFServiceDate" runat="server" Text='' size="8" CssClass="textRight" MaxLength="8" placeholder="ddMMyyyy"> </asp:TextBox>

                                            <%--   <cc1:inticalendar id="txtFServiceDate" runat="server" TextBoxWidth="70"></cc1:inticalendar>--%>
                                        </FooterTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox onkeypress="return numericOnlyUniv(event)" onblur="omitCharsOnCompsTxt(this,'<>?*%$/;')" ID="txtEServiceDate" runat="server" size="8" CssClass="textRight" MaxLength="8"> </asp:TextBox>

                                        </EditItemTemplate>
                                    </asp:TemplateColumn>

                                    <asp:TemplateColumn HeaderText="Field Fix Reg No">
                                        <HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
                                        <ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblRecallRegNo" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.RecallRegNo" ) %>' CssClass="textLeft">
                                            </asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:DropDownList ID="fddRecallRegNo" runat="server"  onChange="RebindRegNo(this);" ></asp:DropDownList>

                                                   <asp:Button runat="server" ID="btnRebindRegNo" Text="" CommandName="RebindRegNo" Style="display: none;"></asp:Button>

                                        </FooterTemplate>
                                        <EditItemTemplate>

                                            <asp:DropDownList ID="eddRecallRegNo" runat="server"></asp:DropDownList>
                                        </EditItemTemplate>
                                    </asp:TemplateColumn>


                                     <asp:TemplateColumn HeaderText="No Service Buletin">
                                        <HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
                                        <ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="TlblBuletingNo" runat="server"  CssClass="textLeft" Text='<%# DataBinder.Eval(Container, "DataItem.BuletinNo" ) %>' >
                                            </asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                              <asp:Label ID="FlblBuletingNo" runat="server"  CssClass="textLeft">
                                            </asp:Label>


                                        </FooterTemplate>
                                        <EditItemTemplate>

                                            <asp:Label ID="ElblBuletingNo" runat="server"  CssClass="textLeft">
                                            </asp:Label>
                                        </EditItemTemplate>
                                    </asp:TemplateColumn>



                                    <asp:BoundColumn DataField="ErrorMessage" SortExpression="ErrorMessage" ReadOnly="True" HeaderText="Pesan">
                                        <HeaderStyle Width="15%" CssClass="titleTableParts"></HeaderStyle>
                                    </asp:BoundColumn>


                                    <asp:TemplateColumn HeaderText="Aksi">
                                        <HeaderStyle HorizontalAlign="Center" Width="10%" CssClass="titleTableParts"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        <ItemTemplate>
                                            <%--  <asp:LinkButton ID="lbtnEdit" CausesValidation="False" runat="server" Text="Ubah" CommandName="edit">
											<img border="0" src="../images/edit.gif" alt="Ubah" style="cursor:hand"></asp:LinkButton>--%>
                                            <asp:LinkButton ID="lbtnDelete" CausesValidation="False" runat="server" Text="Hapus" CommandName="delete">
											<img src="../images/trash.gif" alt="Hapus" border="0" style="cursor:hand"></asp:LinkButton>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Center"></FooterStyle>
                                        <FooterTemplate>
                                            <asp:LinkButton ID="lbtnAdd" runat="server" Text="Tambah" CommandName="add">
											<img src="../images/add.gif" border="0" alt="Tambah" align="center" align="middle" style="Cursor:hand"></asp:LinkButton>
                                        </FooterTemplate>
                                        <EditItemTemplate>
                                            <asp:LinkButton ID="lbtnSave" TabIndex="40" CommandName="save" Text="Simpan" runat="server">
											<img src="../images/simpan.gif" border="0" alt="Simpan"></asp:LinkButton>
                                            <asp:LinkButton ID="lbtnCancel" TabIndex="50" CommandName="cancel" Text="Batal" runat="server">
											<img src="../images/batal.gif" border="0" alt="Batal"></asp:LinkButton>
                                        </EditItemTemplate>
                                    </asp:TemplateColumn>

                                </Columns>
                            </asp:DataGrid>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td style="height: 40px" align="left"></td>
                </tr>
            </table>
        </div>
    </form>


    <script type="text/C#" language="javascript">
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
