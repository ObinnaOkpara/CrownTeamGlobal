using CrownGlobal.Data;
using CrownGlobal.Interfaces;
using CrownGlobal.ViewModels;
using CrownGlobal.ViewModels.TestimonyGroup;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrownGlobal.Services
{
    public class TestimonyGroupService : ITestimonyGroupService
    {
        private readonly CrownGlobal.Data.ApplicationDbContext _context;

        public TestimonyGroupService(CrownGlobal.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ResultModel<string>> Add(TestimonyGroupPostVM model)
        {
            var result = new ResultModel<string>();

            //check if Offer Name already exists for vendor
            var check = await _context.TestimonyGroups
                .Where(m => m.Name == model.Name.ToUpper() && m.Name == model.Name)
                .FirstOrDefaultAsync();

            if (check != null)
            {
                result.AddError("TestimonyGroup already has Offer with this name");
                return result;
            }

            var newObject = new TestimonyGroup()
            {
                Name = model.Name,
            };

            _context.TestimonyGroups.Add(newObject);
            await _context.SaveChangesAsync();

            result.Data = "Saved Successfully";
            return result;
        }

        public async Task<ResultModel<List<TestimonyGroupListVM>>> GetAll()
        {
            var result = new ResultModel<List<TestimonyGroupListVM>>();

            result.Data = await _context.TestimonyGroups.Select(m => new TestimonyGroupListVM()
            {
                Id = m.Id,
                Name = m.Name,
                TestimonyCount = m.Testimonies.Count()
            }).ToListAsync();

            return result;
        }

        public async Task<ResultModel<TestimonyGroupListVM>> Get(int Id)
        {
            var result = new ResultModel<TestimonyGroupListVM>();

            result.Data = await _context.TestimonyGroups.Where(l=>l.Id == Id).Select(m => new TestimonyGroupListVM()
            {
                Id = m.Id,
                Name = m.Name,
                TestimonyCount = m.Testimonies.Count()
            }).FirstOrDefaultAsync();

            return result;
        }

        public async Task<ResultModel<string>> Update(int id, TestimonyGroupPostVM model)
        {
            var result = new ResultModel<string>();

            //check if TestimonyGroup already exists
            var oldObject = await _context.TestimonyGroups
                .Where(m => m.Id == id)
                .FirstOrDefaultAsync();

            if (oldObject is null)
            { 
                result.AddError("Testimony Group not found.");
                return result;
            }

            oldObject.Name = model.Name;

            await _context.SaveChangesAsync();

            result.Data = "Updated Successfully";
            return result;
        }
    }
}
