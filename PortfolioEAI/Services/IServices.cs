using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PortfolioEAI.Models;

namespace PortfolioEAI.Services
{
    public interface IServices
    {
        ProjectService Projects { get; }
    }
}