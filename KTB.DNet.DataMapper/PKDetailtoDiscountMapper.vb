#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : PKDetailtoDiscount Objects Mapper.
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

    Public Class PKDetailtoDiscountMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertPKDetailtoDiscount"
        Private m_UpdateStatement As String = "up_UpdatePKDetailtoDiscount"
        Private m_RetrieveStatement As String = "up_RetrievePKDetailtoDiscount"
        Private m_RetrieveListStatement As String = "up_RetrievePKDetailtoDiscountList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeletePKDetailtoDiscount"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim PKDetailtoDiscount As PKDetailtoDiscount = Nothing
            While dr.Read

                PKDetailtoDiscount = Me.CreateObject(dr)

            End While

            Return PKDetailtoDiscount

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim PKDetailtoDiscountList As ArrayList = New ArrayList

            While dr.Read
                Dim PKDetailtoDiscount As PKDetailtoDiscount = Me.CreateObject(dr)
                PKDetailtoDiscountList.Add(PKDetailtoDiscount)
            End While

            Return PKDetailtoDiscountList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim PKDetailtoDiscount As PKDetailtoDiscount = CType(obj, PKDetailtoDiscount)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, PKDetailtoDiscount.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim PKDetailtoDiscount As PKDetailtoDiscount = CType(obj, PKDetailtoDiscount)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@Discount", DbType.Currency, PKDetailtoDiscount.Discount)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, PKDetailtoDiscount.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, PKDetailtoDiscount.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@PKDetailID", DbType.Int32, Me.GetRefObject(PKDetailtoDiscount.PKDetail))
            DbCommandWrapper.AddInParameter("@SPLDetailtoSPLID", DbType.Int32, Me.GetRefObject(PKDetailtoDiscount.SPLDetailtoSPL))

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

            Dim PKDetailtoDiscount As PKDetailtoDiscount = CType(obj, PKDetailtoDiscount)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, PKDetailtoDiscount.ID)
            DbCommandWrapper.AddInParameter("@Discount", DbType.Currency, PKDetailtoDiscount.Discount)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, PKDetailtoDiscount.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, PKDetailtoDiscount.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@PKDetailID", DbType.Int32, Me.GetRefObject(PKDetailtoDiscount.PKDetail))
            DbCommandWrapper.AddInParameter("@SPLDetailtoSPLID", DbType.Int32, Me.GetRefObject(PKDetailtoDiscount.SPLDetailtoSPL))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As PKDetailtoDiscount

            Dim PKDetailtoDiscount As PKDetailtoDiscount = New PKDetailtoDiscount

            PKDetailtoDiscount.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Discount")) Then PKDetailtoDiscount.Discount = CType(dr("Discount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then PKDetailtoDiscount.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then PKDetailtoDiscount.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then PKDetailtoDiscount.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then PKDetailtoDiscount.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then PKDetailtoDiscount.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("PKDetailID")) Then
                PKDetailtoDiscount.PKDetail = New PKDetail(CType(dr("PKDetailID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("SPLDetailtoSPLID")) Then
                PKDetailtoDiscount.SPLDetailtoSPL = New SPLDetailtoSPL(CType(dr("SPLDetailtoSPLID"), Integer))
            End If

            Return PKDetailtoDiscount

        End Function

        Private Sub SetTableName()

            If Not (GetType(PKDetailtoDiscount) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(PKDetailtoDiscount), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(PKDetailtoDiscount).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace
