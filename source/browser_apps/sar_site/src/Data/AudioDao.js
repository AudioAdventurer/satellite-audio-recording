import BaseDao from "./BaseDao.js"

export default class AudioDao extends BaseDao {

  getAudioSummaryForScenes(projectId) {
    return this.read(`/projects/${projectId}/audiosummary/scenes`);
  }

  getAudioSummaryForCharacters(projectId) {
    return this.read(`/projects/${projectId}/audiosummary/characters`);
  }

}