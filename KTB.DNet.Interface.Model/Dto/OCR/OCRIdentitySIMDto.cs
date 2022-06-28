#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : OCRIdentitySIMDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 19/10/2018 3:32
//
// ===========================================================================	
#endregion


namespace KTB.DNet.Interface.Model
{
    public class OCRIdentitySIMDto : DtoBase
    {
        public string UploadID { get; set; }

        public string UploadUri { get; set; }

        public string UploadFile { get; set; }

        public string FileName { get; set; }

        public string MediaType { get; set; }

        public string FileLength { get; set; }

        public object FormData { get; set; }

        public string Errors { get; set; }
    }

    public class OCRSIMUploadDto
    {
        public bool success { get; set; }

        public OCRIdentitySIMDto data { get; set; }

        public string message { get; set; }

        public int elapsed { get; set; }
    }

    public class OCRResultValueDto
    {
        public string Value { get; set; }
    }

    public class OCRSIMDataDto
    {
        public string Nama { get; set; }
        public string JenisKelamin { get; set; }
        public string Alamat { get; set; }
        public string TempatLahir { get; set; }
        public string TanggalLahir { get; set; }
        public string Tinggi { get; set; }
        public string Pekerjaan { get; set; }
        public string NomorSim { get; set; }
        public string Berlaku { get; set; }
        public string Polda { get; set; }
        public object FormData { get; set; }
        public int TotalChars { get; set; }
        public int ConfidenceChars { get; set; }
        public double ProcessingTime { get; set; }
        public string Errors { get; set; }
    }
}
