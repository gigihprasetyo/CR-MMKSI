
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : BabitMasterAdditionalAllocation Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2019 
'// ---------------------
'// $History      : $
'// Generated on 21/08/2019 - 9:19:00
'//
'// ===========================================================================	
#End Region


#Region ".NET Base Class Namespace Imports"
Imports System
Imports System.Data
Imports System.Collections
#End Region

#Region "Custom Namespace Imports"
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports Microsoft.Practices.EnterpriseLibrary.Logging
Imports KTB.DNet.DataMapper.Framework
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
#End Region

Namespace KTB.DNet.DataMapper

    Public Class BabitMasterAdditionalAllocationMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertBabitMasterAdditionalAllocation"
        Private m_UpdateStatement As String = "up_UpdateBabitMasterAdditionalAllocation"
        Private m_RetrieveStatement As String = "up_RetrieveBabitMasterAdditionalAllocation"
        Private m_RetrieveListStatement As String = "up_RetrieveBabitMasterAdditionalAllocationList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteBabitMasterAdditionalAllocation"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim babitMasterAdditionalAllocation As BabitMasterAdditionalAllocation = Nothing
            While dr.Read

                babitMasterAdditionalAllocation = Me.CreateObject(dr)

            End While

            Return babitMasterAdditionalAllocation

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim babitMasterAdditionalAllocationList As ArrayList = New ArrayList

            While dr.Read
                Dim babitMasterAdditionalAllocation As BabitMasterAdditionalAllocation = Me.CreateObject(dr)
                babitMasterAdditionalAllocationList.Add(babitMasterAdditionalAllocation)
            End While

            Return babitMasterAdditionalAllocationList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim babitMasterAdditionalAllocation As BabitMasterAdditionalAllocation = CType(obj, BabitMasterAdditionalAllocation)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, babitMasterAdditionalAllocation.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim babitMasterAdditionalAllocation As BabitMasterAdditionalAllocation = CType(obj, BabitMasterAdditionalAllocation)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@AdditionalPrice", DbType.Currency, babitMasterAdditionalAllocation.AdditionalPrice)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, babitMasterAdditionalAllocation.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, babitMasterAdditionalAllocation.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(babitMasterAdditionalAllocation.Dealer))
            DbCommandWrapper.AddInParameter("@DealerBranchID", DbType.Int32, Me.GetRefObject(babitMasterAdditionalAllocation.DealerBranch))
            DbCommandWrapper.AddInParameter("@BabitMasterPriceID", DbType.Int32, Me.GetRefObject(babitMasterAdditionalAllocation.BabitMasterPrice))

            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetNewID(ByVal dbCommandWrapper As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper) As Integer

            Return CType(dbCommandWrapper.GetParameterValue("@ID"), Integer)

        End Function

        Protected Overrides Function GetPagingRetrieveCommand() As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_PagingQuery)
            DbCommandWrapper.AddInParameter("@Table", DbType.String, m_TableName)
            DbCommandWrapper.AddInParameter("@PK", DbType.String, "ID")

            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetRetrieveCommand() As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DinamicQuery)
            DbCommandWrapper.AddInParameter("@sqlQuery", DbType.String, "SELECT " + m_TableName + ".* FROM " + m_TableName)

            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetRetrieveListParameter() As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_RetrieveListStatement)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetRetrieveParameter(ByVal id As Integer) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_RetrieveStatement)
            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, id)

            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetUpdateParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim babitMasterAdditionalAllocation As BabitMasterAdditionalAllocation = CType(obj, BabitMasterAdditionalAllocation)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, babitMasterAdditionalAllocation.ID)
            DbCommandWrapper.AddInParameter("@AdditionalPrice", DbType.Currency, babitMasterAdditionalAllocation.AdditionalPrice)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, babitMasterAdditionalAllocation.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, babitMasterAdditionalAllocation.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(babitMasterAdditionalAllocation.Dealer))
            DbCommandWrapper.AddInParameter("@DealerBranchID", DbType.Int32, Me.GetRefObject(babitMasterAdditionalAllocation.DealerBranch))
            DbCommandWrapper.AddInParameter("@BabitMasterPriceID", DbType.Int32, Me.GetRefObject(babitMasterAdditionalAllocation.BabitMasterPrice))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As BabitMasterAdditionalAllocation

            Dim babitMasterAdditionalAllocation As BabitMasterAdditionalAllocation = New BabitMasterAdditionalAllocation

            babitMasterAdditionalAllocation.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("AdditionalPrice")) Then babitMasterAdditionalAllocation.AdditionalPrice = CType(dr("AdditionalPrice"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then babitMasterAdditionalAllocation.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then babitMasterAdditionalAllocation.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then babitMasterAdditionalAllocation.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then babitMasterAdditionalAllocation.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then babitMasterAdditionalAllocation.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                babitMasterAdditionalAllocation.Dealer = New Dealer(CType(dr("DealerID"), Short))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("DealerBranchID")) Then
                babitMasterAdditionalAllocation.DealerBranch = New DealerBranch(CType(dr("DealerBranchID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("BabitMasterPriceID")) Then
                babitMasterAdditionalAllocation.BabitMasterPrice = New BabitMasterPrice(CType(dr("BabitMasterPriceID"), Short))
            End If

            Return babitMasterAdditionalAllocation

        End Function

        Private Sub SetTableName()

            If Not (GetType(BabitMasterAdditionalAllocation) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(BabitMasterAdditionalAllocation), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(BabitMasterAdditionalAllocation).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

