<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FrmEventFinishing.aspx.vb" Inherits="FrmEventFinishing" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FrmEventFinishing</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/PreventNewWindow.js"></script>
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<script language="javascript" src="../WebResources/FormFunctions.js"></script>
		<script language="javascript">
            function calcpercen()
            {
                var lblInviteNumber = document.getElementById("lblInviteNumber").innerText;
                if (lblInviteNumber == "0") return;
                
                var txtInvitation = document.getElementById("txtInvitation").value;
                if (txtInvitation == "") 
                {
                    txtInvitation = "0";
                    document.getElementById("txtInvitation").value = "0"
                }
                if (txtInvitation == "0") return;
                
                var percen = (parseFloat(txtInvitation) / parseFloat(lblInviteNumber)) * 100;
                var lblPercentage = document.getElementById("lblPercentage");
                var roundpercen = Math.round(percen * 100) / 100;
                lblPercentage.innerText = "Persentase : " + roundpercen + "%";
            }
            
            function calcpercen2()
            {
                var txtOwner = document.getElementById("txtOwner").value;
                if (txtOwner == "") 
                {
                    document.getElementById("txtOwner").value = "0";
                    txtOwner = "0";
                }
                
                var txtDriver = document.getElementById("txtDriver").value;
                if (txtDriver == "") 
                {
                    document.getElementById("txtDriver").value = "0";
                    txtDriver = "0";
                }
                
                var txtInvitation = document.getElementById("txtInvitation");
                
                txtInvitation.value = parseInt(txtOwner) + parseInt(txtDriver);
                calcpercen();                
            }
            
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="1" cellPadding="1" border="0">
				<TR>
					<td colspan="3" class="titlePage" style="HEIGHT: 31px">
						<asp:Label Runat="server" ID="lblTitle" Text="Event - Penyelesaian Dealer"></asp:Label>
					</td>
					<TD class="titlePage" style="HEIGHT: 31px"></TD>
					<TD class="titlePage" style="HEIGHT: 31px"></TD>
					<TD class="titlePage" style="HEIGHT: 31px"></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 171px">Nama Kegiatan</TD>
					<TD style="WIDTH: 3px">:</TD>
					<TD>
						<asp:Label id="lblEventName" runat="server"></asp:Label></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 171px">Tanggal Acara</TD>
					<TD style="WIDTH: 3px">:</TD>
					<TD>
						<asp:Label id="lblEventDate" runat="server"></asp:Label></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 171px">Kota</TD>
					<TD style="WIDTH: 3px">:</TD>
					<TD>
						<asp:Label id="lblEventCity" runat="server"></asp:Label></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 171px">Tempat Acara</TD>
					<TD style="WIDTH: 3px">:</TD>
					<TD>
						<asp:Label id="lblEventPlace" runat="server"></asp:Label></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 171px">Jumlah Undangan</TD>
					<TD style="WIDTH: 3px">:</TD>
					<TD>
						<asp:Label id="lblInviteNumber" runat="server"></asp:Label></TD>
					<TD>
						<asp:Label id="lblOwnerInfo" runat="server">Owner</asp:Label></TD>
					<TD>
						<asp:Label id="lblDotOwner" runat="server">:</asp:Label></TD>
					<TD>
						<asp:TextBox id="txtOwner" runat="server" Columns="5" MaxLength="5" onkeypress="return numericOnlyUniv(event);"
							onblur="calcpercen2();">0</asp:TextBox>
					</TD>
				</TR>
				<TR>
					<TD style="WIDTH: 171px">
						<asp:Label Runat="server" ID="lblUndanganKTB" Text="Undangan Yang Hadir"></asp:Label>
					</TD>
					<TD style="WIDTH: 3px">
						<asp:Label Runat="server" ID="lblDotKTB" Text=":"></asp:Label>
					</TD>
					<TD>
						<asp:TextBox id="txtInvitation" runat="server" Columns="5" onkeypress="return numericOnlyUniv(event);"
							onblur="calcpercen();">0</asp:TextBox>
						<asp:Label id="lblPercentage" runat="server" Text="Persentase : 0%"></asp:Label></TD>
					<TD>
						<asp:Label id="lblDriverInfo" runat="server" Text="Driver/Co Driver"></asp:Label></TD>
					<TD>
						<asp:Label id="lblDotDriver" runat="server">:</asp:Label></TD>
					<TD>
						<asp:TextBox id="txtDriver" runat="server" Columns="5" MaxLength="5" onkeypress="return numericOnlyUniv(event);"
							onblur="calcpercen2();">0</asp:TextBox>
					</TD>
				</TR>
			</TABLE>
			<table>
				<TR>
					<TD>
						<asp:DataGrid id="dtgUpload" runat="server" AutoGenerateColumns="False" GridLines="None" ShowHeader="False">
							<Columns>
								<asp:BoundColumn DataField="ID" Visible="False"></asp:BoundColumn>
								<asp:TemplateColumn ItemStyle-Width="170px">
									<ItemTemplate>
										<asp:Label Runat="server" ID="lblInfo" text="<b>Upload</b> Foto <b>Truck Campaign</b>"></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn ItemStyle-Width="4px">
									<ItemTemplate>
										<asp:Label Runat="server" ID="Label1" text=":"></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn>
									<ItemTemplate>
										<input type="file" runat="server" id="fu" name="fu" style="width:300px" />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn>
									<ItemTemplate>
										<asp:Button Runat="server" ID="btnUpload" Text="Upload" CommandName="upload" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.ID") %>'>
										</asp:Button>
										<asp:Label Runat="server" ID="lblFileName" text='<%# DataBinder.Eval(Container, "DataItem.FileName") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
						</asp:DataGrid>
					</TD>
				</TR>
				<TR>
					<TD align="center">
						<asp:Button id="btnSave" runat="server" Text="Simpan"></asp:Button>
						<asp:Button id="btnValidate" runat="server" Text="Validasi"></asp:Button>
						<INPUT id="btnBack" onclick="window.history.back();return false;" type="button" value="Kembali"
							name="btnBack" runat="server">
					</TD>
				</TR>
			</table>
			<INPUT id="hdnSave" type="hidden" runat="server" name="hdnSave"> <INPUT id="hdnValidate" type="hidden" runat="server" name="hdnValidate">
		</form>
	</body>
</HTML>
