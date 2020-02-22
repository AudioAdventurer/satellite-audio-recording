import BaseDao from "./BaseDao.js"

export default class PersonDao extends BaseDao {

  getPerson(personId) {
    return this.read(`/people/${personId}`);
  }

  getSelf() {
    return this.read(`/people/self`);
  }

  getSelfAccess() {
    return this.read(`/access/self`);
  }

  saveProfile(profile) {
    return this.write(`/people/self`, profile);
  }

  getParticipantsWithAccess(projectId) {
    return this.read(`/projects/${projectId}/ui/people`);
  }

  getParticipantWithAccess(projectId, personId) {
    return this.read(`/projects/${projectId}/ui/people/${personId}`);
  }

  getAvailablePeople(projectId) {
    return this.read(`/projects/${projectId}/people/available`);
  }

  saveParticipantAccess(projectId, projectAccess) {
    return this.write(`/projects/${projectId}/people`, projectAccess);
  }
}