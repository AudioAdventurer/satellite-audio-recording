import BaseDao from "./BaseDao.js"

export default class ProjectDao extends BaseDao {

  getProjects() {
    return this.read("/projects");
  }

  getProject(id) {
    return this.read(`/projects/${id}`);
  }

  saveProject(proj) {
    return this.write("/projects", proj);
  }


}