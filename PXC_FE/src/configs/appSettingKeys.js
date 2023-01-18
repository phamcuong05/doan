var appSettingKeys = {
  defaultAPI: "http://192.168.30.8:3838",
  //defaultAPI: 'http://localhost:20299/'
};

var propKeys = Object.keys(appSettingKeys);
document.appSettingKeys = {};
for (let i = 0; i < propKeys.length; i++) {
  var key = propKeys[i];
  document.appSettingKeys[key] = appSettingKeys[key];
}
