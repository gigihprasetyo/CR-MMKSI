<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmPreCustomerEntryUpload.aspx.vb" Inherits="FrmPreCustomerEntryUpload" SmartNavigation="False" %>

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
            omitSomeCharacter(objTxt, '<>?*%$;');
        }
        /* ini untuk handle char yg tdk diperbolehkan, saat keypress */
        function TxtKeypress() {
            return alphaNumericExcept(event, '<>?*%$;')
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
        function ShowPopUpSAPRegisterSalesman() {

            //var txtSapNo = document.getElementById("txtSapNo");
            //showPopUp('../PopUp/PopUpSAPRegisterSalesman.aspx','',460,760,SAPRegisterSelection);
            showPopUp('../PopUp/PopUpSAPRegisterSalesman.aspx?FilterIndicator=Unit&IsGroupDealer=0', '', 500, 760, SAPRegisterSelection);
        }

        function SAPRegisterSelection(SelectedSalesman) {

            //var indek = GetCurrentInputIndex();
            var dgSAPCustomer = document.getElementById("dgSAPCustomer");
            var tempParam = SelectedSalesman.split(';');
            var txtSalesmanID = document.getElementById("txtSalesmanID");
            var txtSalesmanName = document.getElementById("txtSalesmanName");

            txtSalesmanName.value = tempParam[1];
            txtSalesmanID.value = tempParam[0];
            //__doPostBack('__Page', 'searchsalesman');
        }

        function ShowPopUpVechileType() {
            showPopUp('../PopUp/PopUpVechileType.aspx?CategoryID=1&IsActive=A', '', 500, 760, VechileTypeSelection);
        }

        function ShowPopUpCustomer() {
            showPopUp('../PopUp/PopUpCustomerName.aspx', '', 500, 760, CustomerSelection);
        }

        function CustomerSelection(SelectedCustomer) {
            var indek = GetCurrentInputIndex();
            var dgSAPCustomer = document.getElementById("dgSAPCustomer");
            var tempParam = SelectedCustomer.split(';');
            // input berupa teks box, urutan dikolom
            var txtCustomerName = dgSAPCustomer.rows[indek].getElementsByTagName("INPUT")[0];
            var txtCustomerCode = dgSAPCustomer.rows[indek].getElementsByTagName("INPUT")[1];
            var txtCustomerAddress = dgSAPCustomer.rows[indek].getElementsByTagName("INPUT")[2];
            // span berupa label
            ///var DescArea = dgSAPCustomer.rows[indek].getElementsByTagName("SPAN")[1];

            if (navigator.appName == "Microsoft Internet Explorer") {
                txtCustomerName.innerText = tempParam[1];
                txtCustomerCode.innerText = tempParam[0];
                txtCustomerAddress.innerText = tempParam[2];
                //DescArea.innerHTML = tempParam[1];	
            }
            else {
                txtCustomerName.value = tempParam[1];
                //DescArea.value = tempParam[1];
            }
        }
        function GetCurrentInputIndex() {
            var dgSAPCustomer = document.getElementById("dgSAPCustomer");
            var currentRow;
            var index = 0;
            var inputs;
            var indexInput;

            for (index = 0; index < dgSAPCustomer.rows.length; index++) {
                inputs = dgSAPCustomer.rows[index].getElementsByTagName("INPUT");

                if (inputs != null && inputs.length > 0) {
                    for (indexInput = 0; indexInput < inputs.length; indexInput++) {
                        if (inputs[indexInput].type != "hidden")
                            return index;
                    }
                }
            }
            return -1;
        }

        function VechileTypeSelection(SelectedVechileType) {

            var indek = GetCurrentInputIndex();
            var dgSAPCustomer = document.getElementById("dgSAPCustomer");
            var tempParam = SelectedVechileType.split(';');
            var CustomerNameValue = tempParam[1];
            var VechileTypeCode = dgSAPCustomer.rows[indek].getElementsByTagName("INPUT")[3];
            //alert(replace(tempParam[0],' ',''));
            //alert(indek);

            if (navigator.appName == "Microsoft Internet Explorer") {
                VechileTypeCode.innerText = replace(tempParam[0], ' ', '');
            }
            else {
                VechileTypeCode.value = replace(tempParam[0], ' ', '');
            }
        }

        function ShowPPSAP() {
            showPopUp('../SparePart/../PopUp/PopUpSAP.aspx?x=Territory', '', 500, 760, SAPSelection);
        }

        function SAPSelection(selectedSAP) {
            var tempParam = selectedSAP.split(';');

            var txtSAPNo = document.getElementById("txtSAPNo");
            var lblDateFrom = document.getElementById("lblDateFrom");
            var lblDateUntil = document.getElementById("lblDateUntil");
            var txtPeriod = document.getElementById("txtPeriod");

            txtSAPNo.value = tempParam[0];
            lblDateFrom.innerText = tempParam[1];
            lblDateUntil.innerText = tempParam[2];
            txtPeriod.value = tempParam[1] + ';' + tempParam[2];


        }

        function SetSalesmanCode(selectedSales, mode) {

            if (selectedSales != '') {
                var indek = GetCurrentInputIndex();
                var dgSAPCustomer = document.getElementById("dgSAPCustomer");
                var txtSalesmanCode = document.getElementById("txtSalesmanCode");
                // setting posisi berdasarkan urutan kolom di grid
                var lblSalesmanCode = dgSAPCustomer.rows[indek].getElementsByTagName("SPAN")[1];
                lblSalesmanCode.innerText = selectedSales.value;
                txtSalesmanCode.value = selectedSales.value;
            }
        }
    </script>
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <table id="Table2" cellspacing="0" cellpadding="0" width="115%" border="0">
            <tr>
                <td class="titlePage" style="height: 17px">Marketing - Upload Prospektif Konsumen </td>
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
                            <td class="titleField" style="height: 21px" width="24%">Kode Dealer</td>
                            <td style="height: 21px" width="1%">
                                <asp:Label ID="lblColon3" runat="server">:</asp:Label></td>
                            <td colspan="3">
                                <asp:Label ID="lblDealerCode" runat="server"></asp:Label></td>
                            <td style="height: 21px" width="29%">
                                <input id="txtSalesmanCode" type="hidden" runat="server" name="txtSalesmanCode"></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="height: 17px" width="24%">Nama Dealer</td>
                            <td style="height: 17px" width="1%">
                                <asp:Label ID="Label1" runat="server">:</asp:Label></td>
                            <td colspan="3">
                                <asp:Label ID="lblDealerName" runat="server"></asp:Label></td>
                            <td style="height: 17px" width="29%">
                                <input id="txtPeriod" type="hidden" name="Hidden1" runat="server"></td>
                        </tr>
                        <tr style="visibility: hidden">
                            <td class="titleField" style="height: 20px" width="24%">Salesman</td>
                            <td style="height: 20px" width="1%">
                                <asp:Label ID="Label2" runat="server">:</asp:Label></td>
                            <td style="height: 20px" nowrap>
                                <asp:TextBox onkeypress="return HtmlCharUniv(event)" ID="txtSalesmanID" TabIndex="14" runat="server"
                                    MaxLength="50" Width="70px"></asp:TextBox><asp:TextBox onkeypress="return HtmlCharUniv(event)" ID="txtSalesmanName" TabIndex="14" runat="server"
                                        MaxLength="50" Width="130px"></asp:TextBox><asp:Label ID="lblSearchSalesman" runat="server">
										<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup">
                                        </asp:Label><asp:LinkButton ID="lbtnSearchSalesman" Style="display: none" runat="server">Dont remove
                                        </asp:LinkButton>
                            <td class="titleField" style="height: 20px"></td>
                            <td style="height: 20px" width="29%"></td>
                            <td style="height: 20px" width="29%"></td>
                        </tr>
                        <tr>
                            <td class="titleField">File Upload</td>
                            <td width="1%">:</td>
                            <td width="80%" class="titleField">

                                <input onkeypress="return false;" id="DataFile" style="height: 20px" type="file" size="29"
                                    name="File1" runat="server">
                                &nbsp;&nbsp;Minimum Excel 2007 (*.xls / *.xlsx)
                                    <asp:LinkButton ID="LnkDownloadTemplate" runat="server" ToolTip="Download Template Excell">Download Template</asp:LinkButton>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField"></td>
                            <td width="1%"></td>
                            <td width="80%" class="titleField"></td>
                        </tr>
                        <tr>
                            <td class="titleField"></td>
                            <td width="1%"></td>
                            <td width="80%">
                                <asp:Button ID="btnUpload" runat="server" Text="Upload" Width="70px"></asp:Button><asp:Button ID="btnSave" runat="server" Text="Simpan" Width="70px"></asp:Button></td>
                        </tr>
                        <tr>
                            <td class="titleField">
                                <asp:Label ID="lblFilter" runat="server">Filter data</asp:Label></td>
                            <td width="1%">
                                <asp:Label ID="lblFilterSep" runat="server">:</asp:Label></td>
                            <td width="80%">
                                <asp:DropDownList ID="ddlFilter" runat="server" Width="130px" AutoPostBack="True">
                                    <asp:ListItem Value="Semua" Selected="True"></asp:ListItem>
                                    <asp:ListItem Value="Valid"></asp:ListItem>
                                    <asp:ListItem Value="Tidak Valid"></asp:ListItem>
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="height: 11px"></td>
                            <td style="height: 11px"></td>
                            <td style="height: 11px">
                                <asp:Button ID="btnSearch" runat="server" Width="65px" Visible="True" Text="Cari"></asp:Button>
                                <asp:Button ID="btnDownload" runat="server" Width="65px" Visible="True" Text="Download"></asp:Button>
                                <asp:Button ID="btnBatal" Visible="false" runat="server" Width="75px" Text="Batal" CausesValidation="False"></asp:Button>
                                <asp:Button ID="btnNoSales" runat="server" Width="75px" Text="Tanpa Sales" Visible="False"></asp:Button>
                            </td>
                            <td class="titleField" style="height: 11px"></td>
                            <td style="height: 11px"></td>
                            <td style="height: 11px"></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="height: 11px"></td>
                            <td style="height: 11px"></td>
                            <td style="height: 11px"></td>
                            <td class="titleField" style="height: 11px"></td>
                            <td style="height: 11px"></td>
                            <td style="height: 11px"></td>
                        </tr>
                        <tr>
                            <td colspan="6" style="height: 11px">
                                <%--<asp:Label ID="lblTotalRow" Runat="server" />--%>
                                <asp:Label ID="lblMessage" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 100%; height: 11px" colspan="6">
                                <div id="div1" style="overflow: auto; height: 300px">

                                    <asp:DataGrid ID="dgSAPCustomer" runat="server" Width="100%" CellPadding="1" BorderWidth="0px"
                                        CellSpacing="1" BorderColor="Gainsboro" BackColor="#CDCDCD" AutoGenerateColumns="False" AllowSorting="True" AllowCustomPaging="True" PageSize="8"
                                        AllowPaging="True">
                                        <AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
                                        <ItemStyle BackColor="White"></ItemStyle>
                                        <HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Blue"></HeaderStyle>
                                        <Columns>
                                            <asp:BoundColumn Visible="False" DataField="ID" SortExpression="ID" HeaderText="ID"></asp:BoundColumn>
                                            <asp:TemplateColumn HeaderText="No">
                                                <HeaderStyle Width="2%" CssClass="titleTableRsd"></HeaderStyle>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Kode Salesman">
                                                <HeaderStyle CssClass="titleTableRsd"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSalesmanCode" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn SortExpression="CustomerName" HeaderText="Nama Konsumen">
                                                <HeaderStyle CssClass="titleTableRsd"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCustomerName" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn SortExpression="CustomerAddress" HeaderText="Alamat Konsumen">
                                                <HeaderStyle CssClass="titleTableRsd"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCustomerAddress" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn SortExpression="Phone" HeaderText="Telepon">
                                                <HeaderStyle CssClass="titleTableRsd"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPhone" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn SortExpression="InformationType" HeaderText="Sumber Informasi">
                                                <HeaderStyle CssClass="titleTableRsd"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblType" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn SortExpression="BirthDate" HeaderText="Tanggal Lahir">
                                                <HeaderStyle CssClass="titleTableRsd"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBirthDate" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn SortExpression="Sex" HeaderText="Gender">
                                                <HeaderStyle CssClass="titleTableRsd"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGender" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn SortExpression="AgeSegment" HeaderText="Usia">
                                                <HeaderStyle CssClass="titleTableRsd"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAge" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn SortExpression="EstimatedCloseDate" HeaderText="Rencana Pembelian">
                                                <HeaderStyle CssClass="titleTableRsd"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEstimatedCloseDate" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn SortExpression="Status" HeaderText="Status">
                                                <HeaderStyle CssClass="titleTableRsd"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStatus" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn SortExpression="StatusCode" HeaderText="Alasan Batal">
                                                <HeaderStyle CssClass="titleTableRsd"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStatusCode" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn SortExpression="StateCode" HeaderText="Status Akhir">
                                                <HeaderStyle CssClass="titleTableRsd"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStateCode" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn SortExpression="CampaignName" HeaderText="Dealer Event">
                                                <HeaderStyle CssClass="titleTableRsd"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCampaignName" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            
                                            <asp:TemplateColumn SortExpression="VWI_ProfileDetail.Description" HeaderText="Industri">
                                                <HeaderStyle CssClass="titleTableRsd"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBusinessSector" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn SortExpression="InformationSource" HeaderText="Sumber Lead">
                                                <HeaderStyle CssClass="titleTableRsd"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSource" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn SortExpression="CustomerPurpose" HeaderText="Tujuan Konsumen">
                                                <HeaderStyle CssClass="titleTableRsd"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPurpose" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Tipe Kendaraan">
                                                <HeaderStyle Width="100px" CssClass="titleTableRsd"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblVechileTypeCode" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn SortExpression="Qty" HeaderText="Qty">
                                                <HeaderStyle CssClass="titleTableRsd"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblQty" runat="server" Text='<%# Format(DataBinder.Eval(Container, "DataItem.Qty"),"#,##0") %>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn SortExpression="Description" HeaderText="Note">
                                                <HeaderStyle CssClass="titleTableRsd"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNote" runat="server">
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:BoundColumn DataField="ErrorMessage" SortExpression="ErrorMessage" HeaderText="Info">
                                                <HeaderStyle CssClass="titleTableRsd"></HeaderStyle>
                                                <ItemStyle VerticalAlign="Top"></ItemStyle>
                                            </asp:BoundColumn>
                                        </Columns>
                                        <PagerStyle HorizontalAlign="Left" Mode="NumericPages"></PagerStyle>
                                    </asp:DataGrid>
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
