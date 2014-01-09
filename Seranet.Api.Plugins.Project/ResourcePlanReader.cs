using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Seranet.Api.Plugins.Project
{
    class ResourcePlanReader
    {
        public XSSFWorkbook Workbook { get; set; }

        public ResourcePlanReader(string path)
        {
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            this.Workbook = new XSSFWorkbook(fs);
            fs.Close();
        }

        public List<Project> ReadFile()
        {
            ISheet sheet = Workbook.GetSheetAt(0);

            List<Project> projects = new List<Project>();
            LoadProjects(sheet, 0, projects);
            LoadProjects(sheet, 24, projects);

            return projects;
        }

        private void LoadProjects(ISheet sheet, int rowStartIndex, List<Project> projects)
        {
            int maxColCount = 40;
            int rowheight = 20;

            for (int col = 1; col < maxColCount; col = col + 2)
            {

                ICell repCell = sheet.GetRow(rowStartIndex + 1).GetCell(col);
                ICell nameCell = sheet.GetRow(rowStartIndex + 2).GetCell(col);
                ICell idCell = sheet.GetRow(rowStartIndex).GetCell(col);

                if (GetCellValue(repCell) == null || GetCellValue(nameCell) == null || GetCellValue(idCell) == null)
                {
                    continue;
                }

                Project project = new Project();
                project.Id = GetCellValue(idCell);
                project.Name = GetCellValue(nameCell);
                project.Rep = Regex.Replace(GetCellValue(repCell), @"\s+", "").ToLower();

                for (int row = rowStartIndex + 3; row < rowheight + rowStartIndex; row++)
                {
                    IRow rowObj = sheet.GetRow(row);
                    if (rowObj == null)
                    {
                        continue;
                    }
                    ICell cell = rowObj.GetCell(col);
                    string value = GetCellValue(cell);
                    if (value != null)
                    {
                        project.Members.Add(Regex.Replace(value, @"\s+", "").ToLower());
                    }

                }
                projects.Add(project);

            }
        }

        private string GetCellValue(ICell cell)
        {
            if (cell != null)
            {
                string val = cell.StringCellValue;
                if (!"".Equals(val.Trim()))
                {
                    return val.Trim();
                }

            }
            return null;
        }

    }
}
