using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioEAI.Repositories
{
    public interface IRepository
    {
        ProjectRepository Projects { get; }
    }
}