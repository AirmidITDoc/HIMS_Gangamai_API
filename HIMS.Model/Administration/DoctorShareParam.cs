using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Administration
{
    public class DoctorShareParam
    {
        public InsertDoctorShareMasterParam InsertDoctorShareMasterParam { get; set; }
        public UpdateDoctorShareMasterParam UpdateDoctorShareMasterParam { get; set; }
    }
    public class InsertDoctorShareMasterParam
    {
        public long DoctorShareId { get; set; }
        public long DoctorID { get; set; }
        public long ServiceId { get; set; }
        public float servicePercentage { get; set; }
        public long DocShrType { get; set; }
        public string DocShrTypeS { get; set; }
        public float ServiceAmount { get; set; }
        public long ClassId { get; set; }
        public long ShrTypeSerOrGrp { get; set; }
        public long Op_IP_Type { get; set; }

    }
    public class UpdateDoctorShareMasterParam
    {
        public long DoctorShareId { get; set; }
        public long DoctorID { get; set; }
        public long ServiceId { get; set; }
        public float servicePercentage { get; set; }
        public long DocShrType { get; set; }
        public string DocShrTypeS { get; set; }
        public float ServiceAmount { get; set; }
        public long ClassId { get; set; }
        public long ShrTypeSerOrGrp { get; set; }
        public long Op_IP_Type { get; set; }
    }
}
