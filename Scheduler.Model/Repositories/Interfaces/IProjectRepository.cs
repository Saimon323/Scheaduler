using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scheduler.Model.EntityModels;

namespace Scheduler.Model.Repositories.Interfaces
{
    public interface IProjectRepository
    {
        Project GetProjectById(int idProject);
        Project GetProjectByName(string nameProject);
        IEnumerable<Project> GetAllProjects();
        int CreateNewPorject(string projectName);

    }
}
