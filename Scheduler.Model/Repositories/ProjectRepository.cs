﻿using System;
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
    }
}
