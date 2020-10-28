import Vue from 'vue'
import App from './App.vue'
import './registerServiceWorker'
import router from './router'
import store from './store'
import vuetify from './plugins/vuetify';
import CommonFunctions from './libs/common-functions.js';
import ErrorDialog from '@/components/Common/Overlays/Dialogs/Error.vue'

import 'vue-datetime/dist/vue-datetime.css'

// Mixins
import ServiceCaller from './libs/service-caller.js';
Vue.use(ServiceCaller);
Vue.use(CommonFunctions)
Vue.use(ErrorDialog)
Vue.component('ErrorDialog', ErrorDialog)

Vue.config.productionTip = false

new Vue({
  router,
  store,
  vuetify,
  render: function (h) { return h(App) }
}).$mount('#app')
