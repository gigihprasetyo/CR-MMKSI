#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SPLDetailtoSPL Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2020 
'// ---------------------
'// $History      : $
'// Generated on 8/23/2020 - 10:50:26 AM
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

    Public Class SPLDetailtoSPLMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertSPLDetailtoSPL"
        Private m_UpdateStatement As String = "up_UpdateSPLDetailtoSPL"
        Private m_RetrieveStatement As String = "up_RetrieveSPLDetailtoSPL"
        Private m_RetrieveListStatement As String = "up_RetrieveSPLDetailtoSPLList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteSPLDetailtoSPL"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim sPLDetailtoSPL As SPLDetailtoSPL = Nothing
            While dr.Read

                sPLDetailtoSPL = Me.CreateObject(dr)

            End While

            Return sPLDetailtoSPL

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim sPLDetailtoSPLList As ArrayList = New ArrayList

            While dr.Read
                Dim sPLDetailtoSPL As SPLDetailtoSPL = Me.CreateObject(dr)
                sPLDetailtoSPLList.Add(sPLDetailtoSPL)
            End While

            Return sPLDetailtoSPLList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sPLDetailtoSPL As SPLDetailtoSPL = CType(obj, SPLDetailtoSPL)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, sPLDetailtoSPL.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sPLDetailtoSPL As SPLDetailtoSPL = CType(obj, SPLDetailtoSPL)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@Discount", DbType.Decimal, sPLDetailtoSPL.Discount)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, sPLDetailtoSPL.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, sPLDetailtoSPL.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@SPLDetailID", DbType.Int32, Me.GetRefObject(sPLDetailtoSPL.SPLDetail))
            DbCommandWrapper.AddInParameter("@SPLDetailRefferenceID", DbType.Int32, Me.GetRefObject(sPLDetailtoSPL.SPLDetailReference))
            DbCommandWrapper.AddInParameter("@DiscountMasterID", DbType.Int32, Me.GetRefObject(sPLDetailtoSPL.DiscountMaster))

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

            Dim sPLDetailtoSPL As SPLDetailtoSPL = CType(obj, SPLDetailtoSPL)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, sPLDetailtoSPL.ID)
            DbCommandWrapper.AddInParameter("@Discount", DbType.Decimal, sPLDetailtoSPL.Discount)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, sPLDetailtoSPL.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, sPLDetailtoSPL.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@SPLDetailID", DbType.Int32, Me.GetRefObject(sPLDetailtoSPL.SPLDetail))
            DbCommandWrapper.AddInParameter("@SPLDetailRefferenceID", DbType.Int32, Me.GetRefObject(sPLDetailtoSPL.SPLDetailReference))
            DbCommandWrapper.AddInParameter("@DiscountMasterID", DbType.Int32, Me.GetRefObject(sPLDetailtoSPL.DiscountMaster))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As SPLDetailtoSPL

            Dim sPLDetailtoSPL As SPLDetailtoSPL = New SPLDetailtoSPL

            sPLDetailtoSPL.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then sPLDetailtoSPL.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("Discount")) Then sPLDetailtoSPL.Discount = dr("Discount").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then sPLDetailtoSPL.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then sPLDetailtoSPL.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then sPLDetailtoSPL.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then sPLDetailtoSPL.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("SPLDetailID")) Then
                sPLDetailtoSPL.SPLDetail = New SPLDetail(CType(dr("SPLDetailID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("SPLDetailRefferenceID")) Then
                sPLDetailtoSPL.SPLDetailReference = New SPLDetail(CType(dr("SPLDetailRefferenceID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("DiscountMasterID")) Then
                sPLDetailtoSPL.DiscountMaster = New DiscountMaster(CType(dr("DiscountMasterID"), Integer))
            End If

            Try
                If Not dr.IsDBNull(dr.GetOrdinal("LabelTotal")) Then sPLDetailtoSPL.LabelTotal = dr("LabelTotal").ToString
                If Not dr.IsDBNull(dr.GetOrdinal("TotalDiscount")) Then sPLDetailtoSPL.TotalDiscount = dr("TotalDiscount").ToString
            Catch ex As Exception
            End Try

            Return sPLDetailtoSPL

        End Function

        Private Sub SetTableName()

            If Not (GetType(SPLDetailtoSPL) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(SPLDetailtoSPL), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(SPLDetailtoSPL).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace
