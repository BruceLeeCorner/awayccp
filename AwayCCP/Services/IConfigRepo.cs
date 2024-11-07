using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwayCCP.Services
{
    public interface IConfigRepo
    {
        Task<IConfig> LoadAsync();
        Task SaveAsync(IConfig config);
    }
}
