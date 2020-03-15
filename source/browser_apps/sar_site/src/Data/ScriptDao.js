import BaseDao from "./BaseDao.js"

export default class ScriptDao extends BaseDao {

  getLines(projectId, start, end) {
    return this.read(`/projects/${projectId}/script/${start}/${end}`)
  }

  getNextLinesByCharacter(projectId, characterId, start, limit) {
    return this.read(`/projects/${projectId}/script/${characterId}/next/${start}/limit/${limit}`);
  }

  getPreviousLinesByCharacter(projectId, characterId, start, limit) {
    return this.read(`/projects/${projectId}/script/${characterId}/previous/${start}/limit/${limit}`);
  }

  getDialogContext(projectId, dialogId) {
    return this.read(`/projects/${projectId}/script/${dialogId}/context`);
  }

}