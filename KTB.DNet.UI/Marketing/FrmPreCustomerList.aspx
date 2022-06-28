<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmPreCustomerList.aspx.vb" Inherits="FrmPreCustomerList" SmartNavigation="False" %>

<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>FrmAplikasiHeader</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script language="javascript">
        /* Deddy H	validasi value *********************************** */
        /* ini untuk handle char yg tdk diperbolehkan, saat paste */
        function TxtBlur(objTxt) {
            omitSomeCharacter(objTxt, '<>?*%$');
        }
        /* ini untuk handle char yg tdk diperbolehkan, saat keypress */
        function TxtKeypress() {
            return alphaNumericExcept(event, '<>?*%$')
        }

        //function js untuk handle alphanumeric, dengan menghilangkan karakter numeric
        function alphaNumericNonNumeric(event) {
            if (navigator.appName == "Microsoft Internet Explorer")
                pressedKey = event.keyCode;
            else
                pressedKey = event.which

            if ((pressedKey == 32) || (pressedKey >= 97 && pressedKey <= 122) || (pressedKey >= 65 && pressedKey <= 90)) {
                return true;
            }
            else {
                return false;
            }
        }

        function TxtBlurNonNumeric(objTxt) {
            omitSomeCharacter(objTxt, '<>?*%$;0123456789');
        }


        // ******************

        function ShowPopUpVechileType() {
            showPopUp('../PopUp/PopUpVechileTypeMultiple.aspx?CategoryID=1&IsActive=A', '', 500, 760, VechileTypeSelection);
        }


        function VechileTypeSelection(SelectedVechileType) {
            var tempParam = SelectedVechileType.split(';');
            var valueVechileTypeCode = SelectedVechileType;
            var txtVechileTypeCode = document.getElementById("txtVechileTypeCode");
            if (navigator.appName == "Microsoft Internet Explorer") {
                txtVechileTypeCode.innerText = valueVechileTypeCode; //replace(tempParam[0], ' ', '');
            }
            else {
                txtVechileTypeCode.value = valueVechileTypeCode; //replace(tempParam[0], ' ', '');
            }
        }

        function ShowPPDealerSelection() {
            showPopUp('../General/../PopUp/PopUpDealerSelection.aspx', '', 500, 760, DealerSelection);
        }

        function DealerSelection(selectedDealer) {
            var txtDealerSelection = document.getElementById("txtKodeDealer");
            txtDealerSelection.value = selectedDealer;
        }

        function ShowPPSAP() {
            //showPopUp('../PopUp/PopUpSAPRegisterSalesman.aspx?x=Territory','',500,760,SAPSelection);
            showPopUp('../PopUp/PopUpSAPRegisterSalesman.aspx?FilterIndicator=Unit&IsGroupDealer=0', '', 500, 760, SAPSelection);
        }

        function SAPSelection(selectedSAP) {
            var tempParam = selectedSAP.split(';');

            var txtSalesmanID = document.getElementById("txtSalesmanID");
            var hidID = document.getElementById("hdnID");
            var lblNamaSalesman = document.getElementById("lblNamaSalesman");
            var hidName = document.getElementById("hdnName");

            txtSalesmanID.value = tempParam[0];
            lblNamaSalesman.innerText = tempParam[1];

        }


    </script>
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <table id="Table2" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage" style="height: 17px">Marketing - Daftar Prospektif Konsumen</td>
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
                    <table id="Table1" style="text-align: left" cellspacing="1" cellpadding="2" width="100%"
                        border="0">
                        <tr>
                            <td class="titleField">Dealer ID</td>
                            <td>
                                <asp:Label ID="Label2" runat="server">:</asp:Label></td>
                            <td>
                                <asp:TextBox ID="txtKodeDealer" runat="server" Width="70px"
                                    onkeypress="return alphaNumericExcept(event,'<>?*%$')"
                                    onblur="omitSomeCharacter('txtKodeDealer','<>?*%$')"></asp:TextBox>
                                <asp:Label ID="lblSearchDealer" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup">
                                </asp:Label>
                            </td>
                            <td class="titleField">Status</td>
                            <td>
                                <asp:Label ID="Label5" runat="server">:</asp:Label></td>
                            <td>
                                <asp:DropDownList ID="ddlStatus" runat="server" Width="270px"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td class="titleField">Salesman ID</td>
                            <td>
                                <asp:Label ID="lblColon3" runat="server">:</asp:Label></td>
                            <td>
                                <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" ID="txtSalesmanID" onblur="omitSomeCharacter('txtSalesmanID','<>?*%$;')"
                                    runat="server" ToolTip="Dealer Search 1" MaxLength="10" Width="70px"></asp:TextBox>
                                <asp:Label ID="lblPopUpSalesman" runat="server" Width="16px">
										<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0"></asp:Label>
                                <input id="hdnID" type="hidden" name="hdnID" runat="server">
                                <asp:Label ID="lblNamaSalesman" runat="server"></asp:Label>
                                <input id="hdnName" type="hidden" name="hdnName" runat="server">
                            </td>
                            <td class="titleField">Jenis Kelamin</td>
                            <td>
                                <asp:Label ID="Label4" runat="server">:</asp:Label></td>
                            <td>
                                <asp:DropDownList ID="ddlGender" runat="server" Width="270px"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td class="titleField">Nama Konsumen</td>
                            <td>
                                <asp:Label ID="Label1" runat="server">:</asp:Label></td>
                            <td>
                                <asp:TextBox ID="txtCustomerName" runat="server" Width="270px"></asp:TextBox>
                            </td>
                            <td class="titleField">Usia</td>
                            <td>
                                <asp:Label ID="Label3" runat="server">:</asp:Label></td>
                            <td>
                                <asp:DropDownList ID="ddlAge" runat="server" Width="270px"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td class="titleField">Tanggal Prospek</td>
                            <td>:</td>
                            <td>
                                <table cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td>
                                            <cc1:IntiCalendar ID="icPaymentDateFrom" runat="server" TextBoxWidth="70"></cc1:IntiCalendar></td>
                                        <td>&nbsp;s/d&nbsp;</td>
                                        <td>
                                            <cc1:IntiCalendar ID="icPaymentDateTo" runat="server" TextBoxWidth="70"></cc1:IntiCalendar></td>
                                    </tr>
                                </table>
                            </td>
                            <td class="titleField">Sumber Informasi</td>
                            <td>
                                <asp:Label ID="Label6" runat="server">:</asp:Label></td>
                            <td>
                                <asp:DropDownList ID="ddlTipe" runat="server" Width="270px"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td class="titleField">Tipe Konsumen</td>
                            <td>:</td>
                            <td>
                                <asp:DropDownList ID="ddlCustomerType" runat="server" Width="270px"></asp:DropDownList>
                            </td>
                            <td class="titleField">Sumber Lead</td>
                            <td>
                                <asp:Label ID="Label7" runat="server">:</asp:Label></td>
                            <td>
                                <asp:DropDownList ID="ddlSource" runat="server" Width="270px"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td class="titleField">Tipe Kendaraan</td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="txtVechileTypeCode" runat="server" MaxLength="100" Width="250px" onkeypress="return alphaNumericExcept(event,'<>?*%$')" onblur="omitCharsOnCompsTxt(this,'<>?*%$')"></asp:TextBox>
                                <asp:Label ID="lblVechileTypeCode" runat="server">
									    <img style="cursor:hand" alt="Klik Disini untuk memilih Tipe Kendaraan" src="../images/popup.gif" border="0">
                                </asp:Label>
                            </td>
                            <td class="titleField">Tujuan Konsumen</td>
                            <td>
                                <asp:Label ID="Label8" runat="server">:</asp:Label></td>
                            <td>
                                <asp:DropDownList ID="ddlPurpose" runat="server" Width="270px"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td class="titleField"></td>
                            <td></td>
                            <td></td>
                            <td class="titleField"></td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <asp:Panel ID="pnlInterface" runat="server" Style="border: 1px solid gray; padding: 0px 10px 10px 10px; margin-top: 10px">
                                    <table style="text-align: left" cellspacing="1" cellpadding="2" width="100%">                                        
                                        <tr>
                                            <td colspan="6" style="padding-top: 10px">
                                                <table id="titleInterface" border="0" cellspacing="0" cellpadding="0" width="100%">

                                                    <tr>
                                                        <td class="titlePanel"><b>ADDITIONAL DATA FOR DMS</b></td>
                                                    </tr>
                                                    <tr>
                                                        <td height="1" background="../images/bg_hor.gif">
                                                            <img border="0" src="../images/bg_hor.gif" height="1"></td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="titleField" style="width: 12%">Status Follow Up</td>
                                            <td width="10px">:</td>
                                            <td class="auto-style7">
                                                <asp:DropDownList ID="ddlLeadStatus" runat="server" Width="274px"></asp:DropDownList>
                                            </td>
                                            <td class="titleField"></td>
                                            <td colspan="2"></td>
                                        </tr>
                                        <tr>
                                            <td class="titleField">Status Akhir</td>
                                            <td>:</td>
                                            <td>
                                                <asp:DropDownList ID="ddlStateCode" runat="server" Width="274px"></asp:DropDownList>
                                            </td>
                                            <td class="titleField"></td>
                                            <td colspan="2"></td>
                                        </tr>
                                        <tr>
                                            <td class="titleField">Status Konsumen</td>
                                            <td>:</td>
                                            <td>
                                                <asp:DropDownList ID="ddlStatusCode" runat="server" Width="274px"></asp:DropDownList>
                                            </td>
                                            <td class="titleField"></td>
                                            <td colspan="2"></td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>

                        <tr>
                            <td class="titleField"></td>
                            <td></td>
                            <td></td>
                            <td class="titleField"></td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="titleField"></td>
                            <td></td>
                            <td>
                                <asp:Button ID="btnSearch" runat="server" Width="60px" Text="Cari"></asp:Button><asp:Button ID="btnBatal" runat="server" Width="60px" Text="Batal" CausesValidation="False"></asp:Button><asp:Button ID="btnDownload" runat="server" Width="60px" Text="Download" CausesValidation="False"></asp:Button></td>
                            <td class="titleField"></td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <asp:Label ID="lblTotalRow" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="titleField" colspan="6">
                                <div id="div1" style="overflow: auto; height: 300px">
                                    <asp:DataGrid ID="dgSAPCustomer" runat="server" Width="100%" CellPadding="0" BorderWidth="0px"
                                        CellSpacing="1" BorderColor="Gainsboro" BackColor="#CDCDCD" AutoGenerateColumns="False" AllowSorting="True" AllowCustomPaging="True" PageSize="25"
                                        AllowPaging="True">
                                        <AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
                                        <ItemStyle BackColor="White"></ItemStyle>
                                        <HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Blue"></HeaderStyle>
                                        <Columns>
                                            <asp:TemplateColumn Visible="False" HeaderText="ID">
                                                <HeaderStyle CssClass="titleTableMrk"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="No">
                                                <HeaderStyle CssClass="titleTableMrk"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNomor" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn SortExpression="Dealer.DealerCode" HeaderText="Kode Dealer">
                                                <HeaderStyle CssClass="titleTableMrk"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDealerCode" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn SortExpression="SalesmanHeader.DealerBranch.Name" HeaderText="Cabang Dealer">
                                                <HeaderStyle CssClass="titleTableMrk"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDealerBranch" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn SortExpression="CustomerName" HeaderText="Nama Konsumen">
                                                <HeaderStyle CssClass="titleTableMrk"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCustomerName" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn SortExpression="CustomerCode" HeaderText="Kode Konsumen" Visible="false">
                                                <HeaderStyle CssClass="titleTableMrk"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCustomerCode" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn SortExpression="CustomerType" HeaderText="Tipe Konsumen">
                                                <HeaderStyle CssClass="titleTableMrk"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCustomerType" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn SortExpression="CustomerAddress" HeaderText="Alamat Konsumen">
                                                <HeaderStyle CssClass="titleTableMrk"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCustomerAddress" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn SortExpression="Phone" HeaderText="Telepon" Visible="false">
                                                <HeaderStyle CssClass="titleTableMrk"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPhone" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn SortExpression="Email" HeaderText="Email" Visible="false">
                                                <HeaderStyle CssClass="titleTableMrk"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEmail" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn SortExpression="Sex" HeaderText="Gender">
                                                <HeaderStyle CssClass="titleTableMrk"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGender" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn SortExpression="Status" HeaderText="Status Prospek">
                                                <HeaderStyle CssClass="titleTableMrk"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStatus" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn SortExpression="InformationType" HeaderText="Sumber Informasi">
                                                <HeaderStyle CssClass="titleTableMrk"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTipe" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn SortExpression="InformationSource" HeaderText="Sumber Lead">
                                                <HeaderStyle CssClass="titleTableMrk"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSource" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn SortExpression="CustomerPurpose" HeaderText="Tujuan Konsumen">
                                                <HeaderStyle CssClass="titleTableMrk"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPurpose" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn SortExpression="VechileType.VechileTypeCode" HeaderText="Kode Kendaraan">
                                                <HeaderStyle CssClass="titleTableMrk"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblVechileTypeCode" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn SortExpression="Qty" HeaderText="Qty">
                                                <HeaderStyle CssClass="titleTableMrk"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblQty" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn SortExpression="ProspectDate" HeaderText="Tanggal Prospect">
                                                <HeaderStyle CssClass="titleTableMrk"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblProspectDate" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn SortExpression="SalesmanHeader.SalesmanCode" HeaderText="Salesman ID">
                                                <HeaderStyle CssClass="titleTableMrk"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSalesmanID" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn SortExpression="SalesmanHeader.Name" HeaderText="Nama Salesman">
                                                <HeaderStyle CssClass="titleTableMrk"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSalesmanName" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <%--<asp:TemplateColumn SortExpression="LeadStatus" HeaderText="Status">
													<HeaderStyle CssClass="titleTableMrk"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left"></ItemStyle>
													<ItemTemplate>
														<asp:Label ID="lblLeadStatus" Runat="server"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>--%>
                                            <asp:TemplateColumn>
                                                <HeaderStyle CssClass="titleTableMrk" Width="7%"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkbtnEdit" runat="server" Text="Ubah" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>' CommandName="Edit" CausesValidation="False">
									                    <img alt="Ubah" src="../images/edit.gif" border="0"></asp:LinkButton>
                                                    <asp:LinkButton ID="lnkbtnView" runat="server" Width="20px" Text="Lihat" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>' CommandName="View" CausesValidation="False">
									                    <img alt="Lihat" src="../images/detail.gif" border="0"></asp:LinkButton>
                                                    <asp:LinkButton ID="lbtnDelete" runat="server" Width="20px" Text="Hapus" CausesValidation="False" CommandName="Delete">
															<img src="../images/trash.gif" border="0" alt="Hapus" onclick="return confirm('Yakin ingin menghapus data ini?');"></asp:LinkButton>
                                                    <asp:LinkButton ID="lnkbtnSPK" runat="server" Text="Buat SPK" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>' CommandName="SPK" CausesValidation="False">
									                        <img alt="Buat SPK" src="../images/dok.gif" border="0"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                        </Columns>
                                        <PagerStyle HorizontalAlign="Left" Mode="NumericPages"></PagerStyle>
                                    </asp:DataGrid>
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td></td>
            </tr>
            <tr>
                <td style="height: 8px"></td>
            </tr>
        </table>
    </form>
</body>
</html>
