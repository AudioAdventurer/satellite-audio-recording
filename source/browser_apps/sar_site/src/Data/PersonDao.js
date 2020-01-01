import BaseDao from "./BaseDao.js"

export default class PersonDao extends BaseDao {

  getParticipants(projectId) {
    return this.read(`/projects/${projectId}/ui/people`);
  }

}