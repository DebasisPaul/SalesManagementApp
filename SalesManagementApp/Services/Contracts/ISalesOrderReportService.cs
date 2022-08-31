﻿using SalesManagementApp.Models.ReportModels;

namespace SalesManagementApp.Services.Contracts
{
    public interface ISalesOrderReportService
    {
        //SR
        Task<List<GroupedFieldPriceModel>> GetEmployeePricePerMonthData();
        Task<List<GroupedFieldQtyModel>> GetQtyPerProductCategory();
        Task<List<GroupedFieldQtyModel>> GetQtyPerMonthData();

        //TL
        Task<List<GroupedFieldPriceModel>> GetGrossPerTeamMemberData();
        Task<List<GroupedFieldQtyModel>> GetQtyPerTeamMemberData();
        Task<List<GroupedFieldQtyModel>> GetTeamQtyPerMonthData();

    }
}
