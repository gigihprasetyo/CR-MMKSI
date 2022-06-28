#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : DealerBranchDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 19/10/2018 3:32
//
// ===========================================================================	
#endregion

#region Namespace Imports
using System;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class DealerBranchDto : DtoBase
    {
        #region Private Variables
        private int _iD;
        private short _dealerID;
        private string _name = String.Empty;
        private string _status = String.Empty;
        private string _address = String.Empty;
        private string _zipCode = String.Empty;
        private string _phone = String.Empty;
        private string _fax = String.Empty;
        private string _website = String.Empty;
        private string _email = String.Empty;
        private string _typeBranch = String.Empty;
        private string _dealerBranchCode = String.Empty;
        private string _term1 = String.Empty;
        private string _term2 = String.Empty;
        private string _branchAssignmentNo = String.Empty;
        private DateTime _branchAssigmentDate = ((DateTime)(System.Data.SqlTypes.SqlDateTime.MinValue.Value));
        private string _salesUnitFlag = String.Empty;
        private string _serviceFlag = String.Empty;
        private string _sparepartFlag = String.Empty;
        private DealerDto _dealer;
        private CityDto _city;
        private ProvinceDto _province;
        private Area1Dto _area1;
        private Area2Dto _area2;
        private MainAreaDto _mainArea;
        private ArrayList _arrDBArea = new ArrayList();
        #endregion

        #region Constructors
        public DealerBranchDto() { }

        public DealerBranchDto(int id)
        {
            _iD = id;
        }
        #endregion

        #region Properties
        public int ID
        {
            get { return _iD; }
            set { _iD = value; }
        }

        public short DealerID
        {
            get { return _dealerID; }
            set { _dealerID = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Status
        {
            get { return _status; }
            set { _status = value; }
        }

        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }

        public string ZipCode
        {
            get { return _zipCode; }
            set { _zipCode = value; }
        }

        public string Phone
        {
            get { return _phone; }
            set { _phone = value; }
        }

        public string Fax
        {
            get { return _fax; }
            set { _fax = value; }
        }

        public string Website
        {
            get { return _website; }
            set { _website = value; }
        }

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        public string TypeBranch
        {
            get { return _typeBranch; }
            set { _typeBranch = value; }
        }

        public string DealerBranchCode
        {
            get { return _dealerBranchCode; }
            set { _dealerBranchCode = value; }
        }

        public string Term1
        {
            get { return _term1; }
            set { _term1 = value; }
        }

        public string Term2
        {
            get { return _term2; }
            set { _term2 = value; }
        }

        public string BranchAssignmentNo
        {
            get { return _branchAssignmentNo; }
            set { _branchAssignmentNo = value; }
        }

        public DateTime BranchAssignmentDate
        {
            get { return _branchAssigmentDate; }
            set { _branchAssigmentDate = value; }
        }

        public string SalesUnitFlag
        {
            get { return _salesUnitFlag; }
            set { _salesUnitFlag = value; }
        }

        public string ServiceFlag
        {
            get { return _serviceFlag; }
            set { _serviceFlag = value; }
        }

        public string SparepartFlag
        {
            get { return _sparepartFlag; }
            set { _sparepartFlag = value; }
        }

        public DealerDto Dealer
        {
            get { return _dealer; }
            set { _dealer = value; }
        }

        public CityDto City
        {
            get { return _city; }
            set { _city = value; }
        }

        public ProvinceDto Province
        {
            get { return _province; }
            set { _province = value; }
        }

        public Area1Dto Area1
        {
            get { return _area1; }
            set { _area1 = value; }
        }

        public Area2Dto Area2
        {
            get { return _area2; }
            set { _area2 = value; }
        }

        public MainAreaDto MainArea
        {
            get { return _mainArea; }
            set { _mainArea = value; }
        }

        public ArrayList ArrDBArea
        {
            get { return _arrDBArea; }
            set { _arrDBArea = value; }
        }
        #endregion
    }
}
