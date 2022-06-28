#Region "Code Disclaimer"
'Copyright PT. Puspa Intimedia Internusa (Intimedia) 2005

'Intimedia grants you the right to use and modify the code in this Persistence Framework
'(code under the Framework Namespace) but 
'(i) only for the solutions that are developed by Intimedia for you 
'(ii) or in solutions that are developed in join development between you and Intimedia.

'All rights not expressly granted, are reserved.
#End Region

#Region "Summary"
'///////////////////////////////////////////////////////////////////////////////////////
'// Author Name: 
'// PURPOSE       : Enter summary here after generation.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright © 2005 
'// ---------------------
'// $History      : $
'// Generated on 8/11/2005 - 11:10:00 AM
'//
'// ===========================================================================		
#End Region

#Region ".NET Base Class Namespace Imports"

Imports System
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Reflection
Imports System.Configuration
Imports KTB.DNet.BusinessFacade.Salesman
Imports KTB.DNet.BusinessFacade.BusinessForum
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade.UserManagement
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.PK
Imports Ktb.DNet.BusinessFacade.PO
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.BusinessFacade.Training
Imports System.Security.Principal
Imports KTB.DNet.BusinessFacade.IndentPartEquipment
Imports Ktb.DNet.BusinessFacade.SparePart
'Imports KTB.DNet.DataMapper.Framework
'Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports System.IO
Imports System.Text
#End Region

Namespace KTB.DNet.Utility

    Public Class CommonFunction


        Public Shared Function IsInPeriodForFreezePK(ByVal user As System.Security.Principal.IPrincipal) As Boolean

            Dim ReferenceDate As Date = New Date(Today.Year, Today.Month, Date.DaysInMonth(Today.Year, Today.Month))
            Dim objFacade As New NationalHolidayFacade(user)

            Dim counter As Integer = 0
            Do Until counter = 5
                Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(NationalHoliday), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(NationalHoliday), "HolidayDateTime", MatchType.Exact, ReferenceDate))
                Dim arl As ArrayList = objFacade.Retrieve(criterias)

                If arl.Count = 0 Then
                    counter += 1
                End If

                ReferenceDate = ReferenceDate.AddDays(-1)

            Loop

            If Date.Today > ReferenceDate Then
                Return True
            Else
                Return False
            End If

        End Function

        Public Shared Function FormatSavedUser(ByVal sDatabaseUserFormat As String, ByVal user As System.Security.Principal.IPrincipal) As String
            Dim result As String
            Dim sID As String
            Dim sUser As String
            Try

                sID = sDatabaseUserFormat.Substring(0, 6)
                sUser = sDatabaseUserFormat.Substring(6)
                Dim oDealer As Dealer = New DealerFacade(user).Retrieve(CInt(sID))
                If Not oDealer Is Nothing Then
                    result = oDealer.DealerCode & " - " & sUser
                Else
                    result = sUser
                End If
                Return result
            Catch ex As Exception
                Return ""
            End Try
        End Function
        Public Shared Function GetPageBreakFCKeditor() As String
            '<div style="page-break-after: always"><span style="display: none">&nbsp;</span></div>
            Dim dblQuote As String = Chr(34).ToString
            Return "<div style=" & dblQuote & "page-break-after: always" & dblQuote & "><span style=" & dblQuote & "display: none" & dblQuote & ">&nbsp;</span></div>"
        End Function

        Public Shared Function PageAndSortArraylist(ByVal Arl As ArrayList, ByVal IdxPage As Integer, ByVal PageSize As Integer, ByVal ObjType As Type, ByVal SortColumn As String, ByVal SortDirection As Sort.SortDirection) As ArrayList

            Arl = SortArraylist(Arl, ObjType, SortColumn, SortDirection)
            Arl = PageArraylist(Arl, IdxPage, PageSize)
            Return Arl

        End Function

        Public Shared Function PageArraylist(ByVal ArlToPage As ArrayList, ByVal IdxPage As Integer, ByVal PageSize As Integer) As ArrayList

            Dim StartPoint As Integer = (IdxPage * PageSize)
            Dim RecordCount As Integer = 0
            If StartPoint + PageSize > ArlToPage.Count - 1 Then 'Last Page
                RecordCount = ArlToPage.Count - StartPoint
            Else
                RecordCount = PageSize
            End If
            ArlToPage = ArlToPage.GetRange(StartPoint, RecordCount)

            Return ArlToPage

        End Function
        Public Shared Function SortArraylist(ByVal ArlToSort As ArrayList, ByVal ObjType As Type, ByVal SortColumn As String, ByVal SortDirection As Sort.SortDirection) As ArrayList
            'Only support 1 Level Up
            'Dim isDeepSort As Boolean = (SortColumn.IndexOf(".") <> -1)

            'Dim i As Integer
            'Dim x, y As Object
            'Dim currentValue, prevValue As Object
            'For i = 0 To ArlToSort.Count - 1
            '    If i >= 1 Then
            '        If isDeepSort Then 'Only for 2 level max
            '            Dim Properties() As String = SortColumn.Split((".").ToCharArray())
            '            Dim dummyType As Type = ObjType
            '            Dim currentDummyObject As Object
            '            Dim prevDummyObject As Object

            '            For z As Integer = 0 To Properties.Length - 2
            '                If z = 0 Then
            '                    currentDummyObject = dummyType.GetProperty(Properties(z)).GetValue(ArlToSort(i), BindingFlags.GetProperty, Type.DefaultBinder, Nothing, Nothing)
            '                    prevDummyObject = dummyType.GetProperty(Properties(z)).GetValue(ArlToSort(i - 1), BindingFlags.GetProperty, Type.DefaultBinder, Nothing, Nothing)

            '                    dummyType = dummyType.GetProperty(Properties(z)).PropertyType

            '                Else
            '                    currentDummyObject = dummyType.GetProperty(Properties(z)).GetValue(currentDummyObject, BindingFlags.GetProperty, Type.DefaultBinder, Nothing, Nothing)
            '                    prevDummyObject = dummyType.GetProperty(Properties(z)).GetValue(prevDummyObject, BindingFlags.GetProperty, Type.DefaultBinder, Nothing, Nothing)

            '                    dummyType = dummyType.GetProperty(Properties(z)).PropertyType

            '                End If
            '            Next
            '            Dim prop As PropertyInfo
            '            prop = dummyType.GetProperty(Properties(Properties.Length - 1))

            '            currentValue = dummyType.GetProperty(Properties(Properties.Length - 1)).GetValue(currentDummyObject, BindingFlags.GetProperty, Type.DefaultBinder, Nothing, Nothing)
            '            prevValue = dummyType.GetProperty(Properties(Properties.Length - 1)).GetValue(prevDummyObject, BindingFlags.GetProperty, Type.DefaultBinder, Nothing, Nothing)
            '        Else
            '            currentValue = ObjType.GetProperty(SortColumn).GetValue(ArlToSort(i), BindingFlags.GetProperty, Type.DefaultBinder, Nothing, Nothing)
            '            prevValue = ObjType.GetProperty(SortColumn).GetValue(ArlToSort(i - 1), BindingFlags.GetProperty, Type.DefaultBinder, Nothing, Nothing)
            '        End If

            '        If SortDirection = Sort.SortDirection.ASC Then
            '            If currentValue < prevValue Then
            '                x = ArlToSort(i)
            '                y = ArlToSort(i - 1)
            '                ArlToSort(i) = y
            '                ArlToSort(i - 1) = x
            '                i = 0
            '            End If
            '        Else
            '            If currentValue > prevValue Then
            '                x = ArlToSort(i)
            '                y = ArlToSort(i - 1)
            '                ArlToSort(i) = y
            '                ArlToSort(i - 1) = x
            '                i = 0
            '            End If
            '        End If
            '    End If
            'Next

            Dim isASC As Boolean
            If SortDirection = Sort.SortDirection.ASC Then
                isASC = True
            Else
                isASC = False
            End If

            Dim objListComparer As IComparer = New ListComparer(isASC, SortColumn)
            ArlToSort.Sort(objListComparer)
            Return ArlToSort

        End Function

        Public Shared Function DistinctArraylist(ByVal ArlToDistinct As ArrayList, ByVal ObjType As Type, ByVal DistinctColumn As String) As ArrayList
            ' To use this function, data on the arraylist must already been sort based on DistinctColumn
            Dim isDeepDistinct As Boolean = (DistinctColumn.IndexOf(".") <> -1)

            Dim result As New ArrayList
            Dim _CurrentValue, _PrevValue As Object

            For i As Integer = 0 To ArlToDistinct.Count - 1
                If isDeepDistinct Then
                    Dim Properties() As String = DistinctColumn.Split((".").ToCharArray())
                    Dim DummyType As Type = ObjType
                    Dim DummyObject As Object

                    For z As Integer = 0 To Properties.Length - 2
                        If z = 0 Then
                            DummyObject = DummyType.GetProperty(Properties(z)).GetValue(ArlToDistinct(i), BindingFlags.GetProperty, Type.DefaultBinder, Nothing, Nothing)

                            DummyType = DummyType.GetProperty(Properties(z)).PropertyType

                        Else
                            DummyObject = DummyType.GetProperty(Properties(z)).GetValue(DummyObject, BindingFlags.GetProperty, Type.DefaultBinder, Nothing, Nothing)

                            DummyType = DummyType.GetProperty(Properties(z)).PropertyType

                        End If
                    Next
                    Dim prop As PropertyInfo
                    prop = DummyType.GetProperty(Properties(Properties.Length - 1))

                    _CurrentValue = DummyType.GetProperty(Properties(Properties.Length - 1)).GetValue(DummyObject, BindingFlags.GetProperty, Type.DefaultBinder, Nothing, Nothing)
                Else
                    _CurrentValue = ObjType.GetProperty(DistinctColumn).GetValue(ArlToDistinct(i), BindingFlags.GetProperty, Type.DefaultBinder, Nothing, Nothing)
                End If

                If i = 0 Then
                    '_CurrentValue = ObjType.GetProperty(DistinctColumn).GetValue(ArlToDistinct(i), BindingFlags.GetProperty, Type.DefaultBinder, Nothing, Nothing)
                    _PrevValue = _CurrentValue
                    result.Add(_CurrentValue)
                Else
                    '_CurrentValue = ObjType.GetProperty(DistinctColumn).GetValue(ArlToDistinct(i), BindingFlags.GetProperty, Type.DefaultBinder, Nothing, Nothing)
                    If _CurrentValue <> _PrevValue Then
                        _PrevValue = _CurrentValue
                        result.Add(_CurrentValue)
                    End If
                End If
            Next

            Return result
        End Function

        Public Shared Function IsNumeric(ByVal str As String) As Boolean

            If (str.Trim = "") Then
                Return False
            End If

            Dim ca() As Char = str.ToCharArray()

            For i As Integer = 0 To i < ca.Length

                If (Microsoft.VisualBasic.Asc(ca(i)) > 57) Or (Microsoft.VisualBasic.Asc(ca(i)) < 48) Then
                    If (Microsoft.VisualBasic.AscW(ca(i)) <> 46) Then
                        Return False
                    End If
                End If

            Next

            Return True

        End Function

        Public Shared Function PreventDoubleClick(ByVal TheButton As System.Web.UI.Control) As String
            TheButton.Page.GetPostBackClientHyperlink(TheButton, String.Empty)
            Dim bIsThereValidator As Boolean = False
            Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder

            If TheButton.Page.Validators.Count > 0 Then
                bIsThereValidator = True
            End If

            If bIsThereValidator Then
                sb.Append("if (typeof(Page_ClientValidate) == 'function') {")
                sb.Append("if (Page_ClientValidate()) { ")
            End If
            sb.Append("this.disabled = true;")
            sb.Append(TheButton.Page.GetPostBackEventReference(TheButton))
            sb.Append(";")
            If bIsThereValidator Then
                sb.Append("}}")
            End If
            Return sb.ToString()
        End Function

        Public Shared Function PreventDoubleClickAtGrid(ByVal TheLinkButton As System.Web.UI.Control, _
            ByVal ConfirmationMsg As String) As String
            TheLinkButton.Page.GetPostBackClientHyperlink(TheLinkButton, String.Empty)
            'assume CausesValidation of LinkButton always set to false, then ignore it checking all validator
            'Dim bIsThereValidator As Boolean = False
            Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder

            If ConfirmationMsg <> String.Empty Then
                sb.Append("if (! confirm('" & ConfirmationMsg & "')) { return false; } else {")
            End If

            'If TheLinkButton.Page.Validators.Count > 0 Then
            '    bIsThereValidator = True
            'End If
            'If bIsThereValidator Then
            '    'sb.Append("if (typeof(Page_ClientValidate) == 'function') {")
            '    'sb.Append("if (Page_ClientValidate()) { ")
            'End If

            sb.Append("this.style.visibility = 'hidden';")
            sb.Append(TheLinkButton.Page.GetPostBackEventReference(TheLinkButton))
            sb.Append(";")

            'If bIsThereValidator Then
            '    sb.Append("}}")
            'End If

            If ConfirmationMsg <> String.Empty Then
                sb.Append("}")
            End If

            Return sb.ToString()
        End Function

        Public Shared Function IsExistChassisNumberInLoginDealer(ByVal ChassisNumber As String, ByVal DealerID As String, ByVal user As System.Security.Principal.IPrincipal)
            Dim arlCM As New ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ChassisMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(ChassisMaster), "ChassisNumber", MatchType.Exact, ChassisNumber))
            criterias.opAnd(New Criteria(GetType(ChassisMaster), "Dealer.ID", MatchType.Exact, DealerID))
            arlCM = New FinishUnit.ChassisMasterFacade(user).Retrieve(criterias)
            If (arlCM.Count > 0) Then
                Return True
            End If
            Return False
        End Function

        Public Shared Function IsExistChassisNumberInStockMovement(ByVal ChassisNumber As String, ByVal DealerID As String, ByVal user As System.Security.Principal.IPrincipal)
            Dim arl As New ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(StockMovement), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(StockMovement), "ChassisMaster.ChassisNumber", MatchType.Exact, ChassisNumber))

            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Sort(GetType(KTB.DNet.Domain.StockMovement), "ProcessDate", Sort.SortDirection.DESC))

            arl = New DealerReport.StockMovementFacade(user).Retrieve(criterias, sortColl)
            If (arl.Count > 0) Then
                If (CType(arl(0), StockMovement).Dealer.ID = DealerID) Then
                    Return True
                Else
                    Return False
                End If
            End If
            Return False
        End Function
        Public Shared Function RetrieveList(ByVal Modul As String, ByVal arrStatus As ArrayList, ByVal user As System.Security.Principal.IPrincipal) As ArrayList
            Dim arl As ArrayList
            Dim collStatus As String = String.Empty
            Dim sessHelp As SessionHelper = New SessionHelper
            Dim objUser As UserInfo = CType(sessHelp.GetSession("LOGINUSERINFO"), UserInfo)
            Dim DealerID As Integer = objUser.Dealer.ID


            For Each objStatus As AlertStatus In arrStatus
                If collStatus.Length > 0 Then
                    collStatus += ","
                End If
                If Modul.ToUpper = "PEMESANAN" Then
                    Select Case objStatus.Status
                        Case 0
                            collStatus = collStatus & "''"
                        Case 1
                            collStatus = collStatus & "'C'"
                        Case 2
                            collStatus = collStatus & "'S'"
                        Case 3
                            collStatus = collStatus & "'P'"
                        Case 4
                            collStatus = collStatus & "'X'"
                        Case 5
                            collStatus = collStatus & "'T'"

                    End Select

                Else
                    collStatus = collStatus & objStatus.Status.ToString()
                End If
            Next


            Select Case (Modul.Trim().ToLower)
                Case "PK".Trim().ToLower
                    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PKHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criterias.opAnd(New Criteria(GetType(PKHeader), "PKStatus", MatchType.InSet, "(" & collStatus & ")"))
                    criterias.opAnd(New Criteria(GetType(PKHeader), "Dealer.ID", MatchType.Exact, DealerID))
                    arl = New PKHeaderFacade(user).Retrieve(criterias)
                Case "PO".Trim().ToLower
                    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(POHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criterias.opAnd(New Criteria(GetType(POHeader), "Status", MatchType.InSet, "(" & collStatus & ")"))
                    criterias.opAnd(New Criteria(GetType(POHeader), "ContractHeader.Dealer.ID", MatchType.Exact, DealerID))
                    arl = New POHeaderFacade(user).Retrieve(criterias)
                Case "Pembayaran".Trim().ToLower
                    Dim obj As New DailyPayment
                    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DailyPayment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criterias.opAnd(New Criteria(GetType(DailyPayment), "RejectStatus", MatchType.InSet, "(" & collStatus & ")"))
                    criterias.opAnd(New Criteria(GetType(DailyPayment), "POHeader.ContractHeader.Dealer.ID", MatchType.Exact, DealerID))
                    arl = New DailyPaymentFacade(user).Retrieve(criterias)
                Case "Faktur Kendaraan".Trim().ToLower
                    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ChassisMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criterias.opAnd(New Criteria(GetType(ChassisMaster), "FakturStatus", MatchType.InSet, "(" & collStatus & ")"))
                    criterias.opAnd(New Criteria(GetType(ChassisMaster), "Dealer.ID", MatchType.Exact, DealerID))
                    arl = New KTB.DNet.BusinessFacade.FinishUnit.ChassisMasterFacade(user).Retrieve(criterias)
                Case "Konsumen".Trim().ToLower
                    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CustomerRequest), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criterias.opAnd(New Criteria(GetType(CustomerRequest), "Status", MatchType.InSet, "(" & collStatus & ")"))
                    criterias.opAnd(New Criteria(GetType(CustomerRequest), "Dealer.ID", MatchType.Exact, DealerID))
                    arl = New CustomerRequestFacade(user).Retrieve(criterias)
                Case "SPAF dan Subsidi".Trim().ToLower
                    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.SPAFDoc), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.SPAFDoc), "Status", MatchType.InSet, "(" & collStatus & ")"))
                    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.SPAFDoc), "Dealer.ID", MatchType.Exact, DealerID))
                    arl = New KTB.DNet.BusinessFacade.SPAF.SPAFFacade(user).Retrieve(criterias)
                    'Case "Umum - Daftar Dokumen Service".Trim().ToLower                    
                    '    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.MonthlyDocument), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    '    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.MonthlyDocument), "LastDownloadBy", MatchType.InSet, "(" & collStatus & ")"))                    
                    '    arl = New MonthlyDocumentFacade(user).Retrieve(criterias)
                Case "PDI".Trim().ToLower
                    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PDI), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criterias.opAnd(New Criteria(GetType(PDI), "PDIStatus", MatchType.InSet, "(" & collStatus & ")"))
                    criterias.opAnd(New Criteria(GetType(PDI), "Dealer.ID", MatchType.Exact, DealerID))
                    arl = New PDIFacade(user).Retrieve(criterias)
                Case "Free Service".Trim().ToLower
                    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FreeService), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criterias.opAnd(New Criteria(GetType(FreeService), "Status", MatchType.InSet, "(" & collStatus & ")"))
                    criterias.opAnd(New Criteria(GetType(FreeService), "ChassisMaster.Dealer.ID", MatchType.Exact, DealerID))
                    arl = New KTB.DNet.BusinessFacade.Service.FreeServiceFacade(user).Retrieve(criterias)
                Case "WSC".Trim().ToLower
                    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(WSCHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criterias.opAnd(New Criteria(GetType(WSCHeader), "Status", MatchType.InSet, "(" & collStatus & ")"))
                    criterias.opAnd(New Criteria(GetType(WSCHeader), "Dealer.ID", MatchType.Exact, DealerID))
                    arl = New KTB.DNet.BusinessFacade.Service.WSCHeaderFacade(user).Retrieve(criterias)
                Case "Training".Trim().ToLower
                    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrTrainee), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criterias.opAnd(New Criteria(GetType(TrTrainee), "Status", MatchType.InSet, "(" & collStatus & ")"))
                    criterias.opAnd(New Criteria(GetType(TrTrainee), "Dealer.ID", MatchType.Exact, DealerID))
                    arl = New KTB.DNet.BusinessFacade.Training.TrTraineeFacade(user).Retrieve(criterias)
                Case "Periodical Maintenance".Trim().ToLower
                    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PMHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criterias.opAnd(New Criteria(GetType(PMHeader), "PMStatus", MatchType.InSet, "(" & collStatus & ")"))
                    criterias.opAnd(New Criteria(GetType(PMHeader), "Dealer.ID", MatchType.Exact, DealerID))
                    arl = New KTB.DNet.BusinessFacade.Service.PMHeaderFacade(user).Retrieve(criterias)
                Case "Staff Service".Trim().ToLower
                    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SalesmanHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criterias.opAnd(New Criteria(GetType(SalesmanHeader), "Status", MatchType.InSet, "(" & collStatus & ")"))
                    criterias.opAnd(New Criteria(GetType(SalesmanHeader), "SalesIndicator", MatchType.Exact, CInt(EnumSalesmanUnit.SalesmanUnit.Mekanik)))
                    criterias.opAnd(New Criteria(GetType(SalesmanHeader), "Dealer.ID", MatchType.Exact, DealerID))
                    arl = New KTB.DNet.BusinessFacade.Salesman.SalesmanHeaderFacade(user).Retrieve(criterias)
                Case "Equipment Repair".Trim().ToLower
                    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EquipmentSalesHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criterias.opAnd(New Criteria(GetType(EquipmentSalesHeader), "Status", MatchType.InSet, "(" & collStatus & ")"))
                    criterias.opAnd(New Criteria(GetType(EquipmentSalesHeader), "Dealer.ID", MatchType.Exact, DealerID))
                    arl = New EquipmentSalesHeaderFacade(user).Retrieve(criterias)
                Case "PQR".Trim().ToLower()
                    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PQRHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criterias.opAnd(New Criteria(GetType(PQRHeader), "RowStatus", MatchType.InSet, "(" & collStatus & ")"))
                    criterias.opAnd(New Criteria(GetType(PQRHeader), "Dealer.ID", MatchType.Exact, DealerID))
                    arl = New KTB.DNet.BusinessFacade.PQRHeaderFacade(user).Retrieve(criterias)
                Case "Pemesanan".Trim().ToLower()
                    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartPO), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criterias.opAnd(New Criteria(GetType(SparePartPO), "ProcessCode", MatchType.InSet, "(" & collStatus & ")"))
                    criterias.opAnd(New Criteria(GetType(SparePartPO), "Dealer.ID", MatchType.Exact, DealerID))
                    arl = New KTB.DNet.BusinessFacade.SparePart.SparePartPOFacade(user).Retrieve(criterias)
                Case "Master Barang".Trim().ToLower()
                    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.EquipmentSalesHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.EquipmentSalesHeader), "Status", MatchType.InSet, "(" & collStatus & ")"))
                    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.EquipmentSalesHeader), "Dealer.ID", MatchType.Exact, DealerID))
                    arl = New KTB.DNet.BusinessFacade.Service.EquipmentSalesHeaderFacade(user).Retrieve(criterias)
                Case "Permintaan Khusus".Trim().ToLower()
                    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.PartIncidentalHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PartIncidentalHeader), "KTBStatus", MatchType.InSet, "(" & collStatus & ")"))
                    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PartIncidentalHeader), "Dealer.ID", MatchType.Exact, DealerID))
                    arl = New KTB.DNet.BusinessFacade.SparePart.PartIncidentalHeaderFacade(user).Retrieve(criterias)
                Case "Salesman Part".Trim().ToLower()
                    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SalesmanHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criterias.opAnd(New Criteria(GetType(SalesmanHeader), "Status", MatchType.InSet, "(" & collStatus & ")"))
                    criterias.opAnd(New Criteria(GetType(SalesmanHeader), "SalesIndicator", MatchType.Exact, CInt(EnumSalesmanUnit.SalesmanUnit.Sparepart)))
                    criterias.opAnd(New Criteria(GetType(SalesmanHeader), "Dealer.ID", MatchType.Exact, DealerID))
                    arl = New KTB.DNet.BusinessFacade.Salesman.SalesmanHeaderFacade(user).Retrieve(criterias)
                Case "PRP".Trim().ToLower()
                    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PRPCategory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criterias.opAnd(New Criteria(GetType(PRPCategory), "Status", MatchType.InSet, "(" & collStatus & ")"))
                    arl = New KTB.DNet.BusinessFacade.PRPCategoryFacade(user).Retrieve(criterias)
                Case "Claim".Trim().ToLower()
                    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ClaimHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ClaimHeader), "StatusKTB", MatchType.InSet, "(" & collStatus & ")"))
                    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ClaimHeader), "Dealer.ID", MatchType.Exact, DealerID))
                    arl = New KTB.DNet.BusinessFacade.Claim.ClaimHeaderFacade(user).Retrieve(criterias)
                Case "Indent Part".Trim().ToLower()
                    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(IndentPartHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criterias.opAnd(New Criteria(GetType(IndentPartHeader), "Status", MatchType.InSet, "(" & collStatus & ")"))
                    criterias.opAnd(New Criteria(GetType(IndentPartHeader), "Dealer.ID", MatchType.Exact, DealerID))
                    arl = New KTB.DNet.BusinessFacade.IndentPart.IndentPartHeaderFacade(user).Retrieve(criterias)
                Case "Tenaga Penjual".Trim().ToLower()
                    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SalesmanHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criterias.opAnd(New Criteria(GetType(SalesmanHeader), "Status", MatchType.InSet, "(" & collStatus & ")"))
                    criterias.opAnd(New Criteria(GetType(SalesmanHeader), "SalesIndicator", MatchType.Exact, CInt(EnumSalesmanUnit.SalesmanUnit.Unit)))
                    criterias.opAnd(New Criteria(GetType(SalesmanHeader), "Dealer.ID", MatchType.Exact, DealerID))
                    arl = New KTB.DNet.BusinessFacade.Salesman.SalesmanHeaderFacade(user).Retrieve(criterias)
                Case "SAP".Trim().ToLower()
                    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SAPCustomer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criterias.opAnd(New Criteria(GetType(SAPCustomer), "Status", MatchType.InSet, "(" & collStatus & ")"))
                    criterias.opAnd(New Criteria(GetType(SAPCustomer), "SalesmanHeader.Dealer.ID", MatchType.Exact, DealerID))
                    arl = New KTB.DNet.BusinessFacade.SAP.SAPCustomerFacade(user).Retrieve(criterias)
                Case "Material Promotion".Trim().ToLower()
                    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.MaterialPromotion), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.MaterialPromotion), "Status", MatchType.InSet, "(" & collStatus & ")"))
                    arl = New KTB.DNet.BusinessFacade.MaterialPromotion.MaterialPromotionFacade(user).Retrieve(criterias)
                Case "BABIT".Trim().ToLower()
                    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitProposal), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criterias.opAnd(New Criteria(GetType(BabitProposal), "Status", MatchType.InSet, "(" & collStatus & ")"))
                    criterias.opAnd(New Criteria(GetType(BabitProposal), "Dealer.ID", MatchType.Exact, DealerID))
                    arl = New KTB.DNet.BusinessFacade.BabitSalesComm.BabitProposalFacade(user).Retrieve(criterias)
                Case "Transaksi".Trim().ToLower()
                    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DeliveryCustomerHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criterias.opAnd(New Criteria(GetType(DeliveryCustomerHeader), "Status", MatchType.InSet, "(" & collStatus & ")"))
                    criterias.opAnd(New Criteria(GetType(DeliveryCustomerHeader), "Dealer.ID", MatchType.Exact, DealerID))
                    arl = New KTB.DNet.BusinessFacade.DealerReport.DeliveryCustomerHeaderFacade(user).Retrieve(criterias)
                Case "Laporan-laporan".Trim().ToLower()
                    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DealerStockReportHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criterias.opAnd(New Criteria(GetType(DealerStockReportHeader), "Status", MatchType.InSet, "(" & collStatus & ")"))
                    criterias.opAnd(New Criteria(GetType(DealerStockReportHeader), "Dealer.ID", MatchType.Exact, DealerID))
                    arl = New KTB.DNet.BusinessFacade.DealerReport.DealerStockReportHeaderFacade(user).Retrieve(criterias)
                Case "Bulletin".Trim().ToLower()
                    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.Buletin), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Buletin), "Status", MatchType.InSet, "(" & collStatus & ")"))
                    arl = New KTB.DNet.BusinessFacade.Buletin.BuletinFacade(user).Retrieve(criterias)
                Case "Forum".Trim().ToLower()
                    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ForumCategory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ForumCategory), "Status", MatchType.InSet, "(" & collStatus & ")"))
                    arl = New KTB.DNet.BusinessFacade.BusinessForum.ForumCategoryFacade(user).Retrieve(criterias)
                Case "Maintenance".Trim().ToLower()
                    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.Dealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "Status", MatchType.InSet, "(" & collStatus & ")"))
                    arl = New KTB.DNet.BusinessFacade.General.DealerFacade(user).Retrieve(criterias)
                Case Else
                    Throw New Exception("Modul tidak diketahui: " + Modul)
            End Select
            Return arl
        End Function

#Region "ModifyControl"
        Public Shared Sub UpperTextControl(ByVal txtControl As TextBox, ByVal isUpper As Boolean)
            If isUpper Then
                txtControl.Text = txtControl.Text.ToUpper
            Else
                txtControl.Text = txtControl.Text.ToLower
            End If
        End Sub
#End Region

#Region "Terbilang"
        Public Function Terbilang(ByVal Angka As Double) As String
            Dim x As Double, y As Double, z As Double
            Dim strPecahan As String

            If Angka = 0 Then
                Terbilang = ""
                Exit Function
            End If

            Dim strJmlHuruf As String = LTrim(CStr(Angka))
            Dim intPecahan As Double = Val(Right(Mid(CStr(Angka), 15, 2), 2))

            If (intPecahan = 0) Then
                strPecahan = ""
            Else
                strPecahan = LTrim(Str(intPecahan)) + "/100 "
            End If

            x = 0
            y = 0
            Dim Urai As String = ""
            Dim Bil1 As String, Bil2 As String
            While (x < Len(strJmlHuruf))
                x = x + 1
                Dim strTot As Double = Mid(strJmlHuruf, x, 1)
                y = y + Val(strTot)
                z = Len(strJmlHuruf) - x + 1
                Select Case Val(strTot)
                    Case 1
                        If (z = 1 Or z = 7 Or z = 10 Or z = 13) Then
                            Bil1 = "Satu "
                        ElseIf (z = 4) Then
                            If (x = 1) Then
                                Bil1 = "Se"
                            Else
                                Bil1 = "Satu "
                            End If
                        ElseIf (z = 2 Or z = 5 Or z = 8 Or z = 11 Or z = 14) Then
                            x = x + 1
                            strTot = Mid(strJmlHuruf, x, 1)
                            z = Len(strJmlHuruf) - x + 1
                            Bil2 = ""
                            Select Case Val(strTot)
                                Case 0
                                    Bil1 = "Sepuluh "
                                Case 1
                                    Bil1 = "Sebelas "
                                Case 2
                                    Bil1 = "Dua Belas "
                                Case 3
                                    Bil1 = "Tiga Belas "
                                Case 4
                                    Bil1 = "Empat Belas "
                                Case 5
                                    Bil1 = "Lima Belas "
                                Case 6
                                    Bil1 = "Enam Belas "
                                Case 7
                                    Bil1 = "Tujuh Belas "
                                Case 8
                                    Bil1 = "Delapan Belas "
                                Case 9
                                    Bil1 = "Sembilan Belas "
                            End Select
                        Else
                            Bil1 = "Se"
                        End If
                    Case 2
                        Bil1 = "Dua "
                    Case 3
                        Bil1 = "Tiga "
                    Case 4
                        Bil1 = "Empat "
                    Case 5
                        Bil1 = "Lima "
                    Case 6
                        Bil1 = "Enam "
                    Case 7
                        Bil1 = "Tujuh "
                    Case 8
                        Bil1 = "Delapan "
                    Case 9
                        Bil1 = "Sembilan "
                    Case Else
                        Bil1 = ""
                End Select

                If (Val(strTot) > 0) Then
                    If (z = 2 Or z = 5 Or z = 8 Or z = 11 Or z = 14) Then
                        If Bil1 = "Se" Then
                            Bil2 = "puluh "
                        Else
                            Bil2 = "Puluh "
                        End If
                    ElseIf (z = 3 Or z = 6 Or z = 9 Or z = 12 Or z = 15) Then
                        If Bil1 = "Se" Then
                            Bil2 = "ratus "
                        Else
                            Bil2 = "Ratus "
                        End If
                    ElseIf (z = 4) Then
                        If Bil1 = "Se" Then
                            Bil2 = "ribu "
                        Else
                            Bil2 = "Ribu "
                        End If
                    ElseIf (z = 7) Then
                        If Bil1 = "Se" Then
                            Bil2 = "juta "
                        Else
                            Bil2 = "Juta "
                        End If
                    ElseIf (z = 10) Then
                        If Bil1 = "Se" Then
                            Bil2 = "milyar "
                        Else
                            Bil2 = "Milyar "
                        End If
                    ElseIf (z = 13) Then
                        If Bil1 = "Se" Then
                            Bil2 = "trilyun "
                        Else
                            Bil2 = "Trilyun "
                        End If
                    Else
                        Bil2 = ""
                    End If
                Else
                    Bil2 = ""
                End If
                If (y > 0) Then
                    Select Case z
                        Case 4
                            If Bil2 = "Se" Then
                                Bil2 = Bil2 + "ribu "
                            Else
                                Bil2 = Bil2 + "Ribu "
                            End If
                            y = 0
                        Case 7
                            If Bil2 = "Se" Then
                                Bil2 = Bil2 + "juta "
                            Else
                                Bil2 = Bil2 + "Juta "
                            End If
                            y = 0
                        Case 10
                            If Bil2 = "Se" Then
                                Bil2 = Bil2 + "milyar "
                            Else
                                Bil2 = Bil2 + "Milyar "
                            End If
                            y = 0
                        Case 13
                            If Bil2 = "Se" Then
                                Bil2 = Bil2 + "trilyun "
                            Else
                                Bil2 = Bil2 + "Trilyun "
                            End If
                            y = 0
                    End Select
                End If
                Urai = Urai + Bil1 + Bil2
            End While
            Urai = Urai + strPecahan
            Terbilang = Urai & "RUPIAH"
        End Function

        Public Function TerbilangCamelCase(ByVal Angka As Double) As String
            Dim x As Double, y As Double, z As Double
            Dim strPecahan As String

            If Angka = 0 Then
                TerbilangCamelCase = ""
                Exit Function
            End If

            Dim strJmlHuruf As String = LTrim(CStr(Angka))
            Dim intPecahan As Double = Val(Right(Mid(CStr(Angka), 15, 2), 2))

            If (intPecahan = 0) Then
                strPecahan = ""
            Else
                strPecahan = LTrim(Str(intPecahan)) + "/100 "
            End If

            x = 0
            y = 0
            Dim Urai As String = ""
            Dim Bil1 As String, Bil2 As String
            While (x < Len(strJmlHuruf))
                x = x + 1
                Dim strTot As Double = Mid(strJmlHuruf, x, 1)
                y = y + Val(strTot)
                z = Len(strJmlHuruf) - x + 1
                Select Case Val(strTot)
                    Case 1
                        If (z = 1 Or z = 7 Or z = 10 Or z = 13) Then
                            Bil1 = "Satu "
                        ElseIf (z = 4) Then
                            If (x = 1) Then
                                Bil1 = "Se"
                            Else
                                Bil1 = "Satu "
                            End If
                        ElseIf (z = 2 Or z = 5 Or z = 8 Or z = 11 Or z = 14) Then
                            x = x + 1
                            strTot = Mid(strJmlHuruf, x, 1)
                            z = Len(strJmlHuruf) - x + 1
                            Bil2 = ""
                            Select Case Val(strTot)
                                Case 0
                                    Bil1 = "Sepuluh "
                                Case 1
                                    Bil1 = "Sebelas "
                                Case 2
                                    Bil1 = "Dua Belas "
                                Case 3
                                    Bil1 = "Tiga Belas "
                                Case 4
                                    Bil1 = "Empat Belas "
                                Case 5
                                    Bil1 = "Lima Belas "
                                Case 6
                                    Bil1 = "Enam Belas "
                                Case 7
                                    Bil1 = "Tujuh Belas "
                                Case 8
                                    Bil1 = "Delapan Belas "
                                Case 9
                                    Bil1 = "Sembilan Belas "
                            End Select
                        Else
                            Bil1 = "Se"
                        End If
                    Case 2
                        Bil1 = "Dua "
                    Case 3
                        Bil1 = "Tiga "
                    Case 4
                        Bil1 = "Empat "
                    Case 5
                        Bil1 = "Lima "
                    Case 6
                        Bil1 = "Enam "
                    Case 7
                        Bil1 = "Tujuh "
                    Case 8
                        Bil1 = "Delapan "
                    Case 9
                        Bil1 = "Sembilan "
                    Case Else
                        Bil1 = ""
                End Select

                If (Val(strTot) > 0) Then
                    If (z = 2 Or z = 5 Or z = 8 Or z = 11 Or z = 14) Then
                        If Bil1 = "Se" Then
                            Bil2 = "puluh "
                        Else
                            Bil2 = "Puluh "
                        End If
                    ElseIf (z = 3 Or z = 6 Or z = 9 Or z = 12 Or z = 15) Then
                        If Bil1 = "Se" Then
                            Bil2 = "ratus "
                        Else
                            Bil2 = "Ratus "
                        End If
                    ElseIf (z = 4) Then
                        If Bil1 = "Se" Then
                            Bil2 = "ribu "
                        Else
                            Bil2 = "Ribu "
                        End If
                    ElseIf (z = 7) Then
                        If Bil1 = "Se" Then
                            Bil2 = "juta "
                        Else
                            Bil2 = "Juta "
                        End If
                    ElseIf (z = 10) Then
                        If Bil1 = "Se" Then
                            Bil2 = "milyar "
                        Else
                            Bil2 = "Milyar "
                        End If
                    ElseIf (z = 13) Then
                        If Bil1 = "Se" Then
                            Bil2 = "trilyun "
                        Else
                            Bil2 = "Trilyun "
                        End If
                    Else
                        Bil2 = ""
                    End If
                Else
                    Bil2 = ""
                End If
                If (y > 0) Then
                    Select Case z
                        Case 4
                            If Bil2 = "Se" Then
                                Bil2 = Bil2 + "ribu "
                            Else
                                Bil2 = Bil2 + "Ribu "
                            End If
                            y = 0
                        Case 7
                            If Bil2 = "Se" Then
                                Bil2 = Bil2 + "juta "
                            Else
                                Bil2 = Bil2 + "Juta "
                            End If
                            y = 0
                        Case 10
                            If Bil2 = "Se" Then
                                Bil2 = Bil2 + "milyar "
                            Else
                                Bil2 = Bil2 + "Milyar "
                            End If
                            y = 0
                        Case 13
                            If Bil2 = "Se" Then
                                Bil2 = Bil2 + "trilyun "
                            Else
                                Bil2 = Bil2 + "Trilyun "
                            End If
                            y = 0
                    End Select
                End If
                Urai = Urai + Bil1 + Bil2
            End While
            Urai = Urai + strPecahan
            TerbilangCamelCase = Urai & "Rupiah"
        End Function
#End Region

#Region "BindToDatabase"
        ' 20-Jul-2007 Deddy H   Diambil dr database
        Public Shared Function BindSalesmanCode(ByVal ddl As DropDownList, ByVal user As System.Security.Principal.IPrincipal, ByVal isIncludeBlank As Boolean, ByVal isAllList As Boolean, ByVal SalesIndicator As String, Optional ByVal strDealerCode As String = "")
            If (isIncludeBlank) Then
                ddl.Items.Add(New ListItem("", ""))
            End If
            Dim arlSalesmanHeader As ArrayList
            If isAllList Then
                arlSalesmanHeader = New SalesmanHeaderFacade(user).RetrieveList()
            Else
                Dim criterias As New CriteriaComposite(New Criteria(GetType(SalesmanHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(SalesmanHeader), "RegisterStatus", MatchType.Exact, "1"))
                If SalesIndicator <> "" Then
                    criterias.opAnd(New Criteria(GetType(SalesmanHeader), "SalesIndicator", MatchType.Exact, CType(SalesIndicator, Byte)))
                End If

                If strDealerCode <> "" Then
                    criterias.opAnd(New Criteria(GetType(SalesmanHeader), "Dealer.DealerCode", MatchType.Exact, strDealerCode))
                End If

                Dim sortColl As SortCollection = New SortCollection
                Dim sortColumn As String
                Dim sortDirection As Sort.SortDirection

                sortColumn = "SalesmanCode"
                sortDirection = Sort.SortDirection.ASC

                If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                    sortColl.Add(New Sort(GetType(SalesmanHeader), sortColumn, sortDirection))
                Else
                    sortColl = Nothing
                End If

                arlSalesmanHeader = New SalesmanHeaderFacade(user).Retrieve(criterias, sortColl)
            End If

            For Each obj As SalesmanHeader In arlSalesmanHeader
                ddl.Items.Add(New ListItem(obj.SalesmanCode, obj.ID))
            Next
        End Function

        ' 24-Jul-2007 Deddy H   Diambil dr database
        Public Shared Function BindSalesmanArea(ByVal ddl As DropDownList, ByVal user As System.Security.Principal.IPrincipal, ByVal isIncludeBlank As Boolean)
            If (isIncludeBlank) Then
                ddl.Items.Add(New ListItem("", ""))
            End If
            Dim arlSalesmanArea As ArrayList = New SalesmanAreaFacade(user).RetrieveList()

            For Each obj As SalesmanArea In arlSalesmanArea
                ddl.Items.Add(New ListItem(obj.AreaCode, obj.ID))
            Next
        End Function

        Public Shared Function BindSalesmanTrainingCode(ByVal ddl As DropDownList, ByVal user As System.Security.Principal.IPrincipal, ByVal isIncludeBlank As Boolean)
            If (isIncludeBlank) Then
                ddl.Items.Add(New ListItem("", ""))
            End If
            Dim arlSalesmanMasterTraining As ArrayList = New SalesmanMasterTrainingFacade(user).RetrieveList()

            For Each obj As SalesmanMasterTraining In arlSalesmanMasterTraining
                ddl.Items.Add(New ListItem(obj.TrainingCode, obj.ID))
            Next
        End Function


        ' 06-Aug-2007 Deddy H   Diambil dr database
        Public Shared Function BindSalesmanUnifDistributionCode(ByVal ddl As DropDownList, ByVal user As System.Security.Principal.IPrincipal, ByVal isIncludeBlank As Boolean)
            Dim sortColl As SortCollection = New SortCollection
            Dim sortColumn As String
            Dim sortDirection As Sort.SortDirection
            sortColumn = "SalesmanUnifDistributionCode"
            sortDirection = Sort.SortDirection.ASC

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SalesmanUnifDistribution), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As New CriteriaComposite(New Criteria(GetType(SalesmanUnifDistribution), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SalesmanUnifDistribution), "IsActive", MatchType.Exact, CType(enumStatusSalesmanUnifDistribution.StatusSalesmanUnifDistribution.Aktif, Integer)))

            If (isIncludeBlank) Then
                ddl.Items.Add(New ListItem("Silakan Pilih", "-1"))
            End If

            Dim arlSalesmanUnifDistribution As ArrayList = New SalesmanUnifDistributionFacade(user).Retrieve(criterias, sortColl)

            For Each obj As SalesmanUnifDistribution In arlSalesmanUnifDistribution
                ddl.Items.Add(New ListItem(obj.SalesmanUnifDistributionCode, obj.ID))
            Next
        End Function

        ' 06-Aug-2007 Deddy H   Diambil dr database, di filter per SalesmanUnifDistributionID
        Public Shared Function BindSalesmanUniformCode(ByVal ddl As DropDownList, ByVal user As System.Security.Principal.IPrincipal, ByVal isIncludeBlank As Boolean, ByVal intSalesmanUnifDistributionID As Integer, ByVal isAllList As Boolean)
            Dim arlSalesmanUniform As ArrayList

            ddl.Items.Clear()
            If (isIncludeBlank) Then
                ddl.Items.Add(New ListItem("", ""))
            End If
            If isAllList = True Then
                arlSalesmanUniform = New SalesmanUniformFacade(user).RetrieveList()
            Else
                Dim criterias As New CriteriaComposite(New Criteria(GetType(SalesmanUniform), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(SalesmanUniform), "SalesmanUnifDistribution.ID", MatchType.Exact, intSalesmanUnifDistributionID))
                'arlSalesmanUniform = New SalesmanUniformFacade(user).RetrieveByCriteria(criterias, 1, 25, 25)
                arlSalesmanUniform = New SalesmanUniformFacade(user).Retrieve(criterias)
            End If


            For Each obj As SalesmanUniform In arlSalesmanUniform
                ddl.Items.Add(New ListItem(obj.SalesmanUniformCode, obj.ID))
            Next
        End Function

        ' 13-Aug-2007 Deddy H   Diambil dr database
        Public Shared Function BindArea2Code(ByVal ddl As DropDownList, ByVal user As System.Security.Principal.IPrincipal, ByVal isIncludeBlank As Boolean)
            If (isIncludeBlank) Then
                ddl.Items.Add(New ListItem("", ""))
            End If
            Dim arlArea2 As ArrayList = New Area2Facade(user).RetrieveList()

            For Each obj As Area2 In arlArea2
                ddl.Items.Add(New ListItem(obj.AreaCode, obj.ID))
            Next
        End Function

        ' 13-Aug-2007 Deddy H   Diambil dr database
        Public Shared Function BindArea1Code(ByVal ddl As DropDownList, ByVal user As System.Security.Principal.IPrincipal, ByVal isIncludeBlank As Boolean)
            If (isIncludeBlank) Then
                ddl.Items.Add(New ListItem("", ""))
            End If
            Dim arlArea1 As ArrayList = New Area1Facade(user).RetrieveList()

            For Each obj As Area1 In arlArea1
                ddl.Items.Add(New ListItem(obj.AreaCode, obj.ID))
            Next
        End Function


        ' 28-Aug-2007 Diana   Diambil dr database
        Public Shared Function BindForumCategory(ByVal ddl As DropDownList, ByVal user As System.Security.Principal.IPrincipal, ByVal isIncludeBlank As Boolean)
            Dim objForumCategoryFacade As ForumCategoryFacade = New ForumCategoryFacade(user)
            'bindArrayListToDropDownList(Me.ddlKategori, objForumCategoryFacade.RetrieveCategoryTypeList)

            If (isIncludeBlank) Then
                ddl.Items.Add(New ListItem("-Silahkan Pilih-", ""))
            End If
            Dim arlCategory As ArrayList = New ForumCategoryFacade(user).RetrieveCategoryTypeList()

            For Each obj As ForumCategory In arlCategory
                ddl.Items.Add(New ListItem(obj.Category, obj.ID))
            Next
        End Function
        ' 28-Aug-2007 Diana   Diambil dr database
        Public Shared Function BindJobPosition(ByVal ddl As DropDownList, ByVal user As System.Security.Principal.IPrincipal, ByVal isIncludeBlank As Boolean)
            If (isIncludeBlank) Then
                ddl.Items.Add(New ListItem("", ""))
            End If
            Dim cJP As New CriteriaComposite(New Criteria(GetType(JobPosition), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim sJP As New SortCollection
            sJP.Add(New Sort(GetType(JobPosition), "Description", Sort.SortDirection.ASC))
            Dim arlJobPosition As ArrayList = New JobPositionFacade(user).Retrieve(cJP, sJP) '.RetrieveList()

            For Each obj As JobPosition In arlJobPosition
                ddl.Items.Add(New ListItem(obj.Description, obj.ID))
            Next
        End Function
        ' 11-Sep-2007 Deddy H   Diambil dr database, ada criteria yg aktif saja
        Public Shared Function BindJobPosition(ByVal ddl As DropDownList, ByVal user As System.Security.Principal.IPrincipal, ByVal isIncludeBlank As Boolean, ByVal isAllList As Boolean)
            Dim arlJobPosition As ArrayList

            ' mengambil data kategori untuk salesman
            Dim strValidCode As String
            strValidCode = ""
            'CommonKeyName = > KEy dibawah ini
            'With KTB.DNet.Lib.WebConfig.GetValue
            '    strValidCode = .Get("SlmanCode") & ";" & .Get("SCntCode") & ";" & .Get("SSpvCode") & ";" & _
            '    .Get("SManCode") & ";" & .Get("BManCode")
            'End With
            strValidCode = KTB.DNet.Lib.WebConfig.GetValue("CommonKeyName")  

            ddl.Items.Clear()
            If (isIncludeBlank) Then
                ddl.Items.Add(New ListItem("Silakan Pilih", ""))
            End If

            Dim sortColl As SortCollection = New SortCollection
            Dim sortColumn As String
            Dim sortDirection As Sort.SortDirection
            sortColumn = "Description"
            sortDirection = Sort.SortDirection.ASC

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(JobPosition), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            If isAllList = True Then
                arlJobPosition = New JobPositionFacade(user).RetrieveList()
            Else
                Dim criterias As New CriteriaComposite(New Criteria(GetType(JobPosition), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                If strValidCode <> "" Then
                    criterias.opAnd(New Criteria(GetType(JobPosition), "Code", MatchType.InSet, GetStrValue(strValidCode, ";", ",")))
                End If
                arlJobPosition = New JobPositionFacade(user).Retrieve(criterias, sortColl)
            End If

            For Each obj As JobPosition In arlJobPosition
                ddl.Items.Add(New ListItem(obj.Description, obj.ID))
            Next
        End Function

        ' 22-july-2010 ANH   Diambil dr database based on menu assigned
        Public Shared Function BindJobPositionByMenuAssigned(ByVal ddl As DropDownList, ByVal user As System.Security.Principal.IPrincipal, ByVal isIncludeBlank As Boolean, ByVal iMenu As Integer)
            If (isIncludeBlank) Then
                ddl.Items.Add(New ListItem("", ""))
            End If

            Dim arlJobPosition As ArrayList = New JobPositionFacade(user).RetriveListByMenuAssigned(iMenu)
            For Each obj As JobPosition In arlJobPosition
                ddl.Items.Add(New ListItem(obj.Description, obj.ID))
            Next
        End Function

        ' 6-jun-2011 ANH   Diambil dr database based on menu assigned
        'Public Shared Function BindSalemenPartLevel(ByVal ddl As DropDownList, ByVal user As System.Security.Principal.IPrincipal, ByVal isIncludeBlank As Boolean)
        '    If (isIncludeBlank) Then
        '        ddl.Items.Add(New ListItem("", ""))
        '    End If

        '    Dim arlSalesmanCategoryLevel As ArrayList = New SalesmanCategoryLevelFacade(user).RetrieveActiveList()
        '    For Each obj As SalesmanCategoryLevel In arlSalesmanCategoryLevel
        '        ddl.Items.Add(New ListItem(obj.PositionName, obj.ID))
        '    Next
        'End Function

        ' 17-june-2013 ANH   Diambil dr database based on menu assigned
        Public Shared Function BindDealerBranchByDealerID(ByVal ddl As DropDownList, ByVal user As System.Security.Principal.IPrincipal, ByVal objDealerID As Integer, ByVal isIncludeBlank As Boolean)
            If (isIncludeBlank) Then
                ddl.Items.Add(New ListItem("", ""))
            End If
            Dim sortColl As SortCollection = New SortCollection
            Dim sortColumn As String
            Dim sortDirection As Sort.SortDirection
            sortColumn = "DealerName"
            sortDirection = Sort.SortDirection.ASC

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(DealerBranch), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As New CriteriaComposite(New Criteria(GetType(DealerBranch), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(DealerBranch), "Dealer.ID", MatchType.Exact, objDealerID))
            Dim arlDealerBranch As ArrayList = New DealerBranchFacade(user).Retrieve(criterias, sortColl)

            For Each obj As DealerBranch In arlDealerBranch
                ddl.Items.Add(New ListItem(obj.DealerName, obj.ID))
            Next
        End Function

        ' 11-Sep-2007 Deddy H   Diambil dr database, ada criteria yg aktif saja
        Public Shared Function BindProvince(ByVal ddl As DropDownList, ByVal user As System.Security.Principal.IPrincipal, ByVal isIncludeBlank As Boolean, ByVal isAllList As Boolean)
            Dim arlProvince As ArrayList

            ddl.Items.Clear()
            If (isIncludeBlank) Then
                ddl.Items.Add(New ListItem("Silakan Pilih", ""))
            End If

            If isAllList = True Then
                arlProvince = New ProvinceFacade(user).RetrieveList()
            Else
                Dim criterias As New CriteriaComposite(New Criteria(GetType(Province), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                arlProvince = New ProvinceFacade(user).Retrieve(criterias)
            End If

            For Each obj As Province In arlProvince
                ddl.Items.Add(New ListItem(obj.ProvinceName.ToUpper, obj.ID))
            Next
        End Function

        ' 11-Sep-2007 Deddy H   Diambil dr database, di filter per ProvinceID
        Public Shared Function BindCity(ByVal ddl As DropDownList, ByVal user As System.Security.Principal.IPrincipal, ByVal isIncludeBlank As Boolean, ByVal intProvinceID As Integer, ByVal isAllList As Boolean)
            Dim arlCity As ArrayList

            ddl.Items.Clear()
            If (isIncludeBlank) Then
                ddl.Items.Add(New ListItem("Silakan Pilih", ""))
            End If
            If isAllList = True Then
                arlCity = New CityFacade(user).RetrieveList()
            Else
                Dim criterias As New CriteriaComposite(New Criteria(GetType(City), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(City), "Province.ID", MatchType.Exact, intProvinceID))
                arlCity = New CityFacade(user).Retrieve(criterias)
            End If

            For Each obj As City In arlCity
                ddl.Items.Add(New ListItem(obj.CityName.ToUpper, obj.ID))
            Next
        End Function

        Public Shared Function BindCityPart(ByVal ddl As DropDownList, ByVal user As System.Security.Principal.IPrincipal, ByVal isIncludeBlank As Boolean, ByVal intProvinceID As Integer, ByVal isAllList As Boolean)
            Dim arlCity As ArrayList

            ddl.Items.Clear()
            If (isIncludeBlank) Then
                ddl.Items.Add(New ListItem("Silakan Pilih", ""))
            End If
            If isAllList = True Then
                arlCity = New CityPartFacade(user).RetrieveList()
            Else
                Dim criterias As New CriteriaComposite(New Criteria(GetType(CityPart), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(CityPart), "Province.ID", MatchType.Exact, intProvinceID))
                arlCity = New CityPartFacade(user).Retrieve(criterias)
            End If

            For Each obj As CityPart In arlCity
                ddl.Items.Add(New ListItem(obj.CityName.ToUpper, obj.ID))
            Next
        End Function

        ' 10-Oct-2007 Deddy H   Diambil dr database
        Public Shared Function BindSalesmanLevel(ByVal ddl As DropDownList, ByVal user As System.Security.Principal.IPrincipal, ByVal isIncludeBlank As Boolean)
            Dim arlSalesmanLevel As ArrayList

            ddl.Items.Clear()
            If (isIncludeBlank) Then
                ddl.Items.Add(New ListItem("Silahkan Pilih", ""))
            End If

            Dim criterias As New CriteriaComposite(New Criteria(GetType(SalesmanLevel), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            arlSalesmanLevel = New SalesmanHeaderFacade(user).RetrieveSalesmanLevel(criterias)

            For Each obj As SalesmanLevel In arlSalesmanLevel
                ddl.Items.Add(New ListItem(obj.Description.ToUpper, obj.ID))
            Next
        End Function
#End Region

#Region "BindFromEnum"

        Public Shared Function GetSilahkanPilihItem() As ListItem
            Return New ListItem("-Silahkan Pilih-", "")
        End Function

        '26-Jul-2007 Deddy H    Diambil dari domain Enum
        Public Shared Function BindFromEnum(ByVal strEnum As String, ByVal ddl As DropDownList, ByVal user As System.Security.Principal.IPrincipal, ByVal isIncludeBlank As Boolean, ByVal strText As String, ByVal strValue As String)
            ddl.Items.Clear()

            Select Case strEnum
                Case "SalesmanUnit"
                    ddl.DataSource = EnumSalesUnit.RetriveSalesmanUnit(isIncludeBlank)
                Case "SalesmanRegisterStatus"
                    ddl.DataSource = EnumSalesRegisterStatus.RetriveSalesmanRegisterStatus(isIncludeBlank)
                Case "SalesmanStatus"
                    ddl.DataSource = New EnumSalesmanStatus().RetriveSalesmanStatus(isIncludeBlank)

                Case "Operator"
                    ddl.DataSource = EnumOperatorVal.RetriveOperator(isIncludeBlank)
                Case "SPLEnum"
                    ddl.DataSource = SPLEnum.RetrieveEnumInterest()
                Case "Month"
                    ddl.DataSource = enumMonthGet.RetriveMonth(isIncludeBlank)
                Case "SalesmanGender"
                    ddl.DataSource = EnumGenderOp.RetriveSalesGender(isIncludeBlank)
                Case "BabitReleaseStatus"
                    ddl.DataSource = EnumBabit.PaymentReleaseStatusLst(isIncludeBlank)
                Case "StatusSPL"
                    ddl.DataSource = EnumStatusSPL.RetrieveStatus(isIncludeBlank)
                Case "MarriedStatus"
                    ddl.DataSource = EnumSalesMarriedStatus.RetriveMarriedStatus(isIncludeBlank)
                Case "SAPCustomerStatus"
                    ddl.DataSource = EnumSAPCustStatus.RetriveSAPCustomerStatus(isIncludeBlank)
                Case "IndentPartPaymentType"
                    ddl.DataSource = EnumIndentPartStatus.RetriveIndentPartHeaderPaymentType(isIncludeBlank)
                Case "IndentPartMaterialType"
                    ddl.DataSource = EnumMaterialType.RetrieveType(True)
                Case "EstimationEquipmentPaymentType"
                    ddl.DataSource = New EnumEstimationEquipStatus().RetrievePaymentType()
                Case "Religion"
                    ddl.DataSource = EnumReligionValue.RetriveReligion(isIncludeBlank)
                Case "SalesmanPartLevel"
                    ddl.DataSource = EnumSalesmanPartLevel.RetriveSalesmanPartLevel(isIncludeBlank)
                Case "PartShopStatus"
                    ddl.DataSource = New EnumPartShopStatus().RetrieveStatus()
            End Select

            ddl.DataTextField = strText
            ddl.DataValueField = strValue
            ddl.DataBind()

        End Function

#End Region

#Region "StringManipulate"
        '27-Jul-2007 Untuk handle spt 1 -> 001 [maxlength]
        Public Shared Function ConvIntToStr(ByVal intValue As Integer, ByVal intMaxLength As Integer) As String
            Dim strValue As String
            Select Case CType(intValue, String).Length
                Case 1
                    strValue = "00" & CType(intValue, String)
                Case 2
                    strValue = "0" & CType(intValue, String)
            End Select
            Return strValue
        End Function

        '02-Aug-2007 Deddy H    Mengembalikan string yg dibutuhkan untuk match.inset
        Public Shared Function GetStrValue(ByVal strValue As String, ByVal strOperatorFromSplit As String, ByVal strOperatorToSplit As String) As String
            Dim txtTmp As New TextBox
            Dim strReturn As String
            txtTmp.Text = strValue
            strReturn = "('" & txtTmp.Text.Trim().Replace(strOperatorFromSplit, "'" & strOperatorToSplit & "'") & "')"
            Return strReturn
        End Function

        '3-aug-2020 Anna NH   remove space lebih dari 1 char
        Public Shared Function RemoveWhiteSpace(ByVal strText)
            Dim RegEx As New System.Text.RegularExpressions.Regex("\s+")
            'RegEx.Pattern = "\s+"
            'RegEx.Multiline = True
            'RegEx.Global = True
            strText = RegEx.Replace(strText, " ")
            RemoveWhiteSpace = strText
        End Function
#End Region

#Region "Sorting"
        ' 29/Aug/2007   Deddy H     To handle sorting on arraylist,
        ' return arraylist sorted
        Public Shared Function SortListControl(ByRef ArrList As ArrayList, ByVal SortColumn As String, _
                                      ByVal SortDirection As Integer) As ArrayList
            Dim ArrSorted As ArrayList
            Dim IsAsc As Boolean = True
            If SortDirection = Sort.SortDirection.ASC Then
                IsAsc = True
            ElseIf SortDirection = Sort.SortDirection.DESC Then
                IsAsc = False
            End If

            Dim objListComparer As IComparer = New ListComparer(IsAsc, SortColumn)
            ArrList.Sort(objListComparer)
            ArrSorted = ArrList
            Return ArrSorted

        End Function
#End Region


#Region "TerbilangEnglish"
        Public Function Terbilang_Eng(ByVal nNumber As Double) As String

            Dim bNegative As Boolean
            Dim bHundred As Boolean

            If nNumber < 0 Then
                bNegative = True
            End If

            nNumber = Math.Abs(Int(nNumber))

            If nNumber < 1000 Then
                If nNumber \ 100 > 0 Then
                    Terbilang_Eng = Terbilang_Eng & _
                         Terbilang_Eng(nNumber \ 100) & " hundred"
                    bHundred = True
                End If
                nNumber = nNumber - ((nNumber \ 100) * 100)
                Dim bNoFirstDigit As Boolean
                bNoFirstDigit = False
                Select Case nNumber \ 10
                    Case 0
                        Select Case nNumber Mod 10
                            Case 0
                                If Not bHundred Then
                                    Terbilang_Eng = Terbilang_Eng & " zero"
                                End If
                            Case 1 : Terbilang_Eng = Terbilang_Eng & " one"
                            Case 2 : Terbilang_Eng = Terbilang_Eng & " two"
                            Case 3 : Terbilang_Eng = Terbilang_Eng & " three"
                            Case 4 : Terbilang_Eng = Terbilang_Eng & " four"
                            Case 5 : Terbilang_Eng = Terbilang_Eng & " five"
                            Case 6 : Terbilang_Eng = Terbilang_Eng & " six"
                            Case 7 : Terbilang_Eng = Terbilang_Eng & " seven"
                            Case 8 : Terbilang_Eng = Terbilang_Eng & " eight"
                            Case 9 : Terbilang_Eng = Terbilang_Eng & " nine"
                        End Select
                        bNoFirstDigit = True
                    Case 1
                        Select Case nNumber Mod 10
                            Case 0 : Terbilang_Eng = Terbilang_Eng & " ten"
                            Case 1 : Terbilang_Eng = Terbilang_Eng & " eleven"
                            Case 2 : Terbilang_Eng = Terbilang_Eng & " twelve"
                            Case 3 : Terbilang_Eng = Terbilang_Eng & " thirteen"
                            Case 4 : Terbilang_Eng = Terbilang_Eng & " fourteen"
                            Case 5 : Terbilang_Eng = Terbilang_Eng & " fifteen"
                            Case 6 : Terbilang_Eng = Terbilang_Eng & " sixteen"
                            Case 7 : Terbilang_Eng = Terbilang_Eng & " seventeen"
                            Case 8 : Terbilang_Eng = Terbilang_Eng & " eighteen"
                            Case 9 : Terbilang_Eng = Terbilang_Eng & " nineteen"
                        End Select
                        bNoFirstDigit = True
                    Case 2 : Terbilang_Eng = Terbilang_Eng & " twenty"
                    Case 3 : Terbilang_Eng = Terbilang_Eng & " thirty"
                    Case 4 : Terbilang_Eng = Terbilang_Eng & " forty"
                    Case 5 : Terbilang_Eng = Terbilang_Eng & " fifty"
                    Case 6 : Terbilang_Eng = Terbilang_Eng & " sixty"
                    Case 7 : Terbilang_Eng = Terbilang_Eng & " seventy"
                    Case 8 : Terbilang_Eng = Terbilang_Eng & " eighty"
                    Case 9 : Terbilang_Eng = Terbilang_Eng & " ninety"
                End Select
                If Not bNoFirstDigit Then
                    If nNumber Mod 10 <> 0 Then
                        Terbilang_Eng = Terbilang_Eng & " " & _
                                      Mid(Terbilang_Eng(nNumber Mod 10), 2)
                    End If
                End If
            Else
                Dim nTemp As Double
                nTemp = 10 ^ 12 'trillion
                Do While nTemp >= 1
                    If nNumber >= nTemp Then
                        Terbilang_Eng = Terbilang_Eng & _
                                      Terbilang_Eng(Int(nNumber / nTemp))
                        Select Case Int(Math.Log(nTemp) / Math.Log(10) + 0.5)
                            Case 12 : Terbilang_Eng = Terbilang_Eng & " trillion"
                            Case 9 : Terbilang_Eng = Terbilang_Eng & " billion"
                            Case 6 : Terbilang_Eng = Terbilang_Eng & " million"
                            Case 3 : Terbilang_Eng = Terbilang_Eng & " thousand"
                        End Select

                        nNumber = nNumber - (Int(nNumber / nTemp) * nTemp)
                    End If
                    nTemp = nTemp / 1000
                Loop
            End If

            If bNegative Then
                Terbilang_Eng = " negative" & Terbilang_Eng
            End If

            Return Terbilang_Eng

        End Function

#End Region

#Region "Special Method to prevent double event in a time"
        'Ditujukan utk event-event postback yang bukan button. Contoh linkbutton.
        Public Shared Function PreventDoubleEvent(ByVal action As String) As String
            Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder("if (alreadySubmit) { alert('Data sedang diproses');return false;} alreadySubmit=true;")
            sb.Append(action)
            Return sb.ToString()
        End Function

        Public Shared Sub PrepareDoubleEventPrevention(ByVal webPage As Page)
            webPage.Response.Write("<script language='javascript'>var alreadySubmit=false;var alwaysSubmit=false;</script>")
            webPage.RegisterOnSubmitStatement("submit", "if (alreadySubmit && !alwaysSubmit) { alert('Data sedang diproses');return false;} if (!alwaysSubmit) alreadySubmit=true;alwaysSubmit=false;")
        End Sub

        Public Shared Sub NoPrevention(ByVal whichControl As WebControl)
            whichControl.Attributes.Clear()
            whichControl.Attributes.Add("OnClick", "alwaysSubmit=true;")
        End Sub
#End Region

#Region "Vehicle-SubCategory"

        Public Shared Sub BindVehicleSubCategoryToDDL(ByRef ddlSubCategory As DropDownList, ByVal strCategoryCode As String)
            Dim arlItem As ArrayList
            Dim objEVSC As New EnumVehicleSubCategory

            If strCategoryCode.Trim.ToUpper = "PC" Then
                arlItem = objEVSC.GetSubOfPCList()
            ElseIf strCategoryCode.Trim.ToUpper = "CV" Then
                arlItem = objEVSC.GetSubOfCVList()
            ElseIf strCategoryCode.Trim.ToUpper = "LCV" Then
                arlItem = objEVSC.GetSubOfLCVList()
            End If
            ddlSubCategory.Items.Clear()
            ddlSubCategory.Items.Add(New ListItem("Silahkan pilih", -1))
            If Not IsNothing(arlItem) Then
                For Each li As ListItem In arlItem
                    ddlSubCategory.Items.Add(li)
                Next
            End If
        End Sub
        Public Shared Sub SetVehicleSubCategoryCriterias(ByVal ddlSubCategory As DropDownList, ByVal strCategoryCode As String, ByRef crts As CriteriaComposite, ByVal strDomainName As String)
            Dim strSQLValues As String = ""
            Dim objEVSC As New EnumVehicleSubCategory
            Dim i As Integer
            Dim str() As String

            strDomainName = strDomainName.Trim.ToUpper
            If strCategoryCode.Trim.ToUpper = "CV" Then
                strSQLValues = objEVSC.GetSubOfCVSQLValue(ddlSubCategory.SelectedValue)
            ElseIf strCategoryCode.Trim.ToUpper = "LCV" Then
                strSQLValues = objEVSC.GetSubOfLCVSQLValue(ddlSubCategory.SelectedValue)
            ElseIf strCategoryCode.Trim.ToUpper = "PC" Then
                strSQLValues = objEVSC.GetSubOfPCSQLValue(ddlSubCategory.SelectedValue)
            End If
            str = strSQLValues.Split(";")
            If str.Length = 1 Then
                If strDomainName = "VechileColor".ToUpper Then
                    crts.opAnd(New Criteria(GetType(VechileColor), "VechileType.Description", MatchType.[Partial], str(0)))
                ElseIf strDomainName = "VechileType".ToUpper Then
                    crts.opAnd(New Criteria(GetType(VechileType), "Description", MatchType.[Partial], str(0)))
                ElseIf strDomainName = "Price".ToUpper Then
                    crts.opAnd(New Criteria(GetType(Price), "VechileColor.VechileType.Description", MatchType.[Partial], str(0)))
                ElseIf strDomainName = "PKHeader".ToUpper Then
                    crts.opAnd(New Criteria(GetType(PKHeader), "VechileColor.VechileType.Description", MatchType.[Partial], str(0)))
                ElseIf strDomainName = "PKProductionPlan".ToUpper Then
                    crts.opAnd(New Criteria(GetType(PKProductionPlan), "VechileColor.VechileType.Description", MatchType.[Partial], str(0)))
                ElseIf strDomainName = "PKDetail".ToUpper Then
                    crts.opAnd(New Criteria(GetType(PKDetail), "VechileColor.VechileType.Description", MatchType.[Partial], str(0)))
                End If
            Else
                For i = 0 To str.Length - 1
                    If strDomainName = "VechileColor".ToUpper Then
                        'crts.opAnd(New Criteria(GetType(VechileColor), "VechileType.Description", MatchType.Partial, str(0)))
                        If i = 0 Then
                            crts.opAnd(New Criteria(GetType(VechileColor), "VechileType.Description", MatchType.[Partial], str(i)), "(", True)
                        ElseIf i = str.Length - 1 Then
                            crts.opOr(New Criteria(GetType(VechileColor), "VechileType.Description", MatchType.[Partial], str(i)), ")", False)
                        Else
                            crts.opOr(New Criteria(GetType(VechileColor), "VechileType.Description", MatchType.[Partial], str(i)))
                        End If
                    ElseIf strDomainName = "VechileType".ToUpper Then
                        'crts.opAnd(New Criteria(GetType(VechileType), "Description", MatchType.Partial, str(0)))
                        If i = 0 Then
                            crts.opAnd(New Criteria(GetType(VechileType), "Description", MatchType.[Partial], str(i)), "(", True)
                        ElseIf i = str.Length - 1 Then
                            crts.opOr(New Criteria(GetType(VechileType), "Description", MatchType.[Partial], str(i)), ")", False)
                        Else
                            crts.opOr(New Criteria(GetType(VechileType), "Description", MatchType.[Partial], str(i)))
                        End If
                    ElseIf strDomainName = "Price".ToUpper Then
                        'crts.opAnd(New Criteria(GetType(Price), "VechileColor.VechileType.Description", MatchType.Partial, str(0)))
                        If i = 0 Then
                            crts.opAnd(New Criteria(GetType(Price), "VechileColor.VechileType.Description", MatchType.[Partial], str(i)), "(", True)
                        ElseIf i = str.Length - 1 Then
                            crts.opOr(New Criteria(GetType(Price), "VechileColor.VechileType.Description", MatchType.[Partial], str(i)), ")", False)
                        Else
                            crts.opOr(New Criteria(GetType(Price), "VechileColor.VechileType.Description", MatchType.[Partial], str(i)))
                        End If
                    ElseIf strDomainName = "PKProductionPlan".ToUpper Then
                        'crts.opAnd(New Criteria(GetType(PKProductionPlan), "VechileColor.VechileType.Description", MatchType.Partial, str(0)))
                        If i = 0 Then
                            crts.opAnd(New Criteria(GetType(PKProductionPlan), "VechileColor.VechileType.Description", MatchType.[Partial], str(i)), "(", True)
                        ElseIf i = str.Length - 1 Then
                            crts.opOr(New Criteria(GetType(PKProductionPlan), "VechileColor.VechileType.Description", MatchType.[Partial], str(i)), ")", False)
                        Else
                            crts.opOr(New Criteria(GetType(PKProductionPlan), "VechileColor.VechileType.Description", MatchType.[Partial], str(i)))
                        End If
                    ElseIf strDomainName = "PKDetail".ToUpper Then
                        'crts.opAnd(New Criteria(GetType(PKDetail), "VechileColor.VechileType.Description", MatchType.Partial, str(0)))
                        If i = 0 Then
                            crts.opAnd(New Criteria(GetType(PKDetail), "VechileColor.VechileType.Description", MatchType.[Partial], str(i)), "(", True)
                        ElseIf i = str.Length - 1 Then
                            crts.opOr(New Criteria(GetType(PKDetail), "VechileColor.VechileType.Description", MatchType.[Partial], str(i)), ")", False)
                        Else
                            crts.opOr(New Criteria(GetType(PKDetail), "VechileColor.VechileType.Description", MatchType.[Partial], str(i)))
                        End If
                    End If
                Next
            End If

        End Sub
#End Region
        'Additional Property

        Public Shared Function GetNamaKhususEvalTraining(ByVal _classId As String, ByVal _trCourseEvaluationId As Integer) As String


            Dim _trCourseNumEvalFacade As New TrClassNumEvaluationFacade(New GenericPrincipal(New GenericIdentity("asp"), Nothing))
            Dim _lst As New ArrayList
            Dim crtParam As CriteriaComposite

            crtParam = New CriteriaComposite(New Criteria(GetType(TrClassNumEvaluation), "TrCourseEvaluation", MatchType.Exact, _trCourseEvaluationId))
            crtParam.opAnd(New Criteria(GetType(TrClassNumEvaluation), "TrClass", MatchType.Exact, _classId))

            '_lst = DoLoadArray(GetType(TrClassNumEvaluation).ToString, crtParam)
            _lst = _trCourseNumEvalFacade.Retrieve(crtParam)

            If (_lst.Count > 0) Then
                Return CType(_lst(0), TrClassNumEvaluation).SpecialName()
            Else
                Return ""
            End If


        End Function

        Public Shared Function GetColumnIndexOfDTG(ByRef dtg As DataGrid, ByVal HeaderText As String) As Integer
            For i As Integer = 0 To dtg.Columns.Count - 1
                If dtg.Columns(i).HeaderText.Trim.ToUpper = HeaderText.Trim.ToUpper Then Return i
            Next
            Return -1
        End Function

        Public Shared Sub GetKTPAndPhone(ByVal oCR As CustomerRequest, ByRef NOKTP As String, ByRef NOTELP As String)
            NOKTP = ""
            NOTELP = ""
            For Each oCRP As CustomerRequestProfile In oCR.CustomerRequestProfiles
                If oCRP.ProfileHeader.Code.Trim.ToUpper = "NOKTP" Then
                    NOKTP = oCRP.ProfileValue
                ElseIf oCRP.ProfileHeader.Code.Trim.ToUpper = "NOTELP" Then
                    NOTELP = oCRP.ProfileValue
                End If
            Next
        End Sub


#Region "DNet Date Operation"

        Public Shared Function AddNWorkingDay(ByVal StateDate As Date, ByVal nAdded As Integer, Optional ByVal IsBackWard As Boolean = False) As Date
            'it has the same logic in Facade.PO.POHeaderFacade.AddNWorkingDay
            Dim objNHFac As NationalHolidayFacade = New NationalHolidayFacade((New System.Security.Principal.GenericPrincipal(New GenericIdentity("asp"), Nothing)))
            Dim crtNH As CriteriaComposite
            Dim rslDate As Date
            Dim IsHoliday As Boolean = True
            Dim arlNH As ArrayList = New ArrayList
            Dim i As Integer = 0

            rslDate = StateDate
            For i = 1 To Math.Abs(nAdded)
                rslDate = rslDate.AddDays(IIf(IsBackWard, -1, 1))
                IsHoliday = True
                While IsHoliday = True
                    crtNH = New CriteriaComposite(New Criteria(GetType(NationalHoliday), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    'crtNH.opAnd(New Criteria(GetType(NationalHoliday), "HolidayDateTime", MatchType.Exact, rslDate))
                    crtNH.opAnd(New Criteria(GetType(NationalHoliday), "HolidayDate", MatchType.Exact, rslDate.Day))
                    crtNH.opAnd(New Criteria(GetType(NationalHoliday), "HolidayMonth", MatchType.Exact, rslDate.Month))
                    crtNH.opAnd(New Criteria(GetType(NationalHoliday), "HolidayYear", MatchType.Exact, rslDate.Year))
                    arlNH = objNHFac.Retrieve(crtNH)
                    If arlNH.Count < 1 Then
                        IsHoliday = False
                    Else
                        rslDate = rslDate.AddDays(IIf(IsBackWard = False, 1, -1))
                    End If
                End While
            Next
            Return rslDate
        End Function

#End Region

#Region "IndentPartEquipment-EmailRecepient"


        Public Shared Function SetIndentPartEmailRecipient(ByVal GroupType As Integer, ByRef sTo As String, ByRef sCC As String, ByRef objDealer As Dealer) As String
            Dim objEUFac As EquipUserFacade = New EquipUserFacade(New GenericPrincipal(New GenericIdentity("asp"), Nothing))
            Dim crtEU As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EquipUser), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim arlEU As New ArrayList

            sTo = KTB.DNet.Lib.WebConfig.GetValue("EmailIndentRecipient")
            sCC = KTB.DNet.Lib.WebConfig.GetValue("EmailIndentRecipientCC")

            crtEU.opAnd(New Criteria(GetType(EquipUser), "UserName", MatchType.Exact, objDealer.DealerCode))
            crtEU.opAnd(New Criteria(GetType(EquipUser), "GroupType", MatchType.Exact, CType(GroupType, Short)))
            crtEU.opAnd(New Criteria(GetType(EquipUser), "Tipe", MatchType.Exact, CType(EquipUser.EquipUserTipe.TO_SENT, Integer).ToString))
            arlEU = objEUFac.Retrieve(crtEU)
            'If arlEU.Count > 0 Then sTo = CType(arlEU(0), EquipUser).Email
            If arlEU.Count > 0 Then sTo = ""
            For Each oEU As EquipUser In arlEU
                sTo &= IIf(sTo.Trim = "", "", ";") & oEU.Email
            Next

            crtEU = New CriteriaComposite(New Criteria(GetType(EquipUser), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crtEU.opAnd(New Criteria(GetType(EquipUser), "UserName", MatchType.NotInSet, "(select distinct(d.DealerCode) from Dealer d where d.RowStatus=0)"))
            crtEU.opAnd(New Criteria(GetType(EquipUser), "GroupType", MatchType.Exact, CType(GroupType, Short)))
            crtEU.opAnd(New Criteria(GetType(EquipUser), "Tipe", MatchType.Exact, CType(EquipUser.EquipUserTipe.TO_SENT, Integer).ToString))
            crtEU.opAnd(New Criteria(GetType(EquipUser), "Email", MatchType.EndsWith, "@ktb.co.id"))
            arlEU = objEUFac.Retrieve(crtEU)
            For Each oEU As EquipUser In arlEU
                sTo &= IIf(sTo.Trim = "", "", ";") & oEU.Email
            Next

            crtEU = New CriteriaComposite(New Criteria(GetType(EquipUser), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crtEU.opAnd(New Criteria(GetType(EquipUser), "UserName", MatchType.Exact, objDealer.DealerCode))
            crtEU.opAnd(New Criteria(GetType(EquipUser), "GroupType", MatchType.Exact, CType(GroupType, Short)))
            crtEU.opAnd(New Criteria(GetType(EquipUser), "Tipe", MatchType.Exact, CType(EquipUser.EquipUserTipe.CC_TO, Integer).ToString))
            arlEU = objEUFac.Retrieve(crtEU)
            If arlEU.Count > 0 Then sCC = CType(arlEU(0), EquipUser).Email
            crtEU = New CriteriaComposite(New Criteria(GetType(EquipUser), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crtEU.opAnd(New Criteria(GetType(EquipUser), "UserName", MatchType.NotInSet, "(select distinct(d.DealerCode) from Dealer d where RowStatus=0)"))
            crtEU.opAnd(New Criteria(GetType(EquipUser), "GroupType", MatchType.Exact, CType(GroupType, Short)))
            crtEU.opAnd(New Criteria(GetType(EquipUser), "Tipe", MatchType.Exact, CType(EquipUser.EquipUserTipe.CC_TO, Integer).ToString))
            crtEU.opAnd(New Criteria(GetType(EquipUser), "Email", MatchType.[Partial], "@ktb.co.id"))
            arlEU = objEUFac.Retrieve(crtEU)
            For Each oEU As EquipUser In arlEU
                sCC &= IIf(sCC.Trim = "", "", ";") & oEU.Email
            Next
        End Function


#End Region

#Region "Redemption"

        Public Shared Function GetMaxContractDateOfRedemption(ByVal PeriodMonth As Integer, ByVal PeriodYear As Integer) As Date
            Dim dtContract As Date = DateSerial(1990, 1, 1)
            Dim arlRC As New ArrayList
            Dim objRCFac As RedemptionCeilingFacade = New RedemptionCeilingFacade((New System.Security.Principal.GenericPrincipal(New GenericIdentity("dnet"), Nothing)))
            Dim crtRC As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(RedemptionCeiling), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            crtRC.opAnd(New Criteria(GetType(RedemptionCeiling), "PeriodMonth", MatchType.Exact, PeriodMonth))
            crtRC.opAnd(New Criteria(GetType(RedemptionCeiling), "PeriodYear", MatchType.Exact, PeriodYear))

            arlRC = objRCFac.Retrieve(crtRC)
            If arlRC.Count > 0 Then
                dtContract = CType(arlRC(0), RedemptionCeiling).MaxContractDate
            End If
            Return dtContract
        End Function

#End Region

#Region "Factoring"

        Public Shared Function GetTransControlStatus(ByRef oDealer As Dealer, ByVal TransKind As Integer) As Boolean
            'Dim oTC As TransactionControl = New DealerFacade(User).RetrieveTransactionControl(oDealer.ID, EnumDealerTransType.DealerTransKind.FilterPengajuanPO )
            Dim oTC As TransactionControl = New DealerFacade((New System.Security.Principal.GenericPrincipal(New GenericIdentity("dnet"), Nothing))).RetrieveTransactionControl(oDealer.ID, TransKind)
            If Not IsNothing(oTC) AndAlso oTC.ID > 0 AndAlso oTC.Status = 1 Then
                Return True
            End If
            Return False
        End Function

        Public Shared Function IsEnoughForFactoring(ByVal PC As ProductCategory, ByVal POID As Integer, ByVal TotalPO As Decimal, ByVal CreditAccount As String, Optional ByVal IsAfterSaving As Boolean = False, Optional ByRef AvailableCeiling As Decimal = 0 _
         , Optional ByRef Ceiling As Decimal = 0 _
         , Optional ByRef Proposed As Decimal = 0 _
         , Optional ByRef Liquified As Decimal = 0 _
         , Optional ByRef Outstanding As Decimal = 0) As Boolean
            Dim oFMFac As FactoringMasterFacade = New FactoringMasterFacade((New System.Security.Principal.GenericPrincipal(New GenericIdentity("dnet"), Nothing)))
            Dim aFacComp As ArrayList = oFMFac.GetCeilingComponent(PC, CreditAccount)
            Dim AvFactoring As Decimal = oFMFac.GetAvailableCeiling(PC, CreditAccount, aFacComp(0) - aFacComp(1), aFacComp(2), aFacComp(3), aFacComp(4))
            Ceiling = aFacComp(0) - aFacComp(1)
            Proposed = aFacComp(3)
            Liquified = aFacComp(4)
            Outstanding = aFacComp(2)
            'Ceiling As Decimal, ByVal Outstanding As Decimal, ByVal ProposedPO As Decimal, ByVal LiquifiedPO
            AvailableCeiling = AvFactoring
            If IsAfterSaving = False Then
                If TotalPO > AvFactoring Then
                    MessageBox.Show("Total PO melebihi Ceiling yang tersedia")
                    Return False
                Else
                    Return True
                End If
            Else
                Dim oPOInDB As POHeader = New POHeaderFacade(Nothing).Retrieve(POID)
                Dim TotalInDB As Decimal = 0

                If Not IsNothing(oPOInDB) AndAlso oPOInDB.ID > 0 Then
                    TotalInDB = oPOInDB.TotalHarga()
                    AvFactoring = AvFactoring + TotalInDB
                    If TotalPO > AvFactoring Then
                        Return False
                        'after returning false, this PO should be deleted
                    Else
                        Return True
                    End If
                Else
                    Return False
                End If
            End If
        End Function

        Public Shared Function IsCeilingEnough(ByVal PC As ProductCategory, ByVal POID As Integer, ByVal ReqDelDate As Date _
            , ByVal TotalPO As Decimal _
            , ByVal CreditAccount As String _
            , ByVal PaymentType As Short _
            , Optional ByVal IsAfterSaving As Boolean = False _
            , Optional ByRef Ceiling As Decimal = 0 _
            , Optional ByRef Proposed As Decimal = 0 _
            , Optional ByRef Liquified As Decimal = 0 _
            , Optional ByRef Outstanding As Decimal = 0 _
            , Optional ByRef TodaysAvCeiling As Decimal = 0 _
            , Optional ByRef TomorrowAvCeiling As Decimal = 0 _
            , Optional ByRef AvCeiling As Decimal = 0) As Boolean

            Dim oSCFac As New sp_CeilingFacade((New System.Security.Principal.GenericPrincipal(New GenericIdentity("dnet"), Nothing)))
            Dim aSCs As ArrayList
            Dim oSC As sp_Ceiling
            'Dim AvCeiling As Decimal
            Dim IsEnough As Boolean = True
            Dim oSCPOFac As New sp_CeilingPOFacade((New System.Security.Principal.GenericPrincipal(New GenericIdentity("dnet"), Nothing)))
            Dim aTemps As ArrayList
            Dim tmpLiquified As Decimal = 0, tmpProposed As Decimal = 0
            Dim FocusedDate As Date = DateSerial(Now.Year, Now.Month, Now.Day)

            If PaymentType = enumPaymentType.PaymentType.COD Then
                aSCs = oSCFac.RetrieveFromSP(PC, FocusedDate, FocusedDate, CreditAccount, PaymentType)
                If aSCs.Count < 1 Then
                    Return False
                End If
                For Each oSCTemp As sp_Ceiling In aSCs
                    If oSCTemp.PaymentType = PaymentType Then
                        oSC = oSCTemp
                        Exit For
                    End If
                Next
                Ceiling = oSC.Ceiling
                aTemps = oSCPOFac.RetrieveFromSP(PC, CreditAccount, PaymentType, enumCeilingType.CeilingType.Outstanding, FocusedDate, FocusedDate, FocusedDate)
                Outstanding = 0
                For Each oSCPO As sp_CeilingPO In aTemps
                    Outstanding += oSCPO.TotalDetail
                Next
                TodaysAvCeiling = Ceiling - Outstanding
                While (FocusedDate <= ReqDelDate)
                    GetProposedAndLiquified(PC, CreditAccount, PaymentType, FocusedDate, tmpProposed, tmpLiquified)
                    TodaysAvCeiling = TodaysAvCeiling - tmpProposed + tmpLiquified
                    If FocusedDate = ReqDelDate Then
                        Proposed = tmpProposed
                        Liquified = tmpLiquified
                        Exit While
                    End If
                    FocusedDate = AddNWorkingDay(FocusedDate, 1)
                End While
                AvCeiling = TodaysAvCeiling
                If PaymentType = CType(enumPaymentType.PaymentType.COD, Short) Then

                    CommonFunction.GetProposedAndLiquified(PC, CreditAccount, PaymentType, AddNWorkingDay(FocusedDate, 1), tmpProposed, tmpLiquified)
                    TomorrowAvCeiling = TodaysAvCeiling - tmpProposed + tmpLiquified
                    If TodaysAvCeiling <= TomorrowAvCeiling Then
                        AvCeiling = TodaysAvCeiling
                    Else
                        AvCeiling = TomorrowAvCeiling
                    End If
                End If
            ElseIf PaymentType = enumPaymentType.PaymentType.TOP Then
                aSCs = oSCFac.RetrieveFromSP(PC, FocusedDate, ReqDelDate, CreditAccount, PaymentType)
                If aSCs.Count < 1 Then
                    Return False
                End If
                For Each oSCTemp As sp_Ceiling In aSCs
                    If oSCTemp.PaymentType = PaymentType Then
                        oSC = oSCTemp
                        Exit For
                    End If
                Next
                Ceiling = oSC.Ceiling
                Outstanding = oSC.OutStanding
                Proposed = oSC.ProposedPO
                Liquified = oSC.LiquifiedPO
                TodaysAvCeiling = Ceiling - Outstanding - Proposed + Liquified
                TomorrowAvCeiling = TodaysAvCeiling
                AvCeiling = TodaysAvCeiling
            End If

            If Not IsAfterSaving Then
                If TotalPO > AvCeiling Then IsEnough = False
            Else
                Dim oPOInDB As POHeader = New POHeaderFacade(Nothing).Retrieve(POID)
                Dim TotalInDB As Decimal = 0
                If Not IsNothing(oPOInDB) AndAlso oPOInDB.ID > 0 Then
                    TotalInDB = oPOInDB.TotalPODetail()
                    If TotalPO > (AvCeiling + TotalInDB) Then IsEnough = False
                Else
                    IsEnough = False
                End If
            End If
            Return IsEnough
        End Function

        Public Shared Sub GetProposedAndLiquified(ByVal PC As ProductCategory, ByVal CreditAccount As String, ByVal PaymentType As enumPaymentType.PaymentType, ByVal FocusedDate As Date, ByRef Proposed As Decimal, ByRef Liquified As Decimal)
            Dim oSCPOFac As New sp_CeilingPOFacade((New System.Security.Principal.GenericPrincipal(New GenericIdentity("dnet"), Nothing)))
            Dim aTemps As ArrayList

            aTemps = oSCPOFac.RetrieveFromSP(PC, CreditAccount, PaymentType, enumCeilingType.CeilingType.ProposedPO, Now, FocusedDate, FocusedDate)
            Proposed = 0
            For Each oSCPO As sp_CeilingPO In aTemps
                Proposed += oSCPO.TotalDetail
            Next
            aTemps = oSCPOFac.RetrieveFromSP(PC, CreditAccount, PaymentType, enumCeilingType.CeilingType.LiquifiedPO, Now, FocusedDate, FocusedDate)
            Liquified = 0
            For Each oSCPO As sp_CeilingPO In aTemps
                Liquified += oSCPO.TotalDetail
            Next
        End Sub


#End Region

        Public Shared Sub DownloadDTGToExcel(ByRef oPage As Page, ByRef dtg As DataGrid, ByVal FileName As String)
            Dim oSW As New StringWriter
            Dim oHTW As New HtmlTextWriter(oSW)

            If Not FileName.EndsWith(".xls") Then FileName = FileName & ".xls"
            With HttpContext.Current.Response
                .Clear()
                .Buffer = True
                .ContentType = "application/vnd.ms-excel"
                .AddHeader("content-disposition", "attachment;filename=" & FileName)
                .Charset = ""
                oPage.EnableViewState = False
                dtg.RenderControl(oHTW)
                .Write(oSW.ToString)
                .End()
            End With

        End Sub

        Public Shared Sub OpenCallCenter(ByRef oPage As Web.UI.Page, ByVal sUrl As String)
            Dim oUI As UserInfo = CType(HttpContext.Current.Session("LOGINUSERINFO"), UserInfo)
            Dim oUIFac As New UserInfoFacade((New System.Security.Principal.GenericPrincipal(New GenericIdentity("dnet"), Nothing)))
            Dim sKey As String
            Dim rnd As New Random

            sKey = rnd.Next(10000, 99999).ToString & rnd.Next(10000, 99999)

            oUI.CCKey = sKey 'Temporary->next will be store in new field (not UserInfo.Handphone)
            If oUIFac.Update(oUI) > 0 Then
                sUrl = sUrl & "?UID=" & oUI.ID.ToString & "&UKey=" & sKey
                'HttpContext.Current.Response.Redirect(sUrl)
                oPage.RegisterStartupScript("OpenWindow", "<script>window.open('" & sUrl & "','_blank', 'fullscreen=no,titlebar=no,personalbar=no,toolbar=no,status=1,menubar=no,scrollbars=yes,resizable=yes,directories=no,location=no');</script>")
            Else
                oPage.RegisterClientScriptBlock("CallbackScript", "<script language=""JavaScript"" type=""text/javascript"" >alert('Buka Menu Call Center Gagal');</script>")
            End If

        End Sub


        Public Shared Function GetPOCair(ByVal CreditAccount As String, ByVal PaymentType As enumPaymentType.PaymentType, ByVal StartDate As Date, ByVal EndDate As Date) As Decimal
            'Doesnt Have Gyro
            Dim crtPOH As CriteriaComposite '(New Criteria(GetType(POHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim arlPOH As ArrayList
            Dim Total As Decimal = 0
            Dim objPOHFac As New POHeaderFacade((New System.Security.Principal.GenericPrincipal(New GenericIdentity("dnet"), Nothing)))
            Dim Sql As String = ""

            crtPOH = New CriteriaComposite(New Criteria(GetType(POHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crtPOH.opAnd(New Criteria(GetType(POHeader), "Status", MatchType.InSet, "(0,2,4,6,8)"), "(", True) 'LookUp.ArrayStatusPO
            Sql = "(select count(id) from DailyPayment dp where dp.RowStatus=0 and dp.POID=POHeader.ID and dp.RejectStatus=0 and dp.IsReversed=0 and dp.IsCleared=0 and dp.PaymentPurposeID in (3,6) and dp.Status=" & CType(EnumPaymentStatus.PaymentStatus.Selesai, Short) & " )"
            crtPOH.opAnd(New Criteria(GetType(POHeader), "RowStatus", MatchType.Exact, Sql), ")", False)
            crtPOH.opAnd(New Criteria(GetType(POHeader), "ContractHeader.Dealer.CreditAccount", MatchType.Exact, CreditAccount))
            crtPOH.opAnd(New Criteria(GetType(POHeader), "TermOfPayment.PaymentType", MatchType.Exact, PaymentType))
            crtPOH.opAnd(New Criteria(GetType(POHeader), "ReqAllocationDateTime", MatchType.GreaterOrEqual, StartDate))
            crtPOH.opAnd(New Criteria(GetType(POHeader), "ReqAllocationDateTime", MatchType.LesserOrEqual, EndDate))
            crtPOH.opAnd(New Criteria(GetType(POHeader), "IsFactoring", MatchType.No, 1))
            arlPOH = objPOHFac.Retrieve(crtPOH)
            'temp without aggregate
            For Each oPOH As POHeader In arlPOH
                Total += oPOH.TotalPODetail
            Next
            'Having Gyro
            Dim cDP As New CriteriaComposite(New Criteria(GetType(DailyPayment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim aDP As New Aggregate(GetType(DailyPayment), "Amount", AggregateType.Sum)
            Dim oDPFac As New DailyPaymentFacade((New System.Security.Principal.GenericPrincipal(New GenericIdentity("dnet"), Nothing)))
            Dim TotWithGyro As Decimal = 0

            cDP.opAnd(New Criteria(GetType(DailyPayment), "POHeader.IsFactoring", MatchType.Exact, "0"))
            cDP.opAnd(New Criteria(GetType(DailyPayment), "RejectStatus", MatchType.Exact, "0"))
            cDP.opAnd(New Criteria(GetType(DailyPayment), "IsCleared", MatchType.Exact, "0"))
            cDP.opAnd(New Criteria(GetType(DailyPayment), "IsReversed", MatchType.Exact, "0"))
            cDP.opAnd(New Criteria(GetType(DailyPayment), "EffectiveDate", MatchType.GreaterOrEqual, StartDate))
            cDP.opAnd(New Criteria(GetType(DailyPayment), "EffectiveDate", MatchType.Lesser, EndDate.AddDays(1)))
            cDP.opAnd(New Criteria(GetType(DailyPayment), "POHeader.Dealer.CreditAccount", MatchType.Exact, CreditAccount))
            cDP.opAnd(New Criteria(GetType(DailyPayment), "POHeader.TermOfPayment.PaymentType", MatchType.Exact, CType(PaymentType, Short)))
            cDP.opAnd(New Criteria(GetType(DailyPayment), "POHeader.Status", MatchType.Exact, "8"))
            'cDP.opAnd(New Criteria(GetType(DailyPayment), "", MatchType.Exact, ""))
            Try
                TotWithGyro = oDPFac.RetrieveAggregate(cDP, aDP)
            Catch ex As Exception
                TotWithGyro = 0
            End Try
            Total += TotWithGyro

            Return Total
        End Function
    End Class

End Namespace


