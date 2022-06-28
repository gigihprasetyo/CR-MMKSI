<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC"  %>
<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PopUpPQRSelectionSingle.aspx.vb" Inherits="PopUpPQRSelectionSingle" SmartNavigation="False" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >

<HTML>
<HEAD>
    <title>PopUpPQRSelectionSingle</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <base target="_self">
    <script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script language="javascript">

        function getQueryVariable(variable) {
            var query = window.location.search.substring(1);
            var vars = query.split("&");
            for (var i = 0; i < vars.length; i++) {
                var pair = vars[i].split("=");
                if (pair[0] == variable) {
                    return pair[1];
                }
            }
            return "nothing";
        }

        function GetSelectedPQRNo() {
            var table;
            var bcheck = false;
            table = document.getElementById("dgPQRList");
            var PQRNo = '';
            for (i = 1; i < table.rows.length; i++) {
                var radioBtn = table.rows[i].cells[0].getElementsByTagName("INPUT")[0];
                if (radioBtn != null && radioBtn.checked) {
                    if (navigator.appName == "Microsoft Internet Explorer") {
                        PQRNo = replace(table.rows[i].cells[4].innerText, ' ', '');
                        window.returnValue = PQRNo;
                        bcheck = true;
                    }
                    else {
                        PQRNo = replace(table.rows[i].cells[4].getElementsByTagName("span")[0].innerHTML, ' ', '');
                        window.opener.dialogWin.returnFunc(PQRNo);
                        bcheck = true;
                    }
                    break;
                }
            }
            if (bcheck) {
                window.close();
            }
            else {
                alert("Silahkan pilih Nomor PQR");
            }
        }

        function replace(string, text, by) {
            var strLength = string.length, txtLength = text.length;
            if ((strLength == 0) || (txtLength == 0)) return string;

            var i = string.indexOf(text);
            if ((!i) && (text != string.substring(0, txtLength))) return string;
            if (i == -1) return string;

            var newstr = string.substring(0, i) + by;

            if (i + txtLength < strLength)
                newstr += replace(string.substring(i + txtLength, strLength), text, by);

            return newstr;
        }
    </script>
</HEAD>
<body bottommargin="10" topmargin="10">
    <form id="Form1" method="post" runat="server">
		<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
			<tr>
				<td class="titlePage">Daftar PQR</td>
			</tr>
			<tr>
				<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
			</tr>
			<tr>
				<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
			</tr>
            <tr>
                <td>
                    <table id="Table2" cellspacing="0" cellpadding="0" width="100%" border="0">
                         <tr valign="top">
                            <td class="titleField" style="height: 13px">Kode Dealer</td>
                            <td style="height: 13px">:</td>
                            <td style="height: 13px"><asp:label id="lblDealerCode" runat="server"></asp:label>&nbsp;</td>
                            <td style="width: 17px; height: 13px"></td>
                            <td class="titleField" style="height: 13px">Jenis PQR</td>
                            <td style="height: 13px">:</td>
                            <td style="width: 225px; height: 13px">
                                <asp:dropdownlist id="ddlPqrType" runat="server"></asp:dropdownlist></td>
                        </tr>

                        <tr valign="top">
                            <td class="titleField" width="20%">Nomor PQR</td>
                            <td width="1%">:</td>
                            <td width="25%"><asp:textbox onkeypress="return alphaNumericExcept(event,'<>?*%$;')" 
                                id="txtPQRNo" runat="server" MaxLength="25"></asp:textbox></td>
                            <td style="width: 17px" width="2%"></td>
                            <td class="titleField" width="20%">Status</td>
                            <td width="1%">:</td>
                            <td style="width: 225px" width="225">
                                <asp:DropDownList ID="ddlStatus" runat="server"></asp:DropDownList>
                            </td>
                        </tr>
                         <tr valign="top">
                            <td class="titleField" style="height: 15px">Tgl Pembuatan</td>
                            <td style="height: 15px">:</td>
                            <td style="height: 15px"> 
								<table cellSpacing="0" cellPadding="0" border="0">
									<tr vAlign="top">
										<td><asp:checkbox id="chkFilterTanggal" runat="server" Checked="True"></asp:checkbox></td>
										<td><cc1:inticalendar id="icTglApplyDari" runat="server" TextBoxWidth="60"></cc1:inticalendar></td>
										<td vAlign="middle">&nbsp;&nbsp;s.d&nbsp;&nbsp;</td>
										<td><cc1:inticalendar id="icTglApplySampai" runat="server" TextBoxWidth="60"></cc1:inticalendar></td>
									</tr>
								</table>
                            </td>
                            <td style="width: 17px; height: 15px"></td>
                            <td style="height: 15px" class="titleField">Kategori</td>
                            <td style="height: 15px">:</td>
                            <td style="width: 225px; height: 15px">
                                <asp:dropdownlist id="ddlKategori" runat="server"></asp:dropdownlist></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="height: 15px"></td>
                            <td style="height: 15px"></td>
                            <td style="height: 15px"><asp:button id="btnSearch" runat="server" Width="60px" Text=" Cari "></asp:button></td>
                            <td style="width: 17px; height: 15px"></td>
                            <td style="height: 15px"></td>
                            <td style="height: 15px"></td>
                            <td style="width: 225px; height: 15px"></td>
                        </tr>
                        <tr>
                            <td class="titleField" style="height: 15px"></td>
                            <td style="height: 15px"></td>
                            <td style="height: 15px"></td>
                            <td style="width: 17px; height: 15px"></td>
                            <td style="height: 15px"></td>
                            <td style="height: 15px"></td>
                            <td style="width: 225px; height: 15px"></td>
                        </tr>
                        <tr>
                            <td colspan="7">
                                <div id="div1" style="overflow: auto; height: 280px">
                                    <asp:DataGrid ID="dgPQRList" runat="server" Width="100%" AutoGenerateColumns="False"
                                        AllowSorting="True" AllowCustomPaging="True" PageSize="100" AllowPaging="True" >
                                        <AlternatingItemStyle BackColor="#FDF1F2"></AlternatingItemStyle>
                                        <HeaderStyle ForeColor="White" BackColor="#CC3333" Font-Bold="True" HorizontalAlign="Center"></HeaderStyle>
                                        <Columns>
                                            <asp:TemplateColumn>
                                                <HeaderStyle Width="2%" CssClass="titleTableParts"></HeaderStyle>
							                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                            </asp:TemplateColumn>
						                    <asp:TemplateColumn HeaderText="No">
							                    <HeaderStyle ForeColor="White" Width="1%" CssClass="titleTableParts"></HeaderStyle>
							                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
							                    <ItemTemplate>
								                    <asp:Label id="lblNo" runat="server"></asp:Label>
							                    </ItemTemplate>
						                    </asp:TemplateColumn>
						                    <asp:TemplateColumn SortExpression="RowStatus" HeaderText="Status">
							                    <HeaderStyle HorizontalAlign="Center" CssClass="titleTableParts"></HeaderStyle>
							                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                <ItemTemplate>
								                    <asp:Label id="lblStatus" runat="server"></asp:Label>
							                    </ItemTemplate>
						                    </asp:TemplateColumn>
						                    <asp:TemplateColumn SortExpression="Dealer.DealerCode" HeaderText="Dealer">
							                    <HeaderStyle HorizontalAlign="Center" CssClass="titleTableParts"></HeaderStyle>
							                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                <ItemTemplate>
								                    <asp:Label runat="server" Tooltip = '<%# DataBinder.Eval(Container, "DataItem.Dealer.SearchTerm1")%>' Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerCode")%>' ID="lblDealer" NAME="lblDealer">
								                    </asp:Label>
							                    </ItemTemplate>
						                    </asp:TemplateColumn>
						                    <asp:TemplateColumn SortExpression="PQRNo" HeaderText="Nomor PQR">
							                    <HeaderStyle HorizontalAlign="Center" CssClass="titleTableParts"></HeaderStyle>
							                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                <ItemTemplate>
								                    <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PQRNo")%>' ID="lblPQRNo" NAME="lblPQRNo">
								                    </asp:Label>
							                    </ItemTemplate>
						                    </asp:TemplateColumn>
						                    <asp:TemplateColumn SortExpression="PQRType" HeaderText="Jenis PQR">
							                    <HeaderStyle HorizontalAlign="Center" CssClass="titleTableParts"></HeaderStyle>
                                                <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                <ItemTemplate>
								                    <asp:Label id="lblPQRType" runat="server"></asp:Label>
							                    </ItemTemplate>
						                    </asp:TemplateColumn>
						                    <asp:BoundColumn DataField="DocumentDate" SortExpression="DocumentDate" HeaderText="Tgl Pembuatan"
							                    DataFormatString="{0:dd/MM/yyyy}">
							                    <HeaderStyle HorizontalAlign="Center" CssClass="titleTableParts"></HeaderStyle>
                                                <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
						                    </asp:BoundColumn>
						                    <asp:TemplateColumn SortExpression="ChassisMaster.ChassisNumber" HeaderText="No Rangka">
							                    <HeaderStyle HorizontalAlign="Center" CssClass="titleTableParts"></HeaderStyle>
                                                <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
							                    <ItemTemplate>
								                    <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ChassisMaster.ChassisNumber") %>' ID="lblNoChassis" NAME="lblNoChassis">
								                    </asp:Label>
							                    </ItemTemplate>
						                    </asp:TemplateColumn>
						                    <asp:TemplateColumn SortExpression="Category.CategoryCode" HeaderText="Model">
							                    <HeaderStyle HorizontalAlign="Center" CssClass="titleTableParts"></HeaderStyle>
							                    <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                <ItemTemplate>
								                    <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Category.CategoryCode") %>' ID="lblCategory" NAME="lblCategory">
								                    </asp:Label>
							                    </ItemTemplate>
						                    </asp:TemplateColumn>
						                    <asp:BoundColumn DataField="Subject" SortExpression="Subject" HeaderText="Subject">
							                    <HeaderStyle width="15%" CssClass="titleTableParts"></HeaderStyle>
						                    </asp:BoundColumn>
                                        </Columns>
                    					<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
                                    </asp:DataGrid>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="7">
                                <input id="btnChoose" style="width: 60px" onclick="GetSelectedPQRNo()" type="button" value="Pilih"
                                    name="btnChoose">
                                &nbsp;<input id="btnCancel" style="width: 60px" onclick="window.close()" type="button" value="Tutup"
                                        name="btnCancel"></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</BODY>
</html>
