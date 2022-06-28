Imports System.IO
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Service
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Public Class Test
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents TextBox1 As System.Web.UI.WebControls.TextBox
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents Button1 As System.Web.UI.WebControls.Button
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents TextBox2 As System.Web.UI.WebControls.TextBox
    Protected WithEvents Button2 As System.Web.UI.WebControls.Button
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents TextBox3 As System.Web.UI.WebControls.TextBox
    Protected WithEvents Button3 As System.Web.UI.WebControls.Button

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim stream As StreamReader
        Dim val As String
        Dim vt As Ktb.DNet.Domain.VechileType
        Dim coll As ArrayList = New ArrayList
        stream = New StreamReader(TextBox1.Text, True)
        val = NextLine(stream).Trim()
        While (Not val = "")
            vt = New Ktb.DNet.Domain.VechileType
            Dim str() As String = val.Split(";")
            vt.VechileTypeCode = str(2)
            vt.VechileModel = GetVehicleModel(str(4))
            vt.Category = GetCategory(str(6))
            vt.Description = str(3)
            vt.Status = "A"
            coll.Add(vt)
            val = NextLine(stream).Trim()
        End While
        InsertIntoDB(coll)
    End Sub

    Private Sub InsertIntoDB(ByVal coll As ArrayList)
        Dim facade As VechileTypeFacade
        For Each item As VechileType In coll
            facade = New VechileTypeFacade(User)
            If GetVehicleType(item.VechileTypeCode).ID > 0 Then
                Dim vt As VechileType = GetVehicleType(item.VechileTypeCode)
                vt.VechileTypeCode = item.VechileTypeCode
                vt.VechileModel = item.VechileModel
                vt.Category = item.Category
                vt.Description = item.Description
                vt.Status = "A"
                facade.Update(vt)
            Else
                facade.Insert(item)
            End If
        Next
    End Sub

    Private Function GetVehicleType(ByVal code As String) As VechileType
        Dim facade As VechileTypeFacade = New VechileTypeFacade(User)
        Return facade.Retrieve(code)
     End Function

    Private Function NextLine(ByVal stream As StreamReader)
        Dim stemp As Integer = stream.Read
        Dim sReturn = ""
        While (Not (stemp = -1) And (Not stemp = 10)) 'char 10 = /n
            Dim str As String = stemp.ToString
            'If (stemp = 91) Then 'Asc("[")
            '    stemp = 34 'Asc(\")
            'End If
            'If (stemp = 93) Then 'Asc("]")
            '    stemp = 34 'Asc(\")
            'End If
            sReturn += ChrW(stemp)
            stemp = stream.Read
        End While
        Dim strx As String = sReturn.ToString.Trim
        strx = strx.Replace("""", "''")
        Return strx
    End Function

    Private Function GetVehicleModel(ByVal code As String) As VechileModel
        Dim facade As VechileModelFacade = New VechileModelFacade(User)
        Return facade.Retrieve(code)
    End Function

    Private Function GetCategory(ByVal code As String) As Category
        Dim facade As CategoryFacade = New CategoryFacade(User)
        Return facade.Retrieve(code)
    End Function

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim stream As StreamReader
        Dim val As String
        Dim vc As VechileColor
        Dim coll As ArrayList = New ArrayList
        stream = New StreamReader(TextBox2.Text, True)
        val = NextLine(stream).Trim()
        While (Not val = "")
            vc = New VechileColor
            Dim str() As String = val.Split(";")
            vc.VechileType = GetVehicleType(str(2))
            vc.MaterialNumber = str(0)
            vc.MaterialDescription = str(5) & " " & str(3)
            vc.ColorCode = str(0).Substring(4, 4)
            vc.ColorEngName = str(0).Substring(4, 4)
            vc.ColorIndName = str(0).Substring(4, 4)
            coll.Add(vc)
            val = NextLine(stream).Trim()
        End While
        InsertColorIntoDB(coll)
    End Sub

    Private Sub InsertColorIntoDB(ByVal coll As ArrayList)
        Dim facade As VechileColorFacade
        For Each item As VechileColor In coll
            facade = New VechileColorFacade(User)
            If GetVehicleColor(item.MaterialNumber).ID > 0 Then
                'facade.Update(item)
            Else
                facade.Insert(item)
            End If
        Next
    End Sub

    Private Function GetVehicleColor(ByVal code As String) As VechileColor
        Dim facade As VechileColorFacade = New VechileColorFacade(User)
        Return facade.RetrieveByMaterialNumber(code)
    End Function

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Dim stream As StreamReader
        Dim val As String
        Dim lb As LaborMaster
        Dim coll As ArrayList = New ArrayList
        stream = New StreamReader(TextBox3.Text, True)
        val = NextLine(stream).Trim()
        While (Not val = "")
            lb = New LaborMaster
            Dim str() As String = val.Split(",")
            Dim vt As VechileType = GetVehicleType(str(0))
            lb.LaborCode = str(1)
            lb.WorkCode = str(2)
            If vt.ID > 0 Then
                lb.VechileType = vt
                coll.Add(lb)
            End If
            val = NextLine(stream).Trim()
        End While
        InsertLaborIntoDB(coll)
    End Sub

    Private Sub InsertLaborIntoDB(ByVal coll As ArrayList)
        Dim facade As LaborMasterFacade
        Dim labor As LaborMaster
        For Each item As LaborMaster In coll
            facade = New LaborMasterFacade(User)
            labor = GetLaborMaster(item)
            If labor.ID > 0 Then
                labor.LaborCode = item.LaborCode
                labor.WorkCode = item.WorkCode
                labor.VechileType = item.VechileType
                facade.Update(labor)
            Else
                facade.Insert(item)
            End If
        Next
    End Sub

    Private Function GetLaborMaster(ByVal obj As LaborMaster) As LaborMaster
        Dim criterias As New CriteriaComposite(New Criteria(GetType(Ktb.DNet.Domain.LaborMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(Ktb.DNet.Domain.LaborMaster), "VechileType.VechileTypeCode", MatchType.Exact, obj.VechileType.VechileTypeCode))
        criterias.opAnd(New Criteria(GetType(Ktb.DNet.Domain.LaborMaster), "WorkCode", MatchType.Exact, obj.WorkCode))
        criterias.opAnd(New Criteria(GetType(Ktb.DNet.Domain.LaborMaster), "LaborCode", MatchType.Exact, obj.LaborCode))

        Dim facade As LaborMasterFacade = New LaborMasterFacade(User)
        Dim coll As ArrayList = facade.Retrieve(criterias)
        If coll.Count > 0 Then
            Return CType(coll(0), LaborMaster)
        Else
            Return New LaborMaster
        End If

    End Function
End Class
