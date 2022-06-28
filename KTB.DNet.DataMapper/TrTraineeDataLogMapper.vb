#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : TrTraineeDataLog Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2019 
'// ---------------------
'// $History      : $
'// Generated on 10/14/2019 - 8:40:34 AM
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

    Public Class TrTraineeDataLogMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertTrTraineeDataLog"
        Private m_UpdateStatement As String = "up_UpdateTrTraineeDataLog"
        Private m_RetrieveStatement As String = "up_RetrieveTrTraineeDataLog"
        Private m_RetrieveListStatement As String = "up_RetrieveTrTraineeDataLogList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteTrTraineeDataLog"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim trTraineeDataLog As TrTraineeDataLog = Nothing
            While dr.Read

                trTraineeDataLog = Me.CreateObject(dr)

            End While

            Return trTraineeDataLog

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim trTraineeDataLogList As ArrayList = New ArrayList

            While dr.Read
                Dim trTraineeDataLog As TrTraineeDataLog = Me.CreateObject(dr)
                trTraineeDataLogList.Add(trTraineeDataLog)
            End While

            Return trTraineeDataLogList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim trTraineeDataLog As TrTraineeDataLog = CType(obj, TrTraineeDataLog)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, trTraineeDataLog.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim trTraineeDataLog As TrTraineeDataLog = CType(obj, TrTraineeDataLog)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@TrTraineeID", DbType.Int32, Me.GetRefObject(trTraineeDataLog.TrTrainee))
            DbCommandWrapper.AddInParameter("@SalesmanHeaderID", DbType.Int32, Me.GetRefObject(trTraineeDataLog.SalesmanHeader))
            DbCommandWrapper.AddInParameter("@Name", DbType.AnsiString, trTraineeDataLog.Name)
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(trTraineeDataLog.Dealer))
            DbCommandWrapper.AddInParameter("@DealerBranchID", DbType.Int32, Me.GetRefObject(trTraineeDataLog.DealerBranch))
            DbCommandWrapper.AddInParameter("@BirthDate", DbType.DateTime, trTraineeDataLog.BirthDate)
            DbCommandWrapper.AddInParameter("@Gender", DbType.Byte, trTraineeDataLog.Gender)
            DbCommandWrapper.AddInParameter("@NoKTP", DbType.AnsiString, trTraineeDataLog.NoKTP)
            DbCommandWrapper.AddInParameter("@Email", DbType.AnsiString, trTraineeDataLog.Email)
            DbCommandWrapper.AddInParameter("@StartWorkingDate", DbType.DateTime, trTraineeDataLog.StartWorkingDate)
            DbCommandWrapper.AddInParameter("@Status", DbType.AnsiStringFixedLength, trTraineeDataLog.Status)
            DbCommandWrapper.AddInParameter("@JobPositionCode", DbType.AnsiString, trTraineeDataLog.JobPositionCode)
            DbCommandWrapper.AddInParameter("@EducationLevel", DbType.AnsiString, trTraineeDataLog.EducationLevel)
            DbCommandWrapper.AddInParameter("@Photo", DbType.Binary, trTraineeDataLog.Photo)
            DbCommandWrapper.AddInParameter("@ShirtSize", DbType.AnsiString, trTraineeDataLog.ShirtSize)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, trTraineeDataLog.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, trTraineeDataLog.LastUpdateBy)
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

            Dim trTraineeDataLog As TrTraineeDataLog = CType(obj, TrTraineeDataLog)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, trTraineeDataLog.ID)
            DbCommandWrapper.AddInParameter("@TrTraineeID", DbType.Int32, Me.GetRefObject(trTraineeDataLog.TrTrainee))
            DbCommandWrapper.AddInParameter("@SalesmanHeaderID", DbType.Int32, Me.GetRefObject(trTraineeDataLog.SalesmanHeader))
            DbCommandWrapper.AddInParameter("@Name", DbType.AnsiString, trTraineeDataLog.Name)
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(trTraineeDataLog.Dealer))
            DbCommandWrapper.AddInParameter("@DealerBranchID", DbType.Int32, Me.GetRefObject(trTraineeDataLog.DealerBranch))
            DbCommandWrapper.AddInParameter("@BirthDate", DbType.DateTime, trTraineeDataLog.BirthDate)
            DbCommandWrapper.AddInParameter("@Gender", DbType.Byte, trTraineeDataLog.Gender)
            DbCommandWrapper.AddInParameter("@NoKTP", DbType.AnsiString, trTraineeDataLog.NoKTP)
            DbCommandWrapper.AddInParameter("@Email", DbType.AnsiString, trTraineeDataLog.Email)
            DbCommandWrapper.AddInParameter("@StartWorkingDate", DbType.DateTime, trTraineeDataLog.StartWorkingDate)
            DbCommandWrapper.AddInParameter("@Status", DbType.AnsiStringFixedLength, trTraineeDataLog.Status)
            DbCommandWrapper.AddInParameter("@JobPositionCode", DbType.AnsiString, trTraineeDataLog.JobPositionCode)
            DbCommandWrapper.AddInParameter("@EducationLevel", DbType.AnsiString, trTraineeDataLog.EducationLevel)
            DbCommandWrapper.AddInParameter("@Photo", DbType.Binary, trTraineeDataLog.Photo)
            DbCommandWrapper.AddInParameter("@ShirtSize", DbType.AnsiString, trTraineeDataLog.ShirtSize)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, trTraineeDataLog.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, trTraineeDataLog.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As TrTraineeDataLog

            Dim trTraineeDataLog As TrTraineeDataLog = New TrTraineeDataLog

            trTraineeDataLog.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Name")) Then trTraineeDataLog.Name = dr("Name").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("BirthDate")) Then trTraineeDataLog.BirthDate = CType(dr("BirthDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("Gender")) Then trTraineeDataLog.Gender = CType(dr("Gender"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("NoKTP")) Then trTraineeDataLog.NoKTP = dr("NoKTP").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("JobPositionCode")) Then trTraineeDataLog.JobPositionCode = dr("JobPositionCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Email")) Then trTraineeDataLog.Email = dr("Email").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("StartWorkingDate")) Then trTraineeDataLog.StartWorkingDate = CType(dr("StartWorkingDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then trTraineeDataLog.Status = dr("Status").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("EducationLevel")) Then trTraineeDataLog.EducationLevel = dr("EducationLevel").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Photo")) Then trTraineeDataLog.Photo = CType(dr("Photo"), Byte())
            If Not dr.IsDBNull(dr.GetOrdinal("ShirtSize")) Then trTraineeDataLog.ShirtSize = dr("ShirtSize").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then trTraineeDataLog.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then trTraineeDataLog.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then trTraineeDataLog.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then trTraineeDataLog.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then trTraineeDataLog.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            If Not dr.IsDBNull(dr.GetOrdinal("TrTraineeID")) Then
                trTraineeDataLog.TrTrainee = New TrTrainee(CType(dr("TrTraineeID"), Integer))
            End If

            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                trTraineeDataLog.Dealer = New Dealer(CType(dr("DealerID"), Short))
            End If

            If Not dr.IsDBNull(dr.GetOrdinal("SalesmanHeaderID")) Then
                trTraineeDataLog.SalesmanHeader = New SalesmanHeader(CType(dr("SalesmanHeaderID"), Int32))
            End If

            If Not dr.IsDBNull(dr.GetOrdinal("DealerBranchID")) Then
                trTraineeDataLog.DealerBranch = New DealerBranch(CType(dr("DealerBranchID"), Integer))
            End If

           

            Return trTraineeDataLog

        End Function

        Private Sub SetTableName()

            If Not (GetType(TrTraineeDataLog) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(TrTraineeDataLog), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(TrTraineeDataLog).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace
