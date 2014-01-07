using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scheduler.Model.EntityModels;
using System.Data.Spatial;

namespace Scheduler.Model.Repositories.Interfaces
{
    public interface IProjectRepository
    {
        Project getProjectById(int id);

        Project getProjectByName(string ProjectName);

        IEnumerable<Project> getAllProjectByIdOwner(int OwnerId);

        void addNewProject(string ProjectName, float Budget, DateTime StartTime, DateTime StopTime, string OwnerLogin);//dopisac logike


        void addNewProject(string ProjectName, float Budget, DateTime StartTime, string OwnerLogin);//dopisac logike

        void addProjectStopTime(string ProjectName, DateTime StopTime);

        Document getDocumentById(int id);

        IEnumerable<Document> getAllDocumentsByProjectName(string ProjectName);

        void addNewDocument(string ProjectName, string DocumentName, string DocumentContent, string Login);//dopisac user pracujacy nad projektem

        IEnumerable<Project> getAllProjects();
    }
}
