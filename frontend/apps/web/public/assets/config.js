(function (window) {
  const configPropName = 'MyBudgetConfig';

  window[configPropName] = window['MyBudgetConfig'] || {};
  window[configPropName].backendUrl = 'https://localhost:48081';
  window[configPropName].oidcAuthority = 'https://mybudget.identity.app:8081';
  window[configPropName].oidcClientId = 'MyBudget.Frontend';
  window[configPropName].oidcScope = 'openid profile email my_budget.api';
})(this);
