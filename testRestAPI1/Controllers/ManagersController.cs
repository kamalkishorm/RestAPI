using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace testRestAPI1.Controllers
{
    public class ManagersController : ApiController
    {
        //Get Details of all Managers.
        [HttpGet]
        [Route("managers")]
        public HttpResponseMessage Get()
        {
            string ConnectionString = System.Configuration.ConfigurationManager.AppSettings["SQLConnect"];
            string SQL_TableName = "manager";
            string Result;
            try
            {
                using (SqlConnection Sql_Connection = new SqlConnection(ConnectionString))
                {
                    //Retrieve Column Name.
                    SqlDataAdapter columnName = new SqlDataAdapter("SELECT column_name as 'Column Name' FROM information_schema.columns WHERE table_name = '" + SQL_TableName + "';", Sql_Connection);
                    //SqlCommand comm = new SqlCommand("SELECT column_name as 'Column Name' FROM information_schema.columns WHERE table_name = '" + SQL_TableName + "';", Sql_Connection);
                    DataSet tableColumnName = new DataSet();
                    columnName.Fill(tableColumnName, SQL_TableName);
                    String[] tableColumnNameArr = new String[tableColumnName.Tables[SQL_TableName].Rows.Count];
                    int i = 0;
                    foreach (DataRow row in tableColumnName.Tables[SQL_TableName].Rows)//or similar
                    {
                        tableColumnNameArr[i] = row[0].ToString();
                        i++;
                    }

                    DataSet DS = new DataSet();
                    SqlDataAdapter dataR = new SqlDataAdapter("SELECT *  FROM " + SQL_TableName + ";", Sql_Connection);
                    dataR.Fill(DS, "ManagersData");
                    Result= "{ \"response_code\": \"0\",  \"response_message\": \"string\",  \"has_more_record\": true,  \"total_records\": \"0\",\"managers_list\":[";
                    foreach (DataRow DR in DS.Tables["ManagersData"].Rows)
                    {
                        Result = Result + "{\"managers_list\": [{\"manager_id\":\" "+DR[0]+ "\",\"manager\": {\"manager_personal_details\": {\"first_name\": \"" + DR[2] + "\",\"middle_name\": \"" + DR[3] + "\",\"last_name\": \"" + DR[4] + "\", \"job_tilte\": \"" + DR[5] + "\"  },  \"manager_account_info\": { \"account_id\": \"0\", \"account_name\": \"string\"  },  \"manager_contact_information\": { \"business_phone\": \"" + DR[15] + "\", \"business_phone_extension\": \"" + DR[82] + "\", \"business_mobile\": \"" + DR[17] + "\", \"business_email\": \"" + DR[20] + "\", \"personal_email\": \"" + DR[22] + "\", \"business_address_line_1\": \"string\", \"business_address_line_2\": \"string\", \"business_address_country_id\": \"0\", \"business_address_country\": \"" + DR[13] + "\", \"business_address_state_id\": \"0\", \"business_address_state\": \"" + DR[12] + "\", \"business_address_city_id\": \"0\", \"business_address_city\": \"" + DR[11] + "\", \"business_address_zip\": \"" + DR[14] + "\", \"business_unit\": \"string\"  },  \"manager_current_snapshot\": { \"business_current\": \"string\", \"personal_current\": \"string\"  },  \"manager_assigned_sales_reps\": { \"primary_sales_rep\": {\"sales_rep_id\": \"" + DR[37] + "\",\"sales_rep_name\": \"string\" }, \"secondary_sales_rep\": {\"sales_rep_id\": \"string\",\"sales_rep_name\": \"string\" }, \"tertiary_sales_rep\": {\"sales_rep_id\": \"string\",\"sales_rep_name\": \"string\" }  },  \"manager_client_stats\": { \"no_of_contractors\": \"0\", \"no_of_reports\": \"0\", \"last_hire_date\": \"string\", \"expected_hire_date\": \"string\"  },  \"manager_alignment\": { \"is_regional_initiative\": true, \"region\": {\"region_id\": \"0\",\"region_name\": \"string\" }, \"is_top_20\":\"" + DR[76] + "\", \"is_hiring_tcu_skills\": \"" + (DR[0] != null) + "\", \"tcu_skills\": [{  \"tcu_skill_id\": \"" + DR[70] + "\",  \"tcu_skill_name\": \"" + DR[71] + "\"} ], \"do_not_survey\": true, \"msp_vmo\": true, \"personal_alignment_info\": \"string\"  }} }  ]},";
                    }
                    Result = Result.Substring(0,Result.Length - 1);
                    Result = Result + "]}";
                    var ResponseData = JObject.Parse(Result);
                    
                    return Request.CreateResponse(HttpStatusCode.OK, ResponseData);
                }
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);
            }
        }


        //Get Details of Managers by Manager_ID.
        [HttpGet]
        [Route("managers/{id}")]
        public HttpResponseMessage GetByID( int id)
        {
            string ConnectionString = System.Configuration.ConfigurationManager.AppSettings["SQLConnect"];
            string SQL_TableName = "manager";
            string Result;
            try
            {
                using (SqlConnection Sql_Connection = new SqlConnection(ConnectionString))
                {
                    //Retrieve Column Name.
                    SqlDataAdapter columnName = new SqlDataAdapter("SELECT column_name as 'Column Name' FROM information_schema.columns WHERE table_name = '" + SQL_TableName + "';", Sql_Connection);
                    //SqlCommand comm = new SqlCommand("SELECT column_name as 'Column Name' FROM information_schema.columns WHERE table_name = '" + SQL_TableName + "';", Sql_Connection);
                    DataSet tableColumnName = new DataSet();
                    columnName.Fill(tableColumnName, SQL_TableName);
                    String[] tableColumnNameArr = new String[tableColumnName.Tables[SQL_TableName].Rows.Count];
                    int i = 0;
                    foreach (DataRow row in tableColumnName.Tables[SQL_TableName].Rows)//or similar
                    {
                        tableColumnNameArr[i] = row[0].ToString();
                        i++;
                    }

                    DataSet DS = new DataSet();
                    SqlDataAdapter dataR = new SqlDataAdapter("SELECT *  FROM " + SQL_TableName + " where manager_id ="+id+";", Sql_Connection);
                    dataR.Fill(DS, "ManagersData");
                    Result = "{ \"response_code\": \"0\",  \"response_message\": \"string\",\"manager\":";
                    foreach (DataRow DR in DS.Tables["ManagersData"].Rows)
                    {
                        Result = Result + "{\"manager_personal_details\": {\"first_name\": \"" + DR[2] + "\",\"middle_name\": \"" + DR[3] + "\",\"last_name\": \"" + DR[4] + "\", \"job_tilte\": \"" + DR[5] + "\"  },  \"manager_account_info\": { \"account_id\": \"0\", \"account_name\": \"string\"  },  \"manager_contact_information\": { \"business_phone\": \"" + DR[15] + "\", \"business_phone_extension\": \"" + DR[82] + "\", \"business_mobile\": \"" + DR[17] + "\", \"business_email\": \"" + DR[20] + "\", \"personal_email\": \"" + DR[22] + "\", \"business_address_line_1\": \"string\", \"business_address_line_2\": \"string\", \"business_address_country_id\": \"0\", \"business_address_country\": \"" + DR[13] + "\", \"business_address_state_id\": \"0\", \"business_address_state\": \"" + DR[12] + "\", \"business_address_city_id\": \"0\", \"business_address_city\": \"" + DR[11] + "\", \"business_address_zip\": \"" + DR[14] + "\", \"business_unit\": \"string\"  },  \"manager_current_snapshot\": { \"business_current\": \"string\", \"personal_current\": \"string\"  },  \"manager_assigned_sales_reps\": { \"primary_sales_rep\": {\"sales_rep_id\": \"" + DR[37] + "\",\"sales_rep_name\": \"string\" }, \"secondary_sales_rep\": {\"sales_rep_id\": \"string\",\"sales_rep_name\": \"string\" }, \"tertiary_sales_rep\": {\"sales_rep_id\": \"string\",\"sales_rep_name\": \"string\" }  },  \"manager_client_stats\": { \"no_of_contractors\": \"0\", \"no_of_reports\": \"0\", \"last_hire_date\": \"string\", \"expected_hire_date\": \"string\"  },  \"manager_alignment\": { \"is_regional_initiative\": true, \"region\": {\"region_id\": \"0\",\"region_name\": \"string\" }, \"is_top_20\":\"" + DR[76] + "\", \"is_hiring_tcu_skills\": \"" + (DR[0] != null) + "\", \"tcu_skills\": [{  \"tcu_skill_id\": \"" + DR[70] + "\",  \"tcu_skill_name\": \"" + DR[71] + "\"} ], \"do_not_survey\": true, \"msp_vmo\": true, \"personal_alignment_info\": \"string\"  }";
                    }
                    Result = Result + "} }";
                    var ResponseData = JObject.Parse(Result);

                    return Request.CreateResponse(HttpStatusCode.OK, ResponseData);
                }
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);
            }
        }


    }
}
