using HIMS.Model.Master;
using System.Collections.Generic;

namespace HIMS.Data.Master
{
    public interface ICashCounterRepository
    {
        List<dynamic> Get(string cashCounterName);
        bool Save(CashCounter cashCounter);
    }
}