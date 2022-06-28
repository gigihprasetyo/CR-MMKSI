
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : v_Redemption Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2011 
'// ---------------------
'// $History      : $
'// Generated on 11/22/2011 - 9:59:42 AM
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

    Public Class v_RedemptionMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_Insertv_Redemption"
        Private m_UpdateStatement As String = "up_Updatev_Redemption"
        Private m_RetrieveStatement As String = "up_Retrievev_Redemption"
        Private m_RetrieveListStatement As String = "up_Retrievev_RedemptionList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_Deletev_Redemption"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim v_Redemption As v_Redemption = Nothing
            While dr.Read

                v_Redemption = Me.CreateObject(dr)

            End While

            Return v_Redemption

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim v_RedemptionList As ArrayList = New ArrayList

            While dr.Read
                Dim v_Redemption As v_Redemption = Me.CreateObject(dr)
                v_RedemptionList.Add(v_Redemption)
            End While

            Return v_RedemptionList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim v_Redemption As v_Redemption = CType(obj, v_Redemption)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, v_Redemption.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim v_Redemption As v_Redemption = CType(obj, v_Redemption)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@VechileColorID", DbType.Int16, v_Redemption.VechileColorID)
            DbCommandWrapper.AddInParameter("@VehicleCode", DbType.AnsiString, v_Redemption.VehicleCode)
            DbCommandWrapper.AddInParameter("@VehicleDesc", DbType.AnsiString, v_Redemption.VehicleDesc)
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, v_Redemption.DealerID)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, v_Redemption.DealerCode)
            DbCommandWrapper.AddInParameter("@PeriodYear", DbType.Int32, v_Redemption.PeriodYear)
            DbCommandWrapper.AddInParameter("@PeriodMonth", DbType.Int32, v_Redemption.PeriodMonth)
            DBCommandWrapper.AddInParameter("@TotalOC", DbType.Int32, v_Redemption.TotalOC)
            DBCommandWrapper.AddInParameter("@TotalRequest", DbType.Int32, v_Redemption.TotalRequest)
            DBCommandWrapper.AddInParameter("@TotalRespond", DbType.Int32, v_Redemption.TotalRespond)
            DbCommandWrapper.AddInParameter("@RH1", DbType.Int32, v_Redemption.RH1)
            DbCommandWrapper.AddInParameter("@E1", DbType.Int32, v_Redemption.E1)
            DbCommandWrapper.AddInParameter("@R1", DbType.Int32, v_Redemption.R1)
            DbCommandWrapper.AddInParameter("@A1", DbType.Int32, v_Redemption.A1)
            DbCommandWrapper.AddInParameter("@RH2", DbType.Int32, v_Redemption.RH2)
            DbCommandWrapper.AddInParameter("@E2", DbType.Int32, v_Redemption.E2)
            DbCommandWrapper.AddInParameter("@R2", DbType.Int32, v_Redemption.R2)
            DbCommandWrapper.AddInParameter("@A2", DbType.Int32, v_Redemption.A2)
            DbCommandWrapper.AddInParameter("@RH3", DbType.Int32, v_Redemption.RH3)
            DbCommandWrapper.AddInParameter("@E3", DbType.Int32, v_Redemption.E3)
            DbCommandWrapper.AddInParameter("@R3", DbType.Int32, v_Redemption.R3)
            DbCommandWrapper.AddInParameter("@A3", DbType.Int32, v_Redemption.A3)
            DbCommandWrapper.AddInParameter("@RH4", DbType.Int32, v_Redemption.RH4)
            DbCommandWrapper.AddInParameter("@E4", DbType.Int32, v_Redemption.E4)
            DbCommandWrapper.AddInParameter("@R4", DbType.Int32, v_Redemption.R4)
            DbCommandWrapper.AddInParameter("@A4", DbType.Int32, v_Redemption.A4)
            DbCommandWrapper.AddInParameter("@RH5", DbType.Int32, v_Redemption.RH5)
            DbCommandWrapper.AddInParameter("@E5", DbType.Int32, v_Redemption.E5)
            DbCommandWrapper.AddInParameter("@R5", DbType.Int32, v_Redemption.R5)
            DbCommandWrapper.AddInParameter("@A5", DbType.Int32, v_Redemption.A5)
            DbCommandWrapper.AddInParameter("@RH6", DbType.Int32, v_Redemption.RH6)
            DbCommandWrapper.AddInParameter("@E6", DbType.Int32, v_Redemption.E6)
            DbCommandWrapper.AddInParameter("@R6", DbType.Int32, v_Redemption.R6)
            DbCommandWrapper.AddInParameter("@A6", DbType.Int32, v_Redemption.A6)
            DbCommandWrapper.AddInParameter("@RH7", DbType.Int32, v_Redemption.RH7)
            DbCommandWrapper.AddInParameter("@E7", DbType.Int32, v_Redemption.E7)
            DbCommandWrapper.AddInParameter("@R7", DbType.Int32, v_Redemption.R7)
            DbCommandWrapper.AddInParameter("@A7", DbType.Int32, v_Redemption.A7)
            DbCommandWrapper.AddInParameter("@RH8", DbType.Int32, v_Redemption.RH8)
            DbCommandWrapper.AddInParameter("@E8", DbType.Int32, v_Redemption.E8)
            DbCommandWrapper.AddInParameter("@R8", DbType.Int32, v_Redemption.R8)
            DbCommandWrapper.AddInParameter("@A8", DbType.Int32, v_Redemption.A8)
            DbCommandWrapper.AddInParameter("@RH9", DbType.Int32, v_Redemption.RH9)
            DbCommandWrapper.AddInParameter("@E9", DbType.Int32, v_Redemption.E9)
            DbCommandWrapper.AddInParameter("@R9", DbType.Int32, v_Redemption.R9)
            DbCommandWrapper.AddInParameter("@A9", DbType.Int32, v_Redemption.A9)
            DbCommandWrapper.AddInParameter("@RH10", DbType.Int32, v_Redemption.RH10)
            DbCommandWrapper.AddInParameter("@E10", DbType.Int32, v_Redemption.E10)
            DbCommandWrapper.AddInParameter("@R10", DbType.Int32, v_Redemption.R10)
            DbCommandWrapper.AddInParameter("@A10", DbType.Int32, v_Redemption.A10)
            DbCommandWrapper.AddInParameter("@RH11", DbType.Int32, v_Redemption.RH11)
            DbCommandWrapper.AddInParameter("@E11", DbType.Int32, v_Redemption.E11)
            DbCommandWrapper.AddInParameter("@R11", DbType.Int32, v_Redemption.R11)
            DbCommandWrapper.AddInParameter("@A11", DbType.Int32, v_Redemption.A11)
            DbCommandWrapper.AddInParameter("@RH12", DbType.Int32, v_Redemption.RH12)
            DbCommandWrapper.AddInParameter("@E12", DbType.Int32, v_Redemption.E12)
            DbCommandWrapper.AddInParameter("@R12", DbType.Int32, v_Redemption.R12)
            DbCommandWrapper.AddInParameter("@A12", DbType.Int32, v_Redemption.A12)
            DbCommandWrapper.AddInParameter("@RH13", DbType.Int32, v_Redemption.RH13)
            DbCommandWrapper.AddInParameter("@E13", DbType.Int32, v_Redemption.E13)
            DbCommandWrapper.AddInParameter("@R13", DbType.Int32, v_Redemption.R13)
            DbCommandWrapper.AddInParameter("@A13", DbType.Int32, v_Redemption.A13)
            DbCommandWrapper.AddInParameter("@RH14", DbType.Int32, v_Redemption.RH14)
            DbCommandWrapper.AddInParameter("@E14", DbType.Int32, v_Redemption.E14)
            DbCommandWrapper.AddInParameter("@R14", DbType.Int32, v_Redemption.R14)
            DbCommandWrapper.AddInParameter("@A14", DbType.Int32, v_Redemption.A14)
            DbCommandWrapper.AddInParameter("@RH15", DbType.Int32, v_Redemption.RH15)
            DbCommandWrapper.AddInParameter("@E15", DbType.Int32, v_Redemption.E15)
            DbCommandWrapper.AddInParameter("@R15", DbType.Int32, v_Redemption.R15)
            DbCommandWrapper.AddInParameter("@A15", DbType.Int32, v_Redemption.A15)
            DbCommandWrapper.AddInParameter("@RH16", DbType.Int32, v_Redemption.RH16)
            DbCommandWrapper.AddInParameter("@E16", DbType.Int32, v_Redemption.E16)
            DbCommandWrapper.AddInParameter("@R16", DbType.Int32, v_Redemption.R16)
            DbCommandWrapper.AddInParameter("@A16", DbType.Int32, v_Redemption.A16)
            DbCommandWrapper.AddInParameter("@RH17", DbType.Int32, v_Redemption.RH17)
            DbCommandWrapper.AddInParameter("@E17", DbType.Int32, v_Redemption.E17)
            DbCommandWrapper.AddInParameter("@R17", DbType.Int32, v_Redemption.R17)
            DbCommandWrapper.AddInParameter("@A17", DbType.Int32, v_Redemption.A17)
            DbCommandWrapper.AddInParameter("@RH18", DbType.Int32, v_Redemption.RH18)
            DbCommandWrapper.AddInParameter("@E18", DbType.Int32, v_Redemption.E18)
            DbCommandWrapper.AddInParameter("@R18", DbType.Int32, v_Redemption.R18)
            DbCommandWrapper.AddInParameter("@A18", DbType.Int32, v_Redemption.A18)
            DbCommandWrapper.AddInParameter("@RH19", DbType.Int32, v_Redemption.RH19)
            DbCommandWrapper.AddInParameter("@E19", DbType.Int32, v_Redemption.E19)
            DbCommandWrapper.AddInParameter("@R19", DbType.Int32, v_Redemption.R19)
            DbCommandWrapper.AddInParameter("@A19", DbType.Int32, v_Redemption.A19)
            DbCommandWrapper.AddInParameter("@RH20", DbType.Int32, v_Redemption.RH20)
            DbCommandWrapper.AddInParameter("@E20", DbType.Int32, v_Redemption.E20)
            DbCommandWrapper.AddInParameter("@R20", DbType.Int32, v_Redemption.R20)
            DbCommandWrapper.AddInParameter("@A20", DbType.Int32, v_Redemption.A20)
            DbCommandWrapper.AddInParameter("@RH21", DbType.Int32, v_Redemption.RH21)
            DbCommandWrapper.AddInParameter("@E21", DbType.Int32, v_Redemption.E21)
            DbCommandWrapper.AddInParameter("@R21", DbType.Int32, v_Redemption.R21)
            DbCommandWrapper.AddInParameter("@A21", DbType.Int32, v_Redemption.A21)
            DbCommandWrapper.AddInParameter("@RH22", DbType.Int32, v_Redemption.RH22)
            DbCommandWrapper.AddInParameter("@E22", DbType.Int32, v_Redemption.E22)
            DbCommandWrapper.AddInParameter("@R22", DbType.Int32, v_Redemption.R22)
            DbCommandWrapper.AddInParameter("@A22", DbType.Int32, v_Redemption.A22)
            DbCommandWrapper.AddInParameter("@RH23", DbType.Int32, v_Redemption.RH23)
            DbCommandWrapper.AddInParameter("@E23", DbType.Int32, v_Redemption.E23)
            DbCommandWrapper.AddInParameter("@R23", DbType.Int32, v_Redemption.R23)
            DbCommandWrapper.AddInParameter("@A23", DbType.Int32, v_Redemption.A23)
            DbCommandWrapper.AddInParameter("@RH24", DbType.Int32, v_Redemption.RH24)
            DbCommandWrapper.AddInParameter("@E24", DbType.Int32, v_Redemption.E24)
            DbCommandWrapper.AddInParameter("@R24", DbType.Int32, v_Redemption.R24)
            DbCommandWrapper.AddInParameter("@A24", DbType.Int32, v_Redemption.A24)
            DbCommandWrapper.AddInParameter("@RH25", DbType.Int32, v_Redemption.RH25)
            DbCommandWrapper.AddInParameter("@E25", DbType.Int32, v_Redemption.E25)
            DbCommandWrapper.AddInParameter("@R25", DbType.Int32, v_Redemption.R25)
            DbCommandWrapper.AddInParameter("@A25", DbType.Int32, v_Redemption.A25)
            DbCommandWrapper.AddInParameter("@RH26", DbType.Int32, v_Redemption.RH26)
            DbCommandWrapper.AddInParameter("@E26", DbType.Int32, v_Redemption.E26)
            DbCommandWrapper.AddInParameter("@R26", DbType.Int32, v_Redemption.R26)
            DbCommandWrapper.AddInParameter("@A26", DbType.Int32, v_Redemption.A26)
            DbCommandWrapper.AddInParameter("@RH27", DbType.Int32, v_Redemption.RH27)
            DbCommandWrapper.AddInParameter("@E27", DbType.Int32, v_Redemption.E27)
            DbCommandWrapper.AddInParameter("@R27", DbType.Int32, v_Redemption.R27)
            DbCommandWrapper.AddInParameter("@A27", DbType.Int32, v_Redemption.A27)
            DbCommandWrapper.AddInParameter("@RH28", DbType.Int32, v_Redemption.RH28)
            DbCommandWrapper.AddInParameter("@E28", DbType.Int32, v_Redemption.E28)
            DbCommandWrapper.AddInParameter("@R28", DbType.Int32, v_Redemption.R28)
            DbCommandWrapper.AddInParameter("@A28", DbType.Int32, v_Redemption.A28)
            DbCommandWrapper.AddInParameter("@RH29", DbType.Int32, v_Redemption.RH29)
            DbCommandWrapper.AddInParameter("@E29", DbType.Int32, v_Redemption.E29)
            DbCommandWrapper.AddInParameter("@R29", DbType.Int32, v_Redemption.R29)
            DbCommandWrapper.AddInParameter("@A29", DbType.Int32, v_Redemption.A29)
            DbCommandWrapper.AddInParameter("@RH30", DbType.Int32, v_Redemption.RH30)
            DbCommandWrapper.AddInParameter("@E30", DbType.Int32, v_Redemption.E30)
            DbCommandWrapper.AddInParameter("@R30", DbType.Int32, v_Redemption.R30)
            DbCommandWrapper.AddInParameter("@A30", DbType.Int32, v_Redemption.A30)
            DbCommandWrapper.AddInParameter("@RH31", DbType.Int32, v_Redemption.RH31)
            DbCommandWrapper.AddInParameter("@E31", DbType.Int32, v_Redemption.E31)
            DbCommandWrapper.AddInParameter("@R31", DbType.Int32, v_Redemption.R31)
            DbCommandWrapper.AddInParameter("@A31", DbType.Int32, v_Redemption.A31)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int32, v_Redemption.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, v_Redemption.LastUpdateBy)
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

            Dim v_Redemption As v_Redemption = CType(obj, v_Redemption)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, v_Redemption.ID)
            DbCommandWrapper.AddInParameter("@VechileColorID", DbType.Int16, v_Redemption.VechileColorID)
            DbCommandWrapper.AddInParameter("@VehicleCode", DbType.AnsiString, v_Redemption.VehicleCode)
            DbCommandWrapper.AddInParameter("@VehicleDesc", DbType.AnsiString, v_Redemption.VehicleDesc)
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, v_Redemption.DealerID)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, v_Redemption.DealerCode)
            DbCommandWrapper.AddInParameter("@PeriodYear", DbType.Int32, v_Redemption.PeriodYear)
            DbCommandWrapper.AddInParameter("@PeriodMonth", DbType.Int32, v_Redemption.PeriodMonth)
            DBCommandWrapper.AddInParameter("@TotalOC", DbType.Int32, v_Redemption.TotalOC)
            DBCommandWrapper.AddInParameter("@TotalRequest", DbType.Int32, v_Redemption.TotalRequest)
            DBCommandWrapper.AddInParameter("@TotalRespond", DbType.Int32, v_Redemption.TotalRespond)
            DbCommandWrapper.AddInParameter("@RH1", DbType.Int32, v_Redemption.RH1)
            DbCommandWrapper.AddInParameter("@E1", DbType.Int32, v_Redemption.E1)
            DbCommandWrapper.AddInParameter("@R1", DbType.Int32, v_Redemption.R1)
            DbCommandWrapper.AddInParameter("@A1", DbType.Int32, v_Redemption.A1)
            DbCommandWrapper.AddInParameter("@RH2", DbType.Int32, v_Redemption.RH2)
            DbCommandWrapper.AddInParameter("@E2", DbType.Int32, v_Redemption.E2)
            DbCommandWrapper.AddInParameter("@R2", DbType.Int32, v_Redemption.R2)
            DbCommandWrapper.AddInParameter("@A2", DbType.Int32, v_Redemption.A2)
            DbCommandWrapper.AddInParameter("@RH3", DbType.Int32, v_Redemption.RH3)
            DbCommandWrapper.AddInParameter("@E3", DbType.Int32, v_Redemption.E3)
            DbCommandWrapper.AddInParameter("@R3", DbType.Int32, v_Redemption.R3)
            DbCommandWrapper.AddInParameter("@A3", DbType.Int32, v_Redemption.A3)
            DbCommandWrapper.AddInParameter("@RH4", DbType.Int32, v_Redemption.RH4)
            DbCommandWrapper.AddInParameter("@E4", DbType.Int32, v_Redemption.E4)
            DbCommandWrapper.AddInParameter("@R4", DbType.Int32, v_Redemption.R4)
            DbCommandWrapper.AddInParameter("@A4", DbType.Int32, v_Redemption.A4)
            DbCommandWrapper.AddInParameter("@RH5", DbType.Int32, v_Redemption.RH5)
            DbCommandWrapper.AddInParameter("@E5", DbType.Int32, v_Redemption.E5)
            DbCommandWrapper.AddInParameter("@R5", DbType.Int32, v_Redemption.R5)
            DbCommandWrapper.AddInParameter("@A5", DbType.Int32, v_Redemption.A5)
            DbCommandWrapper.AddInParameter("@RH6", DbType.Int32, v_Redemption.RH6)
            DbCommandWrapper.AddInParameter("@E6", DbType.Int32, v_Redemption.E6)
            DbCommandWrapper.AddInParameter("@R6", DbType.Int32, v_Redemption.R6)
            DbCommandWrapper.AddInParameter("@A6", DbType.Int32, v_Redemption.A6)
            DbCommandWrapper.AddInParameter("@RH7", DbType.Int32, v_Redemption.RH7)
            DbCommandWrapper.AddInParameter("@E7", DbType.Int32, v_Redemption.E7)
            DbCommandWrapper.AddInParameter("@R7", DbType.Int32, v_Redemption.R7)
            DbCommandWrapper.AddInParameter("@A7", DbType.Int32, v_Redemption.A7)
            DbCommandWrapper.AddInParameter("@RH8", DbType.Int32, v_Redemption.RH8)
            DbCommandWrapper.AddInParameter("@E8", DbType.Int32, v_Redemption.E8)
            DbCommandWrapper.AddInParameter("@R8", DbType.Int32, v_Redemption.R8)
            DbCommandWrapper.AddInParameter("@A8", DbType.Int32, v_Redemption.A8)
            DbCommandWrapper.AddInParameter("@RH9", DbType.Int32, v_Redemption.RH9)
            DbCommandWrapper.AddInParameter("@E9", DbType.Int32, v_Redemption.E9)
            DbCommandWrapper.AddInParameter("@R9", DbType.Int32, v_Redemption.R9)
            DbCommandWrapper.AddInParameter("@A9", DbType.Int32, v_Redemption.A9)
            DbCommandWrapper.AddInParameter("@RH10", DbType.Int32, v_Redemption.RH10)
            DbCommandWrapper.AddInParameter("@E10", DbType.Int32, v_Redemption.E10)
            DbCommandWrapper.AddInParameter("@R10", DbType.Int32, v_Redemption.R10)
            DbCommandWrapper.AddInParameter("@A10", DbType.Int32, v_Redemption.A10)
            DbCommandWrapper.AddInParameter("@RH11", DbType.Int32, v_Redemption.RH11)
            DbCommandWrapper.AddInParameter("@E11", DbType.Int32, v_Redemption.E11)
            DbCommandWrapper.AddInParameter("@R11", DbType.Int32, v_Redemption.R11)
            DbCommandWrapper.AddInParameter("@A11", DbType.Int32, v_Redemption.A11)
            DbCommandWrapper.AddInParameter("@RH12", DbType.Int32, v_Redemption.RH12)
            DbCommandWrapper.AddInParameter("@E12", DbType.Int32, v_Redemption.E12)
            DbCommandWrapper.AddInParameter("@R12", DbType.Int32, v_Redemption.R12)
            DbCommandWrapper.AddInParameter("@A12", DbType.Int32, v_Redemption.A12)
            DbCommandWrapper.AddInParameter("@RH13", DbType.Int32, v_Redemption.RH13)
            DbCommandWrapper.AddInParameter("@E13", DbType.Int32, v_Redemption.E13)
            DbCommandWrapper.AddInParameter("@R13", DbType.Int32, v_Redemption.R13)
            DbCommandWrapper.AddInParameter("@A13", DbType.Int32, v_Redemption.A13)
            DbCommandWrapper.AddInParameter("@RH14", DbType.Int32, v_Redemption.RH14)
            DbCommandWrapper.AddInParameter("@E14", DbType.Int32, v_Redemption.E14)
            DbCommandWrapper.AddInParameter("@R14", DbType.Int32, v_Redemption.R14)
            DbCommandWrapper.AddInParameter("@A14", DbType.Int32, v_Redemption.A14)
            DbCommandWrapper.AddInParameter("@RH15", DbType.Int32, v_Redemption.RH15)
            DbCommandWrapper.AddInParameter("@E15", DbType.Int32, v_Redemption.E15)
            DbCommandWrapper.AddInParameter("@R15", DbType.Int32, v_Redemption.R15)
            DbCommandWrapper.AddInParameter("@A15", DbType.Int32, v_Redemption.A15)
            DbCommandWrapper.AddInParameter("@RH16", DbType.Int32, v_Redemption.RH16)
            DbCommandWrapper.AddInParameter("@E16", DbType.Int32, v_Redemption.E16)
            DbCommandWrapper.AddInParameter("@R16", DbType.Int32, v_Redemption.R16)
            DbCommandWrapper.AddInParameter("@A16", DbType.Int32, v_Redemption.A16)
            DbCommandWrapper.AddInParameter("@RH17", DbType.Int32, v_Redemption.RH17)
            DbCommandWrapper.AddInParameter("@E17", DbType.Int32, v_Redemption.E17)
            DbCommandWrapper.AddInParameter("@R17", DbType.Int32, v_Redemption.R17)
            DbCommandWrapper.AddInParameter("@A17", DbType.Int32, v_Redemption.A17)
            DbCommandWrapper.AddInParameter("@RH18", DbType.Int32, v_Redemption.RH18)
            DbCommandWrapper.AddInParameter("@E18", DbType.Int32, v_Redemption.E18)
            DbCommandWrapper.AddInParameter("@R18", DbType.Int32, v_Redemption.R18)
            DbCommandWrapper.AddInParameter("@A18", DbType.Int32, v_Redemption.A18)
            DbCommandWrapper.AddInParameter("@RH19", DbType.Int32, v_Redemption.RH19)
            DbCommandWrapper.AddInParameter("@E19", DbType.Int32, v_Redemption.E19)
            DbCommandWrapper.AddInParameter("@R19", DbType.Int32, v_Redemption.R19)
            DbCommandWrapper.AddInParameter("@A19", DbType.Int32, v_Redemption.A19)
            DbCommandWrapper.AddInParameter("@RH20", DbType.Int32, v_Redemption.RH20)
            DbCommandWrapper.AddInParameter("@E20", DbType.Int32, v_Redemption.E20)
            DbCommandWrapper.AddInParameter("@R20", DbType.Int32, v_Redemption.R20)
            DbCommandWrapper.AddInParameter("@A20", DbType.Int32, v_Redemption.A20)
            DbCommandWrapper.AddInParameter("@RH21", DbType.Int32, v_Redemption.RH21)
            DbCommandWrapper.AddInParameter("@E21", DbType.Int32, v_Redemption.E21)
            DbCommandWrapper.AddInParameter("@R21", DbType.Int32, v_Redemption.R21)
            DbCommandWrapper.AddInParameter("@A21", DbType.Int32, v_Redemption.A21)
            DbCommandWrapper.AddInParameter("@RH22", DbType.Int32, v_Redemption.RH22)
            DbCommandWrapper.AddInParameter("@E22", DbType.Int32, v_Redemption.E22)
            DbCommandWrapper.AddInParameter("@R22", DbType.Int32, v_Redemption.R22)
            DbCommandWrapper.AddInParameter("@A22", DbType.Int32, v_Redemption.A22)
            DbCommandWrapper.AddInParameter("@RH23", DbType.Int32, v_Redemption.RH23)
            DbCommandWrapper.AddInParameter("@E23", DbType.Int32, v_Redemption.E23)
            DbCommandWrapper.AddInParameter("@R23", DbType.Int32, v_Redemption.R23)
            DbCommandWrapper.AddInParameter("@A23", DbType.Int32, v_Redemption.A23)
            DbCommandWrapper.AddInParameter("@RH24", DbType.Int32, v_Redemption.RH24)
            DbCommandWrapper.AddInParameter("@E24", DbType.Int32, v_Redemption.E24)
            DbCommandWrapper.AddInParameter("@R24", DbType.Int32, v_Redemption.R24)
            DbCommandWrapper.AddInParameter("@A24", DbType.Int32, v_Redemption.A24)
            DbCommandWrapper.AddInParameter("@RH25", DbType.Int32, v_Redemption.RH25)
            DbCommandWrapper.AddInParameter("@E25", DbType.Int32, v_Redemption.E25)
            DbCommandWrapper.AddInParameter("@R25", DbType.Int32, v_Redemption.R25)
            DbCommandWrapper.AddInParameter("@A25", DbType.Int32, v_Redemption.A25)
            DbCommandWrapper.AddInParameter("@RH26", DbType.Int32, v_Redemption.RH26)
            DbCommandWrapper.AddInParameter("@E26", DbType.Int32, v_Redemption.E26)
            DbCommandWrapper.AddInParameter("@R26", DbType.Int32, v_Redemption.R26)
            DbCommandWrapper.AddInParameter("@A26", DbType.Int32, v_Redemption.A26)
            DbCommandWrapper.AddInParameter("@RH27", DbType.Int32, v_Redemption.RH27)
            DbCommandWrapper.AddInParameter("@E27", DbType.Int32, v_Redemption.E27)
            DbCommandWrapper.AddInParameter("@R27", DbType.Int32, v_Redemption.R27)
            DbCommandWrapper.AddInParameter("@A27", DbType.Int32, v_Redemption.A27)
            DbCommandWrapper.AddInParameter("@RH28", DbType.Int32, v_Redemption.RH28)
            DbCommandWrapper.AddInParameter("@E28", DbType.Int32, v_Redemption.E28)
            DbCommandWrapper.AddInParameter("@R28", DbType.Int32, v_Redemption.R28)
            DbCommandWrapper.AddInParameter("@A28", DbType.Int32, v_Redemption.A28)
            DbCommandWrapper.AddInParameter("@RH29", DbType.Int32, v_Redemption.RH29)
            DbCommandWrapper.AddInParameter("@E29", DbType.Int32, v_Redemption.E29)
            DbCommandWrapper.AddInParameter("@R29", DbType.Int32, v_Redemption.R29)
            DbCommandWrapper.AddInParameter("@A29", DbType.Int32, v_Redemption.A29)
            DbCommandWrapper.AddInParameter("@RH30", DbType.Int32, v_Redemption.RH30)
            DbCommandWrapper.AddInParameter("@E30", DbType.Int32, v_Redemption.E30)
            DbCommandWrapper.AddInParameter("@R30", DbType.Int32, v_Redemption.R30)
            DbCommandWrapper.AddInParameter("@A30", DbType.Int32, v_Redemption.A30)
            DbCommandWrapper.AddInParameter("@RH31", DbType.Int32, v_Redemption.RH31)
            DbCommandWrapper.AddInParameter("@E31", DbType.Int32, v_Redemption.E31)
            DbCommandWrapper.AddInParameter("@R31", DbType.Int32, v_Redemption.R31)
            DbCommandWrapper.AddInParameter("@A31", DbType.Int32, v_Redemption.A31)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int32, v_Redemption.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, v_Redemption.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As v_Redemption

            Dim v_Redemption As v_Redemption = New v_Redemption

            v_Redemption.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("VechileColorID")) Then v_Redemption.VechileColorID = CType(dr("VechileColorID"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("VehicleCode")) Then v_Redemption.VehicleCode = dr("VehicleCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("VehicleDesc")) Then v_Redemption.VehicleDesc = dr("VehicleDesc").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then v_Redemption.DealerID = CType(dr("DealerID"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerCode")) Then v_Redemption.DealerCode = dr("DealerCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PeriodYear")) Then v_Redemption.PeriodYear = CType(dr("PeriodYear"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("PeriodMonth")) Then v_Redemption.PeriodMonth = CType(dr("PeriodMonth"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("TotalOC")) Then v_Redemption.TotalOC = CType(dr("TotalOC"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("TotalRequest")) Then v_Redemption.TotalRequest = CType(dr("TotalRequest"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("TotalRespond")) Then v_Redemption.TotalRespond = CType(dr("TotalRespond"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RH1")) Then v_Redemption.RH1 = CType(dr("RH1"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("E1")) Then v_Redemption.E1 = CType(dr("E1"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("R1")) Then v_Redemption.R1 = CType(dr("R1"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("A1")) Then v_Redemption.A1 = CType(dr("A1"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RH2")) Then v_Redemption.RH2 = CType(dr("RH2"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("E2")) Then v_Redemption.E2 = CType(dr("E2"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("R2")) Then v_Redemption.R2 = CType(dr("R2"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("A2")) Then v_Redemption.A2 = CType(dr("A2"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RH3")) Then v_Redemption.RH3 = CType(dr("RH3"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("E3")) Then v_Redemption.E3 = CType(dr("E3"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("R3")) Then v_Redemption.R3 = CType(dr("R3"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("A3")) Then v_Redemption.A3 = CType(dr("A3"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RH4")) Then v_Redemption.RH4 = CType(dr("RH4"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("E4")) Then v_Redemption.E4 = CType(dr("E4"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("R4")) Then v_Redemption.R4 = CType(dr("R4"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("A4")) Then v_Redemption.A4 = CType(dr("A4"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RH5")) Then v_Redemption.RH5 = CType(dr("RH5"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("E5")) Then v_Redemption.E5 = CType(dr("E5"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("R5")) Then v_Redemption.R5 = CType(dr("R5"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("A5")) Then v_Redemption.A5 = CType(dr("A5"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RH6")) Then v_Redemption.RH6 = CType(dr("RH6"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("E6")) Then v_Redemption.E6 = CType(dr("E6"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("R6")) Then v_Redemption.R6 = CType(dr("R6"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("A6")) Then v_Redemption.A6 = CType(dr("A6"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RH7")) Then v_Redemption.RH7 = CType(dr("RH7"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("E7")) Then v_Redemption.E7 = CType(dr("E7"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("R7")) Then v_Redemption.R7 = CType(dr("R7"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("A7")) Then v_Redemption.A7 = CType(dr("A7"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RH8")) Then v_Redemption.RH8 = CType(dr("RH8"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("E8")) Then v_Redemption.E8 = CType(dr("E8"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("R8")) Then v_Redemption.R8 = CType(dr("R8"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("A8")) Then v_Redemption.A8 = CType(dr("A8"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RH9")) Then v_Redemption.RH9 = CType(dr("RH9"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("E9")) Then v_Redemption.E9 = CType(dr("E9"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("R9")) Then v_Redemption.R9 = CType(dr("R9"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("A9")) Then v_Redemption.A9 = CType(dr("A9"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RH10")) Then v_Redemption.RH10 = CType(dr("RH10"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("E10")) Then v_Redemption.E10 = CType(dr("E10"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("R10")) Then v_Redemption.R10 = CType(dr("R10"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("A10")) Then v_Redemption.A10 = CType(dr("A10"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RH11")) Then v_Redemption.RH11 = CType(dr("RH11"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("E11")) Then v_Redemption.E11 = CType(dr("E11"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("R11")) Then v_Redemption.R11 = CType(dr("R11"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("A11")) Then v_Redemption.A11 = CType(dr("A11"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RH12")) Then v_Redemption.RH12 = CType(dr("RH12"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("E12")) Then v_Redemption.E12 = CType(dr("E12"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("R12")) Then v_Redemption.R12 = CType(dr("R12"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("A12")) Then v_Redemption.A12 = CType(dr("A12"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RH13")) Then v_Redemption.RH13 = CType(dr("RH13"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("E13")) Then v_Redemption.E13 = CType(dr("E13"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("R13")) Then v_Redemption.R13 = CType(dr("R13"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("A13")) Then v_Redemption.A13 = CType(dr("A13"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RH14")) Then v_Redemption.RH14 = CType(dr("RH14"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("E14")) Then v_Redemption.E14 = CType(dr("E14"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("R14")) Then v_Redemption.R14 = CType(dr("R14"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("A14")) Then v_Redemption.A14 = CType(dr("A14"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RH15")) Then v_Redemption.RH15 = CType(dr("RH15"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("E15")) Then v_Redemption.E15 = CType(dr("E15"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("R15")) Then v_Redemption.R15 = CType(dr("R15"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("A15")) Then v_Redemption.A15 = CType(dr("A15"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RH16")) Then v_Redemption.RH16 = CType(dr("RH16"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("E16")) Then v_Redemption.E16 = CType(dr("E16"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("R16")) Then v_Redemption.R16 = CType(dr("R16"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("A16")) Then v_Redemption.A16 = CType(dr("A16"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RH17")) Then v_Redemption.RH17 = CType(dr("RH17"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("E17")) Then v_Redemption.E17 = CType(dr("E17"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("R17")) Then v_Redemption.R17 = CType(dr("R17"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("A17")) Then v_Redemption.A17 = CType(dr("A17"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RH18")) Then v_Redemption.RH18 = CType(dr("RH18"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("E18")) Then v_Redemption.E18 = CType(dr("E18"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("R18")) Then v_Redemption.R18 = CType(dr("R18"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("A18")) Then v_Redemption.A18 = CType(dr("A18"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RH19")) Then v_Redemption.RH19 = CType(dr("RH19"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("E19")) Then v_Redemption.E19 = CType(dr("E19"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("R19")) Then v_Redemption.R19 = CType(dr("R19"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("A19")) Then v_Redemption.A19 = CType(dr("A19"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RH20")) Then v_Redemption.RH20 = CType(dr("RH20"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("E20")) Then v_Redemption.E20 = CType(dr("E20"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("R20")) Then v_Redemption.R20 = CType(dr("R20"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("A20")) Then v_Redemption.A20 = CType(dr("A20"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RH21")) Then v_Redemption.RH21 = CType(dr("RH21"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("E21")) Then v_Redemption.E21 = CType(dr("E21"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("R21")) Then v_Redemption.R21 = CType(dr("R21"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("A21")) Then v_Redemption.A21 = CType(dr("A21"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RH22")) Then v_Redemption.RH22 = CType(dr("RH22"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("E22")) Then v_Redemption.E22 = CType(dr("E22"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("R22")) Then v_Redemption.R22 = CType(dr("R22"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("A22")) Then v_Redemption.A22 = CType(dr("A22"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RH23")) Then v_Redemption.RH23 = CType(dr("RH23"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("E23")) Then v_Redemption.E23 = CType(dr("E23"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("R23")) Then v_Redemption.R23 = CType(dr("R23"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("A23")) Then v_Redemption.A23 = CType(dr("A23"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RH24")) Then v_Redemption.RH24 = CType(dr("RH24"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("E24")) Then v_Redemption.E24 = CType(dr("E24"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("R24")) Then v_Redemption.R24 = CType(dr("R24"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("A24")) Then v_Redemption.A24 = CType(dr("A24"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RH25")) Then v_Redemption.RH25 = CType(dr("RH25"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("E25")) Then v_Redemption.E25 = CType(dr("E25"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("R25")) Then v_Redemption.R25 = CType(dr("R25"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("A25")) Then v_Redemption.A25 = CType(dr("A25"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RH26")) Then v_Redemption.RH26 = CType(dr("RH26"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("E26")) Then v_Redemption.E26 = CType(dr("E26"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("R26")) Then v_Redemption.R26 = CType(dr("R26"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("A26")) Then v_Redemption.A26 = CType(dr("A26"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RH27")) Then v_Redemption.RH27 = CType(dr("RH27"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("E27")) Then v_Redemption.E27 = CType(dr("E27"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("R27")) Then v_Redemption.R27 = CType(dr("R27"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("A27")) Then v_Redemption.A27 = CType(dr("A27"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RH28")) Then v_Redemption.RH28 = CType(dr("RH28"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("E28")) Then v_Redemption.E28 = CType(dr("E28"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("R28")) Then v_Redemption.R28 = CType(dr("R28"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("A28")) Then v_Redemption.A28 = CType(dr("A28"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RH29")) Then v_Redemption.RH29 = CType(dr("RH29"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("E29")) Then v_Redemption.E29 = CType(dr("E29"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("R29")) Then v_Redemption.R29 = CType(dr("R29"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("A29")) Then v_Redemption.A29 = CType(dr("A29"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RH30")) Then v_Redemption.RH30 = CType(dr("RH30"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("E30")) Then v_Redemption.E30 = CType(dr("E30"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("R30")) Then v_Redemption.R30 = CType(dr("R30"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("A30")) Then v_Redemption.A30 = CType(dr("A30"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RH31")) Then v_Redemption.RH31 = CType(dr("RH31"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("E31")) Then v_Redemption.E31 = CType(dr("E31"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("R31")) Then v_Redemption.R31 = CType(dr("R31"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("A31")) Then v_Redemption.A31 = CType(dr("A31"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then v_Redemption.RowStatus = CType(dr("RowStatus"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then v_Redemption.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then v_Redemption.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then v_Redemption.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then v_Redemption.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return v_Redemption

        End Function

        Private Sub SetTableName()

            If Not (GetType(v_Redemption) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(v_Redemption), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(v_Redemption).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

