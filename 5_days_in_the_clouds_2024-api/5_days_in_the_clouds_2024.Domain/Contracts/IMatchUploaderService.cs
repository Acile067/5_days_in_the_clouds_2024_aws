using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5_days_in_the_clouds_2024.Domain.Contracts
{
    public interface IMatchUploaderService
    {
        Task UploadMatchAsync(Domain.Entities.Match match);
    }
}
