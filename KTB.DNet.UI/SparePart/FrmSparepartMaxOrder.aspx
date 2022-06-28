<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmSparepartMaxOrder.aspx.vb" Inherits="FrmSparepartMaxOrder" smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
  <HEAD>
		<TITLE>FrmSparepartMaxOrder</TITLE>
<meta content="Microsoft Visual Studio .NET 7.1" name=GENERATOR>
<meta content="Visual Basic .NET 7.1" name=CODE_LANGUAGE>
<meta content=JavaScript name=vs_defaultClientScript>
<meta content=http://schemas.microsoft.com/intellisense/ie5 name=vs_targetSchema><LINK href="../WebResources/stylesheet.css" type=text/css rel=stylesheet >
<script language=javascript src="../WebResources/InputValidation.js"></script>

<script language=javascript type=text/javascript>
		function ShowPopUpSparePart()
		{			
			showPopUp('../General/../PopUp/PopUpSparePart.aspx','',500,760,SparePartSelection);
		}
		
		function SparePartSelection(selectedSparePart)
		{
			selectedSparePart = selectedSparePart + ";";
			var tempParam = selectedSparePart.split(";");
			
			var txtSPNumber = document.getElementById("txtSPNumber");
			//var txtDescription = document.getElementById("txtDescription");			
			
			txtSPNumber.value = tempParam[0];
			//txtDescription.value = tempParam[1];
		}
		</script>
</HEAD>
<body MS_POSITIONING="GridLayout">
<form id=Form1 method=post runat="server">
<TABLE id=Table1 cellSpacing=1 cellPadding=2 width="100%" border=0>
  <TR>
    <TD colSpan=3>
      <TABLE id=Table1 cellSpacing=0 cellPadding=0 width="100%" border=0>
        <tr>
          <td class=titlePage>MASTER BARANG -&nbsp; 
            Setting Maksimum Stock</TD></TR>
        <tr>
          <td background=../images/bg_hor.gif height=1 
            ><IMG height=1 src="../images/bg_hor.gif" border=0 ></TD></TR>
        <tr>
          <td height=10><IMG height=1 src="../images/dot.gif" border=0 ></TD></TR></TABLE></TD>
	</TR>
	<TR>
		<TD width="20%"><asp:label id=Label1 runat="server" Font-Bold="True"> Nomor Barang</asp:label></TD>
		<td width="1%">:</td>
		<TD width="79%">
            <asp:textbox id=txtSPNumber onkeypress="return alphaNumericExcept(event,'<>?*%$')" onblur="omitSomeCharacter('txtSPNumber','<>?*%$;')" runat="server">
            </asp:textbox>
            <asp:requiredfieldvalidator id=Requiredfieldvalidator2 runat="server" ErrorMessage="*" ControlToValidate="txtSPNumber"></asp:requiredfieldvalidator>
            <asp:label id=lblSearchSparePart runat="server">
							<img src="../images/popup.gif" style="cursor:hand" border="0" alt="Klik popup">
            </asp:label>
		</TD>
	</TR>
  <TR>
    <TD><asp:label id=Label2 runat="server" Font-Bold="True" Visible="False">Nama Barang</asp:label></TD>
		<td width="1%"></td>
    <TD><asp:textbox id=txtDescription runat="server" Visible="False"></asp:textbox></TD></TR>
  <TR>
    <TD><asp:label id=Label3 runat="server" Font-Bold="True">Qty Maksimum</asp:label></TD>
		<td width="1%">:</td>
    <TD><asp:textbox id=txtMaxOrder runat="server"></asp:textbox><asp:requiredfieldvalidator id=RequiredFieldValidator1 runat="server" ErrorMessage="*" ControlToValidate="txtMaxOrder"></asp:requiredfieldvalidator></TD></TR>
  <TR>
    <TD></TD>
		<td width="1%"></td>
    <TD><asp:checkbox id=chkQty runat="server" Text=" Filter Qty Maksiumum > 0"></asp:CheckBox></TD></TR>
  <TR>
    <TD></TD>
		<td width="1%"></td>
    <TD><asp:button id=btnSave runat="server" Text="Simpan" Width="60px"></asp:button> <asp:button id=btnCancel runat="server" Text="Batal" CausesValidation="False" Width="60px"></asp:button> <asp:button id=btnSearch runat="server" Text="Cari" CausesValidation="False" Width="60px"></asp:button></TD></TR>
  <TR>
    <TD></TD>
		<td width="1%"></td>
    <TD><asp:textbox id=txtIDSPMaster runat="server"></asp:textbox><asp:textbox id=txtIDSPMaxOrder runat="server"></asp:textbox></TD></TR>
  <TR>
    <TD colSpan=3>
      <div id=div1 style="OVERFLOW: auto; HEIGHT: 300px"><asp:datagrid id=dgSparePart runat="server" AllowPaging="True" AllowCustomPaging="True" PageSize="25" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" BackColor="White" CellPadding="3" Width="100%" AllowSorting="True" AutoGenerateColumns="False">
								<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#669999"></SelectedItemStyle>
								<ItemStyle ForeColor="#000066"></ItemStyle>
								<HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#006699"></HeaderStyle>
								<FooterStyle ForeColor="#000066" BackColor="White"></FooterStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
										<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn Visible="False" HeaderText="IDSparePartMaxOrder">
										<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White"></HeaderStyle>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White"></HeaderStyle>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="PartNumber" SortExpression="PartNumber" HeaderText="Nomor Barang">
										<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="PartName" SortExpression="PartName" HeaderText="Nama Barang">
										<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Qty Maksimum">
										<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White"   ></HeaderStyle>
									</asp:TemplateColumn>
									<asp:TemplateColumn>
										<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="lbtnView" runat="server" Text="Lihat" Width="20px" CausesValidation="False" CommandName="View">
												<img src="../images/detail.gif" border="0" alt="Lihat"></asp:LinkButton>
											<asp:LinkButton id="lbtnEdit" runat="server" Text="Ubah" Width="20px" CausesValidation="False" CommandName="Edit">
												<img src="../images/edit.gif" border="0" alt="Ubah"></asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" ForeColor="#000066" BackColor="White" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></DIV></TD></TR></TABLE></FORM></SCRIPT>
	</body>
</HTML>
