using AutoMapper;
using Demo.BLL.Interfaces;
using Demo.BLL.Repositories;
using Demo.DAL.Entites;
using Demo.PL.Helper;
using Demo.PL.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace Demo.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        //private readonly IEmployeeRepository employeeRepository;
        //private readonly IDepartmentRepository departmentRepository;

        public EmployeeController(/*IEmployeeRepository employeeRepository, IDepartmentRepository departmentRepository,*/ IUnitOfWork unitOfWork,IMapper mapper )
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            //this.employeeRepository = employeeRepository;
            //this.departmentRepository = departmentRepository;
        }
        public async Task<IActionResult> Index(string searchValue = "")
        {
            IEnumerable<Employee> employees;
            if (string.IsNullOrEmpty(searchValue))
            {
                employees = await unitOfWork.EmployeeRepository.GetAll();

            }
            else
            {
                employees = await unitOfWork.EmployeeRepository.Search(searchValue);
            }

            var MappedEmployees = mapper.Map<IEnumerable<EmployeeViewModel>>(employees);
            return View(MappedEmployees);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Departments = await unitOfWork.DepartmentRepository.GetAll();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmployeeViewModel employeeViewModel)
        {
            if (ModelState.IsValid)
            {
                employeeViewModel.ImageUrl = DocumentSettings.UploadFile(employeeViewModel.Image, "Imgs");
                var mappedEmployee = mapper.Map<Employee>(employeeViewModel);

                await unitOfWork.EmployeeRepository.Add(mappedEmployee);
                return RedirectToAction("Index");
            }
            return View(employeeViewModel);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id is null)
                return NotFound();

            var employee = await unitOfWork.EmployeeRepository.Get(id);

            var mappedEmployee = mapper.Map<EmployeeViewModel>(employee);

            return View(mappedEmployee);
        }

        public async Task<IActionResult> Update(int? id)
        {
            if(id is null)
                return NotFound();

            var employee = await unitOfWork.EmployeeRepository.Get(id);

            ViewBag.Departments = await unitOfWork.DepartmentRepository.GetAll();

            var mappedEmployee = mapper.Map<EmployeeViewModel>(employee);

            if(employee is null)
                return NotFound();
            

            return View(mappedEmployee);

            
        }

        [HttpPost]
        public async Task<IActionResult> Update(int? id , EmployeeViewModel employeeViewModel)
        {
            if (id != employeeViewModel.Id)
                return NotFound();

            if(ModelState.IsValid)
            {
                try
                {
                    employeeViewModel.ImageUrl = DocumentSettings.UploadFile(employeeViewModel.Image, "Imgs");

                    var mappedEmployee = mapper.Map<Employee>(employeeViewModel);
                    await unitOfWork.EmployeeRepository.Update(mappedEmployee);
                    return RedirectToAction("Index");
                }
                catch (Exception ex) 
                {
                    return View(employeeViewModel);
                }
            }
            return View(employeeViewModel);
            
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if(id is null)
                return NotFound();

            var employee = await unitOfWork.EmployeeRepository.Get(id);

            if(employee is null)
                return NotFound();

            DocumentSettings.DeleteFile("Imgs", employee.ImageUrl);

            await unitOfWork.EmployeeRepository.Delete(employee);
            return RedirectToAction("Index");
        }

    }
}
