using ManagementDTOs;
using ManagementRepository.IRepository;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ManagementRepository.Repository
{
    public class EmployeeRepo : IEmployee
    {
        private readonly IConfiguration _configuration;

        public EmployeeRepo(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private string SqlConnection()
        {
            return _configuration.GetConnectionString("ManagementCS").ToString();
        }

        public List<EmployeeDTO> GetEmployees()
        {
            SqlConnection con = new SqlConnection(this.SqlConnection());
            SqlCommand cmd = new SqlCommand("spGetEmployee", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            con.Open();
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            List<EmployeeDTO> list = new List<EmployeeDTO>();
            con.Close();
            foreach (DataRow dr in dt.Rows)
            {
                list.Add(new EmployeeDTO
                {
                    EmpId = Convert.ToInt32(dr["EmpId"]),
                    EmpName = dr["EmpName"].ToString(),
                    StateId = Convert.ToInt32(dr["StateId"]),
                    StateName = dr["StateName"].ToString(),
                    CityId = Convert.ToInt32(dr["CityId"]),
                    CityName = dr["CityName"].ToString()

                });
            }
            return list;

        }

        public List<StateDTO> GetStates()
        {
            SqlConnection con = new SqlConnection(this.SqlConnection());
            SqlCommand cmd = new SqlCommand("spGetState", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            con.Open();
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            List<StateDTO> list = new List<StateDTO>();
            con.Close();
            foreach (DataRow dr in dt.Rows)
            {
                list.Add(new StateDTO
                {
                    StateId = Convert.ToInt32(dr["StateId"]),
                    StateName = dr["StateName"].ToString()
                });
            }
            return list;

        }




        public List<CityDTO> GetCity(int id)
        {
            SqlConnection con = new SqlConnection(this.SqlConnection());
            SqlCommand cmd = new SqlCommand("spGetCity", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StateId", id);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            con.Open();
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            List<CityDTO> list = new List<CityDTO>();
            con.Close();
            foreach (DataRow dr in dt.Rows)
            {
                list.Add(new CityDTO
                {
                    CityId = Convert.ToInt32(dr["CityId"]),
                    CityName = dr["CityName"].ToString(),
                });
            }
            return list;

        }

        public void AddEmployee(EmployeeDTO employeeDTO)
        {
            SqlConnection con = new SqlConnection(this.SqlConnection());
            SqlCommand cmd = new SqlCommand("spAddEmployee", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EmpName", employeeDTO.EmpName);
            cmd.Parameters.AddWithValue("@StateId",employeeDTO.StateId);
            cmd.Parameters.AddWithValue("@CityId", employeeDTO.CityId);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

        }
    }
}
