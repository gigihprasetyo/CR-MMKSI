#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : ConditionMaster Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2009 
'// ---------------------
'// $History      : $
'// Generated on 10/5/2009 - 5:08:07 PM
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

    Public Class ConditionMasterMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertConditionMaster"
        Private m_UpdateStatement As String = "up_UpdateConditionMaster"
        Private m_RetrieveStatement As String = "up_RetrieveConditionMaster"
        Private m_RetrieveListStatement As String = "up_RetrieveConditionMasterList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteConditionMaster"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim conditionMaster As ConditionMaster = Nothing
            While dr.Read

                conditionMaster = Me.CreateObject(dr)

            End While

            Return conditionMaster

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim conditionMasterList As ArrayList = New ArrayList

            While dr.Read
                Dim conditionMaster As ConditionMaster = Me.CreateObject(dr)
                conditionMasterList.Add(conditionMaster)
            End While

            Return conditionMasterList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim conditionMaster As ConditionMaster = CType(obj, ConditionMaster)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, conditionMaster.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim conditionMaster As ConditionMaster = CType(obj, ConditionMaster)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@DocumentType", DbType.Int16, conditionMaster.DocumentType)
            DbCommandWrapper.AddInParameter("@ValidFrom", DbType.DateTime, conditionMaster.ValidFrom)
            DbCommandWrapper.AddInParameter("@BasePrice", DbType.Currency, conditionMaster.BasePrice)
            DbCommandWrapper.AddInParameter("@RetailPrice", DbType.Currency, conditionMaster.RetailPrice)
            DbCommandWrapper.AddInParameter("@Subsidi", DbType.Currency, conditionMaster.Subsidi)
            DbCommandWrapper.AddInParameter("@SPAF", DbType.Currency, conditionMaster.SPAF)
            DbCommandWrapper.AddInParameter("@AssistFee", DbType.Currency, conditionMaster.AssistFee)
            DbCommandWrapper.AddInParameter("@PPhPercent", DbType.Decimal, conditionMaster.PPhPercent)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, conditionMaster.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, conditionMaster.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@VechileTypeID", DbType.Int16, Me.GetRefObject(conditionMaster.VechileType))

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

            Dim conditionMaster As ConditionMaster = CType(obj, ConditionMaster)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, conditionMaster.ID)
            DbCommandWrapper.AddInParameter("@DocumentType", DbType.Int16, conditionMaster.DocumentType)
            DbCommandWrapper.AddInParameter("@ValidFrom", DbType.DateTime, conditionMaster.ValidFrom)
            DbCommandWrapper.AddInParameter("@BasePrice", DbType.Currency, conditionMaster.BasePrice)
            DbCommandWrapper.AddInParameter("@RetailPrice", DbType.Currency, conditionMaster.RetailPrice)
            DbCommandWrapper.AddInParameter("@Subsidi", DbType.Currency, conditionMaster.Subsidi)
            DbCommandWrapper.AddInParameter("@SPAF", DbType.Currency, conditionMaster.SPAF)
            DbCommandWrapper.AddInParameter("@AssistFee", DbType.Currency, conditionMaster.AssistFee)
            DbCommandWrapper.AddInParameter("@PPhPercent", DbType.Decimal, conditionMaster.PPhPercent)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, conditionMaster.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, conditionMaster.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@VechileTypeID", DbType.Int16, Me.GetRefObject(conditionMaster.VechileType))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As ConditionMaster

            Dim conditionMaster As ConditionMaster = New ConditionMaster

            conditionMaster.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("DocumentType")) Then conditionMaster.DocumentType = CType(dr("DocumentType"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("ValidFrom")) Then conditionMaster.ValidFrom = CType(dr("ValidFrom"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("BasePrice")) Then conditionMaster.BasePrice = CType(dr("BasePrice"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("RetailPrice")) Then conditionMaster.RetailPrice = CType(dr("RetailPrice"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("Subsidi")) Then conditionMaster.Subsidi = CType(dr("Subsidi"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("SPAF")) Then conditionMaster.SPAF = CType(dr("SPAF"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("AssistFee")) Then conditionMaster.AssistFee = CType(dr("AssistFee"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("PPhPercent")) Then conditionMaster.PPhPercent = CType(dr("PPhPercent"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then conditionMaster.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then conditionMaster.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then conditionMaster.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then conditionMaster.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then conditionMaster.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("VechileTypeID")) Then
                conditionMaster.VechileType = New VechileType(CType(dr("VechileTypeID"), Short))
            End If

            Return conditionMaster

        End Function

        Private Sub SetTableName()

            If Not (GetType(ConditionMaster) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(ConditionMaster), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(ConditionMaster).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

