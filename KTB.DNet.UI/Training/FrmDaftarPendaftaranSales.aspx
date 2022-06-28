<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmDaftarPendaftaranSales.aspx.vb" Inherits="FrmDaftarPendaftaranSales" SmartNavigation="False" %>

<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>List Pendaftaran</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript">
        function popUpClassInformation(kode) {
            var url = '../PopUp/PopUpClassInformation.aspx?kode=' + kode;
            showPopUp(url, '', 320, 440, null);
        }

        function DealerSelection(selectedCode) {
            var txtDealer = document.getElementById("txtDealerSearchCode");
            txtDealer.value = selectedCode
            txtDealer.focus();
        }

        function ShowCategoryManySelection() {
            showPopUp('../PopUp/PopUpCourseCheck.aspx?category=sales', '', 500, 760, CategorySelection);
        }

        function CategorySelection(selectedCategory) {
            var txtKode = document.getElementById("txtKodeKategori");
            txtKode.value = selectedCategory
        }

        function ShowPPClassSelection() {
            var txtKode = document.getElementById("txtKodeKategori");
            var area = document.getElementById("hdnAreaID");
            showPopUp('../PopUp/PopUpClassSelection.aspx?areaid=1&CourseCode=' + txtKode.value, '', 500, 760, classSelection);
        }

        function classSelection(selectedClass) {
            var tempParam = selectedClass.split(';');
            var txtClassCode = document.getElementById("txtClassCode");
            txtClassCode.value = tempParam[0];
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
            EnableDelete(aspCheckBoxID)
        }

        function EnableDelete(aspCheckBoxID) {
            re = new RegExp(':' + aspCheckBoxID + '$')
            if (!document.forms[0].btnUpdate) {
                return
            }
            document.forms[0].btnUpdate.disabled = true
            for (i = 0; i < document.forms[0].elements.length; i++) {
                elm = document.forms[0].elements[i]
                if (elm.type == 'checkbox') {
                    if (re.test(elm.name)) {
                        if (elm.checked) {
                            if (document.forms[0].btnUpdate) {
                                document.forms[0].btnUpdate.disabled = false;
                                return;
                            }
                        }
                    }
                }
            }
        }

    </script>
</head>
<body onfocus="return checkModal()" onclick="checkModal()" ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <asp:HiddenField ID="hdnAreaID" runat="server" />
        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage">
                    <asp:Label ID="lblHeader" runat="server">TRAINING - List Pendaftaran</asp:Label></td>
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
                <td>
                    <table cellspacing="0" cellpadding="0" border="0" id="Table3">
                        <tr>
                            <td width="50%">
                                <table id="Table2" cellspacing="1" cellpadding="2" width="100%" border="0">
                                    <tr>
                                        <td class="titleField" width="19%" style="height: 18px">Kode Organisasi</td>
                                        <td width="1%" style="height: 18px">:</td>
                                        <td width="216" style="width: 216px; height: 18px">
                                            <asp:Label ID="lblDealerCode" runat="server"></asp:Label></td>
                                        <td class="titleField" width="28" style="width: 28px; height: 18px"></td>
                                    </tr>
                                    <tr>
                                        <td class="titleField">Nama Organisasi</td>
                                        <td width="1%">:</td>
                                        <td style="width: 216px">
                                            <asp:Label ID="lblDealerName" runat="server"></asp:Label></td>
                                        <td class="titleField" style="width: 28px"></td>
                                    </tr>
                                    <asp:Panel ID="pnlDealerSearch" runat="server" Visible="False" Width="100%">
                                        <tr>
                                            <td class="titleField" width="20%">Dealer</td>
                                            <td width="1%">:</td>
                                            <td style="width: 216px" width="216">
                                                <asp:TextBox ID="txtDealerSearchCode" runat="server" Width="150px"></asp:TextBox>
                                                <asp:Label ID="lblPopUpDealer" runat="server" Width="16px">
											<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0"></asp:Label></td>
                                            <td class="titleField" style="width: 28px" width="28"></td>
                                        </tr>
                                    </asp:Panel>
                                    <tr>
                                        <td class="titleField" style="height: 21px" width="24%">Kode Kategori</td>
                                        <td style="height: 21px" width="1%">:</td>
                                        <td style="height: 21px" width="75%">
                                            <asp:TextBox onkeypress="return HtmlCharUniv(event)" ID="txtKodeKategori" runat="server" MaxLength="20"
                                                Width="100"></asp:TextBox>&nbsp;
									<asp:Label ID="lblSearchKodeKategori" runat="server" Width="16px">
										<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td class="titleField" width="20%">Kode Kelas</td>
                                        <td width="1%">:</td>
                                        <td width="216" style="width: 216px">
                                            <asp:TextBox onkeypress="return HtmlCharUniv(event)" ID="txtClassCode" runat="server" Width="100"
                                                MaxLength="20"></asp:TextBox>
                                            <asp:Label ID="lblPopUpClass" runat="server" Width="16px">
										<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0"></asp:Label>
                                        </td>
                                        <td class="titleField" width="28" style="width: 28px"></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="50%">
                                <table>
                                    
                                    <tr>
                                        <td class="titleField" width="20%">No Registrasi</td>
                                        <td width="1%">:</td>
                                        <td width="216" style="width: 216px">
                                            <asp:TextBox onkeypress="return HtmlCharUniv(event)" ID="txtNoReg" runat="server" Width="150px"></asp:TextBox></td>
                                        <td class="titleField" width="28" style="width: 28px"></td>
                                    </tr>
                                    <tr>
                                        <td class="titleField" width="20%" style="height: 26px">Nama Siswa</td>
                                        <td width="1%" style="height: 26px">:</td>
                                        <td width="216" style="width: 216px; height: 26px">
                                            <asp:TextBox onkeypress="return HtmlCharUniv(event)" ID="txtTraineeName" runat="server" Width="256px"></asp:TextBox></td>
                                        <td class="titleField" width="28" style="width: 28px; height: 26px"></td>
                                    </tr>
                                    <tr>
                                        <td class="titleField">Periode Pendaftaran</td>
                                        <td>:</td>
                                        <td class="titleField" style="width: 216px">
                                            <asp:DropDownList ID="ddlYear" runat="server"></asp:DropDownList>
                                        </td>
                                        <td align="right" style="width: 28px"></td>
                                    </tr>
                                    <tr>
                                        <td class="titleField">Periode Kelas &nbsp;&nbsp;&nbsp;&nbsp; 
									&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
                                        </td>
                                        <td>:</td>
                                        <td style="width: 216px">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:CheckBox ID="cbDate" runat="server"></asp:CheckBox>
                                                    </td>
                                                    <td>
                                                        <cc1:IntiCalendar ID="icStart" runat="server" TextBoxWidth="70"></cc1:IntiCalendar>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label3" runat="server">s.d</asp:Label>
                                                    </td>
                                                    <td>
                                                        <cc1:IntiCalendar ID="icEnd" runat="server" TextBoxWidth="70"></cc1:IntiCalendar>
                                                    </td>
                                                    <td align="right" style="width: 50px">
                                                        <asp:Button ID="btnSearch" runat="server" Width="40px" Font-Bold="True" Text=" Cari "></asp:Button>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td style="background-color: Yellow; width: 50px">&nbsp;&nbsp;</td>
                            <td>Job Posisi Berubah</td>
                            <td style="width: 30px"></td>
                            <td style="background-color: LightSalmon; width: 50px">&nbsp;&nbsp;</td>
                            <td>Siswa telah resign</td>
                            <td style="width: 30px"></td>

                        </tr>

                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <div id="div1" style="overflow: auto; height: 280px" designtimedragdrop="77">
                        <asp:DataGrid ID="dtgClassRegistration" runat="server" Width="100%" PageSize="50" AllowPaging="True"
                            Font-Size="Small" AutoGenerateColumns="False" AllowCustomPaging="True" AllowSorting="True" BorderColor="#E0E0E0" BorderStyle="None" BorderWidth="0px" BackColor="#E0E0E0"
                            CellPadding="3" GridLines="Vertical" CellSpacing="1">
                            <SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#008A8C"></SelectedItemStyle>
                            <AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
                            <ItemStyle Font-Size="Smaller" Font-Names="Microsoft Sans Serif" ForeColor="Black" BackColor="White"></ItemStyle>
                            <HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#000084"></HeaderStyle>
                            <FooterStyle ForeColor="Black" BackColor="#CCCCCC"></FooterStyle>
                            <Columns>
                                <asp:BoundColumn Visible="False" DataField="ID"></asp:BoundColumn>
                                <asp:TemplateColumn>
                                    <HeaderStyle HorizontalAlign="Center" Width="3%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <HeaderTemplate>
                                        <input id="cbAll" onclick="CheckAll('cbItem', this.checked)" type="checkbox" runat="server">
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="cbItem" runat="server"></asp:CheckBox>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="No">
                                    <HeaderStyle Width="3%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblNo" runat="server" Width="35px" Font-Size="Smaller"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="RegistrationDate" HeaderText="Tgl Pendaftaran">
                                    <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="Label2" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.RegistrationDate", "{0:dd/MM/yyyy}") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="No Reg">
                                    <HeaderStyle Width="7%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblsalesmanCode">
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="TrTrainee.Name" HeaderText="Nama Siswa">
                                    <HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TrTrainee.Name") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="Dealer.DealerCode" HeaderText="Kode Dealer">
                                    <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblDealerCode" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerCode") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="Dealer.DealerName" Visible="false" HeaderText="Nama Dealer">
                                    <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="Label4" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerName") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="Dealer.City.CityName" Visible="false" HeaderText="Kota Dealer">
                                    <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.City.cityname") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="TrTrainee.JobPosition" HeaderText="Posisi">
                                    <HeaderStyle Width="7%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblPosition" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Grade">
                                    <HeaderStyle Width="7%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblGrade" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Grade Sementara">
                                    <HeaderStyle Width="7%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblGradeTemp" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="TrClass.ClassCode" HeaderText="Kode Kelas">
                                    <HeaderStyle Width="8%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hlClass" runat="server"></asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="TrClass.ClassName" HeaderText="Nama Kelas">
                                    <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="Label5" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TrClass.ClassName") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="TrClass.StartDate" HeaderText="Mulai">
                                    <HeaderStyle Width="7%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblStartDate" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TrClass.StartDate") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="TrClass.FinishDate" HeaderText="Selesai">
                                    <HeaderStyle Width="7%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblEndDate" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TrClass.FinishDate") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>

                                <asp:TemplateColumn SortExpression="Status" HeaderText="Status">
                                    <HeaderStyle Width="7%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblStatus" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn>
                                    <HeaderStyle Width="4%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnLihat" runat="server" CausesValidation="False" CommandName="View">
												<img src="../images/detail.gif" border="0" alt="Lihat"></asp:LinkButton>
                                        <asp:LinkButton ID="btnReplace" runat="server" CausesValidation="False" CommandName="replace">
												<img src="../images/unregistered.gif" border="0" alt="Replace"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                            <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
                        </asp:DataGrid>
                    </div>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:DropDownList ID="ddlStatus" runat="server"></asp:DropDownList>
                    <asp:Button ID="btnUpdate" runat="server" Text="Ubah Status" Enabled="False"></asp:Button><asp:Button ID="btnProsesCetak" runat="server" Text="Proses Cetak/Download" Enabled="False"
                        CausesValidation="False"></asp:Button></td>
            </tr>
        </table>
    </form>
    <script>
        EnableDelete('cbItem');
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
