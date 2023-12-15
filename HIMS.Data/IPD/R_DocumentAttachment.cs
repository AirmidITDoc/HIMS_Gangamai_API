using HIMS.Common.Utility;
using HIMS.Data.Pharmacy;
using HIMS.Model.IPD;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.IPD
{
    public class R_DocumentAttachment : GenericRepository, I_DocumentAttachment
    {
        public R_DocumentAttachment(IUnitofWork unitofWork) : base(unitofWork)
        {
        }

        public List<DocumentAttachmentItem> Save(List<DocumentAttachmentItem> documentAttachment)
        {
            //var dic = documentAttachment.deleteDocument.ToDictionary();
            //ExecNonQueryProcWithOutSaveChanges("delete_MRD_AdmFile_1", dic);
            foreach (var a in documentAttachment)
            {
                var outputId = new SqlParameter
                {
                    SqlDbType = SqlDbType.BigInt,
                    ParameterName = "@Id",
                    Value = 0,
                    Direction = ParameterDirection.Output
                };
                var disc = a.ToDictionary();
                disc.Remove("DocFile");
                disc.Remove("Id");
                a.Id = ExecNonQueryProcWithOutSaveChanges("insert_T_MRD_AdmFile_1", disc, outputId).ToString();
            }
            _unitofWork.SaveChanges();
            return documentAttachment;
        }
        public List<DocumentAttachmentItem> GetFiles(int OPD_IPD_ID, int OPD_IPD_Type)
        {
            SqlParameter[] para = new SqlParameter[2];
            para[0] = new SqlParameter("@OPD_IPD_Id", OPD_IPD_ID);
            para[1] = new SqlParameter("@OPD_IPD_Type", OPD_IPD_Type);
            return GetList<DocumentAttachmentItem>("SELECT * FROM [T_MRD_AdmFile] WHERE OPD_IPD_ID=@OPD_IPD_ID AND OPD_IPD_Type=@OPD_IPD_Type", para);
        }
        public DocumentAttachmentItem GetFileById(int Id)
        {
            SqlParameter[] para = new SqlParameter[1];
            para[0] = new SqlParameter("@Id", Id);
            return GetList<DocumentAttachmentItem>("SELECT * FROM [T_MRD_AdmFile] WHERE Id=@Id", para).FirstOrDefault();
        }

        public void copyfunc(string f_sourcePath, string f_desPath)
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
