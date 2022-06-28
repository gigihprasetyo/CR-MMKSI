<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frmDSFUploadLeasingClaim.aspx.vb" Inherits=".frmDSFUploadLeasingClaim" %>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<html>
    <head>
		<title>FrmUploadText</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<%--<script language="javascript" src="../WebResources/FormFunctions.js"></script>--%>
        <style type="text/css">

            table#Table2 tr {
                padding-bottom:0.25em;
            }
            .hiddencol {
                display: none;
            }

            .overlay {
                left: 0;
                top: 0;
                width: 100%;
                height: 100%;
                position: fixed;
                background: #222;
                opacity: 0.3;
            }

            .overlay__inner {
                left: 0;
                top: 0;
                width: 100%;
                height: 100%;
                position: absolute;
            }

            .overlay__content {
                left: 50%;
                position: absolute;
                top: 50%;
                transform: translate(-50%, -50%);
            }

            .spinner {
                width: 75px;
                height: 75px;
                display: inline-block;
                border-width: 2px;
                border-color: rgba(255, 255, 255, 0.05);
                border-top-color: #fff;
                animation: spin 1s infinite linear;
                border-radius: 100%;
                border-style: solid;
            }

            @keyframes spin {
              100% {
                transform: rotate(360deg);
              }
            }

        </style>
        <script type="text/javascript">
            function closepanel2() {
                document.getElementById('Panel1').style.display = ''
                document.getElementById('Panel2').style.display = 'none'
            }
            function GetSelectedValue() {
                var table;
                var bcheck = false;
                table = document.getElementById('dgGridDetil');
                var val = '';
                var value = ""; var valueshow = ""; var valueformula = ""
                for (i = 1; i < table.rows.length; i++) {

                    var radioBtn = table.rows[i].cells[0].getElementsByTagName('INPUT')[0];
                    if (radioBtn != null && radioBtn.checked) {
                        if (navigator.appName == "Microsoft Internet Explorer") {
                            value = table.rows[i].cells[1].innerText;
                            valueshow = table.rows[i].cells[4].innerText;
                            valueformula = table.rows[i].cells[5].innerText;
                            bcheck = true;
                        }
                        else {
                            value = replace(table.rows[i].cells[1].getElementsByTagName("span")[0].innerHTML, ' ', '');
                            valueshow = replace(table.rows[i].cells[4].getElementsByTagName("span")[0].innerHTML, ' ', '');
                            valueformula = replace(table.rows[i].cells[5].getElementsByTagName("span")[0].innerHTML, ' ', '');
                            bcheck = true;
                        }
                        break;
                    }
                }

                if (bcheck) {
                    document.getElementById('txtIdDetailMasterShow').value = valueshow
                    closepanel2()
                }
                else {
                    alert("Silahkan pilih ");
                }
            }
            function getElement(tipeElement, IdElement) {
                var selectbox;
                var inputs = document.getElementsByTagName(tipeElement);

                for (var i = 0; i < inputs.length; i++) {
                    if (inputs[i].id.indexOf(IdElement) > -1) {
                        selectbox = inputs[i]
                        break;
                    }
                }
                return selectbox;
            }

            function hideLoader() {
                var _loader = document.getElementById('loader');
                _loader.style.display = 'none';
            }

            function showLoader() {
                var _loader = document.getElementById('loader');
                _loader.style.display = 'block';
            }

        </script>
	</head>
    <body ms_positioning="GridLayout">
        <%--<div id="loading">
            <img src="../images/loader.gif" width="100px" height="100px" />
        </div>--%>

        <div class="overlay" id="loader">
            <div class="overlay__inner">
                <div class="overlay__content"><span class="spinner"></span></div>
            </div>
        </div>

        <form id="Form1" method="post" runat="server">
            <asp:Panel ID="panel1" runat="server">
                <div id="form-header">
                    <table id="tbl-form-header" cellspacing="5" cellpadding="0" width="100%" border="0">
                        <tr>
					        <td class="titlePage" style="height: 17px" colspan="2">CAMPAIGN&nbsp;-&nbsp;UPLOAD CHASSIS CLAIM</td>
				        </tr>
                        <tr>
                            <td background="../images/bg_hor.gif" colspan="2" height="1">
                                <img height="1" src="../images/bg_hor.gif" border="0"></td>
                        </tr>
                        <tr>
                            <td style="height: 6px" colspan="2" height="6">
                                <img height="1" src="../images/dot.gif" border="0"></td>
                        </tr>
                    </table>
                </div>
                <div id="form-body">
                    <table id="Table2" cellspacing="5" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td class="titleField" style="display:none;">Pilihan Claim&nbsp;</td>
                            <td style="display:none;">
                                <asp:DropDownList ID="ddlPilihanClaim" runat="server"></asp:DropDownList>
                            </td>
                            <td class="titleField"><label>Tanggal Claim&nbsp;</label></td>
                            <td>
                                <cc1:inticalendar id="Inticalendar1" runat="server" Enabled="false" TextBoxWidth="60"></cc1:inticalendar>
                            </td>
                        </tr>
                        <tr>
                            
                            <%--<td>
                                <cc1:inticalendar id="icClaimDate" runat="server" Enabled="false" TextBoxWidth="60"></cc1:inticalendar>
                            </td>--%>
                        </tr>
                        <tr>
                            <td class="titleField" width="20%" style="display:none;">No Surat&nbsp;</td>
                            <td style="display:none;">
                                <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" ID="txtIdDetailMasterShow" onblur="omitSomeCharacter('txtKeyWords','<>?*%$;')" runat="server" Width="242px"></asp:TextBox>
                                <asp:Button ID="btnRefClaim" runat="server" Text="Ref Claim" Width="60px"></asp:Button>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" width="20%">Upload File&nbsp;</td>
                            <td>
                                <asp:FileUpload ID="fileUploadExcel" runat="server" Width="242px"/>
                                <asp:Button ID="btnUpload" runat="server" Text="Upload"/>
                                <%--<asp:Button ID="btnUpload" runat="server" Text="Upload" OnClientClick="showLoader();" />--%>
                                <label style="color:red"><i>*Max.size 70 KB</i></label>
                            </td>
                    
                        </tr>
                        <tr>
                            <td class="titleField" width="20%"></td>
                            <td colspan="3">
                                <asp:LinkButton ID="LinkDownload" runat="server">Download Template</asp:LinkButton>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" width="20%">Status Upload</td>
                            <td>
                                <asp:DropDownList ID="ddlStatusUpload" runat="server"></asp:DropDownList>&nbsp;<asp:Button ID="btnSearch" runat="server" Text="Cari" Width="60px"></asp:Button>
                            </td>
                        </tr>
                        <tr> 
                            <td class="titleField" width="20%">Total Record</td>
                            <td>
                                <asp:Label runat="server" ID="lblRecordCount"></asp:Label>
                                &nbsp;
                                <asp:Label runat="server" ID="Label1">rows</asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <table id="Table1" cellspacing="1" cellpadding="2" width="1200px" border="0">
                                    <tr>
                                        <td valign="top" colspan="6">
                                            <div id="div1" style="overflow: auto; max-height: 440px">
                                                <asp:DataGrid ID="dgTable" runat="server" Width="100%" AllowPaging="True" AllowSorting="False"
                                                    PageSize="5" AllowCustomPaging="True" AutoGenerateColumns="False" BackColor="#CDCDCD" BorderColor="Gainsboro" CellSpacing="1" BorderWidth="0px"
                                                    CellPadding="3" DataKeyField="ID" ShowHeader="true">
                                                    <AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
                                                    <ItemStyle BackColor="White"></ItemStyle>
                                                    
                                                    <HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Blue"></HeaderStyle>
                                                    <Columns>
                                                        <asp:TemplateColumn HeaderText="" Visible="false">
                                                            <HeaderStyle Width="2px" CssClass="titleTableSales"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                            <HeaderTemplate>
                                                                <input id="chkAllItems" onclick="CheckAll('cbAllGrid')"
                                                                    type="checkbox" style="display: none">
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="cbAllGrid" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="No">
                                                            <HeaderStyle Width="10px" CssClass="titleTableSales"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblNoGrid" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="Dealer Name">
                                                            <HeaderStyle Width="300px" CssClass="titleTableSales"></HeaderStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDealerNameGrid" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="Chassis Number">
                                                            <HeaderStyle Width="15px" CssClass="titleTableSales"></HeaderStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblChassisNumberGrid" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="Engine Number">
                                                            <HeaderStyle Width="15px" CssClass="titleTableSales"></HeaderStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblEngineNumberGrid" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="Asset Seq No">
                                                            <HeaderStyle Width="15px" CssClass="titleTableSales"></HeaderStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblAssetSeqNoGrid" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="Agreement Number">
                                                            <HeaderStyle Width="15px" CssClass="titleTableSales"></HeaderStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblAgreementNumberGrid" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="SKD Number">
                                                            <HeaderStyle Width="15px" CssClass="titleTableSales"></HeaderStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSKDNumberGrid" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="SKD Date">
                                                            <HeaderStyle Width="15px" CssClass="titleTableSales"></HeaderStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSKDDateGrid" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="SKD Approval Date">
                                                            <HeaderStyle Width="15px" CssClass="titleTableSales"></HeaderStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSKDApprovalDateGrid" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="Go-Live Date">
                                                            <HeaderStyle Width="15px" CssClass="titleTableSales"></HeaderStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblGoLiveDateGrid" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="ATPM Subsidy">
                                                            <HeaderStyle Width="15px" CssClass="titleTableSales"></HeaderStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblATPMSubsidyGrid" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="Supplier Name">
                                                            <HeaderStyle Width="15px" CssClass="titleTableSales"></HeaderStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSupplierNameGrid" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="Unit">
                                                            <HeaderStyle Width="15px" CssClass="titleTableSales"></HeaderStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblUnitGrid" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="Program Name">
                                                            <HeaderStyle Width="15px" CssClass="titleTableSales"></HeaderStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblProgramNameGrid" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="Collection Period (MMYYYY)">
                                                            <HeaderStyle Width="15px" CssClass="titleTableSales"></HeaderStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCollectionPeriodGrid" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="Total Down Payment">
                                                            <HeaderStyle Width="15px" CssClass="titleTableSales"></HeaderStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTotalDownPaymentGrid" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="Total Amount Lease">
                                                            <HeaderStyle Width="15px" CssClass="titleTableSales"></HeaderStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTotalAmountLeaseGrid" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="Period Lease (Months)">
                                                            <HeaderStyle Width="15px" CssClass="titleTableSales"></HeaderStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPeriodLeaseGrid" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="% Interest Lease">
                                                            <HeaderStyle Width="15px" CssClass="titleTableSales"></HeaderStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblInterestLeaseGrid" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="Insurance">
                                                            <HeaderStyle Width="15px" CssClass="titleTableSales"></HeaderStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblInsuranceGrid" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="Type Insurance">
                                                            <HeaderStyle Width="15px" CssClass="titleTableSales"></HeaderStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTypeInsuranceGrid" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="Status Upload" Visible="true">
                                                            <HeaderStyle Width="10px" CssClass="titleTableSales"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblStatusUpload" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn>
                                                            <HeaderStyle ForeColor="White" Width="100px" CssClass="titleTableSales"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkbtnEdit" runat="server" CommandName="Edit" CausesValidation="False" Visible="false">
												                                                <img src="../images/edit.gif"  border="0" alt="Ubah"></asp:LinkButton>
                                                                <asp:LinkButton ID="lnkbtnDelete" runat="server" CommandName="Delete" CausesValidation="False">
												                    <img src="../images/trash.gif" border="0" alt="Hapus" onclick="return confirm('Yakin ingin menghapus data ini?');">
                                                                </asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                    </Columns>
                                                    <PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
                                                </asp:DataGrid>
                                            </div>

                                            <asp:Panel ID="Panel6" runat="server">
                                                <asp:Label ID="lblpanel6" runat="server" Text="" Width="100%"></asp:Label>
                                            </asp:Panel>

                                            <asp:Panel ID="Panel7" runat="server">
                                                <asp:Label ID="lblpanel7" runat="server" Text="" Width="100%"></asp:Label>
                                            </asp:Panel>

                                        </td>
                                    </tr>
                                    <%--<tr>
                                        <td style="width:10%"><asp:CheckBox runat="server" AutoPostBack="true" OnCheckedChanged="cbxDisclaimer_CheckedChanged" ID="cbxDisclaimer" Visible="false" Text="Dengan ini Leasing menyatakan setuju untuk melakukan Validasi nomor rangka"/></td>
                                    </tr>--%>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:CheckBox runat="server" AutoPostBack="true" OnCheckedChanged="cbxDisclaimer_CheckedChanged" ID="cbxDisclaimer" Visible="false" Text="Dengan ini Leasing menyatakan setuju untuk melakukan Validasi nomor rangka"/>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table>
                                    <tr>
                                        <td style="width:5%"><asp:Button ID="btnDownload" runat="server" Width="96px" Text="Download" Visible="false"></asp:Button></td>
                                        <td style="width:5%"><asp:Button ID="btnSave" runat="server" Width="96px" Text="Simpan" Visible="false"></asp:Button></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
            </asp:Panel>

            <asp:Panel ID="Panel2" runat="server">
                <div id="areahidden">

                <div class="titlePage">
                    Daftar Referensi
                </div>

                <div style="background-image: url(../images/bg_hor.gif)">
                    <img border="0" src="../images/bg_hor.gif">
                </div>
                <div>
                    <img height="1" border="0" src="../images/dot.gif">
                </div>
                <div>
                    <asp:DataGrid ID="dgGridDetil" runat="server" Width="100%" 
                        AllowPaging="True" AllowSorting="True" PageSize="15" AllowCustomPaging="True" 
                        AutoGenerateColumns="False" BackColor="#CDCDCD" BorderColor="Gainsboro" 
                        CellSpacing="1" BorderWidth="0px"
                        CellPadding="3" DataKeyField="ID">
                        <AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
                        <ItemStyle BackColor="White"></ItemStyle>
                        <HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Blue"></HeaderStyle>
                        <Columns>
                            <asp:TemplateColumn>
                                <HeaderStyle Width="1%" CssClass="titleTableSales"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>

                                <ItemTemplate>
                                    &nbsp;
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="ID">
                                <HeaderStyle Width="0px" CssClass="titleTableSales hiddencol"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" CssClass="hiddencol"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblIDoGridDetil" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="No">
                                <HeaderStyle Width="2%" CssClass="titleTableSales"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblNoGridDetil" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="No Reg Benefit">
                                <HeaderStyle Width="15%" CssClass="titleTableSales"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblNoRegBenefitGridDetil" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="No Surat">
                                <HeaderStyle Width="15%" CssClass="titleTableSales"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblnnosuratGridDetil" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Formula">
                                <HeaderStyle Width="15%" CssClass="titleTableSales hiddencol"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" CssClass="hiddencol"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblformula" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Deskripsi">
                                <HeaderStyle Width="15%" CssClass="titleTableSales"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lbldeskripsiGridDetil" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        <PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
                    </asp:DataGrid>
                </div>

                <input style="width: 60px" onclick="GetSelectedValue()" type="button" value="Pilih" name="btnChoose" />
                &nbsp;
                <input style="width: 60px" onclick="closepanel2()" type="button" value="Tutup" />
            </div>
            </asp:Panel>
            
        </form>
    </body>
</html>
