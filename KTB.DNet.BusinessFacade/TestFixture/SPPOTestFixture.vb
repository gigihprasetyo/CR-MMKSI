
#Region "Summary"
'===========================================================================
'AUTHOR        : Didin Komarudin
'PURPOSE       : Test Fixture for Spare part PO Transaction.
'SPECIAL NOTES : This test fixture requires setting up hardcode id value
'					Must change value every test run		   	
'---------------------
'Copyright  2005 
'---------------------
'===========================================================================	
#End Region

#Region ".Net namespaces"
Imports System
Imports System.Collections
Imports System.ComponentModel
Imports System.Data

#End Region

#Region "Custom Namespaces"
Imports KTB.DNet.BusinessFacade.SparePart
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Domain
Imports NUnit.Framework
Imports System.Security.Principal
#End Region

Namespace KTB.DNET.BusinessFacade.TestFixture

    <TestFixture()> Public Class SPPOTestFixture
        'Constant Variables For Insert Test
        Private Const CONST_DEALERID1 As Integer = 45
        Private Const CONST_DEALERID2 As Integer = 46
        Private Const CONST_PARTMASTER1 As Integer = 1
        Private Const CONST_PARTMASTER2 As Integer = 2
        Private Const CONST_QTY1 As Integer = 10
        Private Const CONST_QTY2 As Integer = 20

        'Constant Variables For Update Test
        Private Const CONST_NEWPARTMASTER1 As Integer = 5
        Private Const CONST_NEWPARTMASTER2 As Integer = 6
        Private Const CONST_NEWQTY1 As Integer = 5
        Private Const CONST_NEWQTY2 As Integer = 8

        'Private variables 
        Private CurrUser As IPrincipal
        Private _sparePartPOFacade As SparePartPOFacade
        Private _sparePartPO As SparePartPO = New SparePartPO
        Private _sparePartPORetrieved As SparePartPO
        Private _sparePartPOUpdated As SparePartPO
        Private _sparePartPODetailList As ArrayList = New ArrayList
        Private _dealer1, _dealer2 As Dealer
        Private _sparePartMaster1, _sparePartMaster2 As SparePartMaster
        Private _ID As Integer

#Region "Constructors/Destructors/Finalizers"
        Public Sub SparePartPOTestFixtures()
        End Sub
#End Region

#Region "Public Methods"
        'Assertions are central to unit testing in any of the xUnit frameworks, 
        'and NUnit is no exception. NUnit provides a rich set of assertions as 
        'static methods of the Assert class.
        'If an assertion fails, the method call does not return 
        'and an error is reported. If a test contains multiple assertions, 
        'any that follow the one that failed will not be executed. 
        'For this reason, it's usually best to try for one assertion per test.

        '---If NUnit found <Test()> tag, this function will be executed 
        <SetUp()> Public Sub Init()
            '--Get user principal from database 
            CurrUser = UserTestFixtures.GetUser()
            '--Instantiate Sparepare PO Facade class
            _sparePartPOFacade = New SparePartPOFacade(CurrUser)
        End Sub

        '---Test Insert scenario
        <Test()> Public Sub TestInsertSparePartPO()
            InitialHeaderObject()
            InitialDetailsObject()
            'Insert PO to Database
            _ID = _sparePartPOFacade.InsertSparePartPO(_sparePartPO, _sparePartPODetailList)
            'NUnit verified return value from InsertSparePartPO method, 
            'if the value is equal -1  NUnit will generete error
            Assert.IsTrue(_ID <> -1)
            'Condition(IsTrue)
            'Methods that test a specific condition are named 
            'for the condition they test and take the value tested as 
            'their first argument and, optionally a message as the second
        End Sub

        '---Test retrieve scenario
        <Test()> Public Sub TestRetrieveSparePartPO()
            'Retrieve inserted PO from database by ID 
            _sparePartPORetrieved = _sparePartPOFacade.Retrieve(_ID)
            'NUnit verified return value from Retrieve method, 
            'if the value is nothing  NUnit will generete error
            Assert.IsTrue(Not IsNothing(_sparePartPORetrieved))

            'NUnit check the properties/data, 
            'if the result is false  NUnit will generete error
            '---Verified PO Header 
            Assert.AreEqual(_sparePartPORetrieved.ID, _ID)
            Assert.IsTrue(_sparePartPORetrieved.PONumber <> String.Empty)
            Assert.AreEqual(_sparePartPORetrieved.PODate.ToShortDateString(), DateTime.Now.ToShortDateString())
            Assert.IsTrue(_sparePartPORetrieved.ProcessCode = String.Empty)

            '---Verified PO Details
            Dim sparePartPODetails1 As SparePartPODetail = CType(_sparePartPORetrieved.SparePartPODetails(0), SparePartPODetail)
            Dim sparePartPODetails2 As SparePartPODetail = CType(_sparePartPORetrieved.SparePartPODetails(1), SparePartPODetail)
            Assert.AreEqual(sparePartPODetails1.SparePartMaster.ID, CONST_PARTMASTER1)
            Assert.AreEqual(sparePartPODetails1.Quantity, CONST_QTY1)
            Assert.AreEqual(sparePartPODetails1.Amount, CONST_QTY1 * sparePartPODetails1.RetailPrice)
            Assert.AreEqual(sparePartPODetails2.SparePartMaster.ID, CONST_PARTMASTER2)
            Assert.AreEqual(sparePartPODetails2.Quantity, CONST_QTY2)
            Assert.AreEqual(sparePartPODetails2.Amount, CONST_QTY2 * sparePartPODetails2.RetailPrice)
            'Comparisons(AreEqual)
            'Assertions that perform comparisons are often your best choice 
            'because they report both expected and actual values

        End Sub

        '---Test retrieve scenario
        <Test()> Public Sub TestUpdateSparePartPO()
            _sparePartPOUpdated = _sparePartPOFacade.Retrieve(_ID)
            Assert.IsTrue(Not IsNothing(_sparePartPOUpdated))
            ' Change PO Item data to test Update scenario
            ChangeDataPODetail()
            _ID = _sparePartPOFacade.UpdateSparePartPO(_sparePartPOUpdated, _sparePartPOUpdated.SparePartPODetails)
            Assert.IsTrue(_ID <> -1)

            '---Verified PO Check Header
            _sparePartPOUpdated = _sparePartPOFacade.Retrieve(_ID)
            Assert.AreEqual(_ID, _sparePartPORetrieved.ID)
            Assert.AreEqual(_sparePartPOUpdated.PONumber, _sparePartPORetrieved.PONumber)
            Assert.AreEqual(_sparePartPOUpdated.PODate.ToShortDateString(), _sparePartPORetrieved.PODate.ToShortDateString())

            '---Verified PO Check Details
            Dim sparePartPODetails1 As SparePartPODetail = CType(_sparePartPOUpdated.SparePartPODetails(0), SparePartPODetail)
            Dim sparePartPODetails2 As SparePartPODetail = CType(_sparePartPOUpdated.SparePartPODetails(1), SparePartPODetail)
            Assert.AreEqual(sparePartPODetails1.SparePartMaster.ID, CONST_NEWPARTMASTER1)
            Assert.AreEqual(sparePartPODetails1.Quantity, CONST_NEWQTY1)
            Assert.AreEqual(sparePartPODetails1.Amount, CONST_NEWQTY1 * sparePartPODetails1.RetailPrice)
            Assert.AreEqual(sparePartPODetails2.SparePartMaster.ID, CONST_NEWPARTMASTER2)
            Assert.AreEqual(sparePartPODetails2.Quantity, CONST_NEWQTY2)
            Assert.AreEqual(sparePartPODetails2.Amount, CONST_NEWQTY2 * sparePartPODetails1.RetailPrice)
        End Sub

#End Region

#Region "Private Methodes"

        Private Sub InitialHeaderObject()
            _dealer1 = New DealerFacade(CurrUser).Retrieve(CONST_DEALERID1)
            _dealer2 = New DealerFacade(CurrUser).Retrieve(CONST_DEALERID2)
            _sparePartPO.Dealer = _dealer1
            _sparePartPO.OrderType = "E"
            _sparePartPO.PODate = Date.Now
            _sparePartPO.ProcessCode = ""
        End Sub

        Private Sub InitialDetailsObject()
            Dim objPartItem1 As SparePartPODetail = New SparePartPODetail
            Dim objPartItem2 As SparePartPODetail = New SparePartPODetail
            Dim sparePartMaster1 As SparePartMaster = New SparePartMasterFacade(CurrUser).Retrieve(CONST_PARTMASTER1)
            Dim sparePartMaster2 As SparePartMaster = New SparePartMasterFacade(CurrUser).Retrieve(CONST_PARTMASTER2)
            objPartItem1.SparePartMaster = sparePartMaster1
            objPartItem1.Quantity = CONST_QTY1
            objPartItem1.RetailPrice = sparePartMaster1.RetalPrice
            objPartItem1.CheckListStatus = 0
            _sparePartPODetailList.Add(objPartItem1)

            objPartItem2.SparePartMaster = sparePartMaster2
            objPartItem2.Quantity = CONST_QTY2
            objPartItem2.RetailPrice = sparePartMaster2.RetalPrice
            objPartItem2.CheckListStatus = 0
            _sparePartPODetailList.Add(objPartItem2)
        End Sub

        Private Sub ChangeDataPODetail()
            Dim objPartItem1 As SparePartPODetail = New SparePartPODetail
            Dim objPartItem2 As SparePartPODetail = New SparePartPODetail
            CType(_sparePartPOUpdated.SparePartPODetails(0), SparePartPODetail).RowStatus = DBRowStatus.Deleted
            CType(_sparePartPOUpdated.SparePartPODetails(1), SparePartPODetail).RowStatus = DBRowStatus.Deleted
            Dim sparePartMaster1 As SparePartMaster = New SparePartMasterFacade(CurrUser).Retrieve(CONST_NEWPARTMASTER1)
            Dim sparePartMaster2 As SparePartMaster = New SparePartMasterFacade(CurrUser).Retrieve(CONST_NEWPARTMASTER2)

            objPartItem1.SparePartMaster = sparePartMaster1
            objPartItem1.Quantity = CONST_NEWQTY1
            objPartItem1.RetailPrice = sparePartMaster1.RetalPrice
            objPartItem1.CheckListStatus = 0
            _sparePartPOUpdated.SparePartPODetails.Add(objPartItem1)

            objPartItem2.SparePartMaster = sparePartMaster2
            objPartItem2.Quantity = CONST_NEWQTY2
            objPartItem2.RetailPrice = sparePartMaster2.RetalPrice
            objPartItem2.CheckListStatus = 0
            _sparePartPOUpdated.SparePartPODetails.Add(objPartItem2)
        End Sub
#End Region
    End Class
End Namespace
