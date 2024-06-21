using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;

namespace HIMS.Data.InventoryReports
{
    public interface I_InventoryReport
    {
        
        string ViewItemList(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader);
        string ViewSupplierList(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader);
        string ViewIndentReport(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader);
        string ViewMonthlyPurchaseGRNReport(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader);
        string ViewGRNReport(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader);
        string ViewGRNReportNΑΒΗ(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader);
        string ViewGRNReturnReport(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader);
      
        string ViewIssueToDepartmentMonthlySummary(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader);
        string ViewGRNWiseProductQtyReport(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader);
        string ViewGRNPurchaseReport(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader);
        string ViewSupplierWiseGRNList(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader);
        string ViewIssueToDepartment(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader);
        string ViewIssueToDepartmentItemWise(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader);
        string ViewReturnFromDepartment(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader);
        string ViewPurchaseOrder(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader);
        string ViewMaterialConsumptionMonthlySummary(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader);
        string ViewMaterialConsumption(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader);
        string ViewItemExpiryReport(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader);
        string ViewCurrentStockReport(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader);
        string ViewClosingCurrentStockReport(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader);
        string ViewItemWiseSupplierList(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader);
        string ViewCurrentStockDateWise(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader);
        string ViewNonMovingItemList(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader);
        string ViewNonMovingItemWithoutBatchList(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader);
        string ViewPatientWiseMaterialConsumption(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader);
        string ViewLastPurchaseRateWiseConsumtion(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader);
        string ViewItemCount(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader);
        string ViewSupplierWiseDebitCreditNote(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader);
        string ViewStockAdjustmentReport(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader);
        string ViewPurchaseWiseGRNSummary(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader);
    }
}
