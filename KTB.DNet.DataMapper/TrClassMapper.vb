
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : TrClass Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2006 
'// ---------------------
'// $History      : $
'// Generated on 11/2/2006 - 3:55:48 PM
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

    Public Class TrClassMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertTrClass"
        Private m_UpdateStatement As String = "up_UpdateTrClass"
        Private m_RetrieveStatement As String = "up_RetrieveTrClass"
        Private m_RetrieveListStatement As String = "up_RetrieveTrClassList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteTrClass"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim trClass As TrClass = Nothing
            While dr.Read

                trClass = Me.CreateObject(dr)

            End While

            Return trClass

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim trClassList As ArrayList = New ArrayList

            While dr.Read
                Dim trClass As TrClass = Me.CreateObject(dr)
                trClassList.Add(trClass)
            End While

            Return trClassList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim trClass As TrClass = CType(obj, TrClass)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, trClass.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim trClass As TrClass = CType(obj, TrClass)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@ClassCode", DbType.AnsiString, trClass.ClassCode)
            DbCommandWrapper.AddInParameter("@ClassName", DbType.AnsiString, trClass.ClassName)
            DbCommandWrapper.AddInParameter("@Location", DbType.AnsiString, trClass.Location)
            DbCommandWrapper.AddInParameter("@Lodging", DbType.AnsiString, trClass.Lodging)
            DbCommandWrapper.AddInParameter("@LocationName", DbType.AnsiString, trClass.LocationName)
            DbCommandWrapper.AddInParameter("@Trainer1", DbType.AnsiString, trClass.Trainer1)
            DbCommandWrapper.AddInParameter("@Trainer2", DbType.AnsiString, trClass.Trainer2)
            DbCommandWrapper.AddInParameter("@Trainer3", DbType.AnsiString, trClass.Trainer3)
            DbCommandWrapper.AddInParameter("@StartDate", DbType.DateTime, trClass.StartDate)
            DbCommandWrapper.AddInParameter("@FinishDate", DbType.DateTime, trClass.FinishDate)
            DbCommandWrapper.AddInParameter("@ConfirmDueDate", DbType.DateTime, trClass.ConfirmDueDate)
            DbCommandWrapper.AddInParameter("@Capacity", DbType.Int32, trClass.Capacity)
            DbCommandWrapper.AddInParameter("@PaidDay", DbType.Int32, trClass.PaidDay)
            DbCommandWrapper.AddInParameter("@PricePerDay", DbType.Decimal, trClass.PricePerDay)
            DbCommandWrapper.AddInParameter("@PriceTotal", DbType.Decimal, trClass.PriceTotal)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, trClass.Description)
            DbCommandWrapper.AddInParameter("@ClassType", DbType.Int16, trClass.ClassType)
            DbCommandWrapper.AddInParameter("@CityID", DbType.Int32, Me.GetRefObject(trClass.City))
            DbCommandWrapper.AddInParameter("@TrMRTCID", DbType.Int32, Me.GetRefObject(trClass.TrMRTC))
            DbCommandWrapper.AddInParameter("@TrCertificateConfigID", DbType.Int32, Me.GetRefObject(trClass.TrCertificateConfig))
            DbCommandWrapper.AddInParameter("@FilePath", DbType.AnsiString, trClass.FilePath)
            DbCommandWrapper.AddInParameter("@UrlPath", DbType.AnsiString, trClass.UrlPath)
            DbCommandWrapper.AddInParameter("@FileCertificatePath", DbType.AnsiString, trClass.FileCertificatePath)
            DbCommandWrapper.AddInParameter("@FiscalYear", DbType.AnsiString, trClass.FiscalYear)
            DbCommandWrapper.AddInParameter("@Status", DbType.AnsiStringFixedLength, trClass.Status)
            DBCommandWrapper.AddInParameter("@SubmitStatus", DbType.Int16, trClass.SubmitStatus)
            DBCommandWrapper.AddInParameter("@Category", DbType.Int32, trClass.Category)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, trClass.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, trClass.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@CourseID", DbType.Int32, Me.GetRefObject(trClass.TrCourse))

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

            Dim trClass As TrClass = CType(obj, TrClass)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, trClass.ID)
            DbCommandWrapper.AddInParameter("@ClassCode", DbType.AnsiString, trClass.ClassCode)
            DbCommandWrapper.AddInParameter("@ClassName", DbType.AnsiString, trClass.ClassName)
            DbCommandWrapper.AddInParameter("@Location", DbType.AnsiString, trClass.Location)
            DbCommandWrapper.AddInParameter("@Lodging", DbType.AnsiString, trClass.Lodging)
            DbCommandWrapper.AddInParameter("@LocationName", DbType.AnsiString, trClass.LocationName)
            DbCommandWrapper.AddInParameter("@Trainer1", DbType.AnsiString, trClass.Trainer1)
            DbCommandWrapper.AddInParameter("@Trainer2", DbType.AnsiString, trClass.Trainer2)
            DbCommandWrapper.AddInParameter("@Trainer3", DbType.AnsiString, trClass.Trainer3)
            DbCommandWrapper.AddInParameter("@StartDate", DbType.DateTime, trClass.StartDate)
            DbCommandWrapper.AddInParameter("@FinishDate", DbType.DateTime, trClass.FinishDate)
            DbCommandWrapper.AddInParameter("@ConfirmDueDate", DbType.DateTime, trClass.ConfirmDueDate)
            DbCommandWrapper.AddInParameter("@Capacity", DbType.Int32, trClass.Capacity)
            DbCommandWrapper.AddInParameter("@PaidDay", DbType.Int32, trClass.PaidDay)
            DbCommandWrapper.AddInParameter("@PricePerDay", DbType.Decimal, trClass.PricePerDay)
            DbCommandWrapper.AddInParameter("@PriceTotal", DbType.Decimal, trClass.PriceTotal)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, trClass.Description)
            DbCommandWrapper.AddInParameter("@ClassType", DbType.Int16, trClass.ClassType)
            DbCommandWrapper.AddInParameter("@CityID", DbType.Int32, Me.GetRefObject(trClass.City))
            DbCommandWrapper.AddInParameter("@TrMRTCID", DbType.Int32, Me.GetRefObject(trClass.TrMRTC))
            DbCommandWrapper.AddInParameter("@TrCertificateConfigID", DbType.Int32, Me.GetRefObject(trClass.TrCertificateConfig))
            DbCommandWrapper.AddInParameter("@FileCertificatePath", DbType.AnsiString, trClass.FileCertificatePath)
            DbCommandWrapper.AddInParameter("@FilePath", DbType.AnsiString, trClass.FilePath)
            DbCommandWrapper.AddInParameter("@UrlPath", DbType.AnsiString, trClass.UrlPath)
            DbCommandWrapper.AddInParameter("@FiscalYear", DbType.AnsiString, trClass.FiscalYear)
            DbCommandWrapper.AddInParameter("@Status", DbType.AnsiStringFixedLength, trClass.Status)
            DBCommandWrapper.AddInParameter("@SubmitStatus", DbType.Int16, trClass.SubmitStatus)
            DBCommandWrapper.AddInParameter("@Category", DbType.Int32, trClass.Category)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, trClass.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, trClass.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@CourseID", DbType.Int32, Me.GetRefObject(trClass.TrCourse))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As TrClass

            Dim trClass As TrClass = New TrClass

            trClass.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ClassCode")) Then trClass.ClassCode = dr("ClassCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ClassName")) Then trClass.ClassName = dr("ClassName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Location")) Then trClass.Location = dr("Location").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Lodging")) Then trClass.Lodging = dr("Lodging").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LocationName")) Then trClass.LocationName = dr("LocationName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Trainer1")) Then trClass.Trainer1 = dr("Trainer1").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Trainer2")) Then trClass.Trainer2 = dr("Trainer2").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Trainer3")) Then trClass.Trainer3 = dr("Trainer3").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("StartDate")) Then trClass.StartDate = CType(dr("StartDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("PaidDay")) Then trClass.PaidDay = CType(dr("PaidDay"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("PricePerDay")) Then trClass.PricePerDay = CType(dr("PricePerDay"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("PriceTotal")) Then trClass.PriceTotal = CType(dr("PriceTotal"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("FinishDate")) Then trClass.FinishDate = CType(dr("FinishDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ConfirmDueDate")) Then trClass.ConfirmDueDate = CType(dr("ConfirmDueDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("Capacity")) Then trClass.Capacity = CType(dr("Capacity"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Description")) Then trClass.Description = dr("Description").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then trClass.Status = dr("Status").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SubmitStatus")) Then trClass.SubmitStatus = CType(dr("SubmitStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("Category")) Then trClass.Category = CType(dr("Category"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then trClass.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then trClass.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then trClass.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then trClass.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then trClass.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("CourseID")) Then
                trClass.TrCourse = New TrCourse(CType(dr("CourseID"), Integer))
            End If

            If Not dr.IsDBNull(dr.GetOrdinal("ClassType")) Then trClass.ClassType = CType(dr("ClassType"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("FilePath")) Then trClass.FilePath = CType(dr("FilePath"), String)
            If Not dr.IsDBNull(dr.GetOrdinal("UrlPath")) Then trClass.UrlPath = CType(dr("UrlPath"), String)
            If Not dr.IsDBNull(dr.GetOrdinal("FileCertificatePath")) Then trClass.FileCertificatePath = CType(dr("FileCertificatePath"), String)
            If Not dr.IsDBNull(dr.GetOrdinal("FiscalYear")) Then trClass.FiscalYear = CType(dr("FiscalYear"), String)

            If Not dr.IsDBNull(dr.GetOrdinal("CityID")) Then
                trClass.City = New City(CType(dr("CityID"), Integer))
            End If

            If Not dr.IsDBNull(dr.GetOrdinal("TrMRTCID")) Then
                trClass.TrMRTC = New TrMRTC(CType(dr("TrMRTCID"), Integer))
            End If

            If Not dr.IsDBNull(dr.GetOrdinal("TrCertificateConfigID")) Then
                trClass.TrCertificateConfig = New TrCertificateConfig(CType(dr("TrCertificateConfigID"), Integer))
            End If

            Return trClass

        End Function

        Private Sub SetTableName()

            If Not (GetType(TrClass) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(TrClass), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(TrClass).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

