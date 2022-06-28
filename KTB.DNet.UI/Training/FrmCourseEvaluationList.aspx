<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmCourseEvaluationList.aspx.vb" Inherits="FrmCourseEvaluationList" SmartNavigation="False" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>FrmCourseEvaluationList</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <link media="print" href="../WebResources/printstyle.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script language="javascript">
        function DealerSelection(selectedCode) {
            var txtDealer = document.getElementById("txtDealerSelection");

            if (navigator.appName == "Microsoft Internet Explorer") {
                txtDealer.innerText = selectedCode;
            }
            else {
                txtDealer.value = selectedCode;
            }
            //txtDealer.value = selectedCode;
            txtDealer.focus();
        }

        function ShowCategoryManySelection() {
            showPopUp('../PopUp/PopUpCourseMany.aspx', '', 500, 760, CategorySelection);
        }

        function CategorySelection(selectedCategory) {
            var txtKode = document.getElementById("txtKodeKategori");
            txtKode.value = selectedCategory
        }

        function ShowPPClassSelectionMany(areaid) {
            var txtKode = document.getElementById("txtKodeKategori");
            var FiscalYear = document.getElementById("ddlFiscalYear");
            showPopUp('../PopUp/PopUpClassSelectionMany.aspx?areaid=' + areaid + '&CourseCode=' + txtKode.value + '&FiscalYear=' + FiscalYear.value, '', 500, 760, classSelectionMany);
        }
        function ShowPPCertificate() {
            showPopUp('../PopUp/PopUpCertificateConfig.aspx?', '', 500, 760, CertificateSelection);
        }

        function CertificateSelection(selectedCertificate) {
            var tempParam = selectedCertificate.split(';');

            var hdnID = document.getElementById("hdnCerID");
            var txtNama = document.getElementById("txtNamaPenandatangan");
            var txtjabatan = document.getElementById("txtJabatanPenandatangan");
            hdnID.value = tempParam[0];
            txtNama.value = tempParam[1];
            txtjabatan.value = tempParam[2];
        }

        function classSelectionMany(selectedClass) {
            var txtKode = document.getElementById("txtKodeKelas");
            txtKode.value = selectedClass;
        }
        function ShowPPClassSelection() {
            var area = document.getElementById("hdnArea");
            showPopUp('../PopUp/PopUpClassSelection.aspx?areaid=' + area.value, '', 500, 760, classSelection);
        }
        function classSelection(selectedClass) {
            var tempParam = selectedClass.split(';');
            var txtKode = document.getElementById("txtKodeKelas");
            txtKode.value = tempParam[0];
        }
        function ShowPPCourseSelection(obj) {
            showPopUp('../General/../PopUp/PopUpCourse.aspx?category=' + obj.toString(), '', 550, 760, courseSelection);
        }

        function courseSelection(selectedCourse) {
            var txtKodekategori = document.getElementById("txtKodeKategori");
            txtKodekategori.value = selectedCourse;
            //document.getElementById("getCode").click();
        }

        function cetak() {
            window.print();

        }

    </script>
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <asp:HiddenField ID="hdnArea" runat="server" />

        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage" width="740">
                    <asp:Label ID="lblPageTitle" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td height="10"></td>
                <td height="10">
                    <img height="1" src="../images/dot.gif" border="0"></td>
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
                        <tr>
                            <td class="titleField" style="width: 105px; height: 23px" width="105">
                                <asp:Label ID="Label2" runat="server" Width="104px">Kode Organisasi</asp:Label></td>
                            <td style="height: 23px" width="1%">:</td>
                            <td style="width: 175px; height: 23px" width="175">
                                <asp:Label ID="lblKodeOrganisasi" runat="server" Width="128px"></asp:Label></td>
                            <td style="width: 149px; height: 23px" width="149"></td>
                            <td style="width: 186px; height: 23px" width="186"></td>
                            <td style="width: 47px; height: 23px" width="47"></td>
                            <td style="width: 47px; height: 23px" width="47"></td>
                            <td style="width: 126px; height: 23px" width="126"></td>
                            <td style="height: 23px" width="60%"></td>
                        </tr>
                        <tr>
                            <td class="titleField">
                                <asp:Label ID="Label3" runat="server" Width="104px">Nama Organisasi</asp:Label></td>
                            <td style="height: 23px" width="1%">:</td>
                            <td colspan="4">
                                <asp:Label ID="lblNamaOrganisasi" runat="server"></asp:Label></td>
                        </tr>

                        <asp:Panel ID="pnlDealer" Visible="False" runat="server">
                            <tr class="hideTrOnPrint">
                                <td class="titleField" style="width: 105px; height: 23px" width="105">Dealer</td>
                                <td style="height: 23px" width="1%">:</td>
                                <td style="width: 175px; height: 23px" width="175">
                                    <asp:TextBox ID="txtDealerSelection" runat="server" Width="112px"></asp:TextBox>&nbsp;
										<asp:Label class="hideSpanOnPrint" ID="lblPopUpDealer" runat="server" Width="16px">
											<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0"></asp:Label></td>
                                <td style="width: 149px; height: 23px" width="149"></td>
                                <td style="width: 186px; height: 23px" width="186"><strong><strong>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</strong></strong></td>
                                <td style="width: 47px; height: 23px" width="47"></td>
                                <td style="width: 47px; height: 23px" width="47"></td>
                                <td style="width: 126px; height: 23px" width="126"></td>
                                <td style="height: 23px" width="60%"></td>
                            </tr>
                        </asp:Panel>
                        <asp:Panel ID="pnlCriteria" Visible="true" runat="server">
                            <tr class="hideTrOnPrint">
                                <td class="titleField" style="width: 105px; height: 19px" width="105"><strong>Tahun Fiskal</strong></td>
                                <td style="height: 19px" width="1%">:</td>
                                <td style="width: 175px; height: 19px" width="175">
                                    <!--<asp:dropdownlist class="hideDropDownOnPrint" id="ddlYear" runat="server" Width="104px" AutoPostBack="True"></asp:dropdownlist></TD>-->
                                    <%--<asp:TextBox id="txtYear" Width="60px" Runat="server"></asp:TextBox>--%>
                                    <asp:DropDownList ID="ddlFiscalYear" Width="120px" AutoPostBack="true" runat="server" TabIndex="9"></asp:DropDownList>
                                <td style="width: 149px; height: 19px" width="149"></td>
                                <td style="width: 186px; height: 19px" width="186"><strong><strong>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                </strong></strong>
                                </td>
                                <td style="width: 47px; height: 19px" width="47"></td>
                                <td style="width: 47px; height: 19px" width="47"></td>
                                <td style="width: 126px; height: 19px" width="126"></td>
                                <td style="height: 19px" width="60%"></td>
                            </tr>
                            <tr class="hideTrOnPrint">
                                <td class="titleField" style="width: 105px; height: 19px" width="105">Kategori&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                <td style="height: 19px" width="1%">:</td>
                                <td style="width: 175px; height: 19px" width="175">
                                    <!--<asp:dropdownlist id="ddlCategory" runat="server" Width="104px" AutoPostBack="True"></asp:dropdownlist>-->
                                    <asp:TextBox onkeypress="return HtmlCharUniv(event)" ID="txtKodeKategori" runat="server" Width="100"
                                        MaxLength="20"></asp:TextBox>&nbsp;
										<asp:Label ID="lblSearchKodeKategori" runat="server" Width="16px">
											<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0"></asp:Label></td>
                                <td style="width: 149px; height: 19px" width="149"></td>
                                <td style="width: 186px; height: 19px" width="186"></td>
                                <td style="width: 47px; height: 19px" width="47"></td>
                                <td style="width: 47px; height: 19px" width="47"></td>
                                <td style="width: 126px; height: 19px" width="126"></td>
                                <td style="height: 19px" width="60%"></td>
                            </tr>
                            <tr class="hideTrOnPrint">
                                <td class="titleField" style="width: 105px; height: 17px" width="105">Kelas</td>
                                <td style="height: 17px" width="1%">:</td>
                                <td style="width: 175px; height: 17px" width="175">
                                    <!--<asp:dropdownlist id="ddlClass" runat="server" Width="104px" AutoPostBack="True"></asp:dropdownlist>-->
                                    <asp:TextBox onkeypress="return HtmlCharUniv(event)" ID="txtKodeKelas" runat="server" Width="100"
                                        MaxLength="20"></asp:TextBox>&nbsp;
										<asp:Label ID="lblPopUpClass" runat="server" Width="16px">
											<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0"></asp:Label></td>
                                <td style="width: 149px; height: 17px" width="149"></td>
                                <td style="width: 186px; height: 17px" width="186"></td>
                                <td style="width: 47px; height: 17px" width="47"></td>
                                <td style="width: 47px; height: 17px" width="47"></td>
                                <td style="width: 126px; height: 17px" width="126"></td>
                                <td style="height: 17px" width="60%"></td>
                            </tr>

                            <tr class="hideTrOnPrint">
                                <td class="titleField" style="width: 105px; height: 19px" width="105">No Registrasi</td>
                                <td style="height: 19px" width="1%">:</td>
                                <td style="width: 175px; height: 19px" width="175">
                                    <asp:TextBox ID="txtNoReg" Width="60px" runat="server"></asp:TextBox></td>
                                <td style="width: 149px; height: 17px" width="149"></td>
                                <td style="width: 186px; height: 17px" width="186">
                                    <asp:Button class="hideButtonOnPrint" ID="btnCari" runat="server" Width="60px" Text="Cari"></asp:Button></td>
                                <td style="width: 47px; height: 17px" width="47"></td>
                                <td style="width: 47px; height: 17px" width="47"></td>
                                <td style="width: 126px; height: 17px" width="126"></td>
                                <td style="height: 17px" width="60%"></td>
                            </tr>
                        </asp:Panel>
                        <tr>
                            <td class="titleField" style="width: 105px; height: 17px" width="105"></td>
                            <td style="height: 17px" width="1%"></td>
                            <td class="titleField" width="175">
                                <asp:Label ID="lblKodeKelas" runat="server">Kode Kelas</asp:Label></td>
                            <td style="width: 149px; height: 17px" width="149"></td>
                            <td style="width: 186px; height: 17px" width="186">
                                <asp:Label ID="lblClassCode" runat="server" Width="96px"></asp:Label></td>
                            <td style="width: 47px; height: 17px" width="47"></td>
                            <td class="titleField" style="width: 47px; height: 17px" width="47">
                                <asp:Label ID="lblNamaKelas" runat="server" Width="80px">Nama Kelas</asp:Label></td>
                            <td style="width: 126px; height: 17px" width="126"></td>
                            <td style="height: 17px" width="60%">
                                <p>
                                    &nbsp;
										<asp:Label ID="lblClassName" runat="server"></asp:Label>&nbsp;
                                </p>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" style="width: 105px"></td>
                            <td></td>
                            <td class="titleField" style="width: 175px">
                                <asp:Label ID="lblMulai" runat="server">Mulai</asp:Label></td>
                            <td style="width: 149px"></td>
                            <td style="width: 186px">
                                <asp:Label ID="lblStart" runat="server" Width="144px"></asp:Label></td>
                            <td style="width: 47px"></td>
                            <td class="titleField" style="width: 47px">
                                <asp:Label ID="lblSelesai" runat="server" Width="83px">     Selesai</asp:Label></td>
                            <td style="width: 126px"></td>
                            <td>&nbsp;
									<asp:Label ID="lblFinish" runat="server"></asp:Label></td>
                        </tr>
                        <tr class="hideTrOnPrint">
                            <td class="titleField" style="width: 105px; height: 18px"></td>
                            <td style="height: 18px"></td>
                            <td style="width: 175px; height: 18px">
                                <asp:Label ID="Label4" runat="server" ForeColor="White">.</asp:Label></td>
                            <td style="width: 149px; height: 18px"></td>
                            <td style="width: 186px; height: 18px"></td>
                            <td style="width: 47px; height: 18px"></td>
                            <td style="width: 47px; height: 18px"></td>
                            <td style="width: 126px; height: 18px"></td>
                            <td style="height: 18px"></td>
                        </tr>
                        
                    </table>
                </td>
            </tr>
            <tr id="trSertifikat" runat="server">
                <td>
                    <table>
                        <tr class="hideTrOnPrint">
                            <td class="titleField" style="width: 175px; height: 17px" >Penandatangan Sertifikat</td>
                            <td style="height: 17px" width="1%">:</td>
                            <td colspan="4">
                                <asp:HiddenField ID="hdnCerID" runat="server" />
                                <asp:TextBox onkeypress="return HtmlCharUniv(event)" ID="txtNamaPenandatangan" runat="server" Width="175"
                                    MaxLength="20">
                                </asp:TextBox>
                                <asp:TextBox onkeypress="return HtmlCharUniv(event)" ID="txtJabatanPenandatangan" runat="server" Width="150"
                                    MaxLength="20"></asp:TextBox>&nbsp;
										<asp:Label ID="lblCerConfig" runat="server" Width="16px">
											<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0">
                                        </asp:Label></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <asp:DataGrid ID="dtgClassRegistration" runat="server" Width="100%" ForeColor="Gray" AutoGenerateColumns="False"
                        BorderColor="#CDCDCD" BorderStyle="None" BorderWidth="1px" BackColor="#CDCDCD" CellPadding="3" AllowSorting="True"
                        PageSize="25">
                        <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                        <AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
                        <ItemStyle BackColor="White"></ItemStyle>
                        <HeaderStyle Font-Bold="True" ForeColor="White" BorderColor="#E0E0E0" BackColor="#CC3333"></HeaderStyle>
                        <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
                        <Columns>
                            <asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
                                <HeaderStyle CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="No">
                                <HeaderStyle Width="3%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblNo" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn SortExpression="TrTrainee.ID" HeaderText="No. Reg">
                                <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblSalesmanCode" runat="server"></asp:Label>
                                    <asp:Label ID="lblRegCode" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn SortExpression="TrTrainee.Name" HeaderText="Nama Siswa">
                                <HeaderStyle Width="12%" CssClass="titleTableService"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblTraineeName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TrTrainee.Name") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn SortExpression="Dealer.DealerName" HeaderText="Dealer">
                                <HeaderStyle Width="14%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerName") %>' ID="lblDealerName">
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn SortExpression="Dealer.City.CityName" HeaderText="Kota">
                                <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblCity" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.City.CityName") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn SortExpression="InitialTest" HeaderText="Initial">
                                <HeaderStyle Width="4%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblInitial" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Test 1">
                                <HeaderStyle HorizontalAlign="Center" Width="3%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblTest1" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Test 2">
                                <HeaderStyle HorizontalAlign="Center" Width="3%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblTest2" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Test 3">
                                <HeaderStyle HorizontalAlign="Center" Width="3%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblTest3" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Test 4">
                                <HeaderStyle HorizontalAlign="Center" Width="3%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblTest4" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Test 5">
                                <HeaderStyle HorizontalAlign="Center" Width="3%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblTest5" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Test 6">
                                <HeaderStyle HorizontalAlign="Center" Width="3%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblTest6" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Test 7">
                                <HeaderStyle HorizontalAlign="Center" Width="3%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="LblTest7" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn Visible="True" HeaderText="Test 8">
                                <HeaderStyle HorizontalAlign="Center" Width="3%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="LblTest8" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn Visible="True" HeaderText="Test 9">
                                <HeaderStyle HorizontalAlign="Center" Width="3%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="LblTest9" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn Visible="False" HeaderText="Test 10">
                                <HeaderStyle HorizontalAlign="Center" Width="3%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="LblTest10" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn SortExpression="FinalTest" HeaderText="Final">
                                <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblFinal" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn SortExpression="Avarage" HeaderText="Rata-Rata">
                                <HeaderStyle Width="6%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblAverage" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn SortExpression="Rank" HeaderText="Rank">
                                <HeaderStyle Width="4%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblRank" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn SortExpression="Status" HeaderText="Status">
                                <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblStatus" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtnView" runat="server" Width="20px" Text="Lihat" CommandName="View" CausesValidation="False">
											<img src="../images/detail.gif" class="hideLinkButtonOnPrint" border="0" alt="Lihat"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
                    </asp:DataGrid>
                    <div></div>
                </td>
            </tr>
            <tr>
                <td style="height: 40px" align="center">
                    <input class="hideButtonOnPrint" id="btnCetak" style="width: 42px; height: 21px" onclick="cetak()"
                        type="button" value="Cetak" runat="server">
                    <asp:Button class="hideButtonOnPrint" ID="btnDownLoad" runat="server" Text="Download" CausesValidation="False"></asp:Button>
                    <asp:Button class="hideButtonOnPrint" ID="btnSave" runat="server" Width="136px" Text="Hitung Ulang  dan Simpan"></asp:Button>
                    <asp:Button class="hideButtonOnPrint" ID="btnSubmit" runat="server" Text="Submit" CausesValidation="False"></asp:Button>
                    <asp:Button class="hideButtonOnPrint" ID="btnCancel" runat="server" Text="Batal Submit" CausesValidation="False"></asp:Button>
                    <asp:Button class="hideButtonOnPrint" ID="btnBack" runat="server" Text="Kembali" CausesValidation="False"></asp:Button>
                </td>
            </tr>
            <tr>
                <td style="height: 15px"></td>
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
</body>
</html>
