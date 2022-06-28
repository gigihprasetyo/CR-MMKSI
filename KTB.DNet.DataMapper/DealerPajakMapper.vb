
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : DealerPajak Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2012 
'// ---------------------
'// $History      : $
'// Generated on 9/27/2012 - 4:54:54 PM
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

    Public Class DealerPajakMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertDealerPajak"
        Private m_UpdateStatement As String = "up_UpdateDealerPajak"
        Private m_RetrieveStatement As String = "up_RetrieveDealerPajak"
        Private m_RetrieveListStatement As String = "up_RetrieveDealerPajakList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteDealerPajak"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim dealerPajak As DealerPajak = Nothing
            While dr.Read

                dealerPajak = Me.CreateObject(dr)

            End While

            Return dealerPajak

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim dealerPajakList As ArrayList = New ArrayList

            While dr.Read
                Dim dealerPajak As DealerPajak = Me.CreateObject(dr)
                dealerPajakList.Add(dealerPajak)
            End While

            Return dealerPajakList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim dealerPajak As DealerPajak = CType(obj, DealerPajak)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, dealerPajak.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim dealerPajak As DealerPajak = CType(obj, DealerPajak)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@NPWP", DbType.String, dealerPajak.NPWP)
            DbCommandWrapper.AddInParameter("@KPP", DbType.AnsiString, dealerPajak.KPP)
            DbCommandWrapper.AddInParameter("@Pejabat1", DbType.AnsiString, dealerPajak.Pejabat1)
            DbCommandWrapper.AddInParameter("@Jabatan1", DbType.AnsiString, dealerPajak.Jabatan1)
            DbCommandWrapper.AddInParameter("@Pejabat2", DbType.AnsiString, dealerPajak.Pejabat2)
            DbCommandWrapper.AddInParameter("@Jabatan2", DbType.AnsiString, dealerPajak.Jabatan2)
            DbCommandWrapper.AddInParameter("@Pejabat3", DbType.AnsiString, dealerPajak.Pejabat3)
            DbCommandWrapper.AddInParameter("@Jabatan3", DbType.AnsiString, dealerPajak.Jabatan3)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, dealerPajak.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, dealerPajak.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(dealerPajak.Dealer))

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

            Dim dealerPajak As DealerPajak = CType(obj, DealerPajak)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, dealerPajak.ID)
            DbCommandWrapper.AddInParameter("@NPWP", DbType.String, dealerPajak.NPWP)
            DbCommandWrapper.AddInParameter("@KPP", DbType.AnsiString, dealerPajak.KPP)
            DbCommandWrapper.AddInParameter("@Pejabat1", DbType.AnsiString, dealerPajak.Pejabat1)
            DbCommandWrapper.AddInParameter("@Jabatan1", DbType.AnsiString, dealerPajak.Jabatan1)
            DbCommandWrapper.AddInParameter("@Pejabat2", DbType.AnsiString, dealerPajak.Pejabat2)
            DbCommandWrapper.AddInParameter("@Jabatan2", DbType.AnsiString, dealerPajak.Jabatan2)
            DbCommandWrapper.AddInParameter("@Pejabat3", DbType.AnsiString, dealerPajak.Pejabat3)
            DbCommandWrapper.AddInParameter("@Jabatan3", DbType.AnsiString, dealerPajak.Jabatan3)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, dealerPajak.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, dealerPajak.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(dealerPajak.Dealer))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As DealerPajak

            Dim dealerPajak As DealerPajak = New DealerPajak

            dealerPajak.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("NPWP")) Then dealerPajak.NPWP = dr("NPWP").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("KPP")) Then dealerPajak.KPP = dr("KPP").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Pejabat1")) Then dealerPajak.Pejabat1 = dr("Pejabat1").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Jabatan1")) Then dealerPajak.Jabatan1 = dr("Jabatan1").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Pejabat2")) Then dealerPajak.Pejabat2 = dr("Pejabat2").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Jabatan2")) Then dealerPajak.Jabatan2 = dr("Jabatan2").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Pejabat3")) Then dealerPajak.Pejabat3 = dr("Pejabat3").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Jabatan3")) Then dealerPajak.Jabatan3 = dr("Jabatan3").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then dealerPajak.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then dealerPajak.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then dealerPajak.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then dealerPajak.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then dealerPajak.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                dealerPajak.Dealer = New Dealer(CType(dr("DealerID"), Short))
            End If

            Return dealerPajak

        End Function

        Private Sub SetTableName()

            If Not (GetType(DealerPajak) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(DealerPajak), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(DealerPajak).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

