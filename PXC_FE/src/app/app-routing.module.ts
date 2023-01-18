import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { sample } from 'rxjs/operators';
import { AppRoutingGuard } from './app-routing.guard';

const routes: Routes = [

  //*region WH
  {
    path: 'ListServiceCharge',
    loadChildren: () =>
      import('./pages/wh/list-service-charge/list-service-charge-list/list-service-charge-list.module').then(
        (m) => m.ListServiceChargeListModule
      ),
    canLoad: [AppRoutingGuard],
  },

  {
    path: 'ListPackage',
    loadChildren: () =>
      import('./pages/wh/list-package/list-package-list/list-package-list.module').then(
        (m) => m.ListPackageListModule
      ),
    canLoad: [AppRoutingGuard],
  },

  {
    path: 'ListOrder',
    loadChildren: () =>
      import('./pages/wh/list-order/list-order-list/list-order-list.module').then(
        (m) => m.ListOrderListModule
      ),
    canLoad: [AppRoutingGuard],
  },

  {
    path: 'ListMawb',
    loadChildren: () =>
      import('./pages/wh/list-mawb/list-mawb-list/list-mawb-list.module').then(
        (m) => m.ListMawbListModule
      ),
    canLoad: [AppRoutingGuard],
  },

  {
    path: 'ListWH',
    loadChildren: () =>
      import('./pages/wh/list-wh/list-wh-list/list-wh-list.module').then(
        (m) => m.ListWhListModule
      ),
    canLoad: [AppRoutingGuard],
  },

  {
    path: 'ListDelivery',
    loadChildren: () =>
      import('./pages/wh/list-delivery/list-delivery-list/list-delivery-list.module').then(
        (m) => m.ListDeliveryListModule
      ),
    canLoad: [AppRoutingGuard],
  },

  {
    path: 'ListContainer',
    loadChildren: () =>
      import('./pages/wh/list-container/list-container-list/list-container-list.module').then(
        (m) => m.ListContainerListModule
      ),
    canLoad: [AppRoutingGuard],
  },
  //end


  {
    path: '',
    loadChildren: () =>
      import('./pages/dash-board/dash-board.module').then(
        (m) => m.DashBoardModule
      ),
    canLoad: [AppRoutingGuard],
  },
  {
    path: 'dashboard',
    loadChildren: () =>
      import('./pages/dash-board/dash-board.module').then(
        (m) => m.DashBoardModule
      ),
    canLoad: [AppRoutingGuard],
  },
  {
    path: 'login',
    loadChildren: () =>
      import('./pages/login/login.module').then((m) => m.LoginModule),
    canLoad: [AppRoutingGuard],
  },

  //#region dictionary
  {
    path: 'DmPeriod',
    loadChildren: () =>
      import(
        './pages/dictionary/dm-period/dm-period-list/dm-period-list.module'
      ).then((m) => m.DmPeriodListModule),
    canLoad: [AppRoutingGuard],
  },
  {
    path: 'DmItem',
    loadChildren: () =>
      import(
        './pages/dictionary/dm-item/dm-item-list/dm-item-list.module'
      ).then((m) => m.DmItemListModule),
    canLoad: [AppRoutingGuard],
  },
  {
    path: 'DmCurrency',
    loadChildren: () =>
      import(
        './pages/dictionary/dm-currency/dm-currency-list/dm-currency-list.module'
      ).then((m) => m.DmCurrencyListModule),
    canLoad: [AppRoutingGuard],
  },
  {
    path: 'PrDetailClass',
    loadChildren: () =>
      import(
        './pages/dictionary/dm-pr-detail-class/dm-pr-detail-class-list/dm-pr-detail-class-list.module'
      ).then((m) => m.DmPrDetailClassListModule),
    canLoad: [AppRoutingGuard],
  },
  {
    path: 'DmAccount',
    loadChildren: () =>
      import(
        './pages/dictionary/dm-account/dm-account-list/dm-account-list.module'
      ).then((m) => m.DmAccountListModule),
    canLoad: [AppRoutingGuard],
  },
  {
    path: 'DmPrDetail',
    loadChildren: () =>
      import(
        './pages/dictionary/dm-pr-detail/dm-pr-detail-list/dm-pr-detail-list.module'
      ).then((m) => m.DmPrDetailListModule),
    canLoad: [AppRoutingGuard],
  },
  {
    path: 'DmProvince',
    loadChildren: () =>
      import(
        './pages/dictionary/dm-province/dm-province-list/dm-province-list.module'
      ).then((m) => m.DmProvinceListModule),
    canLoad: [AppRoutingGuard],
  },
  {
    path: 'DmDistrict',
    loadChildren: () =>
      import(
        './pages/dictionary/dm-district/dm-district-list/dm-district-list.module'
      ).then((m) => m.DmDistrictListModule),
    canLoad: [AppRoutingGuard],
  },
  {
    path: 'DmRiskClass',
    loadChildren: () =>
      import(
        './pages/dictionary/dm-risk-class/dm-risk-class-list/dm-risk-class-list.module'
      ).then((m) => m.DmRiskClassListModule),
    canLoad: [AppRoutingGuard],
  },
  {
    path: 'DmContractClass',
    loadChildren: () =>
      import(
        './pages/dictionary/dm-contract-class/dm-contract-class-list/dm-contract-class-list.module'
      ).then((m) => m.DmContractClassListModule),
    canLoad: [AppRoutingGuard],
  },
  {
    path: 'DmContractStatus',
    loadChildren: () =>
      import(
        './pages/dictionary/dm-contract-status/dm-contract-status-list/dm-contract-status-list.module'
      ).then((m) => m.DmContractStatusListModule),
    canLoad: [AppRoutingGuard],
  },
  {
    path: 'DmExchangeRate',
    loadChildren: () =>
      import(
        './pages/dictionary/dm-exchange-rate/dm-exchange-rate.module'
      ).then((m) => m.DmExchangeRateModule),
    canLoad: [AppRoutingGuard],
  },
  {
    path: 'DmExpense',
    loadChildren: () =>
      import('./pages/dictionary/dm-expense/dm-expense.module').then(
        (m) => m.DmExpenseModule
      ),
    canLoad: [AppRoutingGuard],
  },
  {
    path: 'DmExpenseClass',
    loadChildren: () =>
      import(
        './pages/dictionary/dm-expense-class/dm-expense-class.module'
      ).then((m) => m.DmExpenseClassModule),
    canLoad: [AppRoutingGuard],
  },
  {
    path: 'DmEmployee',
    loadChildren: () =>
      import(
        './pages/dictionary/dm-employee/dm-employee-list/dm-employee-list.module'
      ).then((m) => m.DmEmployeeListModule),
    canLoad: [AppRoutingGuard],
  },
  {
    path: 'DmDepartment',
    loadChildren: () =>
      import(
        './pages/dictionary/dm-department/dm-department-list/dm-department-list.module'
      ).then((m) => m.DmDepartmentListModule),
    canLoad: [AppRoutingGuard],
  },
  {
    path: 'DmUnit',
    loadChildren: () =>
      import(
        './pages/dictionary/dm-unit/dm-unit-list/dm-unit-list.module'
      ).then((m) => m.DmUnitListModule),
    canLoad: [AppRoutingGuard],
  },
  {
    path: 'DmVatTax',
    loadChildren: () =>
      import(
        './pages/dictionary/dm-vat-tax/dm-vat-tax-list/dm-vat-tax-list.module'
      ).then((m) => m.DmVatTaxListModule),
    canLoad: [AppRoutingGuard],
  },
  {
    path: 'DmItemClass',
    loadChildren: () =>
      import(
        './pages/dictionary/dm-item-class/dm-item-class-list/dm-item-class-list.module'
      ).then((m) => m.DmItemClassListModule),
    canLoad: [AppRoutingGuard],
  },
  {
    path: 'DmWarehouse',
    loadChildren: () =>
      import(
        './pages/dictionary/dm-warehouse/dm-warehouse-list/dm-warehouse-list.module'
      ).then((m) => m.DmWarehouseListModule),
    canLoad: [AppRoutingGuard],
  },
  {
    path: 'DmItemOp',
    loadChildren: () =>
      import(
        './pages/dictionary/dm-item-op/dm-item-op-list/dm-item-op-list.module'
      ).then((m) => m.DmItemOpListModule),
    canLoad: [AppRoutingGuard],
  },
  {
    path: 'DmBank',
    loadChildren: () =>
      import(
        './pages/dictionary/dm-bank/dm-bank-list/dm-bank-list.module'
      ).then((m) => m.DmBankListModule),
    canLoad: [AppRoutingGuard],
  },
  {
    path: 'DmEstimateType',
    loadChildren: () =>
      import(
        './pages/dictionary/dm-estimate-type/dm-estimate-type-list/dm-estimate-type-list.module'
      ).then((m) => m.DmEstimateTypeListModule),
    canLoad: [AppRoutingGuard],
  },
  {
    path: 'DmCapitalSource',
    loadChildren: () =>
      import(
        './pages/dictionary/dm-capital-source/dm-capital-source-list/dm-capital-source-list.module'
      ).then((m) => m.DmCapitalSourceListModule),
    canLoad: [AppRoutingGuard],
  },
  {
    path: 'DmAgent',
    loadChildren: () =>
      import(
        './pages/dictionary/dm-agent/dm-agent-list/dm-agent-list.module'
      ).then((m) => m.DmAgentListModule),
    canLoad: [AppRoutingGuard],
  },
  {
    path: 'DmSecurityClass',
    loadChildren: () =>
      import(
        './pages/dictionary/dm-security-class/dm-security-class-list/dm-security-class-list.module'
      ).then((m) => m.DmSecurityClassListModule),
    canLoad: [AppRoutingGuard],
  },
  {
    path: 'DmSecurityType',
    loadChildren: () =>
      import(
        './pages/dictionary/dm-security-type/dm-security-type-list/dm-security-type-list.module'
      ).then((m) => m.DmSecurityTypeListModule),
    canLoad: [AppRoutingGuard],
  },
  {
    path: 'DmSecurity',
    loadChildren: () =>
      import(
        './pages/dictionary/dm-security/dm-security-list/dm-security-list.module'
      ).then((m) => m.DmSecurityTypeListModule),
    canLoad: [AppRoutingGuard],
  },
  {
    path: 'DmAdvanceLimit',
    loadChildren: () =>
      import(
        './pages/dictionary/dm-advance-limit/dm-advance-limit-edit-list.module'
      ).then((m) => m.DmAdvanceLimitEditListModule),
    canLoad: [AppRoutingGuard],
  },
  {
    path: 'DmVatPurchase',
    loadChildren: () => import(
      './pages/dictionary/dm-vat-purchase/dm-vat-purchase-list/dm-vat-purchase-list.module'
    ).then((m) => m.DmVatPurchaseListModule),
    canLoad: [AppRoutingGuard],
  },
  {
    path: 'DmCashbankLimit',
    loadChildren: () => import(
      './pages/dictionary/dm-cashbank-limit/dm-cashbank-limit-list/dm-cashbank-limit-list.module'
    ).then((m) => m.DmCashbankLimitListModule),
    canLoad: [AppRoutingGuard],
  },
  {
    path: 'DmJob',
    loadChildren: () => import(
      './pages/dictionary/dm-job/dm-job-list/dm-job-list.module'
    ).then((m) => m.DmJobListModule),
    canLoad: [AppRoutingGuard],
  },
  //#endregion

  //#region system
  {
    path: 'SecUser',
    loadChildren: () =>
      import('./pages/system/sec-user/sec-user-list/sec-user-list.module').then(
        (m) => m.SecUserListModule
      ),
    canLoad: [AppRoutingGuard],
  },
  {
    path: 'DmOrganizarion',
    loadChildren: () =>
      import(
        './pages/system/dm-organizarion/dm-organizarion-list/dm-organizarion-list.module'
      ).then((m) => m.DmOrganizarionListModule),
    canLoad: [AppRoutingGuard],
  },
  {
    path: 'change-password',
    loadChildren: () =>
      import('./pages/system/change-password/change-password.module').then(
        (m) => m.ChangePasswordModule
      ),
    canLoad: [AppRoutingGuard],
  },
  {
    path: 'change-organizarion',
    loadChildren: () =>
      import(
        './pages/system/change-organizarion/change-organizarion.module'
      ).then((m) => m.ChangeOrganizarionModule),
  },
  {
    path: 'SysResource',
    loadChildren: () =>
      import(
        './pages/system/sys-resource/sys-resource-list/sys-resource-list.module'
      ).then((m) => m.SysResourceListModule),
    canLoad: [AppRoutingGuard],
  },
  {
    path: 'SysMenu',
    loadChildren: () =>
      import('./pages/system/sys-menu/sys-menu-list/sys-menu-list.module').then(
        (m) => m.SysMenuListModule
      ),
    canLoad: [AppRoutingGuard],
  },
  {
    path: 'SysSystemVar',
    loadChildren: () =>
      import(
        './pages/system/sys-systemvar/sys-systemvar-list/sys-systemvar-list.module'
      ).then((m) => m.SysSystemVarListModule),
    canLoad: [AppRoutingGuard],
  },

  {
    path: 'SysTable',
    loadChildren: () =>
      import(
        './pages/system/sys-table/sys-table-list/sys-table-list.module'
      ).then((m) => m.SysTableListModule),
    canLoad: [AppRoutingGuard],
  },
  //#endregion

  {
    path: 'DmContractLimit',
    loadChildren: () =>
      import(
        './pages/dictionary/dm-contract-limit/dm-contract-limit-list/dm-contract-limit-list.module'
      ).then((m) => m.DmContractLimitListModule),
    canLoad: [AppRoutingGuard],
  },
  //#endregion

  {
    path: 'page-not-found',
    loadChildren: () =>
      import('./pages/page-not-found/page-not-found.module').then(
        (m) => m.PageNotFoundModule
      ),
    canLoad: [AppRoutingGuard],
  },

  {
    path: '**',
    loadChildren: () =>
      import('./pages/page-not-found/page-not-found.module').then(
        (m) => m.PageNotFoundModule
      ),
    canLoad: [AppRoutingGuard],
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule { }
