using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace BusinessLayer
{
    public class EmployeeBusinessLayer
    {
        public List<Employee> Employees
        {
            get
            {
                List<Employee> employees = new List<Employee>();
                string connectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand("spGetAllEmployees", sqlConnection);
                    command.CommandType = CommandType.StoredProcedure;
                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = command.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        Employee employee = new Employee()
                        {
                            Id = Convert.ToInt32(sqlDataReader["Id"]),
                            Name = Convert.ToString(sqlDataReader["Name"]),
                            Gender = Convert.ToString(sqlDataReader["Gender"]),
                            City = Convert.ToString(sqlDataReader["City"]),
                            DateOfBirth = Convert.ToDateTime(sqlDataReader["DateOfBirth"])
                        };
                        employees.Add(employee);
                    }
                }
                return employees;
            }
        }

        public bool AddEmployee(Employee employee)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand("spAddEmployee", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                
                sqlCommand.Parameters.AddRange(new SqlParameter[]
                {
                    new SqlParameter("@Name", employee.Name),
                    new SqlParameter("@City", employee.City),
                    new SqlParameter("@Gender", employee.Gender),
                    new SqlParameter("@DateOfBirth", employee.DateOfBirth)
                });
                
                sqlConnection.Open();
                return sqlCommand.ExecuteNonQuery() != 0;
            }
        }

        public bool SaveEmployee(Employee employee)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand("spSaveEmployee", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                sqlCommand.Parameters.AddRange(new SqlParameter[]
                {
                    new SqlParameter("@Id", employee.Id), 
                    new SqlParameter("@Name", employee.Name),
                    new SqlParameter("@City", employee.City),
                    new SqlParameter("@Gender", employee.Gender),
                    new SqlParameter("@DateOfBirth", employee.DateOfBirth)
                });

                sqlConnection.Open();
                return sqlCommand.ExecuteNonQuery() != 0;
            }
        }

        public bool DeleteEmployee(int id)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand("spDeleteEmployee", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                sqlCommand.Parameters.AddRange(new SqlParameter[]
                {
                    new SqlParameter("@Id", id)
                });

                sqlConnection.Open();
                return sqlCommand.ExecuteNonQuery() != 0;
            }
        }
    }
}
