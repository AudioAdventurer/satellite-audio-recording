import BaseDao from "./BaseDao.js"

export default class RecordingDao extends BaseDao {
  saveRecording(projectId, dialogId, formData) {
    return this.writeFile(`/projects/${projectId}/script/${dialogId}/recording`, formData);
  }

  getRecording(projectId, recordingId) {
    return this.readFile(`/projects/${projectId}/recordings/${recordingId}`);
  }

  getRecordings(projectId, dialogId) {
    return this.read(`/projects/${projectId}/script/${dialogId}`);
  }

  getRecordingUrl(projectId, recordingId){
    return `${super.baseUrl}/projects/${projectId}/recordings/${recordingId}`;
  }
}