using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using api_azure_test.Common;

namespace api_azure_test.Models
{
    public class EmployeeDataAccessLayer
    {
        string connectionString = GeneralFunctions.GetConnectionString();
		//@"]ConnectionString[" in case this need to be set;

        //To View all employees details
        public IEnumerable<Employee> GetAllEmployees()
        {
            try
            {
                List<Employee> lstemployee = new List<Employee>();

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_Employee_Get", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        Employee employee = new Employee();

                        employee.ID = Convert.ToInt32(rdr["ID"]);
                        employee.Name  = (Convert.IsDBNull(rdr["Name"])) ? "" : Convert.ToString(rdr["Name"]);
                        employee.Address  = (Convert.IsDBNull(rdr["Address"])) ? "" : Convert.ToString(rdr["Address"]);
                        employee.Role  = (Convert.IsDBNull(rdr["Role"])) ? "" : Convert.ToString(rdr["Role"]);
                        employee.Department  = (Convert.IsDBNull(rdr["Department"])) ? "" : Convert.ToString(rdr["Department"]);
                        employee.SkillSets  = (Convert.IsDBNull(rdr["SkillSets"])) ? "" : Convert.ToString(rdr["SkillSets"]);
                        employee.Date_of_Birth  = (Convert.IsDBNull(rdr["Date of Birth"])) ? "" : Convert.ToString(rdr["Date of Birth"]);
                        employee.Date_of_Joining  = (Convert.IsDBNull(rdr["Date of Joining"])) ? "" : Convert.ToString(rdr["Date of Joining"]);
                        employee.IsActive  = (Convert.IsDBNull(rdr["IsActive"])) ? false : Convert.ToBoolean(rdr["IsActive"]);

                        lstemployee.Add(employee);
                    }
                    con.Close();
                }
                return lstemployee;
            }
            catch (Exception ex)
            {
				string err = ex.Message;
				string stackTrace = ex.StackTrace;
                throw;
            }
        }

        //To Add new employee record 
        public object AddEmployee(Employee employee)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_Employee_Save", con);
                    cmd.CommandType = CommandType.StoredProcedure;
					cmd.Parameters.AddWithValue("@ID", 0);
                    cmd.Parameters.AddWithValue("@Name", employee.Name);
                    cmd.Parameters.AddWithValue("@Address", employee.Address);
                    cmd.Parameters.AddWithValue("@Role", employee.Role);
                    cmd.Parameters.AddWithValue("@Department", employee.Department);
                    cmd.Parameters.AddWithValue("@SkillSets", employee.SkillSets);
                    cmd.Parameters.AddWithValue("@Date_of_Birth", employee.Date_of_Birth);
                    cmd.Parameters.AddWithValue("@Date_of_Joining", employee.Date_of_Joining);
                    cmd.Parameters.AddWithValue("@IsActive", employee.IsActive);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                return new ResultVM
                { Status = "Success", Message = "SuccessFully saved." };
            }
			catch (Exception ex)
            {
                return new ResultVM
                { Status = "Error", Message = ex.Message.ToString() };
            }
        }

        //To Update the records_of_a particluar employee
        public object UpdateEmployee(Employee employee)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_Employee_Save", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ID", employee.ID);
                    cmd.Parameters.AddWithValue("@Name", employee.Name);
                    cmd.Parameters.AddWithValue("@Address", employee.Address);
                    cmd.Parameters.AddWithValue("@Role", employee.Role);
                    cmd.Parameters.AddWithValue("@Department", employee.Department);
                    cmd.Parameters.AddWithValue("@SkillSets", employee.SkillSets);
                    cmd.Parameters.AddWithValue("@Date_of_Birth", employee.Date_of_Birth);
                    cmd.Parameters.AddWithValue("@Date_of_Joining", employee.Date_of_Joining);
                    cmd.Parameters.AddWithValue("@IsActive", employee.IsActive);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                return new ResultVM
                { Status = "Success", Message = "SuccessFully Updated." };
            }
			catch (Exception ex)
            {
                return new ResultVM
                { Status = "Error", Message = ex.Message.ToString() };
            }
        }

        //Get the details_of_a particular employee
        public Employee GetEmployeeData(int id)
        {
            try
            {
                Employee employee = new Employee();

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string sqlQuery = "SELECT * FROM Employee WHERE ID = " + id;
                    SqlCommand cmd = new SqlCommand(sqlQuery, con);

                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        employee.ID = Convert.ToInt32(rdr["ID"]);
                        employee.Name  = (Convert.IsDBNull(rdr["Name"])) ? "" : Convert.ToString(rdr["Name"]);
                        employee.Address  = (Convert.IsDBNull(rdr["Address"])) ? "" : Convert.ToString(rdr["Address"]);
                        employee.Role  = (Convert.IsDBNull(rdr["Role"])) ? "" : Convert.ToString(rdr["Role"]);
                        employee.Department  = (Convert.IsDBNull(rdr["Department"])) ? "" : Convert.ToString(rdr["Department"]);
                        employee.SkillSets  = (Convert.IsDBNull(rdr["SkillSets"])) ? "" : Convert.ToString(rdr["SkillSets"]);
                        employee.Date_of_Birth  = (Convert.IsDBNull(rdr["Date of Birth"])) ? "" : Convert.ToString(rdr["Date of Birth"]);
                        employee.Date_of_Joining  = (Convert.IsDBNull(rdr["Date of Joining"])) ? "" : Convert.ToString(rdr["Date of Joining"]);
                        if (Convert.IsDBNull(rdr["IsActive"]))
                        {
                            //employee.IsActive = "false";
                            employee.IsActive = false;
                        }
                        else
                        {
                            if (rdr["IsActive"].ToString() == "True")
                            {
                                //employee.IsActive = "true";
                                employee.IsActive = true;
                            }
                            else
                            {
                                employee.IsActive = false;

                            }
                        }
                    }
                }
                return employee;
            }
            catch (Exception ex)
            {
				string err = ex.Message;
				string stackTrace = ex.StackTrace;
                throw;
            }
        }

		//Get the Objects employee for the filter
        public IEnumerable<Employee> GetFilteredEmployeeData(int id)
        {
			try
            {
                List<Employee> lstemployee = new List<Employee>();

				Employee _employee = new Employee();

                        _employee.ID = Convert.ToInt32(0);
					/*
                        _employee.Name  = (Convert.IsDBNull(rdr["Name"])) ? "" : Convert.ToString(rdr["Name"]);
                        _employee.Address  = (Convert.IsDBNull(rdr["Address"])) ? "" : Convert.ToString(rdr["Address"]);
                        _employee.Role  = (Convert.IsDBNull(rdr["Role"])) ? "" : Convert.ToString(rdr["Role"]);
                        _employee.Department  = (Convert.IsDBNull(rdr["Department"])) ? "" : Convert.ToString(rdr["Department"]);
                        _employee.SkillSets  = (Convert.IsDBNull(rdr["SkillSets"])) ? "" : Convert.ToString(rdr["SkillSets"]);
                        _employee.Date_of_Birth  = (Convert.IsDBNull(rdr["Date of Birth"])) ? "" : Convert.ToString(rdr["Date of Birth"]);
                        _employee.Date_of_Joining  = (Convert.IsDBNull(rdr["Date of Joining"])) ? "" : Convert.ToString(rdr["Date of Joining"]);
                        _employee.IsActive  = (Convert.IsDBNull(rdr["IsActive"])) ? 0 : Convert.ToInt32(rdr["IsActive"]);
 
					*/
                        lstemployee.Add(_employee);

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_Employee_Get", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        Employee employee = new Employee();

                        employee.ID = Convert.ToInt32(rdr["ID"]);
                        employee.Name  = (Convert.IsDBNull(rdr["Name"])) ? "" : Convert.ToString(rdr["Name"]);
                        employee.Address  = (Convert.IsDBNull(rdr["Address"])) ? "" : Convert.ToString(rdr["Address"]);
                        employee.Role  = (Convert.IsDBNull(rdr["Role"])) ? "" : Convert.ToString(rdr["Role"]);
                        employee.Department  = (Convert.IsDBNull(rdr["Department"])) ? "" : Convert.ToString(rdr["Department"]);
                        employee.SkillSets  = (Convert.IsDBNull(rdr["SkillSets"])) ? "" : Convert.ToString(rdr["SkillSets"]);
                        employee.Date_of_Birth  = (Convert.IsDBNull(rdr["Date of Birth"])) ? "" : Convert.ToString(rdr["Date of Birth"]);
                        employee.Date_of_Joining  = (Convert.IsDBNull(rdr["Date of Joining"])) ? "" : Convert.ToString(rdr["Date of Joining"]);
                        //if (!Convert.IsDBNull(rdr["IsActive"]))
                        //{
                        //    employee.IsActive = false;
                        //}
                        //else
                        //{
                        //    employee.IsActive = Convert.ToBoolean(rdr["IsActive"]);
                        //}
                        lstemployee.Add(employee);
                    }
                    con.Close();
                }
                return lstemployee;
            }
            catch (Exception ex)
            {
				string err = ex.Message;
				string stackTrace = ex.StackTrace;
                throw;
            }
        }


        //To Delete the record on a particular employee
        public object DeleteEmployee(int ID)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_Employee_DeleteByID", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ID", ID);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
				return new ResultVM  
                { Status = "Success", Message = "SuccessFully Deleted." };  
            }  
            catch (Exception ex)  
            {  
                return new ResultVM  
                { Status = "Error", Message = ex.Message.ToString() };  
            }  
        }
    }
}
