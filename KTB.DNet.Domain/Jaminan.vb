
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : Jaminan Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2010 
'// ---------------------
'// $History      : $
'// Generated on 1/7/2010 - 2:11:16 PM
'//
'// ===========================================================================	
#End Region

#Region ".NET Base Class Namespace Imports"
Imports System
Imports System.Collections
#End Region

#Region "Custom Namespace Imports"
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports KTB.DNet.Domain.Search
#End Region

Namespace KTB.DNet.Domain
    <Serializable(), TableInfo("Jaminan")> _
    Public Class Jaminan
        Inherits DomainObject

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
        End Sub

        Public Sub New(ByVal ID As Integer)
            _iD = ID
        End Sub

#End Region

#Region "Private Variables"

        Private _iD As Integer
        Private _dealerCode As String = String.Empty
        Private _depositInfo As String = String.Empty
        Private _description As String = String.Empty
        Private _validFrom As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _validTo As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _attachment As String = String.Empty
        Private _status As Short
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _jaminanDetails As ArrayList = New ArrayList
        Private _dealerCodes As New ArrayList

#End Region

#Region "Public Properties"

        <ColumnInfo("ID", "{0}")> _
        Public Property ID() As Integer
            Get
                Return _iD
            End Get
            Set(ByVal value As Integer)
                _iD = value
            End Set
        End Property


        <ColumnInfo("DealerCode", "'{0}'")> _
        Public Property DealerCode() As String
            Get
                Return _dealerCode
            End Get
            Set(ByVal value As String)
                _dealerCode = value
            End Set
        End Property


        <ColumnInfo("DepositInfo", "'{0}'")> _
        Public Property DepositInfo() As String
            Get
                Return _depositInfo
            End Get
            Set(ByVal value As String)
                _depositInfo = value
            End Set
        End Property


        <ColumnInfo("Description", "'{0}'")> _
        Public Property Description() As String
            Get
                Return _description
            End Get
            Set(ByVal value As String)
                _description = value
            End Set
        End Property


        <ColumnInfo("ValidFrom", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property ValidFrom() As DateTime
            Get
                Return _validFrom
            End Get
            Set(ByVal value As DateTime)
                _validFrom = value
            End Set
        End Property


        <ColumnInfo("ValidTo", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property ValidTo() As DateTime
            Get
                Return _validTo
            End Get
            Set(ByVal value As DateTime)
                _validTo = value
            End Set
        End Property


        <ColumnInfo("Attachment", "'{0}'")> _
        Public Property Attachment() As String
            Get
                Return _attachment
            End Get
            Set(ByVal value As String)
                _attachment = value
            End Set
        End Property


        <ColumnInfo("Status", "{0}")> _
        Public Property Status() As Short
            Get
                Return _status
            End Get
            Set(ByVal value As Short)
                _status = value
            End Set
        End Property


        <ColumnInfo("RowStatus", "{0}")> _
        Public Property RowStatus() As Short
            Get
                Return _rowStatus
            End Get
            Set(ByVal value As Short)
                _rowStatus = value
            End Set
        End Property


        <ColumnInfo("CreatedBy", "'{0}'")> _
        Public Property CreatedBy() As String
            Get
                Return _createdBy
            End Get
            Set(ByVal value As String)
                _createdBy = value
            End Set
        End Property


        <ColumnInfo("CreatedTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property CreatedTime() As DateTime
            Get
                Return _createdTime
            End Get
            Set(ByVal value As DateTime)
                _createdTime = value
            End Set
        End Property


        <ColumnInfo("LastUpdateBy", "'{0}'")> _
        Public Property LastUpdateBy() As String
            Get
                Return _lastUpdateBy
            End Get
            Set(ByVal value As String)
                _lastUpdateBy = value
            End Set
        End Property


        <ColumnInfo("LastUpdateTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property LastUpdateTime() As DateTime
            Get
                Return _lastUpdateTime
            End Get
            Set(ByVal value As DateTime)
                _lastUpdateTime = value
            End Set
        End Property


        <RelationInfo("Jaminan", "ID", "JaminanDetail", "JaminanID")> _
        Public Property JaminanDetails() As System.Collections.ArrayList
            Get
                Try
                    If (Me._jaminanDetails.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(JaminanDetail), "Jaminan", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(JaminanDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._jaminanDetails = DoLoadArray(GetType(JaminanDetail).ToString, criterias)
                    End If

                    Return Me._jaminanDetails

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
            Set(ByVal Value As System.Collections.ArrayList)
                Me._jaminanDetails = Value
            End Set
        End Property

        Public ReadOnly Property DealerCodes() As System.Collections.ArrayList
            Get
                Try
                    Dim str() As String = Me.DealerCode.Split(";")
                    Me._dealerCodes.Clear()
                    For Each strCode As String In str
                        Me._dealerCodes.Add(strCode.Trim)
                    Next
                    Return Me._dealerCodes
                Catch ex As Exception
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property

        Public ReadOnly Property JaminanDetailIn(ByVal PeriodMonth As Integer, ByVal PeriodYear As Integer) As ArrayList
            Get
                Dim arlIn As New ArrayList
                For Each oJD As JaminanDetail In JaminanDetails
                    If oJD.PeriodMonth = PeriodMonth AndAlso oJD.PeriodYear = PeriodYear Then
                        arlIn.Add(oJD)
                    End If
                Next
                Return arlIn
            End Get
        End Property

        Public ReadOnly Property JaminanDetailOut(ByVal PeriodMonth As Integer, ByVal PeriodYear As Integer) As ArrayList
            Get
                Dim arlOut As New ArrayList
                For Each oJD As JaminanDetail In JaminanDetails
                    If oJD.PeriodMonth <> PeriodMonth OrElse oJD.PeriodYear <> PeriodYear Then
                        arlOut.Add(oJD)
                    End If
                Next
                Return arlOut
            End Get
        End Property
#End Region

#Region "Generated Method"
        Public Function GetStrDate(ByVal dateInput As DateTime, ByVal dateFormat As String) As String
            If dateInput = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) Then
                Return ""
            Else
                Return Format(dateInput, dateFormat)
            End If
        End Function
#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

