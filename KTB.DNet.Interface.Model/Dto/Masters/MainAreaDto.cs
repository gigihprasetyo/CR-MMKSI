#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : MainAreaDto  class
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
#endregion

namespace KTB.DNet.Interface.Model
{
    public class MainAreaDto
    {
        #region Private Valiables
        private int _id;
        private string _areaCode = String.Empty;
        private string _description = String.Empty;
        private string _pIC = String.Empty;
        #endregion

        #region Constructors
        public MainAreaDto() { }

        public MainAreaDto(int id)
        {
            _id = id;
        }

        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        public string AreaCode
        {
            get { return _areaCode; }
            set { _areaCode = value; }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public string PIC
        {
            get { return _pIC; }
            set { _pIC = value; }
        }
        #endregion
    }
}
