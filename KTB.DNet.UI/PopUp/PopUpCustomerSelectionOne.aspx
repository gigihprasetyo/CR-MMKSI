<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PopUpCustomerSelectionOne.aspx.vb" Inherits="PopUpCustomerSelectionOne" SmartNavigation="False" %>

<%@ Register TagPrefix="cc1" Namespace="FilterCompositeControl" Assembly="FilterCompositeControl" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>Frm Customer</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <base target="_self">
    <script language="javascript">

        function GetSelectedCustomer() {
            var table;
            var bcheck = false;
            table = document.getElementById("dtgCustomerSelection");
            var Customer = '';

            for (i = 1; i < table.rows.length; i++) {
                var radioBtn = table.rows[i].cells[0].getElementsByTagName("INPUT")[0];
                if (radioBtn != null && radioBtn.checked) {
                    if (navigator.appName == "Microsoft Internet Explorer") {
                        Customer = table.rows[i].cells[1].getElementsByTagName("SPAN")[0].innerHTML + ';' +
                        table.rows[i].cells[2].getElementsByTagName("SPAN")[0].innerHTML + ';' +
                        table.rows[i].cells[2].getElementsByTagName("SPAN")[1].innerHTML + ';' +
                        table.rows[i].cells[2].getElementsByTagName("SPAN")[2].innerHTML + ';' +
                        table.rows[i].cells[2].getElementsByTagName("SPAN")[3].innerHTML + ';' +
                        table.rows[i].cells[2].getElementsByTagName("SPAN")[4].innerHTML + ';' +
                        table.rows[i].cells[2].getElementsByTagName("SPAN")[5].innerHTML + ';' +
                        table.rows[i].cells[4].getElementsByTagName("SPAN")[0].innerHTML + ';' +
                        table.rows[i].cells[2].getElementsByTagName("SPAN")[6].innerHTML + ';' +
                        table.rows[i].cells[2].getElementsByTagName("SPAN")[7].innerHTML + ';' +
                        table.rows[i].cells[2].getElementsByTagName("SPAN")[8].innerHTML + ';' +
                        table.rows[i].cells[2].getElementsByTagName("SPAN")[9].innerHTML + ';' +
                        table.rows[i].cells[2].getElementsByTagName("SPAN")[10].innerHTML + ';' +
                    table.rows[i].cells[5].getElementsByTagName("SPAN")[0].innerHTML + ';' +
                    table.rows[i].cells[6].getElementsByTagName("input")[0].value;

                        window.returnValue = Customer;
                        bcheck = true;
                    }
                    else if (navigator.appName == "Netscape") {
                        Customer = table.rows[i].cells[1].getElementsByTagName("SPAN")[0].innerHTML + ';' +
                        table.rows[i].cells[2].getElementsByTagName("SPAN")[0].innerHTML + ';' +
                        table.rows[i].cells[2].getElementsByTagName("SPAN")[1].innerHTML + ';' +
                        table.rows[i].cells[2].getElementsByTagName("SPAN")[2].innerHTML + ';' +
                        table.rows[i].cells[2].getElementsByTagName("SPAN")[3].innerHTML + ';' +
                        table.rows[i].cells[2].getElementsByTagName("SPAN")[4].innerHTML + ';' +
                        table.rows[i].cells[2].getElementsByTagName("SPAN")[5].innerHTML + ';' +
                        table.rows[i].cells[4].getElementsByTagName("SPAN")[0].innerHTML + ';' +
                        table.rows[i].cells[2].getElementsByTagName("SPAN")[6].innerHTML + ';' +
                        table.rows[i].cells[2].getElementsByTagName("SPAN")[7].innerHTML + ';' +
                        table.rows[i].cells[2].getElementsByTagName("SPAN")[8].innerHTML + ';' +
                        table.rows[i].cells[2].getElementsByTagName("SPAN")[9].innerHTML + ';' +
                        table.rows[i].cells[2].getElementsByTagName("SPAN")[10].innerHTML + ';' +
                        table.rows[i].cells[5].getElementsByTagName("SPAN")[0].innerHTML + ';' +
                    table.rows[i].cells[6].getElementsByTagName("input")[0].value;

                        opener.dialogWin.returnFunc(Customer);
                        bcheck = true;
                    }
                    else {
                        Customer = table.rows[i].cells[1].getElementsByTagName("SPAN")[0].innerHTML + ';' +
                        table.rows[i].cells[2].getElementsByTagName("SPAN")[0].innerHTML + ';' +
                        table.rows[i].cells[2].getElementsByTagName("SPAN")[1].innerHTML + ';' +
                        table.rows[i].cells[2].getElementsByTagName("SPAN")[2].innerHTML + ';' +
                        table.rows[i].cells[2].getElementsByTagName("SPAN")[3].innerHTML + ';' +
                        table.rows[i].cells[2].getElementsByTagName("SPAN")[4].innerHTML + ';' +
                        table.rows[i].cells[2].getElementsByTagName("SPAN")[5].innerHTML + ';' +
                        table.rows[i].cells[4].getElementsByTagName("SPAN")[0].innerHTML + ';' +
                        table.rows[i].cells[2].getElementsByTagName("SPAN")[6].innerHTML + ';' +
                        table.rows[i].cells[2].getElementsByTagName("SPAN")[7].innerHTML + ';' +
                        table.rows[i].cells[2].getElementsByTagName("SPAN")[8].innerHTML + ';' +
                        table.rows[i].cells[2].getElementsByTagName("SPAN")[9].innerHTML + ';' +
                        table.rows[i].cells[2].getElementsByTagName("SPAN")[10].innerHTML + ';' +
                    table.rows[i].cells[5].getElementsByTagName("SPAN")[0].innerHTML + ';' +
                    table.rows[i].cells[6].getElementsByTagName("input")[0].value;
                        //alert(opener.dialogWin.returnFunc);
                        opener.dialogWin.returnFunc(Customer);
                        bcheck = true;
                    }
                    break;
                }
            }
            if (bcheck) {
                window.close();
            }
            else {
                alert("Silahkan Pilih Customer terlebih dahulu");
            }
        }

    </script>
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <table id="Table1" cellspacing="0" cellpadding="6" width="100%" border="0">
            <tr>
                <td>
                    <table id="Table2" cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td class="titlePage" colspan="7">KONSUMEN&nbsp;-&nbsp;Pencarian&nbsp;Konsumen&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width: 872px" background="../images/bg_hor_sales.gif" colspan="7" height="1">
                                <img height="1" src="../images/bg_hor.gif" border="0"></td>
                        </tr>
                        <tr>
                            <td height="10">
                                <img height="1" src="../images/dot.gif" border="0"></td>
                        </tr>
                        <tr>
                            <td class="titleField" width="20%">Kota</td>
                            <td width="1%">:</td>
                            <td style="width: 553px" width="553">
                                <asp:TextBox ID="txtCity" onkeypress="return alphaNumericExcept(event,'<>?*%$;')" onblur="omitSomeCharacter('txtCity','<>?*%$;')"
                                    runat="server" Width="152px" Height="22"></asp:TextBox></td>
                            <td style="width: 17px" width="2%"></td>
                            <td class="titleField" width="20%"></td>
                            <td width="1%"></td>
                            <td style="width: 225px" width="225"></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="height: 13px">Nama Konsumen</td>
                            <td style="height: 13px">:</td>
                            <td style="width: 553px; height: 13px">
                                <asp:TextBox ID="txtCustName" onkeypress="return alphaNumericExcept(event,'<>?*%$;')" onblur="omitSomeCharacter('txtCustName','<>?*%$;')"
                                    runat="server" Width="152px" Height="22"></asp:TextBox><asp:Button ID="btnSearch" runat="server" Text=" Cari "></asp:Button></td>
                            <td style="width: 17px; height: 13px"></td>
                            <td class="titleField" style="height: 13px"></td>
                            <td style="height: 13px"></td>
                            <td style="width: 225px; height: 13px"></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="height: 15px"></td>
                            <td style="height: 15px"></td>
                            <td style="width: 553px; height: 15px"></td>
                            <td style="width: 17px; height: 15px"></td>
                            <td style="height: 15px"></td>
                            <td style="height: 15px"></td>
                            <td style="width: 225px; height: 15px"></td>
                        </tr>
                        <tr>
                            <td colspan="7">
                                <div id="div1" style="overflow: auto; height: 310px">
                                    <asp:DataGrid ID="dtgCustomerSelection" runat="server" Width="100%" AutoGenerateColumns="False"
                                        AllowCustomPaging="True" PageSize="50" AllowPaging="True" AllowSorting="True">
                                        <AlternatingItemStyle BackColor="#FDF1F2"></AlternatingItemStyle>
                                        <ItemStyle BackColor="White"></ItemStyle>
                                        <HeaderStyle ForeColor="white"></HeaderStyle>
                                        <Columns>
                                            <asp:TemplateColumn>
                                                <HeaderStyle Width="5%" CssClass="titleTableGeneral"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <HeaderTemplate>
                                                    &nbsp;Pilih
                                                </HeaderTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn SortExpression="Code" HeaderText="Kode">
                                                <HeaderStyle Width="5%" CssClass="titleTableGeneral"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Code" )%>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Nama" SortExpression="Name1">
                                                <HeaderStyle Width="15%" CssClass="titleTableGeneral"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="txtNama" runat="server"></asp:Label>
                                                    <asp:Label ID="lblGedung" runat="server" Style="display: none" Text='<%# DataBinder.Eval(Container, "DataItem.Name3")%>'>
                                                    </asp:Label>
                                                    <asp:Label ID="lblAlamat" runat="server" Style="display: none" Text='<%# DataBinder.Eval(Container, "DataItem.Alamat")%>'>
                                                    </asp:Label>
                                                    <asp:Label ID="lblKeluharan" runat="server" Style="display: none" Text='<%# DataBinder.Eval(Container, "DataItem.Kelurahan")%>'>
                                                    </asp:Label>
                                                    <asp:Label ID="lblKecamatan" runat="server" Style="display: none" Text='<%# DataBinder.Eval(Container, "DataItem.Kecamatan")%>'>
                                                    </asp:Label>
                                                    <asp:Label ID="lblPostalCode" runat="server" Style="display: none" Text='<%# DataBinder.Eval(Container, "DataItem.PostalCode" )%>'>
                                                    </asp:Label>
                                                    <asp:Label ID="lblProvince" runat="server" Style="display: none" Text='<%# DataBinder.Eval(Container, "DataItem.City.Province.ProvinceName")%>'>
                                                    </asp:Label>
                                                    <asp:Label ID="lblEmail" runat="server" Style="display: none" Text='<%# DataBinder.Eval(Container, "DataItem.Email")%>'>
                                                    </asp:Label>
                                                    <asp:Label ID="lblPhoneNo" runat="server" Style="display: none" Text='<%# DataBinder.Eval(Container, "DataItem.PhoneNo")%>'>
                                                    </asp:Label>
                                                    <asp:Label ID="lblNama1" runat="server" Style="display: none" Text='<%# DataBinder.Eval(Container, "DataItem.Name1")%>'>
                                                    </asp:Label>
                                                    <asp:Label ID="lblNama2" runat="server" Style="display: none" Text='<%# DataBinder.Eval(Container, "DataItem.Name2")%>'>
                                                    </asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:BoundColumn DataField="Alamat" SortExpression="Alamat" HeaderText="Alamat">
                                                <HeaderStyle Width="15%" CssClass="titleTableGeneral"></HeaderStyle>
                                            </asp:BoundColumn>
                                            <asp:TemplateColumn SortExpression="City.CityName" HeaderText="Kota">
                                                <HeaderStyle Width="15%" CssClass="titleTableGeneral"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCity" runat="server">
															<%# DataBinder.Eval(Container, "DataItem.City.CityName")  %>
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="No KTP">
                                                <HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNoKTP" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Model Tipe Warna">
                                                <HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblModelTipeWarna" runat="server"></asp:Label>
                                                    <input type="hidden" id="ID" value="  <%# DataBinder.Eval(Container, "DataItem.ID")%> ">
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Nomor SPK Detail DMS" Visible="false">
                                                <HeaderStyle Width="10%" CssClass="titleTableGeneral"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSPKDetailID" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                        </Columns>
                                        <PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
                                    </asp:DataGrid>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="7">
                                <input id="btnChoose" style="width: 60px" disabled onclick="GetSelectedCustomer()" type="button"
                                    value="Pilih" name="btnChoose" runat="server">&nbsp;
                                <input id="btnCancel" style="width: 60px" onclick="window.close()" type="button" value="Tutup"
                                    name="btnCancel"></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
