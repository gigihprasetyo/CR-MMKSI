<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmUploadFleetCustomer.aspx.vb" Inherits=".FrmUploadFleetCustomer" smartNavigation="False"%>

<html>
<head>
    <title>Upload Fleet Customer</title>
	<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
	<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
	<meta content="JavaScript" name="vs_defaultClientScript">
	<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
	<script language=javascript src="../WebResources/PreventNewWindow.js"></script>
	<script language="javascript" src="../WebResources/InputValidation.js"></script>
    <script language="javascript">
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
    
<body MS_POSITIONING="GridLayout">
    <form id="form1" runat="server">
        <table id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
           
            <tr>
			    <td class="titlePage">FLEET MANAGEMENT - Upload Fleet Customer</td>
		    </tr>
		    <tr>
			    <td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
		    </tr>
		    <tr>
			    <td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
		    </tr>

            <tr>
			    <td vAlign="top" align="left">
                    <table id="Table2" cellSpacing="1" width="100%" cellPadding="1" border="0">
                        
                        <tr>
						    <td class="titleField" width="14%">Upload File</td>
						    <td width="1%">:</td>
						    <td width="85%">
                                <input id="UploadFile" onkeydown="return false;" style="WIDTH: 240px" type="file" name="File1" runat="server">
                                <asp:Button ID="btnUpload" runat="server" Text="Upload" />
						    </td>
					    </tr>
                        <tr>
						    <td class="titleField" width="14%">
                                <asp:LinkButton ID="UploadTemplate" runat="server">Upload Template</asp:LinkButton>
                            </td>
						    <td width="1%"></td>
						    <td width="85%">
                            
						    </td>
					    </tr>
                        <asp:Panel ID="pnlGrid" runat="server" Width="100%">
                        <tr>
                            <td colspan="3">
                                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                    <tr>
                                        <td colspan="6" class="titlePanel">
                                            <b>Fleet Customer</b>
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr>
				                        <td background="../images/bg_hor.gif" height="1" colspan="6"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
                                        <td></td>
			                        </tr>
                                    <tr>
                                        <td colspan="6">
                                             <div id="div1" style="OVERFLOW: auto;">
                                                <asp:Datagrid ID="dtgUploadFleetCustomer" runat="server" Width="100%" CellSpacing="1" GridLines="None"
                                                CellPadding="3" BackColor="#E0E0E0" BorderWidth="0px" BorderStyle="None" BorderColor="#E0E0E0"
                                                AutoGenerateColumns="False" PageSize="100" AllowCustomPaging="true" AllowPaging="false" AllowSorting="True"
                                                DataKeyField="ID" ShowFooter="false">
                                    
                                                <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                                                <AlternatingItemStyle BackColor="#F5F1EE"></AlternatingItemStyle>
                                                <ItemStyle ForeColor="#4A3C8C" BackColor="White"></ItemStyle>
                                                <HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
                                                <FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
								                    <Columns>
                                                        <asp:BoundColumn Visible="false" DataField="ID" HeaderText="ID">
									                        <HeaderStyle Width="5%" CssClass="titleTableGeneral"></HeaderStyle>
									                        <ItemStyle ForeColor="Gray" BackColor="LightPink"></ItemStyle>
								                        </asp:BoundColumn>

                                                        <asp:TemplateColumn HeaderText="Code">
										                    <HeaderStyle CssClass="titleTableSales" ForeColor="White"></HeaderStyle>
										                    <ItemTemplate>
                                                                <div style="width:120px">
                                                                    <asp:Label id="lblCode" runat="server" Text=""></asp:Label>
                                                                </div>
											                    
										                    </ItemTemplate>
                                                            <EditItemTemplate>
                                                                 <div style="width:140px">
                                                                   <asp:HiddenField id="hdnType" runat="server"></asp:HiddenField>
                                                                    <asp:HiddenField id="hdnProfile" runat="server"></asp:HiddenField>
                                                                    <asp:HiddenField id="hdnGroup" runat="server"></asp:HiddenField>
                                                                    <asp:Label id="lblCodeEdit" runat="server" Text=""></asp:Label>
														            <asp:textbox id="txtCode" width="50px" runat="server" Text=""></asp:textbox>
                                                                </div>
                                                                	
													        </EditItemTemplate>
									                    </asp:TemplateColumn>

                                                        <asp:TemplateColumn SortExpression="CategoryIndex" HeaderText="Kategori(Grup)">
										                    <HeaderStyle CssClass="titleTableSales" ForeColor="White"></HeaderStyle>
                                                           
										                    <ItemTemplate>
											                    <asp:Label id="lblCategory" runat="server" Text=""></asp:Label>
										                    </ItemTemplate>                                                           
                                                             <EditItemTemplate>
														        <asp:dropdownlist id="ddlCategory" OnSelectedIndexChanged="ddlGrid_SelectedIndexChanged" AutoPostBack = "true"  runat="server" Width="150px" ></asp:dropdownlist>	
													        </EditItemTemplate>
									                    </asp:TemplateColumn>

                                                         <asp:TemplateColumn SortExpression="TypeIndex" HeaderText="Tipe Perusahaan">
										                    <HeaderStyle CssClass="titleTableSales" ForeColor="White"></HeaderStyle>
                                                            
										                    <ItemTemplate>
											                    <asp:Label id="lblTypePerusahaan" runat="server" Text=""></asp:Label>
										                    </ItemTemplate>
                                                              <EditItemTemplate>
														        <asp:dropdownlist id="ddlType" OnSelectedIndexChanged="ddlGrid_SelectedIndexChanged" AutoPostBack = "true"  runat="server" Width="150px" ></asp:dropdownlist>	
													        </EditItemTemplate>
									                    </asp:TemplateColumn>

                                                        <asp:TemplateColumn SortExpression="ClassificationIndex" HeaderText="Klasifikasi">
										                    <HeaderStyle CssClass="titleTableSales" ForeColor="White"></HeaderStyle>
                                                           
										                    <ItemTemplate>
											                    <asp:Label id="lblClassification" runat="server" Text=""></asp:Label>
										                    </ItemTemplate>
                                                             <EditItemTemplate>
														        <asp:dropdownlist id="ddlClassification"  runat="server" OnSelectedIndexChanged="ddlGrid_SelectedIndexChanged" AutoPostBack = "true" Width="120px" ></asp:dropdownlist>	
													        </EditItemTemplate>
									                    </asp:TemplateColumn>

                                                        <asp:BoundColumn DataField="Name" SortExpression="Name" HeaderText="Nama Fleet Konsumen">
										                    <HeaderStyle CssClass="titleTableSales" ForeColor="White"></HeaderStyle>
									                    </asp:BoundColumn>

                                                        <asp:TemplateColumn SortExpression="CustomerGroupID.Name" HeaderText="Nama Grup Fleet Konsumen">
										                    <HeaderStyle CssClass="titleTableSales" ForeColor="White"></HeaderStyle>
										                    <ItemTemplate>
											                    <asp:Label id="lblCustomerGroup" runat="server" Text=""></asp:Label>
										                    </ItemTemplate>
                                                             <EditItemTemplate>
														        <asp:dropdownlist id="ddlCustomerGroup" OnSelectedIndexChanged="ddlGrid_SelectedIndexChanged" AutoPostBack = "true"  runat="server" Width="150px" ></asp:dropdownlist>	
													        </EditItemTemplate>
									                    </asp:TemplateColumn>

                                                        <asp:TemplateColumn SortExpression="BusinessSectorDetailID" HeaderText="Profil Bisnis Konsumen">
										                    <HeaderStyle CssClass="titleTableSales" ForeColor="White"></HeaderStyle>
										                    <ItemTemplate>
											                    <asp:Label id="lblBusinessSectorDetail" runat="server" Text=""></asp:Label>
										                    </ItemTemplate>
                                                             <EditItemTemplate>
														        <asp:dropdownlist id="ddlBusinessSectorDetail" OnSelectedIndexChanged="ddlGrid_SelectedIndexChanged" AutoPostBack = "true"  runat="server" Width="150px" ></asp:dropdownlist>	
													        </EditItemTemplate>
									                    </asp:TemplateColumn>

                                                         <asp:BoundColumn DataField="Alamat" SortExpression="Alamat" HeaderText="Alamat Fleet Konsumen">
										                    <HeaderStyle CssClass="titleTableSales" ForeColor="White"></HeaderStyle>
									                    </asp:BoundColumn>

                                                         <asp:BoundColumn DataField="Gedung" SortExpression="Gedung" HeaderText="Gedung Fleet Konsumen">
										                    <HeaderStyle CssClass="titleTableSales" ForeColor="White"></HeaderStyle>
									                    </asp:BoundColumn>

                                                         <asp:TemplateColumn SortExpression="City.CityName" HeaderText="Kota">
										                    <HeaderStyle CssClass="titleTableSales" ForeColor="White"></HeaderStyle>
										                    <ItemTemplate>
											                    <asp:Label id="lblCity" runat="server" Text=""></asp:Label>
										                    </ItemTemplate>
                                                             <EditItemTemplate>
														        <asp:dropdownlist id="ddlCity"  runat="server" Width="150px" ></asp:dropdownlist>	
													        </EditItemTemplate>
									                    </asp:TemplateColumn>

                                                         <asp:BoundColumn DataField="Kecamatan" SortExpression="Kecamatan" HeaderText="Kecamatan">
										                    <HeaderStyle CssClass="titleTableSales" ForeColor="White"></HeaderStyle>
									                    </asp:BoundColumn>

                                                         <asp:BoundColumn DataField="Kelurahan" SortExpression="Kelurahan" HeaderText="Kelurahan">
										                    <HeaderStyle CssClass="titleTableSales" ForeColor="White"></HeaderStyle>
									                    </asp:BoundColumn>
                                                        
                                                         <asp:TemplateColumn SortExpression="PreArea" HeaderText="Pre Area">
										                    <HeaderStyle CssClass="titleTableSales" ForeColor="White"></HeaderStyle>
                                                             <ItemTemplate>
											                    <asp:Label id="lblPreArea" runat="server" Text=""></asp:Label>
										                    </ItemTemplate>
									                    </asp:TemplateColumn>

                                                        <asp:BoundColumn DataField="Email" SortExpression="Email" HeaderText="Email">
										                    <HeaderStyle CssClass="titleTableSales" ForeColor="White"></HeaderStyle>
									                    </asp:BoundColumn>

                                                        <asp:BoundColumn DataField="PhoneNo" SortExpression="PhoneNo" HeaderText="No Telepon">
										                    <HeaderStyle CssClass="titleTableSales" ForeColor="White"></HeaderStyle>
									                    </asp:BoundColumn>

                                                        <asp:BoundColumn DataField="PostalCode" SortExpression="PostalCode" HeaderText="Kode Pos">
										                    <HeaderStyle CssClass="titleTableSales" ForeColor="White"></HeaderStyle>
									                    </asp:BoundColumn>
                                                        <asp:BoundColumn DataField="IdentityType" SortExpression="IdentityType" HeaderText="Tipe Identitas">
										                    <HeaderStyle CssClass="titleTableSales" ForeColor="White"></HeaderStyle>
									                    </asp:BoundColumn>
                                                        <asp:BoundColumn DataField="IdentityNumber" SortExpression="IdentityNumber" HeaderText="No. Identitas">
										                    <HeaderStyle CssClass="titleTableSales" ForeColor="White"></HeaderStyle>
									                    </asp:BoundColumn>

                                                        <asp:TemplateColumn HeaderText="Dealer">
										                    <HeaderStyle CssClass="titleTableSales" ForeColor="White"></HeaderStyle>
										                    <ItemTemplate>
											                    <asp:Label id="lblDealers" runat="server" Text=""></asp:Label>
										                    </ItemTemplate>
                                                            <EditItemTemplate>
														        <asp:textbox id="txtDealers" width="150px" runat="server" Text=""></asp:textbox>	
													        </EditItemTemplate>
									                    </asp:TemplateColumn>

                                                         <asp:TemplateColumn HeaderText="Remarks">
										                    <HeaderStyle CssClass="titleTableSales" ForeColor="White"></HeaderStyle>
										                    <ItemTemplate>
                                                                <div style="width:200px">
                                                                    <asp:Label id="lblRemarks" runat="server" Text="" ></asp:Label>
                                                                </div>
											                    
										                    </ItemTemplate>
                                                             <EditItemTemplate>
                                                                 <div style="width:200px">
                                                                     <asp:Label id="lblRemarksEdit" runat="server" Text=""></asp:Label>
                                                                 </div>
														        
													        </EditItemTemplate>
									                    </asp:TemplateColumn>

                                                        <%--<asp:TemplateColumn>
                                                            <HeaderStyle ForeColor="White" HorizontalAlign="Center" CssClass="titleTableSales"></HeaderStyle>
                                                            <FooterStyle HorizontalAlign="Left"></FooterStyle>
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            <ItemTemplate>  
                                                                <div style="width:55px;text-align:center" runat="server" id="btnCommand">
                                                                      <asp:LinkButton id="lbtnEdit" runat="server" Width="20px" Text="Ubah" CausesValidation="False" CommandName="edit"><img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton><asp:LinkButton id="lbtnDelete" runat="server" Width="16px" Text="Hapus" CausesValidation="False" CommandName="Delete"><img src="../images/trash.gif" border="0" alt="Hapus">
										                        </asp:LinkButton>
                                                                </div>
                                                             
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <div style="width:55px;text-align:center">
                                                                    <asp:LinkButton id="lbtnSave" tabIndex="40" Runat="server" CausesValidation="True" CommandName="Update"
															        text="Simpan">
															        <img src="../images/simpan.gif" border="0" alt="Simpan"></asp:LinkButton>
														        <asp:LinkButton id="lbtnCancel" tabIndex="50" Runat="server" CausesValidation="True" CommandName="Cancel"
															        text="Batal">
															        <img src="../images/batal.gif" border="0" alt="Batal"></asp:LinkButton>
                                                                </div>

													        </EditItemTemplate>
                                            
                                                        </asp:TemplateColumn>--%>
								                    </Columns>                                                    
							                    </asp:Datagrid>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <asp:Button ID="btnSave" runat="server" Text="Simpan" />
                                <asp:Button ID="btnBack" runat="server" Text="Kembali" visible="false" />
                            </td>
                        </tr>
                        </asp:Panel>
                        
                    </table>
                </td>
            </tr>
                
        </table>
    </form>
</body>
</html>
