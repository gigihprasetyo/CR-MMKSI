<%@ Control Language="vb" AutoEventWireup="false" Codebehind="TraineeNotify.ascx.vb" Inherits="TraineeNotify" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<asp:Table ID="tblNotify" Runat="server">
	<asp:TableRow>
		<asp:TableCell Runat="server" HorizontalAlign="Center">
			<p>
				<span style='font-size:10.0pt;font-family:Tahoma;mso-fareast-font-family:"Times New Roman";
mso-ansi-language:EN-US;mso-fareast-language:EN-US;mso-bidi-language:AR-SA'>Apakah data status 
					siswa per 
					<asp:Label ID="lblDate" Runat="server"></asp:Label> 
					valid ?</span>
			</p>
		</asp:TableCell>
	</asp:TableRow>
	<asp:TableRow HorizontalAlign="Center">
		<asp:TableCell>
			<asp:Button id="btnYes" Text="Ya" runat="server" Width="80px"></asp:Button>
			<asp:Button id="btnNo" Text="Tidak" runat="server" Width="80px"></asp:Button>
		</asp:TableCell>
	</asp:TableRow>
</asp:Table>
