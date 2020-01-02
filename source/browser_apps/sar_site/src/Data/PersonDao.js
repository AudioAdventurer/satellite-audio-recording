import BaseDao from "./BaseDao.js"

export default class PersonDao extends BaseDao {

  getParticipantsWithAccess(projectId) {
    return this.read(`/projects/${projectId}/ui/people`);
  }

  getParticipantWithAccess(projectId, personId) {
    return this.read(`/projects/${projectId}/ui/people/${personId}`);
  }

  saveParticipant(person) {
    return this.write(`/projects/${person.ProjectId}/ui/people`)
  }
}