
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : ChassisMasterATA Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2019 
'// ---------------------
'// $History      : $
'// Generated on 08/11/2019 - 9:21:57
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

    Public Class ChassisMasterATAMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertChassisMasterATA"
        Private m_UpdateStatement As String = "up_UpdateChassisMasterATA"
        Private m_RetrieveStatement As String = "up_RetrieveChassisMasterATA"
        Private m_RetrieveListStatement As String = "up_RetrieveChassisMasterATAList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteChassisMasterATA"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim chassisMasterATA As ChassisMasterATA = Nothing
            While dr.Read

                chassisMasterATA = Me.CreateObject(dr)

            End While

            Return chassisMasterATA

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim chassisMasterATAList As ArrayList = New ArrayList

            While dr.Read
                Dim chassisMasterATA As ChassisMasterATA = Me.CreateObject(dr)
                chassisMasterATAList.Add(chassisMasterATA)
            End While

            Return chassisMasterATAList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim chassisMasterATA As ChassisMasterATA = CType(obj, ChassisMasterATA)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, chassisMasterATA.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim chassisMasterATA As ChassisMasterATA = CType(obj, ChassisMasterATA)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@ETA", DbType.DateTime, chassisMasterATA.ETA)
            DbCommandWrapper.AddInParameter("@ATA", DbType.DateTime, chassisMasterATA.ATA)
            DbCommandWrapper.AddInParameter("@RemarkATA", DbType.AnsiString, chassisMasterATA.RemarkATA)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, chassisMasterATA.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, chassisMasterATA.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@POHeaderID", DbType.Int32, Me.GetRefObject(chassisMasterATA.POHeader))
            'DbCommandWrapper.AddInParameter("@POHeaderID", DbType.Int32, chassisMasterATA.POHeaderID)
            DbCommandWrapper.AddInParameter("@ChassisMasterID", DbType.Int32, Me.GetRefObject(chassisMasterATA.ChassisMaster))
            'DbCommandWrapper.AddInParameter("@ChassisMasterID", DbType.Int32, chassisMasterATA.ChassisMasterID)

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

            Dim chassisMasterATA As ChassisMasterATA = CType(obj, ChassisMasterATA)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, chassisMasterATA.ID)
            DbCommandWrapper.AddInParameter("@ETA", DbType.DateTime, chassisMasterATA.ETA)
            DbCommandWrapper.AddInParameter("@ATA", DbType.DateTime, chassisMasterATA.ATA)
            DbCommandWrapper.AddInParameter("@RemarkATA", DbType.AnsiString, chassisMasterATA.RemarkATA)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, chassisMasterATA.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, chassisMasterATA.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            DbCommandWrapper.AddInParameter("@POHeaderID", DbType.Int32, Me.GetRefObject(chassisMasterATA.POHeader))
            'DbCommandWrapper.AddInParameter("@POHeaderID", DbType.Int32, chassisMasterATA.POHeaderID)
            DbCommandWrapper.AddInParameter("@ChassisMasterID", DbType.Int32, Me.GetRefObject(chassisMasterATA.ChassisMaster))
            'DbCommandWrapper.AddInParameter("@ChassisMasterID", DbType.Int32, chassisMasterATA.ChassisMasterID)

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As ChassisMasterATA

            Dim chassisMasterATA As ChassisMasterATA = New ChassisMasterATA

            chassisMasterATA.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ETA")) Then chassisMasterATA.ETA = CType(dr("ETA"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ATA")) Then chassisMasterATA.ATA = CType(dr("ATA"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("RemarkATA")) Then chassisMasterATA.RemarkATA = dr("RemarkATA").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then chassisMasterATA.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then chassisMasterATA.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then chassisMasterATA.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then chassisMasterATA.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then chassisMasterATA.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            If Not dr.IsDBNull(dr.GetOrdinal("POHeaderID")) Then
                chassisMasterATA.POHeader = New POHeader(CType(dr("POHeaderID"), Integer))
            End If
            'If Not dr.IsDBNull(dr.GetOrdinal("POHeaderID")) Then
            '    chassisMasterATA.POHeaderID = CType(dr("POHeaderID"), Integer)
            'End If
            If Not dr.IsDBNull(dr.GetOrdinal("ChassisMasterID")) Then
                chassisMasterATA.ChassisMaster = New ChassisMaster(CType(dr("ChassisMasterID"), Integer))
            End If
            'If Not dr.IsDBNull(dr.GetOrdinal("ChassisMasterID")) Then
            '    chassisMasterATA.ChassisMasterID = CType(dr("ChassisMasterID"), Integer)
            'End If

            Return chassisMasterATA

        End Function

        Private Sub SetTableName()

            If Not (GetType(ChassisMasterATA) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(ChassisMasterATA), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(ChassisMasterATA).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

