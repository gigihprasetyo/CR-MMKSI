<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmTrClassRegistration2.aspx.vb" Inherits="FrmTrClassRegistration2" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmTrClassRegistration2</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript">
			/*function DealerSelection(selectedCode)
			{
				var txtDealer = document.getElementById("txtKodeDealer");
				txtDealer.value = selectedCode;
				txtDealer.focus();
			}
			function CheckAll(aspCheckBoxID, checkVal) 
			{
				re = new RegExp(':' + aspCheckBoxID + '$');  
				for(i = 0; i < document.forms[0].elements.length; i++) {
					elm = document.forms[0].elements[i];
					if (elm.type == 'checkbox') {
						if (re.test(elm.name)) {
							elm.checked = checkVal;
						}
					}
				}
			}
			*/
			function GetAllocatedAllowed()
			{
				var allocated = document.getElementById("divAllocatedTot");
				//var registered = document.getElementById("divAllocatedReg");
				return parseInt(allocated.innerText) 
				//return parseInt(allocated.innerText) - parseInt(registered.innerText);
			}
			function CheckEnability()
			{
				var allowed = GetAllocatedAllowed();
				var counterCheck = 0;
				var checkString = "";
				var checkColl = document.getElementById("txtItemCheckColl");
				checkColl.value = "";
				var table = document.getElementById("dtgTrainee");			
				for(i=1;i<table.rows.length;i++)
				{
					var checkbox = table.rows[i].cells[0].getElementsByTagName("INPUT")[0];
					if (checkbox.checked)
					{
						//use to check max trainee that can choosen
						counterCheck++;
						checkString += i + ";";
					}
					else
					// Penambahan oleh Agus untuk bug fix sbb.
					// untuk kode kelas tertentu pada suatu dealer yang memiliki alokasi 1 orang saja 
					// pada saat check salah satu siswa kemudian membatalkannya siswa yang lain tidak 
					// bisa dicheck (disable) 
					{
						checkString += " " + ";";
					}
				}
				//display collection row index in table that has been checked
				checkColl.value = checkString;
				//disable check box if reach max capacity
					if (counterCheck < allowed) {
						DisableCheckBox(false);				
					}
					else {
						if (counterCheck == allowed)
						{
							DisableCheckBox(true);
						}
					}
			}
			function DisableCheckBox(disabled)
			{
				var checkColl = document.getElementById("txtItemCheckColl");
				if (checkColl.value != "")
				{
					var arrCheckColl = checkColl.value.split(";");
					if (arrCheckColl.length > 0)
					{
						var table = document.getElementById("dtgTrainee");			
						//var i;
						for(i=1;i<table.rows.length;i++)
						{
							
							var checkbox = table.rows[i].cells[0].getElementsByTagName("INPUT")[0];
							//just watch index row not in checkColl
							if (! CatchIt(i, arrCheckColl))
							{
								checkbox.disabled = disabled;	
							}
							/*
							var a;
							for(a=0;a<arrCheckColl.length-1;a++)
							{
								if (arrCheckColl[a] != i)
								{	
									checkbox.disabled = disabled;
								}
							}
							*/
						   }
					
					}
				}
			}
			
			
			function CatchIt(indexRow, checkColl)
			{
				var a;
				for(a=0;a<checkColl.length-1;a++)
				{
					if (checkColl[a] == indexRow)
					{	
						return true;
					}
				}
				return false;
			}
			
			
			
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form2" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="titlePage">TRAINING - Pendaftaran - Pilih Siswa</td>
				</tr>
				<tr>
					<td background="../images/bg_hor.gif" height="1"><IMG height="1" src="../images/bg_hor.gif" border="0"></td>
				</tr>
				<tr>
					<td height="10"><IMG height="1" src="../images/dot.gif" border="0"></td>
				</tr>
				<TR>
					<TD style="HEIGHT: 80px">
						<TABLE id="Table2" cellSpacing="1" cellPadding="2" border="0">
							<colgroup>
								<col width="14%">
								<col width="1%">
								<col width="25%">
								<col width="24%">
								<col width="1%">
								<col width="35%">
							</colgroup>
							<TR>
								<TD class="titleField">Kode Kelas</TD>
								<td width="1%">:</td>
								<TD colSpan="4"><asp:label id="lblClassCode" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField">Nama Kelas</TD>
								<td width="1%">:</td>
								<TD colSpan="4"><asp:label id="lblClassName" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField">Mulai</TD>
								<td width="1%">:</td>
								<TD><asp:label id="lblStartDate" runat="server"></asp:label></TD>
								<TD class="titleField">Selesai</TD>
								<td width="1%">:</td>
								<TD><asp:label id="lblFinishDate" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD class="titleField">Lokasi</TD>
								<td width="1%">:</td>
								<TD><asp:label id="lblLocation" runat="server"></asp:label></TD>
								<TD class="titleField">Alokasi</TD>
								<td width="1%">:</td>
								<TD>
									<div id="divAllocatedTot" style="WIDTH: 100%"><asp:label id="lblAllocatedTot" runat="server"></asp:label></div>
								</TD>
							</TR>
							<TR>
								<TD class="titleField"></TD>
								<td width="1%"></td>
								<TD></TD>
								<TD class="titleField">Yang Sudah Terdaftar</TD>
								<td width="1%">
									<P>:</P>
								</td>
								<TD>
									<div id="divAllocatedReg" style="WIDTH: 100%"><asp:label id="lblAllocatedReg" runat="server"></asp:label></div>
								</TD>
							</TR>
						</TABLE>
						<INPUT id="txtItemCheckColl" onkeypress="return numericOnlyUniv(event)" type="hidden" name="txtItemCheckColl"
							runat="server">
					</TD>
				</TR>
				<tr>
					<td></td>
				</tr>
				<tr>
					<td class="titleField">Silakan pilih siswa yang diinginkan untuk mengikuti kelas 
						ini</td>
				</tr>
				<TR>
					<TD>
						<div id="divTrainee" style="OVERFLOW: auto; HEIGHT: 360px"><asp:datagrid id="dtgTrainee" runat="server" Font-Size="Small" AutoGenerateColumns="False" BorderColor="#E0E0E0"
								BorderStyle="None" BorderWidth="0px" BackColor="#E0E0E0" CellPadding="3" GridLines="Vertical" CellSpacing="1" Width="100%">
								<FooterStyle ForeColor="Black" BackColor="#CCCCCC"></FooterStyle>
								<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#008A8C"></SelectedItemStyle>
								<AlternatingItemStyle BackColor="#F6F6F6"></AlternatingItemStyle>
								<ItemStyle Font-Size="Smaller" Font-Names="Microsoft Sans Serif" ForeColor="Black" BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#000084"></HeaderStyle>
								<Columns>
									<asp:TemplateColumn>
										<HeaderStyle Width="3%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:CheckBox id="chkItemChecked" runat="server"></asp:CheckBox>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="No">
										<HeaderStyle Width="3%" CssClass="titleTableService"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblNo" runat="server" Width="35px" Font-Size="Smaller"></asp:Label>
											<asp:TextBox Runat="server" ID="txtID" Width="0px" style="visibility:hidden;" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
											</asp:TextBox>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="ID" SortExpression="id" ReadOnly="True" HeaderText="No. Reg">
										<HeaderStyle Width="3%" CssClass="titleTableService"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn SortExpression="Name" HeaderText="Nama">
										<HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id="lblName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Name") %>'>
											</asp:Label>
											<asp:TextBox id="txtName" Visible="False" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Name") %>'>
											</asp:TextBox>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="StartWorkingDate" SortExpression="StartWorkingDate" ReadOnly="True" HeaderText="Mulai Bekerja"
										DataFormatString="{0:dd/MM/yyyy}">
										<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="JobPosition" SortExpression="JobPosition" ReadOnly="True" HeaderText="Posisi">
										<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn SortExpression="IsTraineeRegistered" HeaderText="Status">
										<HeaderStyle Width="5%" CssClass="titleTableService"></HeaderStyle>
										<ItemTemplate>
											<asp:Label id=Label2 runat="server" Text='<%# IIf(DataBinder.Eval(Container, "DataItem.IsTraineeRegistered"), "Terdaftar", "Belum Terdaftar") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn Visible="False" DataField="IsTraineeRegistered" ReadOnly="True">
										<HeaderStyle Width="10%" CssClass="titleTableService"></HeaderStyle>
									</asp:BoundColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></div>
					</TD>
				</TR>
				<TR>
					<TD align="center"><asp:button id="btnBack" runat="server" Width="80px" Text="Kembali" Font-Bold="True"></asp:button><asp:button id="btnDaftar" runat="server" Width="80px" Text="Daftar" Font-Bold="True"></asp:button></TD>
				</TR>
			</TABLE>
		</form>
		<script language="javascript">
			if (window.parent==window)
			{
				if (!navigator.appName=="Microsoft Internet Explorer")
				{
				  self.opener = null;
				  self.close();
				}
				else
				{
				   this.name = "origWin";
				   origWin= window.open(window.location, "origWin");
				   window.opener = top;
                   window.close();
				}
			}	
		</script>
	</body>
</HTML>
