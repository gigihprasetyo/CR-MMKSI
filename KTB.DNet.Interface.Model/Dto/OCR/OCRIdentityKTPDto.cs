#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : OCRIdentityKTPDto  class
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
    public class OCRIdentityKTPDto : DtoBase
    {
        public string UploadID { get; set; }

        public string UploadUri { get; set; }

        public string UploadFile { get; set; }

        public string FileName { get; set; }

        public string MediaType { get; set; }

        public string FileLength { get; set; }

        public object FormData { get; set; }

        public string Errors { get; set; }

        public string message { get; set; }

        public int elapsed { get; set; }
    }

    public class OCRKTPUploadDto
    {
        public bool success { get; set; }

        public OCRIdentityKTPDto data { get; set; }
    }

    public class OCRKTPDataDto
    {
        public string NIK { get; set; }
        public string Propinsi { get; set; }
        public string Kotakab { get; set; }
        public string Kecamatan { get; set; }
        public string Nama { get; set; }
        public string TempatLahir { get; set; }
        public string TanggalLahir { get; set; }
        public string JenisKelamin { get; set; }
        public string Alamat { get; set; }
        public string RtRw { get; set; }
        public string Kelurahan { get; set; }
        public string Agama { get; set; }
        public string StatusPerkawinan { get; set; }
        public string Pekerjaan { get; set; }
        public string Kewarganegaraan { get; set; }
        public string BerlakuHingga { get; set; }
        public object FormData { get; set; }
        public int TotalChars { get; set; }
        public int ConfidenceChars { get; set; }
        public double ProcessingTime { get; set; }
        public string Errors { get; set; }
    }
}
    