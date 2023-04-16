using HIMS.Model.Master;
using System;
using System.Data;
using System.Data.SqlClient;

namespace HIMS.Data.Master
{
    public class ComboboxRepository : IComboboxRepository
    {
        private readonly IUnitofWork _unitofWork;
        private readonly SqlCommand command;

        public ComboboxRepository(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
            command = _unitofWork.CreateCommand();
        }

        public void FillComboGroup(string procedureName, ref ComboBox cmbCombo)
        {
            command.CommandText = procedureName;
            SqlDataReader dr;
            dr = command.ExecuteReader();
            while (dr.Read())
            {
                var ocls = new OptionDetails(dr[1].ToString())
                {
                    iNameId = Convert.ToInt32(dr[0].ToString())
                };
                cmbCombo.Items.Add(ocls);
            }
            //https://docs.microsoft.com/en-us/dotnet/framework/data/adonet/retrieving-data-using-a-datareader#closing-the-datareader
            dr.Close();
        }

        public void FillComboGroupDefault(string procedureName, ref ComboBox cmbCombo)
        {
            command.CommandText = procedureName;

            SqlDataReader dr;
            dr = command.ExecuteReader();

            var DefaultValue = new OptionDetails("-Select-");
            cmbCombo.Items.Add(DefaultValue);

            while (dr.Read())
            {
                var ocls = new OptionDetails(dr[1].ToString())
                {
                    iNameId = Convert.ToInt32(dr[0].ToString())
                };
                cmbCombo.Items.Add(ocls);
            }
            cmbCombo.SelectedIndex = 0;
            dr.Close();
        }

        public void FillComboGroupDefaultOne(string procedureName, ref ComboBox cmbCombo)
        {
            command.CommandText = procedureName;

            SqlDataReader dr;
            dr = command.ExecuteReader();
            var DefaultValue = new OptionDetails("<Select>");
            cmbCombo.Items.Add(DefaultValue);
            while (dr.Read())
            {
                var ocls = new OptionDetails(dr[1].ToString())
                {
                    iNameId = Convert.ToInt32(dr[0].ToString())
                };
                cmbCombo.Items.Add(ocls);
            }
            cmbCombo.SelectedIndex = 1;
            dr.Close();
        }

        public void FillMasterComboConditionalDT(GenericCombo genericCombo, ref ComboBox cmbCombo)
        {
            command.CommandText = genericCombo.ProcedureName;
            command.Parameters.AddWithValue($"@{genericCombo.ParamName}", genericCombo.ParamValue);

            var DA = new SqlDataAdapter
            {
                SelectCommand = command
            };
            DA.SelectCommand.ExecuteNonQuery();
            var DTable = new DataTable();
            DA.Fill(DTable);
            DataTable objTmpDT;
            objTmpDT = new DataTable();
            objTmpDT.Columns.Add(DTable.Columns[0].ToString());
            objTmpDT.Columns.Add(DTable.Columns[1].ToString());
            // 
            if (DTable.Rows.Count > 0)
            {
                DataRow objRW;
                for (short i = 0; i <= DTable.Rows.Count - 1; i++)
                {
                    if (i == 0)
                    {
                        objRW = objTmpDT.NewRow();
                        objRW[0] = "-1";
                        objRW[1] = " <Select>";
                        objTmpDT.Rows.Add(objRW);
                    }
                    objRW = objTmpDT.NewRow();
                    objRW[0] = DTable.Rows[i][0];
                    objRW[1] = DTable.Rows[i][1];
                    objTmpDT.Rows.Add(objRW);
                }
                {
                    var withBlock = cmbCombo;
                    withBlock.DataSource = null;
                    withBlock.Items.Clear();
                    withBlock.DisplayMember = objTmpDT.Columns[1].ToString();
                    withBlock.ValueMember = objTmpDT.Columns[0].ToString();
                    // If IsDefaultValueNeeded Then
                    // .Items.Add(New ListViewItem("Select", -1))
                    // End If
                    withBlock.DataSource = objTmpDT;
                    // If cmb.Items.Count > 0 Then
                    // .SelectedIndex = 0
                    // End If
                    // .SelectedIndex = 0
                    withBlock.SelectedValue = -1;
                }
            }
            else
            {
                var withBlock = cmbCombo;
                withBlock.DataSource = null;
                withBlock.Items.Clear();
            }

        }

        public void FillMasterComboConditionalWithOutDefaultValue(GenericCombo genericCombo, ref ComboBox cmbCombo)
        {
            command.CommandText = genericCombo.ProcedureName;
            command.Parameters.AddWithValue($"@{genericCombo.ProcedureName}", genericCombo.ParamValue);

            SqlDataAdapter DA = new SqlDataAdapter
            {
                SelectCommand = command
            };
            DA.SelectCommand.ExecuteNonQuery();
            DataTable DTable = new DataTable();
            DA.Fill(DTable);
            DataTable objTmpDT;
            objTmpDT = new DataTable();
            objTmpDT.Columns.Add(DTable.Columns[0].ToString());
            objTmpDT.Columns.Add(DTable.Columns[1].ToString());
            // 
            if (DTable.Rows.Count > 0)
            {
                DataRow objRW;
                for (Int16 i = 0; i <= DTable.Rows.Count - 1; i++)
                {
                    objRW = objTmpDT.NewRow();
                    objRW[0] = DTable.Rows[i][0];
                    objRW[1] = DTable.Rows[i][1];
                    objTmpDT.Rows.Add(objRW);
                }
                {
                    var withBlock = cmbCombo;
                    withBlock.DataSource = null;
                    withBlock.Items.Clear();
                    withBlock.DisplayMember = objTmpDT.Columns[1].ToString();
                    withBlock.ValueMember = objTmpDT.Columns[0].ToString();
                    // If IsDefaultValueNeeded Then
                    // .Items.Add(New ListViewItem("Select", -1))
                    // End If
                    withBlock.DataSource = objTmpDT;
                }
            }
            else
            {
                var withBlock = cmbCombo;
                withBlock.DataSource = null;
                withBlock.Items.Clear();
            }
        }

        public void FillMasterCombo(string procedureName, ref ComboBox cmbCombo)
        {
            command.CommandText = procedureName;
            SqlDataAdapter DA = new SqlDataAdapter
            {
                SelectCommand = command
            };
            DA.SelectCommand.ExecuteNonQuery();
            var DTable = new DataTable();
            DA.Fill(DTable);
            DataTable objTmpDT;

            objTmpDT = new DataTable();
            objTmpDT.Columns.Add(DTable.Columns[0].ToString());
            objTmpDT.Columns.Add(DTable.Columns[1].ToString());
            // 
            if (DTable.Rows.Count > 0)
            {
                DataRow objRW;
                for (short i = 0; i <= DTable.Rows.Count - 1; i++)
                {
                    if (i == 0)
                    {
                        objRW = objTmpDT.NewRow();
                        objRW[0] = "-1";
                        objRW[1] = " <Select>";
                        objTmpDT.Rows.Add(objRW);
                    }
                    objRW = objTmpDT.NewRow();
                    objRW[0] = DTable.Rows[i][0];
                    objRW[1] = DTable.Rows[i][1];
                    objTmpDT.Rows.Add(objRW);
                }
                {
                    var withBlock = cmbCombo;
                    withBlock.DataSource = null;
                    withBlock.Items.Clear();
                    withBlock.DisplayMember = objTmpDT.Columns[1].ToString();
                    withBlock.ValueMember = objTmpDT.Columns[0].ToString();
                    // If IsDefaultValueNeeded Then
                    // .Items.Add(New ListViewItem("Select", -1))
                    // End If
                    withBlock.DataSource = objTmpDT;
                    // If cmb.Items.Count > 0 Then
                    // .SelectedIndex = 0
                    // End If
                    // .SelectedIndex = 0
                    withBlock.SelectedValue = -1;
                }
            }
            else
            {
                var withBlock = cmbCombo;
                withBlock.DataSource = null;
                withBlock.Items.Clear();
            }

        }
    }
}
