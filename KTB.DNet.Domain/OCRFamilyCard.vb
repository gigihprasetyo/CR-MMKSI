#region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : OCRFamilyCard Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2021 
'// ---------------------
'// $History      : $
'// Generated on 7/21/2021 - 11:07:15 AM
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
	<Serializable(), TableInfo("OCRFamilyCard")> _
	public class OCRFamilyCard
		inherits DomainObject
		
		#region "Constructors/Destructors/Finalizers"
		
		Public Sub New()
        End Sub
        
		public sub new(byval ID as integer )
			_iD = ID
		end sub
		
		#end region
		
		#region "Private Variables"
		
		private _iD as integer 		
        Private _sPKHeaderID As Integer
		private _type as short 		
		private _imageID as string = String.Empty 		
		private _imagePath as string = String.Empty 		
		private _fCRowNo as integer 		
		private _name as string = String.Empty 		
		private _identityNumber as integer 		
		private _gender as string = String.Empty 		
		private _placeOfBirth as string = String.Empty 		
		private _dateOfBirth as DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) 		
		private _religion as string = String.Empty 		
		private _education as string = String.Empty 		
		private _occupation as string = String.Empty 		
		private _bloodType as string = String.Empty 		
		private _totalChars as integer 		
		private _confidenceChars as integer 		
		private _processingTime as double 		
		private _errors as string = String.Empty 		
		private _jSon as string = String.Empty 		
		private _rowStatus as short 		
		private _createdBy as string = String.Empty 		
		private _createdTime as DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) 		
		private _lastUpdatedBy as string = String.Empty 		
		private _lastUpdateTime as DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) 		
		
        Private _sPKHeader As SPKHeader

		
		#end region
		
		#region "Public Properties"
		
		<ColumnInfo("ID","{0}")> _
		public property ID as integer
			get
				return _iD
			end get
			set(byval value as integer)
				_iD= value
			end set
		end property
		
        <ColumnInfo("SPKHeaderID", "{0}"), _
        RelationInfo("SPKHeader", "ID", "OCRFamilyCard", "SPKHeaderID")> _
        Public Property SPKHeaderID As SPKHeader
            Get
                Try
                    If Not IsNothing(Me._sPKHeader) AndAlso (Not Me._sPKHeader.IsLoaded) Then

                        Me._sPKHeader = CType(DoLoad(GetType(SPKHeader).ToString(), _sPKHeader.ID), SPKHeader)
                        Me._sPKHeader.MarkLoaded()

                    End If

                    Return Me._sPKHeader

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As SPKHeader)

                Me._sPKHeader = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._sPKHeader.MarkLoaded()
                End If
            End Set
        End Property


		<ColumnInfo("Type","{0}")> _
		public property Type as short
			get
				return _type
			end get
			set(byval value as short)
				_type= value
			end set
		end property
		

		<ColumnInfo("ImageID","'{0}'")> _
		public property ImageID as string
			get
				return _imageID
			end get
			set(byval value as string)
				_imageID= value
			end set
		end property
		

		<ColumnInfo("ImagePath","'{0}'")> _
		public property ImagePath as string
			get
				return _imagePath
			end get
			set(byval value as string)
				_imagePath= value
			end set
		end property
		

		<ColumnInfo("FCRowNo","{0}")> _
		public property FCRowNo as integer
			get
				return _fCRowNo
			end get
			set(byval value as integer)
				_fCRowNo= value
			end set
		end property
		

		<ColumnInfo("Name","'{0}'")> _
		public property Name as string
			get
				return _name
			end get
			set(byval value as string)
				_name= value
			end set
		end property
		

		<ColumnInfo("IdentityNumber","{0}")> _
		public property IdentityNumber as integer
			get
				return _identityNumber
			end get
			set(byval value as integer)
				_identityNumber= value
			end set
		end property
		

		<ColumnInfo("Gender","'{0}'")> _
		public property Gender as string
			get
				return _gender
			end get
			set(byval value as string)
				_gender= value
			end set
		end property
		

		<ColumnInfo("PlaceOfBirth","'{0}'")> _
		public property PlaceOfBirth as string
			get
				return _placeOfBirth
			end get
			set(byval value as string)
				_placeOfBirth= value
			end set
		end property
		

		<ColumnInfo("DateOfBirth","'{0:yyyy/MM/dd}'")> _
		public property DateOfBirth as DateTime
			get
				return _dateOfBirth
			end get
			set(byval value as DateTime)
				_dateOfBirth= value
			end set
		end property
		

		<ColumnInfo("Religion","'{0}'")> _
		public property Religion as string
			get
				return _religion
			end get
			set(byval value as string)
				_religion= value
			end set
		end property
		

		<ColumnInfo("Education","'{0}'")> _
		public property Education as string
			get
				return _education
			end get
			set(byval value as string)
				_education= value
			end set
		end property
		

		<ColumnInfo("Occupation","'{0}'")> _
		public property Occupation as string
			get
				return _occupation
			end get
			set(byval value as string)
				_occupation= value
			end set
		end property
		

		<ColumnInfo("BloodType","'{0}'")> _
		public property BloodType as string
			get
				return _bloodType
			end get
			set(byval value as string)
				_bloodType= value
			end set
		end property
		

		<ColumnInfo("TotalChars","{0}")> _
		public property TotalChars as integer
			get
				return _totalChars
			end get
			set(byval value as integer)
				_totalChars= value
			end set
		end property
		

		<ColumnInfo("ConfidenceChars","{0}")> _
		public property ConfidenceChars as integer
			get
				return _confidenceChars
			end get
			set(byval value as integer)
				_confidenceChars= value
			end set
		end property
		

		<ColumnInfo("ProcessingTime","#,##0")> _
		public property ProcessingTime as double
			get
				return _processingTime
			end get
			set(byval value as double)
				_processingTime= value
			end set
		end property
		

		<ColumnInfo("Errors","'{0}'")> _
		public property Errors as string
			get
				return _errors
			end get
			set(byval value as string)
				_errors= value
			end set
		end property
		

		<ColumnInfo("JSon","'{0}'")> _
		public property JSon as string
			get
				return _jSon
			end get
			set(byval value as string)
				_jSon= value
			end set
		end property
		

		<ColumnInfo("RowStatus","{0}")> _
		public property RowStatus as short
			get
				return _rowStatus
			end get
			set(byval value as short)
				_rowStatus= value
			end set
		end property
		

		<ColumnInfo("CreatedBy","'{0}'")> _
		public property CreatedBy as string
			get
				return _createdBy
			end get
			set(byval value as string)
				_createdBy= value
			end set
		end property
		

		<ColumnInfo("CreatedTime","'{0:yyyy/MM/dd HH:mm:ss}'")> _
		public property CreatedTime as DateTime
			get
				return _createdTime
			end get
			set(byval value as DateTime)
				_createdTime= value
			end set
		end property
		

		<ColumnInfo("LastUpdatedBy","'{0}'")> _
		public property LastUpdatedBy as string
			get
				return _lastUpdatedBy
			end get
			set(byval value as string)
				_lastUpdatedBy= value
			end set
		end property
		

		<ColumnInfo("LastUpdateTime","'{0:yyyy/MM/dd HH:mm:ss}'")> _
		public property LastUpdateTime as DateTime
			get
				return _lastUpdateTime
			end get
			set(byval value as DateTime)
				_lastUpdateTime= value
			end set
		end property
		
		
		

		#end region
		
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
