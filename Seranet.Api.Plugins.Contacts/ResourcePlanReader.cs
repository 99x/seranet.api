using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Seranet.Api.Plugins.Contacts
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

        public List<Contact> ReadFile()
        {
            ISheet sheet1 = Workbook.GetSheetAt(0);
            ISheet sheet2 = Workbook.GetSheetAt(1);

            List<Contact> contacts = new List<Contact>();
            LoadContacts(sheet1, 1, contacts);
            LoadContacts(sheet2, 1, contacts);

            return contacts;
        }

        private void LoadContacts(ISheet sheet, int rowStartIndex, List<Contact> contacts)
        {
            ICell organizationCell = sheet.GetRow(rowStartIndex).GetCell(0);
            ICell nameCell = sheet.GetRow(rowStartIndex).GetCell(1);
            ICell emailCell = sheet.GetRow(rowStartIndex).GetCell(2);
            ICell phoneCell = sheet.GetRow(rowStartIndex).GetCell(3);
            ICell skypeCell = sheet.GetRow(rowStartIndex).GetCell(4);

            while (GetCellValue(nameCell) != null)
            {
                Contact contact = new Contact();
                contact.Organization = GetCellValue(organizationCell);
                while (GetCellValue(nameCell) != null)
                {
                    ContactItem contactItem = new ContactItem();
                    contactItem.Name = GetCellValue(nameCell);
                    contactItem.Email = GetCellValue(emailCell);
                    contactItem.Phone = GetCellValue(phoneCell);
                    contactItem.Skype = GetCellValue(skypeCell);
                    contact.ContactList.Add(contactItem);

                    rowStartIndex++;

                    IRow rowObj = sheet.GetRow(rowStartIndex);
                    if (rowObj == null)
                    {
                        nameCell = null;
                        break;
                    }


                    organizationCell = sheet.GetRow(rowStartIndex).GetCell(0);
                    nameCell = sheet.GetRow(rowStartIndex).GetCell(1);
                    emailCell = sheet.GetRow(rowStartIndex).GetCell(2);
                    phoneCell = sheet.GetRow(rowStartIndex).GetCell(3);
                    skypeCell = sheet.GetRow(rowStartIndex).GetCell(4);

                    if (GetCellValue(organizationCell) != null)
                    {
                        break;
                    }
                }
                contacts.Add(contact);
            }
        }

        private string GetCellValue(ICell cell)
        {
            if (cell != null)
            {
                string val;
                if (cell.CellType == CellType.NUMERIC)
                {
                    val = (cell.NumericCellValue).ToString();
                    return val;
                }
                val = cell.StringCellValue;
                if (!"".Equals(val.Trim()))
                {
                    return val.Trim();
                }

            }
            return null;
        }

    }
}
