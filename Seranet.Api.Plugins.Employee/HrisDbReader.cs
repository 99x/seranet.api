using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Seranet.Api.Plugins.Employee
{
    class HrisDbReader
    {
        public List<Employee> Read(string connectionString)
        {
            List<Employee> employees = new List<Employee>();

            string queryString = SqlResource.emp_details;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Employee emp = ReadBasicData(reader);
                    emp.Addresses.Add(ReadResidentialAddress(reader));
                    emp.Addresses.Add(ReadPermanentAddress(reader));

                    employees.Add(emp);
                }
                reader.Close();
            }

            return employees;
        }

        private Employee ReadBasicData(SqlDataReader reader)
        {
            Employee emp = new Employee();
            emp.FirstName = reader["FirstName"].ToString();
            emp.LastName = reader["Surname"].ToString();
            emp.NIC = reader["NICNumber"].ToString();
            emp.MobileNumber = reader["hrMobileNo"].ToString();
            emp.TwitterId = reader["Twitter"].ToString();
            emp.FacebookId = reader["Facebook"].ToString();
            emp.LinkedInId = reader["LinkedIn"].ToString();

            string email = reader["Email"].ToString();
            if (email.IndexOf("@") > 0)
            {
                string[] emailParts = email.Split('@');
                emp.Id = emailParts[0].Trim().ToLower();
            }

            return emp;
        }

        private Address ReadPermanentAddress(SqlDataReader reader)
        {
            Address address = new Address(AddressType.Permanent);
            address.Number = reader["PAddress1"].ToString();
            address.Street = reader["PAddress2"].ToString();
            address.City = reader["PAddress3"].ToString();
            address.District = reader["PDistrict"].ToString();
            address.Country = reader["PCountry"].ToString();
            address.Phone = reader["PTelephone"].ToString();

            string coordinateStr = reader["PCoordinates"].ToString();
            if (coordinateStr.IndexOf(",") > 0)
            {
                string[] coordinateParts = coordinateStr.Split(',');
                address.Coordinate.Latitude = coordinateParts[0].Trim();
                address.Coordinate.Longitude = coordinateParts[1].Trim();
            }

            return address;
        }

        private Address ReadResidentialAddress(SqlDataReader reader)
        {
            Address address = new Address(AddressType.Residential);
            address.Number = reader["RAddress1"].ToString();
            address.Street = reader["RAddress2"].ToString();
            address.City = reader["RAddress3"].ToString();
            address.District = reader["RDistrict"].ToString();
            address.Country = reader["RCountry"].ToString();
            address.Phone = reader["RTelephone"].ToString();

            string coordinateStr = reader["RCoordinates"].ToString();
            if (coordinateStr.IndexOf(",") > 0)
            {
                string[] coordinateParts = coordinateStr.Split(',');
                address.Coordinate.Latitude = coordinateParts[0].Trim();
                address.Coordinate.Longitude = coordinateParts[1].Trim();
            }

            return address;
        }
    }
}
