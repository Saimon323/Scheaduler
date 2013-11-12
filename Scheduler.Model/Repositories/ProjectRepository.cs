using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scheduler.Model.Repositories.Interfaces;
using Scheduler.Model.EntityModels;

namespace Scheduler.Model.Repositories
{
    public class ProjectRepository : BaseRepository<Scheduler.Model.EntityModels.Project, ProjectsEntities>, IProjectRepository, IDisposable
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

        public int autoIncrementField = 0;

        /// <summary>
        /// Pobieranie projektu po jego Id
        /// </summary>
        /// <param name="idProject"></param>
        /// <returns></returns>
        public Project GetProjectById(int idProject)
        {
            return Items.Where(x => x.id.Equals(idProject)).FirstOrDefault();
        }

        /// <summary>
        /// Pobieranie wszyskich projektow
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Project> GetAllProjects()
        {
            return Items.Select(x => x);
        }

        /// <summary>
        /// Dodawanie nowego projektu do bazy
        /// </summary>
        /// <param name="projectName"></param>
        /// <returns></returns>
        public int CreateNewPorject(string projectName)
        {
            projectName = projectName.ToLower();
            var checkProjectExist = Items.Where(t => t.NameProject.Equals(projectName)).FirstOrDefault();

            if (checkProjectExist == null)
            {
                Project newProject = Project.CreateProject(autoIncrementField, projectName);
                Entities.AddToProjects(newProject);
                Entities.SaveChanges();
                return newProject.id;
            }
            else
                return checkProjectExist.id;
        }
    }
}
