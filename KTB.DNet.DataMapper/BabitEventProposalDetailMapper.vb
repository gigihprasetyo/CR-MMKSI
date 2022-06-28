
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : BabitEventProposalDetail Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2019 
'// ---------------------
'// $History      : $
'// Generated on 15/05/2019 - 7:57:06
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

    Public Class BabitEventProposalDetailMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertBabitEventProposalDetail"
        Private m_UpdateStatement As String = "up_UpdateBabitEventProposalDetail"
        Private m_RetrieveStatement As String = "up_RetrieveBabitEventProposalDetail"
        Private m_RetrieveListStatement As String = "up_RetrieveBabitEventProposalDetailList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteBabitEventProposalDetail"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim babitEventProposalDetail As BabitEventProposalDetail = Nothing
            While dr.Read

                babitEventProposalDetail = Me.CreateObject(dr)

            End While

            Return babitEventProposalDetail

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim babitEventProposalDetailList As ArrayList = New ArrayList

            While dr.Read
                Dim babitEventProposalDetail As BabitEventProposalDetail = Me.CreateObject(dr)
                babitEventProposalDetailList.Add(babitEventProposalDetail)
            End While

            Return babitEventProposalDetailList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim babitEventProposalDetail As BabitEventProposalDetail = CType(obj, BabitEventProposalDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, babitEventProposalDetail.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim babitEventProposalDetail As BabitEventProposalDetail = CType(obj, BabitEventProposalDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)

            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@Item", DbType.AnsiString, babitEventProposalDetail.Item)
            DbCommandWrapper.AddInParameter("@Qty", DbType.Int32, babitEventProposalDetail.Qty)
            DbCommandWrapper.AddInParameter("@Price", DbType.Currency, babitEventProposalDetail.Price)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, babitEventProposalDetail.Description)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, babitEventProposalDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, babitEventProposalDetail.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@BabitParameterDetailID", DbType.Int32, Me.GetRefObject(babitEventProposalDetail.BabitParameterDetail))
            DbCommandWrapper.AddInParameter("@BabitEventProposalHeaderID", DbType.Int32, Me.GetRefObject(babitEventProposalDetail.BabitEventProposalHeader))

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

            Dim babitEventProposalDetail As BabitEventProposalDetail = CType(obj, BabitEventProposalDetail)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, babitEventProposalDetail.ID)
            DbCommandWrapper.AddInParameter("@Item", DbType.AnsiString, babitEventProposalDetail.Item)
            DbCommandWrapper.AddInParameter("@Qty", DbType.Int32, babitEventProposalDetail.Qty)
            DbCommandWrapper.AddInParameter("@Price", DbType.Currency, babitEventProposalDetail.Price)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, babitEventProposalDetail.Description)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, babitEventProposalDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, babitEventProposalDetail.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@BabitParameterDetailID", DbType.Int32, Me.GetRefObject(babitEventProposalDetail.BabitParameterDetail))
            DbCommandWrapper.AddInParameter("@BabitEventProposalHeaderID", DbType.Int32, Me.GetRefObject(babitEventProposalDetail.BabitEventProposalHeader))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As BabitEventProposalDetail

            Dim babitEventProposalDetail As BabitEventProposalDetail = New BabitEventProposalDetail

            babitEventProposalDetail.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Item")) Then babitEventProposalDetail.Item = dr("Item").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Qty")) Then babitEventProposalDetail.Qty = CType(dr("Qty"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Price")) Then babitEventProposalDetail.Price = CType(dr("Price"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("Description")) Then babitEventProposalDetail.Description = dr("Description").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then babitEventProposalDetail.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then babitEventProposalDetail.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then babitEventProposalDetail.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then babitEventProposalDetail.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then babitEventProposalDetail.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("BabitParameterDetailID")) Then
                babitEventProposalDetail.BabitParameterDetail = New BabitParameterDetail(CType(dr("BabitParameterDetailID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("BabitEventProposalHeaderID")) Then
                babitEventProposalDetail.BabitEventProposalHeader = New BabitEventProposalHeader(CType(dr("BabitEventProposalHeaderID"), Integer))
            End If

            Return babitEventProposalDetail

        End Function

        Private Sub SetTableName()

            If Not (GetType(BabitEventProposalDetail) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(BabitEventProposalDetail), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(BabitEventProposalDetail).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

