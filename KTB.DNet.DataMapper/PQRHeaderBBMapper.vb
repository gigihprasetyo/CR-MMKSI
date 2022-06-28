#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : PQRHeaderBB Objects Mapper.
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

    Public Class PQRHeaderBBMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertPQRHeaderBB"
        Private m_UpdateStatement As String = "up_UpdatePQRHeaderBB"
        Private m_RetrieveStatement As String = "up_RetrievePQRHeaderBB"
        Private m_RetrieveListStatement As String = "up_RetrievePQRHeaderBBList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeletePQRHeaderBB"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim PQRHeaderBB As PQRHeaderBB = Nothing
            While dr.Read

                PQRHeaderBB = Me.CreateObject(dr)

            End While

            Return PQRHeaderBB

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim PQRHeaderBBList As ArrayList = New ArrayList

            While dr.Read
                Dim PQRHeaderBB As PQRHeaderBB = Me.CreateObject(dr)
                PQRHeaderBBList.Add(PQRHeaderBB)
            End While

            Return PQRHeaderBBList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim PQRHeaderBB As PQRHeaderBB = CType(obj, PQRHeaderBB)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, PQRHeaderBB.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim PQRHeaderBB As PQRHeaderBB = CType(obj, PQRHeaderBB)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@PQRNo", DbType.AnsiString, PQRHeaderBB.PQRNo)
            DbCommandWrapper.AddInParameter("@PQRType", DbType.Int32, PQRHeaderBB.PQRType)
            DbCommandWrapper.AddInParameter("@RefPQRNo", DbType.AnsiString, PQRHeaderBB.RefPQRNo)
            DbCommandWrapper.AddInParameter("@Year", DbType.Int32, PQRHeaderBB.Year)
            DbCommandWrapper.AddInParameter("@SeqNo", DbType.Int32, PQRHeaderBB.SeqNo)
            DbCommandWrapper.AddInParameter("@DocumentDate", DbType.DateTime, PQRHeaderBB.DocumentDate)
            DbCommandWrapper.AddInParameter("@SoldDate", DbType.DateTime, PQRHeaderBB.SoldDate)
            DbCommandWrapper.AddInParameter("@PQRDate", DbType.DateTime, PQRHeaderBB.PQRDate)
            DbCommandWrapper.AddInParameter("@OdoMeter", DbType.Int32, PQRHeaderBB.OdoMeter)
            DbCommandWrapper.AddInParameter("@Velocity", DbType.Int32, PQRHeaderBB.Velocity)
            DbCommandWrapper.AddInParameter("@CustomerName", DbType.AnsiString, PQRHeaderBB.CustomerName)
            DbCommandWrapper.AddInParameter("@CustomerAddress", DbType.AnsiString, PQRHeaderBB.CustomerAddress)
            DbCommandWrapper.AddInParameter("@ValidationTime", DbType.DateTime, PQRHeaderBB.ValidationTime)
            DbCommandWrapper.AddInParameter("@ConfirmBy", DbType.AnsiString, PQRHeaderBB.ConfirmBy)
            DbCommandWrapper.AddInParameter("@ConfirmTime", DbType.DateTime, PQRHeaderBB.ConfirmTime)
            DbCommandWrapper.AddInParameter("@RealeseTime", DbType.DateTime, PQRHeaderBB.RealeseTime)
            DbCommandWrapper.AddInParameter("@IntervalProcess", DbType.DateTime, PQRHeaderBB.IntervalProcess)
            DbCommandWrapper.AddInParameter("@Complexity", DbType.Int16, PQRHeaderBB.Complexity)
            DbCommandWrapper.AddInParameter("@Subject", DbType.AnsiString, PQRHeaderBB.Subject)
            DbCommandWrapper.AddInParameter("@Symptomps", DbType.AnsiString, PQRHeaderBB.Symptomps)
            DbCommandWrapper.AddInParameter("@Causes", DbType.AnsiString, PQRHeaderBB.Causes)
            DbCommandWrapper.AddInParameter("@Results", DbType.AnsiString, PQRHeaderBB.Results)
            DbCommandWrapper.AddInParameter("@Notes", DbType.AnsiString, PQRHeaderBB.Notes)
            DbCommandWrapper.AddInParameter("@Solutions", DbType.AnsiString, PQRHeaderBB.Solutions)
            DbCommandWrapper.AddInParameter("@Bobot", DbType.Int32, PQRHeaderBB.Bobot)
            DbCommandWrapper.AddInParameter("@ReleaseBy", DbType.AnsiString, PQRHeaderBB.ReleaseBy)
            DbCommandWrapper.AddInParameter("@FinishBy", DbType.AnsiString, PQRHeaderBB.FinishBy)
            DbCommandWrapper.AddInParameter("@FinishDate", DbType.DateTime, PQRHeaderBB.FinishDate)
            DBCommandWrapper.AddInParameter("@CodeA", DbType.AnsiString, PQRHeaderBB.CodeA)
            DBCommandWrapper.AddInParameter("@CodeB", DbType.AnsiString, PQRHeaderBB.CodeB)
            DBCommandWrapper.AddInParameter("@CodeC", DbType.AnsiString, PQRHeaderBB.CodeC)
            DbCommandWrapper.AddInParameter("@WorkOrderNumber", DbType.AnsiString, PQRHeaderBB.WorkOrderNumber)
            DBCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, PQRHeaderBB.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, PQRHeaderBB.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@ChassisMasterBBID", DbType.Int32, Me.GetRefObject(PQRHeaderBB.ChassisMasterBB))
            DbCommandWrapper.AddInParameter("@CategoryID", DbType.Int32, Me.GetRefObject(PQRHeaderBB.Category))
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(PQRHeaderBB.Dealer))
            DbCommandWrapper.AddInParameter("@DealerBranchID", DbType.Int32, Me.GetRefObject(PQRHeaderBB.DealerBranch))
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

            Dim PQRHeaderBB As PQRHeaderBB = CType(obj, PQRHeaderBB)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, PQRHeaderBB.ID)
            DbCommandWrapper.AddInParameter("@PQRNo", DbType.AnsiString, PQRHeaderBB.PQRNo)
            DbCommandWrapper.AddInParameter("@PQRType", DbType.Int32, PQRHeaderBB.PQRType)
            DbCommandWrapper.AddInParameter("@RefPQRNo", DbType.AnsiString, PQRHeaderBB.RefPQRNo)
            DbCommandWrapper.AddInParameter("@Year", DbType.Int32, PQRHeaderBB.Year)
            DbCommandWrapper.AddInParameter("@SeqNo", DbType.Int32, PQRHeaderBB.SeqNo)
            DbCommandWrapper.AddInParameter("@DocumentDate", DbType.DateTime, PQRHeaderBB.DocumentDate)
            DbCommandWrapper.AddInParameter("@SoldDate", DbType.DateTime, PQRHeaderBB.SoldDate)
            DbCommandWrapper.AddInParameter("@PQRDate", DbType.DateTime, PQRHeaderBB.PQRDate)
            DbCommandWrapper.AddInParameter("@OdoMeter", DbType.Int32, PQRHeaderBB.OdoMeter)
            DbCommandWrapper.AddInParameter("@Velocity", DbType.Int32, PQRHeaderBB.Velocity)
            DbCommandWrapper.AddInParameter("@CustomerName", DbType.AnsiString, PQRHeaderBB.CustomerName)
            DbCommandWrapper.AddInParameter("@CustomerAddress", DbType.AnsiString, PQRHeaderBB.CustomerAddress)
            DbCommandWrapper.AddInParameter("@ValidationTime", DbType.DateTime, PQRHeaderBB.ValidationTime)
            DbCommandWrapper.AddInParameter("@ConfirmBy", DbType.AnsiString, PQRHeaderBB.ConfirmBy)
            DbCommandWrapper.AddInParameter("@ConfirmTime", DbType.DateTime, PQRHeaderBB.ConfirmTime)
            DbCommandWrapper.AddInParameter("@RealeseTime", DbType.DateTime, PQRHeaderBB.RealeseTime)
            DbCommandWrapper.AddInParameter("@IntervalProcess", DbType.DateTime, PQRHeaderBB.IntervalProcess)
            DbCommandWrapper.AddInParameter("@Complexity", DbType.Int16, PQRHeaderBB.Complexity)
            DbCommandWrapper.AddInParameter("@Subject", DbType.AnsiString, PQRHeaderBB.Subject)
            DbCommandWrapper.AddInParameter("@Symptomps", DbType.AnsiString, PQRHeaderBB.Symptomps)
            DbCommandWrapper.AddInParameter("@Causes", DbType.AnsiString, PQRHeaderBB.Causes)
            DbCommandWrapper.AddInParameter("@Results", DbType.AnsiString, PQRHeaderBB.Results)
            DbCommandWrapper.AddInParameter("@Notes", DbType.AnsiString, PQRHeaderBB.Notes)
            DbCommandWrapper.AddInParameter("@Solutions", DbType.AnsiString, PQRHeaderBB.Solutions)
            DbCommandWrapper.AddInParameter("@Bobot", DbType.Int32, PQRHeaderBB.Bobot)
            DbCommandWrapper.AddInParameter("@ReleaseBy", DbType.AnsiString, PQRHeaderBB.ReleaseBy)
            DbCommandWrapper.AddInParameter("@FinishBy", DbType.AnsiString, PQRHeaderBB.FinishBy)
            DbCommandWrapper.AddInParameter("@FinishDate", DbType.DateTime, PQRHeaderBB.FinishDate)
            DBCommandWrapper.AddInParameter("@CodeA", DbType.AnsiString, PQRHeaderBB.CodeA)
            DBCommandWrapper.AddInParameter("@CodeB", DbType.AnsiString, PQRHeaderBB.CodeB)
            DBCommandWrapper.AddInParameter("@CodeC", DbType.AnsiString, PQRHeaderBB.CodeC)
            DbCommandWrapper.AddInParameter("@WorkOrderNumber", DbType.AnsiString, PQRHeaderBB.WorkOrderNumber)
            DBCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, PQRHeaderBB.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, PQRHeaderBB.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@ChassisMasterBBID", DbType.Int32, Me.GetRefObject(PQRHeaderBB.ChassisMasterBB))
            DbCommandWrapper.AddInParameter("@CategoryID", DbType.Int32, Me.GetRefObject(PQRHeaderBB.Category))
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(PQRHeaderBB.Dealer))
            DbCommandWrapper.AddInParameter("@DealerBranchID", DbType.Int32, Me.GetRefObject(PQRHeaderBB.DealerBranch))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As PQRHeaderBB

            Dim PQRHeaderBB As PQRHeaderBB = New PQRHeaderBB

            PQRHeaderBB.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("PQRNo")) Then PQRHeaderBB.PQRNo = dr("PQRNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RefPQRNo")) Then PQRHeaderBB.RefPQRNo = dr("RefPQRNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Year")) Then PQRHeaderBB.Year = CType(dr("Year"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("SeqNo")) Then PQRHeaderBB.SeqNo = CType(dr("SeqNo"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("DocumentDate")) Then PQRHeaderBB.DocumentDate = CType(dr("DocumentDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("SoldDate")) Then PQRHeaderBB.SoldDate = CType(dr("SoldDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("PQRDate")) Then PQRHeaderBB.PQRDate = CType(dr("PQRDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("OdoMeter")) Then PQRHeaderBB.OdoMeter = CType(dr("OdoMeter"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Velocity")) Then PQRHeaderBB.Velocity = CType(dr("Velocity"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("CustomerName")) Then PQRHeaderBB.CustomerName = dr("CustomerName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CustomerAddress")) Then PQRHeaderBB.CustomerAddress = dr("CustomerAddress").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ValidationTime")) Then PQRHeaderBB.ValidationTime = CType(dr("ValidationTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ConfirmBy")) Then PQRHeaderBB.ConfirmBy = dr("ConfirmBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ConfirmTime")) Then PQRHeaderBB.ConfirmTime = CType(dr("ConfirmTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("RealeseTime")) Then PQRHeaderBB.RealeseTime = CType(dr("RealeseTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("IntervalProcess")) Then PQRHeaderBB.IntervalProcess = CType(dr("IntervalProcess"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("Complexity")) Then PQRHeaderBB.Complexity = CType(dr("Complexity"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("Subject")) Then PQRHeaderBB.Subject = dr("Subject").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Symptomps")) Then PQRHeaderBB.Symptomps = dr("Symptomps").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Causes")) Then PQRHeaderBB.Causes = dr("Causes").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Results")) Then PQRHeaderBB.Results = dr("Results").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Notes")) Then PQRHeaderBB.Notes = dr("Notes").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Solutions")) Then PQRHeaderBB.Solutions = dr("Solutions").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Bobot")) Then PQRHeaderBB.Bobot = CType(dr("Bobot"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ReleaseBy")) Then PQRHeaderBB.ReleaseBy = dr("ReleaseBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("FinishBy")) Then PQRHeaderBB.FinishBy = dr("FinishBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("FinishDate")) Then PQRHeaderBB.FinishDate = CType(dr("FinishDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("CodeA")) Then PQRHeaderBB.CodeA = dr("CodeA").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CodeB")) Then PQRHeaderBB.CodeB = dr("CodeB").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CodeC")) Then PQRHeaderBB.CodeC = dr("CodeC").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("WorkOrderNumber")) Then PQRHeaderBB.WorkOrderNumber = dr("WorkOrderNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PQRType")) Then PQRHeaderBB.PQRType = dr("PQRType").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then PQRHeaderBB.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then PQRHeaderBB.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then PQRHeaderBB.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then PQRHeaderBB.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then PQRHeaderBB.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ChassisMasterBBID")) Then
                PQRHeaderBB.ChassisMasterBB = New ChassisMasterBB(CType(dr("ChassisMasterBBID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("CategoryID")) Then
                PQRHeaderBB.Category = New Category(CType(dr("CategoryID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                PQRHeaderBB.Dealer = New Dealer(CType(dr("DealerID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("DealerBranchID")) Then
                PQRHeaderBB.DealerBranch = New DealerBranch(CType(dr("DealerBranchID"), Integer))
            End If
            Return PQRHeaderBB

        End Function

        Private Sub SetTableName()

            If Not (GetType(PQRHeaderBB) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(PQRHeaderBB), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(PQRHeaderBB).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

