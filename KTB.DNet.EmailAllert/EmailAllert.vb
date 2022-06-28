Imports System
Imports KTB.DNet.Utility
Imports System.Configuration
Imports System.Web
Imports System.Text

Module Module1
    Sub Main()
        SendingEmail()
    End Sub

    Private Sub SendingEmail()
        Console.WriteLine("Start Email Service............")
        Dim smtp As String = ConfigurationSettings.AppSettings.Item("SMTP")
        Dim sender As String = ConfigurationSettings.AppSettings.Item("Sender")
        Dim rec As String = ConfigurationSettings.AppSettings.Item("Receiver")
        Dim subject As String = ConfigurationSettings.AppSettings.Item("Subject")
        Dim body As String = BuildEmailBody()
        Dim _DetMail As DNetMail = New DNetMail(smtp)
        Try
            _DetMail.Priority = Mail.MailPriority.High
            _DetMail.sendMail(rec, sender, sender, subject, Mail.MailFormat.Html, body)
            Console.WriteLine("Succesfuly Sending Email ............")
        Catch ex As Exception
            Console.WriteLine("Error Sending Email ........ " & ex.Message.ToString)
            Console.ReadLine()
        End Try
    End Sub


    Private Function BuildEmailBody() As String
        Dim sb As StringBuilder = New StringBuilder

        sb.Append("<HTML>")
        sb.Append("<TABLE   cellSpacing=1 cellPadding=1 width=528   border=0 ")
        sb.Append("height=193 ")
        sb.Append("<TR>")
        sb.Append("<TD height=28>")
        sb.Append("<DIV ms_positioning=FlowLayout>")
        sb.Append("<TABLE height=24 cellSpacing=0 cellPadding=0 width=160 border=0 >")
        sb.Append("<TR>")
        sb.Append("<TD>")
        sb.Append("<b>DNet Team</b>")
        sb.Append("</TD>")
        sb.Append("</TR>")
        sb.Append("</TABLE>")
        sb.Append("</DIV>")
        sb.Append("</TD>")
        sb.Append("</TR>")
        sb.Append("<TR>")
        sb.Append("<TD></TD>")
        sb.Append("</TR>")
        sb.Append("<TR>")
        sb.Append("<TD>")
        sb.Append("<DIV ms_positioning=FlowLayout>")
        sb.Append("<TABLE height=36 cellSpacing=0 cellPadding=0 width=480 border=0 >")
        sb.Append("<TR>")
        sb.Append("<TD>Error Occured on SAP Service/ SAP Service Fail&nbsp;on : " & Now)
        sb.Append("</TD>")
        sb.Append("</TR>")
        sb.Append("</TABLE>")
        sb.Append("</DIV>")
        sb.Append("</TD>")
        sb.Append("</TR>")
        sb.Append("<TR>")
        sb.Append("<TD height=60>")
        sb.Append("<DIV ms_positioning=FlowLayout>")
        sb.Append("	<TABLE height=54 cellSpacing=0 cellPadding=0 width=512 border=0>")
        sb.Append("<TR>")
        sb.Append("	<TD>Please Check the Service and Event Log 'Application' and try to start using ")
        sb.Append("					Windows Service Console &nbsp;or SAP Service Manager")
        sb.Append("				</TD>")
        sb.Append("			</TR>")
        sb.Append("		</TABLE>")
        sb.Append("		</DIV>")
        sb.Append("	</TD>")
        sb.Append("	</TR>")
        sb.Append("	<TR>")
        sb.Append("		<TD></TD>")
        sb.Append("	</TR>")
        sb.Append("	<TR>")
        sb.Append("		<TD>")
        sb.Append("	<DIV ms_positioning=FlowLayout>")
        sb.Append("	<TABLE height=24 cellSpacing=0 cellPadding=0 width=144 border=0 >")
        sb.Append("					<TR>")
        sb.Append("						<TD>")
        sb.Append("							<b>DNet System</b>")
        sb.Append("						</TD>")
        sb.Append("					</TR>")
        sb.Append("				</TABLE>")
        sb.Append("			</DIV>")
        sb.Append("		</TD>")
        sb.Append("	</TR>")
        sb.Append("</TABLE>")
        sb.Append("</HTML>")
        Return sb.ToString
    End Function
End Module
