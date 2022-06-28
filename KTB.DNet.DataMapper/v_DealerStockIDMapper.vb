
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DN
'// PURPOSE       : v_DealerStockID Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2009 
'// ---------------------
'// $History      : $
'// Generated on 8/19/2009 - 2:40:07 PM
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

    Public Class v_DealerStockIDMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_Insertv_DealerStockID"
        Private m_UpdateStatement As String = "up_Updatev_DealerStockID"
        Private m_RetrieveStatement As String = "up_Retrievev_DealerStockID"
        Private m_RetrieveListStatement As String = "up_Retrievev_DealerStockIDList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_Deletev_DealerStockID"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim v_DealerStockID As v_DealerStockID = Nothing
            While dr.Read

                v_DealerStockID = Me.CreateObject(dr)

            End While

            Return v_DealerStockID

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim v_DealerStockIDList As ArrayList = New ArrayList

            While dr.Read
                Dim v_DealerStockID As v_DealerStockID = Me.CreateObject(dr)
                v_DealerStockIDList.Add(v_DealerStockID)
            End While

            Return v_DealerStockIDList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim v_DealerStockID As v_DealerStockID = CType(obj, v_DealerStockID)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, v_DealerStockID.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim v_DealerStockID As v_DealerStockID = CType(obj, v_DealerStockID)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@dID", DbType.Int16, v_DealerStockID.dID)
            DbCommandWrapper.AddInParameter("@vtID", DbType.Int16, v_DealerStockID.vtID)
            DbCommandWrapper.AddInParameter("@vcID", DbType.Int16, v_DealerStockID.vcID)
            DbCommandWrapper.AddInParameter("@pkhID", DbType.Int32, v_DealerStockID.pkhID)
            DbCommandWrapper.AddInParameter("@custID", DbType.Int32, v_DealerStockID.custID)
            DbCommandWrapper.AddInParameter("@ecID", DbType.Int32, v_DealerStockID.ecID)
            DbCommandWrapper.AddInParameter("@catID", DbType.Byte, v_DealerStockID.catID)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, v_DealerStockID.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, v_DealerStockID.LastUpdateBy)
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

            Dim v_DealerStockID As v_DealerStockID = CType(obj, v_DealerStockID)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, v_DealerStockID.ID)
            DbCommandWrapper.AddInParameter("@dID", DbType.Int16, v_DealerStockID.dID)
            DbCommandWrapper.AddInParameter("@vtID", DbType.Int16, v_DealerStockID.vtID)
            DbCommandWrapper.AddInParameter("@vcID", DbType.Int16, v_DealerStockID.vcID)
            DbCommandWrapper.AddInParameter("@pkhID", DbType.Int32, v_DealerStockID.pkhID)
            DbCommandWrapper.AddInParameter("@custID", DbType.Int32, v_DealerStockID.custID)
            DbCommandWrapper.AddInParameter("@ecID", DbType.Int32, v_DealerStockID.ecID)
            DbCommandWrapper.AddInParameter("@catID", DbType.Byte, v_DealerStockID.catID)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, v_DealerStockID.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, v_DealerStockID.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As v_DealerStockID

            Dim v_DealerStockID As v_DealerStockID = New v_DealerStockID

            v_DealerStockID.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("dID")) Then v_DealerStockID.dID = CType(dr("dID"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("vtID")) Then v_DealerStockID.vtID = CType(dr("vtID"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("vcID")) Then v_DealerStockID.vcID = CType(dr("vcID"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("pkhID")) Then v_DealerStockID.pkhID = CType(dr("pkhID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("custID")) Then v_DealerStockID.custID = CType(dr("custID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ecID")) Then v_DealerStockID.ecID = CType(dr("ecID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("catID")) Then v_DealerStockID.catID = CType(dr("catID"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then v_DealerStockID.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then v_DealerStockID.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then v_DealerStockID.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then v_DealerStockID.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then v_DealerStockID.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return v_DealerStockID

        End Function

        Private Sub SetTableName()

            If Not (GetType(v_DealerStockID) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(v_DealerStockID), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(v_DealerStockID).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

