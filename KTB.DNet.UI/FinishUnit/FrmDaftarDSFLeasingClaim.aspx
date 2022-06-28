<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmDaftarDSFLeasingClaim.aspx.vb" Inherits="FrmDaftarDSFLeasingClaim" SmartNavigation="False" MaintainScrollPositionOnPostback="true"%>

<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>Daftar DSF Leasing Claim</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <link href="../WebResources/ImagePopup.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script language="javascript" src="../WebResources/FormFunctions.js"></script>
    <script language="javascript">
        function ShowPPDealerSelection() {
            //showPopUp('../General/../PopUp/PopUpDealerSelection.aspx', '', 500, 760, DealerSelection);
            showPopUp('../General/../Benefit/PopUpDealerSelectionBenefit.aspx', '', 500, 760, DealerSelection);
        }

        function DealerSelection(selectedDealer) {
            var txtDealerSelection = document.getElementById("txtKodeDealer");
            txtDealerSelection.value = selectedDealer;
        }

        function isSuccesUpload(getSuccesUpload) {
            var dgDSFClaim = document.getElementById("dgDSFClaim");
            var tempParam = getSuccesUpload.split(';');
            var hdnAlasanBatal = document.getElementById("hdnAlasanBatal");
            var hdnDSFLeasingClaimID = document.getElementById("hdnDSFLeasingClaimID");
            hdnDSFLeasingClaimID.value = tempParam[1]
            hdnAlasanBatal.value = tempParam[2]

            if (getSuccesUpload = '1') {
                var btn = document.getElementById('btnSuccessUpload');
                btn.click();
            }
        }
        function SetPath(obj) {
            document.getElementById("lblPath").innerText = obj.lowsrc;
        }

        function ShowEvidenceImage(obj) {
            var fraImageTest = document.getElementById("fraImageTest");
            fraImageTest.src = "../WebResources/GetImageGlobal.aspx?file=" + obj.lowsrc + "&hg=200&wd=200&type=ImageFile";

            var divImageTest = document.getElementById("imgBox");
            if (navigator.appName != "Microsoft Internet Explorer") {
                divImageTest = obj.parentNode.parentNode.childNodes[1];
            }
            divImageTest.style.visibility = "visible";
            divImageTest.innerHTML = '';
            divImageTest.appendChild(fraImageTest);
            divImageTest.style.left = (getElementLeft(obj)) + 'px';
            divImageTest.style.top = (getElementTop(obj)) + 'px';

            document.getElementById("lblPath").innerText = obj.lowsrc;
        }


        function HideEvidenceImage(obj) {
            var divImageTest = document.getElementById("imgBox");
            if (navigator.appName != "Microsoft Internet Explorer") {
                divImageTest = obj.parentNode.parentNode.childNodes[1];
            }
            divImageTest.style.visibility = "hidden";
        }

        function getElementLeft(elm) {
            var x = 0;
            x = elm.offsetLeft;
            elm = elm.offsetParent;
            while (elm != null) {
                x = parseInt(x) + parseInt(elm.offsetLeft) - 34;
                elm = elm.offsetParent;
            }
            return x;
        }

        function getElementTop(elm) {
            var y = 0;
            y = elm.offsetTop;
            elm = elm.offsetParent;
            while (elm != null) {
                y = parseInt(y) + parseInt(elm.offsetTop) - 24;
                elm = elm.offsetParent;
            }
            return y;
        }

        function CheckAll(aspCheckBoxID, checkVal) {
            re = new RegExp(':' + aspCheckBoxID + '$')
            for (i = 0; i < document.forms[0].elements.length; i++) {
                elm = document.forms[0].elements[i]
                if (elm.type == 'checkbox') {
                    if (re.test(elm.name)) {
                        elm.checked = checkVal
                    }
                }
            }
            document.getElementById('btnChkAll').click();
        }

    </script>
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <table id="Table2" cellspacing="5" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage" style="height: 17px" colspan="3">SALES CAMPAIGN - Daftar DSF Leasing Claim </td>
            </tr>
            <tr>
                <td background="../images/bg_hor.gif" colspan="3" height="1">
                    <img height="1" src="../images/bg_hor.gif" border="0"></td>
            </tr>
            <tr>
                <td style="height: 6px" colspan="3" height="6">
                    <img height="1" src="../images/dot.gif" border="0"></td>
            </tr>
            <tr valign="top">
                <td class="titleField" width="10%">Kode Dealer&nbsp;</td>
                <td style="text-align: center" width="2%">:</td>
                <td>
                    <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" ID="txtKodeDealer" onblur="omitSomeCharacter('txtKodeDealer','<>?*%$;')"
                        runat="server" Width="220px" TextMode="MultiLine" Height="30px"></asp:TextBox>
                    &nbsp;<asp:Label ID="lblPopUpDealer" runat="server" Width="16px">
							        <img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0"></asp:Label>
                    <asp:Label ID="lblDelerSession" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="titleField">Periode</td>
                <td style="text-align: center" width="2%">:</td>
                <td>
                    <asp:CheckBox id="cbDate" runat="server" Width="2px"></asp:CheckBox>
                    <asp:DropDownList ID="ddlPeriodeMonth" runat="server" style="margin-right: 15px"></asp:DropDownList>
                    <asp:DropDownList ID="ddlPeriodeYear" runat="server"></asp:DropDownList>
                </td>
            </tr>
            <tr runat="server" id="trLeasingName">
                <td class="titleField" width="15%">Nama Leasing&nbsp;</td>
                <td style="text-align: center" width="2%">:</td>
                <td>
                    <asp:Label ID="lblDealerName" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="titleField" width="15%">Kategori</td>
                <td style="text-align: center" width="2%">:</td>
                <td><asp:DropDownList ID="ddlKategori" runat="server" AutoPostBack="True"></asp:DropDownList></td>
            </tr>
            <tr>
                <td class="titleField" width="15%">Tipe</td>
                <td style="text-align: center" width="2%">:</td>
                <td><asp:DropDownList ID="ddlTipe" runat="server"></asp:DropDownList></td>
            </tr>
            <tr>
                <td class="titleField" width="10%">Nomor Registrasi&nbsp;</td>
                <td style="text-align: center" width="2%">:</td>
                <td>
                    <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" ID="txtRegNumber" onblur="omitSomeCharacter('txtRegNumber','<>?*%$;')"
                        runat="server" Width="242px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="titleField" width="10%">Nomor Rangka&nbsp;</td>
                <td style="text-align: center" width="2%">:</td>
                <td>
                    <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" ID="txtChassisNumber" onblur="omitSomeCharacter('txtChassisNumber','<>?*%$;')"
                        runat="server" Width="242px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="titleField" width="10%">Nomor Agreement&nbsp;</td>
                <td style="text-align: center" width="2%">:</td>
                <td>
                    <asp:TextBox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" ID="txtAgreementNo" onblur="omitSomeCharacter('txtAgreementNo','<>?*%$;')"
                        runat="server" Width="242px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="titleField" width="10%">
                    Tanggal Upload</td>
                <td style="text-align: center" width="2%">:</td>
                <td>
                    <table>
                        <tr>
                            <td><asp:CheckBox ID="chkTglUpload" TextAlign="Right" runat="server" Text=""></asp:CheckBox></td>
                            <td>
                                <cc1:IntiCalendar ID="icUploadDate" runat="server" TextBoxWidth="60"></cc1:IntiCalendar></td>
                            <td>
                                <asp:Label runat="server">&nbsp;s.d&nbsp;</asp:Label></td>
                            <td>
                                <cc1:IntiCalendar ID="icUploadDateTo" runat="server" TextBoxWidth="60"></cc1:IntiCalendar></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="titleField" width="10%">
                    Tanggal Validasi Kendaraan</td>
                <td style="text-align: center" width="2%">:</td>
                <td>
                    <table>
                        <tr>
                            <td><asp:CheckBox ID="chkValidateTime" TextAlign="Right" runat="server" Text=""></asp:CheckBox></td>
                            <td>
                                <cc1:IntiCalendar ID="icValidateTimeFrom" runat="server" TextBoxWidth="60"></cc1:IntiCalendar></td>
                            <td>
                                <asp:Label runat="server">&nbsp;s.d&nbsp;</asp:Label></td>
                            <td>
                                <cc1:IntiCalendar ID="icValidateTimeTo" runat="server" TextBoxWidth="60"></cc1:IntiCalendar></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="titleField" width="10%">Status&nbsp;</td>
                <td style="text-align: center" width="2%">:</td>
                <td>
                    <asp:DropDownList ID="ddlStatus" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="3"><hr /></td>
            </tr>
            <tr>
                <td class="titleField"><asp:Label ID="lblTotalRecords" Font-Bold="true" runat="server"></asp:Label></td>
                <td style="text-align: center" width="2%">&nbsp;</td>
                <td>
                    <asp:Button ID="btnCari" runat="server" Text="Cari" Width="60px"></asp:Button>&nbsp;
                    <asp:Button ID="btnBatal" runat="server" Text="Batal" Width="60px"></asp:Button>&nbsp;
                    <asp:Button ID="btnDownload" runat="server" Text="Download" Width="90px"></asp:Button>
                    <asp:TextBox ID="lblPath" runat="server" Width="0px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <table id="Table1" cellspacing="1" cellpadding="2" width="100%" border="0">
                        <tr>
                            <td valign="top" colspan="6">
                                <div id="div1" style="overflow: auto; max-height: 440px">
                                    <asp:DataGrid ID="dgDSFClaim" runat="server" Width="100%" AllowPaging="True" AllowSorting="True"
                                        PageSize="15" AllowCustomPaging="True" AutoGenerateColumns="False" BackColor="#CDCDCD"
                                        BorderColor="Gainsboro" CellSpacing="1" BorderWidth="0px"
                                        CellPadding="3" DataKeyField="ID">
                                        <AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
                                        <ItemStyle BackColor="White"></ItemStyle>
                                        <HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="Blue"></HeaderStyle>
                                        <Columns>
                                            <asp:TemplateColumn HeaderText="">
                                                <HeaderStyle Width="2%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <HeaderTemplate>
													<asp:CheckBox ID="chkAllItems" Runat="server" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkItemChecked" AutoPostBack="true" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False">
                                                <HeaderStyle Width="2%" CssClass="titleTableMrk"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                            </asp:BoundColumn>
                                            <asp:TemplateColumn HeaderText="No">
                                                <HeaderStyle Width="2%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNo" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Kode Dealer" SortExpression="Dealer.DealerCode">
                                                <HeaderStyle Width="7%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDealerCode" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Nama Dealer" SortExpression="Dealer.DealerName">
                                                <HeaderStyle Width="15%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDealerName" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="No. Registrasi" SortExpression="RegNumber">
                                                <HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRegNumber" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Periode" SortExpression="CollectionPeriodMonth">
                                                <HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPeriode" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>

                                            <asp:TemplateColumn HeaderText="No. Agreement" SortExpression="AgreementNo">
                                                <HeaderStyle Width="12%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAgreementNo" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="No. Rangka" SortExpression="ChassisMaster.ChassisNumber">
                                                <HeaderStyle Width="12%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblChassisNumber" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="No. Mesin" SortExpression="ChassisMaster.EngineNumber">
                                                <HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEngineNumber" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Nama Konsumen" SortExpression="CustomerName">
                                                <HeaderStyle Width="15%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCustomerName" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="ATPM Subsidy" SortExpression="ATPMSubsidy">
                                                <HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblATPMSubsidy" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="No. SKD" SortExpression="SKDNumber">
                                                <HeaderStyle Width="12%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSKDNumber" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Tgl Approval SKD" SortExpression="SKDApprovalDate">
                                                <HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSKDApprovalDate" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Status" SortExpression="Status">
                                                <HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStatus" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Remark Dealer" SortExpression="RemarkByDealer">
                                                <HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRemarkByDealer" Text='<%# DataBinder.Eval(Container, "DataItem.RemarkByDealer")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Remark DSF" SortExpression="RemarkByDSF">
                                                <HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRemarkByMKS" Text='<%# DataBinder.Eval(Container, "DataItem.RemarkByDSF")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Dok Tolak Dealer">
                                                <HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <div id="imgbox">
                                                        <iframe id="fraImageTest" width="200px" height="200px" frameborder="0"></iframe>
                                                    </div>
                                                    <asp:LinkButton ID="lnkbtnDownloadDealer" runat="server" CommandName="lnkbtnDownloadDealer">
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Dok Tolak DSF">
                                                <HeaderStyle Width="10%" CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <div id="imgbox">
                                                        <iframe id="fraImageTest" width="200px" height="200px" frameborder="0"></iframe>
                                                    </div>
                                                    <asp:LinkButton ID="lnkbtnDownloadDSF" runat="server" CommandName="lnkbtnDownloadDSF">
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn>
                                                <HeaderStyle CssClass="titleTableSales"></HeaderStyle>
                                                <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkbtnUpload" runat="server" Width="20px" Text="Upload" CausesValidation="False" CommandName="Upload">
													    <img alt="Upload" src="../images/icon_evid.gif" border="0">
                                                    </asp:LinkButton>
                                                    <asp:LinkButton ID="lnkbtnDelete" OnClientClick="return confirm('Apakah anda yakin menghapus data ini?');" runat="server" Width="20px" Text="Hapus" CausesValidation="False" CommandName="Delete" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.ID")%>'>
															<img alt="Delete"  src="../images/trash.gif"
																border="0">
                                                    </asp:LinkButton>
                                                    <asp:Label ID="lblHistoryStatus" runat="server">
												        <img src="../images/popup.gif" style="cursor:hand" alt="Histori Status">
                                                    </asp:Label>
                                                    <INPUT id="hdnValNew" type="hidden" value="x" runat="server" NAME="hdnValNew">
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                        </Columns>
                                        <PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
                                    </asp:DataGrid>
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="3"><asp:Label ID="lblUpdateStatus" runat="server">Mengubah Status :  </asp:Label><asp:DropDownList ID="ddlstatus2" runat="server"></asp:DropDownList>
                    &nbsp;
                        <asp:Button ID="btnUpdateStatus" runat="server" Text="Proses" Width="60px" OnClientClick="return confirm('Apakah anda mau update status?');" ></asp:Button>
                    <asp:HiddenField ID="arrayCheck" runat="server" />
                    <asp:Button ID="btnSuccessUpload" runat="server" Width="1px" Text="" Style="display: none"></asp:Button>
                    <INPUT id="hdnStatusOld" type="hidden" value="-1" runat="server" NAME="hdnStatusOld">
                    <INPUT id="hdnValNew" type="hidden" value="-2" runat="server" NAME="hdnValNew">
                    <INPUT id="hdnDSFLeasingClaimID" type="hidden" runat="server" NAME="hdnDSFLeasingClaimID">
                    <INPUT id="hdnAlasanBatalAll" type="hidden" runat="server" NAME="hdnAlasanBatalAll">
                    <INPUT id="hdnAlasanBatal" type="hidden" runat="server" NAME="hdnAlasanBatal">
                    <INPUT id="hdnAllID" type="hidden" runat="server" NAME="hdnAllID">
                    
                    <asp:Button ID="btnChkAll" Style="display: none" runat="server" ></asp:Button>&nbsp;&nbsp;
                </td>
            </tr>
        </table>
    </form>
</body>

</html>
