﻿using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace PCIapi.Model
{
    public class ManageProjectMaster : DBconnection
    {
        public ManageProjectMaster(IConfiguration configuration) : base(configuration)
        {

        }
        public IEnumerable<projectMaster> getProjectDetails()
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sQuery = @"SELECT ProjectID ,ProjectCode,ProjectName,ProjectManager FROM MstProjectMaster ";
                dbConnection.Open();
                return dbConnection.Query<projectMaster>(sQuery);
            }
        }
        public IEnumerable<projectMaster> getProjectDetails(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sQuery = @"SELECT ProjectID ,ProjectCode,ProjectNmae,ProjectManager FROM MstProjectMaster WHERE ProjectID=@_Projectid ";
                dbConnection.Open();
                return dbConnection.Query<projectMaster>(sQuery, new { _Projectid = id });
            }
        }
        public IEnumerable<projectMaster> getProjectDetails(projectMaster _projectMaster)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sQuery = @"SELECT ProjectID ,ProjectCode,ProjectName,ProjectManager FROM MstProjectMaster WHERE ProjectCode=@_strProjectCode AND ProjectName=@_strProjectName AND ProjectManager=@_strProjectManager ";
                dbConnection.Open();
                return dbConnection.Query<projectMaster>(sQuery, new { _strProjectCode = _projectMaster.ProjectCode, _strProjectName = _projectMaster.ProjectName, _strProjectManager = _projectMaster.ProjectManager });
            }
        }
        public int insertProjectDetails(projectMaster _projectMaster)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sQuery = @"INSERT INTO MstProjectMaster (ProjectCode,ProjectName,ProjectManager,StartDate,EndDate )  values(@_strProjectCode,@_strProjectName,@_strProjectManager,GETDATE(),@_strEndDate)";
                dbConnection.Open();
                var affectedRows = dbConnection.Execute(sQuery, new { _strProjectCode = _projectMaster.ProjectCode, _strProjectName = _projectMaster.ProjectName, _strProjectManager = _projectMaster.ProjectManager, _strEndDate = _projectMaster.EndDate });
                return affectedRows;

            }

        }
        public string getcheckingProjectCode(string ProjectCode)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sQuery = @"select 1 from  MstProjectMaster  where ProjectCode = @_strProjectCode";
                dbConnection.Open();
                var result = dbConnection.Query<string>(sQuery, new { _strProjectCode = ProjectCode });
                if (result.Count() > 0)
                {
                    return "ProjectCode exist";
                }
                else
                {
                    return "ProjectCode doesnot exist";
                }



            }
        }
        public string getcheckingProjectNmae(string ProjectName)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sQuery = @"select 1 from  MstProjectMaster  where ProjectName = @_strProjectName";
                dbConnection.Open();
                var result = dbConnection.Query<string>(sQuery, new { _strProjectName = ProjectName });
                if (result.Count() > 0)
                {
                    return "ProjectName exist";
                }
                else
                {
                    return "ProjectName doesnot exist";
                }



            }
        }



    }
    public class projectMaster
    {
        public int ProjectID { get; set; }
        public string ProjectCode { get; set; }
        public string ProjectName { get; set; }
        public string ProjectManager { get; set; }
        public DateTime  EndDate { get; set; }
    }
}
