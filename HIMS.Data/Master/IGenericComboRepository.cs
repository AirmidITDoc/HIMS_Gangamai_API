using HIMS.Model.Master;
using System.Collections.Generic;

namespace HIMS.Data.Master
{
    public interface IGenericComboRepository
    {
        List<dynamic> Get(GenericCombo genericCombo);
    }
}