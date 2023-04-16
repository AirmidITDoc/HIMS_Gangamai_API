using HIMS.Model.Pathology;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Pathology
{
    public interface I_PathologyTestResultList
    {
        List<dynamic> GetPathologyTestResultList(PathologyTestResultList pathologyTestResultList);
    }
}
