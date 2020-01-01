import BaseDao from "./BaseDao.js"

export default class PersonDao extends BaseDao {

  getCharactersWithActors(projectId) {
    return this.read(`/projects/${projectId}/ui/characters`);
  }

  getCharacter(projectId, characterId) {
    return this.read(`/projects/${projectId}/characters/${characterId}`);
  }

  saveCharacter(character) {
    return this.write(`/projects/${character.ProjectId}/characters`, character);
  }
}