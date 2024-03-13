using AutoMapper;
using Demo.BLL.Interfaces;
using Demo.BLL.Repositories;
using Demo.DAL.Entites;
using Demo.PL.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demo.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        //private readonly IDepartmentRepository departmentRepository;

        public DepartmentController(/*IDepartmentRepository departmentRepository*/ IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            //this.departmentRepository = departmentRepository;
        }
        public async Task<IActionResult> Index()
        {
            var departments = await unitOfWork.DepartmentRepository.GetAll();

            var MappedDepartments = mapper.Map<IEnumerable<DepartmentViewModel>>(departments);
            return View(MappedDepartments);

            //var departments = await unitOfWork.DepartmentRepository.GetAll();
            //return View(departments);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(DepartmentViewModel departmentViewModel)
        {
            if(ModelState.IsValid) {

                var mappedDepartment = mapper.Map<Department>(departmentViewModel);

                await unitOfWork.DepartmentRepository.Add(mappedDepartment);
                return RedirectToAction("Index");
            }
            return View(departmentViewModel);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id is null)
           return NotFound();

            var department = await unitOfWork.DepartmentRepository.Get(id);

            var mappedDepartment = mapper.Map<DepartmentViewModel>(department);

            if (department is null)
                return NotFound();

            return View(mappedDepartment);
        }

        public async Task <IActionResult> Update(int? id)
        {
            if (id is null)
                return NotFound();

            var department = await unitOfWork.DepartmentRepository.Get(id);

            var mappedDepartment = mapper.Map<DepartmentViewModel>(department);

            if (department is null)
                return NotFound();

            return View(mappedDepartment);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int? id, DepartmentViewModel departmentViewModel)
        {
            if (id != departmentViewModel.Id)
                return NotFound(id);

            if(ModelState.IsValid)
            {
                try
                {
                    var mappedDepartment = mapper.Map<Department>(departmentViewModel);
                    await unitOfWork.DepartmentRepository.Update(mappedDepartment);
                    return RedirectToAction("Index");
                }
                catch(Exception ex) 
                {
                    return View(departmentViewModel);
                }
            }

            return View(departmentViewModel);
        }

        public async Task<IActionResult> Delete(int? id) 
        {
            if (id is null)
                return NotFound();

            var department =  await unitOfWork.DepartmentRepository.Get(id);

            if (department is null)
                return NotFound();

            await unitOfWork.DepartmentRepository.Delete(department);
            return RedirectToAction("Index");
        }
    }
}
