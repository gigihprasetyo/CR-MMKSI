<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frmChassisMasterDoc.aspx.vb" Inherits=".frmChassisMasterDoc" %>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>



<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>ListContract</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/FormFunctions.js"></script>
    <script language="javascript">
     

        var SomeChecked;
        function MakeValid() {
            SomeChecked = true;
        }

        function IsChecked() {
            if (IsAnyCheckedCheckBox('chkSelect')) {
                SomeChecked = true;
                return true;
            }
            else {
                SomeChecked = false;
                alert("Anda belum memilih faktur");
                return false;
            }
        }

        function ShowPPDealerSelection() {
            showPopUp('../General/../PopUp/PopUpDealerSelectionOne.aspx', '', 500, 760, DealerSelection);
        }
        function DealerSelection(selectedDealer) {
            var temp = selectedDealer.split(';');
            var txtDealerSelection = document.getElementById("txtKodeDealer");
            txtDealerSelection.value = temp[0];
        }

        function ShowFleetReqSelection() {
            showPopUp('../PopUp/PopUpFleetReqTersedia.aspx?IsGroupDealer=1', '', 500, 800, FleetReqSelection);
        }

        function FleetReqSelection(selectedFleetReq) {
            var temp = selectedFleetReq.split(";")
            var txtNoFleetReq = document.getElementById('txtNoFleetReq');
            txtNoFleetReq.value = temp[0];
        }

    </script>
</head>
<body onfocus="return checkModal()" onclick="checkModal()" ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <table id="Table2" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage" style="height: 17px">FAKTUR KENDARAAN&nbsp;-&nbsp;Daftar 
						Status Gesek Kendaraan</td>
            </tr>
            <tr>
                <td background="../images/bg_hor.gif" height="1">
                    <img height="1" src="../images/bg_hor.gif" border="0"></td>
            </tr>
            <tr>
                <td style="height: 6px" height="6">
                    <img height="1" src="../images/dot.gif" border="0"></td>
            </tr>
            <tr>
                <td>
                    <table id="Table1" cellspacing="1" cellpadding="2" width="100%" border="0">
                        <tr>
                            <td class="titleField" width="20%" style="height: 11px">
                                <asp:Label ID="lblDealerCode" runat="server">Kode Dealer</asp:Label></td>
                            <td width="1%" style="height: 11px">
                                <asp:Label ID="Label1" runat="server">:</asp:Label></td>
                            <td width="24%" style="height: 11px">
                                <asp:TextBox ID="txtKodeDealer" onkeypress="return alphaNumericExcept(event,'<>?*%$')" onblur="omitSomeCharacter('txtKodeDealer','<>?*%$;')"
                                    runat="server"></asp:TextBox><asp:Label ID="lblSearchDealer" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:Label></td>
                            <td class="titleField" width="20%" style="height: 11px">
                                <asp:Label ID="lblCategory" runat="server"> Kategori</asp:Label></td>
                            <td width="1%" style="height: 11px">
                                <asp:Label ID="lblColon4" runat="server">:</asp:Label></td>
                            <td style="height: 11px">
                                <asp:DropDownList ID="ddlCategory" runat="server" Width="140px"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td class="titleField">
                                <asp:CheckBox ID="chkConfirmPeriod" runat="server" Text="Periode Konfirmasi"></asp:CheckBox></td>
                            <td>
                                <asp:Label ID="Label6" runat="server">:</asp:Label></td>
                            <td nowrap>
                                <table cellspacing="0" cellpadding="0" border="0">
                                    <tr>
                                        <td>
                                            <cc1:inticalendar id="icStartConfirm" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
                                        <td>&nbsp;s/d&nbsp;</td>
                                        <td>
                                            <cc1:inticalendar id="icEndConfirm" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
                                    </tr>
                                </table>
                            </td>
                            <td class="titleField">
                                <asp:CheckBox ID="chkValidPeriod" runat="server" Checked="True" Text="Periode Validasi"></asp:CheckBox></td>
                            <td>
                                <asp:Label ID="Label4" runat="server">:</asp:Label></td>
                            <td width="34%" nowrap>
                                <table cellspacing="0" cellpadding="0" border="0">
                                    <tr>
                                        <td>
                                            <cc1:inticalendar id="icStartValid" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
                                        <td>&nbsp;s/d&nbsp;</td>
                                        <td>
                                            <cc1:inticalendar id="icEndValid" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField">
                                <asp:Label ID="lblChassisNo" runat="server">Nomor Rangka</asp:Label></td>
                            <td>
                                <asp:Label ID="Label3" runat="server">:</asp:Label></td>
                            <td>
                                <asp:TextBox ID="txtChassisNo" onkeypress="return alphaNumericExcept(event,'<>?*%$')" onblur="omitSomeCharacter('txtChassisNo','<>?*%$;')"
                                    runat="server" Width="140px" size="22" MaxLength="20"></asp:TextBox></td>
                            <td class="titleField">
                                Sudah Gesek Rangka</td>
                            <td>
                                :</td>
                            <td nowrap>
                                 <asp:DropDownList runat="server" ID="ddlGesek">
                                     <asp:ListItem Value="" Text="Silahkan Pilih"></asp:ListItem>
                                     <asp:ListItem Value="1" Text="Sudah Di Gesek"></asp:ListItem>
                                     <asp:ListItem Value="0" Text="Belum DI Gesek"></asp:ListItem>

                                 </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td class="titleField">
                                <asp:Label ID="lblInvoiceNo" runat="server">Nomor Faktur</asp:Label></td>
                            <td width="1%" style="height: 11px">
                                <asp:Label ID="Label10" runat="server">:</asp:Label></td>
                            <td>
                                <asp:TextBox ID="txtInvoiceNo" onkeypress="return alphaNumericExcept(event,'<>?*%$')" onblur="omitSomeCharacter('txtInvoiceNo','<>?*%$;')"
                                    runat="server" Width="140px" size="22" MaxLength="20"></asp:TextBox></td>
                            <td class="titleField" width="20%" style="height: 11px">
                                <asp:Label ID="Label9" runat="server">Nomor SPK</asp:Label></td>
                            <td width="1%" style="height: 11px">
                                <asp:Label ID="Label12" runat="server">:</asp:Label></td>
                            <td width="24%" style="height: 11px">
                                <asp:TextBox ID="txtSPKNumber" runat="server" Width="140px"></asp:TextBox>
                            </td>
                           
                        </tr>
                        <tr>
                            <td class="titleField" width="20%" style="height: 11px">
                                &nbsp;</td>
                            <td width="1%" style="height: 11px">
                                &nbsp;</td>
                            <td width="24%" style="height: 11px">
                                &nbsp;</td>
                            <td class="titleField" width="20%" style="height: 11px; display:none;"> &nbsp;</td>
                            <td width="1%" style="height: 11px;display:none;">:</td>
                            <td style="height: 11px;display:none;">&nbsp;</td>
                        </tr>

                        <tr>
                            <td class="titleField">
                                &nbsp;</td>
                            <td></td>
                            <td>
                                &nbsp;</td>
                            <td class="titleField"></td>
                            <td></td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                           
                            <td class="titleField">
                                </td>
                            <td></td>
                            <td>
                                <asp:Button ID="btnSearch" runat="server" Width="60px" Text="Cari"></asp:Button></td>
                            <td class="titleField"></td>
                            <td></td>
                            <td></td>
                        </tr>
                        
                        

                          <tr>
                            <td class="titleField">
                                </td>
                            <td></td>
                            <td></td>
                            <td class="titleField"></td>
                            <td></td>
                            <td></td>
                        </tr>

                        <tr>
                            <td valign="top" colspan="6">
                                <div id="div1" style="overflow: auto; height: 240px">
                                    <asp:DataGrid ID="dgInvoiceList" runat="server" Width="100%" AllowSorting="True" CellPadding="3"
                                        BorderWidth="0px" CellSpacing="1" BorderColor="Gainsboro" BackColor="#CDCDCD" AutoGenerateColumns="False" PageSize="100" AllowPaging="True"
                                        AllowCustomPaging="True">
                                        
                                        <ItemStyle BackColor="White"></ItemStyle>
                                        <HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Blue"></HeaderStyle>
                                        <Columns>
                                            <asp:BoundColumn Visible="False" DataField="ID" ReadOnly="True" HeaderText="ID">
                                                <HeaderStyle Width="1%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            </asp:BoundColumn>
                                            <asp:TemplateColumn HeaderText="No">
                                                <HeaderStyle Width="2%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNo" runat="server" Text=""></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                          
                                            
                                            <asp:TemplateColumn SortExpression="Dealer.DealerCode" HeaderText="Dealer">
                                                <HeaderStyle Width="4%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDealer" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerCode") %>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                              <asp:TemplateColumn SortExpression="Dealer.DealerName" HeaderText="Nama Dealer">
                                                <HeaderStyle Width="6%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDealerName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerName") %>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                             
                                            <asp:TemplateColumn SortExpression="EndCustomer.SPKFaktur.SPKHeader.SPKNumber" HeaderText="No. Reg. SPK">
                                                <HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.EndCustomer.SPKFaktur.SPKHeader.SPKNumber") %>' ID="Label8">
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>

                                              <asp:TemplateColumn SortExpression="EndCustomer.Name1" HeaderText="Nama Customer">
                                                <HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.EndCustomer.Name1") %>' ID="Label88">
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>

                                            <asp:TemplateColumn SortExpression="EndCustomer.SPKFaktur.SPKHeader.CreatedTime" HeaderText="Tgl Pengajuan SPK">
                                                <HeaderStyle Width="5%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSPKCreatedDate" runat="server" Text='<%# Format(DataBinder.Eval(Container, "DataItem.EndCustomer.SPKFaktur.SPKHeader.CreatedTime"),"dd/MM/yyyy") %>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:BoundColumn DataField="ChassisNumber" SortExpression="ChassisNumber" ReadOnly="True" HeaderText="Nomor Rangka">
                                                <HeaderStyle Width="8%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                            </asp:BoundColumn>
                                            <asp:TemplateColumn SortExpression="VechileColor.MaterialNumber" HeaderText="Tipe/Warna">
                                                <HeaderStyle Width="8%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.VechileColor.MaterialNumber") %>' ID="Label2">
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                          
                                            

                                             <asp:TemplateColumn HeaderText="Detail">
                                                <HeaderStyle Width="2%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkDetail" runat="server" CommandName="View">
															<img src="../images/detail.gif" border="0" style="cursor:hand" alt="Lihat detil">
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>

                                        </Columns>
                                        <PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
                                    </asp:DataGrid>
                                </div>
                            </td>
                        </tr>
                    </table>
                    &nbsp;
						&nbsp;
						&nbsp;
						&nbsp;
						&nbsp;
						</td>
            </tr>
             <tr>
                            <td style="height: 8px" colspan="5">
                                <table>
                                    <tr>
                                        <td>
                                            <div style="background-color: aquamarine; width: 15px; height: 10px; border: 1px solid black;"></div>
                                        </td>
                                        <td>Belum Gesek Rangka</td>
                                    </tr>
                                </table>
                            </td>

                        </tr>
            <tr>
                <td style="height: 8px"></td>
            </tr>
        </table>
    </form>
    <script  language="javascript">

         
    </script>
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
</body>
</html>
