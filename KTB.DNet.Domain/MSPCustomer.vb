
#region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : MSPCustomer Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2017 
'// ---------------------
'// $History      : $
'// Generated on 12/12/2017 - 1:49:16 PM
'//
'// ===========================================================================	
#end region

#region ".NET Base Class Namespace Imports"
imports System
imports System.Collections
#end region

#region "Custom Namespace Imports"
imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports KTB.DNet.Domain.Search
#end region

namespace KTB.DNet.Domain
	<Serializable(), TableInfo("MSPCustomer")> _
	public class MSPCustomer
		inherits DomainObject
		
		#region "Constructors/Destructors/Finalizers"
		
		Public Sub New()
        End Sub
        
		public sub new(byval ID as integer )
			_iD = ID
		end sub
		
		#end region
		
		#region "Private Variables"
		
        Private _iD As Integer		
		private _name1 as string = String.Empty 		
		private _name2 as string = String.Empty 		
		private _name3 as string = String.Empty 		
		private _alamat as string = String.Empty 		
		private _kelurahan as string = String.Empty 		
        Private _kecamatan As String = String.Empty
        Private _postalCode As String = String.Empty
		private _preArea as string = String.Empty 
		private _printRegion as string = String.Empty 		
		private _phoneNo as string = String.Empty 		
		private _email as string = String.Empty 		
        Private _attachment As String = String.Empty
        Private _ktpNo As String = String.Empty
		private _status as short 		
		private _deletionMark as short 		
        Private _completeName As String = String.Empty
        Private _age As Integer
		private _rowStatus as short 		
		private _createdBy as string = String.Empty 		
		private _createdTime as DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) 		
		private _lastUpdateBy as string = String.Empty 		
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _objProvince As Province
        Private _objCity As City
        Private _objRefCustomer As Customer
		
		#end region
		
#Region "Public Properties"

        <ColumnInfo("ProvinceID", "{0}"), _
        RelationInfo("Province", "ID", "MSPCustomer", "ProvinceID")> _
        Public Property Province() As Province
            Get
                Try
                    If Not IsNothing(Me._objProvince) AndAlso (Not Me._objProvince.IsLoaded) Then
                        Me._objProvince = CType(DoLoad(GetType(Province).ToString(), _objProvince.ID), Province)
                        Me._objProvince.MarkLoaded()
                    End If

                    Return Me._objProvince
                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If
                End Try

                Return Nothing
            End Get

            Set(ByVal value As Province)

                Me._objProvince = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._objProvince.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("RefCustomerID", "{0}"), _
        RelationInfo("Customer", "ID", "MSPCustomer", "RefCustomerID")> _
        Public Property RefCustomer() As Customer
            Get
                Try
                    If Not IsNothing(Me._objRefCustomer) AndAlso (Not Me._objRefCustomer.IsLoaded) Then
                        Me._objRefCustomer = CType(DoLoad(GetType(Customer).ToString(), _objRefCustomer.ID), Customer)
                        Me._objRefCustomer.MarkLoaded()
                    End If

                    Return Me._objRefCustomer
                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If
                End Try

                Return Nothing
            End Get

            Set(ByVal value As Customer)

                Me._objRefCustomer = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._objRefCustomer.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("CityID", "{0}"), _
        RelationInfo("City", "ID", "MSPCustomer", "CityID")> _
        Public Property City As City
            Get
                Try
                    If Not IsNothing(Me._objCity) AndAlso (Not Me._objCity.IsLoaded) Then
                        Me._objCity = CType(DoLoad(GetType(City).ToString(), _objCity.ID), City)
                        Me._objCity.MarkLoaded()
                    End If

                    Return Me._objCity

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If
                End Try

                Return Nothing
            End Get

            Set(ByVal value As City)

                Me._objCity = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._objCity.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("ID", "{0}")> _
        Public Property ID As Integer
            Get
                Return _iD
            End Get
            Set(ByVal value As Integer)
                _iD = value
            End Set
        End Property

        <ColumnInfo("Name1", "'{0}'")> _
        Public Property Name1 As String
            Get
                Return _name1
            End Get
            Set(ByVal value As String)
                _name1 = value
            End Set
        End Property


        <ColumnInfo("Name2", "'{0}'")> _
        Public Property Name2 As String
            Get
                Return _name2
            End Get
            Set(ByVal value As String)
                _name2 = value
            End Set
        End Property


        <ColumnInfo("Name3", "'{0}'")> _
        Public Property Name3 As String
            Get
                Return _name3
            End Get
            Set(ByVal value As String)
                _name3 = value
            End Set
        End Property


        <ColumnInfo("Alamat", "'{0}'")> _
        Public Property Alamat As String
            Get
                Return _alamat
            End Get
            Set(ByVal value As String)
                _alamat = value
            End Set
        End Property


        <ColumnInfo("Kelurahan", "'{0}'")> _
        Public Property Kelurahan As String
            Get
                Return _kelurahan
            End Get
            Set(ByVal value As String)
                _kelurahan = value
            End Set
        End Property


        <ColumnInfo("Kecamatan", "'{0}'")> _
        Public Property Kecamatan As String
            Get
                Return _kecamatan
            End Get
            Set(ByVal value As String)
                _kecamatan = value
            End Set
        End Property


        <ColumnInfo("PostalCode", "'{0}'")> _
        Public Property PostalCode As String
            Get
                Return _postalCode
            End Get
            Set(ByVal value As String)
                _postalCode = value
            End Set
        End Property


        <ColumnInfo("PreArea", "'{0}'")> _
        Public Property PreArea As String
            Get
                Return _preArea
            End Get
            Set(ByVal value As String)
                _preArea = value
            End Set
        End Property

        <ColumnInfo("PrintRegion", "'{0}'")> _
        Public Property PrintRegion As String
            Get
                Return _printRegion
            End Get
            Set(ByVal value As String)
                _printRegion = value
            End Set
        End Property


        <ColumnInfo("PhoneNo", "'{0}'")> _
        Public Property PhoneNo As String
            Get
                Return _phoneNo
            End Get
            Set(ByVal value As String)
                _phoneNo = value
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


        <ColumnInfo("Attachment", "'{0}'")> _
        Public Property Attachment As String
            Get
                Return _attachment
            End Get
            Set(ByVal value As String)
                _attachment = value
            End Set
        End Property

        <ColumnInfo("KTPNo", "'{0}'")> _
        Public Property KTPNo As String
            Get
                Return _ktpNo
            End Get
            Set(ByVal value As String)
                _ktpNo = value
            End Set
        End Property

        <ColumnInfo("Status", "{0}")> _
        Public Property Status As Short
            Get
                Return _status
            End Get
            Set(ByVal value As Short)
                _status = value
            End Set
        End Property


        <ColumnInfo("DeletionMark", "{0}")> _
        Public Property DeletionMark As Short
            Get
                Return _deletionMark
            End Get
            Set(ByVal value As Short)
                _deletionMark = value
            End Set
        End Property


        <ColumnInfo("CompleteName", "'{0}'")> _
        Public Property CompleteName As String
            Get
                Return _completeName
            End Get
            Set(ByVal value As String)
                _completeName = value
            End Set
        End Property

        <ColumnInfo("Age", "{0}")> _
        Public Property Age As Integer
            Get
                Return _age
            End Get
            Set(ByVal value As Integer)
                _age = value
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
		
	end class
end namespace

