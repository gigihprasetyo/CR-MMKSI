
#region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : MSPDM Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 1/11/2018 - 2:53:53 PM
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
	<Serializable(), TableInfo("MSPDM")> _
	public class MSPDM
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
		private _debitMemoNo as string = String.Empty 		
		private _amount as decimal 		
		private _docType as string = String.Empty 		
		private _documentDate as DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) 		
		private _fileName as string = String.Empty 		
		private _rowStatus as short 		
		private _createdBy as string = String.Empty 		
		private _createdTime as DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) 		
		private _lastUpdateBy as string = String.Empty 		
		private _lastUpdateTime as DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) 		
		
        Private _objMSPDC As MSPDC

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
		

        <ColumnInfo("MSPDCID", "{0}"), _
        RelationInfo("MSPDC", "ID", "MSPDM", "MSPDCID")> _
        Public Property MSPDC() As MSPDC
            Get
                Try
                    If Not IsNothing(Me._objMSPDC) AndAlso (Not Me._objMSPDC.IsLoaded) Then
                        Me._objMSPDC = CType(DoLoad(GetType(MSPDC).ToString(), _objMSPDC.ID), MSPDC)
                        Me._objMSPDC.MarkLoaded()
                    End If

                    Return Me._objMSPDC
                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If
                End Try

                Return Nothing
            End Get

            Set(ByVal value As MSPDC)

                Me._objMSPDC = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._objMSPDC.MarkLoaded()
                End If
            End Set
        End Property
		

		<ColumnInfo("DebitMemoNo","'{0}'")> _
		public property DebitMemoNo as string
			get
				return _debitMemoNo
			end get
			set(byval value as string)
				_debitMemoNo= value
			end set
		end property
		

		<ColumnInfo("Amount","{0}")> _
		public property Amount as decimal
			get
				return _amount
			end get
			set(byval value as decimal)
				_amount= value
			end set
		end property
		

		<ColumnInfo("DocType","'{0}'")> _
		public property DocType as string
			get
				return _docType
			end get
			set(byval value as string)
				_docType= value
			end set
		end property
		

		<ColumnInfo("DocumentDate","'{0:yyyy/MM/dd}'")> _
		public property DocumentDate as DateTime
			get
				return _documentDate
			end get
			set(byval value as DateTime)
				_documentDate= new DateTime(value.Year,value.Month,value.Day)
			end set
		end property
		

		<ColumnInfo("FileName","'{0}'")> _
		public property FileName as string
			get
				return _fileName
			end get
			set(byval value as string)
				_fileName= value
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
		

		<ColumnInfo("LastUpdateBy","'{0}'")> _
		public property LastUpdateBy as string
			get
				return _lastUpdateBy
			end get
			set(byval value as string)
				_lastUpdateBy= value
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

