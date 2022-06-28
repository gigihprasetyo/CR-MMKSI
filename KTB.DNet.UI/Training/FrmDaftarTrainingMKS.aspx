<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmDaftarTrainingMKS.aspx.vb" Inherits="FrmDaftarTrainingMKS" SmartNavigation="False" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>FrmPendaftaranSales2</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript">

        function GetAllocatedAllowed() {
            var allocated = document.getElementById("divAllocatedTot");
            return parseInt(allocated.innerText)
        }
        function CheckEnability() {
            var allowed = GetAllocatedAllowed();
            var counterCheck = 0;
            var checkString = "";
            var checkColl = document.getElementById("txtItemCheckColl");
            checkColl.value = "";
            var table = document.getElementById("dtgTrainee2");
            for (i = 1; i < table.rows.length; i++) {
                var checkbox = table.rows[i].cells[0].getElementsByTagName("INPUT")[0];
                if (checkbox.checked) {
                    //use to check max trainee that can choosen
                    counterCheck++;
                    checkString += i + ";";
                }
                else
                    // Penambahan oleh Agus untuk bug fix sbb.
                    // untuk kode kelas tertentu pada suatu dealer yang memiliki alokasi 1 orang saja 
                    // pada saat check salah satu siswa kemudian membatalkannya siswa yang lain tidak 
                    // bisa dicheck (disable) 
                {
                    checkString += " " + ";";
                }
            }
            //display collection row index in table that has been checked
            checkColl.value = checkString;
            //disable check box if reach max capacity
            if (counterCheck < allowed) {
                DisableCheckBox(false);
            }
            else {
                if (counterCheck == allowed) {
                    DisableCheckBox(true);
                }
            }
        }
        function DisableCheckBox(disabled) {
            var checkColl = document.getElementById("txtItemCheckColl");
            if (checkColl.value != "") {
                var arrCheckColl = checkColl.value.split(";");
                if (arrCheckColl.length > 0) {
                    var table = document.getElementById("dtgTrainee2");
                    //var i;
                    for (i = 1; i < table.rows.length; i++) {

                        var checkbox = table.rows[i].cells[0].getElementsByTagName("INPUT")[0];
                        //just watch index row not in checkColl
                        if (!CatchIt(i, arrCheckColl)) {
                            checkbox.disabled = disabled;
                        }

                    }

                }
            }
        }


        function CatchIt(indexRow, checkColl) {
            var a;
            for (a = 0; a < checkColl.length - 1; a++) {
                if (checkColl[a] == indexRow) {
                    return true;
                }
            }
            return false;
        }

        function filterSuggestion(obj, columnIndex) {
            // Declare variables
            var input, filter, table, tr, td, i, txtValue;
            //input = document.getElementById(obj);
            filter = obj.value.toUpperCase();
            table = document.getElementById("<%= dtgTrainee2.ClientID%>");
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
</head>
<body ms_positioning="GridLayout">
    <form id="Form2" method="post" runat="server">
        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage">Training After Sales - Pendaftaran - Pilih Siswa</td>
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
                <td style="height: 80px">
                    <table id="Table2" cellspacing="1" cellpadding="2" border="0">
                        <colgroup>
                            <col width="14%">
                            <col width="1%">
                            <col width="25%">
                            <col width="24%">
                            <col width="1%">
                            <col width="35%">
                        </colgroup>
                        <tr>
                            <td class="titleField">Kode Kelas</td>
                            <td width="1%">:</td>
                            <td colspan="4">
                                <asp:Label ID="lblClassCode" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="titleField">Nama Kelas</td>
                            <td width="1%">:</td>
                            <td colspan="4">
                                <asp:Label ID="lblClassName" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="titleField">Mulai</td>
                            <td width="1%">:</td>
                            <td>
                                <asp:Label ID="lblStartDate" runat="server"></asp:Label></td>
                            <td class="titleField">Selesai</td>
                            <td width="1%">:</td>
                            <td>
                                <asp:Label ID="lblFinishDate" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="titleField">Lokasi</td>
                            <td width="1%">:</td>
                            <td>
                                <asp:Label ID="lblLocation" runat="server"></asp:Label></td>
                            <td class="titleField">Alokasi</td>
                            <td width="1%">:</td>
                            <td>
                                <div id="divAllocatedTot" style="width: 100%">
                                    <asp:Label ID="lblAllocatedTot" runat="server"></asp:Label></div>
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField"></td>
                            <td width="1%"></td>
                            <td></td>
                            <td class="titleField">Yang Sudah Terdaftar</td>
                            <td width="1%">
                                <p>:</p>
                            </td>
                            <td>
                                <div id="divAllocatedReg" style="width: 100%">
                                    <asp:Label ID="lblAllocatedReg" runat="server"></asp:Label></div>
                            </td>
                        </tr>
                    </table>
                    <input id="txtItemCheckColl" onkeypress="return numericOnlyUniv(event)" type="hidden" name="txtItemCheckColl"
                        runat="server">
                </td>
            </tr>
            <tr>
                <td></td>
            </tr>
            <tr>
                <td class="titleField">Silakan pilih siswa yang diinginkan untuk mengikuti kelas 
						ini</td>
            </tr>
            <tr>
                <td>
                    <div id="divTrainee" style="overflow: auto; height: 320px">
                        <asp:Gridview ID="dtgTrainee2" runat="server" ShowHeaderWhenEmpty="true" ShowHeader="true" AutoGenerateColumns="false" Width="100%" AllowPaging="false">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderStyle Width="3%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkItemChecked" runat="server"></asp:CheckBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="No">
                                    <HeaderStyle Width="2%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblNo" runat="server" Width="35px" Font-Size="Smaller"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="ID" HeaderText="No. Reg">
                                    <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                                    <HeaderTemplate>
                                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                            <tr id="trHead1" align="center">
                                                <td align="center">
                                                    <asp:Label ID="lblHNoreg" CssClass="HeaderFieldGrid" Text="No Reg." runat="server"></asp:Label></td>
                                            </tr>
                                            <tr id="trHead2">
                                                <td>
                                                    <asp:TextBox ID="txtHNoReg" Width="100%" onkeyup="filterSuggestion(this,2)" runat="server"></asp:TextBox></td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblID" runat="server"></asp:Label>
                                        <asp:HiddenField ID="hdnIsregister" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="Name">
                                    <HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
                                    <HeaderTemplate>
                                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                            <tr id="trHead1">
                                                <td  align="center">
                                                    <asp:Label ID="lblHSiswa" CssClass="HeaderFieldGrid" Text="Nama" runat="server"></asp:Label></td>
                                            </tr>
                                            <tr id="trHead2">
                                                <td>
                                                    <asp:TextBox ID="txtHSiswa"  Width="100%" onkeyup="filterSuggestion(this,3)" runat="server"></asp:TextBox></td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Name") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="StartWorkingDate">
                                    <HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
                                    <HeaderTemplate>
                                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                            <tr id="trHead1">
                                                <td  align="center">
                                                    <asp:Label ID="lblHStartWork" CssClass="HeaderFieldGrid" Text="Mulai Bekerja" runat="server"></asp:Label></td>
                                            </tr>
                                            <tr id="trHead2">
                                                <td>
                                                    <asp:TextBox ID="txtHStartWork"  Width="100%" onkeyup="filterSuggestion(this,4)" runat="server"></asp:TextBox></td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblStartWork" runat="server" >
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="JobPosition" HeaderText="Posisi">
                                    <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                                    <HeaderTemplate>
                                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                            <tr id="trHead1">
                                                <td  align="center">
                                                    <asp:Label ID="lblHPosition" CssClass="HeaderFieldGrid" Text="Posisi" runat="server"></asp:Label></td>
                                            </tr>
                                            <tr id="trHead2">
                                                <td>
                                                    <asp:TextBox ID="txtHPosition" Width="100%" onkeyup="filterSuggestion(this,5)" runat="server"></asp:TextBox></td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblJobposition" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="IsTraineeRegistered" HeaderText="Status">
                                    <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                                    <HeaderTemplate>
                                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                            <tr id="trHead1">
                                                <td  align="center">
                                                    <asp:Label ID="lblHStatus" CssClass="HeaderFieldGrid" Text="Posisi" runat="server"></asp:Label></td>
                                            </tr>
                                            <tr id="trHead2">
                                                <td>
                                                    <asp:TextBox ID="txtHStatus" Width="100%" onkeyup="filterSuggestion(this,6)" runat="server"></asp:TextBox></td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="Label2" runat="server" Text='<%# IIf(DataBinder.Eval(Container, "DataItem.IsTraineeRegistered"), "Terdaftar", "Belum Terdaftar") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            
                        </asp:Gridview>
                    </div>
                </td>
            </tr>
            <tr>
                <td align="center">                    
                    <asp:Button ID="btnDaftar" runat="server" Width="80px" Text="Daftar" Font-Bold="True"></asp:Button>&nbsp;
                    <asp:Button ID="btnBack" runat="server" Width="80px" Text="Kembali" Font-Bold="True"></asp:Button>
                </td>
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
