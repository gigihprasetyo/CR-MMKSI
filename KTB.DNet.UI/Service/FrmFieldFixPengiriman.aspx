<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmFieldFixPengiriman.aspx.vb" Inherits=".FrmFieldFixPengiriman" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Daftar Kategori Field Fix Campaign</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script type="text/javascript" language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script type="text/javascript" language="javascript" src="../WebResources/InputValidation.js"></script>
    <script language="javascript" src="../WebResources/FormFunctions.js"></script>
    <script language="javascript">

        function ShowPPDealerSelection() {
            showPopUp('../General/../PopUp/PopUpDealerSelection.aspx', '', 500, 760, DealerSelection);
        }

        function DealerSelection(selectedDealer) {
            var txtDealerCode = document.getElementById("txtKdDealer");
            txtDealerCode.value = selectedDealer;
        }

        function ShowPPSparePartSelection() {
            showPopUp('../PopUp/PopUpPartsForecastMaster.aspx', '', 500, 800, PartSelection);
        }

        function PartSelection(selectedPart) {
            var tempParam = selectedPart.split(';');
            var txtPartSelection = document.getElementById("txtPartNo");
            var lblPartName = document.getElementById("lblPartName");
            //var TextDealerName = document.getElementById("txtPartName");
            txtPartSelection.value = tempParam[0];
            lblPartName.innerHTML = tempParam[1];
            //TextDealerName.value = tempParam[1];
        }

        function ShowPPPOSelection() {
            showPopUp('../PopUp/PopUpPOConfirmationList.aspx', '', 500, 800, POSelection);
        }

        function POSelection(selectedPo) {
            var tempParam = selectedPo.split(';');
            var txtNoPO = document.getElementById("txtNoPO");
            txtNoPO.value = tempParam;
        }

        function CheckAll(aspCheckBoxID, checkVal) {
            re = new RegExp(':' + aspCheckBoxID + '$')
            for (i = 0; i < document.forms[0].elements.length; i++) {
                elm = document.forms[0].elements[i]
                if (elm.type == 'checkbox') {
                    if (re.test(elm.name)) {
                        elm.checked = checkVal
                        if (checkVal) {
                            document.getElementById("btnPrintPDF").disabled = false;
                        } else {
                            document.getElementById("btnPrintPDF").disabled = true;
                        }
                    }
                }
            }
        }

    </script>
</head>
<body>
    <form id="form1" runat="server" ms_positioning="GridLayout">
        <div>
            <table id="Table1" border="0" cellspacing="0" cellpadding="0" width="100%">
                <tr>
                    <td class="titlePage">Management Pengiriman - Cetak Permintaan</td>
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
                            <tbody>

                                <tr>
                                    <td class="titleField">Kode Dealer</td>
                                    <td width="1%">:</td>
                                    <td width="75%">
                                        <asp:TextBox ID="txtKdDealer" runat="server" size="22" onkeypress="return alphaNumericExcept(event,'<>?*%$')"
                                            Width="200px"></asp:TextBox>

                                        <asp:Label ID="Label1" runat="server" onclick="ShowPPDealerSelection();">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup">
                                        </asp:Label>
                                    </td>
                                </tr>

                                <tr>
                                    <td class="titleField" width="24%">Nomor PO</td>
                                    <td width="1%">:</td>
                                    <td width="75%">
                                        <asp:TextBox ID="txtNoPO" runat="server" size="22" onkeypress="return alphaNumericExcept(event,'<>?*%$')"
                                            onblur="omitSomeCharacter('txtNoPO','<>?*%$')" Width="200px"></asp:TextBox>

                                        <asp:Label ID="Label2" runat="server" onclick="ShowPPPOSelection();">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup">
                                        </asp:Label>
                                    </td>
                                </tr>

                                <tr>
                                    <td class="titleField" width="24%">Nomor Part</td>
                                    <td width="1%">:</td>
                                    <td width="75%">
                                        <asp:TextBox ID="txtPartNo" runat="server" size="22" onkeypress="return alphaNumericExcept(event,'<>?*%$')"
                                            onblur="omitSomeCharacter('txtPartName','<>?*%$')" Width="200px"></asp:TextBox>

                                        <asp:Label ID="lblSearchPart" runat="server" onclick="ShowPPSparePartSelection();">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup">
                                        </asp:Label>
                                    </td>
                                </tr>

                                <tr>
                                    <td class="titleField">Nama Part</td>
                                    <td width="1%">:</td>
                                    <td>
                                        <asp:Label ID="lblPartName" runat="server"></asp:Label></td>
                                    <td></td>
                                    <td></td>
                                    <td width="25%"></td>
                                </tr>

                                <tr>
                                    <td class="titleField">Tanggal Permintaan</td>
                                    <td width="1%">:</td>

                                    <td style="height: 20px; white-space: nowrap;" width="75%">
                                        <table cellspacing="0" cellpadding="0" border="0">
                                            <tr>
                                                <td>
                                                    <asp:CheckBox ID="chkTglReq" runat="server" Checked="true"></asp:CheckBox>
                                                </td>
                                                <td>
                                                    <cc1:IntiCalendar ID="icStartDateReq" runat="server" TextBoxWidth="70"></cc1:IntiCalendar></td>
                                                <td>
                                                &nbsp;s.d&nbsp;            
                                                <td>
                                                    <cc1:IntiCalendar ID="icEndDateReq" runat="server" TextBoxWidth="70"></cc1:IntiCalendar></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>

                                <tr>
                                    <td colspan="2"></td>
                                    <td width="75%">&nbsp; &nbsp;
                                        <asp:Button ID="btnSearch" runat="server" Width="64px" Text="Cari" CausesValidation="False"></asp:Button>
                                        <asp:Button ID="btnCancel" runat="server" Width="64px" Text="Batal" CausesValidation="False"></asp:Button>
                                        
                                    </td>
                                </tr>
                            </tbody>

                        </table>
                    </td>
                </tr>
                <tr valign="top">
                    <td>
                        <div id="div1" style="height: 260px; overflow: auto">
                            <asp:DataGrid ID="dtgSentPart" runat="server" Width="100%" AutoGenerateColumns="False" CellPadding="3" BackColor="#E0E0E0" AllowSorting="True" AllowCustomPaging="True" PageSize="50"
                                AllowPaging="True" BorderColor="#CDCDCD" CellSpacing="1" BorderWidth="0px">
                                <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                                <AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
                                <ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
                                <HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#000084"></HeaderStyle>
                                <FooterStyle ForeColor="Black" BackColor="#ededed"></FooterStyle>
                                <Columns>                                    
                                    <asp:TemplateColumn Visible="true">
                                        <HeaderStyle Width="1%" CssClass="titleTableService" ></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        <HeaderTemplate>
                                            <input id="chkAllItems" type="checkbox" onclick="CheckAll('chkSelection', document.forms[0].chkAllItems.checked)" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkSelection" runat="server" AutoPostBack="true" OnCheckedChanged="chkSelection_CheckedChanged"></asp:CheckBox>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
                                        <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                                        <ItemStyle ForeColor="Gray" BackColor="LightPink"></ItemStyle>
                                    </asp:BoundColumn>
                                    <asp:TemplateColumn HeaderText="No" >
                                        <HeaderStyle Width="3%" CssClass="titleTableService"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblNo"></asp:Label>
                                            <asp:Label ID="lblID" runat="server" Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn Visible="true" HeaderText="Nomor PO">
                                        <HeaderStyle Width="8%" CssClass="titleTableService"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lbliPoNo" runat="server" > 
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn Visible="true" HeaderText="Kode Dealer">
                                        <HeaderStyle Width="8%" CssClass="titleTableService"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lbliKdDealer" runat="server" > 
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>

                                    <asp:TemplateColumn Visible="true" HeaderText="Nomor Part">
                                        <HeaderStyle Width="8%" CssClass="titleTableService"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lbliPartNo" runat="server" >
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>

                                    <asp:TemplateColumn Visible="true" HeaderText="Nama Part">
                                        <HeaderStyle Width="12%" CssClass="titleTableService"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lbliPartName" runat="server">
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>

                                    <asp:TemplateColumn Visible="true" HeaderText="No Service Bulletin ">
                                        <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lbliNoBulletin" runat="server" > </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>

                                    <asp:TemplateColumn  HeaderText="Tgl Permintaan (ddmmyyyy)">
                                        <HeaderStyle Width="8%" CssClass="titleTableService"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lbliRequestDate" runat="server">
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>

                                    <asp:TemplateColumn Visible="true" HeaderText="Jumlah Permintaan">
                                        <HeaderStyle Width="7%" CssClass="titleTableService" HorizontalAlign="Right"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>   
                                        <ItemTemplate>
                                            <asp:Label ID="lbliRequestQty" runat="server" >
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>

                                    <asp:TemplateColumn Visible="true" HeaderText="Jumlah Disetujui">
                                        <HeaderStyle Width="7%" CssClass="titleTableService" HorizontalAlign="Right"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>   
                                        <ItemTemplate>
                                            <asp:Label ID="lbliApprovedQty" runat="server">
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>    

                                    <asp:TemplateColumn Visible="true" HeaderText="Status">
                                        <HeaderStyle Width="7%" CssClass="titleTableService"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lbliStatus" runat="server" > </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                </Columns>
                                <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
                            </asp:DataGrid>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td style="height: 40px" align="left"></td>
                </tr>
                <tr>
                    <td width="75%">
                        <asp:Button ID="btnPrintPDF" runat="server" Text="Print" Width="60px" Enabled="false"></asp:Button>&nbsp;
                        &nbsp;&nbsp;<asp:Label ID="lblLoading" ForeColor="Red" runat="server"></asp:Label>
                        <%--<asp:Button ID="Button1" runat="server" Text="Print PDF" Width="60px" ></asp:Button>&nbsp;--%>
                    </td>

                </tr> 
            </table>
        </div>
    </form>
</body>
</html>
