#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : NationalEventDetail Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2021 
'// ---------------------
'// $History      : $
'// Generated on 4/22/2021 - 8:40:54 AM
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

    Public Class NationalEventDetailMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertNationalEventDetail"
        Private m_UpdateStatement As String = "up_UpdateNationalEventDetail"
        Private m_RetrieveStatement As String = "up_RetrieveNationalEventDetail"
        Private m_RetrieveListStatement As String = "up_RetrieveNationalEventDetailList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteNationalEventDetail"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim nationalEventDetail As NationalEventDetail = Nothing
            While dr.Read

                nationalEventDetail = Me.CreateObject(dr)

            End While

            Return nationalEventDetail

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim nationalEventDetailList As ArrayList = New ArrayList

            While dr.Read
                Dim nationalEventDetail As NationalEventDetail = Me.CreateObject(dr)
                nationalEventDetailList.Add(nationalEventDetail)
            End While

            Return nationalEventDetailList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim nationalEventDetail As NationalEventDetail = CType(obj, NationalEventDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, nationalEventDetail.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim nationalEventDetail As NationalEventDetail = CType(obj, NationalEventDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@PICDealerName", DbType.AnsiString, nationalEventDetail.PICDealerName)
            DbCommandWrapper.AddInParameter("@PICDealerHPNo", DbType.AnsiString, nationalEventDetail.PICDealerHPNo)
            DbCommandWrapper.AddInParameter("@PICDealerEmail", DbType.AnsiString, nationalEventDetail.PICDealerEmail)
            DbCommandWrapper.AddInParameter("@SalesmanID", DbType.AnsiString, nationalEventDetail.SalesmanID)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, nationalEventDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, nationalEventDetail.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@NationalEventID", DbType.Int32, Me.GetRefObject(nationalEventDetail.NationalEvent))
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(nationalEventDetail.Dealer))

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

            Dim nationalEventDetail As NationalEventDetail = CType(obj, NationalEventDetail)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, nationalEventDetail.ID)
            DbCommandWrapper.AddInParameter("@PICDealerName", DbType.AnsiString, nationalEventDetail.PICDealerName)
            DbCommandWrapper.AddInParameter("@PICDealerHPNo", DbType.AnsiString, nationalEventDetail.PICDealerHPNo)
            DbCommandWrapper.AddInParameter("@PICDealerEmail", DbType.AnsiString, nationalEventDetail.PICDealerEmail)
            DbCommandWrapper.AddInParameter("@SalesmanID", DbType.AnsiString, nationalEventDetail.SalesmanID)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, nationalEventDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, nationalEventDetail.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@NationalEventID", DbType.Int32, Me.GetRefObject(nationalEventDetail.NationalEvent))
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(nationalEventDetail.Dealer))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As NationalEventDetail

            Dim nationalEventDetail As NationalEventDetail = New NationalEventDetail

            nationalEventDetail.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("PICDealerName")) Then nationalEventDetail.PICDealerName = dr("PICDealerName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PICDealerHPNo")) Then nationalEventDetail.PICDealerHPNo = dr("PICDealerHPNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PICDealerEmail")) Then nationalEventDetail.PICDealerEmail = dr("PICDealerEmail").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SalesmanID")) Then nationalEventDetail.SalesmanID = dr("SalesmanID").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then nationalEventDetail.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then nationalEventDetail.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then nationalEventDetail.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then nationalEventDetail.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then nationalEventDetail.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("NationalEventID")) Then
                nationalEventDetail.NationalEvent = New NationalEvent(CType(dr("NationalEventID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                nationalEventDetail.Dealer = New Dealer(CType(dr("DealerID"), Integer))
            End If

            Return nationalEventDetail

        End Function

        Private Sub SetTableName()

            If Not (GetType(NationalEventDetail) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(NationalEventDetail), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(NationalEventDetail).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace
