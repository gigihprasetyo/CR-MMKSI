
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : Mitrais Team
'// PURPOSE       : TrTrainee Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 23/08/2018 - 13:57:13
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

    Public Class TrTraineeMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertTrTrainee"
        Private m_UpdateStatement As String = "up_UpdateTrTrainee"
        Private m_RetrieveStatement As String = "up_RetrieveTrTrainee"
        Private m_RetrieveListStatement As String = "up_RetrieveTrTraineeList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteTrTrainee"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim trTrainee As TrTrainee = Nothing
            While dr.Read

                trTrainee = Me.CreateObject(dr)

            End While

            Return trTrainee

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim trTraineeList As ArrayList = New ArrayList

            While dr.Read
                Dim trTrainee As TrTrainee = Me.CreateObject(dr)
                trTraineeList.Add(trTrainee)
            End While

            Return trTraineeList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim trTrainee As TrTrainee = CType(obj, TrTrainee)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, trTrainee.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim trTrainee As TrTrainee = CType(obj, TrTrainee)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@Name", DbType.AnsiString, trTrainee.Name)
            DbCommandWrapper.AddInParameter("@BirthDate", DbType.DateTime, trTrainee.BirthDate)
            DbCommandWrapper.AddInParameter("@Gender", DbType.Byte, trTrainee.Gender)
            DbCommandWrapper.AddInParameter("@NoKTP", DbType.AnsiString, trTrainee.NoKTP)
            DbCommandWrapper.AddInParameter("@Email", DbType.AnsiString, trTrainee.Email)
            DbCommandWrapper.AddInParameter("@StartWorkingDate", DbType.DateTime, trTrainee.StartWorkingDate)
            DbCommandWrapper.AddInParameter("@Status", DbType.AnsiStringFixedLength, trTrainee.Status)
            DbCommandWrapper.AddInParameter("@JobPosition", DbType.AnsiString, trTrainee.JobPosition)
            DbCommandWrapper.AddInParameter("@EducationLevel", DbType.AnsiString, trTrainee.EducationLevel)
            DbCommandWrapper.AddInParameter("@Photo", DbType.Binary, trTrainee.Photo)
            DbCommandWrapper.AddInParameter("@ShirtSize", DbType.AnsiString, trTrainee.ShirtSize)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, trTrainee.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, trTrainee.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(trTrainee.Dealer))
            DbCommandWrapper.AddInParameter("@DealerBranchID", DbType.Int32, Me.GetRefObject(trTrainee.DealerBranch))
            DbCommandWrapper.AddInParameter("@SalesmanHeaderID", DbType.Int32, Me.GetRefObject(trTrainee.SalesmanHeader))

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

            Dim trTrainee As TrTrainee = CType(obj, TrTrainee)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, trTrainee.ID)
            DbCommandWrapper.AddInParameter("@Name", DbType.AnsiString, trTrainee.Name)
            DbCommandWrapper.AddInParameter("@BirthDate", DbType.DateTime, trTrainee.BirthDate)
            DbCommandWrapper.AddInParameter("@Gender", DbType.Byte, trTrainee.Gender)
            DbCommandWrapper.AddInParameter("@NoKTP", DbType.AnsiString, trTrainee.NoKTP)
            DbCommandWrapper.AddInParameter("@Email", DbType.AnsiString, trTrainee.Email)
            DbCommandWrapper.AddInParameter("@StartWorkingDate", DbType.DateTime, trTrainee.StartWorkingDate)
            DbCommandWrapper.AddInParameter("@Status", DbType.AnsiStringFixedLength, trTrainee.Status)
            DbCommandWrapper.AddInParameter("@JobPosition", DbType.AnsiString, trTrainee.JobPosition)
            DbCommandWrapper.AddInParameter("@EducationLevel", DbType.AnsiString, trTrainee.EducationLevel)
            DbCommandWrapper.AddInParameter("@Photo", DbType.Binary, trTrainee.Photo)
            DbCommandWrapper.AddInParameter("@ShirtSize", DbType.AnsiString, trTrainee.ShirtSize)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, trTrainee.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, trTrainee.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(trTrainee.Dealer))
            DbCommandWrapper.AddInParameter("@DealerBranchID", DbType.Int32, Me.GetRefObject(trTrainee.DealerBranch))
            DbCommandWrapper.AddInParameter("@SalesmanHeaderID", DbType.Int32, Me.GetRefObject(trTrainee.SalesmanHeader))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As TrTrainee

            Dim trTrainee As TrTrainee = New TrTrainee

            trTrainee.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Name")) Then trTrainee.Name = dr("Name").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("BirthDate")) Then trTrainee.BirthDate = CType(dr("BirthDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("Gender")) Then trTrainee.Gender = CType(dr("Gender"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("NoKTP")) Then trTrainee.NoKTP = dr("NoKTP").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Email")) Then trTrainee.Email = dr("Email").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("StartWorkingDate")) Then trTrainee.StartWorkingDate = CType(dr("StartWorkingDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then trTrainee.Status = dr("Status").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("JobPosition")) Then
                trTrainee.JobPosition = dr("JobPosition").ToString
               
            End If

            If Not dr.IsDBNull(dr.GetOrdinal("EducationLevel")) Then trTrainee.EducationLevel = dr("EducationLevel").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Photo")) Then trTrainee.Photo = CType(dr("Photo"), Byte())
            If Not dr.IsDBNull(dr.GetOrdinal("ShirtSize")) Then trTrainee.ShirtSize = dr("ShirtSize").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then trTrainee.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then trTrainee.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then trTrainee.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then trTrainee.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then trTrainee.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                trTrainee.Dealer = New Dealer(CType(dr("DealerID"), Short))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("DealerBranchID")) Then
                trTrainee.DealerBranch = New DealerBranch(CType(dr("DealerBranchID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("SalesmanHeaderID")) Then
                trTrainee.SalesmanHeader = New SalesmanHeader(CType(dr("SalesmanHeaderID"), Int32))
            End If

            Return trTrainee

        End Function

        Private Sub SetTableName()

            If Not (GetType(TrTrainee) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(TrTrainee), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(TrTrainee).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

