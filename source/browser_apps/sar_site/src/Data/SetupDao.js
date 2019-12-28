import BaseDao from "./BaseDao.js"

export default class SetupDao extends BaseDao {

  isSetup() {
    return this.read("/setup");
  }

  setupService(email, password, givenName, familyName, projectName) {
    let body = {
      OwnerEmail: email,
      OwnerPassword: password,
      OwnerGivenName: givenName,
      OwnerFamilyName: familyName,
      InitialProjectName: projectName
    };

    return this.write('/setup', body);
  }
}