using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class DepartmentController : ApiController
    {
        public HttpResponseMessage Get()
        {
            string query = @"
                             select DepartmentId,DepartmentName from
                             dbo.Department
                                ";
            DataTable table = new DataTable();

            using (var con = new SqlConnection(ConfigurationManager.
                ConnectionStrings["EmployeeAppDB"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }
            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        public string post(Department dep)
        {
            try
            {

                string query = @"
              insert into dbo.Department values 
             ( '" +dep.DepartmentName+ @"' )      
              ";
                DataTable table = new DataTable();

                using (var con = new SqlConnection(ConfigurationManager.
                    ConnectionStrings["EmployeeAppDB"].ConnectionString))
                using (var cmd = new SqlCommand(query, con)) 
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }
                  return "Add Successfully!!";
              
            }
            catch(Exception)
            {
                 return "Failed to Add !!";
            }
        }

        public string put(Department dep)
        {
            try
            {

                string query = @"
              update  dbo.Department set DepartmentName=
              '" + dep.DepartmentName + @"'
              where DepartmentId="+dep.DepartmentId+@"       
              ";
                DataTable table = new DataTable();

                using (var con = new SqlConnection(ConfigurationManager.
                    ConnectionStrings["EmployeeAppDB"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }
                return "update Successfully!!";
            }
            catch (Exception)
            {
                return "Failed to update !!";
            }
        }

        public string Delete(int Id)
        {
            try
            {

                string query = @"
              Delete from dbo.Department 
              where DepartmentId=" + Id + @"       
              ";
                DataTable table = new DataTable();

                using (var con = new SqlConnection(ConfigurationManager.
                    ConnectionStrings["EmployeeAppDB"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }
                return "Delete Successfully!!";
            }
            catch (Exception)
            {
                return "Failed to Delete !!";
            }
        }
    }
}
