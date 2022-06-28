#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : PDISpecimen Objects Mapper.
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

    Public Class PDISpecimenMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertPDISpecimen"
        Private m_UpdateStatement As String = "up_UpdatePDISpecimen"
        Private m_RetrieveStatement As String = "up_RetrievePDISpecimen"
        Private m_RetrieveListStatement As String = "up_RetrievePDISpecimenList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeletePDISpecimen"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim pDISpecimen As PDISpecimen = Nothing
            While dr.Read

                pDISpecimen = Me.CreateObject(dr)

            End While

            Return pDISpecimen

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim pDISpecimenList As ArrayList = New ArrayList

            While dr.Read
                Dim PDISpecimen As PDISpecimen = Me.CreateObject(dr)
                pDISpecimenList.Add(PDISpecimen)
            End While

            Return pDISpecimenList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim pDISpecimen As PDISpecimen = CType(obj, PDISpecimen)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, pDISpecimen.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim pDISpecimen As PDISpecimen = CType(obj, PDISpecimen)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@Name", DbType.AnsiString, pDISpecimen.Name)
            DbCommandWrapper.AddInParameter("@Position", DbType.AnsiString, pDISpecimen.Position)
            DbCommandWrapper.AddInParameter("@Blok", DbType.AnsiString, pDISpecimen.Blok)
            DbCommandWrapper.AddInParameter("@ValidFrom", DbType.DateTime, pDISpecimen.ValidFrom)
            DbCommandWrapper.AddInParameter("@Status", DbType.AnsiString, pDISpecimen.Status)
            DbCommandWrapper.AddInParameter("@FileName", DbType.AnsiString, pDISpecimen.FileName)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, pDISpecimen.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdatedBy", DbType.AnsiString, pDISpecimen.LastUpdatedBy)
            'DbCommandWrapper.AddInParameter("@LastUpdatedTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(pDISpecimen.Dealer))

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

            Dim pDISpecimen As PDISpecimen = CType(obj, PDISpecimen)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, PDISpecimen.ID)
            DbCommandWrapper.AddInParameter("@Name", DbType.AnsiString, pDISpecimen.Name)
            DbCommandWrapper.AddInParameter("@Position", DbType.AnsiString, pDISpecimen.Position)
            DbCommandWrapper.AddInParameter("@Blok", DbType.AnsiString, pDISpecimen.Blok)
            DbCommandWrapper.AddInParameter("@ValidFrom", DbType.DateTime, pDISpecimen.ValidFrom)
            DbCommandWrapper.AddInParameter("@Status", DbType.AnsiString, pDISpecimen.Status)
            DbCommandWrapper.AddInParameter("@FileName", DbType.AnsiString, pDISpecimen.FileName)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, pDISpecimen.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, pDISpecimen.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdatedTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(pDISpecimen.Dealer))


            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As PDISpecimen

            Dim pDISpecimen As PDISpecimen = New PDISpecimen

            pDISpecimen.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Name")) Then pDISpecimen.Name = dr("Name").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Position")) Then pDISpecimen.Position = dr("Position").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Blok")) Then pDISpecimen.Blok = dr("Blok").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ValidFrom")) Then pDISpecimen.ValidFrom = CType(dr("ValidFrom"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then pDISpecimen.Status = dr("Status").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("FileName")) Then pDISpecimen.FileName = dr("FileName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then PDISpecimen.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then PDISpecimen.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then PDISpecimen.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdatedBy")) Then PDISpecimen.LastUpdatedBy = dr("LastUpdatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdatedTime")) Then PDISpecimen.LastUpdatedTime = CType(dr("LastUpdatedTime"), DateTime)

            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                PDISpecimen.Dealer = New Dealer(CType(dr("DealerID"), Integer))
            End If

            Return PDISpecimen

        End Function

        Private Sub SetTableName()

            If Not (GetType(PDISpecimen) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(PDISpecimen), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(PDISpecimen).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

