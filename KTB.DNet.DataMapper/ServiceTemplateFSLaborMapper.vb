#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : ServiceTemplateFSLabor Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2005 
'// ---------------------
'// $History      : $
'// Generated on 11/30/2005 - 1:05:06 PM
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

    Public Class ServiceTemplateFSLaborMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertServiceTemplateFSLabor"
        Private m_UpdateStatement As String = "up_UpdateServiceTemplateFSLabor"
        Private m_RetrieveStatement As String = "up_RetrieveServiceTemplateFSLabor"
        Private m_RetrieveListStatement As String = "up_RetrieveServiceTemplateFSLaborList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteServiceTemplateFSLabor"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim ServiceTemplateFSLabor As ServiceTemplateFSLabor = Nothing
            While dr.Read

                ServiceTemplateFSLabor = Me.CreateObject(dr)

            End While

            Return ServiceTemplateFSLabor

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim ServiceTemplateFSLaborList As ArrayList = New ArrayList

            While dr.Read
                Dim ServiceTemplateFSLabor As ServiceTemplateFSLabor = Me.CreateObject(dr)
                ServiceTemplateFSLaborList.Add(ServiceTemplateFSLabor)
            End While

            Return ServiceTemplateFSLaborList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim ServiceTemplateFSLabor As ServiceTemplateFSLabor = CType(obj, ServiceTemplateFSLabor)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, ServiceTemplateFSLabor.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim ServiceTemplateFSLabor As ServiceTemplateFSLabor = CType(obj, ServiceTemplateFSLabor)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@LaborDuration", DbType.Decimal, ServiceTemplateFSLabor.LaborDuration)
            DbCommandWrapper.AddInParameter("@LaborCost", DbType.Decimal, ServiceTemplateFSLabor.LaborCost)
            DbCommandWrapper.AddInParameter("@ValidFrom", DbType.DateTime, ServiceTemplateFSLabor.ValidFrom)
            'DbCommandWrapper.AddInParameter("@ValidTo", DbType.DateTime, ServiceTemplateFSLabor.ValidTo)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, ServiceTemplateFSLabor.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdatedBy", DbType.AnsiString, ServiceTemplateFSLabor.LastUpdatedBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(ServiceTemplateFSLabor.Dealer))
            DbCommandWrapper.AddInParameter("@FSKindID", DbType.Int16, Me.GetRefObject(ServiceTemplateFSLabor.FSKind))
            DbCommandWrapper.AddInParameter("@VechileTypeID", DbType.Int32, Me.GetRefObject(ServiceTemplateFSLabor.VechileType))

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

            Dim ServiceTemplateFSLabor As ServiceTemplateFSLabor = CType(obj, ServiceTemplateFSLabor)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, ServiceTemplateFSLabor.ID)
            DbCommandWrapper.AddInParameter("@LaborDuration", DbType.Decimal, ServiceTemplateFSLabor.LaborDuration)
            DbCommandWrapper.AddInParameter("@LaborCost", DbType.Decimal, ServiceTemplateFSLabor.LaborCost)
            DbCommandWrapper.AddInParameter("@ValidFrom", DbType.DateTime, ServiceTemplateFSLabor.ValidFrom)
            'DbCommandWrapper.AddInParameter("@ValidTo", DbType.DateTime, ServiceTemplateFSLabor.ValidTo)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, ServiceTemplateFSLabor.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, ServiceTemplateFSLabor.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(ServiceTemplateFSLabor.Dealer))
            DbCommandWrapper.AddInParameter("@FSKindID", DbType.Int16, Me.GetRefObject(ServiceTemplateFSLabor.FSKind))
            DbCommandWrapper.AddInParameter("@VechileTypeID", DbType.Int16, Me.GetRefObject(ServiceTemplateFSLabor.VechileType))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As ServiceTemplateFSLabor

            Dim ServiceTemplateFSLabor As ServiceTemplateFSLabor = New ServiceTemplateFSLabor

            ServiceTemplateFSLabor.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("LaborDuration")) Then ServiceTemplateFSLabor.LaborDuration = CDec(dr("LaborDuration").ToString)
            If Not dr.IsDBNull(dr.GetOrdinal("LaborCost")) Then ServiceTemplateFSLabor.LaborCost = CDec(dr("LaborCost").ToString)
            If Not dr.IsDBNull(dr.GetOrdinal("ValidFrom")) Then ServiceTemplateFSLabor.ValidFrom = CType(dr("ValidFrom").ToString, DateTime)
            'If Not dr.IsDBNull(dr.GetOrdinal("ValidTo")) Then ServiceTemplateFSLabor.ValidTo = CType(dr("ValidTo").ToString, DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then ServiceTemplateFSLabor.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then ServiceTemplateFSLabor.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then ServiceTemplateFSLabor.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdatedBy")) Then ServiceTemplateFSLabor.LastUpdatedBy = dr("LastUpdatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdatedTime")) Then ServiceTemplateFSLabor.LastUpdatedTime = CType(dr("LastUpdatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                ServiceTemplateFSLabor.Dealer = New Dealer(CType(dr("DealerID"), Short))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("FSKindID")) Then
                ServiceTemplateFSLabor.FSKind = New FSKind(CType(dr("FSKindID"), Short))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("VechileTypeID")) Then
                ServiceTemplateFSLabor.VechileType = New VechileType(CType(dr("VechileTypeID"), Short))
            End If

            Return ServiceTemplateFSLabor

        End Function

        Private Sub SetTableName()

            If Not (GetType(ServiceTemplateFSLabor) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(ServiceTemplateFSLabor), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(ServiceTemplateFSLabor).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

