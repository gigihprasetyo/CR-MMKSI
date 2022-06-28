<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmClassConfirmationCS.aspx.vb" Inherits=".FrmClassConfirmationCS" %>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>

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

    <style type="text/css">
        .hidden {
            display: none;
        }
    </style>

    <script type="text/javascript">

        function ShowPPClassSelection() {
            showPopUp('../PopUp/PopUpClassSelection.aspx?areaid=3', '', 500, 760, classSelection);
        }

        function classSelection(selectedClass) {
            var tempParam = selectedClass.split(';');
            var txtClassCode = document.getElementById("txtClassCode");
            txtClassCode.value = tempParam[0];
        }

    </script>

   
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage">
                    <asp:Label ID="lblPageTitle" Text="Training CS - Konfirmasi Pendaftaran Kelas" runat="server"></asp:Label>
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
                    <table id="tbl7" cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td width="60%">
                                <table id="table5">
                                    <tr>
                                        <td width="150" class="titleField">Tahun Fiskal</td>
                                        <td width="10px">:</td>
                                        <td width="80%" nowrap="nowrap">
                                            <asp:DropDownList ID="ddlTahunFiscal" Width="120px" runat="server" ></asp:DropDownList></td>
                                    </tr>

                                    <tr>
                                        <td width="150" class="titleField">Status</td>
                                        <td width="10px">:</td>
                                        <td width="80%" nowrap="nowrap">
                                            <asp:DropDownList ID="ddlStatus" Width="120px" runat="server" ></asp:DropDownList></td>
                                    </tr>

                                    <tr>
                                        <td width="150" class="titleField">Kode Kelas</td>
                                        <td width="10px">:</td>
                                        <td width="80%" nowrap="nowrap">
                                            <asp:TextBox ID="txtClassCode"
                                                runat="server" Width="191px"></asp:TextBox><asp:Label ID="lblPopUpClass" runat="server">
										<img style="cursor:hand" alt="Klik disini" src="../images/popup.gif" onclick="ShowPPClassSelection()"
											border="0"></asp:Label></td>
                                    </tr>

                                    <tr>
                                        <td width="150" class="titleField">Periode Kelas</td>
                                        <td width="10px">:</td>
                                        <td width="80%" nowrap="nowrap">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <cc1:inticalendar id="ICTanggalMasukFrom" runat="server" value=""></cc1:inticalendar>
                                                    </td>
                                                    <td>s/d</td>
                                                    <td>
                                                        <cc1:inticalendar id="ICTanggalMasukTo" runat="server" value=""></cc1:inticalendar>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td class="titleField"></td>
                                        <td width="10px"></td>
                                        <td>
                                            <asp:Button ID="btnCari" runat="server" Text="Cari" Width="75px" />
                                            &nbsp;
                                            
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titleField"></td>
                                        <td width="10px"></td>
                                        <td></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <div id="div1" style="overflow: auto; height: 340px">
                        <asp:DataGrid ID="dgList" runat="server" Width="100%" AutoGenerateColumns="False"
                            GridLines="Horizontal" CellPadding="3" BackColor="Gainsboro" BorderWidth="0px" BorderStyle="None" BorderColor="#CDCDCD" AllowPaging="True"
                            AllowCustomPaging="True" AllowSorting="True" PageSize="25" ForeColor="GhostWhite" CellSpacing="1" Font-Names="MS Reference Sans Serif">
                            <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                            <AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
                            <ItemStyle BackColor="White"></ItemStyle>
                            <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
                            <Columns>
                                <asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID"></asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="No">
                                    <HeaderStyle Width="3%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Font-Size="XX-Small" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblNo" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>

                                <asp:TemplateColumn SortExpression="TrTrainee.Name" HeaderText="Nama Siswa">
                                    <HeaderStyle Width="17%" HorizontalAlign="Center" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Font-Size="XX-Small" VerticalAlign="Top" HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblTraineeName" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="TrClass.ClassCode" HeaderText="Kode Kelas">
                                    <HeaderStyle Width="7%" HorizontalAlign="Center" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Font-Size="XX-Small" VerticalAlign="Top" HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblClassCode" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>

                                <asp:TemplateColumn SortExpression="TrClass.TrCourse.CourseName" HeaderText="Kategori Kursus">
                                    <HeaderStyle Width="12%" HorizontalAlign="Center" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Font-Size="XX-Small" VerticalAlign="Top" HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblCourseCategory" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>

                                  <asp:TemplateColumn SortExpression="Status" HeaderText="Status">
                                    <HeaderStyle Width="7%" HorizontalAlign="Center" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Font-Size="XX-Small" VerticalAlign="Top" HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblStatus" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>

                                <asp:TemplateColumn SortExpression="TrClass.Location" HeaderText="Lokasi">
                                    <HeaderStyle Width="10%" HorizontalAlign="Center" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Font-Size="XX-Small" VerticalAlign="Top" HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblLocation" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>

                                <asp:TemplateColumn SortExpression="TrClass.FiscalYear" HeaderText="Tahun Fiskal">
                                    <HeaderStyle Width="7%" HorizontalAlign="Center" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Font-Size="XX-Small" VerticalAlign="Top" HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblFiscalYear" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>

                                <asp:TemplateColumn SortExpression="TrClass.StartDate" HeaderText="Tanggal">
                                    <HeaderStyle Width="10%" HorizontalAlign="Center" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Font-Size="XX-Small" VerticalAlign="Top" HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblDate" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>

                                 <asp:TemplateColumn SortExpression="Notes" HeaderText="Notes">
                                    <HeaderStyle Width="10%" HorizontalAlign="Center" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Font-Size="XX-Small" VerticalAlign="Top" HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblNotes" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>

                                <asp:TemplateColumn>
                                    <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                                    <ItemStyle Font-Size="XX-Small" Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                    <ItemTemplate>

                                        <asp:LinkButton ID="btnConfirmSingle" runat="server" Width="16px" Text="Konfirmasi" CausesValidation="False" CommandName="Confirm">
												<img src="../images/aktif.gif" border="0" alt="Konfirmasi"></asp:LinkButton>

                                        <asp:LinkButton ID="btnReject" runat="server" Width="16px" Text="Tolak" CausesValidation="False" CommandName="Reject">
												<img src="../images/in-aktif.gif" border="0" alt="Tolak"></asp:LinkButton>


                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                            <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
                        </asp:DataGrid>
                    </div>
                </td>
                <asp:HiddenField ID="hdnRejectReason" runat="server" />
            </tr>


            <tr>
                <td></td>
            </tr>
        </table>
    </form>

</body>
</html>

 <script type="text/javascript">
     function GetRejectReason() {
        var reason = prompt("Silakan input alasan penolakan..", "...");
        var hdnReject = document.getElementById('hdnRejectReason');
        if (reason != null) {
            hdnReject.value = reason;
                return true;
            }
            else
                return false;
        }

    </script>

