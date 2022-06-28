
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : Mitrais Team
'// PURPOSE       : VWI_JobPositionParts Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2019 
'// ---------------------
'// $History      : $
'// Generated on 09/01/2019 - 9:22:40
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

    Public Class VWI_JobPositionPartsMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertVWI_JobPositionParts"
        Private m_UpdateStatement As String = "up_UpdateVWI_JobPositionParts"
        Private m_RetrieveStatement As String = "up_RetrieveVWI_JobPositionParts"
        Private m_RetrieveListStatement As String = "up_RetrieveVWI_JobPositionPartsList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteVWI_JobPositionParts"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim VWI_JobPositionParts As VWI_JobPositionParts = Nothing
            While dr.Read

                VWI_JobPositionParts = Me.CreateObject(dr)

            End While

            Return VWI_JobPositionParts

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim VWI_JobPositionPartsList As ArrayList = New ArrayList

            While dr.Read
                Dim VWI_JobPositionParts As VWI_JobPositionParts = Me.CreateObject(dr)
                VWI_JobPositionPartsList.Add(VWI_JobPositionParts)
            End While

            Return VWI_JobPositionPartsList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim VWI_JobPositionParts As VWI_JobPositionParts = CType(obj, VWI_JobPositionParts)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, VWI_JobPositionParts.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim VWI_JobPositionParts As VWI_JobPositionParts = CType(obj, VWI_JobPositionParts)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@Code", DbType.AnsiString, VWI_JobPositionParts.Code)
            DbCommandWrapper.AddInParameter("@PositionName", DbType.AnsiString, VWI_JobPositionParts.PositionName)
            DbCommandWrapper.AddInParameter("@ParentID", DbType.Int32, VWI_JobPositionParts.ParentID)
            DbCommandWrapper.AddInParameter("@ParentCode", DbType.AnsiString, VWI_JobPositionParts.ParentCode)
            DbCommandWrapper.AddInParameter("@ParentPositionName", DbType.AnsiString, VWI_JobPositionParts.ParentPositionName)


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

            Dim VWI_JobPositionParts As VWI_JobPositionParts = CType(obj, VWI_JobPositionParts)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, VWI_JobPositionParts.ID)
            DbCommandWrapper.AddInParameter("@Code", DbType.AnsiString, VWI_JobPositionParts.Code)
            DbCommandWrapper.AddInParameter("@PositionName", DbType.AnsiString, VWI_JobPositionParts.PositionName)
            DbCommandWrapper.AddInParameter("@ParentID", DbType.Int32, VWI_JobPositionParts.ParentID)
            DbCommandWrapper.AddInParameter("@ParentCode", DbType.AnsiString, VWI_JobPositionParts.ParentCode)
            DbCommandWrapper.AddInParameter("@ParentPositionName", DbType.AnsiString, VWI_JobPositionParts.ParentPositionName)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As VWI_JobPositionParts

            Dim VWI_JobPositionParts As VWI_JobPositionParts = New VWI_JobPositionParts

            VWI_JobPositionParts.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Code")) Then VWI_JobPositionParts.Code = dr("Code").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PositionName")) Then VWI_JobPositionParts.PositionName = dr("PositionName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ParentID")) Then VWI_JobPositionParts.ParentID = CType(dr("ParentID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ParentCode")) Then VWI_JobPositionParts.ParentCode = dr("ParentCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ParentPositionName")) Then VWI_JobPositionParts.ParentPositionName = dr("ParentPositionName").ToString

            Return VWI_JobPositionParts

        End Function

        Private Sub SetTableName()

            If Not (GetType(VWI_JobPositionParts) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(VWI_JobPositionParts), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(VWI_JobPositionParts).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

