<%@ Page Language="vb" MaintainScrollPositionOnPostback="true" AutoEventWireup="false" CodeBehind="FrmCourseRegistrationInput.aspx.vb" Inherits="FrmCourseRegistrationInput" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>Data Status Siswa</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script type="text/javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script type="text/javascript" src="../WebResources/InputValidation.js"></script>
    <script type="text/javascript" src="../WebResources/jquery-1.8.3.min.js"></script>

    <script type="text/javascript">

        function popUpClassInformation(kode) {
            var url = '../PopUp/PopUpClassInformation.aspx?kode=' + kode;
            showPopUp(url, '', 320, 440, null);
        }

        function popUpDataSiswa(trTraineeID) {
            //var url = '../PopUp/PopUpSelectingDealer.aspx?trtraineeid=' + trTraineeID;
            var url = '../PopUp/PopUpTrTraineeDetail.aspx?isreload=1&trtraineeid=' + trTraineeID;
            showPopUp(url, '', 600, 800, TraineeUpdate);
        }

        function TraineeUpdate(result) {
            var arrResult = result.split(';');
            table = document.getElementById("dtgHeader");
            var trid = arrResult[0].toString();
            for (i = 1; i < table.rows.length; i++) {
                var regid = table.rows[i].cells[2].innerText;
                if (regid.replace(/^\s+|\s+$/gm, '') == trid.replace(/^\s+|\s+$/gm, '')) {
                    var lblNoKTP = table.rows[i].cells[4].getElementsByTagName("span")[0];
                    lblNoKTP.appendChild(document.createTextNode(arrResult[1].toString()));
                    GetSelectedTrainee();
                }
            }
        }

        function GetSelectedTrainee() {
            var table;
            table = document.getElementById("dtgHeader");
            var trid;
            var hdnIsvalid = document.getElementById("isValidAdd");
            var noKTp = '';
            hdnIsvalid.value = '';
            for (i = 1; i < table.rows.length; i++) {
                var CheckBox = table.rows[i].cells[0].getElementsByTagName("INPUT")[0];
                if (CheckBox != null && CheckBox.checked) {
                    if (navigator.appName == "Microsoft Internet Explorer") {
                        if (replace(table.rows[i].cells[4].innerText, ' ', '') == '' || replace(table.rows[i].cells[4].innerText, ' ', '') == undefined) {
                            hdnIsvalid.value = '0';
                            trid = replace(table.rows[i].cells[2].innerText, ' ', '');
                            break;
                        }

                    }
                    else {
                        var ktpNumber = replace(table.rows[i].cells[4].getElementsByTagName("span")[0].innerHTML, ' ', '')
                        if (ktpNumber == '' || ktpNumber == undefined) {
                            hdnIsvalid.value = '1';
                            trid = replace(table.rows[i].cells[2].innerText, ' ', '');
                            break;
                        }
                    }
                }
            }
            if (hdnIsvalid.value == '0' || hdnIsvalid.value == '1') {
                var nama = table.rows[i].cells[3].innerText;
                alert('Peserta ' + nama + ' dengan No Registrasi ' + trid + ' tidak memiliki data KTP');
                popUpDataSiswa(trid);
            }
           
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
        <asp:HiddenField ID="isValidAdd" runat="server" />
        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage">
                    <asp:Label ID="lblPageTitle" runat="server"></asp:Label>
                    <asp:HiddenField ID="hdnCategory" runat="server" />
                </td>
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
                    <table>
                        <tr>
                            <td width="150" class="titleField" height="1">Tahun Fiskal</td>
                            <td height="1" width="1%">:</td>
                            <td width="80%" nowrap="nowrap">
                                <asp:Label ID="lbltahunFiscal" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td width="150" class="titleField" height="1">Kode Training</td>
                            <td height="1" width="1%">:</td>
                            <td width="80%" nowrap="nowrap">
                                <asp:Label ID="lblKodeTraining" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td width="150" class="titleField" height="1">Kategori</td>
                            <td height="1" width="1%">:</td>
                            <td width="80%" nowrap="nowrap">
                                <asp:Label ID="lblKategori" runat="server"></asp:Label>
                            </td>
                        </tr>
                         <tr>
                            <td width="150" class="titleField" height="1">Sisa Free Pass Training</td>
                            <td height="1" width="1%">:</td>
                            <td width="80%" nowrap="nowrap">
                                <asp:Label ID="lblFreePass" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>

            <tr id="trGridHeader" runat="server">
                <td valign="top">
                    <div id="div1" style="overflow: auto; height: 200px">
                        <asp:DataGrid ID="dtgHeader" runat="server" Width="100%" AutoGenerateColumns="False"
                            GridLines="Horizontal" CellPadding="3" BackColor="Gainsboro" BorderWidth="0px" BorderStyle="None" BorderColor="#CDCDCD"
                            AllowSorting="True" ForeColor="GhostWhite" CellSpacing="1" Font-Names="MS Reference Sans Serif">
                            <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                            <AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
                            <ItemStyle BackColor="White"></ItemStyle>
                            <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
                            <Columns>
                                <asp:TemplateColumn>
                                    <HeaderStyle Width="2%" CssClass="titleTableService"></HeaderStyle>
                                    <HeaderTemplate>
                                        <input id="chkAllItems" type="checkbox" onclick="CheckAll('chkItemChecked',
														document.forms[0].chkAllItems.checked)" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkItemChecked" runat="server"></asp:CheckBox>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Wrap="False"></ItemStyle>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="No" SortExpression="">
                                    <HeaderStyle Width="3%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblNo" runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Wrap="False"></ItemStyle>
                                </asp:TemplateColumn>

                                <asp:TemplateColumn HeaderText="No. Reg" SortExpression="ID">
                                    <HeaderStyle Width="7%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblNoReg" runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Wrap="False"></ItemStyle>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Nama Siswa" SortExpression="Name">
                                    <HeaderStyle Width="20%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Wrap="False"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblNamaSiswa" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="No KTP" SortExpression="ID">
                                    <HeaderStyle Width="7%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblNoKtp"  runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Wrap="False"></ItemStyle>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Mulai Bekerja" SortExpression="StartWorkingDate">
                                    <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblMulaiKerja" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Wrap="False"></ItemStyle>
                                </asp:TemplateColumn>

                                <asp:TemplateColumn HeaderText="Posisi" SortExpression="RefJobPosition.Description">
                                    <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblPosisi" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Wrap="False"></ItemStyle>
                                </asp:TemplateColumn>

                                <asp:TemplateColumn HeaderText="Status" SortExpression="">
                                    <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Wrap="False"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblStatus" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>

                            </Columns>
                            <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
                        </asp:DataGrid>
                    </div>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
            </tr>
            <tr id="trBtnTambah" runat="server">
                <td align="center">
                    <asp:Button ID="btnAdd" runat="server" CausesValidation="false" Text="Tambahkan" Width="100px" OnClientClick="GetSelectedTrainee()" />
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
            </tr>
            <tr id="trNotif" runat="server">
                <td class="titleField">
                    <asp:Label ID="lblTotalBayar" runat="server"></asp:Label></td>
            </tr>

            <tr>
                <td>
                    <script type="text/javascript">
                        window.onload = function () {
                            var div = document.getElementById('<%=dvBooking.ClientID%>');
                            var div_position = document.getElementById('<%= div_position.ClientID%>');
                            var position = parseInt('<%=Request.Form("div_position") %>');
                            if (isNaN(position)) {
                                position = 0;
                            }
                            div.scrollTop = position;
                            div.onscroll = function () {
                                div_position.value = div.scrollTop;
                            };
                        };

                    </script>
                    <input type="hidden" id="div_position" runat="server" />
                    <div id="dvBooking" runat="server" style="overflow: auto; height: 150px">
                        <asp:DataGrid ID="dtgBooking" runat="server" Width="100%" AutoGenerateColumns="False"
                            GridLines="Horizontal" CellPadding="3" BackColor="Gainsboro" BorderWidth="0px" BorderStyle="None" BorderColor="#CDCDCD"
                            AllowCustomPaging="True" AllowSorting="True" PageSize="25" ForeColor="GhostWhite" CellSpacing="1" Font-Names="MS Reference Sans Serif">
                            <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                            <AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
                            <ItemStyle BackColor="White"></ItemStyle>
                            <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
                            <Columns>
                                <asp:TemplateColumn SortExpression="">
                                    <HeaderStyle Width="3%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Wrap="False"></ItemStyle>
                                    <ItemTemplate>
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:LinkButton ID="lbtnUp" runat="server" CausesValidation="False" Text="Up" CommandName="upList">
												<img src="../images/icon_up.png" border="0" alt="Up"></asp:LinkButton>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:LinkButton ID="lbtnDown" runat="server" CausesValidation="False" Text="Down" CommandName="downList">
												<img src="../images/icon_down.png" border="0" alt="Down"></asp:LinkButton>
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </asp:TemplateColumn>

                                <asp:TemplateColumn HeaderText="Urutan Prioritas" SortExpression="">
                                    <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblNo" runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Wrap="False"></ItemStyle>
                                </asp:TemplateColumn>

                                <asp:TemplateColumn HeaderText="No. Reg" SortExpression="ID">
                                    <HeaderStyle Width="7%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblNoReg" runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Wrap="False"></ItemStyle>
                                </asp:TemplateColumn>

                                <asp:TemplateColumn HeaderText="Nama Siswa" SortExpression="Name">
                                    <HeaderStyle Width="20%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Wrap="False"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblNamaSiswa" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>

                                <asp:TemplateColumn HeaderText="Mulai Bekerja" SortExpression="StartWorkingDate">
                                    <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblMulaiKerja" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Wrap="False"></ItemStyle>
                                </asp:TemplateColumn>

                                <asp:TemplateColumn HeaderText="Posisi" >
                                    <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblPosisi" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Wrap="False"></ItemStyle>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Tanggal Validasi" >
                                    <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblValidasi" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Wrap="False"></ItemStyle>
                                </asp:TemplateColumn>

                                <asp:TemplateColumn HeaderText="Status" SortExpression="">
                                    <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Wrap="False"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblStatus" runat="server">
                                        </asp:Label>
                                        <asp:HyperLink ID="hKodeKelas" runat="server">
                                        </asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn>
                                    <HeaderStyle Width="2%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Wrap="False"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnDelete" runat="server" Text="Hapus" CausesValidation="False" CommandName="Delete">
												<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
                                        <asp:HiddenField ID="hdnClass" runat="server" />
                                        <asp:HiddenField ID="hdnIDBooking" runat="server" />
                                        <asp:Label ID="lblID" runat="server" Visible="false"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                            <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
                        </asp:DataGrid>
                    </div>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Button ID="btnSimpan" runat="server" Width="60px" Text="Simpan" TabIndex="15"></asp:Button>
                    &nbsp;
                    <asp:Button ID="btnValidasi" runat="server" Width="100px" CausesValidation="false" Text="Validasi" TabIndex="15"></asp:Button> &nbsp;
                    <asp:Button ID="btnBatal" runat="server" Width="100px" CausesValidation="false" Text="Batal Validasi" TabIndex="15"></asp:Button> &nbsp;
                    <asp:Button ID="btnKembali" runat="server" Text="Kembali" CausesValidation="False" TabIndex="14"></asp:Button>

                </td>
            </tr>
        </table>
    </form>

</body>
</html>

