
#region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : AlertConfirmation Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2010 
'// ---------------------
'// $History      : $
'// Generated on 8/9/2010 - 10:10:36 AM
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
	<Serializable(), TableInfo("AlertConfirmation")> _
	public class AlertConfirmation
		inherits DomainObject
		
		#region "Constructors/Destructors/Finalizers"
		
		Public Sub New()
        End Sub
        
		public sub new(byval ID as short )
			_iD = ID
		end sub
		
		#end region
		
		#region "Private Variables"
		
		private _iD as short 		
		private _confirmDate as DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) 		
        Private _status As Short
        Private _rowStatus As Short
		private _createdTime as DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) 		
		private _createdBy as string = String.Empty 		
		private _lastUpdateTime as DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) 		
		private _lastUpdateBy as string = String.Empty 		
		
		private _alertMaster as AlertMaster
		private _dealerID as short 


		
		#end region
		
		#region "Public Properties"
		
		<ColumnInfo("ID","{0}")> _
		public property ID as short
			get
				return _iD
			end get
			set(byval value as short)
				_iD= value
			end set
		end property
		

		<ColumnInfo("ConfirmDate","'{0:yyyy/MM/dd}'")> _
		public property ConfirmDate as DateTime
			get
				return _confirmDate
			end get
			set(byval value as DateTime)
				_confirmDate= new DateTime(value.Year,value.Month,value.Day)
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


        <ColumnInfo("CreatedTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property CreatedTime() As DateTime
            Get
                Return _createdTime
            End Get
            Set(ByVal value As DateTime)
                _createdTime = value
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


        <ColumnInfo("LastUpdateTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property LastUpdateTime() As DateTime
            Get
                Return _lastUpdateTime
            End Get
            Set(ByVal value As DateTime)
                _lastUpdateTime = value
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


        <ColumnInfo("AlertMasterID", "{0}"), _
        RelationInfo("AlertMaster", "ID", "AlertConfirmation", "AlertMasterID")> _
        Public Property AlertMaster() As AlertMaster
            Get
                Try
                    If Not IsNothing(Me._alertMaster) AndAlso (Not Me._alertMaster.IsLoaded) Then

                        Me._alertMaster = CType(DoLoad(GetType(AlertMaster).ToString(), _alertMaster.ID), AlertMaster)
                        Me._alertMaster.MarkLoaded()

                    End If

                    Return Me._alertMaster

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As AlertMaster)

                Me._alertMaster = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._alertMaster.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("DealerID", "{0}")> _
        Public Property DealerID() As Short

            Get
                Return _dealerID
            End Get
            Set(ByVal value As Short)
                _dealerID = value
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

