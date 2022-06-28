
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : Mitrais Team
'// PURPOSE       : DealerBranch Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 12/11/2018 - 18:24:26
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
    <Serializable(), TableInfo("DealerBranch")> _
    Public Class DealerBranch
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
        Private _name As String = String.Empty
        Private _status As String = String.Empty
        Private _address As String = String.Empty
        Private _zipCode As String = String.Empty
        Private _phone As String = String.Empty
        Private _fax As String = String.Empty
        Private _website As String = String.Empty
        Private _email As String = String.Empty
        Private _typeBranch As String = String.Empty
        Private _dealerBranchCode As String = String.Empty
        Private _term1 As String = String.Empty
        Private _term2 As String = String.Empty
        Private _mainAreaID As Integer
        Private _branchAssignmentNo As String = String.Empty
        Private _branchAssignmentDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _salesUnitFlag As String = String.Empty
        Private _serviceFlag As String = String.Empty
        Private _sparepartFlag As String = String.Empty
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _dealer As Dealer
        Private _city As City
        Private _province As Province

        Private _area1 As Area1
        Private _area2 As Area2
        Private _mainArea As MainArea
        Private _arrDBArea As ArrayList = New ArrayList




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


        <ColumnInfo("DealerID", "{0}"), _
        RelationInfo("Dealer", "ID", "DealerBranch", "DealerID")> _
        Public Property Dealer() As Dealer
            Get
                Try
                    If Not IsNothing(Me._dealer) AndAlso (Not Me._dealer.IsLoaded) Then

                        Me._dealer = CType(DoLoad(GetType(Dealer).ToString(), _dealer.ID), Dealer)
                        Me._dealer.MarkLoaded()

                    End If

                    Return Me._dealer

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As Dealer)

                Me._dealer = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._dealer.MarkLoaded()
                End If
            End Set
        End Property


        <ColumnInfo("Name", "'{0}'")> _
        Public Property Name As String
            Get
                Return _name
            End Get
            Set(ByVal value As String)
                _name = value
            End Set
        End Property

        <ColumnInfo("Status", "'{0}'")> _
        Public Property Status As String
            Get
                Return _status
            End Get
            Set(ByVal value As String)
                _status = value
            End Set
        End Property


        <ColumnInfo("Address", "'{0}'")> _
        Public Property Address As String
            Get
                Return _address
            End Get
            Set(ByVal value As String)
                _address = value
            End Set
        End Property


        <ColumnInfo("ZipCode", "'{0}'")> _
        Public Property ZipCode As String
            Get
                Return _zipCode
            End Get
            Set(ByVal value As String)
                _zipCode = value
            End Set
        End Property


        <ColumnInfo("Phone", "'{0}'")> _
        Public Property Phone As String
            Get
                Return _phone
            End Get
            Set(ByVal value As String)
                _phone = value
            End Set
        End Property


        <ColumnInfo("Fax", "'{0}'")> _
        Public Property Fax As String
            Get
                Return _fax
            End Get
            Set(ByVal value As String)
                _fax = value
            End Set
        End Property


        <ColumnInfo("Website", "'{0}'")> _
        Public Property Website As String
            Get
                Return _website
            End Get
            Set(ByVal value As String)
                _website = value
            End Set
        End Property


        <ColumnInfo("Email", "'{0}'")> _
        Public Property Email As String
            Get
                Return _email
            End Get
            Set(ByVal value As String)
                _email = value
            End Set
        End Property


        <ColumnInfo("TypeBranch", "'{0}'")> _
        Public Property TypeBranch As String
            Get
                Return _typeBranch
            End Get
            Set(ByVal value As String)
                _typeBranch = value
            End Set
        End Property


        <ColumnInfo("DealerBranchCode", "'{0}'")> _
        Public Property DealerBranchCode As String
            Get
                Return _dealerBranchCode
            End Get
            Set(ByVal value As String)
                _dealerBranchCode = value
            End Set
        End Property


        <ColumnInfo("Term1", "'{0}'")> _
        Public Property Term1 As String
            Get
                Return _term1
            End Get
            Set(ByVal value As String)
                _term1 = value
            End Set
        End Property


        <ColumnInfo("Term2", "'{0}'")> _
        Public Property Term2 As String
            Get
                Return _term2
            End Get
            Set(ByVal value As String)
                _term2 = value
            End Set
        End Property

        <RelationInfo("DealerBranch", "ID", "DealerBranchBusinessArea", "DealerBranch.ID")> _
        Public ReadOnly Property DealerBranchBusinesAreas() As System.Collections.ArrayList
            Get
                Try
                    If (Me._arrDBArea.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(DealerBranchBusinessArea), "DealerBranch.ID", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(DealerBranchBusinessArea), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._arrDBArea = DoLoadArray(GetType(DealerBranchBusinessArea).ToString, criterias)
                    End If

                    Return Me._arrDBArea

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property

        <ColumnInfo("BranchAssignmentNo", "'{0}'")> _
        Public Property BranchAssignmentNo As String
            Get
                Return _branchAssignmentNo
            End Get
            Set(ByVal value As String)
                _branchAssignmentNo = value
            End Set
        End Property


        <ColumnInfo("BranchAssignmentDate", "'{0:yyyy/MM/dd}'")> _
        Public Property BranchAssignmentDate As DateTime
            Get
                Return _branchAssignmentDate
            End Get
            Set(ByVal value As DateTime)
                _branchAssignmentDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("SalesUnitFlag", "'{0}'")> _
        Public Property SalesUnitFlag As String
            Get
                Return _salesUnitFlag
            End Get
            Set(ByVal value As String)
                _salesUnitFlag = value
            End Set
        End Property


        <ColumnInfo("ServiceFlag", "'{0}'")> _
        Public Property ServiceFlag As String
            Get
                Return _serviceFlag
            End Get
            Set(ByVal value As String)
                _serviceFlag = value
            End Set
        End Property


        <ColumnInfo("SparepartFlag", "'{0}'")> _
        Public Property SparepartFlag As String
            Get
                Return _sparepartFlag
            End Get
            Set(ByVal value As String)
                _sparepartFlag = value
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


        <ColumnInfo("CityID", "{0}"), _
        RelationInfo("City", "ID", "DealerBranch", "CityID")> _
        Public Property City As City
            Get
                Try
                    If Not IsNothing(Me._city) AndAlso (Not Me._city.IsLoaded) Then

                        Me._city = CType(DoLoad(GetType(City).ToString(), _city.ID), City)
                        Me._city.MarkLoaded()

                    End If

                    Return Me._city

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As City)

                Me._city = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._city.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("ProvinceID", "{0}"), _
        RelationInfo("Province", "ID", "DealerBranch", "ProvinceID")> _
        Public Property Province As Province
            Get
                Try
                    If Not IsNothing(Me._province) AndAlso (Not Me._province.IsLoaded) Then

                        Me._province = CType(DoLoad(GetType(Province).ToString(), _province.ID), Province)
                        Me._province.MarkLoaded()

                    End If

                    Return Me._province

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As Province)

                Me._province = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._province.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("Area1ID", "{0}"), _
        RelationInfo("Area1", "ID", "DealerBranch", "Area1ID")> _
        Public Property Area1 As Area1
            Get
                Try
                    If Not IsNothing(Me._area1) AndAlso (Not Me._area1.IsLoaded) Then

                        Me._area1 = CType(DoLoad(GetType(Area1).ToString(), _area1.ID), Area1)
                        Me._area1.MarkLoaded()

                    End If

                    Return Me._area1

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As Area1)

                Me._area1 = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._area1.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("Area2ID", "{0}"), _
        RelationInfo("Area2", "ID", "DealerBranch", "Area2ID")> _
        Public Property Area2 As Area2
            Get
                Try
                    If Not IsNothing(Me._area2) AndAlso (Not Me._area2.IsLoaded) Then

                        Me._area2 = CType(DoLoad(GetType(Area2).ToString(), _area2.ID), Area2)
                        Me._area2.MarkLoaded()

                    End If

                    Return Me._area2

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As Area2)

                Me._area2 = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._area2.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("MainAreaID", "{0}"), _
        RelationInfo("MainArea", "ID", "DealerBranch", "MainAreaID")> _
        Public Property MainArea As MainArea
            Get
                Try
                    If Not IsNothing(Me._mainArea) AndAlso (Not Me._mainArea.IsLoaded) Then

                        Me._mainArea = CType(DoLoad(GetType(MainArea).ToString(), _mainArea.ID), MainArea)
                        Me._mainArea.MarkLoaded()

                    End If

                    Return Me._mainArea

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As MainArea)

                Me._mainArea = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._mainArea.MarkLoaded()
                End If
            End Set
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

        Public Function extPhone() As String

            Try
                If Me._phone <> String.Empty Then
                    If Me._phone.Contains("-") Then

                        Return Me._phone.Split("-")(0).ToString()
                    End If

                End If
            Catch ex As Exception

            End Try


            Return String.Empty
        End Function


        Public Function noPhone() As String

            Try
                If Me._phone <> String.Empty Then
                    If Me._phone.Contains("-") Then

                        Return Me._phone.Split("-")(1).ToString()
                    End If

                End If
            Catch ex As Exception

            End Try


            Return Me._phone.Replace("-", "")
        End Function



        Public Function extFax() As String

            Try
                If Me._fax <> String.Empty Then
                    If Me._fax.Contains("-") Then

                        Return Me._fax.Split("-")(0).ToString()
                    End If

                End If
            Catch ex As Exception

            End Try


            Return String.Empty
        End Function


        Public Function nofax() As String

            Try
                If Me._fax <> String.Empty Then
                    If Me._fax.Contains("-") Then

                        Return Me._fax.Split("-")(1).ToString()
                    End If

                End If
            Catch ex As Exception

            End Try


            Return Me._fax.Replace("-", "")
        End Function
#End Region

    End Class
End Namespace

