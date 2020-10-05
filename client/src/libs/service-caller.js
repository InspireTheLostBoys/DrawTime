let Axios = require("axios");

class AAxios {
    constructor(address) {
        this.Address = address;
    }

    /**
     * get
     * @param {*} route 
     * @param {*} body 
     * @param {*} useAuth - defaulted to true, can override on requese
     */
    get(route, body) {
        let self = this;

        return new Promise((resolve, reject) => {
            Axios.get(self.Address + route, body)
                .then(r => {
                    resolve(r);
                })
                .catch(e => {
                    reject(e);
                })
        })
    }

    /**
     * post
     * @param {*} route 
     * @param {*} body 
     * @param {*} useAuth - defaulted to true, can override on request
     */
    post(route, body, config) {
        let self = this;
        return new Promise((resolve, reject) => {
            try {
                let token = localStorage.getItem("access_token");
                Axios.defaults.headers.common['Authorization'] = "bearer " + token;

                Axios.post(self.Address + route, body, config)
                    .then(r => {
                        delete Axios.defaults.headers.common["Authorization"];
                        resolve(r);
                    })
                    .catch(e => {
                        delete Axios.defaults.headers.common["Authorization"];
                        reject(e);
                    })

            } catch (error) {
                reject(error)
            }
        })
    }

    /**
     * postProgress
     * @param {*} route 
     * @param {*} body 
     * @param {*} useAuth - defaulted to true, can override on request
     */
    postProgress(route, body, config) {
        let self = this;
        return new Promise((resolve, reject) => {
            try {
                let token = localStorage.getItem("access_token");
                Axios.defaults.headers.common['Authorization'] = "bearer " + token;

                Axios.post(self.Address + route, body, config)
                    .then(r => {
                        delete Axios.defaults.headers.common["Authorization"];
                        resolve(r);
                    })
                    .catch(e => {
                        delete Axios.defaults.headers.common["Authorization"];
                        reject(e);
                    })

            } catch (error) {
                reject(error)
            }
        })
    }

    /**
     * put
     * @param {*} route 
     * @param {*} body 
     * @param {*} useAuth - defaulted to true, can override on requese
     */
    put(route, body) {
        let self = this;

        return new Promise((resolve, reject) => {
            let token = localStorage.getItem("access_token");
            Axios.defaults.headers.common['Authorization'] = "bearer " + token;

            Axios.put(self.Address + route, body)
                .then(r => {
                    delete Axios.defaults.headers.common["Authorization"];
                    resolve(r);
                })
                .catch(e => {
                    delete Axios.defaults.headers.common["Authorization"];
                    reject(e);
                })
        })

    }

    /**
     * delete
     * @param {*} route 
     * @param {*} body 
     * @param {*} useAuth - defaulted to true, can override on requese
     */
    delete(route, body) {
        let self = this;

        return new Promise((resolve, reject) => {
            let token = localStorage.getItem("access_token");

            Axios.defaults.headers.common['Authorization'] = "bearer " + token;

            Axios.delete(self.Address + route, body)
                .then(r => {
                    delete Axios.defaults.headers.common["Authorization"];
                    resolve(r);
                })
                .catch(e => {
                    delete Axios.defaults.headers.common["Authorization"];
                    reject(e);
                })
        })
    }
}

export default {
    install(Vue) {
        let caller = new AAxios(process.env.VUE_APP_API);

        Vue.prototype.get = (route, body) => {
            return caller.get(route, body);
        };

        Vue.prototype.post = (route, body, config) => {
            return caller.post(route, body, config);
        };

        Vue.prototype.postProgress = (route, body, config) => {
            return caller.postProgress(route, body, config);
        };

        Vue.prototype.put = (route, body) => {
            return caller.put(route, body);
        };

        Vue.prototype.delete = (route, body) => {
            return caller.delete(route, body);
        };
    }
}