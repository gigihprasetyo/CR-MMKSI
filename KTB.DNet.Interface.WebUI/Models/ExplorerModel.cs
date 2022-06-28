#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : ExplorerModel.cs class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 18/10/2018 17:05
//
// ===========================================================================	
#endregion

using System;
using System.Collections.Generic;

namespace KTB.DNet.Interface.WebUI.Models
{
    public class ExplorerModel
    {
        public DateTime Date { get; set; }
        public string GroupName { get; set; }
        public List<DirModel> DirModelList;
        public List<FileModel> FileModelList;

        public ExplorerModel()
        {
            DirModelList = new List<DirModel>();
            FileModelList = new List<FileModel>();
        }

        public ExplorerModel(List<DirModel> _dirModelList, List<FileModel> _fileModelList)
        {
            DirModelList = _dirModelList;
            FileModelList = _fileModelList;
        }
    }

    public class DirModel
    {
        public DateTime Date { get; set; }
        public string DirName { get; set; }
        public string When { get; set; }
        public bool IsHasSubDir { get; set; }
    }

    public class FileModel
    {
        public string FileName { get; set; }
    }


    //public class ExplorerGroupModel
    //{
    //    public string GroupName { get; set; }

    //    public List<ExplorerModel> GroupMember;

    //    //public ExplorerModel(List<DirModel> _dirModelList, List<FileModel> _fileModelList)
    //    //{
    //    //    dirModelList = _dirModelList;
    //    //    fileModelList = _fileModelList;
    //    //}
    //}
}