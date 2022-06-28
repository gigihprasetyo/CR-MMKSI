<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmTrCertificateLine3.aspx.vb" Inherits="FrmTrCertificateLine3" SmartNavigation="False" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>FrmTrCertificateLine3</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script language="javascript">

        function ShowPPCourseSelection(obj) {
            showPopUp('../General/../PopUp/PopUpCourse.aspx?category=' + obj.toString(), '', 550, 760, courseSelection);
        }

        function courseSelection(selectedCourse) {
            var txtKodekategori = document.getElementById("txtKodeKategori");
            txtKodekategori.value = selectedCourse;
        }

        function ChangedData() {
            var StrHiden = document.getElementById("hidenTxt");
            StrHiden.value = "1"
        }

        function CheckChangedData() {
            var StrHiden = document.getElementById("hidenTxt");

            if (StrHiden.value == "1") {
                alert("Perubahan data harus disimpan terlebih dahulu !");
                return false;
            }
            return true;
        }

        function ResetChangeData() {
            var StrHiden = document.getElementById("hidenTxt");
            StrHiden.value = "0"
            return true;
        }

        function ClassSelection(selectedCode) {
            var tempParam = selectedCode.split(';');
            var str1 = document.getElementById("txtClassCode");
            var str2 = document.getElementById("txtClassName");
            //var str3 = document.getElementById("txtRegNo");
            //var str4 = document.getElementById("txtTrainee");
            {
                str1.value = tempParam[0];
                str2.value = tempParam[1];
                //str3.value='';
                //str4.value='';
                //var txtRegNoVar = document.getElementById("txtRegNo");
                //txtRegNoVar.focus();
                str1.focus()
            }
        }

        function TraineeSelection(selectedCode) {
            var tempParam = selectedCode.split(';');
            //var str1 = document.getElementById("txtHidenCourseID");
            var str2 = document.getElementById("txtRegNo");
            var str3 = document.getElementById("txtTrainee");
            var btn1 = document.getElementById("btnRefresh");
            //str1.value=tempParam[0];
            str2.value = tempParam[1];
            str3.value = tempParam[2];
            btn1.click();
        }


        function ShowPopupTraineeSelection() {
            var txtClassCode = document.getElementById("txtClassCode");
            showPopUp('../PopUp/PopUpClassRegistration.aspx?ClassCode=' + txtClassCode.value, '', 500, 760, TraineeSelection)
        }

        function ShowPopupClassSelection() {
            var txtClassCode = document.getElementById("txtClassCode");
            var txtYear = document.getElementById("ddlFiscalYear");
            var txtCourse = document.getElementById("txtKodeKategori");
            var hdnArea = document.getElementById("hdnAreaID");

            showPopUp('../PopUp/PopUpClassSelection.aspx?areaid=' + hdnArea.value + '&fiscalyear=' + txtYear.value + '&CourseCode=' + txtCourse.value, '', 500, 760, ClassSelection)
        }

        function GetCatatan() {
            return false
        }

        function SelectAll(CheckBoxControl) {
            if (CheckBoxControl.checked == true) {
                var i;
                for (i = 0; i < document.forms[0].elements.length; i++) {
                    if ((document.forms[0].elements[i].type == 'checkbox') &&
					(document.forms[0].elements[i].name.indexOf('dgNumEval') > -1)) {
                        document.forms[0].elements[i].checked = true;
                    }
                }
            }
            else {
                var i;
                for (i = 0; i < document.forms[0].elements.length; i++) {
                    if ((document.forms[0].elements[i].type == 'checkbox') &&
                    (document.forms[0].elements[i].name.indexOf('dgNumEval') > -1)) {
                        document.forms[0].elements[i].checked = false;
                    }
                }
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
    <link rel="stylesheet" type="text/css" href="../WebResources/stylesheet.css">
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <asp:HiddenField ID="hdnAreaID" runat="server" />
        <table id="Table1" border="0" cellspacing="0" cellpadding="0" width="100%">
            <tr>
                <td style="width: 750px" class="titlePage">
                    <asp:Label ID="lblPageTitle" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 750px" height="1" background="../images/bg_hor.gif">
                    <img border="0" src="../images/bg_hor.gif" height="1"></td>
            </tr>
            <tr>
                <td style="width: 750px" height="10">
                    <img border="0" src="../images/dot.gif" height="1"></td>
            </tr>
            <tr>
                <td style="width: 750px" align="left">
                    <table id="Table2" border="0" cellspacing="1" cellpadding="2">
                        <tr>
                            <td width="24%" class="titleField">Tahun Fiskal</td>
                            <td style="height: 19px">:</td>
                            <td width="75%" style="height: 19px" nowrap="nowrap">
                                <asp:DropDownList ID="ddlFiscalYear" Width="120px" AutoPostBack="true" runat="server" TabIndex="9"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td width="150" class="titleField">Kode Kategori</td>
                            <td style="height: 10px">:</td>
                            <td width="80%" nowrap="nowrap">
                                <asp:TextBox ID="txtKodeKategori" onkeypress="return HtmlCharUniv(event)" runat="server" MaxLength="20"
                                    Width="120px" AutoPostBack="False" TabIndex="8"></asp:TextBox><asp:Label ID="lblPopUpCourse" runat="server" Width="16px">
													<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0"></asp:Label>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="titleField" width="24%">Kelas</td>
                            <td style="height: 19px" width="1%">:</td>
                            <td style="height: 19px" width="75%">
                                <asp:TextBox ID="txtClassCode" TabIndex="3" onkeypress="return HtmlCharUniv(event)" runat="server"
                                    Width="136px"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Width="6px" ErrorMessage="*" ControlToValidate="txtClassCode"></asp:RequiredFieldValidator><asp:TextBox ID="txtClassName" TabIndex="4" runat="server" Width="272px" ReadOnly="True"></asp:TextBox><asp:Label ID="lblPopUpClass" runat="server" ToolTip="Klik PopUp" Width="16px" Visible="False">
										<img style="cursor:hand" src="../images/popup.gif" border="0"></asp:Label><img style="cursor: hand" id="lblpopup" onclick="ShowPopupClassSelection()" alt="" src="../images/popup.gif"
                                            width="16" height="16"></td>
                        </tr>
                        <tr>
                            <td style="height: 25px" class="titleField">Jenis Evaluasi</td>
                            <td style="height: 25px" width="1%">:</td>
                            <td style="height: 25px" width="75%">
                                <asp:DropDownList ID="ddlJenis" TabIndex="5" runat="server" Width="208px" AutoPostBack="True"></asp:DropDownList>&nbsp;&nbsp;
                                <asp:Button ID="btnRefresh" TabIndex="6" runat="server" Width="72px" Text="Cari" CausesValidation="False"></asp:Button>
                                <asp:HiddenField ID="hdnJenisNilai" runat="server" />
                                <asp:HiddenField ID="hdnClassCode" runat="server" />
                                <asp:HiddenField ID="hdnCourseID" runat="server" />
                            </td>
                        </tr>
                        <tr id="trTemplate" runat="server" visible="false">
                            <td class="titleField">&nbsp;</td>
                            <td style="height: 25px"></td>
                            <td style="height: 25px">
                                <asp:LinkButton ID="linkTemplate" runat="server" CausesValidation="false">Download Template</asp:LinkButton></td>
                        </tr>
                        <tr id="trUpload" runat="server" visible="false">
                            <td class="titleField">Upload Nilai</td>
                            <td style="height: 25px">:</td>
                            <td style="height: 25px">
                                <input onkeypress="return false;" id="fileUpload" style="width: 350px; height: 20px" type="file"
                                    size="46" name="fileUpload" runat="server">
                                <asp:Button ID="btnUpload" runat="server" Text="Upload"></asp:Button>

                            </td>
                        </tr>
                        <tr>
                            <td class="titleField"></td>
                            <td style="height: 25px"></td>
                            <td style="height: 25px">
                                <input id="hidenTxt" type="hidden"></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table id="Table7" border="0" cellspacing="0" cellpadding="0" width="100%">
                        <tr>
                            <td>
                                <asp:DataGrid ID="dgNumEval" DataKeyField="ID" AutoGenerateColumns="False" runat="server">
                                    <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                                    <AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
                                    <ItemStyle BackColor="White"></ItemStyle>
                                    <HeaderStyle Font-Bold="True" ForeColor="White" BorderColor="#E0E0E0" BackColor="#CC3333"></HeaderStyle>
                                    <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
                                    <Columns>
                                        <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="Type" HeaderText="Type" Visible="False"></asp:BoundColumn>
                                        <asp:TemplateColumn HeaderText="Type">
                                            <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="LblType" runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:BoundColumn DataField="EvaluationCode" HeaderText="Kode Evaluasi">
                                            <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="Name" HeaderText="Nama Umum">
                                            <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                        </asp:BoundColumn>
                                        <asp:TemplateColumn HeaderText="Nama Khusus">
                                            <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtSpecialName" runat="server"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="">
                                            <HeaderTemplate>
                                                <input type="CheckBox" name="SelectAllCheckBox" onclick="SelectAll(this)">
                                            </HeaderTemplate>
                                            <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkNumEval" runat="server"></asp:CheckBox>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                    </Columns>
                                </asp:DataGrid></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnSaveNumEval" Visible="False" Text="Simpan" runat="server"></asp:Button>&nbsp;&nbsp;<asp:Button ID="btnUpdateNumEval" Visible="False" Text="Ubah" runat="server"></asp:Button></td>
            </tr>
            <tr>
                <td>
                    <br>
                    <br>
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <div style="overflow: auto; height: 310px" id="div1">
                        <asp:DataGrid ID="dtgClassRegistration" runat="server" Width="100%" AutoGenerateColumns="False"
                            CellSpacing="1" ForeColor="Gray" PageSize="25" AllowSorting="True" GridLines="Horizontal" CellPadding="3" BackColor="#CDCDCD" BorderWidth="0px"
                            BorderStyle="None" BorderColor="#CDCDCD">
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
                                    <ItemTemplate>
                                        <asp:Label ID="lblNo" runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="TrTrainee.ID" HeaderText="No. Reg Siswa">
                                    <HeaderStyle Width="4%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblTrId" runat="server"></asp:Label>
                                        <asp:Label ID="lblSalesmanCode" runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox runat="server"></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="TrTrainee.Name" HeaderText="Nama Siswa">
                                    <HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblTraineeName" Text='<%# (DataBinder.Eval(Container, "DataItem.TrTrainee.Name")) %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="Dealer.DealerCode" HeaderText="Kode Org.">
                                    <HeaderStyle Width="8%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblKodeOrg" runat="server" Text='<%# (DataBinder.Eval(Container, "DataItem.Dealer.DealerCode")) %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Test Awal">
                                    <HeaderStyle Width="4%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox onkeypress="return numericOnlyWithComa(event)" ID="TxtTestAwal" runat="server" Width="32px"
                                            ToolTip="diisi dengan angka skala [0,100]" BorderStyle="Inset" BorderWidth="2px" MaxLength="5"></asp:TextBox>
                                        <asp:Label ID="lblMsgTestAwal" runat="server" ForeColor="Red"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Test 1">
                                    <HeaderStyle Width="4%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox onkeypress="return numericOnlyWithComa(event)" ID="TxtTest1" runat="server" Width="32px"
                                            ToolTip="diisi dengan angka skala [0,100]" BorderStyle="Inset" BorderWidth="2px" MaxLength="5"></asp:TextBox>
                                        <asp:Label ID="lblMsgTest1" runat="server" ForeColor="Red"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Test 2">
                                    <HeaderStyle Width="4%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox onkeypress="return numericOnlyWithComa(event)" ID="TxtTest2" runat="server" Width="32px"
                                            ToolTip="diisi dengan angka skala [0,100]" BorderStyle="Inset" BorderWidth="2px" MaxLength="5"></asp:TextBox>
                                        <asp:Label ID="lblMsgTest2" runat="server" ForeColor="Red"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Test 3">
                                    <HeaderStyle HorizontalAlign="Center" Width="4%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox onkeypress="return numericOnlyWithComa(event)" ID="TxtTest3" runat="server" Width="32px"
                                            ToolTip="diisi dengan angka skala [0,100]" BorderStyle="Inset" BorderWidth="2px" MaxLength="5"></asp:TextBox>
                                        <asp:Label ID="lblMsgTest3" runat="server" ForeColor="Red"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Test 4">
                                    <HeaderStyle HorizontalAlign="Center" Width="4%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox onkeypress="return numericOnlyWithComa(event)" ID="TxtTest4" runat="server" Width="32px"
                                            ToolTip="diisi dengan angka skala [0,100]" BorderStyle="Inset" BorderWidth="2px" MaxLength="5"></asp:TextBox>
                                        <asp:Label ID="lblMsgTest4" runat="server" ForeColor="Red"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Test 5">
                                    <HeaderStyle HorizontalAlign="Center" Width="4%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox onkeypress="return numericOnlyWithComa(event)" ID="TxtTest5" runat="server" Width="32px"
                                            ToolTip="diisi dengan angka skala [0,100]" BorderStyle="Inset" BorderWidth="2px" MaxLength="5"></asp:TextBox>
                                        <asp:Label ID="lblMsgTest5" runat="server" ForeColor="Red"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Test 6">
                                    <HeaderStyle HorizontalAlign="Center" Width="4%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox onkeypress="return numericOnlyWithComa(event)" ID="TxtTest6" runat="server" Width="32px"
                                            ToolTip="diisi dengan angka skala [0,100]" BorderStyle="Inset" BorderWidth="2px" MaxLength="5"></asp:TextBox>
                                        <asp:Label ID="lblMsgTest6" runat="server" ForeColor="Red"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Test 7">
                                    <HeaderStyle HorizontalAlign="Center" Width="4%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox onkeypress="return numericOnlyWithComa(event)" ID="TxtTest7" runat="server" Width="32px"
                                            ToolTip="diisi dengan angka skala [0,100]" BorderStyle="Inset" BorderWidth="2px" MaxLength="5"></asp:TextBox>
                                        <asp:Label ID="lblMsgTest7" runat="server" ForeColor="Red"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn Visible="True" HeaderText="Test 8">
                                    <HeaderStyle HorizontalAlign="Center" Width="4%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox onkeypress="return numericOnlyWithComa(event)" ID="txtTest8" runat="server" Width="32px"
                                            ToolTip="diisi dengan angka skala [0,100]" BorderStyle="Inset" BorderWidth="2px" MaxLength="5"></asp:TextBox>
                                        <asp:Label ID="lblMsgTest8" runat="server" ForeColor="Red"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn Visible="True" HeaderText="Test 9">
                                    <HeaderStyle HorizontalAlign="Center" Width="4%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox onkeypress="return numericOnlyWithComa(event)" ID="txtTest9" runat="server" Width="32px"
                                            ToolTip="diisi dengan angka skala [0,100]" BorderStyle="Inset" BorderWidth="2px" MaxLength="5"></asp:TextBox>
                                        <asp:Label ID="lblMsgTest9" runat="server" ForeColor="Red"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Test Akhir">
                                    <HeaderStyle HorizontalAlign="Center" Width="4%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="TxtTestAkhir" onkeypress="return numericOnlyWithComa(event)" runat="server"
                                            Width="32px" ToolTip="diisi dengan angka skala [0,100]" BorderStyle="Inset" BorderWidth="2px"
                                            MaxLength="5"></asp:TextBox>
                                        <asp:Label ID="lblMsgTestAkhir" runat="server" ForeColor="Red"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="">
                                    <HeaderStyle HorizontalAlign="Center" Width="4%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <HeaderTemplate>
                                        <label style="color: white; text-align: center">Status</label>
                                        <input id="chkAllItems" type="checkbox" onclick="CheckAll('chkPass', document.forms[0].chkAllItems.checked)" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkPass" runat="server" OnCheckedChanged="chkPass_CheckedChanged" AutoPostBack="true" OnClick="ChangedData()"></asp:CheckBox>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Catatan">
                                    <HeaderStyle HorizontalAlign="Center" Width="5%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        &nbsp;
											<asp:Label ID="lbtnCatatan" runat="server" CssClass="menuImage">
												<img src="../images/edit.gif" border="0" alt="Ubah"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                            <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
                        </asp:DataGrid><asp:DataGrid ID="dtgClassRegistration2" runat="server" Width="100%" AutoGenerateColumns="False"
                            CellSpacing="1" ForeColor="Gray" PageSize="5" AllowSorting="True" GridLines="Horizontal" CellPadding="3"
                            BackColor="#CDCDCD" BorderWidth="0px" BorderStyle="None" BorderColor="#CDCDCD">
                            <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                            <AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
                            <ItemStyle BackColor="White"></ItemStyle>
                            <HeaderStyle Font-Bold="True" ForeColor="White" BorderColor="#E0E0E0" BackColor="#CC3333"></HeaderStyle>
                            <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
                            <Columns>
                                <asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
                                    <HeaderStyle CssClass="titleTableService"></HeaderStyle>
                                </asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="No">
                                    <HeaderStyle Width="3%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblNo" runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="TrTrainee.ID" HeaderText="No. Reg Siswa">
                                    <HeaderStyle Width="4%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblTrId" runat="server"></asp:Label>
                                        <asp:Label ID="lblSalesmanCode" runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox runat="server"></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="TrTrainee.Name" HeaderText="Nama Siswa">
                                    <HeaderStyle Width="14%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblTraineeName" Text='<%# (DataBinder.Eval(Container, "DataItem.TrTrainee.Name")) %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="Dealer.DealerCode" HeaderText="Kode Org.">
                                    <HeaderStyle Width="8%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblKodeOrg" runat="server" Text='<%# (DataBinder.Eval(Container, "DataItem.Dealer.DealerCode")) %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Nilai Sikap 1">
                                    <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="TxtTest1" runat="server" Width="30px" BorderStyle="Inset" BorderWidth="2px"
                                            MaxLength="1"></asp:TextBox>
                                        <asp:Label ID="lblMsgTest1" runat="server" ForeColor="Red"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Nilai Sikap 2">
                                    <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="TxtTest2" runat="server" Width="30px" BorderStyle="Inset" BorderWidth="2px"
                                            MaxLength="1"></asp:TextBox>
                                        <asp:Label ID="lblMsgTest2" runat="server" ForeColor="Red"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Nilai Sikap 3">
                                    <HeaderStyle HorizontalAlign="Center" Width="5%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="TxtTest3" runat="server" Width="30px" BorderWidth="2px" BorderStyle="Inset"
                                            MaxLength="1"></asp:TextBox>
                                        <asp:Label ID="lblMsgTest3" runat="server" ForeColor="Red"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Nilai Sikap 4">
                                    <HeaderStyle HorizontalAlign="Center" Width="5%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="TxtTest4" runat="server" Width="30px" BorderStyle="Inset" BorderWidth="2px"
                                            MaxLength="1"></asp:TextBox>
                                        <asp:Label ID="lblMsgTest4" runat="server" ForeColor="Red"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Nilai Sikap 5">
                                    <HeaderStyle HorizontalAlign="Center" Width="5%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="TxtTest5" runat="server" Width="30px" BorderWidth="2px" BorderStyle="Inset"
                                            MaxLength="1"></asp:TextBox>
                                        <asp:Label ID="lblMsgTest5" runat="server" ForeColor="Red"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Nilai Sikap 6">
                                    <HeaderStyle HorizontalAlign="Center" Width="5%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="TxtTest6" runat="server" Width="30px" BorderWidth="2px" BorderStyle="Inset"
                                            MaxLength="1"></asp:TextBox>
                                        <asp:Label ID="lblMsgTest6" runat="server" ForeColor="Red"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Nilai Sikap 7">
                                    <HeaderStyle HorizontalAlign="Center" Width="5%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="TxtTest7" runat="server" Width="30px" BorderWidth="2px" BorderStyle="Inset"
                                            MaxLength="1"></asp:TextBox>
                                        <asp:Label ID="lblMsgTest7" runat="server" ForeColor="Red"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Nilai Sikap 8">
                                    <HeaderStyle HorizontalAlign="Center" Width="5%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="TxtTest8" runat="server" Width="30px" BorderWidth="2px" BorderStyle="Inset"
                                            MaxLength="1"></asp:TextBox>
                                        <asp:Label ID="lblMsgTest8" runat="server" ForeColor="Red"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Nilai Sikap 9" Visible="True">
                                    <HeaderStyle HorizontalAlign="Center" Width="5%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="TxtTest9" runat="server" Width="30px" BorderWidth="2px" BorderStyle="Inset"
                                            MaxLength="1"></asp:TextBox>
                                        <asp:Label ID="lblMsgTest9" runat="server" ForeColor="Red"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Catatan">
                                    <HeaderStyle HorizontalAlign="Center" Width="6%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lbtnCatatan" runat="server" CssClass="menuImage">
												<img src="../images/edit.gif" border="0" alt="Ubah"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                            <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
                        </asp:DataGrid><asp:DataGrid ID="dtgClassRegistration3" runat="server" Width="100%" AutoGenerateColumns="False"
                            CellSpacing="1" ForeColor="Gray" PageSize="5" AllowSorting="True" GridLines="Horizontal" CellPadding="3"
                            BackColor="#CDCDCD" BorderWidth="0px" BorderStyle="None" BorderColor="#CDCDCD">
                            <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                            <AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
                            <ItemStyle BackColor="White"></ItemStyle>
                            <HeaderStyle Font-Bold="True" ForeColor="White" BorderColor="#E0E0E0" BackColor="#CC3333"></HeaderStyle>
                            <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
                            <Columns>
                                <asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
                                    <HeaderStyle CssClass="titleTableService"></HeaderStyle>
                                </asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="No">
                                    <HeaderStyle Width="3%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblNo" runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="TrTrainee.ID" HeaderText="No. Reg">
                                    <HeaderStyle Width="4%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblTrId" runat="server"></asp:Label>
                                        <asp:Label ID="lblSalesmanCode" runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox runat="server"></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="TrTrainee.Name" HeaderText="Nama Siswa">
                                    <HeaderStyle Width="14%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblTraineeName" Text='<%# (DataBinder.Eval(Container, "DataItem.TrTrainee.Name")) %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="Dealer.DealerCode" HeaderText="Kode Org.">
                                    <HeaderStyle Width="8%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblKodeOrg" runat="server" Text='<%# (DataBinder.Eval(Container, "DataItem.Dealer.DealerCode")) %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Nilai Prestasi 1">
                                    <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="TxtTest1" runat="server" Width="30px" BorderStyle="Inset" BorderWidth="2px"
                                            MaxLength="1"></asp:TextBox>
                                        <asp:Label ID="lblMsgTest1" runat="server" ForeColor="Red"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Nilai Prestasi 2">
                                    <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="TxtTest2" runat="server" Width="30px" BorderWidth="2px" BorderStyle="Inset"
                                            MaxLength="1"></asp:TextBox>
                                        <asp:Label ID="lblMsgTest2" runat="server" ForeColor="Red"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Nilai Prestasi 3">
                                    <HeaderStyle HorizontalAlign="Center" Width="5%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="TxtTest3" runat="server" Width="30px" BorderWidth="2px" BorderStyle="Inset"
                                            MaxLength="1"></asp:TextBox>
                                        <asp:Label ID="lblMsgTest3" runat="server" ForeColor="Red"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Nilai Prestasi 4">
                                    <HeaderStyle HorizontalAlign="Center" Width="5%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="TxtTest4" runat="server" Width="30px" BorderWidth="2px" BorderStyle="Inset"
                                            MaxLength="1"></asp:TextBox>
                                        <asp:Label ID="lblMsgTest4" runat="server" ForeColor="Red"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Nilai Prestasi 5">
                                    <HeaderStyle HorizontalAlign="Center" Width="5%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="TxtTest5" runat="server" Width="30px" BorderWidth="2px" BorderStyle="Inset"
                                            MaxLength="1"></asp:TextBox>
                                        <asp:Label ID="lblMsgTest5" runat="server" ForeColor="Red"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Nilai Prestasi 6">
                                    <HeaderStyle HorizontalAlign="Center" Width="5%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="TxtTest6" runat="server" Width="30px" BorderWidth="2px" BorderStyle="Inset"
                                            MaxLength="1"></asp:TextBox>
                                        <asp:Label ID="lblMsgTest6" runat="server" ForeColor="Red"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Nilai Prestasi 7">
                                    <HeaderStyle HorizontalAlign="Center" Width="5%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="TxtTest7" runat="server" Width="30px" BorderWidth="2px" BorderStyle="Inset"
                                            MaxLength="1"></asp:TextBox>
                                        <asp:Label ID="lblMsgTest7" runat="server" ForeColor="Red"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn Visible="False" HeaderText="Nilai Prestasi 8">
                                    <HeaderStyle HorizontalAlign="Center" Width="5%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="TxtTest8" runat="server" Width="30px" BorderWidth="2px" Text="0" BorderStyle="Inset"
                                            MaxLength="1"></asp:TextBox>
                                        <asp:Label ID="lblMsgTest8" runat="server" ForeColor="Red"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn Visible="False" HeaderText="Nilai Prestasi 9">
                                    <HeaderStyle HorizontalAlign="Center" Width="5%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="TxtTest9" runat="server" Width="30px" BorderWidth="2px" Text="0" BorderStyle="Inset"
                                            MaxLength="1"></asp:TextBox>
                                        <asp:Label ID="lblMsgTest9" runat="server" ForeColor="Red"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Catatan">
                                    <HeaderStyle HorizontalAlign="Center" Width="6%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lbtnCatatan" runat="server" CssClass="menuImage">
												<img src="../images/edit.gif" border="0" alt="Ubah"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                            <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
                        </asp:DataGrid>
                    </div>
                </td>
            </tr>
            <tr id ="trAction" runat="server">
                <td style="width: 750px; height: 40px">
                    <p>
                        <asp:Button ID="btnInsert" runat="server" Width="72px" Text="Simpan"></asp:Button>
                        <asp:Button ID="btnHitung" runat="server" Text="Nilai Rata-Rata dan Ranking"></asp:Button>
                        <asp:Button ID="btnStatus" runat="server" Text="Update Status"></asp:Button>
                    </p>
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
