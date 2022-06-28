<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmDownloadESRUT.aspx.vb" Inherits=".FrmDownloadESRUT" MaintainScrollPositionOnPostback="true" %>

<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>

    <title>Training Kelas Khusus</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR" />
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE" />
    <meta content="JavaScript" name="vs_defaultClientScript" />
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet" />
    <script type="text/javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script type="text/javascript" src="../WebResources/InputValidation.js"></script>

    <script type="text/javascript">
      

        function ShowPopupDealer() {
            showPopUp('../PopUp/PopUpDealerSelection.aspx', '', 500, 760, dealerSelection);
            
        }


        function dealerSelection(selectedDealer) {
            var txtKodeDealer = document.getElementById("txtKodeDealer");
            txtKodeDealer.value = selectedDealer;
            
        }

        function ShowPopUpTipe() {
            showPopUp('../PopUp/PopUpVechileTypeMultiple.aspx', '', 500, 760, tipeSelection);
        }


        function tipeSelection(selectedTipe) {
            var txtTipe = document.getElementById("txtTipe");
            txtTipe.value = selectedTipe;

        }

    </script>

    <style type="text/css">
        .style-Label {
            width: 150px;
        }

        .style-Colon {
            width: 5px;
        }

        .style-Field {
            width: 200px;
        }

        .style-Separator {
            width: 100px;
        }

        .style-RowSeparator {
            height: 10px;
        }

        .hidden {
            display: none;
        }
    </style>

</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage">
                    <asp:Label ID="lblTitle" Text="Download E-SRUT" runat="server"></asp:Label>
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
                            <td class="style-Label">Kode Dealer</td>
                            <td class="style-Colon">:</td>
                            <td class="style-Field">
                                <asp:TextBox ID="txtKodeDealer" runat="server" Width="80%"></asp:TextBox>
                                <asp:Label ID="lblPopUpKodeDealer" runat="server" Width="16px">
										<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" onclick="ShowPopupDealer();" border="0" /></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label4" runat="server" Text=" ; sebagai separator "></asp:Label></td>

                        </tr>
                        <tr>
                            <td class="style-Label">Nomor Pengajuan</td>
                            <td class="style-Colon">:</td>
                            <td class="style-Field">
                                <asp:TextBox ID="txtNomorPengajuan" runat="server" Width="80%"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label1" runat="server" Text=" ; sebagai separator "></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="style-Label">Nomor Rangka</td>
                            <td class="style-Colon">:</td>
                            <td class="style-Field">
                                <asp:TextBox ID="txtChassisNumber" runat="server" Width="80%"></asp:TextBox>

                            </td>
                            <td>
                                <asp:Label ID="lblChassisNumberDesc" runat="server" Text=" ; sebagai separator "></asp:Label></td>

                        </tr>
                        <tr>
                            <td class="style-Label">Nomor Mesin</td>
                            <td class="style-Colon">:</td>
                            <td class="style-Field">
                                <asp:TextBox ID="txtEngineNumber" runat="server" Width="80%"></asp:TextBox>

                            </td>
                            <td>
                                <asp:Label ID="lblEngineNumberDesc" runat="server" Text=" ; sebagai separator "></asp:Label></td>

                        </tr>
                         <tr>
                            <td class="style-Label">Kode Tipe</td>
                            <td class="style-Colon">:</td>
                            <td class="style-Field">
                                <asp:TextBox ID="txtTipe" runat="server" Width="80%"></asp:TextBox>
                                <asp:Label ID="lblPopUpTupe" runat="server" Width="16px">
										<img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" onclick="ShowPopUpTipe();" border="0" /></asp:Label>
                            </td>
                             <td>
                                <asp:Label ID="Label2" runat="server" Text=" ; sebagai separator "></asp:Label></td>

                        </tr>
                         <tr>
                            <td class="style-Label">Kategori</td>
                            <td class="style-Colon">:</td>
                            <td class="style-Field">
                                 <asp:DropDownList ID="ddlCategory" runat="server" Width="140px" AutoPostBack="true"></asp:DropDownList>
                                <br />
                                <asp:DropDownList ID="ddlSubCategory" runat="server"></asp:DropDownList>
                            </td>

                        </tr>
                        <tr>
                            <td class="style-Label">Tanggal DO</td>
                            <td class="style-Colon">:</td>
                            <td colspan="2">
                                <table>
                                    <tr>
                                        <td>
                                            <cc1:IntiCalendar ID="ICDOFrom" runat="server" Value=""></cc1:IntiCalendar></td>
                                        <td>s/d</td>
                                        <td>
                                            <cc1:IntiCalendar ID="ICDOTo" runat="server" Value=""></cc1:IntiCalendar></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="style-Label">Status</td>
                            <td class="style-Colon">:</td>
                            <td class="style-Field">
                                <asp:DropDownList ID="ddlStatus" runat="server"></asp:DropDownList>
                            </td>
                        </tr>

                        <tr>
                            <td></td>
                            <td></td>
                            <td colspan="2">
                                <asp:Button ID="btnCari" runat="server" Text="Cari" Width="75px" />
                                &nbsp;
                                <asp:Button ID="btnBatal" runat="server" Text="Batal" Width="75px" />
                                &nbsp;
                                <asp:Button ID="btnDownload" runat="server" Text="Download" Width="75px" />
                            </td>

                        </tr>
                    </table>
                </td>
            </tr>
            <tr class="style-RowSeparator">
                <td>
                    <asp:Button ID="btnShowPopup" runat="server" CssClass="hidden" OnClientClick="ShowPPErrorExcel()" CausesValidation="false" /></td>
            </tr>
            <tr id="rowList" runat="server">
                <td>
                    <asp:DataGrid ID="dgList" runat="server" Width="100%" CellSpacing="1" ForeColor="Gray" PageSize="10"
                        AllowSorting="True" AllowPaging="True" AllowCustomPaging="True" GridLines="Horizontal" CellPadding="3" BackColor="#CDCDCD" BorderWidth="0px"
                        BorderStyle="None" BorderColor="#CDCDCD" AutoGenerateColumns="False">
                        <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                        <AlternatingItemStyle BackColor="#F1F6FB"></AlternatingItemStyle>
                        <ItemStyle BackColor="White"></ItemStyle>
                        <HeaderStyle Font-Bold="True" ForeColor="White" BorderColor="#E0E0E0" BackColor="#CC3333"></HeaderStyle>
                        <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
                        <Columns>
                            <asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID"></asp:BoundColumn>
                            <asp:TemplateColumn Visible="False" HeaderText="No">
                                <HeaderStyle Width="3%" CssClass="titleTableService"></HeaderStyle>
                            </asp:TemplateColumn>
                          
                            <asp:TemplateColumn>
                                <HeaderStyle Width="3%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle Font-Size="XX-Small" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblNo" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                               <asp:TemplateColumn>
                                <HeaderStyle Width="3%" CssClass="titleTableService"></HeaderStyle>
                                   <HeaderTemplate>
                                       <asp:CheckBox id="chkDownloadAll" AutoPostBack="true" OnCheckedChanged="chkDownloadAll_CheckedChanged" runat="server"/>
                                   </HeaderTemplate>
                                <ItemStyle Font-Size="XX-Small" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                <ItemTemplate>
                                   <asp:CheckBox id="chkDownload" runat="server"/>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn SortExpression="ChassisMaster.Dealer.DealerCode" HeaderText="Kode Dealer">
                                <HeaderStyle Width="7%" HorizontalAlign="Center" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle Font-Size="XX-Small" VerticalAlign="Top" HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblDealerCode" runat="server">
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                           <asp:TemplateColumn SortExpression="ChassisMaster.Dealer.DealerName" HeaderText="Nama Dealer">
                                <HeaderStyle Width="17%" HorizontalAlign="Center" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle Font-Size="XX-Small" VerticalAlign="Top" HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblDealerName" runat="server">
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>

                              <asp:TemplateColumn SortExpression="ChassisNumber" HeaderText="Nomor Rangka">
                                <HeaderStyle Width="12%" HorizontalAlign="Center" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle Font-Size="XX-Small" VerticalAlign="Top" HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblChassisNumber" runat="server">
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>

                              <asp:TemplateColumn SortExpression="EngineNumber" HeaderText="Nomor Mesin">
                                <HeaderStyle Width="7%" HorizontalAlign="Center" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle Font-Size="XX-Small" VerticalAlign="Top" HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblEngineNumber" runat="server">
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                           
                             <asp:TemplateColumn SortExpression="ChassisMaster.DODate" HeaderText="Tanggal DO">
                                <HeaderStyle Width="7%" HorizontalAlign="Center" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle Font-Size="XX-Small" VerticalAlign="Top" HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblDODate" runat="server">
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>

                              <asp:TemplateColumn SortExpression="NomorSRUT" HeaderText="Nomor SRUT">
                                <HeaderStyle Width="17%" HorizontalAlign="Center" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle Font-Size="XX-Small" VerticalAlign="Top" HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblNomorSRUT" runat="server">
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>


                            <asp:TemplateColumn SortExpression="Status" HeaderText="Status">
                                <HeaderStyle Width="17%" CssClass="titleTableService" HorizontalAlign="Center"></HeaderStyle>
                                <ItemStyle Font-Size="XX-Small" VerticalAlign="Top" HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblStatus" runat="server">
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                           
                            <asp:TemplateColumn>
                                <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                                <ItemStyle Font-Size="XX-Small" Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                <ItemTemplate>
                                     <asp:Label ID="lblPopUpLog" runat="server" Width="16px">
										<img style="cursor:hand" alt="Log" src="../images/popup.gif" border="0" /></asp:Label>
                                    <asp:LinkButton ID="btnUbah" runat="server" Width="16px" Text="Download" CausesValidation="False" CommandName="Download">
												<img src="../images/print.gif" border="0" alt="Download"></asp:LinkButton>

                                     <asp:LinkButton ID="btnDelete" runat="server" Width="16px" Text="Delete" CausesValidation="False" CommandName="Delete">
												<img src="../images/trash.gif" border="0" alt="Delete"></asp:LinkButton>
                                   

                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
                    </asp:DataGrid>
                </td>

            </tr>

        </table>
    </form>
</body>
</html>
