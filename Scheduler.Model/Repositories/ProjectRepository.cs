using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scheduler.Model.Repositories.Interfaces;
using Scheduler.Model.EntityModels;
using System.Data.Spatial;

namespace Scheduler.Model.Repositories
{
    public class ProjectRepository : BaseRepository<Scheduler.Model.EntityModels.Project, SchedulerEntities>, IProjectRepository, IDisposable
    {
        #region BaseRepository

        public override void Add(Project project)
        {
            Entities.AddToProjects(project);
        }

        public override IQueryable<Project> Items
        {
            get
            {
                return Entities.Projects;
            }
        }

        #endregion

        string ownerRole = "Owner";
        string managerRole = "Menager";
        int autoIncrement = 0;
       // IUserRepository userRepo = new UserRepository();
        //IGroupRepository groupRepo = new GroupRepository();

        public Project getProjectById(int id)
        {
            return Items.Where(x => x.id.Equals(id)).FirstOrDefault();
        }

        public Project getProjectByName(string ProjectName)
        {
            return Items.Where(x => x.ProjectName.Equals(ProjectName)).FirstOrDefault();
        }

        public void addNewProject(string ProjectName, float Budget, DateTime StartTime, DateTime StopTime, string OwnerLogin)
        {
            Project projectExist = getProjectByName(ProjectName);

            if (projectExist != null)
                return;

            IUserRepository userRepository = new UserRepository();

            User userExist = userRepository.getOwnerByLogin(OwnerLogin, ownerRole);

            if (userExist == null)
                return;

            Project project = new Project(ProjectName, Budget, StartTime, StopTime, userExist.id);
            Entities.AddToProjects(project);
            Entities.SaveChanges();
        }

        public void addNewProject(string ProjectName, float Budget, DateTime StartTime, string OwnerLogin)
        {
            IUserRepository userRepository = new UserRepository();

            Project projectExist = getProjectByName(ProjectName);

            if (projectExist != null)
                return;

            User userExist = userRepository.getOwnerByLogin(OwnerLogin, ownerRole);

            if (userExist == null)
                return;

            Project project = Project.CreateProject(autoIncrement, ProjectName, Budget, StartTime, userExist.id);
            Entities.AddToProjects(project);
            Entities.SaveChanges();
        }

        public void addProjectStopTime(string ProjectName, DateTime StopTime)
        {
            Project projectExist = getProjectByName(ProjectName);

            if (projectExist == null)
                return;

            projectExist.StopTime = StopTime;
            Entities.SaveChanges();
        }

        public Document getDocumentById(int id)
        {
            return Entities.Documents.Where(x => x.id.Equals(id)).FirstOrDefault();
        }

        public IEnumerable<Document> getAllDocumentsByProjectName(string ProjectName)
        {
            Project projectExist = getProjectByName(ProjectName);
            if (projectExist == null)
                return null;

            return Entities.Documents.Where(x => x.ProjectId.Equals(projectExist.id));
        }

        public void addNewDocument(string ProjectName, string DocumentName, string DocumentContent, string Login)
        {
            IUserRepository userRepo = new UserRepository();
            IGroupRepository groupRepo = new GroupRepository();
            Project projectExist = getProjectByName(ProjectName);

            if (projectExist == null)
                return;

            User userExist = userRepo.getUserByLogin(Login);

            if (userExist == null)
                return;

            List<Group> groupList = groupRepo.getAllGroupWorkingInProject(projectExist.ProjectName);

            if (userExist.GroupId != null)
            {
                foreach (var gro in groupList)
                {
                    if (gro.id == userExist.GroupId)
                    {
                        Document document = Document.CreateDocument(autoIncrement, projectExist.id, DocumentName, DocumentContent, userExist.id);
                        Entities.AddToDocuments(document);
                        Entities.SaveChanges();
                    }
                }
            }
            else
            {
                Role role = userRepo.getRoleByName(managerRole);
                if(userExist.RoleId != role.id)
                    return;

                List<User> menagersList = new List<User>();//groupRepo.getMenagerById(

                foreach (var single in groupList)
                {
                    User menager = groupRepo.getMenagerById(single.MenagerId);
                    menagersList.Add(menager);
                }

                foreach (var z in menagersList)
                {
                    if (z.id == userExist.id)
                    {
                        Document document = Document.CreateDocument(autoIncrement, projectExist.id, DocumentName, DocumentContent, userExist.id);
                        Entities.AddToDocuments(document);
                        Entities.SaveChanges();
                    }
                }
            }







            
        }

        public IEnumerable<Document> getDocumentsByDocumentName(string ProjectName, string DocumentName)
        {
            Project projectExist = getProjectByName(ProjectName);

            if (projectExist == null)
                return null;

            return Entities.Documents.Where(x => x.ProjectId.Equals(projectExist.id) && x.DocumentName.Equals(DocumentName));

        }

        public void deleteDocument(Document document)
        {
            Document documentExist = getDocumentById(document.id);
            if (documentExist == null)
                return;

            Entities.DeleteObject(documentExist);
            Entities.SaveChanges();

        }

        public void deleteProject(string ProjectName)
        {
            Project projectExist = getProjectByName(ProjectName);

            if (projectExist == null)
                return;

            Entities.DeleteObject(projectExist);
            Entities.SaveChanges();
        }
    }
}
