#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : ServiceTemplatePMLabor Objects Mapper.
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

    Public Class ServiceTemplatePMLaborMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertServiceTemplatePMLabor"
        Private m_UpdateStatement As String = "up_UpdateServiceTemplatePMLabor"
        Private m_RetrieveStatement As String = "up_RetrieveServiceTemplatePMLabor"
        Private m_RetrieveListStatement As String = "up_RetrieveServiceTemplatePMLaborList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteServiceTemplatePMLabor"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim ServiceTemplatePMLabor As ServiceTemplatePMLabor = Nothing
            While dr.Read

                ServiceTemplatePMLabor = Me.CreateObject(dr)

            End While

            Return ServiceTemplatePMLabor

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim ServiceTemplatePMLaborList As ArrayList = New ArrayList

            While dr.Read
                Dim ServiceTemplatePMLabor As ServiceTemplatePMLabor = Me.CreateObject(dr)
                ServiceTemplatePMLaborList.Add(ServiceTemplatePMLabor)
            End While

            Return ServiceTemplatePMLaborList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim ServiceTemplatePMLabor As ServiceTemplatePMLabor = CType(obj, ServiceTemplatePMLabor)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, ServiceTemplatePMLabor.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim ServiceTemplatePMLabor As ServiceTemplatePMLabor = CType(obj, ServiceTemplatePMLabor)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@LaborDuration", DbType.Decimal, ServiceTemplatePMLabor.LaborDuration)
            DbCommandWrapper.AddInParameter("@LaborCost", DbType.Decimal, ServiceTemplatePMLabor.LaborCost)
            DbCommandWrapper.AddInParameter("@ValidFrom", DbType.DateTime, ServiceTemplatePMLabor.ValidFrom)
            'DbCommandWrapper.AddInParameter("@ValidTo", DbType.DateTime, ServiceTemplatePMLabor.ValidTo)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, ServiceTemplatePMLabor.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdatedBy", DbType.AnsiString, ServiceTemplatePMLabor.LastUpdatedBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(ServiceTemplatePMLabor.Dealer))
            DbCommandWrapper.AddInParameter("@PMKindID", DbType.Int32, Me.GetRefObject(ServiceTemplatePMLabor.PMKind))
            DbCommandWrapper.AddInParameter("@VechileTypeID", DbType.Int32, Me.GetRefObject(ServiceTemplatePMLabor.VechileType))

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

            Dim ServiceTemplatePMLabor As ServiceTemplatePMLabor = CType(obj, ServiceTemplatePMLabor)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, ServiceTemplatePMLabor.ID)
            DbCommandWrapper.AddInParameter("@LaborDuration", DbType.Decimal, ServiceTemplatePMLabor.LaborDuration)
            DbCommandWrapper.AddInParameter("@LaborCost", DbType.Decimal, ServiceTemplatePMLabor.LaborCost)
            DbCommandWrapper.AddInParameter("@ValidFrom", DbType.DateTime, ServiceTemplatePMLabor.ValidFrom)
            'DbCommandWrapper.AddInParameter("@ValidTo", DbType.DateTime, ServiceTemplatePMLabor.ValidTo)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, ServiceTemplatePMLabor.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, ServiceTemplatePMLabor.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(ServiceTemplatePMLabor.Dealer))
            DbCommandWrapper.AddInParameter("@PMKindID", DbType.Int32, Me.GetRefObject(ServiceTemplatePMLabor.PMKind))
            DbCommandWrapper.AddInParameter("@VechileTypeID", DbType.Int16, Me.GetRefObject(ServiceTemplatePMLabor.VechileType))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As ServiceTemplatePMLabor

            Dim ServiceTemplatePMLabor As ServiceTemplatePMLabor = New ServiceTemplatePMLabor

            ServiceTemplatePMLabor.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("LaborDuration")) Then ServiceTemplatePMLabor.LaborDuration = CDec(dr("LaborDuration").ToString)
            If Not dr.IsDBNull(dr.GetOrdinal("LaborCost")) Then ServiceTemplatePMLabor.LaborCost = CDec(dr("LaborCost").ToString)
            If Not dr.IsDBNull(dr.GetOrdinal("ValidFrom")) Then ServiceTemplatePMLabor.ValidFrom = CType(dr("ValidFrom").ToString, DateTime)
            'If Not dr.IsDBNull(dr.GetOrdinal("ValidTo")) Then ServiceTemplatePMLabor.ValidTo = CType(dr("ValidTo").ToString, DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then ServiceTemplatePMLabor.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then ServiceTemplatePMLabor.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then ServiceTemplatePMLabor.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdatedBy")) Then ServiceTemplatePMLabor.LastUpdatedBy = dr("LastUpdatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdatedTime")) Then ServiceTemplatePMLabor.LastUpdatedTime = CType(dr("LastUpdatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                ServiceTemplatePMLabor.Dealer = New Dealer(CType(dr("DealerID"), Short))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("PMKindID")) Then
                ServiceTemplatePMLabor.PMKind = New PMKind(CType(dr("PMKindID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("VechileTypeID")) Then
                ServiceTemplatePMLabor.VechileType = New VechileType(CType(dr("VechileTypeID"), Short))
            End If

            Return ServiceTemplatePMLabor

        End Function

        Private Sub SetTableName()

            If Not (GetType(ServiceTemplatePMLabor) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(ServiceTemplatePMLabor), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(ServiceTemplatePMLabor).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

