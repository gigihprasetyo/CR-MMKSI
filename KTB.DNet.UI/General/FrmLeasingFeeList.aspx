<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmLeasingFeeList.aspx.vb" Inherits="FrmLeasingFeeList"%>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
    <HEAD>
        <title>FrmLeasingFeeList</title>
        <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
        <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
        <meta content="JavaScript" name="vs_defaultClientScript">
        <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
        <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
        <meta content="JavaScript" name="vs_defaultClientScript">
        <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
        <LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
        <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
        <script language="javascript" src="../WebResources/InputValidation.js"></script>
        <script language="javascript">

			function CheckAll(aspCheckBoxID, checkVal) 
			{
				re = new RegExp(':' + aspCheckBoxID + '$')  
				for(i = 0; i < document.forms[0].elements.length; i++) {
					elm = document.forms[0].elements[i]
					if (elm.type == 'checkbox') {
						if (re.test(elm.name)) {
							elm.checked = checkVal
						}
					}
				}
			}

        </script>
    </HEAD>
    <body MS_POSITIONING="GridLayout">
        <form id="Form1" method="post" runat="server">
            <div class="titlePage">GENERAL - Daftar Leasing Fee
            </div>
            <br>
            <table>
                <tr>
                    <td><asp:checkbox id="chkPeriode" runat="server" Text="Periode"></asp:checkbox></td>
                    <td>:
                    </td>
                    <td><cc1:inticalendar id="dtmFrom" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
                    <td>&nbsp;s/d&nbsp;
                    </td>
                    <td><cc1:inticalendar id="dtmTo" runat="server" TextBoxWidth="70"></cc1:inticalendar></td>
                </tr>
                <TR>
                    <TD>Variant</TD>
                    <TD>:</TD>
                    <TD><asp:dropdownlist id="ddlVehicleCAtegory" runat="server" AutoPostBack="True"></asp:dropdownlist></TD>
                    <TD></TD>
                    <TD><asp:dropdownlist id="ddlVehicleType" runat="server"></asp:dropdownlist></TD>
                </TR>
                <TR>
                    <TD></TD>
                    <TD></TD>
                    <TD><asp:button id="btnSearch" runat="server" Text="Cari"></asp:button></TD>
                    <TD></TD>
                    <TD></TD>
                </TR>
            </table>
            <asp:datagrid id="dtgLeasingFee" runat="server" AutoGenerateColumns="False" BackColor="#CDCDCD"
                BorderColor="Gainsboro" CellSpacing="1" BorderWidth="0px" CellPadding="3" AllowSorting="True"
                PageSize="25" Width="100%" AllowPaging="True" AllowCustomPaging="True">
                <AlternatingItemStyle BackColor="#F5F1EE"></AlternatingItemStyle>
                <ItemStyle BackColor="White"></ItemStyle>
                <Columns>
                    <asp:BoundColumn Visible="False" DataField="ID"></asp:BoundColumn>
                    <asp:TemplateColumn HeaderText="No">
                        <HeaderStyle ForeColor="White" Width="3%" HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn SortExpression="VechileType.VechileModel.Description" HeaderText="Vechile Model">
                        <HeaderStyle ForeColor="White"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.VechileType.VechileModel.Description") %>' ID="Label2" NAME="Label1">
                            </asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn SortExpression="VechileType.Description" HeaderText="Vechile Type">
                        <HeaderStyle ForeColor="White"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.VechileType.Description") %>' ID="Label1" NAME="Label1">
                            </asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:BoundColumn DataFormatString="{0:dd MMM yyyy}" DataField="DateFrom" HeaderText="DateFrom" SortExpression="DateTo">
                        <HeaderStyle ForeColor="White"></HeaderStyle>
                    </asp:BoundColumn>
                    <asp:BoundColumn DataFormatString="{0:dd MMM yyyy}" DataField="DateTo" HeaderText="DateTo" SortExpression="DateTo">
                        <HeaderStyle ForeColor="White"></HeaderStyle>
                    </asp:BoundColumn>
                    <asp:BoundColumn DataFormatString="{0:N}" DataField="Fee" HeaderText="Fee (%)" SortExpression="Fee">
                        <HeaderStyle ForeColor="White" HorizontalAlign="Right"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                    </asp:BoundColumn>
                    <asp:TemplateColumn>
                        <ItemTemplate>
                            <asp:LinkButton Runat="server" ID="lnkDel" CommandName="Del" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
                                <Img src="../images/batal.gif" alt="Delete" border="0" />
                            </asp:LinkButton>
                            <asp:HyperLink Runat="server" ID="lnkEdit" NavigateUrl='<%# DataBinder.Eval(Container, "DataItem.ID" , "~/General/FrmLeasingFee.aspx?id={0}&mode=true") %>'>
                                <Img src="../images/edit.gif" alt="Edit" border="0" />
                            </asp:HyperLink>
                            <asp:HyperLink Runat="server" ID="lnkView" NavigateUrl='<%# DataBinder.Eval(Container, "DataItem.ID" , "~/General/FrmLeasingFee.aspx?id={0}&mode=false") %>'>
                                <Img src="../images/detail.gif" alt="View" border="0" />
                            </asp:HyperLink>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                </Columns>
                <PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
            </asp:datagrid>
         </form>
    </body>
</HTML>
