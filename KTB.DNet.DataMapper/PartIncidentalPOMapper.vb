
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : PartIncidentalPO Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2006 
'// ---------------------
'// $History      : $
'// Generated on 10/31/2006 - 2:56:46 PM
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

    Public Class PartIncidentalPOMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertPartIncidentalPO"
        Private m_UpdateStatement As String = "up_UpdatePartIncidentalPO"
        Private m_RetrieveStatement As String = "up_RetrievePartIncidentalPO"
        Private m_RetrieveListStatement As String = "up_RetrievePartIncidentalPOList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeletePartIncidentalPO"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim partIncidentalPO As PartIncidentalPO = Nothing
            While dr.Read

                partIncidentalPO = Me.CreateObject(dr)

            End While

            Return partIncidentalPO

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim partIncidentalPOList As ArrayList = New ArrayList

            While dr.Read
                Dim partIncidentalPO As PartIncidentalPO = Me.CreateObject(dr)
                partIncidentalPOList.Add(partIncidentalPO)
            End While

            Return partIncidentalPOList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim partIncidentalPO As PartIncidentalPO = CType(obj, PartIncidentalPO)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, partIncidentalPO.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim partIncidentalPO As PartIncidentalPO = CType(obj, PartIncidentalPO)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@PONumber", DbType.AnsiString, partIncidentalPO.PONumber)
            DbCommandWrapper.AddInParameter("@Alocation", DbType.Int32, partIncidentalPO.Alocation)
            DbCommandWrapper.AddInParameter("@ProcessDate", DbType.DateTime, partIncidentalPO.ProcessDate)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, partIncidentalPO.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, partIncidentalPO.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@PartIncidentalDetailID", DbType.Int32, Me.GetRefObject(partIncidentalPO.PartIncidentalDetail))

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

            Dim partIncidentalPO As PartIncidentalPO = CType(obj, PartIncidentalPO)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, partIncidentalPO.ID)
            DbCommandWrapper.AddInParameter("@PONumber", DbType.AnsiString, partIncidentalPO.PONumber)
            DbCommandWrapper.AddInParameter("@Alocation", DbType.Int32, partIncidentalPO.Alocation)
            DbCommandWrapper.AddInParameter("@ProcessDate", DbType.DateTime, partIncidentalPO.ProcessDate)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, partIncidentalPO.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, partIncidentalPO.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@PartIncidentalDetailID", DbType.Int32, Me.GetRefObject(partIncidentalPO.PartIncidentalDetail))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As PartIncidentalPO

            Dim partIncidentalPO As PartIncidentalPO = New PartIncidentalPO

            partIncidentalPO.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("PONumber")) Then partIncidentalPO.PONumber = dr("PONumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Alocation")) Then partIncidentalPO.Alocation = CType(dr("Alocation"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ProcessDate")) Then partIncidentalPO.ProcessDate = CType(dr("ProcessDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then partIncidentalPO.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then partIncidentalPO.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then partIncidentalPO.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then partIncidentalPO.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then partIncidentalPO.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("PartIncidentalDetailID")) Then
                partIncidentalPO.PartIncidentalDetail = New PartIncidentalDetail(CType(dr("PartIncidentalDetailID"), Integer))
            End If

            Return partIncidentalPO

        End Function

        Private Sub SetTableName()

            If Not (GetType(PartIncidentalPO) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(PartIncidentalPO), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(PartIncidentalPO).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

