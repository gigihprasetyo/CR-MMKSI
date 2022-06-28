
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : v_Redemption Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2011 
'// ---------------------
'// $History      : $
'// Generated on 11/22/2011 - 9:59:14 AM
'//
'// ===========================================================================	
#End Region

#Region ".NET Base Class Namespace Imports"
Imports System
Imports System.Collections
#End Region

#Region "Custom Namespace Imports"
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports KTB.DNet.Domain.Search
#End Region

Namespace KTB.DNet.Domain
    <Serializable(), TableInfo("v_Redemption")> _
    Public Class v_Redemption
        Inherits DomainObject

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
        End Sub

        Public Sub New(ByVal ID As Integer)
            _iD = ID
        End Sub

#End Region

#Region "Private Variables"

        Private _iD As Integer
        Private _vechileColorID As Short
        Private _vehicleCode As String = String.Empty
        Private _vehicleDesc As String = String.Empty
        Private _dealerID As Short
        Private _dealerCode As String = String.Empty
        Private _periodYear As Integer
        Private _periodMonth As Integer
        Private _totalOC As Integer
        Private _totalRequest As Integer
        Private _totalRespond As Integer
        Private _rH1 As Integer
        Private _e1 As Integer
        Private _r1 As Integer
        Private _a1 As Integer
        Private _rH2 As Integer
        Private _e2 As Integer
        Private _r2 As Integer
        Private _a2 As Integer
        Private _rH3 As Integer
        Private _e3 As Integer
        Private _r3 As Integer
        Private _a3 As Integer
        Private _rH4 As Integer
        Private _e4 As Integer
        Private _r4 As Integer
        Private _a4 As Integer
        Private _rH5 As Integer
        Private _e5 As Integer
        Private _r5 As Integer
        Private _a5 As Integer
        Private _rH6 As Integer
        Private _e6 As Integer
        Private _r6 As Integer
        Private _a6 As Integer
        Private _rH7 As Integer
        Private _e7 As Integer
        Private _r7 As Integer
        Private _a7 As Integer
        Private _rH8 As Integer
        Private _e8 As Integer
        Private _r8 As Integer
        Private _a8 As Integer
        Private _rH9 As Integer
        Private _e9 As Integer
        Private _r9 As Integer
        Private _a9 As Integer
        Private _rH10 As Integer
        Private _e10 As Integer
        Private _r10 As Integer
        Private _a10 As Integer
        Private _rH11 As Integer
        Private _e11 As Integer
        Private _r11 As Integer
        Private _a11 As Integer
        Private _rH12 As Integer
        Private _e12 As Integer
        Private _r12 As Integer
        Private _a12 As Integer
        Private _rH13 As Integer
        Private _e13 As Integer
        Private _r13 As Integer
        Private _a13 As Integer
        Private _rH14 As Integer
        Private _e14 As Integer
        Private _r14 As Integer
        Private _a14 As Integer
        Private _rH15 As Integer
        Private _e15 As Integer
        Private _r15 As Integer
        Private _a15 As Integer
        Private _rH16 As Integer
        Private _e16 As Integer
        Private _r16 As Integer
        Private _a16 As Integer
        Private _rH17 As Integer
        Private _e17 As Integer
        Private _r17 As Integer
        Private _a17 As Integer
        Private _rH18 As Integer
        Private _e18 As Integer
        Private _r18 As Integer
        Private _a18 As Integer
        Private _rH19 As Integer
        Private _e19 As Integer
        Private _r19 As Integer
        Private _a19 As Integer
        Private _rH20 As Integer
        Private _e20 As Integer
        Private _r20 As Integer
        Private _a20 As Integer
        Private _rH21 As Integer
        Private _e21 As Integer
        Private _r21 As Integer
        Private _a21 As Integer
        Private _rH22 As Integer
        Private _e22 As Integer
        Private _r22 As Integer
        Private _a22 As Integer
        Private _rH23 As Integer
        Private _e23 As Integer
        Private _r23 As Integer
        Private _a23 As Integer
        Private _rH24 As Integer
        Private _e24 As Integer
        Private _r24 As Integer
        Private _a24 As Integer
        Private _rH25 As Integer
        Private _e25 As Integer
        Private _r25 As Integer
        Private _a25 As Integer
        Private _rH26 As Integer
        Private _e26 As Integer
        Private _r26 As Integer
        Private _a26 As Integer
        Private _rH27 As Integer
        Private _e27 As Integer
        Private _r27 As Integer
        Private _a27 As Integer
        Private _rH28 As Integer
        Private _e28 As Integer
        Private _r28 As Integer
        Private _a28 As Integer
        Private _rH29 As Integer
        Private _e29 As Integer
        Private _r29 As Integer
        Private _a29 As Integer
        Private _rH30 As Integer
        Private _e30 As Integer
        Private _r30 As Integer
        Private _a30 As Integer
        Private _rH31 As Integer
        Private _e31 As Integer
        Private _r31 As Integer
        Private _a31 As Integer
        Private _rowStatus As Integer
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)




#End Region

#Region "Public Properties"

        <ColumnInfo("ID", "{0}")> _
        Public Property ID() As Integer
            Get
                Return _iD
            End Get
            Set(ByVal value As Integer)
                _iD = value
            End Set
        End Property


        <ColumnInfo("VechileColorID", "{0}")> _
        Public Property VechileColorID() As Short
            Get
                Return _vechileColorID
            End Get
            Set(ByVal value As Short)
                _vechileColorID = value
            End Set
        End Property


        <ColumnInfo("VehicleCode", "'{0}'")> _
        Public Property VehicleCode() As String
            Get
                Return _vehicleCode
            End Get
            Set(ByVal value As String)
                _vehicleCode = value
            End Set
        End Property


        <ColumnInfo("VehicleDesc", "'{0}'")> _
        Public Property VehicleDesc() As String
            Get
                Return _vehicleDesc
            End Get
            Set(ByVal value As String)
                _vehicleDesc = value
            End Set
        End Property


        <ColumnInfo("DealerID", "{0}")> _
        Public Property DealerID() As Short
            Get
                Return _dealerID
            End Get
            Set(ByVal value As Short)
                _dealerID = value
            End Set
        End Property


        <ColumnInfo("DealerCode", "'{0}'")> _
        Public Property DealerCode() As String
            Get
                Return _dealerCode
            End Get
            Set(ByVal value As String)
                _dealerCode = value
            End Set
        End Property


        <ColumnInfo("PeriodYear", "{0}")> _
        Public Property PeriodYear() As Integer
            Get
                Return _periodYear
            End Get
            Set(ByVal value As Integer)
                _periodYear = value
            End Set
        End Property


        <ColumnInfo("PeriodMonth", "{0}")> _
        Public Property PeriodMonth() As Integer
            Get
                Return _periodMonth
            End Get
            Set(ByVal value As Integer)
                _periodMonth = value
            End Set
        End Property


        <ColumnInfo("TotalOC", "{0}")> _
        Public Property TotalOC() As Integer
            Get
                Return _totalOC
            End Get
            Set(ByVal value As Integer)
                _totalOC = value
            End Set
        End Property

        <ColumnInfo("TotalRequest", "{0}")> _
        Public Property TotalRequest() As Integer
            Get
                Return _totalRequest
            End Get
            Set(ByVal value As Integer)
                _totalRequest = value
            End Set
        End Property


        <ColumnInfo("TotalRespond", "{0}")> _
        Public Property TotalRespond() As Integer
            Get
                Return _totalRespond
            End Get
            Set(ByVal value As Integer)
                _totalRespond = value
            End Set
        End Property


        <ColumnInfo("RH1", "{0}")> _
        Public Property RH1() As Integer
            Get
                Return _rH1
            End Get
            Set(ByVal value As Integer)
                _rH1 = value
            End Set
        End Property


        <ColumnInfo("E1", "{0}")> _
        Public Property E1() As Integer
            Get
                Return _e1
            End Get
            Set(ByVal value As Integer)
                _e1 = value
            End Set
        End Property


        <ColumnInfo("R1", "{0}")> _
        Public Property R1() As Integer
            Get
                Return _r1
            End Get
            Set(ByVal value As Integer)
                _r1 = value
            End Set
        End Property


        <ColumnInfo("A1", "{0}")> _
        Public Property A1() As Integer
            Get
                Return _a1
            End Get
            Set(ByVal value As Integer)
                _a1 = value
            End Set
        End Property


        <ColumnInfo("RH2", "{0}")> _
        Public Property RH2() As Integer
            Get
                Return _rH2
            End Get
            Set(ByVal value As Integer)
                _rH2 = value
            End Set
        End Property


        <ColumnInfo("E2", "{0}")> _
        Public Property E2() As Integer
            Get
                Return _e2
            End Get
            Set(ByVal value As Integer)
                _e2 = value
            End Set
        End Property


        <ColumnInfo("R2", "{0}")> _
        Public Property R2() As Integer
            Get
                Return _r2
            End Get
            Set(ByVal value As Integer)
                _r2 = value
            End Set
        End Property


        <ColumnInfo("A2", "{0}")> _
        Public Property A2() As Integer
            Get
                Return _a2
            End Get
            Set(ByVal value As Integer)
                _a2 = value
            End Set
        End Property


        <ColumnInfo("RH3", "{0}")> _
        Public Property RH3() As Integer
            Get
                Return _rH3
            End Get
            Set(ByVal value As Integer)
                _rH3 = value
            End Set
        End Property


        <ColumnInfo("E3", "{0}")> _
        Public Property E3() As Integer
            Get
                Return _e3
            End Get
            Set(ByVal value As Integer)
                _e3 = value
            End Set
        End Property


        <ColumnInfo("R3", "{0}")> _
        Public Property R3() As Integer
            Get
                Return _r3
            End Get
            Set(ByVal value As Integer)
                _r3 = value
            End Set
        End Property


        <ColumnInfo("A3", "{0}")> _
        Public Property A3() As Integer
            Get
                Return _a3
            End Get
            Set(ByVal value As Integer)
                _a3 = value
            End Set
        End Property


        <ColumnInfo("RH4", "{0}")> _
        Public Property RH4() As Integer
            Get
                Return _rH4
            End Get
            Set(ByVal value As Integer)
                _rH4 = value
            End Set
        End Property


        <ColumnInfo("E4", "{0}")> _
        Public Property E4() As Integer
            Get
                Return _e4
            End Get
            Set(ByVal value As Integer)
                _e4 = value
            End Set
        End Property


        <ColumnInfo("R4", "{0}")> _
        Public Property R4() As Integer
            Get
                Return _r4
            End Get
            Set(ByVal value As Integer)
                _r4 = value
            End Set
        End Property


        <ColumnInfo("A4", "{0}")> _
        Public Property A4() As Integer
            Get
                Return _a4
            End Get
            Set(ByVal value As Integer)
                _a4 = value
            End Set
        End Property


        <ColumnInfo("RH5", "{0}")> _
        Public Property RH5() As Integer
            Get
                Return _rH5
            End Get
            Set(ByVal value As Integer)
                _rH5 = value
            End Set
        End Property


        <ColumnInfo("E5", "{0}")> _
        Public Property E5() As Integer
            Get
                Return _e5
            End Get
            Set(ByVal value As Integer)
                _e5 = value
            End Set
        End Property


        <ColumnInfo("R5", "{0}")> _
        Public Property R5() As Integer
            Get
                Return _r5
            End Get
            Set(ByVal value As Integer)
                _r5 = value
            End Set
        End Property


        <ColumnInfo("A5", "{0}")> _
        Public Property A5() As Integer
            Get
                Return _a5
            End Get
            Set(ByVal value As Integer)
                _a5 = value
            End Set
        End Property


        <ColumnInfo("RH6", "{0}")> _
        Public Property RH6() As Integer
            Get
                Return _rH6
            End Get
            Set(ByVal value As Integer)
                _rH6 = value
            End Set
        End Property


        <ColumnInfo("E6", "{0}")> _
        Public Property E6() As Integer
            Get
                Return _e6
            End Get
            Set(ByVal value As Integer)
                _e6 = value
            End Set
        End Property


        <ColumnInfo("R6", "{0}")> _
        Public Property R6() As Integer
            Get
                Return _r6
            End Get
            Set(ByVal value As Integer)
                _r6 = value
            End Set
        End Property


        <ColumnInfo("A6", "{0}")> _
        Public Property A6() As Integer
            Get
                Return _a6
            End Get
            Set(ByVal value As Integer)
                _a6 = value
            End Set
        End Property


        <ColumnInfo("RH7", "{0}")> _
        Public Property RH7() As Integer
            Get
                Return _rH7
            End Get
            Set(ByVal value As Integer)
                _rH7 = value
            End Set
        End Property


        <ColumnInfo("E7", "{0}")> _
        Public Property E7() As Integer
            Get
                Return _e7
            End Get
            Set(ByVal value As Integer)
                _e7 = value
            End Set
        End Property


        <ColumnInfo("R7", "{0}")> _
        Public Property R7() As Integer
            Get
                Return _r7
            End Get
            Set(ByVal value As Integer)
                _r7 = value
            End Set
        End Property


        <ColumnInfo("A7", "{0}")> _
        Public Property A7() As Integer
            Get
                Return _a7
            End Get
            Set(ByVal value As Integer)
                _a7 = value
            End Set
        End Property


        <ColumnInfo("RH8", "{0}")> _
        Public Property RH8() As Integer
            Get
                Return _rH8
            End Get
            Set(ByVal value As Integer)
                _rH8 = value
            End Set
        End Property


        <ColumnInfo("E8", "{0}")> _
        Public Property E8() As Integer
            Get
                Return _e8
            End Get
            Set(ByVal value As Integer)
                _e8 = value
            End Set
        End Property


        <ColumnInfo("R8", "{0}")> _
        Public Property R8() As Integer
            Get
                Return _r8
            End Get
            Set(ByVal value As Integer)
                _r8 = value
            End Set
        End Property


        <ColumnInfo("A8", "{0}")> _
        Public Property A8() As Integer
            Get
                Return _a8
            End Get
            Set(ByVal value As Integer)
                _a8 = value
            End Set
        End Property


        <ColumnInfo("RH9", "{0}")> _
        Public Property RH9() As Integer
            Get
                Return _rH9
            End Get
            Set(ByVal value As Integer)
                _rH9 = value
            End Set
        End Property


        <ColumnInfo("E9", "{0}")> _
        Public Property E9() As Integer
            Get
                Return _e9
            End Get
            Set(ByVal value As Integer)
                _e9 = value
            End Set
        End Property


        <ColumnInfo("R9", "{0}")> _
        Public Property R9() As Integer
            Get
                Return _r9
            End Get
            Set(ByVal value As Integer)
                _r9 = value
            End Set
        End Property


        <ColumnInfo("A9", "{0}")> _
        Public Property A9() As Integer
            Get
                Return _a9
            End Get
            Set(ByVal value As Integer)
                _a9 = value
            End Set
        End Property


        <ColumnInfo("RH10", "{0}")> _
        Public Property RH10() As Integer
            Get
                Return _rH10
            End Get
            Set(ByVal value As Integer)
                _rH10 = value
            End Set
        End Property


        <ColumnInfo("E10", "{0}")> _
        Public Property E10() As Integer
            Get
                Return _e10
            End Get
            Set(ByVal value As Integer)
                _e10 = value
            End Set
        End Property


        <ColumnInfo("R10", "{0}")> _
        Public Property R10() As Integer
            Get
                Return _r10
            End Get
            Set(ByVal value As Integer)
                _r10 = value
            End Set
        End Property


        <ColumnInfo("A10", "{0}")> _
        Public Property A10() As Integer
            Get
                Return _a10
            End Get
            Set(ByVal value As Integer)
                _a10 = value
            End Set
        End Property


        <ColumnInfo("RH11", "{0}")> _
        Public Property RH11() As Integer
            Get
                Return _rH11
            End Get
            Set(ByVal value As Integer)
                _rH11 = value
            End Set
        End Property


        <ColumnInfo("E11", "{0}")> _
        Public Property E11() As Integer
            Get
                Return _e11
            End Get
            Set(ByVal value As Integer)
                _e11 = value
            End Set
        End Property


        <ColumnInfo("R11", "{0}")> _
        Public Property R11() As Integer
            Get
                Return _r11
            End Get
            Set(ByVal value As Integer)
                _r11 = value
            End Set
        End Property


        <ColumnInfo("A11", "{0}")> _
        Public Property A11() As Integer
            Get
                Return _a11
            End Get
            Set(ByVal value As Integer)
                _a11 = value
            End Set
        End Property


        <ColumnInfo("RH12", "{0}")> _
        Public Property RH12() As Integer
            Get
                Return _rH12
            End Get
            Set(ByVal value As Integer)
                _rH12 = value
            End Set
        End Property


        <ColumnInfo("E12", "{0}")> _
        Public Property E12() As Integer
            Get
                Return _e12
            End Get
            Set(ByVal value As Integer)
                _e12 = value
            End Set
        End Property


        <ColumnInfo("R12", "{0}")> _
        Public Property R12() As Integer
            Get
                Return _r12
            End Get
            Set(ByVal value As Integer)
                _r12 = value
            End Set
        End Property


        <ColumnInfo("A12", "{0}")> _
        Public Property A12() As Integer
            Get
                Return _a12
            End Get
            Set(ByVal value As Integer)
                _a12 = value
            End Set
        End Property


        <ColumnInfo("RH13", "{0}")> _
        Public Property RH13() As Integer
            Get
                Return _rH13
            End Get
            Set(ByVal value As Integer)
                _rH13 = value
            End Set
        End Property


        <ColumnInfo("E13", "{0}")> _
        Public Property E13() As Integer
            Get
                Return _e13
            End Get
            Set(ByVal value As Integer)
                _e13 = value
            End Set
        End Property


        <ColumnInfo("R13", "{0}")> _
        Public Property R13() As Integer
            Get
                Return _r13
            End Get
            Set(ByVal value As Integer)
                _r13 = value
            End Set
        End Property


        <ColumnInfo("A13", "{0}")> _
        Public Property A13() As Integer
            Get
                Return _a13
            End Get
            Set(ByVal value As Integer)
                _a13 = value
            End Set
        End Property


        <ColumnInfo("RH14", "{0}")> _
        Public Property RH14() As Integer
            Get
                Return _rH14
            End Get
            Set(ByVal value As Integer)
                _rH14 = value
            End Set
        End Property


        <ColumnInfo("E14", "{0}")> _
        Public Property E14() As Integer
            Get
                Return _e14
            End Get
            Set(ByVal value As Integer)
                _e14 = value
            End Set
        End Property


        <ColumnInfo("R14", "{0}")> _
        Public Property R14() As Integer
            Get
                Return _r14
            End Get
            Set(ByVal value As Integer)
                _r14 = value
            End Set
        End Property


        <ColumnInfo("A14", "{0}")> _
        Public Property A14() As Integer
            Get
                Return _a14
            End Get
            Set(ByVal value As Integer)
                _a14 = value
            End Set
        End Property


        <ColumnInfo("RH15", "{0}")> _
        Public Property RH15() As Integer
            Get
                Return _rH15
            End Get
            Set(ByVal value As Integer)
                _rH15 = value
            End Set
        End Property


        <ColumnInfo("E15", "{0}")> _
        Public Property E15() As Integer
            Get
                Return _e15
            End Get
            Set(ByVal value As Integer)
                _e15 = value
            End Set
        End Property


        <ColumnInfo("R15", "{0}")> _
        Public Property R15() As Integer
            Get
                Return _r15
            End Get
            Set(ByVal value As Integer)
                _r15 = value
            End Set
        End Property


        <ColumnInfo("A15", "{0}")> _
        Public Property A15() As Integer
            Get
                Return _a15
            End Get
            Set(ByVal value As Integer)
                _a15 = value
            End Set
        End Property


        <ColumnInfo("RH16", "{0}")> _
        Public Property RH16() As Integer
            Get
                Return _rH16
            End Get
            Set(ByVal value As Integer)
                _rH16 = value
            End Set
        End Property


        <ColumnInfo("E16", "{0}")> _
        Public Property E16() As Integer
            Get
                Return _e16
            End Get
            Set(ByVal value As Integer)
                _e16 = value
            End Set
        End Property


        <ColumnInfo("R16", "{0}")> _
        Public Property R16() As Integer
            Get
                Return _r16
            End Get
            Set(ByVal value As Integer)
                _r16 = value
            End Set
        End Property


        <ColumnInfo("A16", "{0}")> _
        Public Property A16() As Integer
            Get
                Return _a16
            End Get
            Set(ByVal value As Integer)
                _a16 = value
            End Set
        End Property


        <ColumnInfo("RH17", "{0}")> _
        Public Property RH17() As Integer
            Get
                Return _rH17
            End Get
            Set(ByVal value As Integer)
                _rH17 = value
            End Set
        End Property


        <ColumnInfo("E17", "{0}")> _
        Public Property E17() As Integer
            Get
                Return _e17
            End Get
            Set(ByVal value As Integer)
                _e17 = value
            End Set
        End Property


        <ColumnInfo("R17", "{0}")> _
        Public Property R17() As Integer
            Get
                Return _r17
            End Get
            Set(ByVal value As Integer)
                _r17 = value
            End Set
        End Property


        <ColumnInfo("A17", "{0}")> _
        Public Property A17() As Integer
            Get
                Return _a17
            End Get
            Set(ByVal value As Integer)
                _a17 = value
            End Set
        End Property


        <ColumnInfo("RH18", "{0}")> _
        Public Property RH18() As Integer
            Get
                Return _rH18
            End Get
            Set(ByVal value As Integer)
                _rH18 = value
            End Set
        End Property


        <ColumnInfo("E18", "{0}")> _
        Public Property E18() As Integer
            Get
                Return _e18
            End Get
            Set(ByVal value As Integer)
                _e18 = value
            End Set
        End Property


        <ColumnInfo("R18", "{0}")> _
        Public Property R18() As Integer
            Get
                Return _r18
            End Get
            Set(ByVal value As Integer)
                _r18 = value
            End Set
        End Property


        <ColumnInfo("A18", "{0}")> _
        Public Property A18() As Integer
            Get
                Return _a18
            End Get
            Set(ByVal value As Integer)
                _a18 = value
            End Set
        End Property


        <ColumnInfo("RH19", "{0}")> _
        Public Property RH19() As Integer
            Get
                Return _rH19
            End Get
            Set(ByVal value As Integer)
                _rH19 = value
            End Set
        End Property


        <ColumnInfo("E19", "{0}")> _
        Public Property E19() As Integer
            Get
                Return _e19
            End Get
            Set(ByVal value As Integer)
                _e19 = value
            End Set
        End Property


        <ColumnInfo("R19", "{0}")> _
        Public Property R19() As Integer
            Get
                Return _r19
            End Get
            Set(ByVal value As Integer)
                _r19 = value
            End Set
        End Property


        <ColumnInfo("A19", "{0}")> _
        Public Property A19() As Integer
            Get
                Return _a19
            End Get
            Set(ByVal value As Integer)
                _a19 = value
            End Set
        End Property


        <ColumnInfo("RH20", "{0}")> _
        Public Property RH20() As Integer
            Get
                Return _rH20
            End Get
            Set(ByVal value As Integer)
                _rH20 = value
            End Set
        End Property


        <ColumnInfo("E20", "{0}")> _
        Public Property E20() As Integer
            Get
                Return _e20
            End Get
            Set(ByVal value As Integer)
                _e20 = value
            End Set
        End Property


        <ColumnInfo("R20", "{0}")> _
        Public Property R20() As Integer
            Get
                Return _r20
            End Get
            Set(ByVal value As Integer)
                _r20 = value
            End Set
        End Property


        <ColumnInfo("A20", "{0}")> _
        Public Property A20() As Integer
            Get
                Return _a20
            End Get
            Set(ByVal value As Integer)
                _a20 = value
            End Set
        End Property


        <ColumnInfo("RH21", "{0}")> _
        Public Property RH21() As Integer
            Get
                Return _rH21
            End Get
            Set(ByVal value As Integer)
                _rH21 = value
            End Set
        End Property


        <ColumnInfo("E21", "{0}")> _
        Public Property E21() As Integer
            Get
                Return _e21
            End Get
            Set(ByVal value As Integer)
                _e21 = value
            End Set
        End Property


        <ColumnInfo("R21", "{0}")> _
        Public Property R21() As Integer
            Get
                Return _r21
            End Get
            Set(ByVal value As Integer)
                _r21 = value
            End Set
        End Property


        <ColumnInfo("A21", "{0}")> _
        Public Property A21() As Integer
            Get
                Return _a21
            End Get
            Set(ByVal value As Integer)
                _a21 = value
            End Set
        End Property


        <ColumnInfo("RH22", "{0}")> _
        Public Property RH22() As Integer
            Get
                Return _rH22
            End Get
            Set(ByVal value As Integer)
                _rH22 = value
            End Set
        End Property


        <ColumnInfo("E22", "{0}")> _
        Public Property E22() As Integer
            Get
                Return _e22
            End Get
            Set(ByVal value As Integer)
                _e22 = value
            End Set
        End Property


        <ColumnInfo("R22", "{0}")> _
        Public Property R22() As Integer
            Get
                Return _r22
            End Get
            Set(ByVal value As Integer)
                _r22 = value
            End Set
        End Property


        <ColumnInfo("A22", "{0}")> _
        Public Property A22() As Integer
            Get
                Return _a22
            End Get
            Set(ByVal value As Integer)
                _a22 = value
            End Set
        End Property


        <ColumnInfo("RH23", "{0}")> _
        Public Property RH23() As Integer
            Get
                Return _rH23
            End Get
            Set(ByVal value As Integer)
                _rH23 = value
            End Set
        End Property


        <ColumnInfo("E23", "{0}")> _
        Public Property E23() As Integer
            Get
                Return _e23
            End Get
            Set(ByVal value As Integer)
                _e23 = value
            End Set
        End Property


        <ColumnInfo("R23", "{0}")> _
        Public Property R23() As Integer
            Get
                Return _r23
            End Get
            Set(ByVal value As Integer)
                _r23 = value
            End Set
        End Property


        <ColumnInfo("A23", "{0}")> _
        Public Property A23() As Integer
            Get
                Return _a23
            End Get
            Set(ByVal value As Integer)
                _a23 = value
            End Set
        End Property


        <ColumnInfo("RH24", "{0}")> _
        Public Property RH24() As Integer
            Get
                Return _rH24
            End Get
            Set(ByVal value As Integer)
                _rH24 = value
            End Set
        End Property


        <ColumnInfo("E24", "{0}")> _
        Public Property E24() As Integer
            Get
                Return _e24
            End Get
            Set(ByVal value As Integer)
                _e24 = value
            End Set
        End Property


        <ColumnInfo("R24", "{0}")> _
        Public Property R24() As Integer
            Get
                Return _r24
            End Get
            Set(ByVal value As Integer)
                _r24 = value
            End Set
        End Property


        <ColumnInfo("A24", "{0}")> _
        Public Property A24() As Integer
            Get
                Return _a24
            End Get
            Set(ByVal value As Integer)
                _a24 = value
            End Set
        End Property


        <ColumnInfo("RH25", "{0}")> _
        Public Property RH25() As Integer
            Get
                Return _rH25
            End Get
            Set(ByVal value As Integer)
                _rH25 = value
            End Set
        End Property


        <ColumnInfo("E25", "{0}")> _
        Public Property E25() As Integer
            Get
                Return _e25
            End Get
            Set(ByVal value As Integer)
                _e25 = value
            End Set
        End Property


        <ColumnInfo("R25", "{0}")> _
        Public Property R25() As Integer
            Get
                Return _r25
            End Get
            Set(ByVal value As Integer)
                _r25 = value
            End Set
        End Property


        <ColumnInfo("A25", "{0}")> _
        Public Property A25() As Integer
            Get
                Return _a25
            End Get
            Set(ByVal value As Integer)
                _a25 = value
            End Set
        End Property


        <ColumnInfo("RH26", "{0}")> _
        Public Property RH26() As Integer
            Get
                Return _rH26
            End Get
            Set(ByVal value As Integer)
                _rH26 = value
            End Set
        End Property


        <ColumnInfo("E26", "{0}")> _
        Public Property E26() As Integer
            Get
                Return _e26
            End Get
            Set(ByVal value As Integer)
                _e26 = value
            End Set
        End Property


        <ColumnInfo("R26", "{0}")> _
        Public Property R26() As Integer
            Get
                Return _r26
            End Get
            Set(ByVal value As Integer)
                _r26 = value
            End Set
        End Property


        <ColumnInfo("A26", "{0}")> _
        Public Property A26() As Integer
            Get
                Return _a26
            End Get
            Set(ByVal value As Integer)
                _a26 = value
            End Set
        End Property


        <ColumnInfo("RH27", "{0}")> _
        Public Property RH27() As Integer
            Get
                Return _rH27
            End Get
            Set(ByVal value As Integer)
                _rH27 = value
            End Set
        End Property


        <ColumnInfo("E27", "{0}")> _
        Public Property E27() As Integer
            Get
                Return _e27
            End Get
            Set(ByVal value As Integer)
                _e27 = value
            End Set
        End Property


        <ColumnInfo("R27", "{0}")> _
        Public Property R27() As Integer
            Get
                Return _r27
            End Get
            Set(ByVal value As Integer)
                _r27 = value
            End Set
        End Property


        <ColumnInfo("A27", "{0}")> _
        Public Property A27() As Integer
            Get
                Return _a27
            End Get
            Set(ByVal value As Integer)
                _a27 = value
            End Set
        End Property


        <ColumnInfo("RH28", "{0}")> _
        Public Property RH28() As Integer
            Get
                Return _rH28
            End Get
            Set(ByVal value As Integer)
                _rH28 = value
            End Set
        End Property


        <ColumnInfo("E28", "{0}")> _
        Public Property E28() As Integer
            Get
                Return _e28
            End Get
            Set(ByVal value As Integer)
                _e28 = value
            End Set
        End Property


        <ColumnInfo("R28", "{0}")> _
        Public Property R28() As Integer
            Get
                Return _r28
            End Get
            Set(ByVal value As Integer)
                _r28 = value
            End Set
        End Property


        <ColumnInfo("A28", "{0}")> _
        Public Property A28() As Integer
            Get
                Return _a28
            End Get
            Set(ByVal value As Integer)
                _a28 = value
            End Set
        End Property


        <ColumnInfo("RH29", "{0}")> _
        Public Property RH29() As Integer
            Get
                Return _rH29
            End Get
            Set(ByVal value As Integer)
                _rH29 = value
            End Set
        End Property


        <ColumnInfo("E29", "{0}")> _
        Public Property E29() As Integer
            Get
                Return _e29
            End Get
            Set(ByVal value As Integer)
                _e29 = value
            End Set
        End Property


        <ColumnInfo("R29", "{0}")> _
        Public Property R29() As Integer
            Get
                Return _r29
            End Get
            Set(ByVal value As Integer)
                _r29 = value
            End Set
        End Property


        <ColumnInfo("A29", "{0}")> _
        Public Property A29() As Integer
            Get
                Return _a29
            End Get
            Set(ByVal value As Integer)
                _a29 = value
            End Set
        End Property


        <ColumnInfo("RH30", "{0}")> _
        Public Property RH30() As Integer
            Get
                Return _rH30
            End Get
            Set(ByVal value As Integer)
                _rH30 = value
            End Set
        End Property


        <ColumnInfo("E30", "{0}")> _
        Public Property E30() As Integer
            Get
                Return _e30
            End Get
            Set(ByVal value As Integer)
                _e30 = value
            End Set
        End Property


        <ColumnInfo("R30", "{0}")> _
        Public Property R30() As Integer
            Get
                Return _r30
            End Get
            Set(ByVal value As Integer)
                _r30 = value
            End Set
        End Property


        <ColumnInfo("A30", "{0}")> _
        Public Property A30() As Integer
            Get
                Return _a30
            End Get
            Set(ByVal value As Integer)
                _a30 = value
            End Set
        End Property


        <ColumnInfo("RH31", "{0}")> _
        Public Property RH31() As Integer
            Get
                Return _rH31
            End Get
            Set(ByVal value As Integer)
                _rH31 = value
            End Set
        End Property


        <ColumnInfo("E31", "{0}")> _
        Public Property E31() As Integer
            Get
                Return _e31
            End Get
            Set(ByVal value As Integer)
                _e31 = value
            End Set
        End Property


        <ColumnInfo("R31", "{0}")> _
        Public Property R31() As Integer
            Get
                Return _r31
            End Get
            Set(ByVal value As Integer)
                _r31 = value
            End Set
        End Property


        <ColumnInfo("A31", "{0}")> _
        Public Property A31() As Integer
            Get
                Return _a31
            End Get
            Set(ByVal value As Integer)
                _a31 = value
            End Set
        End Property


        <ColumnInfo("RowStatus", "{0}")> _
        Public Property RowStatus() As Integer
            Get
                Return _rowStatus
            End Get
            Set(ByVal value As Integer)
                _rowStatus = value
            End Set
        End Property


        <ColumnInfo("CreatedBy", "'{0}'")> _
        Public Property CreatedBy() As String
            Get
                Return _createdBy
            End Get
            Set(ByVal value As String)
                _createdBy = value
            End Set
        End Property


        <ColumnInfo("CreatedTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property CreatedTime() As DateTime
            Get
                Return _createdTime
            End Get
            Set(ByVal value As DateTime)
                _createdTime = value
            End Set
        End Property


        <ColumnInfo("LastUpdateBy", "'{0}'")> _
        Public Property LastUpdateBy() As String
            Get
                Return _lastUpdateBy
            End Get
            Set(ByVal value As String)
                _lastUpdateBy = value
            End Set
        End Property


        <ColumnInfo("LastUpdateTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property LastUpdateTime() As DateTime
            Get
                Return _lastUpdateTime
            End Get
            Set(ByVal value As DateTime)
                _lastUpdateTime = value
            End Set
        End Property




#End Region

#Region "Generated Method"
        Public Function GetStrDate(ByVal dateInput As DateTime, ByVal dateFormat As String) As String
            If dateInput = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) Then
                Return ""
            Else
                Return Format(dateInput, dateFormat)
            End If
        End Function
#End Region

#Region "Custom Method"
        Private _redemptionHeaders As New ArrayList

        Public Property RedemptionHeaders() As ArrayList
            Get
                If Me._redemptionHeaders.Count < 31 Then
                    InitiateRH()
                End If
                Return Me._redemptionHeaders
            End Get
            Set(ByVal Value As ArrayList)
                Me._redemptionHeaders = Value
            End Set
        End Property

        Private Sub InitiateRH()
            'Dim oRH As RedemptionHeader
            Dim i As Integer

            Me._redemptionHeaders = New ArrayList
            For i = 1 To 31
                Me._redemptionHeaders.Add(New RedemptionHeader)
            Next

            Me._redemptionHeaders(0) = DoLoad(GetType(RedemptionHeader).ToString, Me.RH1)
            Me._redemptionHeaders(1) = DoLoad(GetType(RedemptionHeader).ToString, Me.RH2)
            Me._redemptionHeaders(2) = DoLoad(GetType(RedemptionHeader).ToString, Me.RH3)
            Me._redemptionHeaders(3) = DoLoad(GetType(RedemptionHeader).ToString, Me.RH4)
            Me._redemptionHeaders(4) = DoLoad(GetType(RedemptionHeader).ToString, Me.RH5)
            Me._redemptionHeaders(5) = DoLoad(GetType(RedemptionHeader).ToString, Me.RH6)
            Me._redemptionHeaders(6) = DoLoad(GetType(RedemptionHeader).ToString, Me.RH7)
            Me._redemptionHeaders(7) = DoLoad(GetType(RedemptionHeader).ToString, Me.RH8)
            Me._redemptionHeaders(8) = DoLoad(GetType(RedemptionHeader).ToString, Me.RH9)
            Me._redemptionHeaders(9) = DoLoad(GetType(RedemptionHeader).ToString, Me.RH10)
            Me._redemptionHeaders(10) = DoLoad(GetType(RedemptionHeader).ToString, Me.RH11)
            Me._redemptionHeaders(11) = DoLoad(GetType(RedemptionHeader).ToString, Me.RH12)
            Me._redemptionHeaders(12) = DoLoad(GetType(RedemptionHeader).ToString, Me.RH13)
            Me._redemptionHeaders(13) = DoLoad(GetType(RedemptionHeader).ToString, Me.RH14)
            Me._redemptionHeaders(14) = DoLoad(GetType(RedemptionHeader).ToString, Me.RH15)
            Me._redemptionHeaders(15) = DoLoad(GetType(RedemptionHeader).ToString, Me.RH16)
            Me._redemptionHeaders(16) = DoLoad(GetType(RedemptionHeader).ToString, Me.RH17)
            Me._redemptionHeaders(17) = DoLoad(GetType(RedemptionHeader).ToString, Me.RH18)
            Me._redemptionHeaders(18) = DoLoad(GetType(RedemptionHeader).ToString, Me.RH19)
            Me._redemptionHeaders(19) = DoLoad(GetType(RedemptionHeader).ToString, Me.RH20)
            Me._redemptionHeaders(20) = DoLoad(GetType(RedemptionHeader).ToString, Me.RH21)
            Me._redemptionHeaders(21) = DoLoad(GetType(RedemptionHeader).ToString, Me.RH22)
            Me._redemptionHeaders(22) = DoLoad(GetType(RedemptionHeader).ToString, Me.RH23)
            Me._redemptionHeaders(23) = DoLoad(GetType(RedemptionHeader).ToString, Me.RH24)
            Me._redemptionHeaders(24) = DoLoad(GetType(RedemptionHeader).ToString, Me.RH25)
            Me._redemptionHeaders(25) = DoLoad(GetType(RedemptionHeader).ToString, Me.RH26)
            Me._redemptionHeaders(26) = DoLoad(GetType(RedemptionHeader).ToString, Me.RH27)
            Me._redemptionHeaders(27) = DoLoad(GetType(RedemptionHeader).ToString, Me.RH28)
            Me._redemptionHeaders(28) = DoLoad(GetType(RedemptionHeader).ToString, Me.RH29)
            Me._redemptionHeaders(29) = DoLoad(GetType(RedemptionHeader).ToString, Me.RH30)
            Me._redemptionHeaders(30) = DoLoad(GetType(RedemptionHeader).ToString, Me.RH31)
            For i = 0 To Me._redemptionHeaders.Count - 1
                If IsNothing(Me._redemptionHeaders(i)) Then Me._redemptionHeaders(i) = New RedemptionHeader
            Next
            'Try
            'oRH = CType(DoLoad(GetType(RedemptionHeader).ToString, Me.RH1), RedemptionHeader) : If IsNothing(oRH) Then oRH = New RedemptionHeader : Me._redemptionHeaders(0) = oRH
            'oRH = DoLoad(GetType(RedemptionHeader).ToString, Me.RH2) : If IsNothing(oRH) Then oRH = New RedemptionHeader : Me._redemptionHeaders(1) = oRH
            'oRH = DoLoad(GetType(RedemptionHeader).ToString, Me.RH3) : If IsNothing(oRH) Then oRH = New RedemptionHeader : Me._redemptionHeaders(2) = oRH
            'oRH = DoLoad(GetType(RedemptionHeader).ToString, Me.RH4) : If IsNothing(oRH) Then oRH = New RedemptionHeader : Me._redemptionHeaders(3) = oRH
            'oRH = DoLoad(GetType(RedemptionHeader).ToString, Me.RH5) : If IsNothing(oRH) Then oRH = New RedemptionHeader : Me._redemptionHeaders(4) = oRH
            'oRH = DoLoad(GetType(RedemptionHeader).ToString, Me.RH6) : If IsNothing(oRH) Then oRH = New RedemptionHeader : Me._redemptionHeaders(5) = oRH
            'oRH = DoLoad(GetType(RedemptionHeader).ToString, Me.RH7) : If IsNothing(oRH) Then oRH = New RedemptionHeader : Me._redemptionHeaders(6) = oRH
            'oRH = DoLoad(GetType(RedemptionHeader).ToString, Me.RH8) : If IsNothing(oRH) Then oRH = New RedemptionHeader : Me._redemptionHeaders(7) = oRH
            'oRH = DoLoad(GetType(RedemptionHeader).ToString, Me.RH9) : If IsNothing(oRH) Then oRH = New RedemptionHeader : Me._redemptionHeaders(8) = oRH
            'oRH = DoLoad(GetType(RedemptionHeader).ToString, Me.RH10) : If IsNothing(oRH) Then oRH = New RedemptionHeader : Me._redemptionHeaders(9) = oRH
            'oRH = DoLoad(GetType(RedemptionHeader).ToString, Me.RH11) : If IsNothing(oRH) Then oRH = New RedemptionHeader : Me._redemptionHeaders(10) = oRH
            'oRH = DoLoad(GetType(RedemptionHeader).ToString, Me.RH12) : If IsNothing(oRH) Then oRH = New RedemptionHeader : Me._redemptionHeaders(11) = oRH
            'oRH = DoLoad(GetType(RedemptionHeader).ToString, Me.RH13) : If IsNothing(oRH) Then oRH = New RedemptionHeader : Me._redemptionHeaders(12) = oRH
            'oRH = DoLoad(GetType(RedemptionHeader).ToString, Me.RH14) : If IsNothing(oRH) Then oRH = New RedemptionHeader : Me._redemptionHeaders(13) = oRH
            'oRH = DoLoad(GetType(RedemptionHeader).ToString, Me.RH15) : If IsNothing(oRH) Then oRH = New RedemptionHeader : Me._redemptionHeaders(14) = oRH
            'oRH = DoLoad(GetType(RedemptionHeader).ToString, Me.RH16) : If IsNothing(oRH) Then oRH = New RedemptionHeader : Me._redemptionHeaders(15) = oRH
            'oRH = DoLoad(GetType(RedemptionHeader).ToString, Me.RH17) : If IsNothing(oRH) Then oRH = New RedemptionHeader : Me._redemptionHeaders(16) = oRH
            'oRH = DoLoad(GetType(RedemptionHeader).ToString, Me.RH18) : If IsNothing(oRH) Then oRH = New RedemptionHeader : Me._redemptionHeaders(17) = oRH
            'oRH = DoLoad(GetType(RedemptionHeader).ToString, Me.RH19) : If IsNothing(oRH) Then oRH = New RedemptionHeader : Me._redemptionHeaders(18) = oRH
            'oRH = DoLoad(GetType(RedemptionHeader).ToString, Me.RH20) : If IsNothing(oRH) Then oRH = New RedemptionHeader : Me._redemptionHeaders(19) = oRH
            'oRH = DoLoad(GetType(RedemptionHeader).ToString, Me.RH21) : If IsNothing(oRH) Then oRH = New RedemptionHeader : Me._redemptionHeaders(20) = oRH
            'oRH = DoLoad(GetType(RedemptionHeader).ToString, Me.RH22) : If IsNothing(oRH) Then oRH = New RedemptionHeader : Me._redemptionHeaders(21) = oRH
            'oRH = DoLoad(GetType(RedemptionHeader).ToString, Me.RH23) : If IsNothing(oRH) Then oRH = New RedemptionHeader : Me._redemptionHeaders(22) = oRH
            'oRH = DoLoad(GetType(RedemptionHeader).ToString, Me.RH24) : If IsNothing(oRH) Then oRH = New RedemptionHeader : Me._redemptionHeaders(23) = oRH
            'oRH = DoLoad(GetType(RedemptionHeader).ToString, Me.RH25) : If IsNothing(oRH) Then oRH = New RedemptionHeader : Me._redemptionHeaders(24) = oRH
            'oRH = DoLoad(GetType(RedemptionHeader).ToString, Me.RH26) : If IsNothing(oRH) Then oRH = New RedemptionHeader : Me._redemptionHeaders(25) = oRH
            'oRH = DoLoad(GetType(RedemptionHeader).ToString, Me.RH27) : If IsNothing(oRH) Then oRH = New RedemptionHeader : Me._redemptionHeaders(26) = oRH
            'oRH = DoLoad(GetType(RedemptionHeader).ToString, Me.RH28) : If IsNothing(oRH) Then oRH = New RedemptionHeader : Me._redemptionHeaders(27) = oRH
            'oRH = DoLoad(GetType(RedemptionHeader).ToString, Me.RH29) : If IsNothing(oRH) Then oRH = New RedemptionHeader : Me._redemptionHeaders(28) = oRH
            'oRH = DoLoad(GetType(RedemptionHeader).ToString, Me.RH30) : If IsNothing(oRH) Then oRH = New RedemptionHeader : Me._redemptionHeaders(29) = oRH
            'oRH = DoLoad(GetType(RedemptionHeader).ToString, Me.RH31) : If IsNothing(oRH) Then oRH = New RedemptionHeader : Me._redemptionHeaders(30) = oRH


            'oRH = DoLoad(GetType(RedemptionHeader).ToString, Me.RH1) : If IsNothing(oRH) Then oRH = New RedemptionHeader : Me._redemptionHeaders.Add(oRH)
            'oRH = DoLoad(GetType(RedemptionHeader).ToString, Me.RH2) : If IsNothing(oRH) Then oRH = New RedemptionHeader : Me._redemptionHeaders.Add(oRH)
            'oRH = DoLoad(GetType(RedemptionHeader).ToString, Me.RH3) : If IsNothing(oRH) Then oRH = New RedemptionHeader : Me._redemptionHeaders.Add(oRH)
            'oRH = DoLoad(GetType(RedemptionHeader).ToString, Me.RH4) : If IsNothing(oRH) Then oRH = New RedemptionHeader : Me._redemptionHeaders.Add(oRH)
            'oRH = DoLoad(GetType(RedemptionHeader).ToString, Me.RH5) : If IsNothing(oRH) Then oRH = New RedemptionHeader : Me._redemptionHeaders.Add(oRH)
            'oRH = DoLoad(GetType(RedemptionHeader).ToString, Me.RH6) : If IsNothing(oRH) Then oRH = New RedemptionHeader : Me._redemptionHeaders.Add(oRH)
            'oRH = DoLoad(GetType(RedemptionHeader).ToString, Me.RH7) : If IsNothing(oRH) Then oRH = New RedemptionHeader : Me._redemptionHeaders.Add(oRH)
            'oRH = DoLoad(GetType(RedemptionHeader).ToString, Me.RH8) : If IsNothing(oRH) Then oRH = New RedemptionHeader : Me._redemptionHeaders.Add(oRH)
            'oRH = DoLoad(GetType(RedemptionHeader).ToString, Me.RH9) : If IsNothing(oRH) Then oRH = New RedemptionHeader : Me._redemptionHeaders.Add(oRH)
            'oRH = DoLoad(GetType(RedemptionHeader).ToString, Me.RH10) : If IsNothing(oRH) Then oRH = New RedemptionHeader : Me._redemptionHeaders.Add(oRH)
            'oRH = DoLoad(GetType(RedemptionHeader).ToString, Me.RH11) : If IsNothing(oRH) Then oRH = New RedemptionHeader : Me._redemptionHeaders.Add(oRH)
            'oRH = DoLoad(GetType(RedemptionHeader).ToString, Me.RH12) : If IsNothing(oRH) Then oRH = New RedemptionHeader : Me._redemptionHeaders.Add(oRH)
            'oRH = DoLoad(GetType(RedemptionHeader).ToString, Me.RH13) : If IsNothing(oRH) Then oRH = New RedemptionHeader : Me._redemptionHeaders.Add(oRH)
            'oRH = DoLoad(GetType(RedemptionHeader).ToString, Me.RH14) : If IsNothing(oRH) Then oRH = New RedemptionHeader : Me._redemptionHeaders.Add(oRH)
            'oRH = DoLoad(GetType(RedemptionHeader).ToString, Me.RH15) : If IsNothing(oRH) Then oRH = New RedemptionHeader : Me._redemptionHeaders.Add(oRH)
            'oRH = DoLoad(GetType(RedemptionHeader).ToString, Me.RH16) : If IsNothing(oRH) Then oRH = New RedemptionHeader : Me._redemptionHeaders.Add(oRH)
            'oRH = DoLoad(GetType(RedemptionHeader).ToString, Me.RH17) : If IsNothing(oRH) Then oRH = New RedemptionHeader : Me._redemptionHeaders.Add(oRH)
            'oRH = DoLoad(GetType(RedemptionHeader).ToString, Me.RH18) : If IsNothing(oRH) Then oRH = New RedemptionHeader : Me._redemptionHeaders.Add(oRH)
            'oRH = DoLoad(GetType(RedemptionHeader).ToString, Me.RH19) : If IsNothing(oRH) Then oRH = New RedemptionHeader : Me._redemptionHeaders.Add(oRH)
            'oRH = DoLoad(GetType(RedemptionHeader).ToString, Me.RH20) : If IsNothing(oRH) Then oRH = New RedemptionHeader : Me._redemptionHeaders.Add(oRH)
            'oRH = DoLoad(GetType(RedemptionHeader).ToString, Me.RH21) : If IsNothing(oRH) Then oRH = New RedemptionHeader : Me._redemptionHeaders.Add(oRH)
            'oRH = DoLoad(GetType(RedemptionHeader).ToString, Me.RH22) : If IsNothing(oRH) Then oRH = New RedemptionHeader : Me._redemptionHeaders.Add(oRH)
            'oRH = DoLoad(GetType(RedemptionHeader).ToString, Me.RH23) : If IsNothing(oRH) Then oRH = New RedemptionHeader : Me._redemptionHeaders.Add(oRH)
            'oRH = DoLoad(GetType(RedemptionHeader).ToString, Me.RH24) : If IsNothing(oRH) Then oRH = New RedemptionHeader : Me._redemptionHeaders.Add(oRH)
            'oRH = DoLoad(GetType(RedemptionHeader).ToString, Me.RH25) : If IsNothing(oRH) Then oRH = New RedemptionHeader : Me._redemptionHeaders.Add(oRH)
            'oRH = DoLoad(GetType(RedemptionHeader).ToString, Me.RH26) : If IsNothing(oRH) Then oRH = New RedemptionHeader : Me._redemptionHeaders.Add(oRH)
            'oRH = DoLoad(GetType(RedemptionHeader).ToString, Me.RH27) : If IsNothing(oRH) Then oRH = New RedemptionHeader : Me._redemptionHeaders.Add(oRH)
            'oRH = DoLoad(GetType(RedemptionHeader).ToString, Me.RH28) : If IsNothing(oRH) Then oRH = New RedemptionHeader : Me._redemptionHeaders.Add(oRH)
            'oRH = DoLoad(GetType(RedemptionHeader).ToString, Me.RH29) : If IsNothing(oRH) Then oRH = New RedemptionHeader : Me._redemptionHeaders.Add(oRH)
            'oRH = DoLoad(GetType(RedemptionHeader).ToString, Me.RH30) : If IsNothing(oRH) Then oRH = New RedemptionHeader : Me._redemptionHeaders.Add(oRH)
            'oRH = DoLoad(GetType(RedemptionHeader).ToString, Me.RH31) : If IsNothing(oRH) Then oRH = New RedemptionHeader : Me._redemptionHeaders.Add(oRH)
            'Catch ex As Exception
            'If Me._redemptionHeaders.Count < 31 Then
            '   For i = Me._redemptionHeaders.Count To 31
            '        Me._redemptionHeaders.Add(New RedemptionHeader)
            '    Next
            'End If
            'End Try
            'oRH = DoLoad(GetType(RedemptionHeader).ToString, Me.RH1) : If IsNothing(oRH) Then oRH = New RedemptionHeader : Me._redemptionHeaders.Add(oRH)
            'Me._redemptionHeaders.Add(DoLoad(GetType(RedemptionHeader).ToString, Me.RH1))
            'Me._redemptionHeaders.Add(DoLoad(GetType(RedemptionHeader).ToString, Me.RH2))
            'Me._redemptionHeaders.Add(DoLoad(GetType(RedemptionHeader).ToString, Me.RH3))
            'Me._redemptionHeaders.Add(DoLoad(GetType(RedemptionHeader).ToString, Me.RH4))
            'Me._redemptionHeaders.Add(DoLoad(GetType(RedemptionHeader).ToString, Me.RH5))
            'Me._redemptionHeaders.Add(DoLoad(GetType(RedemptionHeader).ToString, Me.RH6))
            'Me._redemptionHeaders.Add(DoLoad(GetType(RedemptionHeader).ToString, Me.RH7))
            'Me._redemptionHeaders.Add(DoLoad(GetType(RedemptionHeader).ToString, Me.RH8))
            'Me._redemptionHeaders.Add(DoLoad(GetType(RedemptionHeader).ToString, Me.RH9))
            'Me._redemptionHeaders.Add(DoLoad(GetType(RedemptionHeader).ToString, Me.RH10))
            'Me._redemptionHeaders.Add(DoLoad(GetType(RedemptionHeader).ToString, Me.RH11))
            'Me._redemptionHeaders.Add(DoLoad(GetType(RedemptionHeader).ToString, Me.RH12))
            'Me._redemptionHeaders.Add(DoLoad(GetType(RedemptionHeader).ToString, Me.RH13))
            'Me._redemptionHeaders.Add(DoLoad(GetType(RedemptionHeader).ToString, Me.RH14))
            'Me._redemptionHeaders.Add(DoLoad(GetType(RedemptionHeader).ToString, Me.RH15))
            'Me._redemptionHeaders.Add(DoLoad(GetType(RedemptionHeader).ToString, Me.RH16))
            'Me._redemptionHeaders.Add(DoLoad(GetType(RedemptionHeader).ToString, Me.RH17))
            'Me._redemptionHeaders.Add(DoLoad(GetType(RedemptionHeader).ToString, Me.RH18))
            'Me._redemptionHeaders.Add(DoLoad(GetType(RedemptionHeader).ToString, Me.RH19))
            'Me._redemptionHeaders.Add(DoLoad(GetType(RedemptionHeader).ToString, Me.RH20))
            'Me._redemptionHeaders.Add(DoLoad(GetType(RedemptionHeader).ToString, Me.RH21))
            'Me._redemptionHeaders.Add(DoLoad(GetType(RedemptionHeader).ToString, Me.RH22))
            'Me._redemptionHeaders.Add(DoLoad(GetType(RedemptionHeader).ToString, Me.RH23))
            'Me._redemptionHeaders.Add(DoLoad(GetType(RedemptionHeader).ToString, Me.RH24))
            'Me._redemptionHeaders.Add(DoLoad(GetType(RedemptionHeader).ToString, Me.RH25))
            'Me._redemptionHeaders.Add(DoLoad(GetType(RedemptionHeader).ToString, Me.RH26))
            'Me._redemptionHeaders.Add(DoLoad(GetType(RedemptionHeader).ToString, Me.RH27))
            'Me._redemptionHeaders.Add(DoLoad(GetType(RedemptionHeader).ToString, Me.RH28))
            'Me._redemptionHeaders.Add(DoLoad(GetType(RedemptionHeader).ToString, Me.RH29))
            'Me._redemptionHeaders.Add(DoLoad(GetType(RedemptionHeader).ToString, Me.RH30))
            'Me._redemptionHeaders.Add(DoLoad(GetType(RedemptionHeader).ToString, Me.RH31))

            'Dim oRH As RedemptionHeader
            'Dim i As Integer
            'Dim aRHsTemp As New ArrayList
            'For i = 0 To Me._redemptionHeaders.Count - 1
            '    'oRH = CType(Me._redemptionHeaders(i), RedemptionHeader)
            '    'If IsNothing(oRH) Then oRH = New RedemptionHeader
            '    'aRHsTemp.Add(oRH)
            '    If IsNothing(Me._redemptionHeaders(i)) Then
            '        aRHsTemp.Add(New RedemptionHeader)
            '    Else
            '        aRHsTemp.Add(CType(Me._redemptionHeaders(i), RedemptionHeader))
            '    End If
            'Next
            'Me._redemptionHeaders = aRHsTemp
        End Sub



#End Region

    End Class
End Namespace

