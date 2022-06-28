
#region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : MSPExDebitCharge Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2020 
'// ---------------------
'// $History      : $
'// Generated on 11/10/2020 - 4:08:44 PM
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
	<Serializable(), TableInfo("MSPExDebitCharge")> _
	public class MSPExDebitCharge
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
		private _debitChargeNo as string = String.Empty 		
		private _amount as decimal 		
		private _tOP as string = String.Empty 		
		private _documentDate as DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) 		
		private _fileName as string = String.Empty 		
		private _rowstatus as short 		
		private _createdby as string = String.Empty 		
		private _createdtime as DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) 		
		private _lastUpdatedby as string = String.Empty 		
		private _lastUpdatedtime as DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) 		
		
        Private _mSPExRegistration As MSPExRegistration


		
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
		

		<ColumnInfo("DebitChargeNo","'{0}'")> _
		public property DebitChargeNo as string
			get
				return _debitChargeNo
			end get
			set(byval value as string)
				_debitChargeNo= value
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
		

		<ColumnInfo("TOP","'{0}'")> _
		public property TOP as string
			get
				return _tOP
			end get
			set(byval value as string)
				_tOP= value
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
		

        <ColumnInfo("RowStatus", "{0}")> _
        Public Property RowStatus As Short
            Get
                Return _rowstatus
            End Get
            Set(ByVal value As Short)
                _rowstatus = value
            End Set
        End Property
		

		<ColumnInfo("Createdby","'{0}'")> _
		public property Createdby as string
			get
				return _createdby
			end get
			set(byval value as string)
				_createdby= value
			end set
		end property
		

		<ColumnInfo("Createdtime","'{0:yyyy/MM/dd HH:mm:ss}'")> _
		public property Createdtime as DateTime
			get
				return _createdtime
			end get
			set(byval value as DateTime)
				_createdtime= value
			end set
		end property
		

		<ColumnInfo("LastUpdatedby","'{0}'")> _
		public property LastUpdatedby as string
			get
				return _lastUpdatedby
			end get
			set(byval value as string)
				_lastUpdatedby= value
			end set
		end property
		

		<ColumnInfo("LastUpdatedtime","'{0:yyyy/MM/dd HH:mm:ss}'")> _
		public property LastUpdatedtime as DateTime
			get
				return _lastUpdatedtime
			end get
			set(byval value as DateTime)
				_lastUpdatedtime= value
			end set
        End Property
		
        <ColumnInfo("MSPExRegistrationID", "{0}"), _
        RelationInfo("MSPExRegistration", "ID", "MSPExDebitCharge", "MSPExRegistrationID")> _
        Public Property MSPExRegistration As MSPExRegistration
            Get
                Try
                    If Not IsNothing(Me._mSPExRegistration) AndAlso (Not Me._mSPExRegistration.IsLoaded) Then

                        Me._mSPExRegistration = CType(DoLoad(GetType(MSPExRegistration).ToString(), _mSPExRegistration.ID), MSPExRegistration)
                        Me._mSPExRegistration.MarkLoaded()

                    End If

                    Return Me._mSPExRegistration

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As MSPExRegistration)

                Me._mSPExRegistration = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._mSPExRegistration.MarkLoaded()
                End If
            End Set
        End Property
		

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

