﻿using Microsoft.EntityFrameworkCore;
using SalesManagementApp.Data;
using SalesManagementApp.Entities;
using SalesManagementApp.Models;

namespace SalesManagementApp.Extensions
{
    public static class Conversions
    {
        public static async Task<List<EmployeeModel>> Convert(this IQueryable<Employee> employees)
        {
            return await (from e in employees
                          select new EmployeeModel
                          {
                              Id = e.Id,
                              FirstName = e.FirstName,
                              LastName = e.LastName,
                              EmployeeJobTitleId = e.EmployeeJobTitleId,
                              Email = e.Email,
                              DateOfBirth = e.DateOfBirth,
                              ReportToEmpId = e.ReportToEmpId,
                              Gender = e.Gender,
                              ImagePath = e.ImagePath,
                          }).ToListAsync();
        }
        public static Employee Convert(this EmployeeModel employeeModel)
        {
            return new Employee
            {
                FirstName = employeeModel.FirstName,
                LastName = employeeModel.LastName,
                EmployeeJobTitleId = employeeModel.EmployeeJobTitleId,
                Email = employeeModel.Email,
                DateOfBirth = employeeModel.DateOfBirth,
                ReportToEmpId = employeeModel.ReportToEmpId,
                Gender = employeeModel.Gender,
                ImagePath = employeeModel.Gender.ToUpper() == "MALE" ? "/Images/Profile/MaleDefault.jpg"
                                                              : "/Images/Profile/FemaleDefault.jpg"
            };
        }

        public static async Task<List<ProductModel>> Convert(this IQueryable<Product> Products,
                                                            SalesManagementDbContext context)
        {
            return await (from prod in Products
                          join prodCat in context.ProductCategories
                          on prod.CategoryId equals prodCat.Id
                          select new ProductModel
                          {
                              Id = prod.Id,
                              Name = prod.Name,
                              Description = prod.Description,
                              ImagePath = prod.ImagePath,
                              Price = prod.Price,
                              CategoryId = prod.CategoryId,
                              CategoryName = prodCat.Name
                          }).ToListAsync();
        }

        public static async Task<List<ClientModel>> Convert(this IQueryable<Client> clients,
                                                    SalesManagementDbContext context)
        {
            return await (from c in clients
                          join r in context.RetailOutlets
                          on c.RetailOutletId equals r.Id
                          select new ClientModel
                          {
                              Id = c.Id,
                              Email = c.Email,
                              FirstName = c.FirstName,
                              LastName = c.LastName,
                              JobTitle = c.JobTitle,
                              PhoneNumber = c.PhoneNumber,
                              RetailOutletId = c.RetailOutletId,
                              RetailOutletLoction = r.Location,
                              RetailOutletName = r.Name

                          }).ToListAsync();

        }

        public static async Task<List<OrganisationModel>> ConvertToHierarchy(this IQueryable<Employee> employees,
                                             SalesManagementDbContext context)
        {
            return await (from e in employees
                          join t in context.EmployeeJobTitles
                          on e.EmployeeJobTitleId equals t.EmployeeJobTitleId
                          orderby e.Id
                          select new OrganisationModel
                          {
                              EmployeeId = e.Id.ToString(),
                              ReportsToId = e.ReportToEmpId != null ? e.ReportToEmpId.ToString() : "",
                              Email = e.Email,
                              FirstName = e.FirstName,
                              LastName = e.LastName,
                              ImagePath = e.ImagePath,
                              JobTitle = t.Name

                          }).ToListAsync();
        }
    }
}
