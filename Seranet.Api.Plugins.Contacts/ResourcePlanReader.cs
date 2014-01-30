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
            ICell assignmentCell = sheet.GetRow(rowStartIndex).GetCell(1);
            ICell nameCell = sheet.GetRow(rowStartIndex).GetCell(2);
            ICell emailCell = sheet.GetRow(rowStartIndex).GetCell(3);
            ICell phoneCell = sheet.GetRow(rowStartIndex).GetCell(4);
            ICell skypeCell = sheet.GetRow(rowStartIndex).GetCell(5);

            while (GetCellValue(organizationCell) != null)
            {
                Contact contact = new Contact();
                contact.Organization = GetCellValue(organizationCell);

                while (GetCellValue(assignmentCell) != null)
                {
                    ContactAssignment contactAssignment= new ContactAssignment();
                    contactAssignment.Assignment = GetCellValue(assignmentCell);

                    while (GetCellValue(nameCell) != null)
                    {
                        ContactItem contactItem = new ContactItem();
                        contactItem.Name = GetCellValue(nameCell);
                        contactItem.Email = GetCellValue(emailCell);
                        contactItem.Phone = GetCellValue(phoneCell);
                        contactItem.Skype = GetCellValue(skypeCell);
                        contactAssignment.ContactList.Add(contactItem);

                        rowStartIndex++;

                        IRow rowObj = sheet.GetRow(rowStartIndex);
                        if (rowObj == null)
                        {
                            nameCell = null;
                            break;
                        }

                        organizationCell = sheet.GetRow(rowStartIndex).GetCell(0);
                        assignmentCell = sheet.GetRow(rowStartIndex).GetCell(1);
                        nameCell = sheet.GetRow(rowStartIndex).GetCell(2);
                        emailCell = sheet.GetRow(rowStartIndex).GetCell(3);
                        phoneCell = sheet.GetRow(rowStartIndex).GetCell(4);
                        skypeCell = sheet.GetRow(rowStartIndex).GetCell(5);

                        if (GetCellValue(assignmentCell) != null)
                        {
                            break;
                        }
                    }
                    contact.ContactAssignment.Add(contactAssignment);
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
