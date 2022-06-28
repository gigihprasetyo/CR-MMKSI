<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmTrClassAllocationNew.aspx.vb" Inherits="FrmTrClassAllocationNew" SmartNavigation="False" %>

<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>Master Alokasi Kelas</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script type="text/javascript">

        function ShowPPCourseSelection2() {
            var hdnCategory = document.getElementById("hdnCategory");
            showPopUp('../PopUp/PopUpCourseCheck.aspx?category=' + hdnCategory.value, '', 500, 760, courseSelection2);
        }

        function courseSelection2(selectedCourse) {

            var txtKode = document.getElementById("txtKodeKategori");
            txtKode.value = selectedCourse
        }

        function ShowPPErrorExcel() {
            showPopUp('../General/../PopUp/PopUpErrorexcel.aspx', '', 500, 760, null);
        }

        function ShowPPClassSelectionMany() {
            var hdnCategory = document.getElementById("hdnArea");
            var txtKode = document.getElementById("txtKodeKategori");
            showPopUp('../PopUp/PopUpTrClassMany.aspx?CourseCode=' + txtKode.value + '&area=' + hdnCategory.value, '', 500, 760, classSelectionMany);
        }
        function classSelectionMany(selectedClass) {
            var txtKode = document.getElementById("txtKodeKelas");
            txtKode.value = selectedClass;
        }

        function ShowPPClassSelection() {
            showPopUp('../PopUp/PopUpClassSelection.aspx', '', 500, 760, classSelection);
        }
        function classSelection(selectedClass) {
            var tempParam = selectedClass.split(';');
            var txtKode = document.getElementById("txtKodeKelas");
            var txtNama = document.getElementById("txtNamaKelas");
            var txtKap = document.getElementById("txtKapasitas");
            txtKode.value = tempParam[0];
            txtNama.value = tempParam[1];
            txtKap.value = tempParam[2];
        }

        function ShowPPDealerSelection() {
            var area = document.getElementById("hdnArea");

            showPopUp('../PopUp/PopUpDealerSelection.aspx?areaid=' + area.value, '', 500, 760, DealerSelection);
        }

        function DealerSelection(selectedDealer) {
            var txtKodeDealer = document.getElementById("txtKodeDealer");
            txtKodeDealer.value = selectedDealer;
        }

    </script>
    <style type="text/css">
        .hidden {
            display: none;
        }
    </style>
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <asp:HiddenField ID="hdnCategory" runat="server" />
        <asp:HiddenField ID="hdnArea" runat="server" />
        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage">
                    <asp:Label ID="lblTitle" runat="server"></asp:Label></td>
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
                <td style="height: 57px" align="left">
                    <asp:Panel ID="pnl1" runat="server">
                        <table id="Table2" cellspacing="1" cellpadding="2" width="100%" border="0">
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
                                <td class="titleField" style="height: 21px" width="24%">Kode Kelas</td>
                                <td style="height: 21px" width="1%">:</td>
                                <td style="height: 21px" width="75%">
                                    <asp:TextBox onkeypress="return HtmlCharUniv(event)" ID="txtKodeKelas" runat="server" MaxLength="20"
                                        Width="100"></asp:TextBox>&nbsp;
										<asp:Label ID="lblPopUpClass" runat="server" Width="16px">
											<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0"></asp:Label>
                                    <asp:TextBox ID="txtNamaKelas" runat="server" Width="200px" Visible="False" ReadOnly="True"></asp:TextBox>
                                    <asp:TextBox ID="txtKapasitas" runat="server" Width="100" Visible="False" ReadOnly="True"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td class="titleField" style="height: 23px">Kode Dealer</td>
                                <td style="height: 23px">:</td>
                                <td style="height: 23px">
                                    <asp:TextBox onkeypress="return HtmlCharUniv(event)" ID="txtKodeDealer" runat="server" Width="300px"></asp:TextBox>&nbsp;
										<asp:Label ID="lblPopUpDealer" runat="server" Width="16px">
											<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0"></asp:Label></td>
                            </tr>
                            <tr>
                                <td class="titleField" style="height: 23px">
                                    <asp:CheckBox ID="chkAllocation" runat="server" Text="Alokasi > 0"></asp:CheckBox>&nbsp;
										<asp:CheckBox ID="chkBatal" runat="server" Text="Batal"></asp:CheckBox>&nbsp;&nbsp;
                                </td>
                                <td style="height: 23px"></td>
                                <td style="height: 23px"><strong>Periode</strong> :
										<asp:TextBox ID="txtPeriod" runat="server" Width="60px"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td class="titleField" style="height: 23px"></td>
                                <td style="height: 23px"></td>
                                <td style="height: 23px">
                                    <asp:Button ID="btnCari" runat="server" Width="80px" Text="Cari" CausesValidation="False"></asp:Button>&nbsp;&nbsp;&nbsp;&nbsp;
										<asp:Button ID="btnSetAllocation" runat="server" Width="150px" Text="Masukkan Jumlah Alokasi"
                                            CausesValidation="False"></asp:Button></td>
                            </tr>
                            <tr>
                                <td colspan="4">&nbsp;</td>
                            </tr>
                            <tr id="trTemplateupload" runat="server">
                                <td class="titleField" style="height: 23px"></td>
                                <td style="height: 23px"></td>
                                <td style="height: 23px">
                                    <asp:LinkButton ID="linkTemplate" runat="server" CausesValidation="false">Download Template</asp:LinkButton>
                                </td>
                            </tr>
                            <tr id="trUpload" runat="server">
                                <td class="titleField" style="height: 23px">Upload File</td>
                                <td style="height: 23px">:</td>
                                <td style="height: 23px">
                                    <input onkeypress="return false;" id="fileUpload" style="width: 392px; height: 20px" type="file"
                                        size="46" name="fileUpload" runat="server">
                                    <asp:Button ID="btnUpload" runat="server" Text="Upload"></asp:Button></td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <div id="div1" style="overflow: auto; height: 300px">
                        <asp:DataGrid ID="dtgTrClassAllocation" runat="server" Width="100%" AutoGenerateColumns="False"
                            BorderColor="#CDCDCD" BorderStyle="None" BorderWidth="0px" BackColor="#CDCDCD" CellPadding="3" GridLines="Horizontal" AllowCustomPaging="True"
                            AllowSorting="True" PageSize="25" ForeColor="Gray" CellSpacing="1">
                            <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                            <AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
                            <ItemStyle BackColor="White"></ItemStyle>
                            <HeaderStyle Font-Bold="True" ForeColor="White" BorderColor="#E0E0E0" BackColor="#CC3333"></HeaderStyle>
                            <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
                            <Columns>
                                <asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
                                    <HeaderStyle Width="2%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:TemplateColumn Visible="False" HeaderText="No">
                                    <HeaderStyle Width="2%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn>
                                    <HeaderStyle Width="3%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblNo" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="TrClass.ClassCode" HeaderText="Kode Kelas">
                                    <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="Label2" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TrClass.ClassCode") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="Dealer.DealerCode" HeaderText="Kode Dealer">
                                    <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblDealerCode" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="Dealer.DealerName" HeaderText="Nama Dealer">
                                    <HeaderStyle Width="20%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerName") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="Dealer.City.CityName" HeaderText="Kota">
                                    <HeaderStyle Width="20%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="Label3" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.City.CityName") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Kapasitas">
                                    <HeaderStyle Width="8%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblKapasitas" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Selisih Alokasi">
                                    <HeaderStyle Width="8%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="Allocated" HeaderText="Jumlah Alokasi">
                                    <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox onkeypress="return numericOnlyUniv(event)" ID="txtAllocated" runat="server" MaxLength="3" Width="50" Text='<%# DataBinder.Eval(Container, "DataItem.Allocated") %>'>
                                        </asp:TextBox>
                                        <asp:RangeValidator ID="RangeValidator1" runat="server" MinimumValue="0" MaximumValue="100" Type="Integer"
                                            ControlToValidate="txtAllocated" ErrorMessage="X"></asp:RangeValidator>
                                        <asp:TextBox ID="txtTemp" runat="server" Width="64px" Visible="False"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Alokasi Diambil">
                                    <HeaderStyle Width="8%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblAllocationTaken" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="LastAllocated" HeaderText="Alokasi Sebelum">
                                    <HeaderStyle Width="8%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblLastAllocated" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.LastAllocated") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="LastUpdateTime" HeaderText="Update Terakhir">
                                    <HeaderStyle Width="8%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblLastUpdateTime" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.LastUpdateTime") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="CancelReason" HeaderText="Alasan">
                                    <HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtCancelReason" runat="server" Style="visibility: hidden;" Width="0px" Text='<%# DataBinder.Eval(Container, "DataItem.CancelReason") %>'>
                                        </asp:TextBox>
                                        <asp:Label ID="lblCancelReason" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CancelReason") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn>
                                    <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnHapus" runat="server" Text="Hapus" CausesValidation="False" CommandName="Delete">
												<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn>
                                    <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnUbah" runat="server" Text="Edit" CausesValidation="False" CommandName="Edit">
												<img src="../images/edit.gif" border="0" alt="Edit"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                            <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
                        </asp:DataGrid><asp:DataGrid ID="dtgUpload" runat="server" Width="100%" AutoGenerateColumns="False" BorderColor="#CDCDCD"
                            BorderStyle="None" BorderWidth="0px" BackColor="#CDCDCD" CellPadding="3" GridLines="Horizontal" AllowCustomPaging="True"
                            AllowSorting="True" PageSize="25" ForeColor="Gray" CellSpacing="1">
                            <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
                            <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                            <AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
                            <ItemStyle BackColor="White"></ItemStyle>
                            <HeaderStyle Font-Bold="True" ForeColor="White" BorderColor="#E0E0E0" BackColor="#CC3333"></HeaderStyle>
                            <Columns>
                                <asp:TemplateColumn HeaderText="No">
                                    <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblNo" runat="server"></asp:Label>
                                        <asp:TextBox runat="server" ID="txtClassID" Width="0px" Text='<%# iif(isnothing(DataBinder.Eval(Container, "DataItem.TrClass")),0, DataBinder.Eval(Container, "DataItem.TrClass.ID")) %>' Style="visibility: hidden;">
                                        </asp:TextBox>
                                        <asp:TextBox runat="server" ID="txtDealerID" Width="0px" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.ID") %>' Style="visibility: hidden;">
                                        </asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Kode Kelas">
                                    <HeaderStyle Width="20%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblClassCode" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Kode Dealer">
                                    <HeaderStyle Width="25%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblDealerCode" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Nama Dealer">
                                    <HeaderStyle Width="30%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblDealerName" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Kota">
                                    <HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblCity" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Kapasitas">
                                    <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblKapasitas" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Selisih Alokasi">
                                    <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblSelisih" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Jumlah Alokasi">
                                    <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtAllocated" Width="40px" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Allocated") %>' Style="text-align: right;">
                                        </asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Alokasi Sebelum">
                                    <HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblLastAllocated" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Last Update">
                                    <HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblLastUpdateUpload" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Error">
                                    <HeaderStyle Width="25%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblError" Width="50px" runat="server"></asp:Label>
                                        <asp:TextBox runat="server" ID="txtErrorFlag" Width="10px" Style="visibility: hidden;"></asp:TextBox>
                                        <asp:TextBox runat="server" ID="txtErrorOverLimit" Width="10px" Style="visibility: hidden;"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn>
                                    <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="Linkbutton1" runat="server" Text="Hapus" CausesValidation="False" CommandName="Delete">
												<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                            <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
                        </asp:DataGrid>
                    </div>
                </td>
            </tr>
            <tr>
                <td align="center" height="27">
                    <asp:DataGrid ID="grid" runat="server" Width="100%" Visible="False" AutoGenerateColumns="False"
                        BorderColor="#CDCDCD" BorderStyle="None" BorderWidth="0px" BackColor="#CDCDCD" CellPadding="3" GridLines="Horizontal"
                        AllowCustomPaging="False" AllowSorting="False" PageSize="25" ForeColor="Gray" CellSpacing="1">
                        <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                        <AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
                        <ItemStyle BackColor="White"></ItemStyle>
                        <HeaderStyle Font-Bold="True" ForeColor="White" BorderColor="#E0E0E0" BackColor="#CC3333"></HeaderStyle>
                        <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
                        <Columns>
                            <asp:TemplateColumn HeaderText="No">
                                <HeaderStyle Width="3%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Kode Kelas">
                                <HeaderStyle Width="20%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Kode Dealer">
                                <HeaderStyle Width="25%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Nama Dealer">
                                <HeaderStyle Width="30%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Kota">
                                <HeaderStyle Width="30%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Kapasitas">
                                <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Selisih Alokasi">
                                <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="Allocated" HeaderText="Jumlah Alokasi">
                                <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="Alokasi Diambil">
                                <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblUsedAllocation" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="LastAllocated" HeaderText="Alokasi Sebelum">
                                <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                            </asp:BoundColumn>
                            
                            <asp:TemplateColumn SortExpression="LastUpdateTime" HeaderText="Update Terakhir">
                                <HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblLastUpdateTimeInGrid" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.LastUpdateTime") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="CancelReason" HeaderText="Alasan">
                                <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                            </asp:BoundColumn>
                        </Columns>
                        <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
                    </asp:DataGrid></td>
            </tr>
            <tr>
                <td align="center" height="27">
                    <asp:Button ID="btnSimpan" runat="server" Width="60px" Text="Simpan"></asp:Button>&nbsp;
						<asp:Button ID="btnBatal" runat="server" Width="60px" Text="Batal" CausesValidation="False"></asp:Button>&nbsp;&nbsp;
						<asp:Button ID="btnDownload" runat="server" Width="60px" Text="Download" CausesValidation="False"></asp:Button>
                        <asp:Button ID="btnShowPopup" runat="server" CssClass="hidden" OnClientClick="ShowPPErrorExcel()" CausesValidation="false" />
                </td>
            </tr>
            <tr>
                <td align="center" height="27"></td>
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
