
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : OCRIdentity Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 02/14/2018 - 4:48:24 PM
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
    <Serializable(), TableInfo("OCRIdentity")> _
    Public Class OCRIdentity
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
        Private _spkCustomerID As Integer
        Private _spkDetailCustomerID As Integer
        Private _type As Short
        Private _imageID As String = String.Empty
        Private _imagePath As String = String.Empty
        Private _identityNumber As String = String.Empty
        Private _name As String = String.Empty
        Private _birthOfDate As String = String.Empty
        Private _birthOfPlace As String = String.Empty
        Private _gender As String = String.Empty
        Private _height As String = String.Empty
        Private _address As String = String.Empty
        Private _rtRw As String = String.Empty
        Private _district As String = String.Empty
        Private _subdistrict As String = String.Empty
        Private _regency As String = String.Empty
        Private _province As String = String.Empty
        Private _religion As String = String.Empty
        Private _maritalStatus As String = String.Empty
        Private _occupation As String = String.Empty
        Private _citizenship As String = String.Empty
        Private _validUntil As String = String.Empty
        Private _polda As String = String.Empty
        Private _totalChars As Integer
        Private _confidenceChars As Integer
        Private _processingTime As Double
        Private _errors As String = String.Empty
        Private _jSon As String = String.Empty
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _SPKCustomer As SPKCustomer
        Private _SPKDetailCustomer As SPKDetailCustomer


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

        <ColumnInfo("SPKCustomerID", "{0}"), _
    RelationInfo("SPKCustomer", "ID", "OCRIdentity", "SPKCustomerID")> _
        Public Property SPKCustomer() As SPKCustomer
            Get
                Try
                    If Not IsNothing(Me._SPKCustomer) AndAlso (Not Me._SPKCustomer.IsLoaded) Then

                        Me._SPKCustomer = CType(DoLoad(GetType(SPKCustomer).ToString(), _SPKCustomer.ID), SPKCustomer)
                        Me._SPKCustomer.MarkLoaded()

                    End If

                    Return Me._SPKCustomer

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As SPKCustomer)

                Me._SPKCustomer = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._SPKCustomer.MarkLoaded()
                End If
            End Set
        End Property


        <ColumnInfo("SPKCustomerID", "{0}"), _
  RelationInfo("SPKDetailCustomer", "ID", "OCRIdentity", "SPKCustomerID")> _
        Public Property SPKDetailCustomer() As SPKDetailCustomer
            Get
                Try
                    If Not IsNothing(Me._SPKCustomer) AndAlso (Not Me._SPKCustomer.IsLoaded) Then

                        Me._SPKDetailCustomer = CType(DoLoad(GetType(SPKDetailCustomer).ToString(), _SPKDetailCustomer.ID), SPKDetailCustomer)
                        Me._SPKDetailCustomer.MarkLoaded()

                    End If

                    Return Me._SPKDetailCustomer

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As SPKDetailCustomer)

                Me._SPKDetailCustomer = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._SPKDetailCustomer.MarkLoaded()
                End If
            End Set
        End Property



        <ColumnInfo("SPKCustomerID", "{0}")> _
        Public Property SPKCustomerID As Integer
            Get
                Return _spkCustomerID
            End Get
            Set(ByVal value As Integer)
                _spkCustomerID = value
            End Set
        End Property


        <ColumnInfo("SPKDetailCustomerID", "{0}")> _
        Public Property SPKDetailCustomerID As Integer
            Get
                Return _spkDetailCustomerID
            End Get
            Set(ByVal value As Integer)
                _spkDetailCustomerID = value
            End Set
        End Property




        <ColumnInfo("Type", "{0}")> _
        Public Property Type As Short
            Get
                Return _type
            End Get
            Set(ByVal value As Short)
                _type = value
            End Set
        End Property


        <ColumnInfo("ImageID", "'{0}'")> _
        Public Property ImageID As String
            Get
                Return _imageID
            End Get
            Set(ByVal value As String)
                _imageID = value
            End Set
        End Property


        <ColumnInfo("ImagePath", "'{0}'")> _
        Public Property ImagePath As String
            Get
                Return _imagePath
            End Get
            Set(ByVal value As String)
                _imagePath = value
            End Set
        End Property


        <ColumnInfo("IdentityNumber", "'{0}'")> _
        Public Property IdentityNumber As String
            Get
                Return _identityNumber
            End Get
            Set(ByVal value As String)
                _identityNumber = value
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


        <ColumnInfo("BirthOfDate", "'{0}'")> _
        Public Property BirthOfDate As String
            Get
                Return _birthOfDate
            End Get
            Set(ByVal value As String)
                _birthOfDate = value
            End Set
        End Property


        <ColumnInfo("BirthOfPlace", "'{0}'")> _
        Public Property BirthOfPlace As String
            Get
                Return _birthOfPlace
            End Get
            Set(ByVal value As String)
                _birthOfPlace = value
            End Set
        End Property


        <ColumnInfo("Gender", "'{0}'")> _
        Public Property Gender As String
            Get
                Return _gender
            End Get
            Set(ByVal value As String)
                _gender = value
            End Set
        End Property


        <ColumnInfo("Height", "'{0}'")> _
        Public Property Height As String
            Get
                Return _height
            End Get
            Set(ByVal value As String)
                _height = value
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


        <ColumnInfo("RtRw", "'{0}'")> _
        Public Property RtRw As String
            Get
                Return _rtRw
            End Get
            Set(ByVal value As String)
                _rtRw = value
            End Set
        End Property


        <ColumnInfo("District", "'{0}'")> _
        Public Property District As String
            Get
                Return _district
            End Get
            Set(ByVal value As String)
                _district = value
            End Set
        End Property


        <ColumnInfo("Subdistrict", "'{0}'")> _
        Public Property Subdistrict As String
            Get
                Return _subdistrict
            End Get
            Set(ByVal value As String)
                _subdistrict = value
            End Set
        End Property


        <ColumnInfo("Regency", "'{0}'")> _
        Public Property Regency As String
            Get
                Return _regency
            End Get
            Set(ByVal value As String)
                _regency = value
            End Set
        End Property


        <ColumnInfo("Province", "'{0}'")> _
        Public Property Province As String
            Get
                Return _province
            End Get
            Set(ByVal value As String)
                _province = value
            End Set
        End Property


        <ColumnInfo("Religion", "'{0}'")> _
        Public Property Religion As String
            Get
                Return _religion
            End Get
            Set(ByVal value As String)
                _religion = value
            End Set
        End Property


        <ColumnInfo("MaritalStatus", "'{0}'")> _
        Public Property MaritalStatus As String
            Get
                Return _maritalStatus
            End Get
            Set(ByVal value As String)
                _maritalStatus = value
            End Set
        End Property


        <ColumnInfo("Occupation", "'{0}'")> _
        Public Property Occupation As String
            Get
                Return _occupation
            End Get
            Set(ByVal value As String)
                _occupation = value
            End Set
        End Property


        <ColumnInfo("Citizenship", "'{0}'")> _
        Public Property Citizenship As String
            Get
                Return _citizenship
            End Get
            Set(ByVal value As String)
                _citizenship = value
            End Set
        End Property


        <ColumnInfo("ValidUntil", "'{0}'")> _
        Public Property ValidUntil As String
            Get
                Return _validUntil
            End Get
            Set(ByVal value As String)
                _validUntil = value
            End Set
        End Property


        <ColumnInfo("Polda", "'{0}'")> _
        Public Property Polda As String
            Get
                Return _polda
            End Get
            Set(ByVal value As String)
                _polda = value
            End Set
        End Property


        <ColumnInfo("TotalChars", "{0}")> _
        Public Property TotalChars As Integer
            Get
                Return _totalChars
            End Get
            Set(ByVal value As Integer)
                _totalChars = value
            End Set
        End Property


        <ColumnInfo("ConfidenceChars", "{0}")> _
        Public Property ConfidenceChars As Integer
            Get
                Return _confidenceChars
            End Get
            Set(ByVal value As Integer)
                _confidenceChars = value
            End Set
        End Property


        <ColumnInfo("ProcessingTime", "#,##0")> _
        Public Property ProcessingTime As Double
            Get
                Return _processingTime
            End Get
            Set(ByVal value As Double)
                _processingTime = value
            End Set
        End Property


        <ColumnInfo("Errors", "'{0}'")> _
        Public Property Errors As String
            Get
                Return _errors
            End Get
            Set(ByVal value As String)
                _errors = value
            End Set
        End Property


        <ColumnInfo("JSon", "'{0}'")> _
        Public Property JSon As String
            Get
                Return _jSon
            End Get
            Set(ByVal value As String)
                _jSon = value
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

