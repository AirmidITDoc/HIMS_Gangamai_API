using HIMS.Common.Utility;
using HIMS.Model.IPD;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Threading.Tasks;

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
                var disc = a.ToDictionary();
                ExecNonQueryProcWithOutSaveChanges("insert_T_MRD_AdmFile_1", disc);
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
        //public async Task<string> WriteFile(IFileName file)
        //{
        //    string filename = "";
        //    try
        //    {
        //        var extension = "." + file.filename.Split('.')[file.filename.Split('.').Length - 1];
        //        filename = DateTime.Now.Ticks.ToString() + extension;

        //        var filepath = Path.Combine(Directory.GetCurrentDirectory(), "upload");

        //        if (!Directory.Exists(filepath))
        //        {
        //            Directory.CreateDirectory(filepath);
        //        }
        //        var exactpath = Path.Combine(Directory.GetCurrentDirectory(), "upload", filename);
        //        using (var stream = new FileStream(exactpath, FileMode.Create))
        //        {
        //            await file.CopytoAsync(stream);
        //        }

        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    return filename;

        //}

    }
}
