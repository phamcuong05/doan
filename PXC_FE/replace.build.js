var replace = require("replace-in-file");
var buildVersion = new Date().getTime();// process.argv[2];
const options = {
  files: "src/environments/environment.prod.ts",
   from: /version:.*/gm,
  to: `version: '${buildVersion}',`,
};

try {
  replace.sync(options);
  console.log("Build version set: " + buildVersion);
} catch (error) {
  console.error("Error occurred:", error);
}