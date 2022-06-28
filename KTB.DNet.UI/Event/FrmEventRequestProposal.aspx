<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmEventRequestProposal.aspx.vb" Inherits="FrmEventRequestProposal" %>
<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
    <HEAD>
        <title>FrmEventRequestProposal</title>
        <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
        <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
        <meta content="JavaScript" name="vs_defaultClientScript">
        <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
        <LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
        <script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
        <script language="javascript" src="../WebResources/InputValidation.js"></script>
        <script language="javascript" src="../WebResources/FormFunctions.js"></script>
        <script language="javascript" type="text/javascript">
			function findPosX(obj)
			{
				var curleft = 0;
				if(obj.offsetParent)
					while(1) 
					{
						curleft += obj.offsetLeft;
						if(!obj.offsetParent)
							break;
						obj = obj.offsetParent;
					}
				else if(obj.x)
					curleft += obj.x;
				return curleft;
			}
			function findPosY(obj)
			{
				var curtop = 0;
				if(obj.offsetParent)
					while(1)
					{
						curtop += obj.offsetTop;
						if(!obj.offsetParent)
							break;
						obj = obj.offsetParent;
					}
				else if(obj.y)
					curtop += obj.y;
				return curtop;
			}
			function showheadertip(sender)
			{
				var tooltip = (document.getElementById) ?
					document.getElementById('htipToolTip') : eval("document.all['htipToolTip']");
				if (tooltip != null)
				{
					tooltip.style.pixelLeft = findPosX(sender) + 50;
					tooltip.style.pixelTop = findPosY(sender) + 20;
					tooltip.style.visibility = "visible";
				}
			}
			function hideheadertip(sender)
			{
				var tooltip = (document.getElementById) ?
				document.getElementById('htipToolTip') : eval("document.all['htipToolTip']");
				if (tooltip != null)
				{
					tooltip.style.visibility = "hidden";
				}
			}
        </script>
    </HEAD>
    <body MS_POSITIONING="GridLayout">
        <form id="Form1" method="post" runat="server">
            <TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
                <tr>
                    <td class="titlePage" style="HEIGHT: 31px" colSpan="2">EVENT&nbsp;-&nbsp;Proposal 
                        Event</td>
                </tr>
                <tr>
                    <td background="../images/bg_hor.gif" colSpan="2" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
                </tr>
                <tr>
                    <td colSpan="2" height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
                </tr>
                <tr>
                    <td style="HEIGHT: 93px" colSpan="2">
                        <table cellSpacing="1" cellPadding="2" width="100%" border="0">
                            <TR>
                                <td class="titleField" style="WIDTH: 20%; HEIGHT: 19px">Kode Dealer</td>
                                <TD style="WIDTH: 1px; HEIGHT: 19px" width="1">:</TD>
                                <TD style="WIDTH: 30%"><asp:label id="lblDealerCode" Runat="server"></asp:label></TD>
                                <% If GetActivityTypeID = Ctype(KTB.DNet.Domain.EnumActivityType.ActivityGroupCode.StradaTriton, Integer) OrElse GetActivityTypeID = Ctype(KTB.DNet.Domain.EnumActivityType.ActivityGroupCode.SmallGathering, Integer) Then %>
                                <td class="titleField" style="WIDTH: 20%; HEIGHT: 18px">Kelurahan</td>
                                <TD style="WIDTH: 1px; HEIGHT: 18px" width="1">:</TD>
                                <TD style="WIDTH: 50%; HEIGHT: 18px"><asp:textbox id="txtRavine" Runat="server" MaxLength="50"></asp:textbox></TD>
                                <% Else %>
                                <td class="titleField" style="WIDTH: 20%; HEIGHT: 18px">Area</td>
                                <TD style="WIDTH: 1px; HEIGHT: 18px" width="1">:</TD>
                                <TD style="WIDTH: 50%; HEIGHT: 18px"><asp:label id="lblArea" Runat="server"></asp:label></TD>
                                <% End If %>
                            </TR>
                            <TR>
                                <td class="titleField" style="HEIGHT: 19px">Kota</td>
                                <TD style="WIDTH: 1px; HEIGHT: 19px" width="1">:</TD>
                                <TD><asp:label id="lblCity" Runat="server"></asp:label></TD>
                                <% If GetActivityTypeID = Ctype(KTB.DNet.Domain.EnumActivityType.ActivityGroupCode.StradaTriton, Integer) OrElse GetActivityTypeID = Ctype(KTB.DNet.Domain.EnumActivityType.ActivityGroupCode.SmallGathering, Integer) Then %>
                                <td class="titleField" style="WIDTH: 20%; HEIGHT: 18px">Kecamatan</td>
                                <TD style="WIDTH: 1px; HEIGHT: 18px" width="1">:</TD>
                                <TD style="WIDTH: 50%; HEIGHT: 18px"><asp:textbox id="txtSubDistrict" Runat="server" MaxLength="50"></asp:textbox></TD>
                                <% End If %>
                            </TR>
                            <TR>
                                <td class="titleField" style="HEIGHT: 19px">Jenis Kegiatan</td>
                                <TD style="WIDTH: 1px; HEIGHT: 19px" width="1">:</TD>
                                <TD><asp:dropdownlist id="ddlActivityType" runat="server" AutoPostBack="True"></asp:dropdownlist><asp:requiredfieldvalidator id="RequiredFieldValidator1" runat="server" ControlToValidate="ddlActivityType"
                                        ErrorMessage="Jenis kegiatan harus dipilih">*</asp:requiredfieldvalidator></TD>
                                <% If GetActivityTypeID = Ctype(KTB.DNet.Domain.EnumActivityType.ActivityGroupCode.StradaTriton, Integer) OrElse GetActivityTypeID = Ctype(KTB.DNet.Domain.EnumActivityType.ActivityGroupCode.SmallGathering, Integer) Then %>
                                <td class="titleField" style="WIDTH: 20%; HEIGHT: 18px">Owner</td>
                                <TD style="WIDTH: 1px; HEIGHT: 18px" width="1">:</TD>
                                <TD style="WIDTH: 50%; HEIGHT: 18px"><asp:textbox onkeypress="return NumericOnlyWith(event,'');" id="txtOwner" Runat="server" MaxLength="9"
                                        Text="0" Width="40px"></asp:textbox></TD>
                                <% End If %>
                            </TR>
                            <TR>
                                <td class="titleField" style="HEIGHT: 19px">Nama Kegiatan</td>
                                <TD style="WIDTH: 1px; HEIGHT: 19px" width="1">:</TD>
                                <TD><asp:dropdownlist id="ddlEventName" Runat="server" AutoPostBack="True"></asp:dropdownlist>
                                    <asp:RequiredFieldValidator id="RequiredFieldValidator4" runat="server" ErrorMessage="Nama kegiatan harus dipilih"
                                        ControlToValidate="ddlEventName">*</asp:RequiredFieldValidator></TD>
                                <% If GetActivityTypeID = Ctype(KTB.DNet.Domain.EnumActivityType.ActivityGroupCode.StradaTriton, Integer) OrElse GetActivityTypeID = Ctype(KTB.DNet.Domain.EnumActivityType.ActivityGroupCode.SmallGathering, Integer) Then %>
                                <td class="titleField" style="WIDTH: 20%; HEIGHT: 18px">Driver/Co Driver</td>
                                <TD style="WIDTH: 1px; HEIGHT: 18px" width="1">:</TD>
                                <TD style="WIDTH: 50%; HEIGHT: 18px"><asp:textbox onkeypress="return NumericOnlyWith(event,'');" id="txtDriver" Runat="server" MaxLength="9"
                                        Text="0" Width="40px"></asp:textbox></TD>
                                <% End If %>
                            </TR>
                            <TR>
                                <td class="titleField" style="HEIGHT: 19px">Periode</td>
                                <TD style="WIDTH: 1px; HEIGHT: 19px" width="1">:</TD>
                                <TD colSpan="5"><asp:label id="lblEventPeriod" Runat="server"></asp:label></TD>
                            </TR>
                            <TR>
                                <td class="titleField" style="HEIGHT: 19px">Tanggal Acara</td>
                                <TD style="WIDTH: 1px; HEIGHT: 19px" width="1">:</TD>
                                <TD colSpan="5"><cc1:inticalendar id="calEventDate" runat="server" TextBoxWidth="60"></cc1:inticalendar></TD>
                            </TR>
                            <TR>
                                <td class="titleField" style="HEIGHT: 19px">Tempat</td>
                                <TD style="WIDTH: 1px; HEIGHT: 19px" width="1">:</TD>
                                <TD colSpan="5"><asp:textbox id="txtPlace" Runat="server" MaxLength="100"></asp:textbox><asp:requiredfieldvalidator id="RequiredFieldValidator2" runat="server" ControlToValidate="txtPlace" Display="Dynamic"
                                        ErrorMessage="Masukkan Tempat acara">*</asp:requiredfieldvalidator></TD>
                            </TR>
                            <TR>
                                <td class="titleField" style="HEIGHT: 19px">Jumlah Undangan</td>
                                <TD style="WIDTH: 1px; HEIGHT: 19px" width="1">:</TD>
                                <TD colSpan="5"><asp:textbox onkeypress="return NumericOnlyWith(event,'');" id="txtInvitationNumber" Runat="server"
                                        MaxLength="9" Width="40px"></asp:textbox><asp:requiredfieldvalidator id="RequiredFieldValidator3" runat="server" ControlToValidate="txtInvitationNumber"
                                        Display="Dynamic" ErrorMessage="Masukkan jumlah undangan">*</asp:requiredfieldvalidator></TD>
                            </TR>
                        </table>
                        <asp:label id="lblError" runat="server" Width="624px" ForeColor="Red" EnableViewState="False"></asp:label><asp:validationsummary id="ValidationSummary1" runat="server"></asp:validationsummary></td>
                </tr>
                <tr>
                    <td style="HEIGHT: 19px"></td>
                </tr>
                <tr>
                    <td class="titleField" id="trGridTamu" colSpan="2" runat="server">Tamu/PIC KTB
                        <asp:datagrid id="dtgGuest" runat="server" Width="100%" ShowFooter="False" AllowSorting="True"
                            AutoGenerateColumns="False">
                            <AlternatingItemStyle BackColor="#FDF1F2"></AlternatingItemStyle>
                            <ItemStyle BackColor="White"></ItemStyle>
                            <HeaderStyle ForeColor="White"></HeaderStyle>
                            <Columns>
                                <asp:BoundColumn Visible="False" DataField="id" HeaderText="Id">
                                    <HeaderStyle Width="3%" CssClass="titleTablePromo"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="No">
                                    <HeaderStyle Width="2%" CssClass="titleTablePromo"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="EventActivityTypeName" HeaderText="Jenis">
                                    <HeaderStyle Width="10%" CssClass="titleTablePromo"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.EventActivityTypeName") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddlGuestType" runat="server" Width="100%"></asp:DropDownList>
                                    </EditItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="Item" HeaderText="Nama">
                                    <HeaderStyle Width="15%" CssClass="titleTablePromo"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label id=Label1 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Item") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox id=txtGuestName runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Item") %>' MaxLength="70" Width="100%">
                                        </asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="Description" HeaderText="Jabatan">
                                    <HeaderStyle Width="18%" CssClass="titleTablePromo"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Description") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtGuestRank" runat="server" Width="100%" Text='<%# DataBinder.Eval(Container, "DataItem.Description") %>'>
                                        </asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn>
                                    <HeaderStyle Width="3%" CssClass="titleTablePromo"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnbGuestEdit" runat="server" CommandName="Edit" CausesValidation="false" CommandArgument="GUE">
                                            <img src="../images/edit.gif" border="0" alt="Ubah">
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="lnbGuestDelete" runat="server" CausesValidation="false" CommandArgument="GUE"
                                            OnClick="lnbDelete_Click">
                                            <img src="../images/trash.gif" border="0" alt="Hapus">
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:LinkButton ID="lnbGuestUpdate" runat="server" CausesValidation="false" CommandArgument="GUE"
                                            OnClick="lnbUpdate_Click">
                                            <img src="../images/simpan.gif" border="0" alt="Simpan">
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="lnbGuestInsert" runat="server" CausesValidation="false" CommandArgument="GUE"
                                            OnClick="lnbInsert_Click">
                                            <img src="../images/add.gif" border="0" alt="Tambah">
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="lnbGuestCancel" runat="server" CommandName="Cancel" CausesValidation="false"
                                            CommandArgument="GUE">
                                            <img src="../images/batal.gif" border="0" alt="Batal">
                                        </asp:LinkButton>
                                    </EditItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                            <PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
                        </asp:datagrid></td>
                </tr>
                <tr>
                    <td style="HEIGHT: 19px"></td>
                </tr>
                <tr>
                    <td class="titleField" colSpan="2">Makanan, Minuman<% If GetActivityTypeID <> Ctype(KTB.DNet.Domain.EnumActivityType.ActivityType.Small_Gathering, Integer) %>
                        , Sewa Tempat<% End If %>
                        <asp:datagrid id="dtgFoodAndBeverage" runat="server" Width="100%" ShowFooter="True" AllowSorting="True"
                            AutoGenerateColumns="False">
                            <AlternatingItemStyle BackColor="#FDF1F2"></AlternatingItemStyle>
                            <ItemStyle BackColor="White"></ItemStyle>
                            <HeaderStyle ForeColor="White"></HeaderStyle>
                            <Columns>
                                <asp:BoundColumn Visible="False" DataField="id" HeaderText="Id">
                                    <HeaderStyle Width="3%" CssClass="titleTablePromo"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="No">
                                    <HeaderStyle Width="2%" CssClass="titleTablePromo"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="EventActivityTypeName" HeaderText="Jenis">
                                    <HeaderStyle Width="10%" CssClass="titleTablePromo"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.EventActivityTypeName") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddlFoodEventActivityTypeEdit" runat="server" Width="100%"></asp:DropDownList>
                                    </EditItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="Item" HeaderText="Item">
                                    <HeaderStyle Width="10%" CssClass="titleTablePromo"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label id=Label2 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Item") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox id=txtFoodItemEdit runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Item") %>' MaxLength="70" Width="100%">
                                        </asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="Quantity" HeaderText="Qty">
                                    <HeaderStyle Width="3%" CssClass="titleTablePromo"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label id=Label3 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Quantity", "{0:#,##0}") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox onkeypress="return NumericOnlyWith(event,'');" id=txtFoodQuantityEdit runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Quantity", "{0:#,##0}") %>' MaxLength="9" Width="100%">
                                        </asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="UnitCost" HeaderText="Biaya Satuan">
                                    <HeaderStyle Width="10%" CssClass="titleTablePromo"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label id=Label4 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.UnitCost", "{0:#,##0}") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label id="lblFoodUnitCostTotal" runat="server" Text="0"></asp:Label>
                                    </FooterTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox onkeypress="return NumericOnlyWith(event,'');" id=txtFoodUnitCostEdit runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.UnitCost", "{0:#,##0}") %>' MaxLength="9" Width="100%">
                                        </asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="TotalCost" HeaderText="Sub Total Biaya">
                                    <HeaderStyle Width="10%" CssClass="titleTablePromo"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TotalCost", "{0:#,##0}") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblFoodTotalAllCost" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TotalCost", "{0:#,##0}") %>'>
                                        </asp:Label>
                                    </FooterTemplate>
                                    <EditItemTemplate>
                                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TotalCost", "{0:#,##0}") %>'>
                                        </asp:Label>
                                    </EditItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn>
                                    <HeaderStyle Width="3%" CssClass="titleTablePromo"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnbFoodEdit" runat="server" CommandName="Edit" CausesValidation="false" CommandArgument="AKO">
                                            <img src="../images/edit.gif" border="0" alt="Ubah">
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="lnbFoodDelete" runat="server" CausesValidation="false" CommandArgument="AKO"
                                            OnClick="lnbDelete_Click">
                                            <img src="../images/trash.gif" border="0" alt="Hapus">
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:LinkButton ID="lnbFoodUpdate" runat="server" CausesValidation="false" CommandArgument="AKO"
                                            OnClick="lnbUpdate_Click">
                                            <img src="../images/simpan.gif" border="0" alt="Simpan">
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="lnbFoodAdd" runat="server" CausesValidation="false" CommandArgument="AKO" OnClick="lnbInsert_Click">
                                            <img src="../images/add.gif" border="0" alt="Tambah">
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="lnbFoodCancel" runat="server" CommandName="Cancel" CausesValidation="false"
                                            CommandArgument="AKO">
                                            <img src="../images/batal.gif" border="0" alt="Batal">
                                        </asp:LinkButton>
                                    </EditItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                            <PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
                        </asp:datagrid></td>
                </tr>
                <% If GetActivityTypeID = Ctype(KTB.DNet.Domain.EnumActivityType.ActivityType.Small_Gathering, Integer) %>
                <tr>
                    <td style="HEIGHT: 19px"></td>
                </tr>
                <tr>
                    <td class="titleField" colSpan="2">Sewa Tempat
                        <asp:datagrid id="dtgPlaces" runat="server" Width="100%" ShowFooter="True" AllowSorting="True"
                            AutoGenerateColumns="False">
                            <AlternatingItemStyle BackColor="#FDF1F2"></AlternatingItemStyle>
                            <ItemStyle BackColor="White"></ItemStyle>
                            <HeaderStyle ForeColor="White"></HeaderStyle>
                            <Columns>
                                <asp:BoundColumn Visible="False" DataField="id" HeaderText="Id">
                                    <HeaderStyle Width="3%" CssClass="titleTablePromo"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="No">
                                    <HeaderStyle Width="2%" CssClass="titleTablePromo"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="EventActivityTypeName" HeaderText="Jenis">
                                    <HeaderStyle Width="10%" CssClass="titleTablePromo"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.EventActivityTypeName") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddlPlaceEventActivityTypeEdit" runat="server" Width="100%"></asp:DropDownList>
                                    </EditItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="Item" HeaderText="Item">
                                    <HeaderStyle Width="10%" CssClass="titleTablePromo"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label id=Label5 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Item") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox id=txtPlaceItemEdit runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Item") %>' Width="100%" Rows="70">
                                        </asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="Quantity" HeaderText="Qty">
                                    <HeaderStyle Width="3%" CssClass="titleTablePromo"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label id=Label6 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Quantity", "{0:#,##0}") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox onkeypress="return NumericOnlyWith(event,'');" id=txtPlaceQuantity runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Quantity", "{0:#,##0}") %>' MaxLength="9" Width="100%">
                                        </asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="UnitCost" HeaderText="Biaya Satuan">
                                    <HeaderStyle Width="10%" CssClass="titleTablePromo"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label id=Label7 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.UnitCost", "{0:#,##0}") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label id="lblPlaceUnitCost" runat="server" Text="0"></asp:Label>
                                    </FooterTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox onkeypress="return NumericOnlyWith(event,'');" id=txtPlaceUnitCost runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.UnitCost", "{0:#,##0}") %>' Width="100%" Rows="9">
                                        </asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="TotalCost" HeaderText="Sub Total Biaya">
                                    <HeaderStyle Width="10%" CssClass="titleTablePromo"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TotalCost", "{0:#,##0}") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblPlaceSubTotalBiaya" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TotalCost", "{0:#,##0}") %>'>
                                        </asp:Label>
                                    </FooterTemplate>
                                    <EditItemTemplate>
                                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TotalCost", "{0:#,##0}") %>'>
                                        </asp:Label>
                                    </EditItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn>
                                    <HeaderStyle Width="3%" CssClass="titleTablePromo"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnbPlaceEdit" runat="server" CommandName="Edit" CausesValidation="false" CommandArgument="PLC">
                                            <img src="../images/edit.gif" border="0" alt="Ubah">
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="lnbPlaceDelete" runat="server" CausesValidation="false" CommandArgument="PLC"
                                            OnClick="lnbDelete_Click">
                                            <img src="../images/trash.gif" border="0" alt="Hapus">
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:LinkButton ID="lnbPlaceUpdate" runat="server" CausesValidation="false" CommandArgument="PLC"
                                            OnClick="lnbUpdate_Click">
                                            <img src="../images/simpan.gif" border="0" alt="Simpan">
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="lnbPlaceAdd" runat="server" CausesValidation="false" CommandArgument="PLC" OnClick="lnbInsert_Click">
                                            <img src="../images/add.gif" border="0" alt="Tambah">
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="lnbPlaceCancel" runat="server" CommandName="Cancel" CausesValidation="false"
                                            CommandArgument="PLC">
                                            <img src="../images/batal.gif" border="0" alt="Batal">
                                        </asp:LinkButton>
                                    </EditItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                            <PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
                        </asp:datagrid></td>
                </tr>
                <% End If %>
                <tr>
                    <td style="HEIGHT: 19px"></td>
                </tr>
                <tr>
                    <td class="titleField" colSpan="2">Entertainment (MC, Pengisi Acara, Artis, dll)
                        <asp:datagrid id="dtgEntertainment" runat="server" Width="100%" ShowFooter="True" AllowSorting="True"
                            AutoGenerateColumns="False" PageSize="25">
                            <AlternatingItemStyle BackColor="#FDF1F2"></AlternatingItemStyle>
                            <ItemStyle BackColor="White"></ItemStyle>
                            <HeaderStyle ForeColor="White"></HeaderStyle>
                            <Columns>
                                <asp:BoundColumn Visible="False" DataField="id" HeaderText="Id">
                                    <HeaderStyle Width="3%" CssClass="titleTablePromo"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="No">
                                    <HeaderStyle Width="2%" CssClass="titleTablePromo"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="EventActivityTypeName" HeaderText="Jenis">
                                    <HeaderStyle Width="10%" CssClass="titleTablePromo"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.EventActivityTypeName") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddlEntEventActivityTypeEdit" runat="server" Width="100%"></asp:DropDownList>
                                    </EditItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="Item" HeaderText="Item">
                                    <HeaderStyle Width="10%" CssClass="titleTablePromo"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label id=Label8 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Item") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox id=txtEntItemEdit runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Item") %>' MaxLength="70" Width="100%">
                                        </asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="Quantity" HeaderText="Qty">
                                    <HeaderStyle Width="3%" CssClass="titleTablePromo"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label id=Label9 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Quantity", "{0:#,##0}") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox onkeypress="return NumericOnlyWith(event,'');" id=txtEntQuantityEdit runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Quantity") %>' MaxLength="9" Width="100%">
                                        </asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="UnitCost" HeaderText="Biaya Satuan">
                                    <HeaderStyle Width="10%" CssClass="titleTablePromo"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label id=Label10 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.UnitCost", "{0:#,##0}") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label id="lblEntUnitCost" runat="server" Text="0"></asp:Label>
                                    </FooterTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox onkeypress="return NumericOnlyWith(event,'');" id=txtEntUnitCost runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.UnitCost", "{0:#,##0}") %>' MaxLength="9" Width="100%">
                                        </asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="TotalCost" HeaderText="Sub Total Biaya">
                                    <HeaderStyle Width="10%" CssClass="titleTablePromo"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TotalCost", "{0:#,##0}") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblEntTotalUnitCost" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TotalCost", "{0:#,##0}") %>'>
                                        </asp:Label>
                                    </FooterTemplate>
                                    <EditItemTemplate>
                                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TotalCost", "{0:#,##0}") %>'>
                                        </asp:Label>
                                    </EditItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn>
                                    <HeaderStyle Width="3%" CssClass="titleTablePromo"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnbEntEdit" runat="server" CommandName="Edit" CausesValidation="false" CommandArgument="ENT">
                                            <img src="../images/edit.gif" border="0" alt="Ubah">
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="lnbEntDelete" runat="server" CommandName="Delete" CausesValidation="false" CommandArgument="ENT"
                                            OnClick="lnbDelete_Click">
                                            <img src="../images/trash.gif" border="0" alt="Hapus">
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:LinkButton ID="lnbEntUpdate" runat="server" CausesValidation="false" CommandArgument="ENT"
                                            OnClick="lnbUpdate_Click">
                                            <img src="../images/simpan.gif" border="0" alt="Simpan">
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="lnbEntInsert" runat="server" CausesValidation="false" CommandArgument="ENT"
                                            OnClick="lnbInsert_Click">
                                            <img src="../images/add.gif" border="0" alt="Tambah">
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="lnbEntCancel" runat="server" CommandName="Cancel" CausesValidation="false" CommandArgument="ENT">
                                            <img src="../images/batal.gif" border="0" alt="Batal">
                                        </asp:LinkButton>
                                    </EditItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                            <PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
                        </asp:datagrid></td>
                </tr>
                <tr>
                    <td style="HEIGHT: 19px"></td>
                </tr>
                <tr>
                    <td class="titleField" colSpan="2">Dekorasi
                        <asp:datagrid id="dtgDecoration" runat="server" Width="100%" ShowFooter="True" AllowSorting="True"
                            AutoGenerateColumns="False" PageSize="25">
                            <AlternatingItemStyle BackColor="#FDF1F2"></AlternatingItemStyle>
                            <ItemStyle BackColor="White"></ItemStyle>
                            <HeaderStyle ForeColor="White"></HeaderStyle>
                            <Columns>
                                <asp:BoundColumn Visible="False" DataField="id" HeaderText="Id">
                                    <HeaderStyle Width="3%" CssClass="titleTablePromo"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="No">
                                    <HeaderStyle Width="2%" CssClass="titleTablePromo"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="EventActivityTypeName" HeaderText="Jenis">
                                    <HeaderStyle Width="10%" CssClass="titleTablePromo"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.EventActivityTypeName") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddlDecEventActivityTypeEdit" runat="server" Width="100%"></asp:DropDownList>
                                    </EditItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="Item" HeaderText="Item">
                                    <HeaderStyle Width="10%" CssClass="titleTablePromo"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label id=Label11 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Item") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox id=txtDecEventActivityTypeEdit runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Item") %>' MaxLength="70" Width="100%">
                                        </asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="Quantity" HeaderText="Qty">
                                    <HeaderStyle Width="3%" CssClass="titleTablePromo"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label id=Label12 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Quantity", "{0:#,##0}") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox onkeypress="return NumericOnlyWith(event,'');" id=txtDecQuantity runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Quantity") %>' MaxLength="9" Width="100%">
                                        </asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="UnitCost" HeaderText="Biaya Satuan">
                                    <HeaderStyle Width="10%" CssClass="titleTablePromo"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label id=Label13 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.UnitCost", "{0:#,##0}") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label id="lblDecUnitCost" runat="server" Text="0"></asp:Label>
                                    </FooterTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox onkeypress="return NumericOnlyWith(event,'');" id=txtDecUnitCost runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.UnitCost", "{0:#,##0}") %>' MaxLength="9" Width="100%">
                                        </asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="TotalCost" HeaderText="Sub Total Biaya">
                                    <HeaderStyle Width="10%" CssClass="titleTablePromo"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TotalCost", "{0:#,##0}") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblDecTotalUnitCost" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TotalCost", "{0:#,##0}") %>'>
                                        </asp:Label>
                                    </FooterTemplate>
                                    <EditItemTemplate>
                                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TotalCost", "{0:#,##0}") %>'>
                                        </asp:Label>
                                    </EditItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn>
                                    <HeaderStyle Width="3%" CssClass="titleTablePromo"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnbDecEdit" runat="server" CommandName="Edit" CausesValidation="false" CommandArgument="DEK">
                                            <img src="../images/edit.gif" border="0" alt="Ubah">
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="lnbDecDelete" runat="server" CommandName="Delete" CausesValidation="false" CommandArgument="DEK"
                                            OnClick="lnbDelete_Click">
                                            <img src="../images/trash.gif" border="0" alt="Hapus">
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                    </FooterTemplate>
                                    <EditItemTemplate>
                                        <asp:LinkButton ID="lnbDecUpdate" runat="server" CausesValidation="false" CommandArgument="DEK"
                                            OnClick="lnbUpdate_Click">
                                            <img src="../images/simpan.gif" border="0" alt="Simpan">
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="lnbDecInsert" runat="server" CausesValidation="false" CommandArgument="DEK"
                                            OnClick="lnbInsert_Click">
                                            <img src="../images/add.gif" border="0" alt="Tambah">
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="lnbDecCancel" runat="server" CommandName="Cancel" CausesValidation="false" CommandArgument="DEK">
                                            <img src="../images/batal.gif" border="0" alt="Batal">
                                        </asp:LinkButton>
                                    </EditItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                            <PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
                        </asp:datagrid></td>
                </tr>
                <tr>
                    <td style="HEIGHT: 19px"></td>
                </tr>
                <tr>
                    <td class="titleField" colSpan="2">Display Car
                        <asp:datagrid id="dtgCar" runat="server" Width="100%" ShowFooter="False" AllowSorting="True" AutoGenerateColumns="False"
                            PageSize="25">
                            <AlternatingItemStyle BackColor="#FDF1F2"></AlternatingItemStyle>
                            <ItemStyle BackColor="White"></ItemStyle>
                            <HeaderStyle ForeColor="White"></HeaderStyle>
                            <Columns>
                                <asp:BoundColumn Visible="False" DataField="id" HeaderText="Id">
                                    <HeaderStyle Width="3%" CssClass="titleTablePromo"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="No">
                                    <HeaderStyle Width="2%" CssClass="titleTablePromo"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="VechileModelDescription" HeaderText="Model">
                                    <HeaderStyle Width="12%" CssClass="titleTablePromo"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.VechileModelDescription") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddlCarModel" runat="server" Width="100%" AutoPostBack="True" OnSelectedIndexChanged="ddlCarModel_SelectedIndexChanged"></asp:DropDownList>
                                    </EditItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="VechileTypeDescription" HeaderText="Item">
                                    <HeaderStyle Width="12%" CssClass="titleTablePromo"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label id=Label14 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.VechileTypeDescription") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList id="ddlCarVariant" runat="server" Width="100%"></asp:DropDownList>
                                    </EditItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="Quantity" HeaderText="Qty">
                                    <HeaderStyle Width="3%" CssClass="titleTablePromo"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label id=Label15 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Quantity", "{0:#,##0}") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox onkeypress="return NumericOnlyWith(event,'');" id=txtCarQuantity runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Quantity") %>' MaxLength="9" Width="100%">
                                        </asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="Description" HeaderText="Keterangan">
                                    <HeaderStyle Width="15%" CssClass="titleTablePromo"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label id=Label16 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Description") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox id=txtCarDescription runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Description") %>' MaxLength="100" Width="100%">
                                        </asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn>
                                    <HeaderStyle Width="3%" CssClass="titleTablePromo"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnbCardEdit" runat="server" CommandName="Edit" CausesValidation="false" CommandArgument="CAR">
                                            <img src="../images/edit.gif" border="0" alt="Ubah">
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="lnbCarDelete" runat="server" CommandName="Delete" CausesValidation="false" CommandArgument="CAR"
                                            OnClick="lnbDelete_Click">
                                            <img src="../images/trash.gif" border="0" alt="Hapus">
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:LinkButton ID="lnbCarUpdate" runat="server" CausesValidation="false" CommandArgument="CAR"
                                            OnClick="lnbUpdate_Click">
                                            <img src="../images/simpan.gif" border="0" alt="Simpan">
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="lnbCarInsert" runat="server" CausesValidation="false" CommandArgument="CAR"
                                            OnClick="lnbInsert_Click">
                                            <img src="../images/add.gif" border="0" alt="Tambah">
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="lnbCarCancel" runat="server" CommandName="Cancel" CausesValidation="false" CommandArgument="CAR">
                                            <img src="../images/batal.gif" border="0" alt="Batal">
                                        </asp:LinkButton>
                                    </EditItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                            <PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
                        </asp:datagrid></td>
                </tr>
                <tr>
                    <td style="HEIGHT: 19px"></td>
                </tr>
                <tr>
                    <td class="titleField" colSpan="2">Door Prize
                        <asp:datagrid id="dtgDoorPize" runat="server" Width="100%" ShowFooter="True" AllowSorting="True"
                            AutoGenerateColumns="False" PageSize="25">
                            <AlternatingItemStyle BackColor="#FDF1F2"></AlternatingItemStyle>
                            <ItemStyle BackColor="White"></ItemStyle>
                            <HeaderStyle ForeColor="White"></HeaderStyle>
                            <Columns>
                                <asp:BoundColumn Visible="False" DataField="id" HeaderText="Id">
                                    <HeaderStyle Width="3%" CssClass="titleTablePromo"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="No">
                                    <HeaderStyle Width="2%" CssClass="titleTablePromo"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="EventActivityTypeName" HeaderText="Jenis">
                                    <HeaderStyle Width="10%" CssClass="titleTablePromo"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.EventActivityTypeName") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddlDoorPrizeType" Width="100%" runat="server"></asp:DropDownList>
                                    </EditItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="Item" HeaderText="Item">
                                    <HeaderStyle Width="10%" CssClass="titleTablePromo"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label id=Label17 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Item") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox id=txtDoorPrizeItem runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Item") %>' MaxLength="70" Width="100%">
                                        </asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="Quantity" HeaderText="Qty">
                                    <HeaderStyle Width="3%" CssClass="titleTablePromo"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label id=Label18 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Quantity", "{0:#,##0}") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox onkeypress="return NumericOnlyWith(event,'');" id=txtDoorPrizeQuantity runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Quantity") %>' MaxLength="9" Width="100%">
                                        </asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="UnitCost" HeaderText="Biaya Satuan">
                                    <HeaderStyle Width="10%" CssClass="titleTablePromo"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label id=Label19 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.UnitCost", "{0:#,##0}") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label id="lblDoorPrizeUnitCost" runat="server" Text="0"></asp:Label>
                                    </FooterTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox onkeypress="return NumericOnlyWith(event,'');" id=txtDoorPrizeUnitCost runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.UnitCost", "{0:#,##0}") %>' MaxLength="9" Width="100%">
                                        </asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="TotalCost" HeaderText="Sub Total Biaya">
                                    <HeaderStyle Width="10%" CssClass="titleTablePromo"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TotalCost", "{0:#,##0}") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblDoorPrizeTotalUnitCost" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TotalCost", "{0:#,##0}") %>'>
                                        </asp:Label>
                                    </FooterTemplate>
                                    <EditItemTemplate>
                                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TotalCost", "{0:#,##0}") %>'>
                                        </asp:Label>
                                    </EditItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn>
                                    <HeaderStyle Width="3%" CssClass="titleTablePromo"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnbDoorPrizeEdit" runat="server" CommandName="Edit" CausesValidation="false"
                                            CommandArgument="DOR">
                                            <img src="../images/edit.gif" border="0" alt="Ubah">
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="lnbDoorPrizeDelete" runat="server" CommandName="Delete" CausesValidation="false"
                                            CommandArgument="DOR" OnClick="lnbDelete_Click">
                                            <img src="../images/trash.gif" border="0" alt="Hapus">
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:LinkButton ID="lnbDoorPrizeUpdate" runat="server" CausesValidation="false" CommandArgument="DOR"
                                            OnClick="lnbUpdate_Click">
                                            <img src="../images/simpan.gif" border="0" alt="Simpan">
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="lnbDoorPrizeInsert" runat="server" CausesValidation="false" CommandArgument="DOR"
                                            OnClick="lnbInsert_Click">
                                            <img src="../images/add.gif" border="0" alt="Tambah">
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="lnbDoorPrizeCancel" runat="server" CommandName="Cancel" CausesValidation="false"
                                            CommandArgument="DOR">
                                            <img src="../images/batal.gif" border="0" alt="Batal">
                                        </asp:LinkButton>
                                    </EditItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                            <PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
                        </asp:datagrid></td>
                </tr>
                <tr>
                    <td style="HEIGHT: 19px"></td>
                </tr>
                <tr>
                    <td class="titleField" colSpan="2">Lain-Lain
                        <asp:datagrid id="dtgOthers" runat="server" Width="100%" ShowFooter="True" AllowSorting="True"
                            AutoGenerateColumns="False" PageSize="25">
                            <AlternatingItemStyle BackColor="#FDF1F2"></AlternatingItemStyle>
                            <ItemStyle BackColor="White"></ItemStyle>
                            <HeaderStyle ForeColor="White"></HeaderStyle>
                            <Columns>
                                <asp:BoundColumn Visible="False" DataField="id" HeaderText="Id">
                                    <HeaderStyle Width="3%" CssClass="titleTablePromo"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="No">
                                    <HeaderStyle Width="2%" CssClass="titleTablePromo"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="EventActivityTypeName" HeaderText="Jenis">
                                    <HeaderStyle Width="10%" CssClass="titleTablePromo"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.EventActivityTypeName") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddlOtherType" Width="100%" runat="server"></asp:DropDownList>
                                    </EditItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="Item" HeaderText="Item">
                                    <HeaderStyle Width="10%" CssClass="titleTablePromo"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label id=Label20 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Item") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox id=txtOtherItem runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Item") %>' MaxLength="70" Width="100%">
                                        </asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="Quantity" HeaderText="Qty">
                                    <HeaderStyle Width="3%" CssClass="titleTablePromo"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label id=Label21 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Quantity", "{0:#,##0}") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox onkeypress="return NumericOnlyWith(event,'');" id=txtOtherQuantity runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Quantity") %>' MaxLength="9" Width="100%">
                                        </asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="UnitCost" HeaderText="Biaya Satuan">
                                    <HeaderStyle Width="10%" CssClass="titleTablePromo"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label id=Label22 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.UnitCost", "{0:#,##0}") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label id="lblOtherUnitCost" runat="server" Text="0"></asp:Label>
                                    </FooterTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox onkeypress="return NumericOnlyWith(event,'');" id=txtOtherUnitCost runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.UnitCost", "{0:#,##0}") %>' MaxLength="9" Width="100%">
                                        </asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="TotalCost" HeaderText="Sub Total Biaya">
                                    <HeaderStyle Width="10%" CssClass="titleTablePromo"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TotalCost", "{0:#,##0}") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblOtherTotalCost" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TotalCost", "{0:#,##0}") %>'>
                                        </asp:Label>
                                    </FooterTemplate>
                                    <EditItemTemplate>
                                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TotalCost", "{0:#,##0}") %>'>
                                        </asp:Label>
                                    </EditItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn>
                                    <HeaderStyle Width="3%" CssClass="titleTablePromo"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnbOtherEdit" runat="server" CommandName="Edit" CausesValidation="false" CommandArgument="OTH">
                                            <img src="../images/edit.gif" border="0" alt="Ubah">
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="lnbOtherDelete" runat="server" CommandName="Delete" CausesValidation="false"
                                            CommandArgument="OTH" OnClick="lnbDelete_Click">
                                            <img src="../images/trash.gif" border="0" alt="Hapus">
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:LinkButton ID="lnbOtherUpdate" runat="server" CausesValidation="false" CommandArgument="OTH"
                                            OnClick="lnbUpdate_Click">
                                            <img src="../images/simpan.gif" border="0" alt="Simpan">
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="lnbOtherInsert" runat="server" CausesValidation="false" CommandArgument="OTH"
                                            OnClick="lnbInsert_Click">
                                            <img src="../images/add.gif" border="0" alt="Tambah">
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="lnbOtherCancel" runat="server" CommandName="Cancel" CausesValidation="false"
                                            CommandArgument="OTH">
                                            <img src="../images/batal.gif" border="0" alt="Batal">
                                        </asp:LinkButton>
                                    </EditItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                            <PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
                        </asp:datagrid></td>
                </tr>
                <tr>
                    <td style="WIDTH: 73%"></td>
                    <td class="titleField" style="WIDTH: 27%"><asp:label id="lblTotalAllCost" Runat="server"></asp:label></td>
                </tr>
                <TR>
                    <td class="titleField" style="PADDING-LEFT: 20px" colSpan="2"><asp:button id="btnEdit" runat="server" Text="Ubah" Width="64px" Visible="False"></asp:button><asp:button id="btnSave" runat="server" Text="Simpan" Width="64px"></asp:button><asp:button id="btnValidation" runat="server" Text="Validasi" Width="64px" Visible="False" CausesValidation="False"></asp:button><INPUT id="btnBack" onclick="window.history.back();return false;" type="button" value="Kembali"
                            name="btnBack" runat="server"></td>
                </TR>
            </TABLE>
            <asp:panel id="htipToolTip" Runat="server" CssClass="gridtooltip">
                <asp:Label id="lblToolTip" Runat="server">Bentuk rear body/Modifikasi Kendaraan</asp:Label>
            </asp:panel></form>
    </body>
</HTML>
