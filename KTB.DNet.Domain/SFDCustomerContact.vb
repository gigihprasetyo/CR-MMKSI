#region "Summary"
'// ===========================================================================
'// AUTHOR        : dnet
'// PURPOSE       : SFDCustomerContact Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2021 
'// ---------------------
'// $History      : $
'// Generated on 8/19/2021 - 4:33:15 PM
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
    <Serializable(), TableInfo("SFDCustomerContact")> _
    Public Class SFDCustomerContact
        Inherits DomainObject

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
        End Sub

        Public Sub New(ByVal ID As Long, SalesmanCode As String, HPNo As String)
            _iD = ID
            _salesmanCode = SalesmanCode
            _hPNo = HPNo
        End Sub

#End Region

#Region "Private Variables"

        Private _iD As Long
        Private _salesmanCode As String = String.Empty
        Private _firstName As String = String.Empty
        Private _lastName As String = String.Empty
        Private _hPNo As String = String.Empty
        Private _phoneType As Short
        Private _phone As String = String.Empty
        Private _email As String = String.Empty
        Private _gender As Byte
        Private _address As String = String.Empty
        Private _customerType As Short
        Private _leadSource As Short
        Private _notes As String = String.Empty
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _dealer As Dealer
        Private _sPKMasterCountryCodePhoneID As Integer
        Private _city As City



#End Region

#Region "Public Properties"

        <ColumnInfo("ID", "{0}")> _
        Public Property ID As Long
            Get
                Return _iD
            End Get
            Set(ByVal value As Long)
                _iD = value
            End Set
        End Property


        <ColumnInfo("SalesmanCode", "'{0}'")> _
        Public Property SalesmanCode As String
            Get
                Return _salesmanCode
            End Get
            Set(ByVal value As String)
                _salesmanCode = value
            End Set
        End Property


        <ColumnInfo("FirstName", "'{0}'")> _
        Public Property FirstName As String
            Get
                Return _firstName
            End Get
            Set(ByVal value As String)
                _firstName = value
            End Set
        End Property


        <ColumnInfo("LastName", "'{0}'")> _
        Public Property LastName As String
            Get
                Return _lastName
            End Get
            Set(ByVal value As String)
                _lastName = value
            End Set
        End Property


        <ColumnInfo("HPNo", "'{0}'")> _
        Public Property HPNo As String
            Get
                Return _hPNo
            End Get
            Set(ByVal value As String)
                _hPNo = value
            End Set
        End Property


        <ColumnInfo("PhoneType", "{0}")> _
        Public Property PhoneType As Short
            Get
                Return _phoneType
            End Get
            Set(ByVal value As Short)
                _phoneType = value
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


        <ColumnInfo("Email", "'{0}'")> _
        Public Property Email As String
            Get
                Return _email
            End Get
            Set(ByVal value As String)
                _email = value
            End Set
        End Property


        <ColumnInfo("Gender", "{0}")> _
        Public Property Gender As Byte
            Get
                Return _gender
            End Get
            Set(ByVal value As Byte)
                _gender = value
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


        <ColumnInfo("CustomerType", "{0}")> _
        Public Property CustomerType As Short
            Get
                Return _customerType
            End Get
            Set(ByVal value As Short)
                _customerType = value
            End Set
        End Property


        <ColumnInfo("LeadSource", "{0}")> _
        Public Property LeadSource As Short
            Get
                Return _leadSource
            End Get
            Set(ByVal value As Short)
                _leadSource = value
            End Set
        End Property


        <ColumnInfo("Notes", "'{0}'")> _
        Public Property Notes As String
            Get
                Return _notes
            End Get
            Set(ByVal value As String)
                _notes = value
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


        <ColumnInfo("DealerID", "{0}"), _
        RelationInfo("Dealer", "ID", "SFDCustomerContact", "DealerID")> _
        Public Property Dealer As Dealer
            Get
                Try
                    If Not isnothing(Me._dealer) AndAlso (Not Me._dealer.IsLoaded) Then

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
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._dealer.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("SPKMasterCountryCodePhoneID", "{0}")> _
        Public Property SPKMasterCountryCodePhoneID As Integer

            Get
                Return _sPKMasterCountryCodePhoneID
            End Get
            Set(ByVal value As Integer)
                _sPKMasterCountryCodePhoneID = value
            End Set
        End Property

        <ColumnInfo("CityID", "{0}"), _
        RelationInfo("City", "ID", "SFDCustomerContact", "CityID")> _
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
