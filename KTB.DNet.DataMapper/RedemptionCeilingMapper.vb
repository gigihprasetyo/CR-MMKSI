
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : RedemptionCeiling Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2010 
'// ---------------------
'// $History      : $
'// Generated on 4/29/2010 - 8:37:05 AM
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

    Public Class RedemptionCeilingMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertRedemptionCeiling"
        Private m_UpdateStatement As String = "up_UpdateRedemptionCeiling"
        Private m_RetrieveStatement As String = "up_RetrieveRedemptionCeiling"
        Private m_RetrieveListStatement As String = "up_RetrieveRedemptionCeilingList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteRedemptionCeiling"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim redemptionCeiling As RedemptionCeiling = Nothing
            While dr.Read

                redemptionCeiling = Me.CreateObject(dr)

            End While

            Return redemptionCeiling

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim redemptionCeilingList As ArrayList = New ArrayList

            While dr.Read
                Dim redemptionCeiling As RedemptionCeiling = Me.CreateObject(dr)
                redemptionCeilingList.Add(redemptionCeiling)
            End While

            Return redemptionCeilingList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim redemptionCeiling As RedemptionCeiling = CType(obj, RedemptionCeiling)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, redemptionCeiling.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim redemptionCeiling As RedemptionCeiling = CType(obj, RedemptionCeiling)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@CreditAccount", DbType.AnsiString, redemptionCeiling.CreditAccount)
            DbCommandWrapper.AddInParameter("@PaymentType", DbType.Byte, redemptionCeiling.PaymentType)
            DbCommandWrapper.AddInParameter("@PeriodMonth", DbType.Int32, redemptionCeiling.PeriodMonth)
            DbCommandWrapper.AddInParameter("@PeriodYear", DbType.Int32, redemptionCeiling.PeriodYear)
            DbCommandWrapper.AddInParameter("@Ceiling", DbType.Currency, redemptionCeiling.Ceiling)
            DbCommandWrapper.AddInParameter("@MaxContractDate", DbType.DateTime, redemptionCeiling.MaxContractDate)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, redemptionCeiling.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, redemptionCeiling.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


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

            Dim redemptionCeiling As RedemptionCeiling = CType(obj, RedemptionCeiling)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, redemptionCeiling.ID)
            DbCommandWrapper.AddInParameter("@CreditAccount", DbType.AnsiString, redemptionCeiling.CreditAccount)
            DbCommandWrapper.AddInParameter("@PaymentType", DbType.Byte, redemptionCeiling.PaymentType)
            DbCommandWrapper.AddInParameter("@PeriodMonth", DbType.Int32, redemptionCeiling.PeriodMonth)
            DbCommandWrapper.AddInParameter("@PeriodYear", DbType.Int32, redemptionCeiling.PeriodYear)
            DbCommandWrapper.AddInParameter("@Ceiling", DbType.Currency, redemptionCeiling.Ceiling)
            DbCommandWrapper.AddInParameter("@MaxContractDate", DbType.DateTime, redemptionCeiling.MaxContractDate)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, redemptionCeiling.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, redemptionCeiling.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As RedemptionCeiling

            Dim redemptionCeiling As RedemptionCeiling = New RedemptionCeiling

            redemptionCeiling.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("CreditAccount")) Then redemptionCeiling.CreditAccount = dr("CreditAccount").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PaymentType")) Then redemptionCeiling.PaymentType = CType(dr("PaymentType"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("PeriodMonth")) Then redemptionCeiling.PeriodMonth = CType(dr("PeriodMonth"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("PeriodYear")) Then redemptionCeiling.PeriodYear = CType(dr("PeriodYear"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Ceiling")) Then redemptionCeiling.Ceiling = CType(dr("Ceiling"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("MaxContractDate")) Then redemptionCeiling.MaxContractDate = CType(dr("MaxContractDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then redemptionCeiling.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then redemptionCeiling.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then redemptionCeiling.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then redemptionCeiling.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then redemptionCeiling.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return redemptionCeiling

        End Function

        Private Sub SetTableName()

            If Not (GetType(RedemptionCeiling) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(RedemptionCeiling), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(RedemptionCeiling).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

