using HIMS.Common.Utility;
using HIMS.Model.IPD;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;

namespace HIMS.Data.IPD
{
    public class R_DocumentAttachment : GenericRepository, I_DocumentAttachment
    {
        public R_DocumentAttachment(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection is open when you inject unitofwork
        }

        public bool Save(DocumentAttachment documentAttachment)
        {
            var dic = documentAttachment.deleteDocument.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("delete_MRD_AdmFile_1", dic);

         

            foreach (var a in documentAttachment.InsertdocumentAttach)
            {
                var disc = documentAttachment.InsertdocumentAttach.ToDictionary();
                //string UserFileName = "D:\VBD";
                //string filename = "c:";

                //File.WriteAllText(tmpfile, JsonConvert.SerializeObject(current), Encoding.UTF8);

                Dictionary<string, Object> PathParams = new Dictionary<string, object>();

                PathParams.Add("OPD_IPD_ID", a.OPD_IPD_ID);
                PathParams.Add("OPD_IPD_Type", a.OPD_IPD_Type);
                PathParams.Add("FileName", a.FileName);
                PathParams.Add("FilePath", File.ReadAllText(a.FilePath,Encoding.UTF8));

                copyfunc(a.FilePath, a.FilePathLocation);

                PathParams.Add("FilePathLocation", a.FilePathLocation);

                PathParams.Add("CategoryId", a.CategoryId);
                PathParams.Add("CategoryName", a.CategoryName);


                ExecNonQueryProcWithOutSaveChanges("insert_T_MRD_AdmFile_1", PathParams);

                //var disc1 = a.ToDictionary();
                //disc1["DRNo"] = DRNo;
                //ExecNonQueryProcWithOutSaveChanges("insert_T_DRBillDet_1", disc1);
            }

            _unitofWork.SaveChanges();

            return true;
        }

        public void copyfunc(string f_sourcePath, string f_desPath )
        {
            string sourcepath = f_sourcePath;
            string DestPath = f_desPath;

            if (!System.IO.Directory.Exists(DestPath))
                System.IO.Directory.CreateDirectory(DestPath);
            var file = new System.IO.FileInfo(sourcepath);

            file.CopyTo(System.IO.Path.Combine(DestPath, file.Name), true);
        }


    }
}
