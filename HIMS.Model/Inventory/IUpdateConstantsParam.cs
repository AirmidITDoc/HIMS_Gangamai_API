using System;

namespace HIMS.Model.Inventory
{
    public interface IUpdateConstantsParam
    {
        long ConstantId { get; set; }
        string ConstantType { get; set; }
        string CreatedBy { get; set; }
        DateTime CreatedOn { get; set; }
        long IsActive { get; set; }
        string Name { get; set; }
        long value { get; set; }
    }
}