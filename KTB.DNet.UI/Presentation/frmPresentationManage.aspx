 

<%@ Page Language="vb" AutoEventWireup="true" CodeBehind="frmPresentationManage.aspx.vb" Inherits=".frmPresentationManage" SmartNavigation="False" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head  >
    <title>FrmPresentationUpload</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
    <script type="text/javascript" language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script type="text/javascript" language="javascript" src="../WebResources/InputValidation.js"></script>
		<script type="text/javascript" language="javascript" src="../WebResources/FormFunctions.js"></script>
		
    	<script type="text/javascript" language="javascript">
   	    function ShowPopUpForumMember() {
   	        showPopUp('../PopUp/PopUpForumMember.aspx', '', 500, 760, GetIdMemberSelection);
   	    }

   	    function GetIdMemberSelection(selectedUserId) {
   	        var txtKodeID = document.getElementById("txtIDMember");
   	        txtKodeID.value = selectedUserId
   	    }
   	    function GetUserGroupSelection(selectedUserId) {

   	    }
   	    function SetSource(SourceID) {
   	        var hidSourceID =
            document.getElementById("<%=CustomHiddenField.ClientID%>");
        hidSourceID.value = SourceID;
    }
		</script>
</head>
<body onfocus="return checkModal()" onclick="checkModal()" ms_positioning="GridLayout">
    <form id="Form1" runat="server" mehod="post">
         	<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD class="titlePage">Presentasi &nbsp; - &nbsp;Unggah Presentasi</TD>
				</TR>
				<TR>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="/images/bg_hor.gif" border="0"></td>
				</TR>
				<TR>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</TR>
				<TR>
					<TD>
						<TABLE id="Table2" cellSpacing="1" cellPadding="2" width="100%" border="0">
                            <tr>
								<TD class="titleField"><asp:label id="lblTitle" runat="server">Nama</asp:label></TD>
								<TD>:</TD>
								<TD colSpan="2"><asp:textbox onkeypress="return alphaNumericPlusSpaceUniv(event);" id="txtTitle" onblur="alphaNumericPlusSpaceBlur(txtTitle);"
										runat="server" size="65" MaxLength="100"></asp:textbox><asp:requiredfieldvalidator id="rfvTitle" runat="server" ErrorMessage="*" ControlToValidate="txtTitle">*</asp:requiredfieldvalidator></TD>
                                <td rowspan="8" style="width:25%" valign="Top" align="justify"><div style="border:1px solid black;"><ol>
	<li>
		Buka Berkas Presentation (ppt/pptx)</li>
	<li>
		Simpan Ke (Other Format)</li>
	<li>
		Pilih PNG Format (*.png)</li>
	<li>
		Pada Konfirmasi Dialog, Pilih "Every Slide" </li>
	<li>
		Compress hasilnya ke dalam berkas *.Zip tunggal</li>
</ol></div></td>
							</TR>
							<TR>
								<TD class="titleField">Deskripsi</TD>
								<TD>:</TD>
								<TD colSpan="2"><asp:textbox onkeypress="return alphaNumericPlusSpaceUniv(event);" id="txtDescription" onblur="alphaNumericPlusSpaceBlur(txtDescription);"
										runat="server" size="65" MaxLength="255"></asp:textbox><asp:requiredfieldvalidator id="rfvDescription" runat="server" ErrorMessage="*" ControlToValidate="txtDescription">*</asp:requiredfieldvalidator></TD>
							</TR>
							  
							<TR>
								<TD class="titleField">File</TD>
								<TD>:</TD>
								<TD colSpan="2"><INPUT onkeypress="return false;" id="fileUpload" style="WIDTH: 216px; HEIGHT: 20px" type="file"
										size="16" runat="server"> &nbsp; <asp:Label runat="server" ID="lblFileName" ></asp:Label>
								</TD>
                                 
							</TR>
                            
                            <TR >
								<TD class="titleField"></TD>
								<TD></TD>
								<TD colSpan="2"> 
                                    <asp:label id="Label12" runat="server" Width="231px">*.Zip (Ukuran Maksimum File 10Mb)</asp:label>
								</TD>
							</TR>

                            <TR style="display:none;">
								<TD class="titleField">Logo</TD>
								<TD>:</TD>
								<TD colSpan="2"><INPUT onkeypress="return false;" id="fileUploadLogo" style="WIDTH: 216px; HEIGHT: 20px" type="file"
										size="16" runat="server">
								</TD>
							</TR>
                             <TR id="tdStatus" runat="server" visible="true">
								<TD class="titleField">Status</TD>
								<TD>:</TD>
								<TD colSpan="2"> 
                                    <asp:DropDownList ID="ddlStatus" runat="server">
                                     <%--   <asp:ListItem Value="-1">Silahkan Pilih</asp:ListItem>--%>
                                        <asp:ListItem Value="1" >Aktif</asp:ListItem>
                                        <asp:ListItem Value="0">Non Aktif</asp:ListItem>
                                     
                                    </asp:DropDownList>
								</TD>
							</TR>

							<TR>
								<TD class="titleField"></TD>
								<TD></TD>
								<TD colSpan="2">
									<asp:Label id="lblMessage" runat="server" ForeColor="Red" Visible="False">Panjang nama file tidak boleh melebihi 50 character</asp:Label></TD>
							</TR>

                            <TR>
								<TD class="titleField"></TD>
								<TD></TD>
								<TD colSpan="2">
									 </TD>
							</TR>

                            <tr id="trHist" runat="server" visible="false">
                                
                                <td class="titleField" valign="top">DiUpload Oleh & Tanggal Upload</td>
                                <td   >:</td>
                                 
                                            <td><asp:Label ID="lblCreatedBy" runat="server"  valign="top"></asp:Label></td>
                                        
                                
                            </tr>  
                 <tr  id="trhistB" runat="server" visible="false">
                     <td class="titleField" valign="top">Diubah Oleh & Tanggal ubah</td>
                     <td>:</td>
                       <td><asp:Label ID="lblUpdatedBy" runat="server"  valign="top"></asp:Label></td>
                 </tr>
                 

							<TR>
								<TD></TD>
								<TD></TD>
								<TD colSpan="2"><asp:button id="btnUpload" runat="server" Text="Upload"></asp:button>&nbsp;
									<asp:button id="btnBack" runat="server" Text="Kembali" CausesValidation="False" visible="false"></asp:button><asp:button id="btnTambahGroup" runat="server" Text="Tambah Group" CausesValidation="False"></asp:button></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
                 <tr>
                     <td  class="titleField" style="height:5px;">
                         <label></label>
                     </td>
                 </tr>
				<TR>
					<TD>
						<div id="div1" style="OVERFLOW: auto; HEIGHT: 200px"><asp:datagrid id="dtgUserGroup" runat="server" Width="100%" Visible="False" AllowSorting="False"
								AllowCustomPaging="True" AutoGenerateColumns="False" BackColor="Gainsboro" CellPadding="3" BorderColor="#E0E0E0" AllowPaging="True">
								<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
								<AlternatingItemStyle ForeColor="Black" BackColor="#FDF1F2"></AlternatingItemStyle>
								<ItemStyle ForeColor="Black" BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
								<FooterStyle ForeColor="#4A3C8C" BackColor="#B5C7DE"></FooterStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="id" HeaderText="id">
										<HeaderStyle Width="5%" CssClass="titleTableMrk"></HeaderStyle>
									</asp:BoundColumn>

                                    <asp:TemplateColumn visible="false">
													<HeaderStyle Width="3%" CssClass="titleTableMrk"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
													<HeaderTemplate>
														<INPUT id="chkAllItems" onclick="CheckAllDataGridCheckBoxes('chkSelect', document.all.chkAllItems.checked)"
															type="CheckBox">
													</HeaderTemplate>
													<ItemTemplate>
														<asp:CheckBox id="chkSelect" runat="server" ViewStateMode="Enabled"></asp:CheckBox>
													</ItemTemplate>
												</asp:TemplateColumn>

									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle Width="3%" CssClass="titleTableMrk"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblNo" runat="server"></asp:Label>
                                            <asp:Label ID="lblID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>' Visible="false"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
                                   
									<asp:TemplateColumn SortExpression="UserGroup.Code" HeaderText="Kode Group">
										<HeaderStyle Width="25%" CssClass="titleTableMrk"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblUserGroupCode" Text='<%# DataBinder.Eval(Container, "DataItem.UserGroup.Code") %>' runat="server">
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="UserInfo.UserName" HeaderText="Deskripsi Group">
										<HeaderStyle Width="25%" CssClass="titleTableMrk"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblUserDescription" Text='<%# DataBinder.Eval(Container, "DataItem.UserGroup.Description") %>' runat="server">
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn>
										<HeaderStyle Width="15%" CssClass="titleTableMrk"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="lbtnDelete" runat="server" Text="Hapus" Width="16px" CausesValidation="False"
												CommandName="Delete">
												<img src="../images/trash.gif" border="0" alt="Hapus"></asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></div>
					</TD>
				</TR>
                 <tr>
                     <td><br /></td>
                 </tr>
                  <tr>
                     <td><br /></td>
                 </tr>

                  <tr>
                     <td><table><tr></tr>

                         </table></td>
                 </tr>
                 <tr style="display:none;">
                     <TD class="titleField" style="HEIGHT: 11px"  >
                          <asp:button id="btnDeleteUserGroup" runat="server" Text="Delete UserGroup"  Visible="false" OnClientClick = "SetSource(this.id)"></asp:button>
                         <asp:HiddenField ID="CustomHiddenField" runat="server" />
                     </td>
                 </tr>
			</TABLE>
    </form>

    <script type="text/javascript" language="javascript">
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
