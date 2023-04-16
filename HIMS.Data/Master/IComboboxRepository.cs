using HIMS.Model.Master;

namespace HIMS.Data.Master
{
    public interface IComboboxRepository
    {
        void FillComboGroup(string procedureName, ref ComboBox cmbCombo);
        void FillComboGroupDefault(string procedureName, ref ComboBox cmbCombo);
        void FillComboGroupDefaultOne(string procedureName, ref ComboBox cmbCombo);
        void FillMasterComboConditionalDT(GenericCombo generic, ref ComboBox cmbCombo);
        void FillMasterComboConditionalWithOutDefaultValue(GenericCombo generic, ref ComboBox cmbCombo);
        void FillMasterCombo(string procedureName, ref ComboBox cmbCombo);
    }
}