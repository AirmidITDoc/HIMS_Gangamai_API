using System;
using System.Collections.Generic;
using System.Text;
using HIMS.Data.Inventory;
using HIMS.Common.Utility;
using System.Data;
using System.Data.SqlClient;
using HIMS.Model.Users;
using HIMS.Model.Opd.OP;
using HIMS.Model.Master;
using HIMS.Model.Dashboard;
using System.Linq;
using System.Drawing;
using System.Reflection;

namespace HIMS.Data.Dashboard
{
    public class R_Dashboard : GenericRepository, I_Dashboard
    {
        public R_Dashboard(IUnitofWork unitofWork) : base(unitofWork)
        {

        }
        public PieChartModel GetPieChartData(string procName, Dictionary<string, object> entity)
        {
            PieChartModel obj = new PieChartModel() { colors = new List<string>() };
            SqlParameter[] para = new SqlParameter[entity.Count];
            int i = 0;
            foreach (var ent in entity)
            {
                para[i] = new SqlParameter(ent.Key, ent.Value.ToString());
                i++;
            }
            obj.data = GetListBySp<PieChartDto>(procName, para);
            foreach (var item in obj.data)
            {
                obj.colors.Add(item.color);
            }
            return obj;
        }
        public object GetBarChartData(string procName, Dictionary<string, object> entity)
        {
            BarChartModel obj = new BarChartModel() { colors = new List<string>(), data = new List<BarChartDto>() };
            SqlParameter[] para = new SqlParameter[entity.Count];
            int i = 0;
            foreach (var ent in entity)
            {
                para[i] = new SqlParameter(ent.Key, ent.Value.ToString());
                i++;
            }
            var Items = GetListBySp<BarChartItem>(procName, para);
            if (Items.Any(x => x.multiid > 0))
            {
                foreach (var item in Items.Select(x => new { x.multiid, x.multi }).Distinct())
                {
                    BarChartDto objDto = new BarChartDto() { name = item.multi, series = new List<BarChartItem>() };
                    var data = Items.Where(x => x.multiid == item.multiid);
                    foreach (var objItem in data)
                    {
                        objDto.series.Add(new BarChartItem() { name = objItem.name, value = objItem.value });
                    }
                    obj.data.Add(objDto);
                    Random rnd = new Random();
                    byte[] b = new Byte[3];
                    rnd.NextBytes(b);
                    Color color = Color.FromArgb(b[0], b[1], b[2]);
                    obj.colors.Add("#" + color.Name);
                }
                return obj;
            }
            else
            {
                PieChartModel model = new PieChartModel() { data = new List<PieChartDto>(), colors = new List<string>() };
                foreach (var item in Items)
                {
                    model.data.Add(new PieChartDto() { name = item.name, value = item.value.ToString() });
                    Random rnd = new Random();
                    byte[] b = new Byte[3];
                    rnd.NextBytes(b);
                    Color color = Color.FromArgb(b[0], b[1], b[2]);
                    model.colors.Add("#" + color.Name);
                }
                return model;
            }
        }
        public List<RoleModel> GetRoles(string RoleName)
        {
            if (string.IsNullOrEmpty(RoleName))
                RoleName = "";
            SqlParameter[] para = new SqlParameter[1];
            para[0] = new SqlParameter("@RoleName", RoleName);
            return GetList<RoleModel>("SELECT RoleId,RoleName FROM RoleMaster WHERE IsActive=1 AND RoleName LIKE '%'+@RoleName+'%'", para);
        }
        public List<MenuMaster> GetPermisison(int RoleId)
        {
            // return GetListBySp<MenuMaster>("SELECT M.* FROM MenuMaster M LEFT JOIN MenuMaster MM on M.Id=MM.UpId WHERE ISNULL(MM.Id,0)=0", new SqlParameter[0]);

            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@RoleId", RoleId);
            var lst = GetListBySp<MenuMaster>("GET_PERMISSION", sqlParameters);
            _unitofWork.SaveChanges();
            return lst;
        }
        public void SavePermission(List<PermissionModel> lst)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("RoleID", typeof(int));
            dt.Columns.Add("MenuId", typeof(int));
            dt.Columns.Add("IsView", typeof(int));
            dt.Columns.Add("IsAdd", typeof(int));
            dt.Columns.Add("IsEdit", typeof(int));
            dt.Columns.Add("IsDelete", typeof(int));
            foreach (var item in lst)
                dt.Rows.Add(item.RoleId, item.MenuId, item.IsView, item.IsAdd, item.IsEdit, item.IsDelete);
            SqlParameter[] para = new SqlParameter[1];
            para[0] = new SqlParameter("@tbl", SqlDbType.Structured)
            {
                Value = dt,
                TypeName = "dbo.Permission"
            };
            object dd = ExecuteObjectBySP("Insert_Permission", para);
        }

    }
}
