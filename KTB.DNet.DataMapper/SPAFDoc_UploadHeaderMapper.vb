
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SPAFDoc_UploadHeader Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2010 
'// ---------------------
'// $History      : $
'// Generated on 5/25/2010 - 4:23:14 PM
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

    Public Class SPAFDoc_UploadHeaderMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertSPAFDoc_UploadHeader"
        Private m_UpdateStatement As String = "up_UpdateSPAFDoc_UploadHeader"
        Private m_RetrieveStatement As String = "up_RetrieveSPAFDoc_UploadHeader"
        Private m_RetrieveListStatement As String = "up_RetrieveSPAFDoc_UploadHeaderList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteSPAFDoc_UploadHeader"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim sPAFDoc_UploadHeader As SPAFDoc_UploadHeader = Nothing
            While dr.Read

                sPAFDoc_UploadHeader = Me.CreateObject(dr)

            End While

            Return sPAFDoc_UploadHeader

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim sPAFDoc_UploadHeaderList As ArrayList = New ArrayList

            While dr.Read
                Dim sPAFDoc_UploadHeader As SPAFDoc_UploadHeader = Me.CreateObject(dr)
                sPAFDoc_UploadHeaderList.Add(sPAFDoc_UploadHeader)
            End While

            Return sPAFDoc_UploadHeaderList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sPAFDoc_UploadHeader As SPAFDoc_UploadHeader = CType(obj, SPAFDoc_UploadHeader)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, sPAFDoc_UploadHeader.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sPAFDoc_UploadHeader As SPAFDoc_UploadHeader = CType(obj, SPAFDoc_UploadHeader)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@Filename", DbType.AnsiString, sPAFDoc_UploadHeader.Filename)
            DBCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(sPAFDoc_UploadHeader.Dealer))
            DbCommandWrapper.AddInParameter("@NumberOfData", DbType.Int32, sPAFDoc_UploadHeader.NumberOfData)
            DbCommandWrapper.AddInParameter("@NumberOfValid", DbType.Int32, sPAFDoc_UploadHeader.NumberOfValid)
            DbCommandWrapper.AddInParameter("@NumberOfError", DbType.Int32, sPAFDoc_UploadHeader.NumberOfError)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, sPAFDoc_UploadHeader.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, sPAFDoc_UploadHeader.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, sPAFDoc_UploadHeader.LastUpdateBy)
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

            Dim sPAFDoc_UploadHeader As SPAFDoc_UploadHeader = CType(obj, SPAFDoc_UploadHeader)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, sPAFDoc_UploadHeader.ID)
            DbCommandWrapper.AddInParameter("@Filename", DbType.AnsiString, sPAFDoc_UploadHeader.Filename)
            DBCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(sPAFDoc_UploadHeader.Dealer))
            DbCommandWrapper.AddInParameter("@NumberOfData", DbType.Int32, sPAFDoc_UploadHeader.NumberOfData)
            DbCommandWrapper.AddInParameter("@NumberOfValid", DbType.Int32, sPAFDoc_UploadHeader.NumberOfValid)
            DbCommandWrapper.AddInParameter("@NumberOfError", DbType.Int32, sPAFDoc_UploadHeader.NumberOfError)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, sPAFDoc_UploadHeader.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, sPAFDoc_UploadHeader.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, sPAFDoc_UploadHeader.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As SPAFDoc_UploadHeader

            Dim sPAFDoc_UploadHeader As SPAFDoc_UploadHeader = New SPAFDoc_UploadHeader

            sPAFDoc_UploadHeader.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Filename")) Then sPAFDoc_UploadHeader.Filename = dr("Filename").ToString
            'If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then sPAFDoc_UploadHeader.DealerID = CType(dr("DealerID"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("NumberOfData")) Then sPAFDoc_UploadHeader.NumberOfData = CType(dr("NumberOfData"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("NumberOfValid")) Then sPAFDoc_UploadHeader.NumberOfValid = CType(dr("NumberOfValid"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("NumberOfError")) Then sPAFDoc_UploadHeader.NumberOfError = CType(dr("NumberOfError"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then sPAFDoc_UploadHeader.Status = CType(dr("Status"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then sPAFDoc_UploadHeader.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then sPAFDoc_UploadHeader.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then sPAFDoc_UploadHeader.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then sPAFDoc_UploadHeader.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then sPAFDoc_UploadHeader.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                sPAFDoc_UploadHeader.Dealer = New Dealer(CType(dr("DealerID"), Short))
            End If
            Return sPAFDoc_UploadHeader

        End Function

        Private Sub SetTableName()

            If Not (GetType(SPAFDoc_UploadHeader) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(SPAFDoc_UploadHeader), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(SPAFDoc_UploadHeader).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

