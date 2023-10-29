using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using System.IO;

namespace HIMS.API.Controllers.Transaction
{

    [ApiController]
    [Route("api/[controller]")]
    public class NursingController : Controller
    {
        /* public IActionResult Index()
         {
             return View();
         }*/
        //[HttpPost("Uploadfile")]
        //public async Task<IActionResult> Uploadfile()
        //{
        //    //var result = await WriteFile(file);

        //    return Ok("");
        //}

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

    //public interface IFileName
    //{
    //    string filename { get; set; }

    //    Task CopytoAsync(FileStream stream);
    //}
}

