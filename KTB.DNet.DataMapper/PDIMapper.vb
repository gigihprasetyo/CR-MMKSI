#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : PDI Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2005 
'// ---------------------
'// $History      : $
'// Generated on 11/30/2005 - 1:05:06 PM
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

    Public Class PDIMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertPDI"
        Private m_UpdateStatement As String = "up_UpdatePDI"
        Private m_RetrieveStatement As String = "up_RetrievePDI"
        Private m_RetrieveListStatement As String = "up_RetrievePDIList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeletePDI"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim pDI As PDI = Nothing
            While dr.Read

                pDI = Me.CreateObject(dr)

            End While

            Return pDI

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim pDIList As ArrayList = New ArrayList

            While dr.Read
                Dim pDI As PDI = Me.CreateObject(dr)
                pDIList.Add(pDI)
            End While

            Return pDIList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim pDI As PDI = CType(obj, PDI)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, pDI.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim pDI As PDI = CType(obj, PDI)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@Kind", DbType.AnsiStringFixedLength, pDI.Kind)
            DbCommandWrapper.AddInParameter("@PDIStatus", DbType.AnsiStringFixedLength, pDI.PDIStatus)
            DbCommandWrapper.AddInParameter("@PDIDate", DbType.DateTime, pDI.PDIDate)
            DbCommandWrapper.AddInParameter("@ReleaseBy", DbType.AnsiString, pDI.ReleaseBy)
            DbCommandWrapper.AddInParameter("@ReleaseDate", DbType.DateTime, pDI.ReleaseDate)
            DbCommandWrapper.AddInParameter("@WorkOrderNumber", DbType.AnsiString, pDI.WorkOrderNumber)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, pDI.RowStatus)
            DbCommandWrapper.AddInParameter("@FileName", DbType.AnsiString, pDI.FileName)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, pDI.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@ChassisMasterID", DbType.Int32, Me.GetRefObject(pDI.ChassisMaster))
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(pDI.Dealer))
            DbCommandWrapper.AddInParameter("@DealerBranchID", DbType.Int32, Me.GetRefObject(pDI.DealerBranch))

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

            Dim pDI As PDI = CType(obj, PDI)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, pDI.ID)
            DbCommandWrapper.AddInParameter("@Kind", DbType.AnsiStringFixedLength, pDI.Kind)
            DbCommandWrapper.AddInParameter("@PDIStatus", DbType.AnsiStringFixedLength, pDI.PDIStatus)
            DbCommandWrapper.AddInParameter("@PDIDate", DbType.DateTime, pDI.PDIDate)
            DbCommandWrapper.AddInParameter("@ReleaseBy", DbType.AnsiString, pDI.ReleaseBy)
            DbCommandWrapper.AddInParameter("@ReleaseDate", DbType.DateTime, pDI.ReleaseDate)
            DbCommandWrapper.AddInParameter("@WorkOrderNumber", DbType.AnsiString, pDI.WorkOrderNumber)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, pDI.RowStatus)
            DbCommandWrapper.AddInParameter("@FileName", DbType.AnsiString, pDI.FileName)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, pDI.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@ChassisMasterID", DbType.Int32, Me.GetRefObject(pDI.ChassisMaster))
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(pDI.Dealer))
            DbCommandWrapper.AddInParameter("@DealerBranchID", DbType.Int32, Me.GetRefObject(pDI.DealerBranch))


            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As PDI

            Dim pDI As PDI = New PDI

            pDI.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Kind")) Then pDI.Kind = dr("Kind").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PDIStatus")) Then pDI.PDIStatus = dr("PDIStatus").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PDIDate")) Then pDI.PDIDate = CType(dr("PDIDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ReleaseBy")) Then pDI.ReleaseBy = dr("ReleaseBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ReleaseDate")) Then pDI.ReleaseDate = CType(dr("ReleaseDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("WorkOrderNumber")) Then pDI.WorkOrderNumber = dr("WorkOrderNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then pDI.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("FileName")) Then pDI.FileName = dr("FileName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then pDI.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then pDI.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then pDI.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then pDI.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ChassisMasterID")) Then
                pDI.ChassisMaster = New ChassisMaster(CType(dr("ChassisMasterID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                pDI.Dealer = New Dealer(CType(dr("DealerID"), Integer))
            End If

            If Not dr.IsDBNull(dr.GetOrdinal("DealerBranchID")) Then
                pDI.DealerBranch = New DealerBranch(CType(dr("DealerBranchID"), Integer))
            End If

            Return pDI

        End Function

        Private Sub SetTableName()

            If Not (GetType(PDI) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(PDI), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(PDI).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

