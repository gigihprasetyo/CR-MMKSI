
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : v_POEffectiveDate Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2009 
'// ---------------------
'// $History      : $
'// Generated on 10/18/2009 - 7:18:16 PM
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

    Public Class v_POEffectiveDateMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_Insertv_POEffectiveDate"
        Private m_UpdateStatement As String = "up_Updatev_POEffectiveDate"
        Private m_RetrieveStatement As String = "up_Retrievev_POEffectiveDate"
        Private m_RetrieveListStatement As String = "up_Retrievev_POEffectiveDateList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_Deletev_POEffectiveDate"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim v_POEffectiveDate As v_POEffectiveDate = Nothing
            While dr.Read

                v_POEffectiveDate = Me.CreateObject(dr)

            End While

            Return v_POEffectiveDate

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim v_POEffectiveDateList As ArrayList = New ArrayList

            While dr.Read
                Dim v_POEffectiveDate As v_POEffectiveDate = Me.CreateObject(dr)
                v_POEffectiveDateList.Add(v_POEffectiveDate)
            End While

            Return v_POEffectiveDateList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim v_POEffectiveDate As v_POEffectiveDate = CType(obj, v_POEffectiveDate)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, v_POEffectiveDate.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim v_POEffectiveDate As v_POEffectiveDate = CType(obj, v_POEffectiveDate)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@Status", DbType.AnsiString, v_POEffectiveDate.Status)
            DbCommandWrapper.AddInParameter("@EffectiveDate", DbType.DateTime, v_POEffectiveDate.EffectiveDate)
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, v_POEffectiveDate.DealerID)
            DbCommandWrapper.AddInParameter("@CreditAccount", DbType.AnsiString, v_POEffectiveDate.CreditAccount)
            DbCommandWrapper.AddInParameter("@PaymentType", DbType.Int16, v_POEffectiveDate.PaymentType)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, v_POEffectiveDate.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, v_POEffectiveDate.LastUpdateBy)
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

            Dim v_POEffectiveDate As v_POEffectiveDate = CType(obj, v_POEffectiveDate)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, v_POEffectiveDate.ID)
            DbCommandWrapper.AddInParameter("@Status", DbType.AnsiString, v_POEffectiveDate.Status)
            DbCommandWrapper.AddInParameter("@EffectiveDate", DbType.DateTime, v_POEffectiveDate.EffectiveDate)
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, v_POEffectiveDate.DealerID)
            DbCommandWrapper.AddInParameter("@CreditAccount", DbType.AnsiString, v_POEffectiveDate.CreditAccount)
            DbCommandWrapper.AddInParameter("@PaymentType", DbType.Int16, v_POEffectiveDate.PaymentType)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, v_POEffectiveDate.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, v_POEffectiveDate.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As v_POEffectiveDate

            Dim v_POEffectiveDate As v_POEffectiveDate = New v_POEffectiveDate

            v_POEffectiveDate.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then v_POEffectiveDate.Status = dr("Status").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("EffectiveDate")) Then v_POEffectiveDate.EffectiveDate = CType(dr("EffectiveDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then v_POEffectiveDate.DealerID = CType(dr("DealerID"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreditAccount")) Then v_POEffectiveDate.CreditAccount = dr("CreditAccount").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PaymentType")) Then v_POEffectiveDate.PaymentType = CType(dr("PaymentType"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then v_POEffectiveDate.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then v_POEffectiveDate.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then v_POEffectiveDate.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then v_POEffectiveDate.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then v_POEffectiveDate.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return v_POEffectiveDate

        End Function

        Private Sub SetTableName()

            If Not (GetType(v_POEffectiveDate) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(v_POEffectiveDate), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(v_POEffectiveDate).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

