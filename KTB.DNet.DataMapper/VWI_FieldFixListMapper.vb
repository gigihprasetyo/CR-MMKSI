
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : Mitrais Team
'// PURPOSE       : FieldFixList Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 19/03/2018 - 20:06:51
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

    Public Class VWI_FieldFixListMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertFieldFixList"
        Private m_UpdateStatement As String = "up_UpdateFieldFixList"
        Private m_RetrieveStatement As String = "up_RetrieveFieldFixList"
        Private m_RetrieveListStatement As String = "up_RetrieveFieldFixListList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteFieldFixList"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim VWI_FieldFixList As VWI_FieldFixList = Nothing
            While dr.Read

                VWI_FieldFixList = Me.CreateObject(dr)

            End While

            Return VWI_FieldFixList

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim VWI_FieldFixListList As ArrayList = New ArrayList

            While dr.Read
                Dim VWI_FieldFixList As VWI_FieldFixList = Me.CreateObject(dr)
                VWI_FieldFixListList.Add(VWI_FieldFixList)
            End While

            Return VWI_FieldFixListList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim VWI_FieldFixList As VWI_FieldFixList = CType(obj, VWI_FieldFixList)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, VWI_FieldFixList.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim VWI_FieldFixList As VWI_FieldFixList = CType(obj, VWI_FieldFixList)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@ChassisNo", DbType.AnsiString, VWI_FieldFixList.ChassisNo)
            DbCommandWrapper.AddInParameter("@RecallRegNo", DbType.AnsiString, VWI_FieldFixList.RecallRegNo)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, VWI_FieldFixList.Description)
            DbCommandWrapper.AddInParameter("@BuletinDescription", DbType.AnsiString, VWI_FieldFixList.BuletinDescription)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, VWI_FieldFixList.DealerCode)
            DbCommandWrapper.AddInParameter("@DealerName", DbType.AnsiString, VWI_FieldFixList.DealerName)

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

            Dim VWI_FieldFixList As VWI_FieldFixList = CType(obj, VWI_FieldFixList)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, VWI_FieldFixList.ID)
            DbCommandWrapper.AddInParameter("@ChassisNo", DbType.AnsiString, VWI_FieldFixList.ChassisNo)
            DbCommandWrapper.AddInParameter("@RecallRegNo", DbType.AnsiString, VWI_FieldFixList.RecallRegNo)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, VWI_FieldFixList.Description)
            DbCommandWrapper.AddInParameter("@BuletinDescription", DbType.AnsiString, VWI_FieldFixList.BuletinDescription)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, VWI_FieldFixList.DealerCode)
            DbCommandWrapper.AddInParameter("@DealerName", DbType.AnsiString, VWI_FieldFixList.DealerName)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As VWI_FieldFixList

            Dim VWI_FieldFixList As VWI_FieldFixList = New VWI_FieldFixList

            VWI_FieldFixList.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ChassisNo")) Then VWI_FieldFixList.ChassisNo = dr("ChassisNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RecallRegNo")) Then VWI_FieldFixList.RecallRegNo = dr("RecallRegNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Description")) Then VWI_FieldFixList.Description = dr("Description").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("BuletinDescription")) Then VWI_FieldFixList.BuletinDescription = dr("BuletinDescription").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ValidStartDate")) Then VWI_FieldFixList.ValidStartDate = CType(dr("ValidStartDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerCode")) Then VWI_FieldFixList.DealerCode = dr("DealerCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DealerName")) Then VWI_FieldFixList.DealerName = dr("DealerName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then VWI_FieldFixList.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return VWI_FieldFixList

        End Function

        Private Sub SetTableName()

            If Not (GetType(VWI_FieldFixList) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(VWI_FieldFixList), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(VWI_FieldFixList).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

