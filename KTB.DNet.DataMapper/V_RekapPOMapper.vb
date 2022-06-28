
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : V_RekapPO Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2009 
'// ---------------------
'// $History      : $
'// Generated on 5/5/2009 - 8:59:33 AM
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

    Public Class V_RekapPOMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertV_RekapPO"
        Private m_UpdateStatement As String = "up_UpdateV_RekapPO"
        Private m_RetrieveStatement As String = "up_RetrieveV_RekapPO"
        Private m_RetrieveListStatement As String = "up_RetrieveV_RekapPOList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteV_RekapPO"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim v_RekapPO As V_RekapPO = Nothing
            While dr.Read

                v_RekapPO = Me.CreateObject(dr)

            End While

            Return v_RekapPO

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim v_RekapPOList As ArrayList = New ArrayList

            While dr.Read
                Dim v_RekapPO As V_RekapPO = Me.CreateObject(dr)
                v_RekapPOList.Add(v_RekapPO)
            End While

            Return v_RekapPOList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim v_RekapPO As V_RekapPO = CType(obj, V_RekapPO)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, v_RekapPO.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim v_RekapPO As V_RekapPO = CType(obj, V_RekapPO)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@TotalHarga", DbType.Currency, v_RekapPO.TotalHarga)
            DbCommandWrapper.AddInParameter("@TotalHargaPP", DbType.Currency, v_RekapPO.TotalHargaPP)
            DbCommandWrapper.AddInParameter("@TotalQuantity", DbType.Int32, v_RekapPO.TotalQuantity)
            DbCommandWrapper.AddInParameter("@TotalHargaIT", DbType.Currency, v_RekapPO.TotalHargaIT)
            DbCommandWrapper.AddInParameter("@TotalHargaLC", DbType.Currency, v_RekapPO.TotalHargaLC)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, v_RekapPO.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, v_RekapPO.LastUpdateBy)
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

            Dim v_RekapPO As V_RekapPO = CType(obj, V_RekapPO)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, v_RekapPO.ID)
            DbCommandWrapper.AddInParameter("@TotalHarga", DbType.Currency, v_RekapPO.TotalHarga)
            DbCommandWrapper.AddInParameter("@TotalHargaPP", DbType.Currency, v_RekapPO.TotalHargaPP)
            DbCommandWrapper.AddInParameter("@TotalQuantity", DbType.Int32, v_RekapPO.TotalQuantity)
            DbCommandWrapper.AddInParameter("@TotalHargaIT", DbType.Currency, v_RekapPO.TotalHargaIT)
            DbCommandWrapper.AddInParameter("@TotalHargaLC", DbType.Currency, v_RekapPO.TotalHargaLC)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, v_RekapPO.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, v_RekapPO.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As V_RekapPO

            Dim v_RekapPO As V_RekapPO = New V_RekapPO

            v_RekapPO.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("TotalHarga")) Then v_RekapPO.TotalHarga = CType(dr("TotalHarga"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("TotalHargaPP")) Then v_RekapPO.TotalHargaPP = CType(dr("TotalHargaPP"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("TotalQuantity")) Then v_RekapPO.TotalQuantity = CType(dr("TotalQuantity"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("TotalHargaIT")) Then v_RekapPO.TotalHargaIT = CType(dr("TotalHargaIT"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("TotalHargaLC")) Then v_RekapPO.TotalHargaLC = CType(dr("TotalHargaLC"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then v_RekapPO.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then v_RekapPO.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then v_RekapPO.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then v_RekapPO.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then v_RekapPO.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            If Not dr.IsDBNull(dr.GetOrdinal("ID")) Then
                v_RekapPO.POHeader = New POHeader(CType(dr("ID"), Integer))
            End If
            Return v_RekapPO

        End Function

        Private Sub SetTableName()

            If Not (GetType(V_RekapPO) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(V_RekapPO), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(V_RekapPO).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

