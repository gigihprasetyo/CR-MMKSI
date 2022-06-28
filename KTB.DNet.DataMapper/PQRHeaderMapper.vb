#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : PQRHeader Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 10/9/2007 - 1:31:56 PM
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

    Public Class PQRHeaderMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertPQRHeader"
        Private m_UpdateStatement As String = "up_UpdatePQRHeader"
        Private m_RetrieveStatement As String = "up_RetrievePQRHeader"
        Private m_RetrieveListStatement As String = "up_RetrievePQRHeaderList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeletePQRHeader"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim pQRHeader As PQRHeader = Nothing
            While dr.Read

                pQRHeader = Me.CreateObject(dr)

            End While

            Return pQRHeader

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim pQRHeaderList As ArrayList = New ArrayList

            While dr.Read
                Dim pQRHeader As PQRHeader = Me.CreateObject(dr)
                pQRHeaderList.Add(pQRHeader)
            End While

            Return pQRHeaderList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim pQRHeader As PQRHeader = CType(obj, PQRHeader)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, pQRHeader.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim pQRHeader As PQRHeader = CType(obj, PQRHeader)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@PQRNo", DbType.AnsiString, pQRHeader.PQRNo)
            DbCommandWrapper.AddInParameter("@PQRType", DbType.Int32, pQRHeader.PQRType)
            DbCommandWrapper.AddInParameter("@RefPQRNo", DbType.AnsiString, pQRHeader.RefPQRNo)
            DbCommandWrapper.AddInParameter("@Year", DbType.Int32, pQRHeader.Year)
            DbCommandWrapper.AddInParameter("@SeqNo", DbType.Int32, pQRHeader.SeqNo)
            DbCommandWrapper.AddInParameter("@DocumentDate", DbType.DateTime, pQRHeader.DocumentDate)
            DbCommandWrapper.AddInParameter("@SoldDate", DbType.DateTime, pQRHeader.SoldDate)
            DbCommandWrapper.AddInParameter("@PQRDate", DbType.DateTime, pQRHeader.PQRDate)
            DbCommandWrapper.AddInParameter("@OdoMeter", DbType.Int32, pQRHeader.OdoMeter)
            DbCommandWrapper.AddInParameter("@Velocity", DbType.Int32, pQRHeader.Velocity)
            DbCommandWrapper.AddInParameter("@CustomerName", DbType.AnsiString, pQRHeader.CustomerName)
            DbCommandWrapper.AddInParameter("@CustomerAddress", DbType.AnsiString, pQRHeader.CustomerAddress)
            DbCommandWrapper.AddInParameter("@ValidationTime", DbType.DateTime, pQRHeader.ValidationTime)
            DbCommandWrapper.AddInParameter("@ConfirmBy", DbType.AnsiString, pQRHeader.ConfirmBy)
            DbCommandWrapper.AddInParameter("@ConfirmTime", DbType.DateTime, pQRHeader.ConfirmTime)
            DbCommandWrapper.AddInParameter("@RealeseTime", DbType.DateTime, pQRHeader.RealeseTime)
            DbCommandWrapper.AddInParameter("@IntervalProcess", DbType.DateTime, pQRHeader.IntervalProcess)
            DbCommandWrapper.AddInParameter("@Complexity", DbType.Int16, pQRHeader.Complexity)
            DbCommandWrapper.AddInParameter("@Subject", DbType.AnsiString, pQRHeader.Subject)
            DbCommandWrapper.AddInParameter("@Symptomps", DbType.AnsiString, pQRHeader.Symptomps)
            DbCommandWrapper.AddInParameter("@Causes", DbType.AnsiString, pQRHeader.Causes)
            DbCommandWrapper.AddInParameter("@Results", DbType.AnsiString, pQRHeader.Results)
            DbCommandWrapper.AddInParameter("@Notes", DbType.AnsiString, pQRHeader.Notes)
            DbCommandWrapper.AddInParameter("@Solutions", DbType.AnsiString, pQRHeader.Solutions)
            DbCommandWrapper.AddInParameter("@Bobot", DbType.Int32, pQRHeader.Bobot)
            DbCommandWrapper.AddInParameter("@ReleaseBy", DbType.AnsiString, pQRHeader.ReleaseBy)
            DbCommandWrapper.AddInParameter("@FinishBy", DbType.AnsiString, pQRHeader.FinishBy)
            DbCommandWrapper.AddInParameter("@FinishDate", DbType.DateTime, pQRHeader.FinishDate)
            DbCommandWrapper.AddInParameter("@CodeA", DbType.AnsiString, pQRHeader.CodeA)
            DbCommandWrapper.AddInParameter("@CodeB", DbType.AnsiString, pQRHeader.CodeB)
            DbCommandWrapper.AddInParameter("@CodeC", DbType.AnsiString, pQRHeader.CodeC)
            DbCommandWrapper.AddInParameter("@WorkOrderNumber", DbType.AnsiString, pQRHeader.WorkOrderNumber)
            DbCommandWrapper.AddInParameter("@InstallDate", DbType.DateTime, pQRHeader.InstallDate)
            DbCommandWrapper.AddInParameter("@InstallOdometer", DbType.Int32, pQRHeader.InstallOdometer)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, pQRHeader.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, pQRHeader.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@ChassisMasterID", DbType.Int32, Me.GetRefObject(pQRHeader.ChassisMaster))
            DbCommandWrapper.AddInParameter("@CategoryID", DbType.Int32, Me.GetRefObject(pQRHeader.Category))
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(pQRHeader.Dealer))
            DbCommandWrapper.AddInParameter("@DealerBranchID", DbType.Int32, Me.GetRefObject(pQRHeader.DealerBranch))
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

            Dim pQRHeader As PQRHeader = CType(obj, PQRHeader)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, pQRHeader.ID)
            DbCommandWrapper.AddInParameter("@PQRNo", DbType.AnsiString, pQRHeader.PQRNo)
            DbCommandWrapper.AddInParameter("@PQRType", DbType.Int32, pQRHeader.PQRType)
            DbCommandWrapper.AddInParameter("@RefPQRNo", DbType.AnsiString, pQRHeader.RefPQRNo)
            DbCommandWrapper.AddInParameter("@Year", DbType.Int32, pQRHeader.Year)
            DbCommandWrapper.AddInParameter("@SeqNo", DbType.Int32, pQRHeader.SeqNo)
            DbCommandWrapper.AddInParameter("@DocumentDate", DbType.DateTime, pQRHeader.DocumentDate)
            DbCommandWrapper.AddInParameter("@SoldDate", DbType.DateTime, pQRHeader.SoldDate)
            DbCommandWrapper.AddInParameter("@PQRDate", DbType.DateTime, pQRHeader.PQRDate)
            DbCommandWrapper.AddInParameter("@OdoMeter", DbType.Int32, pQRHeader.OdoMeter)
            DbCommandWrapper.AddInParameter("@Velocity", DbType.Int32, pQRHeader.Velocity)
            DbCommandWrapper.AddInParameter("@CustomerName", DbType.AnsiString, pQRHeader.CustomerName)
            DbCommandWrapper.AddInParameter("@CustomerAddress", DbType.AnsiString, pQRHeader.CustomerAddress)
            DbCommandWrapper.AddInParameter("@ValidationTime", DbType.DateTime, pQRHeader.ValidationTime)
            DbCommandWrapper.AddInParameter("@ConfirmBy", DbType.AnsiString, pQRHeader.ConfirmBy)
            DbCommandWrapper.AddInParameter("@ConfirmTime", DbType.DateTime, pQRHeader.ConfirmTime)
            DbCommandWrapper.AddInParameter("@RealeseTime", DbType.DateTime, pQRHeader.RealeseTime)
            DbCommandWrapper.AddInParameter("@IntervalProcess", DbType.DateTime, pQRHeader.IntervalProcess)
            DbCommandWrapper.AddInParameter("@Complexity", DbType.Int16, pQRHeader.Complexity)
            DbCommandWrapper.AddInParameter("@Subject", DbType.AnsiString, pQRHeader.Subject)
            DbCommandWrapper.AddInParameter("@Symptomps", DbType.AnsiString, pQRHeader.Symptomps)
            DbCommandWrapper.AddInParameter("@Causes", DbType.AnsiString, pQRHeader.Causes)
            DbCommandWrapper.AddInParameter("@Results", DbType.AnsiString, pQRHeader.Results)
            DbCommandWrapper.AddInParameter("@Notes", DbType.AnsiString, pQRHeader.Notes)
            DbCommandWrapper.AddInParameter("@Solutions", DbType.AnsiString, pQRHeader.Solutions)
            DbCommandWrapper.AddInParameter("@Bobot", DbType.Int32, pQRHeader.Bobot)
            DbCommandWrapper.AddInParameter("@ReleaseBy", DbType.AnsiString, pQRHeader.ReleaseBy)
            DbCommandWrapper.AddInParameter("@FinishBy", DbType.AnsiString, pQRHeader.FinishBy)
            DbCommandWrapper.AddInParameter("@FinishDate", DbType.DateTime, pQRHeader.FinishDate)
            DbCommandWrapper.AddInParameter("@CodeA", DbType.AnsiString, pQRHeader.CodeA)
            DbCommandWrapper.AddInParameter("@CodeB", DbType.AnsiString, pQRHeader.CodeB)
            DbCommandWrapper.AddInParameter("@CodeC", DbType.AnsiString, pQRHeader.CodeC)
            DbCommandWrapper.AddInParameter("@WorkOrderNumber", DbType.AnsiString, pQRHeader.WorkOrderNumber)
            DbCommandWrapper.AddInParameter("@InstallDate", DbType.DateTime, pQRHeader.InstallDate)
            DbCommandWrapper.AddInParameter("@InstallOdometer", DbType.Int32, pQRHeader.InstallOdometer)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, pQRHeader.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, pQRHeader.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@ChassisMasterID", DbType.Int32, Me.GetRefObject(pQRHeader.ChassisMaster))
            DbCommandWrapper.AddInParameter("@CategoryID", DbType.Int32, Me.GetRefObject(pQRHeader.Category))
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(pQRHeader.Dealer))
            DbCommandWrapper.AddInParameter("@DealerBranchID", DbType.Int32, Me.GetRefObject(pQRHeader.DealerBranch))
            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As PQRHeader

            Dim pQRHeader As PQRHeader = New PQRHeader

            pQRHeader.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("PQRNo")) Then pQRHeader.PQRNo = dr("PQRNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PQRType")) Then pQRHeader.PQRType = dr("PQRType").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RefPQRNo")) Then pQRHeader.RefPQRNo = dr("RefPQRNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Year")) Then pQRHeader.Year = CType(dr("Year"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("SeqNo")) Then pQRHeader.SeqNo = CType(dr("SeqNo"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("DocumentDate")) Then pQRHeader.DocumentDate = CType(dr("DocumentDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("SoldDate")) Then pQRHeader.SoldDate = CType(dr("SoldDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("PQRDate")) Then pQRHeader.PQRDate = CType(dr("PQRDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("OdoMeter")) Then pQRHeader.OdoMeter = CType(dr("OdoMeter"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Velocity")) Then pQRHeader.Velocity = CType(dr("Velocity"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("CustomerName")) Then pQRHeader.CustomerName = dr("CustomerName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CustomerAddress")) Then pQRHeader.CustomerAddress = dr("CustomerAddress").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ValidationTime")) Then pQRHeader.ValidationTime = CType(dr("ValidationTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ConfirmBy")) Then pQRHeader.ConfirmBy = dr("ConfirmBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ConfirmTime")) Then pQRHeader.ConfirmTime = CType(dr("ConfirmTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("RealeseTime")) Then pQRHeader.RealeseTime = CType(dr("RealeseTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("IntervalProcess")) Then pQRHeader.IntervalProcess = CType(dr("IntervalProcess"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("Complexity")) Then pQRHeader.Complexity = CType(dr("Complexity"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("Subject")) Then pQRHeader.Subject = dr("Subject").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Symptomps")) Then pQRHeader.Symptomps = dr("Symptomps").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Causes")) Then pQRHeader.Causes = dr("Causes").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Results")) Then pQRHeader.Results = dr("Results").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Notes")) Then pQRHeader.Notes = dr("Notes").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Solutions")) Then pQRHeader.Solutions = dr("Solutions").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Bobot")) Then pQRHeader.Bobot = CType(dr("Bobot"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ReleaseBy")) Then pQRHeader.ReleaseBy = dr("ReleaseBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("FinishBy")) Then pQRHeader.FinishBy = dr("FinishBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("FinishDate")) Then pQRHeader.FinishDate = CType(dr("FinishDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("CodeA")) Then pQRHeader.CodeA = dr("CodeA").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CodeB")) Then pQRHeader.CodeB = dr("CodeB").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CodeC")) Then pQRHeader.CodeC = dr("CodeC").ToString

            If Not dr.IsDBNull(dr.GetOrdinal("WorkOrderNumber")) Then pQRHeader.WorkOrderNumber = dr("WorkOrderNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("InstallDate")) Then pQRHeader.InstallDate = CType(dr("InstallDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("InstallOdometer")) Then pQRHeader.InstallOdometer = CType(dr("InstallOdometer"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then pQRHeader.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then pQRHeader.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then pQRHeader.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then pQRHeader.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then pQRHeader.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ChassisMasterID")) Then
                pQRHeader.ChassisMaster = New ChassisMaster(CType(dr("ChassisMasterID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("CategoryID")) Then
                pQRHeader.Category = New Category(CType(dr("CategoryID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                pQRHeader.Dealer = New Dealer(CType(dr("DealerID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("DealerBranchID")) Then
                pQRHeader.DealerBranch = New DealerBranch(CType(dr("DealerBranchID"), Integer))
            End If
            Return pQRHeader

        End Function

        Private Sub SetTableName()

            If Not (GetType(PQRHeader) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(PQRHeader), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(PQRHeader).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

