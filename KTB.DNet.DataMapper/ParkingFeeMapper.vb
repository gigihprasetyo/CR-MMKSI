
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : ParkingFee Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2012 
'// ---------------------
'// $History      : $
'// Generated on 2/27/2012 - 9:29:47 AM
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

    Public Class ParkingFeeMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertParkingFee"
        Private m_UpdateStatement As String = "up_UpdateParkingFee"
        Private m_RetrieveStatement As String = "up_RetrieveParkingFee"
        Private m_RetrieveListStatement As String = "up_RetrieveParkingFeeList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteParkingFee"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim parkingFee As ParkingFee = Nothing
            While dr.Read

                parkingFee = Me.CreateObject(dr)

            End While

            Return parkingFee

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim parkingFeeList As ArrayList = New ArrayList

            While dr.Read
                Dim parkingFee As ParkingFee = Me.CreateObject(dr)
                parkingFeeList.Add(parkingFee)
            End While

            Return parkingFeeList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim parkingFee As ParkingFee = CType(obj, ParkingFee)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, parkingFee.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim parkingFee As ParkingFee = CType(obj, ParkingFee)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@Periode", DbType.Int16, parkingFee.Periode)
            DBCommandWrapper.AddInParameter("@Year", DbType.Int16, parkingFee.Year)
            DBCommandWrapper.AddInParameter("@LetterNumber", DbType.AnsiString, parkingFee.LetterNumber)
            DbCommandWrapper.AddInParameter("@DebitChargeNumber", DbType.AnsiString, parkingFee.DebitChargeNumber)
            DbCommandWrapper.AddInParameter("@DebitMemoNumber", DbType.AnsiString, parkingFee.DebitMemoNumber)
            DbCommandWrapper.AddInParameter("@FileNameDebitMemo", DbType.AnsiString, parkingFee.FileNameDebitMemo)
            DbCommandWrapper.AddInParameter("@FileNameParkingFee", DbType.AnsiString, parkingFee.FileNameParkingFee)
            DBCommandWrapper.AddInParameter("@AssignmentNumber", DbType.AnsiString, parkingFee.AssignmentNumber)
            DBCommandWrapper.AddInParameter("@FakturPajakNo", DbType.AnsiString, parkingFee.FakturPajakNo)
            DbCommandWrapper.AddInParameter("@Amount", DbType.Currency, parkingFee.Amount)
            DbCommandWrapper.AddInParameter("@Description", DbType.String, parkingFee.Description)
            DbCommandWrapper.AddInParameter("@Status", DbType.Byte, parkingFee.Status)
            DBCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, parkingFee.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, parkingFee.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(parkingFee.Dealer))
            DBCommandWrapper.AddInParameter("@CategoryID", DbType.Int32, Me.GetRefObject(parkingFee.Category))
            DBCommandWrapper.AddInParameter("@DealerDepositA", DbType.Int16, Me.GetRefObject(parkingFee.DealerDepositA))

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

            Dim parkingFee As ParkingFee = CType(obj, ParkingFee)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, parkingFee.ID)
            DbCommandWrapper.AddInParameter("@Periode", DbType.Int16, parkingFee.Periode)
            DbCommandWrapper.AddInParameter("@Year", DbType.Int16, parkingFee.Year)
            DBCommandWrapper.AddInParameter("@LetterNumber", DbType.AnsiString, parkingFee.LetterNumber)
            DBCommandWrapper.AddInParameter("@DebitChargeNumber", DbType.AnsiString, parkingFee.DebitChargeNumber)
            DbCommandWrapper.AddInParameter("@DebitMemoNumber", DbType.AnsiString, parkingFee.DebitMemoNumber)
            DbCommandWrapper.AddInParameter("@FileNameDebitMemo", DbType.AnsiString, parkingFee.FileNameDebitMemo)
            DbCommandWrapper.AddInParameter("@FileNameParkingFee", DbType.AnsiString, parkingFee.FileNameParkingFee)
            DBCommandWrapper.AddInParameter("@AssignmentNumber", DbType.AnsiString, parkingFee.AssignmentNumber)
            DBCommandWrapper.AddInParameter("@FakturPajakNo", DbType.AnsiString, parkingFee.FakturPajakNo)
            DbCommandWrapper.AddInParameter("@Amount", DbType.Currency, parkingFee.Amount)
            DbCommandWrapper.AddInParameter("@Description", DbType.String, parkingFee.Description)
            DBCommandWrapper.AddInParameter("@Status", DbType.Int16, parkingFee.Status)
            DBCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, parkingFee.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, parkingFee.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(parkingFee.Dealer))
            DBCommandWrapper.AddInParameter("@CategoryID", DbType.Int32, Me.GetRefObject(parkingFee.Category))
            DBCommandWrapper.AddInParameter("@DealerDepositA", DbType.Int16, Me.GetRefObject(parkingFee.DealerDepositA))

            Return DbCommandWrapper

        End Function

#End Region

#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As ParkingFee

            Dim parkingFee As parkingFee = New parkingFee

            parkingFee.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Periode")) Then parkingFee.Periode = CType(dr("Periode"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("Year")) Then parkingFee.Year = CType(dr("Year"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("LetterNumber")) Then parkingFee.LetterNumber = dr("LetterNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DebitChargeNumber")) Then parkingFee.DebitChargeNumber = dr("DebitChargeNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DebitMemoNumber")) Then parkingFee.DebitMemoNumber = dr("DebitMemoNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("FileNameDebitMemo")) Then parkingFee.FileNameDebitMemo = dr("FileNameDebitMemo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("FileNameParkingFee")) Then parkingFee.FileNameParkingFee = dr("FileNameParkingFee").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("AssignmentNumber")) Then parkingFee.AssignmentNumber = dr("AssignmentNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("FakturPajakNo")) Then parkingFee.FakturPajakNo = dr("FakturPajakNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Amount")) Then parkingFee.Amount = CType(dr("Amount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("Description")) Then parkingFee.Description = dr("Description").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then parkingFee.Status = CType(dr("Status"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then parkingFee.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then parkingFee.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then parkingFee.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then parkingFee.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then parkingFee.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                parkingFee.Dealer = New Dealer(CType(dr("DealerID"), Short))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("CategoryID")) Then
                parkingFee.Category = New Category(CType(dr("CategoryID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("DealerDepositA")) Then
                parkingFee.DealerDepositA = New Dealer(CType(dr("DealerDepositA"), Short))
            End If
            Return parkingFee

        End Function

        Private Sub SetTableName()

            If Not (GetType(ParkingFee) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(ParkingFee), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(ParkingFee).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

