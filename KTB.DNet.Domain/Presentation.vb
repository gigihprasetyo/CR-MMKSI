
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : Presentation Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2016 
'// ---------------------
'// $History      : $
'// Generated on 25/02/2016 - 10:58:26
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
    <Serializable(), TableInfo("Presentation")> _
    Public Class Presentation
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
        Private _title As String = String.Empty
        Private _description As String = String.Empty
        Private _validFrom As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _validTo As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _uniqueName As String = String.Empty
        Private _fileName As String = String.Empty
        Private _logoName As String = String.Empty
        Private _status As Integer
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)


        Private _presentationGroups As System.Collections.ArrayList = New System.Collections.ArrayList

#End Region

#Region "Public Properties"

        <ColumnInfo("ID", "{0}")> _
        Public Property ID As Integer
            Get
                Return _iD
            End Get
            Set(ByVal value As Integer)
                _iD = value
            End Set
        End Property


        <ColumnInfo("Title", "'{0}'")> _
        Public Property Title As String
            Get
                Return _title
            End Get
            Set(ByVal value As String)
                _title = value
            End Set
        End Property


        <ColumnInfo("Description", "'{0}'")> _
        Public Property Description As String
            Get
                Return _description
            End Get
            Set(ByVal value As String)
                _description = value
            End Set
        End Property


        <ColumnInfo("ValidFrom", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property ValidFrom As DateTime
            Get
                Return _validFrom
            End Get
            Set(ByVal value As DateTime)
                _validFrom = value
            End Set
        End Property


        <ColumnInfo("ValidTo", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property ValidTo As DateTime
            Get
                Return _validTo
            End Get
            Set(ByVal value As DateTime)
                _validTo = value
            End Set
        End Property


        <ColumnInfo("UniqueName", "'{0}'")> _
        Public Property UniqueName As String
            Get
                Return _uniqueName
            End Get
            Set(ByVal value As String)
                _uniqueName = value
            End Set
        End Property


        <ColumnInfo("FileName", "'{0}'")> _
        Public Property FileName As String
            Get
                Return _fileName
            End Get
            Set(ByVal value As String)
                _fileName = value
            End Set
        End Property


        <ColumnInfo("LogoName", "'{0}'")> _
        Public Property LogoName As String
            Get
                Return _logoName
            End Get
            Set(ByVal value As String)
                _logoName = value
            End Set
        End Property


        <ColumnInfo("Status", "{0}")> _
        Public Property Status() As Boolean
            Get
                Return IIf(_status = 1, True, False)
            End Get
            Set(ByVal value As Boolean)
                _status = IIf(value, 1, 0)
            End Set
        End Property


        <ColumnInfo("RowStatus", "{0}")> _
        Public Property RowStatus As Short
            Get
                Return _rowStatus
            End Get
            Set(ByVal value As Short)
                _rowStatus = value
            End Set
        End Property


        <ColumnInfo("CreatedBy", "'{0}'")> _
        Public Property CreatedBy As String
            Get
                Return _createdBy
            End Get
            Set(ByVal value As String)
                _createdBy = value
            End Set
        End Property


        <ColumnInfo("CreatedTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property CreatedTime As DateTime
            Get
                Return _createdTime
            End Get
            Set(ByVal value As DateTime)
                _createdTime = value
            End Set
        End Property


        <ColumnInfo("LastUpdateBy", "'{0}'")> _
        Public Property LastUpdateBy As String
            Get
                Return _lastUpdateBy
            End Get
            Set(ByVal value As String)
                _lastUpdateBy = value
            End Set
        End Property


        <ColumnInfo("LastUpdateTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property LastUpdateTime As DateTime
            Get
                Return _lastUpdateTime
            End Get
            Set(ByVal value As DateTime)
                _lastUpdateTime = value
            End Set
        End Property


        <RelationInfo("Presentation", "ID", "PresentationGroup", "PresentationID")> _
        Public Property PresentationGroup() As System.Collections.ArrayList
            Get
                Try
                    If (Me._presentationGroups.Count < 1 AndAlso Me.ID > 0) Then
                        Dim _criteria As Criteria = New Criteria(GetType(Presentation), "PresentationID", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(Presentation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._presentationGroups = DoLoadArray(GetType(Presentation).ToString, criterias)
                    End If

                    Return Me._presentationGroups

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
            Set(value As System.Collections.ArrayList)
                _presentationGroups = value
            End Set
        End Property

        Public ReadOnly Property Uploader As String
            Get
                If Me._createdBy.Trim() <> "" Then

                    Try
                        Dim Org As String = Left(Me._createdBy, 6)
                        Dim Usr As String = Me._createdBy.Substring(6)
                        Dim Objarr As ArrayList = New ArrayList

                        Dim criterias As New CriteriaComposite(New Criteria(GetType(UserInfo), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        criterias.opAnd(New Criteria(GetType(UserInfo), "UserName", MatchType.Exact, Usr))
                        criterias.opAnd(New Criteria(GetType(UserInfo), "Dealer.ID", MatchType.Exact, CInt(Org)))
                        Objarr = DoLoadArray(GetType(UserInfo).ToString, criterias)

                        If Not IsNothing(Objarr) AndAlso Objarr.Count > 0 Then
                            Return CType(Objarr(0), UserInfo).FirstName & "  " & CType(Objarr(0), UserInfo).LastName
                        End If
                    Catch ex As Exception
                        Return ex.Message
                    End Try
                    


                End If
                Return String.Empty
            End Get
        End Property


        Public ReadOnly Property Updater As String
            Get
                If Me._lastUpdateBy.Trim() <> "" Then

                    Try
                        Dim Org As String = Left(Me._lastUpdateBy, 6)
                        Dim Usr As String = Me._lastUpdateBy.Substring(6)
                        Dim Objarr As ArrayList = New ArrayList

                        Dim criterias As New CriteriaComposite(New Criteria(GetType(UserInfo), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        criterias.opAnd(New Criteria(GetType(UserInfo), "UserName", MatchType.Exact, Usr))
                        criterias.opAnd(New Criteria(GetType(UserInfo), "Dealer.ID", MatchType.Exact, CInt(Org)))
                        Objarr = DoLoadArray(GetType(UserInfo).ToString, criterias)

                        If Not IsNothing(Objarr) AndAlso Objarr.Count > 0 Then
                            Return CType(Objarr(0), UserInfo).FirstName & "  " & CType(Objarr(0), UserInfo).LastName
                        End If
                    Catch ex As Exception
                        Return ex.Message
                    End Try



                End If
                Return String.Empty
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

