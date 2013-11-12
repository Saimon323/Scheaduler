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
    }
}
