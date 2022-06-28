<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmPendaftaranSales2.aspx.vb" Inherits="FrmPendaftaranSales2" SmartNavigation="False" %>

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
        /*function DealerSelection(selectedCode)
        {
            var txtDealer = document.getElementById("txtKodeDealer");
            txtDealer.value = selectedCode;
            txtDealer.focus();
        }
        function CheckAll(aspCheckBoxID, checkVal) 
        {
            re = new RegExp(':' + aspCheckBoxID + '$');  
            for(i = 0; i < document.forms[0].elements.length; i++) {
                elm = document.forms[0].elements[i];
                if (elm.type == 'checkbox') {
                    if (re.test(elm.name)) {
                        elm.checked = checkVal;
                    }
                }
            }
        }
        */
        function GetAllocatedAllowed() {
            var allocated = document.getElementById("divAllocatedTot");
            //var registered = document.getElementById("divAllocatedReg");
            return parseInt(allocated.innerText)
            //return parseInt(allocated.innerText) - parseInt(registered.innerText);
        }
        function CheckEnability() {
            var allowed = GetAllocatedAllowed();
            var counterCheck = 0;
            var checkString = "";
            var checkColl = document.getElementById("txtItemCheckColl");
            checkColl.value = "";
            var table = document.getElementById("dtgTrainee");
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
                    var table = document.getElementById("dtgTrainee");
                    //var i;
                    for (i = 1; i < table.rows.length; i++) {

                        var checkbox = table.rows[i].cells[0].getElementsByTagName("INPUT")[0];
                        //just watch index row not in checkColl
                        if (!CatchIt(i, arrCheckColl)) {
                            checkbox.disabled = disabled;
                        }
                        /*
                        var a;
                        for(a=0;a<arrCheckColl.length-1;a++)
                        {
                            if (arrCheckColl[a] != i)
                            {	
                                checkbox.disabled = disabled;
                            }
                        }
                        */
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



    </script>
</head>
<body ms_positioning="GridLayout">
    <form id="Form2" method="post" runat="server">
        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage">TRAINING SALES - Pendaftaran - Pilih Siswa</td>
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
                    <div id="divTrainee" style="overflow: auto; height: 360px">
                        <asp:DataGrid ID="dtgTrainee" runat="server" Font-Size="Small" AutoGenerateColumns="False" BorderColor="#E0E0E0"
                            BorderStyle="None" BorderWidth="0px" BackColor="#E0E0E0" CellPadding="3" GridLines="Vertical" CellSpacing="1" Width="100%">
                            <FooterStyle ForeColor="Black" BackColor="#CCCCCC"></FooterStyle>
                            <SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#008A8C"></SelectedItemStyle>
                            <AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
                            <ItemStyle Font-Size="Smaller" Font-Names="Microsoft Sans Serif" ForeColor="Black" BackColor="White"></ItemStyle>
                            <HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#000084"></HeaderStyle>
                            <Columns>
                                <asp:TemplateColumn>
                                    <HeaderStyle Width="3%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkItemChecked" runat="server"></asp:CheckBox>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="No">
                                    <HeaderStyle Width="3%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblNo" runat="server" Width="35px" Font-Size="Smaller"></asp:Label>
                                        <asp:TextBox runat="server" ID="txtID" Width="0px" Style="visibility: hidden;" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
                                        </asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="TrTraineeSalesmanHeader.SalesmanHeader.SalesmanCode" HeaderText="No. Reg">
                                    <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblSalesmanCode" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="Name" HeaderText="Nama">
                                    <HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Name") %>'>
                                        </asp:Label>
                                        <asp:TextBox ID="txtName" Visible="False" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Name") %>'>
                                        </asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="StartWorkingDate" SortExpression="StartWorkingDate" ReadOnly="True" HeaderText="Mulai Bekerja"
                                    DataFormatString="{0:dd/MM/yyyy}">
                                    <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:TemplateColumn SortExpression="TrTraineeSalesmanHeader.JobPosition" HeaderText="Posisi">
                                    <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblJobposition" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Grade">
                                    <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblGrade" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Grade Sementara">
                                    <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblGradeTemp" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="IsTraineeRegistered" HeaderText="Status">
                                    <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="Label2" runat="server" Text='<%# IIf(DataBinder.Eval(Container, "DataItem.IsTraineeRegistered"), "Terdaftar", "Belum Terdaftar") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn Visible="False" DataField="IsTraineeRegistered" ReadOnly="True">
                                    <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:BoundColumn>
                            </Columns>
                            <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
                        </asp:DataGrid>
                    </div>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Button ID="btnBack" runat="server" Width="80px" Text="Kembali" Font-Bold="True"></asp:Button><asp:Button ID="btnDaftar" runat="server" Width="80px" Text="Daftar" Font-Bold="True"></asp:Button></td>
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
