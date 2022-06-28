#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : TrAdditionalClass Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2019 
'// ---------------------
'// $History      : $
'// Generated on 8/1/2019 - 11:10:45 AM
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

    Public Class TrAdditionalClassMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertTrAdditionalClass"
        Private m_UpdateStatement As String = "up_UpdateTrAdditionalClass"
        Private m_RetrieveStatement As String = "up_RetrieveTrAdditionalClass"
        Private m_RetrieveListStatement As String = "up_RetrieveTrAdditionalClassList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteTrAdditionalClass"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim trAdditionalClass As TrAdditionalClass = Nothing
            While dr.Read

                trAdditionalClass = Me.CreateObject(dr)

            End While

            Return trAdditionalClass

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim trAdditionalClassList As ArrayList = New ArrayList

            While dr.Read
                Dim trAdditionalClass As TrAdditionalClass = Me.CreateObject(dr)
                trAdditionalClassList.Add(trAdditionalClass)
            End While

            Return trAdditionalClassList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim trAdditionalClass As TrAdditionalClass = CType(obj, TrAdditionalClass)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, trAdditionalClass.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim trAdditionalClass As TrAdditionalClass = CType(obj, TrAdditionalClass)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(trAdditionalClass.Dealer))
            DbCommandWrapper.AddInParameter("@ClassCode", DbType.AnsiString, trAdditionalClass.ClassCode)
            DbCommandWrapper.AddInParameter("@ClassName", DbType.AnsiString, trAdditionalClass.ClassName)
            DbCommandWrapper.AddInParameter("@CourseID", DbType.Int32, Me.GetRefObject(trAdditionalClass.TrCourse))
            DbCommandWrapper.AddInParameter("@Location", DbType.AnsiString, trAdditionalClass.Location)
            DbCommandWrapper.AddInParameter("@LocationName", DbType.AnsiString, trAdditionalClass.LocationName)
            DbCommandWrapper.AddInParameter("@Trainer1", DbType.AnsiString, trAdditionalClass.Trainer1)
            DbCommandWrapper.AddInParameter("@Trainer2", DbType.AnsiString, trAdditionalClass.Trainer2)
            DbCommandWrapper.AddInParameter("@Trainer3", DbType.AnsiString, trAdditionalClass.Trainer3)
            DbCommandWrapper.AddInParameter("@StartDate", DbType.DateTime, trAdditionalClass.StartDate)
            DbCommandWrapper.AddInParameter("@FinishDate", DbType.DateTime, trAdditionalClass.FinishDate)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, trAdditionalClass.Description)
            DbCommandWrapper.AddInParameter("@ClassType", DbType.Int16, trAdditionalClass.ClassType)
            DbCommandWrapper.AddInParameter("@CityID", DbType.Int32, Me.GetRefObject(trAdditionalClass.City))
            DbCommandWrapper.AddInParameter("@FileName", DbType.AnsiString, trAdditionalClass.FileName)
            DbCommandWrapper.AddInParameter("@FileMateriPath", DbType.AnsiString, trAdditionalClass.FileMateriPath)
            DbCommandWrapper.AddInParameter("@FileSiswaPath", DbType.AnsiString, trAdditionalClass.FileSiswaPath)
            DbCommandWrapper.AddInParameter("@FiscalYear", DbType.AnsiString, trAdditionalClass.FiscalYear)
            DbCommandWrapper.AddInParameter("@Status", DbType.AnsiStringFixedLength, trAdditionalClass.Status)
            DbCommandWrapper.AddInParameter("@APMResponse", DbType.AnsiString, trAdditionalClass.APMResponse)
            DbCommandWrapper.AddInParameter("@SubmitStatus", DbType.Int16, trAdditionalClass.SubmitStatus)
            DbCommandWrapper.AddInParameter("@Category", DbType.Int32, trAdditionalClass.Category)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, trAdditionalClass.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, trAdditionalClass.LastUpdateBy)
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

            Dim trAdditionalClass As TrAdditionalClass = CType(obj, TrAdditionalClass)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, trAdditionalClass.ID)
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(trAdditionalClass.Dealer))
            DbCommandWrapper.AddInParameter("@ClassCode", DbType.AnsiString, trAdditionalClass.ClassCode)
            DbCommandWrapper.AddInParameter("@ClassName", DbType.AnsiString, trAdditionalClass.ClassName)
            DbCommandWrapper.AddInParameter("@CourseID", DbType.Int32, Me.GetRefObject(trAdditionalClass.TrCourse))
            DbCommandWrapper.AddInParameter("@Location", DbType.AnsiString, trAdditionalClass.Location)
            DbCommandWrapper.AddInParameter("@LocationName", DbType.AnsiString, trAdditionalClass.LocationName)
            DbCommandWrapper.AddInParameter("@Trainer1", DbType.AnsiString, trAdditionalClass.Trainer1)
            DbCommandWrapper.AddInParameter("@Trainer2", DbType.AnsiString, trAdditionalClass.Trainer2)
            DbCommandWrapper.AddInParameter("@Trainer3", DbType.AnsiString, trAdditionalClass.Trainer3)
            DbCommandWrapper.AddInParameter("@StartDate", DbType.DateTime, trAdditionalClass.StartDate)
            DbCommandWrapper.AddInParameter("@FinishDate", DbType.DateTime, trAdditionalClass.FinishDate)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, trAdditionalClass.Description)
            DbCommandWrapper.AddInParameter("@ClassType", DbType.Int16, trAdditionalClass.ClassType)
            DbCommandWrapper.AddInParameter("@CityID", DbType.Int32, Me.GetRefObject(trAdditionalClass.City))
            DbCommandWrapper.AddInParameter("@FileName", DbType.AnsiString, trAdditionalClass.FileName)
            DbCommandWrapper.AddInParameter("@FileMateriPath", DbType.AnsiString, trAdditionalClass.FileMateriPath)
            DbCommandWrapper.AddInParameter("@FileSiswaPath", DbType.AnsiString, trAdditionalClass.FileSiswaPath)
            DbCommandWrapper.AddInParameter("@FiscalYear", DbType.AnsiString, trAdditionalClass.FiscalYear)
            DbCommandWrapper.AddInParameter("@Status", DbType.AnsiStringFixedLength, trAdditionalClass.Status)
            DbCommandWrapper.AddInParameter("@APMResponse", DbType.AnsiString, trAdditionalClass.APMResponse)
            DbCommandWrapper.AddInParameter("@SubmitStatus", DbType.Int16, trAdditionalClass.SubmitStatus)
            DbCommandWrapper.AddInParameter("@Category", DbType.Int32, trAdditionalClass.Category)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, trAdditionalClass.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, trAdditionalClass.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As TrAdditionalClass

            Dim trAdditionalClass As TrAdditionalClass = New TrAdditionalClass

            trAdditionalClass.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                trAdditionalClass.Dealer = New Dealer(CType(dr("DealerID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("ClassCode")) Then trAdditionalClass.ClassCode = dr("ClassCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ClassName")) Then trAdditionalClass.ClassName = dr("ClassName").ToString

            If Not dr.IsDBNull(dr.GetOrdinal("CourseID")) Then
                trAdditionalClass.TrCourse = New TrCourse(CType(dr("CourseID"), Integer))
            End If

            If Not dr.IsDBNull(dr.GetOrdinal("Location")) Then trAdditionalClass.Location = dr("Location").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LocationName")) Then trAdditionalClass.LocationName = dr("LocationName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Trainer1")) Then trAdditionalClass.Trainer1 = dr("Trainer1").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Trainer2")) Then trAdditionalClass.Trainer2 = dr("Trainer2").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Trainer3")) Then trAdditionalClass.Trainer3 = dr("Trainer3").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("StartDate")) Then trAdditionalClass.StartDate = CType(dr("StartDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("FinishDate")) Then trAdditionalClass.FinishDate = CType(dr("FinishDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("Description")) Then trAdditionalClass.Description = dr("Description").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ClassType")) Then trAdditionalClass.ClassType = CType(dr("ClassType"), Short)

            If Not dr.IsDBNull(dr.GetOrdinal("CityID")) Then
                trAdditionalClass.City = New City(CType(dr("CityID"), Integer))
            End If

            If Not dr.IsDBNull(dr.GetOrdinal("FileName")) Then trAdditionalClass.FileName = dr("FileName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("FileMateriPath")) Then trAdditionalClass.FileMateriPath = dr("FileMateriPath").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("FileSiswaPath")) Then trAdditionalClass.FileSiswaPath = dr("FileSiswaPath").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("FiscalYear")) Then trAdditionalClass.FiscalYear = dr("FiscalYear").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then trAdditionalClass.Status = dr("Status").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("APMResponse")) Then trAdditionalClass.APMResponse = dr("APMResponse").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SubmitStatus")) Then trAdditionalClass.SubmitStatus = CType(dr("SubmitStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("Category")) Then trAdditionalClass.Category = CType(dr("Category"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then trAdditionalClass.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then trAdditionalClass.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then trAdditionalClass.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then trAdditionalClass.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then trAdditionalClass.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return trAdditionalClass

        End Function

        Private Sub SetTableName()

            If Not (GetType(TrAdditionalClass) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(TrAdditionalClass), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(TrAdditionalClass).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace
