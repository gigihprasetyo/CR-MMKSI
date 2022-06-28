#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : PKTSpecimen Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2020 
'// ---------------------
'// $History      : $
'// Generated on 12/8/2020 - 1:25:24 PM
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

    Public Class PKTSpecimenMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertPKTSpecimen"
        Private m_UpdateStatement As String = "up_UpdatePKTSpecimen"
        Private m_RetrieveStatement As String = "up_RetrievePKTSpecimen"
        Private m_RetrieveListStatement As String = "up_RetrievePKTSpecimenList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeletePKTSpecimen"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim pKTSpecimen As PKTSpecimen = Nothing
            While dr.Read

                pKTSpecimen = Me.CreateObject(dr)

            End While

            Return pKTSpecimen

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim pKTSpecimenList As ArrayList = New ArrayList

            While dr.Read
                Dim pKTSpecimen As PKTSpecimen = Me.CreateObject(dr)
                pKTSpecimenList.Add(pKTSpecimen)
            End While

            Return pKTSpecimenList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim pKTSpecimen As PKTSpecimen = CType(obj, PKTSpecimen)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, pKTSpecimen.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim pKTSpecimen As PKTSpecimen = CType(obj, PKTSpecimen)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@Name", DbType.AnsiString, pKTSpecimen.Name)
            DbCommandWrapper.AddInParameter("@Position", DbType.AnsiString, pKTSpecimen.Position)
            DbCommandWrapper.AddInParameter("@Blok", DbType.AnsiString, pKTSpecimen.Blok)
            DbCommandWrapper.AddInParameter("@ValidFrom", DbType.DateTime, pKTSpecimen.ValidFrom)
            'DbCommandWrapper.AddInParameter("@ValidTo", DbType.DateTime, pKTSpecimen.ValidTo)
            DbCommandWrapper.AddInParameter("@Status", DbType.AnsiString, pKTSpecimen.Status)
            DbCommandWrapper.AddInParameter("@FileName", DbType.AnsiString, pKTSpecimen.FileName)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, pKTSpecimen.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdatedBy", DbType.AnsiString, pKTSpecimen.LastUpdatedBy)
            DbCommandWrapper.AddInParameter("@LastUpdatedTime", DbType.DateTime, pKTSpecimen.LastUpdatedTime)

            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(pKTSpecimen.Dealer))

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

            Dim pKTSpecimen As PKTSpecimen = CType(obj, PKTSpecimen)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, pKTSpecimen.ID)
            DbCommandWrapper.AddInParameter("@Name", DbType.AnsiString, pKTSpecimen.Name)
            DbCommandWrapper.AddInParameter("@Position", DbType.AnsiString, pKTSpecimen.Position)
            DbCommandWrapper.AddInParameter("@Blok", DbType.AnsiString, pKTSpecimen.Blok)
            DbCommandWrapper.AddInParameter("@ValidFrom", DbType.DateTime, pKTSpecimen.ValidFrom)
            'DbCommandWrapper.AddInParameter("@ValidTo", DbType.DateTime, pKTSpecimen.ValidTo)
            DbCommandWrapper.AddInParameter("@Status", DbType.AnsiString, pKTSpecimen.Status)
            DbCommandWrapper.AddInParameter("@FileName", DbType.AnsiString, pKTSpecimen.FileName)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, pKTSpecimen.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, pKTSpecimen.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdatedBy", DbType.AnsiString, pKTSpecimen.LastUpdatedBy)
            'DbCommandWrapper.AddInParameter("@LastUpdatedTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(pKTSpecimen.Dealer))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As PKTSpecimen

            Dim pKTSpecimen As PKTSpecimen = New PKTSpecimen

            pKTSpecimen.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Name")) Then pKTSpecimen.Name = dr("Name").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Position")) Then pKTSpecimen.Position = dr("Position").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Blok")) Then pKTSpecimen.Blok = dr("Blok").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ValidFrom")) Then pKTSpecimen.ValidFrom = CType(dr("ValidFrom"), DateTime)
            'If Not dr.IsDBNull(dr.GetOrdinal("ValidTo")) Then pKTSpecimen.ValidTo = CType(dr("ValidTo"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then pKTSpecimen.Status = dr("Status").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("FileName")) Then pKTSpecimen.FileName = dr("FileName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then pKTSpecimen.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then pKTSpecimen.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then pKTSpecimen.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdatedBy")) Then pKTSpecimen.LastUpdatedBy = dr("LastUpdatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdatedTime")) Then pKTSpecimen.LastUpdatedTime = CType(dr("LastUpdatedTime"), DateTime)

            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                pKTSpecimen.Dealer = New Dealer(CType(dr("DealerID"), Integer))
            End If

            Return pKTSpecimen

        End Function

        Private Sub SetTableName()

            If Not (GetType(PKTSpecimen) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(PKTSpecimen), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(PKTSpecimen).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace
