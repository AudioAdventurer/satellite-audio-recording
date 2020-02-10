import BaseDao from "./BaseDao.js"

export default class UserDao extends BaseDao {

  getUsers() {
    return this.read(`/users`);
  }

  createUser(newUser) {
    return this.write('/users', newUser);
  }
}