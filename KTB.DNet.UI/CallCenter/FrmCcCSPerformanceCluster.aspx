<%@ Page Language="vb" MaintainScrollPositionOnPostback="true" AutoEventWireup="false" CodeBehind="FrmCcCSPerformanceCluster.aspx.vb" Inherits=".FrmCcCSPerformanceCluster" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Cluster Dealer Form</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script type="text/javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script type="text/javascript" src="../WebResources/InputValidation.js"></script>

    <script type="text/javascript" src="../WebResources/jquery-1.8.3.min.js"></script>
    <script type="text/javascript">
        $("[src*=plus]").live("click", function () {
            $(this).closest("tr").after("<tr><td></td><td colspan = '50'>" + $(this).next().html() + "</td></tr>")
            $(this).attr("src", "../images/minus.gif");
        });
        $("[src*=minus]").live("click", function () {
            $(this).attr("src", "../images/plus.gif");
            $(this).closest("tr").next().remove();
        });


        function ShowPPDealerSelection(obj) {
            var hdnTxt = document.getElementById('<%= hdnTxtDealerCode.ClientID %>') 
            hdnTxt.value = obj
            showPopUp('../General/../PopUp/PopUpDealerSelectionOne.aspx', '', 500, 760, DealerSelection);
        }

        function DealerSelection(selectedDealer) {
            var data = selectedDealer.split(";");
            var hdnTxt = document.getElementById('<%= hdnTxtDealerCode.ClientID %>') 
            var txtDealerSelection = document.getElementById(hdnTxt.value);
            txtDealerSelection.value = data[0];;
        }
    </script>

    <style type="text/css">
        .auto-style1 {
            font-family: Sans-Serif, Arial;
            font-size: 11px;
            color: #000000;
            margin: 0px;
            font-weight: bold;
            text-align: left;
            width: 88px;
            height: 44px;
        }

        .auto-style2 {
            height: 44px;
        }

        .auto-style3 {
            width: 330px;
            height: 44px;
        }
    </style>
</head>
<body ms_positioning="GridLayout">
    <form id="FrmDealAssessmentMaster" runat="server">
        <table id="table1" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="titlePage">CS Performance - Form Cluster Dealer</td>
            </tr>
            <tr>
                <td background="../images/bg_hor.gif" height="1">
                    <img height="1" src="../images/bg_hor.gif" border="0"></td>
            </tr>
            <tr>
                <td height="10">
                    <img height="1" src="../images/dot.gif" border="0"></td>
            </tr>
            <tr id="trAktif" runat="server">
                <td align="left">
                    <table id="Table2" cellspacing="1" cellpadding="2" width="100%" border="0">
                        <tr valign="top">
                            <td class="titleField">Performance Master</td>
                            <td width="1%">:</td>
                            <td style="width: 330px" width="262">
                                <asp:Label ID="lblPerformanceMaster" runat="server" Text="Performance Master Name"></asp:Label>
                                <asp:HiddenField ID="hdnClusterID" runat="server" />
                            </td>
                        </tr>

                        <tr valign="top">
                            <td class="titleField" style="width: 88px" width="88">Nama Cluster</td>
                            <td width="1%">:</td>
                            <td style="width: 330px" width="262">
                                <asp:TextBox runat="server" ID="txtDescription" Width="200px" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"
                                    ControlToValidate="txtDescription" EnableClientScript="false"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr valign="top">
                            <td class="titleField" style="width: 88px" width="88">Periode</td>
                            <td width="1%">:</td>
                            <td style="width: 330px" width="262">
                                <asp:DropDownList SortExpression="PeriodIDFrom" Width="100px" ID="ddlPeriodeFrom" runat="server">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" EnableClientScript="false" runat="server" ErrorMessage="*" ControlToValidate="ddlPeriodeFrom"></asp:RequiredFieldValidator>
                                &nbsp;s/d &nbsp;
                                <asp:DropDownList SortExpression="PeriodIDTo" Width="100px" ID="ddlPeriodeTo" runat="server">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" EnableClientScript="false" runat="server" ErrorMessage="*" ControlToValidate="ddlPeriodeTo"></asp:RequiredFieldValidator>
                            </td>
                        </tr>


                        <tr valign="top">
                            <td class="titleField">Batas Penjualan </td>
                            <td width="1%">:</td>
                            <td style="width: 330px" width="262">
                                <asp:TextBox runat="server" ID="txtMinPoint" Width="100px" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*"
                                    ControlToValidate="txtDescription" EnableClientScript="false"></asp:RequiredFieldValidator>
                                &nbsp;s/d &nbsp;
                                <asp:TextBox runat="server" ID="txtMaxPoint" Width="100px" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*"
                                    ControlToValidate="txtDescription" EnableClientScript="false"></asp:RequiredFieldValidator>
                            </td>
                        </tr>

                        <tr valign="top">
                            <td class="titleField" style="width: 88px" width="88">Tipe Kalkulasi</td>
                            <td width="1%">:</td>
                            <td style="width: 330px" width="262">
                                <asp:DropDownList SortExpression="PeriodIDFrom" Width="100px" ID="ddlTipeKalkulasi" runat="server">
                                    <asp:ListItem Value="1" Text="Rata-rata" Selected="True"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="Total"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" EnableClientScript="false" runat="server" ErrorMessage="*" ControlToValidate="ddlPeriodeFrom"></asp:RequiredFieldValidator>

                            </td>
                        </tr>
                        <tr valign="top">
                            <td class="titleField">Tipe Kendaraan</td>
                            <td width="1%">:</td>
                            <td style="width: 330px" width="262">
                                <asp:CheckBoxList ID="chkVehicleType" RepeatColumns="3" runat="server"></asp:CheckBoxList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*"
                                    ControlToValidate="txtDescription" EnableClientScript="false"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr valign="top">
                            <td class="titleField">Tipe Dealer</td>
                            <td width="1%">:</td>
                            <td width="262">
                                <asp:CheckBoxList ID="chkDealerType" RepeatColumns="3" runat="server"></asp:CheckBoxList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="*"
                                    ControlToValidate="txtDescription" EnableClientScript="false"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td></td>
                            <td>&nbsp;
                                <asp:Button Text="Simpan" ID="btnSimpan" runat="server" Width="75px" />
                                &nbsp;
                                <asp:Button Text="Batal" ID="btnBatal" runat="server" Width="75px" />
                                &nbsp;
                                <asp:Button Text="Kembali" ID="btnKembali" runat="server" Width="75px" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td></td>
            </tr>
            <tr>
                <td valign="top">
                    <div id="div1" style="overflow: auto; height: 320px">
                        <asp:HiddenField ID="hdnTxtDealerCode" runat="server" />
                        <asp:DataGrid ID="dtgCSPMCluster" runat="server" Width="100%" PageSize="20" CellPadding="3"
                            BackColor="#CDCDCD" AllowCustomPaging="True" AllowPaging="True" BorderWidth="0px" BorderColor="#CDCDCD" AutoGenerateColumns="False" CellSpacing="1"
                            AllowSorting="True" Font-Names="Microsoft Sans Serif">
                            <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                            <AlternatingItemStyle BackColor="White"></AlternatingItemStyle>
                            <ItemStyle ForeColor="Black" BackColor="#FDF1F2"></ItemStyle>
                            <HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="#F7F7F7" BorderColor="#E0E0E0"
                                BackColor="#CC3333"></HeaderStyle>
                            <Columns>
                                <asp:BoundColumn Visible="false" DataField="ID" SortExpression="ID" HeaderText="ID"></asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="">
                                    <HeaderStyle Width="3%" CssClass="titleTableGeneral"></HeaderStyle>
                                    <ItemTemplate>
                                        <img alt="" style="cursor: pointer" id="imgPlus" src="../images/plus.gif" />
                                        <asp:Panel ID="Panel1" runat="server" Style="display: none">
                                            <asp:Label ID="lblInfo" runat="server" Text="Tidak ada data"></asp:Label>
                                            <asp:DataGrid ID="dtgDetail" Width="100%" OnItemDataBound="dtgDetail_ItemDataBound" runat="server" AutoGenerateColumns="false">
                                                <Columns>
                                                    <asp:BoundColumn Visible="false" DataField="ID" SortExpression="">
                                                        <HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
                                                        <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                    </asp:BoundColumn>
                                                    <asp:TemplateColumn SortExpression="" HeaderText="No">
                                                        <HeaderStyle Width="3%" CssClass="titleTableGeneral"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblNo" runat="server" >
                                                            </asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn SortExpression="" HeaderText="Kode">
                                                        <HeaderStyle Width="7%" CssClass="titleTableGeneral"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDealerCode" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerCode")%>'>
                                                            </asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn SortExpression="" HeaderText="Nama">
                                                        <HeaderStyle Width="30%" CssClass="titleTableGeneral"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDealerName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerName")%>'>
                                                            </asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>

                                                    <asp:TemplateColumn HeaderText="Tipe Dealer">
                                                        <HeaderStyle Width="7%" CssClass="titleTableGeneral"></HeaderStyle>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTipeDealer" runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                    </asp:TemplateColumn>

                                                    <asp:TemplateColumn SortExpression="" HeaderText="Group">
                                                        <HeaderStyle Width="20%" CssClass="titleTableGeneral"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDealerGroup" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerGroup.GroupName")%>'>
                                                            </asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>

                                                    <asp:TemplateColumn SortExpression="" HeaderText="Kota">
                                                        <HeaderStyle Width="20%" CssClass="titleTableGeneral"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCityName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.City.CityName")%>'>
                                                            </asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>

                                                    <asp:TemplateColumn SortExpression="" HeaderText="Area">
                                                        <HeaderStyle Width="20%" CssClass="titleTableGeneral"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblArea" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.Area2.Description")%>'>
                                                            </asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn SortExpression="" HeaderText="Penjualan">
                                                        <HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPenjualan" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Penjualan")%>'>
                                                            </asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="">
                                                        <HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="LinkButtonNonActive" OnClick="LinkButtonNonActive_Click" runat="server" Width="16px" CausesValidation="False" CommandName="Deactivate">
												                <img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateColumn>
                                                </Columns>
                                            </asp:DataGrid>
                                        </asp:Panel>
                                    </ItemTemplate>
                                </asp:TemplateColumn>

                                <asp:BoundColumn DataField="ClusterName" SortExpression="ClusterName" HeaderText="Nama Cluster">
                                    <HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:BoundColumn>

                                <asp:TemplateColumn HeaderText="Periode">
                                    <HeaderStyle Width="15%" CssClass="titleTableGeneral"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblPeriod" runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:TemplateColumn>

                                <asp:TemplateColumn HeaderText="Batas Penjualan">
                                    <HeaderStyle Width="15%" CssClass="titleTableGeneral"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblBatasPenjualan" runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateColumn>

                                <asp:TemplateColumn HeaderText="Tipe Kalkulasi">
                                    <HeaderStyle Width="5%" CssClass="titleTableGeneral"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblTipeKalkulasi" runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateColumn>

                                <asp:TemplateColumn HeaderText="Tipe Kendaraan">
                                    <HeaderStyle Width="5%" CssClass="titleTableGeneral"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblTipeKendaraan" runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateColumn>

                                <asp:TemplateColumn HeaderText="Tipe Dealer">
                                    <HeaderStyle Width="7%" CssClass="titleTableGeneral"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblTipeDealer" runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Total Dealer">
                                    <HeaderStyle Width="5%" CssClass="titleTableGeneral"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblTotalDealer" runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Tambah Dealer">
                                    <HeaderStyle Width="25%" CssClass="titleTableGeneral"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblDesc" runat="server" Text="Kode : "></asp:Label>
                                        <asp:TextBox ID="txtClusterDealer" runat="server" Width="150px"></asp:TextBox>
                                        <asp:Label ID="lblPopUpDealer" runat="server" Width="16px">
											            <img style="cursor:hand" alt="Klik Popup" src="../images/popup.gif" border="0"></asp:Label>
                                        &nbsp;
                                               <asp:LinkButton ID="btnDaftar" runat="server" CausesValidation="False" Text="Tambah" CommandName="add">
												<img src="../images/add.gif" border="0" alt="Daftar"></asp:LinkButton>&nbsp;
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn>
                                    <HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnGenerate" runat="server" Width="16px" CausesValidation="False" CommandName="generate">
												<img src="../images/aktif.gif" border="0" alt="Generate Cluster Dealer"></asp:LinkButton>
                                        <asp:LinkButton ID="lbtnEdit" runat="server" Text="Ubah" CommandName="edit" CausesValidation="False">
												<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
                                        <asp:LinkButton ID="lbtnHapus" runat="server" Width="16px" CausesValidation="False" CommandName="hapus">
												<img src="../images/in-aktif.gif" border="0" alt="Klik untuk Non-Aktifkan"></asp:LinkButton>
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
                    <asp:Button ID="btnDownload" Text="Download" runat="server" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
