using CrownGlobal.ViewModels;
using CrownGlobal.ViewModels.TestimonyGroup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrownGlobal.Interfaces
{
    public interface ITestimonyGroupService
    {
        Task<ResultModel<string>> Add(TestimonyGroupPostVM model);
        Task<ResultModel<List<TestimonyGroupListVM>>> GetAll();
        Task<ResultModel<TestimonyGroupListVM>> Get(int Id);
        Task<ResultModel<string>> Update(int id, TestimonyGroupPostVM model);
    }
}
