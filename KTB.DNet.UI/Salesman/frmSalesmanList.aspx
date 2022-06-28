<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>

<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frmSalesmanList.aspx.vb" Inherits="frmSalesmanList" SmartNavigation="False" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>FrmSalesmanList</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/FormFunctions.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript">
        function ShowPPDealerSelection() {
            showPopUp('../SparePart/../PopUp/PopUpDealerSelection.aspx', '', 500, 760, DealerSelection);
        }
        function DealerSelection(selectedDealer) {
            var txtDealer = document.getElementById("txtDealerCode");
            txtDealer.value = selectedDealer;
        }

        /* Deddy H	validasi value *********************************** */
        /* ini untuk handle char yg tdk diperbolehkan, saat paste */
        function TxtBlurName(objTxt) {
            omitSomeCharacterExcludeSingleQuote(objTxt, '<>?*%$;');
        }
        function TxtBlur(objTxt) {
            omitSomeCharacter(objTxt, '<>?*%$;');
        }
        /* ini untuk handle char yg tdk diperbolehkan, saat keypress */
        function TxtKeypress() {
            return alphaNumericExcept(event, '<>?*%$;')
        }

    </script>
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage">
                    <asp:Label ID="lblPageTitle" runat="server"></asp:Label></td>
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
                    <table id="Table2" cellspacing="1" cellpadding="1" width="100%" border="0">
                        <tr>
                            <td class="titleField" width="20%">Kode Dealer</td>
                            <td style="height: 17px" width="1%">:</td>
                            <td width="79%">
                                <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$')" ID="txtDealerCode" onblur="omitSomeCharacter('txtDealerCode','<>?*%$')"
                                    runat="server" Width="191px"></asp:TextBox><asp:Label ID="lblSearchDealer" runat="server">
										<img style="cursor:hand" alt="Klik Disini untuk memilih Nomor Permintaan" src="../images/popup.gif"
											border="0"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="titleField">
                                <asp:Label ID="lblSalesmanUnit" runat="server" Width="220px"></asp:Label></td>
                            <td width="1%">:</td>
                            <td>
                                <asp:DropDownList ID="ddlUnit" runat="server"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td class="titleField">
                                <asp:Label ID="lblSalesmanID" runat="server" Width="220px"></asp:Label></td>
                            <td style="height: 28px" width="1%">:</td>
                            <td>
                                <asp:TextBox onkeypress="TxtKeypress();" ID="txtID" onblur="TxtBlur('txtID');" runat="server"
                                    MaxLength="15"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="titleField">Nama</td>
                            <td style="height: 23px" width="1%">:</td>
                            <td>
                                <asp:TextBox onkeypress="TxtKeypress();" ID="txtNama" onblur="TxtBlurName('txtNama');" runat="server"
                                    MaxLength="60"></asp:TextBox>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="titleField">Posisi</td>
                            <td style="height: 28px" width="1%">:</td>
                            <td>
                                <%--<asp:DropDownList ID="ddlPosisi" runat="server"></asp:DropDownList>--%>
                                <asp:CheckBoxList ID="chkPosisi" runat="server" RepeatColumns="2"></asp:CheckBoxList>

                            </td>
                        </tr>
                        <tr>
                            <td class="titleField">Status</td>
                            <td style="height: 28px" width="1%">:</td>
                            <td>
                                <%--<asp:DropDownList ID="ddlStatus" runat="server"></asp:DropDownList>--%>
                                <asp:CheckBoxList ID="chkStatus" runat="server" RepeatColumns="2"></asp:CheckBoxList>
                            </td>
                        </tr>
                        <tr id="trLevel" runat="server" visible="false">
                            <td class="titleField">Level</td>
                            <td width="1%">:</td>
                            <td>
                                <asp:DropDownList ID="ddlSalesmanLevel" TabIndex="12" runat="server"></asp:DropDownList></td>
                        </tr>

                        <tr>
                            <td class="titleField">
                                <asp:CheckBox ID="chkHireDate" runat="server" Text="Periode Registrasi" Checked="False"></asp:CheckBox></td>
                            <td style="height: 28px" width="1%">
                                <asp:Label ID="lblColonHireDate" runat="server" Text=":" ></asp:Label>
                            </td>
                            <td>
                                <table border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <cc1:IntiCalendar ID="icHireDateFrom" runat="server" TextBoxWidth="60"></cc1:IntiCalendar></td>
                                        <td>
                                            <asp:Label ID="lblUntilHireDate" runat="server" Text ="&nbsp;s/d&nbsp;"></asp:Label></td>
                                        </td>
                                        <td>
                                            <cc1:IntiCalendar ID="icHireDateUntil" runat="server" TextBoxWidth="60"></cc1:IntiCalendar>
                                        </td>
                                    </tr>
                                    </table>
                             
                            </td>
                            </tr>
                      
                        <tr>
                            <td class="titleField">
                                <asp:CheckBox ID="chkResignDate" runat="server" Text="Periode Pengunduran Diri" Checked="False" AutoPostBack="true"></asp:CheckBox></td>
                            <td style="height: 28px" width="1%">
                                <asp:Label ID="lblColonResignDate" runat="server" Text=":" ></asp:Label>
                            </td>
                            <td>
                                <table border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <cc1:IntiCalendar ID="icResignDateFrom" runat="server" TextBoxWidth="60"></cc1:IntiCalendar></td>
                                        <td><asp:Label ID="lblUntilResignDate" runat="server" Text ="&nbsp;s/d&nbsp;"></asp:Label></td></td>
                                        <td>
                                            <cc1:IntiCalendar ID="icResignDateUntil" runat="server" TextBoxWidth="60"></cc1:IntiCalendar></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField">
                                <asp:CheckBox ID="chkDateOfBirth" runat="server" Text="Tanggal Lahir" Checked="False"></asp:CheckBox></td>
                            <td width="1%">
                                <asp:Label ID="lblColonDOB" runat="server" Text=":" ></asp:Label>
                            </td>
                            <td>
                                <table border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <cc1:IntiCalendar ID="icDateOfBirthFrom" runat="server" TextBoxWidth="60"></cc1:IntiCalendar></td>
                                        <td>
                                            <asp:Label ID="lblUntilDOB" runat="server" Text ="&nbsp;s/d&nbsp;"></asp:Label></td>
                                        <td>
                                            <cc1:IntiCalendar ID="icDateOfBirthUntil" runat="server" TextBoxWidth="60"></cc1:IntiCalendar></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField"></td>
                            <td width="1%"></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="titleField"></td>
                            <td width="1%"></td>
                            <td>
                                <asp:Button ID="btnCari" runat="server" Width="60px" Text="Cari"></asp:Button><asp:Button ID="btnCancel" runat="server" Width="60px" Text="Batal"></asp:Button><asp:Button ID="btnDownloadExcel" Text="Download Excel" runat="server"></asp:Button></td>
                        </tr>
                        <tr>
                            <td class="titleField">Total Record :
                                <asp:Label ID="lblTotalRecord" runat="server">0</asp:Label></td>
                            <td width="1%"></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="titleField" colspan="3">
                                <div id="div1" style="overflow: auto; height: 240px">
                                    <asp:DataGrid ID="dgSalesmanHeader" runat="server" Width="100%" CellPadding="3" BackColor="#CDCDCD"
                                        PageSize="25" AllowCustomPaging="True" AllowPaging="True" BorderWidth="0px" BorderColor="Gainsboro" AutoGenerateColumns="False" CellSpacing="1"
                                        AllowSorting="True" DESIGNTIMEDRAGDROP="57">
                                        <AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
                                        <ItemStyle BackColor="White"></ItemStyle>
                                        <HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Gray"></HeaderStyle>
                                        <Columns>
                                            <asp:BoundColumn Visible="False" DataField="ID" SortExpression="ID" HeaderText="ID"></asp:BoundColumn>
                                            <asp:TemplateColumn Visible="true">
                                                <HeaderStyle ForeColor="White" Width="3%" CssClass="titleTableParts"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <HeaderTemplate>
                                                    <input id="chkAllItems" onclick="CheckAllDataGridCheckBoxes('ChkExport', document.all.chkAllItems.checked)"
                                                        type="checkbox">
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="ChkExport" runat="server"></asp:CheckBox>
                                                    <asp:Label ID="lblID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>' Visible="false">
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>

                                            <asp:TemplateColumn HeaderText="No">
                                                <HeaderStyle Width="2%" CssClass="titleTableParts"></HeaderStyle>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn SortExpression="Dealer.DealerCode" HeaderText="Kode Dealer">
                                                <HeaderStyle CssClass="titleTableParts"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblKodeDealer" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:BoundColumn DataField="SalesmanCode" SortExpression="SalesmanCode" HeaderText="Kode">
                                                <HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="Name" SortExpression="Name" HeaderText="Nama">
                                                <HeaderStyle Width="15%" CssClass="titleTableParts"></HeaderStyle>
                                            </asp:BoundColumn>
                                            <asp:TemplateColumn SortExpression="JobPosition.Description" HeaderText="Posisi">
                                                <HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblJobPositionId_Main" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Kode Atasan">
                                                <HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblKodeAtasan" runat="server">
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Nama Atasan">
                                                <HeaderStyle Width="15%" CssClass="titleTableParts"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNamaAtasan" runat="server">
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Posisi Atasan">
                                                <HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPosisiAtasan" runat="server">
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>

                                            <%--<asp:BoundColumn DataField="Email" SortExpression="Email" HeaderText="Email">
                                                <HeaderStyle Width="15%" CssClass="titleTableParts"></HeaderStyle>
                                            </asp:BoundColumn>--%>
                                           <%--   
                                            <asp:BoundColumn DataField="HireDate" SortExpression="HireDate" HeaderText="Tanggal Masuk" DataFormatString="{0:dd/MM/yyyy}">
                                                <HeaderStyle Width="15%" CssClass="titleTableParts"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            </asp:BoundColumn>
                                            <asp:TemplateColumn SortExpression="DateOfBirth" HeaderText="Tanggal Lahir">
                                                <HeaderStyle CssClass="titleTableParts"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblResignDate" Text='<%# Format(DataBinder.Eval(Container, "DataItem.DateOfBirth"),"dd/MM/yyyy") %>' runat="server">
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                             <asp:TemplateColumn SortExpression="SalesmanLevel.Description" HeaderText="Level">
                                                <HeaderStyle Width="8%" CssClass="titleTableParts"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SalesmanLevel.Description") %>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SalesmanLevel.Description") %>'>
                                                    </asp:TextBox>
                                                </EditItemTemplate>
                                            </asp:TemplateColumn>
                                           --%>
                                            <asp:TemplateColumn HeaderText="Kategori Tim">
                                                <HeaderStyle Width="7%" CssClass="titleTableParts"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCategoryTeam" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn SortExpression="Status" HeaderText="Status">
                                                <HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStatus" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            
                                            <asp:TemplateColumn>
                                                <HeaderStyle Width="10%" CssClass="titleTableParts"></HeaderStyle>
                                                <ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnView" runat="server" Width="20px" Text="Lihat" CausesValidation="False"
                                                        CommandName="View">
															<img src="../images/detail.gif" border="0" alt="Lihat"></asp:LinkButton>
                                                    <asp:LinkButton ID="lbtnEdit" runat="server" Width="20px" Text="Ubah" CausesValidation="False" CommandName="Edit">
															<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
                                                    <asp:LinkButton ID="lbtnDelete" runat="server" Width="20px" Text="Hapus" CausesValidation="False"
                                                        CommandName="Delete">
															<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                        </Columns>
                                        <PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
                                    </asp:DataGrid>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" colspan="3">&nbsp;</td>

                        </tr>
                        <tr>

                            <td class="titleField" colspan="3">
                                <asp:DropDownList ID="ddlSalesLevel2" runat="server"></asp:DropDownList>

                                <asp:Button ID="btnChange" runat="server" Width="119px" Text="Ubah Level"></asp:Button>
                            </td>


                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
    <script type="text/javascript">
        (function () {
            var tId = setInterval(function () {
                if (document.readyState == "complete") onComplete()
            }, 11);
            function onComplete() {
                clearInterval(tId);
                var labelIdentityId = document.getElementById('labelPhoto');
                //labelIdentityId.innerHTML = 'Hello';
            };
        })()
    </script>
</body>
</html>
