#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : PengajuanDesignIklan Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 8/23/2007 - 11:40:01 AM
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

    Public Class PengajuanDesignIklanMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertPengajuanDesignIklan"
        Private m_UpdateStatement As String = "up_UpdatePengajuanDesignIklan"
        Private m_RetrieveStatement As String = "up_RetrievePengajuanDesignIklan"
        Private m_RetrieveListStatement As String = "up_RetrievePengajuanDesignIklanList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeletePengajuanDesignIklan"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim pengajuanDesignIklan As PengajuanDesignIklan = Nothing
            While dr.Read

                pengajuanDesignIklan = Me.CreateObject(dr)

            End While

            Return pengajuanDesignIklan

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim pengajuanDesignIklanList As ArrayList = New ArrayList

            While dr.Read
                Dim pengajuanDesignIklan As PengajuanDesignIklan = Me.CreateObject(dr)
                pengajuanDesignIklanList.Add(pengajuanDesignIklan)
            End While

            Return pengajuanDesignIklanList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim pengajuanDesignIklan As PengajuanDesignIklan = CType(obj, PengajuanDesignIklan)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, pengajuanDesignIklan.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim pengajuanDesignIklan As PengajuanDesignIklan = CType(obj, PengajuanDesignIklan)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@NamaIklanKTB", DbType.AnsiString, pengajuanDesignIklan.NamaIklanKTB)
            DbCommandWrapper.AddInParameter("@NamaIklanDealer", DbType.AnsiString, pengajuanDesignIklan.NamaIklanDealer)
            DbCommandWrapper.AddInParameter("@UploadeIklan", DbType.AnsiString, pengajuanDesignIklan.UploadeIklan)
            DbCommandWrapper.AddInParameter("@Note", DbType.AnsiString, pengajuanDesignIklan.Note)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, pengajuanDesignIklan.Description)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int32, pengajuanDesignIklan.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, pengajuanDesignIklan.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, pengajuanDesignIklan.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(pengajuanDesignIklan.Dealer))

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

            Dim pengajuanDesignIklan As PengajuanDesignIklan = CType(obj, PengajuanDesignIklan)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, pengajuanDesignIklan.ID)
            DbCommandWrapper.AddInParameter("@NamaIklanKTB", DbType.AnsiString, pengajuanDesignIklan.NamaIklanKTB)
            DbCommandWrapper.AddInParameter("@NamaIklanDealer", DbType.AnsiString, pengajuanDesignIklan.NamaIklanDealer)
            DbCommandWrapper.AddInParameter("@UploadeIklan", DbType.AnsiString, pengajuanDesignIklan.UploadeIklan)
            DbCommandWrapper.AddInParameter("@Note", DbType.AnsiString, pengajuanDesignIklan.Note)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, pengajuanDesignIklan.Description)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int32, pengajuanDesignIklan.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, pengajuanDesignIklan.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, pengajuanDesignIklan.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(pengajuanDesignIklan.Dealer))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As PengajuanDesignIklan

            Dim pengajuanDesignIklan As PengajuanDesignIklan = New PengajuanDesignIklan

            pengajuanDesignIklan.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("NamaIklanKTB")) Then pengajuanDesignIklan.NamaIklanKTB = dr("NamaIklanKTB").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("NamaIklanDealer")) Then pengajuanDesignIklan.NamaIklanDealer = dr("NamaIklanDealer").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("UploadeIklan")) Then pengajuanDesignIklan.UploadeIklan = dr("UploadeIklan").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Note")) Then pengajuanDesignIklan.Note = dr("Note").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Description")) Then pengajuanDesignIklan.Description = dr("Description").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then pengajuanDesignIklan.Status = CType(dr("Status"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then pengajuanDesignIklan.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then pengajuanDesignIklan.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then pengajuanDesignIklan.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then pengajuanDesignIklan.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then pengajuanDesignIklan.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                pengajuanDesignIklan.Dealer = New Dealer(CType(dr("DealerID"), Integer))
            End If

            Return pengajuanDesignIklan

        End Function

        Private Sub SetTableName()

            If Not (GetType(PengajuanDesignIklan) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(PengajuanDesignIklan), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(PengajuanDesignIklan).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

