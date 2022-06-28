<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frmPKTMasterSpecimenSignature.aspx.vb" Inherits=".frmPKTMasterSpecimenSignature" %>

<%@ Register TagPrefix="cc1" Namespace="KTB.DNet.WebCC" Assembly="KTB.DNet.WebCC" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<html>
<head>
		<title>PKT Master Specimen & Signature</title>
		    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script language="javascript" type="text/javascript" src="../WebResources/PreventNewWindow.js"></script>
    <script language="javascript" type="text/javascript" src="../WebResources/InputValidation.js"></script>
    <script language="javascript" type="text/javascript" src="../WebResources/jquery-1.8.3.min.js"></script>
    <script language="javascript" type="text/javascript">
        function ShowPPDealerSelection() {
            showPopUp('../PopUp/PopUpDealerSelectionOne.aspx', '', 500, 760, DealerSelection);
        }

        function DealerSelection(selectedDealer) {
            var data = selectedDealer.split(";");
            var txtbox = document.getElementById("txtDealerCode");
            txtbox.value = data[0] + " - " + data[1];
        }
        function ClosePreview() {
            $("#PreviewImage").hide("slow");
        }
        function ShowPreview() {
            $("#PreviewImage").show("slow");
        }

    </script>
	    
	</head>
<body>
    <form id="form1" runat="server">
        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
           <tr>
                <td class="titlePage">&nbsp;Umum&nbsp;-&nbsp;PKT Master Specimen & Signature</td>
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
                <td valign="top" align="left">
                     <table cellspacing="0" cellpadding="3" border="0" style="width: 1037px; height: 64px">
                        <tr>
                            <td class="titleField" width="20%">Dealer</td>
                            <TD width="1%">:</TD>
                            <td width="20%">
                                <asp:TextBox ID="txtDealerCode" onkeypress="return alphaNumericExcept(event,'<>?*%$')" onblur="omitSomeCharacter('txtDealerCode','<>?*%$')"
                                    runat="server" Width="180px"></asp:TextBox>
                                <asp:Label ID="lblSearchDealer" runat="server" onclick="ShowPPDealerSelection();">
									<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup"></asp:Label>
                            </td>
                            <td width ="2%"></td>
                            <td width="40%">
                            </td>
                        </tr>
                         <tr>
                            <td class="titleField" width="20%">
                                <asp:Label ID="lblBerlaku" runat="server">Berlaku </asp:Label></td>
                            <TD class="titleField" width="1%">:</TD>
                            <td class="titleField" width="20%">
                                
                                            <cc1:IntiCalendar ID="icPaymentDateFrom" runat="server" TextBoxWidth="70"></cc1:IntiCalendar></td>
                                       
                                 
                            
                        </tr>
                        <tr>
                            <td class="titleField" width="20%">
                                <asp:Label ID="lblUpload" runat="server">Upload Template </asp:Label>:</td>
                           <TD class="titleField" width="1%">:</TD>
                            <td class="titleField" width="20%">
                                <asp:FileUpload ID="fuUpload" runat="server" Width="300px" />
                            </td>
                                <td class="titleField" width="5%" >
                                     <asp:Button ID="btnUpload" runat="server" Width="65px" Visible="True" Text="Upload" OnClick="btnUpload_Click"></asp:Button>
                                </td>
                            <td class="titleField" width="5%" >
                                 <asp:Button ID="btnPreview" runat="server" Width="65px" Visible="false" Text="Preview" OnClick="btnPreview_Click"></asp:Button>
                                <input type="button" value="Preview" onclick="ShowPreview();" />
                            </td>

                             <td class="titleField" width="5%" >
                                 
                            </td>
                                
                        </tr>

                       <tr>
                            <td class="titleField">Nama</td>
                            <TD width="1%">:</td>
                            <td width="20%">
                                <asp:TextBox ID="txtNama" runat="server" Width="200px"></asp:TextBox>
                            </td>
                            <td width ="2%"></td>
                            <td width="40%">
                            </td>
                        </tr>
                        <tr>
                            <td class="titleField">Posisi</td>
                            <TD width="1%">:</td>
                            <td width="20%">
                                <asp:TextBox ID="txtPosisi" runat="server" Width="200px"></asp:TextBox>
                            </td>
                            <td width ="2%"></td>
                            <td width="40%">
                            </td>
                        </tr>

                         <tr>
                            <td class="titleField">Blok</td>
                            <TD width="1%">:</td>
                            <td width="20%">
                                <asp:DropDownList ID="ddlBlok" runat="server">
                                    <asp:ListItem Text="Silahkan Pilih" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="Blok 1" Value="Blok 1"></asp:ListItem>
                                   
                                </asp:DropDownList>
                            </td>
                             <td width ="2%"></td>
                             <td width="40%">
                                
                            </td>
                        </tr>

                         <tr>
                            <td class="titleField">Status</td>
                            <TD width="1%">:</td>
                            <td width="20%">
                                <asp:DropDownList ID="ddlStatus" runat="server">
                                       <asp:ListItem Text="Silahkan Pilih" Selected="True"></asp:ListItem>
                                       <asp:ListItem Text="Non Aktif" Value="0"></asp:ListItem>
                                       <asp:ListItem Text="Aktif" Value="1"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                             <td width ="2%"></td>
                             <td width="40%">
                                
                            </td>
                        </tr>

                         <tr>
                            <td class="titleField" width="20%"></td>
                            <TD class="titleField" width="1%"></TD>
                            <td class="titleField" width="20%">
                                <asp:Button ID="btnSimpan" runat="server" Width="65px" Visible="True" Text="Simpan" OnClick="btnSimpan_Click" Enabled="false"></asp:Button>
                                <asp:Button ID="btnBatal" runat="server" Width="65px" Visible="True" Text="Batal" CausesValidation="False" OnClick="btnBatal_Click"></asp:Button>
                                <asp:Button ID="btnCari" Visible="true" runat="server" Width="75px" Text="Cari" OnClick="btnCari_Click"></asp:Button>
                                

                            </td>
                            <td>
                            </td>
                            <td class="titleField" width="5%"></td>
                            <td class="titleField" width="5%"></td>
                            <td class="titleField" width="5%"></td>
                        </tr>

                        <tr>
                            <td colspan="6" style="height: 11px">
                                <%--<asp:Label ID="lblTotalRow" Runat="server" />--%>
                                <asp:Label ID="lblMessage" runat="server"></asp:Label>
                            </td>
                        </tr>

                        <tr>
                            <td class="titleField" style="width: 100%; height: 11px" colspan="6">
                                <div id="div1" style="overflow: auto; height: 300px">

                                    <asp:DataGrid ID="dgPKTSpecimen" runat="server" Width="100%" AllowSorting="True" AllowCustomPaging="True" AutoGenerateColumns="False"
                                        BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="0px" BackColor="#CDCDCD" CellPadding="3"
                                        CellSpacing="1" GridLines="Vertical" AllowPaging="True" PageSize="10" 
                                        OnItemCommand="dgPKTSpecimen_ItemCommand" 
                                        OnPageIndexChanged="dgPKTSpecimen_PageIndexChanged"
                                        OnSortCommand="dgPKTSpecimen_SortCommand"
                                        OnItemDataBound="dgPKTSpecimen_ItemDataBound">
                                        <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                                        <AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
                                        <ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
                                        <HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
                                        <Columns>
                                            <asp:BoundColumn Visible="False" DataField="ID" SortExpression="ID" HeaderText="ID"></asp:BoundColumn>
                                            <asp:TemplateColumn HeaderText="No">
                                                <HeaderStyle Width="2%" CssClass="titleTableService"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
										        <ItemTemplate>
											        <asp:Label id="lblNo" runat="server"></asp:Label>
										        </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Dealer" SortExpression="Dealer.DealerCode">
                                                <HeaderStyle Width ="10%" CssClass="titleTableService"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDealer" runat="server" ToolTip='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerCode")%>' Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerCode")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Nama Dealer" SortExpression="Dealer.DealerName">
										        <HeaderStyle Width="15%" CssClass="titleTableService"></HeaderStyle>
										        <ItemTemplate>
											        <asp:Label id="lblNamaDealer" runat="server" ToolTip='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerName")%>' Text='<%# DataBinder.Eval(Container, "DataItem.Dealer.DealerName")%>'>
                                                    </asp:Label>
										        </ItemTemplate>
									        </asp:TemplateColumn>								
									        <asp:TemplateColumn HeaderText="Nama" SortExpression="Name">
										        <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
										        <ItemTemplate>
											        <asp:Label id=lblNama runat="server" ToolTip='<%# DataBinder.Eval(Container, "DataItem.Name")%>' Text='<%# DataBinder.Eval(Container, "DataItem.Name")%>'>
                                                    </asp:Label>
										        </ItemTemplate>
									        </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Posisi" SortExpression="Position">
										        <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
										        <ItemTemplate>
											        <asp:Label id=lblPosisi runat="server" ToolTip='<%# DataBinder.Eval(Container, "DataItem.Position")%>' Text='<%# DataBinder.Eval(Container, "DataItem.Position")%>'>
                                                    </asp:Label>
										        </ItemTemplate>
									        </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Blok" SortExpression="Blok">
										        <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
										        <ItemTemplate>
											        <asp:Label id=lblBlok runat="server" ToolTip='<%# DataBinder.Eval(Container, "DataItem.Blok")%>' Text='<%# DataBinder.Eval(Container, "DataItem.Blok")%>'>
                                                    </asp:Label>
										        </ItemTemplate>
									        </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Berlaku Mulai" SortExpression="ValidFrom">
										        <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
										        <ItemTemplate>
											        <asp:Label id=lblBerlakuMulai runat="server" ToolTip='<%# DataBinder.Eval(Container, "DataItem.ValidFrom")%>' Text='<%# FormatDateTime(DataBinder.Eval(Container, "DataItem.ValidFrom"), Microsoft.VisualBasic.DateFormat.ShortDate) %>'>
                                                    </asp:Label>
										        </ItemTemplate>
									        </asp:TemplateColumn>
                                            <%--<asp:TemplateColumn HeaderText="Berlaku Sampai">
										        <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
										        <ItemTemplate>
											        <asp:Label id=lblBerlakuSampai runat="server" Text="">
											        </asp:Label>
										        </ItemTemplate>
									        </asp:TemplateColumn>--%>
                                            <asp:TemplateColumn HeaderText="Status" SortExpression="Status">
										        <HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
										        <ItemTemplate>
											        <asp:Label id=lblStatus runat="server" ToolTip='<%# DataBinder.Eval(Container, "DataItem.StrStatus")%>' Text='<%# DataBinder.Eval(Container, "DataItem.StrStatus")%>'>
                                                    </asp:Label>
										        </ItemTemplate>
									        </asp:TemplateColumn>
									        <asp:TemplateColumn HeaderText="">
                                                <HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
	                                            <ItemTemplate>
                                                    <asp:HiddenField ID="hdFilename" runat="server"  Value='<%# DataBinder.Eval(Container, "DataItem.FileName") %>'/>
		                                             <asp:LinkButton id="lbDownload" runat="server" CommandName="download">
                                                         <img src="../images/download.gif" border="0" alt="Download File">
		                                             </asp:LinkButton>
	                                            </ItemTemplate>
                                            </asp:TemplateColumn>
                                        </Columns>
                            <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
                                    </asp:DataGrid>
                                </div>
                            </td>
                        </tr>

                     </table>
                </td>
            </tr>
         </table>
            <div id="PreviewImage" style="position:absolute; top:30px; right:50px; display:none">
                <p><asp:Image ID="imgPreview" runat="server" Width="250px" Height="150px" /></p>
                <p><input type="button" value="Tutup" onclick="ClosePreview();" /></p>
            </div>
    </form>
</body>
</html>
