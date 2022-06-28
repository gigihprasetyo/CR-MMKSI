#Region ".NET Base Class Namespace Imports"
Imports System.Data.Odbc
Imports System.Security.Principal
Imports System.Text
Imports System
Imports System.Data
Imports System.Collections
Imports System.Text.RegularExpressions
Imports System.IO
Imports System.Data.OleDb
Imports System.Globalization
#End Region

#Region "Custom Namespace"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade

#End Region

Namespace KTB.DNet.Parser

    Public Class UploadRecallServiceParser
        Inherits AbstractExcelParser

#Region "Private Variables"
        Private RecallService As RecallService
        Private RecallChassisMaster As RecallChassisMaster
        Private UserChassisMaster As ChassisMaster
        Private _RecallService As ArrayList
        Private ContentFileType As String
        Private ErrorMessage As StringBuilder
        Private IsDataValid As Boolean
        Private CompanyCode As String
#End Region

#Region "Protected Methods"

        Sub New(Optional ByVal contentFileType As String = "", Optional ByVal companyCode As String = "")
            Me.ContentFileType = contentFileType
            Me.CompanyCode = companyCode
        End Sub

        Public Function GetErrorMessage() As String
            Return Me.ErrorMessage.ToString
        End Function

        Public Function IsAllDataValid() As Boolean
            Return IsDataValid
        End Function

        ''' <summary>
        ''' Parse With No Trans to Obejct
        ''' </summary>
        ''' <param name="fileName">Nama filenya</param>
        ''' <param name="sheetName">Nama Sheet</param>
        ''' <param name="user">user Name</param>
        ''' <returns>Object Data Customer list</returns>
        ''' <remarks>Pake Method Baru</remarks>
        Protected Overrides Function ParsingExcelNoTransaction(ByVal fileName As String, ByVal sheetName As String, ByVal user As String) As Object
            Me.ErrorMessage = New StringBuilder
            _RecallService = New ArrayList  '-- List of Material Promotion 
            IsDataValid = True
            Try
                AbstractExcelParser.ContentTypeExcel = Me.ContentFileType
                Dim Ds As DataSet = ParseExcelDataSet(fileName, sheetName, "")
                If IsNothing(Ds) Then
                    Me.ErrorMessage.Append("Pastikan nama worksheetnya adalah Sheet1.")
                Else

                    'Validasi Column
                    Dim critCol As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(RecallService), "RowStatus", MatchType.Exact, 0))
                    Dim sortCol As SortCollection = New SortCollection
                    sortCol.Add(New Sort(GetType(RecallService), "ID", Sort.SortDirection.ASC))

                    If Ds.Tables(0).Columns.Count <> 7 Then
                        Me.ErrorMessage.Append("Jumlah Kolom tidak sesuai. ")
                        Throw New Exception("Jumlah Kolom tidak sesuai. ")
                    End If

                    Dim colNames As String() = New String() {}
                    Dim iCol As Integer = 0
                    Dim dt As DataTable = Ds.Tables(0)
                    'ReDim Preserve colNames(dt.Columns.Count - 1)
                    ReDim Preserve colNames(7)

                    For Each dc As DataColumn In dt.Columns
                        If iCol < 7 Then
                            colNames(iCol) = dc.ColumnName
                            iCol = iCol + 1
                        End If
                    Next

                    If Me.ErrorMessage.Length > 0 Then
                        IsDataValid = False
                        Return _RecallService
                        Exit Function
                    End If

                    Dim row As DataRow
                    Dim i As Integer = 0
                    For i = 0 To Ds.Tables(0).Rows.Count - 1
                        row = Ds.Tables(0).Rows(i)
                        Try
                            'If Not IsDBNull(row(0)) Then
                            RecallService = New RecallService
                            If IsDBNull(row(0)) Then
                                RecallService.ChassisNumber = ""
                            Else
                                RecallService.ChassisNumber = row(0).ToString
                            End If
                            If Not IsDBNull(row(1)) Then
                                If Not IsNumeric(row(1)) Then
                                    RecallService.MileAge = 0
                                Else
                                    RecallService.MileAge = row(1)
                                End If
                            End If
                            If Not IsDBNull(row(2)) Then
                                If Not IsValidDate(row(2)) Then
                                    RecallService.ServiceDate = New Date(1900, 1, 1)
                                Else
                                    RecallService.ServiceDate = row(2).ToString
                                End If
                            End If
                            If Not IsDBNull(row(3)) Then
                                If Not IsValidDate(row(3)) Then
                                    RecallService.CreatedTime = New Date(1900, 1, 1)
                                Else
                                    RecallService.CreatedTime = row(3).ToString
                                End If
                            End If
                            If Not IsDBNull(row(4)) Then
                                RecallService.ServiceDealerID = row(4).ToString
                            End If
                            If Not IsDBNull(row(5)) Then
                                RecallService.RecallRegNo = row(5).ToString
                            End If
                            If Not IsDBNull(row(6)) Then
                                RecallService.WorkOrderNumber = row(6).ToString
                            End If

                            If Not IsDBNull(row(0)) Then
                                RecallService.RecallChassisMaster = New RecallChassisMasterFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(row(0).ToString)
                                'If Not IsNothing(RecallService.RecallChassisMaster) Then
                                '    RecallService.RecallChassisMaster.RecallCategory = New RecallCategoryFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(row(5).ToString)
                                'End If
                                If Not IsDBNull(row(4)) Then
                                    If IsNumeric(row(4)) Then
                                        RecallService.Dealer = New DealerFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(row(4).ToString)
                                    End If
                                End If
                                'RecallService.ChassisMaster = New ChassisMasterFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(row(0).ToString)
                                'RecallService.ChassisMasterBB = New ChassisMasterBBFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(row(0).ToString)
                                
                            End If
                            If IsDBNull(row(0)) AndAlso IsDBNull(row(1)) AndAlso IsDBNull(row(2)) AndAlso IsDBNull(row(3)) AndAlso IsDBNull(row(4)) AndAlso IsDBNull(row(5)) AndAlso IsDBNull(row(6)) Then
                            Else
                                RecallService = ParseRecallService(row)
                                Dim cMaster As ChassisMaster = New ChassisMasterFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(row(0).ToString)
                                Dim cMasterBB As ChassisMasterBB = New ChassisMasterBBFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(row(0).ToString)

                                If cMaster.ID <> 0 Then
                                    RecallService.ChassisID = cMaster.ID
                                    RecallService.ChassisMaster = cMaster
                                ElseIf cMasterBB.ID <> 0 Then
                                    RecallService.ChassisBBID = cMasterBB.ID
                                    RecallService.ChassisMasterBB = cMasterBB
                                End If
                                _RecallService.Add(RecallService)
                            End If
                            
                            
                        Catch ex As Exception
                            Me.ErrorMessage.Append(ex.Message)
                        End Try
                    Next
                End If
            Catch ex As Exception
                Me.ErrorMessage.Append(ex.Message)
            End Try
            Return _RecallService
        End Function


        Private Function IsValidColumn(ByVal colName As String, ByVal mapCols As ArrayList) As Boolean
            Dim isValid As Boolean = False

            For i As Integer = 0 To mapCols.Count - 1
                If InStr(colName, mapCols(i).ColTitle, CompareMethod.Text) > 0 Then
                    isValid = True
                    Exit For
                End If
            Next

            Return isValid

        End Function

        Private Function ParseRecallService(ByVal row As DataRow) As RecallService
            Dim objRCM As RecallChassisMaster
            Dim ArrUpload As New ArrayList
            Dim ObjRS As New RecallService
            'Dim objChassisMaster As ChassisMaster
            Me.ErrorMessage = New StringBuilder
            ErrorMessage.Clear()

            If Not IsDBNull(row(0)) AndAlso Not IsDBNull(row(5)) Then
                Try
                    Dim strRCM As String = CType(row(0), String)
                    Dim strRCNo As String = CType(row(5), String)
                    'Dim criterias As New CriteriaComposite(New Criteria(GetType(RecallChassisMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    'criterias.opAnd(New Criteria(GetType(RecallChassisMaster), "RecallCategory.RecallRegNo", MatchType.[Partial], txtRecallRegNo.Text.Trim()))
                    objRCM = New RecallChassisMasterFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(strRCM, strRCNo)
                    'check data recallchassismaster
                    If objRCM.ID <> Nothing Then
                        ObjRS = New RecallServiceFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).RetrieveByRM(objRCM.ID) 'ObjFRCS.RetrieveByRM(objRCM.ID) '----- check recallservice
                        'check data recallservice
                        If Not IsNothing(ObjRS) And ObjRS.ID > 0 Then
                            RecallService.RecallChassisMaster = objRCM
                            If ErrorMessage.ToString = "OK" Then
                                ErrorMessage.Clear()
                            End If
                            ErrorMessage.Append("Data Recall Service sudah ada")
                            If RecallService.ErrorMessage = "" Then
                                RecallService.ErrorMessage = "Data Recall Service sudah ada"
                            Else
                                RecallService.ErrorMessage = RecallService.ErrorMessage + "; Data Recall Service sudah ada"
                            End If
                        Else
                            RecallService.RecallChassisMaster = objRCM
                            If ErrorMessage.ToString = "OK" Then
                                ErrorMessage.Clear()
                            End If
                            ErrorMessage.Append("OK")
                            'RecallService.ErrorMessage = "OK"
                        End If

                    Else
                        If ErrorMessage.ToString = "OK" Then
                            ErrorMessage.Clear()
                        End If
                        ErrorMessage.Append("Recall Chassis Master belum terdaftar")
                        RecallService.RecallChassisMaster = Nothing
                        If RecallService.ErrorMessage = "" Then
                            RecallService.ErrorMessage = "Recall Chassis Master belum terdaftar"
                        Else
                            RecallService.ErrorMessage = RecallService.ErrorMessage + "; Recall Chassis Master belum terdaftar"
                        End If
                    End If

                    'check Dealer
                    Dim strDLR As String = CType(row(4), String)
                    Dim ObjDLR As New Dealer
                    ObjDLR = New DealerFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(strDLR)
                    If ObjDLR.ID <= 0 Or IsNothing(ObjDLR) Then
                        If ErrorMessage.ToString = "OK" Then
                            ErrorMessage.Clear()
                        End If
                        ErrorMessage.Append("Data Dealer tidak ada")
                        If RecallService.ErrorMessage = "" Then
                            RecallService.ErrorMessage = "Data Dealer tidak ada"
                        Else
                            RecallService.ErrorMessage = RecallService.ErrorMessage + "; Data Dealer tidak ada"
                        End If
                    End If
                    'check recallcategory
                    Dim strRSC As String = CType(row(5), String)
                    Dim ObjRSC As New RecallCategory
                    ObjRSC = New RecallCategoryFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(strRSC)
                    If ObjRSC.ID <= 0 Or IsNothing(ObjRSC) Then
                        If ErrorMessage.ToString = "OK" Then
                            ErrorMessage.Clear()
                        End If
                        ErrorMessage.Append("Recall Category No tidak ada")
                        If RecallService.ErrorMessage = "" Then
                            RecallService.ErrorMessage = "Recall Category No tidak ada"
                        Else
                            RecallService.ErrorMessage = RecallService.ErrorMessage + "; Recall Category No tidak ada"
                        End If
                    End If

                Catch ex As Exception
                    If ErrorMessage.ToString = "OK" Then
                        ErrorMessage.Clear()
                    End If
                    ErrorMessage.Append("Recall Chassis Master belum terdaftar")
                End Try
            Else
                If ErrorMessage.ToString = "OK" Then
                    ErrorMessage.Clear()
                End If
                If IsDBNull(row(0)) Then
                    If RecallService.ErrorMessage = "" Then
                        RecallService.ErrorMessage = "Chassis No tidak boleh kosong"
                    Else
                        RecallService.ErrorMessage = RecallService.ErrorMessage + "; Chassis No tidak boleh kosong"
                    End If
                End If
                ErrorMessage.Append("Recall Category No belum terdaftar")
            End If

            If Not IsDBNull(row(1)) Then
                If Not IsNumeric(row(1)) Then
                    If RecallService.ErrorMessage = "" Then
                        RecallService.ErrorMessage = "MileAge harus numerik dan tidak boleh 0"
                    Else
                        RecallService.ErrorMessage = RecallService.ErrorMessage + "; MileAge harus numerik dan tidak boleh 0"
                    End If
                End If
            Else
                If RecallService.ErrorMessage = "" Then
                    RecallService.ErrorMessage = "MileAge harus numerik dan tidak boleh 0 "
                Else
                    RecallService.ErrorMessage = RecallService.ErrorMessage + "; MileAge harus numerik dan tidak boleh 0"
                End If
            End If

            If Not IsDBNull(row(2)) Then
                If Not IsValidDate(row(2)) Then
                    If RecallService.ErrorMessage = "" Then
                        RecallService.ErrorMessage = "Format Service Date salah"
                    Else
                        RecallService.ErrorMessage = RecallService.ErrorMessage + "; Format Service Date salah"
                    End If
                End If
            Else
                If RecallService.ErrorMessage = "" Then
                    RecallService.ErrorMessage = "Service Date tidak boleh kosong"
                Else
                    RecallService.ErrorMessage = RecallService.ErrorMessage + "; Service Date tidak boleh kosong"
                End If
            End If

            If Not IsDBNull(row(3)) Then
                If Not IsValidDate(row(3)) Then
                    If RecallService.ErrorMessage = "" Then
                        RecallService.ErrorMessage = "Format Input Date salah"
                    Else
                        RecallService.ErrorMessage = RecallService.ErrorMessage + "; Format Input Date salah"
                    End If
                End If
            Else
                If RecallService.ErrorMessage = "" Then
                    RecallService.ErrorMessage = "Input Date tidak boleh kosong"
                Else
                    RecallService.ErrorMessage = RecallService.ErrorMessage + "; Input Date tidak boleh kosong"
                End If
            End If

            If IsDBNull(row(4)) Then
                If RecallService.ErrorMessage = "" Then
                    RecallService.ErrorMessage = "Service Dealer tidak boleh kosong"
                Else
                    RecallService.ErrorMessage = RecallService.ErrorMessage + "; Service Dealer tidak boleh kosong"
                End If
            End If
            If IsDBNull(row(5)) Then
                If RecallService.ErrorMessage = "" Then
                    RecallService.ErrorMessage = "Recall Category No tidak boleh kosong"
                Else
                    RecallService.ErrorMessage = RecallService.ErrorMessage + "; Recall Category No tidak boleh kosong"
                End If
            End If

            If ErrorMessage.Length > 0 Then
                'RecallService.ErrorMessage = ErrorMessage.ToString
                If IsDataValid = True Then
                    IsDataValid = False
                End If
            End If

            If IsNothing(RecallService.ErrorMessage) Then
                RecallService.ErrorMessage = "OK"
            End If

            Return RecallService
        End Function

        Private Function IsValidDate(ByVal strdate As String) As Boolean
            'Dim strtgl As String = strdate.Substring(2, 2).ToString & "-" & strdate.Substring(0, 2) & "-" & strdate.Substring(4, 4)
            'yang jadi dipakai adalah setting tanggal indonesia
            If Not strdate.Trim = "" AndAlso strdate.Length > 4 Then
                Dim strtgl As String = strdate.Substring(0, 2).ToString & "" & strdate.Substring(2, 2) & "" & strdate.Substring(4, 4)
                If IsDate(strtgl) Then
                    Return True
                Else
                    Return False
                End If
            Else
                Return False
            End If

        End Function

#End Region

    End Class

End Namespace
