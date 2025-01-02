﻿using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Data.Entities.Views;
using SchoolProject.Infrastructure.Abstract;
using SchoolProject.Infrastructure.Abstract.Views;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Service.Implementation
{
    public class DepartmentServices : IDepartmentServices
    {
        #region Fields
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IViewRepository<ViewDepartment> _viewDepartmentRepository;
        #endregion

        #region Constructors
        public DepartmentServices(IDepartmentRepository departmentRepository, IViewRepository<ViewDepartment> viewDepartmentRepository)
        {
            this._departmentRepository = departmentRepository;
            _viewDepartmentRepository = viewDepartmentRepository;
        }
        #endregion

        #region Handel Functions
        public async Task<Department> GetDepartmentById(int id)
        {
            var department = await _departmentRepository.GetTableNoTracking().Where(d => d.Id == id)
                .Include(d => d.Instructors)
                .Include(d => d.DepartmentSubjects).ThenInclude(d => d.Subject)
                .Include(d => d.Instructor)
                .FirstOrDefaultAsync();

            return department;

        }

        public async Task<List<ViewDepartment>> GetViewDepartmentDataAsync()
        {
            return await _viewDepartmentRepository.GetTableNoTracking().ToListAsync();
        }

        public async Task<bool> IsDepartmentIdExist(int id)
        {
            var department = await _departmentRepository.GetByIdAsync(id);

            return department != null;
        }
        #endregion
    }
}
