using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.Models;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;

namespace WebApi.Controllers
{
    public class EmployeeController : ApiController
    {
        public HttpResponseMessage Get()
        {
            string query = @"
                             select EmployeeId,EmployeeName,Department,
                              convert(varchar(10),DateOfJoining,120) as DateOfJoining,
                              PhotoFileName
                                from
                             dbo.Employee
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

        public string post(Employee emp)
        {
            try
            {

                string query = @"
              insert into dbo.Employee values 
             ( 
                '" + emp.EmployeeName + @"' 
                ,'" + emp.Department + @"' 
                ,'" + emp.DateOfJoining + @"' 
                 ,'" + emp.PhotoFileName + @"' 
             )      
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
            catch (Exception)
            {
                return "Failed to Add !!";
            }
        }

        public string put(Employee emp)
        {
            try
            {

                string query = @"
              update  dbo.Employee set 
               EmployeeName='" + emp.EmployeeName + @"'
               ,Department='" + emp.Department + @"'
               ,DateOfJoining='" + emp.DateOfJoining + @"'
               ,PhotoFileName='" + emp.PhotoFileName + @"'
              where EmployeeId=" + emp.EmployeeId + @"       
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
            catch (Exception )
            {
                return "Failed to update !!";
            }
        }

        public string Delete(int Id)
        {
            try
            {

                string query = @"
              Delete from dbo.Employee 
              where EmployeeId=" + Id + @"       
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
        [Route("api/Employee/GetAllDepartmentNames")]
        [HttpGet ]
        public HttpResponseMessage GetAllDepartmentNames()
        {
            string query = @"
              select DepartmentName from dbo.Department"; 

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

        [Route("api/Employee/SaveFile")]
       
        public string SaveFile()
        {
            //try
            //{
                var httpRequest = HttpContext.Current.Request;
                var postedFile = httpRequest.Files[0];
                string filename = postedFile.FileName;
                var physicalpath = HttpContext.Current.Server.MapPath("/photos/" + filename);

                postedFile.SaveAs(physicalpath);

                return filename;
            //}
            //catch(Exception)
            //{
            //    return "download.png";
            //}
        }
    }
}
