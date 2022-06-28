<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmTrApprovalDetail.aspx.vb" Inherits=".FrmTrApprovalDetail" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>FrmCourse</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <style type="text/css">
        .HeaderFieldGrid {
            font-family: Sans-Serif, Arial;
            font-size: 11px;
            color: white;
            margin: 0px;
            font-weight: bold;
            text-align: left;
        }
    </style>
    <script type="text/javascript">
        function filterSuggestion(obj, columnIndex) {
            // Declare variables
            var input, filter, table, tr, td, i, txtValue;
            //input = document.getElementById(obj);
            filter = obj.value.toUpperCase();
            table = document.getElementById("<%= gdSuggestion.ClientID%>");
            tr = table.getElementsByTagName("tr");

            // Loop through all table rows, and hide those who don't match the search query
            for (i = 1; i < tr.length; i++) {
                if (tr[i].id.toString().toLowerCase().indexOf("trhead") > -1) {
                    continue;
                }
                td = tr[i].getElementsByTagName("td")[columnIndex];
                if (td) {
                    txtValue = td.textContent || td.innerText;
                    if (txtValue.toUpperCase().indexOf(filter) > -1) {
                        tr[i].style.display = "";
                    } else {
                        tr[i].style.display = "none";
                    }
                }
            }
        }
        function filterDaftarTunggu(obj, columnIndex) {
            // Declare variables
            var input, filter, table, tr, td, i, txtValue;
            //input = document.getElementById(obj);
            filter = obj.value.toUpperCase();
            table = document.getElementById("<%= gdWaiting.ClientID%>");
            tr = table.getElementsByTagName("tr");

            // Loop through all table rows, and hide those who don't match the search query
            for (i = 1; i < tr.length; i++) {
                if (tr[i].id.toString().toLowerCase().indexOf("trhead") > -1) {
                    continue;
                }
                td = tr[i].getElementsByTagName("td")[columnIndex];
                if (td) {
                    txtValue = td.textContent || td.innerText;
                    if (txtValue.toUpperCase().indexOf(filter) > -1) {
                        tr[i].style.display = "";
                    } else {
                        tr[i].style.display = "none";
                    }
                }
            }
        }
    </script>
</head>

<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage">
                    <asp:Label ID="lblTitle" Text="Training - MRTC" runat="server"></asp:Label>
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
                <td valign="top">
                    <table id="Table2" cellspacing="1" cellpadding="1" width="100%" border="0">
                        <tr>
                            <td class="titleField" width="10%">Kode Kelas</td>
                            <td width="1%">:</td>
                            <td width="75%">
                                <asp:Label ID="lblClassCode" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" width="10%">Kode Kategori</td>
                            <td width="1%">:</td>
                            <td width="75%">
                                <asp:Label ID="lblCourseCode" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" width="10%">Kapasitas Kelas</td>
                            <td width="1%">:</td>
                            <td width="75%">
                                <asp:Label ID="lblAllocation" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" width="10%">Sisa Kelas</td>
                            <td width="1%">:</td>
                            <td width="75%">
                                <asp:Label ID="lblSisaKelas" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" width="10%">Lokasi</td>
                            <td width="1%">:</td>
                            <td width="75%">
                                <asp:Label ID="lblLocation" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField" width="10%">Periode</td>
                            <td width="1%">:</td>
                            <td width="75%">
                                <asp:Label ID="lblPeriode" runat="server"></asp:Label>
                            </td>
                        </tr>


                    </table>
                </td>
            </tr>
        </table>
        <br />
        <center>
            <asp:Button ID="btnSimpan" runat="server" Text="Simpan" Width="100px" />
            &nbsp; 
            <asp:Button ID="btnKembali" runat="server" Text="Kembali" Width="100px" />
        </center>
        <br />
        <table cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td valign="top" width="47%">
                    <h3>Daftar Peserta</h3>
                    <div id="dvGrid1" runat="server" style="overflow: auto; height: 275px" >
                        <asp:GridView ID="gdSuggestion" runat="server" ShowHeaderWhenEmpty="true" ShowHeader="true" AutoGenerateColumns="false" width="100%" AllowPaging="false">
                            <Columns>
                                <asp:TemplateField HeaderText="" SortExpression="">
                                    <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                    <HeaderTemplate>
                                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                            <tr id="trHead1" align="center">
                                                <td align="center">
                                                    <asp:Label ID="lblHNoreg" CssClass="HeaderFieldGrid" Text="No Reg." runat="server"></asp:Label></td>
                                            </tr>
                                            <tr id="trHead2">
                                                <td>
                                                    <asp:TextBox ID="txtHNoReg" Width="100%" onkeyup="filterSuggestion(this,0)" runat="server"></asp:TextBox></td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblIDTrainee" Text='<%# DataBinder.Eval(Container, "DataItem.TrTrainee.ID")%>' runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="">
                                    <HeaderStyle Width="20%" CssClass="titleTableService"></HeaderStyle>
                                    <HeaderTemplate>
                                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                            <tr id="trHead1">
                                                <td  align="center">
                                                    <asp:Label ID="lblHSiswa" CssClass="HeaderFieldGrid" Text="Siswa Terdaftar" runat="server"></asp:Label></td>
                                            </tr>
                                            <tr id="trHead2">
                                                <td>
                                                    <asp:TextBox ID="txtHSiswa"  Width="100%" onkeyup="filterSuggestion(this,1)" runat="server"></asp:TextBox></td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblNamaTrainee" Width="100%" Text='<%# DataBinder.Eval(Container, "DataItem.TrTrainee.Name")%>' runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                    <HeaderTemplate>
                                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                            <tr id="trHead1">
                                                <td  align="center">
                                                    <asp:Label ID="lblHDealer" CssClass="HeaderFieldGrid" Text="Dealer" runat="server"></asp:Label></td>
                                            </tr>
                                            <tr id="trHead2">
                                                <td>
                                                    <asp:TextBox ID="txtHDealer" Width="100%" onkeyup="filterSuggestion(this,2)" runat="server"></asp:TextBox></td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblDealer" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerCode")%>' runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                    <HeaderTemplate>
                                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                            <tr id="trHead1">
                                                <td  align="center">
                                                    <asp:Label ID="lblHPosition" CssClass="HeaderFieldGrid" Text="Posisi" runat="server"></asp:Label></td>
                                            </tr>
                                            <tr id="trHead2">
                                                <td>
                                                    <asp:TextBox ID="txtHPosition" Width="100%" onkeyup="filterSuggestion(this,3)" runat="server"></asp:TextBox></td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblPosition" Text='<%# DataBinder.Eval(Container, "DataItem.TrTrainee.RefJobPosition.Description")%>' runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Priority">
                                    <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblPriority" Text='<%# DataBinder.Eval(Container, "DataItem.PrioritySequence")%>' runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkMark" runat="server" />
                                        <asp:HiddenField ID="hdnIDBooking" runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </td>
                <td width="6%">
                    <table width="100%">
                        <tr>
                            <td><asp:Button ID="btnRight" runat="server" Width="90%" Text=">>" /></td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td><asp:Button ID="btnLeft" runat="server" Width="90%" Text="<<" /></td>
                        </tr>
                    </table>
                    
                </td>
                <td valign="top" width="47%">
                    <h3>Daftar Tunggu</h3>
                    <div id="Div1" runat="server" style="overflow: auto; height: 275px" >
                        <asp:GridView ID="gdWaiting" runat="server" ShowHeaderWhenEmpty="true" ShowHeader="true" AutoGenerateColumns="false" Width="100%" AllowPaging="false">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkMark" runat="server" />
                                        <asp:HiddenField ID="hdnIDBooking" runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="">
                                    <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                    <HeaderTemplate>
                                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                            <tr id="trHead1">
                                                <td align="center">
                                                    <asp:Label ID="lblHNoreg" CssClass="HeaderFieldGrid" Text="No Reg." runat="server"></asp:Label></td>
                                            </tr>
                                            <tr id="trHead2">
                                                <td>
                                                    <asp:TextBox ID="txtHNoReg"  Width="100%" onkeyup="filterDaftarTunggu(this,1)" runat="server"></asp:TextBox></td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblIDTrainee" Text='<%# DataBinder.Eval(Container, "DataItem.TrTrainee.ID")%>' runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="">
                                    <HeaderStyle Width="20%" CssClass="titleTableService"></HeaderStyle>
                                    <HeaderTemplate>
                                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                            <tr id="trHead1">
                                                <td align="center">
                                                    <asp:Label ID="lblHSiswa" CssClass="HeaderFieldGrid" Text="Siswa Terdaftar" runat="server"></asp:Label></td>
                                            </tr>
                                            <tr id="trHead2">
                                                <td>
                                                    <asp:TextBox ID="txtHSiswa"  Width="100%" onkeyup="filterDaftarTunggu(this,2)" runat="server"></asp:TextBox></td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblNamaTrainee" Text='<%# DataBinder.Eval(Container, "DataItem.TrTrainee.Name")%>' runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                    <HeaderTemplate>
                                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                            <tr id="trHead1">
                                                <td align="center">
                                                    <asp:Label ID="lblHDealer" CssClass="HeaderFieldGrid" Text="Dealer" runat="server"></asp:Label></td>
                                            </tr>
                                            <tr id="trHead2">
                                                <td>
                                                    <asp:TextBox ID="txtHDealer" Width="100%" onkeyup="filterDaftarTunggu(this,3)" runat="server"></asp:TextBox></td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblDealer" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerCode")%>' runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
                                    <HeaderTemplate>
                                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                            <tr id="trHead1">
                                                <td align="center">
                                                    <asp:Label ID="lblHPosition" CssClass="HeaderFieldGrid" Text="Posisi" runat="server"></asp:Label></td>
                                            </tr>
                                            <tr id="trHead2">
                                                <td>
                                                    <asp:TextBox ID="txtHPosition" Width="100%" onkeyup="filterSuggestion(this,3)" runat="server"></asp:TextBox></td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblPosition" Text='<%# DataBinder.Eval(Container, "DataItem.TrTrainee.RefJobPosition.Description")%>' runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Priority">
                                    <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblPriority" Text='<%# DataBinder.Eval(Container, "DataItem.PrioritySequence")%>' runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <asp:HiddenField ID="hdnConfirmMove" runat="server" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>

