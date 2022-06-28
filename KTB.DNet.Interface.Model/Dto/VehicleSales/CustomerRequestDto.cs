#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : CustomerRequestDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 19/10/2018 3:32
//
// ===========================================================================	
#endregion

using System;
using System.Collections.Generic;

namespace KTB.DNet.Interface.Model
{
    public class CustomerRequestDto : DtoBase
    {
        #region "Private Variables"
        private int _iD;
        private string _requestNo = String.Empty;
        private string _refRequestNo = String.Empty;
        private string _requestType = String.Empty;
        private int _requestUserID;
        private DateTime _requestDate = ((DateTime)(System.Data.SqlTypes.SqlDateTime.MinValue.Value));
        private int _status;
        private string _processUserID = String.Empty;
        private DateTime _processDate = ((DateTime)(System.Data.SqlTypes.SqlDateTime.MinValue.Value));
        private string _customerCode = String.Empty;
        private string _reffCode = String.Empty;
        private string _name1 = String.Empty;
        private string _name2 = String.Empty;
        private string _name3 = String.Empty;
        private string _alamat = String.Empty;
        private string _kelurahan = String.Empty;
        private string _kecamatan = String.Empty;
        private string _postalCode = String.Empty;
        private string _preArea = String.Empty;
        private string _printRegion = String.Empty;
        private string _phoneNo = String.Empty;
        private string _email = String.Empty;
        private string _attachment = String.Empty;
        private short _status1;
        private short _tipePerusahaan;

        // Private _companyGroupId As Short
        private short _mCPStatus;
        private short _lKPPStatus;
        private short _rowStatus;
        private int _cityID;

        private DealerDto _dealer;
        private SPKHeaderDto _sPKHeader;

        //private List<CustomerRequestProfileDto> _customerRequestProfiles = new List<CustomerRequestProfileDto>();
        //private List<CustomerStatusHistoryDto> _customerStatusHistory = new List<CustomerStatusHistoryDto>();

        private List<SPKHeaderDto> _sPKHeaders = new List<SPKHeaderDto>();

        #endregion

        #region "Public Properties"
        public int ID
        {
            get { return _iD; }
            set { _iD = value; }
        }

        public string RequestNo
        {
            get { return _requestNo; }
            set { _requestNo = value; }
        }

        public string RefRequestNo
        {
            get { return _refRequestNo; }
            set { _refRequestNo = value; }
        }

        public string RequestType
        {
            get { return _requestType; }
            set { _requestType = value; }
        }

        public int RequestUserID
        {
            get { return _requestUserID; }
            set { _requestUserID = value; }
        }

        public DateTime RequestDate
        {
            get { return _requestDate; }
            set { _requestDate = new DateTime(value.Year, value.Month, value.Day); }
        }

        public int Status
        {
            get { return _status; }
            set { _status = value; }
        }

        public string ProcessUserID
        {
            get { return _processUserID; }
            set { _processUserID = value; }
        }

        public DateTime ProcessDate
        {
            get { return _processDate; }
            set { _processDate = new DateTime(value.Year, value.Month, value.Day); }
        }

        public string CustomerCode
        {
            get { return _customerCode; }
            set { _customerCode = value; }
        }

        public string ReffCode
        {
            get { return _reffCode; }
            set { _reffCode = value; }
        }

        public string Name1
        {
            get { return _name1; }
            set { _name1 = value; }
        }

        public string Name2
        {
            get { return _name2; }
            set { _name2 = value; }
        }

        public string Name3
        {
            get { return _name3; }
            set { _name3 = value; }
        }

        public string Alamat
        {
            get { return _alamat; }
            set { _alamat = value; }
        }

        public string Kelurahan
        {
            get { return _kelurahan; }
            set { _kelurahan = value; }
        }

        public string Kecamatan
        {
            get { return _kecamatan; }
            set { _kecamatan = value; }
        }

        public string PostalCode
        {
            get { return _postalCode; }
            set { _postalCode = value; }
        }

        public string PreArea
        {
            get { return _preArea; }
            set { _preArea = value; }
        }

        public string PrintRegion
        {
            get { return _printRegion; }
            set { _printRegion = value; }
        }

        public string PhoneNo
        {
            get { return _phoneNo; }
            set { _phoneNo = value; }
        }

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        public string Attachment
        {
            get { return _attachment; }
            set { _attachment = value; }
        }

        public short Status1
        {
            get { return _status1; }
            set { _status1 = value; }
        }

        public short TipePerusahaan
        {
            get { return _tipePerusahaan; }
            set { _tipePerusahaan = value; }
        }

        public short MCPStatus
        {
            get { return _mCPStatus; }
            set { _mCPStatus = value; }
        }

        public short LKPPStatus
        {
            get { return _lKPPStatus; }
            set { _lKPPStatus = value; }
        }

        public new short RowStatus
        {
            get { return _rowStatus; }
            set { _rowStatus = value; }
        }

        public int CityID
        {
            get { return _cityID; }
            set { _cityID = value; }
        }

        public DealerDto Dealer
        {
            get { return _dealer; }
            set { _dealer = value; }
        }

        public SPKHeaderDto SPKHeader
        {
            get { return _sPKHeader; }
            set { _sPKHeader = value; }
        }

        //private List<CustomerRequestProfileDto> CustomerRequestProfiles
        //{
        //    get { return _customerRequestProfiles; }
        //    set { _customerRequestProfiles = value; }
        //}

        //private List<CustomerStatusHistoryDto> CustomerStatusHistory
        //{
        //    get { return _customerStatusHistory; }
        //    set { _customerStatusHistory = value; }
        //}

        public List<SPKHeaderDto> SPKHeaders
        {
            get { return _sPKHeaders; }
            set { _sPKHeaders = value; }
        }

        #endregion

    }
}