#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : Request  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 19/10/2018 3:32
//
// ===========================================================================	
#endregion

namespace KTB.DNET.Interface.Model
{
    public class Request<TModel>
    {
        public TModel Model { get; set; }
    }
}
