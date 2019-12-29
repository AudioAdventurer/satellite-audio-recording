import Environment from "../env.js";
import axios from 'axios';
import AuthDao from "../Data/AuthDao";
import SetupDao from "../Data/SetupDao";
import ProjectDao from "../Data/ProjectDao";

class SarService {
  static JWT = "";

  //Projects
  static getProjects() {
    const dao = new ProjectDao(Environment.BASE_URL);
    return dao.getProjects();
  }

  static getProject(id) {
    const dao = new ProjectDao(Environment.BASE_URL);
    return dao.getProject(id);
  }

  static saveProject(proj) {
    const dao = new ProjectDao(Environment.BASE_URL);
    return dao.saveProject(proj);
  }


  //Authentication and setup
  static login(username, password) {
    const dao = new AuthDao(Environment.BASE_URL);
    return dao.login(username, password);
  }

  static isSetup() {
    const dao = new SetupDao(Environment.BASE_URL);
    return dao.isSetup();
  }

  static setupService(email, password, givenName, familyName, projectName) {
    const dao = new SetupDao(Environment.BASE_URL);
    return dao.setupService(email, password, givenName, familyName, projectName);
  }

  static setJwt(jwt) {
    SarService.JWT = jwt;
    axios.defaults.headers.common['Authorization'] = 'Bearer ' + SarService.JWT;
  }

  static setCookie(cname, cvalue, exdays) {
    let d = new Date();
    d.setTime(d.getTime() + (exdays*24*60*60*1000));
    let expires = "expires="+ d.toUTCString();
    document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
  }

  static getCookie(cname) {
    let name = cname + "=";
    let decodedCookie = decodeURIComponent(document.cookie);
    let ca = decodedCookie.split(';');
    for(let i = 0; i <ca.length; i++) {
      let c = ca[i];
      while (c.charAt(0) === ' ') {
        c = c.substring(1);
      }
      if (c.indexOf(name) === 0) {
        return c.substring(name.length, c.length);
      }
    }
    return "";
  }


}

export default SarService;